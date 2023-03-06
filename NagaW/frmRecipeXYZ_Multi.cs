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
    public partial class frmRecipeXYZ_Multi : Form
    {
        TEZMCAux.TGroup gantry;

        DPara[] Para = new DPara[10];
        TCmd Tcmd = new TCmd();
        PointD ptBase = new PointD(0, 0);

        public frmRecipeXYZ_Multi()
        {
            InitializeComponent();
        }
        public frmRecipeXYZ_Multi(TEZMCAux.TGroup gantry, PointD ptBase, TCmd tcmd, string title) : this()
        {
            this.gantry = gantry;

            Tcmd = tcmd;
            this.ptBase = ptBase;

            string index = title;

            lblTitle.Text = $"{index} " + tcmd.Cmd.ToString();

            //panel2.Visible = false;

            double min = -999.999;
            double max = 999.999;

            for (int i = 0; i < 10; i++)
            {
                Para[i] = new DPara("", 0, min, max, EUnit.MILLIMETER);
            }

            switch (tcmd.Cmd)
            {
                case ECmd.PATTERN_SPIRAL:
                    lblXY0.Text = "Center XY";
                    lblXY1.Text = "End XY";
                    lblXY2.Text = " ";
                    Para[0] = new DPara($"{index} X0", tcmd.Para[0], min, max, EUnit.MILLIMETER);
                    Para[1] = new DPara($"{index} Y0", tcmd.Para[1], min, max, EUnit.MILLIMETER);
                    Para[2] = new DPara($"{index} X1", tcmd.Para[2], min, max, EUnit.MILLIMETER);
                    Para[3] = new DPara($"{index} Y1", tcmd.Para[3], min, max, EUnit.MILLIMETER);

                    Para[6] = new DPara($"{index} Interval", tcmd.Para[6], 0, 100, EUnit.COUNT, 0);
                    Para[7] = new DPara($"{index} Interval Res", tcmd.Para[7] is 0 ? 1 : tcmd.Para[7], 0, 100, EUnit.PERCENTAGE, 0);
                    Para[8] = new DPara($"{index} Spiral Out", tcmd.Para[8], 0, 1, EUnit.NONE, 0, new string[] { "Spiral Out", "Spiral In" });
                    break;
                case ECmd.PATTERN_RECT_SPIRAL:
                    lblXY0.Text = "Start XY";
                    lblXY1.Text = "End XY 1";
                    lblXY2.Text = "End XY 2";
                    Para[0] = new DPara($"{index} X0", tcmd.Para[0], min, max, EUnit.MILLIMETER);
                    Para[1] = new DPara($"{index} Y0", tcmd.Para[1], min, max, EUnit.MILLIMETER);
                    Para[2] = new DPara($"{index} X1", tcmd.Para[2], min, max, EUnit.MILLIMETER);
                    Para[3] = new DPara($"{index} Y1", tcmd.Para[3], min, max, EUnit.MILLIMETER);
                    Para[4] = new DPara($"{index} X2", tcmd.Para[4], min, max, EUnit.MILLIMETER);
                    Para[5] = new DPara($"{index} Y2", tcmd.Para[5], min, max, EUnit.MILLIMETER);

                    Para[6] = new DPara($"{index} Interval", tcmd.Para[6], 0, 100, EUnit.COUNT, 0);
                    Para[7] = new DPara($"{index} Curvature", tcmd.Para[7] is 0 ? 10 : tcmd.Para[7], 0, 100, EUnit.PERCENTAGE, 0);
                    Para[8] = new DPara($"{index} Spiral Out", tcmd.Para[8], 0, 1, EUnit.NONE, 0, new string[] { "Spiral Out", "Spiral In" });
                    break;
                case ECmd.PATTERN_RECT_FILL:
                    lblXY0.Text = "Start XY";
                    lblXY1.Text = "End XY 1";
                    lblXY2.Text = "End XY 2";
                    Para[0] = new DPara($"{index} X0", tcmd.Para[0], min, max, EUnit.MILLIMETER);
                    Para[1] = new DPara($"{index} Y0", tcmd.Para[1], min, max, EUnit.MILLIMETER);
                    Para[2] = new DPara($"{index} X1", tcmd.Para[2], min, max, EUnit.MILLIMETER);
                    Para[3] = new DPara($"{index} Y1", tcmd.Para[3], min, max, EUnit.MILLIMETER);
                    Para[4] = new DPara($"{index} X2", tcmd.Para[4], min, max, EUnit.MILLIMETER);
                    Para[5] = new DPara($"{index} Y2", tcmd.Para[5], min, max, EUnit.MILLIMETER);

                    Para[6] = new DPara($"{index} Interval", tcmd.Para[6], 0, 100, EUnit.COUNT, 0);
                    Para[7] = new DPara($"{index} Run Path", tcmd.Para[7], 0, 1, EUnit.NONE, 0, Enum.GetNames(typeof(ERunPath)));
                    break;
                case ECmd.PATTERN_CROSS:
                    lblXY0.Text = "Start XY";
                    lblXY1.Text = "End XY 1";
                    lblXY2.Text = "End XY 2";
                    Para[0] = new DPara($"{index} X0", tcmd.Para[0], min, max, EUnit.MILLIMETER);
                    Para[1] = new DPara($"{index} Y0", tcmd.Para[1], min, max, EUnit.MILLIMETER);
                    Para[2] = new DPara($"{index} X1", tcmd.Para[2], min, max, EUnit.MILLIMETER);
                    Para[3] = new DPara($"{index} Y1", tcmd.Para[3], min, max, EUnit.MILLIMETER);
                    Para[4] = new DPara($"{index} X2", tcmd.Para[4], min, max, EUnit.MILLIMETER);
                    Para[5] = new DPara($"{index} Y2", tcmd.Para[5], min, max, EUnit.MILLIMETER);

                    Para[7] = new DPara($"{index} Length Ratio", tcmd.Para[7], 0, 200, EUnit.PERCENTAGE, 0);
                    Para[8] = new DPara($"{index} Cont. Disp", tcmd.Para[8], 0, 1, EUnit.NONE, 0, new string[] { "True", "False" });
                    break;
                case ECmd.PATTERN_STAR:
                    lblXY0.Text = "Start XY";
                    lblXY1.Text = "End XY 1";
                    lblXY2.Text = "End XY 2";
                    Para[0] = new DPara($"{index} X0", tcmd.Para[0], min, max, EUnit.MILLIMETER);
                    Para[1] = new DPara($"{index} Y0", tcmd.Para[1], min, max, EUnit.MILLIMETER);
                    Para[2] = new DPara($"{index} X1", tcmd.Para[2], min, max, EUnit.MILLIMETER);
                    Para[3] = new DPara($"{index} Y1", tcmd.Para[3], min, max, EUnit.MILLIMETER);
                    Para[4] = new DPara($"{index} X2", tcmd.Para[4], min, max, EUnit.MILLIMETER);
                    Para[5] = new DPara($"{index} Y2", tcmd.Para[5], min, max, EUnit.MILLIMETER);

                    Para[6] = new DPara($"{index} Degree", tcmd.Para[6], 0, 360, EUnit.ANGLE, 0);
                    Para[7] = new DPara($"{index} Length Ratio", tcmd.Para[7], 0, 200, EUnit.PERCENTAGE, 0);
                    Para[8] = new DPara($"{index} Cont. Disp", tcmd.Para[8], 0, 1, EUnit.NONE, 0, new string[] { "True", "False" });
                    break;
                case ECmd.PATTERN_MULTIDOT:
                    lblXY0.Text = "Start XY";
                    lblXY1.Text = "End XY 1";
                    lblXY2.Text = "End XY 2";
                    Para[0] = new DPara($"{index} X0", tcmd.Para[0], min, max, EUnit.MILLIMETER);
                    Para[1] = new DPara($"{index} Y0", tcmd.Para[1], min, max, EUnit.MILLIMETER);
                    Para[2] = new DPara($"{index} X1", tcmd.Para[2], min, max, EUnit.MILLIMETER);
                    Para[3] = new DPara($"{index} Y1", tcmd.Para[3], min, max, EUnit.MILLIMETER);
                    Para[4] = new DPara($"{index} X2", tcmd.Para[4], min, max, EUnit.MILLIMETER);
                    Para[5] = new DPara($"{index} Y2", tcmd.Para[5], min, max, EUnit.MILLIMETER);

                    Para[6] = new DPara($"{index} Column", tcmd.Para[6], 0, 250, EUnit.NONE, 0);
                    Para[7] = new DPara($"{index} Row", tcmd.Para[7], 0, 250, EUnit.NONE, 0);
                    Para[8] = new DPara($"{index} Run Path", tcmd.Para[8], 0, 3, EUnit.NONE, 0, Enum.GetNames(typeof(ERunPath)));
                    break;
            }

            panel3.Visible = lblXY2.Text != " ";

            pnlPara6.Visible = Para[6].Name.Length > 0;
            pnlPara7.Visible = Para[7].Name.Length > 0;
            pnlPara8.Visible = Para[8].Name.Length > 0;

            lblParaDesc6.Text = Para[6].Name.Replace(index, "");
            lblParaDesc7.Text = Para[7].Name.Replace(index, "");
            lblParaDesc8.Text = Para[8].Name.Replace(index, "");
        }

        private void frmRecipeXYZ_Multi_Load(object sender, EventArgs e)
        {
            UpdateDisplay();
            GControl.LogForm(this);
        }
        private void UpdateDisplay()
        {
            lblX0.Text = $"{Tcmd.Para[0]:f3}";
            lblY0.Text = $"{Tcmd.Para[1]:f3}";

            lblX1.Text = $"{Tcmd.Para[2]:f3}";
            lblY1.Text = $"{Tcmd.Para[3]:f3}";

            lblX2.Text = $"{Tcmd.Para[4]:f3}";
            lblY2.Text = $"{Tcmd.Para[5]:f3}";

            lblPara6.UpdatePara(Para[6]);
            lblPara7.UpdatePara(Para[7]);
            lblPara8.UpdatePara(Para[8]);

            UpdatePattern();
        }
        private void UpdatePattern()
        {
            List<PointD> pts = new List<PointD>();
            switch (Tcmd.Cmd)
            {
                case ECmd.PATTERN_RECT_FILL: pts = PatternDisp.SquareFilling(Tcmd); break;
                case ECmd.PATTERN_RECT_SPIRAL: pts = PatternDisp.SquareSpiral(Tcmd); break;
                case ECmd.PATTERN_SPIRAL: pts = PatternDisp.SpiralDisplay(Tcmd); break;
                case ECmd.PATTERN_STAR: pts = PatternDisp.Star(Tcmd); break;
                case ECmd.PATTERN_CROSS: pts = PatternDisp.Star(Tcmd, true); break;
                case ECmd.PATTERN_MULTIDOT: return;
            }

            if (pts.Count is 0) return;

            double minX = pts.Min(p => p.X);
            double maxX = pts.Max(p => p.X);
            double minY = pts.Max(p => p.Y);
            double maxY = pts.Min(p => p.Y);

            if ((maxX - minX) is 0 || (maxY - minY) is 0) return;
            if (double.IsNaN(minX) || double.IsNaN(maxX) || double.IsNaN(minY) || double.IsNaN(maxY)) return;
            Size s = new Size((int)Math.Abs(maxX - minX), (int)Math.Abs(maxY - minY));
            float ratio = (float)Math.Min(picBxPreviewImg.Width, picBxPreviewImg.Height) / (float)Math.Max(s.Width, s.Height);
            if (float.IsNaN(ratio) || float.IsInfinity(ratio)) return;
            PointF[] pFs = pts.Select(p => new PointF((float)(p.X - minX) * ratio, -(float)(p.Y - minY) * ratio)).ToArray();

            Image img = new Bitmap((int)Math.Abs(pFs.Max(p => p.X) - pFs.Min(p => p.X)) + 2, (int)Math.Abs(pFs.Max(p => p.Y) - pFs.Min(p => p.Y)) + 2);
            Graphics g = Graphics.FromImage(img);
            g.DrawLines(new Pen(Color.Navy), pFs);

            //var temp = new TCmd(Tcmd);
            //PatternDisp.PatternShrink(1, 1, ref temp);
            //PointF[] temp1 = new PointF[] { new PointF((float)temp.Para[4], (float)temp.Para[5]), new PointF((float)temp.Para[0], (float)temp.Para[1]), new PointF((float)temp.Para[2], (float)temp.Para[3]) };
            //var a = temp1.Select(x => new PointF(-(float)(x.X - minX) * ratio, (float)(x.Y - minY) * ratio)).ToArray();
            //g.DrawLines(new Pen(Color.Red), a);

            picBxPreviewImg.SizeMode = PictureBoxSizeMode.Zoom;
            picBxPreviewImg.Image = img;
        }

        private void lblX0_Click(object sender, EventArgs e)
        {
            if (GLog.SetPara(ref Para[0])) Tcmd.Para[0] = Para[0].Value;
            UpdateDisplay();
        }
        private void lblY0_Click(object sender, EventArgs e)
        {
            if (GLog.SetPara(ref Para[1])) Tcmd.Para[1] = Para[1].Value;
            UpdateDisplay();
        }

        private void lblX1_Click(object sender, EventArgs e)
        {
            if (GLog.SetPara(ref Para[2])) Tcmd.Para[2] = Para[2].Value;
            UpdateDisplay();
        }
        private void lblY1_Click(object sender, EventArgs e)
        {
            if (GLog.SetPara(ref Para[3])) Tcmd.Para[3] = Para[3].Value;
            UpdateDisplay();
        }

        private void lblY2_Click(object sender, EventArgs e)
        {
            if (GLog.SetPara(ref Para[5])) Tcmd.Para[5] = Para[5].Value;
            UpdateDisplay();
        }
        private void lblX2_Click(object sender, EventArgs e)
        {
            if (GLog.SetPara(ref Para[4])) Tcmd.Para[4] = Para[4].Value;
            UpdateDisplay();
        }

        private void lblPara6_Click(object sender, EventArgs e)
        {
            if (GLog.SetPara(ref Para[6])) Tcmd.Para[6] = Para[6].Value;
            UpdateDisplay();
        }
        private void lblPara7_Click(object sender, EventArgs e)
        {
            if (GLog.SetPara(ref Para[7])) Tcmd.Para[7] = Para[7].Value;
            UpdateDisplay();
        }
        private void lblPara8_Click(object sender, EventArgs e)
        {
            if (GLog.SetPara(ref Para[8])) Tcmd.Para[8] = Para[8].Value;
            UpdateDisplay();
        }

        private void btnSetXY0_Click(object sender, EventArgs e)
        {
            var oldPos = new PointXYZ(Tcmd.Para[0], Tcmd.Para[1], 0);

            var pos = new PointXYZ();
            pos.X = gantry.Axis[0].ActualPos;
            pos.Y = gantry.Axis[1].ActualPos;
            pos.Z = gantry.Axis[2].ActualPos;

            var newPos = new PointXYZ(pos.X - ptBase.X, pos.Y - ptBase.Y, pos.Z);
            Para[0].Value = Tcmd.Para[0] = newPos.X;
            Para[1].Value = Tcmd.Para[1] = newPos.Y;

            GLog.WriteLog(ELogType.PARA, $"XY0 {oldPos.ToStringForDisplay()} " + $"=> {newPos.ToStringForDisplay()}");

            UpdateDisplay();
        }
        private void btnSetXY1_Click(object sender, EventArgs e)
        {
            var oldPos = new PointXYZ(Tcmd.Para[2], Tcmd.Para[3], 0);

            var pos = new PointXYZ();
            pos.X = gantry.Axis[0].ActualPos;
            pos.Y = gantry.Axis[1].ActualPos;
            pos.Z = gantry.Axis[2].ActualPos;

            var newPos = new PointXYZ(pos.X - ptBase.X, pos.Y - ptBase.Y, pos.Z);
            Para[2].Value = Tcmd.Para[2] = newPos.X;
            Para[3].Value = Tcmd.Para[3] = newPos.Y;

            GLog.WriteLog(ELogType.PARA, $"XY1 {oldPos.ToStringForDisplay()} " + $"=> {newPos.ToStringForDisplay()}");

            UpdateDisplay();
        }
        private void btnSetXY2_Click(object sender, EventArgs e)
        {
            var oldPos = new PointXYZ(Tcmd.Para[4], Tcmd.Para[5], 0);

            var pos = new PointXYZ();
            pos.X = gantry.Axis[0].ActualPos;
            pos.Y = gantry.Axis[1].ActualPos;
            pos.Z = gantry.Axis[2].ActualPos;

            var newPos = new PointXYZ(pos.X - ptBase.X, pos.Y - ptBase.Y, pos.Z);
            Para[4].Value = Tcmd.Para[4] = newPos.X;
            Para[5].Value = Tcmd.Para[5] = newPos.Y;

            GLog.WriteLog(ELogType.PARA, $"XY1 {oldPos.ToStringForDisplay()} " + $"=> {newPos.ToStringForDisplay()}");

            UpdateDisplay();
        }

        private void btnGotoXY0_Click(object sender, EventArgs e)
        {
            PointD ptPos = new PointD(ptBase);

            ptPos.X = ptBase.X + Tcmd.Para[0];
            ptPos.Y = ptBase.Y + Tcmd.Para[1];
                        
            TFLightCtrl.LightPair[gantry.Index].Set(GRecipes.Board[gantry.Index].LightDefault);

            GControl.UI_Disable();
            try
            {
                gantry.MoveOpXYAbs(ptPos.ToArray);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            finally
            {
                GControl.UI_Enable();
            }
        }
        private void btnGotoXY1_Click(object sender, EventArgs e)
        {
            PointD ptPos = new PointD(ptBase);

            ptPos.X = ptBase.X + Tcmd.Para[2];
            ptPos.Y = ptBase.Y + Tcmd.Para[3];

            TFLightCtrl.LightPair[gantry.Index].Set(GRecipes.Board[gantry.Index].LightDefault);

            GControl.UI_Disable();
            try
            {
                gantry.MoveOpXYAbs(ptPos.ToArray);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            finally
            {
                GControl.UI_Enable();
            }
        }
        private void btnGotoXY2_Click(object sender, EventArgs e)
        {
            PointD ptPos = new PointD(ptBase);

            ptPos.X = ptBase.X + Tcmd.Para[4];
            ptPos.Y = ptBase.Y + Tcmd.Para[5];

            TFLightCtrl.LightPair[gantry.Index].Set(GRecipes.Board[gantry.Index].LightDefault);

            GControl.UI_Disable();
            try
            {
                gantry.MoveOpXYAbs(ptPos.ToArray);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            finally
            {
                GControl.UI_Enable();
            }
        }

        private void cbxDisp_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
