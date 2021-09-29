namespace NagaW
{
    partial class frmPrompt
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
            this.btnClear = new System.Windows.Forms.Button();
            this.rtbx = new System.Windows.Forms.RichTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnClear.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnClear.Location = new System.Drawing.Point(10, 96);
            this.btnClear.Margin = new System.Windows.Forms.Padding(5);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(270, 60);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // rtbx
            // 
            this.rtbx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbx.Location = new System.Drawing.Point(10, 10);
            this.rtbx.Name = "rtbx";
            this.rtbx.ReadOnly = true;
            this.rtbx.Size = new System.Drawing.Size(270, 86);
            this.rtbx.TabIndex = 2;
            this.rtbx.Text = "";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmPrompt
            // 
            this.AcceptButton = this.btnClear;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 166);
            this.Controls.Add(this.rtbx);
            this.Controls.Add(this.btnClear);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmPrompt";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "frmPrompt";
            this.Load += new System.EventHandler(this.frmPrompt_Load);
            this.Shown += new System.EventHandler(this.frmPrompt_Shown);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.RichTextBox rtbx;
        private System.Windows.Forms.Timer timer1;
    }
}