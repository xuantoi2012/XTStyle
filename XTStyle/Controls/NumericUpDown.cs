using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace XTStyle.Controls
{
    /// <summary>
    /// A numeric input control with up/down buttons
    /// </summary>
    public class NumericUpDown : Control
    {
        static NumericUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpDown), new FrameworkPropertyMetadata(typeof(NumericUpDown)));
        }

        public NumericUpDown()
        {
            IncrementCommand = new RelayCommand(Increment, CanIncrement);
            DecrementCommand = new RelayCommand(Decrement, CanDecrement);
        }

        /// <summary>
        /// Gets or sets the current value
        /// </summary>
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(NumericUpDown),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnValueChanged, CoerceValue));

        /// <summary>
        /// Gets or sets the minimum value
        /// </summary>
        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(double), typeof(NumericUpDown),
                new PropertyMetadata(double.MinValue, OnMinMaxChanged));

        /// <summary>
        /// Gets or sets the maximum value
        /// </summary>
        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(double), typeof(NumericUpDown),
                new PropertyMetadata(double.MaxValue, OnMinMaxChanged));

        /// <summary>
        /// Gets or sets the increment/decrement step
        /// </summary>
        public double Increment
        {
            get { return (double)GetValue(IncrementProperty); }
            set { SetValue(IncrementProperty, value); }
        }

        public static readonly DependencyProperty IncrementProperty =
            DependencyProperty.Register("Increment", typeof(double), typeof(NumericUpDown),
                new PropertyMetadata(1.0));

        /// <summary>
        /// Gets or sets the number of decimal places to display
        /// </summary>
        public int DecimalPlaces
        {
            get { return (int)GetValue(DecimalPlacesProperty); }
            set { SetValue(DecimalPlacesProperty, value); }
        }

        public static readonly DependencyProperty DecimalPlacesProperty =
            DependencyProperty.Register("DecimalPlaces", typeof(int), typeof(NumericUpDown),
                new PropertyMetadata(0));

        /// <summary>
        /// Gets the command to increment the value
        /// </summary>
        public ICommand IncrementCommand
        {
            get { return (ICommand)GetValue(IncrementCommandProperty); }
            private set { SetValue(IncrementCommandProperty, value); }
        }

        public static readonly DependencyProperty IncrementCommandProperty =
            DependencyProperty.Register("IncrementCommand", typeof(ICommand), typeof(NumericUpDown));

        /// <summary>
        /// Gets the command to decrement the value
        /// </summary>
        public ICommand DecrementCommand
        {
            get { return (ICommand)GetValue(DecrementCommandProperty); }
            private set { SetValue(DecrementCommandProperty, value); }
        }

        public static readonly DependencyProperty DecrementCommandProperty =
            DependencyProperty.Register("DecrementCommand", typeof(ICommand), typeof(NumericUpDown));

        /// <summary>
        /// Event raised when value changes
        /// </summary>
        public event RoutedPropertyChangedEventHandler<double> ValueChanged;

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (NumericUpDown)d;
            control.ValueChanged?.Invoke(control, new RoutedPropertyChangedEventArgs<double>(
                (double)e.OldValue, (double)e.NewValue));
            CommandManager.InvalidateRequerySuggested();
        }

        private static object CoerceValue(DependencyObject d, object baseValue)
        {
            var control = (NumericUpDown)d;
            var value = (double)baseValue;
            return Math.Max(control.Minimum, Math.Min(control.Maximum, value));
        }

        private static void OnMinMaxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (NumericUpDown)d;
            control.CoerceValue(ValueProperty);
            CommandManager.InvalidateRequerySuggested();
        }

        private void Increment()
        {
            Value = Math.Min(Maximum, Value + Increment);
        }

        private bool CanIncrement()
        {
            return Value < Maximum;
        }

        private void Decrement()
        {
            Value = Math.Max(Minimum, Value - Increment);
        }

        private bool CanDecrement()
        {
            return Value > Minimum;
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
}
