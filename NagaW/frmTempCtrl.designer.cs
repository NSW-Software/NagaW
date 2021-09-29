
namespace NagaW
{
    partial class frmTempCtrl
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cbxTempPrompt = new System.Windows.Forms.CheckBox();
            this.lblAwaitErrorTime = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTimeToIdle = new System.Windows.Forms.Label();
            this.lblTimetoidleMinute = new System.Windows.Forms.Label();
            this.lblAwaitTimeMin = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cbxTempPrompt
            // 
            this.cbxTempPrompt.AutoSize = true;
            this.cbxTempPrompt.Location = new System.Drawing.Point(12, 27);
            this.cbxTempPrompt.Name = "cbxTempPrompt";
            this.cbxTempPrompt.Size = new System.Drawing.Size(168, 18);
            this.cbxTempPrompt.TabIndex = 11;
            this.cbxTempPrompt.Text = "Prompt Wait Temp Setup";
            this.cbxTempPrompt.UseVisualStyleBackColor = true;
            this.cbxTempPrompt.Click += new System.EventHandler(this.cbxTempPrompt_Click);
            // 
            // lblAwaitErrorTime
            // 
            this.lblAwaitErrorTime.BackColor = System.Drawing.Color.White;
            this.lblAwaitErrorTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAwaitErrorTime.Location = new System.Drawing.Point(301, 22);
            this.lblAwaitErrorTime.Name = "lblAwaitErrorTime";
            this.lblAwaitErrorTime.Size = new System.Drawing.Size(93, 30);
            this.lblAwaitErrorTime.TabIndex = 12;
            this.lblAwaitErrorTime.Text = "label1";
            this.lblAwaitErrorTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAwaitErrorTime.Click += new System.EventHandler(this.lblAwaitErrorTime_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.lblAwaitTimeMin);
            this.groupBox1.Controls.Add(this.lblTimetoidleMinute);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblTimeToIdle);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblAwaitErrorTime);
            this.groupBox1.Controls.Add(this.cbxTempPrompt);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 377);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(725, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Setting";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(215, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 30);
            this.label1.TabIndex = 13;
            this.label1.Text = "Await Time";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(420, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 30);
            this.label2.TabIndex = 15;
            this.label2.Text = "Time To Idle";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTimeToIdle
            // 
            this.lblTimeToIdle.BackColor = System.Drawing.Color.White;
            this.lblTimeToIdle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTimeToIdle.Location = new System.Drawing.Point(506, 22);
            this.lblTimeToIdle.Name = "lblTimeToIdle";
            this.lblTimeToIdle.Size = new System.Drawing.Size(93, 30);
            this.lblTimeToIdle.TabIndex = 14;
            this.lblTimeToIdle.Text = "label1";
            this.lblTimeToIdle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTimeToIdle.Click += new System.EventHandler(this.lblTimeToIdle_Click);
            // 
            // lblTimetoidleMinute
            // 
            this.lblTimetoidleMinute.Location = new System.Drawing.Point(506, 52);
            this.lblTimetoidleMinute.Name = "lblTimetoidleMinute";
            this.lblTimetoidleMinute.Size = new System.Drawing.Size(80, 30);
            this.lblTimetoidleMinute.TabIndex = 16;
            this.lblTimetoidleMinute.Text = "Time To Idle";
            this.lblTimetoidleMinute.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAwaitTimeMin
            // 
            this.lblAwaitTimeMin.Location = new System.Drawing.Point(301, 52);
            this.lblAwaitTimeMin.Name = "lblAwaitTimeMin";
            this.lblAwaitTimeMin.Size = new System.Drawing.Size(80, 30);
            this.lblAwaitTimeMin.TabIndex = 17;
            this.lblAwaitTimeMin.Text = "Time To Idle";
            this.lblAwaitTimeMin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmTempCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 477);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.Name = "frmTempCtrl";
            this.Text = "frmTempCtrl";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTempCtrl_FormClosing);
            this.Load += new System.EventHandler(this.frmTempCtrl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox cbxTempPrompt;
        private System.Windows.Forms.Label lblAwaitErrorTime;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTimeToIdle;
        private System.Windows.Forms.Label lblTimetoidleMinute;
        private System.Windows.Forms.Label lblAwaitTimeMin;
    }
}