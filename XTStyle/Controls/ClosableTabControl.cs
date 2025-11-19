using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace XTStyle.Controls
{
    /// <summary>
    /// A tab control with closable tabs
    /// </summary>
    public class ClosableTabControl : TabControl
    {
        static ClosableTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ClosableTabControl), new FrameworkPropertyMetadata(typeof(ClosableTabControl)));
        }

        /// <summary>
        /// Gets or sets whether tabs can be closed
        /// </summary>
        public bool CanCloseTabs
        {
            get { return (bool)GetValue(CanCloseTabsProperty); }
            set { SetValue(CanCloseTabsProperty, value); }
        }

        public static readonly DependencyProperty CanCloseTabsProperty =
            DependencyProperty.Register("CanCloseTabs", typeof(bool), typeof(ClosableTabControl),
                new PropertyMetadata(true));

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ClosableTabItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is ClosableTabItem;
        }

        internal void CloseTab(ClosableTabItem tabItem)
        {
            if (ItemsSource != null)
            {
                // If using ItemsSource binding, remove from source collection
                var collection = ItemsSource as System.Collections.IList;
                collection?.Remove(ItemContainerGenerator.ItemFromContainer(tabItem));
            }
            else
            {
                // Remove from Items collection
                Items.Remove(tabItem);
            }
        }
    }

    /// <summary>
    /// A tab item with a close button
    /// </summary>
    public class ClosableTabItem : TabItem
    {
        static ClosableTabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ClosableTabItem), new FrameworkPropertyMetadata(typeof(ClosableTabItem)));
        }

        public ClosableTabItem()
        {
            CloseCommand = new RelayCommand(Close);
        }

        /// <summary>
        /// Gets or sets whether this tab can be closed
        /// </summary>
        public bool CanClose
        {
            get { return (bool)GetValue(CanCloseProperty); }
            set { SetValue(CanCloseProperty, value); }
        }

        public static readonly DependencyProperty CanCloseProperty =
            DependencyProperty.Register("CanClose", typeof(bool), typeof(ClosableTabItem),
                new PropertyMetadata(true));

        /// <summary>
        /// Gets the close command
        /// </summary>
        public ICommand CloseCommand
        {
            get { return (ICommand)GetValue(CloseCommandProperty); }
            private set { SetValue(CloseCommandProperty, value); }
        }

        public static readonly DependencyProperty CloseCommandProperty =
            DependencyProperty.Register("CloseCommand", typeof(ICommand), typeof(ClosableTabItem));

        /// <summary>
        /// Event raised when the tab is being closed
        /// </summary>
        public event RoutedEventHandler Closing;

        private void Close()
        {
            // Raise closing event
            var args = new RoutedEventArgs();
            Closing?.Invoke(this, args);

            // Close the tab through parent control
            var parent = ItemsControl.ItemsControlFromItemContainer(this) as ClosableTabControl;
            parent?.CloseTab(this);
        }

        private class RelayCommand : ICommand
        {
            private readonly System.Action _execute;

            public RelayCommand(System.Action execute)
            {
                _execute = execute;
            }

            public event System.EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter) => true;

            public void Execute(object parameter) => _execute();
        }
    }
}
