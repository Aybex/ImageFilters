using CommunityToolkit.Mvvm.ComponentModel;

namespace ImageFiltersGallery.WinUI.ViewModels;

public partial class HomeViewModel: ObservableObject
{
    [ObservableProperty] private string name = "Home Page Name";
    [ObservableProperty] private bool buttonEnabled = true;

}