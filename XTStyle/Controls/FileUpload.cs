using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using XTStyle.Helpers;

namespace XTStyle.Controls
{
    public class FileUpload : Control
    {
        static FileUpload()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FileUpload),
                new FrameworkPropertyMetadata(typeof(FileUpload)));

            CreateDefaultStyle();
        }

        private static void CreateDefaultStyle()
        {
            var style = new Style(typeof(FileUpload));
            style.Setters.Add(new Setter(HeightProperty, 40.0));
            style.Setters.Add(new Setter(MinWidthProperty, 200.0));

            // Create template
            var template = new ControlTemplate(typeof(FileUpload));

            var border = new FrameworkElementFactory(typeof(Border));
            border.SetValue(Border.BackgroundProperty, Brushes.White);
            border.SetValue(Border.BorderThicknessProperty, new Thickness(1));
            border.SetValue(Border.CornerRadiusProperty, new CornerRadius(6));
            border.SetResourceReference(Border.BorderBrushProperty, "BorderBrush");

            var grid = new FrameworkElementFactory(typeof(Grid));

            // Column definitions
            var col1 = new FrameworkElementFactory(typeof(ColumnDefinition));
            col1.SetValue(ColumnDefinition.WidthProperty, new GridLength(1, GridUnitType.Star));
            var col2 = new FrameworkElementFactory(typeof(ColumnDefinition));
            col2.SetValue(ColumnDefinition.WidthProperty, GridLength.Auto);

            grid.AppendChild(col1);
            grid.AppendChild(col2);

            // File name text
            var textBlock = new FrameworkElementFactory(typeof(TextBlock));
            textBlock.SetValue(Grid.ColumnProperty, 0);
            textBlock.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
            textBlock.SetValue(TextBlock.MarginProperty, new Thickness(12, 0, 12, 0));
            textBlock.SetBinding(TextBlock.TextProperty, new Binding("FileName")
            {
                RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent)
            });

            grid.AppendChild(textBlock);

            // Browse button (will be handled in code-behind)
            var button = new FrameworkElementFactory(typeof(Button));
            button.SetValue(Grid.ColumnProperty, 1);
            button.SetValue(Button.ContentProperty, "📁 Chọn file");
            button.SetValue(Button.HeightProperty, 32.0);
            button.SetValue(Button.MarginProperty, new Thickness(4));
            button.SetValue(Button.CursorProperty, Cursors.Hand);

            grid.AppendChild(button);

            border.AppendChild(grid);
            template.VisualTree = border;

            style.Setters.Add(new Setter(TemplateProperty, template));

            Application.Current.Resources[typeof(FileUpload)] = style;
        }

        // FileName Property
        public static readonly DependencyProperty FileNameProperty =
            DependencyProperty.Register("FileName", typeof(string), typeof(FileUpload),
                new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string FileName
        {
            get { return (string)GetValue(FileNameProperty); }
            set { SetValue(FileNameProperty, value); }
        }

        // FilePath Property
        public static readonly DependencyProperty FilePathProperty =
            DependencyProperty.Register("FilePath", typeof(string), typeof(FileUpload),
                new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string FilePath
        {
            get { return (string)GetValue(FilePathProperty); }
            set { SetValue(FilePathProperty, value); }
        }

        // Filter Property
        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(string), typeof(FileUpload),
                new PropertyMetadata("All Files (*.*)|*.*"));

        public string Filter
        {
            get { return (string)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }

        // Placeholder Property
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder", typeof(string), typeof(FileUpload),
                new PropertyMetadata("Chọn file..."));

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public FileUpload()
        {
            this.MouseLeftButtonDown += FileUpload_MouseLeftButtonDown;
        }

        private void FileUpload_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Browse();
        }

        private void Browse()
        {
            var dialog = new OpenFileDialog
            {
                Filter = Filter,
                Multiselect = false
            };

            if (dialog.ShowDialog() == true)
            {
                FilePath = dialog.FileName;
                FileName = Path.GetFileName(dialog.FileName);
            }
        }

        public void Clear()
        {
            FilePath = "";
            FileName = "";
        }
    }
}