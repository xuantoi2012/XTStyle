using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace XTStyle.Controls
{
    /// <summary>
    /// Toast notification types
    /// </summary>
    public enum ToastType
    {
        Success,
        Error,
        Warning,
        Info
    }

    /// <summary>
    /// A toast notification control with auto-dismiss and animations
    /// </summary>
    public class ToastNotification : Control
    {
        private static ObservableCollection<ToastNotification> _toasts;
        private static ItemsControl _toastContainer;
        private DispatcherTimer _dismissTimer;

        static ToastNotification()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToastNotification), new FrameworkPropertyMetadata(typeof(ToastNotification)));
            _toasts = new ObservableCollection<ToastNotification>();
        }

        public ToastNotification()
        {
            CloseCommand = new RelayCommand(Close);
        }

        /// <summary>
        /// Gets or sets the toast message
        /// </summary>
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(ToastNotification),
                new PropertyMetadata(string.Empty));

        /// <summary>
        /// Gets or sets the toast type
        /// </summary>
        public ToastType Type
        {
            get { return (ToastType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(ToastType), typeof(ToastNotification),
                new PropertyMetadata(ToastType.Info));

        /// <summary>
        /// Gets or sets the duration in milliseconds before auto-dismiss (0 = no auto-dismiss)
        /// </summary>
        public int Duration
        {
            get { return (int)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(int), typeof(ToastNotification),
                new PropertyMetadata(3000));

        /// <summary>
        /// Gets the close command
        /// </summary>
        public ICommand CloseCommand
        {
            get { return (ICommand)GetValue(CloseCommandProperty); }
            private set { SetValue(CloseCommandProperty, value); }
        }

        public static readonly DependencyProperty CloseCommandProperty =
            DependencyProperty.Register("CloseCommand", typeof(ICommand), typeof(ToastNotification));

        /// <summary>
        /// Gets the collection of active toasts
        /// </summary>
        public static ObservableCollection<ToastNotification> Toasts => _toasts;

        /// <summary>
        /// Initializes the toast container (call once in App.xaml.cs or MainWindow)
        /// </summary>
        public static void Initialize(ItemsControl container)
        {
            _toastContainer = container;
            if (_toastContainer != null)
                _toastContainer.ItemsSource = _toasts;
        }

        /// <summary>
        /// Shows a success toast
        /// </summary>
        public static void Success(string message, int duration = 3000)
        {
            Show(message, ToastType.Success, duration);
        }

        /// <summary>
        /// Shows an error toast
        /// </summary>
        public static void Error(string message, int duration = 5000)
        {
            Show(message, ToastType.Error, duration);
        }

        /// <summary>
        /// Shows a warning toast
        /// </summary>
        public static void Warning(string message, int duration = 4000)
        {
            Show(message, ToastType.Warning, duration);
        }

        /// <summary>
        /// Shows an info toast
        /// </summary>
        public static void Info(string message, int duration = 3000)
        {
            Show(message, ToastType.Info, duration);
        }

        /// <summary>
        /// Shows a toast with specified type and duration
        /// </summary>
        public static void Show(string message, ToastType type, int duration = 3000)
        {
            Application.Current?.Dispatcher.Invoke(() =>
            {
                var toast = new ToastNotification
                {
                    Message = message,
                    Type = type,
                    Duration = duration
                };

                _toasts.Add(toast);

                if (duration > 0)
                {
                    toast.StartDismissTimer();
                }
            });
        }

        private void StartDismissTimer()
        {
            _dismissTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(Duration)
            };
            _dismissTimer.Tick += (s, e) =>
            {
                _dismissTimer.Stop();
                Close();
            };
            _dismissTimer.Start();
        }

        private void Close()
        {
            _dismissTimer?.Stop();
            _toasts.Remove(this);
        }

        private class RelayCommand : ICommand
        {
            private readonly Action _execute;

            public RelayCommand(Action execute)
            {
                _execute = execute;
            }

            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter) => true;

            public void Execute(object parameter) => _execute();
        }
    }
}
