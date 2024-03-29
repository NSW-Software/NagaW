﻿using Emgu.CV.Aruco;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NagaW
{
    public partial class frmRecipeFunction : Form
    {
        TEZMCAux.TGroup gantry;

        TFunction Function = new TFunction();
        public frmRecipeFunction()
        {
            InitializeComponent();
        }
        public frmRecipeFunction(TEZMCAux.TGroup gantry) : this()
        {
            this.gantry = gantry;
        }
        private void frmRecipe_Load(object sender, EventArgs e)
        {
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            //dgv.RowHeadersWidth = 40;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToResizeRows = false;
            dgv.RowHeadersVisible = false;

            dgv.CellContentClick += (a, b) => dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);

            lboxFuncList.DataSource = GRecipes.Functions[gantry.Index];
            cbxCommand.DataSource = GRecipes.CmdsDictionary;
            UpdateDisplay();

            this.Enter += (a, b) => UpdateDisplay();

            GControl.LogForm(this);
        }

        public void UpdateDisplay()
        {
            if (GRecipes.IsLoading) return;

            Function = GRecipes.Functions[gantry.Index].Count is 0 ? Function : GRecipes.Functions[gantry.Index][Math.Max(lboxFuncList.SelectedIndex, 0)];

            tbxFuncName.Text = Function.Name;
            cbxFuncLayout.Items.Clear();
            cbxFuncLayout.Items.AddRange(GRecipes.MultiLayout[gantry.Index].ToArray());
            cbxFuncLayout.SelectedIndex = Function.LayoutNo;

            lblDispGap.Text = new DPara(Function.DispGap, EUnit.MILLIMETER).ToStringForDisplay();
            lblDnWait.Text = new DPara(Function.DnWait, EUnit.MILLISECOND, 0).ToStringForDisplay();
            lblRetGap.Text = new DPara(Function.RetGap, EUnit.MILLIMETER).ToStringForDisplay();
            lblMoveSpeed.Text = new DPara(Function.MoveSpeed, EUnit.MILLIMETER_PER_SECOND, 1).ToStringForDisplay();
            lblMoveAccel.Text = new DPara(Function.MoveAccel, EUnit.MILLIMETER_PER_SECOND_SQUARED, 1).ToStringForDisplay();

            lblDotTime.Text = new DPara(Function.DotTime, EUnit.MILLISECOND, 0).ToStringForDisplay();
            lblDotWait.Text = new DPara(Function.DotWait, EUnit.MILLISECOND, 0).ToStringForDisplay();

            lblLineSDelay.Text = new DPara(Function.LineSDelay, EUnit.MILLISECOND, 0).ToStringForDisplay();
            lblLineSpeed.Text = new DPara(Function.LineSpeed, EUnit.MILLISECOND, 1).ToStringForDisplay();
            lblLineEDelay.Text = new DPara(Function.LineEDelay, EUnit.MILLISECOND, 0).ToStringForDisplay();
            lblLineWait.Text = new DPara(Function.LineWait, EUnit.MILLISECOND, 0).ToStringForDisplay();

            GControl.UpdateFormControl(this);

            tabControl1.Visible = GRecipes.Functions[gantry.Index].Count > 0;
        }

        #region Functions
        private void lboxFuncList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Inst.Board[gantry.Index].tempFuncNo = lboxFuncList.SelectedIndex;

            UpdateDisplay();
            formList.ForEach(f => f.Close());
            UpdateGrid();
        }

        private void btnFuncAdd_Click(object sender, EventArgs e)
        {
            GRecipes.Functions[gantry.Index].Add(new TFunction(gantry.Index) { Name = "Function" + (GRecipes.Functions[gantry.Index].Count + 1).ToString("d2") });
            GRecipes.Functions[gantry.Index].ResetBindings();
            UpdateDisplay();
            UpdateGrid();

        }
        private void btnFuncRemove_Click(object sender, EventArgs e)
        {
            var idx = lboxFuncList.SelectedIndex;
            if (idx == -1) return;

            if (MsgBox.ShowDialog($"Remove Function\r\n{GRecipes.Functions[gantry.Index][idx]}?", MsgBoxBtns.OKCancel) != DialogResult.OK) return;

            GRecipes.Functions[gantry.Index].RemoveAt(idx);
            GRecipes.Functions[gantry.Index].ResetBindings();
            UpdateDisplay();
            UpdateGrid();
        }
        private void btnFunUp_Click(object sender, EventArgs e)
        {
            var idx = lboxFuncList.SelectedIndex;
            if (idx == -1) return;
            GRecipes.Functions[gantry.Index].MoveUp(idx);

            lboxFuncList.SelectedIndex = Math.Max(0, idx - 1);
            GRecipes.Functions[gantry.Index].ResetBindings();
            UpdateDisplay();
        }
        private void btnFuncDn_Click(object sender, EventArgs e)
        {
            var idx = lboxFuncList.SelectedIndex;
            if (idx == -1) return;
            GRecipes.Functions[gantry.Index].MoveDown(idx);

            lboxFuncList.SelectedIndex = Math.Min(GRecipes.Functions[gantry.Index].Count - 1, idx + 1);
            GRecipes.Functions[gantry.Index].ResetBindings();
            UpdateDisplay();
        }
        #endregion

        #region Properties
        private void tbxFuncName_TextChanged(object sender, EventArgs e)
        {
            Function.Name = tbxFuncName.Text;
            UpdateDisplay();
            GRecipes.Functions[gantry.Index].ResetBindings();
        }
        private void cbxFuncLayout_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Function.LayoutNo = cbxFuncLayout.SelectedIndex;
            UpdateDisplay();
        }
        private void lblDispGap_Click(object sender, EventArgs e)
        {
            DPara para = new DPara(Function.Name + " Disp Gap", Function.DispGap, 0, 10, EUnit.MILLIMETER);
            if (!GLog.SetPara(ref para)) return;
            Function.DispGap = para.Value;
            UpdateDisplay();
        }
        private void lblDnWait_Click(object sender, EventArgs e)
        {
            DPara para = new DPara(Function.Name + " Dn Wait", Function.DnWait, 0, 10000, EUnit.MILLISECOND);
            if (!GLog.SetPara(ref para)) return;
            Function.DnWait = para.Value;
            UpdateDisplay();
        }
        private void lblRetGap_Click(object sender, EventArgs e)
        {
            DPara para = new DPara(Function.Name + " Ret Gap", Function.RetGap, 0, 10, EUnit.MILLIMETER);
            if (!GLog.SetPara(ref para)) return;
            Function.RetGap = para.Value;
            UpdateDisplay();
        }
        private void lblMoveSpeed_Click(object sender, EventArgs e)
        {
            DPara para = new DPara(Function.Name + " Move Speed", Function.MoveSpeed, 0, 500, EUnit.MILLIMETER_PER_SECOND);
            if (!GLog.SetPara(ref para)) return;
            Function.MoveSpeed = para.Value;

            UpdateDisplay();
        }
        private void lblMoveAccel_Click(object sender, EventArgs e)
        {
            DPara para = new DPara(Function.Name + " Move Accel", Function.MoveAccel, 0, 2500, EUnit.MILLIMETER_PER_SECOND);
            if (!GLog.SetPara(ref para)) return;
            Function.MoveAccel = para.Value;
            UpdateDisplay();
        }

        private void lblDotTime_Click(object sender, EventArgs e)
        {
            DPara para = new DPara(Function.Name + " Dot Time", Function.DotTime, 0, 10000, EUnit.MILLISECOND);
            if (!GLog.SetPara(ref para)) return;
            Function.DotTime = para.Value;
            UpdateDisplay();
        }
        private void lblDotWait_Click(object sender, EventArgs e)
        {
            DPara para = new DPara(Function.Name + " Dot Wait", Function.DotWait, 0, 10000, EUnit.MILLISECOND);
            if (!GLog.SetPara(ref para)) return;
            Function.DotWait = para.Value;
            UpdateDisplay();
        }

        private void lblLineSDelay_Click(object sender, EventArgs e)
        {
            DPara para = new DPara(Function.Name + " Line Start Delay", Function.LineSDelay, 0, 10000, EUnit.MILLISECOND);
            if (!GLog.SetPara(ref para)) return;
            Function.LineSDelay = para.Value;
            UpdateDisplay();
        }
        private void lblLineSpeed_Click(object sender, EventArgs e)
        {
            DPara para = new DPara(Function.Name + " Line Speed", Function.LineSpeed, 0, 100, EUnit.MILLIMETER_PER_SECOND);
            if (!GLog.SetPara(ref para)) return;
            Function.LineSpeed = para.Value;
            UpdateDisplay();
        }
        private void lblLineEDelay_Click(object sender, EventArgs e)
        {
            DPara para = new DPara(Function.Name + " Line End Delay", Function.LineEDelay, 0, 10000, EUnit.MILLISECOND);
            if (!GLog.SetPara(ref para)) return;
            Function.LineEDelay = para.Value;
            UpdateDisplay();
        }
        private void lblLineWait_Click(object sender, EventArgs e)
        {
            DPara para = new DPara(Function.Name + " Line Wait", Function.LineWait, 0, 10000, EUnit.MILLISECOND);
            if (!GLog.SetPara(ref para)) return;
            Function.LineWait = para.Value;
            UpdateDisplay();
        }
        #endregion

        #region Commands
        public void UpdateGrid()
        {
            if (GRecipes.IsLoading) return;
            dgv.ScrollBars = ScrollBars.Vertical;
            dgv.MultiSelect = false;
            dgv.ReadOnly = false;

            dgv.DataSource = null;
            dgv.DataSource = Function.Cmds;

            if (dgv.ColumnCount != 0)
            {
                dgv.Columns[0].ReadOnly = true;
                dgv.Columns[1].ReadOnly = true;
                dgv.Columns[2].ReadOnly = true;
                dgv.Columns[3].ReadOnly = false;
            }

            if (dgv.Columns.Count is 0) return;
            dgv.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            RefreshGrid();
        }
        private void cbxCommand_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ECmd cmd;
            Enum.TryParse(cbxCommand.SelectedItem.ToString(), out cmd);

            if (cmd > ECmd.NONE)
            {
                TCmd tcmd = new TCmd(cmd);

                switch (tcmd.Cmd)
                {
                    case ECmd.SPLITE_SETUP:
                    case ECmd.SP_SETUP:
                    case ECmd.TP_SETUP:
                        tcmd.Para[0] = GRecipes.SP_Setups[gantry.Index].DispTime.Value;
                        tcmd.Para[1] = GRecipes.SP_Setups[gantry.Index].PulseOnDelay.Value;
                        tcmd.Para[2] = GRecipes.SP_Setups[gantry.Index].PulseOffDelay.Value;
                        tcmd.Para[6] = GRecipes.SP_Setups[gantry.Index].FPress.Value;
                        tcmd.Para[7] = GRecipes.SP_Setups[gantry.Index].PPress.Value;
                        tcmd.Para[8] = GRecipes.SP_Setups[gantry.Index].VacDur.Value;
                        break;
                    case ECmd.DOWN_SETUP:
                        tcmd.Para[0] = Function.DispGap;
                        tcmd.Para[1] = Function.MoveSpeed;
                        tcmd.Para[2] = Function.DnWait;
                        break;
                    case ECmd.RET_SETUP:
                        tcmd.Para[0] = Function.RetGap;
                        tcmd.Para[1] = Function.MoveSpeed;
                        tcmd.Para[2] = 0;
                        break;
                    case ECmd.UP_SETUP:
                        tcmd.Para[0] = Function.RetGap;
                        tcmd.Para[1] = Function.MoveSpeed;
                        tcmd.Para[2] = 0;
                        break;
                    case ECmd.DOT_SETUP:
                        tcmd.Para[0] = Function.DotTime;
                        tcmd.Para[1] = Function.DotWait;
                        break;
                    case ECmd.LINE_SETUP:
                        tcmd.Para[0] = Function.LineSDelay;
                        tcmd.Para[1] = Function.LineSpeed;
                        tcmd.Para[2] = Function.LineEDelay;
                        tcmd.Para[3] = Function.LineWait;
                        break;
                    case ECmd.CUT_TAIL_SETUP:
                        tcmd.Para[0] = 1;
                        tcmd.Para[1] = 0.5;
                        tcmd.Para[2] = 5;
                        break;
                    case ECmd.PAT_ALIGN_UNIT:
                    case ECmd.PAT_ALIGN_CLUSTER:
                    case ECmd.PAT_ALIGN_BOARD:
                        tcmd.Para[6] = 0.75;//MinScore
                        tcmd.Para[7] = 1;//MaxOffset
                        tcmd.Para[8] = 5;//Max Angle
                        break;
                    case ECmd.PAT_ALIGN_SETUP:
                        tcmd.Para[0] = GProcessPara.Vision.SettleTime.Value;
                        tcmd.Para[1] = 0;
                        break;
                    case ECmd.HEIGHT_SETUP:
                        tcmd.Para[0] = GProcessPara.HSensor.SettleTime.Value;
                        break;
                    case ECmd.NEEDLE_VAC_CLEAN:
                        tcmd.Para[0] = GProcessPara.NeedleVacClean.Count[gantry.Index].Value;
                        tcmd.Para[1] = 1;//perunit;
                        tcmd.Para[3] = GProcessPara.NeedleVacClean.DownWait[gantry.Index].Value;
                        tcmd.Para[4] = GProcessPara.NeedleVacClean.DispTime[gantry.Index].Value;
                        tcmd.Para[5] = GProcessPara.NeedleVacClean.VacTime[gantry.Index].Value;
                        tcmd.Para[6] = GProcessPara.NeedleVacClean.PostVacTime[gantry.Index].Value;
                        tcmd.Para[7] = GProcessPara.NeedleVacClean.PostWait[gantry.Index].Value;
                        break;
                    case ECmd.NEEDLE_PURGE:
                        tcmd.Para[0] = GProcessPara.NeedlePurge.Count[gantry.Index].Value;
                        tcmd.Para[1] = 1;//perunit;
                        tcmd.Para[3] = GProcessPara.NeedlePurge.DownWait[gantry.Index].Value;
                        tcmd.Para[4] = GProcessPara.NeedlePurge.DispTime[gantry.Index].Value;
                        tcmd.Para[5] = GProcessPara.NeedlePurge.VacTime[gantry.Index].Value;
                        tcmd.Para[6] = GProcessPara.NeedlePurge.PostVacTime[gantry.Index].Value;
                        tcmd.Para[7] = GProcessPara.NeedlePurge.PostWait[gantry.Index].Value;
                        break;
                    case ECmd.NEEDLE_FLUSH:
                        tcmd.Para[0] = GProcessPara.NeedlePurge.Count[gantry.Index].Value;
                        tcmd.Para[1] = 1;//perunit;       
                        tcmd.Para[3] = GProcessPara.NeedlePurge.DownWait[gantry.Index].Value;
                        tcmd.Para[4] = GProcessPara.NeedlePurge.DispTime[gantry.Index].Value;
                        tcmd.Para[5] = GProcessPara.NeedlePurge.VacTime[gantry.Index].Value;
                        tcmd.Para[6] = GProcessPara.NeedlePurge.PostVacTime[gantry.Index].Value;
                        tcmd.Para[7] = GProcessPara.NeedlePurge.PostWait[gantry.Index].Value;
                        break;
                    case ECmd.DYNAMIC_SAVE_IMAGE:
                        tcmd.Para[6] = 50;//Speed
                        break;
                    case ECmd.LINE_SPEED:
                        tcmd.Para[0] = Function.LineSpeed;
                        break;
                }

                if (cbInsert.Checked)
                {
                    if (dgv.CurrentCell is null) return;
                    var i = dgv.CurrentCell.RowIndex;
                    if (i < dgv.RowCount) Function.Cmds.Insert(i, tcmd);
                    else Function.Cmds.Add(tcmd);
                }
                else
                {
                    Function.Cmds.Add(tcmd);
                }
                RefreshGrid();
            }
            cbxCommand.SelectedIndex = 0;
        }
        private void btnUp_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell is null) return;
            var i = dgv.CurrentCell.RowIndex;
            if (i <= 0) return;
            Function.Cmds.MoveUp(i);
            dgv.CurrentCell = dgv.Rows[i - 1].Cells[0];
            RefreshGrid();
            formList.ForEach(f => f.Close());
        }
        private void btnDn_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell is null) return;
            var i = dgv.CurrentCell.RowIndex;
            if (i >= Function.Cmds.Count - 1) return;
            Function.Cmds.MoveDown(i);
            dgv.CurrentCell = dgv.Rows[i + 1].Cells[0];
            RefreshGrid();
            formList.ForEach(f => f.Close());
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell is null) return;
            var i = dgv.CurrentCell.RowIndex;
            Function.Cmds.RemoveAt(i);
            RefreshGrid();
            formList.ForEach(f => f.Close());
        }

        public List<Form> formList = new List<Form>();
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            void showForm(Form frm, bool enabled = false)
            {
                formList.ForEach(f => f.Close());
                if (frm == null) return;

                formList.Add(frm);
                frm.TopMost = false;
                frm.TopLevel = false;
                frm.Parent = pnlEditor;
                frm.Dock = DockStyle.Fill;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Enabled = enabled;

                frm.Show();
                frm.BringToFront();
                frm.AutoScroll = true;

                //****
                //frm.AutoSize = true;
                //frm.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                //frm.TopMost = true;
                //frm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                //frm.TopLevel = true;
                //frm.Shown += (a, b) => frm.Location = new Point(dgv.Location.X + dgv.Width, Cursor.Position.Y);
                //frm.LostFocus += (a,b)=>frm.Close();

                //frm.FormClosed += (a, b) => RefreshGrid();
                GControl.GetChildItems(frm).ToList().ForEach(x => x.Click += (a, b) => RefreshGrid());
            }

            var cmdIdx = e.RowIndex;
            if (cmdIdx < 0) return;


            int funcNo = lboxFuncList.SelectedIndex;
            int layoutNo = GRecipes.Functions[gantry.Index][funcNo].LayoutNo;
            var tcmd = GRecipes.Functions[gantry.Index][funcNo].Cmds[cmdIdx];

            PointD layoutRel = GRecipes.MultiLayout[gantry.Index][layoutNo].Cluster.RelPos(Inst.Board[gantry.Index].ClusterCR);
            if (tcmd.Cmd == ECmd.HEIGHT_ALIGN_BOARD || tcmd.Cmd == ECmd.PAT_ALIGN_BOARD || tcmd.Cmd == ECmd.PAT_ALIGN_ROTARY) layoutRel = new PointD();//Ignore relative cluster

            PointD unitRel = GRecipes.MultiLayout[gantry.Index][layoutNo].Unit.RelPos(Inst.Board[gantry.Index].UnitCR);
            if (tcmd.Cmd == ECmd.HEIGHT_ALIGN_BOARD || tcmd.Cmd == ECmd.PAT_ALIGN_BOARD || tcmd.Cmd == ECmd.PAT_ALIGN_ROTARY || tcmd.Cmd == ECmd.HEIGHT_ALIGN_CLUSTER || tcmd.Cmd == ECmd.PAT_ALIGN_CLUSTER) unitRel = new PointD();//Ignore relative unit

            PointD origin = GRecipes.Board[gantry.Index].StartPos.GetPointD() + layoutRel + unitRel;

            if (e.Button != MouseButtons.Left) return;

            //var tcmd = GRecipes.Functions[gantry.Index][funcNo].Cmds[cmdIdx];

            //string title = "F" + GRecipes.Functions[gantry.Index][funcNo].Index.ToString("d3") + " C" + cmdIdx.ToString("d3");
            string title = GRecipes.Functions[gantry.Index][funcNo] + " C" + cmdIdx.ToString("d3");

            bool show = true;
            bool edit = false;

            switch (dgv.Columns[e.ColumnIndex].HeaderText)
            {
                case nameof(TCmd.Param_Edit): edit = true; break;
            }

            switch (dgv[Math.Max(0, e.ColumnIndex), e.RowIndex].Value.ToString())
            {
                case "[Goto]":
                    {
                        TFLightCtrl.LightPair[gantry.Index].Set(GRecipes.Board[gantry.Index].LightDefault);

                        origin.X += tcmd.Para[0];
                        origin.Y += tcmd.Para[1];

                        if (gantry.ZAxis.ActualPos < 5)//if z is low, move up
                            gantry.MoveOpZAbs(GRecipes.CamFocusNo[TFGantry.GantrySelect.Index][0]);

                        gantry.MoveOpXYAbs(origin.ToArray);
                        break;
                    }
                    //case "[Edit]": edit = true; break;
                    //default: edit = false; break;
            }
            if (show)
            {
                switch (tcmd.Cmd)
                {
                    case ECmd.CHECK_INPUT:
                    case ECmd.SPEED:
                    case ECmd.ADJUST_OFFSET:

                    case ECmd.VERMES_3280_SETUP:
                    case ECmd.SP_SETUP:
                    case ECmd.SPLITE_SETUP:
                    case ECmd.TP_SETUP:
                    case ECmd.HM_SETUP:
                    case ECmd.PNEUMATICS_JET_SETUP:
                    case ECmd.PATTERN_SETUP:
                    //case ECmd.TEMP_SETUP:
                    //case ECmd.PRESSURE_MONITORING:

                    //case ECmd.PRESSURE_MASTER_SETUP:
                    case ECmd.DOWN_SETUP:
                    case ECmd.RET_SETUP:
                    case ECmd.UP_SETUP:
                    case ECmd.DOT_SETUP:
                    case ECmd.LINE_SETUP:
                    case ECmd.CUT_TAIL_SETUP:
                    case ECmd.DYNAMIC_JET_SETUP:
                    case ECmd.CLUSTER_GAP_SETUP:
                    case ECmd.PAT_ALIGN_SETUP:
                    case ECmd.HEIGHT_SETUP:
                    case ECmd.NEEDLE_VAC_CLEAN:
                    case ECmd.NEEDLE_FLUSH:
                    case ECmd.NEEDLE_PURGE:
                    case ECmd.NEEDLE_AB_CLEAN:
                    case ECmd.NEEDLE_SPRAY_CLEAN:
                    case ECmd.PURGE_STAGE:
                    case ECmd.LINE_SPEED:
                    case ECmd.LINE_GAP_ADJUST:
                    case ECmd.NOZZLE_INSPECTION:
                    case ECmd.DO_WEIGH:
                    case ECmd.USE_WEIGH:
                    case ECmd.GOTO_POSITION:
                        {
                            showForm(new frmRecipeCommonPara(gantry, tcmd, title), edit); break;
                        }
                    case ECmd.DOT:
                    case ECmd.LINE_START:
                    case ECmd.LINE_PASS:
                    case ECmd.LINE_END:
                    case ECmd.CIRC_CENTER:
                    case ECmd.CIRC_PASS:
                    case ECmd.ARC_PASS:
                    case ECmd.DYNAMIC_SAVE_IMAGE:
                    case ECmd.DYNAMIC_JET_DOT:
                    case ECmd.DYNAMIC_JET_DOT_SW:
                        {
                            showForm(new frmRecipeXYZ(gantry, origin, tcmd, title), edit); break;
                        }
                    case ECmd.PAT_ALIGN_UNIT:
                        {
                            showForm(new frmRecipePatAlign(gantry, origin, tcmd, cmdIdx), edit); break;
                        }
                    case ECmd.PAT_ALIGN_ROTARY:
                    case ECmd.PAT_ALIGN_CLUSTER:
                    case ECmd.PAT_ALIGN_BOARD:
                        {
                            PointD clusterOrigin = GRecipes.Board[gantry.Index].StartPos.GetPointD() + layoutRel;
                            showForm(new frmRecipePatAlign(gantry, clusterOrigin, tcmd, cmdIdx), edit); break;
                        }
                    case ECmd.HEIGHT_ALIGN_UNIT:
                        {
                            showForm(new frmRecipeHeightAlign(gantry, origin, tcmd, cmdIdx), edit); break;
                        }
                    case ECmd.HEIGHT_ALIGN_CLUSTER:
                        {
                            PointD clusterOrigin = GRecipes.Board[gantry.Index].StartPos.GetPointD() + layoutRel;
                            showForm(new frmRecipeHeightAlign(gantry, clusterOrigin, tcmd, cmdIdx), edit); break;
                        }
                    case ECmd.HEIGHT_ALIGN_BOARD:
                        {
                            PointD bdOrigin = GRecipes.Board[gantry.Index].StartPos.GetPointD();
                            showForm(new frmRecipeHeightAlign(gantry, bdOrigin, tcmd, cmdIdx), edit); break;
                        }
                    case ECmd.PATTERN_MULTIDOT:
                    case ECmd.PATTERN_CROSS:
                    case ECmd.PATTERN_STAR:
                    case ECmd.PATTERN_SPIRAL:
                    case ECmd.PATTERN_RECT_FILL:
                    case ECmd.PATTERN_RECT_SPIRAL:
                        {
                            showForm(new frmRecipeXYZ_Multi(gantry, origin, tcmd, title), edit); break;
                        }
                    default:
                        {
                            showForm(null);
                            break;
                        }
                }
            }
        }
        private void dgv_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var colIdx = e.ColumnIndex;
            if (colIdx != 3) return;

            int funcNo = lboxFuncList.SelectedIndex;

            foreach (var c in GRecipes.Functions[gantry.Index][funcNo].Cmds) c.Enable = !c.Enable;

            UpdateGrid();
        }

        BindingList<TCmd> cmd = new BindingList<TCmd>();
        private void dgv_MouseDown(object sender, MouseEventArgs e)
        {
            var funcNo = lboxFuncList.SelectedIndex;
            if (GRecipes.Functions[gantry.Index][lboxFuncList.SelectedIndex].Cmds.Count is 0) return;
            switch (e.Button)
            {
                default: return;
                case MouseButtons.Right:
                    {
                        const string copy = "Copy";
                        const string copy_Row = "Copy From This Row";
                        const string paste = "Paste";
                        const string paste_Insert = "Paste (Insert)";
                        ContextMenuStrip d = new ContextMenuStrip();
                        d.Items.Add(copy);
                        d.Items.Add(copy_Row);
                        d.Items.Add(paste);
                        d.Items.Add(paste_Insert);
                        d.Show(Cursor.Position);
                        d.ItemClicked += (a, b) =>
                        {
                            if (b.ClickedItem.Text is copy)
                            {
                                cmd.Clear();
                                cmd.Add(GRecipes.Functions[gantry.Index][funcNo].Cmds[dgv.CurrentRow.Index]);
                            }
                            if (b.ClickedItem.Text is copy_Row)
                            {
                                cmd.Clear();
                                for (int i = dgv.CurrentRow.Index; i < GRecipes.Functions[gantry.Index][funcNo].Cmds.Count; i++) cmd.Add(GRecipes.Functions[gantry.Index][funcNo].Cmds[i]);
                            }
                            if (b.ClickedItem.Text is paste)
                            {
                                foreach (var c in cmd) GRecipes.Functions[gantry.Index][funcNo].Cmds.Add(new TCmd(c));
                            }
                            if (b.ClickedItem.Text is paste_Insert)
                            {
                                foreach (var c in cmd) GRecipes.Functions[gantry.Index][funcNo].Cmds.Insert(dgv.CurrentRow.Index, new TCmd(c));
                            }
                        };
                        break;
                    }
            }
        }

        private void RefreshGrid()
        {
            //Function.Cmds.ResetBindings();
            //foreach (DataGridViewRow row in dgv.Rows)
            //{
            //    row.HeaderCell.Value = (row.Index).ToString("d2");
            //}
            try
            {
                var fidx = 0;
                if (dgv.Rows.Count != 0) fidx = dgv.FirstDisplayedScrollingRowIndex;

                Function.Cmds.ResetBindings();
                if (dgv.Rows.Count != 0) dgv.FirstDisplayedScrollingRowIndex = fidx;
            }
            catch
            {

            }
        }
        #endregion

        private void btnCopy_Click(object sender, EventArgs e)
        {
            int indexDest = gantry.Index;
            int indexSource = indexDest == 0 ? 1 : 0;

            if (MsgBox.ShowDialog("Copy Functions and Pump Setup from " + (indexSource == 0 ? "Left" : "Right") + " ?", MsgBoxBtns.YesNo) == DialogResult.No) return;

            for (int m = 0; m < GRecipes.Functions[indexSource].Count(); m++)
            {
                if (GRecipes.Functions[indexDest].Count() > m)
                    GRecipes.Functions[indexDest][m].Cmds.Clear();
                else
                    GRecipes.Functions[indexDest].Add(new TFunction(gantry.Index));

                GRecipes.Functions[indexDest][m].CopyCommonPara(GRecipes.Functions[indexSource][m]);

                for (int n = 0; n < GRecipes.Functions[indexSource][m].Cmds.Count(); n++)
                    GRecipes.Functions[indexDest][m].Cmds.Add(new TCmd(GRecipes.Functions[indexSource][m].Cmds[n]));
            }

            GRecipes.SP_Setups[indexDest] = new SP_Param(GRecipes.SP_Setups[indexSource]);
            GRecipes.Vermes_Setups[indexDest] = new Vermes3280_Param(GRecipes.Vermes_Setups[indexSource]);

            for (int m = 0; m < GRecipes.PatRecog[indexSource].Count(); m++)
            {
                if (GRecipes.PatRecog[indexDest].Count <= m) GRecipes.PatRecog[indexDest].Add(new TPatRect());
                GRecipes.PatRecog[indexDest][m] = new TPatRect(GRecipes.PatRecog[indexSource][m]);
            }

            for (int m = 0; m < GRecipes.CamFocusNo[indexSource].Count(); m++)
            {
                GRecipes.CamFocusNo[indexDest][m] = GRecipes.CamFocusNo[indexSource][m];
            }

            for (int m = 0; m < GRecipes.Lighting[indexSource].Count(); m++)
            {
                GRecipes.Lighting[indexDest][m] = new LightRGBA(GRecipes.Lighting[indexSource][m]);
            }

            for (int m = 0; m < GRecipes.Reticle[indexSource].Count(); m++)
            {
                GRecipes.Reticle[indexDest][m] = new TEReticle(GRecipes.Reticle[indexSource][m]);
            }

            GRecipes.Functions[gantry.Index].ResetBindings();

            UpdateDisplay();
        }

        private void btnOption_Click(object sender, EventArgs e)
        {
            new frmRecipeCmdEditor().ShowDialog();
            cbxCommand.DataSource = null;
            cbxCommand.DataSource = GRecipes.CmdsDictionary;
        }

        private void lboxFuncList_MouseDown(object sender, MouseEventArgs e)
        {
            if (lboxFuncList.Items.Count is 0) return;
            switch (e.Button)
            {
                default: return;
                case MouseButtons.Right:
                    {
                        ContextMenuStrip cms = new ContextMenuStrip();
                        const string copy = "Copy, Append at Last Row";
                        cms.Items.Add(copy);
                        cms.Show(Cursor.Position);
                        cms.ItemClicked += (a, b) =>
                        {
                            if (b.ClickedItem.Text is copy)
                            {
                                var selectedfunc = new TFunction(GRecipes.Functions[gantry.Index][lboxFuncList.SelectedIndex]);
                                GRecipes.Functions[gantry.Index].Add(new TFunction(selectedfunc) { Name = selectedfunc.Name + " " + nameof(copy) });
                            }
                        };

                        break;
                    }

            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            var func = GRecipes.Functions[gantry.Index];

            frmFileImport frm = new frmFileImport();
            if (frm.ShowDialog() != DialogResult.OK) return;
                   
            for (int i = 0; i < frm.Functions.Count; i++) func.Add(frm.Functions[i]);

            func.ResetBindings();
            UpdateDisplay();

            UpdateGrid();
        }

        #region Function Import
        private void btnLoadFunc_Click(object sender, EventArgs e)
        {
            PointD temp = new PointD();
            TFFuncImport.Load();

            frmFuncImport frmFuncImport = new frmFuncImport();
            frmFuncImport.ShowDialog();

            if (MsgBox.ShowDialog("Please Set Reference Point of the Pattern", MsgBoxBtns.OKCancel) != DialogResult.OK) return;

            temp = TFGantry.GantryLeft.PointXY;

            GRecipes.Functions[gantry.Index].Add(new TFunction() { Name = "DataSet" });

            #region Calculate RelPos
            int funcNo = 0;
            int layoutNo = GRecipes.Functions[gantry.Index][funcNo].LayoutNo;

            PointD layoutRel = GRecipes.MultiLayout[gantry.Index][layoutNo].Cluster.RelPos(Inst.Board[gantry.Index].ClusterCR);
            PointD unitRel = GRecipes.MultiLayout[gantry.Index][layoutNo].Unit.RelPos(Inst.Board[gantry.Index].UnitCR);

            var relPt = temp - GRecipes.Board[gantry.Index].StartPos.GetPointD() + layoutRel + unitRel;
            #endregion

            var funcCount = GRecipes.Functions[gantry.Index].Count;
            var orientation = TFFuncStat.Orientation.Value;

            #region Add point into function
            foreach (var c in TFFuncImport.Functions[TFFuncStat.SelectedIdx].Function.Cmds)
            {
                switch (c.Cmd)
                {
                    default:
                        {
                            GRecipes.Functions[gantry.Index][funcCount - 1].Cmds.Add(new TCmd(c));
                            break;
                        }
                    case ECmd.LINE_START:
                    case ECmd.LINE_PASS:
                    case ECmd.LINE_END:
                    case ECmd.DOT:
                        {

                            #region Scale
                            var scaleX = TFFuncStat.Scale.X;
                            var scaleY = TFFuncStat.Scale.Y;

                            if (c.Para[0] > 0) c.Para[0] += scaleX;
                            else if (c.Para[0] < 0) c.Para[0] -= scaleX;

                            if (c.Para[1] > 0) c.Para[1] += scaleY;
                            else if (c.Para[1] < 0) c.Para[1] -= scaleY;
                            #endregion

                            double newX = c.Para[0];
                            double newY = c.Para[1];
                            //ptRotate.X = alignData.Datum.X + (ptOri.X - alignData.Datum.X) * Math.Cos(angle) - (ptOri.Y - alignData.Datum.Y) * Math.Sin(angle);
                            //ptRotate.Y = alignData.Datum.Y + (ptOri.X - alignData.Datum.X) * Math.Sin(angle) + (ptOri.Y - alignData.Datum.Y) * Math.Cos(angle);
                            #region Orientation
                            if (orientation > 0)
                            {
                                double rad = (360 - orientation) * Math.PI / 180;
                                var radius = Math.Sqrt(Math.Pow(c.Para[0], 2) + Math.Pow(c.Para[1], 2));
                                //newX = 0 + (radius * Math.Sin(rad));
                                //newY = 0 + (radius * Math.Cos(rad));
                                newX = c.Para[0] * Math.Cos(rad) - c.Para[1] * Math.Sin(rad);
                                newY = c.Para[0] * Math.Sin(rad) + c.Para[1] * Math.Cos(rad);
                            }
                            #endregion

                            TCmd cmd = new TCmd(c.Cmd);
                            cmd.Para[0] = newX + relPt.X;
                            cmd.Para[1] = newY + relPt.Y;
                            GRecipes.Functions[gantry.Index][funcCount - 1].Cmds.Add(new TCmd(cmd));
                        }
                        break;
                }
            }
            #endregion
        }
        private void btnSaveFunc_Click(object sender, EventArgs e)
        {
            PointD temp = new PointD();

            if (MsgBox.ShowDialog("Please Set Reference Point of The Pattern.", MsgBoxBtns.OKCancel) != DialogResult.OK) return;
            temp = TFGantry.GantryLeft.PointXY;

            TFFuncImport.Functions.Add(new TEFuncImport(Function, temp));
            TFFuncImport.Save();
            MsgBox.ShowDialog("Saved.", MsgBoxBtns.OK);
        }
        #endregion

        private void btnModels_Click(object sender, EventArgs e)
        {
            frmRecipeModel frmRecipeModel = new frmRecipeModel();
            frmRecipeModel.ShowDialog();
        }
    }
}