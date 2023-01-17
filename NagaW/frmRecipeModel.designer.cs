
namespace NagaW
{
    partial class frmRecipeModel
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
            this.tpHead1 = new System.Windows.Forms.TabPage();
            this.dgv1 = new System.Windows.Forms.DataGridView();
            this.tpHead2 = new System.Windows.Forms.TabPage();
            this.dgv2 = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tpHead1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
            this.tpHead2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv2)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpHead1);
            this.tabControl1.Controls.Add(this.tpHead2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(96, 35);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(925, 517);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 0;
            // 
            // tpHead1
            // 
            this.tpHead1.Controls.Add(this.dgv1);
            this.tpHead1.Location = new System.Drawing.Point(4, 39);
            this.tpHead1.Name = "tpHead1";
            this.tpHead1.Padding = new System.Windows.Forms.Padding(3);
            this.tpHead1.Size = new System.Drawing.Size(917, 474);
            this.tpHead1.TabIndex = 0;
            this.tpHead1.Text = "Head 1";
            this.tpHead1.UseVisualStyleBackColor = true;
            // 
            // dgv1
            // 
            this.dgv1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv1.Location = new System.Drawing.Point(3, 3);
            this.dgv1.Name = "dgv1";
            this.dgv1.RowHeadersWidth = 51;
            this.dgv1.RowTemplate.Height = 24;
            this.dgv1.Size = new System.Drawing.Size(911, 468);
            this.dgv1.TabIndex = 0;
            this.dgv1.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_CellMouseUp);
            // 
            // tpHead2
            // 
            this.tpHead2.Controls.Add(this.dgv2);
            this.tpHead2.Location = new System.Drawing.Point(4, 39);
            this.tpHead2.Name = "tpHead2";
            this.tpHead2.Padding = new System.Windows.Forms.Padding(3);
            this.tpHead2.Size = new System.Drawing.Size(917, 474);
            this.tpHead2.TabIndex = 1;
            this.tpHead2.Text = "Head 2";
            this.tpHead2.UseVisualStyleBackColor = true;
            // 
            // dgv2
            // 
            this.dgv2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv2.Location = new System.Drawing.Point(3, 3);
            this.dgv2.Name = "dgv2";
            this.dgv2.RowHeadersWidth = 51;
            this.dgv2.RowTemplate.Height = 24;
            this.dgv2.Size = new System.Drawing.Size(911, 468);
            this.dgv2.TabIndex = 1;
            this.dgv2.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv2_CellMouseUp);
            // 
            // frmRecipeModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 517);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.Name = "frmRecipeModel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Model";
            this.Load += new System.EventHandler(this.frmRecipeModel_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpHead1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).EndInit();
            this.tpHead2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpHead1;
        private System.Windows.Forms.DataGridView dgv1;
        private System.Windows.Forms.TabPage tpHead2;
        private System.Windows.Forms.DataGridView dgv2;
    }
}