using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace XTStyle.Controls
{
    /// <summary>
    /// A control to switch between Light and Dark themes
    /// </summary>
    public class ThemeSwitcher : Control
    {
        static ThemeSwitcher()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ThemeSwitcher), new FrameworkPropertyMetadata(typeof(ThemeSwitcher)));
        }

        public ThemeSwitcher()
        {
            ToggleThemeCommand = new RelayCommand(ToggleTheme);
            
            // Subscribe to theme manager changes
            ThemeManager.Instance.ThemeChanged += OnThemeManagerChanged;
            IsDarkMode = ThemeManager.Instance.CurrentTheme == ThemeType.Dark;
        }

        /// <summary>
        /// Gets or sets whether dark mode is enabled
        /// </summary>
        public bool IsDarkMode
        {
            get { return (bool)GetValue(IsDarkModeProperty); }
            set { SetValue(IsDarkModeProperty, value); }
        }

        public static readonly DependencyProperty IsDarkModeProperty =
            DependencyProperty.Register("IsDarkMode", typeof(bool), typeof(ThemeSwitcher),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnIsDarkModeChanged));

        /// <summary>
        /// Gets the toggle theme command
        /// </summary>
        public ICommand ToggleThemeCommand
        {
            get { return (ICommand)GetValue(ToggleThemeCommandProperty); }
            private set { SetValue(ToggleThemeCommandProperty, value); }
        }

        public static readonly DependencyProperty ToggleThemeCommandProperty =
            DependencyProperty.Register("ToggleThemeCommand", typeof(ICommand), typeof(ThemeSwitcher));

        private static void OnIsDarkModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var switcher = (ThemeSwitcher)d;
            var isDark = (bool)e.NewValue;
            
            // Update theme manager
            ThemeManager.Instance.SetTheme(isDark ? ThemeType.Dark : ThemeType.Light);
        }

        private void OnThemeManagerChanged(object sender, ThemeType theme)
        {
            // Update control when theme changes externally
            IsDarkMode = theme == ThemeType.Dark;
        }

        private void ToggleTheme()
        {
            IsDarkMode = !IsDarkMode;
        }

        private class RelayCommand : ICommand
        {
            private readonly System.Action _execute;

            public RelayCommand(System.Action execute)
            {
                _execute = execute;
            }

            public event System.EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter) => true;

            public void Execute(object parameter) => _execute();
        }
    }
}
