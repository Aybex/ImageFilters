#region (c)2008-2015 Hawkynt
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
using ImageFilters.Library;
using ImageFilters.Library.Scripting;
using ImageFilters.Library.Scripting.ScriptActions;
using ImageFilters.UI.Classes;
using ImageFilters.UI.Windows;
using Config = ImageFilters.UI.Classes.Config;

/*
 * This file contains all event handlers for the main form.
 * 
 */

namespace ImageFilters.UI; 

partial class MainForm {
  private void btResize_Click(object _, EventArgs __) {
    _scriptEngine.RevertToLastSource();
    _ScaleImageWithCurrentParameters(false);
  }

  private void btSwitch_Click(object sender, EventArgs e) {

    _scriptEngine.ExecuteAction(new TargetToSourceCommand());
    _SourceImage = _scriptEngine.GdiSource;
    _TargetImage = _scriptEngine.GdiTarget;
  }

  private void btRepeat_Click(object sender, EventArgs e) {
    _ScaleImageWithCurrentParameters(true);
  }

  private void openToolStripMenuItem_Click(object sender, EventArgs e) {

    // ask for filename
    var fileDialog = ofdOpenFile;
    fileDialog.InitialDirectory = string.IsNullOrWhiteSpace(Config.LastLoadDirectory) ? Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) : Config.LastLoadDirectory;

    if (fileDialog.ShowDialog() != DialogResult.OK)
      return;

    var fileName = fileDialog.FileName;
    Config.LastLoadDirectory = Path.GetDirectoryName(fileName);

    if (fileName == null)
      return;

    _LoadImageFromFileName(fileName);

    var scriptEngine = _scriptEngine;
    if (nudWidth.Value < 1)
      nudWidth.Value = scriptEngine.GdiSource.Width;

    if (nudHeight.Value < 1)
      nudHeight.Value = scriptEngine.GdiSource.Height;
  }

  private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
    var image = iwhTargetImage.Image;
    if (image == null)
      return;

    var fileName = _lastSaveFileName;
    if (fileName == null) {
      saveAsToolStripMenuItem_Click(sender, e);
      return;
    }

    _scriptEngine.ExecuteAction(new SaveFileCommand(fileName));

    var result = CLI.SaveHelper(fileName, image);
    if (result == CLIExitCode.JpegNotSupportedOnThisPlatform)
      MessageBox.Show(Resources.Resources.txNoJpegSupport, Resources.Resources.ttNoJpegSupport);
    else if (result == CLIExitCode.NothingToSave)
      MessageBox.Show(Resources.Resources.txNothingToSave, Resources.Resources.ttNothingToSave);
  }

  private void saveAsToolStripMenuItem_Click(object sender, EventArgs e) {

    // ask for filename
    var dialog = sfdSave;
    dialog.InitialDirectory = string.IsNullOrWhiteSpace(Config.LastSaveDirectory) ? Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) : Config.LastSaveDirectory;

    if (dialog.ShowDialog() != DialogResult.OK)
      return;

    var fileName = dialog.FileName;
    if (fileName == null)
      return;

    // store the name to use later on
    Config.LastSaveDirectory = Path.GetDirectoryName(fileName);
    _lastSaveFileName = fileName;

    saveToolStripMenuItem_Click(sender, e);
  }

  private void closeToolStripMenuItem_Click(object sender, EventArgs e) {
    _SourceImage = null;
  }

  private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
    Close();
  }

  private void iwhSourceImage_Click(object sender, EventArgs e) {
    openToolStripMenuItem_Click(sender, e);
  }

  private void iwhTargetImage_Click(object sender, EventArgs e) {
    saveToolStripMenuItem_Click(sender, e);

    /*
    // start the image with the associated system handler
    var lastSaveFileName = _lastSaveFileName;
    if (lastSaveFileName != null && File.Exists(lastSaveFileName))
      Process.Start(lastSaveFileName);
  */}

  private void cbResizeMethod_SelectedValueChanged(object sender, EventArgs e) {
    var method = cmbResizeMethod.SelectedValue as IImageManipulator;

    txtDescription.Text = method == null ? null : method.Description;

    _RefreshKernelChart();

    var scriptEngine = _scriptEngine;

    if (!(nudWidth.Enabled = method != null && method.SupportsWidth))
      nudWidth.Value = scriptEngine.GdiTarget == null ? scriptEngine.GdiSource == null ? 0 : scriptEngine.GdiSource.Width : scriptEngine.GdiTarget.Width;

    if (!(nudHeight.Enabled = method != null && method.SupportsHeight))
      nudHeight.Value = scriptEngine.GdiTarget == null ? scriptEngine.GdiSource == null ? 0 : scriptEngine.GdiSource.Height : scriptEngine.GdiTarget.Height;

    chkUseCenteredGrid.Enabled = method != null && method.SupportsGridCentering;
    chkUseThresholds.Enabled = method != null && method.SupportsThresholds;

    if (!(nudRepetitionCount.Enabled = method != null && method.SupportsRepetitionCount))
      nudRepetitionCount.Value = 1;

    nudRadius.Enabled = method != null && method.SupportsRadius;
  }

  private void nudRadius_ValueChanged(object sender, EventArgs e) {
    _RefreshKernelChart();
  }

  private void stretchToolStripMenuItem_Click(object sender, EventArgs e) {
    _SourceImageSizeMode = PictureBoxSizeMode.StretchImage;
  }

  private void centerToolStripMenuItem_Click(object sender, EventArgs e) {
    _SourceImageSizeMode = PictureBoxSizeMode.CenterImage;
  }

  private void zoomToolStripMenuItem_Click(object sender, EventArgs e) {
    _SourceImageSizeMode = PictureBoxSizeMode.Zoom;
  }

  private void stretchToolStripMenuItem1_Click(object sender, EventArgs e) {
    _TargetImageSizeMode = PictureBoxSizeMode.StretchImage;
  }

  private void centerToolStripMenuItem1_Click(object sender, EventArgs e) {
    _TargetImageSizeMode = PictureBoxSizeMode.CenterImage;
  }

  private void zoomToolStripMenuItem1_Click(object sender, EventArgs e) {
    _TargetImageSizeMode = PictureBoxSizeMode.Zoom;
  }

  private void iwhSourceImage_DragEnter(object sender, DragEventArgs e) {
    if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
      var files = _GetSupportedFiles(e);
      if (files == null || files.Length < 1)
        return;

      e.Effect = DragDropEffects.Copy;
      return;
    }
    if (e.Data.GetDataPresent(DataFormats.Bitmap)) {
      e.Effect = DragDropEffects.Copy;
      return;
    }
    e.Effect = DragDropEffects.None;
  }

  private void iwhSourceImage_DragDrop(object sender, DragEventArgs e) {
    if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
      var files = _GetSupportedFiles(e);
      if (files == null || files.Length < 1)
        return;

      if (_IsSupportedFileExtension(Path.GetExtension(files[0])))
        _LoadImageFromFileName(files[0]);
      else
        _ApplyScriptFile(files[0]);
      return;
    }
    if (e.Data.GetDataPresent(DataFormats.Bitmap)) {
      var data = e.Data.GetData(DataFormats.Bitmap) as Image;
      if (data == null)
        return;
      _SourceImage = data;
      _lastSaveFileName = null;
      return;
    }
  }

  private void chkKeepAspect_CheckedChanged(object sender, EventArgs e) {
    var value = chkKeepAspect.Checked;
    if (value) {
      var sourceImage = iwhSourceImage.Image;
      if (sourceImage == null)
        return;

      _CorrectAspectRatioIfNeeded(false);
    }
  }

  private void nudWidth_ValueChanged(object sender, EventArgs e) {
    _CorrectAspectRatioIfNeeded(false);
  }

  private void nudHeight_ValueChanged(object sender, EventArgs e) {
    _CorrectAspectRatioIfNeeded(true);
  }

  private void showToolStripMenuItem_Click(object sender, EventArgs e) {
    MessageBox.Show(ScriptSerializer.SerializeState(_scriptEngine), "Script", MessageBoxButtons.OK, MessageBoxIcon.None);
  }

  private void clearToolStripMenuItem_Click(object sender, EventArgs e) {
    _scriptEngine.Clear();
  }

  private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
    var window = new AboutWindow();
    window.ShowDialog();
  }

  private void wikiToolStripMenuItem_Click(object sender, EventArgs e) {
    Process.Start(Resources.Resources.urlWiki);
  }

  private void executeToolStripMenuItem_Click(object sender, EventArgs e) {
    _scriptEngine.RepeatActions();
    _SourceImage = _scriptEngine.GdiSource;
    _TargetImage = _scriptEngine.GdiTarget;
  }

  private void saveToolStripMenuItem1_Click(object sender, EventArgs e) {
    var engine = _scriptEngine;
    if (!engine.Actions.Any()) {
      MessageBox.Show(Resources.Resources.txNoScriptToSave, Resources.Resources.ttNoScriptToSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
      return;
    }

    var dialog = sfdSaveScript;
    if (dialog.ShowDialog() != DialogResult.OK)
      return;

    var filename = dialog.FileName;
    ScriptSerializer.SaveToFile(engine, filename);
  }

  private void loadToolStripMenuItem_Click(object sender, EventArgs e) {
    var dialog = ofdOpenScript;
    if (dialog.ShowDialog() != DialogResult.OK)
      return;

    var filename = dialog.FileName;
    _scriptEngine.Clear();
    ScriptSerializer.LoadFromFile(_scriptEngine, filename);
  }
}