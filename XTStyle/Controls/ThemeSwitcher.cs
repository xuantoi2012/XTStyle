using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using XTStyle.Themes; // ⭐ QUAN TRỌNG - THÊM DÒNG NÀY

namespace XTStyle.Controls
{
    public class ThemeSwitcher : ToggleButton
    {
        static ThemeSwitcher()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ThemeSwitcher),
                new FrameworkPropertyMetadata(typeof(ThemeSwitcher)));
        }

        // IsDarkMode Property
        public static readonly DependencyProperty IsDarkModeProperty =
            DependencyProperty.Register("IsDarkMode", typeof(bool), typeof(ThemeSwitcher),
                new PropertyMetadata(false, OnIsDarkModeChanged));

        public bool IsDarkMode
        {
            get { return (bool)GetValue(IsDarkModeProperty); }
            set { SetValue(IsDarkModeProperty, value); }
        }

        public ThemeSwitcher()
        {
            // Load current theme state
            IsDarkMode = ThemeManager.Instance.CurrentTheme == ThemeMode.Dark;

            // Listen to IsChecked changes
            this.Checked += (s, e) => IsDarkMode = true;
            this.Unchecked += (s, e) => IsDarkMode = false;
        }

        private static void OnIsDarkModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var switcher = (ThemeSwitcher)d;
            var isDark = (bool)e.NewValue;

            // Update theme
            ThemeManager.Instance.CurrentTheme = isDark ? ThemeMode.Dark : ThemeMode.Light;

            // Update IsChecked to match
            switcher.IsChecked = isDark;
        }
    }
}