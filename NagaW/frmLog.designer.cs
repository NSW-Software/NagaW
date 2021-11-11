namespace NagaW
{
    partial class frmLog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLog));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnImport = new System.Windows.Forms.ToolStripButton();
            this.dtpickerFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpickerTo = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiCopyLog = new System.Windows.Forms.ToolStripMenuItem();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnFilter = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblLogTitle = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnImport});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1012, 27);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtnImport
            // 
            this.tsbtnImport.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnImport.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnImport.Image")));
            this.tsbtnImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnImport.Name = "tsbtnImport";
            this.tsbtnImport.Size = new System.Drawing.Size(58, 24);
            this.tsbtnImport.Text = "Import";
            this.tsbtnImport.Click += new System.EventHandler(this.tsbtnImport_Click);
            // 
            // dtpickerFrom
            // 
            this.dtpickerFrom.Checked = false;
            this.dtpickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpickerFrom.Location = new System.Drawing.Point(52, 3);
            this.dtpickerFrom.Name = "dtpickerFrom";
            this.dtpickerFrom.ShowUpDown = true;
            this.dtpickerFrom.Size = new System.Drawing.Size(101, 26);
            this.dtpickerFrom.TabIndex = 2;
            // 
            // dtpickerTo
            // 
            this.dtpickerTo.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpickerTo.Location = new System.Drawing.Point(208, 3);
            this.dtpickerTo.Name = "dtpickerTo";
            this.dtpickerTo.ShowUpDown = true;
            this.dtpickerTo.Size = new System.Drawing.Size(115, 26);
            this.dtpickerTo.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(159, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 33);
            this.label2.TabIndex = 5;
            this.label2.Text = "To";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 33);
            this.label1.TabIndex = 4;
            this.label1.Text = "From";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // richTextBox1
            // 
            this.richTextBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(1004, 547);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCopyLog});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(113, 28);
            // 
            // tsmiCopyLog
            // 
            this.tsmiCopyLog.Name = "tsmiCopyLog";
            this.tsmiCopyLog.Size = new System.Drawing.Size(112, 24);
            this.tsmiCopyLog.Text = "Copy";
            this.tsmiCopyLog.Click += new System.EventHandler(this.tsmiCopyLog_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.dtpickerFrom);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.dtpickerTo);
            this.flowLayoutPanel1.Controls.Add(this.btnFilter);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 27);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1012, 34);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(329, 3);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 26);
            this.btnFilter.TabIndex = 6;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 79);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1012, 581);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.richTextBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 30);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1004, 547);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Text";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblLogTitle
            // 
            this.lblLogTitle.AutoSize = true;
            this.lblLogTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLogTitle.Location = new System.Drawing.Point(0, 61);
            this.lblLogTitle.Name = "lblLogTitle";
            this.lblLogTitle.Size = new System.Drawing.Size(58, 18);
            this.lblLogTitle.TabIndex = 9;
            this.lblLogTitle.Text = "LogTitle";
            // 
            // frmLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 660);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblLogTitle);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.Name = "frmLog";
            this.Text = "frmLog";
            this.Load += new System.EventHandler(this.frmLog_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.DateTimePicker dtpickerFrom;
        private System.Windows.Forms.DateTimePicker dtpickerTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton tsbtnImport;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyLog;
        private System.Windows.Forms.Label lblLogTitle;
    }
}