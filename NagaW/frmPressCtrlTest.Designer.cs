namespace NagaW
{
    partial class frmPressCtrlTest
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
            this.grb_PressCtrl = new System.Windows.Forms.GroupBox();
            this.btn_RightPumpPress = new System.Windows.Forms.Button();
            this.btn_leftPumpPress = new System.Windows.Forms.Button();
            this.lbl_GetPressUnit = new System.Windows.Forms.Label();
            this.btn_getPressValue = new System.Windows.Forms.Button();
            this.lbl_GetPressCtrl = new System.Windows.Forms.Label();
            this.btn_Set = new System.Windows.Forms.Button();
            this.lbl_SetPressUnit = new System.Windows.Forms.Label();
            this.lbl_SetPressCtrl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_PressCtrl = new System.Windows.Forms.ComboBox();
            this.grb_PressCtrl.SuspendLayout();
            this.SuspendLayout();
            // 
            // grb_PressCtrl
            // 
            this.grb_PressCtrl.Controls.Add(this.btn_RightPumpPress);
            this.grb_PressCtrl.Controls.Add(this.btn_leftPumpPress);
            this.grb_PressCtrl.Controls.Add(this.lbl_GetPressUnit);
            this.grb_PressCtrl.Controls.Add(this.btn_getPressValue);
            this.grb_PressCtrl.Controls.Add(this.lbl_GetPressCtrl);
            this.grb_PressCtrl.Controls.Add(this.btn_Set);
            this.grb_PressCtrl.Controls.Add(this.lbl_SetPressUnit);
            this.grb_PressCtrl.Controls.Add(this.lbl_SetPressCtrl);
            this.grb_PressCtrl.Controls.Add(this.label1);
            this.grb_PressCtrl.Controls.Add(this.cmb_PressCtrl);
            this.grb_PressCtrl.Location = new System.Drawing.Point(7, 7);
            this.grb_PressCtrl.Margin = new System.Windows.Forms.Padding(2);
            this.grb_PressCtrl.Name = "grb_PressCtrl";
            this.grb_PressCtrl.Padding = new System.Windows.Forms.Padding(2);
            this.grb_PressCtrl.Size = new System.Drawing.Size(444, 194);
            this.grb_PressCtrl.TabIndex = 0;
            this.grb_PressCtrl.TabStop = false;
            this.grb_PressCtrl.Text = "Pressure Control Test";
            // 
            // btn_RightPumpPress
            // 
            this.btn_RightPumpPress.Location = new System.Drawing.Point(288, 111);
            this.btn_RightPumpPress.Margin = new System.Windows.Forms.Padding(2);
            this.btn_RightPumpPress.Name = "btn_RightPumpPress";
            this.btn_RightPumpPress.Size = new System.Drawing.Size(140, 25);
            this.btn_RightPumpPress.TabIndex = 10;
            this.btn_RightPumpPress.Text = "Right Pump Pressure";
            this.btn_RightPumpPress.UseVisualStyleBackColor = true;
            this.btn_RightPumpPress.Click += new System.EventHandler(this.btn_RightPumpPress_Click);
            // 
            // btn_leftPumpPress
            // 
            this.btn_leftPumpPress.Location = new System.Drawing.Point(288, 68);
            this.btn_leftPumpPress.Margin = new System.Windows.Forms.Padding(2);
            this.btn_leftPumpPress.Name = "btn_leftPumpPress";
            this.btn_leftPumpPress.Size = new System.Drawing.Size(140, 25);
            this.btn_leftPumpPress.TabIndex = 9;
            this.btn_leftPumpPress.Text = "Left Pump Pressure";
            this.btn_leftPumpPress.UseVisualStyleBackColor = true;
            this.btn_leftPumpPress.Click += new System.EventHandler(this.btn_leftPumpPress_Click);
            // 
            // lbl_GetPressUnit
            // 
            this.lbl_GetPressUnit.AutoSize = true;
            this.lbl_GetPressUnit.Location = new System.Drawing.Point(103, 118);
            this.lbl_GetPressUnit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_GetPressUnit.Name = "lbl_GetPressUnit";
            this.lbl_GetPressUnit.Size = new System.Drawing.Size(41, 14);
            this.lbl_GetPressUnit.TabIndex = 8;
            this.lbl_GetPressUnit.Text = "(MPA)";
            // 
            // btn_getPressValue
            // 
            this.btn_getPressValue.Location = new System.Drawing.Point(162, 111);
            this.btn_getPressValue.Margin = new System.Windows.Forms.Padding(2);
            this.btn_getPressValue.Name = "btn_getPressValue";
            this.btn_getPressValue.Size = new System.Drawing.Size(107, 25);
            this.btn_getPressValue.TabIndex = 7;
            this.btn_getPressValue.Text = "Get";
            this.btn_getPressValue.UseVisualStyleBackColor = true;
            this.btn_getPressValue.Click += new System.EventHandler(this.btn_getPressValue_Click);
            // 
            // lbl_GetPressCtrl
            // 
            this.lbl_GetPressCtrl.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.lbl_GetPressCtrl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_GetPressCtrl.ForeColor = System.Drawing.Color.Navy;
            this.lbl_GetPressCtrl.Location = new System.Drawing.Point(12, 115);
            this.lbl_GetPressCtrl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_GetPressCtrl.Name = "lbl_GetPressCtrl";
            this.lbl_GetPressCtrl.Size = new System.Drawing.Size(87, 20);
            this.lbl_GetPressCtrl.TabIndex = 6;
            this.lbl_GetPressCtrl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_Set
            // 
            this.btn_Set.Location = new System.Drawing.Point(162, 68);
            this.btn_Set.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Set.Name = "btn_Set";
            this.btn_Set.Size = new System.Drawing.Size(107, 25);
            this.btn_Set.TabIndex = 5;
            this.btn_Set.Text = "Set";
            this.btn_Set.UseVisualStyleBackColor = true;
            this.btn_Set.Click += new System.EventHandler(this.btn_Set_Click);
            // 
            // lbl_SetPressUnit
            // 
            this.lbl_SetPressUnit.AutoSize = true;
            this.lbl_SetPressUnit.Location = new System.Drawing.Point(103, 75);
            this.lbl_SetPressUnit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_SetPressUnit.Name = "lbl_SetPressUnit";
            this.lbl_SetPressUnit.Size = new System.Drawing.Size(41, 14);
            this.lbl_SetPressUnit.TabIndex = 4;
            this.lbl_SetPressUnit.Text = "(MPA)";
            // 
            // lbl_SetPressCtrl
            // 
            this.lbl_SetPressCtrl.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_SetPressCtrl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_SetPressCtrl.ForeColor = System.Drawing.Color.Navy;
            this.lbl_SetPressCtrl.Location = new System.Drawing.Point(12, 72);
            this.lbl_SetPressCtrl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_SetPressCtrl.Name = "lbl_SetPressCtrl";
            this.lbl_SetPressCtrl.Size = new System.Drawing.Size(87, 20);
            this.lbl_SetPressCtrl.TabIndex = 3;
            this.lbl_SetPressCtrl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_SetPressCtrl.Click += new System.EventHandler(this.lbl_SetPressCtrl_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "Pressure Control :";
            // 
            // cmb_PressCtrl
            // 
            this.cmb_PressCtrl.FormattingEnabled = true;
            this.cmb_PressCtrl.Location = new System.Drawing.Point(114, 26);
            this.cmb_PressCtrl.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_PressCtrl.Name = "cmb_PressCtrl";
            this.cmb_PressCtrl.Size = new System.Drawing.Size(107, 22);
            this.cmb_PressCtrl.TabIndex = 1;
            // 
            // frmPressCtrlTest
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(488, 227);
            this.Controls.Add(this.grb_PressCtrl);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.Navy;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPressCtrlTest";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmPressCtrlTest";
            this.Load += new System.EventHandler(this.frmPressCtrlTest_Load);
            this.grb_PressCtrl.ResumeLayout(false);
            this.grb_PressCtrl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grb_PressCtrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_PressCtrl;
        private System.Windows.Forms.Label lbl_GetPressUnit;
        private System.Windows.Forms.Button btn_getPressValue;
        private System.Windows.Forms.Label lbl_GetPressCtrl;
        private System.Windows.Forms.Button btn_Set;
        private System.Windows.Forms.Label lbl_SetPressUnit;
        private System.Windows.Forms.Label lbl_SetPressCtrl;
        private System.Windows.Forms.Button btn_RightPumpPress;
        private System.Windows.Forms.Button btn_leftPumpPress;
    }
}