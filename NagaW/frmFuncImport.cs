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
    public partial class frmFuncImport : Form
    {
        public int DatasetIdx = 0;

        public frmFuncImport()
        {
            InitializeComponent();
            FirstLoad();
        }
        
        private void FirstLoad()
        {
            if (TFFuncImport.Functions.Count is 0) return;
            lbxDataSet.DataSource = TFFuncImport.Functions.Select(x => x.Function.Name).ToList();
            lbxPoints.DataSource = TFFuncImport.Functions[DatasetIdx].Function.Cmds.Select(x => new PointD(x.Para[0], x.Para[1])).ToList();

            UpdateDisplay();
        }
        private void UpdateDisplay()
        {
            lblScaleX.Text = TFFuncStat.Scale.X.ToString();
            lblScaleY.Text = TFFuncStat.Scale.Y.ToString();
            lblOrientation.UpdatePara(TFFuncStat.Orientation);

            lbxPoints.DataSource = TFFuncImport.Functions[DatasetIdx].Function.Cmds.Select(x => new PointD(x.Para[0], x.Para[1])).ToList();

            UpdatePicBx();
        }
        private void UpdatePicBx()
        {
            if (TFFuncImport.Functions.Count <= 0) return;

            var scaleX = TFFuncStat.Scale.X; var scaleY = TFFuncStat.Scale.Y;
            var orientation = TFFuncStat.Orientation.Value;
            pnlGraph.Refresh();

            Panel temp = new Panel();
            temp.Refresh();

            var x_val = TFFuncImport.Functions[DatasetIdx].Function.Cmds.Select(x => x.Para[0]);
            var y_val = TFFuncImport.Functions[DatasetIdx].Function.Cmds.Select(x => x.Para[1]);
            var largest_X = Math.Max(Math.Abs(x_val.Min()), Math.Abs(x_val.Max())) + 10 + scaleX;
            var largest_Y = Math.Max(Math.Abs(y_val.Min()), Math.Abs(y_val.Max())) + 10 + scaleY;
            var largest_Size = Math.Max(largest_X, largest_Y);

            temp.Size = new Size((int)(largest_Size * 2), (int)(largest_Size * 2));
            var midX = temp.Width / 2;
            var midY = temp.Height / 2;
            var pnlGraph_midX = pnlGraph.Width / 2;
            var pnlGraph_midY = pnlGraph.Height / 2;
            var ratioX = pnlGraph_midX / midX;
            var ratioY = pnlGraph_midY / midY;

            //Draw
            Graphics g = pnlGraph.CreateGraphics();
            Pen p_pt = new Pen(Color.Black);
            Pen p_refpt = new Pen(Color.Red);
            SolidBrush sb_pt = new SolidBrush(Color.Black);
            SolidBrush sb_refpt = new SolidBrush(Color.Red);

            foreach (var a in TFFuncImport.Functions[DatasetIdx].Function.Cmds)
            {
                //var scaleX = scale;
                //var scaleY = scale;
                var pointX = a.Para[0];
                var pointY = a.Para[1];

                if (pointX > 0) pointX += scaleX;
                else if (pointX < 0) pointX -= scaleX;

                if (pointY > 0) pointY += scaleY;
                else if (pointY < 0) pointY -= scaleY;

                double newX = pointX;
                double newY = pointY;

                var x = midX + newX;
                var y = midY - newY;
                if (orientation > 0)
                {
                    double rad = (360 - orientation) * Math.PI / 180;
                    var radius = Math.Sqrt(Math.Pow(pointX, 2) + Math.Pow(pointY, 2));
                    x = midX + (pointX * Math.Cos(rad) - pointY * Math.Sin(rad));
                    y = midY - (pointX * Math.Sin(rad) + pointY * Math.Cos(rad));
                }

                x = x * ratioX;
                y = y * ratioY;
                g.DrawEllipse(p_pt, (float)x, (float)y, 7, 7);
                g.FillEllipse(sb_pt, (float)x, (float)y, 7, 7);
            }

            var refptX = (midX + TFFuncImport.Functions[DatasetIdx].StartPos.X) * ratioX;
            var refptY = (midY + TFFuncImport.Functions[DatasetIdx].StartPos.Y) * ratioY;
            g.DrawEllipse(p_refpt, (float)refptX, (float)refptY, 7, 7);
            g.FillEllipse(sb_refpt, (float)refptX, (float)refptY, 7, 7);
        }

        DPara TempX = new DPara("ScaleX", 1, 1, 1000, EUnit.NONE);
        DPara TempY = new DPara("ScaleY", 1, 1, 1000, EUnit.NONE);
        private void lblScale_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref TempX);
            TFFuncStat.Scale.X = TempX.Value;
            UpdateDisplay();
        }
        private void lblScaleY_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref TempY);
            TFFuncStat.Scale.Y = TempY.Value;
            UpdateDisplay();
        }
        private void lblOrientation_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref TFFuncStat.Orientation);
            UpdateDisplay();
        }

        private void lbxDataSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            DatasetIdx = lbxDataSet.SelectedIndex;
            UpdateDisplay();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            TFFuncStat.SelectedIdx = DatasetIdx;
            this.Close();
        }

    }
}
