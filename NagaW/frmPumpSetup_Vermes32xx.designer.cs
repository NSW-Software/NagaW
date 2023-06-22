namespace NagaW
{
    partial class frmPumpSetup_Vermes32xx
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
            this.btnValveUpDn = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAdjust = new System.Windows.Forms.Button();
            this.btnVermesIOTrig = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSetupNL = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblSetupPulse = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lblSetupFT = new System.Windows.Forms.Label();
            this.lblFPress = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblSetupOT = new System.Windows.Forms.Label();
            this.lblSetupDelay = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblSetupRT = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnVmsPurge = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnValveUpDn
            // 
            this.btnValveUpDn.Location = new System.Drawing.Point(12, 25);
            this.btnValveUpDn.Name = "btnValveUpDn";
            this.btnValveUpDn.Size = new System.Drawing.Size(80, 37);
            this.btnValveUpDn.TabIndex = 9;
            this.btnValveUpDn.Text = "Valve Up (Hold)";
            this.btnValveUpDn.UseVisualStyleBackColor = true;
            this.btnValveUpDn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnValveUpDn_MouseDown);
            this.btnValveUpDn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnValveUpDn_MouseUp);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox2.Controls.Add(this.btnVmsPurge);
            this.groupBox2.Controls.Add(this.btnAdjust);
            this.groupBox2.Controls.Add(this.btnVermesIOTrig);
            this.groupBox2.Controls.Add(this.btnValveUpDn);
            this.groupBox2.Location = new System.Drawing.Point(8, 209);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(433, 83);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Function";
            // 
            // btnAdjust
            // 
            this.btnAdjust.Location = new System.Drawing.Point(261, 25);
            this.btnAdjust.Name = "btnAdjust";
            this.btnAdjust.Size = new System.Drawing.Size(80, 37);
            this.btnAdjust.TabIndex = 37;
            this.btnAdjust.Text = "Adjust";
            this.btnAdjust.UseVisualStyleBackColor = true;
            this.btnAdjust.Click += new System.EventHandler(this.btnAdjust_Click);
            // 
            // btnVermesIOTrig
            // 
            this.btnVermesIOTrig.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnVermesIOTrig.Location = new System.Drawing.Point(347, 25);
            this.btnVermesIOTrig.Name = "btnVermesIOTrig";
            this.btnVermesIOTrig.Size = new System.Drawing.Size(80, 37);
            this.btnVermesIOTrig.TabIndex = 36;
            this.btnVermesIOTrig.Text = "IO Trigger";
            this.btnVermesIOTrig.UseVisualStyleBackColor = true;
            this.btnVermesIOTrig.Click += new System.EventHandler(this.btnVermesIOTrig_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 25);
            this.label1.TabIndex = 24;
            this.label1.Text = "Rising Time";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSetupNL
            // 
            this.lblSetupNL.BackColor = System.Drawing.Color.White;
            this.lblSetupNL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSetupNL.Location = new System.Drawing.Point(327, 25);
            this.lblSetupNL.Margin = new System.Windows.Forms.Padding(3);
            this.lblSetupNL.Name = "lblSetupNL";
            this.lblSetupNL.Size = new System.Drawing.Size(100, 25);
            this.lblSetupNL.TabIndex = 27;
            this.lblSetupNL.Text = "label6";
            this.lblSetupNL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSetupNL.Click += new System.EventHandler(this.lblSetupNL_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(221, 149);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 25);
            this.label3.TabIndex = 37;
            this.label3.Text = "FPress";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSetupPulse
            // 
            this.lblSetupPulse.BackColor = System.Drawing.Color.White;
            this.lblSetupPulse.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSetupPulse.Location = new System.Drawing.Point(327, 56);
            this.lblSetupPulse.Margin = new System.Windows.Forms.Padding(3);
            this.lblSetupPulse.Name = "lblSetupPulse";
            this.lblSetupPulse.Size = new System.Drawing.Size(100, 25);
            this.lblSetupPulse.TabIndex = 29;
            this.lblSetupPulse.Text = "label8";
            this.lblSetupPulse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSetupPulse.Click += new System.EventHandler(this.lblSetupPulse_Click);
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(221, 56);
            this.label20.Margin = new System.Windows.Forms.Padding(3);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(100, 25);
            this.label20.TabIndex = 30;
            this.label20.Text = "No of Pulse";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSetupFT
            // 
            this.lblSetupFT.BackColor = System.Drawing.Color.White;
            this.lblSetupFT.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSetupFT.Location = new System.Drawing.Point(112, 87);
            this.lblSetupFT.Margin = new System.Windows.Forms.Padding(3);
            this.lblSetupFT.Name = "lblSetupFT";
            this.lblSetupFT.Size = new System.Drawing.Size(100, 25);
            this.lblSetupFT.TabIndex = 22;
            this.lblSetupFT.Text = "label1";
            this.lblSetupFT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSetupFT.Click += new System.EventHandler(this.lblSetupFT_Click);
            // 
            // lblFPress
            // 
            this.lblFPress.BackColor = System.Drawing.Color.White;
            this.lblFPress.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFPress.Location = new System.Drawing.Point(327, 149);
            this.lblFPress.Margin = new System.Windows.Forms.Padding(3);
            this.lblFPress.Name = "lblFPress";
            this.lblFPress.Size = new System.Drawing.Size(100, 25);
            this.lblFPress.TabIndex = 38;
            this.lblFPress.Text = "label12";
            this.lblFPress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFPress.Click += new System.EventHandler(this.lblFPress_Click);
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(6, 87);
            this.label13.Margin = new System.Windows.Forms.Padding(3);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 25);
            this.label13.TabIndex = 26;
            this.label13.Text = "Falling Time";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSetupOT
            // 
            this.lblSetupOT.BackColor = System.Drawing.Color.White;
            this.lblSetupOT.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSetupOT.Location = new System.Drawing.Point(112, 56);
            this.lblSetupOT.Margin = new System.Windows.Forms.Padding(3);
            this.lblSetupOT.Name = "lblSetupOT";
            this.lblSetupOT.Size = new System.Drawing.Size(100, 25);
            this.lblSetupOT.TabIndex = 23;
            this.lblSetupOT.Text = "label2";
            this.lblSetupOT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSetupOT.Click += new System.EventHandler(this.lblSetupOT_Click);
            // 
            // lblSetupDelay
            // 
            this.lblSetupDelay.BackColor = System.Drawing.Color.White;
            this.lblSetupDelay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSetupDelay.Location = new System.Drawing.Point(327, 87);
            this.lblSetupDelay.Margin = new System.Windows.Forms.Padding(3);
            this.lblSetupDelay.Name = "lblSetupDelay";
            this.lblSetupDelay.Size = new System.Drawing.Size(100, 25);
            this.lblSetupDelay.TabIndex = 33;
            this.lblSetupDelay.Text = "label12";
            this.lblSetupDelay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSetupDelay.Click += new System.EventHandler(this.lblSetupDelay_Click);
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(6, 56);
            this.label18.Margin = new System.Windows.Forms.Padding(3);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(100, 25);
            this.label18.TabIndex = 25;
            this.label18.Text = "Open Time";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSetupRT
            // 
            this.lblSetupRT.BackColor = System.Drawing.Color.White;
            this.lblSetupRT.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSetupRT.Location = new System.Drawing.Point(112, 25);
            this.lblSetupRT.Margin = new System.Windows.Forms.Padding(3);
            this.lblSetupRT.Name = "lblSetupRT";
            this.lblSetupRT.Size = new System.Drawing.Size(100, 25);
            this.lblSetupRT.TabIndex = 31;
            this.lblSetupRT.Text = "label10";
            this.lblSetupRT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSetupRT.Click += new System.EventHandler(this.lblSetupRT_Click);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(221, 87);
            this.label6.Margin = new System.Windows.Forms.Padding(3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 25);
            this.label6.TabIndex = 32;
            this.label6.Text = "Delay";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(221, 25);
            this.label17.Margin = new System.Windows.Forms.Padding(3);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(100, 25);
            this.label17.TabIndex = 28;
            this.label17.Text = "Needle Lift";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.lblSetupOT);
            this.groupBox1.Controls.Add(this.lblFPress);
            this.groupBox1.Controls.Add(this.lblSetupDelay);
            this.groupBox1.Controls.Add(this.lblSetupNL);
            this.groupBox1.Controls.Add(this.lblSetupFT);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lblSetupRT);
            this.groupBox1.Controls.Add(this.lblSetupPulse);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(433, 195);
            this.groupBox1.TabIndex = 41;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Param";
            // 
            // btnVmsPurge
            // 
            this.btnVmsPurge.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnVmsPurge.Location = new System.Drawing.Point(175, 25);
            this.btnVmsPurge.Name = "btnVmsPurge";
            this.btnVmsPurge.Size = new System.Drawing.Size(80, 37);
            this.btnVmsPurge.TabIndex = 38;
            this.btnVmsPurge.Text = "Purge";
            this.btnVmsPurge.UseVisualStyleBackColor = true;
            this.btnVmsPurge.Click += new System.EventHandler(this.btnVmsPurge_Click);
            // 
            // frmPumpSetup_Vermes32xx
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(473, 314);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPumpSetup_Vermes32xx";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "frmVermes3280Ctrl";
            this.Load += new System.EventHandler(this.frmVermesCtrl_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnValveUpDn;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblSetupRT;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblSetupPulse;
        private System.Windows.Forms.Label lblSetupFT;
        private System.Windows.Forms.Label lblSetupDelay;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblSetupOT;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lblSetupNL;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblFPress;
        private System.Windows.Forms.Button btnVermesIOTrig;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAdjust;
        private System.Windows.Forms.Button btnVmsPurge;
    }
}