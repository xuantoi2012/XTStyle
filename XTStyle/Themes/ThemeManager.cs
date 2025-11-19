using System;
using System.Windows;
using System.Windows.Media;

namespace XTStyle.Themes
{
    public class ThemeManager
    {
        private static ThemeManager _instance;
        public static ThemeManager Instance => _instance ?? (_instance = new ThemeManager());

        public event EventHandler<ThemeChangedEventArgs> ThemeChanged;

        private ThemeMode _currentTheme = ThemeMode.Light;

        public ThemeMode CurrentTheme
        {
            get => _currentTheme;
            set
            {
                if (_currentTheme != value)
                {
                    _currentTheme = value;
                    ApplyTheme(value);
                    ThemeChanged?.Invoke(this, new ThemeChangedEventArgs(value));
                }
            }
        }

        private ThemeManager()
        {
            // Constructor rỗng, không load Settings
        }

        public void ApplyTheme(ThemeMode theme)
        {
            var app = Application.Current;
            if (app == null) return;

            var resources = app.Resources;

            switch (theme)
            {
                case ThemeMode.Light:
                    ApplyLightTheme(resources);
                    break;
                case ThemeMode.Dark:
                    ApplyDarkTheme(resources);
                    break;
            }
        }

        private void ApplyLightTheme(ResourceDictionary resources)
        {
            // Primary Colors
            UpdateResource(resources, "PrimaryBrush", Color.FromRgb(0, 99, 163));
            UpdateResource(resources, "PrimaryHoverBrush", Color.FromRgb(0, 154, 217));

            // Secondary & Status Colors
            UpdateResource(resources, "SecondaryBrush", Color.FromRgb(16, 185, 129));
            UpdateResource(resources, "DangerBrush", Color.FromRgb(239, 68, 68));
            UpdateResource(resources, "AccentBrush", Color.FromRgb(255, 125, 41));
            UpdateResource(resources, "AccentHoverBrush", Color.FromRgb(255, 166, 47));

            // Background Colors
            UpdateResource(resources, "CardBrush", Colors.White);
            UpdateResource(resources, "BackgroundBrush", Color.FromRgb(249, 250, 251));

            // Border & Text Colors
            UpdateResource(resources, "BorderBrush", Color.FromRgb(229, 231, 235));
            UpdateResource(resources, "TextPrimaryBrush", Color.FromRgb(17, 24, 39));
            UpdateResource(resources, "TextSecondaryBrush", Color.FromRgb(107, 114, 128));
        }

        private void ApplyDarkTheme(ResourceDictionary resources)
        {
            // Primary Colors
            UpdateResource(resources, "PrimaryBrush", Color.FromRgb(59, 130, 246));
            UpdateResource(resources, "PrimaryHoverBrush", Color.FromRgb(96, 165, 250));

            // Secondary & Status Colors
            UpdateResource(resources, "SecondaryBrush", Color.FromRgb(16, 185, 129));
            UpdateResource(resources, "DangerBrush", Color.FromRgb(239, 68, 68));
            UpdateResource(resources, "AccentBrush", Color.FromRgb(251, 146, 60));
            UpdateResource(resources, "AccentHoverBrush", Color.FromRgb(253, 186, 116));

            // Background Colors
            UpdateResource(resources, "CardBrush", Color.FromRgb(31, 41, 55));
            UpdateResource(resources, "BackgroundBrush", Color.FromRgb(17, 24, 39));

            // Border & Text Colors
            UpdateResource(resources, "BorderBrush", Color.FromRgb(55, 65, 81));
            UpdateResource(resources, "TextPrimaryBrush", Color.FromRgb(243, 244, 246));
            UpdateResource(resources, "TextSecondaryBrush", Color.FromRgb(156, 163, 175));
        }

        private void UpdateResource(ResourceDictionary resources, string key, Color color)
        {
            if (resources.Contains(key))
            {
                resources[key] = new SolidColorBrush(color);
            }
            else
            {
                resources.Add(key, new SolidColorBrush(color));
            }
        }

        public void ToggleTheme()
        {
            CurrentTheme = CurrentTheme == ThemeMode.Light ? ThemeMode.Dark : ThemeMode.Light;
        }
    }

    public enum ThemeMode
    {
        Light,
        Dark
    }

    public class ThemeChangedEventArgs : EventArgs
    {
        public ThemeMode Theme { get; }

        public ThemeChangedEventArgs(ThemeMode theme)
        {
            Theme = theme;
        }
    }
}