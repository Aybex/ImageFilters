using System.Drawing;
using System.IO;
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
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (dialog.ShowDialog() == true)
            {
                //Load image
                var image = new BitmapImage(new Uri(dialog.FileName));

                ViewModel.SrcImage = BitmapSourceToBitmap(image);
                //Show image
                sourceImage.Source = image;
            }

        }



        private static Bitmap BitmapSourceToBitmap(BitmapSource bitmapImage)
        {

            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }
    }
}
            