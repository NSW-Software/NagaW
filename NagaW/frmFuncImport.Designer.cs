
namespace NagaW
{
    partial class frmFuncImport
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbxDataSet = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbxPoints = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblScaleY = new System.Windows.Forms.Label();
            this.lblOrientation = new System.Windows.Forms.Label();
            this.lblScaleX = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlGraph = new System.Windows.Forms.Panel();
            this.btnNext = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbxDataSet);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(205, 524);
            this.panel1.TabIndex = 0;
            // 
            // lbxDataSet
            // 
            this.lbxDataSet.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbxDataSet.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxDataSet.ForeColor = System.Drawing.Color.Navy;
            this.lbxDataSet.FormattingEnabled = true;
            this.lbxDataSet.ItemHeight = 14;
            this.lbxDataSet.Location = new System.Drawing.Point(5, 41);
            this.lbxDataSet.Name = "lbxDataSet";
            this.lbxDataSet.Size = new System.Drawing.Size(195, 452);
            this.lbxDataSet.TabIndex = 1;
            this.lbxDataSet.SelectedIndexChanged += new System.EventHandler(this.lbxDataSet_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 36);
            this.label1.TabIndex = 1;
            this.label1.Text = "DataSets";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbxPoints);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(205, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5);
            this.panel2.Size = new System.Drawing.Size(208, 524);
            this.panel2.TabIndex = 1;
            // 
            // lbxPoints
            // 
            this.lbxPoints.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbxPoints.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxPoints.ForeColor = System.Drawing.Color.Navy;
            this.lbxPoints.FormattingEnabled = true;
            this.lbxPoints.ItemHeight = 14;
            this.lbxPoints.Location = new System.Drawing.Point(5, 41);
            this.lbxPoints.Name = "lbxPoints";
            this.lbxPoints.Size = new System.Drawing.Size(198, 452);
            this.lbxPoints.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(5, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(198, 36);
            this.label2.TabIndex = 2;
            this.label2.Text = "Points";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.pnlGraph);
            this.panel3.Controls.Add(this.btnNext);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(413, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(5);
            this.panel3.Size = new System.Drawing.Size(452, 524);
            this.panel3.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lblScaleY);
            this.panel4.Controls.Add(this.lblOrientation);
            this.panel4.Controls.Add(this.lblScaleX);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(5, 358);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(442, 112);
            this.panel4.TabIndex = 6;
            // 
            // lblScaleY
            // 
            this.lblScaleY.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblScaleY.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblScaleY.Location = new System.Drawing.Point(254, 10);
            this.lblScaleY.Name = "lblScaleY";
            this.lblScaleY.Size = new System.Drawing.Size(122, 26);
            this.lblScaleY.TabIndex = 4;
            this.lblScaleY.Text = "1";
            this.lblScaleY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblScaleY.Click += new System.EventHandler(this.lblScaleY_Click);
            // 
            // lblOrientation
            // 
            this.lblOrientation.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblOrientation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblOrientation.Location = new System.Drawing.Point(126, 45);
            this.lblOrientation.Name = "lblOrientation";
            this.lblOrientation.Size = new System.Drawing.Size(122, 26);
            this.lblOrientation.TabIndex = 3;
            this.lblOrientation.Text = "0";
            this.lblOrientation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOrientation.Click += new System.EventHandler(this.lblOrientation_Click);
            // 
            // lblScaleX
            // 
            this.lblScaleX.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblScaleX.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblScaleX.Location = new System.Drawing.Point(126, 10);
            this.lblScaleX.Name = "lblScaleX";
            this.lblScaleX.Size = new System.Drawing.Size(122, 26);
            this.lblScaleX.TabIndex = 2;
            this.lblScaleX.Text = "1";
            this.lblScaleX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblScaleX.Click += new System.EventHandler(this.lblScale_Click);
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Location = new System.Drawing.Point(8, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 26);
            this.label5.TabIndex = 1;
            this.label5.Text = "Orientation";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Location = new System.Drawing.Point(8, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 26);
            this.label4.TabIndex = 0;
            this.label4.Text = "Scale";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlGraph
            // 
            this.pnlGraph.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pnlGraph.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGraph.Location = new System.Drawing.Point(5, 41);
            this.pnlGraph.Name = "pnlGraph";
            this.pnlGraph.Size = new System.Drawing.Size(442, 317);
            this.pnlGraph.TabIndex = 7;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(345, 486);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(99, 28);
            this.btnNext.TabIndex = 5;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(5, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(442, 36);
            this.label3.TabIndex = 3;
            this.label3.Text = "Preview Image";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmFuncImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 524);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.Name = "frmFuncImport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Function Import";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox lbxDataSet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListBox lbxPoints;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblOrientation;
        private System.Windows.Forms.Label lblScaleX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlGraph;
        private System.Windows.Forms.Label lblScaleY;
    }
}