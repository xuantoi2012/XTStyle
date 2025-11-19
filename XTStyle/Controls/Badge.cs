using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace XTStyle.Controls
{
    /// <summary>
    /// Badge types
    /// </summary>
    public enum BadgeType
    {
        Default,
        Primary,
        Success,
        Warning,
        Danger,
        Info
    }

    /// <summary>
    /// A badge component with different types
    /// </summary>
    public class Badge : ContentControl
    {
        static Badge()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Badge), new FrameworkPropertyMetadata(typeof(Badge)));
        }

        /// <summary>
        /// Gets or sets the badge text
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(Badge),
                new PropertyMetadata(string.Empty));

        /// <summary>
        /// Gets or sets the badge type
        /// </summary>
        public BadgeType Type
        {
            get { return (BadgeType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(BadgeType), typeof(Badge),
                new PropertyMetadata(BadgeType.Default, OnTypeChanged));

        /// <summary>
        /// Gets or sets whether the badge is a dot (small circle)
        /// </summary>
        public bool IsDot
        {
            get { return (bool)GetValue(IsDotProperty); }
            set { SetValue(IsDotProperty, value); }
        }

        public static readonly DependencyProperty IsDotProperty =
            DependencyProperty.Register("IsDot", typeof(bool), typeof(Badge),
                new PropertyMetadata(false));

        private static void OnTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var badge = (Badge)d;
            badge.UpdateColors();
        }

        private void UpdateColors()
        {
            // Colors will be set in XAML style based on Type property
        }
    }
}
