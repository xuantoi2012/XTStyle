# XTStyle Custom Controls Guide

## 📚 Overview

XTStyle now includes 20+ modern WPF custom controls to replace DevExpress components. All controls follow MVVM pattern, support Light/Dark themes, and use smooth animations.

## 🎨 Theme Support

### Using ThemeManager

```csharp
// In App.xaml.cs or MainWindow
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

## 🧩 Available Controls

### 1. SearchBox
Search input with icon and clear button.

```xaml
<local:SearchBox Text="{Binding SearchText}" 
                Placeholder="Search items..."
                Width="300"/>
```

**Properties:**
- `Text` - Search text (bindable)
- `Placeholder` - Placeholder text
- `HasText` - Read-only, true if has text
- `ClearCommand` - Clear text command

---

### 2. NumericUpDown
Numeric input with up/down buttons.

```xaml
<local:NumericUpDown Value="{Binding Quantity}"
                    Minimum="0"
                    Maximum="100"
                    Increment="1"
                    DecimalPlaces="0"/>
```

**Properties:**
- `Value` - Current value (bindable)
- `Minimum` - Minimum value
- `Maximum` - Maximum value
- `Increment` - Step value
- `DecimalPlaces` - Number of decimal places

---

### 3. ToggleSwitch
iOS-style toggle switch.

```xaml
<local:ToggleSwitch IsOn="{Binding IsEnabled}"
                   OnText="ON"
                   OffText="OFF"/>
```

**Properties:**
- `IsOn` - Switch state (bindable)
- `OnText` - Text when on
- `OffText` - Text when off

**Events:**
- `Toggled` - Fired when switch state changes

---

### 4. IconButton
Button with icon support.

```xaml
<local:IconButton Icon="📁" 
                 Text="Open File"
                 Command="{Binding OpenCommand}"/>
```

**Properties:**
- `Icon` - Icon content (text or object)
- `Text` - Button text
- `IconPosition` - Icon position (Left/Right/Top/Bottom)
- `IconSpacing` - Space between icon and text

---

### 5. LoadingSpinner
Animated loading spinner.

```xaml
<local:LoadingSpinner IsSpinning="{Binding IsLoading}"
                     Diameter="40"
                     SpinnerColor="{StaticResource PrimaryBrush}"/>
```

**Properties:**
- `IsSpinning` - Whether spinner is active
- `Diameter` - Spinner size
- `SpinnerColor` - Spinner color
- `Thickness` - Spinner line thickness

---

### 6. Pagination
Complete pagination control with page numbers.

```xaml
<local:Pagination TotalItems="{Binding TotalItems}"
                 CurrentPage="{Binding CurrentPage}"
                 PageSize="10"/>
```

**Properties:**
- `TotalItems` - Total number of items
- `CurrentPage` - Current page (bindable)
- `PageSize` - Items per page
- `TotalPages` - Read-only, calculated total pages

**Events:**
- `PageChanged` - Fired when page changes

---

### 7. ToastNotification
Toast notification system with auto-dismiss.

```csharp
// Initialize once in App.xaml.cs or MainWindow
ToastNotification.Initialize(toastContainer); // ItemsControl in XAML

// Show toasts
ToastNotification.Success("Operation completed!");
ToastNotification.Error("An error occurred", 5000);
ToastNotification.Warning("Please review changes");
ToastNotification.Info("New update available");
```

**XAML Setup:**
```xaml
<ItemsControl x:Name="toastContainer" 
             VerticalAlignment="Top" 
             HorizontalAlignment="Right"
             Margin="20"/>
```

---

### 8. Card
Card component with header and footer.

```xaml
<local:Card IsHoverable="True">
    <local:Card.Header>
        <TextBlock Text="Card Title" FontSize="16" FontWeight="Bold"/>
    </local:Card.Header>
    
    <TextBlock Text="Card content goes here"/>
    
    <local:Card.Footer>
        <Button Content="Action" />
    </local:Card.Footer>
</local:Card>
```

**Properties:**
- `Header` - Header content
- `Footer` - Footer content
- `IsHoverable` - Enable hover lift effect
- `Elevation` - Shadow depth

---

### 9. Badge
Badge component with different types.

```xaml
<local:Badge Text="NEW" Type="Primary"/>
<local:Badge Text="10" Type="Success"/>
<local:Badge Text="Error" Type="Danger"/>
<local:Badge IsDot="True" Type="Warning"/>
```

**Properties:**
- `Text` - Badge text
- `Type` - Badge type (Default/Primary/Success/Warning/Danger/Info)
- `IsDot` - Show as small dot

---

### 10. ModalDialog
Modal dialog/popup.

```xaml
<local:ModalDialog IsOpen="{Binding ShowModal}"
                  Title="Confirm Action"
                  CloseOnOverlayClick="True">
    <StackPanel>
        <TextBlock Text="Are you sure?"/>
        <Button Content="OK" Command="{Binding ConfirmCommand}"/>
    </StackPanel>
</local:ModalDialog>
```

**Properties:**
- `IsOpen` - Dialog visibility (bindable)
- `Title` - Dialog title
- `CloseOnOverlayClick` - Close when clicking outside

---

### 11. FilterableDataGrid
DataGrid with built-in filtering.

```xaml
<local:FilterableDataGrid ItemsSource="{Binding Items}"
                         EnableFiltering="True">
    <!-- Define columns -->
</local:FilterableDataGrid>
```

**Methods:**
- `ApplyFilter(columnName, filterText)` - Apply filter to column
- `ClearFilters()` - Clear all filters

---

### 12. Breadcrumb
Breadcrumb navigation.

```xaml
<local:Breadcrumb Separator="/">
    <!-- Add items in code-behind or binding -->
</local:Breadcrumb>
```

```csharp
breadcrumb.Items.Add(new BreadcrumbItem { Text = "Home", Data = homeData });
breadcrumb.Items.Add(new BreadcrumbItem { Text = "Products", Data = productsData });
```

**Events:**
- `ItemClicked` - Fired when breadcrumb item is clicked

---

### 13. EmptyState
Empty state component.

```xaml
<local:EmptyState Icon="📭"
                 Title="No Data"
                 Message="There are no items to display.">
    <local:EmptyState.ActionButton>
        <Button Content="Add Item" Command="{Binding AddCommand}"/>
    </local:EmptyState.ActionButton>
</local:EmptyState>
```

**Properties:**
- `Icon` - Icon content
- `Title` - Title text
- `Message` - Message text
- `ActionButton` - Optional action button

---

### 14. StatsCard
Dashboard statistics widget.

```xaml
<local:StatsCard Title="Total Sales"
                Value="$12,450"
                Icon="💰"
                IconBackground="{StaticResource SecondaryBrush}"
                ChangePercent="+12.5%"
                IsPositiveChange="True"/>
```

**Properties:**
- `Title` - Card title
- `Value` - Stat value
- `Icon` - Icon content
- `IconBackground` - Icon background color
- `ChangePercent` - Change percentage
- `IsPositiveChange` - Show as positive/negative change

---

### 15. RippleButton
Button with Material Design ripple effect.

```xaml
<local:RippleButton Content="Click Me"
                   RippleColor="#50FFFFFF"
                   Command="{Binding ClickCommand}"/>
```

**Properties:**
- `RippleColor` - Ripple effect color

---

### 16. DateRangePicker
Date range picker control.

```xaml
<local:DateRangePicker StartDate="{Binding StartDate}"
                      EndDate="{Binding EndDate}"
                      DateFormat="MM/dd/yyyy"/>
```

**Properties:**
- `StartDate` - Start date (bindable)
- `EndDate` - End date (bindable)
- `DateFormat` - Date format string
- `Placeholder` - Placeholder text
- `DisplayText` - Read-only formatted display text

---

### 17. FileUpload
File upload control with UI.

```xaml
<local:FileUpload FileName="{Binding FileName}"
                 FilePath="{Binding FilePath}"
                 Filter="Text Files (*.txt)|*.txt|All Files (*.*)|*.*"/>
```

**Properties:**
- `FileName` - Selected file name (bindable)
- `FilePath` - Full file path (bindable)
- `Filter` - File filter string
- `Placeholder` - Placeholder text
- `HasFile` - Read-only, true if file selected

---

### 18. ThemeSwitcher
Light/Dark mode switcher.

```xaml
<local:ThemeSwitcher IsDarkMode="{Binding IsDarkMode}"/>
```

**Properties:**
- `IsDarkMode` - Dark mode state (bindable)

---

### 19. ProgressTracker
Step-by-step progress tracker (stepper).

```xaml
<local:ProgressTracker CurrentStep="{Binding CurrentStep}"
                      Orientation="Horizontal">
    <!-- Add steps in code-behind or binding -->
</local:ProgressTracker>
```

```csharp
tracker.Steps.Add(new ProgressStep { Title = "Step 1", Description = "First step" });
tracker.Steps.Add(new ProgressStep { Title = "Step 2", Description = "Second step" });
```

**Properties:**
- `CurrentStep` - Current step index (0-based)
- `Orientation` - Horizontal/Vertical

---

### 20. Accordion
Collapsible panels container.

```xaml
<local:Accordion SingleExpand="True">
    <local:AccordionItem Header="Section 1">
        <TextBlock Text="Content for section 1"/>
    </local:AccordionItem>
    <local:AccordionItem Header="Section 2" IsExpanded="True">
        <TextBlock Text="Content for section 2"/>
    </local:AccordionItem>
</local:Accordion>
```

**Properties:**
- `SingleExpand` - Only one item can be expanded at a time
- `IsExpanded` - Item expansion state

**Events:**
- `Expanded` - Fired when item expands
- `Collapsed` - Fired when item collapses

---

### 21. ClosableTabControl
Tab control with close buttons.

```xaml
<local:ClosableTabControl CanCloseTabs="True">
    <local:ClosableTabItem Header="Tab 1" CanClose="True">
        <TextBlock Text="Tab 1 content"/>
    </local:ClosableTabItem>
    <local:ClosableTabItem Header="Tab 2" CanClose="False">
        <TextBlock Text="Tab 2 content"/>
    </local:ClosableTabItem>
</local:ClosableTabControl>
```

**Properties:**
- `CanCloseTabs` - Global setting for tab closing
- `CanClose` - Per-tab close button visibility

**Events:**
- `Closing` - Fired before tab closes

---

## 🎨 Using Converters

All converters are available in the `XTStyle.Converters` namespace:

```xaml
xmlns:converters="clr-namespace:XTStyle.Converters"

<!-- In Resources -->
<converters:BoolToVisibilityConverter x:Key="BoolToVisibilityConverter"/>

<!-- In Bindings -->
<TextBlock Visibility="{Binding IsVisible, Converter={StaticResource BoolToVisibilityConverter}}"/>
```

**Available Converters:**
- `NullToVisibilityConverter` - Null = Collapsed, Not Null = Visible
- `BoolToVisibilityConverter` - True = Visible, False = Collapsed
- `InverseBoolToVisibilityConverter` - True = Collapsed, False = Visible
- `NumberFormatConverter` - Formats numbers with format string
- `StringEmptyToVisibilityConverter` - Empty = Collapsed, Not Empty = Visible

---

## 🎨 Color Resources

All controls use the existing XTStyle color resources:

- `PrimaryBrush` - #FF0063A3
- `PrimaryHoverBrush` - #FF009AD9
- `SecondaryBrush` - #10B981
- `DangerBrush` - #EF4444
- `CardBrush` - White (Light) / #1F2937 (Dark)
- `BorderBrush` - #E5E7EB (Light) / #374151 (Dark)
- `TextPrimaryBrush` - #111827 (Light) / #F9FAFB (Dark)
- `TextSecondaryBrush` - #6B7280 (Light) / #9CA3AF (Dark)
- `BackgroundBrush` - #F9FAFB (Light) / #111827 (Dark)
- `AccentBrush` - #FF7D29
- `AccentHoverBrush` - #FFA62F

---

## 📝 Complete Example

```xaml
<Window xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:local="clr-namespace:XTStyle.Controls">
    
    <Window.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="/XTStyle;component/Themes/Generic.xaml"/>
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Window.Resources>
    
    <Grid Margin="20">
        <StackPanel>
            <!-- Theme Switcher -->
            <local:ThemeSwitcher HorizontalAlignment="Right" Margin="0,0,0,20"/>
            
            <!-- Search Box -->
            <local:SearchBox Text="{Binding SearchText}" 
                           Placeholder="Search products..."
                           Margin="0,0,0,20"/>
            
            <!-- Stats Cards -->
            <UniformGrid Columns="3" Margin="0,0,0,20">
                <local:StatsCard Title="Total Sales" Value="$12,450" 
                               Icon="💰" IconBackground="{StaticResource SecondaryBrush}"
                               ChangePercent="+12.5%" IsPositiveChange="True"
                               Margin="0,0,10,0"/>
                <local:StatsCard Title="Orders" Value="324" 
                               Icon="📦" IconBackground="{StaticResource PrimaryBrush}"
                               ChangePercent="+5.2%" IsPositiveChange="True"
                               Margin="5,0"/>
                <local:StatsCard Title="Customers" Value="1,250" 
                               Icon="👥" IconBackground="{StaticResource AccentBrush}"
                               ChangePercent="-2.1%" IsPositiveChange="False"
                               Margin="10,0,0,0"/>
            </UniformGrid>
            
            <!-- Data Grid with Pagination -->
            <local:Card Header="Products" Margin="0,0,0,20">
                <StackPanel>
                    <local:FilterableDataGrid ItemsSource="{Binding Products}" 
                                            Height="400"/>
                    <local:Pagination TotalItems="{Binding TotalProducts}"
                                    CurrentPage="{Binding CurrentPage}"
                                    PageSize="10"
                                    Margin="0,10,0,0"/>
                </StackPanel>
            </local:Card>
            
            <!-- Loading Indicator -->
            <local:LoadingSpinner IsSpinning="{Binding IsLoading}"
                                HorizontalAlignment="Center"/>
        </StackPanel>
    </Grid>
</Window>
```

---

## 🚀 Getting Started

1. **Add reference** to XTStyle.dll in your WPF project
2. **Merge resources** in App.xaml or Window resources:
   ```xaml
   <ResourceDictionary Source="/XTStyle;component/Themes/Generic.xaml"/>
   ```
3. **Add namespace** to your XAML:
   ```xaml
   xmlns:local="clr-namespace:XTStyle.Controls;assembly=XTStyle"
   ```
4. **Use controls** in your XAML!

---

## 💡 Tips

- All controls follow MVVM pattern - use data binding for best results
- Controls automatically adapt to Light/Dark themes
- Use `ThemeManager` to switch themes programmatically
- All animations are smooth with 0.2s duration
- Controls use consistent spacing (8px, 12px, 16px, 20px)
- Shadows are subtle (0.05-0.15 opacity)
- Corner radius is consistent (4px small, 6px buttons, 8px cards, 12px+ search)

---

## 🔧 Troubleshooting

**Controls not showing?**
- Ensure Generic.xaml is merged in your resources
- Check that build action for XAML files is set to "Page"

**Theme not changing?**
- Verify ThemeManager.Instance.SetTheme() is called
- Check that color resources are defined in Colors.xaml

**Binding not working?**
- Ensure DataContext is set correctly
- Use TwoWay binding for editable properties
- Check property change notifications in ViewModel

---

## 📄 License

XTStyle © 2024. All rights reserved.
