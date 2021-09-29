namespace NagaW
{
    partial class frmWeightScale
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
            this.btnConnect = new System.Windows.Forms.Button();
            this.cbxComport = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnPool = new System.Windows.Forms.Button();
            this.btnReadStable = new System.Windows.Forms.Button();
            this.btnZero = new System.Windows.Forms.Button();
            this.btnTare = new System.Windows.Forms.Button();
            this.lblValue = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.tmrPool = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(208, 21);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 29;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // cbxComport
            // 
            this.cbxComport.FormattingEnabled = true;
            this.cbxComport.Location = new System.Drawing.Point(82, 22);
            this.cbxComport.Name = "cbxComport";
            this.cbxComport.Size = new System.Drawing.Size(120, 22);
            this.cbxComport.TabIndex = 27;
            this.cbxComport.SelectionChangeCommitted += new System.EventHandler(this.cbxComport_SelectionChangeCommitted);
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(6, 22);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(70, 21);
            this.label22.TabIndex = 28;
            this.label22.Text = "ComPort";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Controls.Add(this.cbxComport);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 65);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection";
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox2.Controls.Add(this.btnPool);
            this.groupBox2.Controls.Add(this.btnReadStable);
            this.groupBox2.Controls.Add(this.btnZero);
            this.groupBox2.Controls.Add(this.btnTare);
            this.groupBox2.Location = new System.Drawing.Point(6, 138);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(289, 119);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Functions";
            // 
            // btnPool
            // 
            this.btnPool.Location = new System.Drawing.Point(208, 29);
            this.btnPool.Name = "btnPool";
            this.btnPool.Size = new System.Drawing.Size(75, 23);
            this.btnPool.TabIndex = 33;
            this.btnPool.Text = "Pool Run";
            this.btnPool.UseVisualStyleBackColor = true;
            this.btnPool.Click += new System.EventHandler(this.btnPool_Click);
            // 
            // btnReadStable
            // 
            this.btnReadStable.Location = new System.Drawing.Point(6, 75);
            this.btnReadStable.Name = "btnReadStable";
            this.btnReadStable.Size = new System.Drawing.Size(75, 23);
            this.btnReadStable.TabIndex = 32;
            this.btnReadStable.Text = "Read";
            this.btnReadStable.UseVisualStyleBackColor = true;
            this.btnReadStable.Click += new System.EventHandler(this.btnReadStable_Click);
            // 
            // btnZero
            // 
            this.btnZero.Location = new System.Drawing.Point(87, 29);
            this.btnZero.Name = "btnZero";
            this.btnZero.Size = new System.Drawing.Size(75, 23);
            this.btnZero.TabIndex = 31;
            this.btnZero.Text = "Zero";
            this.btnZero.UseVisualStyleBackColor = true;
            this.btnZero.Click += new System.EventHandler(this.btnZero_Click);
            // 
            // btnTare
            // 
            this.btnTare.Location = new System.Drawing.Point(6, 29);
            this.btnTare.Name = "btnTare";
            this.btnTare.Size = new System.Drawing.Size(75, 23);
            this.btnTare.TabIndex = 30;
            this.btnTare.Text = "Tare";
            this.btnTare.UseVisualStyleBackColor = true;
            this.btnTare.Click += new System.EventHandler(this.btnTare_Click);
            // 
            // lblValue
            // 
            this.lblValue.BackColor = System.Drawing.Color.Black;
            this.lblValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblValue.Font = new System.Drawing.Font("Tahoma", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValue.ForeColor = System.Drawing.Color.Lime;
            this.lblValue.Location = new System.Drawing.Point(6, 73);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(289, 62);
            this.lblValue.TabIndex = 32;
            this.lblValue.Text = "---";
            this.lblValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(214, 262);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 34;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tmrPool
            // 
            this.tmrPool.Enabled = true;
            this.tmrPool.Interval = 50;
            this.tmrPool.Tick += new System.EventHandler(this.tmrPool_Tick);
            // 
            // frmWeightScale
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(300, 287);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmWeightScale";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "frmWeightScale";
            this.Load += new System.EventHandler(this.frmWeightScale_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox cbxComport;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnZero;
        private System.Windows.Forms.Button btnTare;
        private System.Windows.Forms.Button btnPool;
        private System.Windows.Forms.Button btnReadStable;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Timer tmrPool;
    }
}