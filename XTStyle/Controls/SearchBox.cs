using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using XTStyle.Helpers;

namespace XTStyle.Controls
{
    public class SearchBox : TextBox
    {
        static SearchBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SearchBox),
                new FrameworkPropertyMetadata(typeof(SearchBox)));

            CreateDefaultStyle();
        }

        private static void CreateDefaultStyle()
        {
            var style = new Style(typeof(SearchBox), Application.Current.FindResource(typeof(TextBox)) as Style);

            // Basic properties
            style.Setters.Add(new Setter(HeightProperty, 36.0));
            style.Setters.Add(new Setter(PaddingProperty, new Thickness(36, 0, 36, 0)));
            style.Setters.Add(new Setter(VerticalContentAlignmentProperty, VerticalAlignment.Center));

            // Create control template
            var template = new ControlTemplate(typeof(SearchBox));

            var border = new FrameworkElementFactory(typeof(Border));
            border.SetValue(Border.BackgroundProperty, Brushes.White);
            border.SetValue(Border.BorderThicknessProperty, new Thickness(1));
            border.SetValue(Border.CornerRadiusProperty, new CornerRadius(18));
            border.SetResourceReference(Border.BorderBrushProperty, "BorderBrush");

            var grid = new FrameworkElementFactory(typeof(Grid));

            // Search icon
            var icon = new FrameworkElementFactory(typeof(TextBlock));
            icon.SetValue(TextBlock.TextProperty, "🔍");
            icon.SetValue(TextBlock.FontSizeProperty, 16.0);
            icon.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
            icon.SetValue(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Left);
            icon.SetValue(TextBlock.MarginProperty, new Thickness(12, 0, 0, 0));
            icon.SetValue(TextBlock.IsHitTestVisibleProperty, false);
            icon.SetResourceReference(TextBlock.ForegroundProperty, "TextSecondaryBrush");

            // Content host
            var scrollViewer = new FrameworkElementFactory(typeof(ScrollViewer));
            scrollViewer.Name = "PART_ContentHost";
            scrollViewer.SetValue(MarginProperty, new Thickness(36, 0, 36, 0));
            scrollViewer.SetValue(VerticalAlignmentProperty, VerticalAlignment.Center);

            grid.AppendChild(icon);
            grid.AppendChild(scrollViewer);

            border.AppendChild(grid);
            template.VisualTree = border;

            style.Setters.Add(new Setter(TemplateProperty, template));

            Application.Current.Resources[typeof(SearchBox)] = style;
        }

        // Placeholder Property
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder", typeof(string), typeof(SearchBox),
                new PropertyMetadata("Search..."));

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        // HasText property
        private static readonly DependencyPropertyKey HasTextPropertyKey =
            DependencyProperty.RegisterReadOnly("HasText", typeof(bool), typeof(SearchBox),
                new PropertyMetadata(false));

        public static readonly DependencyProperty HasTextProperty = HasTextPropertyKey.DependencyProperty;

        public bool HasText
        {
            get { return (bool)GetValue(HasTextProperty); }
            private set { SetValue(HasTextPropertyKey, value); }
        }

        public SearchBox()
        {
            // Handle clear on Escape
            this.KeyDown += (s, e) =>
            {
                if (e.Key == Key.Escape)
                {
                    ClearText();
                    e.Handled = true;
                }
            };
        }

        private void ClearText()
        {
            this.Text = string.Empty;
            this.Focus();
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            HasText = !string.IsNullOrEmpty(this.Text);
        }
    }
}