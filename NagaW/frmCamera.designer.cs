namespace NagaW
{
    partial class frmCamera
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnReticle = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnSaveAs = new System.Windows.Forms.ToolStripButton();
            this.tsbtnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.tsZoomFit = new System.Windows.Forms.ToolStripButton();
            this.tsbtnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.tslblScale = new System.Windows.Forms.ToolStripLabel();
            this.pnlImage = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnReticle,
            this.toolStripSeparator1,
            this.tsbtnSaveAs,
            this.tsbtnZoomOut,
            this.tsZoomFit,
            this.tsbtnZoomIn,
            this.tslblScale});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.Size = new System.Drawing.Size(746, 39);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtnReticle
            // 
            this.tsbtnReticle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnReticle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnReticle.Name = "tsbtnReticle";
            this.tsbtnReticle.Size = new System.Drawing.Size(58, 36);
            this.tsbtnReticle.Text = "Reticle";
            this.tsbtnReticle.Click += new System.EventHandler(this.tsbtnReticle_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // tsbtnSaveAs
            // 
            this.tsbtnSaveAs.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSaveAs.Name = "tsbtnSaveAs";
            this.tsbtnSaveAs.Size = new System.Drawing.Size(90, 36);
            this.tsbtnSaveAs.Text = "Save Image";
            this.tsbtnSaveAs.Click += new System.EventHandler(this.tsbtnSaveAs_Click);
            // 
            // tsbtnZoomOut
            // 
            this.tsbtnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnZoomOut.Name = "tsbtnZoomOut";
            this.tsbtnZoomOut.Size = new System.Drawing.Size(29, 36);
            this.tsbtnZoomOut.Text = "Z-";
            this.tsbtnZoomOut.Click += new System.EventHandler(this.tsbtnZoomOut_Click);
            // 
            // tsZoomFit
            // 
            this.tsZoomFit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsZoomFit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsZoomFit.Name = "tsZoomFit";
            this.tsZoomFit.Size = new System.Drawing.Size(29, 36);
            this.tsZoomFit.Text = "ZF";
            this.tsZoomFit.Click += new System.EventHandler(this.tsZoomFit_Click);
            // 
            // tsbtnZoomIn
            // 
            this.tsbtnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnZoomIn.Name = "tsbtnZoomIn";
            this.tsbtnZoomIn.Size = new System.Drawing.Size(32, 36);
            this.tsbtnZoomIn.Text = "Z+";
            this.tsbtnZoomIn.Click += new System.EventHandler(this.tsbtnZoomIn_Click);
            // 
            // tslblScale
            // 
            this.tslblScale.Name = "tslblScale";
            this.tslblScale.Size = new System.Drawing.Size(111, 36);
            this.tslblScale.Text = "toolStripLabel1";
            // 
            // pnlImage
            // 
            this.pnlImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlImage.Location = new System.Drawing.Point(0, 49);
            this.pnlImage.Name = "pnlImage";
            this.pnlImage.Size = new System.Drawing.Size(746, 519);
            this.pnlImage.TabIndex = 2;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmCamera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 454);
            this.Controls.Add(this.pnlImage);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.Name = "frmCamera";
            this.Text = "frmCamera";
            this.Load += new System.EventHandler(this.frmCamera_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtnSaveAs;
        private System.Windows.Forms.ToolStripButton tsbtnReticle;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsZoomFit;
        private System.Windows.Forms.Panel pnlImage;
        private System.Windows.Forms.ToolStripButton tsbtnZoomOut;
        private System.Windows.Forms.ToolStripButton tsbtnZoomIn;
        private System.Windows.Forms.ToolStripLabel tslblScale;
        private System.Windows.Forms.Timer timer1;
    }
}