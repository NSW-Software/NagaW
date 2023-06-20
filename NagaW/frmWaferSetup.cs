﻿using System;
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
            lblPreExhaustTime.UpdatePara(GProcessPara.Wafer.PreExhaustTime);
            lblPostExhaustTime.UpdatePara(GProcessPara.Wafer.PostExhaustTime);
            lblPreExhaustDelay.UpdatePara(GProcessPara.Wafer.PreExhaustDelay);

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
            //lblWaferThickness.UpdatePara(GProcessPara.Wafer.WaferThickness);
            lblWaferThickness.UpdatePara(GRecipes.Board[gantry.Index].WaferHeight);
            lblNotchCheckInterval.UpdatePara(GProcessPara.Wafer.NotchAngleCheck);
            lblNotchAlignSpeed.UpdatePara(GProcessPara.Wafer.NotchAlignSpeed);

            lblNotchEdgeRev.UpdatePara(GProcessPara.Wafer.NotchEdgeRev);

            cbxLearnNotchVision.Checked = GProcessPara.Wafer.IsNotchVisionEnable;
            lblNotchVisionScore.UpdatePara(GProcessPara.Wafer.NotchVisonScore);
            lblCountVision.UpdatePara(GProcessPara.Wafer.NotchVisonRepeatCount);
            lblOverAngle.UpdatePara(GProcessPara.Wafer.NotchOverAngle);

            lblOORCounter.UpdatePara(GProcessPara.Wafer.OORCounter);


            GControl.UpdateFormControl(this);
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
            var target = GMotDef.Preciser_0;
            Action act = () => target.MoveAbs(GSetupPara.Wafer.PrecisorPos_1.Value);
            Action cancel = () => target.Stop();
            MsgBox.Processing($"Moving {target.Name}", act, cancel);
        }

        private void btnP2PosGoto_Click(object sender, EventArgs e)
        {
            var target = GMotDef.Preciser_1;
            Action act = () => target.MoveAbs(GSetupPara.Wafer.PrecisorPos_2.Value);
            Action cancel = () => target.Stop();
            MsgBox.Processing($"Moving {target.Name}", act, cancel);
        }

        private void btnP3PosGoto_Click(object sender, EventArgs e)
        {
            var target = GMotDef.Preciser_2;
            Action act = () => target.MoveAbs(GSetupPara.Wafer.PrecisorPos_3.Value);
            Action cancel = () => target.Stop();
            MsgBox.Processing($"Moving {target.Name}", act, cancel);
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

        private void lblPreExhaustTime_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Wafer.PreExhaustTime);
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

        private void btnNotchAlign_Click(object sender, EventArgs e)
        {
            GControl.UI_Disable();
            //await Task.Run(() => TCWafer.NotchAlignment());
            MsgBox.Processing("Notch Alignment", () => TCWafer.NotchAlignment(), () => TCWafer.StopNotch = true);
            GControl.UI_Enable();
        }

        private void lblWaferThickness_Click(object sender, EventArgs e)
        {
            //GLog.SetPara(ref GProcessPara.Wafer.WaferThickness);
            var para = GRecipes.Board[gantry.Index].WaferHeight;
            GLog.SetPara(ref para);
            GRecipes.Board[gantry.Index].WaferHeight.Value = para.Value;
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

        private void lblPostExhaustTime_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Wafer.PostExhaustTime);
            UpdateDisplay();
        }

        private void lblPreExhaustDelay_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Wafer.PreExhaustDelay);
            UpdateDisplay();
        }

        private void lblNotchEdgeRev_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Wafer.NotchEdgeRev);
            UpdateDisplay();
        }

        private void lblNotchVisionScore_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Wafer.NotchVisonScore);
            UpdateDisplay();
        }

        private void cbxLearnNotchVision_Click(object sender, EventArgs e)
        {
            GProcessPara.Wafer.IsNotchVisionEnable = !GProcessPara.Wafer.IsNotchVisionEnable;
            UpdateDisplay();
        }

        private void btnLearnNotchVision_Click(object sender, EventArgs e)
        {
            int id = 10;

            while (GRecipes.PatRecog[gantry.Index].Count <= id)
            {
                GRecipes.PatRecog[gantry.Index].Add(new TPatRect());
            }

            Emgu.CV.Image<Emgu.CV.Structure.Gray, byte> img = null;
            var patRec = GRecipes.PatRecog[0][id];

            try
            {
                TFCamera1.Cameras[gantry.Index].Snap();
                img = TFCamera1.Cameras[gantry.Index].emgucvImage.Clone();
                TFCamera1.Cameras[gantry.Index].Live();
                //TFCameras.Camera[gantry.Index].Snap();
                //img = TFCameras.Camera[gantry.Index].emgucvImage.Clone();
                //TFCameras.Camera[gantry.Index].Live();


                Rectangle[] rects = new Rectangle[2] { patRec.SearchRect[0], patRec.PatRect[0] };
                int thld = patRec.ImgThld[0];
                TFVision.PatLearn(img, ref patRec.RegImage[0], ref thld, ref rects);

                patRec.ImgThld[0] = thld;
                patRec.SearchRect[0] = rects[0];
                patRec.PatRect[0] = rects[1];

                GSetupPara.Wafer.PatLightRGBA = new LightRGBA(TFLightCtrl.LightPair[gantry.Index].CurrentLight);

                return;
            }
            catch (Exception ex)
            {
                GAlarm.Prompt(EAlarm.VISION_LEARN_PATTERN_ERROR, ex.Message.ToString());
            }
            finally
            {
                if (img != null) img.Dispose();
            }
        }

        private void lblCountVision_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Wafer.NotchVisonRepeatCount);
            UpdateDisplay();
        }

        private void lblOverAngle_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Wafer.NotchOverAngle);
            UpdateDisplay();
        }

        private void cbxAutoThicknessDetect_Click(object sender, EventArgs e)
        {
            GProcessPara.Wafer.EnableAutoHeightDetect = !GProcessPara.Wafer.EnableAutoHeightDetect;
            UpdateDisplay();
        }

        private void lblOORCounter_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Wafer.OORCounter);
            UpdateDisplay();
        }

        private void btnBurnRun_Click(object sender, EventArgs e)
        {
            var res = MsgBox.ShowDialog("BurnRun, Continue?", MsgBoxBtns.OKCancel);
            if (res != DialogResult.OK) return;

            bool brun = true;
            MsgBox.Processing("Notch Alignment",
                () => {
                    while (brun)
                    {
                        TCWafer.NotchAlignment();
                        System.Threading.Thread.Sleep(1000);
                    }}, 
                () => 
                {
                    TCWafer.StopNotch = true;
                    brun = false;
                });
        }
    }
}
