namespace NagaW
{
    partial class frmPumpSetup_SynchroPulse
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblPulseOnDelay = new System.Windows.Forms.Label();
            this.lblPulseOffDelay = new System.Windows.Forms.Label();
            this.lblPulseOffDelayEarly = new System.Windows.Forms.Label();
            this.lblDispTime = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblPPress = new System.Windows.Forms.Label();
            this.lblPPressLabel = new System.Windows.Forms.Label();
            this.lblFPress = new System.Windows.Forms.Label();
            this.lblPulseOnDelayEarly = new System.Windows.Forms.Label();
            this.btnFPress = new System.Windows.Forms.Button();
            this.btnPPress = new System.Windows.Forms.Button();
            this.btnShot = new System.Windows.Forms.Button();
            this.btnFPressH = new System.Windows.Forms.Button();
            this.lblFPressH = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblVacdur = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pnlSPDelay = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnlSP = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnVac = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.pnlSPDelay.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlSP.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 30);
            this.label1.TabIndex = 41;
            this.label1.Text = "F Pressure";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPulseOnDelay
            // 
            this.lblPulseOnDelay.BackColor = System.Drawing.Color.White;
            this.lblPulseOnDelay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPulseOnDelay.Location = new System.Drawing.Point(109, 3);
            this.lblPulseOnDelay.Margin = new System.Windows.Forms.Padding(3);
            this.lblPulseOnDelay.Name = "lblPulseOnDelay";
            this.lblPulseOnDelay.Size = new System.Drawing.Size(100, 30);
            this.lblPulseOnDelay.TabIndex = 44;
            this.lblPulseOnDelay.Text = "99.99";
            this.lblPulseOnDelay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPulseOnDelay.Click += new System.EventHandler(this.lblPulseOnDelay_Click);
            // 
            // lblPulseOffDelay
            // 
            this.lblPulseOffDelay.BackColor = System.Drawing.Color.White;
            this.lblPulseOffDelay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPulseOffDelay.Location = new System.Drawing.Point(109, 39);
            this.lblPulseOffDelay.Margin = new System.Windows.Forms.Padding(3);
            this.lblPulseOffDelay.Name = "lblPulseOffDelay";
            this.lblPulseOffDelay.Size = new System.Drawing.Size(100, 30);
            this.lblPulseOffDelay.TabIndex = 46;
            this.lblPulseOffDelay.Text = "99.99";
            this.lblPulseOffDelay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPulseOffDelay.Click += new System.EventHandler(this.lblPulseOffDelay_Click);
            // 
            // lblPulseOffDelayEarly
            // 
            this.lblPulseOffDelayEarly.Location = new System.Drawing.Point(3, 39);
            this.lblPulseOffDelayEarly.Margin = new System.Windows.Forms.Padding(3);
            this.lblPulseOffDelayEarly.Name = "lblPulseOffDelayEarly";
            this.lblPulseOffDelayEarly.Size = new System.Drawing.Size(100, 30);
            this.lblPulseOffDelayEarly.TabIndex = 47;
            this.lblPulseOffDelayEarly.Text = "PulseOff Delay";
            this.lblPulseOffDelayEarly.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDispTime
            // 
            this.lblDispTime.BackColor = System.Drawing.Color.White;
            this.lblDispTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDispTime.Location = new System.Drawing.Point(109, 3);
            this.lblDispTime.Margin = new System.Windows.Forms.Padding(3);
            this.lblDispTime.Name = "lblDispTime";
            this.lblDispTime.Size = new System.Drawing.Size(100, 30);
            this.lblDispTime.TabIndex = 39;
            this.lblDispTime.Text = "99.99";
            this.lblDispTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDispTime.Click += new System.EventHandler(this.lblDispTime_Click);
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(3, 3);
            this.label13.Margin = new System.Windows.Forms.Padding(3);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 30);
            this.label13.TabIndex = 43;
            this.label13.Text = "Disp Time";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPPress
            // 
            this.lblPPress.BackColor = System.Drawing.Color.White;
            this.lblPPress.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPPress.Location = new System.Drawing.Point(109, 3);
            this.lblPPress.Margin = new System.Windows.Forms.Padding(3);
            this.lblPPress.Name = "lblPPress";
            this.lblPPress.Size = new System.Drawing.Size(100, 30);
            this.lblPPress.TabIndex = 40;
            this.lblPPress.Text = "99.99";
            this.lblPPress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPPress.Click += new System.EventHandler(this.lblPPress_Click);
            // 
            // lblPPressLabel
            // 
            this.lblPPressLabel.Location = new System.Drawing.Point(3, 3);
            this.lblPPressLabel.Margin = new System.Windows.Forms.Padding(3);
            this.lblPPressLabel.Name = "lblPPressLabel";
            this.lblPPressLabel.Size = new System.Drawing.Size(100, 30);
            this.lblPPressLabel.TabIndex = 42;
            this.lblPPressLabel.Text = "P Pressure";
            this.lblPPressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFPress
            // 
            this.lblFPress.BackColor = System.Drawing.Color.White;
            this.lblFPress.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFPress.Location = new System.Drawing.Point(109, 3);
            this.lblFPress.Margin = new System.Windows.Forms.Padding(3);
            this.lblFPress.Name = "lblFPress";
            this.lblFPress.Size = new System.Drawing.Size(100, 30);
            this.lblFPress.TabIndex = 48;
            this.lblFPress.Text = "99.99";
            this.lblFPress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFPress.Click += new System.EventHandler(this.lblFPress_Click);
            // 
            // lblPulseOnDelayEarly
            // 
            this.lblPulseOnDelayEarly.Location = new System.Drawing.Point(3, 3);
            this.lblPulseOnDelayEarly.Margin = new System.Windows.Forms.Padding(3);
            this.lblPulseOnDelayEarly.Name = "lblPulseOnDelayEarly";
            this.lblPulseOnDelayEarly.Size = new System.Drawing.Size(100, 30);
            this.lblPulseOnDelayEarly.TabIndex = 45;
            this.lblPulseOnDelayEarly.Text = "PulseOn Delay";
            this.lblPulseOnDelayEarly.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnFPress
            // 
            this.btnFPress.Location = new System.Drawing.Point(215, 3);
            this.btnFPress.Name = "btnFPress";
            this.btnFPress.Size = new System.Drawing.Size(90, 30);
            this.btnFPress.TabIndex = 49;
            this.btnFPress.Text = "FPress";
            this.btnFPress.UseVisualStyleBackColor = true;
            this.btnFPress.Click += new System.EventHandler(this.btnFPress_Click);
            // 
            // btnPPress
            // 
            this.btnPPress.Location = new System.Drawing.Point(215, 3);
            this.btnPPress.Name = "btnPPress";
            this.btnPPress.Size = new System.Drawing.Size(90, 30);
            this.btnPPress.TabIndex = 50;
            this.btnPPress.Text = "PPress";
            this.btnPPress.UseVisualStyleBackColor = true;
            this.btnPPress.Click += new System.EventHandler(this.btnPPress_Click);
            // 
            // btnShot
            // 
            this.btnShot.Location = new System.Drawing.Point(417, 3);
            this.btnShot.Name = "btnShot";
            this.btnShot.Size = new System.Drawing.Size(90, 30);
            this.btnShot.TabIndex = 51;
            this.btnShot.Text = "Shot";
            this.btnShot.UseVisualStyleBackColor = true;
            this.btnShot.Click += new System.EventHandler(this.btnShot_Click);
            // 
            // btnFPressH
            // 
            this.btnFPressH.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnFPressH.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFPressH.Location = new System.Drawing.Point(417, 3);
            this.btnFPressH.Name = "btnFPressH";
            this.btnFPressH.Size = new System.Drawing.Size(90, 30);
            this.btnFPressH.TabIndex = 52;
            this.btnFPressH.Text = "FPressH";
            this.btnFPressH.UseVisualStyleBackColor = true;
            this.btnFPressH.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnFPressH_MouseDown);
            this.btnFPressH.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnFPressH_MouseUp);
            // 
            // lblFPressH
            // 
            this.lblFPressH.BackColor = System.Drawing.Color.White;
            this.lblFPressH.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFPressH.Location = new System.Drawing.Point(311, 3);
            this.lblFPressH.Margin = new System.Windows.Forms.Padding(3);
            this.lblFPressH.Name = "lblFPressH";
            this.lblFPressH.Size = new System.Drawing.Size(100, 30);
            this.lblFPressH.TabIndex = 53;
            this.lblFPressH.Text = "99.99";
            this.lblFPressH.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFPressH.Click += new System.EventHandler(this.lblFPressH_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblVacdur
            // 
            this.lblVacdur.BackColor = System.Drawing.Color.White;
            this.lblVacdur.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblVacdur.Location = new System.Drawing.Point(109, 3);
            this.lblVacdur.Margin = new System.Windows.Forms.Padding(3);
            this.lblVacdur.Name = "lblVacdur";
            this.lblVacdur.Size = new System.Drawing.Size(100, 30);
            this.lblVacdur.TabIndex = 54;
            this.lblVacdur.Text = "99.99";
            this.lblVacdur.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblVacdur.Click += new System.EventHandler(this.lblVacdur_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 30);
            this.label3.TabIndex = 55;
            this.label3.Text = "VacDur";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.pnlSPDelay);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.pnlSP);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(8, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(513, 222);
            this.panel1.TabIndex = 56;
            // 
            // panel5
            // 
            this.panel5.AutoSize = true;
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.lblVacdur);
            this.panel5.Controls.Add(this.btnShot);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 180);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(513, 36);
            this.panel5.TabIndex = 58;
            // 
            // pnlSPDelay
            // 
            this.pnlSPDelay.AutoSize = true;
            this.pnlSPDelay.Controls.Add(this.btnVac);
            this.pnlSPDelay.Controls.Add(this.lblPulseOnDelayEarly);
            this.pnlSPDelay.Controls.Add(this.lblPulseOffDelayEarly);
            this.pnlSPDelay.Controls.Add(this.lblPulseOnDelay);
            this.pnlSPDelay.Controls.Add(this.lblPulseOffDelay);
            this.pnlSPDelay.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSPDelay.Location = new System.Drawing.Point(0, 108);
            this.pnlSPDelay.Name = "pnlSPDelay";
            this.pnlSPDelay.Size = new System.Drawing.Size(513, 72);
            this.pnlSPDelay.TabIndex = 57;
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.lblDispTime);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 72);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(513, 36);
            this.panel3.TabIndex = 56;
            // 
            // pnlSP
            // 
            this.pnlSP.AutoSize = true;
            this.pnlSP.Controls.Add(this.lblPPressLabel);
            this.pnlSP.Controls.Add(this.btnPPress);
            this.pnlSP.Controls.Add(this.lblPPress);
            this.pnlSP.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSP.Location = new System.Drawing.Point(0, 36);
            this.pnlSP.Name = "pnlSP";
            this.pnlSP.Size = new System.Drawing.Size(513, 36);
            this.pnlSP.TabIndex = 55;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lblFPress);
            this.panel2.Controls.Add(this.btnFPress);
            this.panel2.Controls.Add(this.lblFPressH);
            this.panel2.Controls.Add(this.btnFPressH);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(513, 36);
            this.panel2.TabIndex = 54;
            // 
            // btnVac
            // 
            this.btnVac.Location = new System.Drawing.Point(417, 6);
            this.btnVac.Name = "btnVac";
            this.btnVac.Size = new System.Drawing.Size(90, 30);
            this.btnVac.TabIndex = 51;
            this.btnVac.Text = "Vaccum";
            this.btnVac.UseVisualStyleBackColor = true;
            this.btnVac.Click += new System.EventHandler(this.btnVac_Click);
            // 
            // frmPumpSetup_SynchroPulse
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(529, 238);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.Name = "frmPumpSetup_SynchroPulse";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "frmSynchroPulseSetup";
            this.Load += new System.EventHandler(this.frmSynchroPulseSetup_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.pnlSPDelay.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.pnlSP.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPulseOnDelay;
        private System.Windows.Forms.Label lblPulseOffDelay;
        private System.Windows.Forms.Label lblPulseOffDelayEarly;
        private System.Windows.Forms.Label lblDispTime;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblPPress;
        private System.Windows.Forms.Label lblPPressLabel;
        private System.Windows.Forms.Label lblFPress;
        private System.Windows.Forms.Label lblPulseOnDelayEarly;
        private System.Windows.Forms.Button btnFPress;
        private System.Windows.Forms.Button btnPPress;
        private System.Windows.Forms.Button btnShot;
        private System.Windows.Forms.Button btnFPressH;
        private System.Windows.Forms.Label lblFPressH;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblVacdur;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel pnlSP;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel pnlSPDelay;
        private System.Windows.Forms.Button btnVac;
    }
}