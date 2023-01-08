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

namespace ImageFilters.Core.Scripting; 

public class ScriptSerializerException : Exception {
  public string Filename { get; }
  public int LineNumber { get; }
  public ExitCode ErrorType { get; }

  public ScriptSerializerException(string filename, int lineNumber, ExitCode errorType) {
    Filename = filename;
    LineNumber = lineNumber;
    ErrorType = errorType;
  }
}