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

        private async void btnRun_Click(object sender, EventArgs e)
        {
            ERunMode runMode = (ERunMode)cbxRunMode.SelectedIndex;

            if (GDefine.SystemState != ESystemState.Ready )
            {
                frmMsgbox msgbox = new frmMsgbox("System Not Ready.", MsgBoxBtns.OK);
                msgbox.ShowDialog();
                return;
            }

            if (!TFGantry.GLReady()) return;

            GControl.UI_Disable(btnStop);

            if (runMode != ERunMode.Camera) TFLightCtrl.LightPair[0].Off();

            int c = count.Value is 0 ? int.MaxValue : count.Value;


            await Task.Run(() =>
            {
                for (int i = 0; i < c; i++)
                {
                    if (TCWafer.IsWaferDetected)
                    {
                        if (MsgBox.ShowDialog("Wafer detected, continue to run process?", MsgBoxBtns.OKCancel) != DialogResult.OK) break;
                        goto _run;
                    }

                    if (!TCWafer.AutoLoad(timeout.Value * 1000)) break;

                    _run:
                    Inst.Board[0].RunMode = runMode;
                    if (!TCDisp.Run[0].All()) break;

                    if (!TCWafer.AutoUnload()) break;
                }

            });


            if (!TFGantry.GantryLeft.MoveOpZAbs(GRecipes.Board[0].StartPos.Z)) return;

            if (runMode != ERunMode.Camera)
            {
                TFLightCtrl.LightPair[0].Set(GRecipes.Board[0].LightDefault);
            }

            if (runMode == ERunMode.Camera) TFCameras.Camera[TFGantry.GantrySelect.Index].FlirCamera.Live();

            GControl.UI_Enable();
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
        }


        private void frmAuto_Load(object sender, EventArgs e)
        {
            Text = "Auto";
            cbxRunMode.SelectedIndex = (int)ERunMode.Normal;
            UpdateDisplay();
        }

        private void frmAuto_FormClosing(object sender, FormClosingEventArgs e)
        {
        }


        private void tmr1s_Tick(object sender, EventArgs e)
        {
            if (GAuto.run)
            {
                if (!TFCommon.CheckMainAirPressure) GAuto.Stop();
            }
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
    }
}
