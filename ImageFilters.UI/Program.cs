﻿#region (c)2008-2015 Hawkynt
/*
 *  cImage 
 *  Image filtering library 
    Copyright (C) 2008-2015 Hawkynt

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

using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using ImageFilters.Library.Scripting;
using ImageFilters.UI.Classes;
using Config = ImageFilters.UI.Classes.Config;

namespace ImageFilters.UI; 

class Program {
  #region consts

  /// <summary>
  /// This is the command line parameter that force the app to run in GUI mode
  /// </summary>
  private const string _FORCE_GUI_CLP_NAME = "/FORCEGUI";

  /// <summary>
  /// The name and full path to the currently running executable.
  /// </summary>
  private static readonly string _THIS_EXECUTABLES_FILE_NAME = Assembly.GetEntryAssembly().Location;

  /// <summary>
  /// This is the name of the configuration file.
  /// </summary>
  private static readonly string _CONFIGURATION_FILE_NAME = Path.Combine(Path.GetDirectoryName(_THIS_EXECUTABLES_FILE_NAME), "config.xml");

  #endregion

  #region imports
  [DllImport("kernel32.dll", EntryPoint = "GetConsoleWindow")]
  private static extern IntPtr _GetConsoleWindow();
  #endregion

  /// <summary>
  /// The main entry point for the application.
  /// </summary>
  [STAThread]
  static void Main(string[]? args) {

    /*
     * This works as following:
     * First we look for command line parameters and if there are any of them present, we run the CLI version.
     * If there are no parameters, we try to find out if we are run inside a console and if so, we spawn a new copy of ourselves without a console.
     * If there is no console at all, we show the GUI.
     * This way we're both a CLI and a GUI.
     */

    var firstParam = args != null && args.Length > 0 ? args[0] : null;
    var fileToOpenOnStart = firstParam != _FORCE_GUI_CLP_NAME && File.Exists(firstParam) ? firstParam : null;

    if (firstParam != null && firstParam != _FORCE_GUI_CLP_NAME && fileToOpenOnStart == null) {

      // execute CLI if arguments are given which are not forcing into gui or a valid filename
      var result = CLI.ParseCommandLineArguments(args);
      Environment.Exit((int)result);
    } else {
      var consoleHandle = _GetConsoleWindow();

      // run GUI
      var host = AppDomain.CurrentDomain.FriendlyName;
      if (consoleHandle == IntPtr.Zero || AppDomain.CurrentDomain.FriendlyName.Contains(".vshost") || firstParam == _FORCE_GUI_CLP_NAME || fileToOpenOnStart != null) {

        // we either have no console window or we're started from within visual studio or we are forced into GUI mode
        Application.EnableVisualStyles();
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.SetCompatibleTextRenderingDefault(false);
        Config.Load(_CONFIGURATION_FILE_NAME);
        Application.Run(new MainForm(fileToOpenOnStart));
        Config.Save(_CONFIGURATION_FILE_NAME);
        Environment.Exit((int)CLIExitCode.OK);
      } else {

        // we found a console attached to us, so restart ourselves without one
        Process.Start(new ProcessStartInfo(_THIS_EXECUTABLES_FILE_NAME, _FORCE_GUI_CLP_NAME) {
          CreateNoWindow = true,
          UseShellExecute = false
        });
        Environment.Exit((int)CLIExitCode.RestartingInGuiMode);
      }
    }

  }
}