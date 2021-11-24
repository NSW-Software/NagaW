using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace NagaW
{
    public partial class frmAuto : Form
    {
        public frmAuto()
        {
            InitializeComponent();

            cbxRunMode.DataSource = Enum.GetValues(typeof(ERunMode));
        }

        static IPara count = new IPara("Wafer Count", 0, 0, 100, EUnit.COUNT);
        static IPara timeout = new IPara("Interval Timeout", 5, 2, 1000, EUnit.SECOND);
        static bool EnableNotchAlignment = true;
        static bool EnableSVIonizer = true;

        private void frmAuto_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            Text = "Auto";
            cbxRunMode.SelectedIndex = (int)ERunMode.Normal;
            UpdateDisplay();
        }
        private void frmAuto_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private async void btnRun_Click(object sender, EventArgs e)
        {
            if (!TFSafety.LockDoor())
            {
                TFSafety.ReleaseDoor();
                return;
            }

            TCWafer.SvIonizer.Status = EnableSVIonizer;

            ERunMode runMode = (ERunMode)cbxRunMode.SelectedIndex;

            if (GDefine.SystemState != ESystemState.Ready)
            {
                frmMsgbox msgbox = new frmMsgbox("System Not Ready.", MsgBoxBtns.OK);
                msgbox.ShowDialog();
                return;
            }

            if (!TFGantry.GLReady()) return;

            GControl.UI_Disable(btnStop);

            if (runMode != ERunMode.Camera) TFLightCtrl.LightPair[0].Off();

            int c = count.Value is 0 ? int.MaxValue : count.Value;


            bool continueDisp = false;
            if (TCWafer.IsWaferDetected)
            {
                if (MsgBox.ShowDialog("Wafer detected.\nContinue to run process?", MsgBoxBtns.OKCancel) != DialogResult.OK) goto _End;
                continueDisp = true;
            }

            await Task.Run(() =>
            {
                for (int i = 0; i < c; i++)
                {
                    if (!continueDisp)
                    {
                        if (!TCWafer.AutoLoad(timeout.Value * 1000)) break;

                        if (EnableNotchAlignment)
                        {
                            if (!TCWafer.NotchAlignment()) return;
                        }
                    }

                    Inst.Board[0].RunMode = runMode;
                    if (!TCDisp.Run[0].All()) break;

                    if (!TCWafer.AutoUnload()) break;
                }

            });

        _End:
            if (!TFGantry.GantryLeft.MoveOpZAbs(GRecipes.Board[0].StartPos.Z)) return;

            if (runMode != ERunMode.Camera)
            {
                TFLightCtrl.LightPair[0].Set(GRecipes.Board[0].LightDefault);
            }

            if (runMode == ERunMode.Camera) TFCameras.Camera[TFGantry.GantrySelect.Index].FlirCamera.Live();

            GControl.UI_Enable();
            TCWafer.SvIonizer.Status = false;
            TFSafety.ReleaseDoor();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            TCWafer.SMEMA_ING = false;

            foreach (var run in TCDisp.Run)
            {
                run.Stop();
                run.bRun = false;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            foreach (var board in Inst.Board) board.ClearData();
        }

        private void UpdateDisplay()
        {
            lblWaferInput.UpdatePara(count);
            lblIntervalTimeout.UpdatePara(timeout);
            chbxEnaNotch.Checked = EnableNotchAlignment;
            chbxEnableIonizer.Checked = EnableSVIonizer;
        }

        private void lblWaferInput_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref count);
            UpdateDisplay();
        }

        private void lblIntervalTimeout_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref timeout);
            UpdateDisplay();
        }

        private void chbxEnaNotch_Click(object sender, EventArgs e)
        {
            EnableNotchAlignment = !EnableNotchAlignment;
            UpdateDisplay();
        }


        private async void btnAutoLoad_Click(object sender, EventArgs e)
        {
            if (!TCWafer.SMEMA_ING)
            {
                GControl.UI_Disable(sender as Button);
                (sender as Button).Text = "STOP Auto Load";
                await Task.Run(() => TCWafer.AutoLoad(timeout.Value));
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

        private void btnDoorLock_Click(object sender, EventArgs e)
        {
            TFSafety.LockDoor();
        }

        private void btnDoorUnlock_Click(object sender, EventArgs e)
        {
            TFSafety.ReleaseDoor();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDoorLockSens.BackColor = TFSafety.DoorLocked.Status ? Color.Lime : SystemColors.ControlDark;
        }

        private void chbxEnableIonizer_Click(object sender, EventArgs e)
        {
            EnableSVIonizer = !EnableSVIonizer;
        }
    }
}
