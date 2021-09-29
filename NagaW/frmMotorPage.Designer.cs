namespace NagaW
{
    partial class frmMotorPage
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
            this.cbxModule = new System.Windows.Forms.ComboBox();
            this.btnHome = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStop0 = new System.Windows.Forms.Button();
            this.btnHomeAll = new System.Windows.Forms.Button();
            this.btnServoOn0 = new System.Windows.Forms.Button();
            this.btnResetError0 = new System.Windows.Forms.Button();
            this.lblAxis0 = new System.Windows.Forms.Label();
            this.lblEmg = new System.Windows.Forms.Label();
            this.lblSvrOn0 = new System.Windows.Forms.Label();
            this.lblAlarm0 = new System.Windows.Forms.Label();
            this.lblCmdPos0 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblActualPos0 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblActualPos1 = new System.Windows.Forms.Label();
            this.lblCmdPos1 = new System.Windows.Forms.Label();
            this.lblAlarm1 = new System.Windows.Forms.Label();
            this.lblSvrOn1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnResetError1 = new System.Windows.Forms.Button();
            this.btnServoOn1 = new System.Windows.Forms.Button();
            this.lblActualPos2 = new System.Windows.Forms.Label();
            this.lblCmdPos2 = new System.Windows.Forms.Label();
            this.lblAlarm2 = new System.Windows.Forms.Label();
            this.lblSvrOn2 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.btnResetError2 = new System.Windows.Forms.Button();
            this.btnServoOn2 = new System.Windows.Forms.Button();
            this.tmrDisplay = new System.Windows.Forms.Timer(this.components);
            this.lblPos0 = new System.Windows.Forms.Label();
            this.lblPos1 = new System.Windows.Forms.Label();
            this.lblPos2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnMove0 = new System.Windows.Forms.Button();
            this.btnMove1 = new System.Windows.Forms.Button();
            this.btnMove2 = new System.Windows.Forms.Button();
            this.btnStop1 = new System.Windows.Forms.Button();
            this.btnStop2 = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.pnlAxis2 = new System.Windows.Forms.Panel();
            this.lblHLmtN2 = new System.Windows.Forms.Label();
            this.lblHLmtP2 = new System.Windows.Forms.Label();
            this.pnlAxis1 = new System.Windows.Forms.Panel();
            this.lblHLmtN1 = new System.Windows.Forms.Label();
            this.lblHLmtP1 = new System.Windows.Forms.Label();
            this.lblHLmtN0 = new System.Windows.Forms.Label();
            this.lblHLmtP0 = new System.Windows.Forms.Label();
            this.pnlAxis2.SuspendLayout();
            this.pnlAxis1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbxModule
            // 
            this.cbxModule.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxModule.FormattingEnabled = true;
            this.cbxModule.Location = new System.Drawing.Point(114, 11);
            this.cbxModule.Name = "cbxModule";
            this.cbxModule.Size = new System.Drawing.Size(177, 33);
            this.cbxModule.TabIndex = 0;
            this.cbxModule.Text = "Module";
            this.cbxModule.SelectionChangeCommitted += new System.EventHandler(this.cbxModule_SelectionChangeCommitted);
            this.cbxModule.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbxModule_MouseClick);
            // 
            // btnHome
            // 
            this.btnHome.Location = new System.Drawing.Point(297, 8);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(100, 36);
            this.btnHome.TabIndex = 27;
            this.btnHome.Text = "Home";
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(3);
            this.label1.Size = new System.Drawing.Size(100, 36);
            this.label1.TabIndex = 28;
            this.label1.Text = "Module";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnStop0
            // 
            this.btnStop0.Location = new System.Drawing.Point(114, 424);
            this.btnStop0.Name = "btnStop0";
            this.btnStop0.Size = new System.Drawing.Size(100, 36);
            this.btnStop0.TabIndex = 42;
            this.btnStop0.Text = "Stop";
            this.btnStop0.UseVisualStyleBackColor = true;
            this.btnStop0.Click += new System.EventHandler(this.btnStop0_Click);
            // 
            // btnHomeAll
            // 
            this.btnHomeAll.Location = new System.Drawing.Point(450, 8);
            this.btnHomeAll.Name = "btnHomeAll";
            this.btnHomeAll.Size = new System.Drawing.Size(100, 36);
            this.btnHomeAll.TabIndex = 43;
            this.btnHomeAll.Text = "HomeAll";
            this.btnHomeAll.UseVisualStyleBackColor = true;
            this.btnHomeAll.Click += new System.EventHandler(this.btnHomeAll_Click);
            // 
            // btnServoOn0
            // 
            this.btnServoOn0.Location = new System.Drawing.Point(114, 144);
            this.btnServoOn0.Name = "btnServoOn0";
            this.btnServoOn0.Size = new System.Drawing.Size(100, 36);
            this.btnServoOn0.TabIndex = 44;
            this.btnServoOn0.Text = "ServoOn";
            this.btnServoOn0.UseVisualStyleBackColor = true;
            this.btnServoOn0.Click += new System.EventHandler(this.btnServoOn0_Click);
            // 
            // btnResetError0
            // 
            this.btnResetError0.Location = new System.Drawing.Point(114, 102);
            this.btnResetError0.Name = "btnResetError0";
            this.btnResetError0.Size = new System.Drawing.Size(100, 36);
            this.btnResetError0.TabIndex = 45;
            this.btnResetError0.Text = "Reset Error";
            this.btnResetError0.UseVisualStyleBackColor = true;
            this.btnResetError0.Click += new System.EventHandler(this.btnResetError0_Click);
            // 
            // lblAxis0
            // 
            this.lblAxis0.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAxis0.Location = new System.Drawing.Point(114, 60);
            this.lblAxis0.Margin = new System.Windows.Forms.Padding(3);
            this.lblAxis0.Name = "lblAxis0";
            this.lblAxis0.Padding = new System.Windows.Forms.Padding(3);
            this.lblAxis0.Size = new System.Drawing.Size(100, 36);
            this.lblAxis0.TabIndex = 46;
            this.lblAxis0.Text = "Axis0";
            this.lblAxis0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEmg
            // 
            this.lblEmg.BackColor = System.Drawing.Color.Gray;
            this.lblEmg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEmg.Location = new System.Drawing.Point(450, 50);
            this.lblEmg.Margin = new System.Windows.Forms.Padding(3);
            this.lblEmg.Name = "lblEmg";
            this.lblEmg.Padding = new System.Windows.Forms.Padding(3);
            this.lblEmg.Size = new System.Drawing.Size(100, 22);
            this.lblEmg.TabIndex = 47;
            this.lblEmg.Text = "Emg";
            this.lblEmg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSvrOn0
            // 
            this.lblSvrOn0.BackColor = System.Drawing.Color.Gray;
            this.lblSvrOn0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSvrOn0.Location = new System.Drawing.Point(114, 186);
            this.lblSvrOn0.Margin = new System.Windows.Forms.Padding(3);
            this.lblSvrOn0.Name = "lblSvrOn0";
            this.lblSvrOn0.Padding = new System.Windows.Forms.Padding(3);
            this.lblSvrOn0.Size = new System.Drawing.Size(100, 22);
            this.lblSvrOn0.TabIndex = 48;
            this.lblSvrOn0.Text = "Svr On";
            this.lblSvrOn0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAlarm0
            // 
            this.lblAlarm0.BackColor = System.Drawing.Color.Gray;
            this.lblAlarm0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAlarm0.Location = new System.Drawing.Point(114, 214);
            this.lblAlarm0.Margin = new System.Windows.Forms.Padding(3);
            this.lblAlarm0.Name = "lblAlarm0";
            this.lblAlarm0.Padding = new System.Windows.Forms.Padding(3);
            this.lblAlarm0.Size = new System.Drawing.Size(100, 22);
            this.lblAlarm0.TabIndex = 49;
            this.lblAlarm0.Text = "Alarm";
            this.lblAlarm0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCmdPos0
            // 
            this.lblCmdPos0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCmdPos0.Location = new System.Drawing.Point(114, 298);
            this.lblCmdPos0.Margin = new System.Windows.Forms.Padding(3);
            this.lblCmdPos0.Name = "lblCmdPos0";
            this.lblCmdPos0.Padding = new System.Windows.Forms.Padding(3);
            this.lblCmdPos0.Size = new System.Drawing.Size(100, 22);
            this.lblCmdPos0.TabIndex = 50;
            this.lblCmdPos0.Text = "0.000";
            this.lblCmdPos0.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 298);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(3);
            this.label3.Size = new System.Drawing.Size(100, 22);
            this.label3.TabIndex = 51;
            this.label3.Text = "Cmd Pos:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblActualPos0
            // 
            this.lblActualPos0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblActualPos0.Location = new System.Drawing.Point(114, 326);
            this.lblActualPos0.Margin = new System.Windows.Forms.Padding(3);
            this.lblActualPos0.Name = "lblActualPos0";
            this.lblActualPos0.Padding = new System.Windows.Forms.Padding(3);
            this.lblActualPos0.Size = new System.Drawing.Size(100, 22);
            this.lblActualPos0.TabIndex = 53;
            this.lblActualPos0.Text = "0.000";
            this.lblActualPos0.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 326);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(3);
            this.label4.Size = new System.Drawing.Size(100, 22);
            this.label4.TabIndex = 52;
            this.label4.Text = "Actual Pos:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblActualPos1
            // 
            this.lblActualPos1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblActualPos1.Location = new System.Drawing.Point(0, 266);
            this.lblActualPos1.Margin = new System.Windows.Forms.Padding(3);
            this.lblActualPos1.Name = "lblActualPos1";
            this.lblActualPos1.Padding = new System.Windows.Forms.Padding(3);
            this.lblActualPos1.Size = new System.Drawing.Size(100, 22);
            this.lblActualPos1.TabIndex = 60;
            this.lblActualPos1.Text = "0.000";
            this.lblActualPos1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCmdPos1
            // 
            this.lblCmdPos1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCmdPos1.Location = new System.Drawing.Point(0, 238);
            this.lblCmdPos1.Margin = new System.Windows.Forms.Padding(3);
            this.lblCmdPos1.Name = "lblCmdPos1";
            this.lblCmdPos1.Padding = new System.Windows.Forms.Padding(3);
            this.lblCmdPos1.Size = new System.Drawing.Size(100, 22);
            this.lblCmdPos1.TabIndex = 59;
            this.lblCmdPos1.Text = "0.000";
            this.lblCmdPos1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAlarm1
            // 
            this.lblAlarm1.BackColor = System.Drawing.Color.Gray;
            this.lblAlarm1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAlarm1.Location = new System.Drawing.Point(0, 154);
            this.lblAlarm1.Margin = new System.Windows.Forms.Padding(3);
            this.lblAlarm1.Name = "lblAlarm1";
            this.lblAlarm1.Padding = new System.Windows.Forms.Padding(3);
            this.lblAlarm1.Size = new System.Drawing.Size(100, 22);
            this.lblAlarm1.TabIndex = 58;
            this.lblAlarm1.Text = "Alarm";
            this.lblAlarm1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSvrOn1
            // 
            this.lblSvrOn1.BackColor = System.Drawing.Color.Gray;
            this.lblSvrOn1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSvrOn1.Location = new System.Drawing.Point(0, 126);
            this.lblSvrOn1.Margin = new System.Windows.Forms.Padding(3);
            this.lblSvrOn1.Name = "lblSvrOn1";
            this.lblSvrOn1.Padding = new System.Windows.Forms.Padding(3);
            this.lblSvrOn1.Size = new System.Drawing.Size(100, 22);
            this.lblSvrOn1.TabIndex = 57;
            this.lblSvrOn1.Text = "Svr On";
            this.lblSvrOn1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.label8.Name = "label8";
            this.label8.Padding = new System.Windows.Forms.Padding(3);
            this.label8.Size = new System.Drawing.Size(100, 36);
            this.label8.TabIndex = 56;
            this.label8.Text = "Axis1";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnResetError1
            // 
            this.btnResetError1.Location = new System.Drawing.Point(0, 42);
            this.btnResetError1.Name = "btnResetError1";
            this.btnResetError1.Size = new System.Drawing.Size(100, 36);
            this.btnResetError1.TabIndex = 55;
            this.btnResetError1.Text = "Reset Error";
            this.btnResetError1.UseVisualStyleBackColor = true;
            this.btnResetError1.Click += new System.EventHandler(this.btnResetError1_Click);
            // 
            // btnServoOn1
            // 
            this.btnServoOn1.Location = new System.Drawing.Point(0, 84);
            this.btnServoOn1.Name = "btnServoOn1";
            this.btnServoOn1.Size = new System.Drawing.Size(100, 36);
            this.btnServoOn1.TabIndex = 54;
            this.btnServoOn1.Text = "ServoOn";
            this.btnServoOn1.UseVisualStyleBackColor = true;
            this.btnServoOn1.Click += new System.EventHandler(this.btnServoOn1_Click);
            // 
            // lblActualPos2
            // 
            this.lblActualPos2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblActualPos2.Location = new System.Drawing.Point(0, 266);
            this.lblActualPos2.Margin = new System.Windows.Forms.Padding(3);
            this.lblActualPos2.Name = "lblActualPos2";
            this.lblActualPos2.Padding = new System.Windows.Forms.Padding(3);
            this.lblActualPos2.Size = new System.Drawing.Size(100, 22);
            this.lblActualPos2.TabIndex = 67;
            this.lblActualPos2.Text = "0.000";
            this.lblActualPos2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCmdPos2
            // 
            this.lblCmdPos2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCmdPos2.Location = new System.Drawing.Point(0, 238);
            this.lblCmdPos2.Margin = new System.Windows.Forms.Padding(3);
            this.lblCmdPos2.Name = "lblCmdPos2";
            this.lblCmdPos2.Padding = new System.Windows.Forms.Padding(3);
            this.lblCmdPos2.Size = new System.Drawing.Size(100, 22);
            this.lblCmdPos2.TabIndex = 66;
            this.lblCmdPos2.Text = "0.000";
            this.lblCmdPos2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAlarm2
            // 
            this.lblAlarm2.BackColor = System.Drawing.Color.Gray;
            this.lblAlarm2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAlarm2.Location = new System.Drawing.Point(0, 154);
            this.lblAlarm2.Margin = new System.Windows.Forms.Padding(3);
            this.lblAlarm2.Name = "lblAlarm2";
            this.lblAlarm2.Padding = new System.Windows.Forms.Padding(3);
            this.lblAlarm2.Size = new System.Drawing.Size(100, 22);
            this.lblAlarm2.TabIndex = 65;
            this.lblAlarm2.Text = "Alarm";
            this.lblAlarm2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSvrOn2
            // 
            this.lblSvrOn2.BackColor = System.Drawing.Color.Gray;
            this.lblSvrOn2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSvrOn2.Location = new System.Drawing.Point(0, 126);
            this.lblSvrOn2.Margin = new System.Windows.Forms.Padding(3);
            this.lblSvrOn2.Name = "lblSvrOn2";
            this.lblSvrOn2.Padding = new System.Windows.Forms.Padding(3);
            this.lblSvrOn2.Size = new System.Drawing.Size(100, 22);
            this.lblSvrOn2.TabIndex = 64;
            this.lblSvrOn2.Text = "Svr On";
            this.lblSvrOn2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Margin = new System.Windows.Forms.Padding(0);
            this.label13.Name = "label13";
            this.label13.Padding = new System.Windows.Forms.Padding(3);
            this.label13.Size = new System.Drawing.Size(100, 36);
            this.label13.TabIndex = 63;
            this.label13.Text = "Axis2";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnResetError2
            // 
            this.btnResetError2.Location = new System.Drawing.Point(0, 42);
            this.btnResetError2.Name = "btnResetError2";
            this.btnResetError2.Size = new System.Drawing.Size(100, 36);
            this.btnResetError2.TabIndex = 62;
            this.btnResetError2.Text = "Reset Error";
            this.btnResetError2.UseVisualStyleBackColor = true;
            this.btnResetError2.Click += new System.EventHandler(this.btnResetError2_Click);
            // 
            // btnServoOn2
            // 
            this.btnServoOn2.Location = new System.Drawing.Point(0, 84);
            this.btnServoOn2.Name = "btnServoOn2";
            this.btnServoOn2.Size = new System.Drawing.Size(100, 36);
            this.btnServoOn2.TabIndex = 61;
            this.btnServoOn2.Text = "ServoOn";
            this.btnServoOn2.UseVisualStyleBackColor = true;
            this.btnServoOn2.Click += new System.EventHandler(this.btnServoOn2_Click);
            // 
            // tmrDisplay
            // 
            this.tmrDisplay.Tick += new System.EventHandler(this.tmrDisplay_Tick);
            // 
            // lblPos0
            // 
            this.lblPos0.BackColor = System.Drawing.Color.White;
            this.lblPos0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPos0.Location = new System.Drawing.Point(114, 354);
            this.lblPos0.Margin = new System.Windows.Forms.Padding(3);
            this.lblPos0.Name = "lblPos0";
            this.lblPos0.Padding = new System.Windows.Forms.Padding(3);
            this.lblPos0.Size = new System.Drawing.Size(100, 22);
            this.lblPos0.TabIndex = 68;
            this.lblPos0.Text = "0.000";
            this.lblPos0.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPos0.Click += new System.EventHandler(this.lblPos0_Click);
            // 
            // lblPos1
            // 
            this.lblPos1.BackColor = System.Drawing.Color.White;
            this.lblPos1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPos1.Location = new System.Drawing.Point(0, 294);
            this.lblPos1.Margin = new System.Windows.Forms.Padding(3);
            this.lblPos1.Name = "lblPos1";
            this.lblPos1.Padding = new System.Windows.Forms.Padding(3);
            this.lblPos1.Size = new System.Drawing.Size(100, 22);
            this.lblPos1.TabIndex = 69;
            this.lblPos1.Text = "0.000";
            this.lblPos1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPos1.Click += new System.EventHandler(this.lblPos1_Click);
            // 
            // lblPos2
            // 
            this.lblPos2.BackColor = System.Drawing.Color.White;
            this.lblPos2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPos2.Location = new System.Drawing.Point(0, 294);
            this.lblPos2.Margin = new System.Windows.Forms.Padding(3);
            this.lblPos2.Name = "lblPos2";
            this.lblPos2.Padding = new System.Windows.Forms.Padding(3);
            this.lblPos2.Size = new System.Drawing.Size(100, 22);
            this.lblPos2.TabIndex = 70;
            this.lblPos2.Text = "0.000";
            this.lblPos2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPos2.Click += new System.EventHandler(this.lblPos2_Click);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(8, 354);
            this.label6.Margin = new System.Windows.Forms.Padding(3);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(3);
            this.label6.Size = new System.Drawing.Size(100, 22);
            this.label6.TabIndex = 71;
            this.label6.Text = "Position:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnMove0
            // 
            this.btnMove0.Location = new System.Drawing.Point(114, 382);
            this.btnMove0.Name = "btnMove0";
            this.btnMove0.Size = new System.Drawing.Size(100, 36);
            this.btnMove0.TabIndex = 72;
            this.btnMove0.Text = "Move";
            this.btnMove0.UseVisualStyleBackColor = true;
            this.btnMove0.Click += new System.EventHandler(this.btnMove0_Click);
            // 
            // btnMove1
            // 
            this.btnMove1.Location = new System.Drawing.Point(0, 322);
            this.btnMove1.Name = "btnMove1";
            this.btnMove1.Size = new System.Drawing.Size(100, 36);
            this.btnMove1.TabIndex = 73;
            this.btnMove1.Text = "Move";
            this.btnMove1.UseVisualStyleBackColor = true;
            this.btnMove1.Click += new System.EventHandler(this.btnMove1_Click);
            // 
            // btnMove2
            // 
            this.btnMove2.Location = new System.Drawing.Point(0, 322);
            this.btnMove2.Name = "btnMove2";
            this.btnMove2.Size = new System.Drawing.Size(100, 36);
            this.btnMove2.TabIndex = 74;
            this.btnMove2.Text = "Move";
            this.btnMove2.UseVisualStyleBackColor = true;
            this.btnMove2.Click += new System.EventHandler(this.btnMove2_Click);
            // 
            // btnStop1
            // 
            this.btnStop1.Location = new System.Drawing.Point(0, 364);
            this.btnStop1.Name = "btnStop1";
            this.btnStop1.Size = new System.Drawing.Size(100, 36);
            this.btnStop1.TabIndex = 75;
            this.btnStop1.Text = "Stop";
            this.btnStop1.UseVisualStyleBackColor = true;
            this.btnStop1.Click += new System.EventHandler(this.btnStop1_Click);
            // 
            // btnStop2
            // 
            this.btnStop2.Location = new System.Drawing.Point(0, 364);
            this.btnStop2.Name = "btnStop2";
            this.btnStop2.Size = new System.Drawing.Size(100, 36);
            this.btnStop2.TabIndex = 76;
            this.btnStop2.Text = "Stop";
            this.btnStop2.UseVisualStyleBackColor = true;
            this.btnStop2.Click += new System.EventHandler(this.btnStop2_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(450, 326);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 35);
            this.btnClose.TabIndex = 79;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(450, 285);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(100, 35);
            this.btnOpen.TabIndex = 78;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // pnlAxis2
            // 
            this.pnlAxis2.AutoSize = true;
            this.pnlAxis2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlAxis2.Controls.Add(this.lblHLmtN2);
            this.pnlAxis2.Controls.Add(this.label13);
            this.pnlAxis2.Controls.Add(this.lblHLmtP2);
            this.pnlAxis2.Controls.Add(this.btnServoOn2);
            this.pnlAxis2.Controls.Add(this.btnResetError2);
            this.pnlAxis2.Controls.Add(this.lblSvrOn2);
            this.pnlAxis2.Controls.Add(this.btnStop2);
            this.pnlAxis2.Controls.Add(this.lblAlarm2);
            this.pnlAxis2.Controls.Add(this.lblCmdPos2);
            this.pnlAxis2.Controls.Add(this.btnMove2);
            this.pnlAxis2.Controls.Add(this.lblActualPos2);
            this.pnlAxis2.Controls.Add(this.lblPos2);
            this.pnlAxis2.Location = new System.Drawing.Point(326, 60);
            this.pnlAxis2.Name = "pnlAxis2";
            this.pnlAxis2.Size = new System.Drawing.Size(103, 403);
            this.pnlAxis2.TabIndex = 80;
            // 
            // lblHLmtN2
            // 
            this.lblHLmtN2.BackColor = System.Drawing.Color.Gray;
            this.lblHLmtN2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblHLmtN2.Location = new System.Drawing.Point(0, 210);
            this.lblHLmtN2.Margin = new System.Windows.Forms.Padding(3);
            this.lblHLmtN2.Name = "lblHLmtN2";
            this.lblHLmtN2.Padding = new System.Windows.Forms.Padding(3);
            this.lblHLmtN2.Size = new System.Drawing.Size(100, 22);
            this.lblHLmtN2.TabIndex = 87;
            this.lblHLmtN2.Text = "HLmtN";
            this.lblHLmtN2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHLmtP2
            // 
            this.lblHLmtP2.BackColor = System.Drawing.Color.Gray;
            this.lblHLmtP2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblHLmtP2.Location = new System.Drawing.Point(0, 182);
            this.lblHLmtP2.Margin = new System.Windows.Forms.Padding(3);
            this.lblHLmtP2.Name = "lblHLmtP2";
            this.lblHLmtP2.Padding = new System.Windows.Forms.Padding(3);
            this.lblHLmtP2.Size = new System.Drawing.Size(100, 22);
            this.lblHLmtP2.TabIndex = 86;
            this.lblHLmtP2.Text = "HLmtP";
            this.lblHLmtP2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlAxis1
            // 
            this.pnlAxis1.AutoSize = true;
            this.pnlAxis1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlAxis1.Controls.Add(this.label8);
            this.pnlAxis1.Controls.Add(this.btnServoOn1);
            this.pnlAxis1.Controls.Add(this.btnResetError1);
            this.pnlAxis1.Controls.Add(this.lblSvrOn1);
            this.pnlAxis1.Controls.Add(this.lblHLmtN1);
            this.pnlAxis1.Controls.Add(this.lblAlarm1);
            this.pnlAxis1.Controls.Add(this.lblHLmtP1);
            this.pnlAxis1.Controls.Add(this.btnStop1);
            this.pnlAxis1.Controls.Add(this.lblCmdPos1);
            this.pnlAxis1.Controls.Add(this.btnMove1);
            this.pnlAxis1.Controls.Add(this.lblActualPos1);
            this.pnlAxis1.Controls.Add(this.lblPos1);
            this.pnlAxis1.Location = new System.Drawing.Point(220, 60);
            this.pnlAxis1.Name = "pnlAxis1";
            this.pnlAxis1.Size = new System.Drawing.Size(103, 403);
            this.pnlAxis1.TabIndex = 81;
            // 
            // lblHLmtN1
            // 
            this.lblHLmtN1.BackColor = System.Drawing.Color.Gray;
            this.lblHLmtN1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblHLmtN1.Location = new System.Drawing.Point(0, 210);
            this.lblHLmtN1.Margin = new System.Windows.Forms.Padding(3);
            this.lblHLmtN1.Name = "lblHLmtN1";
            this.lblHLmtN1.Padding = new System.Windows.Forms.Padding(3);
            this.lblHLmtN1.Size = new System.Drawing.Size(100, 22);
            this.lblHLmtN1.TabIndex = 83;
            this.lblHLmtN1.Text = "HLmtN";
            this.lblHLmtN1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHLmtP1
            // 
            this.lblHLmtP1.BackColor = System.Drawing.Color.Gray;
            this.lblHLmtP1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblHLmtP1.Location = new System.Drawing.Point(0, 182);
            this.lblHLmtP1.Margin = new System.Windows.Forms.Padding(3);
            this.lblHLmtP1.Name = "lblHLmtP1";
            this.lblHLmtP1.Padding = new System.Windows.Forms.Padding(3);
            this.lblHLmtP1.Size = new System.Drawing.Size(100, 22);
            this.lblHLmtP1.TabIndex = 82;
            this.lblHLmtP1.Text = "HLmtP";
            this.lblHLmtP1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHLmtN0
            // 
            this.lblHLmtN0.BackColor = System.Drawing.Color.Gray;
            this.lblHLmtN0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblHLmtN0.Location = new System.Drawing.Point(114, 270);
            this.lblHLmtN0.Margin = new System.Windows.Forms.Padding(3);
            this.lblHLmtN0.Name = "lblHLmtN0";
            this.lblHLmtN0.Padding = new System.Windows.Forms.Padding(3);
            this.lblHLmtN0.Size = new System.Drawing.Size(100, 22);
            this.lblHLmtN0.TabIndex = 85;
            this.lblHLmtN0.Text = "HLmtN";
            this.lblHLmtN0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHLmtP0
            // 
            this.lblHLmtP0.BackColor = System.Drawing.Color.Gray;
            this.lblHLmtP0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblHLmtP0.Location = new System.Drawing.Point(114, 242);
            this.lblHLmtP0.Margin = new System.Windows.Forms.Padding(3);
            this.lblHLmtP0.Name = "lblHLmtP0";
            this.lblHLmtP0.Padding = new System.Windows.Forms.Padding(3);
            this.lblHLmtP0.Size = new System.Drawing.Size(100, 22);
            this.lblHLmtP0.TabIndex = 84;
            this.lblHLmtP0.Text = "HLmtP";
            this.lblHLmtP0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmMotorPage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(556, 485);
            this.Controls.Add(this.lblHLmtN0);
            this.Controls.Add(this.lblHLmtP0);
            this.Controls.Add(this.pnlAxis1);
            this.Controls.Add(this.pnlAxis2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnMove0);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblPos0);
            this.Controls.Add(this.lblActualPos0);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblCmdPos0);
            this.Controls.Add(this.lblAlarm0);
            this.Controls.Add(this.lblSvrOn0);
            this.Controls.Add(this.lblEmg);
            this.Controls.Add(this.lblAxis0);
            this.Controls.Add(this.btnResetError0);
            this.Controls.Add(this.btnServoOn0);
            this.Controls.Add(this.btnHomeAll);
            this.Controls.Add(this.btnStop0);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnHome);
            this.Controls.Add(this.cbxModule);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmMotorPage";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "frmMotorPage";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMotorPage_FormClosing);
            this.Load += new System.EventHandler(this.frmMotorPage_Load);
            this.pnlAxis2.ResumeLayout(false);
            this.pnlAxis1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxModule;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStop0;
        private System.Windows.Forms.Button btnHomeAll;
        private System.Windows.Forms.Button btnServoOn0;
        private System.Windows.Forms.Button btnResetError0;
        private System.Windows.Forms.Label lblAxis0;
        private System.Windows.Forms.Label lblEmg;
        private System.Windows.Forms.Label lblSvrOn0;
        private System.Windows.Forms.Label lblAlarm0;
        private System.Windows.Forms.Label lblCmdPos0;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblActualPos0;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblActualPos1;
        private System.Windows.Forms.Label lblCmdPos1;
        private System.Windows.Forms.Label lblAlarm1;
        private System.Windows.Forms.Label lblSvrOn1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnResetError1;
        private System.Windows.Forms.Button btnServoOn1;
        private System.Windows.Forms.Label lblActualPos2;
        private System.Windows.Forms.Label lblCmdPos2;
        private System.Windows.Forms.Label lblAlarm2;
        private System.Windows.Forms.Label lblSvrOn2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnResetError2;
        private System.Windows.Forms.Button btnServoOn2;
        private System.Windows.Forms.Timer tmrDisplay;
        private System.Windows.Forms.Label lblPos0;
        private System.Windows.Forms.Label lblPos1;
        private System.Windows.Forms.Label lblPos2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnMove0;
        private System.Windows.Forms.Button btnMove1;
        private System.Windows.Forms.Button btnMove2;
        private System.Windows.Forms.Button btnStop1;
        private System.Windows.Forms.Button btnStop2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Panel pnlAxis2;
        private System.Windows.Forms.Panel pnlAxis1;
        private System.Windows.Forms.Label lblHLmtP1;
        private System.Windows.Forms.Label lblHLmtN1;
        private System.Windows.Forms.Label lblHLmtN2;
        private System.Windows.Forms.Label lblHLmtP2;
        private System.Windows.Forms.Label lblHLmtN0;
        private System.Windows.Forms.Label lblHLmtP0;
    }
}