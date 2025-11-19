using System.Windows;
using System.Windows.Controls;

namespace XTStyle.Controls
{
    /// <summary>
    /// A button control with icon support
    /// </summary>
    public class IconButton : Button
    {
        static IconButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IconButton), new FrameworkPropertyMetadata(typeof(IconButton)));
        }

        /// <summary>
        /// Gets or sets the icon content (text or symbol)
        /// </summary>
        public object Icon
        {
            get { return GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(object), typeof(IconButton),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the text content
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(IconButton),
                new PropertyMetadata(string.Empty));

        /// <summary>
        /// Gets or sets the icon position relative to text
        /// </summary>
        public Dock IconPosition
        {
            get { return (Dock)GetValue(IconPositionProperty); }
            set { SetValue(IconPositionProperty, value); }
        }

        public static readonly DependencyProperty IconPositionProperty =
            DependencyProperty.Register("IconPosition", typeof(Dock), typeof(IconButton),
                new PropertyMetadata(Dock.Left));

        /// <summary>
        /// Gets or sets the spacing between icon and text
        /// </summary>
        public double IconSpacing
        {
            get { return (double)GetValue(IconSpacingProperty); }
            set { SetValue(IconSpacingProperty, value); }
        }

        public static readonly DependencyProperty IconSpacingProperty =
            DependencyProperty.Register("IconSpacing", typeof(double), typeof(IconButton),
                new PropertyMetadata(8.0));
    }
}
