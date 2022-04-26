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
    public partial class frmRecipePatAlign : Form
    {
        TEZMCAux.TGroup gantry;

        PointD ptBase = new PointD(0, 0);
        DPara[] Para = new DPara[10];
        TCmd Tcmd = new TCmd();
        int iOpt2Pts = 0x00000001;


        public frmRecipePatAlign()
        {
            InitializeComponent();
        }

        public frmRecipePatAlign(TEZMCAux.TGroup gantry, PointD ptBase, TCmd tcmd, int cmdIndex) : this()
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

            Para[0] = new DPara($"{index} Rel X0", Tcmd.Para[0], -999, 999, EUnit.MILLIMETER, 4);
            Para[1] = new DPara($"{index} Rel Y0", Tcmd.Para[1], -999, 999, EUnit.MILLIMETER, 4);
            Para[3] = new DPara($"{index} Rel X1", Tcmd.Para[3], -999, 999, EUnit.MILLIMETER, 4);
            Para[4] = new DPara($"{index} Rel Y1", Tcmd.Para[4], -999, 999, EUnit.MILLIMETER, 4);

            Para[6] = new DPara($"{index} Min Score", Tcmd.Para[6], 0, 1, EUnit.NONE, 2);
            Para[7] = new DPara($"{index} Max offset", Tcmd.Para[7], -999, 999, EUnit.MILLIMETER);
            Para[8] = new DPara($"{index} Max Angle", Tcmd.Para[8], -360, 360, EUnit.ANGLE, 0);
        }

        private void frmRecipePatAlign_Load(object sender, EventArgs e)
        {
            lblXY0.Text = "Pattern 1";
            lblXY1.Text = "Pattern 2";

            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            lblPatternID.Text = Tcmd.ID.ToString();

            lblX0.UpdatePara(Para[0]);
            lblY0.UpdatePara(Para[1]);

            lblX1.UpdatePara(Para[3]);
            lblY1.UpdatePara(Para[4]);

            int iOption = (int)Tcmd.Para[9];
            bool enablePt2 = (iOption & iOpt2Pts) == iOpt2Pts;
            cb2Points.Checked = enablePt2;

            cb2PointsWithPat1Img.Checked = Tcmd.Para[9] is 10;
            btnLearn2.Visible = !cb2PointsWithPat1Img.Checked;
            //pnlPt2.Visible = enablePt2;
            pnlPt2.Visible = Tcmd.Para[9] > 0;


            lblMinScore.UpdatePara(Para[6]);
            lblMaxXYOffset.UpdatePara(Para[7]);
            lblMaxAngle.UpdatePara(Para[8]);

            btnClear.BackColor = alignData.Status == EPatAlignStatus.None ? SystemColors.Control : Color.Orange;
        }


        private void lblPatternID_Click(object sender, EventArgs e)
        {
            IPara para = new IPara("ID", Tcmd.ID, 0, 9, EUnit.NONE);

            if (GLog.SetPara(ref para)) Tcmd.ID = para.Value;
            UpdateDisplay();
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
            pos.Z = gantry.Axis[2].ActualPos - GSystemCfg.Camera.Cameras[gantry.Index].DefaultFocusZ;

            var newPos = new PointXYZ(pos.X - ptBase.X, pos.Y - ptBase.Y, pos.Z);
            Tcmd.Para[0] = newPos.X;
            Tcmd.Para[1] = newPos.Y;
            Tcmd.Para[2] = newPos.Z;
            Para[0].Value = Tcmd.Para[0];
            Para[1].Value = Tcmd.Para[1];
            Para[2].Value = Tcmd.Para[2];
            GLog.WriteLog(ELogType.PARA, $"XY0 {oldPos.ToStringForDisplay()} " + $"=> {newPos.ToStringForDisplay()}");

            UpdateDisplay();
        }
        private void btnSetXY1_Click(object sender, EventArgs e)
        {
            var oldPos = new PointXYZ(Tcmd.Para[3], Tcmd.Para[4], Tcmd.Para[5]);

            var pos = new PointXYZ();
            pos.X = gantry.Axis[0].ActualPos;
            pos.Y = gantry.Axis[1].ActualPos;
            pos.Z = gantry.Axis[2].ActualPos - GSystemCfg.Camera.Cameras[gantry.Index].DefaultFocusZ;

            var newPos = new PointXYZ(pos.X - ptBase.X, pos.Y - ptBase.Y, pos.Z);
            Tcmd.Para[3] = newPos.X;
            Tcmd.Para[4] = newPos.Y;
            Tcmd.Para[5] = newPos.Z;
            Para[3].Value = Tcmd.Para[3];
            Para[4].Value = Tcmd.Para[4];
            Para[5].Value = Tcmd.Para[5];
            GLog.WriteLog(ELogType.PARA, $"XY1 {oldPos.ToStringForDisplay()} " + $"=> {newPos.ToStringForDisplay()}");

            UpdateDisplay();
        }

        private void btnGotoXY0_Click(object sender, EventArgs e)
        {
            PointD ptPos = new PointD(ptBase);

            PointD relPos = new PointD(Tcmd.Para[0], Tcmd.Para[1]);
            PointD ptRel = TFVision.Translate(relPos, alignData);
            ptPos += ptRel;

            TFLightCtrl.LightPair[gantry.Index].Set(GRecipes.Lighting[gantry.Index][Tcmd.ID]);

            GControl.UI_Disable();
            try
            {
                //gantry.MoveOpZAbs(GSystemCfg.Camera.Cameras[gantry.Index].DefaultFocusZ + Tcmd.Para[2]);
                gantry.MoveOpZAbs(GRecipes.Board[gantry.Index].StartPos.Z);
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

            PointD relPos = new PointD(Tcmd.Para[3], Tcmd.Para[4]);
            PointD ptRel = TFVision.Translate(relPos, alignData);
            ptPos += ptRel;

            TFLightCtrl.LightPair[gantry.Index].Set(GRecipes.Lighting[gantry.Index][Tcmd.ID]);

            GControl.UI_Disable();
            try
            {
                //gantry.MoveOpZAbs(GSystemCfg.Camera.Cameras[gantry.Index].DefaultFocusZ + Tcmd.Para[5]);
                gantry.MoveOpZAbs(GRecipes.Board[gantry.Index].StartPos.Z);
                gantry.MoveOpXYAbs(ptPos.ToArray);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            finally
            {
                GControl.UI_Enable();
            }
        }

        private void cb2Points_Click(object sender, EventArgs e)
        {
            //int iOption = (int)Tcmd.Para[9];
            //iOption = iOption ^ iOpt2Pts;
            //Tcmd.Para[9] = (double)iOption;
            Tcmd.Para[9] = Tcmd.Para[9] is 1 ? 0 : 1;

            UpdateDisplay();
        }

        private void lblMinScore_Click(object sender, EventArgs e)
        {
            if (GLog.SetPara(ref Para[6])) Tcmd.Para[6] = Para[6].Value;
            UpdateDisplay();
        }
        private void lblMaxXYOffset_Click(object sender, EventArgs e)
        {
            if (GLog.SetPara(ref Para[7])) Tcmd.Para[7] = Para[7].Value;
            UpdateDisplay();
        }
        private void lblMaxAngle_Click(object sender, EventArgs e)
        {
            if (GLog.SetPara(ref Para[8])) Tcmd.Para[8] = Para[8].Value;
            UpdateDisplay();
        }

        TAlignData alignData = new TAlignData();
        private void btnLearn_Click(object sender, EventArgs e)
        {
            Emgu.CV.Image<Emgu.CV.Structure.Gray, byte> img = null;

            try
            {
                TFCameras.Camera[gantry.Index].FlirCamera.Snap();
                img = TFCameras.Camera[gantry.Index].FlirCamera.emgucvImage.Clone();
                TFCameras.Camera[gantry.Index].FlirCamera.Live();

                int id = Tcmd.ID;

                while (GRecipes.PatRecog[gantry.Index].Count <= id)
                {
                    GRecipes.PatRecog[gantry.Index].Add(new TPatRect());
                }

                Rectangle[] rects = new Rectangle[2] { GRecipes.PatRecog[gantry.Index][id].SearchRect[0], GRecipes.PatRecog[gantry.Index][id].PatRect[0] };
                int thld = GRecipes.PatRecog[gantry.Index][id].ImgThld[0];
                TFVision.PatLearn(img, ref GRecipes.PatRecog[gantry.Index][id].RegImage[0], ref thld, ref rects);

                GRecipes.PatRecog[gantry.Index][id].ImgThld[0] = thld;
                GRecipes.PatRecog[gantry.Index][id].SearchRect[0] = rects[0];
                GRecipes.PatRecog[gantry.Index][id].PatRect[0] = rects[1];

                GRecipes.Lighting[gantry.Index][id] = new LightRGBA(TFLightCtrl.LightPair[gantry.Index].CurrentLight);

                return;
            }
            catch (Exception ex)
            {
                GAlarm.Prompt(EAlarm.VISION_LEARN_PATTERN_ERROR, ex.Message.ToString());
            }
            finally
            {
                if (img != null) img.Dispose();
            }
        }
        private void btnLearn2_Click(object sender, EventArgs e)
        {
            Emgu.CV.Image<Emgu.CV.Structure.Gray, byte> img = null;

            try
            {
                TFCameras.Camera[gantry.Index].FlirCamera.Snap();
                img = TFCameras.Camera[gantry.Index].FlirCamera.emgucvImage.Clone();
                TFCameras.Camera[gantry.Index].FlirCamera.Live();

                int id = Tcmd.ID;

                while (GRecipes.PatRecog[gantry.Index].Count <= id)
                {
                    GRecipes.PatRecog[gantry.Index].Add(new TPatRect());
                }

                Rectangle[] rects = new Rectangle[2] { GRecipes.PatRecog[gantry.Index][id].SearchRect[1], GRecipes.PatRecog[gantry.Index][id].PatRect[1] };
                int thld = GRecipes.PatRecog[gantry.Index][id].ImgThld[1];
                TFVision.PatLearn(img, ref GRecipes.PatRecog[gantry.Index][id].RegImage[1], ref thld, ref rects);

                GRecipes.PatRecog[gantry.Index][id].ImgThld[1] = thld;
                GRecipes.PatRecog[gantry.Index][id].SearchRect[1] = rects[0];
                GRecipes.PatRecog[gantry.Index][id].PatRect[1] = rects[1];

                return;
            }
            catch (Exception ex)
            {
                GAlarm.Prompt(EAlarm.VISION_LEARN_PATTERN_ERROR, ex.Message.ToString());
            }
            finally
            {
                if (img != null) img.Dispose();
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            alignData = new TAlignData();
            rtbxResult.Text = "";
            UpdateDisplay();
        }
        private async void btnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                GControl.ExceptionCtrl = new dynamic[0]; 
                Inst.TBoard instBoard = Inst.Board[gantry.Index];

                await Task.Run(() => GRecipes.Functions[gantry.Index][instBoard.FuncNo].PatAlignExecute(gantry, ptBase, Tcmd, ref alignData));

                string res = alignData.Status.ToString();
                switch (alignData.Status)
                {
                    case EPatAlignStatus.BoardOK:
                    case EPatAlignStatus.ClusterOK:
                    case EPatAlignStatus.OK:
                        res = $"Ofst {alignData.Offset.X:f3}, {alignData.Offset.Y:f3} \rScore {alignData.Score:f2}, Angle {alignData.Angle_Deg:f3}";
                        break;
                    default:
                        break;
                }
                rtbxResult.Text = res;
                switch (Tcmd.Cmd)
                {
                    case ECmd.PAT_ALIGN_ROTARY:
                        {
                            if (!GMotDef.GRAxis.MoveRel(alignData.Angle_Deg)) return;

                            break;
                        }
                }
                UpdateDisplay();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            finally
            {
                GControl.UI_Enable();
            }
            GControl.UI_Enable();
        }

        private void cb2PointsWithPat1Img_Click(object sender, EventArgs e)
        {
            Tcmd.Para[9] = Tcmd.Para[9] is 10 ? 0 : 10;
            UpdateDisplay();
        }
    }
}
