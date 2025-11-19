using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace XTStyle.Controls
{
    public class ModalDialog : ContentControl
    {
        private static Grid _overlayContainer;
        private Border _overlay;
        private Button _closeButton;

        static ModalDialog()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ModalDialog),
                new FrameworkPropertyMetadata(typeof(ModalDialog)));

            CreateDefaultStyle();
        }

        private static void CreateDefaultStyle()
        {
            var style = new Style(typeof(ModalDialog));
            style.Setters.Add(new Setter(HorizontalAlignmentProperty, HorizontalAlignment.Center));
            style.Setters.Add(new Setter(VerticalAlignmentProperty, VerticalAlignment.Center));

            // Create template
            var template = new ControlTemplate(typeof(ModalDialog));

            var outerBorder = new FrameworkElementFactory(typeof(Border));
            outerBorder.SetValue(Border.BackgroundProperty, Brushes.White);
            outerBorder.SetValue(Border.CornerRadiusProperty, new CornerRadius(12));
            outerBorder.SetValue(Border.MaxHeightProperty, 600.0);
            outerBorder.SetValue(Border.WidthProperty, 500.0);

            var dropShadow = new DropShadowEffect
            {
                Color = Colors.Black,
                Opacity = 0.3,
                BlurRadius = 20,
                ShadowDepth = 8
            };
            outerBorder.SetValue(Border.EffectProperty, dropShadow);

            var mainGrid = new FrameworkElementFactory(typeof(Grid));

            // Row definitions
            var row1 = new FrameworkElementFactory(typeof(RowDefinition));
            row1.SetValue(RowDefinition.HeightProperty, GridLength.Auto);
            var row2 = new FrameworkElementFactory(typeof(RowDefinition));
            row2.SetValue(RowDefinition.HeightProperty, new GridLength(1, GridUnitType.Star));

            mainGrid.AppendChild(row1);
            mainGrid.AppendChild(row2);

            // Header
            var headerBorder = new FrameworkElementFactory(typeof(Border));
            headerBorder.SetValue(Grid.RowProperty, 0);
            headerBorder.SetValue(Border.BorderThicknessProperty, new Thickness(0, 0, 0, 1));
            headerBorder.SetValue(Border.CornerRadiusProperty, new CornerRadius(12, 12, 0, 0));
            headerBorder.SetValue(Border.PaddingProperty, new Thickness(20, 16, 20, 16));
            headerBorder.SetResourceReference(Border.BackgroundProperty, "BackgroundBrush");
            headerBorder.SetResourceReference(Border.BorderBrushProperty, "BorderBrush");

            var headerGrid = new FrameworkElementFactory(typeof(Grid));

            // Title
            var titleText = new FrameworkElementFactory(typeof(TextBlock));
            titleText.SetValue(TextBlock.FontSizeProperty, 18.0);
            titleText.SetValue(TextBlock.FontWeightProperty, FontWeights.SemiBold);
            titleText.SetResourceReference(TextBlock.ForegroundProperty, "TextPrimaryBrush");
            titleText.SetBinding(TextBlock.TextProperty, new System.Windows.Data.Binding("Title")
            {
                RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent)
            });

            headerGrid.AppendChild(titleText);

            // Close button
            var closeBtn = new FrameworkElementFactory(typeof(Button));
            closeBtn.Name = "PART_CloseButton";
            closeBtn.SetValue(Button.ContentProperty, "✕");
            closeBtn.SetValue(Button.WidthProperty, 32.0);
            closeBtn.SetValue(Button.HeightProperty, 32.0);
            closeBtn.SetValue(Button.FontSizeProperty, 16.0);
            closeBtn.SetValue(Button.BackgroundProperty, Brushes.Transparent);
            closeBtn.SetValue(Button.BorderThicknessProperty, new Thickness(0));
            closeBtn.SetValue(Button.HorizontalAlignmentProperty, HorizontalAlignment.Right);
            closeBtn.SetValue(Button.CursorProperty, Cursors.Hand);
            closeBtn.SetResourceReference(Button.ForegroundProperty, "TextSecondaryBrush");

            headerGrid.AppendChild(closeBtn);
            headerBorder.AppendChild(headerGrid);
            mainGrid.AppendChild(headerBorder);

            // Content
            var contentScroll = new FrameworkElementFactory(typeof(ScrollViewer));
            contentScroll.SetValue(Grid.RowProperty, 1);
            contentScroll.SetValue(ScrollViewer.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Auto);
            contentScroll.SetValue(ScrollViewer.PaddingProperty, new Thickness(20));

            var contentPresenter = new FrameworkElementFactory(typeof(ContentPresenter));
            contentScroll.AppendChild(contentPresenter);

            mainGrid.AppendChild(contentScroll);
            outerBorder.AppendChild(mainGrid);

            template.VisualTree = outerBorder;

            style.Setters.Add(new Setter(TemplateProperty, template));

            Application.Current.Resources[typeof(ModalDialog)] = style;
        }

        // Title Property
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(ModalDialog),
                new PropertyMetadata("Dialog"));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // DialogWidth Property
        public static readonly DependencyProperty DialogWidthProperty =
            DependencyProperty.Register("DialogWidth", typeof(double), typeof(ModalDialog),
                new PropertyMetadata(500.0));

        public double DialogWidth
        {
            get { return (double)GetValue(DialogWidthProperty); }
            set { SetValue(DialogWidthProperty, value); }
        }

        public ModalDialog()
        {
            this.Loaded += ModalDialog_Loaded;
        }

        private void ModalDialog_Loaded(object sender, RoutedEventArgs e)
        {
            _closeButton = GetTemplateChild("PART_CloseButton") as Button;
            if (_closeButton != null)
            {
                _closeButton.Click += (s, ev) => Close();
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _closeButton = GetTemplateChild("PART_CloseButton") as Button;
            if (_closeButton != null)
            {
                _closeButton.Click += (s, e) => Close();
            }
        }

        // Static Show Method
        public static ModalDialog Show(string title, UIElement content, double width = 500)
        {
            var dialog = new ModalDialog
            {
                Title = title,
                Content = content,
                DialogWidth = width
            };

            dialog.ShowDialog();
            return dialog;
        }

        private void ShowDialog()
        {
            InitializeOverlay();

            _overlay = new Border
            {
                Background = new SolidColorBrush(Color.FromArgb(128, 0, 0, 0)),
                Child = this,
                Opacity = 0
            };

            _overlayContainer.Children.Add(_overlay);

            var fadeIn = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(200)
            };

            _overlay.BeginAnimation(OpacityProperty, fadeIn);
        }

        public void Close()
        {
            if (_overlay == null) return;

            var fadeOut = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(200)
            };

            fadeOut.Completed += (s, e) =>
            {
                if (_overlayContainer != null && _overlay != null)
                {
                    _overlayContainer.Children.Remove(_overlay);
                }
            };

            _overlay.BeginAnimation(OpacityProperty, fadeOut);
        }

        private static void InitializeOverlay()
        {
            if (_overlayContainer == null)
            {
                var mainWindow = Application.Current.MainWindow;
                if (mainWindow != null)
                {
                    var grid = mainWindow.Content as Grid;
                    if (grid == null)
                    {
                        var existingContent = mainWindow.Content as UIElement;
                        grid = new Grid();
                        mainWindow.Content = grid;
                        if (existingContent != null)
                        {
                            grid.Children.Add(existingContent);
                        }
                    }

                    _overlayContainer = grid;
                }
            }
        }
    }
}