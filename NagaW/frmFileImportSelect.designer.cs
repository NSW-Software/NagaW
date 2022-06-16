namespace NagaW
{
    partial class frmFileImportSelect
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbxFileType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxFilename = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxFolder = new System.Windows.Forms.TextBox();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.cbxLayerName = new System.Windows.Forms.ComboBox();
            this.cbxStepName = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.pnlODB = new System.Windows.Forms.Panel();
            this.pnlODB.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "File type";
            // 
            // cbxFileType
            // 
            this.cbxFileType.FormattingEnabled = true;
            this.cbxFileType.Location = new System.Drawing.Point(85, 12);
            this.cbxFileType.Name = "cbxFileType";
            this.cbxFileType.Size = new System.Drawing.Size(166, 22);
            this.cbxFileType.TabIndex = 1;
            this.cbxFileType.SelectionChangeCommitted += new System.EventHandler(this.cbxFileType_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "File name";
            // 
            // tbxFilename
            // 
            this.tbxFilename.Location = new System.Drawing.Point(85, 125);
            this.tbxFilename.Name = "tbxFilename";
            this.tbxFilename.Size = new System.Drawing.Size(381, 22);
            this.tbxFilename.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(431, 164);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(350, 164);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 14);
            this.label3.TabIndex = 7;
            this.label3.Text = "Folder";
            // 
            // tbxFolder
            // 
            this.tbxFolder.Location = new System.Drawing.Point(77, 3);
            this.tbxFolder.Name = "tbxFolder";
            this.tbxFolder.Size = new System.Drawing.Size(381, 22);
            this.tbxFolder.TabIndex = 8;
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Location = new System.Drawing.Point(464, 3);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(34, 20);
            this.btnSelectFolder.TabIndex = 9;
            this.btnSelectFolder.Text = "...";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // cbxLayerName
            // 
            this.cbxLayerName.FormattingEnabled = true;
            this.cbxLayerName.Location = new System.Drawing.Point(152, 56);
            this.cbxLayerName.Name = "cbxLayerName";
            this.cbxLayerName.Size = new System.Drawing.Size(306, 22);
            this.cbxLayerName.TabIndex = 10;
            this.cbxLayerName.SelectedValueChanged += new System.EventHandler(this.cbxLayerName_SelectedValueChanged);
            // 
            // cbxStepName
            // 
            this.cbxStepName.FormattingEnabled = true;
            this.cbxStepName.Location = new System.Drawing.Point(152, 29);
            this.cbxStepName.Name = "cbxStepName";
            this.cbxStepName.Size = new System.Drawing.Size(306, 22);
            this.cbxStepName.TabIndex = 11;
            this.cbxStepName.SelectedValueChanged += new System.EventHandler(this.cbxStepName_SelectedValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(74, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 14);
            this.label4.TabIndex = 12;
            this.label4.Text = "Step Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(74, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 14);
            this.label5.TabIndex = 13;
            this.label5.Text = "Layer Name";
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(472, 125);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(34, 20);
            this.btnSelectFile.TabIndex = 15;
            this.btnSelectFile.Text = "...";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // pnlODB
            // 
            this.pnlODB.AutoSize = true;
            this.pnlODB.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlODB.Controls.Add(this.label3);
            this.pnlODB.Controls.Add(this.tbxFolder);
            this.pnlODB.Controls.Add(this.btnSelectFolder);
            this.pnlODB.Controls.Add(this.label5);
            this.pnlODB.Controls.Add(this.cbxLayerName);
            this.pnlODB.Controls.Add(this.label4);
            this.pnlODB.Controls.Add(this.cbxStepName);
            this.pnlODB.Location = new System.Drawing.Point(8, 39);
            this.pnlODB.Name = "pnlODB";
            this.pnlODB.Size = new System.Drawing.Size(501, 81);
            this.pnlODB.TabIndex = 16;
            this.pnlODB.Visible = false;
            // 
            // frmFileImportSelect
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(527, 201);
            this.ControlBox = false;
            this.Controls.Add(this.pnlODB);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tbxFilename);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbxFileType);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.Name = "frmFileImportSelect";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Open File or Select Folder";
            this.Load += new System.EventHandler(this.frmImport_Load);
            this.pnlODB.ResumeLayout(false);
            this.pnlODB.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxFileType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxFilename;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxFolder;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.ComboBox cbxLayerName;
        private System.Windows.Forms.ComboBox cbxStepName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.Panel pnlODB;
    }
}