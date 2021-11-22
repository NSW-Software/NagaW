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
    public partial class frmRecipeXYZ : Form
    {
        TEZMCAux.TGroup gantry;

        DPara[] Para = new DPara[10];
        TCmd Tcmd = new TCmd();
        PointD ptBase = new PointD(0, 0);

        public frmRecipeXYZ()
        {
            InitializeComponent();
        }
        public frmRecipeXYZ(TEZMCAux.TGroup gantry, PointD ptBase, TCmd tcmd, string title) : this()
        {
            this.gantry = gantry;

            Tcmd = tcmd;
            this.ptBase = ptBase;

            string index = title;

            lblTitle.Text = $"{index} " + tcmd.Cmd.ToString();

            cbxDisp.Visible = false;
            panel2.Visible = false;

            double min = -999.999;
            double max = 999.999;

            for (int i = 0; i < 10; i++)
            {
                Para[i] = new DPara("", 0, min, max, EUnit.MILLIMETER);
            }

            switch (tcmd.Cmd)
            {
                case ECmd.DOT:
                    cbxDisp.Visible = true;
                    lblXY0.Text = "DOT XY";
                    Para[0] = new DPara($"{index} Dot X", tcmd.Para[0], min, max, EUnit.MILLIMETER);
                    Para[1] = new DPara($"{index} Dot Y", tcmd.Para[1], min, max, EUnit.MILLIMETER);
                    break;
                case ECmd.LINE_START:
                    cbxDisp.Visible = true;
                    lblXY0.Text = "START XY";
                    Para[0] = new DPara($"{index} Start X", tcmd.Para[0], min, max, EUnit.MILLIMETER);
                    Para[1] = new DPara($"{index} Start Y", tcmd.Para[1], min, max, EUnit.MILLIMETER);
                    break;
                case ECmd.LINE_PASS:
                    cbxDisp.Visible = true;
                    lblXY0.Text = "PASS XY";
                    Para[0] = new DPara($"{index} Pass X", tcmd.Para[0], min, max, EUnit.MILLIMETER);
                    Para[1] = new DPara($"{index} Pass Y", tcmd.Para[1], min, max, EUnit.MILLIMETER);
                    Para[7] = new DPara($"{index} Corner Radius", tcmd.Para[7], 0, 5, EUnit.MILLIMETER);
                    Para[8] = new DPara($"{index} Corner Speed", tcmd.Para[8], 0, 50, EUnit.MILLIMETER_PER_SECOND);
                    Para[9] = new DPara($"{index} Early Cut", tcmd.Para[9], 0, 5, EUnit.MILLIMETER);
                    break;
                case ECmd.LINE_END:
                    cbxDisp.Visible = true;
                    lblXY0.Text = "END XY";
                    Para[0] = new DPara($"{index} End X", tcmd.Para[0], min, max, EUnit.MILLIMETER);
                    Para[1] = new DPara($"{index} End Y", tcmd.Para[1], min, max, EUnit.MILLIMETER);
                    Para[9] = new DPara($"{index} Early Cut", tcmd.Para[9], 0, 5, EUnit.MILLIMETER);
                    break;
                case ECmd.CIRC_CENTER:
                    cbxDisp.Visible = true;
                    lblXY0.Text = "CENTER XY";
                    Para[0] = new DPara($"{index} Center X", tcmd.Para[0], min, max, EUnit.MILLIMETER);
                    Para[1] = new DPara($"{index} Center Y", tcmd.Para[1], min, max, EUnit.MILLIMETER);
                    Para[7] = new DPara($"{index} Direction", tcmd.Para[7], 0, 1, EUnit.CWCCW);
                    break;
                case ECmd.CIRC_PASS:
                    cbxDisp.Visible = true;
                    lblXY0.Text = "PASS1 XY";
                    Para[0] = new DPara($"{index} Pass1 X", tcmd.Para[0], min, max, EUnit.MILLIMETER);
                    Para[1] = new DPara($"{index} Pass1 Y", tcmd.Para[1], min, max, EUnit.MILLIMETER);
                    lblXY1.Text = "PASS2 XY";
                    Para[0] = new DPara($"{index} Pass2 X", tcmd.Para[3], min, max, EUnit.MILLIMETER);
                    Para[1] = new DPara($"{index} Pass2 Y", tcmd.Para[4], min, max, EUnit.MILLIMETER);
                    panel2.Visible = true;
                    break;
                case ECmd.ARC_PASS:
                    cbxDisp.Visible = true;
                    lblXY0.Text = "PASS1 XY";
                    Para[0] = new DPara($"{index} Pass1 X", tcmd.Para[0], min, max, EUnit.MILLIMETER);
                    Para[1] = new DPara($"{index} Pass1 Y", tcmd.Para[1], min, max, EUnit.MILLIMETER);
                    lblXY1.Text = "PASS2 XY";
                    Para[0] = new DPara($"{index} End2 X", tcmd.Para[3], min, max, EUnit.MILLIMETER);
                    Para[1] = new DPara($"{index} End2 Y", tcmd.Para[4], min, max, EUnit.MILLIMETER);
                    panel2.Visible = true;
                    break;
                case ECmd.DYNAMIC_SAVE_IMAGE:
                    lblXY0.Text = "POS XY";
                    Para[0] = new DPara($"{index} Pass1 X", tcmd.Para[0], min, max, EUnit.MILLIMETER);
                    Para[1] = new DPara($"{index} Pass1 Y", tcmd.Para[1], min, max, EUnit.MILLIMETER);
                    Para[6] = new DPara($"{index} Speed", tcmd.Para[6], 1, 250, EUnit.MILLIMETER_PER_SECOND);
                    break;
                case ECmd.DYNAMIC_JET_DOT:
                case ECmd.DYNAMIC_JET_DOT_SW:
                    cbxDisp.Visible = true;
                    lblXY0.Text = $"DYNAMICJET DOT {(tcmd.Cmd == ECmd.DYNAMIC_JET_DOT_SW ? "SW" : "")} XY";
                    Para[0] = new DPara($"{index} JetDot X", tcmd.Para[0], min, max, EUnit.MILLIMETER);
                    Para[1] = new DPara($"{index} JetDot Y", tcmd.Para[1], min, max, EUnit.MILLIMETER);
                    //var dirs = new string[4] { "X+", "X-", "Y+", "Y-" };
                    var dirs = Enum.GetNames(typeof(EDynamicJetDir));
                    Para[7] = new DPara($"{index} Direction", Math.Max(0, tcmd.Para[7]), 0, dirs.Length - 1, EUnit.MILLIMETER, 0, dirs);
                    break;
            }

            pnlPara6.Visible = Para[6].Name.Length > 0;
            pnlPara7.Visible = Para[7].Name.Length > 0;
            pnlPara8.Visible = Para[8].Name.Length > 0;
            pnlPara9.Visible = Para[9].Name.Length > 0;

            lblParaDesc6.Text = Para[6].Name.Replace(index, "");
            lblParaDesc7.Text = Para[7].Name.Replace(index, "");
            lblParaDesc8.Text = Para[8].Name.Replace(index, "");
            lblParaDesc9.Text = Para[9].Name.Replace(index, "");
        }

        private void frmRecipeXYZ_Load(object sender, EventArgs e)
        {
            UpdateDisplay();
            GControl.LogForm(this);
        }

        private void UpdateDisplay()
        {
            cbxDisp.Checked = Tcmd.Para[6] is 0;

            lblX0.Text = $"{Tcmd.Para[0]:f3}";
            lblY0.Text = $"{Tcmd.Para[1]:f3}";

            lblX1.Text = $"{Tcmd.Para[3]:f3}";
            lblY1.Text = $"{Tcmd.Para[4]:f3}";
 
            lblPara6.UpdatePara(Para[6]);
            lblPara7.UpdatePara(Para[7]);
            lblPara8.UpdatePara(Para[8]);
            lblPara9.UpdatePara(Para[9]);

            lblVirtualPos.Visible = frmRecipeLayout.EnableDynamicJetSWSetXY;
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
            if (GLog.SetPara(ref Para[3])) Tcmd.Para[3] = Para[3].Value;
            UpdateDisplay();
        }
        private void lblY1_Click(object sender, EventArgs e)
        {
            if (GLog.SetPara(ref Para[4])) Tcmd.Para[4] = Para[4].Value;
            UpdateDisplay();
        }

        private void btnSetXY0_Click(object sender, EventArgs e)
        {
            var oldPos = new PointXYZ(Tcmd.Para[0], Tcmd.Para[1], Tcmd.Para[2]);

            var pos = new PointXYZ();
            pos.X = gantry.Axis[0].ActualPos;
            pos.Y = gantry.Axis[1].ActualPos;
            pos.Z = gantry.Axis[2].ActualPos;

            if (frmRecipeLayout.EnableDynamicJetSWSetXY)
            {
                var virtualpos = frmRecipeLayout.VirtualStartPos;

                var newPosV = new PointXYZ(pos.X - virtualpos.X, pos.Y - virtualpos.Y, 0);
                Para[0].Value = Tcmd.Para[0] = newPosV.X;
                Para[1].Value = Tcmd.Para[1] = newPosV.Y;

                GLog.WriteLog(ELogType.PARA, $"XY0 {oldPos.ToStringForDisplay()} " + $"=> {newPosV.ToStringForDisplay()}");

                UpdateDisplay();

                return;
            }

            var newPos = new PointXYZ(pos.X - ptBase.X, pos.Y - ptBase.Y, pos.Z);
            Para[0].Value = Tcmd.Para[0] = newPos.X;
            Para[1].Value = Tcmd.Para[1] = newPos.Y;

            GLog.WriteLog(ELogType.PARA, $"XY0 {oldPos.ToStringForDisplay()} " + $"=> {newPos.ToStringForDisplay()}");

            UpdateDisplay();
        }
        private void btnSetXY1_Click(object sender, EventArgs e)
        {
            var oldPos = new PointXYZ(Tcmd.Para[3], Tcmd.Para[4], Tcmd.Para[5]);

            var pos = new PointXYZ();
            pos.X = gantry.Axis[0].ActualPos;
            pos.Y = gantry.Axis[1].ActualPos;
            pos.Z = gantry.Axis[2].ActualPos;

            var newPos = new PointXYZ(pos.X - ptBase.X, pos.Y - ptBase.Y, pos.Z);
            Para[3].Value = Tcmd.Para[3] = newPos.X;
            Para[4].Value = Tcmd.Para[4] = newPos.Y;

            GLog.WriteLog(ELogType.PARA, $"XY1 {oldPos.ToStringForDisplay()} " + $"=> {newPos.ToStringForDisplay()}");

            UpdateDisplay();
        }

        private void btnGotoXY0_Click(object sender, EventArgs e)
        {
            PointD ptPos = new PointD(ptBase);

            ptPos.X = ptBase.X + Tcmd.Para[0];
            ptPos.Y = ptBase.Y + Tcmd.Para[1];


            if (frmRecipeLayout.EnableDynamicJetSWSetXY)
            {
                var virtualpos = frmRecipeLayout.VirtualStartPos;

                ptPos.X = virtualpos.X + Tcmd.Para[0];
                ptPos.Y = virtualpos.Y + Tcmd.Para[1];
            }

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

            ptPos.X = ptBase.X + Tcmd.Para[3];
            ptPos.Y = ptBase.Y + Tcmd.Para[4];

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

        private void cbxDisp_Click(object sender, EventArgs e)
        {
            Tcmd.Para[6] = cbxDisp.Checked ? 0 : 1;
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
        private void lblPara9_Click(object sender, EventArgs e)
        {
            if (GLog.SetPara(ref Para[9])) Tcmd.Para[9] = Para[9].Value;
            UpdateDisplay();
        }
    }
}
