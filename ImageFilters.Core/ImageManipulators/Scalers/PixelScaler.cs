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


using ImageFilters.Core.Imager;
using ImageFilters.Core.Imager.Interface;

namespace ImageFilters.Core.ImageManipulators.Scalers; 

public class PixelScaler : AScaler {
  private readonly PixelScalerType _type;

  #region Implementation of AScaler
  public override cImage Apply(cImage source) => source.ApplyScaler(_type);
  public override byte ScaleFactorX { get; }
  public override byte ScaleFactorY { get; }
  public override string Description => ReflectionUtils.GetDescriptionForEnumValue(_type);

  #endregion

  public PixelScaler(PixelScalerType type) {
    var info = cImage.GetPixelScalerInfo(type);
    _type = type;
    ScaleFactorX = info.Item1;
    ScaleFactorY = info.Item2;
  }
}