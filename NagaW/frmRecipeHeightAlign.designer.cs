namespace NagaW
{
    partial class frmRecipeHeightAlign
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
            this.btnGotoXY0 = new System.Windows.Forms.Button();
            this.btnSetXY0 = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblY0 = new System.Windows.Forms.Label();
            this.lblXY0 = new System.Windows.Forms.Label();
            this.lblX0 = new System.Windows.Forms.Label();
            this.btnExecute = new System.Windows.Forms.Button();
            this.rtbxResult = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnGotoXY0
            // 
            this.btnGotoXY0.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGotoXY0.ForeColor = System.Drawing.Color.Navy;
            this.btnGotoXY0.Location = new System.Drawing.Point(183, 68);
            this.btnGotoXY0.Name = "btnGotoXY0";
            this.btnGotoXY0.Size = new System.Drawing.Size(65, 23);
            this.btnGotoXY0.TabIndex = 43;
            this.btnGotoXY0.Text = "Goto";
            this.btnGotoXY0.UseVisualStyleBackColor = true;
            this.btnGotoXY0.Click += new System.EventHandler(this.btnGotoXY0_Click);
            // 
            // btnSetXY0
            // 
            this.btnSetXY0.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetXY0.ForeColor = System.Drawing.Color.Navy;
            this.btnSetXY0.Location = new System.Drawing.Point(127, 68);
            this.btnSetXY0.Name = "btnSetXY0";
            this.btnSetXY0.Size = new System.Drawing.Size(50, 23);
            this.btnSetXY0.TabIndex = 42;
            this.btnSetXY0.Text = "Set";
            this.btnSetXY0.UseVisualStyleBackColor = true;
            this.btnSetXY0.Click += new System.EventHandler(this.btnSetXY0_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Navy;
            this.lblTitle.Location = new System.Drawing.Point(6, 6);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(3);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(200, 25);
            this.lblTitle.TabIndex = 41;
            this.lblTitle.Text = "Title";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblY0
            // 
            this.lblY0.BackColor = System.Drawing.Color.White;
            this.lblY0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblY0.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblY0.ForeColor = System.Drawing.Color.Navy;
            this.lblY0.Location = new System.Drawing.Point(183, 37);
            this.lblY0.Margin = new System.Windows.Forms.Padding(3);
            this.lblY0.Name = "lblY0";
            this.lblY0.Size = new System.Drawing.Size(65, 25);
            this.lblY0.TabIndex = 40;
            this.lblY0.Text = "500";
            this.lblY0.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblY0.Click += new System.EventHandler(this.lblY0_Click);
            // 
            // lblXY0
            // 
            this.lblXY0.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblXY0.ForeColor = System.Drawing.Color.Navy;
            this.lblXY0.Location = new System.Drawing.Point(6, 37);
            this.lblXY0.Margin = new System.Windows.Forms.Padding(3);
            this.lblXY0.Name = "lblXY0";
            this.lblXY0.Size = new System.Drawing.Size(100, 25);
            this.lblXY0.TabIndex = 38;
            this.lblXY0.Text = "lblXY0";
            this.lblXY0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblX0
            // 
            this.lblX0.BackColor = System.Drawing.Color.White;
            this.lblX0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblX0.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblX0.ForeColor = System.Drawing.Color.Navy;
            this.lblX0.Location = new System.Drawing.Point(112, 37);
            this.lblX0.Margin = new System.Windows.Forms.Padding(3);
            this.lblX0.Name = "lblX0";
            this.lblX0.Size = new System.Drawing.Size(65, 25);
            this.lblX0.TabIndex = 39;
            this.lblX0.Text = "-999.999";
            this.lblX0.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblX0.Click += new System.EventHandler(this.lblX0_Click);
            // 
            // btnExecute
            // 
            this.btnExecute.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExecute.ForeColor = System.Drawing.Color.Navy;
            this.btnExecute.Location = new System.Drawing.Point(183, 128);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(65, 23);
            this.btnExecute.TabIndex = 44;
            this.btnExecute.Text = "Exec";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // rtbxResult
            // 
            this.rtbxResult.Location = new System.Drawing.Point(6, 157);
            this.rtbxResult.Name = "rtbxResult";
            this.rtbxResult.Size = new System.Drawing.Size(242, 83);
            this.rtbxResult.TabIndex = 53;
            this.rtbxResult.Text = "";
            // 
            // frmRecipeHeightAlign
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(269, 278);
            this.Controls.Add(this.rtbxResult);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.btnGotoXY0);
            this.Controls.Add(this.btnSetXY0);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblY0);
            this.Controls.Add(this.lblXY0);
            this.Controls.Add(this.lblX0);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.Name = "frmRecipeHeightAlign";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "frmRecipeHeightAlign";
            this.Load += new System.EventHandler(this.frmRecipeHeightAlign_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnGotoXY0;
        private System.Windows.Forms.Button btnSetXY0;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblY0;
        private System.Windows.Forms.Label lblXY0;
        private System.Windows.Forms.Label lblX0;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.RichTextBox rtbxResult;
    }
}