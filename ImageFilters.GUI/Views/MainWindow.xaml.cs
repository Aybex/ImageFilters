using Wpf.Ui.Controls.Navigation;

namespace ImageFilters.GUI.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : INavigationWindow
{
	public ViewModels.MainWindowViewModel ViewModel
	{
		get;
	}

	public MainWindow(ViewModels.MainWindowViewModel viewModel, IPageService pageService, INavigationService navigationService)
	{
		ViewModel = viewModel;
		DataContext = this;

		Wpf.Ui.Appearance.Watcher.Watch(this);

		InitializeComponent();
		SetPageService(pageService);

		navigationService.SetNavigationControl(RootNavigation);
	}

	#region INavigationWindow methods

	public INavigationView GetNavigation()
		=> RootNavigation;

	public bool Navigate(Type pageType)
		=> RootNavigation.Navigate(pageType);

	public void SetPageService(IPageService pageService)
		=> RootNavigation.SetPageService(pageService);

	public void ShowWindow()
		=> Show();

	public void CloseWindow()
		=> Close();

	#endregion INavigationWindow methods

	/// <summary>
	/// Raises the closed event.
	/// </summary>
	protected override void OnClosed(EventArgs e)
	{
		base.OnClosed(e);

		// Make sure that closing this window will begin the process of closing the application.
		Application.Current.Shutdown();
	}

	INavigationView INavigationWindow.GetNavigation()
	{
		throw new NotImplementedException();
	}

	public void SetServiceProvider(IServiceProvider serviceProvider)
	{
		throw new NotImplementedException();
	}
}