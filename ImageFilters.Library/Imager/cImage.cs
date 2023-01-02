#region (c)2008-2019 Hawkynt
/*
 *  cImage 
 *  Image filtering library 
    Copyright (C) 2008-2019 Hawkynt

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
#endregion

// TODO: seamless carving aka content-aware resizing
// TODO: smart filtering http://www.hiend3d.com/smartflt.html
// TODO: on transparent out of bounds mode, resize source image first, apply filter, resize back
using System.Collections.Concurrent;
using ImageFilters.Library.Imager.Interface;

namespace ImageFilters.Library.Imager; 

/// <summary>
/// A bitmap image
/// </summary>
public partial class cImage : ICloneable {
  #region fields
  // image data
  /// <summary>
  /// An array containing the images' pixel data
  /// </summary>
  private readonly sPixel[] _imageData;
  /// <summary>
  /// The images' width
  /// </summary>
  private readonly int _width;
  /// <summary>
  /// The images' height
  /// </summary>
  private readonly int _height;

  private OutOfBoundsMode _horizontalOutOfBoundsMode;
  private OutOfBoundsUtils.OutOfBoundsHandler _horizontalOutOfBoundsHandler;
  private OutOfBoundsMode _verticalOutOfBoundsMode;
  private OutOfBoundsUtils.OutOfBoundsHandler _verticalOutOfBoundsHandler;
  #endregion

  #region properties
  /// <summary>
  /// Gets the width of the image.
  /// </summary>
  /// <value>The width.</value>
  public int Width => _width;

  /// <summary>
  /// Gets the height of the image.
  /// </summary>
  /// <value>The height.</value>
  public int Height => _height;

  /// <summary>
  /// Gets the a new instance containing a greyscale image of the red values only.
  /// </summary>
  /// <value>The greyscale image from the red components.</value>
  public cImage Red => new(this, pixel => pixel.Red);

  /// <summary>
  /// Gets the a new instance containing a greyscale image of the green values only.
  /// </summary>
  /// <value>The greyscale image from the green components.</value>
  public cImage Green => new(this, pixel => pixel.Green);

  /// <summary>
  /// Gets the a new instance containing a greyscale image of the blue values only.
  /// </summary>
  /// <value>The greyscale image from the blue components.</value>
  public cImage Blue => new(this, pixel => pixel.Blue);

  /// <summary>
  /// Gets the a new instance containing a greyscale image of the alpha values only.
  /// </summary>
  /// <value>The greyscale image from the alpha components.</value>
  public cImage Alpha => new(this, pixel => pixel.Alpha);

  /// <summary>
  /// Gets the a new instance containing a greyscale image of the luminance values only.
  /// </summary>
  /// <value>The greyscale image from the luminance components.</value>
  public cImage Luminance => new(this, pixel => pixel.Luminance);

  /// <summary>
  /// Gets the a new instance containing a greyscale image of the color(U) values only.
  /// </summary>
  /// <value>The greyscale image from the color(U) components.</value>
  public cImage ChrominanceU => new(this, pixel => pixel.ChrominanceU);

  /// <summary>
  /// Gets the a new instance containing a greyscale image of the color(V) values only.
  /// </summary>
  /// <value>The greyscale image from the color(V) components.</value>
  public cImage ChrominanceV => new(this, pixel => pixel.ChrominanceV);

  /// <summary>
  /// Gets the a new instance containing a greyscale image of the color(u) values only.
  /// </summary>
  /// <value>The greyscale image from the color(u) components.</value>
  public cImage u => new(this, pixel => pixel.u);

  /// <summary>
  /// Gets the a new instance containing a greyscale image of the color(v) values only.
  /// </summary>
  /// <value>The greyscale image from the color(v) components.</value>
  public cImage v => new(this, pixel => pixel.v);

  /// <summary>
  /// Gets the a new instance containing a greyscale image of the brightness values only.
  /// </summary>
  /// <value>The greyscale image from the brightness components.</value>
  public cImage Brightness => new(this, pixel => pixel.Brightness);

  /// <summary>
  /// Gets the a new instance containing a greyscale image of the minimum values only.
  /// </summary>
  /// <value>The greyscale image from the minimum of all components.</value>
  public cImage Min => new(this, pixel => pixel.Minimum);

  /// <summary>
  /// Gets the a new instance containing a greyscale image of the maximum values only.
  /// </summary>
  /// <value>The greyscale image from the maximum of all components.</value>
  public cImage Max => new(this, pixel => pixel.Maximum);

  /// <summary>
  /// Extracts the colors from an image and returns a new image with only base colors.
  /// </summary>
  public cImage ExtractColors => new(this, pixel => pixel.ExtractColors);

  /// <summary>
  /// Extracts the grey deltas for use with an image that is color extracted.
  /// </summary>
  public cImage ExtractDeltas => new(this, pixel => pixel.ExtractDeltas);

  /// <summary>
  /// Gets the a new instance containing a greyscale image of the hue values only.
  /// </summary>
  /// <value>The greyscale image from the hue components.</value>
  public cImage Hue => new(this, pixel => pixel.Hue);

  /// <summary>
  /// Gets the a new instance containing an image of the hue values only.
  /// </summary>
  /// <value>The image from the hue components.</value>
  public cImage HueColored {
    get {
      return new cImage(this, pixel => {
        const float conversionFactor = 360f / 256f;
        var hue = pixel.Hue * conversionFactor;
        const float saturation = 1f;
        const float value = 1f;
        float red, green, blue;

        if (hue < 0.001) {
          red = green = blue = 0.5f;
        } else {
          hue = hue / 60f;
          var i = (byte)Math.Floor(hue);
          var f = hue - i;
          const float p = value * (1 - saturation);
          var q = value * (1 - saturation * f);
          var t = value * (1 - saturation * (1 - f));
          switch (i) {
            case 0:
            {
              red = value;
              green = t;
              blue = p;
              break;
            }
            case 1:
            {
              red = q;
              green = value;
              blue = p;
              break;
            }
            case 2:
            {
              red = p;
              green = value;
              blue = t;
              break;
            }
            case 3:
            {
              red = p;
              green = q;
              blue = value;
              break;
            }
            case 4:
            {
              red = t;
              green = p;
              blue = value;
              break;
            }
            case 5:
            {
              red = value;
              green = p;
              blue = q;
              break;
            }
            default:
            {
              throw new NotSupportedException();
            }
          }
        }
        return new sPixel(red, green, blue, pixel.Alpha / 255.0);
      });
    }
  }

  /// <summary>
  /// Gets or sets the horizontal out of bounds mode.
  /// </summary>
  /// <value>
  /// The horizontal out of bounds mode.
  /// </value>
  public OutOfBoundsMode HorizontalOutOfBoundsMode {
    get {
      return _horizontalOutOfBoundsMode;
    }
    set {
      _horizontalOutOfBoundsMode = value;
      _horizontalOutOfBoundsHandler = OutOfBoundsUtils.GetHandlerOrCrash(value);
    }
  }

  /// <summary>
  /// Gets or sets the vertical out of bounds mode.
  /// </summary>
  /// <value>
  /// The vertical out of bounds mode.
  /// </value>
  public OutOfBoundsMode VerticalOutOfBoundsMode {
    get {
      return _verticalOutOfBoundsMode;
    }
    set {
      _verticalOutOfBoundsMode = value;
      _verticalOutOfBoundsHandler = OutOfBoundsUtils.GetHandlerOrCrash(value);
    }
  }
  #endregion
  #region ctor dtor idx
  /// <summary>
  /// Initializes a new instance of the <see cref="cImage"/> class.
  /// </summary>
  /// <param name="width">Width of the image.</param>
  /// <param name="height">Height of the image.</param>
  public cImage(int width, int height) {
    _width = width;
    _height = height;
    _imageData = new sPixel[width * height];
    HorizontalOutOfBoundsMode = OutOfBoundsMode.ConstantExtension;
    VerticalOutOfBoundsMode = OutOfBoundsMode.ConstantExtension;
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="cImage"/> class from a given one.
  /// </summary>
  /// <param name="sourceImage">The source image.</param>
  public cImage(cImage sourceImage)
    : this(sourceImage?._width ?? 0, sourceImage?._height ?? 0) {
    if (sourceImage == null)
      return;

    for (long index = 0; index < sourceImage._imageData.LongLength; index++)
      _imageData[index] = sourceImage._imageData[index];
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="cImage"/> class by filtering a given one.
  /// </summary>
  /// <param name="sourceImage">The source image.</param>
  /// <param name="filterFunction">The filter.</param>
  public cImage(cImage sourceImage, Func<sPixel, sPixel> filterFunction)
    : this(sourceImage?._width ?? 0, sourceImage?._height ?? 0) {
    if (sourceImage == null)
      return;

    var width = sourceImage._width;
    Parallel.ForEach(Partitioner.Create(0, _height), () => 0, (range, _, threadStorage) => {
      for (var y = range.Item2; y > range.Item1;) {
        --y;
        for (var x = width; x > 0;) {
          --x;
          this[x, y] = filterFunction(sourceImage[x, y]);
        }
      }
      return threadStorage;
    }, _ => {
    });
  }

  /// <summary>
  /// Initializes a new greyscale instance of the <see cref="cImage"/> class by filtering a given one.
  /// </summary>
  /// <param name="sourceImage">The source image.</param>
  /// <param name="colorFilter">The greyscale filter.</param>
  public cImage(cImage sourceImage, Func<sPixel, byte> colorFilter)
    : this(sourceImage == null ? 0 : sourceImage._width, sourceImage == null ? 0 : sourceImage._height) {
    if (sourceImage == null)
      return;

    var width = sourceImage._width;
    Parallel.ForEach(Partitioner.Create(0, _height), () => 0, (range, _, threadStorage) => {
      for (var y = range.Item2; y > range.Item1;) {
        --y;
        for (var x = width; x > 0;) {
          --x;
          this[x, y] = sPixel.FromGrey(colorFilter(sourceImage[x, y]));
        }
      }
      return threadStorage;
    }, _ => {
    });
  }

  /// <summary>
  /// Gets or sets the <see cref="sPixel"/> with the specified X, Y coordinates.
  /// </summary>
  /// <value>The pixel</value>
  public sPixel this[int x, int y] {
    get { return GetPixel(x, y); }
    set { SetPixel(x, y, value); }
  }

#if NET45
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
  internal void SetPixel(int x, int y, sPixel value) {
    var width = _width;
    var height = _height;

    if (x < width && y < height && x >= 0 && y >= 0)
      _imageData[y * width + x] = value;
  }

#if NET45
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
  internal sPixel GetPixel(int x, int y) {
    var width = _width;
    var height = _height;

    if (x < 0 || x >= width)
      x = _horizontalOutOfBoundsHandler(x, width, x < 0);

    if (y < 0 || y >= height)
      y = _verticalOutOfBoundsHandler(y, height, y < 0);

    return _imageData[y * width + x];
  }

#if NET45
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
  internal sPixel[] GetImageData() => _imageData;

  #endregion

  /// <summary>
  /// Fills the image with the specified color.
  /// </summary>
  /// <param name="red">The red-value.</param>
  /// <param name="green">The green-value.</param>
  /// <param name="blue">The blue-value.</param>
  /// <param name="alpha">The alpha-value.</param>
  public void Fill(byte red, byte green, byte blue, byte alpha = 255) => Fill(new sPixel(red, green, blue, alpha));

  /// <summary>
  /// Fills the image with the specified pixel.
  /// </summary>
  /// <param name="pixel">The pixel instance.</param>
  public void Fill(sPixel pixel) => Parallel.For(0, _imageData.LongLength, offset => _imageData[offset] = pixel);

  #region ICloneable Members
  /// <summary>
  /// Creates a new object that is a copy of the current instance.
  /// </summary>
  /// <returns>
  /// A new object that is a copy of this instance.
  /// </returns>
  public object Clone() => new cImage(this);

  #endregion
}