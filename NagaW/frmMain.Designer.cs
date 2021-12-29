namespace NagaW
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.tmrDisplay = new System.Windows.Forms.Timer(this.components);
            this.btnHomeAll = new System.Windows.Forms.Button();
            this.btnSetupPara = new System.Windows.Forms.Button();
            this.btnMotorPage = new System.Windows.Forms.Button();
            this.btnConfig = new System.Windows.Forms.Button();
            this.btnIOPage = new System.Windows.Forms.Button();
            this.btnRecipeLeft = new System.Windows.Forms.Button();
            this.btnRecipeRight = new System.Windows.Forms.Button();
            this.btnAuto = new System.Windows.Forms.Button();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnWafer = new System.Windows.Forms.Button();
            this.btnConv = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnSaveAll = new System.Windows.Forms.Button();
            this.btnSetupLeft = new System.Windows.Forms.Button();
            this.btnSetupRight = new System.Windows.Forms.Button();
            this.btnSaveRecipe = new System.Windows.Forms.Button();
            this.pnlSubSetup = new System.Windows.Forms.Panel();
            this.btnAdmin = new System.Windows.Forms.Button();
            this.ssBottom = new System.Windows.Forms.StatusStrip();
            this.tsslblEvent = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslblConnection = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslblSystemState = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslblUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlNone = new System.Windows.Forms.Panel();
            this.btnNone = new System.Windows.Forms.Button();
            this.tsTools = new System.Windows.Forms.ToolStrip();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlRecipe = new System.Windows.Forms.Panel();
            this.btnPressureMaster = new System.Windows.Forms.Button();
            this.btnRecipeTempCtrl = new System.Windows.Forms.Button();
            this.btnRecipePump = new System.Windows.Forms.Button();
            this.btnLoadRecipe = new System.Windows.Forms.Button();
            this.pnlConv = new System.Windows.Forms.Panel();
            this.btn = new System.Windows.Forms.Button();
            this.pnlAuto = new System.Windows.Forms.Panel();
            this.btnAutoPump = new System.Windows.Forms.Button();
            this.pnlTop.SuspendLayout();
            this.pnlSubSetup.SuspendLayout();
            this.ssBottom.SuspendLayout();
            this.pnlNone.SuspendLayout();
            this.pnlRecipe.SuspendLayout();
            this.pnlConv.SuspendLayout();
            this.pnlAuto.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmrDisplay
            // 
            this.tmrDisplay.Tick += new System.EventHandler(this.tmrDisplay_Tick);
            // 
            // btnHomeAll
            // 
            this.btnHomeAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHomeAll.Location = new System.Drawing.Point(897, 6);
            this.btnHomeAll.Name = "btnHomeAll";
            this.btnHomeAll.Size = new System.Drawing.Size(100, 35);
            this.btnHomeAll.TabIndex = 5;
            this.btnHomeAll.Text = "Init All";
            this.btnHomeAll.UseVisualStyleBackColor = true;
            this.btnHomeAll.Click += new System.EventHandler(this.btnHomeAll_Click);
            // 
            // btnSetupPara
            // 
            this.btnSetupPara.Location = new System.Drawing.Point(6, 6);
            this.btnSetupPara.Name = "btnSetupPara";
            this.btnSetupPara.Size = new System.Drawing.Size(100, 35);
            this.btnSetupPara.TabIndex = 8;
            this.btnSetupPara.Text = "SetupPara";
            this.btnSetupPara.UseVisualStyleBackColor = true;
            this.btnSetupPara.Click += new System.EventHandler(this.btnSetupPara_Click);
            // 
            // btnMotorPage
            // 
            this.btnMotorPage.Location = new System.Drawing.Point(112, 6);
            this.btnMotorPage.Name = "btnMotorPage";
            this.btnMotorPage.Size = new System.Drawing.Size(100, 35);
            this.btnMotorPage.TabIndex = 9;
            this.btnMotorPage.Text = "Motor Page";
            this.btnMotorPage.UseVisualStyleBackColor = true;
            this.btnMotorPage.Click += new System.EventHandler(this.btnMotorPage_Click);
            // 
            // btnConfig
            // 
            this.btnConfig.Location = new System.Drawing.Point(324, 6);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(100, 35);
            this.btnConfig.TabIndex = 10;
            this.btnConfig.Text = "Config";
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // btnIOPage
            // 
            this.btnIOPage.Location = new System.Drawing.Point(218, 6);
            this.btnIOPage.Name = "btnIOPage";
            this.btnIOPage.Size = new System.Drawing.Size(100, 35);
            this.btnIOPage.TabIndex = 11;
            this.btnIOPage.Text = "IO Page";
            this.btnIOPage.UseVisualStyleBackColor = true;
            this.btnIOPage.Click += new System.EventHandler(this.btnIOPage_Click);
            // 
            // btnRecipeLeft
            // 
            this.btnRecipeLeft.Location = new System.Drawing.Point(112, 6);
            this.btnRecipeLeft.Name = "btnRecipeLeft";
            this.btnRecipeLeft.Size = new System.Drawing.Size(100, 35);
            this.btnRecipeLeft.TabIndex = 12;
            this.btnRecipeLeft.Text = "Recipe";
            this.btnRecipeLeft.UseVisualStyleBackColor = true;
            this.btnRecipeLeft.Click += new System.EventHandler(this.btnRecipeLeft_Click);
            // 
            // btnRecipeRight
            // 
            this.btnRecipeRight.Location = new System.Drawing.Point(218, 6);
            this.btnRecipeRight.Name = "btnRecipeRight";
            this.btnRecipeRight.Size = new System.Drawing.Size(100, 35);
            this.btnRecipeRight.TabIndex = 13;
            this.btnRecipeRight.Text = "Recipe Right";
            this.btnRecipeRight.UseVisualStyleBackColor = true;
            this.btnRecipeRight.Click += new System.EventHandler(this.btnRecipeRight_Click);
            // 
            // btnAuto
            // 
            this.btnAuto.Location = new System.Drawing.Point(6, 6);
            this.btnAuto.Name = "btnAuto";
            this.btnAuto.Size = new System.Drawing.Size(100, 35);
            this.btnAuto.TabIndex = 14;
            this.btnAuto.Text = "Auto";
            this.btnAuto.UseVisualStyleBackColor = true;
            this.btnAuto.Click += new System.EventHandler(this.btnAuto_Click);
            // 
            // pnlTop
            // 
            this.pnlTop.AutoSize = true;
            this.pnlTop.Controls.Add(this.btnWafer);
            this.pnlTop.Controls.Add(this.btnConv);
            this.pnlTop.Controls.Add(this.btnSettings);
            this.pnlTop.Controls.Add(this.btnSaveAll);
            this.pnlTop.Controls.Add(this.btnSetupLeft);
            this.pnlTop.Controls.Add(this.btnRecipeLeft);
            this.pnlTop.Controls.Add(this.btnRecipeRight);
            this.pnlTop.Controls.Add(this.btnSetupRight);
            this.pnlTop.Controls.Add(this.btnHomeAll);
            this.pnlTop.Controls.Add(this.btnAuto);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(3);
            this.pnlTop.Size = new System.Drawing.Size(1003, 47);
            this.pnlTop.TabIndex = 16;
            // 
            // btnWafer
            // 
            this.btnWafer.Location = new System.Drawing.Point(748, 6);
            this.btnWafer.Name = "btnWafer";
            this.btnWafer.Size = new System.Drawing.Size(100, 35);
            this.btnWafer.TabIndex = 23;
            this.btnWafer.Text = "Wafer";
            this.btnWafer.UseVisualStyleBackColor = true;
            this.btnWafer.Click += new System.EventHandler(this.btnWafer_Click);
            // 
            // btnConv
            // 
            this.btnConv.Location = new System.Drawing.Point(536, 6);
            this.btnConv.Name = "btnConv";
            this.btnConv.Size = new System.Drawing.Size(100, 35);
            this.btnConv.TabIndex = 22;
            this.btnConv.Text = "Conveyor";
            this.btnConv.UseVisualStyleBackColor = true;
            this.btnConv.Click += new System.EventHandler(this.btnConv_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(642, 6);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(100, 35);
            this.btnSettings.TabIndex = 21;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnSaveAll
            // 
            this.btnSaveAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveAll.Location = new System.Drawing.Point(791, 6);
            this.btnSaveAll.Name = "btnSaveAll";
            this.btnSaveAll.Size = new System.Drawing.Size(100, 35);
            this.btnSaveAll.TabIndex = 12;
            this.btnSaveAll.Text = "Save All";
            this.btnSaveAll.UseVisualStyleBackColor = true;
            this.btnSaveAll.Click += new System.EventHandler(this.btnSaveAll_Click);
            // 
            // btnSetupLeft
            // 
            this.btnSetupLeft.Location = new System.Drawing.Point(324, 6);
            this.btnSetupLeft.Name = "btnSetupLeft";
            this.btnSetupLeft.Size = new System.Drawing.Size(100, 35);
            this.btnSetupLeft.TabIndex = 20;
            this.btnSetupLeft.Text = "Setup";
            this.btnSetupLeft.UseVisualStyleBackColor = true;
            this.btnSetupLeft.Click += new System.EventHandler(this.btnSetupLeft_Click);
            // 
            // btnSetupRight
            // 
            this.btnSetupRight.Location = new System.Drawing.Point(430, 6);
            this.btnSetupRight.Name = "btnSetupRight";
            this.btnSetupRight.Size = new System.Drawing.Size(100, 35);
            this.btnSetupRight.TabIndex = 12;
            this.btnSetupRight.Text = "SetupRight";
            this.btnSetupRight.UseVisualStyleBackColor = true;
            this.btnSetupRight.Click += new System.EventHandler(this.btnSetupRight_Click);
            // 
            // btnSaveRecipe
            // 
            this.btnSaveRecipe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveRecipe.Location = new System.Drawing.Point(897, 6);
            this.btnSaveRecipe.Name = "btnSaveRecipe";
            this.btnSaveRecipe.Size = new System.Drawing.Size(100, 35);
            this.btnSaveRecipe.TabIndex = 17;
            this.btnSaveRecipe.Text = "Save Recipe";
            this.btnSaveRecipe.UseVisualStyleBackColor = true;
            this.btnSaveRecipe.Click += new System.EventHandler(this.btnSaveRecipe_Click);
            // 
            // pnlSubSetup
            // 
            this.pnlSubSetup.AutoSize = true;
            this.pnlSubSetup.Controls.Add(this.btnAdmin);
            this.pnlSubSetup.Controls.Add(this.btnIOPage);
            this.pnlSubSetup.Controls.Add(this.btnMotorPage);
            this.pnlSubSetup.Controls.Add(this.btnSetupPara);
            this.pnlSubSetup.Controls.Add(this.btnConfig);
            this.pnlSubSetup.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSubSetup.Location = new System.Drawing.Point(0, 94);
            this.pnlSubSetup.Name = "pnlSubSetup";
            this.pnlSubSetup.Padding = new System.Windows.Forms.Padding(3);
            this.pnlSubSetup.Size = new System.Drawing.Size(1003, 47);
            this.pnlSubSetup.TabIndex = 17;
            this.pnlSubSetup.Visible = false;
            // 
            // btnAdmin
            // 
            this.btnAdmin.Location = new System.Drawing.Point(430, 6);
            this.btnAdmin.Name = "btnAdmin";
            this.btnAdmin.Size = new System.Drawing.Size(100, 35);
            this.btnAdmin.TabIndex = 19;
            this.btnAdmin.Text = "Admin";
            this.btnAdmin.UseVisualStyleBackColor = true;
            this.btnAdmin.Click += new System.EventHandler(this.btnAdmin_Click);
            // 
            // ssBottom
            // 
            this.ssBottom.AutoSize = false;
            this.ssBottom.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ssBottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslblEvent,
            this.tsslblConnection,
            this.tsslblSystemState,
            this.tsslblUser});
            this.ssBottom.Location = new System.Drawing.Point(0, 598);
            this.ssBottom.Name = "ssBottom";
            this.ssBottom.Size = new System.Drawing.Size(1003, 35);
            this.ssBottom.SizingGrip = false;
            this.ssBottom.TabIndex = 18;
            this.ssBottom.Text = "statusStrip1";
            // 
            // tsslblEvent
            // 
            this.tsslblEvent.Font = new System.Drawing.Font("Tahoma", 9F);
            this.tsslblEvent.Name = "tsslblEvent";
            this.tsslblEvent.Size = new System.Drawing.Size(758, 29);
            this.tsslblEvent.Spring = true;
            this.tsslblEvent.Text = "Event";
            this.tsslblEvent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsslblEvent.Click += new System.EventHandler(this.tsslblEvent_Click);
            // 
            // tsslblConnection
            // 
            this.tsslblConnection.Name = "tsslblConnection";
            this.tsslblConnection.Size = new System.Drawing.Size(84, 29);
            this.tsslblConnection.Text = "Connection";
            // 
            // tsslblSystemState
            // 
            this.tsslblSystemState.Name = "tsslblSystemState";
            this.tsslblSystemState.Size = new System.Drawing.Size(101, 29);
            this.tsslblSystemState.Text = "System State: ";
            this.tsslblSystemState.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsslblSystemState.Click += new System.EventHandler(this.tsslblSystemState_Click);
            // 
            // tsslblUser
            // 
            this.tsslblUser.Name = "tsslblUser";
            this.tsslblUser.Size = new System.Drawing.Size(45, 29);
            this.tsslblUser.Text = "User: ";
            this.tsslblUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsslblUser.Click += new System.EventHandler(this.tsslblUser_Click);
            // 
            // pnlNone
            // 
            this.pnlNone.AutoSize = true;
            this.pnlNone.Controls.Add(this.btnNone);
            this.pnlNone.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNone.Location = new System.Drawing.Point(0, 47);
            this.pnlNone.Name = "pnlNone";
            this.pnlNone.Padding = new System.Windows.Forms.Padding(3);
            this.pnlNone.Size = new System.Drawing.Size(1003, 47);
            this.pnlNone.TabIndex = 19;
            // 
            // btnNone
            // 
            this.btnNone.Location = new System.Drawing.Point(6, 6);
            this.btnNone.Name = "btnNone";
            this.btnNone.Size = new System.Drawing.Size(100, 35);
            this.btnNone.TabIndex = 8;
            this.btnNone.UseVisualStyleBackColor = true;
            this.btnNone.Visible = false;
            // 
            // tsTools
            // 
            this.tsTools.AutoSize = false;
            this.tsTools.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tsTools.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tsTools.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tsTools.Location = new System.Drawing.Point(0, 563);
            this.tsTools.Name = "tsTools";
            this.tsTools.Size = new System.Drawing.Size(1003, 35);
            this.tsTools.TabIndex = 21;
            this.tsTools.Text = "toolStrip2";
            // 
            // pnlMain
            // 
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 282);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1003, 281);
            this.pnlMain.TabIndex = 22;
            this.pnlMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMain_Paint);
            // 
            // pnlRecipe
            // 
            this.pnlRecipe.AutoSize = true;
            this.pnlRecipe.Controls.Add(this.btnPressureMaster);
            this.pnlRecipe.Controls.Add(this.btnRecipeTempCtrl);
            this.pnlRecipe.Controls.Add(this.btnRecipePump);
            this.pnlRecipe.Controls.Add(this.btnLoadRecipe);
            this.pnlRecipe.Controls.Add(this.btnSaveRecipe);
            this.pnlRecipe.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRecipe.Location = new System.Drawing.Point(0, 141);
            this.pnlRecipe.Name = "pnlRecipe";
            this.pnlRecipe.Padding = new System.Windows.Forms.Padding(3);
            this.pnlRecipe.Size = new System.Drawing.Size(1003, 47);
            this.pnlRecipe.TabIndex = 23;
            this.pnlRecipe.Visible = false;
            // 
            // btnPressureMaster
            // 
            this.btnPressureMaster.Location = new System.Drawing.Point(218, 6);
            this.btnPressureMaster.Name = "btnPressureMaster";
            this.btnPressureMaster.Size = new System.Drawing.Size(100, 35);
            this.btnPressureMaster.TabIndex = 21;
            this.btnPressureMaster.Text = "Pressure";
            this.btnPressureMaster.UseVisualStyleBackColor = true;
            this.btnPressureMaster.Visible = false;
            this.btnPressureMaster.Click += new System.EventHandler(this.btnPressureMaster_Click);
            // 
            // btnRecipeTempCtrl
            // 
            this.btnRecipeTempCtrl.Location = new System.Drawing.Point(112, 6);
            this.btnRecipeTempCtrl.Name = "btnRecipeTempCtrl";
            this.btnRecipeTempCtrl.Size = new System.Drawing.Size(100, 35);
            this.btnRecipeTempCtrl.TabIndex = 20;
            this.btnRecipeTempCtrl.Text = "Temp";
            this.btnRecipeTempCtrl.UseVisualStyleBackColor = true;
            this.btnRecipeTempCtrl.Visible = false;
            this.btnRecipeTempCtrl.Click += new System.EventHandler(this.btnRecipeTempCtrl_Click);
            // 
            // btnRecipePump
            // 
            this.btnRecipePump.Location = new System.Drawing.Point(6, 6);
            this.btnRecipePump.Name = "btnRecipePump";
            this.btnRecipePump.Size = new System.Drawing.Size(100, 35);
            this.btnRecipePump.TabIndex = 19;
            this.btnRecipePump.Text = "Pump";
            this.btnRecipePump.UseVisualStyleBackColor = true;
            this.btnRecipePump.Visible = false;
            this.btnRecipePump.Click += new System.EventHandler(this.btnRecipePump_Click);
            // 
            // btnLoadRecipe
            // 
            this.btnLoadRecipe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadRecipe.Location = new System.Drawing.Point(791, 6);
            this.btnLoadRecipe.Name = "btnLoadRecipe";
            this.btnLoadRecipe.Size = new System.Drawing.Size(100, 35);
            this.btnLoadRecipe.TabIndex = 8;
            this.btnLoadRecipe.Text = "Load Recipe";
            this.btnLoadRecipe.UseVisualStyleBackColor = true;
            this.btnLoadRecipe.Click += new System.EventHandler(this.btnLoadRecipe_Click);
            // 
            // pnlConv
            // 
            this.pnlConv.AutoSize = true;
            this.pnlConv.Controls.Add(this.btn);
            this.pnlConv.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlConv.Location = new System.Drawing.Point(0, 188);
            this.pnlConv.Name = "pnlConv";
            this.pnlConv.Padding = new System.Windows.Forms.Padding(3);
            this.pnlConv.Size = new System.Drawing.Size(1003, 47);
            this.pnlConv.TabIndex = 24;
            this.pnlConv.Visible = false;
            // 
            // btn
            // 
            this.btn.Location = new System.Drawing.Point(6, 6);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(100, 35);
            this.btn.TabIndex = 8;
            this.btn.Text = "btn";
            this.btn.UseVisualStyleBackColor = true;
            this.btn.Visible = false;
            // 
            // pnlAuto
            // 
            this.pnlAuto.AutoSize = true;
            this.pnlAuto.Controls.Add(this.btnAutoPump);
            this.pnlAuto.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAuto.Location = new System.Drawing.Point(0, 235);
            this.pnlAuto.Name = "pnlAuto";
            this.pnlAuto.Padding = new System.Windows.Forms.Padding(3);
            this.pnlAuto.Size = new System.Drawing.Size(1003, 47);
            this.pnlAuto.TabIndex = 25;
            this.pnlAuto.Visible = false;
            // 
            // btnAutoPump
            // 
            this.btnAutoPump.Location = new System.Drawing.Point(6, 6);
            this.btnAutoPump.Name = "btnAutoPump";
            this.btnAutoPump.Size = new System.Drawing.Size(100, 35);
            this.btnAutoPump.TabIndex = 8;
            this.btnAutoPump.Text = "Pump";
            this.btnAutoPump.UseVisualStyleBackColor = true;
            this.btnAutoPump.Visible = false;
            this.btnAutoPump.Click += new System.EventHandler(this.btnAutoPump_Click_1);
            // 
            // frmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1003, 633);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlAuto);
            this.Controls.Add(this.pnlConv);
            this.Controls.Add(this.pnlRecipe);
            this.Controls.Add(this.pnlSubSetup);
            this.Controls.Add(this.tsTools);
            this.Controls.Add(this.ssBottom);
            this.Controls.Add(this.pnlNone);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmMain";
            this.Text = "NagaW";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.pnlTop.ResumeLayout(false);
            this.pnlSubSetup.ResumeLayout(false);
            this.ssBottom.ResumeLayout(false);
            this.ssBottom.PerformLayout();
            this.pnlNone.ResumeLayout(false);
            this.pnlRecipe.ResumeLayout(false);
            this.pnlConv.ResumeLayout(false);
            this.pnlAuto.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmrDisplay;
        private System.Windows.Forms.Button btnHomeAll;
        private System.Windows.Forms.Button btnSetupPara;
        private System.Windows.Forms.Button btnMotorPage;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.Button btnIOPage;
        private System.Windows.Forms.Button btnRecipeLeft;
        private System.Windows.Forms.Button btnRecipeRight;
        private System.Windows.Forms.Button btnAuto;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlSubSetup;
        private System.Windows.Forms.Button btnSaveAll;
        private System.Windows.Forms.Button btnSetupRight;
        private System.Windows.Forms.StatusStrip ssBottom;
        private System.Windows.Forms.ToolStripStatusLabel tsslblEvent;
        private System.Windows.Forms.ToolStripStatusLabel tsslblSystemState;
        private System.Windows.Forms.ToolStripStatusLabel tsslblUser;
        private System.Windows.Forms.Button btnAdmin;
        private System.Windows.Forms.Panel pnlNone;
        private System.Windows.Forms.Button btnNone;
        private System.Windows.Forms.ToolStrip tsTools;
        private System.Windows.Forms.ToolStripStatusLabel tsslblConnection;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Button btnSaveRecipe;
        private System.Windows.Forms.Panel pnlRecipe;
        private System.Windows.Forms.Button btnLoadRecipe;
        private System.Windows.Forms.Button btnSetupLeft;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnConv;
        private System.Windows.Forms.Panel pnlConv;
        private System.Windows.Forms.Button btnRecipePump;
        private System.Windows.Forms.Panel pnlAuto;
        private System.Windows.Forms.Button btnAutoPump;
        private System.Windows.Forms.Button btn;
        private System.Windows.Forms.Button btnRecipeTempCtrl;
        private System.Windows.Forms.Button btnPressureMaster;
        private System.Windows.Forms.Button btnWafer;
    }
}

