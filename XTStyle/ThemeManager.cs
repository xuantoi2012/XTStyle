using System;
using System.Windows;
using System.Windows.Media;

namespace XTStyle
{
    /// <summary>
    /// Theme types supported by the application
    /// </summary>
    public enum ThemeType
    {
        Light,
        Dark
    }

    /// <summary>
    /// Manages application theme switching between Light and Dark modes
    /// </summary>
    public class ThemeManager
    {
        private static ThemeManager _instance;
        private static readonly object _lock = new object();
        private ThemeType _currentTheme = ThemeType.Light;

        /// <summary>
        /// Gets the singleton instance of ThemeManager
        /// </summary>
        public static ThemeManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                            _instance = new ThemeManager();
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Event raised when theme changes
        /// </summary>
        public event EventHandler<ThemeType> ThemeChanged;

        /// <summary>
        /// Gets the current theme
        /// </summary>
        public ThemeType CurrentTheme
        {
            get => _currentTheme;
            private set
            {
                if (_currentTheme != value)
                {
                    _currentTheme = value;
                    ThemeChanged?.Invoke(this, _currentTheme);
                }
            }
        }

        private ThemeManager()
        {
            // Load saved theme preference
            LoadThemePreference();
        }

        /// <summary>
        /// Sets the application theme
        /// </summary>
        /// <param name="theme">Theme to apply</param>
        public void SetTheme(ThemeType theme)
        {
            CurrentTheme = theme;
            ApplyTheme(theme);
            SaveThemePreference(theme);
        }

        /// <summary>
        /// Toggles between Light and Dark themes
        /// </summary>
        public void ToggleTheme()
        {
            SetTheme(CurrentTheme == ThemeType.Light ? ThemeType.Dark : ThemeType.Light);
        }

        private void ApplyTheme(ThemeType theme)
        {
            var resources = Application.Current?.Resources;
            if (resources == null) return;

            // Define theme colors
            if (theme == ThemeType.Dark)
            {
                // Dark theme colors
                resources["CardBrush"] = new SolidColorBrush(Color.FromRgb(0x1F, 0x29, 0x37));
                resources["BackgroundBrush"] = new SolidColorBrush(Color.FromRgb(0x11, 0x18, 0x27));
                resources["BorderBrush"] = new SolidColorBrush(Color.FromRgb(0x37, 0x41, 0x51));
                resources["TextPrimaryBrush"] = new SolidColorBrush(Color.FromRgb(0xF9, 0xFA, 0xFB));
                resources["TextSecondaryBrush"] = new SolidColorBrush(Color.FromRgb(0x9C, 0xA3, 0xAF));
            }
            else
            {
                // Light theme colors (default)
                resources["CardBrush"] = new SolidColorBrush(Colors.White);
                resources["BackgroundBrush"] = new SolidColorBrush(Color.FromRgb(0xF9, 0xFA, 0xFB));
                resources["BorderBrush"] = new SolidColorBrush(Color.FromRgb(0xE5, 0xE7, 0xEB));
                resources["TextPrimaryBrush"] = new SolidColorBrush(Color.FromRgb(0x11, 0x18, 0x27));
                resources["TextSecondaryBrush"] = new SolidColorBrush(Color.FromRgb(0x6B, 0x72, 0x80));
            }
        }

        private void LoadThemePreference()
        {
            try
            {
                string themePref = Properties.Settings.Default.Theme;
                if (Enum.TryParse(themePref, out ThemeType theme))
                {
                    SetTheme(theme);
                }
            }
            catch
            {
                // Default to light theme if loading fails
                SetTheme(ThemeType.Light);
            }
        }

        private void SaveThemePreference(ThemeType theme)
        {
            try
            {
                Properties.Settings.Default.Theme = theme.ToString();
                Properties.Settings.Default.Save();
            }
            catch
            {
                // Ignore save errors
            }
        }
    }
}
