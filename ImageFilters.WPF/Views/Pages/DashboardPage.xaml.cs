using System.Windows;
using System.Windows.Media.Imaging;
using Wpf.Ui.Common.Interfaces;

namespace ImageFilters.WPF.Views.Pages
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class DashboardPage : INavigableView<ViewModels.DashboardViewModel>
    {

        public DashboardPage(ViewModels.DashboardViewModel viewModel)
        {
            ViewModel = viewModel;
            
            InitializeComponent();
        }

        public ViewModels.DashboardViewModel ViewModel
        {
            get;
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

    }
}
            