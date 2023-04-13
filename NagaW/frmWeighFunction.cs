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
using System.Windows.Forms.DataVisualization.Charting;

namespace NagaW
{
    public partial class frmWeighFunction : Form
    {
        int gantryIdx = 0;
        TEZMCAux.TGroup Gantry;

        List<TCmd>[] TCmds = Enumerable.Range(0, 2).Select(x => new List<TCmd>()).ToArray();
        List<string>[] Profiles = Enumerable.Range(0, 2).Select(x => new List<string>()).ToArray();
        List<DPara>[] SV = Enumerable.Range(0, 2).Select(x => new List<DPara>().Select(y => new DPara()).ToList()).ToArray();

        static SeriesChartType seriesChartType = SeriesChartType.Point;

        int ResultTab => tabControl1.TabPages.IndexOf(tpResult);

        public frmWeighFunction()
        {
            InitializeComponent();
        }

        private void frmWeigh_Load(object sender, EventArgs e)
        {
            btnPump2.Visible = btnPump2.Enabled = false;
            //tabControl1.TabPages.RemoveAt(1);
            GControl.ConvertTabCtrlToFLP(tabControl1);

            cbxWeighMode.DataSource = Enum.GetValues(typeof(EWeighMode));
            cbxWeighType.DataSource = Enum.GetValues(typeof(EWeighType));

            var ca = chartWeigh.ChartAreas[0];
            ca.AxisX.ScaleView.Zoomable = true;
            ca.CursorX.AutoScroll = true;
            ca.CursorY.AutoScroll = false;
            ca.AxisX.MajorGrid.Enabled = false;

            UpdateProfile();
            UpdateSV();
            UpdateDisplay();

            #region chartcontextmenu
            chartWeigh.MouseDown += (a, b) =>
            {
                if (b.Button != MouseButtons.Right) return;

                ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
                contextMenuStrip.Items.AddRange(Enum.GetValues(typeof(SeriesChartType)).OfType<SeriesChartType>().Select(x =>
                new ToolStripMenuItem(x.ToString(), null, (c, d) =>
                {
                    seriesChartType = x;
                    if (chartWeigh.Series.Count > 0) chartWeigh.Series[0].ChartType = x;
                })
                { Checked = x == seriesChartType }).ToArray());

                contextMenuStrip.Show();
            };
            #endregion
        }

        private void UpdateProfile()
        {
            int functionNo = 0;

            TCmds = Enumerable.Range(0, 2).Select(x => new List<TCmd>()).ToArray();
            Profiles = Enumerable.Range(0, 2).Select(x => new List<string>()).ToArray();

            var pumptype = GSystemCfg.Pump.Pumps[gantryIdx].PumpType;

            #region cmdpump
            foreach (var func in GRecipes.Functions[gantryIdx])
            {
                foreach (var tcmd in func.Cmds)
                {
                    switch (tcmd.Cmd)
                    {
                        case ECmd.SP_SETUP:
                            {
                                if (pumptype == EPumpType.SP)
                                {
                                    TCmds[gantryIdx].Add(tcmd);
                                    Profiles[gantryIdx].Add($"Recipe_Function: [{func.Name}]");
                                }
                                break;
                            }

                        case ECmd.VERMES_3280_SETUP:
                            {
                                if (pumptype == EPumpType.VERMES_3280)
                                {
                                    TCmds[gantryIdx].Add(tcmd);
                                    Profiles[gantryIdx].Add($"Recipe_Function: [{func.Name}]");
                                }
                                break;
                            }

                        case ECmd.HM_SETUP:
                            {
                                if (pumptype == EPumpType.HM)
                                {
                                    TCmds[gantryIdx].Add(tcmd);
                                    Profiles[gantryIdx].Add($"Recipe_Function: [{func.Name}]");
                                }
                                break;
                            }
                    }
                }
                functionNo++;
            }
            #endregion

            #region defaultpump
            switch (pumptype)
            {
                case EPumpType.VERMES_3280:
                    {
                        TCmd cmd = new TCmd(ECmd.VERMES_3280_SETUP);
                        var setup = new Vermes3280_Param(GRecipes.Vermes_Setups[gantryIdx]);
                        Profiles[gantryIdx].Add("Default Vermes_Setup");

                        cmd.Para[0] = setup.RisingTime.Value;
                        cmd.Para[1] = setup.OpenTime.Value;
                        cmd.Para[2] = setup.FallingTime.Value;
                        cmd.Para[3] = setup.NeedleLift.Value;
                        cmd.Para[4] = setup.Pulses.Value;
                        cmd.Para[5] = setup.Delay.Value;
                        cmd.Para[6] = setup.FPress.Value;

                        TCmds[gantryIdx].Add(cmd);
                        break;
                    }
                case EPumpType.SP:
                    {
                        TCmd cmd = new TCmd(ECmd.SP_SETUP);
                        var setup = new SP_Param(GRecipes.SP_Setups[gantryIdx]);
                        Profiles[gantryIdx].Add("Default SP_Setup");

                        cmd.Para[0] = setup.DispTime.Value;
                        cmd.Para[1] = setup.PulseOnDelay.Value;
                        cmd.Para[2] = setup.PulseOffDelay.Value;
                        cmd.Para[6] = setup.FPress.Value;
                        cmd.Para[7] = setup.PPress.Value;
                        cmd.Para[8] = setup.VacDur.Value;

                        TCmds[gantryIdx].Add(cmd);
                        break;
                    }
                case EPumpType.HM:
                    {
                        TCmd cmd = new TCmd(ECmd.HM_SETUP);
                        var setup = new HM_Param(GRecipes.HM_Setups[gantryIdx]);
                        Profiles[gantryIdx].Add("Default HM_Setup");

                        cmd.Para[0] = setup.DispTime.Value;
                        cmd.Para[1] = setup.DispRPM.Value;
                        cmd.Para[2] = setup.BSuckTime.Value;
                        cmd.Para[3] = setup.BSuckRPM.Value;
                        cmd.Para[4] = setup.DispAccel.Value;
                        cmd.Para[5] = setup.BSuckAccel.Value;
                        cmd.Para[6] = setup.FPress.Value;
                        cmd.Para[8] = setup.VacDur.Value;

                        TCmds[gantryIdx].Add(cmd);
                        break;
                    }
            }
            #endregion

            cbxCalProfile.DataSource = Profiles[gantryIdx];

            List<EWeighSVParam> tunevars = new List<EWeighSVParam>();
            switch (pumptype)
            {
                case EPumpType.SP: tunevars.Add(EWeighSVParam.FPressPPress); break;
                case EPumpType.HM: /*tunevars.AddRange(new EWeighSVParam[] { EWeighSVParam.FPress, EWeighSVParam.DispRPM }); break;*/
                case EPumpType.SPLite:
                case EPumpType.TP: tunevars.Add(EWeighSVParam.FPress); break;
                case EPumpType.VERMES_3280:
                    {
                        tunevars.Add(EWeighSVParam.FPress);
                        //tunevars.Add(EWeighSVParam.RisingTime);
                        //tunevars.Add(EWeighSVParam.OpeningTime);
                        //tunevars.Add(EWeighSVParam.FallingTime);
                        //tunevars.Add(EWeighSVParam.NeedleLift);
                        //tunevars.Add(EWeighSVParam.Delay);
                        break;
                    }
            }
            cbxTuneVar.DataSource = tunevars;
        }

        private void UpdateSV()
        {
            if (cbxCalProfile.SelectedIndex < 0) return;
            int profileIdx = cbxCalProfile.SelectedIndex;

            SV = Enumerable.Range(0, 2).Select(x => new List<DPara>().Select(y => new DPara()).ToList()).ToArray();

            var pumptype = GSystemCfg.Pump.Pumps[gantryIdx].PumpType;

            var tcmd = TCmds[gantryIdx][profileIdx];


            switch (pumptype)
            {
                case EPumpType.VERMES_3280:
                    {
                        var setup = new Vermes3280_Param(tcmd.Para[0], tcmd.Para[1], tcmd.Para[2], (int)tcmd.Para[3], (int)tcmd.Para[4], tcmd.Para[5], tcmd.Para[6]);

                        SV[gantryIdx].Add(new DPara(setup.FPress));
                        SV[gantryIdx].Add(new DPara(setup.RisingTime));
                        SV[gantryIdx].Add(new DPara(setup.OpenTime));
                        SV[gantryIdx].Add(new DPara(setup.FallingTime));
                        SV[gantryIdx].Add(new DPara(setup.NeedleLift.Value, EUnit.PERCENTAGE, 0));
                        SV[gantryIdx].Add(new DPara(setup.Delay));
                    }
                    break;

                case EPumpType.HM:
                    {
                        var setup = new HM_Param((int)tcmd.Para[0], tcmd.Para[1], (int)tcmd.Para[2], tcmd.Para[3], tcmd.Para[4], tcmd.Para[5], tcmd.Para[6], (int)tcmd.Para[8]);

                        SV[gantryIdx].Add(new DPara(setup.FPress));
                        SV[gantryIdx].Add(new DPara(setup.DispRPM));
                    }
                    break;

                case EPumpType.TP:
                case EPumpType.SPLite:
                case EPumpType.SP:
                    {
                        var setup = new SP_Param(tcmd.Para[0], tcmd.Para[6], tcmd.Para[7], tcmd.Para[1], tcmd.Para[2], 0, tcmd.Para[8]);

                        SV[gantryIdx].Add(new DPara(setup.FPress));
                    }
                    break;
            }
        }

        private void UpdateDisplay()
        {
            btnPump1.BackColor = gantryIdx is 0 ? Color.Lime : SystemColors.Control;
            btnPump2.BackColor = gantryIdx is 1 ? Color.Lime : SystemColors.Control;
            btnPump1.Text = $"Head 1 ({GSystemCfg.Pump.Pumps[0].PumpType})";
            btnPump2.Text = $"Head 2 ({GSystemCfg.Pump.Pumps[1].PumpType})";
            Gantry = gantryIdx is 0 ? TFGantry.GantrySetup : TFGantry.GantryRight;

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

            chkbxUpdataParaAfterCal.Checked = GProcessPara.Weighing.EnableUpdateTCmd[gantryIdx];

            var svlist = SV[gantryIdx];
            if (svlist.Count > 0)
            {
                var sv = svlist[Math.Max(0, cbxTuneVar.SelectedIndex)];
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
            lblRepeatCount.UpdatePara(GProcessPara.Weighing.RepeatCount[gantryIdx]);

            lblPos.Text = GSetupPara.Weighing.Pos[gantryIdx, (int)GSystemCfg.Pump.Pumps[gantryIdx].PumpType].ToStringForDisplay();

            lblBoardCount.UpdatePara(new IPara("", GSetupPara.Weighing.WeighBoardCount[gantryIdx], 0, 99999, EUnit.COUNT));
            lblResult.UpdatePara(GProcessPara.Weighing.ResultState);

            //Event
            lblCleanAF.UpdatePara(GProcessPara.Weighing.CleanAfterFill);
            lblPurgeAF.UpdatePara(GProcessPara.Weighing.PurgeAfterFill);
            lblFlushAF.UpdatePara(GProcessPara.Weighing.FlushAfterFill);

            groupBox11.Controls.Clear();
            if (TCmds[gantryIdx].Count > 0)
            {
                frmRecipeCommonPara frm = new frmRecipeCommonPara(Gantry, TCmds[gantryIdx][cbxCalProfile.SelectedIndex], $"{Profiles[gantryIdx][cbxCalProfile.SelectedIndex]}");
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Dock = DockStyle.Fill;
                frm.TopMost = frm.TopLevel = false;
                frm.Parent = groupBox11;
                frm.Show();
            }

            //UI Control
            GControl.UpdateFormControl(this);

            lblDotPerSampleCal.Visible = cbxWeighType.SelectedIndex is 0;
            lblMdotDispTime.Visible = cbxWeighType.SelectedIndex is 1;

            gbxMass.Visible = cbxWeighType.SelectedIndex is 0;
            gbxMassFlowRate.Visible = cbxWeighType.SelectedIndex is 1;

            groupBox11.Controls.OfType<Form>().ToList().ForEach(x => x.Enabled = false);

            lblActualFR.Enabled = false;

            //Calculator

            lblCalFlowRate.Enabled = false;
            lblCalSpeed.Enabled = false;

            lblCalLineDistInput.UpdatePara(CalLineDist);
            lblCalSpeedInput.UpdatePara(CalSpeed);
            lblCalTargetMassInput.UpdatePara(CalTargetMass);
            lblCalFlowRateInput.UpdatePara(CalFlowRate);

            OutputSpeed.Value = new TFunction().WeighReturnVelocity(CalLineDist.Value, CalTargetMass.Value, CalFlowRate.Value);
            lblCalSpeed.UpdatePara(OutputSpeed);

            OutputFlowRate.Value = new TFunction().WeighReturnFlowRate(CalLineDist.Value, CalTargetMass.Value, CalSpeed.Value);
            lblCalFlowRate.UpdatePara(OutputFlowRate);

            cbxTuneVar.Enabled = GSystemCfg.Pump.Pumps[0].PumpType != EPumpType.HM;

            #region Cut
            cbxCutFunc.Checked = GProcessPara.Weighing.CutTailEnable;
            pnlCutFunction.Visible = GProcessPara.Weighing.CutTailEnable;
            lblEndPos.Text = GSetupPara.Weighing.EndPos[gantryIdx, (int)GSystemCfg.Pump.Pumps[gantryIdx].PumpType].ToStringForDisplay();

            lblSpeed.UpdatePara(GProcessPara.Weighing.XYCutSpeed);
            lblZStepCount.UpdatePara(GProcessPara.Weighing.ZStepCount);
            lblZStepDist.UpdatePara(GProcessPara.Weighing.ZStepDist);
            #endregion
        }

        private void UpdateChart(BindingList<TEWeighData> result)
        {
            chartWeigh.Series.Clear();

            var mode = result[0].EWeighCalMode;

            Series series = new Series(mode == EWeighType.Mass ? "Mass[mg]" : "MassFlowrate[mg/s]");
            series.ChartType = seriesChartType;
            series.BorderWidth = 2;
            series.Color = Color.Blue;
            series.Points.DataBindXY(Enumerable.Range(1, result.Count).ToArray(), result.Select(x => mode == EWeighType.Mass ? x.ActualMass : x.FlowRate).ToArray());
            chartWeigh.Series.Add(series);

            var ca = chartWeigh.ChartAreas[0];
            ca.AxisX.Title = "Count";
            ca.AxisY.Title = series.LegendText;

            ca.AxisX.Minimum = 1;
            ca.AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            ca.AxisY.Minimum = 0;
            ca.AxisX.Maximum = result.Count;
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
            Gantry.MoveOpZAbs(0);
        }

        private void btnGoto_Click(object sender, EventArgs e)
        {
            var anotherG = gantryIdx is 0 ? TFGantry.GantryRight : TFGantry.GantrySetup;
            anotherG.GotoXYZ(new PointXYZ());
            Gantry.GotoXYZ(GSetupPara.Weighing.Pos[gantryIdx, (int)GSystemCfg.Pump.Pumps[gantryIdx].PumpType], true);
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
        private void lblRepeatCount_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Weighing.RepeatCount[gantryIdx]);
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

        private void lblResult_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Weighing.ResultState);
            UpdateDisplay();
        }
        #endregion

        #region Func
        private void btnExecCal_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbxCalProfile.SelectedIndex < 0)
                {
                    MsgBox.ShowDialog("Select a Profile before execution.");
                    return;
                }
                tabControl1.SelectTab(ResultTab);
                richTextBox1.Clear();
                chartWeigh.Series.Clear();
                lblCpk.Text = "";

                (gantryIdx is 0 ? TFGantry.GantryRight : TFGantry.GantrySetup).GotoXYZ(new PointXYZ());

                var wmode = (EWeighType)cbxWeighType.SelectedItem;
                var wtype = (EWeighMode)cbxWeighMode.SelectedItem;
                var wtunesv = (EWeighSVParam)cbxTuneVar.SelectedItem;

                var tcmd = TCmds[gantryIdx][cbxCalProfile.SelectedIndex];
                var result = TCWeighFunc.WeighCals[gantryIdx].Result;

                string s = wmode == EWeighType.Mass ? $"DotPerSample {GProcessPara.Weighing.DotPerSample[gantryIdx].Value}" : $"DispTime {GProcessPara.Weighing.DispTime_DotM.Value}";
                UpdateResultinRTB($"Head{gantryIdx + 1}/{GSystemCfg.Pump.Pumps[gantryIdx].PumpType}/SampleCount {GProcessPara.Weighing.SampleCount[gantryIdx].Value}/{s}/{wtype}/{wmode}/Start");
                result.ListChanged += Result_ListChanged;
                tabControl1.SelectTab(ResultTab);

                GControl.UI_Disable();
                MsgBox.Processing($"Weigh Calibration Head {gantryIdx + 1}", () => TCWeighFunc.WeighCals[gantryIdx].Execute(0, 0, wmode, wtype, ref tcmd, wtunesv), () => TCWeighFunc.WeighCals[gantryIdx].Stop = true);
                GControl.UI_Enable();

                result.ListChanged -= Result_ListChanged;
                string word = TCWeighFunc.WeighCals[gantryIdx].Finish ? "Success" : "Fail";
                UpdateResultinRTB($"Head{gantryIdx + 1}/{wtype}/{wmode}/{word}\n");

                UpdateResultinRTB($"{nameof(Variance)}\t{Variance(result.Select(x => wmode == EWeighType.Mass ? x.ActualMass : x.FlowRate).ToList())}");
                UpdateResultinRTB($"{nameof(StandardDeviation)}\t{StandardDeviation(result.Select(x => wmode == EWeighType.Mass ? x.ActualMass : x.FlowRate).ToList())}");

                //default
                if (cbxCalProfile.Items.Count is 1)
                {
                    switch (GSystemCfg.Pump.Pumps[gantryIdx].PumpType)
                    {
                        case EPumpType.VERMES_3280: GRecipes.Vermes_Setups[gantryIdx] = new Vermes3280_Param(tcmd.Para); break;
                        case EPumpType.SP:
                        case EPumpType.SPLite: GRecipes.SP_Setups[gantryIdx] = new SP_Param(tcmd.Para); break;
                        case EPumpType.HM: GRecipes.HM_Setups[gantryIdx] = new HM_Param(tcmd.Para); break;
                    }
                }

                UpdateProfile();
                UpdateDisplay();
                lblCpk.Text = TCWeighFunc.WeighCals[gantryIdx].UpdateCPK(wmode, wtype);
                               
                var format = ChartImageFormat.Png;
                chartWeigh.SaveImage(GDoc.WeighDataDateTimeDir.FullName + DateTime.Now.ToString("yyyyMMddTHHmmss") + $"chart.{format.ToString().ToLower()}", format);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Result_ListChanged(object sender, ListChangedEventArgs e)
        {
            var idx = e.NewIndex;
            if (idx < 0) return;

            UpdateResultinRTB($"{idx + 1}: {TCWeighFunc.WeighCals[gantryIdx].Result[idx]}");
            this.Invoke(new Action(() => UpdateChart(TCWeighFunc.WeighCals[gantryIdx].Result)));
        }

        private void UpdateResultinRTB(string msg)
        {
            this.richTextBox1.Invoke(new Action(() =>
            {
                richTextBox1.AppendText($"{DateTime.Now}\t{msg}{Environment.NewLine}");
                richTextBox1.ScrollToCaret();
            }));
            GLog.WriteLog(ELogType.PROCESS, msg, GDoc.WeighDataFile.FullName);
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

        private void cbxCalProfile_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateSV();
            UpdateDisplay();
        }
        private void cbxWeighType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateDisplay();
        }
        private void cbxWeighMode_SelectionChangeCommitted(object sender, EventArgs e)
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
            GProcessPara.Weighing.EnableUpdateTCmd[gantryIdx] = !GProcessPara.Weighing.EnableUpdateTCmd[gantryIdx];
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

        private void cbxTuneVar_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void btnBoardCountReset_Click(object sender, EventArgs e)
        {
            GSetupPara.Weighing.WeighBoardCount[gantryIdx] = 0;
            UpdateDisplay();
        }

        #region Calculator
        static DPara CalTargetMass = new DPara(nameof(CalTargetMass), 0, 0, 1000, EUnit.MILLIGRAM);
        static DPara CalLineDist = new DPara(nameof(CalLineDist), 0, 0, 1000, EUnit.MILLIMETER);
        static DPara CalFlowRate = new DPara(nameof(CalFlowRate), 0, 0, 1000, EUnit.MASS_FLOW_RATE);
        static DPara CalSpeed = new DPara(nameof(CalSpeed), 0, 0, 1000, EUnit.MILLIMETER_PER_SECOND);

        static DPara OutputSpeed = new DPara(nameof(OutputSpeed), 0, 0, 1000, EUnit.MILLIMETER_PER_SECOND);
        static DPara OutputFlowRate = new DPara(nameof(OutputFlowRate), 0, 0, 1000, EUnit.MASS_FLOW_RATE);

        private void btnStaticFRExplain_Click(object sender, EventArgs e)
        {
            string msg = $"mdot = mg/t (1)\nv = s/t (2)\n(1) into (2)\nv = s/(mg/mdot)\nv = s*mdot/mg";
            MsgBox.ShowDialog(msg);
        }
        private void btnAdaptiveFRExplain_Click(object sender, EventArgs e)
        {
            string msg = "mdot = mg/t (1)\nv = s/t (2)\n(2) into (1)\nmdot = mg/(s/v)\nmdot = mg*v/s";
            MsgBox.ShowDialog(msg);
        }

        private void lblCalFlowRateInput_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref CalFlowRate);
            UpdateDisplay();
        }
        private void lblCalSpeedInput_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref CalSpeed);
            UpdateDisplay();
        }
        private void lblCalTargetMassInput_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref CalTargetMass);
            UpdateDisplay();
        }
        private void lblCalLineDistInput_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref CalLineDist);
            UpdateDisplay();
        }

        #endregion

        private double Variance(List<double> values)
        {
            if (values is null || values.Count is 0) return 0;

            double mean = values.Sum() / values.Count;
            var variance = values.Select(x => Math.Pow(x - mean, 2)).Sum() / values.Count;
            return variance;
        }
        private double StandardDeviation(List<double> values)
        {
            var v = Variance(values);
            return v <= 0 ? 0 : Math.Sqrt(v);
        }

        #region Cut
        private void cbxCutFunc_CheckedChanged(object sender, EventArgs e)
        {
            GProcessPara.Weighing.CutTailEnable = cbxCutFunc.Checked;
            UpdateDisplay();
        }
        private void btnSet2_Click(object sender, EventArgs e)
        {
            GLog.SetPointD(ref GSetupPara.Weighing.EndPos[gantryIdx, (int)GSystemCfg.Pump.Pumps[gantryIdx].PumpType], Gantry.PointXY, "Weigh Pos");
            UpdateDisplay();
            Gantry.MoveOpZAbs(0);
        }
        private void btnGoto2_Click(object sender, EventArgs e)
        {
            var anotherG = gantryIdx is 0 ? TFGantry.GantryRight : TFGantry.GantrySetup;
            anotherG.GotoXYZ(new PointXYZ());
            Gantry.MoveOpXYAbs(GSetupPara.Weighing.EndPos[gantryIdx, (int)GSystemCfg.Pump.Pumps[gantryIdx].PumpType].ToArray, true);
        }
        private void lblSpeed_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Weighing.XYCutSpeed);
            UpdateDisplay();
        }
        private void lblZStepCount_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Weighing.ZStepCount);
            UpdateDisplay();
        }
        private void lblZStepDist_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Weighing.ZStepDist);
            UpdateDisplay();
        }
        #endregion

    }
}
