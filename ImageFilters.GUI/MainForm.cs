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
using System.Diagnostics.Contracts;
using ImageFilters.GUI.Windows;
using ImageFilters.ImageManipulators;
using ImageFilters.Imager;
using ImageFilters.Imager.Interface;
using ImageFilters.Scripting;
using ImageFilters.Scripting.ScriptActions;
using word = System.UInt16;

namespace ImageFilters.GUI;

/// <summary>
/// Our main GUI.
/// </summary>
public partial class MainForm : Form
{
    #region fields
    /// <summary>
    /// The last used filename for SaveAs.
    /// </summary>
    private string _lastSaveFileName;
    /// <summary>
    /// The used scripting engine.
    /// </summary>
    private readonly ScriptEngine _scriptEngine = new();
    #endregion

    #region props
    /// <summary>
    /// Gets or sets the source image.
    /// </summary>
    /// <value>
    /// The source image.
    /// </value>
    private Image _SourceImage
    {
        set
        {
            gbActions.Enabled =
              closeToolStripMenuItem.Enabled =
                value != null;
            _TargetImage = null;
            iwhSourceImage.Image = value;
            _CorrectAspectRatioIfNeeded(false);
        }
    }

    /// <summary>
    /// Gets or sets the target image.
    /// </summary>
    /// <value>
    /// The target image.
    /// </value>
    private Image _TargetImage
    {
        set
        {
            butRepeat.Enabled =
              butSwitch.Enabled =
                saveToolStripMenuItem.Enabled =
                  saveAsToolStripMenuItem.Enabled =
                    tssBenchmark.Visible =
                      value != null;
            iwhTargetImage.Image = value;
        }
    }

    private PictureBoxSizeMode _SourceImageSizeMode
    {
        get { return iwhSourceImage.SizeMode; }
        set
        {
            Config.SourceSizeMode = iwhSourceImage.SizeMode = value;
            stretchToolStripMenuItem.Checked =
              centerToolStripMenuItem.Checked =
                zoomToolStripMenuItem.Checked = false;

            switch (value)
            {
                case PictureBoxSizeMode.StretchImage:
                    {
                        stretchToolStripMenuItem.Checked = true;
                        break;
                    }
                case PictureBoxSizeMode.CenterImage:
                    {
                        centerToolStripMenuItem.Checked = true;
                        break;
                    }
                case PictureBoxSizeMode.Zoom:
                    {
                        zoomToolStripMenuItem.Checked = true;
                        break;
                    }
            }
        }
    }

    private PictureBoxSizeMode _TargetImageSizeMode
    {
        get { return iwhTargetImage.SizeMode; }
        set
        {
            Config.TargetSizeMode = iwhTargetImage.SizeMode = value;
            stretchToolStripMenuItem1.Checked =
              centerToolStripMenuItem1.Checked =
                zoomToolStripMenuItem1.Checked = false;

            switch (value)
            {
                case PictureBoxSizeMode.StretchImage:
                    {
                        stretchToolStripMenuItem1.Checked = true;
                        break;
                    }
                case PictureBoxSizeMode.CenterImage:
                    {
                        centerToolStripMenuItem1.Checked = true;
                        break;
                    }
                case PictureBoxSizeMode.Zoom:
                    {
                        zoomToolStripMenuItem1.Checked = true;
                        break;
                    }
            }
        }
    }

    #endregion

    #region ctor
    public MainForm(string fileToOpenOnStart = null)
    {
        InitializeComponent();

        //this.cbResizeMethod.DataSource = Program.IMAGE_RESIZERS;
        cmbResizeMethod.DataSource = SupportedManipulators.MANIPULATORS;
        cmbResizeMethod.DisplayMember = "Key";
        cmbResizeMethod.ValueMember = "Value";

        cmbResizeMethod.SelectedIndex = 0;

        cmbHorizontalBPH.DataSource = Enum.GetValues(typeof(OutOfBoundsMode));
        cmbVerticalBPH.DataSource = Enum.GetValues(typeof(OutOfBoundsMode));

        _SourceImage = null;

        sfdSave.InitialDirectory =
          ofdOpenFile.InitialDirectory =
            Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

        chkUseThresholds.Checked = sPixel.AllowThresholds;

        _LoadConfigurationSettings();

        if (fileToOpenOnStart != null)
            _LoadImageFromFileName(fileToOpenOnStart);

    }

    #endregion

    /// <summary>
    /// Loads and applies the configuration settings.
    /// </summary>
    private void _LoadConfigurationSettings()
    {
        if (Config.SourceSizeMode != null)
            _SourceImageSizeMode = Config.SourceSizeMode.Value;

        if (Config.TargetSizeMode != null)
            _TargetImageSizeMode = Config.TargetSizeMode.Value;
    }

    /// <summary>
    /// Resizes the given image with the currently set parameters from the GUI.
    /// </summary>
    private void _ScaleImageWithCurrentParameters(bool applyToTarget)
    {
        var method = (IImageManipulator)cmbResizeMethod.SelectedValue;
        var targetWidth = (word)nudWidth.Value;
        var targetHeight = (word)nudHeight.Value;
        var maintainAspect = chkKeepAspect.Checked;
        var useThresholds = chkUseThresholds.Checked;
        var useCenteredGrid = chkUseCenteredGrid.Checked;
        var repetitionCount = (byte)nudRepetitionCount.Value;
        var horizontalBph = (OutOfBoundsMode)cmbHorizontalBPH.SelectedItem;
        var verticalBph = (OutOfBoundsMode)cmbVerticalBPH.SelectedItem;
        var radius = (float)nudRadius.Value;

        if (targetWidth <= 0 && method.SupportsWidth || targetHeight <= 0 && method.SupportsHeight)
        {
            MessageBox.Show(Resources.txNeedWidthAndHeightAboveZero, Resources.ttNeedWidthAndHeightAboveZero, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            return;
        }

        var command = new ResizeCommand(applyToTarget, method, targetWidth, targetHeight, 0, maintainAspect, horizontalBph, verticalBph, repetitionCount, useThresholds, useCenteredGrid, radius);

        _ExecuteScriptActions(command);
    }

    /// <summary>
    /// Executes the given script actions.
    /// </summary>
    /// <param name="commands">The commands.</param>
    private void _ExecuteScriptActions(params IScriptAction[] commands)
    {
        Contract.Requires(commands != null);

        // tell the user that we're busy
        msMain.Enabled =
          tlpMainLayout.Enabled =
            !(tssBusy.Visible = true);

        Task t = Task.Factory.StartNew(() =>
        {
            // filter image
            var stopwatch = new Stopwatch();
            stopwatch.Restart();

            foreach (var command in commands)
                _scriptEngine.ExecuteAction(command);

            var gdiSource = _scriptEngine.GdiSource;
            var gdiTarget = _scriptEngine.GdiTarget;
            stopwatch.Stop();

            Invoke(() =>
            {
                _SourceImage = gdiSource;
                _TargetImage = gdiTarget;

                tssBenchmark.Text = stopwatch.ElapsedMilliseconds + "ms";
                tssBenchmark.Visible = true;

                // let the user know, that we're no longer busy
                msMain.Enabled =
                  tlpMainLayout.Enabled =
                    !(tssBusy.Visible = false);

                Enabled = true;
            });
        });
    }

    /// <summary>
    /// Refreshes the kernel chart if necessary or hides it when not applicable.
    /// </summary>
    private void _RefreshKernelChart()
    {
        var method = cmbResizeMethod.SelectedValue as IImageManipulator;

        var chart = chtKernel;
        var dataPointCollection = chart.Series[0].Points;
        dataPointCollection.Clear();
        chart.Visible = false;

        var kernelBasedResampler = method as Resampler;
        var kernelBasedRadiusResampler = method as RadiusResampler;
        if (kernelBasedResampler == null && kernelBasedRadiusResampler == null)
            return;

        var info = kernelBasedRadiusResampler == null ? kernelBasedResampler.GetKernelMethodInfo() : kernelBasedRadiusResampler.GetKernelMethodInfo((float)nudRadius.Value);
        for (var x = -info.KernelRadius; x <= info.KernelRadius; x += 0.001f)
            dataPointCollection.AddXY(Math.Round(x, 3), Math.Round(info.Kernel(x), 3));
        chart.ChartAreas[0].AxisX.Minimum = -Math.Round(info.KernelRadius, 1);
        chart.ChartAreas[0].AxisX.Maximum = Math.Round(info.KernelRadius, 1);
        chart.Visible = true;
    }

    /// <summary>
    /// Loads the image from the given filename into the GUI.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    private void _LoadImageFromFileName(string fileName)
    {
        try
        {
            var scriptEngine = _scriptEngine;
            scriptEngine.ExecuteAction(new LoadFileCommand(fileName));
            _SourceImage = scriptEngine.GdiSource;
            _lastSaveFileName = null;
        }
        catch (Exception exception)
        {
            MessageBox.Show(string.Format(Resources.txCouldNotLoadImage, fileName, exception.Message), Resources.ttCouldNotLoadImage, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Corrects target width/height if forced to keep aspect ratio.
    /// </summary>
    /// <param name="useHeight">if set to <c>true</c> we calculate target width from height; otherwise, we calculate target height from width.</param>
    private void _CorrectAspectRatioIfNeeded(bool useHeight)
    {
        if (!chkKeepAspect.Checked)
            return;

        var image = iwhSourceImage.Image;
        if (image == null)
            return;

        var width = nudWidth.Value;
        var height = nudHeight.Value;

        if (useHeight)
            width = Math.Round(height * image.Width / image.Height);

        else
            height = Math.Round(width * image.Height / image.Width);


        if (width != nudWidth.Value)
            nudWidth.Value = width;

        if (height != nudHeight.Value)
            nudHeight.Value = height;
    }

    /// <summary>
    /// Filters the image.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="method">The method.</param>
    /// <param name="targetWidth">Width of the target.</param>
    /// <param name="targetHeight">Height of the target.</param>
    /// <param name="horizontalBh">The horizontal bounds handling.</param>
    /// <param name="verticalBh">The vertical bounds handling.</param>
    /// <param name="useThresholds">if set to <c>true</c> [use thresholds].</param>
    /// <param name="useCenteredGrid">if set to <c>true</c> [use centered grid].</param>
    /// <param name="repetitionCount">The repetition count.</param>
    /// <param name="radius">The radius.</param>
    /// <returns></returns>
    public static cImage FilterImage(cImage source, IImageManipulator method, ushort targetWidth, ushort targetHeight, OutOfBoundsMode horizontalBh, OutOfBoundsMode verticalBh, bool useThresholds, bool useCenteredGrid, byte repetitionCount, float radius)
    {
        Contract.Requires(source != null);
        sPixel.AllowThresholds = useThresholds;
        source.HorizontalOutOfBoundsMode = horizontalBh;
        source.VerticalOutOfBoundsMode = verticalBh;

        cImage result = null;
        var scaler = method as AScaler;
        var planeExtractor = method as PlaneExtractor;
        var resampler = method as Resampler;
        var radiusResampler = method as RadiusResampler;

        if (scaler != null)
        {
            result = source;
            for (var i = 0; i < repetitionCount; i++)
                result = scaler.Apply(result);
        }
        else if (method is Interpolator interpolator)
        {
            if (targetWidth <= 0 || targetHeight <= 0)
                MessageBox.Show(Resources.txNeedWidthAndHeightAboveZero, Resources.ttNeedWidthAndHeightAboveZero, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            else
                result = interpolator.Apply(source, targetWidth, targetHeight);
        }
        else if (planeExtractor != null)
            result = planeExtractor.Apply(source);
        else if (resampler != null)
            if (targetWidth <= 0 || targetHeight <= 0)
                MessageBox.Show(Resources.txNeedWidthAndHeightAboveZero, Resources.ttNeedWidthAndHeightAboveZero, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            else
                result = resampler.Apply(source, targetWidth, targetHeight, useCenteredGrid);
        else if (radiusResampler != null)
            if (targetWidth <= 0 || targetHeight <= 0)
                MessageBox.Show(Resources.txNeedWidthAndHeightAboveZero, Resources.ttNeedWidthAndHeightAboveZero, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            else
                result = radiusResampler.Apply(source, targetWidth, targetHeight, radius, useCenteredGrid);

        return result;
    }

    /// <summary>
    /// Determines whether or not the given file extension is usable for the program.
    /// </summary>
    /// <param name="extension">The extension.</param>
    /// <returns><c>true</c> if we accept this file extensions; otherwise, <c>false</c>.</returns>
    private static bool _IsSupportedFileExtension(string extension)
    {
        if (string.IsNullOrWhiteSpace(extension))
            return false;
        return extension.Trim().ToUpper() is ".JPEG" or ".JPG" or ".BMP" or ".PNG" or ".GIF" or ".TIF" or ".TIFF";
    }

    /// <summary>
    /// Gets all supported file names from a Drag'N'Drop operation.
    /// </summary>
    /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
    /// <returns>The list of files which could be accepted.</returns>
    private static string[]? _GetSupportedFiles(DragEventArgs e)
    {
        var files = ((Array)e?.Data.GetData(DataFormats.FileDrop))?.OfType<string>().ToArray();
        if (files == null || files.Length < 1)
            return null;
        return files.Where(f => _IsSupportedFileExtension(Path.GetExtension(f)) || string.Equals(Resources.defaultFileExtension, Path.GetExtension(f))).ToArray();
    }


    #region EventHandler

    private void btResize_Click(object _, EventArgs __)
    {
        _scriptEngine.RevertToLastSource();
        _ScaleImageWithCurrentParameters(false);
    }

    private void btSwitch_Click(object sender, EventArgs e)
    {

        _scriptEngine.ExecuteAction(new TargetToSourceCommand());
        _SourceImage = _scriptEngine.GdiSource;
        _TargetImage = _scriptEngine.GdiTarget;
    }

    private void btRepeat_Click(object sender, EventArgs e)
    {
        _ScaleImageWithCurrentParameters(true);
    }

    private void openToolStripMenuItem_Click(object sender, EventArgs e)
    {

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

    private void saveToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var image = iwhTargetImage.Image;
        if (image == null)
            return;

        var fileName = _lastSaveFileName;
        if (fileName == null)
        {
            saveAsToolStripMenuItem_Click(sender, e);
            return;
        }

        _scriptEngine.ExecuteAction(new SaveFileCommand(fileName));

        /*
        var result = CLI.SaveHelper(fileName, image);
        if (result == CLIExitCode.JpegNotSupportedOnThisPlatform)
          MessageBox.Show(Resources.txNoJpegSupport, Resources.ttNoJpegSupport);
        else if (result == CLIExitCode.NothingToSave)
          MessageBox.Show(Resources.txNothingToSave, Resources.ttNothingToSave);
      */
    }

    private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
    {

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

    private void closeToolStripMenuItem_Click(object sender, EventArgs e)
    {
        _SourceImage = null;
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void iwhSourceImage_Click(object sender, EventArgs e)
    {
        openToolStripMenuItem_Click(sender, e);
    }

    private void iwhTargetImage_Click(object sender, EventArgs e)
    {
        saveToolStripMenuItem_Click(sender, e);

        /*
        // start the image with the associated system handler
        var lastSaveFileName = _lastSaveFileName;
        if (lastSaveFileName != null && File.Exists(lastSaveFileName))
          Process.Start(lastSaveFileName);
      */
    }

    private void cbResizeMethod_SelectedValueChanged(object sender, EventArgs e)
    {
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

    private void nudRadius_ValueChanged(object sender, EventArgs e)
    {
        _RefreshKernelChart();
    }

    private void stretchToolStripMenuItem_Click(object sender, EventArgs e)
    {
        _SourceImageSizeMode = PictureBoxSizeMode.StretchImage;
    }

    private void centerToolStripMenuItem_Click(object sender, EventArgs e)
    {
        _SourceImageSizeMode = PictureBoxSizeMode.CenterImage;
    }

    private void zoomToolStripMenuItem_Click(object sender, EventArgs e)
    {
        _SourceImageSizeMode = PictureBoxSizeMode.Zoom;
    }

    private void stretchToolStripMenuItem1_Click(object sender, EventArgs e)
    {
        _TargetImageSizeMode = PictureBoxSizeMode.StretchImage;
    }

    private void centerToolStripMenuItem1_Click(object sender, EventArgs e)
    {
        _TargetImageSizeMode = PictureBoxSizeMode.CenterImage;
    }

    private void zoomToolStripMenuItem1_Click(object sender, EventArgs e)
    {
        _TargetImageSizeMode = PictureBoxSizeMode.Zoom;
    }

    private void iwhSourceImage_DragEnter(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            var files = _GetSupportedFiles(e);
            if (files == null || files.Length < 1)
                return;

            e.Effect = DragDropEffects.Copy;
            return;
        }
        if (e.Data.GetDataPresent(DataFormats.Bitmap))
        {
            e.Effect = DragDropEffects.Copy;
            return;
        }
        e.Effect = DragDropEffects.None;
    }

    private void iwhSourceImage_DragDrop(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            var files = _GetSupportedFiles(e);
            if (files == null || files.Length < 1)
                return;

            if (_IsSupportedFileExtension(Path.GetExtension(files[0])))
                _LoadImageFromFileName(files[0]);
            /*
            else
              _ApplyScriptFile(files[0]);
          */
            return;
        }
        if (e.Data.GetDataPresent(DataFormats.Bitmap))
        {
            var data = e.Data.GetData(DataFormats.Bitmap) as Image;
            if (data == null)
                return;
            _SourceImage = data;
            _lastSaveFileName = null;
            return;
        }
    }

    private void chkKeepAspect_CheckedChanged(object sender, EventArgs e)
    {
        var value = chkKeepAspect.Checked;
        if (value)
        {
            var sourceImage = iwhSourceImage.Image;
            if (sourceImage == null)
                return;

            _CorrectAspectRatioIfNeeded(false);
        }
    }

    private void nudWidth_ValueChanged(object sender, EventArgs e)
    {
        _CorrectAspectRatioIfNeeded(false);
    }

    private void nudHeight_ValueChanged(object sender, EventArgs e)
    {
        _CorrectAspectRatioIfNeeded(true);
    }

    private void showToolStripMenuItem_Click(object sender, EventArgs e)
    {
        MessageBox.Show(ScriptSerializer.SerializeState(_scriptEngine), "Script", MessageBoxButtons.OK, MessageBoxIcon.None);
    }

    private void clearToolStripMenuItem_Click(object sender, EventArgs e)
    {
        _scriptEngine.Clear();
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var window = new AboutWindow();
        window.ShowDialog();
    }

    private void wikiToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Process.Start(Resources.urlWiki);
    }

    private void executeToolStripMenuItem_Click(object sender, EventArgs e)
    {
        _scriptEngine.RepeatActions();
        _SourceImage = _scriptEngine.GdiSource;
        _TargetImage = _scriptEngine.GdiTarget;
    }

    private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
    {
        var engine = _scriptEngine;
        if (!engine.Actions.Any())
        {
            MessageBox.Show(Resources.txNoScriptToSave, Resources.ttNoScriptToSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var dialog = sfdSaveScript;
        if (dialog.ShowDialog() != DialogResult.OK)
            return;

        var filename = dialog.FileName;
        ScriptSerializer.SaveToFile(engine, filename);
    }

    private void loadToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var dialog = ofdOpenScript;
        if (dialog.ShowDialog() != DialogResult.OK)
            return;

        var filename = dialog.FileName;
        _scriptEngine.Clear();
        ScriptSerializer.LoadFromFile(_scriptEngine, filename);
    }

    #endregion

}