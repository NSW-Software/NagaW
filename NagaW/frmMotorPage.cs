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
    public partial class frmMotorPage : Form
    {
        public frmMotorPage()
        {
            InitializeComponent();
            cbxModule.DataSource = Enum.GetNames(typeof(EModule));
        }

        private void frmMotorPage_Load(object sender, EventArgs e)
        {
            cbxModule_SelectionChangeCommitted(sender, e);
            tmrDisplay.Enabled = true;
        }

        private void frmMotorPage_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void tmrDisplay_Tick(object sender, EventArgs e)
        {
            if (!Visible) return;

            UpdateDisplay();
        }

        TEZMCAux.TAxis axis0;
        TEZMCAux.TAxis axis1;
        TEZMCAux.TAxis axis2;
        private void cbxModule_SelectionChangeCommitted(object sender, EventArgs e)
        {
            switch (cbxModule.SelectedIndex)
            {
                case (int)EModule.GantryMain:
                    axis0 = GMotDef.GXAxis;
                    axis1 = GMotDef.GYAxis;
                    axis2 = GMotDef.GZAxis;
                    pnlAxis1.Visible = true;
                    pnlAxis2.Visible = true;
                    break;
                case (int)EModule.GantryVR:
                    axis0 = GMotDef.GVAxis;
                    axis1 = GMotDef.GRAxis;

                    pnlAxis1.Visible = true;
                    pnlAxis2.Visible = false;
                    break;
                case (int)EModule.Lifter:
                    axis0 = GMotDef.Lifter;

                    pnlAxis1.Visible = false;
                    pnlAxis2.Visible = false;
                    break;
                case (int)EModule.Precisor:
                    axis0 = GMotDef.Preciser_0;
                    axis1 = GMotDef.Preciser_1;
                    axis2 = GMotDef.Preciser_2;
                    pnlAxis1.Visible = true;
                    pnlAxis2.Visible = true;
                    break;
            }
        }

        private void UpdateDisplay()
        {
            lblEmg.BackColor = GMotDef.EMO_Ready.Status ? Color.Gray : Color.Red;

            lblAxis0.Text = axis0.Name;
            label8.Text = axis1.Name;
            label13.Text = axis2.Name;

            btnServoOn0.BackColor = axis0.SvOn ? Color.Red : Color.Gray;
            btnServoOn1.BackColor = axis1.SvOn ? Color.Red : Color.Gray;
            btnServoOn2.BackColor = axis2.SvOn ? Color.Red : Color.Gray;

            lblSvrOn0.BackColor = axis0.SvOn ? Color.Red : Color.Gray;
            lblSvrOn1.BackColor = axis1.SvOn ? Color.Red : Color.Gray;
            lblSvrOn2.BackColor = axis2.SvOn ? Color.Red : Color.Gray;

            lblAlarm0.BackColor = axis0.Alarm ? Color.Red : Color.Gray;
            lblAlarm1.BackColor = axis1.Alarm ? Color.Red : Color.Gray;
            lblAlarm2.BackColor = axis2.Alarm ? Color.Red : Color.Gray;

            lblHLmtP0.BackColor = axis0.HLmtP ? Color.Red : Color.Gray;
            lblHLmtN0.BackColor = axis0.HLmtN ? Color.Red : Color.Gray;
            lblHLmtP1.BackColor = axis1.HLmtP ? Color.Red : Color.Gray;
            lblHLmtN1.BackColor = axis1.HLmtN ? Color.Red : Color.Gray;
            lblHLmtP2.BackColor = axis2.HLmtP ? Color.Red : Color.Gray;
            lblHLmtN2.BackColor = axis2.HLmtN ? Color.Red : Color.Gray;

            lblCmdPos0.Text = axis0.CmdPos.ToString("f3");
            lblCmdPos1.Text = axis1.CmdPos.ToString("f3");
            lblCmdPos2.Text = axis2.CmdPos.ToString("f3");

            lblActualPos0.Text = axis0.ActualPos.ToString("f3");
            lblActualPos1.Text = axis1.ActualPos.ToString("f3");
            lblActualPos2.Text = axis2.ActualPos.ToString("f3");

            lblPos0.Text = $"{pos0.Value:f3}";
            lblPos1.Text = $"{pos1.Value:f3}";
            lblPos2.Text = $"{pos2.Value:f3}";
        }

        private async void btnHome_Click(object sender, EventArgs e)
        {
            GControl.UI_Disable(btnStop0, btnStop1, btnStop2);

            switch (cbxModule.SelectedIndex)
            {
                case (int)EModule.GantryMain:
                    await Task.Run(() =>
                    {
                        TFGantry.GXYZHome();
                    });
                    break;
                case (int)EModule.GantryVR:
                    await Task.Run(() =>
                    {
                        TFGantry.GVRHome();
                    });
                    break;
            }

            GControl.UI_Enable();
        }
        private void btnHomeAll_Click(object sender, EventArgs e)
        {
            GControl.UI_Disable();

            MsgBox.Processing("Init all in Progress.", () =>
            {
                TCSystem.InitAll();
            });

            GControl.UI_Enable();
        }

        DPara pos0 = new DPara("Pos0", 0, -500, 500, EUnit.MILLIMETER);
        private void btnResetError0_Click(object sender, EventArgs e)
        {
            axis0.AlmClr = true;
        }
        private void btnServoOn0_Click(object sender, EventArgs e)
        {
            axis0.SvOn = !axis0.SvOn;
        }
        private void lblPos0_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref pos0);
            UpdateDisplay();
        }
        private void btnMove0_Click(object sender, EventArgs e)
        {
            axis0.MoveRel(pos0.Value, true);
        }
        private void btnStop0_Click(object sender, EventArgs e)
        {
            axis0.Stop();
        }

        DPara pos1 = new DPara("Pos1", 0, -500, 500, EUnit.MILLIMETER);
        private void btnResetError1_Click(object sender, EventArgs e)
        {
            axis1.AlmClr = true;
        }
        private void btnServoOn1_Click(object sender, EventArgs e)
        {
            axis1.SvOn = !axis1.SvOn;
        }
        private void lblPos1_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref pos1);
            UpdateDisplay();
        }
        private void btnMove1_Click(object sender, EventArgs e)
        {
            axis1.MoveRel(pos1.Value, true);
        }
        private void btnStop1_Click(object sender, EventArgs e)
        {
            axis1.Stop();
        }

        DPara pos2 = new DPara("Pos2", 0, -500, 500, EUnit.MILLIMETER);
        private void btnResetError2_Click(object sender, EventArgs e)
        {
            axis2.AlmClr = true;
        }
        private void btnServoOn2_Click(object sender, EventArgs e)
        {
            axis2.SvOn = !axis2.SvOn;
        }
        private void lblPos2_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref pos2);
            UpdateDisplay();
        }
        private void btnMove2_Click(object sender, EventArgs e)
        {
            axis2.MoveRel(pos2.Value, true);
        }
        private void btnStop2_Click(object sender, EventArgs e)
        {
            axis2.Stop();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                TFGantry.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                TFGantry.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cbxModule_MouseClick(object sender, MouseEventArgs e)
        {
            cbxModule.DroppedDown = true;
        }
    }
}

