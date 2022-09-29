using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NagaW
{
    public enum EZErrorCode { SlotScanFailed = 100, AxisUndercount = 101, AxisOverCount = 102, BusStartFailed = 110, ExpansionIOError = 200, EMOtrigger = 10 }

    public enum ESystemState { Unknown, ErrorRestart, ErrorInit, Ready, Initing }
    //public enum ESystemStatus { Unknown, Error, Busy, Idle, Wait }

    public enum EModule { GantryMain, GantryVR, Lifter, Precisor }
    public enum EStatus { Unknown, Ready, /*ErrorStop, Busy,*/ Initing, InitError }

    public enum EBdStatus { Unknown, None, Busy, /*Preheating, PostHeating,*/ WaitProcess, Processing, Ready, WaitUnload, WaitLoadToPro }

    class GDefine
    {
        public static ESystemState SystemState = ESystemState.Unknown;
        //public static ESystemStatus SystemStatus = ESystemStatus.Unknown;

        //public static EModule GantrySelect = EModule.GantryL;
    }

    public class PointI
    {
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public PointI(int x, int y)
        {
            X = x; Y = y;
        }
        public PointI(PointI pointI) : this(pointI.X, pointI.Y)
        {
        }
        public PointI()
        {
        }
        public static PointI operator +(PointI a, PointI b)
        {
            return new PointI(a.X + b.X, a.Y + b.Y);
        }
        public static PointI operator -(PointI a, PointI b)
        {
            return new PointI(a.X - b.X, a.Y - b.Y);
        }
        public bool IsZero
        {
            get { return X == 0 && Y == 0; }
        }
        public bool IsEqual(PointI point)
        {
            return X == point.X && Y == point.Y;
        }
        public int[] ToArray
        {
            get { return new int[2] { X, Y }; }
        }
        public string ToStringForDisplay()
        {
            return $"X:{X}, Y:{Y}";
        }

        public override string ToString()
        {
            return ToStringForDisplay();
        }
    }

    public class IPara
    {
        public string Name { get; set; } = string.Empty;
        public int Value { get; set; } = 0;
        public int Min { get; private set; } = 0;
        public int Max { get; private set; } = 0;
        public EUnit Unit { get; private set; } = EUnit.NONE;
        public bool IsOutofRange => Value > Max || Value < Min;

        public IPara(string name, int value, int min, int max, EUnit unit)
        {
            Value = value;
            Min = min;
            Max = max;
            Unit = unit;
            Name = name;
        }
        public IPara(int value, EUnit unit)
        {
            Value = value;
            Unit = unit;
        }
        public IPara(IPara para) : this(para.Name, para.Value, (int)para.Min, (int)para.Max, para.Unit)
        {
        }
        public IPara()
        {
        }
        public string ToStringForDisplay()
        {
            return $"{Value} {Unit.ToStringForDisplay()}";
        }
        public TEParamCtrl ParamCtrl => new TEParamCtrl(Name, Min, Max);

        public DPara ToDPara
        {
            get
            {
                return new DPara(Name, Value, Min, Max, Unit, 0);
            }
        }
    }
    public class PointD
    {
        public double X { get; set; } = 0;
        public double Y { get; set; } = 0;
        public PointD(double x, double y)
        {
            X = x; Y = y;
        }
        public PointD(PointD pointD)
            : this(pointD.X, pointD.Y) { }
        public PointD()
        {
        }
        public static PointD operator +(PointD a, PointD b)
        {
            return new PointD(a.X + b.X, a.Y + b.Y);
        }
        public static PointD operator -(PointD a, PointD b)
        {
            return new PointD(a.X - b.X, a.Y - b.Y);
        }
        public static PointD operator *(PointD a, PointI b)
        {
            return new PointD(a.X * b.X, a.Y * b.Y);
        }
        public static PointD operator *(PointD a, double d)
        {
            return new PointD(a.X * d, a.Y * d);
        }
        public static PointD operator /(PointD a, PointI b)
        {
            return new PointD(a.X / b.X, a.Y / b.Y);
        }
        public static PointD operator /(PointD a, double d)
        {
            return new PointD(a.X / d, a.Y / d);
        }
        public bool IsZero
        {
            get { return X == 0 && Y == 0; }
        }
        public double[] ToArray
        {
            get { return new double[2] { X, Y }; }
        }
        public string ToStringForDisplay()
        {
            return $"X:{X:f3}, Y:{Y:f3}";
        }
        //public PointD Rounded
        //{
        //    get { return new PointD(Math.Round(X, 6), Math.Round(Y, 6)); }
        //}
        public override string ToString()
        {
            return ToStringForDisplay();
        }
    }
    public class DPara
    {
        [ReadOnly(true)]
        public string Name { get; set; } = string.Empty;
        public double Value { get; set; } = 0;
        public double Min { get; private set; } = 0;
        public double Max { get; private set; } = 0;
        public EUnit Unit { get; private set; } = EUnit.NONE;
        public int DecimalPlace { get; private set; } = 3;
        public bool IsOutofRange => Value > Max || Value < Min;

        public string[] Collection = null;

        public DPara(string name, double value, double min, double max, EUnit unit, int decimal_places = 3, string[] collection = null)
        {
            Name = name;
            Value = value;
            Min = min;
            Max = max;
            Unit = unit;
            DecimalPlace = decimal_places;
            Collection = collection;
        }
        public DPara(double value, EUnit unit, int decimal_places = 3)
        {
            Value = value;
            Unit = unit;
            DecimalPlace = decimal_places;
        }
        public DPara(DPara para) : this(para.Name, para.Value, para.Min, para.Max, para.Unit, para.DecimalPlace, para.Collection)
        {
        }
        public DPara()
        {
        }
        public string ToStringForDisplay()
        {

            if (Collection is null)
            {
                string f = $"f{(DecimalPlace < 0 ? 3 : DecimalPlace)}";
                return $"{Value.ToString(f)} {Unit.ToStringForDisplay()}";
            }
            else
            {
                return Collection[(int)Value];
            }
        }

        public TEParamCtrl ParamCtrl => new TEParamCtrl(Name, Min, Max);



        //Unit Conversion [only applied for Dpara]
        const double PSI = 145.038;
        const double Bar = 10;
        const double Inch = 1 / 24.5;

        public DPara ConvertTo(EUnit eUnit)
        {
            double r;

            switch (this.Unit)
            {
                case EUnit.MPA:
                    {
                        switch (eUnit)
                        {
                            case EUnit.PSI: r = PSI; break;
                            case EUnit.BAR: r = Bar; break;

                            default: return this;
                        }

                        return new DPara(Name, Value * r, Min * r, Max * r, eUnit);
                    }
                case EUnit.MILLIMETER:
                    {
                        switch (eUnit)
                        {
                            case EUnit.INCH: r = Inch; break;

                            default: return this;
                        }
                        return new DPara(Name, Value * r, Min * r, Max * r, eUnit);
                    }
            }
            return this;
        }
        public DPara ConvertFrom(EUnit eUnit)
        {
            double r;

            switch (this.Unit)
            {
                case EUnit.BAR:
                case EUnit.PSI:
                    {
                        switch (eUnit)
                        {
                            case EUnit.PSI: r = PSI; break;
                            case EUnit.BAR: r = Bar; break;

                            default: return this;
                        }

                        return new DPara(Name, Value / r, Min / r, Max / r, eUnit);
                    }
                case EUnit.INCH:
                    {
                        switch (eUnit)
                        {
                            case EUnit.INCH: r = Inch; break;

                            default: return this;
                        }
                        return new DPara(Name, Value / r, Min / r, Max / r, eUnit);
                    }
            }

            return this;
        }

    }
    public class PointXYZ
    {
        public string Name { get; private set; } = string.Empty;
        public double X { get; set; } = 0;
        public double Y { get; set; } = 0;
        public double Z { get; set; } = 0;
        public virtual double[] XYPos => new double[2] { X, Y };
        public virtual double[] XYZPos => new double[3] { X, Y, Z };

        public PointXYZ(double x, double y, double z, string name = "")
        {
            X = x;
            Y = y;
            Z = z;
            Name = name;
        }
        public PointXYZ(PointXYZ xyz) : this(xyz.X, xyz.Y, xyz.Z, xyz.Name)
        {
        }
        public PointXYZ()
        {
        }
        public static PointXYZ operator +(PointXYZ a, PointD b)
        {
            return new PointXYZ(a.X + b.X, a.Y + b.Y, a.Z);
        }

        public string ToStringForDisplay()
        {
            return $"{nameof(X)}:{X:f3}, {nameof(Y)}:{Y:f3}, {nameof(Z)}:{Z:f3}";
        }
        public PointD GetPointD()
        {
            return new PointD(X, Y);
        }

        public override string ToString()
        {
            return ToStringForDisplay();
        }
    }
    public class LightRGBA
    {
        public int C1 { get; set; } = 0;
        public int C2 { get; set; } = 0;
        public int C3 { get; set; } = 0;
        public int C4 { get; set; } = 0;
        public int[] ToArray => new int[4] { C1, C2, C3, C4 };

        public LightRGBA(int c1, int c2, int c3, int c4)
        {
            C1 = c1;
            C2 = c2;
            C3 = c3;
            C4 = c4;
        }
        public LightRGBA(LightRGBA rGBA) : this(rGBA.C1, rGBA.C2, rGBA.C3, rGBA.C4)
        {
        }
        public LightRGBA()
        {
        }
        public string ToStringForDisplay()
        {
            return $"( {C1}  {C2}  {C3}  {C4} )";
        }
    }


    public class SP_Param
    {
        public DPara FPress { get; set; } = new DPara(nameof(FPress), 0, 0, TFPressCtrl.UpperLimitMPa, EUnit.MPA, 2);
        public DPara PPress { get; set; } = new DPara(nameof(PPress), 0, 0, TFPressCtrl.UpperLimitMPa, EUnit.MPA, 2);
        public DPara DispTime { get; set; } = new DPara(nameof(DispTime), 0, 0, 10000, EUnit.MILLISECOND, 1);
        public DPara PulseOnDelay { get; set; } = new DPara(nameof(PulseOnDelay), 0, -10000, 10000, EUnit.MILLISECOND, 1);
        public DPara PulseOffDelay { get; set; } = new DPara(nameof(PulseOffDelay), 0, -10000, 10000, EUnit.MILLISECOND, 1);
        public DPara VacDur { get; set; } = new DPara(nameof(VacDur), 0, 0, 2000, EUnit.MILLISECOND, 1);
        public DPara FPressH { get; set; } = new DPara(nameof(FPressH), 0, 0, TFPressCtrl.UpperLimitMPa, EUnit.MPA, 2);
        public double[] ToArray
        {
            get
            {
                return new double[5] { FPress.Value, PPress.Value, DispTime.Value, PulseOnDelay.Value, PulseOffDelay.Value };
            }
        }

        public SP_Param(SP_Param setup)
        {
            FPress = new DPara(setup.FPress);
            PPress = new DPara(setup.PPress);
            DispTime = new DPara(setup.DispTime);
            PulseOnDelay = new DPara(setup.PulseOnDelay);
            PulseOffDelay = new DPara(setup.PulseOffDelay);
            FPressH = new DPara(setup.FPressH);
            VacDur = new DPara(setup.VacDur);
        }
        public SP_Param(double disptime, double fpress, double ppress, double pulseondly, double pulseoffdly, double fpressh, double vacDur)
        {
            FPress.Value = fpress;
            PPress.Value = ppress;
            DispTime.Value = disptime;
            PulseOnDelay.Value = pulseondly;
            PulseOffDelay.Value = pulseoffdly;
            FPressH.Value = fpressh;
            VacDur.Value = vacDur;
        }
        public SP_Param(int index) : this()
        {
            string name = $"SPSetupNo{index} ";
            FPress.Name = "as";// name + FPress.Name;
            PPress.Name = name + PPress.Name;
            DispTime.Name = name + DispTime.Name;
            PulseOnDelay.Name = name + PulseOnDelay.Name;
            PulseOffDelay.Name = name + PulseOffDelay.Name;
            FPressH.Name = name + FPressH.Name;
            VacDur.Name = name + VacDur.Name;
        }
        public SP_Param(double[] param)
        {
            DispTime.Value = param[0];
            PulseOnDelay.Value = param[1];
            PulseOffDelay.Value = param[2];
            FPress.Value = param[6];
            PPress.Value = param[7];
            VacDur.Value = param[8];
        }
        public SP_Param()
        {
        }

        public bool SameAs(SP_Param setup)
        {
            for (int i = 0; i < ToArray.Length; i++)
            {
                if (ToArray[i] != setup.ToArray[i]) return false;
            }
            return true;
        }
    }
    public class Vermes3280_Param
    {
        public DPara RisingTime { get; set; } = new DPara(nameof(RisingTime), 0.01, 0.01, 300, EUnit.MILLISECOND, 2);
        public DPara OpenTime { get; set; } = new DPara(nameof(OpenTime), 0, 0, 3000, EUnit.MILLISECOND, 2);
        public DPara FallingTime { get; set; } = new DPara(nameof(FallingTime), 0.01, 0.01, 300, EUnit.MILLISECOND, 2);
        public IPara NeedleLift { get; set; } = new IPara(nameof(NeedleLift), 1, 1, 100, EUnit.PERCENTAGE);
        public IPara Pulses { get; set; } = new IPara(nameof(Pulses), 1, 0, 32000, EUnit.COUNT);
        public DPara Delay { get; set; } = new DPara(nameof(Delay), 1, 0.1, 1000, EUnit.MILLISECOND, 1);
        public DPara FPress { get; set; } = new DPara(nameof(FPress), 0, 0, TFPressCtrl.UpperLimitMPa, EUnit.MPA);
        public IPara MHC48_1Temp { get; set; } = new IPara(nameof(MHC48_1Temp), 30, 20, 180, EUnit.DEGREE_CELSIUS);
        public double[] ToArray
        {
            get { return new double[] { RisingTime.Value, OpenTime.Value, FallingTime.Value, NeedleLift.Value, Pulses.Value, Delay.Value }; }
        }

        public Vermes3280_Param(Vermes3280_Param setup)
        {
            RisingTime = new DPara(setup.RisingTime);
            OpenTime = new DPara(setup.OpenTime);
            FallingTime = new DPara(setup.FallingTime);
            NeedleLift = new IPara(setup.NeedleLift);
            Pulses = new IPara(setup.Pulses);

            Delay = new DPara(setup.Delay);
            FPress = new DPara(setup.FPress);
        }
        public Vermes3280_Param(double risingT, double openT, double fallingT, int needleLift, int pulse, double delay, double fpress)
        {
            RisingTime.Value = risingT;
            OpenTime.Value = openT;
            FallingTime.Value = fallingT;
            NeedleLift.Value = needleLift;
            Pulses.Value = pulse;
            Delay.Value = delay;
            FPress.Value = fpress;
        }
        public Vermes3280_Param(int index)
        {
            string name = $"VermesSetupNo{index + 1}";

            RisingTime = new DPara(name + nameof(RisingTime), 0.01, 0.01, 300, EUnit.MILLISECOND, 2);
            OpenTime = new DPara(name + nameof(OpenTime), 0, 0, 3000, EUnit.MILLISECOND, 2);
            FallingTime = new DPara(name + nameof(FallingTime), 0.01, 0.01, 300, EUnit.MILLISECOND, 2);
            NeedleLift = new IPara(name + nameof(NeedleLift), 1, 1, 100, EUnit.PERCENTAGE);
            Pulses = new IPara(name + nameof(Pulses), 1, 0, 32000, EUnit.COUNT);
            Delay = new DPara(name + nameof(Delay), 1, 1, 1000, EUnit.MILLISECOND, 1);
            FPress = new DPara(name + nameof(FPress), 0, 0, TFPressCtrl.UpperLimitMPa, EUnit.MPA);
        }
        public Vermes3280_Param(double[] param)
        {
            this.RisingTime.Value = param[0];
            this.OpenTime.Value = param[1];
            this.FallingTime.Value = param[2];
            this.NeedleLift.Value = (int)param[3];
            this.Pulses.Value = (int)param[4];
            this.Delay.Value = param[5];
            this.FPress.Value = param[6];
        }

        public Vermes3280_Param()
        {
        }

        public bool SameAs(Vermes3280_Param setup)
        {
            for (int i = 0; i < ToArray.Length; i++)
            {
                if (ToArray[i] != setup.ToArray[i]) return false;
            }
            return true;
        }
    }
    public class HM_Param
    {
        //Operation
        public IPara DispTime { get; set; } = new IPara(nameof(DispTime), 10, 10, 10000, EUnit.MILLISECOND);
        public IPara BSuckTime { get; set; } = new IPara(nameof(BSuckTime), 0, 0, 5000, EUnit.MILLISECOND);
        public DPara DispRPM { get; set; } = new DPara(nameof(DispRPM), 50, 1, 200, EUnit.RPM);
        public DPara BSuckRPM { get; set; } = new DPara(nameof(BSuckRPM), 50, 1, 200, EUnit.RPM);

        public DPara DispAccel { get; set; } = new DPara(nameof(DispAccel), 100, 50, 800, EUnit.RPMPERSEC);
        public DPara BSuckAccel { get; set; } = new DPara(nameof(BSuckAccel), 100, 50, 800, EUnit.RPMPERSEC);

        public IPara VacDur { get; set; } = new IPara(nameof(VacDur), 10, 0, 5000, EUnit.MILLISECOND);
        public DPara FPress { get; set; } = new DPara(nameof(FPress), 0, 0, TFPressCtrl.UpperLimitMPa, EUnit.MPA);

        public double[] ToArray
        {
            get
            {
                return new double[] { DispTime.Value, BSuckTime.Value, DispRPM.Value, BSuckRPM.Value, DispAccel.Value, BSuckAccel.Value, FPress.Value, VacDur.Value };
            }
        }

        public HM_Param(HM_Param setup)
        {
            DispTime = new IPara(setup.DispTime);
            DispRPM = new DPara(setup.DispRPM);

            BSuckTime = new IPara(setup.BSuckTime);
            BSuckRPM = new DPara(setup.BSuckRPM);

            DispAccel = new DPara(setup.DispAccel);
            BSuckAccel = new DPara(setup.BSuckAccel);

            FPress = new DPara(setup.FPress);
            VacDur = new IPara(setup.VacDur);
        }
        public HM_Param(int dispTime, double dispRPM, int bsuckTime, double bsuckRPM, double dispAcc, double bsuckAcc, double fpress, int vacDur)
        {
            DispTime.Value = dispTime;
            DispRPM.Value = dispRPM;

            BSuckTime.Value = bsuckTime;
            BSuckRPM.Value = bsuckRPM;

            DispAccel.Value = dispAcc;
            BSuckAccel.Value = bsuckAcc;

            FPress.Value = fpress;
            VacDur.Value = vacDur;
        }
        public HM_Param(int index)
        {
            string name = $"HMSetupNo{index} ";

            DispTime.Name = name + DispTime.Name;
            BSuckTime.Name = name + BSuckTime.Name;
            DispRPM.Name = name + DispRPM.Name;
            BSuckRPM.Name = name + BSuckRPM.Name;

            DispAccel.Name = name + DispAccel.Name;
            BSuckAccel.Name = name + BSuckAccel.Name;

            FPress.Name = name + FPress.Name;
            VacDur.Name = name + VacDur.Name;
        }
        public HM_Param(double[] param)
        {
            this.DispTime.Value = (int)param[0];
            this.DispRPM.Value = param[1];

            this.BSuckTime.Value = (int)param[2];
            this.BSuckRPM.Value = param[3];

            this.DispAccel.Value = param[4];
            this.BSuckAccel.Value = param[5];

            this.FPress.Value = param[6];
            this.VacDur.Value = (int)param[8];
        }
        public HM_Param()
        {
        }
        public bool SameAs(HM_Param setup)
        {
            for (int i = 0; i < ToArray.Length; i++)
            {
                if (ToArray[i] != setup.ToArray[i]) return false;
            }
            return true;
        }
    }
    public class PneumaticJet_Param
    {
        public DPara FPress { get; set; } = new DPara(nameof(FPress), 0, 0, TFPressCtrl.UpperLimitMPa, EUnit.MPA, 2);
        public DPara VPress { get; set; } = new DPara(nameof(VPress), 0, 0, TFPressCtrl.UpperLimitMPa, EUnit.MPA, 2);
        public DPara DispTime { get; set; } = new DPara(nameof(DispTime), 0, 0, 10000, EUnit.MILLISECOND, 0);
        public DPara OffTime { get; set; } = new DPara(nameof(OffTime), 0, 0, 10000, EUnit.MILLISECOND, 0);

        public double[] ToArray
        {
            get
            {
                return new double[] { FPress.Value, VPress.Value, DispTime.Value, OffTime.Value };
            }
        }
        public PneumaticJet_Param(PneumaticJet_Param jet_Param)
        {
            FPress = new DPara(jet_Param.FPress);
            VPress = new DPara(jet_Param.VPress);
            DispTime = new DPara(jet_Param.DispTime);
            OffTime = new DPara(jet_Param.OffTime);
        }
        public PneumaticJet_Param(double fpress, double vpress, double disptime, double offtime)
        {
            FPress.Value = fpress;
            VPress.Value = vpress;
            DispTime.Value = disptime;
            OffTime.Value = offtime;
        }
        public PneumaticJet_Param(int index)
        {
            string name = $"PnuematicJetSetupNo{index + 1}";
            FPress.Name = name + nameof(FPress);
            VPress.Name = name + nameof(VPress);
            DispTime.Name = name + nameof(DispTime);
            OffTime.Name = name + nameof(OffTime);
        }
        public PneumaticJet_Param()
        {
        }

        public bool SameAs(PneumaticJet_Param jet_Param)
        {
            foreach (var para in jet_Param.ToArray)
            {
                if (para != ToArray[Array.IndexOf(jet_Param.ToArray, para)]) return false;
            }
            return true;
        }
    }

    public class Temp_Setup
    {
        public IPara SetValue { get; set; } = new IPara(nameof(SetValue), 0, -50, 200, EUnit.DEGREE_CELSIUS);
        public IPara PresentValue { get; set; } = new IPara(nameof(PresentValue), 0, -50, 200, EUnit.DEGREE_CELSIUS);
        public IPara PosLimit { get; set; } = new IPara(nameof(PosLimit), 0, 0, 10, EUnit.DEGREE_CELSIUS);
        public IPara NegLimit { get; set; } = new IPara(nameof(NegLimit), 0, -10, 0, EUnit.DEGREE_CELSIUS);
        public Temp_Setup()
        {
        }
    }

    public class PressureSetup
    {
        public DPara FPress { get; set; } = new DPara(nameof(FPress), 0, 0, TFPressCtrl.UpperLimitMPa, EUnit.MPA);
        public DPara PPress { get; set; } = new DPara(nameof(PPress), 0, 0, TFPressCtrl.UpperLimitMPa, EUnit.MPA);
        public DPara VPress { get; set; } = new DPara(nameof(VPress), 0, 0, TFPressCtrl.UpperLimitMPa, EUnit.MPA);
        public bool Master { get; set; } = false;

        public DPara FPress_NegLmt { get; set; } = new DPara(nameof(FPress_NegLmt), 0, -0.1, 0, EUnit.MPA);
        public DPara FPress_PosLmt { get; set; } = new DPara(nameof(FPress_PosLmt), 0, 0, 0.1, EUnit.MPA);
        public DPara PPress_NegLmt { get; set; } = new DPara(nameof(PPress_NegLmt), 0, -0.1, 0, EUnit.MPA);
        public DPara PPress_PosLmt { get; set; } = new DPara(nameof(PPress_PosLmt), 0, 0, 0.1, EUnit.MPA);
        public bool isMonitoring { get; set; } = false;

        public IPara Interval { get; set; } = new IPara(nameof(Interval), 100, 10, 10000, EUnit.SECOND);

        public PressureSetup()
        {

        }
        public PressureSetup(PressureSetup setup)
        {
            FPress = new DPara(setup.FPress);
            PPress = new DPara(setup.PPress);
            VPress = new DPara(setup.VPress);
            Master = setup.Master;

            FPress_NegLmt = new DPara(setup.FPress_NegLmt);
            FPress_PosLmt = new DPara(setup.FPress_PosLmt);
            PPress_NegLmt = new DPara(setup.PPress_NegLmt);
            PPress_PosLmt = new DPara(setup.PPress_PosLmt);
            isMonitoring = setup.isMonitoring;

            Interval = new IPara(setup.Interval);
        }
    }
}
