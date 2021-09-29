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
            this.cbContinuous = new System.Windows.Forms.CheckBox();
            this.btn_SetCnyStatusReady = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.cbxRunMode = new System.Windows.Forms.ComboBox();
            this.lblLeftSmemaOutSHow = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblLeftOutBdReady = new System.Windows.Forms.Label();
            this.lblLeftOutMcReady = new System.Windows.Forms.Label();
            this.lblLFConvStatus = new System.Windows.Forms.Label();
            this.lblLFBDStatus = new System.Windows.Forms.Label();
            this.lblLeftBdReady = new System.Windows.Forms.Label();
            this.lblLeftConvStatus = new System.Windows.Forms.Label();
            this.lblLeftMcReady = new System.Windows.Forms.Label();
            this.lblLeftBdStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRightBdStatus = new System.Windows.Forms.Label();
            this.tmrDisplay = new System.Windows.Forms.Timer(this.components);
            this.lblRightConvStatus = new System.Windows.Forms.Label();
            this.tmr1s = new System.Windows.Forms.Timer(this.components);
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.btnStart);
            this.flowLayoutPanel1.Controls.Add(this.btnStop);
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Controls.Add(this.cbContinuous);
            this.flowLayoutPanel1.Controls.Add(this.btn_SetCnyStatusReady);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 397);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(648, 46);
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
            // cbContinuous
            // 
            this.cbContinuous.AutoSize = true;
            this.cbContinuous.Location = new System.Drawing.Point(321, 3);
            this.cbContinuous.Name = "cbContinuous";
            this.cbContinuous.Size = new System.Drawing.Size(87, 18);
            this.cbContinuous.TabIndex = 4;
            this.cbContinuous.Text = "Continuous";
            this.cbContinuous.UseVisualStyleBackColor = true;
            // 
            // btn_SetCnyStatusReady
            // 
            this.btn_SetCnyStatusReady.Location = new System.Drawing.Point(414, 3);
            this.btn_SetCnyStatusReady.Name = "btn_SetCnyStatusReady";
            this.btn_SetCnyStatusReady.Size = new System.Drawing.Size(150, 40);
            this.btn_SetCnyStatusReady.TabIndex = 5;
            this.btn_SetCnyStatusReady.Text = "Set Conveyor Status to Ready";
            this.btn_SetCnyStatusReady.UseVisualStyleBackColor = true;
            this.btn_SetCnyStatusReady.Visible = false;
            this.btn_SetCnyStatusReady.Click += new System.EventHandler(this.btn_SetCnyStatusReady_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(-1, 369);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 14);
            this.label9.TabIndex = 14;
            this.label9.Text = "RunMode";
            // 
            // cbxRunMode
            // 
            this.cbxRunMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxRunMode.FormattingEnabled = true;
            this.cbxRunMode.Location = new System.Drawing.Point(75, 369);
            this.cbxRunMode.Name = "cbxRunMode";
            this.cbxRunMode.Size = new System.Drawing.Size(121, 22);
            this.cbxRunMode.TabIndex = 13;
            // 
            // lblLeftSmemaOutSHow
            // 
            this.lblLeftSmemaOutSHow.AutoSize = true;
            this.lblLeftSmemaOutSHow.Location = new System.Drawing.Point(12, 84);
            this.lblLeftSmemaOutSHow.Name = "lblLeftSmemaOutSHow";
            this.lblLeftSmemaOutSHow.Size = new System.Drawing.Size(38, 14);
            this.lblLeftSmemaOutSHow.TabIndex = 97;
            this.lblLeftSmemaOutSHow.Text = "UPH :";
            this.lblLeftSmemaOutSHow.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 14);
            this.label3.TabIndex = 94;
            this.label3.Text = "Gantry Status : ";
            // 
            // lblLeftOutBdReady
            // 
            this.lblLeftOutBdReady.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLeftOutBdReady.Location = new System.Drawing.Point(121, 107);
            this.lblLeftOutBdReady.Margin = new System.Windows.Forms.Padding(3);
            this.lblLeftOutBdReady.Name = "lblLeftOutBdReady";
            this.lblLeftOutBdReady.Size = new System.Drawing.Size(100, 20);
            this.lblLeftOutBdReady.TabIndex = 95;
            this.lblLeftOutBdReady.Text = "[Out] BdReady";
            this.lblLeftOutBdReady.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLeftOutBdReady.Visible = false;
            // 
            // lblLeftOutMcReady
            // 
            this.lblLeftOutMcReady.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLeftOutMcReady.Location = new System.Drawing.Point(121, 81);
            this.lblLeftOutMcReady.Margin = new System.Windows.Forms.Padding(3);
            this.lblLeftOutMcReady.Name = "lblLeftOutMcReady";
            this.lblLeftOutMcReady.Size = new System.Drawing.Size(100, 20);
            this.lblLeftOutMcReady.TabIndex = 96;
            this.lblLeftOutMcReady.Text = "[IN] McReady";
            this.lblLeftOutMcReady.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLeftOutMcReady.Visible = false;
            // 
            // lblLFConvStatus
            // 
            this.lblLFConvStatus.AutoSize = true;
            this.lblLFConvStatus.Location = new System.Drawing.Point(12, 9);
            this.lblLFConvStatus.Name = "lblLFConvStatus";
            this.lblLFConvStatus.Size = new System.Drawing.Size(105, 14);
            this.lblLFConvStatus.TabIndex = 92;
            this.lblLFConvStatus.Text = "Conveyor Status :";
            // 
            // lblLFBDStatus
            // 
            this.lblLFBDStatus.AutoSize = true;
            this.lblLFBDStatus.Location = new System.Drawing.Point(12, 32);
            this.lblLFBDStatus.Name = "lblLFBDStatus";
            this.lblLFBDStatus.Size = new System.Drawing.Size(89, 14);
            this.lblLFBDStatus.TabIndex = 93;
            this.lblLFBDStatus.Text = "Board Status : ";
            // 
            // lblLeftBdReady
            // 
            this.lblLeftBdReady.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLeftBdReady.Location = new System.Drawing.Point(237, 55);
            this.lblLeftBdReady.Margin = new System.Windows.Forms.Padding(3);
            this.lblLeftBdReady.Name = "lblLeftBdReady";
            this.lblLeftBdReady.Size = new System.Drawing.Size(100, 20);
            this.lblLeftBdReady.TabIndex = 88;
            this.lblLeftBdReady.Text = "Right Gantry";
            this.lblLeftBdReady.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLeftConvStatus
            // 
            this.lblLeftConvStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLeftConvStatus.Location = new System.Drawing.Point(121, 3);
            this.lblLeftConvStatus.Margin = new System.Windows.Forms.Padding(3);
            this.lblLeftConvStatus.Name = "lblLeftConvStatus";
            this.lblLeftConvStatus.Size = new System.Drawing.Size(100, 20);
            this.lblLeftConvStatus.TabIndex = 91;
            this.lblLeftConvStatus.Text = "[In] BdReady";
            this.lblLeftConvStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLeftMcReady
            // 
            this.lblLeftMcReady.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLeftMcReady.Location = new System.Drawing.Point(121, 55);
            this.lblLeftMcReady.Margin = new System.Windows.Forms.Padding(3);
            this.lblLeftMcReady.Name = "lblLeftMcReady";
            this.lblLeftMcReady.Size = new System.Drawing.Size(100, 20);
            this.lblLeftMcReady.TabIndex = 89;
            this.lblLeftMcReady.Text = "Left Gantry";
            this.lblLeftMcReady.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLeftBdStatus
            // 
            this.lblLeftBdStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLeftBdStatus.Location = new System.Drawing.Point(121, 29);
            this.lblLeftBdStatus.Margin = new System.Windows.Forms.Padding(3);
            this.lblLeftBdStatus.Name = "lblLeftBdStatus";
            this.lblLeftBdStatus.Size = new System.Drawing.Size(100, 20);
            this.lblLeftBdStatus.TabIndex = 90;
            this.lblLeftBdStatus.Text = "[In] BdReady";
            this.lblLeftBdStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 14);
            this.label1.TabIndex = 98;
            this.label1.Text = "Procession Time :";
            this.label1.Visible = false;
            // 
            // lblRightBdStatus
            // 
            this.lblRightBdStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRightBdStatus.Location = new System.Drawing.Point(237, 29);
            this.lblRightBdStatus.Margin = new System.Windows.Forms.Padding(3);
            this.lblRightBdStatus.Name = "lblRightBdStatus";
            this.lblRightBdStatus.Size = new System.Drawing.Size(100, 20);
            this.lblRightBdStatus.TabIndex = 99;
            this.lblRightBdStatus.Text = "[In] BdReady";
            this.lblRightBdStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmrDisplay
            // 
            this.tmrDisplay.Tick += new System.EventHandler(this.tmrDisplay_Tick);
            // 
            // lblRightConvStatus
            // 
            this.lblRightConvStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRightConvStatus.Location = new System.Drawing.Point(237, 3);
            this.lblRightConvStatus.Margin = new System.Windows.Forms.Padding(3);
            this.lblRightConvStatus.Name = "lblRightConvStatus";
            this.lblRightConvStatus.Size = new System.Drawing.Size(100, 20);
            this.lblRightConvStatus.TabIndex = 100;
            this.lblRightConvStatus.Text = "[In] BdReady";
            this.lblRightConvStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmr1s
            // 
            this.tmr1s.Tick += new System.EventHandler(this.tmr1s_Tick);
            // 
            // frmAuto
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(648, 443);
            this.Controls.Add(this.lblRightConvStatus);
            this.Controls.Add(this.lblRightBdStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblLeftSmemaOutSHow);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblLeftOutBdReady);
            this.Controls.Add(this.lblLeftOutMcReady);
            this.Controls.Add(this.lblLFConvStatus);
            this.Controls.Add(this.lblLFBDStatus);
            this.Controls.Add(this.lblLeftBdReady);
            this.Controls.Add(this.lblLeftConvStatus);
            this.Controls.Add(this.lblLeftMcReady);
            this.Controls.Add(this.lblLeftBdStatus);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.cbxRunMode);
            this.Controls.Add(this.label9);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.Name = "frmAuto";
            this.Text = "frmAuto";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAuto_FormClosing);
            this.Load += new System.EventHandler(this.frmAuto_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox cbContinuous;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbxRunMode;
        private System.Windows.Forms.Label lblLeftSmemaOutSHow;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblLeftOutBdReady;
        private System.Windows.Forms.Label lblLeftOutMcReady;
        private System.Windows.Forms.Label lblLFConvStatus;
        private System.Windows.Forms.Label lblLFBDStatus;
        private System.Windows.Forms.Label lblLeftBdReady;
        public System.Windows.Forms.Label lblLeftConvStatus;
        private System.Windows.Forms.Label lblLeftMcReady;
        public System.Windows.Forms.Label lblLeftBdStatus;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label lblRightBdStatus;
        private System.Windows.Forms.Timer tmrDisplay;
        public System.Windows.Forms.Label lblRightConvStatus;
        private System.Windows.Forms.Button btn_SetCnyStatusReady;
        private System.Windows.Forms.Timer tmr1s;
    }
}