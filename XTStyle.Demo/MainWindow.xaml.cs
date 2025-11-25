using System.Windows;
using XTStyle.Demo.ViewModels;
using XTStyle.Demo.Models;
using XTStyle.Controls;

namespace XTStyle.Demo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            // Initialize Toast Notification system
            ToastNotification.Initialize(ToastContainer);
            
            // Setup Breadcrumb
            var vm = DataContext as MainViewModel;
            if (vm != null)
            {
                foreach (var item in vm.Breadcrumbs)
                {
                    BreadcrumbControl.Items.Add(item);
                }
                
                // Setup Progress Tracker
                foreach (var step in vm.ProgressSteps)
                {
                    ProgressTrackerControl.Steps.Add(step);
                }
            }
            
            // Show welcome toast
            Loaded += (s, e) =>
            {
                ToastNotification.Success("Welcome to XTStyle Demo! 🎉");
                
                // Demo different toast types
                System.Threading.Tasks.Task.Run(async () =>
                {
                    await System.Threading.Tasks.Task.Delay(2000);
                    Dispatcher.Invoke(() => ToastNotification.Info("Explore all 21 custom controls"));
                    
                    await System.Threading.Tasks.Task.Delay(2000);
                    Dispatcher.Invoke(() => ToastNotification.Warning("Remember to test theme switching!"));
                });
            };
            
            // Handle Breadcrumb clicks
            BreadcrumbControl.ItemClicked += (s, e) =>
            {
                ToastNotification.Info($"Navigated to: {e.Item.Text}");
            };
            
            // Handle Accordion events
            AccordionControl.Expanded += (s, e) =>
            {
                var item = e.Item as AccordionItem;
                if (item != null)
                {
                    ToastNotification.Info($"Expanded: {item.Header}");
                }
            };
            
            // Handle Tab closing
            MainTabs.Closing += (s, e) =>
            {
                var result = MessageBox.Show(
                    "Are you sure you want to close this tab?",
                    "Confirm Close",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);
                    
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            };
        }
    }
}
