// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Collections.ObjectModel;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Navigation;

namespace ImageFilters.GUI.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
	private bool _isInitialized = false;

	[ObservableProperty]
	private string _applicationTitle = "";

	[ObservableProperty]
	private ObservableCollection<object> _navigationItems = new();

	[ObservableProperty]
	private ObservableCollection<object> _navigationFooter = new();

	[ObservableProperty]
	private ObservableCollection<MenuItem> _trayMenuItems = new();

	public MainWindowViewModel(INavigationService navigationService)
	{
		if (!_isInitialized)
			InitializeViewModel();
	}

	private void InitializeViewModel()
	{
		ApplicationTitle = "Image Filters";

		NavigationItems = new ObservableCollection<object>
			{
				new NavigationViewItem()
				{
					Content = "Home",
					Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
					TargetPageType = typeof(Views.Pages.HomePage)
				}
			};

		NavigationFooter = new ObservableCollection<object>
			{
				new NavigationViewItem()
				{
					Content = "Settings",
					Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
					TargetPageType = typeof(Views.Pages.SettingsPage)
				}
			};

		TrayMenuItems = new ObservableCollection<MenuItem>
			{
				new MenuItem
				{
					Header = "Home",
					Tag = "tray_home"
				}
			};

		_isInitialized = true;
	}
}
