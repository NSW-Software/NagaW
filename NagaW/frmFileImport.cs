using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace NagaW
{
    public partial class frmFileImport : Form
    {
        PointD OriginPoint = new PointD(0, 0);
        public BindingList<TFunction> Functions;
        private TFunction FunctionSelected => lbxApertureList.SelectedIndex < 0 ? new TFunction() : Functions[lbxApertureList.SelectedIndex];
        private List<TCmd> SelectedTcmds => lbxFeaturesList.SelectedIndices.OfType<int>().Select(x => FunctionSelected.Cmds[x]).ToList();
        DPara Scale = new DPara("File Import Scale", 100, 0, 60000, EUnit.PERCENTAGE, 0);

        private bool mirrorX = false;
        private bool mirrorY = false;
        private TAlignData align = new TAlignData();

        public frmFileImport()
        {
            InitializeComponent();
        }

        enum ESelect { All, Features, OneFeature }
        private void frmFileImport_Load(object sender, EventArgs e)
        {
            lbxFeaturesList.SelectionMode = SelectionMode.MultiExtended;

            //tscmbxTheta.ComboBox.DataSource = Enumerable.Range(0, 360).Select(x => x).ToArray();
            //tscmbxTheta.ComboBox.SelectionChangeCommitted += (a, b) => UpdateDisplay();

            UpdateApertureList();
            UpdateDisplay();

            UpdatePicBx(Scale.Value / 100);
        }

        private void UpdateDisplay()
        {
            lbxApertureList.DataSource = Functions;

            if (Functions != null)
            {
                lbxFeaturesList.DataSource = FunctionSelected.Cmds;
                ModifyFuncs();
            }

            tslblZoom.Text = $"{Scale.Value}%";

            UpdateInfo();
            UpdateFeatureList();
            UpdateApertureList();
            UpdatePicBx(Scale.Value / 100);
        }
        private void UpdateApertureList()
        {
            lbxApertureList.DataSource = Functions;//TFFileImport.Feature.Apertures;
        }
        private void UpdateInfo()
        {
            int i = lbxApertureList.SelectedIndex;
            if (i < 0)
            {
                rtbFeatureDetail.Clear();
                return;
            }

            int count = 0;
            for (int j = 0; j < Functions.Count; j++) count += Functions[j].Cmds.Count;

            rtbFeatureDetail.Text = "Origin: " + OriginPoint.ToStringForDisplay() + '\r' +
            //    "Name: " + TFFileImport.Feature.Apertures[i].Name + '\r' +
            //    "Para: " + TFFileImport.Feature.Apertures[i].Para + '\r' +
            //    "Size: " + TFFileImport.Feature.Apertures[lbxApertureList.SelectedIndex].Size.ToStringForDisplay() + '\r' +
                "Count: " + $"{lbxFeaturesList.Items.Count}" + " / "+ $"{count}";
        }
        List<int> featureListIndex = new List<int>();
        private void UpdateFeatureList()
        {
            int i = lbxApertureList.SelectedIndex;
            if (i < 0)
            {
                lbxFeaturesList.DataSource = null;
                return;
            }
            lbxFeaturesList.DataSource = null;
            lbxFeaturesList.DataSource = Functions[i].Cmds.Select(x => $"{x.Cmd} X: {x.Para[0]:f3}, Y: {x.Para[1]:f3}").ToList();//TFFileImport.Feature.Features.Where(x => x.ApertureIndex == i).ToArray();
            featureListIndex = Functions[i].Cmds.Select(y => y.ID).ToList();//TFFileImport.Feature.Features.Where(x => x.ApertureIndex == i).Select(y => y.ID).ToList();
            UpdateInfo();
        }

        private void ModifyFuncs()
        {
            if (Functions is null) return;
            //align.Angle_Rad = (int)tscmbxTheta.SelectedItem * Math.PI / 180;

            for (int i = 0; i < Functions.Count; i++)
            {
                foreach (var c in Functions[i].Cmds)
                {
                    var pD = new PointD(c.Para[0], c.Para[1]);
                    pD = new PointD(pD.X * (mirrorX ? -1 : 1), pD.Y * (mirrorY ? -1 : 1));
                    //pD = MathPro.Translate(pD, align);
                    c.Para[0] = pD.X;
                    c.Para[1] = pD.Y;
                }
            }
        }

        private void lbxApertureList_Click(object sender, EventArgs e)
        {
            UpdateFeatureList();
        }
        private void lbxApertureList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFeatureList();
        }

        private void btnOriginXY_Click(object sender, EventArgs e)
        {
            var temp = Functions[lbxApertureList.SelectedIndex].Cmds[lbxFeaturesList.SelectedIndex];

            OriginPoint = new PointD(temp.Para[0], temp.Para[1]);
            UpdateInfo();
        }
        private void btnRef1_Click(object sender, EventArgs e)
        {
            int f = lbxFeaturesList.SelectedIndex;
            if (f < 0) return;
            int a = lbxApertureList.SelectedIndex;
            if (a < 0) return;

            Functions[a].Cmds[f].Cmd = ECmd.PAT_ALIGN_BOARD;

            var tempCmd = Functions[a].Cmds[f];
            Functions[a].Cmds.RemoveAt(f);
            Functions[a].Cmds.Insert(0, tempCmd);

            UpdateDisplay();
        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            SelectedTcmds.ForEach(x => x.Cmd = ECmd.NONE);
            FunctionSelected.Cmds.ResetBindings();
            UpdateDisplay();
        }
        private void btnDot_Click(object sender, EventArgs e)
        {
            SelectedTcmds.ForEach(x => x.Cmd = ECmd.DOT);
            FunctionSelected.Cmds.ResetBindings();
            UpdateDisplay();
        }
        private void btnLine_Click(object sender, EventArgs e)
        {
            if (SelectedTcmds.Count < 2) return;
            SelectedTcmds.ForEach(x => x.Cmd = ECmd.LINE_PASS);
            SelectedTcmds.First().Cmd = ECmd.LINE_START;
            SelectedTcmds.Last().Cmd = ECmd.LINE_END;

            FunctionSelected.Cmds.ResetBindings();
            UpdateDisplay();
        }
        private void btnArc_Click(object sender, EventArgs e)
        {
            int i = lbxFeaturesList.SelectedIndex;
            if (i < 0) return;

            //switch (cbxSelect.SelectedIndex)
            //{
            //    case (int)ESelect.All:
            //        foreach (var v in TFFileImport.Feature.Features)
            //        {
            //            v.DispType = TFFileImport.EDispType.Arc;
            //        }
            //        break;
            //    case (int)ESelect.Features:
            //        foreach (var v in TFFileImport.Feature.Features)
            //        {
            //            if (v.ApertureIndex == lbxApertureList.SelectedIndex)
            //            {
            //                v.DispType = TFFileImport.EDispType.Arc;
            //            }
            //        }
            //        break;
            //    case (int)ESelect.OneFeature:
            //        {
            //            int idx = featureListIndex[i];
            //            TFFileImport.Feature.Features[idx].DispType = TFFileImport.EDispType.Arc;
            //        }
            //        break;
            //}
            UpdateDisplay();
            lbxFeaturesList.SelectedIndex = i;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = GDoc.SMTRecipeDir.FullName;
            ofd.Filter = "NSW Disp Definition|*.ndd";

            if (ofd.ShowDialog() != DialogResult.OK) return;

            if (!TFFileImport.Load(ofd.FileName)) return;

            UpdateDisplay();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = GDoc.SMTRecipeDir.FullName;
            sfd.Filter ="NSW Disp Definition|*.ndd";

            if (sfd.ShowDialog() != DialogResult.OK) return;

            TFFileImport.Save(sfd.FileName);
        }
        private void btnImport_Click(object sender, EventArgs e)
        {
            frmFileImportSelect frm = new frmFileImportSelect();
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.Cancel) return;

            Functions = frm.Functions;
            lbxApertureList.DataSource = Functions; //TFFileImport.Feature.Apertures;

            //lbxFeaturesList.DataSource = null;
            //lbxFeaturesList.DataSource = Functions[0].Cmds;//TFFileImport.Feature.Features.Where(x => x.ApertureIndex == i).ToArray();

            ZoomFit();
            UpdateDisplay();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            foreach (var cmd in Functions.SelectMany(x => x.Cmds))
            {
                cmd.Para[0] = cmd.Para[0] - OriginPoint.X;
                cmd.Para[1] = cmd.Para[1] - OriginPoint.Y;
            }

            Functions.ToList().ForEach(f => ModifyFuncs());

            DialogResult = DialogResult.OK;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (Functions.Count <= 0) return;
            Functions.Clear();
            UpdateDisplay();
        }

        private void btnApUp_Click(object sender, EventArgs e)
        {
            int i = lbxApertureList.SelectedIndex;
            if (i <= 0) return;

            Functions.MoveUp(i);

            UpdateApertureList();
            lbxApertureList.SelectedIndex = i - 1;
        }
        private void btnApDn_Click(object sender, EventArgs e)
        {
            int i = lbxApertureList.SelectedIndex;
            if (i < 0) return;
            if (i >= Functions.Count - 1) return;

            Functions.MoveDown(i);

            UpdateApertureList();
            lbxApertureList.SelectedIndex = i + 1;
        }
        private void btnApDel_Click(object sender, EventArgs e)
        {
            int i = lbxApertureList.SelectedIndex;
            if (i < 0) return;

            Functions.RemoveAt(i);
            UpdateDisplay();
        }

        private void btnFtUp_Click(object sender, EventArgs e)
        {
            int i = lbxFeaturesList.SelectedIndex;
            if (i <= 0) return;
            int j = lbxApertureList.SelectedIndex;

            Functions[j].Cmds.MoveUp(i);

            UpdateFeatureList();
            lbxFeaturesList.SelectedIndex = i - 1;
        }
        private void btnFtDn_Click(object sender, EventArgs e)
        {
            int i = lbxFeaturesList.SelectedIndex;
            if (i < 0) return;
            if (i >= lbxFeaturesList.Items.Count - 1) return;
            int j = lbxApertureList.SelectedIndex;

            Functions[j].Cmds.MoveDown(i);

            UpdateFeatureList();
            lbxFeaturesList.SelectedIndex = i + 1;
        }
        private void btnFtDel_Click(object sender, EventArgs e)
        {
            int i = lbxFeaturesList.SelectedIndex;
            if (i <= 0) return;

            Functions[lbxApertureList.SelectedIndex].Cmds.RemoveAt(i);
            UpdateDisplay();
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            //int i = lbxFeaturesList.SelectedIndex;
            //if (i < 0) return;
            //TFFileImport.Feature.Sort(new PointD(0, 0));
            //UpdateFeatureList();
            //UpdateApertureList();
        }

        private void cbxOptimizeAll_CheckedChanged(object sender, EventArgs e)
        {
            TFFileImport.OptimizeAll = cbxOptimizeAll.Checked;
        }

        #region Image Box
        private void tsbtnZoomIn_Click(object sender, EventArgs e)
        {
            ZoomIn();
        }
        private void tsbtnZoomFit_Click(object sender, EventArgs e)
        {
            ZoomFit();
        }
        private void tsbtnZoomOut_Click(object sender, EventArgs e)
        {
            ZoomOut();
        }

        public void ZoomFit()
        {
            Scale.Value = 100;
            UpdatePicBx(Scale.Value / 100);
            UpdateDisplay();
        }
        public void ZoomIn()
        {
            Scale.Value += 20;
            UpdatePicBx(Scale.Value / 100);
            UpdateDisplay();
        }
        public void ZoomOut()
        {
            Scale.Value -= 20;
            UpdatePicBx(Scale.Value / 100);
            UpdateDisplay();
        }
        private void tslblZoom_Click(object sender, EventArgs e)
        {
            if (!GLog.SetPara(ref Scale)) return;
            UpdatePicBx(Scale.Value / 100);
            UpdateDisplay();
        }

        public void UpdatePicBx(double multiply = 1)
        {
            if (Functions is null) return;
            int width = 3;
            int startPoint = 5;

            List<double> para0 = new List<double>(); List<double> para1 = new List<double>();
            List<TCmd> cmdList = new List<TCmd>();

            for (int i = 0; i < Functions.Count; i++)
            {
                para0.AddRange(Functions[i].Cmds.Select(x => x.Para[0]).ToList());
                para1.AddRange(Functions[i].Cmds.Select(x => x.Para[1]).ToList());
                cmdList.AddRange(Functions[i].Cmds);
            }

            if (para0.Count <= 0 || para1.Count <= 0) return;

            double minX = para0.Min();
            double maxX = para0.Max();
            double minY = para1.Max(); 
            double maxY = para1.Min(); 

            if ((maxX - minX) is 0 || (maxY - minY) is 0) return;
            if (double.IsNaN(minX) || double.IsNaN(maxX) || double.IsNaN(minY) || double.IsNaN(maxY)) return;
            Size s = new Size((int)Math.Abs(maxX - minX), (int)Math.Abs(maxY - minY));
            s = s.Height is 0 && s.Width is 0 ? new Size(1, 1) : s;
            float ratio = (float)Math.Min(picBxPrevImg.Width, picBxPrevImg.Height) / (float)Math.Max(s.Width, s.Height);
            ratio *= (float)multiply;
            if (float.IsNaN(ratio) || float.IsInfinity(ratio)) return;

            List<PointF> pts = new List<PointF>();
            for (int i = 0; i < para0.Count; i++) pts.Add(new PointF((float)para0[i], (float)para1[i]));

            PointF[] pFs = pts.Select(p => new PointF((float)(p.X - minX) * ratio, -(float)(p.Y - minY) * ratio)).ToArray();

            var sizeMaxX = Math.Abs(pFs.Max(p => p.X) - pFs.Min(p => p.X));
            var sizeMaxY = Math.Abs(pFs.Max(p => p.Y) - pFs.Min(p => p.Y));
            Image img = new Bitmap((int)sizeMaxX + startPoint, (int)sizeMaxY + startPoint);
            Graphics g = Graphics.FromImage(img);

            Pen pen = new Pen(Color.Navy);
            SolidBrush solidBrush = new SolidBrush(Color.Navy);

            for (int i = 0; i < cmdList.Count; i++)
            {
                var xy = pFs[i];
                xy = new PointF(xy.X, xy.Y);

                PointF prevPt = new PointF(0, 0);
                if (i > 0)
                {
                    var temp = pFs[i - 1];
                    prevPt = new PointF(temp.X, temp.Y);
                }

                switch (cmdList[i].Cmd)
                {
                    default: break;
                    case ECmd.DOT:
                        g.DrawEllipse(pen, xy.X, xy.Y, width, width);
                        g.FillEllipse(solidBrush, xy.X, xy.Y, width, width);
                        break;
                    case ECmd.LINE_START:
                        g.DrawLine(pen, pFs[i].X, pFs[i].Y, pFs[i].X, pFs[i].Y);
                        break;
                    case ECmd.LINE_END:
                    case ECmd.LINE_PASS:
                        g.DrawLine(pen, pFs[i - 1].X, pFs[i - 1].Y, pFs[i].X, pFs[i].Y);
                        break;
                    case ECmd.ARC_PASS:
                        //bool circle = prevPt == xy;
                        //var radius = TFFileImport.Feature.Features[i].Radius * ratio;

                        //if (circle) g.DrawEllipse(pen, xy.X, xy.Y, (float)radius, (float)radius);
                        //else
                        //{
                        //    PointD center = new PointD(radius + xy.X, radius + xy.Y);
                        //    var angleStart = Math.Atan2((prevPt.Y - center.Y), (prevPt.X - center.X));
                        //    var angleEnd = Math.Atan2((xy.Y - center.Y), (xy.X - center.X));
                        //    if (angleStart < 0) angleStart = Math.PI * 2 - angleStart;
                        //    if (angleEnd < 0) angleEnd = Math.PI * 2 - angleEnd;
                        //    double rad = angleEnd - angleStart;

                        //    var sweepDeg = rad * 180 / Math.PI;
                        //    var startDeg = angleStart * 180 / Math.PI;

                        //    g.DrawArc(pen, xy.X, xy.Y, (float)radius, (float)radius, (float)startDeg, (float)sweepDeg);
                        //}

                        break;
                }
            }

            int tempCount = 0;
            for (int i = 0; i < lbxApertureList.SelectedIndex; i++) tempCount += Functions[i].Cmds.Count;

            int y = lbxFeaturesList.SelectedIndex + tempCount;
            pen = new Pen(Color.Red); solidBrush = new SolidBrush(Color.Red);
            if (y >= 0)
            {
                //width += 1;
                g.DrawEllipse(pen, pFs[y].X, pFs[y].Y, width, width);
                g.FillEllipse(solidBrush, pFs[y].X, pFs[y].Y, width, width);
            }

            picBxPrevImg.SizeMode = PictureBoxSizeMode.StretchImage;
            picBxPrevImg.Image = img;

        }
        #endregion

        private void lbxFeaturesList_Click(object sender, EventArgs e)
        {
            //UpdatePicBx(Scale.Value / 100);
        }

        private void lbxFeaturesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePicBx(Scale.Value / 100);
        }

        private void tsbtnMirrorX_Click(object sender, EventArgs e)
        {
            mirrorX = tsbtnMirrorX.Checked;
            UpdateDisplay();
        }
        private void tsbtnMirrorY_Click(object sender, EventArgs e)
        {
            mirrorY = tsbtnMirrorY.Checked;
            UpdateDisplay();
        }
    }
}
