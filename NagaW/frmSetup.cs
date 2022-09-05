using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NagaW
{
    public partial class frmSetup : Form
    {
        TEZMCAux.TGroup gantry;

        int SelectedHead = 0;

        public frmSetup()
        {
            InitializeComponent();
        }
        public frmSetup(TEZMCAux.TGroup gantry):this()
        {

            TFGantry.GantrySelect = TFGantry.GantryLeft;
            this.gantry = TFGantry.GantrySelect;
            SelectedHead = this.gantry.Index;
        }

        private void frmHeadFunc_Load(object sender, EventArgs e)
        {
            cbxPath.DataSource = Enum.GetValues(typeof(ERunPath));
            cbxRunMode.DataSource = Enum.GetValues(typeof(ERunMode));
            cbxDynamicDir.DataSource = Enum.GetValues(typeof(TCCalibration.DynamicOffset.EDir));

            //GControl.ConvertTabCtrlToFLP(tabctrlSetup);

            tabctrlSetup.TabPages.Remove(tpAirClean);
            tabctrlSetup.TabPages.Remove(tpNozzleInsp);
            GControl.LogForm(this);

            //var frm = new frmNozzleInsp(gantry);
            //frm.Dock = DockStyle.Fill;
            //frm.FormBorderStyle = FormBorderStyle.None;
            //frm.TopMost = frm.TopLevel = false;
            //frm.Parent = tpNozzleInsp;
            //frm.Show();


            UpdateDisplay();
            GMotDef.GVAxis.MoveAbs(0);

        }
        private void tabctrlSetup_SelectedIndexChanged(object sender, EventArgs e)
        {
            gantry.MoveOpZAbs(0);

            switch (tabctrlSetup.SelectedIndex)
            {
                case 0:
                case 1:
                case 5:
                case 6:
                    {
                        TFGantry.GantrySelect = TFGantry.GantryLeft;
                        GMotDef.GVAxis.MoveAbs(0, true);
                        break;
                    }

                default: TFGantry.GantrySelect = TFGantry.GantrySetup; break;
            }
            gantry = TFGantry.GantrySelect;
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            if (gantry == TFGantry.GantryLeft) BackColor = GSystemCfg.Display.LeftColor;
            else BackColor = GSystemCfg.Display.RightColor;

            #region VacClean
            btnVCPosSet.Text = "Set " + (cbVacCleanNeedle.Checked ? "(Needle)" : "(Camera)");
            btnVCPosGoto.Text = "Goto " + (cbVacCleanNeedle.Checked ? "(Needle)" : "(Camera)");

            lblVCPos.Text = GSetupPara.NeedleVacClean.Pos[SelectedHead].ToStringForDisplay();
            lblVCHSensorOffset.Text = GSetupPara.NeedleVacClean.HSensorOffset[SelectedHead].ToStringForDisplay() + $", H:{GSetupPara.NeedleVacClean.HSensorValue[SelectedHead]:f3}";

            lblVCDnWait.UpdatePara(GProcessPara.NeedleVacClean.DownWait[SelectedHead]);
            lblVCDispTime.UpdatePara(GProcessPara.NeedleVacClean.DispTime[SelectedHead]);
            lblVCVacTime.UpdatePara(GProcessPara.NeedleVacClean.VacTime[SelectedHead]);
            lblVCPostVacTime.UpdatePara(GProcessPara.NeedleVacClean.PostVacTime[SelectedHead]);
            lblVCCount.UpdatePara(GProcessPara.NeedleVacClean.Count[SelectedHead]);
            lblVCPostWait.UpdatePara(GProcessPara.NeedleVacClean.PostWait[SelectedHead]);
            lblVCRelZPos.UpdatePara(GProcessPara.NeedleVacClean.RelZPos[SelectedHead]);
            #endregion

            #region Flush
            btnFlushSet.Text = "Set " + (cbFlushNeedle.Checked ? "(Needle)" : "(Camera)");
            btnFlushGoto.Text = "Goto " + (cbFlushNeedle.Checked ? "(Needle)" : "(Camera)");

            lblFlushPos.Text = GSetupPara.NeedleFlush.Pos[SelectedHead].ToStringForDisplay();
            lblFlushHSensorOffset.Text = GSetupPara.NeedleFlush.HSensorOffset[SelectedHead].ToStringForDisplay() + $", H:{GSetupPara.NeedleFlush.HSensorValue[SelectedHead]:f3}";

            lblFlushDnWait.UpdatePara(GProcessPara.NeedleFlush.DownWait[SelectedHead]);
            lblFlushDispTime.UpdatePara(GProcessPara.NeedleFlush.DispTime[SelectedHead]);
            lblFlushVacTime.UpdatePara(GProcessPara.NeedleFlush.VacTime[SelectedHead]);
            lblFlushPostVacTime.UpdatePara(GProcessPara.NeedleFlush.PostVacTime[SelectedHead]);
            lblFlushCount.UpdatePara(GProcessPara.NeedleFlush.Count[SelectedHead]);
            lblFlushPostWait.UpdatePara(GProcessPara.NeedleFlush.PostWait[SelectedHead]);
            lblFlushRelZPos.UpdatePara(GProcessPara.NeedleFlush.RelZPos[SelectedHead]);
            #endregion

            #region Purge
            btnPurgePosSet.Text = "Set " + (cbPurgeNeedle.Checked ? "(Needle)" : "(Camera)");
            btnPurgePosGoto.Text = "Goto " + (cbPurgeNeedle.Checked ? "(Needle)" : "(Camera)");

            lblPurgePos.Text = GSetupPara.NeedlePurge.Pos[SelectedHead].ToStringForDisplay();
            lblPurgeHSensorOffset.Text = GSetupPara.NeedlePurge.HSensorOffset[SelectedHead].ToStringForDisplay() + $", H:{GSetupPara.NeedlePurge.HSensorValue[SelectedHead]:f3}";

            lblPurgeDnWait.UpdatePara(GProcessPara.NeedlePurge.DownWait[SelectedHead]);
            lblPurgeDispTime.UpdatePara(GProcessPara.NeedlePurge.DispTime[SelectedHead]);
            lblPurgeVacTime.UpdatePara(GProcessPara.NeedlePurge.VacTime[SelectedHead]);
            lblPurgePostVacTime.UpdatePara(GProcessPara.NeedlePurge.PostVacTime[SelectedHead]);
            lblPurgeCount.UpdatePara(GProcessPara.NeedlePurge.Count[SelectedHead]);
            lblPurgePostWait.UpdatePara(GProcessPara.NeedlePurge.PostWait[SelectedHead]);
            lblPurgeRelZPos.UpdatePara(GProcessPara.NeedlePurge.RelZPos[SelectedHead]);
            #endregion

            #region Cal
            lblLaserCamPos.Text = GSetupPara.Calibration.LaserCamPos[SelectedHead].ToStringForDisplay();
            lblLaserOffset.Text = GSetupPara.Calibration.LaserOffset[SelectedHead].ToStringForDisplay();

            var lasecalstate = TCCalibration.LaserCal[SelectedHead].CalibrationState;
            lblLaserCalState.Text = lasecalstate.ToString();
            lblLaserCalState.BackColor = lasecalstate == ECalibrationState.Completed ? Color.Lime : lasecalstate == ECalibrationState.Fail ? Color.Orange : Color.Red;


            gboxNeedleXY.Text = $"Needle XY";
            lblNeedleCamPos.Text = GSetupPara.Calibration.NeedleXYCamPos[SelectedHead].ToStringForDisplay();
            lblNeedleOffset.Text = GSetupPara.Calibration.NeedleXYOffset[SelectedHead].ToStringForDisplay();
            lblXTTouchGap.UpdatePara(GProcessPara.Calibration.XYTouchMarkGap[SelectedHead]);

            lasecalstate = TCCalibration.NeedleXYOffsets[SelectedHead].CalibrationState;
            lblXYCalState.Text = lasecalstate.ToString();
            lblXYCalState.BackColor = lasecalstate == ECalibrationState.Completed ? Color.Lime : lasecalstate == ECalibrationState.Fail ? Color.Orange : Color.Red;

            gboxZTouch.Text = $"Z Touch Mode: {GSystemCfg.Config.ZTouchType}";
            lblZCamPos.Text = GSetupPara.Calibration.ZTouchCamPos[SelectedHead].ToStringForDisplay();
            lblHSensor.Text = GSetupPara.Calibration.HSensorValue[SelectedHead].ToString("f3");
            lblZTouch.Text = GSetupPara.Calibration.ZTouchValue[SelectedHead].ToString("f3");
            lblTouchDotDispGap.UpdatePara(GProcessPara.Calibration.TouchDotDispGap[SelectedHead]);
            lblEncoderRes.UpdatePara(GProcessPara.Calibration.ZTouchEncoderRes[SelectedHead]);


            lasecalstate = TCCalibration.NeedleZTouches[SelectedHead].CalibrationState;
            lblZTouchCalState.Text = lasecalstate.ToString();
            lblZTouchCalState.BackColor = lasecalstate == ECalibrationState.Completed ? Color.Lime : lasecalstate == ECalibrationState.Fail ? Color.Orange : Color.Red;
            #endregion

            #region PurgeStage
            btnPurgeStagePosSet.Text = "Set " + (cbPurgeStageNeedle.Checked ? "(Needle)" : "(Camera)");
            btnPurgeStagePosGoto.Text = "Goto " + (cbPurgeStageNeedle.Checked ? "(Needle)" : "(Camera)");


            var layout = GProcessPara.PurgeStage.Layout;
            var current = GProcessPara.PurgeStage.CurrentCR;
            lblPurgeStagePos.Text = GSetupPara.PurgeStage.Pos[SelectedHead].ToStringForDisplay();
            lblLayoutCol.Text = layout.CR.X.ToString();
            lblLayoutRow.Text = layout.CR.Y.ToString();
            lblLayoutPitchColX.Text = layout.PitchCol.X.ToString("f3");
            lblLayoutPitchColY.Text = layout.PitchCol.Y.ToString("f3");
            lblLayoutPitchRowX.Text = layout.PitchRow.X.ToString("f3");
            lblLayoutPitchRowY.Text = layout.PitchRow.Y.ToString("f3");
            cbxPath.SelectedItem = layout.RunPath;

            lblCurrentC.Text = (current.X + 1).ToString();
            lblCurrentR.Text = (current.Y + 1).ToString();
            lblUsedCR.Text = $"({GProcessPara.PurgeStage.Layout.CummulativeCR(current) - 1}/{GProcessPara.PurgeStage.Layout.TotalCR})";

            lblPurgeStageOffset.UpdatePara(GSetupPara.PurgeStage.Gap);
            lblPurgeStageCount.UpdatePara(GProcessPara.PurgeStage.Count);
            chkbxCamOffset.Checked = GProcessPara.PurgeStage.CamOffsetAfterPurge;
            cbxRunMode.SelectedItem = TCNeedleFunc.PurgeStage[SelectedHead].RunMode;

            lblDotTime.UpdatePara(GProcessPara.PurgeStage.DotTime[SelectedHead]);
            lblDotWait.UpdatePara(GProcessPara.PurgeStage.DotWait[SelectedHead]);
            lblRetDist.UpdatePara(GProcessPara.PurgeStage.RetDist[SelectedHead]);
            lblRetWait.UpdatePara(GProcessPara.PurgeStage.RetWait[SelectedHead]);
            lblUpDist.UpdatePara(GProcessPara.PurgeStage.UpDist[SelectedHead]);
            lblUpWait.UpdatePara(GProcessPara.PurgeStage.UpWait[SelectedHead]);
            lblDnSpeed.UpdatePara(GProcessPara.PurgeStage.DnSpeed[SelectedHead]);
            lblDnWait.UpdatePara(GProcessPara.PurgeStage.DnWait[SelectedHead]);
            #endregion

            #region Maintenance
            lblMaintainancePos.Text = GSetupPara.Maintenance.MachinePos[gantry.Index].ToStringForDisplay();
            lblPumpPos.Text = GSetupPara.Maintenance.PumpPos[gantry.Index].ToStringForDisplay();
            #endregion

            #region ABClean
            btnABCleanPosSet.Text = "Set " + (cbAirBladeCleanNeedle.Checked ? "(Needle)" : "(Camera)");
            btnABCleanPosGoto.Text = "Goto " + (cbAirBladeCleanNeedle.Checked ? "(Needle)" : "(Camera)");

            lblABCleanPos.Text = GSetupPara.NeedleAirBladeClean.Pos[SelectedHead].ToStringForDisplay();
            lblABCleanHSensorOffset.Text = GSetupPara.NeedleAirBladeClean.HSensorOffset[SelectedHead].ToStringForDisplay() + $", H:{GSetupPara.NeedleAirBladeClean.HSensorValue[SelectedHead]:f3}";

            lblABCleanDnWait.UpdatePara(GProcessPara.NeedleAirBladeClean.DownWait[SelectedHead]);
            lblABCleanRelZPos.UpdatePara(GProcessPara.NeedleAirBladeClean.RelZPos[SelectedHead]);
            lblABCleanUpSpeed.UpdatePara(GProcessPara.NeedleAirBladeClean.UpSpeed[SelectedHead]);
            lblABCleanCount.UpdatePara(GProcessPara.NeedleAirBladeClean.Count[SelectedHead]);
            #endregion

            #region DynamicCal

            lbldynamicCamPos.Text = GSetupPara.Calibration.DynamicXYCamPos[SelectedHead].ToStringForDisplay();

            lblDynamicSpd.UpdatePara(GProcessPara.Calibration.DynamicTouchDotSpd[SelectedHead]);
            lblDynamicAcc.UpdatePara(GProcessPara.Calibration.DynamicTouchDotAcc[SelectedHead]);
            lblJetGap.UpdatePara(GProcessPara.Calibration.DynamicJetGap[SelectedHead]);

            cbxDynamicDir.SelectedItem = GProcessPara.Calibration.Dirs[SelectedHead];

            lblAccDist.UpdatePara(GProcessPara.Calibration.DynamicAccelDist[SelectedHead]);
            lblDotCount.UpdatePara(GProcessPara.Calibration.DynamicDotCount[SelectedHead]);
            lblDyDotPitch.UpdatePara(GProcessPara.Calibration.DynamicPitch[SelectedHead]);

            var dynamic_offset = GSetupPara.Calibration.DynamicOffsets[gantry.Index, cbxDynamicDir.SelectedIndex];
            lblDOX.Text = new DPara(dynamic_offset.X, EUnit.MILLIMETER).ToStringForDisplay();
            lblDOY.Text = new DPara(dynamic_offset.Y, EUnit.MILLIMETER).ToStringForDisplay();


            lasecalstate = TCCalibration.DynamicOffsets[SelectedHead].CalibrationState[cbxDynamicDir.SelectedIndex];
            lblDyCalState.Text = lasecalstate.ToString();
            lblDyCalState.BackColor = lasecalstate == ECalibrationState.Completed ? Color.Lime : lasecalstate == ECalibrationState.Fail ? Color.Orange : Color.Red;

            #endregion

            #region Spray
            btnSprayPosSet.Text = "Set " + (cbNeedleSpray.Checked ? "(Needle)" : "(Camera)");
            btnSprayPosGoto.Text = "Goto " + (cbNeedleSpray.Checked ? "(Needle)" : "(Camera)");

            lblSprayPos.Text = GSetupPara.NeedleSprayClean.Pos[SelectedHead].ToStringForDisplay();
            lblSprayHSensorOffset.Text = GSetupPara.NeedleSprayClean.HSensorOffset[SelectedHead].ToStringForDisplay() + $", H:{GSetupPara.NeedleSprayClean.HSensorValue[SelectedHead]:f3}";

            lblSprayDnWait.UpdatePara(GProcessPara.NeedleSpray.DownWait[SelectedHead]);
            lblSprayTime.UpdatePara(GProcessPara.NeedleSpray.SprayTime[SelectedHead]);
            lblSprayCount.UpdatePara(GProcessPara.NeedleSpray.Count[SelectedHead]);
            lblSprayPostWait.UpdatePara(GProcessPara.NeedleSpray.PostWait[SelectedHead]);
            lblSprayRelZPos.UpdatePara(GProcessPara.NeedleSpray.RelZPos[SelectedHead]);
            #endregion

            GControl.UpdateFormControl(this);


            btnLaserCalFast.Enabled = TCCalibration.LaserCal[SelectedHead].CalibrationState == ECalibrationState.Completed;
            btnNeedleOffsetFast.Enabled = TCCalibration.NeedleXYOffsets[SelectedHead].CalibrationState == ECalibrationState.Completed;
            btnZTouchCalFast.Enabled = TCCalibration.NeedleZTouches[SelectedHead].CalibrationState == ECalibrationState.Completed;
        }

        #region Calibration
        private void btnLaserCamPosSet_Click(object sender, EventArgs e)
        {
            GLog.SetPos(ref GSetupPara.Calibration.LaserCamPos[gantry.Index], gantry.PointXYZ, gantry.Name + " Laser Cam Pos");
            GSetupPara.Calibration.LaserOffsetLighting[gantry.Index] = new LightRGBA(TFLightCtrl.LightPair[gantry.Index].CurrentLight);
            UpdateDisplay();
        }
        private void btnLaserCamPosGoto_Click(object sender, EventArgs e)
        {
            TFLightCtrl.LightPair[gantry.Index].Set(GSetupPara.Calibration.LaserOffsetLighting[gantry.Index]);
            gantry.GotoXYZ(GSetupPara.Calibration.LaserCamPos[gantry.Index], true);

        }
        private async void btnLaserCal_Click(object sender, EventArgs e)
        {
            GControl.UI_DisableExceptCamJog();
            if (cboxLaserOffsetManual.Checked)
            {
                cboxLaserOffsetManual.Checked = false;
                await Task.Run(() => TCCalibration.LaserCal[SelectedHead].Execute_Manual());
            }
            else
                await Task.Run(() => TCCalibration.LaserCal[SelectedHead].Execute(false));
            GControl.UI_Enable();
            UpdateDisplay();
        }

        private void btnNeedleXYCamPosSet_Click(object sender, EventArgs e)
        {
            GLog.SetPos(ref GSetupPara.Calibration.NeedleXYCamPos[SelectedHead], gantry.PointXYZ, gantry.Name + " NeedleXY Cam Pos");
            GSetupPara.Calibration.NeedleXYLighting[SelectedHead] = new LightRGBA(TFLightCtrl.LightPair[gantry.Index].CurrentLight);
            UpdateDisplay();
        }
        private void btnNeedleXYCamPosGoto_Click(object sender, EventArgs e)
        {
            TFLightCtrl.LightPair[gantry.Index].Set(GSetupPara.Calibration.NeedleXYLighting[SelectedHead]);
            gantry.GotoXYZ(GSetupPara.Calibration.NeedleXYCamPos[SelectedHead], true);
        }
        private async void btnXYCal_Click(object sender, EventArgs e)
        {
            GControl.UI_DisableExceptCamJog();
            await Task.Run(() => TCCalibration.NeedleXYOffsets[SelectedHead].Execute());
            GControl.UI_Enable();
            UpdateDisplay();
        }
        private async void btnNormalXYZCal_Click(object sender, EventArgs e)
        {
            GControl.UI_DisableExceptCamJog();
            await Task.Run(() => TCCalibration.FullCals[gantry.Index].Execute(TCCalibration.FullCal.ECalMode.TouchMark));
            GControl.UI_Enable();
            UpdateDisplay();
        }

        private void btnZTouchCamPosSet_Click(object sender, EventArgs e)
        {
            GLog.SetPos(ref GSetupPara.Calibration.ZTouchCamPos[SelectedHead], gantry.PointXYZ, gantry.Name + " ZTouch Cam Pos");
            GSetupPara.Calibration.ZTouchLighting[SelectedHead] = new LightRGBA(TFLightCtrl.LightPair[gantry.Index].CurrentLight);
            UpdateDisplay();
        }
        private void btnZTouchCamPosGoto_Click(object sender, EventArgs e)
        {
            TFLightCtrl.LightPair[gantry.Index].Set(GSetupPara.Calibration.ZTouchLighting[SelectedHead]);
            gantry.GotoXYZ(GSetupPara.Calibration.ZTouchCamPos[SelectedHead], true);

        }
        private async void btnZTouchCal_Click(object sender, EventArgs e)
        {
            GControl.UI_DisableExceptCamJog();
            if (cbZTouchManual.Checked)
                await Task.Run(() => TCCalibration.NeedleZTouches[gantry.Index].Execute(TCCalibration.NeedleZTouch.EZCalMode.Manual));
            else
                await Task.Run(() => TCCalibration.NeedleZTouches[gantry.Index].Execute(TCCalibration.NeedleZTouch.EZCalMode.Normal));
            GControl.UI_Enable();
            UpdateDisplay();
        }
        private async void btnTouchDot_Click(object sender, EventArgs e)
        {
            GControl.UI_DisableExceptCamJog();
            await Task.Run(() => TCCalibration.FullCals[gantry.Index].Execute(TCCalibration.FullCal.ECalMode.TouchDot));
            GControl.UI_Enable();
            UpdateDisplay();
        }

        private async void btnNeedleOffsetFast_Click(object sender, EventArgs e)
        {
            GControl.UI_DisableExceptCamJog();
            await Task.Run(() => TCCalibration.NeedleXYOffsets[gantry.Index].Execute(true));
            GControl.UI_Enable();
            UpdateDisplay();
        }
        private async void btnLaserCalFast_Click(object sender, EventArgs e)
        {
            GControl.UI_DisableExceptCamJog();
            await Task.Run(() => TCCalibration.LaserCal[SelectedHead].Execute(true));
            GControl.UI_Enable();
            UpdateDisplay();
        }
        private async void btnZTouchCalFast_Click(object sender, EventArgs e)
        {
            GControl.UI_DisableExceptCamJog();
            await Task.Run(() => TCCalibration.NeedleZTouches[SelectedHead].Execute(TCCalibration.NeedleZTouch.EZCalMode.Normal, true));
            GControl.UI_Enable();
            UpdateDisplay();
        }
        #endregion

        #region DynamicCal
        private async void btnDynamicCal_Click(object sender, EventArgs e)
        {
            GControl.UI_DisableExceptCamJog();

            await Task.Run(() => TCCalibration.DynamicOffsets[SelectedHead].Execute());

            GControl.UI_Enable();
            UpdateDisplay();
        }
        private void btndynamicCamPosSet_Click(object sender, EventArgs e)
        {
            GLog.SetPos(ref GSetupPara.Calibration.DynamicXYCamPos[SelectedHead], gantry.PointXYZ, $"{gantry.Name} Dynamic Cam Pos");
            GSetupPara.Calibration.DynamicLighting[SelectedHead] = new LightRGBA(TFLightCtrl.LightPair[SelectedHead].CurrentLight);
            UpdateDisplay();
        }
        private void btndynamicCamPosGoto_Click(object sender, EventArgs e)
        {
            TFLightCtrl.LightPair[SelectedHead].Set(GSetupPara.Calibration.DynamicLighting[SelectedHead]);
            gantry.GotoXYZ(GSetupPara.Calibration.DynamicXYCamPos[SelectedHead], true);
        }
        private void lblDynamicSpd_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Calibration.DynamicTouchDotSpd[SelectedHead]);
            UpdateDisplay();
        }
        private void lblDynamicAcc_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Calibration.DynamicTouchDotAcc[SelectedHead]);
            UpdateDisplay();
        }
        private void lblJetGap_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Calibration.DynamicJetGap[SelectedHead]);
            UpdateDisplay();
        }
        private void lblAccDist_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Calibration.DynamicAccelDist[SelectedHead]);
            UpdateDisplay();
        }
        private void lblDotCount_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Calibration.DynamicDotCount[SelectedHead]);
            UpdateDisplay();
        }
        private void lblDotPitch_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Calibration.DynamicPitch[SelectedHead]);
            UpdateDisplay();
        }
        private void lblDOX_Click(object sender, EventArgs e)
        {
            var dynamic_offset = GSetupPara.Calibration.DynamicOffsets[gantry.Index, cbxDynamicDir.SelectedIndex];
            var para = new DPara(nameof(GSetupPara.Calibration.DynamicOffsets), dynamic_offset.X, -999.999, 999.999, EUnit.MILLIMETER);
            GLog.SetPara(ref para);
            dynamic_offset.X = para.Value;
            UpdateDisplay();
        }
        private void lblDOY_Click(object sender, EventArgs e)
        {
            var dynamic_offset = GSetupPara.Calibration.DynamicOffsets[gantry.Index, cbxDynamicDir.SelectedIndex];
            var para = new DPara(nameof(GSetupPara.Calibration.DynamicOffsets), dynamic_offset.Y, -999.999, 999.999, EUnit.MILLIMETER);
            GLog.SetPara(ref para);
            dynamic_offset.Y = para.Value;
            UpdateDisplay();
        }
        private void cbxDynamicDir_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GProcessPara.Calibration.Dirs[SelectedHead] = (TCCalibration.DynamicOffset.EDir)cbxDynamicDir.SelectedItem;
            UpdateDisplay();
        }
        #endregion

        #region VacClean
        private void btnVCSetPos_Click(object sender, EventArgs e)
        {
            PointXYZ point = gantry.PointXYZ;
            if (cbVacCleanNeedle.Checked)
            {
                point.X -= GSetupPara.Calibration.NeedleXYOffset[gantry.Index].X;
                point.Y -= GSetupPara.Calibration.NeedleXYOffset[gantry.Index].Y;
            }

            GLog.SetPos(ref GSetupPara.NeedleVacClean.Pos[SelectedHead], point, gantry.Name + " VacClean Pos");
            gantry.MoveOpZAbs(0);

            UpdateDisplay();
        }
        private void btnVCGotoPos_Click(object sender, EventArgs e)
        {
            PointXYZ point = new PointXYZ(GSetupPara.NeedleVacClean.Pos[SelectedHead]);
            if (cbVacCleanNeedle.Checked)
            {
                point.X += GSetupPara.Calibration.NeedleXYOffset[gantry.Index].X;
                point.Y += GSetupPara.Calibration.NeedleXYOffset[gantry.Index].Y;
            }

            TFLightCtrl.LightPair[gantry.Index].Set(GRecipes.Board[gantry.Index].LightDefault);
            gantry.GotoXYZ(point, true);
        }
        private void btnLearn_Click(object sender, EventArgs e)
        {
            MsgBox.Processing("Vac Clean Learn", () => TCNeedleFunc.CFP[gantry.Index].Learn(ENeedleCleanMode.VacClean));
            UpdateDisplay();
        }
        private void btnVCReset_Click(object sender, EventArgs e)
        {
            GSetupPara.NeedleVacClean.HSensorValue[SelectedHead] = 0;
            UpdateDisplay();
        }
        private void btnExecVC_Click(object sender, EventArgs e)
        {
            MsgBox.Processing("Vac Clean", () => TCNeedleFunc.CFP[gantry.Index].Execute(ENeedleCleanMode.VacClean), () => TCNeedleFunc.CFP[gantry.Index].running = false);
        }

        private void lblVCDnWait_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.NeedleVacClean.DownWait[SelectedHead]);
            UpdateDisplay();
        }
        private void lblVCDispTime_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.NeedleVacClean.DispTime[SelectedHead]);
            UpdateDisplay();
        }
        private void lblVCVacTime_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.NeedleVacClean.VacTime[SelectedHead]);
            UpdateDisplay();
        }
        private void lblVCPostVacTime_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.NeedleVacClean.PostVacTime[SelectedHead]);
            UpdateDisplay();
        }
        private void lblVCPostWait_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.NeedleVacClean.PostWait[SelectedHead]);
            UpdateDisplay();
        }

        private void lblVCCount_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.NeedleVacClean.Count[SelectedHead]);
            UpdateDisplay();
        }
        private void lblVCRelZPos_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.NeedleVacClean.RelZPos[SelectedHead]);
            UpdateDisplay();
        }
        private void cbVacCleanNeedle_Click(object sender, EventArgs e)
        {
            UpdateDisplay();
        }
        #endregion

        #region Flush
        private void btnFlushSet_Click(object sender, EventArgs e)
        {
            PointXYZ point = gantry.PointXYZ;
            if (cbFlushNeedle.Checked)
            {
                point.X -= GSetupPara.Calibration.NeedleXYOffset[gantry.Index].X;
                point.Y -= GSetupPara.Calibration.NeedleXYOffset[gantry.Index].Y;
            }

            GLog.SetPos(ref GSetupPara.NeedleFlush.Pos[SelectedHead], point, gantry.Name + " Flush Pos");
            gantry.MoveOpZAbs(0);

            UpdateDisplay();
        }
        private void btnFlushGoto_Click(object sender, EventArgs e)
        {
            PointXYZ point = new PointXYZ(GSetupPara.NeedleFlush.Pos[SelectedHead]);
            if (cbFlushNeedle.Checked)
            {
                point.X += GSetupPara.Calibration.NeedleXYOffset[gantry.Index].X;
                point.Y += GSetupPara.Calibration.NeedleXYOffset[gantry.Index].Y;
            }

            TFLightCtrl.LightPair[gantry.Index].Set(GRecipes.Board[gantry.Index].LightDefault);
            gantry.GotoXYZ(point, true);
        }
        private void btnFlushLearn_Click(object sender, EventArgs e)
        {
            MsgBox.Processing("Flush Learn", () => TCNeedleFunc.CFP[gantry.Index].Learn(ENeedleCleanMode.Flush));
            UpdateDisplay();
        }
        private void btnFlushReset_Click(object sender, EventArgs e)
        {
            GSetupPara.NeedleFlush.HSensorValue[SelectedHead] = 0;
            UpdateDisplay();
        }
        private void btnFlushExec_Click(object sender, EventArgs e)
        {
            MsgBox.Processing("Flush", () => TCNeedleFunc.CFP[gantry.Index].Execute(ENeedleCleanMode.Flush), () => TCNeedleFunc.CFP[gantry.Index].running = false);
        }

        private void lblFlushDnWait_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.NeedleFlush.DownWait[SelectedHead]);
            UpdateDisplay();
        }
        private void lblFlushDispTime_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.NeedleFlush.DispTime[SelectedHead]);
            UpdateDisplay();
        }
        private void lblFlushVacTime_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.NeedleFlush.VacTime[SelectedHead]);
            UpdateDisplay();
        }
        private void lblFlushPostVacTime_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.NeedleFlush.PostVacTime[SelectedHead]);
            UpdateDisplay();
        }
        private void lblFlushPostWait_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.NeedleFlush.PostWait[SelectedHead]);
            UpdateDisplay();
        }
        private void lblFlushCount_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.NeedleFlush.Count[SelectedHead]);
            UpdateDisplay();
        }
        private void lblFlushRelZPos_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.NeedleFlush.RelZPos[SelectedHead]);
            UpdateDisplay();
        }
        #endregion

        #region Purge
        private void btnPurgePosSet_Click(object sender, EventArgs e)
        {
            PointXYZ point = gantry.PointXYZ;
            if (cbPurgeNeedle.Checked)
            {
                point.X -= GSetupPara.Calibration.NeedleXYOffset[gantry.Index].X;
                point.Y -= GSetupPara.Calibration.NeedleXYOffset[gantry.Index].Y;
            }

            GLog.SetPos(ref GSetupPara.NeedlePurge.Pos[SelectedHead], point, gantry.Name + " Purge Pos");
            gantry.MoveOpZAbs(0);

            UpdateDisplay();
        }
        private void btnPurgePosGoto_Click(object sender, EventArgs e)
        {
            PointXYZ point = new PointXYZ(GSetupPara.NeedlePurge.Pos[SelectedHead]);
            if (cbPurgeNeedle.Checked)
            {
                point.X += GSetupPara.Calibration.NeedleXYOffset[gantry.Index].X;
                point.Y += GSetupPara.Calibration.NeedleXYOffset[gantry.Index].Y;
            }

            TFLightCtrl.LightPair[gantry.Index].Set(GRecipes.Board[gantry.Index].LightDefault);
            gantry.GotoXYZ(point, true);
        }
        private void btnPurgeLearn_Click(object sender, EventArgs e)
        {
            MsgBox.Processing("Purge Learn", () => TCNeedleFunc.CFP[gantry.Index].Learn(ENeedleCleanMode.Purge));
            UpdateDisplay();
        }
        private void btnPurgeReset_Click(object sender, EventArgs e)
        {
            GSetupPara.NeedlePurge.HSensorValue[SelectedHead] = 0;
            UpdateDisplay();
        }
        private void btnPurgeExec_Click(object sender, EventArgs e)
        {
            MsgBox.Processing("Purge", () => TCNeedleFunc.CFP[gantry.Index].Execute(ENeedleCleanMode.Purge), () => TCNeedleFunc.CFP[gantry.Index].running = false);
        }

        private void lblPurgeDnWait_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.NeedlePurge.DownWait[SelectedHead]);
            UpdateDisplay();
        }
        private void lblPurgeDispTime_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.NeedlePurge.DispTime[SelectedHead]);
            UpdateDisplay();
        }
        private void lblPurgeVacTime_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.NeedlePurge.VacTime[SelectedHead]);
            UpdateDisplay();
        }
        private void lblPurgePostVacTime_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.NeedlePurge.PostVacTime[SelectedHead]);
            UpdateDisplay();
        }
        private void lblPurgePostWait_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.NeedlePurge.PostWait[SelectedHead]);
            UpdateDisplay();
        }
        private void lblPurgeCount_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.NeedlePurge.Count[SelectedHead]);
            UpdateDisplay();
        }
        private void lblPurgeRelZPos_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.NeedlePurge.RelZPos[SelectedHead]);
            UpdateDisplay();
        }
        #endregion

        #region Purge Stage
        private void btnPurgeStagePosSet_Click(object sender, EventArgs e)
        {
            PointXYZ point = gantry.PointXYZ;
            if (cbPurgeStageNeedle.Checked)
            {
                point.X -= GSetupPara.Calibration.NeedleXYOffset[gantry.Index].X;
                point.Y -= GSetupPara.Calibration.NeedleXYOffset[gantry.Index].Y;
            }

            GLog.SetPos(ref GSetupPara.PurgeStage.Pos[SelectedHead], point, gantry.Name + " Purge Stage Pos");
            gantry.MoveOpZAbs(0);

            UpdateDisplay();
        }
        private void btnPurgeStagePosGoto_Click(object sender, EventArgs e)
        {
            PointXYZ point = new PointXYZ(GSetupPara.PurgeStage.Pos[gantry.Index]);
            if (cbPurgeStageNeedle.Checked)
            {
                point.X += GSetupPara.Calibration.NeedleXYOffset[gantry.Index].X;
                point.Y += GSetupPara.Calibration.NeedleXYOffset[gantry.Index].Y;
            }

            TFLightCtrl.LightPair[gantry.Index].Set(GRecipes.Board[gantry.Index].LightDefault);
            gantry.GotoXYZ(point, true);
        }
        private void lblLayoutCol_Click(object sender, EventArgs e)
        {
            var layout = GProcessPara.PurgeStage.Layout;
            IPara para = new IPara("PurgeStage Col", layout.CR.X, TLayout.MIN_CR, TLayout.MAX_UNIT_CR, EUnit.COUNT);
            if (!GLog.SetPara(ref para)) return;

            layout.CR.X = para.Value;
            UpdateDisplay();
        }
        private void lblLayoutRow_Click(object sender, EventArgs e)
        {
            var layout = GProcessPara.PurgeStage.Layout;
            IPara para = new IPara("PurgeStage Row", layout.CR.Y, TLayout.MIN_CR, TLayout.MAX_UNIT_CR, EUnit.COUNT);
            if (!GLog.SetPara(ref para)) return;

            layout.CR.Y = para.Value;
            UpdateDisplay();
        }

        private void lblLayoutPitchColX_Click(object sender, EventArgs e)
        {
            var layout = GProcessPara.PurgeStage.Layout;
            DPara para = new DPara("PurgeStage Pitch ColX", layout.PitchCol.X, -10, 10, EUnit.MILLIMETER);
            if (!GLog.SetPara(ref para)) return;

            layout.PitchCol.X = para.Value;
            UpdateDisplay();
        }
        private void lblLayoutPitchColY_Click(object sender, EventArgs e)
        {
            var layout = GProcessPara.PurgeStage.Layout;
            DPara para = new DPara("PurgeStage Pitch ColY", layout.PitchCol.Y, -10, 10, EUnit.MILLIMETER);
            if (!GLog.SetPara(ref para)) return;

            layout.PitchCol.Y = para.Value;
            UpdateDisplay();
        }
        private void lblLayoutPitchRowX_Click(object sender, EventArgs e)
        {
            var layout = GProcessPara.PurgeStage.Layout;
            DPara para = new DPara("PurgeStage Pitch RowX", layout.PitchRow.X, -10, 10, EUnit.MILLIMETER);
            if (!GLog.SetPara(ref para)) return;

            layout.PitchRow.X = para.Value;
            UpdateDisplay();
        }
        private void lblLayoutPitchRowY_Click(object sender, EventArgs e)
        {
            var layout = GProcessPara.PurgeStage.Layout;
            DPara para = new DPara("PurgeStage Pitch RowY", layout.PitchRow.Y, -10, 10, EUnit.MILLIMETER);
            if (!GLog.SetPara(ref para)) return;

            layout.PitchRow.Y = para.Value;
            UpdateDisplay();
        }

        private void btnLayoutPitchColSet_Click(object sender, EventArgs e)
        {
            var layout = GProcessPara.PurgeStage.Layout;

            var pos = new PointXYZ();
            if (cbPurgeStageNeedle.Checked)
            {
                pos.X -= GSetupPara.Calibration.NeedleXYOffset[gantry.Index].X;
                pos.Y -= GSetupPara.Calibration.NeedleXYOffset[gantry.Index].Y;
            }

            if (!GLog.SetPos(ref pos, gantry.PointXYZ, $"PurgeStage Pitch Pos X")) return;
            var relpos = new PointD(pos.X, pos.Y) - GSetupPara.PurgeStage.Pos[SelectedHead].GetPointD();

            layout.PitchCol.X = relpos.X;
            layout.PitchCol.X /= (layout.CR.X - 1);

            layout.PitchCol.Y = relpos.Y;
            layout.PitchCol.Y /= (layout.CR.Y - 1);
            UpdateDisplay();
        }
        private void btnLayoutPitchColGoto_Click(object sender, EventArgs e)
        {
            var layout = GProcessPara.PurgeStage.Layout;

            PointXYZ point = new PointXYZ(GSetupPara.PurgeStage.Pos[gantry.Index]);
            point.X += layout.PitchCol.X * (layout.CR.X - 1);
            point.Y += layout.PitchCol.Y * (layout.CR.X - 1);

            if (cbPurgeStageNeedle.Checked)
            {
                point.X += GSetupPara.Calibration.NeedleXYOffset[gantry.Index].X;
                point.Y += GSetupPara.Calibration.NeedleXYOffset[gantry.Index].Y;
            }

            gantry.GotoXYZ(point, true);
        }
        private void btnLayoutPitchRowSet_Click(object sender, EventArgs e)
        {
            var layout = GProcessPara.PurgeStage.Layout;

            var pos = new PointXYZ();
            if (cbPurgeStageNeedle.Checked)
            {
                pos.X -= GSetupPara.Calibration.NeedleXYOffset[gantry.Index].X;
                pos.Y -= GSetupPara.Calibration.NeedleXYOffset[gantry.Index].Y;
            }

            if (!GLog.SetPos(ref pos, gantry.PointXYZ, $"PurgeStage Pitch Pos Y")) return;
            var relpos = new PointD(pos.X, pos.Y) - (GSetupPara.PurgeStage.Pos[SelectedHead].GetPointD());

            layout.PitchRow.X = relpos.X;
            layout.PitchRow.X /= (layout.CR.Y - 1);

            layout.PitchRow.Y = relpos.Y;
            layout.PitchRow.Y /= (layout.CR.Y - 1);
            UpdateDisplay();
        }
        private void btnLayoutPitchRowGoto_Click(object sender, EventArgs e)
        {
            var layout = GProcessPara.PurgeStage.Layout;

            PointXYZ point = new PointXYZ(GSetupPara.PurgeStage.Pos[gantry.Index]);
            point.X += layout.PitchRow.X * (layout.CR.Y - 1);
            point.Y += layout.PitchRow.Y * (layout.CR.Y - 1);

            if (cbPurgeStageNeedle.Checked)
            {
                point.X += GSetupPara.Calibration.NeedleXYOffset[gantry.Index].X;
                point.Y += GSetupPara.Calibration.NeedleXYOffset[gantry.Index].Y;
            }

            gantry.GotoXYZ(point, true);
        }
        private void cbxPath_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GProcessPara.PurgeStage.Layout.RunPath = (ERunPath)cbxPath.SelectedItem;
            UpdateDisplay();
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            GProcessPara.PurgeStage.CurrentCR = new PointI();
            TCNeedleFunc.PurgeStage[gantry.Index].Notification_AlmostFull = true;
            UpdateDisplay();
        }
        private void lblPurgeStageOffset_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GSetupPara.PurgeStage.Gap);
            UpdateDisplay();
        }
        private void lblTouchDotDispGap_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Calibration.TouchDotDispGap[SelectedHead]);
            UpdateDisplay();
        }

        private void lblPurgeStageCount_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.PurgeStage.Count);
            UpdateDisplay();
        }
        private async void btnPurgeStageExec_Click(object sender, EventArgs e)
        {
            GControl.UI_Disable(btnPurgeStageStop);
            await Task.Run(() => TCNeedleFunc.PurgeStage[gantry.Index].Execute());

            GControl.UI_Enable();
            UpdateDisplay();
        }
        private void cbxRunMode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            TCNeedleFunc.PurgeStage[gantry.Index].RunMode = (ERunMode)cbxRunMode.SelectedItem;
            UpdateDisplay();
        }
        private void btnPurgeStageStop_Click(object sender, EventArgs e)
        {
            TCNeedleFunc.PurgeStage[gantry.Index].Running = false;
        }
        private void chkbxCamOffset_Click(object sender, EventArgs e)
        {
            GProcessPara.PurgeStage.CamOffsetAfterPurge = chkbxCamOffset.Checked;
            UpdateDisplay();
        }

        private void lblDotTime_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.PurgeStage.DotTime[SelectedHead]);
            UpdateDisplay();
        }
        private void lblDotWait_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.PurgeStage.DotWait[SelectedHead]);
            UpdateDisplay();
        }
        private void lblRetDist_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.PurgeStage.RetDist[SelectedHead]);
            UpdateDisplay();
        }
        private void lblRetWait_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.PurgeStage.RetWait[SelectedHead]);
            UpdateDisplay();
        }
        private void lblUpDist_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.PurgeStage.UpDist[SelectedHead]);
            UpdateDisplay();
        }
        private void lblUpWait_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.PurgeStage.UpWait[SelectedHead]);
            UpdateDisplay();
        }
        private void lblDnSpeed_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.PurgeStage.DnSpeed[SelectedHead]);
            UpdateDisplay();
        }
        private void lblDnWait_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.PurgeStage.DnWait[SelectedHead]);
            UpdateDisplay();
        }
        #endregion

        #region Maintenance
        private void btnMaintPosSet_Click(object sender, EventArgs e)
        {
            GLog.SetPos(ref GSetupPara.Maintenance.MachinePos[gantry.Index], gantry.PointXYZ, "Maintainance Pos");
            UpdateDisplay();
        }
        private void btnMaintPosGoto_Click(object sender, EventArgs e)
        {
            TFLightCtrl.LightPair[gantry.Index].Off();
            TCNeedleFunc.Maint[gantry.Index].GotoMcMaint(true);
        }
        private void btnPumpPosSet_Click(object sender, EventArgs e)
        {
            GLog.SetPos(ref GSetupPara.Maintenance.PumpPos[gantry.Index], gantry.PointXYZ, "Pump Maint Pos");
            UpdateDisplay();
        }
        private void btnPumpPosGoto_Click(object sender, EventArgs e)
        {
            TFLightCtrl.LightPair[gantry.Index].Off();
            TCNeedleFunc.Maint[gantry.Index].GotoPumpMaint(true);
        }
        #endregion

        #region Air Blade Clean
        private void btnABCleanPosSet_Click(object sender, EventArgs e)
        {
            PointXYZ point = gantry.PointXYZ;
            if (cbAirBladeCleanNeedle.Checked)
            {
                point.X -= GSetupPara.Calibration.NeedleXYOffset[gantry.Index].X;
                point.Y -= GSetupPara.Calibration.NeedleXYOffset[gantry.Index].Y;
            }
            else
            {
                point.Z = GSetupPara.NeedleAirBladeClean.Pos[gantry.Index].Z;
            }
            if (!GLog.SetPos(ref GSetupPara.NeedleAirBladeClean.Pos[gantry.Index], point, gantry.Name + " Needle Air Blade Clean Pos")) return;
            if (gantry.MoveOpZAbs(0)) return;

            UpdateDisplay();
        }
        private void btnABCleanPosGoto_Click(object sender, EventArgs e)
        {
            PointXYZ point = new PointXYZ(GSetupPara.NeedleAirBladeClean.Pos[gantry.Index]);
            if (cbAirBladeCleanNeedle.Checked)
            {
                point.X += GSetupPara.Calibration.NeedleXYOffset[gantry.Index].X;
                point.Y += GSetupPara.Calibration.NeedleXYOffset[gantry.Index].Y;
            }
            else
            {
                point.Z = 0;
            }

            TFLightCtrl.LightPair[gantry.Index].Set(GRecipes.Board[gantry.Index].LightDefault);
            gantry.GotoXYZ(point, true);
        }
        private void btnABCleanLearn_Click(object sender, EventArgs e)
        {
            MsgBox.Processing("AirBladeClean Learn", () => TCNeedleFunc.AirBladeClean[gantry.Index].Learn(), () => TCNeedleFunc.CFP[gantry.Index].running = false);
        }
        private void cbAirBladeCleanCamera_Click(object sender, EventArgs e)
        {
            UpdateDisplay();
        }
        private void btnExecACClean_Click(object sender, EventArgs e)
        {
            MsgBox.Processing("AirBladeClean", () => TCNeedleFunc.AirBladeClean[gantry.Index].Execute());
        }

        private void lblABCleanDnWait_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.NeedleAirBladeClean.DownWait[gantry.Index]);
            UpdateDisplay();
        }
        private void lblABCleanRelZPos_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.NeedleAirBladeClean.RelZPos[gantry.Index]);
            UpdateDisplay();
        }
        private void lblABCleanUpSpeed_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.NeedleAirBladeClean.UpSpeed[gantry.Index]);
            UpdateDisplay();
        }
        private void lblABCleanCount_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.NeedleAirBladeClean.Count[gantry.Index]);
            UpdateDisplay();
        }


        #endregion

        #region SprayNeedle
        private void btnSprayExec_Click(object sender, EventArgs e)
        {
            MsgBox.Processing("Spray", () => TCNeedleFunc.SprayClean[gantry.Index].Execute(), () => TCNeedleFunc.SprayClean[gantry.Index].running = false);
        }

        private void btnSprayLearn_Click(object sender, EventArgs e)
        {
            MsgBox.Processing("Spray Learn", () => TCNeedleFunc.SprayClean[gantry.Index].Learn());
            UpdateDisplay();
        }
        private void btnSprayPosSet_Click(object sender, EventArgs e)
        {
            PointXYZ point = gantry.PointXYZ;
            if (cbNeedleSpray.Checked)
            {
                point.X -= GSetupPara.Calibration.NeedleXYOffset[gantry.Index].X;
                point.Y -= GSetupPara.Calibration.NeedleXYOffset[gantry.Index].Y;
            }

            GLog.SetPos(ref GSetupPara.NeedleSprayClean.Pos[SelectedHead], point, gantry.Name + " Spray Pos");
            gantry.MoveOpZAbs(0);

            UpdateDisplay();
        }
        private void btnSprayPosGoto_Click(object sender, EventArgs e)
        {
            PointXYZ point = new PointXYZ(GSetupPara.NeedleSprayClean.Pos[SelectedHead]);
            if (cbNeedleSpray.Checked)
            {
                point.X += GSetupPara.Calibration.NeedleXYOffset[gantry.Index].X;
                point.Y += GSetupPara.Calibration.NeedleXYOffset[gantry.Index].Y;
            }

            TFLightCtrl.LightPair[gantry.Index].Set(GRecipes.Board[gantry.Index].LightDefault);
            gantry.GotoXYZ(point, true);
        }

        private void cbNeedleSpray_Click(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void lblSprayDnWait_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.NeedleSpray.DownWait[SelectedHead]);
            UpdateDisplay();
        }
        private void lblSprayTime_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.NeedleSpray.SprayTime[SelectedHead]);
            UpdateDisplay();
        }
        private void lblSprayPostWait_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.NeedleSpray.PostWait[SelectedHead]);
            UpdateDisplay();
        }
        private void lblSprayCount_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.NeedleSpray.Count[SelectedHead]);
            UpdateDisplay();
        }
        private void lblSprayRelZPos_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.NeedleSpray.RelZPos[SelectedHead]);
            UpdateDisplay();
        }
        #endregion

        private void lblXTTouchGap_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Calibration.XYTouchMarkGap[gantry.Index]);
            UpdateDisplay();
        }

        private void frmSetup_FormClosing(object sender, FormClosingEventArgs e)
        {
            TFGantry.GantrySelect = TFGantry.GantryLeft;
            GMotDef.GVAxis.MoveAbs(0);
        }

        private void lblEncoderRes_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Calibration.ZTouchEncoderRes[gantry.Index]);
            UpdateDisplay();
        }

    }
}
