using ImageFilters.GUI.ViewModels;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Controls;

namespace ImageFilters.GUI.Views.Pages;

public partial class HomePage : Page
{
	private HomePageViewModel ViewModel { get; }

	public HomePage()
	{
		InitializeComponent();
		DataContext = ViewModel = new HomePageViewModel();
	}

	private void LoadImage_Click(object sender, RoutedEventArgs e)
	{
		//Open file dialog
		Microsoft.Win32.OpenFileDialog dialog = new()
		{
			Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*"
		};

		if (dialog.ShowDialog() != true) return;
		var image = new BitmapImage(new Uri(dialog.FileName));
		ViewModel.SourceImage = image;
	}

	private void SaveImage_Click(object sender, RoutedEventArgs e)
	{
		if (ViewModel.TargetImage is null) return;
		//Open file dialog
		Microsoft.Win32.SaveFileDialog dialog = new()
		{
			Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*"
		};

		if (dialog.ShowDialog() != true) return;
		ViewModel.TargetImage.Save(dialog.FileName);

	}

}