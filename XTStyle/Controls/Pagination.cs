using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace XTStyle.Controls
{
    /// <summary>
    /// A complete pagination control with page numbers and navigation
    /// </summary>
    public class Pagination : Control
    {
        static Pagination()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Pagination), new FrameworkPropertyMetadata(typeof(Pagination)));
        }

        public Pagination()
        {
            FirstPageCommand = new RelayCommand(GoToFirstPage, CanGoToPrevious);
            PreviousPageCommand = new RelayCommand(GoToPreviousPage, CanGoToPrevious);
            NextPageCommand = new RelayCommand(GoToNextPage, CanGoToNext);
            LastPageCommand = new RelayCommand(GoToLastPage, CanGoToNext);
            PageNumbers = new ObservableCollection<PageNumberItem>();
            UpdatePageNumbers();
        }

        /// <summary>
        /// Gets or sets the total number of items
        /// </summary>
        public int TotalItems
        {
            get { return (int)GetValue(TotalItemsProperty); }
            set { SetValue(TotalItemsProperty, value); }
        }

        public static readonly DependencyProperty TotalItemsProperty =
            DependencyProperty.Register("TotalItems", typeof(int), typeof(Pagination),
                new PropertyMetadata(0, OnPaginationPropertyChanged));

        /// <summary>
        /// Gets or sets the current page (1-based)
        /// </summary>
        public int CurrentPage
        {
            get { return (int)GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }

        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register("CurrentPage", typeof(int), typeof(Pagination),
                new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnCurrentPageChanged, CoerceCurrentPage));

        /// <summary>
        /// Gets or sets the page size
        /// </summary>
        public int PageSize
        {
            get { return (int)GetValue(PageSizeProperty); }
            set { SetValue(PageSizeProperty, value); }
        }

        public static readonly DependencyProperty PageSizeProperty =
            DependencyProperty.Register("PageSize", typeof(int), typeof(Pagination),
                new PropertyMetadata(10, OnPaginationPropertyChanged));

        /// <summary>
        /// Gets the total number of pages
        /// </summary>
        public int TotalPages
        {
            get { return (int)GetValue(TotalPagesProperty); }
            private set { SetValue(TotalPagesProperty, value); }
        }

        public static readonly DependencyProperty TotalPagesProperty =
            DependencyProperty.Register("TotalPages", typeof(int), typeof(Pagination),
                new PropertyMetadata(1));

        /// <summary>
        /// Gets the page numbers to display
        /// </summary>
        public ObservableCollection<PageNumberItem> PageNumbers
        {
            get { return (ObservableCollection<PageNumberItem>)GetValue(PageNumbersProperty); }
            private set { SetValue(PageNumbersProperty, value); }
        }

        public static readonly DependencyProperty PageNumbersProperty =
            DependencyProperty.Register("PageNumbers", typeof(ObservableCollection<PageNumberItem>), typeof(Pagination));

        /// <summary>
        /// Gets the first page command
        /// </summary>
        public ICommand FirstPageCommand
        {
            get { return (ICommand)GetValue(FirstPageCommandProperty); }
            private set { SetValue(FirstPageCommandProperty, value); }
        }

        public static readonly DependencyProperty FirstPageCommandProperty =
            DependencyProperty.Register("FirstPageCommand", typeof(ICommand), typeof(Pagination));

        /// <summary>
        /// Gets the previous page command
        /// </summary>
        public ICommand PreviousPageCommand
        {
            get { return (ICommand)GetValue(PreviousPageCommandProperty); }
            private set { SetValue(PreviousPageCommandProperty, value); }
        }

        public static readonly DependencyProperty PreviousPageCommandProperty =
            DependencyProperty.Register("PreviousPageCommand", typeof(ICommand), typeof(Pagination));

        /// <summary>
        /// Gets the next page command
        /// </summary>
        public ICommand NextPageCommand
        {
            get { return (ICommand)GetValue(NextPageCommandProperty); }
            private set { SetValue(NextPageCommandProperty, value); }
        }

        public static readonly DependencyProperty NextPageCommandProperty =
            DependencyProperty.Register("NextPageCommand", typeof(ICommand), typeof(Pagination));

        /// <summary>
        /// Gets the last page command
        /// </summary>
        public ICommand LastPageCommand
        {
            get { return (ICommand)GetValue(LastPageCommandProperty); }
            private set { SetValue(LastPageCommandProperty, value); }
        }

        public static readonly DependencyProperty LastPageCommandProperty =
            DependencyProperty.Register("LastPageCommand", typeof(ICommand), typeof(Pagination));

        /// <summary>
        /// Event raised when the page changes
        /// </summary>
        public event RoutedPropertyChangedEventHandler<int> PageChanged;

        private static void OnPaginationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pagination = (Pagination)d;
            pagination.UpdateTotalPages();
            pagination.UpdatePageNumbers();
            CommandManager.InvalidateRequerySuggested();
        }

        private static void OnCurrentPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pagination = (Pagination)d;
            pagination.UpdatePageNumbers();
            pagination.PageChanged?.Invoke(pagination, new RoutedPropertyChangedEventArgs<int>(
                (int)e.OldValue, (int)e.NewValue));
            CommandManager.InvalidateRequerySuggested();
        }

        private static object CoerceCurrentPage(DependencyObject d, object baseValue)
        {
            var pagination = (Pagination)d;
            var value = (int)baseValue;
            return Math.Max(1, Math.Min(pagination.TotalPages, value));
        }

        private void UpdateTotalPages()
        {
            TotalPages = PageSize > 0 ? (int)Math.Ceiling((double)TotalItems / PageSize) : 1;
            CoerceValue(CurrentPageProperty);
        }

        private void UpdatePageNumbers()
        {
            PageNumbers.Clear();

            if (TotalPages <= 1)
                return;

            const int maxVisible = 7;
            int startPage = 1;
            int endPage = TotalPages;

            if (TotalPages > maxVisible)
            {
                int middle = maxVisible / 2;
                if (CurrentPage <= middle)
                {
                    endPage = maxVisible - 1;
                }
                else if (CurrentPage >= TotalPages - middle)
                {
                    startPage = TotalPages - maxVisible + 2;
                }
                else
                {
                    startPage = CurrentPage - middle + 1;
                    endPage = CurrentPage + middle - 1;
                }
            }

            // Add first page
            if (startPage > 1)
            {
                PageNumbers.Add(new PageNumberItem { PageNumber = 1, IsEllipsis = false });
                if (startPage > 2)
                    PageNumbers.Add(new PageNumberItem { PageNumber = -1, IsEllipsis = true });
            }

            // Add middle pages
            for (int i = startPage; i <= endPage; i++)
            {
                PageNumbers.Add(new PageNumberItem { PageNumber = i, IsEllipsis = false });
            }

            // Add last page
            if (endPage < TotalPages)
            {
                if (endPage < TotalPages - 1)
                    PageNumbers.Add(new PageNumberItem { PageNumber = -1, IsEllipsis = true });
                PageNumbers.Add(new PageNumberItem { PageNumber = TotalPages, IsEllipsis = false });
            }
        }

        private void GoToFirstPage() => CurrentPage = 1;
        private void GoToPreviousPage() => CurrentPage--;
        private void GoToNextPage() => CurrentPage++;
        private void GoToLastPage() => CurrentPage = TotalPages;

        private bool CanGoToPrevious() => CurrentPage > 1;
        private bool CanGoToNext() => CurrentPage < TotalPages;

        internal void GoToPage(int pageNumber)
        {
            if (pageNumber > 0 && pageNumber <= TotalPages)
                CurrentPage = pageNumber;
        }

        private class RelayCommand : ICommand
        {
            private readonly Action _execute;
            private readonly Func<bool> _canExecute;

            public RelayCommand(Action execute, Func<bool> canExecute = null)
            {
                _execute = execute;
                _canExecute = canExecute;
            }

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;
            public void Execute(object parameter) => _execute();
        }
    }

    /// <summary>
    /// Represents a page number item in the pagination control
    /// </summary>
    public class PageNumberItem
    {
        public int PageNumber { get; set; }
        public bool IsEllipsis { get; set; }
    }
}
