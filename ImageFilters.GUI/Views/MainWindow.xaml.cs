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
    
}