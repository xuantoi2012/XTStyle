using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;

namespace XTStyle.Controls
{
    /// <summary>
    /// A file upload control with UI
    /// </summary>
    public class FileUpload : Control
    {
        static FileUpload()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FileUpload), new FrameworkPropertyMetadata(typeof(FileUpload)));
        }

        public FileUpload()
        {
            BrowseCommand = new RelayCommand(Browse);
            ClearCommand = new RelayCommand(Clear, () => !string.IsNullOrEmpty(FileName));
        }

        /// <summary>
        /// Gets or sets the selected file name
        /// </summary>
        public string FileName
        {
            get { return (string)GetValue(FileNameProperty); }
            set { SetValue(FileNameProperty, value); }
        }

        public static readonly DependencyProperty FileNameProperty =
            DependencyProperty.Register("FileName", typeof(string), typeof(FileUpload),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnFileChanged));

        /// <summary>
        /// Gets or sets the full file path
        /// </summary>
        public string FilePath
        {
            get { return (string)GetValue(FilePathProperty); }
            set { SetValue(FilePathProperty, value); }
        }

        public static readonly DependencyProperty FilePathProperty =
            DependencyProperty.Register("FilePath", typeof(string), typeof(FileUpload),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// Gets or sets the file filter
        /// </summary>
        public string Filter
        {
            get { return (string)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }

        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(string), typeof(FileUpload),
                new PropertyMetadata("All Files (*.*)|*.*"));

        /// <summary>
        /// Gets or sets the placeholder text
        /// </summary>
        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder", typeof(string), typeof(FileUpload),
                new PropertyMetadata("No file selected"));

        /// <summary>
        /// Gets whether a file is selected
        /// </summary>
        public bool HasFile
        {
            get { return (bool)GetValue(HasFileProperty); }
            private set { SetValue(HasFileProperty, value); }
        }

        public static readonly DependencyProperty HasFileProperty =
            DependencyProperty.Register("HasFile", typeof(bool), typeof(FileUpload),
                new PropertyMetadata(false));

        /// <summary>
        /// Gets the browse command
        /// </summary>
        public ICommand BrowseCommand
        {
            get { return (ICommand)GetValue(BrowseCommandProperty); }
            private set { SetValue(BrowseCommandProperty, value); }
        }

        public static readonly DependencyProperty BrowseCommandProperty =
            DependencyProperty.Register("BrowseCommand", typeof(ICommand), typeof(FileUpload));

        /// <summary>
        /// Gets the clear command
        /// </summary>
        public ICommand ClearCommand
        {
            get { return (ICommand)GetValue(ClearCommandProperty); }
            private set { SetValue(ClearCommandProperty, value); }
        }

        public static readonly DependencyProperty ClearCommandProperty =
            DependencyProperty.Register("ClearCommand", typeof(ICommand), typeof(FileUpload));

        /// <summary>
        /// Event raised when file selection changes
        /// </summary>
        public event RoutedEventHandler FileChanged;

        private static void OnFileChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (FileUpload)d;
            control.HasFile = !string.IsNullOrEmpty((string)e.NewValue);
            control.FileChanged?.Invoke(control, new RoutedEventArgs());
            CommandManager.InvalidateRequerySuggested();
        }

        private void Browse()
        {
            var dialog = new OpenFileDialog
            {
                Filter = Filter
            };

            if (dialog.ShowDialog() == true)
            {
                FilePath = dialog.FileName;
                FileName = System.IO.Path.GetFileName(dialog.FileName);
            }
        }

        private void Clear()
        {
            FileName = string.Empty;
            FilePath = string.Empty;
        }

        private class RelayCommand : ICommand
        {
            private readonly System.Action _execute;
            private readonly System.Func<bool> _canExecute;

            public RelayCommand(System.Action execute, System.Func<bool> canExecute = null)
            {
                _execute = execute;
                _canExecute = canExecute;
            }

            public event System.EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

            public void Execute(object parameter) => _execute();
        }
    }
}
