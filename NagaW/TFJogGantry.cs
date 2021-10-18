using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace NagaW
{
    public enum EJogMode { Fine, Coarse, Step }
    public enum ERate { Norm, um1, um5, um10, Step };
    public enum EDirection { Positive, Negative }

    class TFJogGantry
    {
        public static EJogMode JogMode = EJogMode.Fine;
        public static ERate JogRate = ERate.Norm;
        public static DPara JogStep = new DPara(nameof(JogStep), 0.05, 0.001, 1, EUnit.MILLIMETER);

        static TEZMCAux.TGroup gantry = TFGantry.GantryLeft;
        public static bool JogStart(int axisIndex, EDirection direction)
        {
            gantry = TFGantry.GantrySelect;
            try
            {
                switch (JogRate)
                {
                    case ERate.Norm:
                        {
                            switch (JogMode)
                            {
                                case EJogMode.Coarse:
                                    gantry.Axis[axisIndex].SpeedProfile(gantry.Axis[axisIndex].Name.Contains("ZAxis") ?
                                        new double[] { GProcessPara.Jog.GZSlowSpeed.Value, GProcessPara.Jog.GZFastSpeed.Value, GProcessPara.Jog.GZAccel.Value, GProcessPara.Jog.GZAccel.Value, 0 } :
                                        new double[] { GProcessPara.Jog.GXYSlowSpeed.Value, GProcessPara.Jog.GXYFastSpeed.Value, GProcessPara.Jog.GXYAccel.Value, GProcessPara.Jog.GXYAccel.Value, 0 });
                                    break;
                                case EJogMode.Fine:
                                    gantry.Axis[axisIndex].SpeedProfile(gantry.Axis[axisIndex].Name.Contains("ZAxis") ?
                                        new double[] { GProcessPara.Jog.GZSlowSpeed.Value, GProcessPara.Jog.GZSlowSpeed.Value, GProcessPara.Jog.GZAccel.Value, GProcessPara.Jog.GZAccel.Value, 0 } :
                                        new double[] { GProcessPara.Jog.GXYSlowSpeed.Value, GProcessPara.Jog.GXYSlowSpeed.Value, GProcessPara.Jog.GXYAccel.Value, GProcessPara.Jog.GXYAccel.Value, 0 });
                                    break;
                            }
                            switch (direction)
                            {
                                case EDirection.Positive:
                                    gantry.Axis[axisIndex].JogAxisP = true;
                                    break;
                                case EDirection.Negative:
                                    gantry.Axis[axisIndex].JogAxisN = true;
                                    break;
                            }
                        }
                        break;
                    case ERate.um1:
                        MoveRel(0.001);
                        break;
                    case ERate.um5:
                        MoveRel(0.005);
                        break;
                    case ERate.um10:
                        MoveRel(0.01);
                        break;
                    case ERate.Step:
                        {
                            MoveRel(JogStep.Value);
                        }
                        break;
                }

                switch (direction)
                {
                    case EDirection.Negative:
                        if (axisIndex is 0) RealTimeChecking(axisIndex);
                        if (axisIndex is 1) RealTimeChecking(axisIndex);
                        if (axisIndex is 2) RealTimeChecking(axisIndex);

                        break;
                    case EDirection.Positive:
                        if (axisIndex is 0) RealTimeChecking(axisIndex);
                        if (axisIndex is 1) RealTimeChecking(axisIndex);

                        break;
                }

                void MoveRel(double dist)
                {
                    switch (direction)
                    {
                        case EDirection.Positive:
                            gantry.Axis[axisIndex].MoveRel(dist, true);
                            break;
                        case EDirection.Negative:
                            gantry.Axis[axisIndex].MoveRel(-dist, true);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return true;
        }
        public static bool JogStop(int axisIndex)
        {
            try
            {
                gantry.Axis[axisIndex].JogAxisP = false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return true;
        }

        public static void ToggleMode()
        {
            JogRate = ERate.Norm;
            switch (JogMode)
            {
                case EJogMode.Step:
                case EJogMode.Coarse:
                    JogMode = EJogMode.Fine;
                    break;
                case EJogMode.Fine:
                    JogMode = EJogMode.Coarse;
                    break;
            }
        }
        public static void ToggleRate(ERate rate)
        {
            if (JogRate == rate)
            {
                JogMode = EJogMode.Fine;
                JogRate = ERate.Norm;
                return;
            }
            JogMode = EJogMode.Step;
            JogRate = rate;
        }


        public static void RealTimeChecking(int axisindex)
        {
            Task.Run(() =>
            {
                while (gantry.Axis[axisindex].Busy)
                {
                    if (GMotDef.GXAxis.ActualPos > GSetupPara.Calibration.ZTouchCamPos[0].X)
                    {
                        if (GMotDef.GZAxis.ActualPos < GSetupPara.Calibration.ZTouchValue[0] + 1)
                        {
                            JogStop(axisindex);
                            MsgBox.ShowDialog("Exit limitation, lift Z up to safety zone");
                            break;
                        }
                    }
                }
            });
        }
    }
}

