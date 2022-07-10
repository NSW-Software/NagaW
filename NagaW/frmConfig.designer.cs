namespace NagaW
{
    partial class frmConfig
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tabcontrol1 = new System.Windows.Forms.TabControl();
            this.tpConfig = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.tpGantry = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.tpConveyor = new System.Windows.Forms.TabPage();
            this.tpHSensor = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnHsensorOpen = new System.Windows.Forms.Button();
            this.btnHSensorTest = new System.Windows.Forms.Button();
            this.tpCamera = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCamOpen = new System.Windows.Forms.Button();
            this.tpLightingCtrl = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnLightOpen = new System.Windows.Forms.Button();
            this.tpWeighing = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel7 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnWeighOpen = new System.Windows.Forms.Button();
            this.btnWeighTest = new System.Windows.Forms.Button();
            this.tpPressureCtrl = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel8 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnPressCtrlOpen = new System.Windows.Forms.Button();
            this.btnPressCtrlTest = new System.Windows.Forms.Button();
            this.tpTempCtrl = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel9 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnTempOpen = new System.Windows.Forms.Button();
            this.btnTempCtrlPanel = new System.Windows.Forms.Button();
            this.tpDispCtrl = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnDispCtrlOpen = new System.Windows.Forms.Button();
            this.btnDispExTempCtrlOpen = new System.Windows.Forms.Button();
            this.tpDisplay = new System.Windows.Forms.TabPage();
            this.tpAdvance = new System.Windows.Forms.TabPage();
            this.tpMakerData = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel10 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnReset = new System.Windows.Forms.Button();
            this.tpOption = new System.Windows.Forms.TabPage();
            this.cbxCentrePatAlign = new System.Windows.Forms.CheckBox();
            this.tpGem = new System.Windows.Forms.TabPage();
            this.btnEquipConst = new System.Windows.Forms.Button();
            this.btnSVID = new System.Windows.Forms.Button();
            this.btnCEID = new System.Windows.Forms.Button();
            this.btnALID = new System.Windows.Forms.Button();
            this.flowLayoutPanel11 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSecsGemOpen = new System.Windows.Forms.Button();
            this.tabcontrol1.SuspendLayout();
            this.tpConfig.SuspendLayout();
            this.tpGantry.SuspendLayout();
            this.tpHSensor.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.tpCamera.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.tpLightingCtrl.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tpWeighing.SuspendLayout();
            this.flowLayoutPanel7.SuspendLayout();
            this.tpPressureCtrl.SuspendLayout();
            this.panel2.SuspendLayout();
            this.flowLayoutPanel8.SuspendLayout();
            this.tpTempCtrl.SuspendLayout();
            this.flowLayoutPanel9.SuspendLayout();
            this.tpDispCtrl.SuspendLayout();
            this.flowLayoutPanel6.SuspendLayout();
            this.tpMakerData.SuspendLayout();
            this.flowLayoutPanel10.SuspendLayout();
            this.tpOption.SuspendLayout();
            this.tpGem.SuspendLayout();
            this.flowLayoutPanel11.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tabcontrol1
            // 
            this.tabcontrol1.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabcontrol1.Controls.Add(this.tpConfig);
            this.tabcontrol1.Controls.Add(this.tpGantry);
            this.tabcontrol1.Controls.Add(this.tpConveyor);
            this.tabcontrol1.Controls.Add(this.tpHSensor);
            this.tabcontrol1.Controls.Add(this.tpCamera);
            this.tabcontrol1.Controls.Add(this.tpLightingCtrl);
            this.tabcontrol1.Controls.Add(this.tpWeighing);
            this.tabcontrol1.Controls.Add(this.tpPressureCtrl);
            this.tabcontrol1.Controls.Add(this.tpTempCtrl);
            this.tabcontrol1.Controls.Add(this.tpDispCtrl);
            this.tabcontrol1.Controls.Add(this.tpDisplay);
            this.tabcontrol1.Controls.Add(this.tpAdvance);
            this.tabcontrol1.Controls.Add(this.tpMakerData);
            this.tabcontrol1.Controls.Add(this.tpOption);
            this.tabcontrol1.Controls.Add(this.tpGem);
            this.tabcontrol1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabcontrol1.ItemSize = new System.Drawing.Size(100, 35);
            this.tabcontrol1.Location = new System.Drawing.Point(5, 5);
            this.tabcontrol1.Multiline = true;
            this.tabcontrol1.Name = "tabcontrol1";
            this.tabcontrol1.SelectedIndex = 0;
            this.tabcontrol1.Size = new System.Drawing.Size(617, 438);
            this.tabcontrol1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabcontrol1.TabIndex = 5;
            // 
            // tpConfig
            // 
            this.tpConfig.Controls.Add(this.flowLayoutPanel5);
            this.tpConfig.Location = new System.Drawing.Point(4, 115);
            this.tpConfig.Name = "tpConfig";
            this.tpConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tpConfig.Size = new System.Drawing.Size(609, 319);
            this.tpConfig.TabIndex = 0;
            this.tpConfig.Text = "Config";
            this.tpConfig.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel5.Location = new System.Drawing.Point(3, 266);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(603, 50);
            this.flowLayoutPanel5.TabIndex = 7;
            // 
            // tpGantry
            // 
            this.tpGantry.Controls.Add(this.flowLayoutPanel4);
            this.tpGantry.Location = new System.Drawing.Point(4, 115);
            this.tpGantry.Name = "tpGantry";
            this.tpGantry.Padding = new System.Windows.Forms.Padding(3);
            this.tpGantry.Size = new System.Drawing.Size(609, 319);
            this.tpGantry.TabIndex = 1;
            this.tpGantry.Text = "Gantry";
            this.tpGantry.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(3, 266);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(603, 50);
            this.flowLayoutPanel4.TabIndex = 7;
            // 
            // tpConveyor
            // 
            this.tpConveyor.Location = new System.Drawing.Point(4, 115);
            this.tpConveyor.Name = "tpConveyor";
            this.tpConveyor.Padding = new System.Windows.Forms.Padding(3);
            this.tpConveyor.Size = new System.Drawing.Size(609, 319);
            this.tpConveyor.TabIndex = 12;
            this.tpConveyor.Text = "Conveyor";
            this.tpConveyor.UseVisualStyleBackColor = true;
            // 
            // tpHSensor
            // 
            this.tpHSensor.Controls.Add(this.flowLayoutPanel2);
            this.tpHSensor.Location = new System.Drawing.Point(4, 115);
            this.tpHSensor.Name = "tpHSensor";
            this.tpHSensor.Padding = new System.Windows.Forms.Padding(3);
            this.tpHSensor.Size = new System.Drawing.Size(609, 319);
            this.tpHSensor.TabIndex = 2;
            this.tpHSensor.Text = "HSensor";
            this.tpHSensor.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.btnHsensorOpen);
            this.flowLayoutPanel2.Controls.Add(this.btnHSensorTest);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 270);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel2.Size = new System.Drawing.Size(603, 46);
            this.flowLayoutPanel2.TabIndex = 5;
            // 
            // btnHsensorOpen
            // 
            this.btnHsensorOpen.Location = new System.Drawing.Point(500, 3);
            this.btnHsensorOpen.Name = "btnHsensorOpen";
            this.btnHsensorOpen.Size = new System.Drawing.Size(100, 40);
            this.btnHsensorOpen.TabIndex = 2;
            this.btnHsensorOpen.Text = "Open";
            this.btnHsensorOpen.UseVisualStyleBackColor = true;
            this.btnHsensorOpen.Click += new System.EventHandler(this.btnHsensorOpen_Click);
            // 
            // btnHSensorTest
            // 
            this.btnHSensorTest.Location = new System.Drawing.Point(394, 3);
            this.btnHSensorTest.Name = "btnHSensorTest";
            this.btnHSensorTest.Size = new System.Drawing.Size(100, 40);
            this.btnHSensorTest.TabIndex = 3;
            this.btnHSensorTest.Text = "Test";
            this.btnHSensorTest.UseVisualStyleBackColor = true;
            this.btnHSensorTest.Visible = false;
            this.btnHSensorTest.Click += new System.EventHandler(this.btnHSensorTest_Click);
            // 
            // tpCamera
            // 
            this.tpCamera.Controls.Add(this.flowLayoutPanel3);
            this.tpCamera.Location = new System.Drawing.Point(4, 115);
            this.tpCamera.Name = "tpCamera";
            this.tpCamera.Padding = new System.Windows.Forms.Padding(3);
            this.tpCamera.Size = new System.Drawing.Size(609, 319);
            this.tpCamera.TabIndex = 3;
            this.tpCamera.Text = "Camera";
            this.tpCamera.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.btnCamOpen);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 266);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel3.Size = new System.Drawing.Size(603, 50);
            this.flowLayoutPanel3.TabIndex = 7;
            // 
            // btnCamOpen
            // 
            this.btnCamOpen.Location = new System.Drawing.Point(500, 3);
            this.btnCamOpen.Name = "btnCamOpen";
            this.btnCamOpen.Size = new System.Drawing.Size(100, 40);
            this.btnCamOpen.TabIndex = 2;
            this.btnCamOpen.Text = "Open";
            this.btnCamOpen.UseVisualStyleBackColor = true;
            this.btnCamOpen.Click += new System.EventHandler(this.btnCamOpen_Click);
            // 
            // tpLightingCtrl
            // 
            this.tpLightingCtrl.Controls.Add(this.flowLayoutPanel1);
            this.tpLightingCtrl.Location = new System.Drawing.Point(4, 115);
            this.tpLightingCtrl.Name = "tpLightingCtrl";
            this.tpLightingCtrl.Padding = new System.Windows.Forms.Padding(3);
            this.tpLightingCtrl.Size = new System.Drawing.Size(609, 319);
            this.tpLightingCtrl.TabIndex = 4;
            this.tpLightingCtrl.Text = "LightingCtrl";
            this.tpLightingCtrl.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.btnLightOpen);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 270);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(603, 46);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // btnLightOpen
            // 
            this.btnLightOpen.Location = new System.Drawing.Point(500, 3);
            this.btnLightOpen.Name = "btnLightOpen";
            this.btnLightOpen.Size = new System.Drawing.Size(100, 40);
            this.btnLightOpen.TabIndex = 2;
            this.btnLightOpen.Text = "Open";
            this.btnLightOpen.UseVisualStyleBackColor = true;
            this.btnLightOpen.Click += new System.EventHandler(this.btnLightOpen_Click);
            // 
            // tpWeighing
            // 
            this.tpWeighing.Controls.Add(this.flowLayoutPanel7);
            this.tpWeighing.Location = new System.Drawing.Point(4, 115);
            this.tpWeighing.Name = "tpWeighing";
            this.tpWeighing.Padding = new System.Windows.Forms.Padding(3);
            this.tpWeighing.Size = new System.Drawing.Size(609, 319);
            this.tpWeighing.TabIndex = 5;
            this.tpWeighing.Text = "Weighing";
            this.tpWeighing.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel7
            // 
            this.flowLayoutPanel7.AutoSize = true;
            this.flowLayoutPanel7.Controls.Add(this.btnWeighOpen);
            this.flowLayoutPanel7.Controls.Add(this.btnWeighTest);
            this.flowLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel7.Location = new System.Drawing.Point(3, 270);
            this.flowLayoutPanel7.Name = "flowLayoutPanel7";
            this.flowLayoutPanel7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel7.Size = new System.Drawing.Size(603, 46);
            this.flowLayoutPanel7.TabIndex = 9;
            // 
            // btnWeighOpen
            // 
            this.btnWeighOpen.Location = new System.Drawing.Point(500, 3);
            this.btnWeighOpen.Name = "btnWeighOpen";
            this.btnWeighOpen.Size = new System.Drawing.Size(100, 40);
            this.btnWeighOpen.TabIndex = 2;
            this.btnWeighOpen.Text = "Open";
            this.btnWeighOpen.UseVisualStyleBackColor = true;
            this.btnWeighOpen.Click += new System.EventHandler(this.btnWeighOpen_Click);
            // 
            // btnWeighTest
            // 
            this.btnWeighTest.Location = new System.Drawing.Point(394, 3);
            this.btnWeighTest.Name = "btnWeighTest";
            this.btnWeighTest.Size = new System.Drawing.Size(100, 40);
            this.btnWeighTest.TabIndex = 4;
            this.btnWeighTest.Text = "Test";
            this.btnWeighTest.UseVisualStyleBackColor = true;
            this.btnWeighTest.Click += new System.EventHandler(this.btnWeighTest_Click);
            // 
            // tpPressureCtrl
            // 
            this.tpPressureCtrl.Controls.Add(this.panel2);
            this.tpPressureCtrl.Location = new System.Drawing.Point(4, 115);
            this.tpPressureCtrl.Name = "tpPressureCtrl";
            this.tpPressureCtrl.Padding = new System.Windows.Forms.Padding(3);
            this.tpPressureCtrl.Size = new System.Drawing.Size(609, 319);
            this.tpPressureCtrl.TabIndex = 6;
            this.tpPressureCtrl.Text = "PressureCtrl";
            this.tpPressureCtrl.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.flowLayoutPanel8);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 267);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(603, 49);
            this.panel2.TabIndex = 10;
            // 
            // flowLayoutPanel8
            // 
            this.flowLayoutPanel8.AutoSize = true;
            this.flowLayoutPanel8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel8.Controls.Add(this.btnPressCtrlOpen);
            this.flowLayoutPanel8.Controls.Add(this.btnPressCtrlTest);
            this.flowLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel8.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel8.Name = "flowLayoutPanel8";
            this.flowLayoutPanel8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel8.Size = new System.Drawing.Size(603, 46);
            this.flowLayoutPanel8.TabIndex = 9;
            // 
            // btnPressCtrlOpen
            // 
            this.btnPressCtrlOpen.Location = new System.Drawing.Point(500, 3);
            this.btnPressCtrlOpen.Name = "btnPressCtrlOpen";
            this.btnPressCtrlOpen.Size = new System.Drawing.Size(100, 40);
            this.btnPressCtrlOpen.TabIndex = 2;
            this.btnPressCtrlOpen.Text = "Open";
            this.btnPressCtrlOpen.UseVisualStyleBackColor = true;
            this.btnPressCtrlOpen.Click += new System.EventHandler(this.btnPressCtrlOpen_Click);
            // 
            // btnPressCtrlTest
            // 
            this.btnPressCtrlTest.Location = new System.Drawing.Point(394, 3);
            this.btnPressCtrlTest.Name = "btnPressCtrlTest";
            this.btnPressCtrlTest.Size = new System.Drawing.Size(100, 40);
            this.btnPressCtrlTest.TabIndex = 3;
            this.btnPressCtrlTest.Text = "Test";
            this.btnPressCtrlTest.UseVisualStyleBackColor = true;
            this.btnPressCtrlTest.Click += new System.EventHandler(this.btnPressCtrlTest_Click);
            // 
            // tpTempCtrl
            // 
            this.tpTempCtrl.Controls.Add(this.flowLayoutPanel9);
            this.tpTempCtrl.Location = new System.Drawing.Point(4, 115);
            this.tpTempCtrl.Name = "tpTempCtrl";
            this.tpTempCtrl.Padding = new System.Windows.Forms.Padding(3);
            this.tpTempCtrl.Size = new System.Drawing.Size(609, 319);
            this.tpTempCtrl.TabIndex = 7;
            this.tpTempCtrl.Text = "TempCtrl";
            this.tpTempCtrl.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel9
            // 
            this.flowLayoutPanel9.AutoSize = true;
            this.flowLayoutPanel9.Controls.Add(this.btnTempOpen);
            this.flowLayoutPanel9.Controls.Add(this.btnTempCtrlPanel);
            this.flowLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel9.Location = new System.Drawing.Point(3, 270);
            this.flowLayoutPanel9.Name = "flowLayoutPanel9";
            this.flowLayoutPanel9.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel9.Size = new System.Drawing.Size(603, 46);
            this.flowLayoutPanel9.TabIndex = 9;
            // 
            // btnTempOpen
            // 
            this.btnTempOpen.Location = new System.Drawing.Point(500, 3);
            this.btnTempOpen.Name = "btnTempOpen";
            this.btnTempOpen.Size = new System.Drawing.Size(100, 40);
            this.btnTempOpen.TabIndex = 2;
            this.btnTempOpen.Text = "Open";
            this.btnTempOpen.UseVisualStyleBackColor = true;
            this.btnTempOpen.Click += new System.EventHandler(this.btnTempOpen_Click);
            // 
            // btnTempCtrlPanel
            // 
            this.btnTempCtrlPanel.Location = new System.Drawing.Point(394, 3);
            this.btnTempCtrlPanel.Name = "btnTempCtrlPanel";
            this.btnTempCtrlPanel.Size = new System.Drawing.Size(100, 40);
            this.btnTempCtrlPanel.TabIndex = 4;
            this.btnTempCtrlPanel.Text = "Ctrl Panel";
            this.btnTempCtrlPanel.UseVisualStyleBackColor = true;
            this.btnTempCtrlPanel.Visible = false;
            this.btnTempCtrlPanel.Click += new System.EventHandler(this.btnTempCtrlPanel_Click);
            // 
            // tpDispCtrl
            // 
            this.tpDispCtrl.Controls.Add(this.flowLayoutPanel6);
            this.tpDispCtrl.Location = new System.Drawing.Point(4, 115);
            this.tpDispCtrl.Name = "tpDispCtrl";
            this.tpDispCtrl.Padding = new System.Windows.Forms.Padding(3);
            this.tpDispCtrl.Size = new System.Drawing.Size(609, 319);
            this.tpDispCtrl.TabIndex = 11;
            this.tpDispCtrl.Text = "Pump";
            this.tpDispCtrl.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel6
            // 
            this.flowLayoutPanel6.Controls.Add(this.btnDispCtrlOpen);
            this.flowLayoutPanel6.Controls.Add(this.btnDispExTempCtrlOpen);
            this.flowLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel6.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel6.Location = new System.Drawing.Point(3, 267);
            this.flowLayoutPanel6.Name = "flowLayoutPanel6";
            this.flowLayoutPanel6.Size = new System.Drawing.Size(603, 49);
            this.flowLayoutPanel6.TabIndex = 4;
            // 
            // btnDispCtrlOpen
            // 
            this.btnDispCtrlOpen.Location = new System.Drawing.Point(500, 3);
            this.btnDispCtrlOpen.Name = "btnDispCtrlOpen";
            this.btnDispCtrlOpen.Size = new System.Drawing.Size(100, 40);
            this.btnDispCtrlOpen.TabIndex = 2;
            this.btnDispCtrlOpen.Text = "Open";
            this.btnDispCtrlOpen.UseVisualStyleBackColor = true;
            this.btnDispCtrlOpen.Click += new System.EventHandler(this.btnDispCtrlOpen_Click);
            // 
            // btnDispExTempCtrlOpen
            // 
            this.btnDispExTempCtrlOpen.Location = new System.Drawing.Point(394, 3);
            this.btnDispExTempCtrlOpen.Name = "btnDispExTempCtrlOpen";
            this.btnDispExTempCtrlOpen.Size = new System.Drawing.Size(100, 40);
            this.btnDispExTempCtrlOpen.TabIndex = 3;
            this.btnDispExTempCtrlOpen.Text = "Open";
            this.btnDispExTempCtrlOpen.UseVisualStyleBackColor = true;
            this.btnDispExTempCtrlOpen.Click += new System.EventHandler(this.btnDispExTempCtrlOpen_Click_1);
            // 
            // tpDisplay
            // 
            this.tpDisplay.Location = new System.Drawing.Point(4, 115);
            this.tpDisplay.Name = "tpDisplay";
            this.tpDisplay.Padding = new System.Windows.Forms.Padding(3);
            this.tpDisplay.Size = new System.Drawing.Size(609, 319);
            this.tpDisplay.TabIndex = 8;
            this.tpDisplay.Text = "Display";
            this.tpDisplay.UseVisualStyleBackColor = true;
            // 
            // tpAdvance
            // 
            this.tpAdvance.Location = new System.Drawing.Point(4, 115);
            this.tpAdvance.Name = "tpAdvance";
            this.tpAdvance.Padding = new System.Windows.Forms.Padding(3);
            this.tpAdvance.Size = new System.Drawing.Size(609, 319);
            this.tpAdvance.TabIndex = 13;
            this.tpAdvance.Text = "Advance";
            this.tpAdvance.UseVisualStyleBackColor = true;
            // 
            // tpMakerData
            // 
            this.tpMakerData.Controls.Add(this.flowLayoutPanel10);
            this.tpMakerData.Location = new System.Drawing.Point(4, 115);
            this.tpMakerData.Name = "tpMakerData";
            this.tpMakerData.Padding = new System.Windows.Forms.Padding(3);
            this.tpMakerData.Size = new System.Drawing.Size(609, 319);
            this.tpMakerData.TabIndex = 14;
            this.tpMakerData.Text = "MakerData";
            this.tpMakerData.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel10
            // 
            this.flowLayoutPanel10.AutoSize = true;
            this.flowLayoutPanel10.Controls.Add(this.btnReset);
            this.flowLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel10.Location = new System.Drawing.Point(3, 270);
            this.flowLayoutPanel10.Name = "flowLayoutPanel10";
            this.flowLayoutPanel10.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel10.Size = new System.Drawing.Size(603, 46);
            this.flowLayoutPanel10.TabIndex = 10;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(500, 3);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(100, 40);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // tpOption
            // 
            this.tpOption.Controls.Add(this.cbxCentrePatAlign);
            this.tpOption.Location = new System.Drawing.Point(4, 115);
            this.tpOption.Name = "tpOption";
            this.tpOption.Padding = new System.Windows.Forms.Padding(3);
            this.tpOption.Size = new System.Drawing.Size(609, 319);
            this.tpOption.TabIndex = 15;
            this.tpOption.Text = "Option";
            this.tpOption.UseVisualStyleBackColor = true;
            // 
            // cbxCentrePatAlign
            // 
            this.cbxCentrePatAlign.AutoSize = true;
            this.cbxCentrePatAlign.Location = new System.Drawing.Point(6, 6);
            this.cbxCentrePatAlign.Name = "cbxCentrePatAlign";
            this.cbxCentrePatAlign.Size = new System.Drawing.Size(166, 18);
            this.cbxCentrePatAlign.TabIndex = 9;
            this.cbxCentrePatAlign.Text = "Prompt Centre Alignment";
            this.cbxCentrePatAlign.UseVisualStyleBackColor = true;
            this.cbxCentrePatAlign.Click += new System.EventHandler(this.cbxCentrePatAlign_Click);
            // 
            // tpGem
            // 
            this.tpGem.Controls.Add(this.flowLayoutPanel11);
            this.tpGem.Location = new System.Drawing.Point(4, 115);
            this.tpGem.Name = "tpGem";
            this.tpGem.Padding = new System.Windows.Forms.Padding(3);
            this.tpGem.Size = new System.Drawing.Size(609, 319);
            this.tpGem.TabIndex = 16;
            this.tpGem.Text = "Gem";
            this.tpGem.UseVisualStyleBackColor = true;
            // 
            // btnEquipConst
            // 
            this.btnEquipConst.Location = new System.Drawing.Point(394, 3);
            this.btnEquipConst.Name = "btnEquipConst";
            this.btnEquipConst.Size = new System.Drawing.Size(100, 40);
            this.btnEquipConst.TabIndex = 14;
            this.btnEquipConst.Text = "Export EquipConst";
            this.btnEquipConst.UseVisualStyleBackColor = true;
            this.btnEquipConst.Click += new System.EventHandler(this.btnEquipConst_Click);
            // 
            // btnSVID
            // 
            this.btnSVID.Location = new System.Drawing.Point(76, 3);
            this.btnSVID.Name = "btnSVID";
            this.btnSVID.Size = new System.Drawing.Size(100, 40);
            this.btnSVID.TabIndex = 13;
            this.btnSVID.Text = "Export SVID";
            this.btnSVID.UseVisualStyleBackColor = true;
            this.btnSVID.Click += new System.EventHandler(this.btnSVID_Click);
            // 
            // btnCEID
            // 
            this.btnCEID.Location = new System.Drawing.Point(182, 3);
            this.btnCEID.Name = "btnCEID";
            this.btnCEID.Size = new System.Drawing.Size(100, 40);
            this.btnCEID.TabIndex = 12;
            this.btnCEID.Text = "Export CEID";
            this.btnCEID.UseVisualStyleBackColor = true;
            this.btnCEID.Click += new System.EventHandler(this.btnCEID_Click);
            // 
            // btnALID
            // 
            this.btnALID.Location = new System.Drawing.Point(288, 3);
            this.btnALID.Name = "btnALID";
            this.btnALID.Size = new System.Drawing.Size(100, 40);
            this.btnALID.TabIndex = 11;
            this.btnALID.Text = "Export ALID";
            this.btnALID.UseVisualStyleBackColor = true;
            this.btnALID.Click += new System.EventHandler(this.btnALID_Click);
            // 
            // flowLayoutPanel11
            // 
            this.flowLayoutPanel11.AutoSize = true;
            this.flowLayoutPanel11.Controls.Add(this.btnSecsGemOpen);
            this.flowLayoutPanel11.Controls.Add(this.btnEquipConst);
            this.flowLayoutPanel11.Controls.Add(this.btnALID);
            this.flowLayoutPanel11.Controls.Add(this.btnCEID);
            this.flowLayoutPanel11.Controls.Add(this.btnSVID);
            this.flowLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel11.Location = new System.Drawing.Point(3, 270);
            this.flowLayoutPanel11.Name = "flowLayoutPanel11";
            this.flowLayoutPanel11.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel11.Size = new System.Drawing.Size(603, 46);
            this.flowLayoutPanel11.TabIndex = 15;
            // 
            // btnSecsGemOpen
            // 
            this.btnSecsGemOpen.Location = new System.Drawing.Point(500, 3);
            this.btnSecsGemOpen.Name = "btnSecsGemOpen";
            this.btnSecsGemOpen.Size = new System.Drawing.Size(100, 40);
            this.btnSecsGemOpen.TabIndex = 2;
            this.btnSecsGemOpen.Text = "Open";
            this.btnSecsGemOpen.UseVisualStyleBackColor = true;
            this.btnSecsGemOpen.Click += new System.EventHandler(this.btnSecsGemOpen_Click);
            // 
            // frmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 448);
            this.Controls.Add(this.tabcontrol1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmConfig";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Config";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmConfig_FormClosing);
            this.Load += new System.EventHandler(this.frmSystemCfg_Load);
            this.tabcontrol1.ResumeLayout(false);
            this.tpConfig.ResumeLayout(false);
            this.tpGantry.ResumeLayout(false);
            this.tpHSensor.ResumeLayout(false);
            this.tpHSensor.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.tpCamera.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.tpLightingCtrl.ResumeLayout(false);
            this.tpLightingCtrl.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tpWeighing.ResumeLayout(false);
            this.tpWeighing.PerformLayout();
            this.flowLayoutPanel7.ResumeLayout(false);
            this.tpPressureCtrl.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.flowLayoutPanel8.ResumeLayout(false);
            this.tpTempCtrl.ResumeLayout(false);
            this.tpTempCtrl.PerformLayout();
            this.flowLayoutPanel9.ResumeLayout(false);
            this.tpDispCtrl.ResumeLayout(false);
            this.flowLayoutPanel6.ResumeLayout(false);
            this.tpMakerData.ResumeLayout(false);
            this.tpMakerData.PerformLayout();
            this.flowLayoutPanel10.ResumeLayout(false);
            this.tpOption.ResumeLayout(false);
            this.tpOption.PerformLayout();
            this.tpGem.ResumeLayout(false);
            this.tpGem.PerformLayout();
            this.flowLayoutPanel11.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabControl tabcontrol1;
        private System.Windows.Forms.TabPage tpConfig;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.TabPage tpGantry;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.TabPage tpHSensor;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button btnHsensorOpen;
        private System.Windows.Forms.Button btnHSensorTest;
        private System.Windows.Forms.TabPage tpCamera;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Button btnCamOpen;
        private System.Windows.Forms.TabPage tpLightingCtrl;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnLightOpen;
        private System.Windows.Forms.TabPage tpWeighing;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel7;
        private System.Windows.Forms.Button btnWeighOpen;
        private System.Windows.Forms.Button btnWeighTest;
        private System.Windows.Forms.TabPage tpPressureCtrl;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel8;
        private System.Windows.Forms.Button btnPressCtrlOpen;
        private System.Windows.Forms.TabPage tpTempCtrl;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel9;
        private System.Windows.Forms.Button btnTempOpen;
        private System.Windows.Forms.TabPage tpDispCtrl;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
        private System.Windows.Forms.Button btnDispCtrlOpen;
        private System.Windows.Forms.Button btnDispExTempCtrlOpen;
        private System.Windows.Forms.TabPage tpDisplay;
        private System.Windows.Forms.Button btnPressCtrlTest;
        private System.Windows.Forms.TabPage tpConveyor;
        private System.Windows.Forms.TabPage tpAdvance;
        private System.Windows.Forms.TabPage tpMakerData;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel10;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnTempCtrlPanel;
        private System.Windows.Forms.TabPage tpOption;
        private System.Windows.Forms.CheckBox cbxCentrePatAlign;
        private System.Windows.Forms.TabPage tpGem;
        private System.Windows.Forms.Button btnSVID;
        private System.Windows.Forms.Button btnCEID;
        private System.Windows.Forms.Button btnALID;
        private System.Windows.Forms.Button btnEquipConst;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel11;
        private System.Windows.Forms.Button btnSecsGemOpen;
    }
}