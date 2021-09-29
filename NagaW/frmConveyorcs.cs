using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;
using System.Diagnostics;

namespace NagaW
{
    public partial class frmConveyorcs : Form
    {
        public frmConveyorcs()
        {
            InitializeComponent();
        }

        private void frmConveyorcs_Load(object sender, EventArgs e)
        {
            tmrDisplay.Enabled = true;
            if(GSystemCfg.Conveyor.Conv_Interface == EConvInterface.SMEMA_LR_MIRROR)
            {
                btnLeftSmemaOutTest.Visible = true;
                btnRightSmemaInTest.Visible = true;
                lblLeftSmemaOutSHow.Visible = true;
                lblLeftOutBdReady.Visible = true;
                lblLeftOutMcReady.Visible = true;

                lblRightSmemaINShow.Visible = true;
                lblRightInBdReady.Visible = true;
                lblRightInMcReady.Visible = true;

            }

        }

        private void frmConveyorcs_FormClosing(object sender, FormClosingEventArgs e)
        {
            tmrDisplay.Enabled = false;
            running = false;
            GControl.UI_Enable();
        }

        private void UpdateDisplay()
        {
            lblLeftBdStatus.Text = TFLConv.BdStatus.ToString();
            lblLeftConvStatus.Text = TFLConv.Status.ToString();

            lblLeftBdReady.BackColor = !TFLConv.ByPassBdReady ? TFLConv.BdReady.Status ? Color.Lime : Color.LightGray:Color.Lime;
            lblLeftMcReady.BackColor = TFLConv.McReady.Status ? Color.Red : Color.LightGray;

            lblLeftOutBdReady.BackColor = TFLConv.MirrorSmemaOutBdReady .Status ? Color.Red : Color.LightGray;
            lblLeftOutMcReady.BackColor = !TFLConv.ByPassMcReady ?  TFLConv.MirrorSmemaOutMcReady.Status ? Color.Red : Color.LightGray: Color.Lime; ;

            lblRightBdStatus.Text = TFRConv.BdStatus.ToString();
            lblRightConvStatus.Text = TFRConv.Status.ToString();

            lblRightMcReady.BackColor = !TFRConv.ByPassMcReady ?  TFRConv.McReady.Status ? Color.Lime : Color.LightGray : Color.Lime;
            lblRightBdReady.BackColor = TFRConv.BdReady.Status ? Color.Red : Color.LightGray;

            lblRightInBdReady.BackColor = !TFRConv.ByPassBdReady ? TFRConv.MirrorSmemaInBdReady.Status ? Color.Lime : Color.LightGray : Color.Lime;
            lblRightInMcReady.BackColor = TFRConv.MirrorSmemaInMcReady.Status ? Color.Red : Color.LightGray;
            lblContinuousTimeOut.Text = TFConv.testRunTime.ToString();

        }

        private async void btnConvInitAll_Click(object sender, EventArgs e)
        {
            GControl.UI_Disable();

            await Task.Run(() =>
            {
                TFConv.InitAll();
            });

            GControl.UI_Enable();
        }

        private void btnReturnLeft_Click(object sender, EventArgs e)
        {
            if (TFLConv.SensInPsnt.Status)
            {
                GAlarm.Prompt(EAlarm.CONV_Remove_Board_On_IN_PSNT);
                return;
            }
            Task.Run(() => TFLConv.ReturnToLeft());
        }

        private void btnLoadFromLeft_Click(object sender, EventArgs e)
        {
            if (TFLConv.SensBdPsnt.Status)
            {
                GAlarm.Prompt(EAlarm.CONV_Remove_Board_On_LEFT_PSNT);
                return;
            }

            Task.Run(() => TFLConv.LoadToPro());
        }

        private void btnLeftReturnRight_Click(object sender, EventArgs e)
        {
            if (TFRConv.SensBdPsnt.Status)
            {
                GAlarm.Prompt(EAlarm.CONV_Remove_Board_On_RIGHT_PSNT);
                return;
            }

            if (TFRConv.SensOutPsnt.Status)
            {
                GAlarm.Prompt(EAlarm.CONV_Remove_Board_On_OUT_PSNT);
                return;
            }
            Task.Run(() => TFLConv.ReturnToRight());
        }

        private void btnUnloadLeft_Click(object sender, EventArgs e)
        {

        }

        private void btnLeftUnloadRight_Click(object sender, EventArgs e)
        {
            if (TFRConv.SensBdPsnt.Status)
            {
                GAlarm.Prompt(EAlarm.CONV_Remove_Board_On_RIGHT_PSNT);
                return;
            }

            if (TFRConv.SensOutPsnt.Status)
            {
                GAlarm.Prompt(EAlarm.CONV_Remove_Board_On_OUT_PSNT);
                return;
            }
            Task.Run(() => TFLConv.UnLoadToRight());
        }

        private void btnLoadRightFromLeft_Click(object sender, EventArgs e)
        {
            if (!(TFLConv.SensInPsnt.Status || TFLConv.SensBdPsnt.Status))
            {
                return;
            }

            if (TFRConv.SensBdPsnt.Status)
            {
                GAlarm.Prompt(EAlarm.CONV_Remove_Board_On_RIGHT_PSNT);
                return;
            }

            if (TFRConv.SensOutPsnt.Status)
            {
                GAlarm.Prompt(EAlarm.CONV_Remove_Board_On_OUT_PSNT);
                return;
            }
            Task.Run(() => TFRConv.LoadLeftToPro());
        }

        private void btnLoadFromRight_Click(object sender, EventArgs e)
        {
            if (TFRConv.SensBdPsnt.Status)
            {
                GAlarm.Prompt(EAlarm.CONV_Remove_Board_On_RIGHT_PSNT);
                return;
            }
            Task.Run(() => TFRConv.LoadRightToPro());
        }

        private void btnReturnRight_Click(object sender, EventArgs e)
        {
            if (TFRConv.SensOutPsnt.Status)
            {
                GAlarm.Prompt(EAlarm.CONV_Remove_Board_On_OUT_PSNT);
                return;
            }
            Task.Run(() => TFRConv.ReturnToRight());
        }

        private void btnUnloadRight_Click(object sender, EventArgs e)
        {

        }

        private void btnRReturnLeft_Click(object sender, EventArgs e)
        {
            if(TFLConv.SensInPsnt.Status)
            {
                GAlarm.Prompt(EAlarm.CONV_Remove_Board_On_IN_PSNT);
                return;

            }
            if(TFLConv.SensBdPsnt.Status)
            {
                GAlarm.Prompt(EAlarm.CONV_Remove_Board_On_LEFT_PSNT);
                return;
            }
            Task.Run(() => TFRConv.ReturnToLeft());
        }

        private void tmrDisplay_Tick(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void btnSmemaIn_Click(object sender, EventArgs e)
        {
            Task.Run(() => TFLConv.SmemaLoadIn());
        }
        private void btnLeftSmemaOutTest_Click(object sender, EventArgs e)
        {
            Task.Run(() => TFLConv.SmemaSendOut());
        }



        private void lblLeftBdReady_MouseDown(object sender, MouseEventArgs e)
        {
            TFLConv.ByPassBdReady = true;
        }

        private void lblRightMcReady_MouseDown(object sender, MouseEventArgs e)
        {
            TFRConv.ByPassMcReady = true;
        }

        private void btnSmemaSendOut_Click(object sender, EventArgs e)
        {
            Task.Run(() => TFRConv.SmemaSendOut());
        }
        private void btnRightSmemaInTest_Click(object sender, EventArgs e)
        {
            Task.Run(() => TFRConv.SmemaLoadIn());
        }
        private void lblLeftBdReady_MouseUp(object sender, MouseEventArgs e)
        {
            TFLConv.ByPassBdReady = false;
        }

        private void lblRightMcReady_MouseUp(object sender, MouseEventArgs e)
        {
            TFRConv.ByPassMcReady = false;
        }

        private void lblLeftBdReady_Click(object sender, EventArgs e)
        {

        }


        private void lblLeftBdStatus_Click(object sender, EventArgs e)
        {
            TFLConv.BdStatus = EBdStatus.Ready;
        }

        private void lblRightBdStatus_Click(object sender, EventArgs e)
        {
            TFRConv.BdStatus = EBdStatus.Ready;
        }

        bool running = false;
        private void btnTestRun_Click(object sender, EventArgs e)
        {
            running = true;
            GControl.UI_Disable();

            btnStopRun.Enabled = true;
            lblLeftBdStatus.Enabled = true;
            lblRightBdStatus.Enabled = true;
            lblLeftBdReady.Enabled = true;
            lblRightMcReady.Enabled = true;
            lblLeftOutMcReady.Enabled = true;
            lblRightInBdReady.Enabled = true;

            if (TFConv.cToken == null || TFConv.cToken.IsCancellationRequested)
            {
                TFConv.StartConv();
            }

            Task.Run(() =>
            {
                while (running)
                {
                    TFConv.AutoBdReady();
                    Thread.Sleep(100);
                };
            });
        }

        private void btnStopRun_Click(object sender, EventArgs e)
        {
            running = false;
            TFConv.StopConv();
            GControl.UI_Enable();
        }

        private async void btnLefConvInit_Click(object sender, EventArgs e)
        {
            GControl.UI_Disable();
            //TFConv.stopConv();
            await Task.Run(() =>
            {
                TFLConv.Init();
            });

            GControl.UI_Enable();

        }

        private async void btnRightConvInit_Click(object sender, EventArgs e)
        {
            GControl.UI_Disable();
            //TFConv.stopConv();
            await Task.Run(() =>
            {
                TFRConv.Init();
            });

            GControl.UI_Enable();
        }

        private void lblContinuousTimeOut_Click(object sender, EventArgs e)
        {
            IPara ReadyTimer = new IPara("Ready Timer",  TFConv.testRunTime, 10, 99999, EUnit.MILLISECOND);
            GLog.SetPara(ref ReadyTimer);
            lblContinuousTimeOut.Text = ReadyTimer.Value.ToString();
            TFConv.testRunTime = ReadyTimer.Value;
        }

        private void lblLeftOutMcReady_MouseDown(object sender, MouseEventArgs e)
        {
            TFLConv.ByPassMcReady = true;
        }

        private void lblLeftOutMcReady_MouseUp(object sender, MouseEventArgs e)
        {
            TFLConv.ByPassMcReady = false;
        }

        private void lblRightInBdReady_MouseDown(object sender, MouseEventArgs e)
        {
            TFRConv.ByPassBdReady = true;
        }

        private void lblRightInBdReady_MouseUp(object sender, MouseEventArgs e)
        {
            TFRConv.ByPassBdReady = false;
        }

        private void btnLeftUpDn_Click(object sender, EventArgs e)
        {
            if (TFLConv.SensConvUp.Status)
                TFLConv.ConvDn();
            else
                TFLConv.ConvUp();
        }
        private void btnRightUpDn_Click(object sender, EventArgs e)
        {
            if (TFRConv.SensConvUp.Status)
                TFRConv.ConvDn();
            else
                TFRConv.ConvUp();
        }

        private void btnLeftRev_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X < ((Button)sender).Width / 2)
                TFLConv.MoveRevFast();
            else
                TFLConv.MoveRevSlow();
        }
        private void btnLeftRev_MouseUp(object sender, MouseEventArgs e)
        {
            TFLConv.Stop();
        }
        private void btnLeftFwd_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X > ((Button)sender).Width / 2)
                TFLConv.MoveFwdFast();
            else
                TFLConv.MoveFwdSlow();
        }
        private void btnLeftFwd_MouseUp(object sender, MouseEventArgs e)
        {
            TFLConv.Stop();
        }

        private void btnRightRev_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X < ((Button)sender).Width / 2)
                TFRConv.MoveRevFast();
            else
                TFRConv.MoveRevSlow();
        }
        private void btnRightRev_MouseUp(object sender, MouseEventArgs e)
        {
            TFRConv.Stop();
        }
        private void btnRightFwd_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X > ((Button)sender).Width / 2)
                TFRConv.MoveFwdFast();
            else
                TFRConv.MoveFwdSlow();
        }
        private void btnRightFwd_MouseUp(object sender, MouseEventArgs e)
        {
            TFRConv.Stop();
        }
    }
}
