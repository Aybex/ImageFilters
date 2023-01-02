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

using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using ImageFilters.Library.Imager.Interface;

namespace ImageFilters.Library.Imager; 

/// <summary>
/// This class gets us fast access to a small window of pixels in the source and target images.
/// </summary>
public interface IPixelWorker<TColorStorage> {
  int SourceX();
  int SourceY();
  int SourceHeight();

  void IncrementX(int targetXIncrementor);

  #region access source points
  TColorStorage SourceM2M2();
  TColorStorage SourceM1M2();
  TColorStorage SourceP0M2();
  TColorStorage SourceP1M2();
  TColorStorage SourceP2M2();
  TColorStorage SourceM2M1();
  TColorStorage SourceM1M1();
  TColorStorage SourceP0M1();
  TColorStorage SourceP1M1();
  TColorStorage SourceP2M1();
  TColorStorage SourceM2P0();
  TColorStorage SourceM1P0();
  TColorStorage SourceP0P0();
  TColorStorage SourceP1P0();
  TColorStorage SourceP2P0();
  TColorStorage SourceM2P1();
  TColorStorage SourceM1P1();
  TColorStorage SourceP0P1();
  TColorStorage SourceP1P1();
  TColorStorage SourceP2P1();
  TColorStorage SourceM2P2();
  TColorStorage SourceM1P2();
  TColorStorage SourceP0P2();
  TColorStorage SourceP1P2();
  TColorStorage SourceP2P2();
  #endregion

  #region access target points
  void TargetP0M1(TColorStorage value);
  void TargetP1M1(TColorStorage value);
  void TargetP2M1(TColorStorage value);
  void TargetP3M1(TColorStorage value);
  void TargetP4M1(TColorStorage value);
  void TargetP0P0(TColorStorage value);
  void TargetP1P0(TColorStorage value);
  void TargetP2P0(TColorStorage value);
  void TargetP3P0(TColorStorage value);
  void TargetP4P0(TColorStorage value);
  void TargetP0P1(TColorStorage value);
  void TargetP1P1(TColorStorage value);
  void TargetP2P1(TColorStorage value);
  void TargetP3P1(TColorStorage value);
  void TargetP4P1(TColorStorage value);
  void TargetP0P2(TColorStorage value);
  void TargetP1P2(TColorStorage value);
  void TargetP2P2(TColorStorage value);
  void TargetP3P2(TColorStorage value);
  void TargetP4P2(TColorStorage value);
  void TargetP0P3(TColorStorage value);
  void TargetP1P3(TColorStorage value);
  void TargetP2P3(TColorStorage value);
  void TargetP3P3(TColorStorage value);
  void TargetP4P3(TColorStorage value);
  void TargetP0P4(TColorStorage value);
  void TargetP1P4(TColorStorage value);
  void TargetP2P4(TColorStorage value);
  void TargetP3P4(TColorStorage value);
  void TargetP4P4(TColorStorage value);
  #endregion

}

/// <summary>
/// This class gets us fast access to a small window of pixels in the source and target images.
/// </summary>
internal class PixelWorker<TColorStorage>:IPixelWorker<TColorStorage> {
  private readonly Func<int, TColorStorage> _sourceImageGetter;
  private int _sourceX;
  private readonly int _sourceY;
  private int _sourceOffset;
    
  private readonly int _sourceWidth;
  private readonly int _sourceStride;
  private readonly int _sourceHeight;
  private readonly OutOfBoundsUtils.OutOfBoundsHandler _sourceXWrapper;
  private readonly OutOfBoundsUtils.OutOfBoundsHandler _sourceYWrapper;

  private readonly Action<int,TColorStorage> _targetImageSetter;
  private int _targetOffset;

  #region offset calculators for source image
  // these are lazy calculated offsets, so once used, the method pointer gets replaced by a function that returns the calculated constant
  private Func<int> _sourceOffsetM2X;
  private int _sourceOffsetM2XValue;
  private Func<int> _sourceOffsetM1X;
  private int _sourceOffsetM1XValue;
  private Func<int> _sourceOffsetP1X;
  private int _sourceOffsetP1XValue;
  private Func<int> _sourceOffsetP2X;
  private int _sourceOffsetP2XValue;
  private Func<int> _sourceOffsetM2Y;
  private int _sourceOffsetM2YValue;
  private Func<int> _sourceOffsetM1Y;
  private int _sourceOffsetM1YValue;
  private Func<int> _sourceOffsetP1Y;
  private int _sourceOffsetP1YValue;
  private Func<int> _sourceOffsetP2Y;
  private int _sourceOffsetP2YValue;
  #endregion

  #region offsets for target image
  private readonly int _targetOffsetM1Y;
  private readonly int _targetOffsetP1Y;
  private readonly int _targetOffsetP2Y;
  private readonly int _targetOffsetP3Y;
  private readonly int _targetOffsetP4Y;
  #endregion

  public PixelWorker(Func<int, TColorStorage> sourceImageGetter, int sourceX, int sourceY, int sourceWidth, int sourceHeight, int sourceStride, OutOfBoundsUtils.OutOfBoundsHandler sourceXWrapper, OutOfBoundsUtils.OutOfBoundsHandler sourceYWrapper, Action<int, TColorStorage> targetImageSetter, int targetX, int targetY, int targetStride) {
    Contract.Requires(sourceX >= 0 && sourceX < sourceWidth && sourceY >= 0 && sourceY < sourceHeight);
    _sourceImageGetter = sourceImageGetter;
    _sourceX = sourceX;
    _sourceY = sourceY;
    _sourceWidth = sourceWidth;
    _sourceStride = sourceStride;
    _sourceHeight = sourceHeight;

    // we can safely calculate this offset, because we assume that the central source pixel is never out of bounds
    _sourceOffset = sourceWidth * sourceY + sourceX;

    // we only check pixels in a row or column once for over-/underflow, to avoid calling the wrappers over and over again
    _sourceXWrapper = sourceXWrapper;
    _sourceYWrapper = sourceYWrapper;

    // we calculate a delta offset for pixels around the center and store these independent X from Y
    _sourceOffsetM2X = _CalculateOffsetM2X;
    _sourceOffsetM1X = _CalculateOffsetM1X;
    _sourceOffsetP1X = _CalculateOffsetP1X;
    _sourceOffsetP2X = _CalculateOffsetP2X;
    _sourceOffsetM2Y = _CalculateOffsetM2Y;
    _sourceOffsetM1Y = _CalculateOffsetM1Y;
    _sourceOffsetP1Y = _CalculateOffsetP1Y;
    _sourceOffsetP2Y = _CalculateOffsetP2Y;

    _targetImageSetter = targetImageSetter;
    _targetOffset = targetStride * targetY + targetX;

    // pre-calculating the row offset for target image, because they surely get used
    _targetOffsetM1Y = targetStride *  -1;          // for nx0 filters
    _targetOffsetP1Y = targetStride     ;          // for nx2 filters
    _targetOffsetP2Y = targetStride << 1;          // for nx3 filters
    _targetOffsetP3Y = targetStride *  3;          // for nx4 filters
    _targetOffsetP4Y = targetStride << 2;          // for nx5 filters
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public int SourceX() => _sourceX;
    
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public int SourceY() => _sourceY;
    
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public int SourceHeight() => _sourceHeight;

  public void IncrementX(int targetXIncrementor){
    _targetOffset+=targetXIncrementor;
    ++_sourceOffset;
    ++_sourceX;
    _sourceOffsetM2X = _CalculateOffsetM2X;
    _sourceOffsetM1X = _GetOffsetM1X;
    _sourceOffsetM1XValue = -1;
    _sourceOffsetP1X = _CalculateOffsetP1X;
    _sourceOffsetP2X = _CalculateOffsetP2X;
  }

  #region access source points
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public TColorStorage SourceM2M2()
    => _sourceImageGetter(_sourceOffset + _sourceOffsetM2X() + _sourceOffsetM2Y())
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public TColorStorage SourceM1M2()
    => _sourceImageGetter(_sourceOffset + _sourceOffsetM1X() + _sourceOffsetM2Y())
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public TColorStorage SourceP0M2()
    => _sourceImageGetter(_sourceOffset + _sourceOffsetM2Y())
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public TColorStorage SourceP1M2()
    => _sourceImageGetter(_sourceOffset + _sourceOffsetP1X() + _sourceOffsetM2Y())
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public TColorStorage SourceP2M2()
    => _sourceImageGetter(_sourceOffset + _sourceOffsetP2X() + _sourceOffsetM2Y())
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public TColorStorage SourceM2M1()
    => _sourceImageGetter(_sourceOffset + _sourceOffsetM2X() + _sourceOffsetM1Y())
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public TColorStorage SourceM1M1()
    => _sourceImageGetter(_sourceOffset + _sourceOffsetM1X() + _sourceOffsetM1Y())
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public TColorStorage SourceP0M1()
    => _sourceImageGetter(_sourceOffset + _sourceOffsetM1Y())
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public TColorStorage SourceP1M1()
    => _sourceImageGetter(_sourceOffset + _sourceOffsetP1X() + _sourceOffsetM1Y())
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public TColorStorage SourceP2M1()
    => _sourceImageGetter(_sourceOffset + _sourceOffsetP2X() + _sourceOffsetM1Y())
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public TColorStorage SourceM2P0()
    => _sourceImageGetter(_sourceOffset + _sourceOffsetM2X())
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public TColorStorage SourceM1P0()
    => _sourceImageGetter(_sourceOffset + _sourceOffsetM1X())
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public TColorStorage SourceP0P0()
    => _sourceImageGetter(_sourceOffset)
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public TColorStorage SourceP1P0()
    => _sourceImageGetter(_sourceOffset + _sourceOffsetP1X())
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public TColorStorage SourceP2P0()
    => _sourceImageGetter(_sourceOffset + _sourceOffsetP2X())
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public TColorStorage SourceM2P1()
    => _sourceImageGetter(_sourceOffset + _sourceOffsetM2X() + _sourceOffsetP1Y())
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public TColorStorage SourceM1P1()
    => _sourceImageGetter(_sourceOffset + _sourceOffsetM1X() + _sourceOffsetP1Y())
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public TColorStorage SourceP0P1()
    => _sourceImageGetter(_sourceOffset + _sourceOffsetP1Y())
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public TColorStorage SourceP1P1()
    => _sourceImageGetter(_sourceOffset + _sourceOffsetP1X() + _sourceOffsetP1Y())
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public TColorStorage SourceP2P1()
    => _sourceImageGetter(_sourceOffset + _sourceOffsetP2X() + _sourceOffsetP1Y())
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public TColorStorage SourceM2P2()
    => _sourceImageGetter(_sourceOffset + _sourceOffsetM2X() + _sourceOffsetP2Y())
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public TColorStorage SourceM1P2()
    => _sourceImageGetter(_sourceOffset + _sourceOffsetM1X() + _sourceOffsetP2Y())
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public TColorStorage SourceP0P2()
    => _sourceImageGetter(_sourceOffset + _sourceOffsetP2Y())
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public TColorStorage SourceP1P2()
    => _sourceImageGetter(_sourceOffset + _sourceOffsetP1X() + _sourceOffsetP2Y())
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public TColorStorage SourceP2P2()
    => _sourceImageGetter(_sourceOffset + _sourceOffsetP2X() + _sourceOffsetP2Y())
  ;
  #endregion

  #region access target points
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void TargetP0M1(TColorStorage value)
    => _targetImageSetter(_targetOffset + _targetOffsetM1Y, value)
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void TargetP1M1(TColorStorage value)
    => _targetImageSetter(_targetOffset + 1 + _targetOffsetM1Y, value)
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void TargetP2M1(TColorStorage value)
    => _targetImageSetter(_targetOffset + 2 + _targetOffsetM1Y, value)
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void TargetP3M1(TColorStorage value)
    => _targetImageSetter(_targetOffset + 3 + _targetOffsetM1Y, value)
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void TargetP4M1(TColorStorage value)
    => _targetImageSetter(_targetOffset + 4 + _targetOffsetM1Y, value)
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void TargetP0P0(TColorStorage value)
    => _targetImageSetter(_targetOffset, value)
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void TargetP1P0(TColorStorage value)
    => _targetImageSetter(_targetOffset + 1, value)
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void TargetP2P0(TColorStorage value)
    => _targetImageSetter(_targetOffset + 2, value)
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void TargetP3P0(TColorStorage value)
    => _targetImageSetter(_targetOffset + 3, value)
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void TargetP4P0(TColorStorage value)
    => _targetImageSetter(_targetOffset + 4, value)
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void TargetP0P1(TColorStorage value)
    => _targetImageSetter(_targetOffset + _targetOffsetP1Y, value)
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void TargetP1P1(TColorStorage value)
    => _targetImageSetter(_targetOffset + 1 + _targetOffsetP1Y, value)
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void TargetP2P1(TColorStorage value)
    => _targetImageSetter(_targetOffset + 2 + _targetOffsetP1Y, value)
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void TargetP3P1(TColorStorage value)
    => _targetImageSetter(_targetOffset + 3 + _targetOffsetP1Y, value)
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void TargetP4P1(TColorStorage value)
    => _targetImageSetter(_targetOffset + 4 + _targetOffsetP1Y, value)
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void TargetP0P2(TColorStorage value)
    => _targetImageSetter(_targetOffset + _targetOffsetP2Y, value)
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void TargetP1P2(TColorStorage value)
    => _targetImageSetter(_targetOffset + 1 + _targetOffsetP2Y, value)
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void TargetP2P2(TColorStorage value)
    => _targetImageSetter(_targetOffset + 2 + _targetOffsetP2Y, value)
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void TargetP3P2(TColorStorage value)
    => _targetImageSetter(_targetOffset + 3 + _targetOffsetP2Y, value)
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void TargetP4P2(TColorStorage value)
    => _targetImageSetter(_targetOffset + 4 + _targetOffsetP2Y, value)
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void TargetP0P3(TColorStorage value)
    => _targetImageSetter(_targetOffset + _targetOffsetP3Y, value)
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void TargetP1P3(TColorStorage value)
    => _targetImageSetter(_targetOffset + 1 + _targetOffsetP3Y, value)
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void TargetP2P3(TColorStorage value)
    => _targetImageSetter(_targetOffset + 2 + _targetOffsetP3Y, value)
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void TargetP3P3(TColorStorage value)
    => _targetImageSetter(_targetOffset + 3 + _targetOffsetP3Y, value)
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void TargetP4P3(TColorStorage value)
    => _targetImageSetter(_targetOffset + 4 + _targetOffsetP3Y, value)
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void TargetP0P4(TColorStorage value)
    => _targetImageSetter(_targetOffset + _targetOffsetP4Y, value)
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void TargetP1P4(TColorStorage value)
    => _targetImageSetter(_targetOffset + 1 + _targetOffsetP4Y, value)
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void TargetP2P4(TColorStorage value)
    => _targetImageSetter(_targetOffset + 2 + _targetOffsetP4Y, value)
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void TargetP3P4(TColorStorage value)
    => _targetImageSetter(_targetOffset + 3 + _targetOffsetP4Y, value)
  ;
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void TargetP4P4(TColorStorage value)
    => _targetImageSetter(_targetOffset + 4 + _targetOffsetP4Y, value)
  ;
  #endregion

  #region calculate source offset deltas

  private int _GetOffsetM2X() => _sourceOffsetM2XValue;
  private int _CalculateOffsetM2X() {
    var current = _sourceX;
    var value = current - 2;
    if (value < 0)
      value = _sourceXWrapper(value, _sourceWidth, true);

    var result = value - current;
    _sourceOffsetM2XValue = result;
    _sourceOffsetM2X = _GetOffsetM2X;
    return result;
  }
  private int _GetOffsetM1X() => _sourceOffsetM1XValue;
  private int _CalculateOffsetM1X() {
    var current = _sourceX;
    var value = current - 1;
    if (value < 0)
      value = _sourceXWrapper(value, _sourceWidth, true);

    var result = value - current;
    _sourceOffsetM1XValue = result;
    _sourceOffsetM1X = _GetOffsetM1X;
    return result;
  }
  private int _GetOffsetP1X() => _sourceOffsetP1XValue;
  private int _CalculateOffsetP1X() {
    var current = _sourceX;
    var value = current + 1;
    if (value >= _sourceWidth)
      value = _sourceXWrapper(value, _sourceWidth, false);

    var result = value - current;
    _sourceOffsetP1XValue = result;
    _sourceOffsetP1X = _GetOffsetP1X;
    return result;
  }
  private int _GetOffsetP2X() => _sourceOffsetP2XValue;
  private int _CalculateOffsetP2X() {
    var current = _sourceX;
    var value = current + 2;
    if (value >= _sourceWidth)
      value = _sourceXWrapper(value, _sourceWidth, false);

    var result = value - current;
    _sourceOffsetP2XValue = result;
    _sourceOffsetP2X = _GetOffsetP2X;
    return result;
  }
  private int _GetOffsetM2Y() => _sourceOffsetM2YValue;
  private int _CalculateOffsetM2Y() {
    var current = _sourceY;
    var value = current - 2;
    if (value < 0)
      value = _sourceYWrapper(value, _sourceHeight, true);

    var result = (value - current) * _sourceStride;
    _sourceOffsetM2YValue = result;
    _sourceOffsetM2Y = _GetOffsetM2Y;
    return result;
  }
  private int _GetOffsetM1Y() => _sourceOffsetM1YValue;
  private int _CalculateOffsetM1Y() {
    var current = _sourceY;
    var value = current - 1;
    if (value < 0)
      value = _sourceYWrapper(value, _sourceHeight, true);

    var result = (value - current) * _sourceStride;
    _sourceOffsetM1YValue = result;
    _sourceOffsetM1Y = _GetOffsetM1Y;
    return result;
  }
  private int _GetOffsetP1Y() => _sourceOffsetP1YValue;
  private int _CalculateOffsetP1Y() {
    var current = _sourceY;
    var value = current + 1;
    if (value >= _sourceHeight)
      value = _sourceYWrapper(value, _sourceHeight, false);

    var result = (value - current) * _sourceStride;
    _sourceOffsetP1YValue = result;
    _sourceOffsetP1Y = _GetOffsetP1Y;
    return result;
  }
  private int _GetOffsetP2Y() => _sourceOffsetP2YValue;
  private int _CalculateOffsetP2Y() {
    var current = _sourceY;
    var value = current + 2;
    if (value >= _sourceHeight)
      value = _sourceYWrapper(value, _sourceHeight, false);

    var result = (value - current) * _sourceStride;
    _sourceOffsetP2YValue = result;
    _sourceOffsetP2Y = _GetOffsetP2Y;
    return result;
  }
  #endregion

}