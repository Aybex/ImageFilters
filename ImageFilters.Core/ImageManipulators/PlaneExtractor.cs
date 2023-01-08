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
using System.Diagnostics.Contracts;
using ImageFilters.Core.Imager;

namespace ImageFilters.Core.ImageManipulators; 

[Description("Color component extractors")]
public class PlaneExtractor : IImageManipulator {
  private readonly Func<cImage, cImage> _planeExtractionFunction;

  #region Implementation of IImageManipulator
  public bool SupportsWidth => false;
  public bool SupportsHeight => false;
  public bool SupportsRepetitionCount => false;
  public bool SupportsGridCentering => false;
  public bool ChangesResolution => false;
  public bool SupportsThresholds => false;
  public bool SupportsRadius => false;
  public string Description { get; }

  #endregion

  public cImage Apply(cImage source) => _planeExtractionFunction(source);

  public PlaneExtractor(Func<cImage, cImage> planeExtractionFunction, string description) {
    Contract.Requires(planeExtractionFunction != null);
    _planeExtractionFunction = planeExtractionFunction;
    Description = description;
  }

}