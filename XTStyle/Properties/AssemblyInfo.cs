using System.Reflection;
using System.Windows;
using System.Windows.Markup;

// Cho phép XAML tìm Generic.xaml trong thư mục Themes
[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, // theme-specific resources
    ResourceDictionaryLocation.SourceAssembly // Generic.xaml location
)]

// Thông tin Assembly
[assembly: AssemblyTitle("XTStyle")]
[assembly: AssemblyDescription("Modern WPF Style Library")]
[assembly: AssemblyCompany("Your Company")]
[assembly: AssemblyProduct("XTStyle")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

// Cho phép XAML Namespace
[assembly: XmlnsPrefix("http://schemas.xtstyle.com/wpf", "xt")]
[assembly: XmlnsDefinition("http://schemas.xtstyle.com/wpf", "XTStyle")]
[assembly: XmlnsDefinition("http://schemas.xtstyle.com/wpf", "XTStyle.Controls")]