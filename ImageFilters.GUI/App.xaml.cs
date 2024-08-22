using System.IO;
using System.Windows.Threading;
using ImageFilters.GUI.Services;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wpf.Ui;

namespace ImageFilters.GUI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
	// The.NET Generic Host provides dependency injection, configuration, logging, and other services.
	// https://docs.microsoft.com/dotnet/core/extensions/generic-host
	// https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
	// https://docs.microsoft.com/dotnet/core/extensions/configuration
	// https://docs.microsoft.com/dotnet/core/extensions/logging
	private static readonly IHost _host = Host
		.CreateDefaultBuilder()
		.ConfigureAppConfiguration(c => { c.SetBasePath(Path.GetDirectoryName(AppContext.BaseDirectory)); })
		.ConfigureServices((context, services) =>
		{
			// App Host
			services.AddHostedService<ApplicationHostService>();

			// Page resolver service
			services.AddSingleton<IPageService, PageService>();

			// Theme manipulation
			services.AddSingleton<IThemeService, ThemeService>();

			// TaskBar manipulation
			services.AddSingleton<ITaskBarService, TaskBarService>();

			// Service containing navigation, same as INavigationWindow... but without window
			services.AddSingleton<INavigationService, NavigationService>();

			// Main window with navigation
			services.AddScoped<INavigationWindow, Views.MainWindow>();
			services.AddScoped<ViewModels.MainWindowViewModel>();

			// Views and ViewModels
			services.AddScoped<Views.Pages.HomePage>();
			services.AddScoped<ViewModels.HomePageViewModel>();
			services.AddScoped<Views.Pages.SettingsPage>();
			services.AddScoped<Views.Pages.ScriptsPage>();
			services.AddScoped<ViewModels.SettingsViewModel>();
			services.AddScoped<ViewModels.ScriptsViewModel>();

			// Configuration
			//services.Configure<AppConfig>(context.Configuration.GetSection(nameof(AppConfig)));
		}).Build();

	/// <summary>
	/// Gets registered service.
	/// </summary>
	/// <typeparam name="T">Type of the service to get.</typeparam>
	/// <returns>Instance of the service or <see langword="null"/>.</returns>
	public static T GetService<T>()
		where T : class
	{
		return _host.Services.GetService(typeof(T)) as T;
	}

	/// <summary>
	/// Occurs when the application is loading.
	/// </summary>
	private async void OnStartup(object sender, StartupEventArgs e)
	{
		await _host.StartAsync();
		LiveCharts.Configure(config => config.AddSkiaSharp().AddDefaultMappers().AddDarkTheme());

	}

	/// <summary>
	/// Occurs when the application is closing.
	/// </summary>
	private async void OnExit(object sender, ExitEventArgs e)
	{
		await _host.StopAsync();

		_host.Dispose();
	}

	/// <summary>
	/// Occurs when an exception is thrown by an application but not handled.
	/// </summary>
	private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
	{
		// For more info see https://docs.microsoft.com/en-us/dotnet/api/system.windows.application.dispatcherunhandledexception?view=windowsdesktop-6.0
	}
}
