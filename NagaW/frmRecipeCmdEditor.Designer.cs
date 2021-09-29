
namespace NagaW
{
    partial class frmRecipeCmdEditor
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
            this.lboxCmdDic = new System.Windows.Forms.ListBox();
            this.btnDirToSel = new System.Windows.Forms.Button();
            this.lblDicCmd = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSelectedCmd = new System.Windows.Forms.Label();
            this.btnSelToDir = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnRemoveAll = new System.Windows.Forms.Button();
            this.lboxCmdSelected = new System.Windows.Forms.ListBox();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lboxCmdDic
            // 
            this.lboxCmdDic.Dock = System.Windows.Forms.DockStyle.Left;
            this.lboxCmdDic.FormattingEnabled = true;
            this.lboxCmdDic.ItemHeight = 18;
            this.lboxCmdDic.Location = new System.Drawing.Point(0, 32);
            this.lboxCmdDic.Name = "lboxCmdDic";
            this.lboxCmdDic.Size = new System.Drawing.Size(185, 495);
            this.lboxCmdDic.TabIndex = 0;
            // 
            // btnDirToSel
            // 
            this.btnDirToSel.Location = new System.Drawing.Point(191, 159);
            this.btnDirToSel.Name = "btnDirToSel";
            this.btnDirToSel.Size = new System.Drawing.Size(97, 34);
            this.btnDirToSel.TabIndex = 1;
            this.btnDirToSel.Text = ">";
            this.btnDirToSel.UseVisualStyleBackColor = true;
            this.btnDirToSel.Click += new System.EventHandler(this.btnDirToSel_Click);
            // 
            // lblDicCmd
            // 
            this.lblDicCmd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblDicCmd.Location = new System.Drawing.Point(0, 0);
            this.lblDicCmd.Name = "lblDicCmd";
            this.lblDicCmd.Size = new System.Drawing.Size(185, 32);
            this.lblDicCmd.TabIndex = 2;
            this.lblDicCmd.Text = "Dictonary";
            this.lblDicCmd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblSelectedCmd);
            this.panel1.Controls.Add(this.lblDicCmd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(479, 32);
            this.panel1.TabIndex = 3;
            // 
            // lblSelectedCmd
            // 
            this.lblSelectedCmd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblSelectedCmd.Location = new System.Drawing.Point(294, 0);
            this.lblSelectedCmd.Name = "lblSelectedCmd";
            this.lblSelectedCmd.Size = new System.Drawing.Size(185, 32);
            this.lblSelectedCmd.TabIndex = 3;
            this.lblSelectedCmd.Text = "Selected";
            this.lblSelectedCmd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSelToDir
            // 
            this.btnSelToDir.Location = new System.Drawing.Point(191, 199);
            this.btnSelToDir.Name = "btnSelToDir";
            this.btnSelToDir.Size = new System.Drawing.Size(97, 34);
            this.btnSelToDir.TabIndex = 4;
            this.btnSelToDir.Text = "<";
            this.btnSelToDir.UseVisualStyleBackColor = true;
            this.btnSelToDir.Click += new System.EventHandler(this.btnSelToDir_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(191, 38);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(97, 34);
            this.btnSelectAll.TabIndex = 5;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.Location = new System.Drawing.Point(191, 79);
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Size = new System.Drawing.Size(97, 34);
            this.btnRemoveAll.TabIndex = 6;
            this.btnRemoveAll.Text = "Remove All";
            this.btnRemoveAll.UseVisualStyleBackColor = true;
            this.btnRemoveAll.Click += new System.EventHandler(this.btnRemoveAll_Click);
            // 
            // lboxCmdSelected
            // 
            this.lboxCmdSelected.Dock = System.Windows.Forms.DockStyle.Right;
            this.lboxCmdSelected.FormattingEnabled = true;
            this.lboxCmdSelected.ItemHeight = 18;
            this.lboxCmdSelected.Location = new System.Drawing.Point(294, 32);
            this.lboxCmdSelected.Name = "lboxCmdSelected";
            this.lboxCmdSelected.Size = new System.Drawing.Size(185, 495);
            this.lboxCmdSelected.TabIndex = 7;
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(191, 336);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(97, 34);
            this.btnDown.TabIndex = 9;
            this.btnDown.Text = "Down";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(191, 296);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(97, 34);
            this.btnUp.TabIndex = 8;
            this.btnUp.Text = "Up";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(276, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(97, 34);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save,Close";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(379, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 34);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel,Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Controls.Add(this.btnSave);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 527);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(479, 40);
            this.flowLayoutPanel1.TabIndex = 12;
            // 
            // frmRecipeCmdEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 567);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.lboxCmdSelected);
            this.Controls.Add(this.btnRemoveAll);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.btnSelToDir);
            this.Controls.Add(this.btnDirToSel);
            this.Controls.Add(this.lboxCmdDic);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.Name = "frmRecipeCmdEditor";
            this.Text = "frmRecipeCmdEditor";
            this.Load += new System.EventHandler(this.frmRecipeCmdEditor_Load);
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lboxCmdDic;
        private System.Windows.Forms.Button btnDirToSel;
        private System.Windows.Forms.Label lblDicCmd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSelToDir;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnRemoveAll;
        private System.Windows.Forms.ListBox lboxCmdSelected;
        private System.Windows.Forms.Label lblSelectedCmd;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}