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
        PointD InitialPnlSize = new PointD(0, 0);
        DPara Scale = new DPara("File Import Scale", 100, 0, 60000, EUnit.PERCENTAGE, 0);
        //private PictureBox PictureBox;

        public frmFileImport()
        {
            InitializeComponent();
            //PictureBox = new PictureBox() { /*Dock = DockStyle.Fill,*/Width = pnlPicture.Width, Height = pnlPicture.Height, BackColor = Color.Black, };
            //pnlPicture.BackColor = SystemColors.Control;//Color.Black;
            //pnlPicture.Controls.Add(PictureBox);
            //InitialPnlSize = new PointD(pnlPicture.Width, pnlPicture.Height);

            //var temp = typeof(Color).GetProperties().Where(x => x.PropertyType == typeof(Color)).Select(x => (Color)x.GetValue(null)).ToArray();
            //var temp = typeof(Color).GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public).Select(x => (Color)x.GetValue(null, null)).ToArray();
            //foreach (var t in temp) tscmbxColor.Items.Add(t);
            //tscmbxColor.Items.AddRange(typeof(Color).GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public).Select(x => (Color)x.GetValue(null, null)).ToArray());
        }

        enum ESelect { All, Features, OneFeature }
        private void frmFileImport_Load(object sender, EventArgs e)
        {
            cbxSelect.DataSource = Enum.GetValues(typeof(ESelect));
            UpdateApertureList();
            UpdateDisplay();

            UpdatePicBx(Scale.Value / 100);

            pnlPicture.Scroll += (a, b) => { UpdatePicBx(Scale.Value / 100); };
        }

        private void UpdateDisplay()
        {
            //cbxOptimizeAll.Checked = TFFileImport.OptimizeAll;
            tslblZoom.Text = $"{Scale.Value}%";
        }
        private void UpdateApertureList()
        {
            lbxApertureList.DataSource = TFFileImport.Feature.Apertures;
        }
        private void UpdateInfo()
        {
            int i = lbxApertureList.SelectedIndex;
            if (i < 0)
            {
                rtbFeatureDetail.Clear();
                return;
            }

            rtbFeatureDetail.Text = "Origin: " + TFFileImport.Feature.Origin.ToStringForDisplay() + '\r' +
                "Name: " + TFFileImport.Feature.Apertures[i].Name + '\r' +
                "Para: " + TFFileImport.Feature.Apertures[i].Para + '\r' +
                "Size: " + TFFileImport.Feature.Apertures[lbxApertureList.SelectedIndex].Size.ToStringForDisplay() + '\r' +
                "Count: " + $"{lbxFeaturesList.Items.Count}" + " / "+ $"{TFFileImport.Feature.Features.Count}";
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
            lbxFeaturesList.DataSource = TFFileImport.Feature.Features.Where(x => x.ApertureIndex == i).ToArray();
            featureListIndex = TFFileImport.Feature.Features.Where(x => x.ApertureIndex == i).Select(y => y.ID).ToList();
            UpdateInfo();
        }
        private void lbxApertureList_Click(object sender, EventArgs e)
        {
            UpdateFeatureList();
        }
        private void lbxApertureList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFeatureList();
        }
        private void lblFeatures_Click(object sender, EventArgs e)
        {

        }

        private void btnOriginXY_Click(object sender, EventArgs e)
        {
            int tempCount = 0;
            tempCount = TFFileImport.Feature.Features.Where(x => x.ApertureIndex < lbxApertureList.SelectedIndex).Count();

            int i = lbxFeaturesList.SelectedIndex + tempCount;
            if (i < 0) return;

            //int idx = featureListIndex[i];
            TFFileImport.Feature.Origin = TFFileImport.Feature.Features[i].Point;
            UpdateInfo();
        }
        private void btnRef1_Click(object sender, EventArgs e)
        {
            int i = lbxFeaturesList.SelectedIndex;
            if (i < 0) return;

            int idx = featureListIndex[i];
            TFFileImport.Feature.Features[idx].DispType = TFFileImport.EDispType.Ref1;
            TFFileImport.Feature.Ref1 = TFFileImport.Feature.Features[idx].Point;
            UpdateFeatureList();
            lbxFeaturesList.SelectedIndex = i;
        }
        private void btnRef2_Click(object sender, EventArgs e)
        {
            int i = lbxFeaturesList.SelectedIndex;
            if (i < 0) return;

            int idx = featureListIndex[i];
            TFFileImport.Feature.Features[idx].DispType = TFFileImport.EDispType.Ref2;
            TFFileImport.Feature.Ref2 = TFFileImport.Feature.Features[idx].Point;
            UpdateFeatureList();
            lbxFeaturesList.SelectedIndex = i;
        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            int i = lbxFeaturesList.SelectedIndex;
            if (i < 0) return;

            switch (cbxSelect.SelectedIndex)
            {
                case (int)ESelect.All:
                    foreach (var v in TFFileImport.Feature.Features)
                    {
                        v.DispType = TFFileImport.EDispType.None;
                    }
                    break;
                case (int)ESelect.Features:
                    foreach (var v in TFFileImport.Feature.Features)
                    {
                        if (v.ApertureIndex == lbxApertureList.SelectedIndex)
                        {
                            v.DispType = TFFileImport.EDispType.None;
                        }
                    }
                    break;
                case (int)ESelect.OneFeature:
                    {
                        int idx = featureListIndex[i];
                        TFFileImport.Feature.Features[idx].DispType = TFFileImport.EDispType.None;
                    }
                    break;
            }
            UpdateFeatureList();
            lbxFeaturesList.SelectedIndex = i;
        }
        private void btnDot_Click(object sender, EventArgs e)
        {
            int i = lbxFeaturesList.SelectedIndex;
            if (i < 0) return;

            switch (cbxSelect.SelectedIndex)
            {
                case (int)ESelect.All:
                    foreach (var v in TFFileImport.Feature.Features)
                    {
                        v.DispType = TFFileImport.EDispType.Dot;
                    }
                    break;
                case (int)ESelect.Features:
                    foreach (var v in TFFileImport.Feature.Features)
                    {
                        if (v.ApertureIndex == lbxApertureList.SelectedIndex)
                        {
                            v.DispType = TFFileImport.EDispType.Dot;
                        }
                    }
                    break;
                case (int)ESelect.OneFeature:
                    {
                        int idx = featureListIndex[i];
                        TFFileImport.Feature.Features[idx].DispType = TFFileImport.EDispType.Dot;
                    }
                    break;
            }
            UpdateFeatureList();
            lbxFeaturesList.SelectedIndex = i;
        }
        private void btnLine_Click(object sender, EventArgs e)
        {
            int i = lbxFeaturesList.SelectedIndex;
            if (i < 0) return;

            switch (cbxSelect.SelectedIndex)
            {
                case (int)ESelect.All:
                    foreach (var v in TFFileImport.Feature.Features)
                    {
                        v.DispType = TFFileImport.EDispType.Line;
                    }
                    break;
                case (int)ESelect.Features:
                    foreach (var v in TFFileImport.Feature.Features)
                    {
                        if (v.ApertureIndex == lbxApertureList.SelectedIndex)
                        {
                            v.DispType = TFFileImport.EDispType.Line;
                        }
                    }
                    break;
                case (int)ESelect.OneFeature:
                    {
                        int idx = featureListIndex[i];
                        TFFileImport.Feature.Features[idx].DispType = TFFileImport.EDispType.Line;
                    }
                    break;
            }
            UpdateFeatureList();
            lbxFeaturesList.SelectedIndex = i;
        }
        private void btnArc_Click(object sender, EventArgs e)
        {
            int i = lbxFeaturesList.SelectedIndex;
            if (i < 0) return;

            switch (cbxSelect.SelectedIndex)
            {
                case (int)ESelect.All:
                    foreach (var v in TFFileImport.Feature.Features)
                    {
                        v.DispType = TFFileImport.EDispType.Arc;
                    }
                    break;
                case (int)ESelect.Features:
                    foreach (var v in TFFileImport.Feature.Features)
                    {
                        if (v.ApertureIndex == lbxApertureList.SelectedIndex)
                        {
                            v.DispType = TFFileImport.EDispType.Arc;
                        }
                    }
                    break;
                case (int)ESelect.OneFeature:
                    {
                        int idx = featureListIndex[i];
                        TFFileImport.Feature.Features[idx].DispType = TFFileImport.EDispType.Arc;
                    }
                    break;
            }
            UpdateFeatureList();
            lbxFeaturesList.SelectedIndex = i;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = GDoc.SMTRecipeDir.FullName;
            ofd.Filter = "NSW Disp Definition|*.ndd";

            if (ofd.ShowDialog() != DialogResult.OK) return;

            if (!TFFileImport.Load(ofd.FileName)) return;

            UpdateApertureList();
            UpdateFeatureList();
            UpdateInfo();
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

            lbxApertureList.DataSource = TFFileImport.Feature.Apertures;

            int i = lbxApertureList.SelectedIndex;
            lbxFeaturesList.DataSource = null;
            lbxFeaturesList.DataSource = TFFileImport.Feature.Features.Where(x => x.ApertureIndex == i).ToArray();

            UpdateInfo();
            //UpdatePicBx();
            ZoomFit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            TFFileImport.Feature.Clear();
            UpdateApertureList();
            UpdateFeatureList();
            UpdateInfo();
        }

        private void btnApUp_Click(object sender, EventArgs e)
        {
            int i = lbxApertureList.SelectedIndex;
            if (i <= 0) return;

            TFFileImport.Feature.Apertures.MoveUp(i);

            TFFileImport.Feature.Features.Where(x => x.ApertureIndex == i - 1).ToList().ForEach(f => f.ApertureIndex = -1);
            TFFileImport.Feature.Features.Where(x => x.ApertureIndex == i).ToList().ForEach(f => f.ApertureIndex = i-1);
            TFFileImport.Feature.Features.Where(x => x.ApertureIndex == - 1).ToList().ForEach(f => f.ApertureIndex = i);

            UpdateApertureList();
            lbxApertureList.SelectedIndex = i - 1;
        }
        private void btnApDn_Click(object sender, EventArgs e)
        {
            int i = lbxApertureList.SelectedIndex;
            if (i < 0) return;
            if (i >= TFFileImport.Feature.Apertures.Count - 1) return;

            TFFileImport.Feature.Apertures.MoveDown(i);

            TFFileImport.Feature.Features.Where(x => x.ApertureIndex == i + 1).ToList().ForEach(f => f.ApertureIndex = -1);
            TFFileImport.Feature.Features.Where(x => x.ApertureIndex == i).ToList().ForEach(f => f.ApertureIndex = i + 1);
            TFFileImport.Feature.Features.Where(x => x.ApertureIndex == -1).ToList().ForEach(f => f.ApertureIndex = i);

            UpdateApertureList();
            lbxApertureList.SelectedIndex = i + 1;
        }

        private void btnFtUp_Click(object sender, EventArgs e)
        {
            int i = lbxFeaturesList.SelectedIndex;
            if (i <= 0) return;
            int j = lbxApertureList.SelectedIndex;

            int idx = 0;
            foreach (TFFileImport.TFeatures feat in TFFileImport.Feature.Features)
            {
                if (feat.ApertureIndex == j) break;
                idx++;
            }
            TFFileImport.Feature.Features.MoveUp(idx + i);

            UpdateFeatureList();
            lbxFeaturesList.SelectedIndex = i - 1;
        }
        private void btnFtDn_Click(object sender, EventArgs e)
        {
            int i = lbxFeaturesList.SelectedIndex;
            if (i < 0) return;
            if (i >= lbxFeaturesList.Items.Count - 1) return;
            int j = lbxApertureList.SelectedIndex;

            int idx = 0;
            foreach (TFFileImport.TFeatures feat in TFFileImport.Feature.Features)
            {
                if (feat.ApertureIndex == j) break;
                idx++;
            }
            TFFileImport.Feature.Features.MoveDown(idx + i);

            UpdateFeatureList();
            lbxFeaturesList.SelectedIndex = i + 1;
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            int i = lbxFeaturesList.SelectedIndex;
            if (i < 0) return;
            TFFileImport.Feature.Sort(new PointD(0, 0));
            UpdateFeatureList();
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

        public void UpdatePicBx2(double multiply = 1)
        {
            if (TFFileImport.Feature.Features.Count < 1) return;
            int width = 3;

            double minX = TFFileImport.Feature.Features.Min(p => p.Point.X);
            double maxX = TFFileImport.Feature.Features.Max(p => p.Point.X);
            double minY = TFFileImport.Feature.Features.Min(p => p.Point.Y);
            double maxY = TFFileImport.Feature.Features.Max(p => p.Point.Y);

            if ((maxX - minX) is 0 || (maxY - minY) is 0) return;
            if (double.IsNaN(minX) || double.IsNaN(maxX) || double.IsNaN(minY) || double.IsNaN(maxY)) return;
            Size s = new Size((int)Math.Abs(maxX - minX), (int)Math.Abs(maxY - minY));
            s = s.Height is 0 && s.Width is 0 ? new Size(1, 1) : s;
            float ratio = (float)Math.Min(picBxPrevImg.Width, picBxPrevImg.Height) / (float)Math.Max(s.Width, s.Height);
            if (float.IsNaN(ratio) || float.IsInfinity(ratio)) return;

            PointF[] pFs = TFFileImport.Feature.Features.Select(p => new PointF((float)(p.Point.X - minX) * ratio, -(float)(p.Point.Y - minY) * ratio)).ToArray();

            Image img = new Bitmap((int)Math.Abs(pFs.Max(p => p.X) - pFs.Min(p => p.X)) + 2, (int)Math.Abs(pFs.Max(p => p.Y) - pFs.Min(p => p.Y)) + 2);
            Graphics g = Graphics.FromImage(img);

            Pen pen = new Pen(Color.Navy);
            SolidBrush solidBrush = new SolidBrush(Color.Navy);

            for (int i = 0; i < TFFileImport.Feature.Features.Count; i++)
            {
                var xy = TFFileImport.Feature.Features[i].Point;

                PointD prevPt = new PointD(0, 0);
                if (i > 0)
                {
                    prevPt = TFFileImport.Feature.Features[i - 1].Point;
                    //var temp = TFFileImport.Feature.Features[i - 1].Point;
                }

                switch (TFFileImport.Feature.Features[i].Type)
                {
                    case TFFileImport.EFeatureType.Dot:
                        g.DrawEllipse(pen, (float)xy.X, (float)xy.Y, width, width);
                        g.FillEllipse(solidBrush, (float)xy.X, (float)xy.Y, width, width);
                        break;
                    case TFFileImport.EFeatureType.Line:
                        var prevXY = i is 0 ? xy : new PointD(0, 0);
                        if (i > 0) prevXY = TFFileImport.Feature.Features[i - 1].Type == TFFileImport.Feature.Features[i].Type ? prevPt : xy;
                        //var prevXY = new PointD(mid[0] + (prevPt.X * multiply), mid[1] - (prevPt.Y * multiply));
                        g.DrawLine(pen, (float)prevXY.X, (float)prevXY.Y, (float)xy.X, (float)xy.Y);
                        break;
                    case TFFileImport.EFeatureType.Arc:
                        bool circle = prevPt == xy;
                        var radius = TFFileImport.Feature.Features[i].Radius;

                        if (circle) g.DrawEllipse(pen, (float)xy.X, (float)xy.Y, (float)radius, (float)radius);
                        else
                        {
                            PointD center = new PointD(radius + xy.X, radius + xy.Y);
                            var angleStart = Math.Atan2((prevPt.Y - center.Y), (prevPt.X - center.X));
                            var angleEnd = Math.Atan2((xy.Y - center.Y), (xy.X - center.X));
                            if (angleStart < 0) angleStart = Math.PI * 2 - angleStart;
                            if (angleEnd < 0) angleEnd = Math.PI * 2 - angleEnd;
                            double rad = angleEnd - angleStart;

                            var sweepDeg = rad * 180 / Math.PI;
                            var startDeg = angleStart * 180 / Math.PI;

                            g.DrawArc(pen, (float)xy.X, (float)xy.Y, (float)radius, (float)radius, (float)startDeg, (float)sweepDeg);
                        }

                        break;
                }
            }

            picBxPrevImg.SizeMode = PictureBoxSizeMode.Zoom;
            picBxPrevImg.Image = img;
            //img.Save("C:\\Users\\yhlee\\Desktop\\temp.PNG");
        }

        public void UpdatePicBx1(double multiply = 1)
        {
            if (TFFileImport.Feature.Features.Count < 1) return;
            int width = 3;
            int startPoint = 5;

            picBxPrevImg.Refresh();

            var mid = new int[] { startPoint, picBxPrevImg.Height - startPoint };

            double minX = TFFileImport.Feature.Features.Min(p => p.Point.X);
            double maxX = TFFileImport.Feature.Features.Max(p => p.Point.X);
            double minY = TFFileImport.Feature.Features.Min(p => p.Point.Y);
            double maxY = TFFileImport.Feature.Features.Max(p => p.Point.Y);

            if ((maxX - minX) is 0 || (maxY - minY) is 0) return;
            if (double.IsNaN(minX) || double.IsNaN(maxX) || double.IsNaN(minY) || double.IsNaN(maxY)) return;
            Size s = new Size((int)Math.Abs(maxX - minX), (int)Math.Abs(maxY - minY));
            s = s.Height is 0 && s.Width is 0 ? new Size(1, 1) : s;
            float ratio = (float)Math.Min(picBxPrevImg.Width, picBxPrevImg.Height) / (float)Math.Max(s.Width, s.Height);
            if (float.IsNaN(ratio) || float.IsInfinity(ratio)) return;

            PointF[] pFs = TFFileImport.Feature.Features.Select(p => new PointF((float)(p.Point.X - minX) * ratio, -(float)(p.Point.Y - minY) * ratio)).ToArray();

            Graphics g = picBxPrevImg.CreateGraphics();
            Pen pen = new Pen(Color.Navy);
            SolidBrush solidBrush = new SolidBrush(Color.Navy);

            for (int i = 0; i < pFs.Length; i++)
            {
                var xy = pFs[i];
                xy = new PointF(mid[0] + (xy.X * (float)multiply), mid[1] + (xy.Y * (float)multiply));

                PointF prevPt = new PointF(0, 0);
                if (i > 0)
                {
                    var temp = pFs[i - 1];
                    prevPt = new PointF(mid[0] + (temp.X * (float)multiply), mid[1] + (temp.Y * (float)multiply));
                }

                switch (TFFileImport.Feature.Features[i].Type)
                {
                    case TFFileImport.EFeatureType.Dot:
                        g.DrawEllipse(pen, xy.X, xy.Y, width, width);
                        g.FillEllipse(solidBrush, xy.X, xy.Y, width, width);
                        break;
                    case TFFileImport.EFeatureType.Line:
                        var prevXY = i is 0 ? xy : new PointF(0, 0);
                        if (i > 0) prevXY = TFFileImport.Feature.Features[i - 1].Type == TFFileImport.Feature.Features[i].Type ? prevPt : xy;
                        g.DrawLine(pen, prevXY.X, prevXY.Y, xy.X, xy.Y);
                        break;
                    case TFFileImport.EFeatureType.Arc:
                        bool circle = prevPt == xy;
                        var radius = TFFileImport.Feature.Features[i].Radius * ratio;

                        if (circle) g.DrawEllipse(pen, xy.X, xy.Y, (float)radius, (float)radius);
                        else
                        {
                            PointD center = new PointD(radius + xy.X, radius + xy.Y);
                            var angleStart = Math.Atan2((prevPt.Y - center.Y), (prevPt.X - center.X));
                            var angleEnd = Math.Atan2((xy.Y - center.Y), (xy.X - center.X));
                            if (angleStart < 0) angleStart = Math.PI * 2 - angleStart;
                            if (angleEnd < 0) angleEnd = Math.PI * 2 - angleEnd;
                            double rad = angleEnd - angleStart;

                            var sweepDeg = rad * 180 / Math.PI;
                            var startDeg = angleStart * 180 / Math.PI;

                            g.DrawArc(pen, xy.X, xy.Y, (float)radius, (float)radius, (float)startDeg, (float)sweepDeg);
                        }

                        break;
                }
            }
            //picBxPrevImg.SizeMode = PictureBoxSizeMode.Zoom;

        }

        public void UpdatePicBx(double multiply = 1)
        {
            if (TFFileImport.Feature.Features.Count < 1) return;
            int width = 3;
            int startPoint = 5;

            //picBxPrevImg.Refresh();
            bool lineNo = TFFileImport.Feature.Features.Where(x => x.Type == TFFileImport.EFeatureType.Line).ToList().Count > 0;

            double minX = lineNo is true ? Math.Min(TFFileImport.Feature.Features.Min(p => p.Point.X), TFFileImport.Feature.Features.Min(p => p.Point2.X)) : TFFileImport.Feature.Features.Min(p => p.Point.X);
            double maxX = lineNo is true ? Math.Max(TFFileImport.Feature.Features.Max(p => p.Point.X), TFFileImport.Feature.Features.Max(p => p.Point2.X)) : TFFileImport.Feature.Features.Max(p => p.Point.X);
            double minY = lineNo is true ? Math.Max(TFFileImport.Feature.Features.Max(p => p.Point.Y), TFFileImport.Feature.Features.Max(p => p.Point2.Y)) : TFFileImport.Feature.Features.Max(p => p.Point.Y);
            double maxY = lineNo is true ? Math.Min(TFFileImport.Feature.Features.Min(p => p.Point.Y), TFFileImport.Feature.Features.Min(p => p.Point2.Y)) : TFFileImport.Feature.Features.Min(p => p.Point.Y);

            if ((maxX - minX) is 0 || (maxY - minY) is 0) return;
            if (double.IsNaN(minX) || double.IsNaN(maxX) || double.IsNaN(minY) || double.IsNaN(maxY)) return;
            Size s = new Size((int)Math.Abs(maxX - minX), (int)Math.Abs(maxY - minY));
            s = s.Height is 0 && s.Width is 0 ? new Size(1, 1) : s;
            float ratio = (float)Math.Min(picBxPrevImg.Width, picBxPrevImg.Height) / (float)Math.Max(s.Width, s.Height);
            ratio *= (float)multiply;
            if (float.IsNaN(ratio) || float.IsInfinity(ratio)) return;

            PointF[] pFs = TFFileImport.Feature.Features.Select(p => new PointF((float)(p.Point.X - minX) * ratio, -(float)(p.Point.Y - minY) * ratio)).ToArray();
            PointF[] pFs1 = TFFileImport.Feature.Features.Select(p => new PointF((float)(p.Point2.X - minX) * ratio, -(float)(p.Point2.Y - minY) * ratio)).ToArray();

            //Image img = new Bitmap((int)Math.Abs(pFs.Max(p => p.X) - pFs.Min(p => p.X)) + startPoint, (int)Math.Abs(pFs.Max(p => p.Y) - pFs.Min(p => p.Y)) + startPoint);
            var sizeMaxX = Math.Max(Math.Abs(pFs.Max(p => p.X) - pFs.Min(p => p.X)), Math.Abs(pFs1.Max(p => p.X) - pFs1.Min(p => p.X)));
            var sizeMaxY = Math.Max(Math.Abs(pFs.Max(p => p.Y) - pFs.Min(p => p.Y)), Math.Abs(pFs1.Max(p => p.Y) - pFs1.Min(p => p.Y)));
            Image img = new Bitmap((int)sizeMaxX + startPoint, (int)sizeMaxY + startPoint);
            Graphics g = Graphics.FromImage(img);

            Pen pen = new Pen(Color.Navy);
            SolidBrush solidBrush = new SolidBrush(Color.Navy);

            for (int i = 0; i < pFs.Length; i++)
            {
                var xy = pFs[i];
                xy = new PointF(xy.X, xy.Y);

                PointF prevPt = new PointF(0, 0);
                if (i > 0)
                {
                    var temp = pFs[i - 1];
                    prevPt = new PointF(temp.X, temp.Y);
                }

                switch (TFFileImport.Feature.Features[i].Type)
                {
                    case TFFileImport.EFeatureType.Pad:
                    case TFFileImport.EFeatureType.Dot:
                        g.DrawEllipse(pen, xy.X, xy.Y, width, width);
                        g.FillEllipse(solidBrush, xy.X, xy.Y, width, width);
                        break;
                    case TFFileImport.EFeatureType.Line:
                        //var prevXY = i is 0 ? xy : new PointF(0, 0);
                        //if (i > 0) prevXY = TFFileImport.Feature.Features[i - 1].Type == TFFileImport.Feature.Features[i].Type ? prevPt : xy;
                        //g.DrawLine(pen, prevXY.X, prevXY.Y, xy.X, xy.Y);
                        g.DrawLine(pen, pFs1[i].X, pFs1[i].Y, pFs[i].X, pFs[i].Y);
                        break;
                    case TFFileImport.EFeatureType.Arc:
                        bool circle = prevPt == xy;
                        var radius = TFFileImport.Feature.Features[i].Radius * ratio;

                        if (circle) g.DrawEllipse(pen, xy.X, xy.Y, (float)radius, (float)radius);
                        else
                        {
                            PointD center = new PointD(radius + xy.X, radius + xy.Y);
                            var angleStart = Math.Atan2((prevPt.Y - center.Y), (prevPt.X - center.X));
                            var angleEnd = Math.Atan2((xy.Y - center.Y), (xy.X - center.X));
                            if (angleStart < 0) angleStart = Math.PI * 2 - angleStart;
                            if (angleEnd < 0) angleEnd = Math.PI * 2 - angleEnd;
                            double rad = angleEnd - angleStart;

                            var sweepDeg = rad * 180 / Math.PI;
                            var startDeg = angleStart * 180 / Math.PI;

                            g.DrawArc(pen, xy.X, xy.Y, (float)radius, (float)radius, (float)startDeg, (float)sweepDeg);
                        }

                        break;
                }
            }

            int tempCount = 0;
            tempCount = TFFileImport.Feature.Features.Where(x => x.ApertureIndex < lbxApertureList.SelectedIndex).Count();

            int y = lbxFeaturesList.SelectedIndex + tempCount;
            pen = new Pen(Color.Red); solidBrush = new SolidBrush(Color.Red);
            if (y >= 0)
            {
                width += 1;
                g.DrawEllipse(pen, pFs[y].X, pFs[y].Y, width, width);
                g.FillEllipse(solidBrush, pFs[y].X, pFs[y].Y, width, width);
            }

            picBxPrevImg.SizeMode = PictureBoxSizeMode.StretchImage;
            picBxPrevImg.Image = img;

        }
        #endregion

        private void lbxFeaturesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePicBx(Scale.Value / 100);
        }
    }
}
