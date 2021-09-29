using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NagaW
{
    public partial class frmNozzleInsp : Form
    {
        TEZMCAux.TGroup Gantry;
        static bool Result = false;

        public frmNozzleInsp()
        {
            InitializeComponent();
            cbxDO.DataSource = GMotDef.Outputs;
            cbxStatusOK.DataSource = GMotDef.Inputs;
            cbxStatusNG.DataSource = GMotDef.Inputs;
        }

        public frmNozzleInsp(TEZMCAux.TGroup gantry) : this()
        {
            Gantry = gantry;
        }

        private void frmNozzleInsp_Load(object sender, EventArgs e)
        {
            Updatedisplay();
        }

        private void Updatedisplay()
        {
            lblWaitTime.UpdatePara(GProcessPara.NozzleInspection.WaitTime);
            cbxDO.SelectedIndex = GProcessPara.NozzleInspection.DO_Idx;
            cbxStatusOK.SelectedIndex = GProcessPara.NozzleInspection.DI_Idx[0];
            cbxStatusNG.SelectedIndex = GProcessPara.NozzleInspection.DI_Idx[1];

            lblResult.Text = Result ? "Pass" : "Fail";
            lblResult.BackColor = Result ? Color.Lime : Color.Red;
            btnTrigIO.BackColor = GMotDef.Outputs[(int)cbxDO.SelectedIndex].Status ? Color.Lime : SystemColors.Control;
            lblStatusOK.BackColor = GMotDef.Inputs[cbxStatusOK.SelectedIndex].Status? Color.Lime : SystemColors.Control;
            lblStatusNG.BackColor = GMotDef.Inputs[cbxStatusNG.SelectedIndex].Status ? Color.Red : SystemColors.Control;
            lblPos.Text = GSetupPara.NozzleInsp.Pos[Gantry.Index].ToStringForDisplay();
        }

        private async void btnExecute_Click(object sender, EventArgs e)
        {
            GControl.UI_Disable();
            Result = await Task.Run(() => TCExternalFunc.NoozleInsps[Gantry.Index].Execute());
            GControl.UI_Enable();
            Updatedisplay();
        }

        private void btnSetPos_Click(object sender, EventArgs e)
        {
            GLog.SetPos(ref GSetupPara.NozzleInsp.Pos[Gantry.Index], Gantry.PointXYZ, $"{Gantry.Name} NozzleInspecPos", true);
        }

        private void btnGotoPos_Click(object sender, EventArgs e)
        {
            Gantry.GotoXYZ(GSetupPara.NozzleInsp.Pos[Gantry.Index]);
        }

        private void lblWaitTime_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.NozzleInspection.WaitTime);
            Updatedisplay();
        }

        private void cbxDO_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GProcessPara.NozzleInspection.DO_Idx = cbxDO.SelectedIndex;
        }

        private void cbxStatusOK_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GProcessPara.NozzleInspection.DI_Idx[0] = cbxStatusOK.SelectedIndex;
        }

        private void btnTrigIO_Click(object sender, EventArgs e)
        {
            GMotDef.Outputs[(int)cbxDO.SelectedIndex].Status = !GMotDef.Outputs[(int)cbxDO.SelectedIndex].Status;
            Updatedisplay();
        }

        private void cbxStatusNG_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GProcessPara.NozzleInspection.DI_Idx[1] = cbxStatusOK.SelectedIndex;
        }
    }
}
