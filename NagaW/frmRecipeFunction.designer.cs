namespace NagaW
{
    partial class frmRecipeFunction
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
            this.gbxFuncsCmds = new System.Windows.Forms.GroupBox();
            this.pnlEditor = new System.Windows.Forms.Panel();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnUp = new System.Windows.Forms.Button();
            this.cbxCommand = new System.Windows.Forms.ComboBox();
            this.cbInsert = new System.Windows.Forms.CheckBox();
            this.btnDn = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.lblDispGap = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tbxFuncName = new System.Windows.Forms.TextBox();
            this.lblMoveSpeed = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblRetGap = new System.Windows.Forms.Label();
            this.lblMoveAccel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnOption = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.lboxFuncList = new System.Windows.Forms.ListBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnFuncAdd = new System.Windows.Forms.Button();
            this.btnFunUp = new System.Windows.Forms.Button();
            this.btnFuncRemove = new System.Windows.Forms.Button();
            this.btnFuncDn = new System.Windows.Forms.Button();
            this.cbxFuncLayout = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblLineWait = new System.Windows.Forms.Label();
            this.lblLineEDelay = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblLineSpeed = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblLineSDelay = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblDotWait = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblDotTime = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblDnWait = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.gbxFuncsCmds.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxFuncsCmds
            // 
            this.gbxFuncsCmds.Controls.Add(this.pnlEditor);
            this.gbxFuncsCmds.Controls.Add(this.dgv);
            this.gbxFuncsCmds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxFuncsCmds.Location = new System.Drawing.Point(3, 34);
            this.gbxFuncsCmds.Name = "gbxFuncsCmds";
            this.gbxFuncsCmds.Size = new System.Drawing.Size(654, 495);
            this.gbxFuncsCmds.TabIndex = 36;
            this.gbxFuncsCmds.TabStop = false;
            this.gbxFuncsCmds.Text = "Commands";
            // 
            // pnlEditor
            // 
            this.pnlEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEditor.Location = new System.Drawing.Point(399, 18);
            this.pnlEditor.Name = "pnlEditor";
            this.pnlEditor.Size = new System.Drawing.Size(252, 474);
            this.pnlEditor.TabIndex = 14;
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgv.Location = new System.Drawing.Point(3, 18);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersWidth = 51;
            this.dgv.RowTemplate.Height = 24;
            this.dgv.Size = new System.Drawing.Size(396, 474);
            this.dgv.TabIndex = 38;
            this.dgv.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            this.dgv.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_ColumnHeaderMouseClick);
            this.dgv.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgv_MouseDown);
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.btnUp);
            this.panel1.Controls.Add(this.cbxCommand);
            this.panel1.Controls.Add(this.cbInsert);
            this.panel1.Controls.Add(this.btnDn);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(654, 31);
            this.panel1.TabIndex = 20;
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(282, 3);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(60, 25);
            this.btnUp.TabIndex = 20;
            this.btnUp.Text = "Up";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // cbxCommand
            // 
            this.cbxCommand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCommand.FormattingEnabled = true;
            this.cbxCommand.Location = new System.Drawing.Point(3, 3);
            this.cbxCommand.Name = "cbxCommand";
            this.cbxCommand.Size = new System.Drawing.Size(209, 22);
            this.cbxCommand.TabIndex = 18;
            this.cbxCommand.SelectionChangeCommitted += new System.EventHandler(this.cbxCommand_SelectionChangeCommitted);
            // 
            // cbInsert
            // 
            this.cbInsert.AutoSize = true;
            this.cbInsert.Location = new System.Drawing.Point(218, 7);
            this.cbInsert.Name = "cbInsert";
            this.cbInsert.Size = new System.Drawing.Size(58, 18);
            this.cbInsert.TabIndex = 19;
            this.cbInsert.Text = "Insert";
            this.cbInsert.UseVisualStyleBackColor = true;
            // 
            // btnDn
            // 
            this.btnDn.Location = new System.Drawing.Point(348, 3);
            this.btnDn.Name = "btnDn";
            this.btnDn.Size = new System.Drawing.Size(60, 25);
            this.btnDn.TabIndex = 16;
            this.btnDn.Text = "Dn";
            this.btnDn.UseVisualStyleBackColor = true;
            this.btnDn.Click += new System.EventHandler(this.btnDn_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(414, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(60, 25);
            this.btnDelete.TabIndex = 17;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(6, 25);
            this.label20.Margin = new System.Windows.Forms.Padding(3);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(90, 25);
            this.label20.TabIndex = 21;
            this.label20.Text = "Disp Gap";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDispGap
            // 
            this.lblDispGap.BackColor = System.Drawing.Color.White;
            this.lblDispGap.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDispGap.Location = new System.Drawing.Point(102, 25);
            this.lblDispGap.Margin = new System.Windows.Forms.Padding(3);
            this.lblDispGap.Name = "lblDispGap";
            this.lblDispGap.Size = new System.Drawing.Size(100, 25);
            this.lblDispGap.TabIndex = 22;
            this.lblDispGap.Text = "100";
            this.lblDispGap.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDispGap.Click += new System.EventHandler(this.lblDispGap_Click);
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(217, 21);
            this.label22.Margin = new System.Windows.Forms.Padding(3);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(46, 25);
            this.label22.TabIndex = 33;
            this.label22.Text = "Name:";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(6, 118);
            this.label14.Margin = new System.Windows.Forms.Padding(3);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(90, 25);
            this.label14.TabIndex = 27;
            this.label14.Text = "Move Speed";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbxFuncName
            // 
            this.tbxFuncName.Location = new System.Drawing.Point(260, 23);
            this.tbxFuncName.Name = "tbxFuncName";
            this.tbxFuncName.Size = new System.Drawing.Size(156, 22);
            this.tbxFuncName.TabIndex = 32;
            this.tbxFuncName.TextChanged += new System.EventHandler(this.tbxFuncName_TextChanged);
            // 
            // lblMoveSpeed
            // 
            this.lblMoveSpeed.BackColor = System.Drawing.Color.White;
            this.lblMoveSpeed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMoveSpeed.Location = new System.Drawing.Point(102, 118);
            this.lblMoveSpeed.Margin = new System.Windows.Forms.Padding(3);
            this.lblMoveSpeed.Name = "lblMoveSpeed";
            this.lblMoveSpeed.Size = new System.Drawing.Size(100, 25);
            this.lblMoveSpeed.TabIndex = 28;
            this.lblMoveSpeed.Text = "100";
            this.lblMoveSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMoveSpeed.Click += new System.EventHandler(this.lblMoveSpeed_Click);
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(6, 87);
            this.label18.Margin = new System.Windows.Forms.Padding(3);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(90, 25);
            this.label18.TabIndex = 23;
            this.label18.Text = "Ret Gap";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRetGap
            // 
            this.lblRetGap.BackColor = System.Drawing.Color.White;
            this.lblRetGap.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRetGap.Location = new System.Drawing.Point(102, 87);
            this.lblRetGap.Margin = new System.Windows.Forms.Padding(3);
            this.lblRetGap.Name = "lblRetGap";
            this.lblRetGap.Size = new System.Drawing.Size(100, 25);
            this.lblRetGap.TabIndex = 24;
            this.lblRetGap.Text = "100";
            this.lblRetGap.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRetGap.Click += new System.EventHandler(this.lblRetGap_Click);
            // 
            // lblMoveAccel
            // 
            this.lblMoveAccel.BackColor = System.Drawing.Color.White;
            this.lblMoveAccel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMoveAccel.Location = new System.Drawing.Point(102, 149);
            this.lblMoveAccel.Margin = new System.Windows.Forms.Padding(3);
            this.lblMoveAccel.Name = "lblMoveAccel";
            this.lblMoveAccel.Size = new System.Drawing.Size(100, 25);
            this.lblMoveAccel.TabIndex = 30;
            this.lblMoveAccel.Text = "100";
            this.lblMoveAccel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMoveAccel.Click += new System.EventHandler(this.lblMoveAccel_Click);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 149);
            this.label6.Margin = new System.Windows.Forms.Padding(3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 25);
            this.label6.TabIndex = 29;
            this.label6.Text = "Move Accel";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox2.Controls.Add(this.btnImport);
            this.groupBox2.Controls.Add(this.btnOption);
            this.groupBox2.Controls.Add(this.btnCopy);
            this.groupBox2.Controls.Add(this.lboxFuncList);
            this.groupBox2.Controls.Add(this.flowLayoutPanel2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(668, 102);
            this.groupBox2.TabIndex = 32;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Functions";
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(487, 50);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 25);
            this.btnImport.TabIndex = 19;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnOption
            // 
            this.btnOption.Location = new System.Drawing.Point(487, 21);
            this.btnOption.Name = "btnOption";
            this.btnOption.Size = new System.Drawing.Size(75, 23);
            this.btnOption.TabIndex = 18;
            this.btnOption.Text = "Option";
            this.btnOption.UseVisualStyleBackColor = true;
            this.btnOption.Click += new System.EventHandler(this.btnOption_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(406, 21);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 17;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // lboxFuncList
            // 
            this.lboxFuncList.Dock = System.Windows.Forms.DockStyle.Left;
            this.lboxFuncList.ItemHeight = 14;
            this.lboxFuncList.Location = new System.Drawing.Point(3, 18);
            this.lboxFuncList.Name = "lboxFuncList";
            this.lboxFuncList.Size = new System.Drawing.Size(223, 81);
            this.lboxFuncList.TabIndex = 0;
            this.lboxFuncList.SelectedIndexChanged += new System.EventHandler(this.lboxFuncList_SelectedIndexChanged);
            this.lboxFuncList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lboxFuncList_MouseDown);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.btnFuncAdd);
            this.flowLayoutPanel2.Controls.Add(this.btnFunUp);
            this.flowLayoutPanel2.Controls.Add(this.btnFuncRemove);
            this.flowLayoutPanel2.Controls.Add(this.btnFuncDn);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(232, 18);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(166, 63);
            this.flowLayoutPanel2.TabIndex = 16;
            // 
            // btnFuncAdd
            // 
            this.btnFuncAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFuncAdd.Location = new System.Drawing.Point(3, 3);
            this.btnFuncAdd.Name = "btnFuncAdd";
            this.btnFuncAdd.Size = new System.Drawing.Size(75, 25);
            this.btnFuncAdd.TabIndex = 1;
            this.btnFuncAdd.Text = "Add";
            this.btnFuncAdd.UseVisualStyleBackColor = true;
            this.btnFuncAdd.Click += new System.EventHandler(this.btnFuncAdd_Click);
            // 
            // btnFunUp
            // 
            this.btnFunUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFunUp.Location = new System.Drawing.Point(84, 3);
            this.btnFunUp.Name = "btnFunUp";
            this.btnFunUp.Size = new System.Drawing.Size(75, 25);
            this.btnFunUp.TabIndex = 3;
            this.btnFunUp.Text = "Up";
            this.btnFunUp.UseVisualStyleBackColor = true;
            this.btnFunUp.Click += new System.EventHandler(this.btnFunUp_Click);
            // 
            // btnFuncRemove
            // 
            this.btnFuncRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFuncRemove.Location = new System.Drawing.Point(3, 34);
            this.btnFuncRemove.Name = "btnFuncRemove";
            this.btnFuncRemove.Size = new System.Drawing.Size(75, 25);
            this.btnFuncRemove.TabIndex = 2;
            this.btnFuncRemove.Text = "Remove";
            this.btnFuncRemove.UseVisualStyleBackColor = true;
            this.btnFuncRemove.Click += new System.EventHandler(this.btnFuncRemove_Click);
            // 
            // btnFuncDn
            // 
            this.btnFuncDn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFuncDn.Location = new System.Drawing.Point(84, 34);
            this.btnFuncDn.Name = "btnFuncDn";
            this.btnFuncDn.Size = new System.Drawing.Size(75, 25);
            this.btnFuncDn.TabIndex = 4;
            this.btnFuncDn.Text = "Dn";
            this.btnFuncDn.UseVisualStyleBackColor = true;
            this.btnFuncDn.Click += new System.EventHandler(this.btnFuncDn_Click);
            // 
            // cbxFuncLayout
            // 
            this.cbxFuncLayout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFuncLayout.FormattingEnabled = true;
            this.cbxFuncLayout.Location = new System.Drawing.Point(70, 23);
            this.cbxFuncLayout.Name = "cbxFuncLayout";
            this.cbxFuncLayout.Size = new System.Drawing.Size(107, 22);
            this.cbxFuncLayout.TabIndex = 37;
            this.cbxFuncLayout.SelectionChangeCommitted += new System.EventHandler(this.cbxFuncLayout_SelectionChangeCommitted);
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(100, 35);
            this.tabControl1.Location = new System.Drawing.Point(0, 102);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(668, 575);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 37;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gbxFuncsCmds);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 39);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(660, 532);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Commands";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox5);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 39);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(660, 528);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Properties";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.AutoSize = true;
            this.groupBox5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox5.Controls.Add(this.lblLineWait);
            this.groupBox5.Controls.Add(this.lblLineEDelay);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.lblLineSpeed);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.lblLineSDelay);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Location = new System.Drawing.Point(222, 185);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(208, 160);
            this.groupBox5.TabIndex = 43;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Line Parameter";
            // 
            // lblLineWait
            // 
            this.lblLineWait.BackColor = System.Drawing.Color.White;
            this.lblLineWait.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLineWait.Location = new System.Drawing.Point(102, 114);
            this.lblLineWait.Margin = new System.Windows.Forms.Padding(3);
            this.lblLineWait.Name = "lblLineWait";
            this.lblLineWait.Size = new System.Drawing.Size(100, 25);
            this.lblLineWait.TabIndex = 40;
            this.lblLineWait.Text = "100";
            this.lblLineWait.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLineWait.Click += new System.EventHandler(this.lblLineWait_Click);
            // 
            // lblLineEDelay
            // 
            this.lblLineEDelay.BackColor = System.Drawing.Color.White;
            this.lblLineEDelay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLineEDelay.Location = new System.Drawing.Point(102, 83);
            this.lblLineEDelay.Margin = new System.Windows.Forms.Padding(3);
            this.lblLineEDelay.Name = "lblLineEDelay";
            this.lblLineEDelay.Size = new System.Drawing.Size(100, 25);
            this.lblLineEDelay.TabIndex = 39;
            this.lblLineEDelay.Text = "100";
            this.lblLineEDelay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLineEDelay.Click += new System.EventHandler(this.lblLineEDelay_Click);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(6, 114);
            this.label11.Margin = new System.Windows.Forms.Padding(3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(90, 25);
            this.label11.TabIndex = 38;
            this.label11.Text = "Line Wait";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(6, 83);
            this.label10.Margin = new System.Windows.Forms.Padding(3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(90, 25);
            this.label10.TabIndex = 37;
            this.label10.Text = "End Delay";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLineSpeed
            // 
            this.lblLineSpeed.BackColor = System.Drawing.Color.White;
            this.lblLineSpeed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLineSpeed.Location = new System.Drawing.Point(102, 52);
            this.lblLineSpeed.Margin = new System.Windows.Forms.Padding(3);
            this.lblLineSpeed.Name = "lblLineSpeed";
            this.lblLineSpeed.Size = new System.Drawing.Size(100, 25);
            this.lblLineSpeed.TabIndex = 36;
            this.lblLineSpeed.Text = "100";
            this.lblLineSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLineSpeed.Click += new System.EventHandler(this.lblLineSpeed_Click);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(6, 52);
            this.label5.Margin = new System.Windows.Forms.Padding(3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 25);
            this.label5.TabIndex = 35;
            this.label5.Text = "Line Speed";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLineSDelay
            // 
            this.lblLineSDelay.BackColor = System.Drawing.Color.White;
            this.lblLineSDelay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLineSDelay.Location = new System.Drawing.Point(102, 21);
            this.lblLineSDelay.Margin = new System.Windows.Forms.Padding(3);
            this.lblLineSDelay.Name = "lblLineSDelay";
            this.lblLineSDelay.Size = new System.Drawing.Size(100, 25);
            this.lblLineSDelay.TabIndex = 34;
            this.lblLineSDelay.Text = "100";
            this.lblLineSDelay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLineSDelay.Click += new System.EventHandler(this.lblLineSDelay_Click);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(6, 21);
            this.label9.Margin = new System.Windows.Forms.Padding(3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 25);
            this.label9.TabIndex = 33;
            this.label9.Text = "Start Delay";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblDotWait);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.lblDotTime);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Location = new System.Drawing.Point(222, 79);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(208, 100);
            this.groupBox4.TabIndex = 42;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Dot Parameter";
            // 
            // lblDotWait
            // 
            this.lblDotWait.BackColor = System.Drawing.Color.White;
            this.lblDotWait.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDotWait.Location = new System.Drawing.Point(102, 52);
            this.lblDotWait.Margin = new System.Windows.Forms.Padding(3);
            this.lblDotWait.Name = "lblDotWait";
            this.lblDotWait.Size = new System.Drawing.Size(100, 25);
            this.lblDotWait.TabIndex = 36;
            this.lblDotWait.Text = "100";
            this.lblDotWait.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDotWait.Click += new System.EventHandler(this.lblDotWait_Click);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(6, 52);
            this.label7.Margin = new System.Windows.Forms.Padding(3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 25);
            this.label7.TabIndex = 35;
            this.label7.Text = "Dot Wait";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDotTime
            // 
            this.lblDotTime.BackColor = System.Drawing.Color.White;
            this.lblDotTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDotTime.Location = new System.Drawing.Point(102, 21);
            this.lblDotTime.Margin = new System.Windows.Forms.Padding(3);
            this.lblDotTime.Name = "lblDotTime";
            this.lblDotTime.Size = new System.Drawing.Size(100, 25);
            this.lblDotTime.TabIndex = 34;
            this.lblDotTime.Text = "100";
            this.lblDotTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDotTime.Click += new System.EventHandler(this.lblDotTime_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 21);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 25);
            this.label4.TabIndex = 33;
            this.label4.Text = "Dot Time";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox3
            // 
            this.groupBox3.AutoSize = true;
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.tbxFuncName);
            this.groupBox3.Controls.Add(this.cbxFuncLayout);
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Location = new System.Drawing.Point(8, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(422, 74);
            this.groupBox3.TabIndex = 41;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Info";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 25);
            this.label1.TabIndex = 40;
            this.label1.Text = "Layout:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.lblDnWait);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lblMoveSpeed);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.lblMoveAccel);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.lblDispGap);
            this.groupBox1.Controls.Add(this.lblRetGap);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Location = new System.Drawing.Point(8, 79);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(208, 266);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Common Parameter";
            // 
            // lblDnWait
            // 
            this.lblDnWait.BackColor = System.Drawing.Color.White;
            this.lblDnWait.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDnWait.Location = new System.Drawing.Point(102, 56);
            this.lblDnWait.Margin = new System.Windows.Forms.Padding(3);
            this.lblDnWait.Name = "lblDnWait";
            this.lblDnWait.Size = new System.Drawing.Size(100, 25);
            this.lblDnWait.TabIndex = 32;
            this.lblDnWait.Text = "100";
            this.lblDnWait.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDnWait.Click += new System.EventHandler(this.lblDnWait_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 56);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 25);
            this.label3.TabIndex = 31;
            this.label3.Text = "Down Wait";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmRecipeFunction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 677);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Navy;
            this.Name = "frmRecipeFunction";
            this.Text = "frmRecipeFunction";
            this.Load += new System.EventHandler(this.frmRecipe_Load);
            this.gbxFuncsCmds.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox gbxFuncsCmds;
        private System.Windows.Forms.Panel pnlEditor;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbxCommand;
        private System.Windows.Forms.CheckBox cbInsert;
        private System.Windows.Forms.Button btnDn;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox tbxFuncName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblDispGap;
        private System.Windows.Forms.Label lblMoveAccel;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblRetGap;
        private System.Windows.Forms.Label lblMoveSpeed;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lboxFuncList;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button btnFuncAdd;
        private System.Windows.Forms.Button btnFuncRemove;
        private System.Windows.Forms.Button btnFunUp;
        private System.Windows.Forms.Button btnFuncDn;
        private System.Windows.Forms.ComboBox cbxFuncLayout;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblDnWait;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblDotWait;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblDotTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lblLineSpeed;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblLineSDelay;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblLineWait;
        private System.Windows.Forms.Label lblLineEDelay;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnOption;
        private System.Windows.Forms.Button btnImport;
    }
}