using ImageFiltersGallery.WinUI.ViewModels;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ImageFiltersGallery.WinUI.Views;

public sealed partial class HomePage : Page
{
    public HomeViewModel ViewModel { get; } = new();

    public HomePage()
    {
        InitializeComponent();
        this.DataContext = ViewModel;
    }
}