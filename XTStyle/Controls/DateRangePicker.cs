using System;
using System.Windows;
using System.Windows.Controls;

namespace XTStyle.Controls
{
    /// <summary>
    /// A date range picker control
    /// </summary>
    public class DateRangePicker : Control
    {
        static DateRangePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DateRangePicker), new FrameworkPropertyMetadata(typeof(DateRangePicker)));
        }

        /// <summary>
        /// Gets or sets the start date
        /// </summary>
        public DateTime? StartDate
        {
            get { return (DateTime?)GetValue(StartDateProperty); }
            set { SetValue(StartDateProperty, value); }
        }

        public static readonly DependencyProperty StartDateProperty =
            DependencyProperty.Register("StartDate", typeof(DateTime?), typeof(DateRangePicker),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnDateChanged));

        /// <summary>
        /// Gets or sets the end date
        /// </summary>
        public DateTime? EndDate
        {
            get { return (DateTime?)GetValue(EndDateProperty); }
            set { SetValue(EndDateProperty, value); }
        }

        public static readonly DependencyProperty EndDateProperty =
            DependencyProperty.Register("EndDate", typeof(DateTime?), typeof(DateRangePicker),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnDateChanged));

        /// <summary>
        /// Gets or sets the placeholder text
        /// </summary>
        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder", typeof(string), typeof(DateRangePicker),
                new PropertyMetadata("Select date range..."));

        /// <summary>
        /// Gets or sets the date format
        /// </summary>
        public string DateFormat
        {
            get { return (string)GetValue(DateFormatProperty); }
            set { SetValue(DateFormatProperty, value); }
        }

        public static readonly DependencyProperty DateFormatProperty =
            DependencyProperty.Register("DateFormat", typeof(string), typeof(DateRangePicker),
                new PropertyMetadata("MM/dd/yyyy"));

        /// <summary>
        /// Gets the formatted date range text
        /// </summary>
        public string DisplayText
        {
            get { return (string)GetValue(DisplayTextProperty); }
            private set { SetValue(DisplayTextProperty, value); }
        }

        public static readonly DependencyProperty DisplayTextProperty =
            DependencyProperty.Register("DisplayText", typeof(string), typeof(DateRangePicker),
                new PropertyMetadata(string.Empty));

        /// <summary>
        /// Event raised when date range changes
        /// </summary>
        public event EventHandler DateRangeChanged;

        private static void OnDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var picker = (DateRangePicker)d;
            picker.UpdateDisplayText();
            picker.DateRangeChanged?.Invoke(picker, EventArgs.Empty);
        }

        private void UpdateDisplayText()
        {
            if (StartDate.HasValue && EndDate.HasValue)
            {
                DisplayText = $"{StartDate.Value.ToString(DateFormat)} - {EndDate.Value.ToString(DateFormat)}";
            }
            else if (StartDate.HasValue)
            {
                DisplayText = StartDate.Value.ToString(DateFormat);
            }
            else if (EndDate.HasValue)
            {
                DisplayText = EndDate.Value.ToString(DateFormat);
            }
            else
            {
                DisplayText = string.Empty;
            }
        }
    }
}
