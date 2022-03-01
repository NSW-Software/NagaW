using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace NagaW
{
    public partial class frmMain : Form
    {
        string ProgramName = Application.ProductName + " ver." + Application.ProductVersion + " ©" + Application.CompanyName;
        public frmMain()
        {
            InitializeComponent();
            //WindowState = FormWindowState.Maximized;
            //StartPosition = FormStartPosition.CenterScreen;

            //btnAuto.Parent.Controls.Remove(btnAuto);
            btnRecipeRight.Parent.Controls.Remove(btnRecipeRight);
            btnSetupRight.Parent.Controls.Remove(btnSetupRight);
            btnConv.Parent.Controls.Remove(btnConv);

        }

        public static frmFlirCamera Cam = new frmFlirCamera();
        public static frmJogCtrl Jog = new frmJogCtrl();
        private void Form1_Load(object sender, EventArgs e)
        {
            //Width = Screen.PrimaryScreen.WorkingArea.Width;
            //Height = Width;
            //Top = 0;//Screen.PrimaryScreen.WorkingArea.Height;
            this.SendToBack();

            Rectangle wa = Screen.PrimaryScreen.WorkingArea;
            int max = Math.Max(wa.Width, wa.Height);
            if (wa.Width > wa.Height)//Landscape
            {
                Size = new Size((int)(max * 0.75), wa.Height);
                Top = 0;
                Left = wa.Width - Size.Width;
            }
            else//Potrait
            {
                Size = new Size(wa.Height, (int)(max * 0.75));
                Top = wa.Height - Size.Height;
                Left = 0;
            }

            tmrDisplay.Enabled = true;

            TFTool.UpdateTool(tsTools);
            GControl.LogForm(this);

            if (Debugger.IsAttached) TFUser.LoginAsNSW(); else LoginTillDie();

            //frmFlirCamera Cam = new frmFlirCamera();
            //frmJogCtrl Jog = new frmJogCtrl();

            Cam.TopMost = false;
            Cam.TopLevel = false;
            Cam.Parent = pnlMain;
            Cam.Show();
            Cam.ZoomFit();
            Cam.BringToFront();

            //Jog.TopMost = false;
            Jog.TopMost = true;
            Jog.TopLevel = false;
            Jog.Parent = pnlMain;
            Jog.Show();
            Jog.SendToBack();

            AutoArrange();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason == CloseReason.UserClosing)
            {
                switch (MsgBox.ShowDialog("Save Recipe before close?", MsgBoxBtns.YesNoCancel))
                {
                    case DialogResult.Yes: GRecipes.Save(false); break;
                    case DialogResult.No: break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        if (!TFUser.Logged) LoginTillDie();
                        return;
                }
            }
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            AutoArrange();
            Cam.ZoomFit();
        }

        private void AutoArrange()
        {
            Jog.Left = pnlMain.Width - Jog.Width;
            Jog.Top = pnlMain.Height - Jog.Height;

            Cam.Left = pnlMain.Width - Cam.Width;
            Cam.Top = 0;
            Cam.Height = pnlMain.Height - Jog.Height;
        }

        private void UpdateDisplay()
        {
        }

        private void LoginTillDie()
        {
            if (TFUser.Logged)
            {
                if (!TFUser.Logout())
                    return;
            }

            this.Enabled = false;

            formList.ForEach(x => x.Close());
            formList.Clear();

            while (!TFUser.Logged)
            {
                if (!TFUser.Login())
                {
                    Close();
                    return;
                }
            }
            this.Enabled = true;
            UpdateDisplay();
        }
        List<Form> formList = new List<Form>();
        List<Panel> subPanelList = new List<Panel>();
        private void PublishForm(Form frm, Panel subPanel)
        {
            if (subPanelList.Count == 0) subPanelList.Add(pnlNone);
            if (subPanel != null)
            {
                subPanelList.ForEach(t => t.Visible = false);
                if (!subPanelList.Contains(subPanel)) subPanelList.Add(subPanel);
                subPanel.Visible = true;
            }

            if (frm != null)
            {
                frm.TopMost = false;
                frm.TopLevel = false;
                frm.ControlBox = false;
                frm.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                frm.Parent = pnlMain;
                frm.Location = new Point(0, 0);

                if (frm.Text == "frmRecipe")
                {
                    frm.Left = 0;
                    frm.Top = 0;
                    frm.Height = pnlMain.Height;
                }

                frm.Show();
                frm.BringToFront();

                formList.ForEach(f => f.Close());
                if (!formList.Contains(frm)) formList.Add(frm);
            }
        }
        private void PublishFormCloseAll()
        {
            formList.ForEach(f => f.Close());
        }

        private void tmrDisplay_Tick(object sender, EventArgs e)
        {
            Text = ProgramName + $" [{GDoc.RecipeNameWithPath}]";
            tsslblUser.Text = "User: " + TFUser.CurrentUser.ToStringForDisplay();
            tsslblEvent.Text = "Recent Activity: " + GLog.RecentHistory;
            tsslblConnection.Text = "Connection: " + (TEZMCAux.Online ? "Online" : "Offline");
            tsslblSystemState.Text = "System State: " + GDefine.SystemState.ToString() + " L: " + TFGantry.GLStatus.ToString() + " R: " + TFGantry.GRStatus.ToString();
        }

        private void btnHomeAll_Click(object sender, EventArgs e)
        {
            GControl.UI_Disable();

            if (!TEZMCAux.Online)
            {
                if (!TFGantry.Open())
                {
                    GControl.UI_Enable();
                }
            }

            TCSystem.InitAll();

            GControl.UI_Enable();
        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
            PublishForm(new frmAuto(), pnlAuto);
        }
        private void btnAutoPump_Click(object sender, EventArgs e)
        {
            int index = TFGantry.GantrySelect.Index;
            PublishForm(new frmPumpCtrl(index), null);
        }

        private void btnRecipeLeft_Click(object sender, EventArgs e)
        {
            PublishForm(new frmRecipe(TFGantry.GantryLeft), pnlRecipe);

            TFGantry.GantrySelect = TFGantry.GantryLeft;
            Cam.Link(0);
            TFLightCtrl.lightPair = TFLightCtrl.LightPair[0];
            TFTool.UpdateTool(tsTools, 0);
        }
        private void btnRecipeRight_Click(object sender, EventArgs e)
        {
            PublishForm(new frmRecipe(TFGantry.GantryRight), pnlRecipe);

            TFGantry.GantrySelect = TFGantry.GantryRight;
            Cam.Link(1);
            TFLightCtrl.lightPair = TFLightCtrl.LightPair[1];
            TFTool.UpdateTool(tsTools, 1);
        }
        private void btnRecipePump_Click(object sender, EventArgs e)
        {
            int index = TFGantry.GantrySelect.Index;
            PublishForm(new frmPumpCtrl(index), null);
        }
        private void btnLoadRecipe_Click(object sender, EventArgs e)
        {
            GRecipes.Load();
            PublishFormCloseAll();
        }
        private void btnSaveRecipe_Click(object sender, EventArgs e)
        {
            GRecipes.Save();
        }

        private void btnSetupRight_Click(object sender, EventArgs e)
        {
            PublishForm(new frmSetup(TFGantry.GantryRight), pnlRecipe);

            TFGantry.GantrySelect = TFGantry.GantryRight;
            Cam.Link(1);
            TFLightCtrl.lightPair = TFLightCtrl.LightPair[1];
            TFTool.UpdateTool(tsTools, 1);
        }
        private void btnSetupLeft_Click(object sender, EventArgs e)
        {
            PublishForm(new frmSetup(TFGantry.GantryLeft), pnlRecipe);

            TFGantry.GantrySelect = TFGantry.GantryLeft;
            Cam.Link(0);
            TFLightCtrl.lightPair = TFLightCtrl.LightPair[0];
            TFTool.UpdateTool(tsTools, 0);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            PublishForm(new frmSetupPara(), pnlSubSetup);
        }

        private void btnSetupPara_Click(object sender, EventArgs e)
        {
            PublishForm(new frmSetupPara(), pnlSubSetup);
        }
        private void btnMotorPage_Click(object sender, EventArgs e)
        {
            PublishForm(new frmMotorPage(), pnlSubSetup);
        }
        private void btnIOPage_Click(object sender, EventArgs e)
        {
            PublishForm(new frmIOPage(), pnlSubSetup);
        }
        private void btnConfig_Click(object sender, EventArgs e)
        {
            PublishForm(new frmConfig(), pnlSubSetup);
        }
        private void btnAdmin_Click(object sender, EventArgs e)
        {
            if (TFUser.CurrentUser.Level < Elevel.ADMIN)
            {
                MsgBox.ShowDialog($"Invalid User Level");
                return;
            }
            PublishForm(new frmAdmin(), pnlSubSetup);
        }
        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            bool save = false;
            MsgBox.Processing("Saving param...", () =>
            {
                save = GSystemCfg.SaveFile();
                save = save && GMotDef.SaveFile();
                save = save && GSetupPara.SaveFile();
                save = save && GProcessPara.SaveFile();
                save = save && TFUser.SaveFile();
                save = save && TFTool.Save();
                save = save && GRecipes.Save(false);
            });

            if (save) MsgBox.ShowDialog("Save All Completed");
            else MsgBox.ShowDialog("Fail to save");
        }

        private void tsslblUser_Click(object sender, EventArgs e)
        {
            LoginTillDie();
        }
        private void tsslblSystemState_Click(object sender, EventArgs e)
        {

        }
        private void tsslblEvent_Click(object sender, EventArgs e)
        {
            //Process.Start(GDoc.MachineLogFile.FullName);
            new frmLog().Show();
        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }
        private void btnConv_Click(object sender, EventArgs e)
        {
            PublishForm(new frmConveyorcs(), pnlNone);
        }
        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.Alt)
            {
                switch (e.KeyCode)
                {
                    case Keys.C: GControl.UI_Enable(); break;
                }
            }
        }

        private void btnRecipeTempCtrl_Click(object sender, EventArgs e)
        {
            PublishForm(new frmTempCtrl(), null);
        }
        private void btnPressureMaster_Click(object sender, EventArgs e)
        {
            new frmPressureMaster().ShowDialog();
        }
        private void btnWafer_Click(object sender, EventArgs e)
        {
            PublishForm(new frmWaferSetup(TFGantry.GantryLeft), pnlRecipe);

            TFGantry.GantrySelect = TFGantry.GantryLeft;
        }

        private void btnAutoPump_Click_1(object sender, EventArgs e)
        {
            int index = TFGantry.GantrySelect.Index;
            PublishForm(new frmPumpCtrl(index), null);
        }
    }
}
