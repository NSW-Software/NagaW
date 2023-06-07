namespace NagaW
{
    partial class frmRecipe
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpLayout = new System.Windows.Forms.TabPage();
            this.tpFunc = new System.Windows.Forms.TabPage();
            this.tpMap = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblFunctionClusterUnit = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.cbxRunSelect = new System.Windows.Forms.ComboBox();
            this.cbxRunMode = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControl1.Controls.Add(this.tpLayout);
            this.tabControl1.Controls.Add(this.tpFunc);
            this.tabControl1.Controls.Add(this.tpMap);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(100, 35);
            this.tabControl1.Location = new System.Drawing.Point(6, 6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(692, 632);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 0;
            // 
            // tpLayout
            // 
            this.tpLayout.BackColor = System.Drawing.SystemColors.Control;
            this.tpLayout.Location = new System.Drawing.Point(4, 39);
            this.tpLayout.Name = "tpLayout";
            this.tpLayout.Padding = new System.Windows.Forms.Padding(3);
            this.tpLayout.Size = new System.Drawing.Size(684, 589);
            this.tpLayout.TabIndex = 1;
            this.tpLayout.Text = "Layout";
            // 
            // tpFunc
            // 
            this.tpFunc.Location = new System.Drawing.Point(4, 39);
            this.tpFunc.Name = "tpFunc";
            this.tpFunc.Padding = new System.Windows.Forms.Padding(3);
            this.tpFunc.Size = new System.Drawing.Size(684, 439);
            this.tpFunc.TabIndex = 2;
            this.tpFunc.Text = "Function";
            this.tpFunc.UseVisualStyleBackColor = true;
            // 
            // tpMap
            // 
            this.tpMap.BackColor = System.Drawing.SystemColors.Control;
            this.tpMap.Location = new System.Drawing.Point(4, 39);
            this.tpMap.Name = "tpMap";
            this.tpMap.Padding = new System.Windows.Forms.Padding(3);
            this.tpMap.Size = new System.Drawing.Size(684, 439);
            this.tpMap.TabIndex = 0;
            this.tpMap.Text = "Map";
            // 
            // panel2
            // 
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.Controls.Add(this.lblFunctionClusterUnit);
            this.panel2.Controls.Add(this.flowLayoutPanel1);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.cbxRunSelect);
            this.panel2.Controls.Add(this.cbxRunMode);
            this.panel2.Controls.Add(this.label19);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(6, 638);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(692, 67);
            this.panel2.TabIndex = 16;
            // 
            // lblFunctionClusterUnit
            // 
            this.lblFunctionClusterUnit.AutoSize = true;
            this.lblFunctionClusterUnit.Location = new System.Drawing.Point(3, 38);
            this.lblFunctionClusterUnit.Name = "lblFunctionClusterUnit";
            this.lblFunctionClusterUnit.Size = new System.Drawing.Size(167, 14);
            this.lblFunctionClusterUnit.TabIndex = 20;
            this.lblFunctionClusterUnit.Text = "Func: 1 Cluster: 4,5 Unit: 1,2";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.btnRun);
            this.flowLayoutPanel1.Controls.Add(this.btnStop);
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(386, 6);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(294, 49);
            this.flowLayoutPanel1.TabIndex = 19;
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(3, 3);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(92, 43);
            this.btnRun.TabIndex = 2;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(101, 3);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(92, 43);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(199, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(92, 43);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 14);
            this.label9.TabIndex = 12;
            this.label9.Text = "Mode";
            // 
            // cbxRunSelect
            // 
            this.cbxRunSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxRunSelect.FormattingEnabled = true;
            this.cbxRunSelect.Location = new System.Drawing.Point(190, 6);
            this.cbxRunSelect.Name = "cbxRunSelect";
            this.cbxRunSelect.Size = new System.Drawing.Size(99, 22);
            this.cbxRunSelect.TabIndex = 13;
            this.cbxRunSelect.SelectionChangeCommitted += new System.EventHandler(this.cbxRunSelect_SelectionChangeCommitted);
            // 
            // cbxRunMode
            // 
            this.cbxRunMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxRunMode.FormattingEnabled = true;
            this.cbxRunMode.Location = new System.Drawing.Point(46, 6);
            this.cbxRunMode.Name = "cbxRunMode";
            this.cbxRunMode.Size = new System.Drawing.Size(91, 22);
            this.cbxRunMode.TabIndex = 11;
            this.cbxRunMode.SelectedIndexChanged += new System.EventHandler(this.cbxRunMode_SelectedIndexChanged);
            this.cbxRunMode.SelectionChangeCommitted += new System.EventHandler(this.cbxRunMode_SelectionChangeCommitted);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(143, 9);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(41, 14);
            this.label19.TabIndex = 14;
            this.label19.Text = "Select";
            // 
            // frmRecipe
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.PaleVioletRed;
            this.ClientSize = new System.Drawing.Size(704, 711);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.MinimumSize = new System.Drawing.Size(600, 600);
            this.Name = "frmRecipe";
            this.Padding = new System.Windows.Forms.Padding(6);
            this.Text = "Recipe";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRecipe_FormClosing);
            this.Load += new System.EventHandler(this.frmRecipe_Load);
            this.tabControl1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpMap;
        private System.Windows.Forms.TabPage tpLayout;
        private System.Windows.Forms.TabPage tpFunc;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbxRunSelect;
        private System.Windows.Forms.ComboBox cbxRunMode;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblFunctionClusterUnit;
    }
}