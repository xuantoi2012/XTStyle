using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace XTStyle.Controls
{
    /// <summary>
    /// A search input control with icon and clear button
    /// </summary>
    public class SearchBox : Control
    {
        static SearchBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SearchBox), new FrameworkPropertyMetadata(typeof(SearchBox)));
        }

        public SearchBox()
        {
            ClearCommand = new RelayCommand(Clear);
        }

        /// <summary>
        /// Gets or sets the search text
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(SearchBox),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnTextChanged));

        /// <summary>
        /// Gets or sets the placeholder text
        /// </summary>
        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder", typeof(string), typeof(SearchBox),
                new PropertyMetadata("Search..."));

        /// <summary>
        /// Gets the command to clear the search text
        /// </summary>
        public ICommand ClearCommand
        {
            get { return (ICommand)GetValue(ClearCommandProperty); }
            private set { SetValue(ClearCommandProperty, value); }
        }

        public static readonly DependencyProperty ClearCommandProperty =
            DependencyProperty.Register("ClearCommand", typeof(ICommand), typeof(SearchBox),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets whether the search box has text
        /// </summary>
        public bool HasText
        {
            get { return (bool)GetValue(HasTextProperty); }
            private set { SetValue(HasTextProperty, value); }
        }

        public static readonly DependencyProperty HasTextProperty =
            DependencyProperty.Register("HasText", typeof(bool), typeof(SearchBox),
                new PropertyMetadata(false));

        /// <summary>
        /// Event raised when search text changes
        /// </summary>
        public event RoutedPropertyChangedEventHandler<string> TextChanged;

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var searchBox = (SearchBox)d;
            searchBox.HasText = !string.IsNullOrEmpty((string)e.NewValue);
            searchBox.TextChanged?.Invoke(searchBox, new RoutedPropertyChangedEventArgs<string>(
                (string)e.OldValue, (string)e.NewValue));
        }

        private void Clear()
        {
            Text = string.Empty;
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
