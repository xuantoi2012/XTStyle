using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace XTStyle.Themes
{
    public partial class ModernMessageBox : Window
    {
        public MessageBoxResult Result { get; private set; } = MessageBoxResult.None;

        private ModernMessageBox(string message, string title, MessageBoxButton buttons, MessageBoxImage icon)
        {
            InitializeComponent();

            // Set title
            TitleText.Text = title;
            MessageText.Text = message;

            // Set icon
            SetIcon(icon);

            // Set buttons
            SetButtons(buttons);
        }

        private void SetIcon(MessageBoxImage icon)
        {
            switch (icon)
            {
                case MessageBoxImage.Information:
                    IconBorder.Background = new SolidColorBrush(Color.FromRgb(59, 130, 246)); // Blue
                    IconPath.Data = (Geometry)FindResource("InfoIcon");
                    break;

                case MessageBoxImage.Question:
                    IconBorder.Background = new SolidColorBrush(Color.FromRgb(139, 92, 246)); // Purple
                    IconPath.Data = (Geometry)FindResource("QuestionIcon");
                    break;

                case MessageBoxImage.Warning:
                    IconBorder.Background = new SolidColorBrush(Color.FromRgb(245, 158, 11)); // Orange
                    IconPath.Data = (Geometry)FindResource("WarningIcon");
                    break;

                case MessageBoxImage.Error:
                    IconBorder.Background = new SolidColorBrush(Color.FromRgb(239, 68, 68)); // Red
                    IconPath.Data = (Geometry)FindResource("ErrorIcon");
                    break;

                default: // None - Success style
                    IconBorder.Background = new SolidColorBrush(Color.FromRgb(34, 197, 94)); // Green
                    IconPath.Data = (Geometry)FindResource("SuccessIcon");
                    break;
            }
        }

        private void SetButtons(MessageBoxButton buttons)
        {
            ButtonPanel.Children.Clear();

            switch (buttons)
            {
                case MessageBoxButton.OK:
                    AddButton("OK", MessageBoxResult.OK, true);
                    break;

                case MessageBoxButton.OKCancel:
                    // OK -> Hủy
                    AddButton("OK", MessageBoxResult.OK, true);
                    AddButton("Hủy", MessageBoxResult.Cancel, false);
                    break;

                case MessageBoxButton.YesNo:
                    // Có -> Không
                    AddButton("Có", MessageBoxResult.Yes, true);
                    AddButton("Không", MessageBoxResult.No, false);
                    break;

                case MessageBoxButton.YesNoCancel:
                    // Có -> Không -> Hủy
                    AddButton("Có", MessageBoxResult.Yes, true);
                    AddButton("Không", MessageBoxResult.No, false);
                    AddButton("Hủy", MessageBoxResult.Cancel, false);
                    break;
            }
        }

        private void AddButton(string text, MessageBoxResult result, bool isPrimary)
        {
            var button = new Button
            {
                Content = text,
                Width = 80,
                Height = 30,
                Margin = new Thickness(10, 0, 0, 0),
                Cursor = System.Windows.Input.Cursors.Hand,
                FontSize = 12,
                FontWeight = FontWeights.Medium
            };

            // Style
            var style = new Style(typeof(Button));

            if (isPrimary)
            {
                // ✅ Primary button - Màu #0063A3
                style.Setters.Add(new Setter(BackgroundProperty, new SolidColorBrush(Color.FromRgb(0, 154, 217))));
                style.Setters.Add(new Setter(ForegroundProperty, Brushes.White));
            }
            else
            {
                // Secondary button
                style.Setters.Add(new Setter(BackgroundProperty, Brushes.White));
                style.Setters.Add(new Setter(ForegroundProperty, new SolidColorBrush(Color.FromRgb(75, 85, 99))));
                style.Setters.Add(new Setter(BorderBrushProperty, new SolidColorBrush(Color.FromRgb(209, 213, 219))));
                style.Setters.Add(new Setter(BorderThicknessProperty, new Thickness(1)));
            }

            // Template
            var template = new ControlTemplate(typeof(Button));
            var borderFactory = new FrameworkElementFactory(typeof(Border));
            borderFactory.Name = "border";
            borderFactory.SetValue(Border.BackgroundProperty, new TemplateBindingExtension(BackgroundProperty));
            borderFactory.SetValue(Border.BorderBrushProperty, new TemplateBindingExtension(BorderBrushProperty));
            borderFactory.SetValue(Border.BorderThicknessProperty, new TemplateBindingExtension(BorderThicknessProperty));
            borderFactory.SetValue(Border.CornerRadiusProperty, new CornerRadius(8));

            var contentFactory = new FrameworkElementFactory(typeof(ContentPresenter));
            contentFactory.SetValue(ContentPresenter.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            contentFactory.SetValue(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center);
            borderFactory.AppendChild(contentFactory);

            template.VisualTree = borderFactory;

            // Triggers
            var hoverTrigger = new Trigger { Property = IsMouseOverProperty, Value = true };
            if (isPrimary)
            {
                // ✅ Hover color - Tối hơn một chút: #004F85
                hoverTrigger.Setters.Add(new Setter(BackgroundProperty, new SolidColorBrush(Color.FromRgb(0, 99, 163)), "border"));
            }
            else
            {
                hoverTrigger.Setters.Add(new Setter(BackgroundProperty, new SolidColorBrush(Color.FromRgb(249, 250, 251)), "border"));
            }
            template.Triggers.Add(hoverTrigger);

            style.Setters.Add(new Setter(TemplateProperty, template));
            button.Style = style;

            // Click event
            button.Click += (s, e) =>
            {
                Result = result;
                Close();
            };

            ButtonPanel.Children.Add(button);
        }

        // Static Show methods
        public static MessageBoxResult Show(string message)
        {
            return Show(message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static MessageBoxResult Show(string message, string title)
        {
            return Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static MessageBoxResult Show(string message, string title, MessageBoxButton buttons)
        {
            return Show(message, title, buttons, MessageBoxImage.Information);
        }

        public static MessageBoxResult Show(string message, string title, MessageBoxButton buttons, MessageBoxImage icon)
        {
            var msgBox = new ModernMessageBox(message, title, buttons, icon)
            {
                Owner = Application.Current.MainWindow
            };

            msgBox.ShowDialog();
            return msgBox.Result;
        }
    }
}