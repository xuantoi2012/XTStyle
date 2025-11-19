using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace XTStyle.Controls
{
    /// <summary>
    /// A dashboard statistics card widget
    /// </summary>
    public class StatsCard : Control
    {
        static StatsCard()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StatsCard), new FrameworkPropertyMetadata(typeof(StatsCard)));
        }

        /// <summary>
        /// Gets or sets the card title
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(StatsCard),
                new PropertyMetadata(string.Empty));

        /// <summary>
        /// Gets or sets the stat value
        /// </summary>
        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(string), typeof(StatsCard),
                new PropertyMetadata("0"));

        /// <summary>
        /// Gets or sets the icon
        /// </summary>
        public object Icon
        {
            get { return GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(object), typeof(StatsCard),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the icon background color
        /// </summary>
        public Brush IconBackground
        {
            get { return (Brush)GetValue(IconBackgroundProperty); }
            set { SetValue(IconBackgroundProperty, value); }
        }

        public static readonly DependencyProperty IconBackgroundProperty =
            DependencyProperty.Register("IconBackground", typeof(Brush), typeof(StatsCard),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the change percentage
        /// </summary>
        public string ChangePercent
        {
            get { return (string)GetValue(ChangePercentProperty); }
            set { SetValue(ChangePercentProperty, value); }
        }

        public static readonly DependencyProperty ChangePercentProperty =
            DependencyProperty.Register("ChangePercent", typeof(string), typeof(StatsCard),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets whether the change is positive
        /// </summary>
        public bool IsPositiveChange
        {
            get { return (bool)GetValue(IsPositiveChangeProperty); }
            set { SetValue(IsPositiveChangeProperty, value); }
        }

        public static readonly DependencyProperty IsPositiveChangeProperty =
            DependencyProperty.Register("IsPositiveChange", typeof(bool), typeof(StatsCard),
                new PropertyMetadata(true));
    }
}
