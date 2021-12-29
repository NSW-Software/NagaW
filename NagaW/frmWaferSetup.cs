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
    public partial class frmWaferSetup : Form
    {
        TEZMCAux.TGroup gantry;

        public frmWaferSetup()
        {
            InitializeComponent();
        }
        public frmWaferSetup(TEZMCAux.TGroup gantry) : this()
        {
            Text = "Wafer Setup";
            this.gantry = gantry;
        }
        private void frmWaferSetup_Load(object sender, EventArgs e)
        {
            GMotDef.GVAxis.MoveAbs(0);
            GControl.LogForm(this);
            UpdateDisplay();
            timer1.Enabled = true;
        }
        private void UpdateDisplay()
        {
            lblManualPos.Text = GSetupPara.Wafer.ManualLoadPos.ToStringForDisplay();
            lblAutoPostion.Text = GSetupPara.Wafer.AutoLoadPos.ToStringForDisplay();

            lblAirBlowDuration.UpdatePara(GProcessPara.Wafer.AirBlowDuration);
            lblPreOnVac.UpdatePara(GProcessPara.Wafer.PreOnVacuum);
            lblExhaustTime.UpdatePara(GProcessPara.Wafer.ExhaustTime);

            lblAirBlowPos.Text = GSetupPara.Wafer.AirBlowPos.ToStringForDisplay();

            lblP1Pos.UpdatePara(GSetupPara.Wafer.PrecisorPos_1);
            lblP2Pos.UpdatePara(GSetupPara.Wafer.PrecisorPos_2);
            lblP3Pos.UpdatePara(GSetupPara.Wafer.PrecisorPos_3);

            lblPrecisorSpeed.UpdatePara(GProcessPara.Wafer.PrecisorSpeed);
            lblPrecisorAcc.UpdatePara(GProcessPara.Wafer.PrecisorAccel);

            lblLifterSpeed.UpdatePara(GProcessPara.Wafer.LifterSpeed);
            lblLifterAccel.UpdatePara(GProcessPara.Wafer.LifterAccel);
            lblLifterStroke.UpdatePara(GProcessPara.Wafer.LifterStroke);

            cbxAirblowPre.Checked = GProcessPara.Wafer.PreAirBlow;
            cbxPreVac.Checked = GProcessPara.Wafer.PreVacuumEnable;

            lblNotchTeachCamPos.Text = GSetupPara.Wafer.TeachNotchCamPos.ToStringForDisplay();
            lblWaferThickness.UpdatePara(GProcessPara.Wafer.WaferThickness);
            lblNotchCheckInterval.UpdatePara(GProcessPara.Wafer.NotchAngleCheck);
            lblNotchAlignSpeed.UpdatePara(GProcessPara.Wafer.NotchAlignSpeed);
        }

        private async void btnManualLoad_Click(object sender, EventArgs e)
        {
            GControl.UI_Disable();
            await Task.Run(() => TCWafer.Manual_Load());
            GControl.UI_Enable();
        }

        private async void btnManualUnload_Click(object sender, EventArgs e)
        {
            GControl.UI_Disable();
            await Task.Run(() => TCWafer.Manual_Unload());
            GControl.UI_Enable();
        }

        private void btnSetManualLoadPos_Click(object sender, EventArgs e)
        {
            GLog.SetPos(ref GSetupPara.Wafer.ManualLoadPos, gantry.PointXYZ, nameof(GSetupPara.Wafer.ManualLoadPos));
            UpdateDisplay();
        }

        private void btnGotoManualLoadPos_Click(object sender, EventArgs e)
        {
            gantry.GotoXYZ(GSetupPara.Wafer.ManualLoadPos);
        }

        private void btnAirBlowPosSet_Click(object sender, EventArgs e)
        {
            GLog.SetPos(ref GSetupPara.Wafer.AirBlowPos, gantry.PointXYZ, nameof(GSetupPara.Wafer.AirBlowPos));
            UpdateDisplay();
        }

        private void btnAirBlowPosGoto_Click(object sender, EventArgs e)
        {
            gantry.GotoXYZ(GSetupPara.Wafer.AirBlowPos);

        }

        private void lblAirBlowDuration_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Wafer.AirBlowDuration);
            UpdateDisplay();
        }

        private void lblPreOnVac_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Wafer.PreOnVacuum);
            UpdateDisplay();
        }

        private void btnSVWaferVacLow_Click(object sender, EventArgs e)
        {
            TCWafer.WaferVacLow.Status = !TCWafer.WaferVacLow.Status;
        }

        private void btnSVChuckVacOn_Click(object sender, EventArgs e)
        {
            TCWafer.ChuckVac.Status = !TCWafer.ChuckVac.Status;
        }

        private void btnSVWaferVacOn_Click(object sender, EventArgs e)
        {
            TCWafer.WaferVacHigh.Status = !TCWafer.WaferVacHigh.Status;
        }

        private void btnSVWaferExhOn_Click(object sender, EventArgs e)
        {
            TCWafer.WaferExh.Status = !TCWafer.WaferExh.Status;
        }

        private void btnBlowerIonizer_Click(object sender, EventArgs e)
        {
            TCWafer.AirBlow.Status = !TCWafer.AirBlow.Status;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Color on = Color.Lime;
            Color off = SystemColors.Control;

            btnBlowerIonizer.BackColor = TCWafer.AirBlow.Status ? on : off;
            btnSVWaferVacLow.BackColor = TCWafer.WaferVacLow.Status ? on : off;
            btnSVChuckVacOn.BackColor = TCWafer.ChuckVac.Status ? on : off;
            btnSVWaferVacOn.BackColor = TCWafer.WaferVacHigh.Status ? on : off;
            btnSVWaferExhOn.BackColor = TCWafer.WaferExh.Status ? on : off;

            lblDIChuckVac.BackColor = TCWafer.ChuckVacSens.Status ? on : off;
            lblDIWaferVacHigh.BackColor = TCWafer.WaferVacHighSens.Status ? on : off;
            lblDIWaferVacLow.BackColor = TCWafer.WaferVacLowSens.Status ? on : off;

            lblSMEMA_UpIn.BackColor = TCWafer.SMEMA_UP_IN.Status ? on : off;
            btnSMEMA_UpOut.BackColor = TCWafer.SMEMA_UP_OUT.Status ? on : off;
            lblSMEMA_DownIn.BackColor = TCWafer.SMEMA_DN_IN.Status ? on : off;
            btnSMEMA_DnOut.BackColor = TCWafer.SMEMA_DN_OUT.Status ? on : off;
        }

        private void btnAutoPosSet_Click(object sender, EventArgs e)
        {
            GLog.SetPos(ref GSetupPara.Wafer.AutoLoadPos, gantry.PointXYZ, "Wafer Load Pos");
            UpdateDisplay();
        }

        private void btnAutoPosGoto_Click(object sender, EventArgs e)
        {
            gantry.GotoXYZ(GSetupPara.Wafer.AutoLoadPos);
        }


        private async void btnAutoLoad_Click(object sender, EventArgs e)
        {
            if (!TCWafer.SMEMA_ING)
            {
                GControl.UI_Disable(sender as Button);
                (sender as Button).Text = "STOP Auto Load";
                await Task.Run(() => TCWafer.AutoLoad());
                (sender as Button).Text = "Auto Load";
                GControl.UI_Enable();
            }
            else
            {
                if (MessageBox.Show("Wafer - Robot performing Loading.\nAbort process", "Warning", MessageBoxButtons.OKCancel) != DialogResult.OK) return;
                TCWafer.SMEMA_ING = false;
            }
        }

        private async void btnAutoUnload_Click(object sender, EventArgs e)
        {
            if (!TCWafer.SMEMA_ING)
            {
                GControl.UI_Disable(sender as Button);
                (sender as Button).Text = "STOP Auto Unload";
                await Task.Run(() => TCWafer.AutoUnload());
                (sender as Button).Text = "Auto Unload";
                GControl.UI_Enable();
            }
            else
            {
                if (MessageBox.Show("Wafer - Robot performing Unloading.\nAbort process", "Warning", MessageBoxButtons.OKCancel) != DialogResult.OK) return;
                TCWafer.SMEMA_ING = false;
            }
        }

        private void lblP1Pos_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GSetupPara.Wafer.PrecisorPos_1);
            UpdateDisplay();
        }

        private void lblP2Pos_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GSetupPara.Wafer.PrecisorPos_2);
            UpdateDisplay();
        }

        private void lblP3Pos_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GSetupPara.Wafer.PrecisorPos_3);
            UpdateDisplay();
        }

        private void lblP1Speed_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Wafer.PrecisorSpeed);
            UpdateDisplay();
        }

        private void lblP1Acc_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Wafer.PrecisorAccel);
            UpdateDisplay();
        }

        private void btnP1PosSet_Click(object sender, EventArgs e)
        {
            GSetupPara.Wafer.PrecisorPos_1.Value = GMotDef.Preciser_0.ActualPos;
            UpdateDisplay();
        }

        private void btnP2PosSet_Click(object sender, EventArgs e)
        {
            GSetupPara.Wafer.PrecisorPos_2.Value = GMotDef.Preciser_1.ActualPos;
            UpdateDisplay();
        }

        private void btnP3PosSet_Click(object sender, EventArgs e)
        {
            GSetupPara.Wafer.PrecisorPos_3.Value = GMotDef.Preciser_2.ActualPos;
            UpdateDisplay();
        }

        private void btnP1PosGoto_Click(object sender, EventArgs e)
        {
            GMotDef.Preciser_0.MoveAbs(GSetupPara.Wafer.PrecisorPos_1.Value);
        }

        private void btnP2PosGoto_Click(object sender, EventArgs e)
        {
            GMotDef.Preciser_1.MoveAbs(GSetupPara.Wafer.PrecisorPos_2.Value);
        }

        private void btnP3PosGoto_Click(object sender, EventArgs e)
        {
            GMotDef.Preciser_2.MoveAbs(GSetupPara.Wafer.PrecisorPos_3.Value);
        }

        private async void btnPrecisorON_Click(object sender, EventArgs e)
        {
            GControl.UI_Disable();
            await Task.Run(() => TCWafer.PreciserOn());
            GControl.UI_Enable();
        }

        private void lblLifterSpeed_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Wafer.LifterSpeed);
            UpdateDisplay();
        }

        private void lblLifterAccel_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Wafer.LifterAccel);
            UpdateDisplay();
        }

        private void lblLifterStroke_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Wafer.LifterStroke);
            UpdateDisplay();
        }

        private async void btnLifterUp_Click(object sender, EventArgs e)
        {
            GControl.UI_Disable();
            await Task.Run(() => TCWafer.LifterUp());
            GControl.UI_Enable();
        }

        private async void btnPrecisorHoming_Click(object sender, EventArgs e)
        {
            GControl.UI_Disable();
            await Task.Run(() => TCWafer.PrecisorHoming());
            GControl.UI_Enable();
        }

        private async void btnLifterHoming_Click(object sender, EventArgs e)
        {
            GControl.UI_Disable();
            await Task.Run(() => TCWafer.LifterHoming());
            GControl.UI_Enable();
        }

        private void lblExhaustTime_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Wafer.ExhaustTime);
            UpdateDisplay();
        }

        private void btnSMEMA_UpOut_Click(object sender, EventArgs e)
        {
            //GRecipes.Functions[gantry.Index][0].Execute(ERunMode.Camera, GRecipes.Board[0].StartPos.GetPointD(), new PointD(), new PointD(), 0, 0, new PointI(), new PointI(), new List<PointI>(), new List<PointI>(),Inst.Board[0].MAP);
            //PointD clusterRel = Inst.Board[0].CurrentMLayout.Cluster.RelPos(new PointI());
            //PointD unitRel = Inst.Board[0].CurrentMLayout.Unit.RelPos(new PointI(17,104));

            //unitRel = new TFunction().Translate(unitRel, Inst.Board[gantry.Index].LayerData[Inst.Board[0].CurrentMLayout.Index].GetUnitAlign(new PointI[] { new PointI(), new PointI(18, 105) }));

            //PointD origin = GRecipes.Board[gantry.Index].StartPos.GetPointD() + clusterRel + unitRel;

            //if (!gantry.MoveOpZAbs(GRecipes.Board[gantry.Index].StartPos.Z)) return;
            //gantry.MoveOpXYAbs(origin.ToArray);
            //while (gantry.Busy) System.Threading.Thread.Sleep(0);
            //var currentpos = gantry.PointXYZ;

            //var a = Inst.Board[0].CurrentMLayout.Unit.PitchCol.X * 18 * -1;
            //var b = Inst.Board[0].CurrentMLayout.Unit.PitchRow.Y *105 * -1;

            //GRecipes.Board[gantry.Index].StartPos = new PointXYZ(currentpos.X + a, currentpos.Y + b, GRecipes.Board[gantry.Index].StartPos.Z);


            TCWafer.SMEMA_UP_OUT.Status = !TCWafer.SMEMA_UP_OUT.Status;
        }

        private void btnSMEMA_DnOut_Click(object sender, EventArgs e)
        {
            TCWafer.SMEMA_DN_OUT.Status = !TCWafer.SMEMA_DN_OUT.Status;
        }

        private void cbxAirblowPre_Click(object sender, EventArgs e)
        {
            GProcessPara.Wafer.PreAirBlow = !GProcessPara.Wafer.PreAirBlow;
            UpdateDisplay();
        }

        private void btnNotchTeachCamPosSet_Click(object sender, EventArgs e)
        {
            GLog.SetPos(ref GSetupPara.Wafer.TeachNotchCamPos, gantry.PointXYZ, nameof(GSetupPara.Wafer.TeachNotchCamPos));
            UpdateDisplay();
        }

        private void btnNotchTeachCamPosGoto_Click(object sender, EventArgs e)
        {
            TFLightCtrl.LightPair[gantry.Index].Set(GRecipes.Board[gantry.Index].LightDefault);
            gantry.GotoXYZ(GSetupPara.Wafer.TeachNotchCamPos);

        }

        private async void btnNotchAlign_Click(object sender, EventArgs e)
        {
            GControl.UI_Disable();
            await Task.Run(() => TCWafer.NotchAlignment());
            GControl.UI_Enable();
        }

        private void lblWaferThickness_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Wafer.WaferThickness);
            UpdateDisplay();
        }

        private void lblNotchCheckInterval_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Wafer.NotchAngleCheck);
            UpdateDisplay();
        }

        private void lblNotchAlignSpeed_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Wafer.NotchAlignSpeed);
            UpdateDisplay();
        }

        private void cbxPreVac_Click(object sender, EventArgs e)
        {
            GProcessPara.Wafer.PreVacuumEnable = !GProcessPara.Wafer.PreVacuumEnable;
            UpdateDisplay();
        }
    }
}
