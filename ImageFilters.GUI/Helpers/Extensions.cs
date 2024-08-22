using System.Drawing;
using System.IO;
using System.Windows.Markup;
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

		using var fileStream = new FileStream(filePath, FileMode.Create);
		encoder.Save(fileStream);
	}
}

public class EnumBindingSourceExtension : MarkupExtension
{
	private Type? _enumType;
	public Type? EnumType
	{
		get => _enumType;
		set
		{
			if (value == _enumType) return;
			if (value is not null)
			{
				var enumType = Nullable.GetUnderlyingType(value) ?? value;
				if (!enumType.IsEnum)
					throw new ArgumentException("Type must be for an Enum.");
			}

			_enumType = value;
		}
	}

	public EnumBindingSourceExtension() { }

	public EnumBindingSourceExtension(Type? enumType)
	{
		EnumType = enumType;
	}

	public override object ProvideValue(IServiceProvider serviceProvider)
	{
		if (null == _enumType)
			throw new InvalidOperationException("The EnumType must be specified.");

		Type actualEnumType = Nullable.GetUnderlyingType(_enumType) ?? _enumType;
		Array enumValues = Enum.GetValues(actualEnumType);

		if (actualEnumType == _enumType)
			return enumValues;

		Array tempArray = Array.CreateInstance(actualEnumType, enumValues.Length + 1);
		enumValues.CopyTo(tempArray, 1);
		return tempArray;
	}
}
