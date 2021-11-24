namespace NagaW
{
    partial class frmRecipeMap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRecipeMap));
            this.tsbtnZoomPlus = new System.Windows.Forms.GroupBox();
            this.lblBuffer = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblGotoUnitR = new System.Windows.Forms.Label();
            this.lblGotoUnitC = new System.Windows.Forms.Label();
            this.btnLayoutCRReset = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lblGotoClusterR = new System.Windows.Forms.Label();
            this.lblGotoClusterC = new System.Windows.Forms.Label();
            this.btnLayoutCRGoto = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnClearAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnZoomMinus = new System.Windows.Forms.ToolStripButton();
            this.tsbtnZoomFit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnGoto = new System.Windows.Forms.ToolStripButton();
            this.picBox = new System.Windows.Forms.PictureBox();
            this.ssBottom = new System.Windows.Forms.StatusStrip();
            this.tsslUnit = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tscbxViewMode = new System.Windows.Forms.ToolStripComboBox();
            this.tsbtnZoomPlus.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.ssBottom.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsbtnZoomPlus
            // 
            this.tsbtnZoomPlus.AutoSize = true;
            this.tsbtnZoomPlus.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tsbtnZoomPlus.Controls.Add(this.lblBuffer);
            this.tsbtnZoomPlus.Controls.Add(this.label12);
            this.tsbtnZoomPlus.Controls.Add(this.lblGotoUnitR);
            this.tsbtnZoomPlus.Controls.Add(this.lblGotoUnitC);
            this.tsbtnZoomPlus.Controls.Add(this.btnLayoutCRReset);
            this.tsbtnZoomPlus.Controls.Add(this.label4);
            this.tsbtnZoomPlus.Controls.Add(this.lblGotoClusterR);
            this.tsbtnZoomPlus.Controls.Add(this.lblGotoClusterC);
            this.tsbtnZoomPlus.Controls.Add(this.btnLayoutCRGoto);
            this.tsbtnZoomPlus.Dock = System.Windows.Forms.DockStyle.Top;
            this.tsbtnZoomPlus.Location = new System.Drawing.Point(0, 0);
            this.tsbtnZoomPlus.Name = "tsbtnZoomPlus";
            this.tsbtnZoomPlus.Size = new System.Drawing.Size(772, 93);
            this.tsbtnZoomPlus.TabIndex = 9;
            this.tsbtnZoomPlus.TabStop = false;
            this.tsbtnZoomPlus.Text = "Navigator";
            this.tsbtnZoomPlus.Visible = false;
            // 
            // lblBuffer
            // 
            this.lblBuffer.AutoSize = true;
            this.lblBuffer.Location = new System.Drawing.Point(6, 53);
            this.lblBuffer.Name = "lblBuffer";
            this.lblBuffer.Size = new System.Drawing.Size(44, 18);
            this.lblBuffer.TabIndex = 32;
            this.lblBuffer.Text = "label3";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(199, 25);
            this.label12.Margin = new System.Windows.Forms.Padding(3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(75, 25);
            this.label12.TabIndex = 24;
            this.label12.Text = "Unit CR";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // lblGotoUnitR
            // 
            this.lblGotoUnitR.BackColor = System.Drawing.Color.White;
            this.lblGotoUnitR.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblGotoUnitR.Location = new System.Drawing.Point(336, 25);
            this.lblGotoUnitR.Margin = new System.Windows.Forms.Padding(3);
            this.lblGotoUnitR.Name = "lblGotoUnitR";
            this.lblGotoUnitR.Size = new System.Drawing.Size(50, 25);
            this.lblGotoUnitR.TabIndex = 19;
            this.lblGotoUnitR.Text = "R";
            this.lblGotoUnitR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblGotoUnitR.Click += new System.EventHandler(this.lblGotoUnitR_Click);
            // 
            // lblGotoUnitC
            // 
            this.lblGotoUnitC.BackColor = System.Drawing.Color.White;
            this.lblGotoUnitC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblGotoUnitC.Location = new System.Drawing.Point(280, 25);
            this.lblGotoUnitC.Margin = new System.Windows.Forms.Padding(3);
            this.lblGotoUnitC.Name = "lblGotoUnitC";
            this.lblGotoUnitC.Size = new System.Drawing.Size(50, 25);
            this.lblGotoUnitC.TabIndex = 18;
            this.lblGotoUnitC.Text = "C";
            this.lblGotoUnitC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblGotoUnitC.Click += new System.EventHandler(this.lblGotoUnitC_Click);
            // 
            // btnLayoutCRReset
            // 
            this.btnLayoutCRReset.Location = new System.Drawing.Point(401, 25);
            this.btnLayoutCRReset.Name = "btnLayoutCRReset";
            this.btnLayoutCRReset.Size = new System.Drawing.Size(75, 25);
            this.btnLayoutCRReset.TabIndex = 17;
            this.btnLayoutCRReset.Text = "Reset";
            this.btnLayoutCRReset.UseVisualStyleBackColor = true;
            this.btnLayoutCRReset.Click += new System.EventHandler(this.btnLayoutCRReset_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 25);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 25);
            this.label4.TabIndex = 4;
            this.label4.Text = "Cluster CR";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblGotoClusterR
            // 
            this.lblGotoClusterR.BackColor = System.Drawing.Color.White;
            this.lblGotoClusterR.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblGotoClusterR.Location = new System.Drawing.Point(143, 25);
            this.lblGotoClusterR.Margin = new System.Windows.Forms.Padding(3);
            this.lblGotoClusterR.Name = "lblGotoClusterR";
            this.lblGotoClusterR.Size = new System.Drawing.Size(50, 25);
            this.lblGotoClusterR.TabIndex = 6;
            this.lblGotoClusterR.Text = "R";
            this.lblGotoClusterR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblGotoClusterR.Click += new System.EventHandler(this.lblGotoClusterR_Click);
            // 
            // lblGotoClusterC
            // 
            this.lblGotoClusterC.BackColor = System.Drawing.Color.White;
            this.lblGotoClusterC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblGotoClusterC.Location = new System.Drawing.Point(87, 25);
            this.lblGotoClusterC.Margin = new System.Windows.Forms.Padding(3);
            this.lblGotoClusterC.Name = "lblGotoClusterC";
            this.lblGotoClusterC.Size = new System.Drawing.Size(50, 25);
            this.lblGotoClusterC.TabIndex = 5;
            this.lblGotoClusterC.Text = "C";
            this.lblGotoClusterC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblGotoClusterC.Click += new System.EventHandler(this.lblGotoClusterC_Click);
            // 
            // btnLayoutCRGoto
            // 
            this.btnLayoutCRGoto.Location = new System.Drawing.Point(482, 25);
            this.btnLayoutCRGoto.Name = "btnLayoutCRGoto";
            this.btnLayoutCRGoto.Size = new System.Drawing.Size(75, 25);
            this.btnLayoutCRGoto.TabIndex = 7;
            this.btnLayoutCRGoto.Text = "Goto";
            this.btnLayoutCRGoto.UseVisualStyleBackColor = true;
            this.btnLayoutCRGoto.Click += new System.EventHandler(this.btnLayoutCRGoto_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnClearAll,
            this.toolStripSeparator1,
            this.tsbtnZoomMinus,
            this.tsbtnZoomFit,
            this.toolStripButton2,
            this.toolStripSeparator2,
            this.tsbtnGoto,
            this.tscbxViewMode});
            this.toolStrip1.Location = new System.Drawing.Point(0, 93);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(772, 28);
            this.toolStrip1.TabIndex = 13;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtnClearAll
            // 
            this.tsbtnClearAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnClearAll.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnClearAll.Image")));
            this.tsbtnClearAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnClearAll.Name = "tsbtnClearAll";
            this.tsbtnClearAll.Size = new System.Drawing.Size(69, 25);
            this.tsbtnClearAll.Text = "Clear All";
            this.tsbtnClearAll.Click += new System.EventHandler(this.tsbtnClearAll_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
            // 
            // tsbtnZoomMinus
            // 
            this.tsbtnZoomMinus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnZoomMinus.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnZoomMinus.Image")));
            this.tsbtnZoomMinus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnZoomMinus.Name = "tsbtnZoomMinus";
            this.tsbtnZoomMinus.Size = new System.Drawing.Size(59, 25);
            this.tsbtnZoomMinus.Text = "Zoom-";
            this.tsbtnZoomMinus.Click += new System.EventHandler(this.tsbtnZoomMinus_Click);
            // 
            // tsbtnZoomFit
            // 
            this.tsbtnZoomFit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnZoomFit.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnZoomFit.Image")));
            this.tsbtnZoomFit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnZoomFit.Name = "tsbtnZoomFit";
            this.tsbtnZoomFit.Size = new System.Drawing.Size(69, 25);
            this.tsbtnZoomFit.Text = "ZoomFit";
            this.tsbtnZoomFit.Click += new System.EventHandler(this.tsbtnZoomFit_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(63, 25);
            this.toolStripButton2.Text = "Zoom+";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 28);
            // 
            // tsbtnGoto
            // 
            this.tsbtnGoto.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnGoto.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnGoto.Image")));
            this.tsbtnGoto.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnGoto.Name = "tsbtnGoto";
            this.tsbtnGoto.Size = new System.Drawing.Size(46, 25);
            this.tsbtnGoto.Text = "Goto";
            this.tsbtnGoto.Click += new System.EventHandler(this.tsbtnGoto_Click);
            // 
            // picBox
            // 
            this.picBox.Location = new System.Drawing.Point(3, 3);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(284, 314);
            this.picBox.TabIndex = 11;
            this.picBox.TabStop = false;
            this.picBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBox_MouseDown);
            this.picBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picBox_MouseMove);
            this.picBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBox_MouseUp);
            // 
            // ssBottom
            // 
            this.ssBottom.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ssBottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslUnit});
            this.ssBottom.Location = new System.Drawing.Point(0, 565);
            this.ssBottom.Name = "ssBottom";
            this.ssBottom.Size = new System.Drawing.Size(772, 26);
            this.ssBottom.TabIndex = 12;
            this.ssBottom.Text = "statusStrip1";
            // 
            // tsslUnit
            // 
            this.tsslUnit.Name = "tsslUnit";
            this.tsslUnit.Size = new System.Drawing.Size(57, 20);
            this.tsslUnit.Text = "tsslUnit";
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.picBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 121);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(772, 444);
            this.panel2.TabIndex = 12;
            // 
            // tscbxViewMode
            // 
            this.tscbxViewMode.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tscbxViewMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbxViewMode.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tscbxViewMode.Name = "tscbxViewMode";
            this.tscbxViewMode.Size = new System.Drawing.Size(121, 28);
            // 
            // frmRecipeMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 591);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.ssBottom);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tsbtnZoomPlus);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.Name = "frmRecipeMap";
            this.Text = "frmRecipeMap";
            this.Load += new System.EventHandler(this.frmRecipeMap_Load);
            this.tsbtnZoomPlus.ResumeLayout(false);
            this.tsbtnZoomPlus.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.ssBottom.ResumeLayout(false);
            this.ssBottom.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox tsbtnZoomPlus;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblGotoUnitR;
        private System.Windows.Forms.Label lblGotoUnitC;
        private System.Windows.Forms.Button btnLayoutCRReset;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblGotoClusterR;
        private System.Windows.Forms.Label lblGotoClusterC;
        private System.Windows.Forms.Button btnLayoutCRGoto;
        private System.Windows.Forms.Label lblBuffer;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtnClearAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbtnZoomMinus;
        private System.Windows.Forms.ToolStripButton tsbtnZoomFit;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbtnGoto;
        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.StatusStrip ssBottom;
        private System.Windows.Forms.ToolStripStatusLabel tsslUnit;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripComboBox tscbxViewMode;
    }
}