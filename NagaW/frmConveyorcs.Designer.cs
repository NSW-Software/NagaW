namespace NagaW
{
    partial class frmConveyorcs
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
            this.btnLeftReturnRight = new System.Windows.Forms.Button();
            this.btnReturnLeft = new System.Windows.Forms.Button();
            this.btnLoadFromLeft = new System.Windows.Forms.Button();
            this.btnConvInitALL = new System.Windows.Forms.Button();
            this.btnLoadRightFromLeft = new System.Windows.Forms.Button();
            this.btnReturnRight = new System.Windows.Forms.Button();
            this.btnLoadFromRight = new System.Windows.Forms.Button();
            this.btnUnloadRight = new System.Windows.Forms.Button();
            this.btnLeftUnloadRight = new System.Windows.Forms.Button();
            this.lblLeftBdReady = new System.Windows.Forms.Label();
            this.lblLeftMcReady = new System.Windows.Forms.Label();
            this.lblRightMcReady = new System.Windows.Forms.Label();
            this.lblRightBdReady = new System.Windows.Forms.Label();
            this.tmrDisplay = new System.Windows.Forms.Timer(this.components);
            this.btnSmemaIn = new System.Windows.Forms.Button();
            this.btnSmemaSendOut = new System.Windows.Forms.Button();
            this.lblLeftBdStatus = new System.Windows.Forms.Label();
            this.lblRightBdStatus = new System.Windows.Forms.Label();
            this.btnTestRun = new System.Windows.Forms.Button();
            this.lblLeftConvStatus = new System.Windows.Forms.Label();
            this.lblLFConvStatus = new System.Windows.Forms.Label();
            this.lblLFBDStatus = new System.Windows.Forms.Label();
            this.lblRBDStatus = new System.Windows.Forms.Label();
            this.lblRConvStatus = new System.Windows.Forms.Label();
            this.lblRightConvStatus = new System.Windows.Forms.Label();
            this.btnStopRun = new System.Windows.Forms.Button();
            this.pnlAxis1 = new System.Windows.Forms.Panel();
            this.btnLeftSmemaOutTest = new System.Windows.Forms.Button();
            this.lblLeftSmemaOutSHow = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLefConvInit = new System.Windows.Forms.Button();
            this.lblLeftOutBdReady = new System.Windows.Forms.Label();
            this.lblPanelLConv = new System.Windows.Forms.Label();
            this.lblLeftOutMcReady = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRightSmemaInTest = new System.Windows.Forms.Button();
            this.lblRightSmemaINShow = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnRightConvInit = new System.Windows.Forms.Button();
            this.lblPanelRConv = new System.Windows.Forms.Label();
            this.lblRightInBdReady = new System.Windows.Forms.Label();
            this.lblRightInMcReady = new System.Windows.Forms.Label();
            this.btnRReturnLeft = new System.Windows.Forms.Button();
            this.chkContinuous = new System.Windows.Forms.CheckBox();
            this.lblContinuousTimeOut = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLeftUpDn = new System.Windows.Forms.Button();
            this.btnLeftFwd = new System.Windows.Forms.Button();
            this.btnLeftRev = new System.Windows.Forms.Button();
            this.btnRightRev = new System.Windows.Forms.Button();
            this.btnRightFwd = new System.Windows.Forms.Button();
            this.btnRightUpDn = new System.Windows.Forms.Button();
            this.pnlAxis1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLeftReturnRight
            // 
            this.btnLeftReturnRight.Location = new System.Drawing.Point(220, 165);
            this.btnLeftReturnRight.Name = "btnLeftReturnRight";
            this.btnLeftReturnRight.Size = new System.Drawing.Size(100, 35);
            this.btnLeftReturnRight.TabIndex = 17;
            this.btnLeftReturnRight.Text = "Return Right";
            this.btnLeftReturnRight.UseVisualStyleBackColor = true;
            this.btnLeftReturnRight.Click += new System.EventHandler(this.btnLeftReturnRight_Click);
            // 
            // btnReturnLeft
            // 
            this.btnReturnLeft.Location = new System.Drawing.Point(8, 165);
            this.btnReturnLeft.Name = "btnReturnLeft";
            this.btnReturnLeft.Size = new System.Drawing.Size(100, 35);
            this.btnReturnLeft.TabIndex = 15;
            this.btnReturnLeft.Text = "Return Left";
            this.btnReturnLeft.UseVisualStyleBackColor = true;
            this.btnReturnLeft.Click += new System.EventHandler(this.btnReturnLeft_Click);
            // 
            // btnLoadFromLeft
            // 
            this.btnLoadFromLeft.Location = new System.Drawing.Point(114, 165);
            this.btnLoadFromLeft.Name = "btnLoadFromLeft";
            this.btnLoadFromLeft.Size = new System.Drawing.Size(100, 35);
            this.btnLoadFromLeft.TabIndex = 16;
            this.btnLoadFromLeft.Text = "Load Fr Left";
            this.btnLoadFromLeft.UseVisualStyleBackColor = true;
            this.btnLoadFromLeft.Click += new System.EventHandler(this.btnLoadFromLeft_Click);
            // 
            // btnConvInitALL
            // 
            this.btnConvInitALL.Location = new System.Drawing.Point(8, 8);
            this.btnConvInitALL.Name = "btnConvInitALL";
            this.btnConvInitALL.Size = new System.Drawing.Size(100, 35);
            this.btnConvInitALL.TabIndex = 14;
            this.btnConvInitALL.Text = "ConvInit All";
            this.btnConvInitALL.UseVisualStyleBackColor = true;
            this.btnConvInitALL.Click += new System.EventHandler(this.btnConvInitAll_Click);
            // 
            // btnLoadRightFromLeft
            // 
            this.btnLoadRightFromLeft.Location = new System.Drawing.Point(8, 165);
            this.btnLoadRightFromLeft.Name = "btnLoadRightFromLeft";
            this.btnLoadRightFromLeft.Size = new System.Drawing.Size(100, 35);
            this.btnLoadRightFromLeft.TabIndex = 20;
            this.btnLoadRightFromLeft.Text = "Load Fr Left";
            this.btnLoadRightFromLeft.UseVisualStyleBackColor = true;
            this.btnLoadRightFromLeft.Click += new System.EventHandler(this.btnLoadRightFromLeft_Click);
            // 
            // btnReturnRight
            // 
            this.btnReturnRight.Location = new System.Drawing.Point(220, 165);
            this.btnReturnRight.Name = "btnReturnRight";
            this.btnReturnRight.Size = new System.Drawing.Size(100, 35);
            this.btnReturnRight.TabIndex = 19;
            this.btnReturnRight.Text = "Return Right";
            this.btnReturnRight.UseVisualStyleBackColor = true;
            this.btnReturnRight.Click += new System.EventHandler(this.btnReturnRight_Click);
            // 
            // btnLoadFromRight
            // 
            this.btnLoadFromRight.Location = new System.Drawing.Point(114, 165);
            this.btnLoadFromRight.Name = "btnLoadFromRight";
            this.btnLoadFromRight.Size = new System.Drawing.Size(100, 35);
            this.btnLoadFromRight.TabIndex = 18;
            this.btnLoadFromRight.Text = "Load Fr Right";
            this.btnLoadFromRight.UseVisualStyleBackColor = true;
            this.btnLoadFromRight.Click += new System.EventHandler(this.btnLoadFromRight_Click);
            // 
            // btnUnloadRight
            // 
            this.btnUnloadRight.Location = new System.Drawing.Point(778, 304);
            this.btnUnloadRight.Name = "btnUnloadRight";
            this.btnUnloadRight.Size = new System.Drawing.Size(100, 35);
            this.btnUnloadRight.TabIndex = 22;
            this.btnUnloadRight.Text = "Unload Right";
            this.btnUnloadRight.UseVisualStyleBackColor = true;
            this.btnUnloadRight.Visible = false;
            this.btnUnloadRight.Click += new System.EventHandler(this.btnUnloadRight_Click);
            // 
            // btnLeftUnloadRight
            // 
            this.btnLeftUnloadRight.Location = new System.Drawing.Point(778, 345);
            this.btnLeftUnloadRight.Name = "btnLeftUnloadRight";
            this.btnLeftUnloadRight.Size = new System.Drawing.Size(100, 35);
            this.btnLeftUnloadRight.TabIndex = 23;
            this.btnLeftUnloadRight.Text = "Unload Right";
            this.btnLeftUnloadRight.UseVisualStyleBackColor = true;
            this.btnLeftUnloadRight.Visible = false;
            this.btnLeftUnloadRight.Click += new System.EventHandler(this.btnLeftUnloadRight_Click);
            // 
            // lblLeftBdReady
            // 
            this.lblLeftBdReady.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLeftBdReady.Location = new System.Drawing.Point(220, 95);
            this.lblLeftBdReady.Margin = new System.Windows.Forms.Padding(3);
            this.lblLeftBdReady.Name = "lblLeftBdReady";
            this.lblLeftBdReady.Size = new System.Drawing.Size(100, 20);
            this.lblLeftBdReady.TabIndex = 24;
            this.lblLeftBdReady.Text = "[In] BdReady";
            this.lblLeftBdReady.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLeftBdReady.Click += new System.EventHandler(this.lblLeftBdReady_Click);
            this.lblLeftBdReady.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblLeftBdReady_MouseDown);
            this.lblLeftBdReady.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblLeftBdReady_MouseUp);
            // 
            // lblLeftMcReady
            // 
            this.lblLeftMcReady.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLeftMcReady.Location = new System.Drawing.Point(114, 95);
            this.lblLeftMcReady.Margin = new System.Windows.Forms.Padding(3);
            this.lblLeftMcReady.Name = "lblLeftMcReady";
            this.lblLeftMcReady.Size = new System.Drawing.Size(100, 20);
            this.lblLeftMcReady.TabIndex = 25;
            this.lblLeftMcReady.Text = "[Out] McReady";
            this.lblLeftMcReady.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRightMcReady
            // 
            this.lblRightMcReady.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRightMcReady.Location = new System.Drawing.Point(220, 95);
            this.lblRightMcReady.Margin = new System.Windows.Forms.Padding(3);
            this.lblRightMcReady.Name = "lblRightMcReady";
            this.lblRightMcReady.Size = new System.Drawing.Size(100, 20);
            this.lblRightMcReady.TabIndex = 27;
            this.lblRightMcReady.Text = "[In] McReady";
            this.lblRightMcReady.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRightMcReady.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblRightMcReady_MouseDown);
            this.lblRightMcReady.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblRightMcReady_MouseUp);
            // 
            // lblRightBdReady
            // 
            this.lblRightBdReady.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRightBdReady.Location = new System.Drawing.Point(114, 95);
            this.lblRightBdReady.Margin = new System.Windows.Forms.Padding(3);
            this.lblRightBdReady.Name = "lblRightBdReady";
            this.lblRightBdReady.Size = new System.Drawing.Size(100, 20);
            this.lblRightBdReady.TabIndex = 26;
            this.lblRightBdReady.Text = "[Out] BdReady";
            this.lblRightBdReady.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmrDisplay
            // 
            this.tmrDisplay.Interval = 10;
            this.tmrDisplay.Tick += new System.EventHandler(this.tmrDisplay_Tick);
            // 
            // btnSmemaIn
            // 
            this.btnSmemaIn.Location = new System.Drawing.Point(8, 206);
            this.btnSmemaIn.Name = "btnSmemaIn";
            this.btnSmemaIn.Size = new System.Drawing.Size(100, 35);
            this.btnSmemaIn.TabIndex = 28;
            this.btnSmemaIn.Text = "SMEMA IN";
            this.btnSmemaIn.UseVisualStyleBackColor = true;
            this.btnSmemaIn.Click += new System.EventHandler(this.btnSmemaIn_Click);
            // 
            // btnSmemaSendOut
            // 
            this.btnSmemaSendOut.Location = new System.Drawing.Point(8, 206);
            this.btnSmemaSendOut.Name = "btnSmemaSendOut";
            this.btnSmemaSendOut.Size = new System.Drawing.Size(100, 35);
            this.btnSmemaSendOut.TabIndex = 29;
            this.btnSmemaSendOut.Text = "SMEMA OUT";
            this.btnSmemaSendOut.UseVisualStyleBackColor = true;
            this.btnSmemaSendOut.Click += new System.EventHandler(this.btnSmemaSendOut_Click);
            // 
            // lblLeftBdStatus
            // 
            this.lblLeftBdStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLeftBdStatus.Location = new System.Drawing.Point(114, 69);
            this.lblLeftBdStatus.Margin = new System.Windows.Forms.Padding(3);
            this.lblLeftBdStatus.Name = "lblLeftBdStatus";
            this.lblLeftBdStatus.Size = new System.Drawing.Size(100, 20);
            this.lblLeftBdStatus.TabIndex = 30;
            this.lblLeftBdStatus.Text = "[In] BdReady";
            this.lblLeftBdStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLeftBdStatus.Click += new System.EventHandler(this.lblLeftBdStatus_Click);
            // 
            // lblRightBdStatus
            // 
            this.lblRightBdStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRightBdStatus.Location = new System.Drawing.Point(114, 69);
            this.lblRightBdStatus.Margin = new System.Windows.Forms.Padding(3);
            this.lblRightBdStatus.Name = "lblRightBdStatus";
            this.lblRightBdStatus.Size = new System.Drawing.Size(100, 20);
            this.lblRightBdStatus.TabIndex = 31;
            this.lblRightBdStatus.Text = "[In] BdReady";
            this.lblRightBdStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRightBdStatus.Click += new System.EventHandler(this.lblRightBdStatus_Click);
            // 
            // btnTestRun
            // 
            this.btnTestRun.Location = new System.Drawing.Point(16, 400);
            this.btnTestRun.Name = "btnTestRun";
            this.btnTestRun.Size = new System.Drawing.Size(100, 35);
            this.btnTestRun.TabIndex = 32;
            this.btnTestRun.Text = "Test Run";
            this.btnTestRun.UseVisualStyleBackColor = true;
            this.btnTestRun.Click += new System.EventHandler(this.btnTestRun_Click);
            // 
            // lblLeftConvStatus
            // 
            this.lblLeftConvStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLeftConvStatus.Location = new System.Drawing.Point(114, 43);
            this.lblLeftConvStatus.Margin = new System.Windows.Forms.Padding(3);
            this.lblLeftConvStatus.Name = "lblLeftConvStatus";
            this.lblLeftConvStatus.Size = new System.Drawing.Size(100, 20);
            this.lblLeftConvStatus.TabIndex = 33;
            this.lblLeftConvStatus.Text = "[In] BdReady";
            this.lblLeftConvStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLFConvStatus
            // 
            this.lblLFConvStatus.AutoSize = true;
            this.lblLFConvStatus.Location = new System.Drawing.Point(8, 46);
            this.lblLFConvStatus.Name = "lblLFConvStatus";
            this.lblLFConvStatus.Size = new System.Drawing.Size(105, 14);
            this.lblLFConvStatus.TabIndex = 34;
            this.lblLFConvStatus.Text = "Conveyor Status :";
            // 
            // lblLFBDStatus
            // 
            this.lblLFBDStatus.AutoSize = true;
            this.lblLFBDStatus.Location = new System.Drawing.Point(8, 70);
            this.lblLFBDStatus.Name = "lblLFBDStatus";
            this.lblLFBDStatus.Size = new System.Drawing.Size(89, 14);
            this.lblLFBDStatus.TabIndex = 35;
            this.lblLFBDStatus.Text = "Board Status : ";
            // 
            // lblRBDStatus
            // 
            this.lblRBDStatus.AutoSize = true;
            this.lblRBDStatus.Location = new System.Drawing.Point(8, 72);
            this.lblRBDStatus.Name = "lblRBDStatus";
            this.lblRBDStatus.Size = new System.Drawing.Size(89, 14);
            this.lblRBDStatus.TabIndex = 38;
            this.lblRBDStatus.Text = "Board Status : ";
            // 
            // lblRConvStatus
            // 
            this.lblRConvStatus.AutoSize = true;
            this.lblRConvStatus.Location = new System.Drawing.Point(8, 46);
            this.lblRConvStatus.Name = "lblRConvStatus";
            this.lblRConvStatus.Size = new System.Drawing.Size(105, 14);
            this.lblRConvStatus.TabIndex = 37;
            this.lblRConvStatus.Text = "Conveyor Status :";
            // 
            // lblRightConvStatus
            // 
            this.lblRightConvStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRightConvStatus.Location = new System.Drawing.Point(114, 43);
            this.lblRightConvStatus.Margin = new System.Windows.Forms.Padding(3);
            this.lblRightConvStatus.Name = "lblRightConvStatus";
            this.lblRightConvStatus.Size = new System.Drawing.Size(100, 20);
            this.lblRightConvStatus.TabIndex = 36;
            this.lblRightConvStatus.Text = "[In] BdReady";
            this.lblRightConvStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnStopRun
            // 
            this.btnStopRun.Location = new System.Drawing.Point(122, 400);
            this.btnStopRun.Name = "btnStopRun";
            this.btnStopRun.Size = new System.Drawing.Size(100, 35);
            this.btnStopRun.TabIndex = 39;
            this.btnStopRun.Text = "Stop Run";
            this.btnStopRun.UseVisualStyleBackColor = true;
            this.btnStopRun.Click += new System.EventHandler(this.btnStopRun_Click);
            // 
            // pnlAxis1
            // 
            this.pnlAxis1.AutoSize = true;
            this.pnlAxis1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlAxis1.Controls.Add(this.btnLeftRev);
            this.pnlAxis1.Controls.Add(this.btnLeftFwd);
            this.pnlAxis1.Controls.Add(this.btnLeftUpDn);
            this.pnlAxis1.Controls.Add(this.btnLeftSmemaOutTest);
            this.pnlAxis1.Controls.Add(this.lblLeftSmemaOutSHow);
            this.pnlAxis1.Controls.Add(this.label1);
            this.pnlAxis1.Controls.Add(this.btnLefConvInit);
            this.pnlAxis1.Controls.Add(this.lblLeftOutBdReady);
            this.pnlAxis1.Controls.Add(this.lblPanelLConv);
            this.pnlAxis1.Controls.Add(this.lblLeftOutMcReady);
            this.pnlAxis1.Controls.Add(this.lblLFConvStatus);
            this.pnlAxis1.Controls.Add(this.btnLoadFromLeft);
            this.pnlAxis1.Controls.Add(this.btnSmemaIn);
            this.pnlAxis1.Controls.Add(this.btnReturnLeft);
            this.pnlAxis1.Controls.Add(this.btnLeftReturnRight);
            this.pnlAxis1.Controls.Add(this.lblLFBDStatus);
            this.pnlAxis1.Controls.Add(this.lblLeftBdReady);
            this.pnlAxis1.Controls.Add(this.lblLeftConvStatus);
            this.pnlAxis1.Controls.Add(this.lblLeftMcReady);
            this.pnlAxis1.Controls.Add(this.lblLeftBdStatus);
            this.pnlAxis1.Location = new System.Drawing.Point(8, 49);
            this.pnlAxis1.Name = "pnlAxis1";
            this.pnlAxis1.Padding = new System.Windows.Forms.Padding(5);
            this.pnlAxis1.Size = new System.Drawing.Size(328, 339);
            this.pnlAxis1.TabIndex = 82;
            // 
            // btnLeftSmemaOutTest
            // 
            this.btnLeftSmemaOutTest.Location = new System.Drawing.Point(114, 206);
            this.btnLeftSmemaOutTest.Name = "btnLeftSmemaOutTest";
            this.btnLeftSmemaOutTest.Size = new System.Drawing.Size(100, 35);
            this.btnLeftSmemaOutTest.TabIndex = 92;
            this.btnLeftSmemaOutTest.Text = "SMEMA OUT";
            this.btnLeftSmemaOutTest.UseVisualStyleBackColor = true;
            this.btnLeftSmemaOutTest.Visible = false;
            this.btnLeftSmemaOutTest.Click += new System.EventHandler(this.btnLeftSmemaOutTest_Click);
            // 
            // lblLeftSmemaOutSHow
            // 
            this.lblLeftSmemaOutSHow.AutoSize = true;
            this.lblLeftSmemaOutSHow.Location = new System.Drawing.Point(8, 124);
            this.lblLeftSmemaOutSHow.Name = "lblLeftSmemaOutSHow";
            this.lblLeftSmemaOutSHow.Size = new System.Drawing.Size(84, 14);
            this.lblLeftSmemaOutSHow.TabIndex = 87;
            this.lblLeftSmemaOutSHow.Text = "Smema Out : ";
            this.lblLeftSmemaOutSHow.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 14);
            this.label1.TabIndex = 84;
            this.label1.Text = "Smema IN : ";
            // 
            // btnLefConvInit
            // 
            this.btnLefConvInit.Location = new System.Drawing.Point(220, 5);
            this.btnLefConvInit.Name = "btnLefConvInit";
            this.btnLefConvInit.Size = new System.Drawing.Size(100, 35);
            this.btnLefConvInit.TabIndex = 83;
            this.btnLefConvInit.Text = "ConvInit";
            this.btnLefConvInit.UseVisualStyleBackColor = true;
            this.btnLefConvInit.Click += new System.EventHandler(this.btnLefConvInit_Click);
            // 
            // lblLeftOutBdReady
            // 
            this.lblLeftOutBdReady.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLeftOutBdReady.Location = new System.Drawing.Point(220, 121);
            this.lblLeftOutBdReady.Margin = new System.Windows.Forms.Padding(3);
            this.lblLeftOutBdReady.Name = "lblLeftOutBdReady";
            this.lblLeftOutBdReady.Size = new System.Drawing.Size(100, 20);
            this.lblLeftOutBdReady.TabIndex = 85;
            this.lblLeftOutBdReady.Text = "[Out] BdReady";
            this.lblLeftOutBdReady.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLeftOutBdReady.Visible = false;
            // 
            // lblPanelLConv
            // 
            this.lblPanelLConv.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPanelLConv.Location = new System.Drawing.Point(5, 5);
            this.lblPanelLConv.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.lblPanelLConv.Name = "lblPanelLConv";
            this.lblPanelLConv.Padding = new System.Windows.Forms.Padding(3);
            this.lblPanelLConv.Size = new System.Drawing.Size(214, 38);
            this.lblPanelLConv.TabIndex = 56;
            this.lblPanelLConv.Text = "Left Conveyor";
            this.lblPanelLConv.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLeftOutMcReady
            // 
            this.lblLeftOutMcReady.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLeftOutMcReady.Location = new System.Drawing.Point(114, 121);
            this.lblLeftOutMcReady.Margin = new System.Windows.Forms.Padding(3);
            this.lblLeftOutMcReady.Name = "lblLeftOutMcReady";
            this.lblLeftOutMcReady.Size = new System.Drawing.Size(100, 20);
            this.lblLeftOutMcReady.TabIndex = 86;
            this.lblLeftOutMcReady.Text = "[IN] McReady";
            this.lblLeftOutMcReady.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLeftOutMcReady.Visible = false;
            this.lblLeftOutMcReady.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblLeftOutMcReady_MouseDown);
            this.lblLeftOutMcReady.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblLeftOutMcReady_MouseUp);
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.btnRightRev);
            this.panel1.Controls.Add(this.btnRightSmemaInTest);
            this.panel1.Controls.Add(this.btnRightFwd);
            this.panel1.Controls.Add(this.lblRightSmemaINShow);
            this.panel1.Controls.Add(this.btnRightUpDn);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btnRightConvInit);
            this.panel1.Controls.Add(this.lblPanelRConv);
            this.panel1.Controls.Add(this.lblRightInBdReady);
            this.panel1.Controls.Add(this.lblRightInMcReady);
            this.panel1.Controls.Add(this.btnSmemaSendOut);
            this.panel1.Controls.Add(this.btnLoadFromRight);
            this.panel1.Controls.Add(this.lblRightMcReady);
            this.panel1.Controls.Add(this.lblRightBdReady);
            this.panel1.Controls.Add(this.btnReturnRight);
            this.panel1.Controls.Add(this.lblRBDStatus);
            this.panel1.Controls.Add(this.btnLoadRightFromLeft);
            this.panel1.Controls.Add(this.lblRConvStatus);
            this.panel1.Controls.Add(this.lblRightConvStatus);
            this.panel1.Controls.Add(this.lblRightBdStatus);
            this.panel1.Location = new System.Drawing.Point(347, 49);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(328, 339);
            this.panel1.TabIndex = 84;
            // 
            // btnRightSmemaInTest
            // 
            this.btnRightSmemaInTest.Location = new System.Drawing.Point(114, 206);
            this.btnRightSmemaInTest.Name = "btnRightSmemaInTest";
            this.btnRightSmemaInTest.Size = new System.Drawing.Size(100, 35);
            this.btnRightSmemaInTest.TabIndex = 93;
            this.btnRightSmemaInTest.Text = "SMEMA IN";
            this.btnRightSmemaInTest.UseVisualStyleBackColor = true;
            this.btnRightSmemaInTest.Visible = false;
            this.btnRightSmemaInTest.Click += new System.EventHandler(this.btnRightSmemaInTest_Click);
            // 
            // lblRightSmemaINShow
            // 
            this.lblRightSmemaINShow.AutoSize = true;
            this.lblRightSmemaINShow.Location = new System.Drawing.Point(8, 124);
            this.lblRightSmemaINShow.Name = "lblRightSmemaINShow";
            this.lblRightSmemaINShow.Size = new System.Drawing.Size(75, 14);
            this.lblRightSmemaINShow.TabIndex = 91;
            this.lblRightSmemaINShow.Text = "Smema IN : ";
            this.lblRightSmemaINShow.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 14);
            this.label5.TabIndex = 90;
            this.label5.Text = "Smema Out : ";
            // 
            // btnRightConvInit
            // 
            this.btnRightConvInit.Location = new System.Drawing.Point(220, 5);
            this.btnRightConvInit.Name = "btnRightConvInit";
            this.btnRightConvInit.Size = new System.Drawing.Size(100, 35);
            this.btnRightConvInit.TabIndex = 84;
            this.btnRightConvInit.Text = "ConvInit";
            this.btnRightConvInit.UseVisualStyleBackColor = true;
            this.btnRightConvInit.Click += new System.EventHandler(this.btnRightConvInit_Click);
            // 
            // lblPanelRConv
            // 
            this.lblPanelRConv.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPanelRConv.Location = new System.Drawing.Point(5, 5);
            this.lblPanelRConv.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.lblPanelRConv.Name = "lblPanelRConv";
            this.lblPanelRConv.Padding = new System.Windows.Forms.Padding(3);
            this.lblPanelRConv.Size = new System.Drawing.Size(209, 38);
            this.lblPanelRConv.TabIndex = 56;
            this.lblPanelRConv.Text = "Right Conveyor";
            this.lblPanelRConv.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRightInBdReady
            // 
            this.lblRightInBdReady.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRightInBdReady.Location = new System.Drawing.Point(220, 121);
            this.lblRightInBdReady.Margin = new System.Windows.Forms.Padding(3);
            this.lblRightInBdReady.Name = "lblRightInBdReady";
            this.lblRightInBdReady.Size = new System.Drawing.Size(100, 20);
            this.lblRightInBdReady.TabIndex = 88;
            this.lblRightInBdReady.Text = "[In] BdReady";
            this.lblRightInBdReady.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRightInBdReady.Visible = false;
            this.lblRightInBdReady.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblRightInBdReady_MouseDown);
            this.lblRightInBdReady.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblRightInBdReady_MouseUp);
            // 
            // lblRightInMcReady
            // 
            this.lblRightInMcReady.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRightInMcReady.Location = new System.Drawing.Point(114, 121);
            this.lblRightInMcReady.Margin = new System.Windows.Forms.Padding(3);
            this.lblRightInMcReady.Name = "lblRightInMcReady";
            this.lblRightInMcReady.Size = new System.Drawing.Size(100, 20);
            this.lblRightInMcReady.TabIndex = 89;
            this.lblRightInMcReady.Text = "[Out] McReady";
            this.lblRightInMcReady.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRightInMcReady.Visible = false;
            // 
            // btnRReturnLeft
            // 
            this.btnRReturnLeft.Location = new System.Drawing.Point(778, 263);
            this.btnRReturnLeft.Name = "btnRReturnLeft";
            this.btnRReturnLeft.Size = new System.Drawing.Size(100, 35);
            this.btnRReturnLeft.TabIndex = 85;
            this.btnRReturnLeft.Text = "Return Left";
            this.btnRReturnLeft.UseVisualStyleBackColor = true;
            this.btnRReturnLeft.Visible = false;
            this.btnRReturnLeft.Click += new System.EventHandler(this.btnRReturnLeft_Click);
            // 
            // chkContinuous
            // 
            this.chkContinuous.AutoSize = true;
            this.chkContinuous.Location = new System.Drawing.Point(589, 424);
            this.chkContinuous.Name = "chkContinuous";
            this.chkContinuous.Size = new System.Drawing.Size(87, 18);
            this.chkContinuous.TabIndex = 84;
            this.chkContinuous.Text = "Continuous";
            this.chkContinuous.UseVisualStyleBackColor = true;
            // 
            // lblContinuousTimeOut
            // 
            this.lblContinuousTimeOut.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblContinuousTimeOut.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblContinuousTimeOut.Location = new System.Drawing.Point(609, 394);
            this.lblContinuousTimeOut.Margin = new System.Windows.Forms.Padding(3);
            this.lblContinuousTimeOut.Name = "lblContinuousTimeOut";
            this.lblContinuousTimeOut.Size = new System.Drawing.Size(66, 25);
            this.lblContinuousTimeOut.TabIndex = 85;
            this.lblContinuousTimeOut.Text = "1000 ms";
            this.lblContinuousTimeOut.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblContinuousTimeOut.Click += new System.EventHandler(this.lblContinuousTimeOut_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(465, 398);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 14);
            this.label2.TabIndex = 84;
            this.label2.Text = "Set Board Ready Timer:";
            // 
            // btnLeftUpDn
            // 
            this.btnLeftUpDn.Location = new System.Drawing.Point(114, 296);
            this.btnLeftUpDn.Name = "btnLeftUpDn";
            this.btnLeftUpDn.Size = new System.Drawing.Size(100, 35);
            this.btnLeftUpDn.TabIndex = 93;
            this.btnLeftUpDn.Text = "Conv Up/Dn";
            this.btnLeftUpDn.UseVisualStyleBackColor = true;
            this.btnLeftUpDn.Click += new System.EventHandler(this.btnLeftUpDn_Click);
            // 
            // btnLeftFwd
            // 
            this.btnLeftFwd.Location = new System.Drawing.Point(220, 296);
            this.btnLeftFwd.Name = "btnLeftFwd";
            this.btnLeftFwd.Size = new System.Drawing.Size(100, 35);
            this.btnLeftFwd.TabIndex = 94;
            this.btnLeftFwd.Text = "> Fwd >>>";
            this.btnLeftFwd.UseVisualStyleBackColor = true;
            this.btnLeftFwd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnLeftFwd_MouseDown);
            this.btnLeftFwd.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnLeftFwd_MouseUp);
            // 
            // btnLeftRev
            // 
            this.btnLeftRev.Location = new System.Drawing.Point(8, 296);
            this.btnLeftRev.Name = "btnLeftRev";
            this.btnLeftRev.Size = new System.Drawing.Size(100, 35);
            this.btnLeftRev.TabIndex = 95;
            this.btnLeftRev.Text = "<<< Rev <";
            this.btnLeftRev.UseVisualStyleBackColor = true;
            this.btnLeftRev.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnLeftRev_MouseDown);
            this.btnLeftRev.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnLeftRev_MouseUp);
            // 
            // btnRightRev
            // 
            this.btnRightRev.Location = new System.Drawing.Point(8, 296);
            this.btnRightRev.Name = "btnRightRev";
            this.btnRightRev.Size = new System.Drawing.Size(100, 35);
            this.btnRightRev.TabIndex = 98;
            this.btnRightRev.Text = "<<< Rev <";
            this.btnRightRev.UseVisualStyleBackColor = true;
            this.btnRightRev.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnRightRev_MouseDown);
            this.btnRightRev.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnRightRev_MouseUp);
            // 
            // btnRightFwd
            // 
            this.btnRightFwd.Location = new System.Drawing.Point(220, 296);
            this.btnRightFwd.Name = "btnRightFwd";
            this.btnRightFwd.Size = new System.Drawing.Size(100, 35);
            this.btnRightFwd.TabIndex = 97;
            this.btnRightFwd.Text = "> Fwd >>>";
            this.btnRightFwd.UseVisualStyleBackColor = true;
            this.btnRightFwd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnRightFwd_MouseDown);
            this.btnRightFwd.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnRightFwd_MouseUp);
            // 
            // btnRightUpDn
            // 
            this.btnRightUpDn.Location = new System.Drawing.Point(114, 296);
            this.btnRightUpDn.Name = "btnRightUpDn";
            this.btnRightUpDn.Size = new System.Drawing.Size(100, 35);
            this.btnRightUpDn.TabIndex = 96;
            this.btnRightUpDn.Text = "Conv Up/Dn";
            this.btnRightUpDn.UseVisualStyleBackColor = true;
            this.btnRightUpDn.Click += new System.EventHandler(this.btnRightUpDn_Click);
            // 
            // frmConveyorcs
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(917, 665);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblContinuousTimeOut);
            this.Controls.Add(this.chkContinuous);
            this.Controls.Add(this.btnRReturnLeft);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlAxis1);
            this.Controls.Add(this.btnStopRun);
            this.Controls.Add(this.btnTestRun);
            this.Controls.Add(this.btnConvInitALL);
            this.Controls.Add(this.btnUnloadRight);
            this.Controls.Add(this.btnLeftUnloadRight);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.Name = "frmConveyorcs";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "frmConveyorcs";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmConveyorcs_FormClosing);
            this.Load += new System.EventHandler(this.frmConveyorcs_Load);
            this.pnlAxis1.ResumeLayout(false);
            this.pnlAxis1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLeftReturnRight;
        private System.Windows.Forms.Button btnReturnLeft;
        private System.Windows.Forms.Button btnLoadFromLeft;
        private System.Windows.Forms.Button btnConvInitALL;
        private System.Windows.Forms.Button btnLoadRightFromLeft;
        private System.Windows.Forms.Button btnReturnRight;
        private System.Windows.Forms.Button btnLoadFromRight;
        private System.Windows.Forms.Button btnUnloadRight;
        private System.Windows.Forms.Button btnLeftUnloadRight;
        private System.Windows.Forms.Label lblLeftBdReady;
        private System.Windows.Forms.Label lblLeftMcReady;
        private System.Windows.Forms.Label lblRightMcReady;
        private System.Windows.Forms.Label lblRightBdReady;
        private System.Windows.Forms.Timer tmrDisplay;
        private System.Windows.Forms.Button btnSmemaIn;
        private System.Windows.Forms.Button btnSmemaSendOut;
        private System.Windows.Forms.Label lblRightBdStatus;
        public System.Windows.Forms.Label lblLeftBdStatus;
        private System.Windows.Forms.Button btnTestRun;
        public System.Windows.Forms.Label lblLeftConvStatus;
        private System.Windows.Forms.Label lblLFConvStatus;
        private System.Windows.Forms.Label lblLFBDStatus;
        private System.Windows.Forms.Label lblRBDStatus;
        private System.Windows.Forms.Label lblRConvStatus;
        public System.Windows.Forms.Label lblRightConvStatus;
        private System.Windows.Forms.Button btnStopRun;
        private System.Windows.Forms.Panel pnlAxis1;
        private System.Windows.Forms.Button btnLefConvInit;
        private System.Windows.Forms.Label lblPanelLConv;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRightConvInit;
        private System.Windows.Forms.Label lblPanelRConv;
        private System.Windows.Forms.CheckBox chkContinuous;
        private System.Windows.Forms.Label lblContinuousTimeOut;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRReturnLeft;
        private System.Windows.Forms.Button btnLeftSmemaOutTest;
        private System.Windows.Forms.Label lblLeftSmemaOutSHow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblLeftOutBdReady;
        private System.Windows.Forms.Label lblLeftOutMcReady;
        private System.Windows.Forms.Button btnRightSmemaInTest;
        private System.Windows.Forms.Label lblRightSmemaINShow;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblRightInBdReady;
        private System.Windows.Forms.Label lblRightInMcReady;
        private System.Windows.Forms.Button btnLeftRev;
        private System.Windows.Forms.Button btnLeftFwd;
        private System.Windows.Forms.Button btnLeftUpDn;
        private System.Windows.Forms.Button btnRightRev;
        private System.Windows.Forms.Button btnRightFwd;
        private System.Windows.Forms.Button btnRightUpDn;
    }
}