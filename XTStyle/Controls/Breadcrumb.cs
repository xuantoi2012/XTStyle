using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace XTStyle.Controls
{
    /// <summary>
    /// Breadcrumb navigation item
    /// </summary>
    public class BreadcrumbItem
    {
        public string Text { get; set; }
        public object Data { get; set; }
    }

    /// <summary>
    /// A breadcrumb navigation control
    /// </summary>
    public class Breadcrumb : ItemsControl
    {
        static Breadcrumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Breadcrumb), new FrameworkPropertyMetadata(typeof(Breadcrumb)));
        }

        public Breadcrumb()
        {
            Items = new ObservableCollection<BreadcrumbItem>();
            ItemsSource = Items;
        }

        /// <summary>
        /// Gets the breadcrumb items
        /// </summary>
        public new ObservableCollection<BreadcrumbItem> Items
        {
            get { return (ObservableCollection<BreadcrumbItem>)GetValue(ItemsProperty); }
            private set { SetValue(ItemsProperty, value); }
        }

        public new static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ObservableCollection<BreadcrumbItem>), typeof(Breadcrumb));

        /// <summary>
        /// Gets or sets the separator character
        /// </summary>
        public string Separator
        {
            get { return (string)GetValue(SeparatorProperty); }
            set { SetValue(SeparatorProperty, value); }
        }

        public static readonly DependencyProperty SeparatorProperty =
            DependencyProperty.Register("Separator", typeof(string), typeof(Breadcrumb),
                new PropertyMetadata("/"));

        /// <summary>
        /// Event raised when a breadcrumb item is clicked
        /// </summary>
        public event RoutedEventHandler ItemClicked;

        internal void OnItemClicked(BreadcrumbItem item)
        {
            ItemClicked?.Invoke(item, new RoutedEventArgs());
        }
    }
}
