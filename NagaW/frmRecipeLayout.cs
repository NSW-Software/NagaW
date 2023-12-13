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
    public partial class frmRecipeLayout : Form
    {
        TEZMCAux.TGroup gantry;

        TMultiLayout MLayout = new TMultiLayout();
        TLayout SelectedLayout = new TLayout();
        PointD StartPos = new PointD();
        string name;

        public static PointXYZ VirtualStartPos = new PointXYZ();
        public static bool EnableDynamicJetSWSetXY = false;

        public frmRecipeLayout()
        {
            InitializeComponent();
        }
        public frmRecipeLayout(TEZMCAux.TGroup gantry):this()
        {
            this.gantry = gantry;
        }
        private void frmRecipeLayout_Load(object sender, EventArgs e)
        {
            lboxMLayoutList.DataSource = GRecipes.MultiLayout[gantry.Index];
            cbxPath.DataSource = Enum.GetValues(typeof(ERunPath));
            UpdateDisplay();
            GControl.LogForm(this);

            gbxVirtual.Visible = false;
        }
        public void UpdateDisplay()
        {
            GControl.UpdateFormControl(this);

            if (GRecipes.IsLoading) return;

            lblBoardStartPos.Text = GRecipes.Board[gantry.Index].StartPos.ToStringForDisplay();
            lblBoardHeight.Text = GRecipes.Board[gantry.Index].Height.ToString("f3") + " mm";

            //MLayout = GRecipes.MultiLayout[gantry.Index][Math.Max(lboxMLayoutList.SelectedIndex, 0)];
            MLayout = GRecipes.MultiLayout[gantry.Index][0];
            //btnCopy.Text = "Copy " + (gantry.Index == 0 ? "R" : "L");

            bool isCluster = rbtnCluster.Checked;
            name = $"Layout:{MLayout} [{(isCluster ? "Cluster" : "Unit")}]";
            SelectedLayout = isCluster ? MLayout.Cluster : MLayout.Unit;
            StartPos = isCluster ? GRecipes.Board[gantry.Index].StartPos.GetPointD() : GRecipes.Board[gantry.Index].StartPos.GetPointD() + MLayout.Cluster.StartPos;

            gbxLayout.Text = MLayout.ToString();/* "Layout " + name;*/
            lblLayoutCol.Text = SelectedLayout.CR.X.ToString();
            lblLayoutRow.Text = SelectedLayout.CR.Y.ToString();

            lblLayoutStartPos.Text = SelectedLayout.StartPos.ToStringForDisplay();

            lblLayoutPitchColX.Text = SelectedLayout.PitchCol.X.ToString("f4");
            lblLayoutPitchColY.Text = SelectedLayout.PitchCol.Y.ToString("f4");
            lblLayoutPitchRowX.Text = SelectedLayout.PitchRow.X.ToString("f4");
            lblLayoutPitchRowY.Text = SelectedLayout.PitchRow.Y.ToString("f4");

            tbxMLayoutName.Text = MLayout.Name;

            cbxPath.SelectedItem = SelectedLayout.RunPath;

            lblStartPosCol.UpdatePara(StartPosCol);
            lblStartPosRow.UpdatePara(StartPosRow);

            // Virtual
            lblVirtualStartPos.Text = VirtualStartPos.ToStringForDisplay();
            chbxEnaDynJet.Checked = EnableDynamicJetSWSetXY;

            gbxVirtual.Visible = virtualEnable;
        }

        private void btnBoardOrgPosSet_Click(object sender, EventArgs e)
        {
            var pos = GRecipes.Board[gantry.Index].StartPos;
            if (!GLog.SetPos(ref pos, gantry.PointXYZ, "Set Board Start Pos")) return;
            GRecipes.Board[gantry.Index].LightDefault = new LightRGBA(TFLightCtrl.LightPair[gantry.Index].CurrentLight);
            UpdateDisplay();
        }
        private void btnBoardOrgPosGoto_Click(object sender, EventArgs e)
        {
            TFLightCtrl.LightPair[gantry.Index].Set(GRecipes.Board[gantry.Index].LightDefault);
            gantry.GotoXYZ(new PointXYZ(GRecipes.Board[gantry.Index].StartPos));
        }

        private void lboxMLayoutList_SelectedIndexChanged(object sender, EventArgs e)
        {
            rbtnUnit.Checked = true;
            Inst.Board[gantry.Index].LayoutNoDisplay = lboxMLayoutList.SelectedIndex;
            UpdateDisplay();
        }

        private void tbxMLayoutName_TextChanged(object sender, EventArgs e)
        {
            MLayout.Name = tbxMLayoutName.Text;
            UpdateDisplay();
            GRecipes.MultiLayout[gantry.Index].ResetBindings();
        }

        private void btnLayoutStartPosPSet_Click(object sender, EventArgs e)
        {
            var sp = SelectedLayout.StartPos;
            var newsp = gantry.PointXY - StartPos;

            if (!GLog.SetPointD(ref sp, newsp, $"{name} Rel Start Pos")) return;

            SelectedLayout.StartPos = new PointD(newsp);
            UpdateDisplay();
        }
        private void btnLayoutStartPosPGoto_Click(object sender, EventArgs e)
        {
            var pos = StartPos + SelectedLayout.StartPos;
            //gantry.MoveOpXYAbs(pos.ToArray);
            gantry.GotoXYZ(new PointXYZ(pos.X, pos.Y, GRecipes.Board[gantry.Index].StartPos.Z));
        }

        private void lblLayoutCol_Click(object sender, EventArgs e)
        {
            IPara para = new IPara(name + " Col", SelectedLayout.CR.X, TLayout.MIN_CR, rbtnCluster.Checked ? TLayout.MAX_CLUSTER_CR : TLayout.MAX_UNIT_CR, EUnit.COUNT);
            if (!GLog.SetPara(ref para)) return;

            SelectedLayout.CR.X = para.Value;
            MapRefine();

            UpdateDisplay();
        }
        private void lblLayoutRow_Click(object sender, EventArgs e)
        {
            IPara para = new IPara(name + " Row", SelectedLayout.CR.Y, TLayout.MIN_CR, rbtnCluster.Checked ? TLayout.MAX_CLUSTER_CR : TLayout.MAX_UNIT_CR, EUnit.COUNT);
            if (!GLog.SetPara(ref para)) return;

            SelectedLayout.CR.Y = para.Value;
            MapRefine();

            UpdateDisplay();
        }

        private void lblLayoutPitchColX_Click(object sender, EventArgs e)
        {
            DPara dPara = new DPara(name + " Pitch Col X", SelectedLayout.PitchCol.X, -TLayout.MAX_PITCH, TLayout.MAX_PITCH, EUnit.MILLIMETER, 4);
            if (GLog.SetPara(ref dPara)) SelectedLayout.PitchCol.X = dPara.Value;

            MapRefine();
            UpdateDisplay();
        }
        private void lblLayoutPitchColY_Click(object sender, EventArgs e)
        {
            DPara dPara = new DPara(name + " Pitch Col Y", SelectedLayout.PitchCol.Y, -TLayout.MAX_PITCH, TLayout.MAX_PITCH, EUnit.MILLIMETER, 4);
            if (GLog.SetPara(ref dPara)) SelectedLayout.PitchCol.Y = dPara.Value;

            MapRefine();
            UpdateDisplay();
        }

        private void btnLayoutPitchColSet_Click(object sender, EventArgs e)
        {
            var pt = SelectedLayout.PitchCol;

            var newpy = gantry.PointXY - (StartPos + SelectedLayout.StartPos);
            newpy.X /= Math.Max(1, SelectedLayout.CR.X - 1);
            newpy.Y /= Math.Max(1, SelectedLayout.CR.X - 1);

            if (!GLog.SetPointD(ref pt, newpy, $"{name} Pitch Col Pos X")) return;

            SelectedLayout.PitchCol = new PointD(newpy);
            UpdateDisplay();
        }
        private void btnLayoutPitchColGoto_Click(object sender, EventArgs e)
        {
            var pos = StartPos + SelectedLayout.StartPos + new PointD(SelectedLayout.PitchCol.X * (SelectedLayout.CR.X - 1), SelectedLayout.PitchCol.Y * (SelectedLayout.CR.X - 1));
            //gantry.MoveOpXYAbs(pos.ToArray);
            gantry.GotoXYZ(new PointXYZ(pos.X, pos.Y, GRecipes.Board[gantry.Index].StartPos.Z));
        }

        private void lblLayoutPitchRowX_Click(object sender, EventArgs e)
        {
            DPara dPara = new DPara(name + " Pitch Row X", SelectedLayout.PitchRow.X, -TLayout.MAX_PITCH, TLayout.MAX_PITCH, EUnit.MILLIMETER, 4);
            if (GLog.SetPara(ref dPara)) SelectedLayout.PitchRow.X = dPara.Value;

            MapRefine();
            UpdateDisplay();
        }
        private void lblLayoutPitchRowY_Click(object sender, EventArgs e)
        {
            DPara dPara = new DPara(name + " Pitch Row Y", SelectedLayout.PitchRow.Y, -TLayout.MAX_PITCH, TLayout.MAX_PITCH, EUnit.MILLIMETER, 4);
            if (GLog.SetPara(ref dPara)) SelectedLayout.PitchRow.Y = dPara.Value;

            MapRefine();
            UpdateDisplay();
        }

        private void btnLayoutPitchRowSet_Click(object sender, EventArgs e)
        {
            var pt = SelectedLayout.PitchRow;
            var newpy = gantry.PointXY - (StartPos + SelectedLayout.StartPos);
            newpy.X /= Math.Max(1, SelectedLayout.CR.Y - 1);
            newpy.Y /= Math.Max(1, SelectedLayout.CR.Y - 1);

            if (!GLog.SetPointD(ref pt, newpy, $"{name} Pitch Row Pos X")) return;

            SelectedLayout.PitchRow = new PointD(newpy);
            UpdateDisplay();
        }
        private void btnLayoutPitchRowGoto_Click(object sender, EventArgs e)
        {
            var pos = StartPos + SelectedLayout.StartPos + new PointD(SelectedLayout.PitchRow.X * (SelectedLayout.CR.Y - 1), SelectedLayout.PitchRow.Y * (SelectedLayout.CR.Y - 1));
            //gantry.MoveOpXYAbs(pos.ToArray);
            gantry.GotoXYZ(new PointXYZ(pos.X, pos.Y, GRecipes.Board[gantry.Index].StartPos.Z));
        }

        private void cbxPath_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SelectedLayout.RunPath = (ERunPath)cbxPath.SelectedItem;
            MapRefine();
            UpdateDisplay();
        }

        private void rbtnUnit_Click(object sender, EventArgs e)
        {
            UpdateDisplay();
        }
        private void rbtnCluster_Click(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void btnBoardHeightSet_Click(object sender, EventArgs e)
        {
            var pos = new PointXYZ();
            if (!GLog.SetPos(ref pos, gantry.PointXYZ, "Set Board Height")) return;
            GRecipes.Board[gantry.Index].Height = pos.Z;

            gantry.MoveOpZAbs(GRecipes.CamFocusNo[gantry.Index][0]);

            UpdateDisplay();
        }

        private void btnResetLayout_Click(object sender, EventArgs e)
        {
            if (MsgBox.ShowDialog($"Reset {name} all properties?", MsgBoxBtns.OKCancel) != DialogResult.OK) return;

            SelectedLayout.ResetAll();
            GRecipes.MultiLayout[gantry.Index].ResetBindings();
            MapRefine();
            UpdateDisplay();
        }

        private void btnResetBoard_Click(object sender, EventArgs e)
        {
            if (MsgBox.ShowDialog($"Reset Board all properties?", MsgBoxBtns.OKCancel) != DialogResult.OK) return;

            GRecipes.Board[gantry.Index].Reset();
            MapRefine();
            UpdateDisplay();
        }

        private void rbtnCluster_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            int indexDest = gantry.Index;
            int indexSource = indexDest == 0 ? 1 : 0;

            if (MsgBox.ShowDialog("Copy Layout from " + (indexSource == 0 ? "Left" : "Right") + " ?", MsgBoxBtns.YesNo) == DialogResult.No) return;

            for (int m = 0; m < GRecipes.MultiLayout[indexSource].Count(); m++)
            {
                GRecipes.MultiLayout[indexDest][m].Copy(GRecipes.MultiLayout[indexSource][m]);
            }

            MapRefine();
            UpdateDisplay();
        }

        frmRecipeMap frmMap = null;
        private void btnMap_Click(object sender, EventArgs e)
        {
            //int idx = lboxMLayoutList.SelectedIndex;

            //if (GRecipes.Maps[gantry.Index].Count <= idx)
            //{
            //    while (GRecipes.Maps[gantry.Index].Count <= idx) GRecipes.Maps[gantry.Index].Add(new TMAP());
            //    GRecipes.Maps[gantry.Index][idx] = new TMAP(MLayout);
            //}

            //GRecipes.Maps[gantry.Index][idx].ClusterL = new TLayout(MLayout.Cluster);
            //GRecipes.Maps[gantry.Index][idx].UnitL = new TLayout(MLayout.Unit);

            //new frmRecipeMap(gantry, GRecipes.Maps[gantry.Index][idx]).ShowDialog();
            MapRefine();

            if (frmMap != null)
            {
                frmMap.BringToFront();
                return;
            }

            int idx = lboxMLayoutList.SelectedIndex;
            frmMap = new frmRecipeMap(gantry, GRecipes.Maps[gantry.Index][idx]);
            frmMap.TopLevel = frmMap.TopMost = true;
            frmMap.StartPosition = FormStartPosition.CenterScreen;

            this.FormClosing += (a, b) =>
            {
                if (frmMap != null) frmMap.Close();
            };

            frmMap.Show();
            frmMap.Location = new Point(frmMap.Location.X, 0);
            frmMap.FormClosing += (a, b) =>
            {
                frmMap = null;
                Inst.Board[gantry.Index].MAP = new TMAP(GRecipes.Maps[gantry.Index][idx]);
                var frmmap = Application.OpenForms.OfType<frmRecipeMap>().FirstOrDefault();
                if (frmmap != null) frmmap.RefreshUI();
            };
        }
        private void MapRefine()
        {
            int idx = lboxMLayoutList.SelectedIndex;//0;
            //if (GRecipes.Maps[gantry.Index].Count is 0) GRecipes.Maps[gantry.Index].Add(new TMAP());
            if (GRecipes.Maps[gantry.Index].Count <= idx)
            {
                while (GRecipes.Maps[gantry.Index].Count <= idx) GRecipes.Maps[gantry.Index].Add(new TMAP());
                GRecipes.Maps[gantry.Index][idx] = new TMAP(MLayout);
            }

            GRecipes.Maps[gantry.Index][idx].ClusterL = new TLayout(MLayout.Cluster);
            GRecipes.Maps[gantry.Index][idx].UnitL = new TLayout(MLayout.Unit);
            GRecipes.Maps[gantry.Index][idx] = new TMAP(GRecipes.Maps[gantry.Index][idx]);

            Inst.Board[gantry.Index].MAP = new TMAP(GRecipes.Maps[gantry.Index][idx]);

            if (frmMap != null) frmMap.UpdateMapFromOutside(GRecipes.Maps[gantry.Index][idx]);
        }

        private void btnImportMap_Click(object sender, EventArgs e)
        {
            if (frmMap != null) frmMap.Close();

            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() != DialogResult.OK) return;

            Form form = new Form() { Text = "Choose Map Type", Font = this.Font };
            FlowLayoutPanel flp = new FlowLayoutPanel() { AutoSizeMode = AutoSizeMode.GrowAndShrink, AutoSize = true };
            Button btn = new Button() { Text = "OK" };
            btn.Click += (a, b) => form.Close();
            ComboBox combo = new ComboBox();
            combo.DataSource = Enum.GetValues(typeof(EMapType));
            combo.DropDownStyle = ComboBoxStyle.DropDownList;

            flp.Controls.Add(combo);
            flp.Controls.Add(btn);
            form.Controls.Add(flp);
            form.TopLevel = form.TopMost = true;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ControlBox = false;
            form.AutoSize = true;
            form.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            form.ShowDialog();
            var maptype = (EMapType)combo.SelectedItem;

            if (TFMap.Decode(openFile.FileName, maptype, out TMultiLayout layout, out TMAP map, out string mapname, out TFunction function, out PointI ref1colrow))
            {
                var path = MLayout.Unit.RunPath;
                var idx = 0;

                var gantryidx = gantry.Index;

                GRecipes.MultiLayout[gantryidx][idx] = layout;
                GRecipes.MultiLayout[gantryidx][idx].Unit.RunPath = path;
                GRecipes.MultiLayout[gantryidx][idx].Name = mapname;
                MapRefine();
                GRecipes.Maps[gantryidx][idx] = map;

                StartPosCol.Value = ref1colrow.X; StartPosRow.Value = ref1colrow.Y;

                UpdateDisplay();

                if (MessageBox.Show("Use pat align from Wafer MapData?\r\nOK to continue\r\nCancel to Skip", "Alignment", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    //GRecipes.Functions[0].Clear();
                    GRecipes.Functions[0].Add(function);
                }
            }
        }


        static IPara StartPosCol = new IPara("", 0, 0, 10000, EUnit.NONE);
        static IPara StartPosRow = new IPara("", 0, 0, 10000, EUnit.NONE);


        private void btnSetFromColRow_Click(object sender, EventArgs e)
        {
            var a = MLayout.Unit.PitchCol.X * (StartPosCol.Value - 1) * -1;
            var b = MLayout.Unit.PitchRow.Y * (StartPosRow.Value - 1) * -1;

            var currentpos = gantry.PointXYZ;

            GRecipes.Board[gantry.Index].StartPos = new PointXYZ(currentpos.X + a, currentpos.Y + b, GRecipes.Board[gantry.Index].StartPos.Z);
            UpdateDisplay();
        }

        private void lblStartPosCol_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref StartPosCol);
            UpdateDisplay();
        }

        private void lblStartPosRow_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref StartPosRow);
            UpdateDisplay();
        }
        bool virtualEnable = false;
        private void btnViewVirtualMode_Click(object sender, EventArgs e)
        {
            virtualEnable = !virtualEnable;
            UpdateDisplay();
            //gbxVirtual.Visible = !gbxVirtual.Visible;
        }

        private void chbxEnaDynJet_Click(object sender, EventArgs e)
        {
            EnableDynamicJetSWSetXY = !EnableDynamicJetSWSetXY;
            UpdateDisplay();
        }

        private void btnSetVirtualPos_Click(object sender, EventArgs e)
        {
            VirtualStartPos = new PointXYZ(gantry.PointXYZ);
            UpdateDisplay();
        }

        private void btnGotoVirtualPos_Click(object sender, EventArgs e)
        {
            gantry.MoveOpXYAbs(VirtualStartPos.GetPointD().ToArray);
        }

        private void lboxMLayoutList_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                default:
                    return;

                case MouseButtons.Right:
                    {
                        ContextMenuStrip cms = new ContextMenuStrip();
                        cms.Items.Add("Add");
                        cms.Items.Add("Remove");
                        cms.Show(Cursor.Position);
                        cms.ItemClicked += (a, b) =>
                        {
                            switch (b.ClickedItem.Text)
                            {
                                case "Add":
                                    {
                                        GRecipes.MultiLayout[gantry.Index].Add(new TMultiLayout { Index = GRecipes.MultiLayout[gantry.Index][GRecipes.MultiLayout[gantry.Index].Count - 1].Index + 1 });
                                        GRecipes.MultiLayout[gantry.Index].ResetBindings();
                                        Inst.Board[gantry.Index].LayerData.Add(new TUnit());

                                        UpdateDisplay();
                                    }
                                    break;
                                case "Remove":
                                    {
                                        if (GRecipes.MultiLayout[gantry.Index].Count == 1)
                                        {
                                            MsgBox.ShowDialog("Minimum 1 layout is needed.");
                                            return;
                                        }
                                        if (MsgBox.ShowDialog("Removing layout will affect Function LayoutNo,Continue?", MsgBoxBtns.YesNo) != DialogResult.Yes) return;
                                        GRecipes.MultiLayout[gantry.Index].RemoveAt(lboxMLayoutList.SelectedIndex);
                                        GRecipes.MultiLayout[gantry.Index].ResetBindings();
                                        Inst.Board[gantry.Index].LayerData.RemoveAt(lboxMLayoutList.SelectedIndex);
                                        UpdateDisplay();
                                    }
                                    break;
                            }
                        };

                    }
                    break;
            }
        }
    }
}
