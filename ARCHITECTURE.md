# 📐 XTStyle Architecture

## 🏗️ Solution Structure

```
XTStyle.sln
│
├── 📦 XTStyle (Class Library)
│   │
│   ├── 📁 Controls/ (21 Custom Controls)
│   │   ├── Accordion.cs
│   │   ├── Badge.cs
│   │   ├── Breadcrumb.cs
│   │   ├── Card.cs
│   │   ├── ClosableTabControl.cs
│   │   ├── DateRangePicker.cs
│   │   ├── EmptyState.cs
│   │   ├── FileUpload.cs
│   │   ├── FilterableDataGrid.cs
│   │   ├── IconButton.cs
│   │   ├── LoadingSpinner.cs
│   │   ├── ModalDialog.cs
│   │   ├── NumericUpDown.cs
│   │   ├── Pagination.cs
│   │   ├── ProgressTracker.cs
│   │   ├── RippleButton.cs
│   │   ├── SearchBox.cs
│   │   ├── StatsCard.cs
│   │   ├── ThemeSwitcher.cs
│   │   ├── ToastNotification.cs
│   │   └── ToggleSwitch.cs
│   │
│   ├── 📁 Themes/ (Theme Files - MODULAR)
│   │   ├── Generic.xaml ⭐ (Master - merges all)
│   │   ├── Colors.xaml
│   │   ├── Brushes.xaml
│   │   ├── Fonts.xaml
│   │   ├── Buttons.xaml
│   │   ├── TextBoxes.xaml
│   │   ├── ComboBoxes.xaml
│   │   ├── CheckBoxes.xaml
│   │   ├── DataGrids.xaml
│   │   ├── Labels.xaml
│   │   ├── GroupBoxes.xaml
│   │   ├── ScrollBars.xaml
│   │   ├── Cards.xaml
│   │   ├── DatePickers.xaml
│   │   ├── CustomControls.xaml
│   │   └── ThemeManager.cs
│   │
│   ├── 📁 Converters/
│   │   └── Converters.cs (5 value converters)
│   │
│   ├── 📁 Helpers/
│   │   └── RelayCommand.cs
│   │
│   └── 📁 Properties/
│       ├── AssemblyInfo.cs
│       ├── Resources.resx
│       └── Settings.settings
│
└── 🎯 XTStyle.Demo (WPF Application)
    │
    ├── 📁 Models/
    │   └── SampleData.cs
    │       ├── Product class
    │       ├── BreadcrumbItem class
    │       ├── ProgressStep class
    │       ├── GetProducts() → 15 sample products
    │       ├── GetCategories() → 6 categories
    │       ├── GetBreadcrumbs() → 3 items
    │       └── GetProgressSteps() → 4 steps
    │
    ├── 📁 ViewModels/
    │   └── MainViewModel.cs (MVVM)
    │       ├── Properties (20+)
    │       ├── Commands (9)
    │       └── Methods (filtering, etc.)
    │
    ├── 📁 Properties/
    │   ├── AssemblyInfo.cs
    │   ├── Resources.resx
    │   └── Settings.settings
    │
    ├── App.xaml + App.xaml.cs
    ├── MainWindow.xaml + MainWindow.xaml.cs
    └── App.config
```

---

## 🔄 Data Flow (MVVM Pattern)

```
┌─────────────────────────────────────────────────────────┐
│                    MainWindow.xaml                       │
│  ┌───────────────────────────────────────────────────┐  │
│  │ View (UI)                                         │  │
│  │ - SearchBox, DataGrid, Buttons, etc.              │  │
│  │ - Bindings: {Binding SearchText}                  │  │
│  │ - Commands: {Binding SearchCommand}               │  │
│  └───────────────────────────────────────────────────┘  │
│                          ↕ Binding                       │
│  ┌───────────────────────────────────────────────────┐  │
│  │ ViewModel (MainViewModel.cs)                      │  │
│  │ - Properties: SearchText, Products, etc.          │  │
│  │ - Commands: SearchCommand, RefreshCommand         │  │
│  │ - Logic: FilterProducts(), ExecuteSearch()        │  │
│  │ - INotifyPropertyChanged                          │  │
│  └───────────────────────────────────────────────────┘  │
│                          ↕                               │
│  ┌───────────────────────────────────────────────────┐  │
│  │ Model (SampleData.cs)                             │  │
│  │ - Product, BreadcrumbItem, ProgressStep           │  │
│  │ - GetProducts(), GetCategories(), etc.            │  │
│  └───────────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────────┘
```

---

## 🎨 Theme System

```
App.xaml
  └─ Merges: Generic.xaml
       │
       ├─ Colors.xaml → Color definitions
       ├─ Brushes.xaml → Brush resources
       ├─ Fonts.xaml → Font families
       │
       ├─ Buttons.xaml → Button styles
       ├─ TextBoxes.xaml → TextBox styles
       ├─ ComboBoxes.xaml → ComboBox styles
       ├─ CheckBoxes.xaml → CheckBox styles
       ├─ DataGrids.xaml → DataGrid styles
       ├─ Labels.xaml → Label styles
       ├─ GroupBoxes.xaml → GroupBox styles
       ├─ ScrollBars.xaml → ScrollBar styles
       ├─ Cards.xaml → Card styles
       ├─ DatePickers.xaml → DatePicker styles
       │
       └─ CustomControls.xaml → 21 Custom Control Templates
            ├─ SearchBox Template
            ├─ NumericUpDown Template
            ├─ ToggleSwitch Template
            ├─ ... (18 more)
            └─ Accordion Template

ThemeManager.cs
  ├─ SetTheme(ThemeType.Light/Dark)
  ├─ ToggleTheme()
  └─ ThemeChanged event
```

---

## 📊 Demo Window Layout

```
┌──────────────────────────────────────────────────────────────┐
│ Header                                    [ThemeSwitcher]    │
│ 🎨 XTStyle Controls Demo [v1.0]                              │
├──────────────────────────────────────────────────────────────┤
│ [Breadcrumb: Home / Products / Electronics]                  │
├──────────────────────────────────────────────────────────────┤
│ 🔍 Search & Filters (Card)                                   │
│ [SearchBox] [ComboBox] [🔄 Refresh]                          │
│ [DateRangePicker]                                            │
├──────────────────────────────────────────────────────────────┤
│ 📊 Statistics                                                │
│ [StatsCard] [StatsCard] [StatsCard] [StatsCard]             │
├──────────────────────────────────────────────────────────────┤
│ [Tab: 📋 Products] [Tab: 📝 Forms] [Tab: ⚙️ Advanced]       │
│ ┌────────────────────────────────────────────────────────┐  │
│ │ TAB 1: Products                                        │  │
│ │ ┌────────────────────────────────────────────────────┐ │  │
│ │ │ FilterableDataGrid (15 products)                   │ │  │
│ │ │ ID | Name | Category | Price | Stock | Status      │ │  │
│ │ └────────────────────────────────────────────────────┘ │  │
│ │ [Pagination: ◀ 1 2 3 ▶]                               │  │
│ └────────────────────────────────────────────────────────┘  │
├──────────────────────────────────────────────────────────────┤
│ Other Controls (Card)                                        │
│ [LoadingSpinner] [Show Modal] [Badges: NEW 5 ✓ •]           │
└──────────────────────────────────────────────────────────────┘
     [Toast Notifications] ↗ (top-right corner)
```

---

## 🔌 Control Dependencies

```
Custom Controls → Generic.xaml → Individual Theme Files

Example: SearchBox
  ├─ Inherits from: Control
  ├─ Template in: CustomControls.xaml
  ├─ Uses styles from:
  │   ├─ Colors.xaml (PrimaryBrush, BorderBrush)
  │   ├─ Fonts.xaml (FontSize, FontFamily)
  │   └─ Buttons.xaml (Clear button style)
  └─ Uses converters:
      └─ StringEmptyToVisibilityConverter
```

---

## 🎯 Build Process

```
1. Build XTStyle.csproj
   ├─ Compile 21 Control classes
   ├─ Compile Converters, Helpers
   ├─ Compile ThemeManager
   └─ Package all XAML resources
   └─ Output: XTStyle.dll

2. Build XTStyle.Demo.csproj
   ├─ Reference XTStyle.dll
   ├─ Compile ViewModels, Models
   ├─ Compile MainWindow
   └─ Output: XTStyle.Demo.exe

3. Run XTStyle.Demo.exe
   ├─ Load Generic.xaml (all themes)
   ├─ Initialize MainViewModel
   ├─ Initialize ToastNotification
   ├─ Setup Breadcrumb, ProgressTracker
   └─ Show MainWindow
```

---

## 📦 Distribution

```
XTStyle Library Usage:

Your WPF App
  ├─ Add Reference: XTStyle.dll
  ├─ App.xaml:
  │   └─ Merge: /XTStyle;component/Themes/Generic.xaml
  ├─ XAML:
  │   └─ xmlns:local="clr-namespace:XTStyle.Controls;assembly=XTStyle"
  └─ Use controls:
      └─ <local:SearchBox ... />
```

---

**Architecture Summary:**
- ✅ Modular theme files (easy to maintain)
- ✅ MVVM pattern (clean separation)
- ✅ Reusable controls (21 controls)
- ✅ Sample data (realistic demo)
- ✅ Complete documentation
