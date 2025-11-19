using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace XTStyle.Controls
{
    /// <summary>
    /// A button with Material Design ripple effect
    /// </summary>
    public class RippleButton : Button
    {
        private Grid _grid;
        private Canvas _rippleCanvas;

        static RippleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RippleButton), new FrameworkPropertyMetadata(typeof(RippleButton)));
        }

        /// <summary>
        /// Gets or sets the ripple color
        /// </summary>
        public Brush RippleColor
        {
            get { return (Brush)GetValue(RippleColorProperty); }
            set { SetValue(RippleColorProperty, value); }
        }

        public static readonly DependencyProperty RippleColorProperty =
            DependencyProperty.Register("RippleColor", typeof(Brush), typeof(RippleButton),
                new PropertyMetadata(new SolidColorBrush(Color.FromArgb(50, 255, 255, 255))));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _grid = GetTemplateChild("PART_Grid") as Grid;
            _rippleCanvas = GetTemplateChild("PART_RippleCanvas") as Canvas;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            if (_rippleCanvas != null && _grid != null)
            {
                var position = e.GetPosition(_grid);
                CreateRipple(position);
            }
        }

        private void CreateRipple(Point position)
        {
            var maxRadius = Math.Max(ActualWidth, ActualHeight) * 1.5;

            var ellipse = new Ellipse
            {
                Width = 0,
                Height = 0,
                Fill = RippleColor,
                Opacity = 1
            };

            Canvas.SetLeft(ellipse, position.X);
            Canvas.SetTop(ellipse, position.Y);

            _rippleCanvas.Children.Add(ellipse);

            // Scale animation
            var scaleAnimation = new DoubleAnimation
            {
                From = 0,
                To = maxRadius,
                Duration = TimeSpan.FromMilliseconds(600),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            // Opacity animation
            var opacityAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(600),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            opacityAnimation.Completed += (s, e) =>
            {
                _rippleCanvas.Children.Remove(ellipse);
            };

            // Apply animations
            ellipse.BeginAnimation(WidthProperty, scaleAnimation);
            ellipse.BeginAnimation(HeightProperty, scaleAnimation);
            ellipse.BeginAnimation(OpacityProperty, opacityAnimation);

            // Animate position to center the expanding circle
            var offsetAnimation = new DoubleAnimation
            {
                From = 0,
                To = -maxRadius / 2,
                Duration = TimeSpan.FromMilliseconds(600),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            ellipse.BeginAnimation(Canvas.LeftProperty, offsetAnimation);
            ellipse.BeginAnimation(Canvas.TopProperty, offsetAnimation);
        }
    }
}
