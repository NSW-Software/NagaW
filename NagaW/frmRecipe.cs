using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
 
namespace NagaW
{
    public partial class frmRecipe : Form
    {
        TEZMCAux.TGroup gantry;
        Inst.TBoard InstBoard;

        public frmRecipe()
        {
            InitializeComponent();
        }
        public frmRecipe(TEZMCAux.TGroup gantry) : this()
        {
            this.gantry = gantry;
            InstBoard = Inst.Board[gantry.Index];

            Timer tmr = new Timer();
            tmr.Interval = 500;
            tmr.Tick += (a, b) =>
            {
                int tableBase = gantry.Index * 10;
                PointI runUnitCR = new PointI(TEZMCAux.Table(tableBase + 0), TEZMCAux.Table(tableBase + 1));
                PointI runClusterCR = new PointI(TEZMCAux.Table(tableBase + 2), TEZMCAux.Table(tableBase + 3));
                lblFunctionClusterUnit.Text = $"Running Func: {InstBoard.FuncNo} Cluster: {InstBoard.ClusterCR.X + 1},{InstBoard.ClusterCR.Y + 1} Unit: {runUnitCR.X + 1},{runUnitCR.Y + 1}";
            };
            tmr.Enabled = true;
        }

        frmRecipeMap rcpMap;
        frmRecipeLayout rcpLayout;
        frmRecipeFunction rcpFunc;

        private void frmRecipe_Load(object sender, EventArgs e)
        {
            BackColor = (gantry.Index == 0 ? GSystemCfg.Display.LeftColor : GSystemCfg.Display.RightColor);

            SetForm(rcpLayout = new frmRecipeLayout(gantry), tpLayout);
            SetForm(rcpFunc = new frmRecipeFunction(gantry), tpFunc);
            SetForm(rcpMap = new frmRecipeMap(gantry, true), tpMap);

            void SetForm(Form frm, TabPage tp)
            {
                frm.TopMost = false;
                frm.TopLevel = false;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Dock = DockStyle.Fill;
                frm.Parent = tp;
                frm.Show();
            }

            cbxRunMode.DataSource = Enum.GetValues(typeof(ERunMode));
            cbxRunSelect.DataSource = Enum.GetValues(typeof(ERunSelect));
            cbxRunMode.SelectedItem = InstBoard.RunMode;
            cbxRunSelect.SelectedItem = TCDisp.RunSelect;

            GControl.LogForm(this);

            tabControl1.SelectedIndexChanged += (a, b) => rcpFunc.formList.ForEach(x => x.Close());
        }

        public void UpdateDisplay()
        {
            rcpLayout.UpdateDisplay();
            rcpFunc.UpdateDisplay();
            rcpFunc.UpdateGrid();
        }

        private async void btnRun_Click(object sender, EventArgs e)
        {
            try
            {
                TFTower.Process(true);
                GControl.UI_Disable(btnStop);
                if (!TFSafety.LockDoor()) return;

                TCPressCtrl.StartTime[gantry.Index] = DateTime.Now;

                if (InstBoard.RunMode != ERunMode.Camera) TFLightCtrl.LightPair[gantry.Index].Off();

                if (!gantry.MoveOpZAbs(0)) return;

                await Task.Run(() => TCDisp.Run[gantry.Index].Disp());

                gantry.MoveOpZAbs(GRecipes.Board[gantry.Index].StartPos.Z);
                TFLightCtrl.LightPair[gantry.Index].Set(GRecipes.Board[gantry.Index].LightDefault);
                TFCameras.Camera[gantry.Index].Live();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                TFTower.Process(false);
                GControl.UI_Enable();
                TFSafety.ReleaseDoor();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            TCDisp.Run[gantry.Index].Stop();
            TCDisp.Run[gantry.Index].bRun = false;
            rcpFunc.formList.ForEach(x => x.Close());
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            TCDisp.Run[gantry.Index].CancelBuffer();
            InstBoard.ClearData();

            foreach (var frm in Application.OpenForms.OfType<frmRecipeMap>())
            {
                frm.RefreshUI();
                frm.Refresh();
            }
            rcpFunc.formList.ForEach(x => x.Close());
        }

        private void cbxRunMode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            InstBoard.RunMode = (ERunMode)cbxRunMode.SelectedIndex;
        }
        private void cbxRunSelect_SelectionChangeCommitted(object sender, EventArgs e)
        {
            TCDisp.RunSelect = (ERunSelect)cbxRunSelect.SelectedIndex;
        }

        private void frmRecipe_FormClosing(object sender, FormClosingEventArgs e)
        {
            rcpMap.Close();
            rcpLayout.Close();
            rcpFunc.Close();
        }

        private void cbxRunMode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tmrDisplay_Tick(object sender, EventArgs e)
        {

        }
    }
}
