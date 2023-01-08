﻿#region (c)2008-2019 Hawkynt
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

using System.ComponentModel;
using System.Drawing.Drawing2D;
using ImageFilters.Core.Imager;

namespace ImageFilters.Core.ImageManipulators; 

[Description("GDI+ .NET public filters")]
public class Interpolator : IImageManipulator {
  private readonly InterpolationMode _type;

  #region Implementation of IImageManipulator
  public bool SupportsWidth => true;
  public bool SupportsHeight => true;
  public bool SupportsRepetitionCount => false;
  public bool SupportsGridCentering => false;
  public bool SupportsRadius => false;
  public bool ChangesResolution => true;
  public bool SupportsThresholds => false;

  public string Description {
    get {
      switch (_type) {
        case InterpolationMode.NearestNeighbor:
          return "Nearest neighbor interpolation using the Microsoft GDI+ API.";
        case InterpolationMode.Bilinear:
          return "Bilinear interpolation using the Microsoft GDI+ API. No prefiltering is done. This mode is not suitable for shrinking an image below 50 percent of its original size.";
        case InterpolationMode.Bicubic:
          return "Bicubic interpolation using the Microsoft GDI+ API. No prefiltering is done. This mode is not suitable for shrinking an image below 25 percent of its original size.";
        case InterpolationMode.HighQualityBilinear:
          return "Bilinear interpolation using the Microsoft GDI+ API. Prefiltering is performed to ensure high-quality shrinking.";
        case InterpolationMode.HighQualityBicubic:
          return "Bicubic interpolation using the Microsoft GDI+ API. Prefiltering is performed to ensure high-quality shrinking.";
        default:
          return null;
      }
    }
  }
  #endregion

  public cImage Apply(cImage source, int width, int height) => source.ApplyScaler(_type, width, height);
  public Interpolator(InterpolationMode type) => _type = type;

}