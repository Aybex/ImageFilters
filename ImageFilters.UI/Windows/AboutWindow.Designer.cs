namespace ImageFilters.GUI.Windows {
  partial class AboutWindow {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutWindow));
            this.butOK = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.llaHomepage = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // butOK
            // 
            this.butOK.Location = new System.Drawing.Point(175, 115);
            this.butOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(100, 35);
            this.butOK.TabIndex = 0;
            this.butOK.Text = "OK";
            this.butOK.UseVisualStyleBackColor = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.Location = new System.Drawing.Point(16, 14);
            this.lblInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(437, 35);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "This is {appname} v{version}, {copyright}";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // llaHomepage
            // 
            this.llaHomepage.AutoSize = true;
            this.llaHomepage.Location = new System.Drawing.Point(184, 68);
            this.llaHomepage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.llaHomepage.Name = "llaHomepage";
            this.llaHomepage.Size = new System.Drawing.Size(91, 20);
            this.llaHomepage.TabIndex = 2;
            this.llaHomepage.TabStop = true;
            this.llaHomepage.Text = "Project Page";
            this.llaHomepage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llaHomepage_LinkClicked);
            // 
            // AboutWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 177);
            this.Controls.Add(this.llaHomepage);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.butOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = global::ImageFilters.GUI.Resources.Resources.Icon;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "AboutWindow";
            this.Text = "About";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button butOK;
    private System.Windows.Forms.Label lblInfo;
    private System.Windows.Forms.LinkLabel llaHomepage;
  }
}