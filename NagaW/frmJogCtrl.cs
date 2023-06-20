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
    using Emgu.CV;
    using Emgu.CV.Structure;

    public partial class frmJogCtrl : Form
    {
        public frmJogCtrl()
        {
            InitializeComponent();
            tmrUpdateDisplay.Interval = 50;
        }

        private void frmJogCtrl_Load(object sender, EventArgs e)
        {
            tmrUpdateDisplay.Enabled = true;

            TFJogGantry.JogMode = EJogMode.Fine;
            TFJogGantry.JogRate = ERate.Norm;

            UpdateGantrySelection();
            UpdateDisplay();

            cbxFocusPoint.SelectedIndex = 0;

            //GControl.LogForm(this);
            //btnGantryL.Parent.Controls.Remove(btnGantryL);
            //btnGantryR.Parent.Controls.Remove(btnGantryR);
        }

        TEZMCAux.TGroup gantry = TFGantry.GantryLeft;
        private void UpdateGantrySelection()
        {
            btnGantryL.BackColor = GSystemCfg.Display.LeftColor;
            btnGantryR.BackColor = GSystemCfg.Display.RightColor;

            trackBar1.Value = TFLightCtrl.lightPair.CurrentLight.C1;
            trackBar2.Value = TFLightCtrl.lightPair.CurrentLight.C2;
            trackBar3.Value = TFLightCtrl.lightPair.CurrentLight.C3;
            trackBar4.Value = TFLightCtrl.lightPair.CurrentLight.C4;

            gantry = TFGantry.GantrySelect;
            BackColor = gantry == TFGantry.GantryLeft ? GSystemCfg.Display.LeftColor : GSystemCfg.Display.RightColor;
            TFLightCtrl.lightPair = gantry == TFGantry.GantryLeft ? TFLightCtrl.LightPair[0] : TFLightCtrl.LightPair[1];


            btnPosX.Text = gantry == TFGantry.GantryVR ? "R+" : "X+";
            btnNegX.Text = gantry == TFGantry.GantryVR ? "R-" : "X-";

            btnPosY.Text = gantry == TFGantry.GantryLeft ? "Y+" : "V+";
            btnNegY.Text = gantry == TFGantry.GantryLeft ? "Y-" : "V-";


            //btnGantryR.Visible = btnGantryL.Visible = TFGantry.GantrySelect == TFGantry.GantrySetup;
        }

        bool bLaserPool = true;
        private void timer1_Tick(object sender, EventArgs e)
        {
            tmrUpdateDisplay.Interval = 100;
            if (gantry != TFGantry.GantrySelect) UpdateGantrySelection();

            lblLightValue.Text = TFLightCtrl.lightPair.CurrentLight.ToStringForDisplay();

            lblAxisPos.ForeColor = offsetAxis ? Color.Red : this.ForeColor;
            string pos = new PointXYZ(gantry.Axis[0].ActualPos - xyzPos[0], gantry.Axis[1].ActualPos - xyzPos[1], gantry.Axis[2].ActualPos - xyzPos[2]).ToStringForDisplay();
            lblAxisPos.Text = pos.Replace(",", "\r\n").Replace(" ", "");

            btnPosX.BackColor = gantry.Axis[0].HLmtP ? Parent.BackColor : Color.Red;
            btnNegX.BackColor = gantry.Axis[0].HLmtN ? Parent.BackColor : Color.Red;
            btnPosY.BackColor = gantry.Axis[1].HLmtP ? Parent.BackColor : Color.Red;
            btnNegY.BackColor = gantry.Axis[1].HLmtN ? Parent.BackColor : Color.Red;
            btnPosZ.BackColor = gantry.Axis[2].HLmtP ? Parent.BackColor : Color.Red;
            btnNegZ.BackColor = gantry.Axis[2].HLmtN ? Parent.BackColor : Color.Red;

            var light = TFLightCtrl.lightPair.CurrentLight;

            trackBar1.Value = light.C1;
            trackBar2.Value = light.C2;
            trackBar3.Value = light.C3;
            trackBar4.Value = light.C4;

            lblLaser.ForeColor = measLaser ? Color.Red : this.ForeColor;
            if (bLaserPool)
            {
                if (TCWafer.IsProcessing) return;
                double value = 0;
                if (!TFHSensors.Sensor[gantry.Index].GetValue(ref value)) bLaserPool = false;
                lblLaser.Text = (value - laserZero).ToString("f3");
            }
            else
            {
                lblLaser.Text = "----";
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UpdateDisplay()
        {
            lblStep.UpdatePara(TFJogGantry.JogStep);

            btnJogMode.Text = TFJogGantry.JogMode.ToString();
            btnStep1um.BackColor = TFJogGantry.JogRate == ERate.um1 ? Color.Lime : Color.Transparent;
            btnStep5um.BackColor = TFJogGantry.JogRate == ERate.um5 ? Color.Lime : Color.Transparent;
            btnStep10um.BackColor = TFJogGantry.JogRate == ERate.um10 ? Color.Lime : Color.Transparent;
            btnStepDefineum.BackColor = TFJogGantry.JogRate == ERate.Step ? Color.Lime : Color.Transparent;

            btnNegZ.Enabled = btnPosZ.Enabled = zEnable;

            btnLightToggle.Text = $"Light {selectedLight}";
            btnLed0.BackColor = selectedLight == eLight.Led0 ? Color.Lime : Color.Transparent;
            btnLed1.BackColor = selectedLight == eLight.Led1 ? Color.Lime : Color.Transparent;
            btnLed2.BackColor = selectedLight == eLight.Led2 ? Color.Lime : Color.Transparent;
        }

        bool zEnable;
        bool offsetAxis;
        double[] xyzPos = new double[3];
        bool measLaser = false;
        double laserZero = 0;

        #region Jog
        private void btnJogMode_Click(object sender, EventArgs e)
        {
            TFJogGantry.ToggleMode();
            UpdateDisplay();
        }

        private void btnLockZ_Click(object sender, EventArgs e)
        {
            zEnable = !zEnable;
            UpdateDisplay();
        }

        private /*async*/ void btnPosX_MouseDown(object sender, MouseEventArgs e)
        {
            //await Task.Run(() => TFJogGantry.JogStart(0, EDirection.Positive));
            TFJogGantry.JogStart(0, EDirection.Positive);
        }
        private void btnPosX_MouseUp(object sender, MouseEventArgs e)
        {
            TFJogGantry.JogStop(0);
        }
        private void btnPosX_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X < 0 || e.X > ((Button)sender).Width || e.Y < 0 || e.Y > ((Button)sender).Height)
                TFJogGantry.JogStop(0);
        }

        private /*async*/ void btnNegX_MouseDown(object sender, MouseEventArgs e)
        {
            //await Task.Run(() => TFJogGantry.JogStart(0, EDirection.Negative));
            TFJogGantry.JogStart(0, EDirection.Negative);
        }
        private void btnNegX_MouseUp(object sender, MouseEventArgs e)
        {
            TFJogGantry.JogStop(0);
        }
        private void btnNegX_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X < 0 || e.X > ((Button)sender).Width || e.Y < 0 || e.Y > ((Button)sender).Height)
                TFJogGantry.JogStop(0);
        }

        private /*async*/ void btnPosY_MouseDown(object sender, MouseEventArgs e)
        {
            //await Task.Run(() => TFJogGantry.JogStart(1, EDirection.Positive));
            TFJogGantry.JogStart(1, EDirection.Positive);
        }
        private void btnPosY_MouseUp(object sender, MouseEventArgs e)
        {
            TFJogGantry.JogStop(1);
        }
        private void btnPosY_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X < 0 || e.X > ((Button)sender).Width || e.Y < 0 || e.Y > ((Button)sender).Height)
                TFJogGantry.JogStop(1);
        }

        private /*async*/ void btnNegY_MouseDown(object sender, MouseEventArgs e)
        {
            //await Task.Run(() => TFJogGantry.JogStart(1, EDirection.Negative));
            TFJogGantry.JogStart(1, EDirection.Negative);
        }
        private void btnNegY_MouseUp(object sender, MouseEventArgs e)
        {
            TFJogGantry.JogStop(1);
        }
        private void btnNegY_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X < 0 || e.X > ((Button)sender).Width || e.Y < 0 || e.Y > ((Button)sender).Height)
                TFJogGantry.JogStop(1);
        }

        private /*async*/ void btnPosZ_MouseDown(object sender, MouseEventArgs e)
        {
            //await Task.Run(() => TFJogGantry.JogStart(2, EDirection.Positive));
            TFJogGantry.JogStart(2, EDirection.Positive);
        }
        private void btnPosZ_MouseUp(object sender, MouseEventArgs e)
        {
            TFJogGantry.JogStop(2);
        }
        private void btnPosZ_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X < 0 || e.X > ((Button)sender).Width || e.Y < 0 || e.Y > ((Button)sender).Height)
                TFJogGantry.JogStop(2);
        }

        private /*async*/ void btnNegZ_MouseDown(object sender, MouseEventArgs e)
        {
            //await Task.Run(() => TFJogGantry.JogStart(2, EDirection.Negative));
            TFJogGantry.JogStart(2, EDirection.Negative);
        }
        private void btnNegZ_MouseUp(object sender, MouseEventArgs e)
        {
            TFJogGantry.JogStop(2);
        }
        private void btnNegZ_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X < 0 || e.X > ((Button)sender).Width || e.Y < 0 || e.Y > ((Button)sender).Height)
                TFJogGantry.JogStop(2);
        }
        #endregion

        #region Vision
        private void SetLight()
        {
            var light = new LightRGBA(trackBar1.Value, trackBar2.Value, trackBar3.Value, trackBar4.Value);
            TFLightCtrl.lightPair.Set(light);
            
            switch (selectedLight)
            {
                case eLight.Led0:
                    GRecipes.Board[gantry.Index].LightDefault = new LightRGBA(light);
                    break;
                case eLight.Led1:
                    GRecipes.Board[gantry.Index].Light1 = new LightRGBA(light);
                    break;
                case eLight.Led2:
                    GRecipes.Board[gantry.Index].Light2 = new LightRGBA(light);
                    break;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            SetLight();
        }
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            SetLight();
        }
        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            SetLight();
        }
        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            SetLight();
        }

        private void btnLightOnOff_Click(object sender, EventArgs e)
        {
            if (TFLightCtrl.lightPair.IsOff) TFLightCtrl.lightPair.Set(TFLightCtrl.lightPair.CurrentLight);
            else TFLightCtrl.lightPair.Off();
        }

        #endregion

        private void btnAutoFocus_Click(object sender, EventArgs e)
        {
            MsgBox.Processing("Auto Focusing", () => TCAutoFocus.Execute(gantry));
            //TFCameras.Camera[gantry.Index].Live();
            TFCamera1.Cameras[gantry.Index].Live();
            UpdateFocus();
        }
        private void btnToggleLight_Click(object sender, EventArgs e)
        {
            selectedLight = eLight.None;

            if (TFLightCtrl.lightPair.IsOff) 
                TFLightCtrl.lightPair.Set(TFLightCtrl.lightPair.CurrentLight);
            else 
                TFLightCtrl.lightPair.Off();
        }

        private void btnStep1um_Click(object sender, EventArgs e)
        {
            TFJogGantry.ToggleRate(ERate.um1);
            UpdateDisplay();
        }
        private void btnStep5um_Click(object sender, EventArgs e)
        {
            TFJogGantry.ToggleRate(ERate.um5);
            UpdateDisplay();
        }
        private void btnStep10um_Click(object sender, EventArgs e)
        {
            TFJogGantry.ToggleRate(ERate.um10);
            UpdateDisplay();
        }
        private void btnStepDefineum_Click(object sender, EventArgs e)
        {
            TFJogGantry.ToggleRate(ERate.Step);
            UpdateDisplay();
        }
        private void lblStep_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref TFJogGantry.JogStep);
            UpdateDisplay();
        }

        private void lblAxisPos_Click(object sender, EventArgs e)
        {
            offsetAxis = !offsetAxis;
            xyzPos = offsetAxis ? new double[] { gantry.Axis[0].ActualPos, gantry.Axis[1].ActualPos, gantry.Axis[2].ActualPos } : new double[3];
        }
        private void lblLaser_Click(object sender, EventArgs e)
        {
            bLaserPool = true;
            //bLaserOK = !bLaserOK;
            measLaser = !measLaser;

            laserZero = 0;
            if (measLaser) TFHSensors.Sensor[gantry.Index].GetValue(ref laserZero);
        }

        private void btnNegZ_EnabledChanged(object sender, EventArgs e)
        {
            if (!zEnable) UpdateDisplay();
        }

        #region FocusZ
        private void btnFocusSet_Click(object sender, EventArgs e)
        {
            int idx = cbxFocusPoint.SelectedIndex;
            double z = TFGantry.GantrySelect.ZAxis.ActualPos;
            if (MsgBox.ShowDialog($"Set Focus No[{idx}]\r\n{GRecipes.CamFocusNo[TFGantry.GantrySelect.Index][idx]} mm to {z} mm", MsgBoxBtns.OKCancel) != DialogResult.OK) return;
            GRecipes.CamFocusNo[TFGantry.GantrySelect.Index][idx] = z;
            UpdateFocus();
        }
        private void btnFocusGet_Click(object sender, EventArgs e)
        {
            TFGantry.GantrySelect.MoveOpZAbs(GRecipes.CamFocusNo[TFGantry.GantrySelect.Index][cbxFocusPoint.SelectedIndex]);
        }
        private void cbxFocusPoint_Click(object sender, EventArgs e)
        {
            UpdateFocus();
        }
        private void UpdateFocus()
        {
            int idx = Math.Max(cbxFocusPoint.SelectedIndex, 0);
            cbxFocusPoint.DataSource = Enumerable.Range(0, GRecipes.CamFocusNo[gantry.Index].Count).Select(x => $"{x}: {GRecipes.CamFocusNo[gantry.Index][x]:f3}").ToArray();
            cbxFocusPoint.SelectedIndex = idx;
        }
        private void btnFocusZReset_Click(object sender, EventArgs e)
        {
            int idx = cbxFocusPoint.SelectedIndex;
            if (MsgBox.ShowDialog($"Reset Focus No[{idx}]\r\n{GRecipes.CamFocusNo[TFGantry.GantrySelect.Index][idx]} mm", MsgBoxBtns.OKCancel) != DialogResult.OK) return;
            GRecipes.CamFocusNo[TFGantry.GantrySelect.Index][idx] = 0;
            UpdateFocus();
        }
        #endregion

        private void btnGantryL_Click(object sender, EventArgs e)
        {
            TFGantry.GantrySelect = TFGantry.GantryLeft;
            //frmMain.Cam.Link(0);
            frmMain.Cam = new frmCamera(0);
        }
        private void btnGantryR_Click(object sender, EventArgs e)
        {
            TFGantry.GantrySelect = TFGantry.GantryVR;
            //frmMain.Cam.Link(0);
            frmMain.Cam = new frmCamera(0);
        }

        private void btnLightToggle_Click(object sender, EventArgs e)
        {
            if (TFLightCtrl.lightPair.IsOff)
            {
                switch (selectedLight)
                {
                    case eLight.None:
                        btnLed0_Click(sender, e); break;
                    case eLight.Led0:
                        btnLed0_Click(sender, e); break;
                    case eLight.Led1:
                        btnLed1_Click(sender, e); break;
                    case eLight.Led2:
                        btnLed2_Click(sender, e); break;
                }
            }
            else
            switch (selectedLight)
            {
                case eLight.None:
                    btnLed0_Click(sender, e); break;
                case eLight.Led0:
                    btnLed1_Click(sender, e); break;
                case eLight.Led1:
                    btnLed2_Click(sender, e); break;
                case eLight.Led2:
                    btnLed0_Click(sender, e); break;
            }
            UpdateDisplay();
        }

        enum eLight { None, Led0, Led1, Led2 };
        eLight selectedLight = eLight.None;
        private void btnLed0_Click(object sender, EventArgs e)
        {
            if (selectedLight == eLight.Led0)
                selectedLight = eLight.None;
            else
            {
                selectedLight = eLight.Led0;
                TFLightCtrl.LightPair[gantry.Index].Set(GRecipes.Board[gantry.Index].LightDefault);
            }
            UpdateDisplay();
        }

        private void btnLed1_Click(object sender, EventArgs e)
        {
            if (selectedLight == eLight.Led1)
                selectedLight = eLight.None;
            else
            {
                selectedLight = eLight.Led1;
                TFLightCtrl.LightPair[gantry.Index].Set(GRecipes.Board[gantry.Index].Light1);
            }
            UpdateDisplay();
        }

        private void btnLed2_Click(object sender, EventArgs e)
        {
            if (selectedLight == eLight.Led2)
                selectedLight = eLight.None;
            else
            {
                selectedLight = eLight.Led2;
                TFLightCtrl.LightPair[gantry.Index].Set(GRecipes.Board[gantry.Index].Light2);
            }
            UpdateDisplay();
        }
    }
}
