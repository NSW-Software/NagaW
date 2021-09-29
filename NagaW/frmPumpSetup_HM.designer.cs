namespace NagaW
{
    partial class frmPumpSetup_HM
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
            this.lblDispAmt = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblBSuckAmt = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblDispSpeed = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lblBSuckSpeed = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDispAcc = new System.Windows.Forms.Label();
            this.btnTriggerH = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblFPress = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblVacDur = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblShotCount = new System.Windows.Forms.Label();
            this.btnTrigger = new System.Windows.Forms.Button();
            this.btnShot = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnVacuum = new System.Windows.Forms.Button();
            this.btnFPress = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 23);
            this.label1.TabIndex = 51;
            this.label1.Text = "Disp Time";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDispAmt
            // 
            this.lblDispAmt.BackColor = System.Drawing.Color.White;
            this.lblDispAmt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDispAmt.Location = new System.Drawing.Point(132, 25);
            this.lblDispAmt.Margin = new System.Windows.Forms.Padding(3);
            this.lblDispAmt.Name = "lblDispAmt";
            this.lblDispAmt.Size = new System.Drawing.Size(100, 23);
            this.lblDispAmt.TabIndex = 58;
            this.lblDispAmt.Text = "99.99";
            this.lblDispAmt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDispAmt.Click += new System.EventHandler(this.lblDispAmt_Click);
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(6, 54);
            this.label18.Margin = new System.Windows.Forms.Padding(3);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(120, 23);
            this.label18.TabIndex = 52;
            this.label18.Text = "BSuck Time";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBSuckAmt
            // 
            this.lblBSuckAmt.BackColor = System.Drawing.Color.White;
            this.lblBSuckAmt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblBSuckAmt.Location = new System.Drawing.Point(131, 54);
            this.lblBSuckAmt.Margin = new System.Windows.Forms.Padding(3);
            this.lblBSuckAmt.Name = "lblBSuckAmt";
            this.lblBSuckAmt.Size = new System.Drawing.Size(100, 23);
            this.lblBSuckAmt.TabIndex = 50;
            this.lblBSuckAmt.Text = "99.99";
            this.lblBSuckAmt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblBSuckAmt.Click += new System.EventHandler(this.lblBSuckAmt_Click);
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(6, 83);
            this.label13.Margin = new System.Windows.Forms.Padding(3);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(120, 23);
            this.label13.TabIndex = 53;
            this.label13.Text = "Disp Speed";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDispSpeed
            // 
            this.lblDispSpeed.BackColor = System.Drawing.Color.White;
            this.lblDispSpeed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDispSpeed.Location = new System.Drawing.Point(132, 83);
            this.lblDispSpeed.Margin = new System.Windows.Forms.Padding(3);
            this.lblDispSpeed.Name = "lblDispSpeed";
            this.lblDispSpeed.Size = new System.Drawing.Size(100, 23);
            this.lblDispSpeed.TabIndex = 49;
            this.lblDispSpeed.Text = "99.99";
            this.lblDispSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDispSpeed.Click += new System.EventHandler(this.lblDispSpeed_Click);
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(6, 112);
            this.label17.Margin = new System.Windows.Forms.Padding(3);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(120, 23);
            this.label17.TabIndex = 55;
            this.label17.Text = "BSuck Speed";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBSuckSpeed
            // 
            this.lblBSuckSpeed.BackColor = System.Drawing.Color.White;
            this.lblBSuckSpeed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblBSuckSpeed.Location = new System.Drawing.Point(131, 112);
            this.lblBSuckSpeed.Margin = new System.Windows.Forms.Padding(3);
            this.lblBSuckSpeed.Name = "lblBSuckSpeed";
            this.lblBSuckSpeed.Size = new System.Drawing.Size(100, 23);
            this.lblBSuckSpeed.TabIndex = 54;
            this.lblBSuckSpeed.Text = "99.99";
            this.lblBSuckSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblBSuckSpeed.Click += new System.EventHandler(this.lblBSuckSpeed_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 23);
            this.label2.TabIndex = 61;
            this.label2.Text = "Accel";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDispAcc
            // 
            this.lblDispAcc.BackColor = System.Drawing.Color.White;
            this.lblDispAcc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDispAcc.Location = new System.Drawing.Point(132, 25);
            this.lblDispAcc.Margin = new System.Windows.Forms.Padding(3);
            this.lblDispAcc.Name = "lblDispAcc";
            this.lblDispAcc.Size = new System.Drawing.Size(100, 23);
            this.lblDispAcc.TabIndex = 66;
            this.lblDispAcc.Text = "99.99";
            this.lblDispAcc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDispAcc.Click += new System.EventHandler(this.lblDispAcc_Click);
            // 
            // btnTriggerH
            // 
            this.btnTriggerH.Location = new System.Drawing.Point(6, 130);
            this.btnTriggerH.Name = "btnTriggerH";
            this.btnTriggerH.Size = new System.Drawing.Size(90, 35);
            this.btnTriggerH.TabIndex = 0;
            this.btnTriggerH.Text = "Trigger H";
            this.btnTriggerH.UseVisualStyleBackColor = true;
            this.btnTriggerH.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnTriggerH_MouseDown);
            this.btnTriggerH.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnTriggerH_MouseUp);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblBSuckSpeed);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.lblDispAmt);
            this.groupBox1.Controls.Add(this.lblBSuckAmt);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.lblDispSpeed);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(238, 156);
            this.groupBox1.TabIndex = 61;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Operation";
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox2.Controls.Add(this.lblFPress);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.lblVacDur);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.lblDispAcc);
            this.groupBox2.Location = new System.Drawing.Point(11, 202);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(470, 98);
            this.groupBox2.TabIndex = 62;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Parameter";
            // 
            // lblFPress
            // 
            this.lblFPress.BackColor = System.Drawing.Color.White;
            this.lblFPress.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFPress.Location = new System.Drawing.Point(364, 54);
            this.lblFPress.Margin = new System.Windows.Forms.Padding(3);
            this.lblFPress.Name = "lblFPress";
            this.lblFPress.Size = new System.Drawing.Size(100, 23);
            this.lblFPress.TabIndex = 75;
            this.lblFPress.Text = "99.99";
            this.lblFPress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFPress.Click += new System.EventHandler(this.lblFPress_Click);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(242, 54);
            this.label6.Margin = new System.Windows.Forms.Padding(3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 23);
            this.label6.TabIndex = 76;
            this.label6.Text = "FPress";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblVacDur
            // 
            this.lblVacDur.BackColor = System.Drawing.Color.White;
            this.lblVacDur.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblVacDur.Location = new System.Drawing.Point(364, 25);
            this.lblVacDur.Margin = new System.Windows.Forms.Padding(3);
            this.lblVacDur.Name = "lblVacDur";
            this.lblVacDur.Size = new System.Drawing.Size(100, 23);
            this.lblVacDur.TabIndex = 73;
            this.lblVacDur.Text = "99.99";
            this.lblVacDur.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblVacDur.Click += new System.EventHandler(this.lblVacDur_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(238, 25);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 23);
            this.label4.TabIndex = 74;
            this.label4.Text = "Vac Dur";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblShotCount);
            this.groupBox3.Controls.Add(this.btnTrigger);
            this.groupBox3.Controls.Add(this.btnShot);
            this.groupBox3.Controls.Add(this.btnTriggerH);
            this.groupBox3.Location = new System.Drawing.Point(256, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(105, 181);
            this.groupBox3.TabIndex = 63;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Function";
            // 
            // lblShotCount
            // 
            this.lblShotCount.BackColor = System.Drawing.Color.White;
            this.lblShotCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblShotCount.Location = new System.Drawing.Point(6, 18);
            this.lblShotCount.Margin = new System.Windows.Forms.Padding(3);
            this.lblShotCount.Name = "lblShotCount";
            this.lblShotCount.Size = new System.Drawing.Size(90, 23);
            this.lblShotCount.TabIndex = 59;
            this.lblShotCount.Text = "99.99";
            this.lblShotCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblShotCount.Click += new System.EventHandler(this.lblShotCount_Click);
            // 
            // btnTrigger
            // 
            this.btnTrigger.Location = new System.Drawing.Point(6, 89);
            this.btnTrigger.Name = "btnTrigger";
            this.btnTrigger.Size = new System.Drawing.Size(90, 35);
            this.btnTrigger.TabIndex = 2;
            this.btnTrigger.Text = "Trigger";
            this.btnTrigger.UseVisualStyleBackColor = true;
            this.btnTrigger.Click += new System.EventHandler(this.btnTrigger_Click);
            // 
            // btnShot
            // 
            this.btnShot.Location = new System.Drawing.Point(6, 47);
            this.btnShot.Name = "btnShot";
            this.btnShot.Size = new System.Drawing.Size(90, 36);
            this.btnShot.TabIndex = 1;
            this.btnShot.Text = "Shot";
            this.btnShot.UseVisualStyleBackColor = true;
            this.btnShot.Click += new System.EventHandler(this.btnShot_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnVacuum);
            this.groupBox4.Controls.Add(this.btnFPress);
            this.groupBox4.Location = new System.Drawing.Point(367, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(114, 181);
            this.groupBox4.TabIndex = 64;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "IO";
            // 
            // btnVacuum
            // 
            this.btnVacuum.Location = new System.Drawing.Point(8, 63);
            this.btnVacuum.Name = "btnVacuum";
            this.btnVacuum.Size = new System.Drawing.Size(90, 35);
            this.btnVacuum.TabIndex = 1;
            this.btnVacuum.Text = "Vacuum";
            this.btnVacuum.UseVisualStyleBackColor = true;
            this.btnVacuum.Click += new System.EventHandler(this.btnVacuum_Click);
            // 
            // btnFPress
            // 
            this.btnFPress.Location = new System.Drawing.Point(8, 22);
            this.btnFPress.Name = "btnFPress";
            this.btnFPress.Size = new System.Drawing.Size(90, 35);
            this.btnFPress.TabIndex = 0;
            this.btnFPress.Text = "FPress";
            this.btnFPress.UseVisualStyleBackColor = true;
            this.btnFPress.Click += new System.EventHandler(this.btFPress_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmPumpSetup_HM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 403);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.Name = "frmPumpSetup_HM";
            this.Text = "frmHeliMasterSetup";
            this.Load += new System.EventHandler(this.frmHeliMasterSetup_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDispAmt;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblBSuckAmt;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblDispSpeed;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblBSuckSpeed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDispAcc;
        private System.Windows.Forms.Button btnTriggerH;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblVacDur;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblFPress;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnFPress;
        private System.Windows.Forms.Button btnVacuum;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnShot;
        private System.Windows.Forms.Button btnTrigger;
        private System.Windows.Forms.Label lblShotCount;
    }
}