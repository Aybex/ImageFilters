using Wpf.Ui.Controls.Navigation;

namespace ImageFilters.GUI.Views.Pages;

/// <summary>
/// Interaction logic for SettingsPage.xaml
/// </summary>
public partial class ScriptsPage : INavigableView<ViewModels.ScriptsViewModel>
{
    public ViewModels.ScriptsViewModel ViewModel
    {
        get;
    }

    public ScriptsPage(ViewModels.ScriptsViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }
}
