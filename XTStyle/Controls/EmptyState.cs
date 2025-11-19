using System.Windows;
using System.Windows.Controls;

namespace XTStyle.Controls
{
    /// <summary>
    /// An empty state component to show when no data is available
    /// </summary>
    public class EmptyState : Control
    {
        static EmptyState()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EmptyState), new FrameworkPropertyMetadata(typeof(EmptyState)));
        }

        /// <summary>
        /// Gets or sets the icon content
        /// </summary>
        public object Icon
        {
            get { return GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(object), typeof(EmptyState),
                new PropertyMetadata("📭"));

        /// <summary>
        /// Gets or sets the title text
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(EmptyState),
                new PropertyMetadata("No Data"));

        /// <summary>
        /// Gets or sets the message text
        /// </summary>
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(EmptyState),
                new PropertyMetadata("There is no data to display."));

        /// <summary>
        /// Gets or sets the action button content
        /// </summary>
        public object ActionButton
        {
            get { return GetValue(ActionButtonProperty); }
            set { SetValue(ActionButtonProperty, value); }
        }

        public static readonly DependencyProperty ActionButtonProperty =
            DependencyProperty.Register("ActionButton", typeof(object), typeof(EmptyState),
                new PropertyMetadata(null));
    }
}
