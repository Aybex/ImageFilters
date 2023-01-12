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
// This file is only generated to unroll the main image working loop upon compile time
using System.Collections.Concurrent;
using System.Drawing;
#if NET45
using System.Windows;
#endif

namespace ImageFilters.Core.Imager {
  partial class cImage {
    private cImage _RunLoop(Rectangle? filterRegion, byte scaleX, byte scaleY, Action<PixelWorker<sPixel>> scaler) {
      var startX = filterRegion == null ? 0 : Math.Max(0, filterRegion.Value.Left);
      var startY = filterRegion == null ? 0 : Math.Max(0, filterRegion.Value.Top);

      var endX = filterRegion == null ? Width : Math.Min(Width, filterRegion.Value.Right);
      var endY = filterRegion == null ? Height : Math.Min(Height, filterRegion.Value.Bottom);

      var width = endX - startX;

      var result = new cImage(width * scaleX, (endY - startY) * scaleY);
          
      Parallel.ForEach(
        Partitioner.Create(startY, endY),
        () => 0,
        (range, _, threadStorage) => {
          var threadSrcMinY = range.Item1;
          var threadSrcMaxY = range.Item2;
          
          var targetY = (threadSrcMinY - startY) * scaleY;
          for (var sourceY = threadSrcMinY; sourceY < threadSrcMaxY;++sourceY) {
            var worker=new PixelWorker<sPixel>(
              i=>GetImageData()[i],
              startX,
              sourceY, 
              _width, 
              _height,
              _width,  
              _horizontalOutOfBoundsHandler,
              _verticalOutOfBoundsHandler, 
              (i,c)=>result.GetImageData()[i]=c,
              0, 
              targetY, 
              result._width
            );
            var xRange=width;
            while(xRange>=64){
              xRange-=64;
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
              scaler(worker);
              worker.IncrementX(scaleX);
            }
            for (; xRange>0;--xRange) {
              scaler(worker);
              worker.IncrementX(scaleX);
            }
            
            targetY += scaleY;
          }
          return (threadStorage);
        },
        _ => { }
      );

      return(result);
    }

#if NET45
    private cImage _RunLoop(Rect? filterRegion, byte scaleX, byte scaleY, Action<PixelWorker<sPixel>> scaler) {
      return _RunLoop(filterRegion?.ToRectangle(), scaleX, scaleY, scaler);
    }
#endif
  }
}