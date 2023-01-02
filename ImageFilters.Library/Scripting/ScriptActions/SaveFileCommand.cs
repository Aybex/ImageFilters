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

using System.Diagnostics.Contracts;
using System.Drawing;
using System.Drawing.Imaging;
using ImageFilters.Library.Imager;

namespace ImageFilters.Library.Scripting.ScriptActions;

public class SaveFileCommand : IScriptAction
{
    #region Implementation of IScriptAction
    public bool ChangesSourceImage => false;

    public bool ChangesTargetImage => false;
    public bool ProvidesNewGdiSource => false;

    public bool Execute()
    {
        var image = TargetImage.ToBitmap();
        if (image is null || string.IsNullOrWhiteSpace(FileName))
            throw new NullReferenceException("Nothing to save");

        var extension = Path.GetExtension(FileName)?.ToUpperInvariant();

        switch (extension)
        {
            case ".JPG":
            case ".JPEG":
                image.Save(FileName, ImageFormat.Bmp);
                break;
            case ".BMP":
                image.Save(FileName, ImageFormat.Bmp);
                break;
            case ".GIF":
                image.Save(FileName, ImageFormat.Gif);
                break;
            case ".TIF":
                image.Save(FileName, ImageFormat.Tiff);
                break;
            default:
                image.Save(FileName, ImageFormat.Png);
                break;
        }
        return true;
    }

    public Bitmap GdiSource => null;

    public cImage SourceImage { get; set; }

    public cImage TargetImage { get; set; }
    #endregion

    public string FileName { get; }

    public SaveFileCommand(string fileName)
    {
        Contract.Requires(!string.IsNullOrWhiteSpace(fileName));
        FileName = fileName;
    }
}