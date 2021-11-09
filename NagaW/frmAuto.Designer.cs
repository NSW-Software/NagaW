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
            this.tmr1s = new System.Windows.Forms.Timer(this.components);
            this.label13 = new System.Windows.Forms.Label();
            this.lblWaferInput = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblIntervalTimeout = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 252);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(420, 46);
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
            // tmr1s
            // 
            this.tmr1s.Tick += new System.EventHandler(this.tmr1s_Tick);
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
            this.label1.Size = new System.Drawing.Size(178, 25);
            this.label1.TabIndex = 28;
            this.label1.Text = "(Set 0 for infinite mode)";
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
            this.panel1.Location = new System.Drawing.Point(0, 217);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(420, 35);
            this.panel1.TabIndex = 31;
            // 
            // frmAuto
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(420, 298);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblIntervalTimeout);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.lblWaferInput);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.Name = "frmAuto";
            this.Text = "frmAuto";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAuto_FormClosing);
            this.Load += new System.EventHandler(this.frmAuto_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.Timer tmr1s;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblWaferInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblIntervalTimeout;
        private System.Windows.Forms.Panel panel1;
    }
}