using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NagaW
{
    public partial class frmWeighFunction : Form
    {
        int gantryIdx = 0;
        TEZMCAux.TGroup Gantry;

        List<TCmd>[] TCmds = Enumerable.Range(0, 2).Select(x => new List<TCmd>()).ToArray();
        List<string>[] Profiles = Enumerable.Range(0, 2).Select(x => new List<string>()).ToArray();
        List<DPara>[] SV = Enumerable.Range(0, 2).Select(x => new List<DPara>().Select(y => new DPara()).ToList()).ToArray();

        int ResultTab => 3;
        public frmWeighFunction()
        {
            InitializeComponent();
        }

        private void frmWeigh_Load(object sender, EventArgs e)
        {
            tabControl1.TabPages.RemoveAt(1);
            GControl.ConvertTabCtrlToFLP(tabControl1);

            //cbxWeighCalMode.DataSource = Enum.GetValues(typeof(EWeighCalMode));

            timer1.Interval = 100;
            timer1.Enabled = true;

            UpdateProfile();
            UpdateDisplay();
        }


        private void UpdateProfile()
        {
            //TCmds = Enumerable.Range(0, 2).Select(x => new List<TCmd>()).ToArray();
            //Profiles = Enumerable.Range(0, 2).Select(x => new List<string>()).ToArray();

            //var pumptype = GSystemCfg.Pump.Pumps[gantryIdx].PumpType;
            //foreach (var func in GRecipes.Functions)
            //{
            //    foreach (var tcmd in func.Cmds)
            //    {
            //        switch (tcmd.Cmd)
            //        {
            //            case ECmd.SP_SETUP:
            //                {
            //                    if (pumptype == EPumpType.SP)
            //                    {
            //                        TCmds[gantryIdx].Add(tcmd);
            //                    }
            //                    break;
            //                }
            //            case ECmd.PP4_SETUP:
            //                {
            //                    if (pumptype == EPumpType.PP4)
            //                    {
            //                        TCmds[gantryIdx].Add(tcmd);

            //                        var setup = new PP4_Setup(GRecipe.PP4_Setups[gantryIdx]);
            //                        setup.DispAmount.Value = tcmd.Para[0];
            //                        setup.DispSpeed.Value = tcmd.Para[1];
            //                        setup.BSuckAmount.Value = tcmd.Para[2];
            //                        setup.BSuckSpeed.Value = tcmd.Para[3];
            //                        setup.DispAD.Value = tcmd.Para[4];
            //                        setup.BSuckAD.Value = tcmd.Para[5];

            //                        Profiles[gantryIdx].Add($"Recipe_Function: [{func.Name}]");
            //                        SV[gantryIdx].Add(new DPara(setup.DispAmount));
            //                    }
            //                    break;
            //                }
            //            case ECmd.VERMES_32xx_SETUP:
            //                {
            //                    if (pumptype == EPumpType.VERMES_3280 || pumptype == EPumpType.VERMES_3200)
            //                    {
            //                        TCmds[gantryIdx].Add(tcmd);

            //                        var setup = new Vermes32xx_Setup(tcmd.Para[0], tcmd.Para[1], tcmd.Para[2], (int)tcmd.Para[3], (int)tcmd.Para[4], tcmd.Para[5], tcmd.Para[6]);

            //                        Profiles[gantryIdx].Add($"Recipe_Function: [{func.Name}]");
            //                        SV[gantryIdx].Add(new DPara(setup.FPress));
            //                    }
            //                    break;
            //                }
            //        }
            //    }
            //}

            //cbxCalProfile.DataSource = Profiles[gantryIdx];
        }
        private void UpdateDisplay()
        {
            btnPump1.BackColor = gantryIdx is 0 ? Color.Lime : SystemColors.Control;
            btnPump2.BackColor = gantryIdx is 1 ? Color.Lime : SystemColors.Control;
            btnPump1.Text = $"Head 1 ({GSystemCfg.Pump.Pumps[0].PumpType})";
            btnPump2.Text = $"Head 2 ({GSystemCfg.Pump.Pumps[1].PumpType})";
            Gantry = gantryIdx is 0 ? TFGantry.GantryLeft : TFGantry.GantryRight;

            //Meas
            lblDotPerSampleMeas.UpdatePara(GProcessPara.Weighing.DotPerSample[gantryIdx]);
            lblSampleCountMeas.UpdatePara(GProcessPara.Weighing.SampleCount[gantryIdx]);

            //Cal
            lblDotPerSampleCal.UpdatePara(GProcessPara.Weighing.DotPerSample[gantryIdx]);
            lblSampleCountCal.UpdatePara(GProcessPara.Weighing.SampleCount[gantryIdx]);

            lblTargetMass.UpdatePara(GProcessPara.Weighing.Target_Mass);
            lblTargetMassPercentage.UpdatePara(GProcessPara.Weighing.Target_Mass_Percentage);
            lblTargetMassRange.UpdatePara(GProcessPara.Weighing.Target_Mass_Range);

            lblTargetFR.UpdatePara(GProcessPara.Weighing.Target_FlowRate);
            lblTargetFRPercentage.UpdatePara(GProcessPara.Weighing.Target_FlowRate_Percentage);
            lblTargetFRRange.UpdatePara(GProcessPara.Weighing.Target_FlowRate_Range);

            lblTunePercentageMin.UpdatePara(GProcessPara.Weighing.Tune_Percentage_LowerLimit);
            lblTunePercentageMax.UpdatePara(GProcessPara.Weighing.Tune_Percentage_UpperLimit);

            lblIgnoreCount.UpdatePara(GProcessPara.Weighing.IgnoreCount);
            lblMdotDispTime.UpdatePara(GProcessPara.Weighing.DispTime_DotM);

            lblActualFR.UpdatePara(GProcessPara.Weighing.ActualMassFlowRate[gantryIdx]);

            //chkbxUpdataParaAfterCal.Checked = GProcessPara.Weighing.EnableUpdateTCmd;

            var svlist = SV[gantryIdx];
            if (svlist.Count > 0)
            {
                var sv = svlist[Math.Max(0, cbxCalProfile.SelectedIndex)];
                lblSVRange.Text = $"{sv.ToStringForDisplay()}\t ({sv.Value * (100 - GProcessPara.Weighing.Tune_Percentage_LowerLimit.Value) / 100} ~ {sv.Value + (sv.Value * GProcessPara.Weighing.Tune_Percentage_UpperLimit.Value / 100)})";
            }
            lblTargetMassLimit.Text = $"({GProcessPara.Weighing.Target_Mass.Value - GProcessPara.Weighing.Target_Mass_Range.Value} ~ {GProcessPara.Weighing.Target_Mass.Value + GProcessPara.Weighing.Target_Mass_Range.Value})";

            //Setting
            lblStartWait.UpdatePara(GProcessPara.Weighing.StartWait[gantryIdx]);
            lblEndWait.UpdatePara(GProcessPara.Weighing.EndWait[gantryIdx]);
            lblDotWait.UpdatePara(GProcessPara.Weighing.DotWait[gantryIdx]);
            lblReadWait.UpdatePara(GProcessPara.Weighing.ReadWait[gantryIdx]);
            lblZUpVel.UpdatePara(GProcessPara.Weighing.ZUpVel[gantryIdx]);
            lblZUpDist.UpdatePara(GProcessPara.Weighing.ZUpDist[gantryIdx]);
            lblDotPerSample.UpdatePara(GProcessPara.Weighing.DotPerSample[gantryIdx]);
            lblSampleCount.UpdatePara(GProcessPara.Weighing.SampleCount[gantryIdx]);

            lblPos.Text = GSetupPara.Weighing.Pos[gantryIdx, (int)GSystemCfg.Pump.Pumps[gantryIdx].PumpType].ToStringForDisplay();

            //Event
            lblCleanAF.UpdatePara(GProcessPara.Weighing.CleanAfterFill);
            lblPurgeAF.UpdatePara(GProcessPara.Weighing.PurgeAfterFill);
            lblFlushAF.UpdatePara(GProcessPara.Weighing.FlushAfterFill);

            groupBox11.Controls.Clear();
            if (TCmds[gantryIdx].Count > 0)
            {
                //frmRecipeCommonPara frm = new frmRecipeCommonPara(TCmds[gantryIdx][cbxCalProfile.SelectedIndex], $"{Profiles[gantryIdx][cbxCalProfile.SelectedIndex]}");
                //frm.FormBorderStyle = FormBorderStyle.None;
                //frm.Dock = DockStyle.Fill;
                //frm.TopMost = frm.TopLevel = false;
                //frm.Parent = groupBox11;
                //frm.Show();
            }


            GControl.UpdateFormControl(this);

            gbxMass.Visible = cbxWeighCalMode.SelectedIndex is 0;
            gbxMassFlowRate.Visible = cbxWeighCalMode.SelectedIndex is 1;

            tabControl2.Visible = !(cbxCalProfile.Items.Count is 0);
            groupBox11.Controls.OfType<Form>().ToList().ForEach(x => x.Enabled = false);
        }

        BindingList<TEWeighData> InstantWData = new BindingList<TEWeighData>();
        private void timer1_Tick(object sender, EventArgs e)
        {
        }


        private void btnPump1_Click(object sender, EventArgs e)
        {
            gantryIdx = 0;
            UpdateProfile();
            UpdateDisplay();

        }
        private void btnPump2_Click(object sender, EventArgs e)
        {
            gantryIdx = 1;
            UpdateProfile();
            UpdateDisplay();
        }

        #region Setting
        private void btnSet_Click(object sender, EventArgs e)
        { 
            GLog.SetPos(ref GSetupPara.Weighing.Pos[gantryIdx, (int)GSystemCfg.Pump.Pumps[gantryIdx].PumpType], Gantry.PointXYZ, "Weigh Pos");
            UpdateDisplay();
        }

        private void btnGoto_Click(object sender, EventArgs e)
        {
            var anotherG = gantryIdx is 0 ? TFGantry.GantryRight : TFGantry.GantryLeft;
            anotherG.GotoXYZ(new PointXYZ());
            Gantry.GotoXYZ(GSetupPara.Weighing.Pos[gantryIdx, (int)GSystemCfg.Pump.Pumps[gantryIdx].PumpType]);

        }
        private void lblDotWait_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Weighing.DotWait[gantryIdx]);
            UpdateDisplay();
        }
        private void lblEndWait_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Weighing.EndWait[gantryIdx]);
            UpdateDisplay();
        }
        private void lblStartWait_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Weighing.StartWait[gantryIdx]);
            UpdateDisplay();
        }
        private void lblReadWait_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Weighing.ReadWait[gantryIdx]);
            UpdateDisplay();
        }
        private void lblZUpVel_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Weighing.ZUpVel[gantryIdx]);
            UpdateDisplay();
        }
        private void lblSampleCount_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Weighing.SampleCount[gantryIdx]);
            UpdateDisplay();
        }
        private void lblDotPerSample_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Weighing.DotPerSample[gantryIdx]);
            UpdateDisplay();
        }
        private void lblZUpDist_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Weighing.ZUpDist[gantryIdx]);
            UpdateDisplay();
        }

        private void lblIgnoreCount_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Weighing.IgnoreCount);
            UpdateDisplay();
        }
        private void lblFlushAF_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Weighing.FlushAfterFill);
            UpdateDisplay();
        }
        private void lblCleanAF_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Weighing.CleanAfterFill);
            UpdateDisplay();
        }
        private void lblPurgeAF_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Weighing.PurgeAfterFill);
            UpdateDisplay();
        }
        #endregion


        #region Meas
        private void lblDotPerSampleMeas_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Weighing.DotPerSample[gantryIdx]);
            UpdateDisplay();
        }
        private void lblSampleCountMeas_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Weighing.SampleCount[gantryIdx]);
            UpdateDisplay();
        }
        private void btnbtnExec_Click(object sender, EventArgs e)
        {
            UpdateResultinRTB($"Head{gantryIdx + 1}/Measurement/Start");
            TCWeighMeas.Result.ListChanged += Result_ListChangedMeas;
            GControl.UI_Disable();
            MsgBox.Processing($"Weighing H{gantryIdx + 1}", () => TCWeighMeas.Execute(gantryIdx), () => TCWeighMeas.Stop = true);
            GControl.UI_Enable();

            TCWeighMeas.Result.ListChanged -= Result_ListChangedMeas;
            UpdateResultinRTB($"Head{gantryIdx + 1}/Measurement/End");
        }

        private void Result_ListChangedMeas(object sender, ListChangedEventArgs e)
        {
            var idx = e.NewIndex;
            if (idx < 0) return;
            UpdateResultinRTB($"{idx}: {TCWeighMeas.Result[idx]}");
        }
        #endregion

        #region Cal
        private void btnExecCal_Click(object sender, EventArgs e)
        {
            //var mode = (EWeighCalMode)cbxWeighCalMode.SelectedItem;
            //var tcmd = GProcessPara.Weighing.EnableUpdateTCmd ? TCmds[gantryIdx][cbxCalProfile.SelectedIndex] : new TCmd(TCmds[gantryIdx][cbxCalProfile.SelectedIndex]);
            //double flowrate = 0;

            //UpdateResultinRTB($"Head{gantryIdx + 1}/Calibration/{mode}/Start");
            //TCWeighCal.Result.ListChanged += Result_ListChanged;
            //tabControl1.SelectTab(ResultTab);

            //GControl.UI_Disable();
            //MsgBox.Processing($"Weigh Calibration Head {gantryIdx + 1}", () => TCWeighCal.Execute(gantryIdx, 0, 0, mode, ref tcmd, ref flowrate), () => TCWeighCal.Stop = true);
            //GControl.UI_Enable();

            //TCWeighCal.Result.ListChanged -= Result_ListChanged;
            //UpdateResultinRTB($"Head{gantryIdx + 1}/Calibration/{mode}/End");

            ////After Action
            //if (mode == EWeighCalMode.MassFlowRate) GProcessPara.Weighing.ActualMassFlowRate[gantryIdx].Value = flowrate;
            //UpdateDisplay();
        }

        private void Result_ListChanged(object sender, ListChangedEventArgs e)
        {
            //var idx = e.NewIndex;
            //if (idx < 0) return;
            //UpdateResultinRTB($"{idx}: {TCWeighCal.Result[idx].FlowRate}");
        }

        private void UpdateResultinRTB(string msg)
        {
            this.richTextBox1.Invoke(new Action(() =>
            {
                richTextBox1.AppendText($"{DateTime.Now}\t{msg}{Environment.NewLine}");
                richTextBox1.ScrollToCaret();
            }));
        }

        private void lblDotperSampleCal_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Weighing.DotPerSample[gantryIdx]);
            UpdateDisplay();
        }
        private void lblSampleCountCal_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Weighing.SampleCount[gantryIdx]);
            UpdateDisplay();
        }

        private void rbtnSVVolume_Click(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void rbtnSVFPress_Click(object sender, EventArgs e)
        {
            UpdateDisplay();
        }
        private void cbxCalProfile_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateDisplay();
        }
        private void cbxCalMode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateDisplay();
        }
        private void lblTargetMass_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Weighing.Target_Mass);
            GProcessPara.Weighing.Target_Mass_Range.Value = GProcessPara.Weighing.Target_Mass.Value * GProcessPara.Weighing.Target_Mass_Percentage.Value / 100;
            UpdateDisplay();
        }
        private void lblTargetMassPercentage_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Weighing.Target_Mass_Percentage);
            GProcessPara.Weighing.Target_Mass_Range.Value = GProcessPara.Weighing.Target_Mass.Value * GProcessPara.Weighing.Target_Mass_Percentage.Value / 100;
            UpdateDisplay();
        }
        private void lblTargetMassRange_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Weighing.Target_Mass_Range);
            GProcessPara.Weighing.Target_Mass_Percentage.Value = GProcessPara.Weighing.Target_Mass_Range.Value / GProcessPara.Weighing.Target_Mass.Value * 100;
            UpdateDisplay();
        }

        private void lblTargetFR_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Weighing.Target_FlowRate);
            GProcessPara.Weighing.Target_FlowRate_Range.Value = GProcessPara.Weighing.Target_FlowRate.Value * GProcessPara.Weighing.Target_FlowRate_Percentage.Value / 100;
            UpdateDisplay();
        }
        private void lblTargetFRPercentage_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Weighing.Target_FlowRate_Percentage);
            GProcessPara.Weighing.Target_FlowRate_Range.Value = GProcessPara.Weighing.Target_FlowRate.Value * GProcessPara.Weighing.Target_FlowRate_Percentage.Value / 100;
            UpdateDisplay();
        }
        private void lblTargetFRRange_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Weighing.Target_FlowRate_Range);
            GProcessPara.Weighing.Target_FlowRate_Percentage.Value = GProcessPara.Weighing.Target_FlowRate_Range.Value / GProcessPara.Weighing.Target_FlowRate.Value * 100;
            UpdateDisplay();
        }




        private void lblTunePercentageMin_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Weighing.Tune_Percentage_LowerLimit);
            UpdateDisplay();
        }
        private void lblTunePercentageMax_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Weighing.Tune_Percentage_UpperLimit);
            UpdateDisplay();
        }






        #endregion

        private void cbxWeighCalMode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void lblMdotDispTime_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Weighing.DispTime_DotM);
            UpdateDisplay();
        }

        private void lblActualFR_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Weighing.ActualMassFlowRate[gantryIdx]);
            UpdateDisplay();
        }

        private void chkbxUpdataParaAfterCal_Click(object sender, EventArgs e)
        {
            //GProcessPara.Weighing.EnableUpdateTCmd = !GProcessPara.Weighing.EnableUpdateTCmd;
            //UpdateDisplay();
        }
    }
}
