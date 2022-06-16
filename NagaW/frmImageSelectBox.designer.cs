namespace NagaW
{
    partial class frmImageSelectBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImageSelectBox));
            this.lbl_Instruction = new System.Windows.Forms.Label();
            this.imgboxEmgu = new Emgu.CV.UI.ImageBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tscbxImage = new System.Windows.Forms.ToolStripComboBox();
            this.tsbtnUpdate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnThreshold = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbZoomM = new System.Windows.Forms.ToolStripButton();
            this.tsbZoomF = new System.Windows.Forms.ToolStripButton();
            this.tsbZoomP = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbResetROI = new System.Windows.Forms.ToolStripButton();
            this.tscbxROI = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnCancel = new System.Windows.Forms.ToolStripButton();
            this.tsbtnOK = new System.Windows.Forms.ToolStripButton();
            this.tsbtnLoadImage = new System.Windows.Forms.ToolStripButton();
            this.tsbtnLearnImage = new System.Windows.Forms.ToolStripButton();
            this.tsbtnMatchImage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tbarThreshold = new System.Windows.Forms.TrackBar();
            this.pnlInstruction = new System.Windows.Forms.Panel();
            this.tslblScore = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.imgboxEmgu)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbarThreshold)).BeginInit();
            this.pnlInstruction.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Instruction
            // 
            this.lbl_Instruction.AutoSize = true;
            this.lbl_Instruction.Location = new System.Drawing.Point(3, 3);
            this.lbl_Instruction.Name = "lbl_Instruction";
            this.lbl_Instruction.Size = new System.Drawing.Size(97, 18);
            this.lbl_Instruction.TabIndex = 4;
            this.lbl_Instruction.Text = "lbl_Instruction";
            // 
            // imgboxEmgu
            // 
            this.imgboxEmgu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imgboxEmgu.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.imgboxEmgu.Location = new System.Drawing.Point(3, 61);
            this.imgboxEmgu.Name = "imgboxEmgu";
            this.imgboxEmgu.Size = new System.Drawing.Size(801, 458);
            this.imgboxEmgu.TabIndex = 2;
            this.imgboxEmgu.TabStop = false;
            this.imgboxEmgu.SizeChanged += new System.EventHandler(this.imgboxEmgu_SizeChanged);
            this.imgboxEmgu.Paint += new System.Windows.Forms.PaintEventHandler(this.imgboxEmgu_Paint);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslblScore});
            this.statusStrip1.Location = new System.Drawing.Point(3, 519);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(801, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tscbxImage,
            this.tsbtnUpdate,
            this.toolStripSeparator4,
            this.tsbtnThreshold,
            this.toolStripSeparator1,
            this.tsbZoomM,
            this.tsbZoomF,
            this.tsbZoomP,
            this.toolStripSeparator2,
            this.tsbResetROI,
            this.tscbxROI,
            this.toolStripSeparator3,
            this.tsbtnCancel,
            this.tsbtnOK,
            this.tsbtnLoadImage,
            this.tsbtnLearnImage,
            this.tsbtnMatchImage,
            this.toolStripSeparator5});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(801, 33);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(51, 30);
            this.toolStripLabel1.Text = "Image";
            // 
            // tscbxImage
            // 
            this.tscbxImage.Name = "tscbxImage";
            this.tscbxImage.Size = new System.Drawing.Size(80, 33);
            this.tscbxImage.Text = "Registered";
            this.tscbxImage.SelectedIndexChanged += new System.EventHandler(this.tscbxImage_SelectedIndexChanged);
            // 
            // tsbtnUpdate
            // 
            this.tsbtnUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnUpdate.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnUpdate.Image")));
            this.tsbtnUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnUpdate.Name = "tsbtnUpdate";
            this.tsbtnUpdate.Size = new System.Drawing.Size(62, 30);
            this.tsbtnUpdate.Text = "Update";
            this.tsbtnUpdate.Click += new System.EventHandler(this.tsbtnUpdate_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 33);
            // 
            // tsbtnThreshold
            // 
            this.tsbtnThreshold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnThreshold.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnThreshold.Image")));
            this.tsbtnThreshold.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnThreshold.Name = "tsbtnThreshold";
            this.tsbtnThreshold.Size = new System.Drawing.Size(78, 30);
            this.tsbtnThreshold.Text = "Threshold";
            this.tsbtnThreshold.Click += new System.EventHandler(this.tsbtnThreshold_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 33);
            // 
            // tsbZoomM
            // 
            this.tsbZoomM.AutoSize = false;
            this.tsbZoomM.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbZoomM.Image = ((System.Drawing.Image)(resources.GetObject("tsbZoomM.Image")));
            this.tsbZoomM.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZoomM.Name = "tsbZoomM";
            this.tsbZoomM.Size = new System.Drawing.Size(35, 22);
            this.tsbZoomM.Text = "Z-";
            this.tsbZoomM.Click += new System.EventHandler(this.tsbZoomM_Click);
            // 
            // tsbZoomF
            // 
            this.tsbZoomF.AutoSize = false;
            this.tsbZoomF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbZoomF.Image = ((System.Drawing.Image)(resources.GetObject("tsbZoomF.Image")));
            this.tsbZoomF.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZoomF.Name = "tsbZoomF";
            this.tsbZoomF.Size = new System.Drawing.Size(35, 22);
            this.tsbZoomF.Text = "ZF";
            this.tsbZoomF.Click += new System.EventHandler(this.tsbZoomF_Click);
            // 
            // tsbZoomP
            // 
            this.tsbZoomP.AutoSize = false;
            this.tsbZoomP.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbZoomP.Image = ((System.Drawing.Image)(resources.GetObject("tsbZoomP.Image")));
            this.tsbZoomP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZoomP.Name = "tsbZoomP";
            this.tsbZoomP.Size = new System.Drawing.Size(35, 22);
            this.tsbZoomP.Text = "Z+";
            this.tsbZoomP.Click += new System.EventHandler(this.tsbZoomP_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 33);
            // 
            // tsbResetROI
            // 
            this.tsbResetROI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbResetROI.Image = ((System.Drawing.Image)(resources.GetObject("tsbResetROI.Image")));
            this.tsbResetROI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbResetROI.Name = "tsbResetROI";
            this.tsbResetROI.Size = new System.Drawing.Size(77, 30);
            this.tsbResetROI.Text = "Reset ROI";
            this.tsbResetROI.Click += new System.EventHandler(this.tsbResetROI_Click);
            // 
            // tscbxROI
            // 
            this.tscbxROI.Name = "tscbxROI";
            this.tscbxROI.Size = new System.Drawing.Size(80, 33);
            this.tscbxROI.Text = "Pattern";
            this.tscbxROI.SelectedIndexChanged += new System.EventHandler(this.tscbxROI_SelectedIndexChanged);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 33);
            // 
            // tsbtnCancel
            // 
            this.tsbtnCancel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnCancel.AutoSize = false;
            this.tsbtnCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnCancel.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnCancel.Image")));
            this.tsbtnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnCancel.Name = "tsbtnCancel";
            this.tsbtnCancel.Size = new System.Drawing.Size(60, 30);
            this.tsbtnCancel.Text = "Cancel";
            this.tsbtnCancel.Click += new System.EventHandler(this.tsbCancel_Click);
            // 
            // tsbtnOK
            // 
            this.tsbtnOK.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnOK.AutoSize = false;
            this.tsbtnOK.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnOK.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnOK.Image")));
            this.tsbtnOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnOK.Name = "tsbtnOK";
            this.tsbtnOK.Size = new System.Drawing.Size(60, 30);
            this.tsbtnOK.Text = "OK";
            this.tsbtnOK.Click += new System.EventHandler(this.tsbOK_Click);
            // 
            // tsbtnLoadImage
            // 
            this.tsbtnLoadImage.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnLoadImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnLoadImage.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnLoadImage.Image")));
            this.tsbtnLoadImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnLoadImage.Name = "tsbtnLoadImage";
            this.tsbtnLoadImage.Size = new System.Drawing.Size(46, 30);
            this.tsbtnLoadImage.Text = "Load";
            this.tsbtnLoadImage.Click += new System.EventHandler(this.tsbtnLoadImage_Click);
            // 
            // tsbtnLearnImage
            // 
            this.tsbtnLearnImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnLearnImage.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnLearnImage.Image")));
            this.tsbtnLearnImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnLearnImage.Name = "tsbtnLearnImage";
            this.tsbtnLearnImage.Size = new System.Drawing.Size(49, 24);
            this.tsbtnLearnImage.Text = "Learn";
            this.tsbtnLearnImage.Click += new System.EventHandler(this.tsbtnLearnImage_Click);
            // 
            // tsbtnMatchImage
            // 
            this.tsbtnMatchImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnMatchImage.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnMatchImage.Image")));
            this.tsbtnMatchImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnMatchImage.Name = "tsbtnMatchImage";
            this.tsbtnMatchImage.Size = new System.Drawing.Size(54, 24);
            this.tsbtnMatchImage.Text = "Match";
            this.tsbtnMatchImage.Click += new System.EventHandler(this.tsbtnMatchImage_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 33);
            // 
            // tbarThreshold
            // 
            this.tbarThreshold.AutoSize = false;
            this.tbarThreshold.BackColor = System.Drawing.SystemColors.Control;
            this.tbarThreshold.Location = new System.Drawing.Point(3, 61);
            this.tbarThreshold.Maximum = 255;
            this.tbarThreshold.Minimum = -1;
            this.tbarThreshold.Name = "tbarThreshold";
            this.tbarThreshold.Size = new System.Drawing.Size(323, 30);
            this.tbarThreshold.TabIndex = 7;
            this.tbarThreshold.TickFrequency = 50;
            this.tbarThreshold.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.tbarThreshold.Value = 127;
            this.tbarThreshold.Scroll += new System.EventHandler(this.tbarThreshold_Scroll);
            // 
            // pnlInstruction
            // 
            this.pnlInstruction.AutoSize = true;
            this.pnlInstruction.Controls.Add(this.lbl_Instruction);
            this.pnlInstruction.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInstruction.Location = new System.Drawing.Point(3, 36);
            this.pnlInstruction.MinimumSize = new System.Drawing.Size(0, 25);
            this.pnlInstruction.Name = "pnlInstruction";
            this.pnlInstruction.Size = new System.Drawing.Size(801, 25);
            this.pnlInstruction.TabIndex = 10;
            // 
            // tslblScore
            // 
            this.tslblScore.Name = "tslblScore";
            this.tslblScore.Size = new System.Drawing.Size(0, 16);
            // 
            // frmImageSelectBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(807, 544);
            this.ControlBox = false;
            this.Controls.Add(this.tbarThreshold);
            this.Controls.Add(this.imgboxEmgu);
            this.Controls.Add(this.pnlInstruction);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.Name = "frmImageSelectBox";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "frmImageSelectBox";
            this.Load += new System.EventHandler(this.frmVisionSelectBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgboxEmgu)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbarThreshold)).EndInit();
            this.pnlInstruction.ResumeLayout(false);
            this.pnlInstruction.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbl_Instruction;
        private Emgu.CV.UI.ImageBox imgboxEmgu;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtnThreshold;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbResetROI;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbZoomM;
        private System.Windows.Forms.ToolStripButton tsbZoomF;
        private System.Windows.Forms.ToolStripButton tsbZoomP;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.TrackBar tbarThreshold;
        private System.Windows.Forms.ToolStripButton tsbtnCancel;
        private System.Windows.Forms.ToolStripButton tsbtnOK;
        private System.Windows.Forms.Panel pnlInstruction;
        private System.Windows.Forms.ToolStripButton tsbtnMatchImage;
        private System.Windows.Forms.ToolStripButton tsbtnLoadImage;
        private System.Windows.Forms.ToolStripComboBox tscbxImage;
        private System.Windows.Forms.ToolStripButton tsbtnLearnImage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox tscbxROI;
        private System.Windows.Forms.ToolStripButton tsbtnUpdate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripStatusLabel tslblScore;
    }
}