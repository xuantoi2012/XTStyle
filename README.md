# XTStyle Library - Optimized & Demo

## 📋 Tổng quan

XTStyle là một thư viện WPF control library hiện đại với **21 custom controls**, hỗ trợ Light/Dark themes và animations mượt mà.

## ✨ Tối ưu hóa đã thực hiện

### 1. **Cấu trúc Theme được tối ưu**
- ✅ **Giữ lại các theme files riêng lẻ** để dễ bảo trì:
  - `Colors.xaml` - Bảng màu
  - `Brushes.xaml` - Brushes
  - `Fonts.xaml` - Fonts
  - `Buttons.xaml` - Button styles
  - `TextBoxes.xaml` - TextBox styles
  - `ComboBoxes.xaml` - ComboBox styles
  - `CheckBoxes.xaml` - CheckBox styles
  - `DataGrids.xaml` - DataGrid styles
  - `Labels.xaml` - Label styles
  - `GroupBoxes.xaml` - GroupBox styles
  - `ScrollBars.xaml` - ScrollBar styles
  - `Cards.xaml` - Card styles
  - `DatePickers.xaml` - DatePicker styles
  - `CustomControls.xaml` - Custom control templates

- ❌ **Đã loại bỏ `XTStyle.xaml`** (file theme tổng không cần thiết)
- ✅ **`Generic.xaml`** merge tất cả theme files riêng lẻ

### 2. **21 Custom Controls**

#### 🔍 Input & Search
1. **SearchBox** - Search input với icon và clear button
2. **NumericUpDown** - Numeric input với up/down buttons
3. **FileUpload** - File upload control với UI
4. **DateRangePicker** - Date range picker

#### 🎛️ Selection & Toggle
5. **ToggleSwitch** - iOS-style toggle switch
6. **ThemeSwitcher** - Light/Dark mode switcher

#### 🎨 Display & Layout
7. **Card** - Card component với header và footer
8. **Badge** - Badge component với nhiều types
9. **StatsCard** - Dashboard statistics widget
10. **EmptyState** - Empty state component

#### 📊 Data & Navigation
11. **FilterableDataGrid** - DataGrid với built-in filtering
12. **Pagination** - Complete pagination control
13. **Breadcrumb** - Breadcrumb navigation
14. **ClosableTabControl** - Tab control với close buttons

#### 🔘 Buttons & Actions
15. **IconButton** - Button với icon support
16. **RippleButton** - Button với Material Design ripple effect

#### 📢 Feedback & Progress
17. **ToastNotification** - Toast notification system
18. **LoadingSpinner** - Animated loading spinner
19. **ProgressTracker** - Step-by-step progress tracker (stepper)
20. **ModalDialog** - Modal dialog/popup
21. **Accordion** - Collapsible panels container

## 🚀 Demo Application

### XTStyle.Demo Project
Đã tạo một WPF demo application hoàn chỉnh với:

- ✅ **Tất cả 21 controls** được demo với sample data
- ✅ **Sample data models** (Products, Categories, Breadcrumbs, Progress Steps)
- ✅ **MVVM pattern** với ViewModels đầy đủ
- ✅ **Interactive features**:
  - Search & Filter products
  - Pagination
  - Theme switching (Light/Dark)
  - Toast notifications
  - Modal dialogs
  - Progress tracking
  - Accordion panels
  - Closable tabs
  - Stats cards với data thực
  - Form controls đầy đủ

## 🏗️ Cấu trúc Solution

```
XTStyle/
├── XTStyle/                    # Library project
│   ├── Controls/              # 21 custom controls
│   ├── Converters/            # Value converters
│   ├── Helpers/               # Helper classes (RelayCommand)
│   ├── Themes/                # Theme files (riêng lẻ)
│   │   ├── Generic.xaml      # Master theme file
│   │   ├── Colors.xaml
│   │   ├── Brushes.xaml
│   │   ├── Buttons.xaml
│   │   └── ... (các theme khác)
│   └── XTStyle.csproj
│
└── XTStyle.Demo/              # Demo application
    ├── Models/                # Data models
    │   └── SampleData.cs     # Sample data generator
    ├── ViewModels/            # ViewModels
    │   └── MainViewModel.cs  # Main ViewModel
    ├── MainWindow.xaml        # Main demo window
    └── XTStyle.Demo.csproj
```

## 📦 Build Instructions

### Yêu cầu
- Visual Studio 2019 hoặc mới hơn
- .NET Framework 4.8

### Build từ Visual Studio
1. Mở `XTStyle.sln` trong Visual Studio
2. Chọn **Build > Rebuild Solution** (Ctrl+Shift+B)
3. Set **XTStyle.Demo** làm StartUp Project
4. Nhấn **F5** để run demo

### Build từ Command Line

```powershell
# Tìm MSBuild path
$msbuild = & "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe" -latest -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe | Select-Object -First 1

# Build solution
& $msbuild XTStyle.sln /t:Rebuild /p:Configuration=Debug

# Hoặc build Release
& $msbuild XTStyle.sln /t:Rebuild /p:Configuration=Release
```

### Run Demo
```powershell
# Sau khi build
.\XTStyle.Demo\bin\Debug\XTStyle.Demo.exe
```

## 🎯 Sử dụng XTStyle trong project của bạn

### 1. Add Reference
Thêm reference đến `XTStyle.dll` trong project của bạn

### 2. Merge Resources
Trong `App.xaml` hoặc `Window.Resources`:

```xaml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="/XTStyle;component/Themes/Generic.xaml"/>
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>
```

### 3. Add Namespace
Trong XAML file:

```xaml
xmlns:local="clr-namespace:XTStyle.Controls;assembly=XTStyle"
```

### 4. Sử dụng Controls

```xaml
<!-- SearchBox -->
<local:SearchBox Text="{Binding SearchText}" 
                Placeholder="Search..."/>

<!-- StatsCard -->
<local:StatsCard Title="Total Sales" 
                Value="$12,450" 
                Icon="💰"
                IconBackground="{StaticResource SecondaryBrush}"
                ChangePercent="+12.5%"
                IsPositiveChange="True"/>

<!-- Card -->
<local:Card IsHoverable="True">
    <local:Card.Header>
        <TextBlock Text="Card Title"/>
    </local:Card.Header>
    <TextBlock Text="Card content"/>
</local:Card>
```

## 🎨 Theme Management

```csharp
using XTStyle;

// Set theme
ThemeManager.Instance.SetTheme(ThemeType.Dark);

// Toggle theme
ThemeManager.Instance.ToggleTheme();

// Subscribe to theme changes
ThemeManager.Instance.ThemeChanged += (sender, theme) => {
    // Handle theme change
};
```

## 📚 Documentation

Xem `CONTROLS_GUIDE.md` để biết chi tiết về từng control và cách sử dụng.

## ✅ Checklist Tối ưu hóa

- [x] Loại bỏ XTStyle.xaml (file theme tổng)
- [x] Giữ lại các theme files riêng lẻ
- [x] Cập nhật XTStyle.csproj
- [x] Tạo XTStyle.Demo project
- [x] Tạo sample data models
- [x] Tạo ViewModels với MVVM pattern
- [x] Demo tất cả 21 controls
- [x] Add vào solution file
- [x] Tạo README documentation

## 🎉 Kết quả

- **Library được tối ưu**: Theme files riêng lẻ dễ bảo trì
- **Demo app hoàn chỉnh**: Test được tất cả controls với sample data
- **MVVM pattern**: Code sạch, dễ hiểu
- **Ready to use**: Chỉ cần build và run!

## 📝 License

XTStyle © 2024. All rights reserved.
