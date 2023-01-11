using ImageFilters.GUI.ViewModels;
using System.Windows.Media.Imaging;
using System.Windows;
using ImageFilters.GUI.Helpers;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls.Window;

namespace ImageFilters.GUI.Views;

public partial class MainWindow
{

    private MainWindowViewModel ViewModel { get; }
    public MainWindow()
    {
        DataContext = ViewModel= new MainWindowViewModel();
        
        Watcher.Watch(this,WindowBackdropType.Acrylic);

        InitializeComponent();
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
        //Open file dialog
        Microsoft.Win32.SaveFileDialog dialog = new()
        {
            Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*"
        };

        if (dialog.ShowDialog() != true) return;
        ViewModel.TargetImage.Save(dialog.FileName);
    }
}