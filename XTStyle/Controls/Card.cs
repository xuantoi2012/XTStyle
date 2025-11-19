using System.Windows;
using System.Windows.Controls;

namespace XTStyle.Controls
{
    /// <summary>
    /// A card component with optional header and footer
    /// </summary>
    public class Card : ContentControl
    {
        static Card()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Card), new FrameworkPropertyMetadata(typeof(Card)));
        }

        /// <summary>
        /// Gets or sets the card header content
        /// </summary>
        public object Header
        {
            get { return GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(object), typeof(Card),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the card footer content
        /// </summary>
        public object Footer
        {
            get { return GetValue(FooterProperty); }
            set { SetValue(FooterProperty, value); }
        }

        public static readonly DependencyProperty FooterProperty =
            DependencyProperty.Register("Footer", typeof(object), typeof(Card),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets whether the card has hover effects
        /// </summary>
        public bool IsHoverable
        {
            get { return (bool)GetValue(IsHoverableProperty); }
            set { SetValue(IsHoverableProperty, value); }
        }

        public static readonly DependencyProperty IsHoverableProperty =
            DependencyProperty.Register("IsHoverable", typeof(bool), typeof(Card),
                new PropertyMetadata(false));

        /// <summary>
        /// Gets or sets the card elevation (shadow depth)
        /// </summary>
        public double Elevation
        {
            get { return (double)GetValue(ElevationProperty); }
            set { SetValue(ElevationProperty, value); }
        }

        public static readonly DependencyProperty ElevationProperty =
            DependencyProperty.Register("Elevation", typeof(double), typeof(Card),
                new PropertyMetadata(2.0));
    }
}
