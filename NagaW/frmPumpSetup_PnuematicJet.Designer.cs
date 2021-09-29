
namespace NagaW
{
    partial class frmPumpSetup_PneumaticJet
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
            this.lblFPress = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblVPress = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblDisptime = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblOfftime = new System.Windows.Forms.Label();
            this.btnShot = new System.Windows.Forms.Button();
            this.btnFPressIO = new System.Windows.Forms.Button();
            this.btnVPressIO = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 34);
            this.label1.TabIndex = 54;
            this.label1.Text = "FPress";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFPress
            // 
            this.lblFPress.BackColor = System.Drawing.Color.White;
            this.lblFPress.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFPress.Location = new System.Drawing.Point(120, 12);
            this.lblFPress.Margin = new System.Windows.Forms.Padding(3);
            this.lblFPress.Name = "lblFPress";
            this.lblFPress.Size = new System.Drawing.Size(100, 34);
            this.lblFPress.TabIndex = 55;
            this.lblFPress.Text = "99.99";
            this.lblFPress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFPress.Click += new System.EventHandler(this.lblFPress_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 52);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 34);
            this.label2.TabIndex = 58;
            this.label2.Text = "JPress";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblVPress
            // 
            this.lblVPress.BackColor = System.Drawing.Color.White;
            this.lblVPress.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblVPress.Location = new System.Drawing.Point(120, 52);
            this.lblVPress.Margin = new System.Windows.Forms.Padding(3);
            this.lblVPress.Name = "lblVPress";
            this.lblVPress.Size = new System.Drawing.Size(100, 34);
            this.lblVPress.TabIndex = 59;
            this.lblVPress.Text = "99.99";
            this.lblVPress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblVPress.Click += new System.EventHandler(this.lblVPress_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 124);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 34);
            this.label4.TabIndex = 60;
            this.label4.Text = "OpenTime";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDisptime
            // 
            this.lblDisptime.BackColor = System.Drawing.Color.White;
            this.lblDisptime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDisptime.Location = new System.Drawing.Point(120, 124);
            this.lblDisptime.Margin = new System.Windows.Forms.Padding(3);
            this.lblDisptime.Name = "lblDisptime";
            this.lblDisptime.Size = new System.Drawing.Size(100, 34);
            this.lblDisptime.TabIndex = 61;
            this.lblDisptime.Text = "99.99";
            this.lblDisptime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDisptime.Click += new System.EventHandler(this.lblDisptime_Click);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(12, 164);
            this.label6.Margin = new System.Windows.Forms.Padding(3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 34);
            this.label6.TabIndex = 62;
            this.label6.Text = "OffTime";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOfftime
            // 
            this.lblOfftime.BackColor = System.Drawing.Color.White;
            this.lblOfftime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblOfftime.Location = new System.Drawing.Point(120, 164);
            this.lblOfftime.Margin = new System.Windows.Forms.Padding(3);
            this.lblOfftime.Name = "lblOfftime";
            this.lblOfftime.Size = new System.Drawing.Size(100, 34);
            this.lblOfftime.TabIndex = 63;
            this.lblOfftime.Text = "99.99";
            this.lblOfftime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblOfftime.Click += new System.EventHandler(this.lblOfftime_Click);
            // 
            // btnShot
            // 
            this.btnShot.Location = new System.Drawing.Point(226, 164);
            this.btnShot.Name = "btnShot";
            this.btnShot.Size = new System.Drawing.Size(100, 35);
            this.btnShot.TabIndex = 64;
            this.btnShot.Text = "Shot";
            this.btnShot.UseVisualStyleBackColor = true;
            this.btnShot.Click += new System.EventHandler(this.btnShot_Click);
            // 
            // btnFPressIO
            // 
            this.btnFPressIO.Location = new System.Drawing.Point(226, 12);
            this.btnFPressIO.Name = "btnFPressIO";
            this.btnFPressIO.Size = new System.Drawing.Size(100, 35);
            this.btnFPressIO.TabIndex = 65;
            this.btnFPressIO.Text = "FPress";
            this.btnFPressIO.UseVisualStyleBackColor = true;
            this.btnFPressIO.Click += new System.EventHandler(this.btnFPressIO_Click);
            // 
            // btnVPressIO
            // 
            this.btnVPressIO.Location = new System.Drawing.Point(226, 53);
            this.btnVPressIO.Name = "btnVPressIO";
            this.btnVPressIO.Size = new System.Drawing.Size(100, 35);
            this.btnVPressIO.TabIndex = 66;
            this.btnVPressIO.Text = "JPress";
            this.btnVPressIO.UseVisualStyleBackColor = true;
            this.btnVPressIO.Click += new System.EventHandler(this.btnVPressIO_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmPumpSetup_PneumaticJet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 206);
            this.Controls.Add(this.btnVPressIO);
            this.Controls.Add(this.btnFPressIO);
            this.Controls.Add(this.btnShot);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblOfftime);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblDisptime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblVPress);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFPress);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.Name = "frmPumpSetup_PneumaticJet";
            this.Text = "frmPumpSetup_PnuematicJet";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPumpSetup_PnuematicJet_FormClosing);
            this.Load += new System.EventHandler(this.frmPumpSetup_PneumaticJet_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFPress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblVPress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblDisptime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblOfftime;
        private System.Windows.Forms.Button btnShot;
        private System.Windows.Forms.Button btnFPressIO;
        private System.Windows.Forms.Button btnVPressIO;
        private System.Windows.Forms.Timer timer1;
    }
}