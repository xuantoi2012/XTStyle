using System.Windows;
using System.Windows.Controls;

namespace XTStyle.Controls
{
    /// <summary>
    /// A container for accordion items (collapsible panels)
    /// </summary>
    public class Accordion : ItemsControl
    {
        static Accordion()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Accordion), new FrameworkPropertyMetadata(typeof(Accordion)));
        }

        /// <summary>
        /// Gets or sets whether only one item can be expanded at a time
        /// </summary>
        public bool SingleExpand
        {
            get { return (bool)GetValue(SingleExpandProperty); }
            set { SetValue(SingleExpandProperty, value); }
        }

        public static readonly DependencyProperty SingleExpandProperty =
            DependencyProperty.Register("SingleExpand", typeof(bool), typeof(Accordion),
                new PropertyMetadata(false));

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new AccordionItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is AccordionItem;
        }

        internal void OnItemExpanded(AccordionItem expandedItem)
        {
            if (SingleExpand)
            {
                foreach (var item in Items)
                {
                    if (item is AccordionItem accordionItem && accordionItem != expandedItem)
                    {
                        accordionItem.IsExpanded = false;
                    }
                }
            }
        }
    }

    /// <summary>
    /// A collapsible panel item for use in an Accordion
    /// </summary>
    public class AccordionItem : HeaderedContentControl
    {
        static AccordionItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AccordionItem), new FrameworkPropertyMetadata(typeof(AccordionItem)));
        }

        /// <summary>
        /// Gets or sets whether the item is expanded
        /// </summary>
        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register("IsExpanded", typeof(bool), typeof(AccordionItem),
                new PropertyMetadata(false, OnIsExpandedChanged));

        /// <summary>
        /// Event raised when IsExpanded changes
        /// </summary>
        public event RoutedEventHandler Expanded;
        public event RoutedEventHandler Collapsed;

        protected override void OnMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            // Toggle expansion when clicking anywhere on the header
            IsExpanded = !IsExpanded;
            e.Handled = true;
        }

        private static void OnIsExpandedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var item = (AccordionItem)d;
            var isExpanded = (bool)e.NewValue;

            if (isExpanded)
            {
                item.Expanded?.Invoke(item, new RoutedEventArgs());
                
                // Notify parent accordion
                var parent = ItemsControl.ItemsControlFromItemContainer(item) as Accordion;
                parent?.OnItemExpanded(item);
            }
            else
            {
                item.Collapsed?.Invoke(item, new RoutedEventArgs());
            }
        }
    }
}
