using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace ImageFilters.GUI.Helpers;

public static class Extensions
{
    public static Bitmap ToGdiImage(this BitmapSource bitmapImage)
    {
        using MemoryStream outStream = new();
        var enc = new BmpBitmapEncoder();
        enc.Frames.Add(BitmapFrame.Create(bitmapImage));
        enc.Save(outStream);

        return new Bitmap(outStream);
    }

    public static BitmapImage ToBitmapImage(this Bitmap bitmap)
    {
        using MemoryStream memory = new();
        bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
        memory.Position = 0;
        BitmapImage bitmapimage = new();
        bitmapimage.BeginInit();
        bitmapimage.StreamSource = memory;
        bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
        bitmapimage.EndInit();

        return bitmapimage;
    }

    public static void Save(this BitmapImage image, string filePath)
    {
        BitmapEncoder encoder = new PngBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(image));

        using var fileStream = new System.IO.FileStream(filePath, System.IO.FileMode.Create);
        encoder.Save(fileStream);
    }

}