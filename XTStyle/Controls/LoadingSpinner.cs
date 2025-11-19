using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace XTStyle.Controls
{
    /// <summary>
    /// An animated loading spinner control
    /// </summary>
    public class LoadingSpinner : Control
    {
        private Storyboard _rotationStoryboard;

        static LoadingSpinner()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LoadingSpinner), new FrameworkPropertyMetadata(typeof(LoadingSpinner)));
        }

        public LoadingSpinner()
        {
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        /// <summary>
        /// Gets or sets whether the spinner is spinning
        /// </summary>
        public bool IsSpinning
        {
            get { return (bool)GetValue(IsSpinningProperty); }
            set { SetValue(IsSpinningProperty, value); }
        }

        public static readonly DependencyProperty IsSpinningProperty =
            DependencyProperty.Register("IsSpinning", typeof(bool), typeof(LoadingSpinner),
                new PropertyMetadata(true, OnIsSpinningChanged));

        /// <summary>
        /// Gets or sets the spinner diameter
        /// </summary>
        public double Diameter
        {
            get { return (double)GetValue(DiameterProperty); }
            set { SetValue(DiameterProperty, value); }
        }

        public static readonly DependencyProperty DiameterProperty =
            DependencyProperty.Register("Diameter", typeof(double), typeof(LoadingSpinner),
                new PropertyMetadata(40.0));

        /// <summary>
        /// Gets or sets the spinner color
        /// </summary>
        public Brush SpinnerColor
        {
            get { return (Brush)GetValue(SpinnerColorProperty); }
            set { SetValue(SpinnerColorProperty, value); }
        }

        public static readonly DependencyProperty SpinnerColorProperty =
            DependencyProperty.Register("SpinnerColor", typeof(Brush), typeof(LoadingSpinner),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the spinner thickness
        /// </summary>
        public double Thickness
        {
            get { return (double)GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }

        public static readonly DependencyProperty ThicknessProperty =
            DependencyProperty.Register("Thickness", typeof(double), typeof(LoadingSpinner),
                new PropertyMetadata(4.0));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            StartAnimation();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (IsSpinning)
                StartAnimation();
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            StopAnimation();
        }

        private static void OnIsSpinningChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var spinner = (LoadingSpinner)d;
            if ((bool)e.NewValue)
                spinner.StartAnimation();
            else
                spinner.StopAnimation();
        }

        private void StartAnimation()
        {
            if (!IsLoaded || !IsSpinning)
                return;

            var rotateTransform = GetTemplateChild("PART_RotateTransform") as RotateTransform;
            if (rotateTransform == null)
                return;

            _rotationStoryboard = new Storyboard();
            var animation = new DoubleAnimation
            {
                From = 0,
                To = 360,
                Duration = TimeSpan.FromSeconds(1.2),
                RepeatBehavior = RepeatBehavior.Forever
            };

            Storyboard.SetTarget(animation, rotateTransform);
            Storyboard.SetTargetProperty(animation, new PropertyPath(RotateTransform.AngleProperty));

            _rotationStoryboard.Children.Add(animation);
            _rotationStoryboard.Begin();
        }

        private void StopAnimation()
        {
            _rotationStoryboard?.Stop();
            _rotationStoryboard = null;
        }
    }
}
