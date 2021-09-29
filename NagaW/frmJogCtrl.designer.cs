namespace NagaW
{
    partial class frmJogCtrl
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
            this.btnPosX = new System.Windows.Forms.Button();
            this.btnNegY = new System.Windows.Forms.Button();
            this.btnNegX = new System.Windows.Forms.Button();
            this.btnJogMode = new System.Windows.Forms.Button();
            this.tmrUpdateDisplay = new System.Windows.Forms.Timer(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnStep1um = new System.Windows.Forms.Button();
            this.btnStepDefineum = new System.Windows.Forms.Button();
            this.lblStep = new System.Windows.Forms.Label();
            this.btnStep10um = new System.Windows.Forms.Button();
            this.btnPosY = new System.Windows.Forms.Button();
            this.btnStep5um = new System.Windows.Forms.Button();
            this.btnLockZ = new System.Windows.Forms.Button();
            this.btnPosZ = new System.Windows.Forms.Button();
            this.btnNegZ = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnLed2 = new System.Windows.Forms.Button();
            this.btnLed1 = new System.Windows.Forms.Button();
            this.btnLed0 = new System.Windows.Forms.Button();
            this.lblLightValue = new System.Windows.Forms.Label();
            this.trackBar4 = new System.Windows.Forms.TrackBar();
            this.trackBar3 = new System.Windows.Forms.TrackBar();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnFocusZReset = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFocusGet = new System.Windows.Forms.Button();
            this.cbxFocusPoint = new System.Windows.Forms.ComboBox();
            this.btnFocusSet = new System.Windows.Forms.Button();
            this.btnGantryR = new System.Windows.Forms.Button();
            this.btnGantryL = new System.Windows.Forms.Button();
            this.lblAxisPos = new System.Windows.Forms.Label();
            this.lblLaser = new System.Windows.Forms.Label();
            this.btnToggleLight = new System.Windows.Forms.Button();
            this.btnAutoFocus = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLightToggle = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPosX
            // 
            this.btnPosX.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPosX.Location = new System.Drawing.Point(115, 59);
            this.btnPosX.Name = "btnPosX";
            this.btnPosX.Size = new System.Drawing.Size(50, 50);
            this.btnPosX.TabIndex = 1;
            this.btnPosX.TabStop = false;
            this.btnPosX.Text = "X+";
            this.btnPosX.UseVisualStyleBackColor = true;
            this.btnPosX.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnPosX_MouseDown);
            this.btnPosX.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnPosX_MouseMove);
            this.btnPosX.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnPosX_MouseUp);
            // 
            // btnNegY
            // 
            this.btnNegY.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNegY.Location = new System.Drawing.Point(59, 115);
            this.btnNegY.Name = "btnNegY";
            this.btnNegY.Size = new System.Drawing.Size(50, 50);
            this.btnNegY.TabIndex = 2;
            this.btnNegY.TabStop = false;
            this.btnNegY.Text = "Y-";
            this.btnNegY.UseVisualStyleBackColor = true;
            this.btnNegY.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnNegY_MouseDown);
            this.btnNegY.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnNegY_MouseMove);
            this.btnNegY.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnNegY_MouseUp);
            // 
            // btnNegX
            // 
            this.btnNegX.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNegX.Location = new System.Drawing.Point(3, 59);
            this.btnNegX.Name = "btnNegX";
            this.btnNegX.Size = new System.Drawing.Size(50, 50);
            this.btnNegX.TabIndex = 3;
            this.btnNegX.TabStop = false;
            this.btnNegX.Text = "X-";
            this.btnNegX.UseVisualStyleBackColor = false;
            this.btnNegX.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnNegX_MouseDown);
            this.btnNegX.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnNegX_MouseMove);
            this.btnNegX.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnNegX_MouseUp);
            // 
            // btnJogMode
            // 
            this.btnJogMode.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnJogMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJogMode.Location = new System.Drawing.Point(59, 59);
            this.btnJogMode.Name = "btnJogMode";
            this.btnJogMode.Size = new System.Drawing.Size(50, 50);
            this.btnJogMode.TabIndex = 4;
            this.btnJogMode.TabStop = false;
            this.btnJogMode.Text = "Mode";
            this.btnJogMode.UseVisualStyleBackColor = true;
            this.btnJogMode.Click += new System.EventHandler(this.btnJogMode_Click);
            // 
            // tmrUpdateDisplay
            // 
            this.tmrUpdateDisplay.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.ItemSize = new System.Drawing.Size(80, 35);
            this.tabControl1.Location = new System.Drawing.Point(5, 5);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(332, 217);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage1.Controls.Add(this.btnStep1um);
            this.tabPage1.Controls.Add(this.btnStepDefineum);
            this.tabPage1.Controls.Add(this.lblStep);
            this.tabPage1.Controls.Add(this.btnStep10um);
            this.tabPage1.Controls.Add(this.btnPosY);
            this.tabPage1.Controls.Add(this.btnStep5um);
            this.tabPage1.Controls.Add(this.btnLockZ);
            this.tabPage1.Controls.Add(this.btnPosZ);
            this.tabPage1.Controls.Add(this.btnNegZ);
            this.tabPage1.Controls.Add(this.btnNegY);
            this.tabPage1.Controls.Add(this.btnNegX);
            this.tabPage1.Controls.Add(this.btnJogMode);
            this.tabPage1.Controls.Add(this.btnPosX);
            this.tabPage1.Location = new System.Drawing.Point(4, 39);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(324, 174);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Jog";
            // 
            // btnStep1um
            // 
            this.btnStep1um.BackColor = System.Drawing.Color.Transparent;
            this.btnStep1um.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStep1um.Location = new System.Drawing.Point(241, 3);
            this.btnStep1um.Name = "btnStep1um";
            this.btnStep1um.Size = new System.Drawing.Size(66, 25);
            this.btnStep1um.TabIndex = 14;
            this.btnStep1um.TabStop = false;
            this.btnStep1um.Text = "0.001";
            this.btnStep1um.UseVisualStyleBackColor = false;
            this.btnStep1um.Click += new System.EventHandler(this.btnStep1um_Click);
            // 
            // btnStepDefineum
            // 
            this.btnStepDefineum.BackColor = System.Drawing.Color.Transparent;
            this.btnStepDefineum.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStepDefineum.Location = new System.Drawing.Point(241, 109);
            this.btnStepDefineum.Name = "btnStepDefineum";
            this.btnStepDefineum.Size = new System.Drawing.Size(66, 25);
            this.btnStepDefineum.TabIndex = 17;
            this.btnStepDefineum.TabStop = false;
            this.btnStepDefineum.Text = "Step";
            this.btnStepDefineum.UseVisualStyleBackColor = false;
            this.btnStepDefineum.Click += new System.EventHandler(this.btnStepDefineum_Click);
            // 
            // lblStep
            // 
            this.lblStep.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblStep.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStep.Location = new System.Drawing.Point(241, 140);
            this.lblStep.Margin = new System.Windows.Forms.Padding(3);
            this.lblStep.Name = "lblStep";
            this.lblStep.Size = new System.Drawing.Size(66, 25);
            this.lblStep.TabIndex = 7;
            this.lblStep.Text = "9.999 mm";
            this.lblStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStep.Click += new System.EventHandler(this.lblStep_Click);
            // 
            // btnStep10um
            // 
            this.btnStep10um.BackColor = System.Drawing.Color.Transparent;
            this.btnStep10um.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStep10um.Location = new System.Drawing.Point(241, 65);
            this.btnStep10um.Name = "btnStep10um";
            this.btnStep10um.Size = new System.Drawing.Size(66, 25);
            this.btnStep10um.TabIndex = 16;
            this.btnStep10um.TabStop = false;
            this.btnStep10um.Text = "0.010";
            this.btnStep10um.UseVisualStyleBackColor = false;
            this.btnStep10um.Click += new System.EventHandler(this.btnStep10um_Click);
            // 
            // btnPosY
            // 
            this.btnPosY.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPosY.Location = new System.Drawing.Point(59, 3);
            this.btnPosY.Name = "btnPosY";
            this.btnPosY.Size = new System.Drawing.Size(50, 50);
            this.btnPosY.TabIndex = 15;
            this.btnPosY.TabStop = false;
            this.btnPosY.Text = "Y+";
            this.btnPosY.UseVisualStyleBackColor = true;
            this.btnPosY.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnPosY_MouseDown);
            this.btnPosY.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnPosY_MouseMove);
            this.btnPosY.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnPosY_MouseUp);
            // 
            // btnStep5um
            // 
            this.btnStep5um.BackColor = System.Drawing.Color.Transparent;
            this.btnStep5um.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStep5um.Location = new System.Drawing.Point(241, 34);
            this.btnStep5um.Name = "btnStep5um";
            this.btnStep5um.Size = new System.Drawing.Size(66, 25);
            this.btnStep5um.TabIndex = 15;
            this.btnStep5um.TabStop = false;
            this.btnStep5um.Text = "0.005";
            this.btnStep5um.UseVisualStyleBackColor = false;
            this.btnStep5um.Click += new System.EventHandler(this.btnStep5um_Click);
            // 
            // btnLockZ
            // 
            this.btnLockZ.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnLockZ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLockZ.Location = new System.Drawing.Point(176, 59);
            this.btnLockZ.Name = "btnLockZ";
            this.btnLockZ.Size = new System.Drawing.Size(50, 50);
            this.btnLockZ.TabIndex = 6;
            this.btnLockZ.TabStop = false;
            this.btnLockZ.Text = "Lock Z";
            this.btnLockZ.UseVisualStyleBackColor = true;
            this.btnLockZ.Click += new System.EventHandler(this.btnLockZ_Click);
            // 
            // btnPosZ
            // 
            this.btnPosZ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPosZ.Location = new System.Drawing.Point(176, 3);
            this.btnPosZ.Name = "btnPosZ";
            this.btnPosZ.Size = new System.Drawing.Size(50, 50);
            this.btnPosZ.TabIndex = 5;
            this.btnPosZ.TabStop = false;
            this.btnPosZ.Text = "Z+";
            this.btnPosZ.UseVisualStyleBackColor = true;
            this.btnPosZ.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnPosZ_MouseDown);
            this.btnPosZ.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnPosZ_MouseMove);
            this.btnPosZ.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnPosZ_MouseUp);
            // 
            // btnNegZ
            // 
            this.btnNegZ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNegZ.Location = new System.Drawing.Point(176, 115);
            this.btnNegZ.Name = "btnNegZ";
            this.btnNegZ.Size = new System.Drawing.Size(50, 50);
            this.btnNegZ.TabIndex = 7;
            this.btnNegZ.TabStop = false;
            this.btnNegZ.Text = "Z-";
            this.btnNegZ.UseVisualStyleBackColor = true;
            this.btnNegZ.EnabledChanged += new System.EventHandler(this.btnNegZ_EnabledChanged);
            this.btnNegZ.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnNegZ_MouseDown);
            this.btnNegZ.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnNegZ_MouseMove);
            this.btnNegZ.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnNegZ_MouseUp);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnLed2);
            this.tabPage2.Controls.Add(this.btnLed1);
            this.tabPage2.Controls.Add(this.btnLed0);
            this.tabPage2.Controls.Add(this.lblLightValue);
            this.tabPage2.Controls.Add(this.trackBar4);
            this.tabPage2.Controls.Add(this.trackBar3);
            this.tabPage2.Controls.Add(this.trackBar2);
            this.tabPage2.Controls.Add(this.trackBar1);
            this.tabPage2.Location = new System.Drawing.Point(4, 39);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(7);
            this.tabPage2.Size = new System.Drawing.Size(324, 174);
            this.tabPage2.TabIndex = 4;
            this.tabPage2.Text = "Light";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnLed2
            // 
            this.btnLed2.Location = new System.Drawing.Point(155, 137);
            this.btnLed2.Name = "btnLed2";
            this.btnLed2.Size = new System.Drawing.Size(70, 34);
            this.btnLed2.TabIndex = 21;
            this.btnLed2.Text = "Mix 2";
            this.btnLed2.UseVisualStyleBackColor = true;
            this.btnLed2.Click += new System.EventHandler(this.btnLed2_Click);
            // 
            // btnLed1
            // 
            this.btnLed1.Location = new System.Drawing.Point(79, 137);
            this.btnLed1.Name = "btnLed1";
            this.btnLed1.Size = new System.Drawing.Size(70, 34);
            this.btnLed1.TabIndex = 20;
            this.btnLed1.Text = "Mix 1";
            this.btnLed1.UseVisualStyleBackColor = true;
            this.btnLed1.Click += new System.EventHandler(this.btnLed1_Click);
            // 
            // btnLed0
            // 
            this.btnLed0.Location = new System.Drawing.Point(3, 137);
            this.btnLed0.Name = "btnLed0";
            this.btnLed0.Size = new System.Drawing.Size(70, 34);
            this.btnLed0.TabIndex = 19;
            this.btnLed0.Text = "Mix 0";
            this.btnLed0.UseVisualStyleBackColor = true;
            this.btnLed0.Click += new System.EventHandler(this.btnLed0_Click);
            // 
            // lblLightValue
            // 
            this.lblLightValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLightValue.Location = new System.Drawing.Point(231, 137);
            this.lblLightValue.Name = "lblLightValue";
            this.lblLightValue.Size = new System.Drawing.Size(83, 34);
            this.lblLightValue.TabIndex = 18;
            this.lblLightValue.Text = "99, 99, 99, 99";
            this.lblLightValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBar4
            // 
            this.trackBar4.AutoSize = false;
            this.trackBar4.Dock = System.Windows.Forms.DockStyle.Top;
            this.trackBar4.Location = new System.Drawing.Point(7, 100);
            this.trackBar4.Maximum = 255;
            this.trackBar4.Name = "trackBar4";
            this.trackBar4.Size = new System.Drawing.Size(310, 31);
            this.trackBar4.TabIndex = 17;
            this.trackBar4.TickFrequency = 15;
            this.trackBar4.Scroll += new System.EventHandler(this.trackBar4_Scroll);
            // 
            // trackBar3
            // 
            this.trackBar3.AutoSize = false;
            this.trackBar3.Dock = System.Windows.Forms.DockStyle.Top;
            this.trackBar3.Location = new System.Drawing.Point(7, 69);
            this.trackBar3.Maximum = 255;
            this.trackBar3.Name = "trackBar3";
            this.trackBar3.Size = new System.Drawing.Size(310, 31);
            this.trackBar3.TabIndex = 16;
            this.trackBar3.TickFrequency = 15;
            this.trackBar3.Scroll += new System.EventHandler(this.trackBar3_Scroll);
            // 
            // trackBar2
            // 
            this.trackBar2.AutoSize = false;
            this.trackBar2.Dock = System.Windows.Forms.DockStyle.Top;
            this.trackBar2.Location = new System.Drawing.Point(7, 38);
            this.trackBar2.Maximum = 255;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(310, 31);
            this.trackBar2.TabIndex = 15;
            this.trackBar2.TickFrequency = 15;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // trackBar1
            // 
            this.trackBar1.AutoSize = false;
            this.trackBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.trackBar1.Location = new System.Drawing.Point(7, 7);
            this.trackBar1.Maximum = 255;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(310, 31);
            this.trackBar1.TabIndex = 14;
            this.trackBar1.TickFrequency = 15;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // tabPage3
            // 
            this.tabPage3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage3.Controls.Add(this.btnFocusZReset);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.btnFocusGet);
            this.tabPage3.Controls.Add(this.cbxFocusPoint);
            this.tabPage3.Controls.Add(this.btnFocusSet);
            this.tabPage3.Location = new System.Drawing.Point(4, 39);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage3.Size = new System.Drawing.Size(324, 174);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "Focus";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnFocusZReset
            // 
            this.btnFocusZReset.Location = new System.Drawing.Point(6, 98);
            this.btnFocusZReset.Name = "btnFocusZReset";
            this.btnFocusZReset.Size = new System.Drawing.Size(82, 35);
            this.btnFocusZReset.TabIndex = 9;
            this.btnFocusZReset.Text = "Reset";
            this.btnFocusZReset.UseVisualStyleBackColor = true;
            this.btnFocusZReset.Click += new System.EventHandler(this.btnFocusZReset_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 14);
            this.label1.TabIndex = 8;
            this.label1.Text = "Focus No (mm)";
            // 
            // btnFocusGet
            // 
            this.btnFocusGet.Location = new System.Drawing.Point(94, 57);
            this.btnFocusGet.Name = "btnFocusGet";
            this.btnFocusGet.Size = new System.Drawing.Size(82, 35);
            this.btnFocusGet.TabIndex = 7;
            this.btnFocusGet.Text = "Goto";
            this.btnFocusGet.UseVisualStyleBackColor = true;
            this.btnFocusGet.Click += new System.EventHandler(this.btnFocusGet_Click);
            // 
            // cbxFocusPoint
            // 
            this.cbxFocusPoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFocusPoint.FormattingEnabled = true;
            this.cbxFocusPoint.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.cbxFocusPoint.Location = new System.Drawing.Point(6, 25);
            this.cbxFocusPoint.Name = "cbxFocusPoint";
            this.cbxFocusPoint.Size = new System.Drawing.Size(119, 22);
            this.cbxFocusPoint.TabIndex = 5;
            this.cbxFocusPoint.Click += new System.EventHandler(this.cbxFocusPoint_Click);
            // 
            // btnFocusSet
            // 
            this.btnFocusSet.Location = new System.Drawing.Point(6, 57);
            this.btnFocusSet.Name = "btnFocusSet";
            this.btnFocusSet.Size = new System.Drawing.Size(82, 35);
            this.btnFocusSet.TabIndex = 6;
            this.btnFocusSet.Text = "Set";
            this.btnFocusSet.UseVisualStyleBackColor = true;
            this.btnFocusSet.Click += new System.EventHandler(this.btnFocusSet_Click);
            // 
            // btnGantryR
            // 
            this.btnGantryR.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnGantryR.Location = new System.Drawing.Point(95, 107);
            this.btnGantryR.Name = "btnGantryR";
            this.btnGantryR.Size = new System.Drawing.Size(87, 50);
            this.btnGantryR.TabIndex = 19;
            this.btnGantryR.TabStop = false;
            this.btnGantryR.Text = "Gantry VR";
            this.btnGantryR.UseVisualStyleBackColor = false;
            this.btnGantryR.Click += new System.EventHandler(this.btnGantryR_Click);
            // 
            // btnGantryL
            // 
            this.btnGantryL.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnGantryL.Location = new System.Drawing.Point(96, 161);
            this.btnGantryL.Name = "btnGantryL";
            this.btnGantryL.Size = new System.Drawing.Size(86, 50);
            this.btnGantryL.TabIndex = 18;
            this.btnGantryL.TabStop = false;
            this.btnGantryL.Text = "Gantry XYZ";
            this.btnGantryL.UseVisualStyleBackColor = false;
            this.btnGantryL.Click += new System.EventHandler(this.btnGantryL_Click);
            // 
            // lblAxisPos
            // 
            this.lblAxisPos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAxisPos.Location = new System.Drawing.Point(3, 153);
            this.lblAxisPos.Margin = new System.Windows.Forms.Padding(3);
            this.lblAxisPos.Name = "lblAxisPos";
            this.lblAxisPos.Padding = new System.Windows.Forms.Padding(3);
            this.lblAxisPos.Size = new System.Drawing.Size(87, 52);
            this.lblAxisPos.TabIndex = 1;
            this.lblAxisPos.Text = "label1";
            this.lblAxisPos.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblAxisPos.Click += new System.EventHandler(this.lblAxisPos_Click);
            // 
            // lblLaser
            // 
            this.lblLaser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLaser.Location = new System.Drawing.Point(4, 118);
            this.lblLaser.Margin = new System.Windows.Forms.Padding(3);
            this.lblLaser.Name = "lblLaser";
            this.lblLaser.Size = new System.Drawing.Size(87, 29);
            this.lblLaser.TabIndex = 2;
            this.lblLaser.Text = "Laser";
            this.lblLaser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLaser.Click += new System.EventHandler(this.lblLaser_Click);
            // 
            // btnToggleLight
            // 
            this.btnToggleLight.Location = new System.Drawing.Point(63, 52);
            this.btnToggleLight.Name = "btnToggleLight";
            this.btnToggleLight.Size = new System.Drawing.Size(53, 45);
            this.btnToggleLight.TabIndex = 0;
            this.btnToggleLight.Text = "Light On/Off";
            this.btnToggleLight.UseVisualStyleBackColor = true;
            this.btnToggleLight.Click += new System.EventHandler(this.btnToggleLight_Click);
            // 
            // btnAutoFocus
            // 
            this.btnAutoFocus.Location = new System.Drawing.Point(3, 3);
            this.btnAutoFocus.Name = "btnAutoFocus";
            this.btnAutoFocus.Size = new System.Drawing.Size(65, 45);
            this.btnAutoFocus.TabIndex = 1;
            this.btnAutoFocus.Text = "Auto Focus";
            this.btnAutoFocus.UseVisualStyleBackColor = true;
            this.btnAutoFocus.Click += new System.EventHandler(this.btnAutoFocus_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btnLightToggle);
            this.panel1.Controls.Add(this.btnGantryR);
            this.panel1.Controls.Add(this.lblAxisPos);
            this.panel1.Controls.Add(this.btnGantryL);
            this.panel1.Controls.Add(this.lblLaser);
            this.panel1.Controls.Add(this.btnToggleLight);
            this.panel1.Controls.Add(this.btnAutoFocus);
            this.panel1.Location = new System.Drawing.Point(340, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(190, 214);
            this.panel1.TabIndex = 3;
            // 
            // btnLightToggle
            // 
            this.btnLightToggle.Location = new System.Drawing.Point(4, 52);
            this.btnLightToggle.Name = "btnLightToggle";
            this.btnLightToggle.Size = new System.Drawing.Size(53, 45);
            this.btnLightToggle.TabIndex = 21;
            this.btnLightToggle.Text = "Light Toggle";
            this.btnLightToggle.UseVisualStyleBackColor = true;
            this.btnLightToggle.Click += new System.EventHandler(this.btnLightToggle_Click);
            // 
            // frmJogCtrl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(536, 224);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "frmJogCtrl";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "JogCtrl";
            this.Load += new System.EventHandler(this.frmJogCtrl_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnPosX;
        private System.Windows.Forms.Button btnNegY;
        private System.Windows.Forms.Button btnNegX;
        private System.Windows.Forms.Button btnJogMode;
        private System.Windows.Forms.Timer tmrUpdateDisplay;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnLockZ;
        private System.Windows.Forms.Button btnPosZ;
        private System.Windows.Forms.Button btnNegZ;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label lblAxisPos;
        private System.Windows.Forms.Label lblLaser;
        private System.Windows.Forms.Button btnPosY;
        private System.Windows.Forms.Label lblStep;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TrackBar trackBar4;
        private System.Windows.Forms.TrackBar trackBar3;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button btnToggleLight;
        private System.Windows.Forms.Button btnAutoFocus;
        private System.Windows.Forms.Label lblLightValue;
        private System.Windows.Forms.Button btnFocusGet;
        private System.Windows.Forms.ComboBox cbxFocusPoint;
        private System.Windows.Forms.Button btnFocusSet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStep1um;
        private System.Windows.Forms.Button btnStepDefineum;
        private System.Windows.Forms.Button btnStep10um;
        private System.Windows.Forms.Button btnStep5um;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnFocusZReset;
        private System.Windows.Forms.Button btnGantryR;
        private System.Windows.Forms.Button btnGantryL;
        private System.Windows.Forms.Button btnLightToggle;
        private System.Windows.Forms.Button btnLed0;
        private System.Windows.Forms.Button btnLed2;
        private System.Windows.Forms.Button btnLed1;
    }
}