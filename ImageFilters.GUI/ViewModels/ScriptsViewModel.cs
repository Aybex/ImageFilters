using Wpf.Ui.Controls.Navigation;

namespace ImageFilters.GUI.ViewModels;

public partial class ScriptsViewModel : ObservableObject, INavigationAware
{
    private bool _isInitialized = false;

    
    public void OnNavigatedTo()
    {
        if (!_isInitialized)
            InitializeViewModel();
    }

    public void OnNavigatedFrom()
    {
    }

    private void InitializeViewModel()
    {
	    _isInitialized = true;
    }

}
