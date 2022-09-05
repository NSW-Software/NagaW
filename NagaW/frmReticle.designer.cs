namespace NagaW
{
    partial class frmReticle
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
            this.nudSizeX = new System.Windows.Forms.NumericUpDown();
            this.nudSizeY = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxReticleType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblColor = new System.Windows.Forms.Label();
            this.cbxListReticles = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.nudLineWidth = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxText = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudSizeX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSizeY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLineWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // nudSizeX
            // 
            this.nudSizeX.Location = new System.Drawing.Point(201, 75);
            this.nudSizeX.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.nudSizeX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSizeX.Name = "nudSizeX";
            this.nudSizeX.Size = new System.Drawing.Size(85, 26);
            this.nudSizeX.TabIndex = 0;
            this.nudSizeX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSizeX.ValueChanged += new System.EventHandler(this.nudSizeX_ValueChanged);
            // 
            // nudSizeY
            // 
            this.nudSizeY.Location = new System.Drawing.Point(292, 75);
            this.nudSizeY.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.nudSizeY.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSizeY.Name = "nudSizeY";
            this.nudSizeY.Size = new System.Drawing.Size(85, 26);
            this.nudSizeY.TabIndex = 1;
            this.nudSizeY.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSizeY.ValueChanged += new System.EventHandler(this.nudSizeY_ValueChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(120, 75);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Size";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbxReticleType
            // 
            this.cbxReticleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxReticleType.FormattingEnabled = true;
            this.cbxReticleType.Location = new System.Drawing.Point(201, 12);
            this.cbxReticleType.Name = "cbxReticleType";
            this.cbxReticleType.Size = new System.Drawing.Size(176, 26);
            this.cbxReticleType.TabIndex = 3;
            this.cbxReticleType.SelectionChangeCommitted += new System.EventHandler(this.cbxReticleType_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label2.Location = new System.Drawing.Point(120, 12);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Type";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label3.Location = new System.Drawing.Point(120, 44);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Color";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblColor
            // 
            this.lblColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblColor.Location = new System.Drawing.Point(201, 44);
            this.lblColor.Margin = new System.Windows.Forms.Padding(3);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(176, 28);
            this.lblColor.TabIndex = 6;
            this.lblColor.Text = "label4";
            this.lblColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblColor.Click += new System.EventHandler(this.lblColor_Click);
            // 
            // cbxListReticles
            // 
            this.cbxListReticles.Dock = System.Windows.Forms.DockStyle.Left;
            this.cbxListReticles.FormattingEnabled = true;
            this.cbxListReticles.Location = new System.Drawing.Point(0, 0);
            this.cbxListReticles.Name = "cbxListReticles";
            this.cbxListReticles.Size = new System.Drawing.Size(114, 310);
            this.cbxListReticles.TabIndex = 7;
            this.cbxListReticles.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.cbxListReticles_ItemCheck);
            this.cbxListReticles.SelectedIndexChanged += new System.EventHandler(this.cbxListReticles_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label4.Location = new System.Drawing.Point(120, 106);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 25);
            this.label4.TabIndex = 8;
            this.label4.Text = "LineWidth";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudLineWidth
            // 
            this.nudLineWidth.Location = new System.Drawing.Point(201, 107);
            this.nudLineWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLineWidth.Name = "nudLineWidth";
            this.nudLineWidth.Size = new System.Drawing.Size(85, 26);
            this.nudLineWidth.TabIndex = 9;
            this.nudLineWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLineWidth.ValueChanged += new System.EventHandler(this.nudLineWidth_ValueChanged);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label5.Location = new System.Drawing.Point(120, 137);
            this.label5.Margin = new System.Windows.Forms.Padding(3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 25);
            this.label5.TabIndex = 10;
            this.label5.Text = "Text";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbxText
            // 
            this.tbxText.Location = new System.Drawing.Point(201, 137);
            this.tbxText.Name = "tbxText";
            this.tbxText.Size = new System.Drawing.Size(176, 26);
            this.tbxText.TabIndex = 11;
            this.tbxText.TextChanged += new System.EventHandler(this.tbxText_TextChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(198, 166);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(184, 25);
            this.label6.TabIndex = 12;
            this.label6.Text = "Use @ to split text content";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmReticle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 310);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbxText);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nudLineWidth);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbxReticleType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxListReticles);
            this.Controls.Add(this.nudSizeX);
            this.Controls.Add(this.lblColor);
            this.Controls.Add(this.nudSizeY);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.Name = "frmReticle";
            this.Text = "frmReticle";
            this.Load += new System.EventHandler(this.frmReticle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudSizeX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSizeY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLineWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudSizeX;
        private System.Windows.Forms.NumericUpDown nudSizeY;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxReticleType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.CheckedListBox cbxListReticles;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudLineWidth;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxText;
        private System.Windows.Forms.Label label6;
    }
}