using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace XTStyle.Controls
{
    /// <summary>
    /// An iOS-style toggle switch control
    /// </summary>
    public class ToggleSwitch : Control
    {
        static ToggleSwitch()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleSwitch), new FrameworkPropertyMetadata(typeof(ToggleSwitch)));
        }

        public ToggleSwitch()
        {
            ToggleCommand = new RelayCommand(Toggle);
        }

        /// <summary>
        /// Gets or sets whether the switch is on
        /// </summary>
        public bool IsOn
        {
            get { return (bool)GetValue(IsOnProperty); }
            set { SetValue(IsOnProperty, value); }
        }

        public static readonly DependencyProperty IsOnProperty =
            DependencyProperty.Register("IsOn", typeof(bool), typeof(ToggleSwitch),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnIsOnChanged));

        /// <summary>
        /// Gets or sets the text displayed when the switch is on
        /// </summary>
        public string OnText
        {
            get { return (string)GetValue(OnTextProperty); }
            set { SetValue(OnTextProperty, value); }
        }

        public static readonly DependencyProperty OnTextProperty =
            DependencyProperty.Register("OnText", typeof(string), typeof(ToggleSwitch),
                new PropertyMetadata("ON"));

        /// <summary>
        /// Gets or sets the text displayed when the switch is off
        /// </summary>
        public string OffText
        {
            get { return (string)GetValue(OffTextProperty); }
            set { SetValue(OffTextProperty, value); }
        }

        public static readonly DependencyProperty OffTextProperty =
            DependencyProperty.Register("OffText", typeof(string), typeof(ToggleSwitch),
                new PropertyMetadata("OFF"));

        /// <summary>
        /// Gets the command to toggle the switch
        /// </summary>
        public ICommand ToggleCommand
        {
            get { return (ICommand)GetValue(ToggleCommandProperty); }
            private set { SetValue(ToggleCommandProperty, value); }
        }

        public static readonly DependencyProperty ToggleCommandProperty =
            DependencyProperty.Register("ToggleCommand", typeof(ICommand), typeof(ToggleSwitch));

        /// <summary>
        /// Event raised when IsOn changes
        /// </summary>
        public event RoutedPropertyChangedEventHandler<bool> Toggled;

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            if (IsEnabled)
            {
                Toggle();
                e.Handled = true;
            }
        }

        private static void OnIsOnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var toggleSwitch = (ToggleSwitch)d;
            toggleSwitch.Toggled?.Invoke(toggleSwitch, new RoutedPropertyChangedEventArgs<bool>(
                (bool)e.OldValue, (bool)e.NewValue));
        }

        private void Toggle()
        {
            IsOn = !IsOn;
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
