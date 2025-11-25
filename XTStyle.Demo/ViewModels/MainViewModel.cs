using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using XTStyle.Demo.Models;
using XTStyle.Helpers;

namespace XTStyle.Demo.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _searchText;
        private int _quantity = 1;
        private bool _isEnabled = true;
        private bool _isLoading;
        private int _currentPage = 1;
        private string _selectedCategory;
        private DateTime? _startDate = DateTime.Now.AddDays(-7);
        private DateTime? _endDate = DateTime.Now;
        private string _fileName;
        private string _filePath;
        private bool _isDarkMode;
        private int _currentStep = 1;
        private bool _showModal;
        private bool _isAccordion1Expanded = true;
        private bool _isAccordion2Expanded;
        private int _selectedTabIndex;

        public MainViewModel()
        {
            // Initialize collections
            Products = SampleData.GetProducts();
            AllProducts = new ObservableCollection<Product>(Products);
            Categories = SampleData.GetCategories();
            Breadcrumbs = SampleData.GetBreadcrumbs();
            ProgressSteps = SampleData.GetProgressSteps();

            SelectedCategory = "All Categories";

            // Initialize commands
            SearchCommand = new RelayCommand(ExecuteSearch);
            ClearSearchCommand = new RelayCommand(ExecuteClearSearch);
            OpenFileCommand = new RelayCommand(ExecuteOpenFile);
            ConfirmCommand = new RelayCommand(ExecuteConfirm);
            ShowModalCommand = new RelayCommand(ExecuteShowModal);
            NextStepCommand = new RelayCommand(ExecuteNextStep, CanExecuteNextStep);
            PreviousStepCommand = new RelayCommand(ExecutePreviousStep, CanExecutePreviousStep);
            AddProductCommand = new RelayCommand(ExecuteAddProduct);
            RefreshCommand = new RelayCommand(ExecuteRefresh);
        }

        // Properties
        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Product> AllProducts { get; set; }
        public ObservableCollection<string> Categories { get; set; }
        public ObservableCollection<BreadcrumbItem> Breadcrumbs { get; set; }
        public ObservableCollection<ProgressStep> ProgressSteps { get; set; }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                FilterProducts();
            }
        }

        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged();
            }
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        public int TotalItems => AllProducts.Count;

        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
                FilterProducts();
            }
        }

        public DateTime? StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime? EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged();
            }
        }

        public string FileName
        {
            get => _fileName;
            set
            {
                _fileName = value;
                OnPropertyChanged();
            }
        }

        public string FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                OnPropertyChanged();
            }
        }

        public bool IsDarkMode
        {
            get => _isDarkMode;
            set
            {
                _isDarkMode = value;
                OnPropertyChanged();
            }
        }

        public int CurrentStep
        {
            get => _currentStep;
            set
            {
                _currentStep = value;
                OnPropertyChanged();
                ((RelayCommand)NextStepCommand).RaiseCanExecuteChanged();
                ((RelayCommand)PreviousStepCommand).RaiseCanExecuteChanged();
            }
        }

        public bool ShowModal
        {
            get => _showModal;
            set
            {
                _showModal = value;
                OnPropertyChanged();
            }
        }

        public bool IsAccordion1Expanded
        {
            get => _isAccordion1Expanded;
            set
            {
                _isAccordion1Expanded = value;
                OnPropertyChanged();
            }
        }

        public bool IsAccordion2Expanded
        {
            get => _isAccordion2Expanded;
            set
            {
                _isAccordion2Expanded = value;
                OnPropertyChanged();
            }
        }

        public int SelectedTabIndex
        {
            get => _selectedTabIndex;
            set
            {
                _selectedTabIndex = value;
                OnPropertyChanged();
            }
        }

        // Commands
        public ICommand SearchCommand { get; }
        public ICommand ClearSearchCommand { get; }
        public ICommand OpenFileCommand { get; }
        public ICommand ConfirmCommand { get; }
        public ICommand ShowModalCommand { get; }
        public ICommand NextStepCommand { get; }
        public ICommand PreviousStepCommand { get; }
        public ICommand AddProductCommand { get; }
        public ICommand RefreshCommand { get; }

        // Command implementations
        private void ExecuteSearch()
        {
            FilterProducts();
        }

        private void ExecuteClearSearch()
        {
            SearchText = string.Empty;
        }

        private void ExecuteOpenFile()
        {
            // File dialog would be opened here
            FileName = "sample-document.pdf";
            FilePath = @"C:\Documents\sample-document.pdf";
        }

        private void ExecuteConfirm()
        {
            ShowModal = false;
        }

        private void ExecuteShowModal()
        {
            ShowModal = true;
        }

        private void ExecuteNextStep()
        {
            if (CurrentStep < ProgressSteps.Count)
            {
                CurrentStep++;
            }
        }

        private bool CanExecuteNextStep()
        {
            return CurrentStep < ProgressSteps.Count;
        }

        private void ExecutePreviousStep()
        {
            if (CurrentStep > 0)
            {
                CurrentStep--;
            }
        }

        private bool CanExecutePreviousStep()
        {
            return CurrentStep > 0;
        }

        private void ExecuteAddProduct()
        {
            var newProduct = new Product
            {
                Id = AllProducts.Count + 1,
                Name = "New Product",
                Category = "Electronics",
                Price = 99.99m,
                Stock = 10,
                Status = "Active",
                CreatedDate = DateTime.Now
            };
            AllProducts.Add(newProduct);
            FilterProducts();
        }

        private async void ExecuteRefresh()
        {
            IsLoading = true;
            await System.Threading.Tasks.Task.Delay(2000); // Simulate loading
            IsLoading = false;
        }

        private void FilterProducts()
        {
            var filtered = AllProducts.AsEnumerable();

            // Filter by search text
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filtered = filtered.Where(p => 
                    p.Name.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    p.Category.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            // Filter by category
            if (!string.IsNullOrWhiteSpace(SelectedCategory) && SelectedCategory != "All Categories")
            {
                filtered = filtered.Where(p => p.Category == SelectedCategory);
            }

            Products.Clear();
            foreach (var product in filtered)
            {
                Products.Add(product);
            }

            OnPropertyChanged(nameof(TotalItems));
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
