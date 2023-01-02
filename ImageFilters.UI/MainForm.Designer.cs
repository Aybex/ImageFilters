namespace ImageFilters.UI {
  partial class MainForm {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
            System.Windows.Forms.StatusStrip ssBottom;
            System.Windows.Forms.GroupBox gbSourceImage;
            System.Windows.Forms.GroupBox gbTargetImage;
            System.Windows.Forms.FlowLayoutPanel flpActions;
            System.Windows.Forms.GroupBox gbAdvanced;
            System.Windows.Forms.GroupBox gbBorderPixelHandling;
            System.Windows.Forms.Label label7;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.GroupBox gbTargetResolution;
            System.Windows.Forms.Label label9;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.GroupBox gbMethod;
            System.Windows.Forms.GroupBox gbDescription;
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tssBusy = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssBenchmark = new System.Windows.Forms.ToolStripStatusLabel();
            this.iwhSourceImage = new ImageFilters.UI.UserControls.ImageWithDetails();
            this.iwhTargetImage = new ImageFilters.UI.UserControls.ImageWithDetails();
            this.butResize = new System.Windows.Forms.Button();
            this.butSwitch = new System.Windows.Forms.Button();
            this.butRepeat = new System.Windows.Forms.Button();
            this.nudRadius = new System.Windows.Forms.NumericUpDown();
            this.lblRadius = new System.Windows.Forms.Label();
            this.lblRepititionCount = new System.Windows.Forms.Label();
            this.nudRepetitionCount = new System.Windows.Forms.NumericUpDown();
            this.chkUseCenteredGrid = new System.Windows.Forms.CheckBox();
            this.chkUseThresholds = new System.Windows.Forms.CheckBox();
            this.cmbVerticalBPH = new System.Windows.Forms.ComboBox();
            this.cmbHorizontalBPH = new System.Windows.Forms.ComboBox();
            this.chkKeepAspect = new System.Windows.Forms.CheckBox();
            this.nudWidth = new System.Windows.Forms.NumericUpDown();
            this.nudHeight = new System.Windows.Forms.NumericUpDown();
            this.cmbResizeMethod = new System.Windows.Forms.ComboBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sourceImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stretchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.centerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.targetImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stretchToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.centerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.scriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.executeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wikiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tlpMainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.pnMiddle = new System.Windows.Forms.Panel();
            this.gbKernelFunction = new System.Windows.Forms.GroupBox();
            this.chtKernel = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.gbActions = new System.Windows.Forms.GroupBox();
            this.ofdOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.sfdSave = new System.Windows.Forms.SaveFileDialog();
            this.ofdOpenScript = new System.Windows.Forms.OpenFileDialog();
            this.sfdSaveScript = new System.Windows.Forms.SaveFileDialog();
            ssBottom = new System.Windows.Forms.StatusStrip();
            gbSourceImage = new System.Windows.Forms.GroupBox();
            gbTargetImage = new System.Windows.Forms.GroupBox();
            flpActions = new System.Windows.Forms.FlowLayoutPanel();
            gbAdvanced = new System.Windows.Forms.GroupBox();
            gbBorderPixelHandling = new System.Windows.Forms.GroupBox();
            label7 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            gbTargetResolution = new System.Windows.Forms.GroupBox();
            label9 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            gbMethod = new System.Windows.Forms.GroupBox();
            gbDescription = new System.Windows.Forms.GroupBox();
            ssBottom.SuspendLayout();
            gbSourceImage.SuspendLayout();
            gbTargetImage.SuspendLayout();
            flpActions.SuspendLayout();
            gbAdvanced.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRepetitionCount)).BeginInit();
            gbBorderPixelHandling.SuspendLayout();
            gbTargetResolution.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).BeginInit();
            gbMethod.SuspendLayout();
            gbDescription.SuspendLayout();
            this.msMain.SuspendLayout();
            this.tlpMainLayout.SuspendLayout();
            this.pnMiddle.SuspendLayout();
            this.gbKernelFunction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chtKernel)).BeginInit();
            this.gbActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // ssBottom
            // 
            ssBottom.ImageScalingSize = new System.Drawing.Size(20, 20);
            ssBottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssBusy,
            this.tssBenchmark});
            ssBottom.Location = new System.Drawing.Point(0, 904);
            ssBottom.Name = "ssBottom";
            ssBottom.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            ssBottom.Size = new System.Drawing.Size(1185, 22);
            ssBottom.TabIndex = 1;
            ssBottom.Text = "statusStrip1";
            // 
            // tssBusy
            // 
            this.tssBusy.Image = global::ImageFilters.UI.Resources.Resources.ProgressCircularBlue;
            this.tssBusy.Name = "tssBusy";
            this.tssBusy.Size = new System.Drawing.Size(93, 20);
            this.tssBusy.Text = "Resizing...";
            this.tssBusy.Visible = false;
            // 
            // tssBenchmark
            // 
            this.tssBenchmark.Name = "tssBenchmark";
            this.tssBenchmark.Size = new System.Drawing.Size(0, 16);
            // 
            // gbSourceImage
            // 
            gbSourceImage.Controls.Add(this.iwhSourceImage);
            gbSourceImage.Dock = System.Windows.Forms.DockStyle.Fill;
            gbSourceImage.Location = new System.Drawing.Point(4, 5);
            gbSourceImage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            gbSourceImage.Name = "gbSourceImage";
            gbSourceImage.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            gbSourceImage.Size = new System.Drawing.Size(384, 866);
            gbSourceImage.TabIndex = 0;
            gbSourceImage.TabStop = false;
            gbSourceImage.Text = "Source Image";
            // 
            // iwhSourceImage
            // 
            this.iwhSourceImage.AllowDrop = true;
            this.iwhSourceImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iwhSourceImage.Location = new System.Drawing.Point(4, 25);
            this.iwhSourceImage.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.iwhSourceImage.Name = "iwhSourceImage";
            this.iwhSourceImage.Size = new System.Drawing.Size(376, 836);
            this.iwhSourceImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.iwhSourceImage.TabIndex = 0;
            this.iwhSourceImage.Click += new System.EventHandler(this.iwhSourceImage_Click);
            this.iwhSourceImage.DragDrop += new System.Windows.Forms.DragEventHandler(this.iwhSourceImage_DragDrop);
            this.iwhSourceImage.DragEnter += new System.Windows.Forms.DragEventHandler(this.iwhSourceImage_DragEnter);
            // 
            // gbTargetImage
            // 
            gbTargetImage.Controls.Add(this.iwhTargetImage);
            gbTargetImage.Dock = System.Windows.Forms.DockStyle.Fill;
            gbTargetImage.Location = new System.Drawing.Point(796, 5);
            gbTargetImage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            gbTargetImage.Name = "gbTargetImage";
            gbTargetImage.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            gbTargetImage.Size = new System.Drawing.Size(385, 866);
            gbTargetImage.TabIndex = 1;
            gbTargetImage.TabStop = false;
            gbTargetImage.Text = "Target Image";
            // 
            // iwhTargetImage
            // 
            this.iwhTargetImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iwhTargetImage.Location = new System.Drawing.Point(4, 25);
            this.iwhTargetImage.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.iwhTargetImage.Name = "iwhTargetImage";
            this.iwhTargetImage.Size = new System.Drawing.Size(377, 836);
            this.iwhTargetImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.iwhTargetImage.TabIndex = 0;
            this.iwhTargetImage.Click += new System.EventHandler(this.iwhTargetImage_Click);
            // 
            // flpActions
            // 
            flpActions.AutoSize = true;
            flpActions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flpActions.Controls.Add(this.butResize);
            flpActions.Controls.Add(this.butSwitch);
            flpActions.Controls.Add(this.butRepeat);
            flpActions.Dock = System.Windows.Forms.DockStyle.Top;
            flpActions.Location = new System.Drawing.Point(4, 25);
            flpActions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            flpActions.Name = "flpActions";
            flpActions.Size = new System.Drawing.Size(384, 45);
            flpActions.TabIndex = 0;
            // 
            // butResize
            // 
            this.butResize.Image = global::ImageFilters.UI.Resources.Resources.Resize;
            this.butResize.Location = new System.Drawing.Point(4, 5);
            this.butResize.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.butResize.Name = "butResize";
            this.butResize.Size = new System.Drawing.Size(100, 35);
            this.butResize.TabIndex = 0;
            this.butResize.Text = "Resize";
            this.butResize.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.butResize.UseVisualStyleBackColor = true;
            this.butResize.Click += new System.EventHandler(this.btResize_Click);
            // 
            // butSwitch
            // 
            this.butSwitch.Image = global::ImageFilters.UI.Resources.Resources.Switch;
            this.butSwitch.Location = new System.Drawing.Point(112, 5);
            this.butSwitch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.butSwitch.Name = "butSwitch";
            this.butSwitch.Size = new System.Drawing.Size(100, 35);
            this.butSwitch.TabIndex = 0;
            this.butSwitch.Text = "Switch";
            this.butSwitch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.butSwitch.UseVisualStyleBackColor = true;
            this.butSwitch.Click += new System.EventHandler(this.btSwitch_Click);
            // 
            // butRepeat
            // 
            this.butRepeat.Image = global::ImageFilters.UI.Resources.Resources.Repeat;
            this.butRepeat.Location = new System.Drawing.Point(220, 5);
            this.butRepeat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.butRepeat.Name = "butRepeat";
            this.butRepeat.Size = new System.Drawing.Size(100, 35);
            this.butRepeat.TabIndex = 0;
            this.butRepeat.Text = "Repeat";
            this.butRepeat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.butRepeat.UseVisualStyleBackColor = true;
            this.butRepeat.Click += new System.EventHandler(this.btRepeat_Click);
            // 
            // gbAdvanced
            // 
            gbAdvanced.AutoSize = true;
            gbAdvanced.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            gbAdvanced.Controls.Add(this.nudRadius);
            gbAdvanced.Controls.Add(this.lblRadius);
            gbAdvanced.Controls.Add(this.lblRepititionCount);
            gbAdvanced.Controls.Add(this.nudRepetitionCount);
            gbAdvanced.Controls.Add(this.chkUseCenteredGrid);
            gbAdvanced.Controls.Add(this.chkUseThresholds);
            gbAdvanced.Dock = System.Windows.Forms.DockStyle.Top;
            gbAdvanced.Location = new System.Drawing.Point(0, 427);
            gbAdvanced.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            gbAdvanced.Name = "gbAdvanced";
            gbAdvanced.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            gbAdvanced.Size = new System.Drawing.Size(392, 197);
            gbAdvanced.TabIndex = 3;
            gbAdvanced.TabStop = false;
            gbAdvanced.Text = "Advanced";
            // 
            // nudRadius
            // 
            this.nudRadius.DecimalPlaces = 2;
            this.nudRadius.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudRadius.Location = new System.Drawing.Point(76, 140);
            this.nudRadius.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudRadius.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.nudRadius.Name = "nudRadius";
            this.nudRadius.Size = new System.Drawing.Size(65, 27);
            this.nudRadius.TabIndex = 3;
            this.nudRadius.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRadius.ValueChanged += new System.EventHandler(this.nudRadius_ValueChanged);
            // 
            // lblRadius
            // 
            this.lblRadius.AutoSize = true;
            this.lblRadius.Location = new System.Drawing.Point(8, 142);
            this.lblRadius.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRadius.Name = "lblRadius";
            this.lblRadius.Size = new System.Drawing.Size(53, 20);
            this.lblRadius.TabIndex = 2;
            this.lblRadius.Text = "Radius";
            // 
            // lblRepititionCount
            // 
            this.lblRepititionCount.AutoSize = true;
            this.lblRepititionCount.Location = new System.Drawing.Point(8, 68);
            this.lblRepititionCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRepititionCount.Name = "lblRepititionCount";
            this.lblRepititionCount.Size = new System.Drawing.Size(56, 20);
            this.lblRepititionCount.TabIndex = 2;
            this.lblRepititionCount.Text = "Repeat";
            // 
            // nudRepetitionCount
            // 
            this.nudRepetitionCount.Location = new System.Drawing.Point(76, 65);
            this.nudRepetitionCount.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudRepetitionCount.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudRepetitionCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRepetitionCount.Name = "nudRepetitionCount";
            this.nudRepetitionCount.Size = new System.Drawing.Size(65, 27);
            this.nudRepetitionCount.TabIndex = 1;
            this.nudRepetitionCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // chkUseCenteredGrid
            // 
            this.chkUseCenteredGrid.AutoSize = true;
            this.chkUseCenteredGrid.Location = new System.Drawing.Point(8, 105);
            this.chkUseCenteredGrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkUseCenteredGrid.Name = "chkUseCenteredGrid";
            this.chkUseCenteredGrid.Size = new System.Drawing.Size(151, 24);
            this.chkUseCenteredGrid.TabIndex = 0;
            this.chkUseCenteredGrid.Text = "Use Centered Grid";
            this.chkUseCenteredGrid.UseVisualStyleBackColor = true;
            // 
            // chkUseThresholds
            // 
            this.chkUseThresholds.AutoSize = true;
            this.chkUseThresholds.Location = new System.Drawing.Point(8, 29);
            this.chkUseThresholds.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkUseThresholds.Name = "chkUseThresholds";
            this.chkUseThresholds.Size = new System.Drawing.Size(130, 24);
            this.chkUseThresholds.TabIndex = 0;
            this.chkUseThresholds.Text = "Use Thresholds";
            this.chkUseThresholds.UseVisualStyleBackColor = true;
            // 
            // gbBorderPixelHandling
            // 
            gbBorderPixelHandling.AutoSize = true;
            gbBorderPixelHandling.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            gbBorderPixelHandling.Controls.Add(label7);
            gbBorderPixelHandling.Controls.Add(label6);
            gbBorderPixelHandling.Controls.Add(this.cmbVerticalBPH);
            gbBorderPixelHandling.Controls.Add(this.cmbHorizontalBPH);
            gbBorderPixelHandling.Controls.Add(label5);
            gbBorderPixelHandling.Controls.Add(label4);
            gbBorderPixelHandling.Dock = System.Windows.Forms.DockStyle.Top;
            gbBorderPixelHandling.Location = new System.Drawing.Point(0, 298);
            gbBorderPixelHandling.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            gbBorderPixelHandling.Name = "gbBorderPixelHandling";
            gbBorderPixelHandling.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            gbBorderPixelHandling.Size = new System.Drawing.Size(392, 129);
            gbBorderPixelHandling.TabIndex = 3;
            gbBorderPixelHandling.TabStop = false;
            gbBorderPixelHandling.Text = "Border pixel handling";
            // 
            // label7
            // 
            label7.Image = ((System.Drawing.Image)(resources.GetObject("label7.Image")));
            label7.Location = new System.Drawing.Point(345, 79);
            label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(21, 25);
            label7.TabIndex = 2;
            // 
            // label6
            // 
            label6.Image = ((System.Drawing.Image)(resources.GetObject("label6.Image")));
            label6.Location = new System.Drawing.Point(345, 34);
            label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(21, 25);
            label6.TabIndex = 2;
            // 
            // cmbVerticalBPH
            // 
            this.cmbVerticalBPH.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVerticalBPH.FormattingEnabled = true;
            this.cmbVerticalBPH.Location = new System.Drawing.Point(120, 71);
            this.cmbVerticalBPH.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbVerticalBPH.Name = "cmbVerticalBPH";
            this.cmbVerticalBPH.Size = new System.Drawing.Size(213, 28);
            this.cmbVerticalBPH.TabIndex = 1;
            // 
            // cmbHorizontalBPH
            // 
            this.cmbHorizontalBPH.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHorizontalBPH.FormattingEnabled = true;
            this.cmbHorizontalBPH.Location = new System.Drawing.Point(120, 29);
            this.cmbHorizontalBPH.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbHorizontalBPH.Name = "cmbHorizontalBPH";
            this.cmbHorizontalBPH.Size = new System.Drawing.Size(213, 28);
            this.cmbHorizontalBPH.TabIndex = 1;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(8, 75);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(69, 20);
            label5.TabIndex = 0;
            label5.Text = "Vertically";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(8, 34);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(90, 20);
            label4.TabIndex = 0;
            label4.Text = "Horizontally";
            // 
            // gbTargetResolution
            // 
            gbTargetResolution.AutoSize = true;
            gbTargetResolution.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            gbTargetResolution.Controls.Add(this.chkKeepAspect);
            gbTargetResolution.Controls.Add(label9);
            gbTargetResolution.Controls.Add(label8);
            gbTargetResolution.Controls.Add(this.nudWidth);
            gbTargetResolution.Controls.Add(this.nudHeight);
            gbTargetResolution.Controls.Add(label2);
            gbTargetResolution.Controls.Add(label1);
            gbTargetResolution.Dock = System.Windows.Forms.DockStyle.Top;
            gbTargetResolution.Location = new System.Drawing.Point(0, 172);
            gbTargetResolution.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            gbTargetResolution.Name = "gbTargetResolution";
            gbTargetResolution.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            gbTargetResolution.Size = new System.Drawing.Size(392, 126);
            gbTargetResolution.TabIndex = 3;
            gbTargetResolution.TabStop = false;
            gbTargetResolution.Text = "Target Resolution";
            // 
            // chkKeepAspect
            // 
            this.chkKeepAspect.AutoSize = true;
            this.chkKeepAspect.Location = new System.Drawing.Point(268, 49);
            this.chkKeepAspect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkKeepAspect.Name = "chkKeepAspect";
            this.chkKeepAspect.Size = new System.Drawing.Size(114, 24);
            this.chkKeepAspect.TabIndex = 3;
            this.chkKeepAspect.Text = "Keep Aspect";
            this.chkKeepAspect.UseVisualStyleBackColor = true;
            this.chkKeepAspect.CheckedChanged += new System.EventHandler(this.chkKeepAspect_CheckedChanged);
            // 
            // label9
            // 
            label9.Image = ((System.Drawing.Image)(resources.GetObject("label9.Image")));
            label9.Location = new System.Drawing.Point(176, 32);
            label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(21, 25);
            label9.TabIndex = 2;
            // 
            // label8
            // 
            label8.Image = ((System.Drawing.Image)(resources.GetObject("label8.Image")));
            label8.Location = new System.Drawing.Point(176, 72);
            label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(21, 25);
            label8.TabIndex = 2;
            // 
            // nudWidth
            // 
            this.nudWidth.Increment = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.nudWidth.Location = new System.Drawing.Point(76, 29);
            this.nudWidth.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudWidth.Maximum = new decimal(new int[] {
            32768,
            0,
            0,
            0});
            this.nudWidth.Name = "nudWidth";
            this.nudWidth.Size = new System.Drawing.Size(91, 27);
            this.nudWidth.TabIndex = 1;
            this.nudWidth.ValueChanged += new System.EventHandler(this.nudWidth_ValueChanged);
            // 
            // nudHeight
            // 
            this.nudHeight.Increment = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.nudHeight.Location = new System.Drawing.Point(76, 69);
            this.nudHeight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudHeight.Maximum = new decimal(new int[] {
            32768,
            0,
            0,
            0});
            this.nudHeight.Name = "nudHeight";
            this.nudHeight.Size = new System.Drawing.Size(91, 27);
            this.nudHeight.TabIndex = 1;
            this.nudHeight.ValueChanged += new System.EventHandler(this.nudHeight_ValueChanged);
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(8, 72);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(54, 20);
            label2.TabIndex = 0;
            label2.Text = "Height";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(8, 32);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(49, 20);
            label1.TabIndex = 0;
            label1.Text = "Width";
            // 
            // gbMethod
            // 
            gbMethod.AutoSize = true;
            gbMethod.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            gbMethod.Controls.Add(this.cmbResizeMethod);
            gbMethod.Dock = System.Windows.Forms.DockStyle.Top;
            gbMethod.Location = new System.Drawing.Point(0, 0);
            gbMethod.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            gbMethod.Name = "gbMethod";
            gbMethod.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            gbMethod.Size = new System.Drawing.Size(392, 87);
            gbMethod.TabIndex = 3;
            gbMethod.TabStop = false;
            gbMethod.Text = "Method";
            // 
            // cmbResizeMethod
            // 
            this.cmbResizeMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbResizeMethod.FormattingEnabled = true;
            this.cmbResizeMethod.Location = new System.Drawing.Point(8, 29);
            this.cmbResizeMethod.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbResizeMethod.Name = "cmbResizeMethod";
            this.cmbResizeMethod.Size = new System.Drawing.Size(375, 28);
            this.cmbResizeMethod.TabIndex = 0;
            this.cmbResizeMethod.SelectedValueChanged += new System.EventHandler(this.cbResizeMethod_SelectedValueChanged);
            // 
            // gbDescription
            // 
            gbDescription.AutoSize = true;
            gbDescription.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            gbDescription.Controls.Add(this.txtDescription);
            gbDescription.Dock = System.Windows.Forms.DockStyle.Top;
            gbDescription.Location = new System.Drawing.Point(0, 87);
            gbDescription.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            gbDescription.Name = "gbDescription";
            gbDescription.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            gbDescription.Size = new System.Drawing.Size(392, 85);
            gbDescription.TabIndex = 5;
            gbDescription.TabStop = false;
            gbDescription.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtDescription.Location = new System.Drawing.Point(4, 25);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(384, 55);
            this.txtDescription.TabIndex = 0;
            // 
            // msMain
            // 
            this.msMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.scriptToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.msMain.Size = new System.Drawing.Size(1185, 28);
            this.msMain.TabIndex = 0;
            this.msMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(143, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(143, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(143, 26);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(143, 26);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(140, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(143, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sourceImageToolStripMenuItem,
            this.targetImageToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // sourceImageToolStripMenuItem
            // 
            this.sourceImageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stretchToolStripMenuItem,
            this.centerToolStripMenuItem,
            this.zoomToolStripMenuItem});
            this.sourceImageToolStripMenuItem.Name = "sourceImageToolStripMenuItem";
            this.sourceImageToolStripMenuItem.Size = new System.Drawing.Size(183, 26);
            this.sourceImageToolStripMenuItem.Text = "Source Image";
            // 
            // stretchToolStripMenuItem
            // 
            this.stretchToolStripMenuItem.Name = "stretchToolStripMenuItem";
            this.stretchToolStripMenuItem.Size = new System.Drawing.Size(138, 26);
            this.stretchToolStripMenuItem.Text = "Stretch";
            this.stretchToolStripMenuItem.Click += new System.EventHandler(this.stretchToolStripMenuItem_Click);
            // 
            // centerToolStripMenuItem
            // 
            this.centerToolStripMenuItem.Name = "centerToolStripMenuItem";
            this.centerToolStripMenuItem.Size = new System.Drawing.Size(138, 26);
            this.centerToolStripMenuItem.Text = "Actual";
            this.centerToolStripMenuItem.Click += new System.EventHandler(this.centerToolStripMenuItem_Click);
            // 
            // zoomToolStripMenuItem
            // 
            this.zoomToolStripMenuItem.Name = "zoomToolStripMenuItem";
            this.zoomToolStripMenuItem.Size = new System.Drawing.Size(138, 26);
            this.zoomToolStripMenuItem.Text = "Fit";
            this.zoomToolStripMenuItem.Click += new System.EventHandler(this.zoomToolStripMenuItem_Click);
            // 
            // targetImageToolStripMenuItem
            // 
            this.targetImageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stretchToolStripMenuItem1,
            this.centerToolStripMenuItem1,
            this.zoomToolStripMenuItem1});
            this.targetImageToolStripMenuItem.Name = "targetImageToolStripMenuItem";
            this.targetImageToolStripMenuItem.Size = new System.Drawing.Size(183, 26);
            this.targetImageToolStripMenuItem.Text = "Target Image";
            // 
            // stretchToolStripMenuItem1
            // 
            this.stretchToolStripMenuItem1.Name = "stretchToolStripMenuItem1";
            this.stretchToolStripMenuItem1.Size = new System.Drawing.Size(138, 26);
            this.stretchToolStripMenuItem1.Text = "Stretch";
            this.stretchToolStripMenuItem1.Click += new System.EventHandler(this.stretchToolStripMenuItem1_Click);
            // 
            // centerToolStripMenuItem1
            // 
            this.centerToolStripMenuItem1.Name = "centerToolStripMenuItem1";
            this.centerToolStripMenuItem1.Size = new System.Drawing.Size(138, 26);
            this.centerToolStripMenuItem1.Text = "Actual";
            this.centerToolStripMenuItem1.Click += new System.EventHandler(this.centerToolStripMenuItem1_Click);
            // 
            // zoomToolStripMenuItem1
            // 
            this.zoomToolStripMenuItem1.Name = "zoomToolStripMenuItem1";
            this.zoomToolStripMenuItem1.Size = new System.Drawing.Size(138, 26);
            this.zoomToolStripMenuItem1.Text = "Fit";
            this.zoomToolStripMenuItem1.Click += new System.EventHandler(this.zoomToolStripMenuItem1_Click);
            // 
            // scriptToolStripMenuItem
            // 
            this.scriptToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem1,
            this.toolStripSeparator3,
            this.showToolStripMenuItem,
            this.executeToolStripMenuItem});
            this.scriptToolStripMenuItem.Name = "scriptToolStripMenuItem";
            this.scriptToolStripMenuItem.Size = new System.Drawing.Size(61, 24);
            this.scriptToolStripMenuItem.Text = "Script";
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(143, 26);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(143, 26);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(143, 26);
            this.saveToolStripMenuItem1.Text = "Save";
            this.saveToolStripMenuItem1.Click += new System.EventHandler(this.saveToolStripMenuItem1_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(140, 6);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(143, 26);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // executeToolStripMenuItem
            // 
            this.executeToolStripMenuItem.Name = "executeToolStripMenuItem";
            this.executeToolStripMenuItem.Size = new System.Drawing.Size(143, 26);
            this.executeToolStripMenuItem.Text = "Execute";
            this.executeToolStripMenuItem.Click += new System.EventHandler(this.executeToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wikiToolStripMenuItem,
            this.toolStripSeparator2,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // wikiToolStripMenuItem
            // 
            this.wikiToolStripMenuItem.Name = "wikiToolStripMenuItem";
            this.wikiToolStripMenuItem.Size = new System.Drawing.Size(133, 26);
            this.wikiToolStripMenuItem.Text = "Wiki";
            this.wikiToolStripMenuItem.Click += new System.EventHandler(this.wikiToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(130, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(133, 26);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // tlpMainLayout
            // 
            this.tlpMainLayout.ColumnCount = 3;
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMainLayout.Controls.Add(gbSourceImage, 0, 0);
            this.tlpMainLayout.Controls.Add(gbTargetImage, 2, 0);
            this.tlpMainLayout.Controls.Add(this.pnMiddle, 1, 0);
            this.tlpMainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainLayout.Location = new System.Drawing.Point(0, 28);
            this.tlpMainLayout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tlpMainLayout.Name = "tlpMainLayout";
            this.tlpMainLayout.RowCount = 1;
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainLayout.Size = new System.Drawing.Size(1185, 876);
            this.tlpMainLayout.TabIndex = 2;
            // 
            // pnMiddle
            // 
            this.pnMiddle.Controls.Add(this.gbKernelFunction);
            this.pnMiddle.Controls.Add(this.gbActions);
            this.pnMiddle.Controls.Add(gbAdvanced);
            this.pnMiddle.Controls.Add(gbBorderPixelHandling);
            this.pnMiddle.Controls.Add(gbTargetResolution);
            this.pnMiddle.Controls.Add(gbDescription);
            this.pnMiddle.Controls.Add(gbMethod);
            this.pnMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnMiddle.Location = new System.Drawing.Point(396, 5);
            this.pnMiddle.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnMiddle.Name = "pnMiddle";
            this.pnMiddle.Size = new System.Drawing.Size(392, 866);
            this.pnMiddle.TabIndex = 2;
            // 
            // gbKernelFunction
            // 
            this.gbKernelFunction.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbKernelFunction.Controls.Add(this.chtKernel);
            this.gbKernelFunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbKernelFunction.Location = new System.Drawing.Point(0, 699);
            this.gbKernelFunction.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbKernelFunction.Name = "gbKernelFunction";
            this.gbKernelFunction.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbKernelFunction.Size = new System.Drawing.Size(392, 167);
            this.gbKernelFunction.TabIndex = 6;
            this.gbKernelFunction.TabStop = false;
            this.gbKernelFunction.Text = "Kernel";
            // 
            // chtKernel
            // 
            this.chtKernel.BackColor = System.Drawing.SystemColors.Control;
            chartArea1.Name = "chaChart";
            this.chtKernel.ChartAreas.Add(chartArea1);
            this.chtKernel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chtKernel.Location = new System.Drawing.Point(4, 25);
            this.chtKernel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chtKernel.Name = "chtKernel";
            series1.ChartArea = "chaChart";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area;
            series1.Name = "dsKernelData";
            this.chtKernel.Series.Add(series1);
            this.chtKernel.Size = new System.Drawing.Size(384, 137);
            this.chtKernel.TabIndex = 0;
            this.chtKernel.Text = "chart1";
            // 
            // gbActions
            // 
            this.gbActions.AutoSize = true;
            this.gbActions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbActions.Controls.Add(flpActions);
            this.gbActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbActions.Location = new System.Drawing.Point(0, 624);
            this.gbActions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbActions.Name = "gbActions";
            this.gbActions.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbActions.Size = new System.Drawing.Size(392, 75);
            this.gbActions.TabIndex = 4;
            this.gbActions.TabStop = false;
            this.gbActions.Text = "Actions";
            // 
            // ofdOpenFile
            // 
            this.ofdOpenFile.FileName = "InputImage";
            this.ofdOpenFile.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif";
            this.ofdOpenFile.RestoreDirectory = true;
            this.ofdOpenFile.Title = "Select Image to resize";
            // 
            // sfdSave
            // 
            this.sfdSave.DefaultExt = "png";
            this.sfdSave.FileName = "OutputImage";
            this.sfdSave.Filter = "Portable Network Graphics|*.png|JPEG Files|*.jpg;*.jpeg|Windows Bitmap|*.bmp|Grap" +
    "hics Interchange Format|*.gif|Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif";
            this.sfdSave.RestoreDirectory = true;
            this.sfdSave.Title = "Enter filename";
            // 
            // ofdOpenScript
            // 
            this.ofdOpenScript.DefaultExt = "irs";
            this.ofdOpenScript.FileName = "InputScript";
            this.ofdOpenScript.Filter = "Image Resizer Script Files|*.irs|All files|*.*";
            this.ofdOpenScript.RestoreDirectory = true;
            this.ofdOpenScript.Title = "Select script to load";
            // 
            // sfdSaveScript
            // 
            this.sfdSaveScript.DefaultExt = "irs";
            this.sfdSaveScript.FileName = "OutputScript";
            this.sfdSaveScript.Filter = "Image Resizer Script Files|*.irs|All files|*.*";
            this.sfdSaveScript.RestoreDirectory = true;
            this.sfdSaveScript.Title = "Enter filename";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1185, 926);
            this.Controls.Add(this.tlpMainLayout);
            this.Controls.Add(ssBottom);
            this.Controls.Add(this.msMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMain;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(794, 898);
            this.Name = "MainForm";
            this.Text = "ImageFilters.UI";
            ssBottom.ResumeLayout(false);
            ssBottom.PerformLayout();
            gbSourceImage.ResumeLayout(false);
            gbTargetImage.ResumeLayout(false);
            flpActions.ResumeLayout(false);
            gbAdvanced.ResumeLayout(false);
            gbAdvanced.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRepetitionCount)).EndInit();
            gbBorderPixelHandling.ResumeLayout(false);
            gbBorderPixelHandling.PerformLayout();
            gbTargetResolution.ResumeLayout(false);
            gbTargetResolution.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).EndInit();
            gbMethod.ResumeLayout(false);
            gbDescription.ResumeLayout(false);
            gbDescription.PerformLayout();
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.tlpMainLayout.ResumeLayout(false);
            this.pnMiddle.ResumeLayout(false);
            this.pnMiddle.PerformLayout();
            this.gbKernelFunction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chtKernel)).EndInit();
            this.gbActions.ResumeLayout(false);
            this.gbActions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private System.Windows.Forms.Panel pnMiddle;
    private System.Windows.Forms.ComboBox cmbResizeMethod;
    private System.Windows.Forms.NumericUpDown nudWidth;
    private System.Windows.Forms.NumericUpDown nudHeight;
    private System.Windows.Forms.ComboBox cmbVerticalBPH;
    private System.Windows.Forms.ComboBox cmbHorizontalBPH;
    private System.Windows.Forms.Button butResize;
    private System.Windows.Forms.Button butSwitch;
    private System.Windows.Forms.Button butRepeat;
    private System.Windows.Forms.GroupBox gbActions;
    private System.Windows.Forms.CheckBox chkUseThresholds;
    private UserControls.ImageWithDetails iwhTargetImage;
    private UserControls.ImageWithDetails iwhSourceImage;
    private System.Windows.Forms.ToolStripStatusLabel tssBusy;
    private System.Windows.Forms.MenuStrip msMain;
    private System.Windows.Forms.TableLayoutPanel tlpMainLayout;
    private System.Windows.Forms.OpenFileDialog ofdOpenFile;
    private System.Windows.Forms.SaveFileDialog sfdSave;
    private System.Windows.Forms.CheckBox chkUseCenteredGrid;
    private System.Windows.Forms.ToolStripStatusLabel tssBenchmark;
    private System.Windows.Forms.TextBox txtDescription;
    private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem sourceImageToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem stretchToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem centerToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem zoomToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem targetImageToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem stretchToolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem centerToolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem zoomToolStripMenuItem1;
    private System.Windows.Forms.GroupBox gbKernelFunction;
    private System.Windows.Forms.DataVisualization.Charting.Chart chtKernel;
    private System.Windows.Forms.Label lblRepititionCount;
    private System.Windows.Forms.NumericUpDown nudRepetitionCount;
    private System.Windows.Forms.NumericUpDown nudRadius;
    private System.Windows.Forms.Label lblRadius;
    private System.Windows.Forms.CheckBox chkKeepAspect;
    private System.Windows.Forms.ToolStripMenuItem scriptToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem wikiToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripMenuItem executeToolStripMenuItem;
    private System.Windows.Forms.OpenFileDialog ofdOpenScript;
    private System.Windows.Forms.SaveFileDialog sfdSaveScript;

  }
}