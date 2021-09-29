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
    public partial class frmRecipeHeightAlign : Form
    {
        TEZMCAux.TGroup gantry;

        PointD ptBase = new PointD(0, 0);
        TCmd Tcmd = new TCmd();
        DPara[] Para = new DPara[10];

        public frmRecipeHeightAlign()
        {
            InitializeComponent();
        }
        public frmRecipeHeightAlign(TEZMCAux.TGroup gantry, PointD ptBase, TCmd tcmd, int cmdIndex) : this()
        {
            this.gantry = gantry;

            Tcmd = tcmd;
            this.ptBase = ptBase;

            string index = $"{cmdIndex:000}";

            lblTitle.Text = $"{index} " + tcmd.Cmd.ToString();

            double min = -999.999;
            double max = 999.999;
            for (int i = 0; i < 10; i++)
            {
                Para[i] = new DPara("", 0, min, max, EUnit.MILLIMETER);
            }

            switch (tcmd.Cmd)
            {
                case ECmd.HEIGHT_ALIGN_UNIT:
                case ECmd.HEIGHT_ALIGN_CLUSTER:
                case ECmd.HEIGHT_ALIGN_BOARD:
                    {
                        Para[0] = new DPara($"{index} Rel X0", tcmd.Para[0], min, max, EUnit.MILLIMETER);
                        Para[1] = new DPara($"{index} Rel Y0", tcmd.Para[1], min, max, EUnit.MILLIMETER);
                    }
                    break;
            }
        }


        private void frmRecipeHeightAlign_Load(object sender, EventArgs e)
        {
            lblXY0.Text = "Position";
            UpdateDisplay();
            GControl.LogForm(this);
        }

        private void UpdateDisplay()
        {
            lblX0.Text = $"{Tcmd.Para[0]:f3}";
            lblY0.Text = $"{Tcmd.Para[1]:f3}";
        }

        private void btnSetXY0_Click(object sender, EventArgs e)
        {
            var oldPos = new PointXYZ(Tcmd.Para[0], Tcmd.Para[1], Tcmd.Para[2]);

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

        THeightData heightData = new THeightData();
        private async void btnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                Inst.TBoard instBoard = Inst.Board[gantry.Index];
                PointD pt = ptBase + new PointD(Tcmd.Para[0], Tcmd.Para[1]) + GSetupPara.Calibration.LaserOffset[gantry.Index];
                await Task.Run(() => GRecipes.Functions[gantry.Index][instBoard.FuncNo].HeightAlignExecute(gantry, pt, Tcmd, ref heightData));

                string res = heightData.Status.ToString();
                res = $"Value {heightData.SensorValue:f5}";
                rtbxResult.Text = res;
                UpdateDisplay();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            finally
            {
            }
        }
    }
}
