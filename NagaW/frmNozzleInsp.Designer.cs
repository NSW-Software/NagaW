
namespace NagaW
{
    partial class frmNozzleInsp
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
            this.cbxDO = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxStatusOK = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblWaitTime = new System.Windows.Forms.Label();
            this.btnExecute = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lblPos = new System.Windows.Forms.Label();
            this.btnSetPos = new System.Windows.Forms.Button();
            this.btnGotoPos = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.btnTrigIO = new System.Windows.Forms.Button();
            this.lblStatusOK = new System.Windows.Forms.Label();
            this.cbxStatusNG = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblStatusNG = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbxDO
            // 
            this.cbxDO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDO.FormattingEnabled = true;
            this.cbxDO.Location = new System.Drawing.Point(119, 52);
            this.cbxDO.Name = "cbxDO";
            this.cbxDO.Size = new System.Drawing.Size(191, 26);
            this.cbxDO.TabIndex = 0;
            this.cbxDO.SelectionChangeCommitted += new System.EventHandler(this.cbxDO_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 48);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 30);
            this.label1.TabIndex = 1;
            this.label1.Text = "Trigger IO";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 85);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 30);
            this.label2.TabIndex = 2;
            this.label2.Text = "Status OK";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbxStatusOK
            // 
            this.cbxStatusOK.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxStatusOK.FormattingEnabled = true;
            this.cbxStatusOK.Location = new System.Drawing.Point(119, 85);
            this.cbxStatusOK.Name = "cbxStatusOK";
            this.cbxStatusOK.Size = new System.Drawing.Size(191, 26);
            this.cbxStatusOK.TabIndex = 3;
            this.cbxStatusOK.SelectionChangeCommitted += new System.EventHandler(this.cbxStatusOK_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 167);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 30);
            this.label3.TabIndex = 4;
            this.label3.Text = "WaitTime";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblWaitTime
            // 
            this.lblWaitTime.BackColor = System.Drawing.Color.White;
            this.lblWaitTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblWaitTime.Location = new System.Drawing.Point(119, 170);
            this.lblWaitTime.Margin = new System.Windows.Forms.Padding(3);
            this.lblWaitTime.Name = "lblWaitTime";
            this.lblWaitTime.Size = new System.Drawing.Size(121, 25);
            this.lblWaitTime.TabIndex = 5;
            this.lblWaitTime.Text = "WaitTime";
            this.lblWaitTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblWaitTime.Click += new System.EventHandler(this.lblWaitTime_Click);
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(409, 170);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(90, 40);
            this.btnExecute.TabIndex = 6;
            this.btnExecute.Text = "Execute";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(12, 12);
            this.label5.Margin = new System.Windows.Forms.Padding(3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 30);
            this.label5.TabIndex = 7;
            this.label5.Text = "NozzlePos";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPos
            // 
            this.lblPos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPos.Location = new System.Drawing.Point(119, 15);
            this.lblPos.Margin = new System.Windows.Forms.Padding(3);
            this.lblPos.Name = "lblPos";
            this.lblPos.Size = new System.Drawing.Size(191, 25);
            this.lblPos.TabIndex = 8;
            this.lblPos.Text = "Pos";
            this.lblPos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSetPos
            // 
            this.btnSetPos.Location = new System.Drawing.Point(316, 15);
            this.btnSetPos.Name = "btnSetPos";
            this.btnSetPos.Size = new System.Drawing.Size(90, 25);
            this.btnSetPos.TabIndex = 9;
            this.btnSetPos.Text = "Set";
            this.btnSetPos.UseVisualStyleBackColor = true;
            this.btnSetPos.Click += new System.EventHandler(this.btnSetPos_Click);
            // 
            // btnGotoPos
            // 
            this.btnGotoPos.Location = new System.Drawing.Point(412, 15);
            this.btnGotoPos.Name = "btnGotoPos";
            this.btnGotoPos.Size = new System.Drawing.Size(90, 25);
            this.btnGotoPos.TabIndex = 10;
            this.btnGotoPos.Text = "Goto";
            this.btnGotoPos.UseVisualStyleBackColor = true;
            this.btnGotoPos.Click += new System.EventHandler(this.btnGotoPos_Click);
            // 
            // lblResult
            // 
            this.lblResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblResult.Location = new System.Drawing.Point(409, 216);
            this.lblResult.Margin = new System.Windows.Forms.Padding(3);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(90, 30);
            this.lblResult.TabIndex = 11;
            this.lblResult.Text = "Pass/Fail";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnTrigIO
            // 
            this.btnTrigIO.Location = new System.Drawing.Point(316, 52);
            this.btnTrigIO.Name = "btnTrigIO";
            this.btnTrigIO.Size = new System.Drawing.Size(90, 26);
            this.btnTrigIO.TabIndex = 12;
            this.btnTrigIO.Text = "Toggle DO";
            this.btnTrigIO.UseVisualStyleBackColor = true;
            this.btnTrigIO.Click += new System.EventHandler(this.btnTrigIO_Click);
            // 
            // lblStatusOK
            // 
            this.lblStatusOK.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStatusOK.Location = new System.Drawing.Point(316, 85);
            this.lblStatusOK.Margin = new System.Windows.Forms.Padding(3);
            this.lblStatusOK.Name = "lblStatusOK";
            this.lblStatusOK.Size = new System.Drawing.Size(90, 26);
            this.lblStatusOK.TabIndex = 13;
            this.lblStatusOK.Text = "DI";
            this.lblStatusOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbxStatusNG
            // 
            this.cbxStatusNG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxStatusNG.FormattingEnabled = true;
            this.cbxStatusNG.Location = new System.Drawing.Point(119, 121);
            this.cbxStatusNG.Name = "cbxStatusNG";
            this.cbxStatusNG.Size = new System.Drawing.Size(191, 26);
            this.cbxStatusNG.TabIndex = 15;
            this.cbxStatusNG.SelectionChangeCommitted += new System.EventHandler(this.cbxStatusNG_SelectionChangeCommitted);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 121);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 30);
            this.label4.TabIndex = 14;
            this.label4.Text = "Status NG";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatusNG
            // 
            this.lblStatusNG.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStatusNG.Location = new System.Drawing.Point(316, 121);
            this.lblStatusNG.Margin = new System.Windows.Forms.Padding(3);
            this.lblStatusNG.Name = "lblStatusNG";
            this.lblStatusNG.Size = new System.Drawing.Size(90, 26);
            this.lblStatusNG.TabIndex = 16;
            this.lblStatusNG.Text = "DI";
            this.lblStatusNG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmNozzleInsp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 277);
            this.Controls.Add(this.lblStatusNG);
            this.Controls.Add(this.cbxStatusNG);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblStatusOK);
            this.Controls.Add(this.btnTrigIO);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.btnGotoPos);
            this.Controls.Add(this.btnSetPos);
            this.Controls.Add(this.lblPos);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.lblWaitTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbxStatusOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxDO);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.Name = "frmNozzleInsp";
            this.Text = "frmNozzleInsp";
            this.Load += new System.EventHandler(this.frmNozzleInsp_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxDO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxStatusOK;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblWaitTime;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblPos;
        private System.Windows.Forms.Button btnSetPos;
        private System.Windows.Forms.Button btnGotoPos;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Button btnTrigIO;
        private System.Windows.Forms.Label lblStatusOK;
        private System.Windows.Forms.ComboBox cbxStatusNG;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblStatusNG;
    }
}