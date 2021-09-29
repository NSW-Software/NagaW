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
    public partial class frmAuto : Form
    {
        public frmAuto()
        {
            InitializeComponent();

            cbxRunMode.DataSource = Enum.GetValues(typeof(ERunMode));
        }

        private async void btnRun_Click(object sender, EventArgs e)
        {
            //if (!TCTempCtrl.Monitoring()) return;

            ERunMode runMode = (ERunMode)cbxRunMode.SelectedIndex;

            if (GDefine.SystemState != ESystemState.Ready )
            {
                frmMsgbox msgbox = new frmMsgbox("System Not Ready.", MsgBoxBtns.OK);
                msgbox.ShowDialog();
                return;
            }

            if (!TFGantry.GLReady()) return;
            if (!TFGantry.GRReady()) return;

            if (TFLConv.Status != EStatus.Ready)
            {
                frmMsgbox msgbox = new frmMsgbox("Left Conveyor Not Ready.", MsgBoxBtns.OK);
                msgbox.ShowDialog();
                return;
            }
            if (TFRConv.Status != EStatus.Ready)
            {
                frmMsgbox msgbox = new frmMsgbox("Right Conveyor Not Ready.", MsgBoxBtns.OK);
                msgbox.ShowDialog();
                return;
            }

            GAuto.Start();
            GControl.UI_Disable(btnStop, lblLeftBdStatus, lblRightBdStatus);

            if (runMode != ERunMode.Camera) TFLightCtrl.LightPair[0].Off();
            if (runMode != ERunMode.Camera) TFLightCtrl.LightPair[1].Off();

            //var ConveyorTask = Task.Run(() =>
            //{
                ///while (GAuto.run) { if (!TFConv.TestRun()) { GAuto.Stop(); GControl.UI_Enable(); };
                if (TFConv.cToken == null || TFConv.cToken.IsCancellationRequested)
                {
                    TFConv.StartConv();

                    //while (!TFConv.cToken.IsCancellationRequested)
                    //{

                    //    if (cboxContinuous.Checked)
                    //    {
                    //        TFConv.AutoBdWaitUnloadtoNone();
                    //    }
                    //    System.Threading.Thread.Sleep(5);
                    //}
                    //GAuto.Stop();
                    //GControl.UI_Enable();
                }
            //});

            var runL = Task.Run(() =>
            {
                while (GAuto.run)
                {
                    if (TFLConv.BdStatus == EBdStatus.WaitProcess)
                    {
                        TFLConv.BdStatus = EBdStatus.Processing;
                        Inst.Board[0].RunMode = runMode;
                        if (cbContinuous.Checked)
                        {
                            TCDisp.Run[0].Continuous();
                        }
                        else
                        {
                            if (TCDisp.Run[0].All())
                            {
                                TFLConv.BdStatus = EBdStatus.Ready;
                            }
                            else
                            {
                                GAuto.Stop();
                                TFLConv.BdStatus = EBdStatus.WaitProcess;
                            }
                        }
                        TFGantry.GantryLeft.MoveOpZAbs(GRecipes.Board[0].StartPos.Z);
                    }
                    System.Threading.Thread.Sleep(5);
                }
            });
            var runR = Task.Run(() =>
            {
                while (GAuto.run)
                {
                    if (TFRConv.BdStatus == EBdStatus.WaitProcess)
                    {
                        TFRConv.BdStatus = EBdStatus.Processing;
                        Inst.Board[1].RunMode = runMode;
                        if (cbContinuous.Checked)
                        {
                            TCDisp.Run[1].Continuous();
                        }
                        else
                        {
                            if (TCDisp.Run[1].All())
                            {
                                TFRConv.BdStatus = EBdStatus.Ready;
                            }
                            else
                            {
                                GAuto.Stop();
                                TFRConv.BdStatus = EBdStatus.WaitProcess;
                            }
                        }
                        TFGantry.GantryRight.MoveOpZAbs(GRecipes.Board[1].StartPos.Z);
                    }
                    System.Threading.Thread.Sleep(5);
                }
            });

            await Task.Run(() => Task.WaitAll(runL, runR));
            TFGantry.GantryLeft.MoveOpZAbs(GRecipes.Board[0].StartPos.Z);
            TFGantry.GantryRight.MoveOpZAbs(GRecipes.Board[1].StartPos.Z);

            if (runMode != ERunMode.Camera)
            {
                TFLightCtrl.LightPair[0].Set(GRecipes.Board[0].LightDefault);
                TFLightCtrl.LightPair[1].Set(GRecipes.Board[1].LightDefault);
            }

            if (runMode == ERunMode.Camera) TFCameras.Camera[TFGantry.GantrySelect.Index].FlirCamera.Live();

            GControl.UI_Enable();
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            GAuto.Stop();
            foreach (var run in TCDisp.Run)
            {
                run.Stop();
                run.bRun = false;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            GAuto.Stop();
            foreach (var board in Inst.Board) board.ClearData();

            //foreach (var frm in Application.OpenForms.OfType<frmRecipeMap>())
            //{
            //    frm.RefreshUI();
            //    frm.Refresh();
            //}
        }

        private void UpdateDisplay()
        {
            lblLeftBdStatus.Text = TFLConv.BdStatus.ToString();
            lblLeftConvStatus.Text = TFLConv.Status.ToString();
            lblLeftMcReady.Text = TFGantry.GLStatus.ToString();
            //lblLeftBdReady.BackColor = !TFLConv.ByPassBdReady ? TFLConv.BdReady.Status ? Color.Lime : Color.LightGray : Color.Lime;
            //lblLeftMcReady.BackColor = TFLConv.McReady.Status ? Color.Red : Color.LightGray;

            //lblLeftOutBdReady.BackColor = TFLConv.SmemaOutBdReady.Status ? Color.Red : Color.LightGray;
            //lblLeftOutMcReady.BackColor = !TFLConv.ByPassMcReady ? TFLConv.SmemaOutMcReady.Status ? Color.Red : Color.LightGray : Color.Lime; 

            lblRightBdStatus.Text = TFRConv.BdStatus.ToString();
            lblRightConvStatus.Text = TFRConv.Status.ToString();
            lblLeftBdReady.Text = TFGantry.GRStatus.ToString();
            //lblRightMcReady.BackColor = !TFRConv.ByPassMcReady ? TFRConv.McReady.Status ? Color.Lime : Color.LightGray : Color.Lime;
            //lblRightBdReady.BackColor = TFRConv.BdReady.Status ? Color.Red : Color.LightGray;

            //lblRightInBdReady.BackColor = !TFRConv.ByPassBdReady ? TFRConv.SmemaInBdReady.Status ? Color.Lime : Color.LightGray : Color.Lime;
            //lblRightInMcReady.BackColor = TFRConv.SmemaInMcReady.Status ? Color.Red : Color.LightGray;

        }

        private void tmrDisplay_Tick(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void frmAuto_Load(object sender, EventArgs e)
        {
            Text = "Auto";

            tmrDisplay.Enabled = true;
            cbxRunMode.SelectedIndex = (int)ERunMode.Normal;
        }

        private void frmAuto_FormClosing(object sender, FormClosingEventArgs e)
        {
            tmrDisplay.Enabled = false;
        }

        private void btn_SetCnyStatusReady_Click(object sender, EventArgs e)
        {
            TFLConv.Status = EStatus.Ready;
            TFRConv.Status = EStatus.Ready;
            GDefine.SystemState = ESystemState.Ready;
        }

        private void tmr1s_Tick(object sender, EventArgs e)
        {
            if (GAuto.run)
            {
                if (!TFCommon.CheckMainAirPressure) GAuto.Stop();
            }
        }

    }
}
