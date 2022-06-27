namespace NagaW
{
    partial class frmFlirCamera
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.liveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sppnFitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiReticle = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableTrigModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.sourceSwToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sourceHwToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlImage = new System.Windows.Forms.Panel();
            this.imgBoxEmgu = new Emgu.CV.UI.ImageBox();
            this.eventLog1 = new System.Diagnostics.EventLog();
            this.menuStrip1.SuspendLayout();
            this.pnlImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgBoxEmgu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem4,
            this.liveToolStripMenuItem,
            this.sTopToolStripMenuItem,
            this.tigToolStripMenuItem,
            this.toolStripMenuItem5,
            this.zoomOutToolStripMenuItem,
            this.sppnFitToolStripMenuItem,
            this.zoomInToolStripMenuItem,
            this.toolStripMenuItem6,
            this.tsmiReticle,
            this.toolsToolStripMenuItem,
            this.imageToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(718, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(31, 24);
            this.toolStripMenuItem1.Text = "1";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(27, 24);
            this.toolStripMenuItem4.Text = "|";
            // 
            // liveToolStripMenuItem
            // 
            this.liveToolStripMenuItem.Name = "liveToolStripMenuItem";
            this.liveToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.liveToolStripMenuItem.Text = "Live";
            this.liveToolStripMenuItem.Click += new System.EventHandler(this.liveToolStripMenuItem_Click);
            // 
            // sTopToolStripMenuItem
            // 
            this.sTopToolStripMenuItem.Name = "sTopToolStripMenuItem";
            this.sTopToolStripMenuItem.Size = new System.Drawing.Size(54, 24);
            this.sTopToolStripMenuItem.Text = "Stop";
            this.sTopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // tigToolStripMenuItem
            // 
            this.tigToolStripMenuItem.Name = "tigToolStripMenuItem";
            this.tigToolStripMenuItem.Size = new System.Drawing.Size(48, 24);
            this.tigToolStripMenuItem.Text = "Trig";
            this.tigToolStripMenuItem.Click += new System.EventHandler(this.trigToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(27, 24);
            this.toolStripMenuItem5.Text = "|";
            // 
            // zoomOutToolStripMenuItem
            // 
            this.zoomOutToolStripMenuItem.Name = "zoomOutToolStripMenuItem";
            this.zoomOutToolStripMenuItem.Size = new System.Drawing.Size(38, 24);
            this.zoomOutToolStripMenuItem.Text = "Z-";
            this.zoomOutToolStripMenuItem.Click += new System.EventHandler(this.zoomOutToolStripMenuItem_Click);
            // 
            // sppnFitToolStripMenuItem
            // 
            this.sppnFitToolStripMenuItem.Name = "sppnFitToolStripMenuItem";
            this.sppnFitToolStripMenuItem.Size = new System.Drawing.Size(39, 24);
            this.sppnFitToolStripMenuItem.Text = "ZF";
            this.sppnFitToolStripMenuItem.Click += new System.EventHandler(this.zoomFitToolStripMenuItem_Click);
            // 
            // zoomInToolStripMenuItem
            // 
            this.zoomInToolStripMenuItem.Name = "zoomInToolStripMenuItem";
            this.zoomInToolStripMenuItem.Size = new System.Drawing.Size(42, 24);
            this.zoomInToolStripMenuItem.Text = "Z+";
            this.zoomInToolStripMenuItem.Click += new System.EventHandler(this.zoomInToolStripMenuItem_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(27, 24);
            this.toolStripMenuItem6.Text = "|";
            // 
            // tsmiReticle
            // 
            this.tsmiReticle.Name = "tsmiReticle";
            this.tsmiReticle.Size = new System.Drawing.Size(68, 24);
            this.tsmiReticle.Text = "Reticle";
            this.tsmiReticle.Click += new System.EventHandler(this.tsmiReticle_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enableTrigModeToolStripMenuItem,
            this.toolStripComboBox1,
            this.toolStripMenuItem7,
            this.sourceSwToolStripMenuItem,
            this.sourceHwToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(70, 24);
            this.toolsToolStripMenuItem.Text = "Trigger";
            // 
            // enableTrigModeToolStripMenuItem
            // 
            this.enableTrigModeToolStripMenuItem.Checked = true;
            this.enableTrigModeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableTrigModeToolStripMenuItem.Name = "enableTrigModeToolStripMenuItem";
            this.enableTrigModeToolStripMenuItem.Size = new System.Drawing.Size(195, 26);
            this.enableTrigModeToolStripMenuItem.Text = "Enable";
            this.enableTrigModeToolStripMenuItem.Click += new System.EventHandler(this.enableTrigModeToolStripMenuItem_Click);
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripComboBox1.Items.AddRange(new object[] {
            "Source Sw",
            "Source Hw"});
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 28);
            this.toolStripComboBox1.Click += new System.EventHandler(this.toolStripComboBox1_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(192, 6);
            // 
            // sourceSwToolStripMenuItem
            // 
            this.sourceSwToolStripMenuItem.Name = "sourceSwToolStripMenuItem";
            this.sourceSwToolStripMenuItem.Size = new System.Drawing.Size(195, 26);
            this.sourceSwToolStripMenuItem.Text = "Source Sw";
            this.sourceSwToolStripMenuItem.Click += new System.EventHandler(this.sourceSwToolStripMenuItem_Click);
            // 
            // sourceHwToolStripMenuItem
            // 
            this.sourceHwToolStripMenuItem.Name = "sourceHwToolStripMenuItem";
            this.sourceHwToolStripMenuItem.Size = new System.Drawing.Size(195, 26);
            this.sourceHwToolStripMenuItem.Text = "Source Hw";
            this.sourceHwToolStripMenuItem.Click += new System.EventHandler(this.sourceHwToolStripMenuItem_Click);
            // 
            // imageToolStripMenuItem
            // 
            this.imageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            this.imageToolStripMenuItem.Size = new System.Drawing.Size(65, 24);
            this.imageToolStripMenuItem.Text = "Image";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // pnlImage
            // 
            this.pnlImage.AutoScroll = true;
            this.pnlImage.Controls.Add(this.imgBoxEmgu);
            this.pnlImage.Location = new System.Drawing.Point(12, 42);
            this.pnlImage.Name = "pnlImage";
            this.pnlImage.Size = new System.Drawing.Size(374, 300);
            this.pnlImage.TabIndex = 2;
            // 
            // imgBoxEmgu
            // 
            this.imgBoxEmgu.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.imgBoxEmgu.Location = new System.Drawing.Point(171, 184);
            this.imgBoxEmgu.Name = "imgBoxEmgu";
            this.imgBoxEmgu.Size = new System.Drawing.Size(117, 99);
            this.imgBoxEmgu.TabIndex = 3;
            this.imgBoxEmgu.TabStop = false;
            this.imgBoxEmgu.Paint += new System.Windows.Forms.PaintEventHandler(this.imgBoxEmgu_Paint);
            this.imgBoxEmgu.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imgBoxEmgu_MouseDown);
            // 
            // eventLog1
            // 
            this.eventLog1.SynchronizingObject = this;
            // 
            // frmFlirCamera
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(718, 500);
            this.ControlBox = false;
            this.Controls.Add(this.pnlImage);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFlirCamera";
            this.Text = "frmFlirCamera";
            this.Load += new System.EventHandler(this.frmFlirCamera_Load);
            this.ResizeEnd += new System.EventHandler(this.frmFlirCamera_ResizeEnd);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgBoxEmgu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Panel pnlImage;
        private Emgu.CV.UI.ImageBox imgBoxEmgu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem liveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem zoomOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sppnFitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem imageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableTrigModeToolStripMenuItem;
        private System.Diagnostics.EventLog eventLog1;
        private System.Windows.Forms.ToolStripMenuItem tsmiReticle;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem sourceSwToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sourceHwToolStripMenuItem;
    }
}