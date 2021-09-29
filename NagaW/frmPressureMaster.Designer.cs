
namespace NagaW
{
    partial class frmPressureMaster
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblFPress = new System.Windows.Forms.Label();
            this.lblPPress = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxMaster = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblPPressMax = new System.Windows.Forms.Label();
            this.lblFPressMax = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPPressMin = new System.Windows.Forms.Label();
            this.lblFPressMin = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxMonitoring = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblInterval = new System.Windows.Forms.Label();
            this.lblIntervalMinutes = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "FPress";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFPress
            // 
            this.lblFPress.BackColor = System.Drawing.Color.White;
            this.lblFPress.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFPress.Location = new System.Drawing.Point(74, 22);
            this.lblFPress.Name = "lblFPress";
            this.lblFPress.Size = new System.Drawing.Size(98, 24);
            this.lblFPress.TabIndex = 1;
            this.lblFPress.Text = "label2";
            this.lblFPress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFPress.Click += new System.EventHandler(this.lblFPress_Click);
            // 
            // lblPPress
            // 
            this.lblPPress.BackColor = System.Drawing.Color.White;
            this.lblPPress.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPPress.Location = new System.Drawing.Point(74, 56);
            this.lblPPress.Name = "lblPPress";
            this.lblPPress.Size = new System.Drawing.Size(98, 24);
            this.lblPPress.TabIndex = 3;
            this.lblPPress.Text = "label3";
            this.lblPPress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPPress.Click += new System.EventHandler(this.lblPPress_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 24);
            this.label4.TabIndex = 2;
            this.label4.Text = "PPress";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbxMaster
            // 
            this.cbxMaster.AutoSize = true;
            this.cbxMaster.Location = new System.Drawing.Point(12, 120);
            this.cbxMaster.Name = "cbxMaster";
            this.cbxMaster.Size = new System.Drawing.Size(62, 18);
            this.cbxMaster.TabIndex = 6;
            this.cbxMaster.Text = "Master";
            this.cbxMaster.UseVisualStyleBackColor = true;
            this.cbxMaster.Click += new System.EventHandler(this.checkBox1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblPPress);
            this.groupBox1.Controls.Add(this.lblFPress);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(178, 98);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Master Value";
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox2.Controls.Add(this.lblPPressMax);
            this.groupBox2.Controls.Add(this.lblFPressMax);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.lblPPressMin);
            this.groupBox2.Controls.Add(this.lblFPressMin);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(196, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(282, 98);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Monitoring (Limit)";
            // 
            // lblPPressMax
            // 
            this.lblPPressMax.BackColor = System.Drawing.Color.White;
            this.lblPPressMax.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPPressMax.Location = new System.Drawing.Point(178, 56);
            this.lblPPressMax.Name = "lblPPressMax";
            this.lblPPressMax.Size = new System.Drawing.Size(98, 24);
            this.lblPPressMax.TabIndex = 5;
            this.lblPPressMax.Text = "label7";
            this.lblPPressMax.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPPressMax.Click += new System.EventHandler(this.lblPPressMax_Click);
            // 
            // lblFPressMax
            // 
            this.lblFPressMax.BackColor = System.Drawing.Color.White;
            this.lblFPressMax.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFPressMax.Location = new System.Drawing.Point(178, 22);
            this.lblFPressMax.Name = "lblFPressMax";
            this.lblFPressMax.Size = new System.Drawing.Size(98, 24);
            this.lblFPressMax.TabIndex = 4;
            this.lblFPressMax.Text = "label2";
            this.lblFPressMax.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFPressMax.Click += new System.EventHandler(this.lblFPressMax_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 24);
            this.label2.TabIndex = 0;
            this.label2.Text = "MinMax";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPPressMin
            // 
            this.lblPPressMin.BackColor = System.Drawing.Color.White;
            this.lblPPressMin.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPPressMin.Location = new System.Drawing.Point(74, 56);
            this.lblPPressMin.Name = "lblPPressMin";
            this.lblPPressMin.Size = new System.Drawing.Size(98, 24);
            this.lblPPressMin.TabIndex = 3;
            this.lblPPressMin.Text = "label3";
            this.lblPPressMin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPPressMin.Click += new System.EventHandler(this.lblPPressMin_Click);
            // 
            // lblFPressMin
            // 
            this.lblFPressMin.BackColor = System.Drawing.Color.White;
            this.lblFPressMin.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFPressMin.Location = new System.Drawing.Point(74, 22);
            this.lblFPressMin.Name = "lblFPressMin";
            this.lblFPressMin.Size = new System.Drawing.Size(98, 24);
            this.lblFPressMin.TabIndex = 1;
            this.lblFPressMin.Text = "label2";
            this.lblFPressMin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFPressMin.Click += new System.EventHandler(this.lblFPressMin_Click);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 24);
            this.label6.TabIndex = 2;
            this.label6.Text = "MinMax";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbxMonitoring
            // 
            this.cbxMonitoring.AutoSize = true;
            this.cbxMonitoring.Location = new System.Drawing.Point(196, 120);
            this.cbxMonitoring.Name = "cbxMonitoring";
            this.cbxMonitoring.Size = new System.Drawing.Size(83, 18);
            this.cbxMonitoring.TabIndex = 10;
            this.cbxMonitoring.Text = "Monitoring";
            this.cbxMonitoring.UseVisualStyleBackColor = true;
            this.cbxMonitoring.Click += new System.EventHandler(this.cbxMonitoring_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(306, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 24);
            this.label3.TabIndex = 11;
            this.label3.Text = "Interval";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblInterval
            // 
            this.lblInterval.BackColor = System.Drawing.Color.White;
            this.lblInterval.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInterval.Location = new System.Drawing.Point(374, 117);
            this.lblInterval.Name = "lblInterval";
            this.lblInterval.Size = new System.Drawing.Size(98, 24);
            this.lblInterval.TabIndex = 6;
            this.lblInterval.Text = "label3";
            this.lblInterval.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblInterval.Click += new System.EventHandler(this.lblInterval_Click);
            // 
            // lblIntervalMinutes
            // 
            this.lblIntervalMinutes.AutoSize = true;
            this.lblIntervalMinutes.Location = new System.Drawing.Point(371, 150);
            this.lblIntervalMinutes.Name = "lblIntervalMinutes";
            this.lblIntervalMinutes.Size = new System.Drawing.Size(48, 14);
            this.lblIntervalMinutes.TabIndex = 12;
            this.lblIntervalMinutes.Text = "Interval";
            this.lblIntervalMinutes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmPressureMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 191);
            this.Controls.Add(this.lblIntervalMinutes);
            this.Controls.Add(this.lblInterval);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbxMonitoring);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbxMaster);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.Name = "frmPressureMaster";
            this.Text = "frmPressureMaster";
            this.Load += new System.EventHandler(this.frmPressureMaster_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFPress;
        private System.Windows.Forms.Label lblPPress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbxMaster;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblPPressMax;
        private System.Windows.Forms.Label lblFPressMax;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPPressMin;
        private System.Windows.Forms.Label lblFPressMin;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cbxMonitoring;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblInterval;
        private System.Windows.Forms.Label lblIntervalMinutes;
    }
}