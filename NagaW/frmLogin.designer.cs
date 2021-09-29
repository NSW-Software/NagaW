namespace NagaW
{
    partial class frmLogin
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
            this.tboxPassword = new System.Windows.Forms.MaskedTextBox();
            this.tboxName = new System.Windows.Forms.TextBox();
            this.btnSignIn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxLevel = new System.Windows.Forms.ComboBox();
            this.btnExitProgram = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tboxPassword
            // 
            this.tboxPassword.Location = new System.Drawing.Point(81, 73);
            this.tboxPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tboxPassword.Name = "tboxPassword";
            this.tboxPassword.PasswordChar = '*';
            this.tboxPassword.Size = new System.Drawing.Size(121, 26);
            this.tboxPassword.TabIndex = 2;
            this.tboxPassword.UseSystemPasswordChar = true;
            // 
            // tboxName
            // 
            this.tboxName.Location = new System.Drawing.Point(81, 39);
            this.tboxName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tboxName.Name = "tboxName";
            this.tboxName.Size = new System.Drawing.Size(121, 26);
            this.tboxName.TabIndex = 1;
            // 
            // btnSignIn
            // 
            this.btnSignIn.Location = new System.Drawing.Point(239, 65);
            this.btnSignIn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSignIn.Name = "btnSignIn";
            this.btnSignIn.Size = new System.Drawing.Size(85, 45);
            this.btnSignIn.TabIndex = 3;
            this.btnSignIn.Text = "Sign In";
            this.btnSignIn.UseVisualStyleBackColor = true;
            this.btnSignIn.Click += new System.EventHandler(this.btnSignIn_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label3);
            this.flowLayoutPanel1.Controls.Add(this.cbxLevel);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.tboxName);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.tboxPassword);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(8, 8);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(3);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(225, 107);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "Level";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbxLevel
            // 
            this.cbxLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLevel.FormattingEnabled = true;
            this.cbxLevel.Location = new System.Drawing.Point(81, 6);
            this.cbxLevel.Name = "cbxLevel";
            this.cbxLevel.Size = new System.Drawing.Size(121, 26);
            this.cbxLevel.TabIndex = 0;
            // 
            // btnExitProgram
            // 
            this.btnExitProgram.Location = new System.Drawing.Point(239, 14);
            this.btnExitProgram.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExitProgram.Name = "btnExitProgram";
            this.btnExitProgram.Size = new System.Drawing.Size(85, 45);
            this.btnExitProgram.TabIndex = 6;
            this.btnExitProgram.Text = "Exit Program";
            this.btnExitProgram.UseVisualStyleBackColor = true;
            this.btnExitProgram.Click += new System.EventHandler(this.btnExitProgram_Click);
            // 
            // frmLogin
            // 
            this.AcceptButton = this.btnSignIn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(332, 119);
            this.Controls.Add(this.btnExitProgram);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.btnSignIn);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmLogin";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "frmLogin";
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox tboxPassword;
        private System.Windows.Forms.TextBox tboxName;
        private System.Windows.Forms.Button btnSignIn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ComboBox cbxLevel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnExitProgram;
    }
}