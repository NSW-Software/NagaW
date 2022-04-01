namespace NagaW
{
    partial class frmAuto
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.cbxRunMode = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.lblWaferInput = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblIntervalTimeout = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chbxEnaNotch = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblDoorLockSens = new System.Windows.Forms.Label();
            this.btnManualUnload = new System.Windows.Forms.Button();
            this.btnManualLoad = new System.Windows.Forms.Button();
            this.btnAutoUnload = new System.Windows.Forms.Button();
            this.btnAutoLoad = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.chbxEnableIonizer = new System.Windows.Forms.CheckBox();
            this.lblUPIN = new System.Windows.Forms.Label();
            this.lblUPOUT = new System.Windows.Forms.Label();
            this.lblDNOUT = new System.Windows.Forms.Label();
            this.lblDNIN = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnNotchAlign = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.btnStart);
            this.flowLayoutPanel1.Controls.Add(this.btnStop);
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 557);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(425, 46);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(3, 3);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(100, 40);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(109, 3);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(100, 40);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(215, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 40);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 18);
            this.label9.TabIndex = 14;
            this.label9.Text = "RunMode";
            // 
            // cbxRunMode
            // 
            this.cbxRunMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxRunMode.FormattingEnabled = true;
            this.cbxRunMode.Location = new System.Drawing.Point(79, 6);
            this.cbxRunMode.Name = "cbxRunMode";
            this.cbxRunMode.Size = new System.Drawing.Size(121, 26);
            this.cbxRunMode.TabIndex = 13;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(12, 12);
            this.label13.Margin = new System.Windows.Forms.Padding(3);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(118, 25);
            this.label13.TabIndex = 26;
            this.label13.Text = "Wafer Input";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblWaferInput
            // 
            this.lblWaferInput.BackColor = System.Drawing.Color.White;
            this.lblWaferInput.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblWaferInput.Location = new System.Drawing.Point(136, 12);
            this.lblWaferInput.Margin = new System.Windows.Forms.Padding(3);
            this.lblWaferInput.Name = "lblWaferInput";
            this.lblWaferInput.Size = new System.Drawing.Size(113, 25);
            this.lblWaferInput.TabIndex = 27;
            this.lblWaferInput.Text = "label6";
            this.lblWaferInput.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblWaferInput.Click += new System.EventHandler(this.lblWaferInput_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(255, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 25);
            this.label1.TabIndex = 28;
            this.label1.Text = "(0 = ∞)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 43);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 25);
            this.label3.TabIndex = 29;
            this.label3.Text = "Intervel Timeout";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIntervalTimeout
            // 
            this.lblIntervalTimeout.BackColor = System.Drawing.Color.White;
            this.lblIntervalTimeout.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIntervalTimeout.Location = new System.Drawing.Point(136, 43);
            this.lblIntervalTimeout.Margin = new System.Windows.Forms.Padding(3);
            this.lblIntervalTimeout.Name = "lblIntervalTimeout";
            this.lblIntervalTimeout.Size = new System.Drawing.Size(113, 25);
            this.lblIntervalTimeout.TabIndex = 30;
            this.lblIntervalTimeout.Text = "label6";
            this.lblIntervalTimeout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIntervalTimeout.Click += new System.EventHandler(this.lblIntervalTimeout_Click);
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.cbxRunMode);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 522);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(425, 35);
            this.panel1.TabIndex = 31;
            // 
            // chbxEnaNotch
            // 
            this.chbxEnaNotch.AutoSize = true;
            this.chbxEnaNotch.Location = new System.Drawing.Point(15, 85);
            this.chbxEnaNotch.Name = "chbxEnaNotch";
            this.chbxEnaNotch.Size = new System.Drawing.Size(271, 22);
            this.chbxEnaNotch.TabIndex = 32;
            this.chbxEnaNotch.Text = "Enable Notch Alignment After Load-in";
            this.chbxEnaNotch.UseVisualStyleBackColor = true;
            this.chbxEnaNotch.Click += new System.EventHandler(this.chbxEnaNotch_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.btnNotchAlign);
            this.groupBox1.Controls.Add(this.lblDoorLockSens);
            this.groupBox1.Controls.Add(this.btnManualUnload);
            this.groupBox1.Controls.Add(this.btnManualLoad);
            this.groupBox1.Controls.Add(this.btnAutoUnload);
            this.groupBox1.Controls.Add(this.btnAutoLoad);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(108, 380);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Function";
            // 
            // lblDoorLockSens
            // 
            this.lblDoorLockSens.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDoorLockSens.Location = new System.Drawing.Point(6, 253);
            this.lblDoorLockSens.Name = "lblDoorLockSens";
            this.lblDoorLockSens.Size = new System.Drawing.Size(90, 45);
            this.lblDoorLockSens.TabIndex = 34;
            this.lblDoorLockSens.Text = "DoorLock Sens";
            this.lblDoorLockSens.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnManualUnload
            // 
            this.btnManualUnload.Location = new System.Drawing.Point(6, 193);
            this.btnManualUnload.Name = "btnManualUnload";
            this.btnManualUnload.Size = new System.Drawing.Size(90, 45);
            this.btnManualUnload.TabIndex = 3;
            this.btnManualUnload.Text = "Manual Unload";
            this.btnManualUnload.UseVisualStyleBackColor = true;
            this.btnManualUnload.Click += new System.EventHandler(this.btnManualUnload_Click);
            // 
            // btnManualLoad
            // 
            this.btnManualLoad.Location = new System.Drawing.Point(6, 127);
            this.btnManualLoad.Name = "btnManualLoad";
            this.btnManualLoad.Size = new System.Drawing.Size(90, 45);
            this.btnManualLoad.TabIndex = 2;
            this.btnManualLoad.Text = "Manual Load";
            this.btnManualLoad.UseVisualStyleBackColor = true;
            this.btnManualLoad.Click += new System.EventHandler(this.btnManualLoad_Click);
            // 
            // btnAutoUnload
            // 
            this.btnAutoUnload.Location = new System.Drawing.Point(6, 76);
            this.btnAutoUnload.Name = "btnAutoUnload";
            this.btnAutoUnload.Size = new System.Drawing.Size(90, 45);
            this.btnAutoUnload.TabIndex = 1;
            this.btnAutoUnload.Text = "Auto Unload";
            this.btnAutoUnload.UseVisualStyleBackColor = true;
            this.btnAutoUnload.Click += new System.EventHandler(this.btnAutoUnload_Click);
            // 
            // btnAutoLoad
            // 
            this.btnAutoLoad.Location = new System.Drawing.Point(6, 25);
            this.btnAutoLoad.Name = "btnAutoLoad";
            this.btnAutoLoad.Size = new System.Drawing.Size(90, 45);
            this.btnAutoLoad.TabIndex = 0;
            this.btnAutoLoad.Text = "Auto Load";
            this.btnAutoLoad.UseVisualStyleBackColor = true;
            this.btnAutoLoad.Click += new System.EventHandler(this.btnAutoLoad_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // chbxEnableIonizer
            // 
            this.chbxEnableIonizer.AutoSize = true;
            this.chbxEnableIonizer.Location = new System.Drawing.Point(15, 113);
            this.chbxEnableIonizer.Name = "chbxEnableIonizer";
            this.chbxEnableIonizer.Size = new System.Drawing.Size(121, 22);
            this.chbxEnableIonizer.TabIndex = 34;
            this.chbxEnableIonizer.Text = "Enable Ionizer";
            this.chbxEnableIonizer.UseVisualStyleBackColor = true;
            this.chbxEnableIonizer.Click += new System.EventHandler(this.chbxEnableIonizer_Click);
            // 
            // lblUPIN
            // 
            this.lblUPIN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblUPIN.Location = new System.Drawing.Point(6, 22);
            this.lblUPIN.Name = "lblUPIN";
            this.lblUPIN.Size = new System.Drawing.Size(90, 45);
            this.lblUPIN.TabIndex = 35;
            this.lblUPIN.Text = "UP IN";
            this.lblUPIN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUPOUT
            // 
            this.lblUPOUT.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblUPOUT.Location = new System.Drawing.Point(6, 67);
            this.lblUPOUT.Name = "lblUPOUT";
            this.lblUPOUT.Size = new System.Drawing.Size(90, 45);
            this.lblUPOUT.TabIndex = 36;
            this.lblUPOUT.Text = "UP OUT";
            this.lblUPOUT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDNOUT
            // 
            this.lblDNOUT.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDNOUT.Location = new System.Drawing.Point(6, 172);
            this.lblDNOUT.Name = "lblDNOUT";
            this.lblDNOUT.Size = new System.Drawing.Size(90, 45);
            this.lblDNOUT.TabIndex = 38;
            this.lblDNOUT.Text = "DN OUT";
            this.lblDNOUT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDNIN
            // 
            this.lblDNIN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDNIN.Location = new System.Drawing.Point(6, 127);
            this.lblDNIN.Name = "lblDNIN";
            this.lblDNIN.Size = new System.Drawing.Size(90, 45);
            this.lblDNIN.TabIndex = 37;
            this.lblDNIN.Text = "DN IN";
            this.lblDNIN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox2.Controls.Add(this.lblUPIN);
            this.groupBox2.Controls.Add(this.lblDNOUT);
            this.groupBox2.Controls.Add(this.lblUPOUT);
            this.groupBox2.Controls.Add(this.lblDNIN);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 380);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(108, 239);
            this.groupBox2.TabIndex = 39;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SMEMA";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(425, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(108, 603);
            this.panel2.TabIndex = 40;
            // 
            // btnNotchAlign
            // 
            this.btnNotchAlign.Location = new System.Drawing.Point(6, 310);
            this.btnNotchAlign.Name = "btnNotchAlign";
            this.btnNotchAlign.Size = new System.Drawing.Size(90, 45);
            this.btnNotchAlign.TabIndex = 41;
            this.btnNotchAlign.Text = "Notch Align";
            this.btnNotchAlign.UseVisualStyleBackColor = true;
            this.btnNotchAlign.Click += new System.EventHandler(this.btnNotchAlign_Click);
            // 
            // frmAuto
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(533, 603);
            this.Controls.Add(this.chbxEnableIonizer);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.chbxEnaNotch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblIntervalTimeout);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.lblWaferInput);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.Name = "frmAuto";
            this.Text = "frmAuto";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAuto_FormClosing);
            this.Load += new System.EventHandler(this.frmAuto_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbxRunMode;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblWaferInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblIntervalTimeout;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chbxEnaNotch;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnManualUnload;
        private System.Windows.Forms.Button btnManualLoad;
        private System.Windows.Forms.Button btnAutoUnload;
        private System.Windows.Forms.Button btnAutoLoad;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblDoorLockSens;
        private System.Windows.Forms.CheckBox chbxEnableIonizer;
        private System.Windows.Forms.Label lblUPIN;
        private System.Windows.Forms.Label lblUPOUT;
        private System.Windows.Forms.Label lblDNOUT;
        private System.Windows.Forms.Label lblDNIN;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnNotchAlign;
    }
}