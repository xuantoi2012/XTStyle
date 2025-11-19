using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using XTStyle.Helpers;

namespace XTStyle.Controls
{
    public class Pagination : Control
    {
        static Pagination()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Pagination),
                new FrameworkPropertyMetadata(typeof(Pagination)));

            CreateDefaultStyle();
        }

        private static void CreateDefaultStyle()
        {
            var style = new Style(typeof(Pagination));
            style.Setters.Add(new Setter(HeightProperty, 40.0));
            style.Setters.Add(new Setter(HorizontalAlignmentProperty, HorizontalAlignment.Stretch));

            // Create template
            var template = new ControlTemplate(typeof(Pagination));

            var border = new FrameworkElementFactory(typeof(Border));
            border.SetValue(Border.BackgroundProperty, Brushes.White);
            border.SetValue(Border.BorderThicknessProperty, new Thickness(1));
            border.SetValue(Border.CornerRadiusProperty, new CornerRadius(6));
            border.SetValue(Border.PaddingProperty, new Thickness(12, 8, 12, 8));
            border.SetResourceReference(Border.BorderBrushProperty, "BorderBrush");

            var grid = new FrameworkElementFactory(typeof(Grid));

            // Column definitions
            var col1 = new FrameworkElementFactory(typeof(ColumnDefinition));
            col1.SetValue(ColumnDefinition.WidthProperty, GridLength.Auto);
            var col2 = new FrameworkElementFactory(typeof(ColumnDefinition));
            col2.SetValue(ColumnDefinition.WidthProperty, new GridLength(1, GridUnitType.Star));
            var col3 = new FrameworkElementFactory(typeof(ColumnDefinition));
            col3.SetValue(ColumnDefinition.WidthProperty, GridLength.Auto);

            grid.AppendChild(col1);
            grid.AppendChild(col2);
            grid.AppendChild(col3);

            // Info text
            var infoText = new FrameworkElementFactory(typeof(TextBlock));
            infoText.SetValue(Grid.ColumnProperty, 0);
            infoText.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
            infoText.SetValue(TextBlock.MarginProperty, new Thickness(0, 0, 16, 0));
            infoText.SetValue(TextBlock.TextProperty, "Pagination info");

            grid.AppendChild(infoText);

            // Center panel (will be populated in code)
            var centerPanel = new FrameworkElementFactory(typeof(StackPanel));
            centerPanel.SetValue(Grid.ColumnProperty, 1);
            centerPanel.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);
            centerPanel.SetValue(StackPanel.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            centerPanel.Name = "PART_ButtonPanel";

            grid.AppendChild(centerPanel);

            border.AppendChild(grid);
            template.VisualTree = border;

            style.Setters.Add(new Setter(TemplateProperty, template));

            Application.Current.Resources[typeof(Pagination)] = style;
        }

        // TotalItems Property
        public static readonly DependencyProperty TotalItemsProperty =
            DependencyProperty.Register("TotalItems", typeof(int), typeof(Pagination),
                new PropertyMetadata(0, OnPaginationChanged));

        public int TotalItems
        {
            get { return (int)GetValue(TotalItemsProperty); }
            set { SetValue(TotalItemsProperty, value); }
        }

        // PageSize Property
        public static readonly DependencyProperty PageSizeProperty =
            DependencyProperty.Register("PageSize", typeof(int), typeof(Pagination),
                new PropertyMetadata(20, OnPaginationChanged));

        public int PageSize
        {
            get { return (int)GetValue(PageSizeProperty); }
            set { SetValue(PageSizeProperty, value); }
        }

        // CurrentPage Property
        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register("CurrentPage", typeof(int), typeof(Pagination),
                new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnCurrentPageChanged));

        public int CurrentPage
        {
            get { return (int)GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }

        // TotalPages Property
        private static readonly DependencyPropertyKey TotalPagesPropertyKey =
            DependencyProperty.RegisterReadOnly("TotalPages", typeof(int), typeof(Pagination),
                new PropertyMetadata(0));

        public static readonly DependencyProperty TotalPagesProperty = TotalPagesPropertyKey.DependencyProperty;

        public int TotalPages
        {
            get { return (int)GetValue(TotalPagesProperty); }
            private set { SetValue(TotalPagesPropertyKey, value); }
        }

        private StackPanel _buttonPanel;

        public Pagination()
        {
            this.Loaded += Pagination_Loaded;
        }

        private void Pagination_Loaded(object sender, RoutedEventArgs e)
        {
            UpdatePagination();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _buttonPanel = GetTemplateChild("PART_ButtonPanel") as StackPanel;
            UpdatePagination();
        }

        private static void OnPaginationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pagination = (Pagination)d;
            pagination.UpdatePagination();
        }

        private static void OnCurrentPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pagination = (Pagination)d;
            pagination.UpdatePagination();
        }

        private void UpdatePagination()
        {
            if (PageSize <= 0) return;

            TotalPages = (int)Math.Ceiling((double)TotalItems / PageSize);

            if (CurrentPage < 1) CurrentPage = 1;
            if (CurrentPage > TotalPages && TotalPages > 0) CurrentPage = TotalPages;

            UpdateButtons();
        }

        private void UpdateButtons()
        {
            if (_buttonPanel == null) return;

            _buttonPanel.Children.Clear();

            // First button
            var firstBtn = CreateNavigationButton("⏮", () => CurrentPage = 1, CurrentPage > 1);
            _buttonPanel.Children.Add(firstBtn);

            // Previous button
            var prevBtn = CreateNavigationButton("◀", () => CurrentPage--, CurrentPage > 1);
            _buttonPanel.Children.Add(prevBtn);

            // Page numbers
            var pageNumbers = GeneratePageNumbers();
            foreach (var pageNum in pageNumbers)
            {
                if (pageNum.IsEllipsis)
                {
                    var ellipsis = new TextBlock
                    {
                        Text = "...",
                        VerticalAlignment = VerticalAlignment.Center,
                        Margin = new Thickness(4, 0, 4, 0)
                    };
                    _buttonPanel.Children.Add(ellipsis);
                }
                else
                {
                    var pageBtn = CreatePageButton(pageNum.Page, pageNum.Page == CurrentPage);
                    _buttonPanel.Children.Add(pageBtn);
                }
            }

            // Next button
            var nextBtn = CreateNavigationButton("▶", () => CurrentPage++, CurrentPage < TotalPages);
            _buttonPanel.Children.Add(nextBtn);

            // Last button
            var lastBtn = CreateNavigationButton("⏭", () => CurrentPage = TotalPages, CurrentPage < TotalPages);
            _buttonPanel.Children.Add(lastBtn);
        }

        private Button CreateNavigationButton(string content, Action action, bool isEnabled)
        {
            var btn = new Button
            {
                Content = content,
                Width = 32,
                Height = 32,
                Margin = new Thickness(2),
                IsEnabled = isEnabled,
                Cursor = Cursors.Hand
            };

            btn.Click += (s, e) => action();

            return btn;
        }

        private Button CreatePageButton(int pageNumber, bool isCurrent)
        {
            var btn = new Button
            {
                Content = pageNumber.ToString(),
                Width = 32,
                Height = 32,
                Margin = new Thickness(2),
                Cursor = Cursors.Hand,
                FontWeight = isCurrent ? FontWeights.Bold : FontWeights.Normal
            };

            if (isCurrent)
            {
                btn.Background = Application.Current.TryFindResource("PrimaryBrush") as Brush ?? Brushes.Blue;
                btn.Foreground = Brushes.White;
            }

            btn.Click += (s, e) => CurrentPage = pageNumber;

            return btn;
        }

        private List<PageNumberItem> GeneratePageNumbers()
        {
            var pageNumbers = new List<PageNumberItem>();

            if (TotalPages <= 7)
            {
                for (int i = 1; i <= TotalPages; i++)
                {
                    pageNumbers.Add(new PageNumberItem { Page = i, IsEllipsis = false });
                }
            }
            else
            {
                pageNumbers.Add(new PageNumberItem { Page = 1, IsEllipsis = false });

                if (CurrentPage <= 4)
                {
                    for (int i = 2; i <= 5; i++)
                    {
                        pageNumbers.Add(new PageNumberItem { Page = i, IsEllipsis = false });
                    }
                    pageNumbers.Add(new PageNumberItem { Page = 0, IsEllipsis = true });
                }
                else if (CurrentPage >= TotalPages - 3)
                {
                    pageNumbers.Add(new PageNumberItem { Page = 0, IsEllipsis = true });
                    for (int i = TotalPages - 4; i < TotalPages; i++)
                    {
                        pageNumbers.Add(new PageNumberItem { Page = i, IsEllipsis = false });
                    }
                }
                else
                {
                    pageNumbers.Add(new PageNumberItem { Page = 0, IsEllipsis = true });
                    for (int i = CurrentPage - 1; i <= CurrentPage + 1; i++)
                    {
                        pageNumbers.Add(new PageNumberItem { Page = i, IsEllipsis = false });
                    }
                    pageNumbers.Add(new PageNumberItem { Page = 0, IsEllipsis = true });
                }

                pageNumbers.Add(new PageNumberItem { Page = TotalPages, IsEllipsis = false });
            }

            return pageNumbers;
        }
    }

    public class PageNumberItem
    {
        public int Page { get; set; }
        public bool IsEllipsis { get; set; }
    }
}