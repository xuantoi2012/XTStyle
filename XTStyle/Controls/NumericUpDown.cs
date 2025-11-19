using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using XTStyle.Helpers;

namespace XTStyle.Controls
{
    public class NumericUpDown : Control
    {
        private TextBox _textBox;
        private Button _upButton;
        private Button _downButton;

        static NumericUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpDown),
                new FrameworkPropertyMetadata(typeof(NumericUpDown)));

            // ⭐ GÁN TEMPLATE TRỰC TIẾP TRONG CODE
            CreateDefaultTemplate();
        }

        private static void CreateDefaultTemplate()
        {
            // Tạo template mặc định
            var template = new ControlTemplate(typeof(NumericUpDown));

            // Tạo Grid root
            var grid = new FrameworkElementFactory(typeof(Grid));

            // Column definitions
            var col1 = new FrameworkElementFactory(typeof(ColumnDefinition));
            col1.SetValue(ColumnDefinition.WidthProperty, new GridLength(1, GridUnitType.Star));
            var col2 = new FrameworkElementFactory(typeof(ColumnDefinition));
            col2.SetValue(ColumnDefinition.WidthProperty, GridLength.Auto);

            grid.AppendChild(col1);
            grid.AppendChild(col2);

            // Border
            var border = new FrameworkElementFactory(typeof(Border));
            border.SetValue(Border.BackgroundProperty, System.Windows.Media.Brushes.White);
            border.SetValue(Border.BorderThicknessProperty, new Thickness(1));
            border.SetValue(Border.CornerRadiusProperty, new CornerRadius(6));
            border.SetResourceReference(Border.BorderBrushProperty, "BorderBrush");

            // TextBox
            var textBox = new FrameworkElementFactory(typeof(TextBox));
            textBox.Name = "PART_TextBox";
            textBox.SetValue(Grid.ColumnProperty, 0);
            textBox.SetValue(TextBox.BorderThicknessProperty, new Thickness(0));
            textBox.SetValue(TextBox.BackgroundProperty, System.Windows.Media.Brushes.Transparent);
            textBox.SetValue(TextBox.VerticalContentAlignmentProperty, VerticalAlignment.Center);
            textBox.SetValue(TextBox.HorizontalContentAlignmentProperty, HorizontalAlignment.Right);
            textBox.SetValue(TextBox.PaddingProperty, new Thickness(12, 0, 12, 0));

            grid.AppendChild(textBox);

            // Buttons StackPanel
            var buttonPanel = new FrameworkElementFactory(typeof(StackPanel));
            buttonPanel.SetValue(Grid.ColumnProperty, 1);
            buttonPanel.SetValue(StackPanel.OrientationProperty, Orientation.Vertical);
            buttonPanel.SetValue(StackPanel.MarginProperty, new Thickness(2));

            // Up Button
            var upButton = new FrameworkElementFactory(typeof(Button));
            upButton.Name = "PART_UpButton";
            upButton.SetValue(Button.ContentProperty, "▲");
            upButton.SetValue(Button.FontSizeProperty, 8.0);
            upButton.SetValue(Button.HeightProperty, 14.0);
            upButton.SetValue(Button.WidthProperty, 24.0);
            upButton.SetValue(Button.CursorProperty, Cursors.Hand);

            // Down Button
            var downButton = new FrameworkElementFactory(typeof(Button));
            downButton.Name = "PART_DownButton";
            downButton.SetValue(Button.ContentProperty, "▼");
            downButton.SetValue(Button.FontSizeProperty, 8.0);
            downButton.SetValue(Button.HeightProperty, 14.0);
            downButton.SetValue(Button.WidthProperty, 24.0);
            downButton.SetValue(Button.CursorProperty, Cursors.Hand);
            downButton.SetValue(Button.MarginProperty, new Thickness(0, 2, 0, 0));

            buttonPanel.AppendChild(upButton);
            buttonPanel.AppendChild(downButton);
            grid.AppendChild(buttonPanel);

            border.AppendChild(grid);
            template.VisualTree = border;

            // Apply template to style
            var style = new Style(typeof(NumericUpDown));
            style.Setters.Add(new Setter(TemplateProperty, template));
            style.Setters.Add(new Setter(HeightProperty, 32.0));
            style.Setters.Add(new Setter(MinWidthProperty, 100.0));

            Application.Current.Resources.Add(typeof(NumericUpDown), style);
        }

        // ... Rest of properties and methods remain the same ...

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(NumericUpDown),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnValueChanged, CoerceValue));

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(double), typeof(NumericUpDown),
                new PropertyMetadata(0.0, OnMinMaxChanged));

        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(double), typeof(NumericUpDown),
                new PropertyMetadata(100.0, OnMinMaxChanged));

        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly DependencyProperty StepProperty =
            DependencyProperty.Register("Step", typeof(double), typeof(NumericUpDown),
                new PropertyMetadata(1.0));

        public double Step
        {
            get { return (double)GetValue(StepProperty); }
            set { SetValue(StepProperty, value); }
        }

        public static readonly DependencyProperty DecimalPlacesProperty =
            DependencyProperty.Register("DecimalPlaces", typeof(int), typeof(NumericUpDown),
                new PropertyMetadata(0, OnDecimalPlacesChanged));

        public int DecimalPlaces
        {
            get { return (int)GetValue(DecimalPlacesProperty); }
            set { SetValue(DecimalPlacesProperty, value); }
        }

        public ICommand IncrementCommand { get; private set; }
        public ICommand DecrementCommand { get; private set; }

        public NumericUpDown()
        {
            IncrementCommand = new RelayCommand(IncreaseValue, CanIncreaseValue);
            DecrementCommand = new RelayCommand(DecreaseValue, CanDecreaseValue);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _textBox = GetTemplateChild("PART_TextBox") as TextBox;
            _upButton = GetTemplateChild("PART_UpButton") as Button;
            _downButton = GetTemplateChild("PART_DownButton") as Button;

            if (_textBox != null)
            {
                _textBox.PreviewTextInput += TextBox_PreviewTextInput;
                _textBox.LostFocus += TextBox_LostFocus;
                _textBox.KeyDown += TextBox_KeyDown;
            }

            if (_upButton != null)
                _upButton.Click += (s, e) => IncreaseValue();

            if (_downButton != null)
                _downButton.Click += (s, e) => DecreaseValue();

            UpdateTextBox();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                IncreaseValue();
                e.Handled = true;
            }
            else if (e.Key == Key.Down)
            {
                DecreaseValue();
                e.Handled = true;
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private bool IsTextAllowed(string text)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(text, @"^[0-9.\-]+$");
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(_textBox.Text, out double value))
            {
                Value = value;
            }
            else
            {
                UpdateTextBox();
            }
        }

        private void IncreaseValue()
        {
            Value = Math.Min(Value + Step, Maximum);
        }

        private bool CanIncreaseValue()
        {
            return Value < Maximum;
        }

        private void DecreaseValue()
        {
            Value = Math.Max(Value - Step, Minimum);
        }

        private bool CanDecreaseValue()
        {
            return Value > Minimum;
        }

        private void UpdateTextBox()
        {
            if (_textBox != null)
            {
                _textBox.Text = Value.ToString($"F{DecimalPlaces}");
            }
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (NumericUpDown)d;
            control.UpdateTextBox();
            CommandManager.InvalidateRequerySuggested();
        }

        private static object CoerceValue(DependencyObject d, object value)
        {
            var control = (NumericUpDown)d;
            double newValue = (double)value;
            newValue = Math.Max(control.Minimum, Math.Min(control.Maximum, newValue));
            return newValue;
        }

        private static void OnMinMaxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (NumericUpDown)d;
            control.CoerceValue(ValueProperty);
            CommandManager.InvalidateRequerySuggested();
        }

        private static void OnDecimalPlacesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (NumericUpDown)d;
            control.UpdateTextBox();
        }
    }
}