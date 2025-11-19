using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace XTStyle.Controls
{
    /// <summary>
    /// A DataGrid with built-in filtering capabilities
    /// </summary>
    public class FilterableDataGrid : DataGrid
    {
        private readonly Dictionary<string, string> _columnFilters = new Dictionary<string, string>();

        static FilterableDataGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FilterableDataGrid), new FrameworkPropertyMetadata(typeof(FilterableDataGrid)));
        }

        public FilterableDataGrid()
        {
            AutoGenerateColumns = false;
        }

        /// <summary>
        /// Gets or sets whether filtering is enabled
        /// </summary>
        public bool EnableFiltering
        {
            get { return (bool)GetValue(EnableFilteringProperty); }
            set { SetValue(EnableFilteringProperty, value); }
        }

        public static readonly DependencyProperty EnableFilteringProperty =
            DependencyProperty.Register("EnableFiltering", typeof(bool), typeof(FilterableDataGrid),
                new PropertyMetadata(true));

        /// <summary>
        /// Applies filter to a specific column
        /// </summary>
        public void ApplyFilter(string columnName, string filterText)
        {
            if (string.IsNullOrWhiteSpace(filterText))
            {
                _columnFilters.Remove(columnName);
            }
            else
            {
                _columnFilters[columnName] = filterText.ToLower();
            }

            RefreshFilter();
        }

        /// <summary>
        /// Clears all filters
        /// </summary>
        public void ClearFilters()
        {
            _columnFilters.Clear();
            RefreshFilter();
        }

        private void RefreshFilter()
        {
            if (ItemsSource == null)
                return;

            var view = CollectionViewSource.GetDefaultView(ItemsSource);
            if (view == null)
                return;

            if (_columnFilters.Count == 0)
            {
                view.Filter = null;
            }
            else
            {
                view.Filter = FilterItem;
            }
        }

        private bool FilterItem(object item)
        {
            if (item == null)
                return false;

            foreach (var filter in _columnFilters)
            {
                var property = item.GetType().GetProperty(filter.Key);
                if (property == null)
                    continue;

                var value = property.GetValue(item)?.ToString()?.ToLower() ?? string.Empty;
                if (!value.Contains(filter.Value))
                    return false;
            }

            return true;
        }

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);
            RefreshFilter();
        }
    }
}
