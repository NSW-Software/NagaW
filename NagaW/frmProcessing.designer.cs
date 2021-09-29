namespace NagaW
{
    partial class frmProcessing
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblMsg = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(7, 31);
            this.progressBar1.MarqueeAnimationSpeed = 20;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(323, 25);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 0;
            // 
            // lblTime
            // 
            this.lblTime.Location = new System.Drawing.Point(4, 59);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(187, 47);
            this.lblTime.TabIndex = 1;
            this.lblTime.Text = "00:00:00";
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMsg.Location = new System.Drawing.Point(10, 10);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(38, 14);
            this.lblMsg.TabIndex = 2;
            this.lblMsg.Text = "label1";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(234, 62);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 38);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmProcessing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 113);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblMsg);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.Name = "frmProcessing";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "frmProcessing";
            this.Load += new System.EventHandler(this.frmProcessing_Load);
            this.Shown += new System.EventHandler(this.frmProcessing_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnCancel;
    }
}