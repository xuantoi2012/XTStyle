using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace XTStyle.Controls
{
    /// <summary>
    /// A modal dialog/popup control
    /// </summary>
    public class ModalDialog : ContentControl
    {
        static ModalDialog()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ModalDialog), new FrameworkPropertyMetadata(typeof(ModalDialog)));
        }

        public ModalDialog()
        {
            CloseCommand = new RelayCommand(Close);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // Hook up overlay click handler
            var overlay = GetTemplateChild("overlay") as System.Windows.FrameworkElement;
            if (overlay != null)
            {
                overlay.MouseLeftButtonDown += Overlay_MouseLeftButtonDown;
            }
        }

        private void Overlay_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (CloseOnOverlayClick && e.Source == sender)
            {
                Close();
                e.Handled = true;
            }
        }

        /// <summary>
        /// Gets or sets whether the modal is open
        /// </summary>
        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(ModalDialog),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// Gets or sets the modal title
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(ModalDialog),
                new PropertyMetadata(string.Empty));

        /// <summary>
        /// Gets or sets whether clicking overlay closes the modal
        /// </summary>
        public bool CloseOnOverlayClick
        {
            get { return (bool)GetValue(CloseOnOverlayClickProperty); }
            set { SetValue(CloseOnOverlayClickProperty, value); }
        }

        public static readonly DependencyProperty CloseOnOverlayClickProperty =
            DependencyProperty.Register("CloseOnOverlayClick", typeof(bool), typeof(ModalDialog),
                new PropertyMetadata(true));

        /// <summary>
        /// Gets the close command
        /// </summary>
        public ICommand CloseCommand
        {
            get { return (ICommand)GetValue(CloseCommandProperty); }
            private set { SetValue(CloseCommandProperty, value); }
        }

        public static readonly DependencyProperty CloseCommandProperty =
            DependencyProperty.Register("CloseCommand", typeof(ICommand), typeof(ModalDialog));

        private void Close()
        {
            IsOpen = false;
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
