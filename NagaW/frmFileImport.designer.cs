namespace NagaW
{
    partial class frmFileImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFileImport));
            this.btnImport = new System.Windows.Forms.Button();
            this.lbxApertureList = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnApDn = new System.Windows.Forms.Button();
            this.btnApUp = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lbxFeaturesList = new System.Windows.Forms.ListBox();
            this.rtbFeatureDetail = new System.Windows.Forms.RichTextBox();
            this.btnFtUp = new System.Windows.Forms.Button();
            this.btnFtDn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnArc = new System.Windows.Forms.Button();
            this.btnLine = new System.Windows.Forms.Button();
            this.cbxSelect = new System.Windows.Forms.ComboBox();
            this.btnNone = new System.Windows.Forms.Button();
            this.btnDot = new System.Windows.Forms.Button();
            this.btnOriginXY = new System.Windows.Forms.Button();
            this.btnRef1 = new System.Windows.Forms.Button();
            this.btnRef2 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbxOptimizeAll = new System.Windows.Forms.CheckBox();
            this.btnOptimize = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.tsbtnZoomFit = new System.Windows.Forms.ToolStripButton();
            this.tsbtnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tscmbxColor = new System.Windows.Forms.ToolStripComboBox();
            this.pnlPicture = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(459, 5);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 0;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // lbxApertureList
            // 
            this.lbxApertureList.FormattingEnabled = true;
            this.lbxApertureList.ItemHeight = 14;
            this.lbxApertureList.Location = new System.Drawing.Point(6, 62);
            this.lbxApertureList.Name = "lbxApertureList";
            this.lbxApertureList.Size = new System.Drawing.Size(159, 508);
            this.lbxApertureList.TabIndex = 3;
            this.lbxApertureList.Click += new System.EventHandler(this.lbxApertureList_Click);
            this.lbxApertureList.SelectedIndexChanged += new System.EventHandler(this.lbxApertureList_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.AutoSize = true;
            this.groupBox3.Controls.Add(this.btnApDn);
            this.groupBox3.Controls.Add(this.lbxApertureList);
            this.groupBox3.Controls.Add(this.btnApUp);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(171, 591);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Aperture List";
            // 
            // btnApDn
            // 
            this.btnApDn.Location = new System.Drawing.Point(90, 21);
            this.btnApDn.Name = "btnApDn";
            this.btnApDn.Size = new System.Drawing.Size(75, 23);
            this.btnApDn.TabIndex = 21;
            this.btnApDn.Text = "Down";
            this.btnApDn.UseVisualStyleBackColor = true;
            this.btnApDn.Click += new System.EventHandler(this.btnApDn_Click);
            // 
            // btnApUp
            // 
            this.btnApUp.Location = new System.Drawing.Point(9, 21);
            this.btnApUp.Name = "btnApUp";
            this.btnApUp.Size = new System.Drawing.Size(75, 23);
            this.btnApUp.TabIndex = 20;
            this.btnApUp.Text = "Up";
            this.btnApUp.UseVisualStyleBackColor = true;
            this.btnApUp.Click += new System.EventHandler(this.btnApUp_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(0, 5);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 15;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(81, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(1181, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 17;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(1262, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(378, 5);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 19;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lbxFeaturesList
            // 
            this.lbxFeaturesList.FormattingEnabled = true;
            this.lbxFeaturesList.ItemHeight = 14;
            this.lbxFeaturesList.Location = new System.Drawing.Point(6, 134);
            this.lbxFeaturesList.Name = "lbxFeaturesList";
            this.lbxFeaturesList.Size = new System.Drawing.Size(214, 382);
            this.lbxFeaturesList.TabIndex = 4;
            this.lbxFeaturesList.Click += new System.EventHandler(this.lblFeatures_Click);
            // 
            // rtbFeatureDetail
            // 
            this.rtbFeatureDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbFeatureDetail.Location = new System.Drawing.Point(6, 21);
            this.rtbFeatureDetail.Name = "rtbFeatureDetail";
            this.rtbFeatureDetail.Size = new System.Drawing.Size(357, 71);
            this.rtbFeatureDetail.TabIndex = 12;
            this.rtbFeatureDetail.Text = "";
            // 
            // btnFtUp
            // 
            this.btnFtUp.Location = new System.Drawing.Point(64, 105);
            this.btnFtUp.Name = "btnFtUp";
            this.btnFtUp.Size = new System.Drawing.Size(75, 23);
            this.btnFtUp.TabIndex = 22;
            this.btnFtUp.Text = "Up";
            this.btnFtUp.UseVisualStyleBackColor = true;
            this.btnFtUp.Click += new System.EventHandler(this.btnFtUp_Click);
            // 
            // btnFtDn
            // 
            this.btnFtDn.Location = new System.Drawing.Point(145, 105);
            this.btnFtDn.Name = "btnFtDn";
            this.btnFtDn.Size = new System.Drawing.Size(75, 23);
            this.btnFtDn.TabIndex = 23;
            this.btnFtDn.Text = "Down";
            this.btnFtDn.UseVisualStyleBackColor = true;
            this.btnFtDn.Click += new System.EventHandler(this.btnFtDn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.btnArc);
            this.groupBox1.Controls.Add(this.btnLine);
            this.groupBox1.Controls.Add(this.cbxSelect);
            this.groupBox1.Controls.Add(this.btnNone);
            this.groupBox1.Controls.Add(this.btnDot);
            this.groupBox1.Controls.Add(this.btnOriginXY);
            this.groupBox1.Controls.Add(this.btnRef1);
            this.groupBox1.Controls.Add(this.btnRef2);
            this.groupBox1.Location = new System.Drawing.Point(226, 105);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(137, 310);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Disp Type";
            // 
            // btnArc
            // 
            this.btnArc.Location = new System.Drawing.Point(30, 262);
            this.btnArc.Name = "btnArc";
            this.btnArc.Size = new System.Drawing.Size(75, 23);
            this.btnArc.TabIndex = 27;
            this.btnArc.Text = "Arc";
            this.btnArc.UseVisualStyleBackColor = true;
            this.btnArc.Click += new System.EventHandler(this.btnArc_Click);
            // 
            // btnLine
            // 
            this.btnLine.Location = new System.Drawing.Point(30, 233);
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(75, 23);
            this.btnLine.TabIndex = 26;
            this.btnLine.Text = "Line";
            this.btnLine.UseVisualStyleBackColor = true;
            this.btnLine.Click += new System.EventHandler(this.btnLine_Click);
            // 
            // cbxSelect
            // 
            this.cbxSelect.FormattingEnabled = true;
            this.cbxSelect.Location = new System.Drawing.Point(30, 147);
            this.cbxSelect.Name = "cbxSelect";
            this.cbxSelect.Size = new System.Drawing.Size(75, 22);
            this.cbxSelect.TabIndex = 24;
            // 
            // btnNone
            // 
            this.btnNone.Location = new System.Drawing.Point(30, 175);
            this.btnNone.Name = "btnNone";
            this.btnNone.Size = new System.Drawing.Size(75, 23);
            this.btnNone.TabIndex = 23;
            this.btnNone.Text = "None";
            this.btnNone.UseVisualStyleBackColor = true;
            this.btnNone.Click += new System.EventHandler(this.btnNone_Click);
            // 
            // btnDot
            // 
            this.btnDot.Location = new System.Drawing.Point(30, 204);
            this.btnDot.Name = "btnDot";
            this.btnDot.Size = new System.Drawing.Size(75, 23);
            this.btnDot.TabIndex = 20;
            this.btnDot.Text = "Dot";
            this.btnDot.UseVisualStyleBackColor = true;
            this.btnDot.Click += new System.EventHandler(this.btnDot_Click);
            // 
            // btnOriginXY
            // 
            this.btnOriginXY.Location = new System.Drawing.Point(30, 29);
            this.btnOriginXY.Name = "btnOriginXY";
            this.btnOriginXY.Size = new System.Drawing.Size(75, 23);
            this.btnOriginXY.TabIndex = 17;
            this.btnOriginXY.Text = "OriginXY";
            this.btnOriginXY.UseVisualStyleBackColor = true;
            this.btnOriginXY.Click += new System.EventHandler(this.btnOriginXY_Click);
            // 
            // btnRef1
            // 
            this.btnRef1.Location = new System.Drawing.Point(30, 69);
            this.btnRef1.Name = "btnRef1";
            this.btnRef1.Size = new System.Drawing.Size(75, 23);
            this.btnRef1.TabIndex = 18;
            this.btnRef1.Text = "Ref1";
            this.btnRef1.UseVisualStyleBackColor = true;
            this.btnRef1.Click += new System.EventHandler(this.btnRef1_Click);
            // 
            // btnRef2
            // 
            this.btnRef2.Location = new System.Drawing.Point(30, 98);
            this.btnRef2.Name = "btnRef2";
            this.btnRef2.Size = new System.Drawing.Size(75, 23);
            this.btnRef2.TabIndex = 19;
            this.btnRef2.Text = "Ref2";
            this.btnRef2.UseVisualStyleBackColor = true;
            this.btnRef2.Click += new System.EventHandler(this.btnRef2_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.btnFtDn);
            this.groupBox2.Controls.Add(this.btnFtUp);
            this.groupBox2.Controls.Add(this.rtbFeatureDetail);
            this.groupBox2.Controls.Add(this.lbxFeaturesList);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(171, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(369, 591);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Feature List";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbxOptimizeAll);
            this.groupBox4.Controls.Add(this.btnOptimize);
            this.groupBox4.Location = new System.Drawing.Point(226, 421);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(137, 149);
            this.groupBox4.TabIndex = 24;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Tools";
            // 
            // cbxOptimizeAll
            // 
            this.cbxOptimizeAll.AutoSize = true;
            this.cbxOptimizeAll.Location = new System.Drawing.Point(6, 74);
            this.cbxOptimizeAll.Name = "cbxOptimizeAll";
            this.cbxOptimizeAll.Size = new System.Drawing.Size(126, 18);
            this.cbxOptimizeAll.TabIndex = 27;
            this.cbxOptimizeAll.Text = "Optimize Aperture";
            this.cbxOptimizeAll.UseVisualStyleBackColor = true;
            this.cbxOptimizeAll.CheckedChanged += new System.EventHandler(this.cbxOptimizeAll_CheckedChanged);
            // 
            // btnOptimize
            // 
            this.btnOptimize.Location = new System.Drawing.Point(30, 35);
            this.btnOptimize.Name = "btnOptimize";
            this.btnOptimize.Size = new System.Drawing.Size(75, 23);
            this.btnOptimize.TabIndex = 26;
            this.btnOptimize.Text = "Optimize";
            this.btnOptimize.UseVisualStyleBackColor = true;
            this.btnOptimize.Click += new System.EventHandler(this.btnSort_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Controls.Add(this.pnlPicture);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(5, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1343, 591);
            this.panel1.TabIndex = 20;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnZoomOut,
            this.tsbtnZoomFit,
            this.tsbtnZoomIn,
            this.toolStripSeparator1,
            this.tscmbxColor});
            this.toolStrip1.Location = new System.Drawing.Point(540, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(803, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtnZoomOut
            // 
            this.tsbtnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnZoomOut.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbtnZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnZoomOut.Name = "tsbtnZoomOut";
            this.tsbtnZoomOut.Size = new System.Drawing.Size(23, 22);
            this.tsbtnZoomOut.Text = "Z-";
            this.tsbtnZoomOut.Click += new System.EventHandler(this.tsbtnZoomOut_Click);
            // 
            // tsbtnZoomFit
            // 
            this.tsbtnZoomFit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnZoomFit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbtnZoomFit.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnZoomFit.Image")));
            this.tsbtnZoomFit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnZoomFit.Name = "tsbtnZoomFit";
            this.tsbtnZoomFit.Size = new System.Drawing.Size(24, 22);
            this.tsbtnZoomFit.Text = "ZF";
            this.tsbtnZoomFit.Click += new System.EventHandler(this.tsbtnZoomFit_Click);
            // 
            // tsbtnZoomIn
            // 
            this.tsbtnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnZoomIn.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbtnZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnZoomIn.Image")));
            this.tsbtnZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnZoomIn.Name = "tsbtnZoomIn";
            this.tsbtnZoomIn.Size = new System.Drawing.Size(26, 22);
            this.tsbtnZoomIn.Text = "Z+";
            this.tsbtnZoomIn.Click += new System.EventHandler(this.tsbtnZoomIn_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tscmbxColor
            // 
            this.tscmbxColor.ForeColor = System.Drawing.Color.Navy;
            this.tscmbxColor.Name = "tscmbxColor";
            this.tscmbxColor.Size = new System.Drawing.Size(131, 25);
            // 
            // pnlPicture
            // 
            this.pnlPicture.AutoScroll = true;
            this.pnlPicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPicture.Location = new System.Drawing.Point(540, 0);
            this.pnlPicture.Name = "pnlPicture";
            this.pnlPicture.Size = new System.Drawing.Size(803, 591);
            this.pnlPicture.TabIndex = 15;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnClear);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.btnLoad);
            this.panel2.Controls.Add(this.btnImport);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(5, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1343, 34);
            this.panel2.TabIndex = 21;
            // 
            // frmFileImport
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1353, 649);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmFileImport";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Naga Import";
            this.Load += new System.EventHandler(this.frmFileImport_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.ListBox lbxApertureList;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnApDn;
        private System.Windows.Forms.Button btnApUp;
        private System.Windows.Forms.ListBox lbxFeaturesList;
        private System.Windows.Forms.RichTextBox rtbFeatureDetail;
        private System.Windows.Forms.Button btnFtUp;
        private System.Windows.Forms.Button btnFtDn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbxSelect;
        private System.Windows.Forms.Button btnNone;
        private System.Windows.Forms.Button btnDot;
        private System.Windows.Forms.Button btnOriginXY;
        private System.Windows.Forms.Button btnRef1;
        private System.Windows.Forms.Button btnRef2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnLine;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnOptimize;
        private System.Windows.Forms.CheckBox cbxOptimizeAll;
        private System.Windows.Forms.Button btnArc;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlPicture;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtnZoomOut;
        private System.Windows.Forms.ToolStripButton tsbtnZoomFit;
        private System.Windows.Forms.ToolStripButton tsbtnZoomIn;
        private System.Windows.Forms.ToolStripComboBox tscmbxColor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Panel panel2;
    }
}

