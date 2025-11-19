using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Threading;

namespace XTStyle.Controls
{
    public class ToastNotification : Control
    {
        private static Panel _container;
        private DispatcherTimer _timer;

        static ToastNotification()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToastNotification),
                new FrameworkPropertyMetadata(typeof(ToastNotification)));

            CreateDefaultStyle();
        }

        private static void CreateDefaultStyle()
        {
            var style = new Style(typeof(ToastNotification));
            style.Setters.Add(new Setter(WidthProperty, 320.0));
            style.Setters.Add(new Setter(MarginProperty, new Thickness(0, 0, 0, 10)));

            // Create template
            var template = new ControlTemplate(typeof(ToastNotification));

            var border = new FrameworkElementFactory(typeof(Border));
            border.SetValue(Border.CornerRadiusProperty, new CornerRadius(8));
            border.SetValue(Border.PaddingProperty, new Thickness(16, 12, 16, 12));
            border.SetBinding(Border.BackgroundProperty, new System.Windows.Data.Binding("BackgroundColor")
            {
                RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent)
            });

            var dropShadow = new DropShadowEffect
            {
                Color = Colors.Black,
                Opacity = 0.3,
                BlurRadius = 16,
                ShadowDepth = 4
            };
            border.SetValue(Border.EffectProperty, dropShadow);

            var grid = new FrameworkElementFactory(typeof(Grid));

            // Column definitions
            var col1 = new FrameworkElementFactory(typeof(ColumnDefinition));
            col1.SetValue(ColumnDefinition.WidthProperty, GridLength.Auto);
            var col2 = new FrameworkElementFactory(typeof(ColumnDefinition));
            col2.SetValue(ColumnDefinition.WidthProperty, new GridLength(1, GridUnitType.Star));

            grid.AppendChild(col1);
            grid.AppendChild(col2);

            // Icon border
            var iconBorder = new FrameworkElementFactory(typeof(Border));
            iconBorder.SetValue(Grid.ColumnProperty, 0);
            iconBorder.SetValue(Border.WidthProperty, 32.0);
            iconBorder.SetValue(Border.HeightProperty, 32.0);
            iconBorder.SetValue(Border.BackgroundProperty, Brushes.White);
            iconBorder.SetValue(Border.CornerRadiusProperty, new CornerRadius(16));
            iconBorder.SetValue(Border.MarginProperty, new Thickness(0, 0, 12, 0));

            // Icon text
            var iconText = new FrameworkElementFactory(typeof(TextBlock));
            iconText.SetValue(TextBlock.FontSizeProperty, 18.0);
            iconText.SetValue(TextBlock.FontWeightProperty, FontWeights.Bold);
            iconText.SetValue(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            iconText.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
            iconText.SetBinding(TextBlock.TextProperty, new System.Windows.Data.Binding("Icon")
            {
                RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent)
            });
            iconText.SetBinding(TextBlock.ForegroundProperty, new System.Windows.Data.Binding("BackgroundColor")
            {
                RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent)
            });

            iconBorder.AppendChild(iconText);
            grid.AppendChild(iconBorder);

            // Message text
            var messageText = new FrameworkElementFactory(typeof(TextBlock));
            messageText.SetValue(Grid.ColumnProperty, 1);
            messageText.SetValue(TextBlock.ForegroundProperty, Brushes.White);
            messageText.SetValue(TextBlock.FontSizeProperty, 13.0);
            messageText.SetValue(TextBlock.TextWrappingProperty, TextWrapping.Wrap);
            messageText.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
            messageText.SetBinding(TextBlock.TextProperty, new System.Windows.Data.Binding("Message")
            {
                RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent)
            });

            grid.AppendChild(messageText);
            border.AppendChild(grid);

            template.VisualTree = border;

            style.Setters.Add(new Setter(TemplateProperty, template));

            Application.Current.Resources[typeof(ToastNotification)] = style;
        }

        // Message Property
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(ToastNotification),
                new PropertyMetadata(""));

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        // Type Property
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(ToastType), typeof(ToastNotification),
                new PropertyMetadata(ToastType.Info));

        public ToastType Type
        {
            get { return (ToastType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        // Duration Property
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(int), typeof(ToastNotification),
                new PropertyMetadata(3));

        public int Duration
        {
            get { return (int)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        // Icon Property
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(string), typeof(ToastNotification),
                new PropertyMetadata(""));

        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            private set { SetValue(IconProperty, value); }
        }

        // BackgroundColor Property
        public static readonly DependencyProperty BackgroundColorProperty =
            DependencyProperty.Register("BackgroundColor", typeof(Brush), typeof(ToastNotification),
                new PropertyMetadata(Brushes.White));

        public Brush BackgroundColor
        {
            get { return (Brush)GetValue(BackgroundColorProperty); }
            private set { SetValue(BackgroundColorProperty, value); }
        }

        // Static Show Methods
        public static void Show(string message, ToastType type = ToastType.Info, int duration = 3)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (_container == null)
                {
                    InitializeContainer();
                }

                var toast = new ToastNotification
                {
                    Message = message,
                    Type = type,
                    Duration = duration
                };

                toast.UpdateAppearance();
                toast.ShowToast();
            });
        }

        public static void Success(string message, int duration = 3)
        {
            Show(message, ToastType.Success, duration);
        }

        public static void Error(string message, int duration = 3)
        {
            Show(message, ToastType.Error, duration);
        }

        public static void Warning(string message, int duration = 3)
        {
            Show(message, ToastType.Warning, duration);
        }

        public static void Info(string message, int duration = 3)
        {
            Show(message, ToastType.Info, duration);
        }

        private static void InitializeContainer()
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

                _container = new StackPanel
                {
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Margin = new Thickness(0, 20, 20, 0),
                    IsHitTestVisible = false
                };

                Grid.SetRowSpan(_container, int.MaxValue);
                Grid.SetColumnSpan(_container, int.MaxValue);
                Panel.SetZIndex(_container, int.MaxValue);

                grid.Children.Add(_container);
            }
        }

        private void UpdateAppearance()
        {
            switch (Type)
            {
                case ToastType.Success:
                    Icon = "✓";
                    BackgroundColor = new SolidColorBrush(Color.FromRgb(16, 185, 129));
                    break;
                case ToastType.Error:
                    Icon = "✕";
                    BackgroundColor = new SolidColorBrush(Color.FromRgb(239, 68, 68));
                    break;
                case ToastType.Warning:
                    Icon = "⚠";
                    BackgroundColor = new SolidColorBrush(Color.FromRgb(245, 158, 11));
                    break;
                case ToastType.Info:
                    Icon = "ℹ";
                    BackgroundColor = new SolidColorBrush(Color.FromRgb(0, 99, 163));
                    break;
            }
        }

        private void ShowToast()
        {
            this.Opacity = 0;
            this.RenderTransform = new TranslateTransform(300, 0);

            _container.Children.Add(this);

            var slideIn = new DoubleAnimation
            {
                From = 300,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(300),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            var fadeIn = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(300)
            };

            this.RenderTransform.BeginAnimation(TranslateTransform.XProperty, slideIn);
            this.BeginAnimation(OpacityProperty, fadeIn);

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(Duration)
            };
            _timer.Tick += (s, e) =>
            {
                _timer.Stop();
                CloseToast();
            };
            _timer.Start();
        }

        private void CloseToast()
        {
            var slideOut = new DoubleAnimation
            {
                From = 0,
                To = 300,
                Duration = TimeSpan.FromMilliseconds(300),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
            };

            var fadeOut = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(300)
            };

            slideOut.Completed += (s, e) =>
            {
                _container.Children.Remove(this);
            };

            this.RenderTransform.BeginAnimation(TranslateTransform.XProperty, slideOut);
            this.BeginAnimation(OpacityProperty, fadeOut);
        }
    }

    public enum ToastType
    {
        Success,
        Error,
        Warning,
        Info
    }
}