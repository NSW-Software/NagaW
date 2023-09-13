using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;
using System.Drawing;


namespace NagaW
{
    public class GSystemCfg
    {
        public class Config
        {
            [Category("Start Up")]
            [DisplayName("Initialize")]
            public static bool StartUpInitialize { get; set; } = true;
            [Category("Start Up")]
            [DisplayName("Load Recipe")]
            public static bool StartUpLoadRecipe { get; set; } = true;
            [Category("Equipment")]
            [DisplayName("Name")]
            public static string EquipmentName { get; set; } = string.Empty;
            [Category("Equipment")]
            [DisplayName("ID")]
            public static int EquipmentID { get; set; }

            public static EZTouchType ZTouchType { get; set; } = EZTouchType.LINEAR;

            [Description("Second")]
            public static int SafetyDoorDelaySens_Seconds { get; set; } = 5;
        }
        public class Gantry
        {
            [Category("Gantry")]
            [DisplayName("Home Timeout")]
            public static uint GantryHomeTimeout { get; set; } = 30;//seconds
            [Category("Gantry")]
            [DisplayName("GLXHome Mode")]
            public static uint GLXAxisHomeMode { get; set; } = 1;//refer driver
            [Category("Gantry")]
            [DisplayName("GLYHome Mode")]
            public static uint GLYAxisHomeMode { get; set; } = 2;//refer driver
            [Category("Gantry")]
            [DisplayName("GLZHome Mode")]
            public static uint GLZAxisHomeMode { get; set; } = 2;//refer driver
            [Category("Gantry")]
            [DisplayName("GRXHome Mode")]
            public static uint GRXAxisHomeMode { get; set; } = 2;//refer driver
            [Category("Gantry")]
            [DisplayName("GRYHome Mode")]
            public static uint GRYAxisHomeMode { get; set; } = 2;//refer driver
            [Category("Gantry")]
            [DisplayName("GRZHome Mode")]
            public static uint GRZAxisHomeMode { get; set; } = 2;//refer driver
        }
        public class Conveyor
        {
            //[Category("Conveyor")]
            //[DisplayName("Home Timeout")]
            //public static uint ConvHomeTimeout { get; set; } = 15;//seconds
            [Category("Conveyor")]
            [DisplayName("Conveyor Type")]
            public static EConvInterface Conv_Interface { get; set; } = EConvInterface.CONV_BY_PASS;
            [DisplayName("PulsePerUnit")]
            public static double PPU { get; set; } = 28.29;
        }
        public class Camera
        {
            public const int Count = 2;
            #region
            public ECamType CamType { get; set; }
            [Category("Start Up")]
            [DisplayName("Active")]
            public bool StartUpEnable { get; set; }
            [Category("Connectivity")]
            public string IPAddress { get; set; } = "192.168.0.100";
            [Category("Calibration")]
            [DisplayName("Dist Per Pixel X(mm)")]
            public double DistPerPixelX { get; set; } = 0.006;
            [Category("Calibration")]
            [DisplayName("Dist Per Pixel Y(mm)")]
            public double DistPerPixelY { get; set; } = 0.006;
            [Category("Calibration")]
            [DisplayName("Default Focus Position (mm)")]
            public double DefaultFocusZ { get; set; } = 0;
            [Category("Setting")]
            [DisplayName("Gain (dB)")]
            public double Gain { get; set; } = 1;
            [Category("Setting")]
            [DisplayName("Exposure (ns)")]
            public double Exposure { get; set; } = 8000;
            [Category("Setting")]
            [DisplayName("Dynamic Gain (dB)")]
            public double DynamicGain { get; set; } = 5;
            [Category("Setting")]
            [DisplayName("Dynamic Exposure (ns)")]
            public double DynamicExposure { get; set; } = 500;
            #endregion
            public static Camera[] Cameras { get; set; } = Enumerable.Range(0, Count).Select(x => new Camera()).ToArray();
        }
        public class Light
        {
            #region
            [Category("Start Up")]
            [DisplayName("Active")]
            public bool StartUpEnable { get; set; } = true;
            [Category("Connectivity")]
            public ECOM Comport { get; set; } = ECOM.COM6;
            [Category("Connectivity")]
            public int Baudrate { get; set; } = 115200;
            [Category("Connectivity - Left")]
            [DisplayName("BoardID")]
            public int LeftBoardID { get; set; } = 0;
            [Category("Connectivity - Left")]
            [DisplayName("Start Channel")]
            public int LeftStartChannel { get; set; } = 1;
            [Category("Connectivity - Left")]
            [DisplayName("No Of Channels")]
            public int LeftChannelCount { get; set; } = 2;
            [Category("Connectivity - Right")]
            [DisplayName("BoardID")]
            public int RightBoardID { get; set; } = 0;
            [Category("Connectivity - Right")]
            [DisplayName("Start Channel")]
            public int RightStartChannel { get; set; } = 3;
            [Category("Connectivity - Right")]
            [DisplayName("No Of Channels")]
            public int RightChannelCount { get; set; } = 2;
            [Category("Multiplier - Board0")]
            [DisplayName("Channel 1")]
            public int MultiplierBd0Ch1 { get; set; } = 1;
            [Category("Multiplier - Board0")]
            [DisplayName("Channel 2")]
            public int MultiplierBd0Ch2 { get; set; } = 1;
            [Category("Multiplier - Board0")]
            [DisplayName("Channel 3")]
            public int MultiplierBd0Ch3 { get; set; } = 1;
            [Category("Multiplier - Board0")]
            [DisplayName("Channel 4")]
            public int MultiplierBd0Ch4 { get; set; } = 1;
            [Category("Multiplier - Board1")]
            [DisplayName("Channel 1")]
            public int MultiplierBd1Ch1 { get; set; } = 1;
            [Category("Multiplier - Board1")]
            [DisplayName("Channel 2")]
            public int MultiplierBd1Ch2 { get; set; } = 1;
            [Category("Multiplier - Board1")]
            [DisplayName("Channel 3")]
            public int MultiplierBd1Ch3 { get; set; } = 1;
            [Category("Multiplier - Board1")]
            [DisplayName("Channel 4")]
            public int MultiplierBd1Ch4 { get; set; } = 1;
            #endregion
            public static Light Lights { get; set; } = new Light();
        }
        public class HSensor
        {
            public const int Count = 2;
            #region
            [Category("Start Up")]
            [DisplayName("Active")]
            public bool StartUpEnable { get; set; }
            [Category("Connectivity")]
            public TEMEDAQ.EType Type { get; set; } = TEMEDAQ.EType.SENSOR_IFD2451;
            [Category("Connectivity")]
            public ECOM Comport { get; set; }
            [Category("Connectivity")]
            public string IPAddress { get; set; } = "169.254.168.150";//Keyence CL3 IP: 192.168.1.11
            #endregion
            public static HSensor[] HSensors { get; set; } = Enumerable.Range(0, Count).Select(x => new HSensor()).ToArray();
        }
        public class FPress
        {
            public const int Count = 4;
            #region
            [Category("Start Up")]
            [DisplayName("Active")]
            public bool StartUpEnable { get; set; }
            [Category("Connectivity")]
            public ECOM Comport { get; set; }
            [Category("Common")]
            public string Title { get; private set; } = string.Empty;
            #endregion
            static string[] titles = new string[] { "FPRESS_1", "FPRESS_2", "PPRESS_1", "PPRESS_2" };
            public static FPress[] FPresses { get; set; } = Enumerable.Range(0, Count).Select(x => new FPress() { Title = titles[x] }).ToArray();
        }
        public class Pump
        {
            public const int Count = 2;
            #region
            [Category("Common")]
            public EPumpType PumpType { get; set; }
            [Category("DO")]
            public EDOutput FPressDO { get; set; } = EDOutput.DO44;
            [Category("DO")]
            public EDOutput PPressDO { get; set; }
            [Category("DO")]
            public EDOutput VacDO { get; set; }
            [Category("DO")]
            public EDOutput DispDO { get; set; }
            [Category("DO")]
            public EDOutput CleanVacDO { get; set; }
            [Category("DO")]
            public EDOutput CleanAirDO { get; set; }
            [Category("DI")]
            public EDInput DispDI { get; set; }

            [Category("Pump")]
            [DisplayName("Auto Connect")]
            [Browsable(false)]
            public bool StartUpEnable { get; set; }
            [Category("Pump")]
            [Browsable(false)]
            public ECOM Comport { get; set; }

            [Category("Others")]
            public bool VermesSprayEnable { get; set; }
            #endregion
            public static Pump[] Pumps = Enumerable.Range(0, Count).Select(x => new Pump()).ToArray();
        }
        public class Temperature
        {
            public const int ChannelCount = 8;
            #region
            [Category("Start Up")]
            [DisplayName("Active")]
            public bool StartUpEnable { get; set; }
            [Category("Connectivity")]
            public ECOM Comport { get; set; }

            #region Channel
            public class Channel
            {
                int Index = 0;
                public string Name { get; set; } = string.Empty;
                public bool Enable { get; set; } = true;
                public TETempCtrl.EControlOutputMode ChannelOutputMode { get; set; } = TETempCtrl.EControlOutputMode.HEAT;
                public TETempCtrl.EInputSensorTX ChannelInputMode { get; set; } = TETempCtrl.EInputSensorTX.DPT_H;
                public ETempModel TempCtrlModel { get; set; } = new ETempModel();
                public int Address { get; set; } = 0;
                //[DisplayName("ToIdle (minute)")]
                //public int PeriodToStartIdle { get; set; } = 0; 
                public Channel() { }
                public Channel(int idx)
                {
                    Index = idx;
                    Address = Index + 1;
                    switch (Index)
                    {
                        case 0: Name = "Station Left"; break;
                        case 1: Name = "Station Right"; break;
                        case 2: Name = "Pump Left"; break;
                        case 3: Name = "Pump Right"; break;
                        default: Name = "None"; break;
                    }
                }
                public override string ToString()
                {
                    return $" {Name}; Addr[{Address}] {(Enable ? "On" : "Off")}";
                }
            }

            public List<Channel> Channels { get; set; } = Enumerable.Range(0, ChannelCount).Select(x => new Channel(x)).ToList();
            #endregion

            #endregion
            public static Temperature Temp { get; set; } = new Temperature();
        }
        public class Weight
        {
            public const int Count = 2;
            #region
            [Category("Start Up")]
            [DisplayName("Active")]
            public bool StartUpEnable { get; set; }
            [Category("Connectivity")]
            public ECOM Comport { get; set; }
            #endregion
            public static Weight[] Weights { get; set; } = Enumerable.Range(0, Count).Select(x => new Weight()).ToArray();
        }
        public class Display
        {
            [Category("User Interface")]
            public static Color ThemeColor { get; set; } = Color.SteelBlue;
            public static Color LeftColor { get; set; } = Color.LightSteelBlue;
            public static Color RightColor { get; set; } = Color.PaleVioletRed;
            public static Color DualColor { get; set; } = Color.MediumPurple;
            [Category("User Interface")]
            public static ELanguage Language { get; set; } = ELanguage.Default;
            [Category("Parameter")]

            [DisplayName("Notify OutofRange")]
            [Description("Text change red color when value out of range")]
            public static bool NotifyOutOfRange { get; set; } = true;

            [DisplayName("Pressure SI Unit")]
            [Description("Text change red color when value out of range")]
            public static EUnit PressUnit { get; set; } = EUnit.MPA;
            [DisplayName("Precisor SI Unit")]
            [Description("Show mm / angle for precisor parameter")]
            public static EUnit PrecisorUnit { get; set; } = EUnit.ANGLE;
        }
        public class Advance
        {
            [Category("External Process")]
            [DisplayName("External Application")]
            public static string ExternalApp { get; set; } = @"C:\NagaUSBCamera\Camera.exe";//minimum remain of buffer
            [Category("Recipe")]
            [DisplayName("File Type")]
            public static EFileType RecipeFileType { get; set; } = EFileType.XML;
        }
        public class MakerData
        {
            [Category("Equipment")]
            [DisplayName("Model")]
            public static EEquipmentModel EquipmentModel { get; set; } = EEquipmentModel.SD1;
            [Category("ZMotion")]
            [DisplayName("Minimum Buffer Remain")]
            public static int ZMotion_MinimumBufferRemain { get; set; } = 50;//minimum remain of buffer
            [Category("ZMotion")]
            [DisplayName("Buffer Full Sleep Duration")]
            public static int ZMotion_BufferFullSleepDuration { get; set; } = 3000;//time to sleep when buffer is full, allow other task to run
            [Category("ZMotion")]
            [DisplayName("Log CountDown")]
            public static int ZMotion_LogCountDown { get; set; } = 0;//countdown of log commands
        }
        public class Option
        {
            public static bool PromptMSg_AckPAtAlignment_Centred { get; set; } = false;
            public static bool PromptTempBeforeRun { get; set; } = false;

        }
        public class SecsGem
        {
            public static GemTaro.TCPIP.EEntityMode EntityMode { get; set; } = GemTaro.TCPIP.EEntityMode.Passive;
            public static string IPAddress { get; set; } = "192.168.0.1";
            public static int Port { get; set; } = 5000;
            public static int DeviceID { get; set; } = 0;
        }
        public class Safety
        {
            [DisplayName("Side Door Sensor Checking")]
            public static bool SideDoorCheck { get; set; } = false;
            [Category("DI")]
            [DisplayName("SideDoorDI")]
            public static EDInput SideDoorSens { get; set; } = EDInput.DI00;
        }

        public static bool SaveFile(string filepath)
        {
            //return GDoc.SaveXML(filepath, MethodBase.GetCurrentMethod().DeclaringType);
            return GDoc.SaveINI(filepath, MethodBase.GetCurrentMethod().DeclaringType);
        }
        public static bool SaveFile()
        {
            return SaveFile(GDoc.ConfigFile.FullName);
        }
        public static bool LoadFile(string filepath)
        {
            //return GDoc.LoadXML(filepath, MethodBase.GetCurrentMethod().DeclaringType);
            return GDoc.LoadINI(filepath, MethodBase.GetCurrentMethod().DeclaringType);
        }
        public static bool LoadFile()
        {
            return LoadFile(GDoc.ConfigFile.FullName);
        }
    }

    public class GSetupPara
    {
        public const int HeadCount = 2;
        public class Calibration
        {
            public static PointXYZ[] LaserCamPos = Enumerable.Range(0, HeadCount).Select(x => new PointXYZ()).ToArray();
            public static PointD[] LaserOffset = Enumerable.Range(0, HeadCount).Select(x => new PointD()).ToArray();
            public static LightRGBA[] LaserOffsetLighting = Enumerable.Range(0, HeadCount).Select(x => new LightRGBA()).ToArray();

            public static PointXYZ[] NeedleXYCamPos = Enumerable.Range(0, HeadCount).Select(x => new PointXYZ()).ToArray();
            public static PointD[] NeedleXYOffset = Enumerable.Range(0, HeadCount).Select(x => new PointD()).ToArray();
            public static LightRGBA[] NeedleXYLighting = Enumerable.Range(0, HeadCount).Select(x => new LightRGBA()).ToArray();

            public static PointXYZ[] ZTouchCamPos = Enumerable.Range(0, HeadCount).Select(x => new PointXYZ()).ToArray();
            public static double[] HSensorValue = new double[HeadCount];
            public static double[] ZTouchValue = new double[HeadCount];
            public static LightRGBA[] ZTouchLighting = Enumerable.Range(0, HeadCount).Select(x => new LightRGBA()).ToArray();

            public static PointXYZ[] DynamicXYCamPos = Enumerable.Range(0, HeadCount).Select(x => new PointXYZ()).ToArray();
            public static LightRGBA[] DynamicLighting = Enumerable.Range(0, HeadCount).Select(x => new LightRGBA()).ToArray();
            public static PointD[,] DynamicOffsets = (PointD[,])GControl.Create2DArray(typeof(PointD), 2, 4);
        }
        public class NeedleVacClean
        {
            public static PointXYZ[] Pos = Enumerable.Range(0, HeadCount).Select(x => new PointXYZ()).ToArray();
            public static PointD[] HSensorOffset = Enumerable.Range(0, HeadCount).Select(x => new PointD(0, 5)).ToArray();
            public static double[] HSensorValue = new double[HeadCount];
        }
        public class NeedleFlush
        {
            public static PointXYZ[] Pos = Enumerable.Range(0, HeadCount).Select(x => new PointXYZ()).ToArray();
            public static PointD[] HSensorOffset = Enumerable.Range(0, HeadCount).Select(x => new PointD(0, 5)).ToArray();
            public static double[] HSensorValue = new double[HeadCount];
        }
        public class NeedlePurge
        {
            public static PointXYZ[] Pos = Enumerable.Range(0, HeadCount).Select(x => new PointXYZ()).ToArray();
            public static PointD[] HSensorOffset = Enumerable.Range(0, HeadCount).Select(x => new PointD(0, 5)).ToArray();
            public static double[] HSensorValue = new double[HeadCount];
        }
        public class PurgeStage
        {
            public static PointXYZ[] Pos = Enumerable.Range(0, HeadCount).Select(x => new PointXYZ()).ToArray();
            public static DPara Gap = new DPara(nameof(PurgeStage) + nameof(Gap), 1, 0.05, 10, EUnit.MILLIMETER);
        }
        public class NeedleAirBladeClean
        {
            public static PointXYZ[] Pos = Enumerable.Range(0, HeadCount).Select(x => new PointXYZ()).ToArray();
            public static PointD[] HSensorOffset = Enumerable.Range(0, HeadCount).Select(x => new PointD(0, 5)).ToArray();
            public static double[] HSensorValue = new double[HeadCount];
        }
        public class Maintenance
        {
            public static PointXYZ[] MachinePos = Enumerable.Range(0, HeadCount).Select(x => new PointXYZ()).ToArray();
            public static PointXYZ[] PumpPos = Enumerable.Range(0, HeadCount).Select(x => new PointXYZ()).ToArray();
        }
        public class HeadPreperation
        {
            public static PointXYZ[] Pos = Enumerable.Range(0, HeadCount).Select(x => new PointXYZ()).ToArray();

            public static string[] PumpID = Enumerable.Range(0, HeadCount).Select(x => string.Empty).ToArray();
            public static string[] SyringeID = Enumerable.Range(0, HeadCount).Select(x => string.Empty).ToArray();

            public static DateTime[] PumpInstalledTime = Enumerable.Range(0, HeadCount).Select(x => DateTime.Now).ToArray();
            public static DateTime[] SyringeInstalledTime = Enumerable.Range(0, HeadCount).Select(x => DateTime.Now).ToArray();
        }
        public class Weighing
        {
            public static PointXYZ[,] Pos = (PointXYZ[,])GControl.Create2DArray(typeof(PointXYZ), HeadCount, 103/*Enum.GetValues(typeof(EPumpType)).Length*/);
            public static int[] WeighBoardCount = new int[HeadCount];
            public static PointD[,] EndPos = (PointD[,])GControl.Create2DArray(typeof(PointD), HeadCount, 103/*Enum.GetValues(typeof(EPumpType)).Length*/);
            //public static PointXYZ[,] Pos = (PointXYZ[,])GControl.Create2DArray(typeof(PointXYZ), HeadCount, Enum.GetValues(typeof(EPumpType)).Length);/*Enumerable.Range(0, HeadCount).Select(x => new PointXYZ()).ToArray();*/
        }

        public class NozzleInsp
        {
            public static PointXYZ[] Pos = Enumerable.Range(0, HeadCount).Select(x => new PointXYZ()).ToArray();
        }
        public class Wafer
        {
            public static PointXYZ ManualLoadPos = new PointXYZ();
            public static PointXYZ AutoLoadPos = new PointXYZ();
            public static PointXYZ AirBlowPos = new PointXYZ();

            public static DPara PrecisorPos_1 = new DPara(nameof(Wafer) + nameof(PrecisorPos_1), 0, 0, 150, EUnit.ANGLE);
            public static DPara PrecisorPos_2 = new DPara(nameof(Wafer) + nameof(PrecisorPos_2), 0, 0, 150, EUnit.ANGLE);
            public static DPara PrecisorPos_3 = new DPara(nameof(Wafer) + nameof(PrecisorPos_3), 0, 0, 150, EUnit.ANGLE);

            public static PointXYZ TeachNotchCamPos = new PointXYZ();

            //public static TPatRect PatRect = new TPatRect();
            public static LightRGBA PatLightRGBA = new LightRGBA();
        }
        public class NeedleSprayClean
        {
            public static PointXYZ[] Pos = Enumerable.Range(0, HeadCount).Select(x => new PointXYZ()).ToArray();
            public static PointD[] HSensorOffset = Enumerable.Range(0, HeadCount).Select(x => new PointD(0, 5)).ToArray();
            public static double[] HSensorValue = new double[HeadCount];
        }

        public static bool SaveFile(string filepath)
        {
            //return GDoc.SaveXML(filepath, MethodBase.GetCurrentMethod().DeclaringType);
            return GDoc.SaveINI(filepath, MethodBase.GetCurrentMethod().DeclaringType);
        }
        public static bool SaveFile()
        {
            return SaveFile(GDoc.SetupParaFile.FullName);
        }
        public static bool LoadFile(string filepath)
        {
            //return GDoc.LoadXML(filepath, MethodBase.GetCurrentMethod().DeclaringType);
            return GDoc.LoadINI(filepath, MethodBase.GetCurrentMethod().DeclaringType);
        }
        public static bool LoadFile()
        {
            return LoadFile(GDoc.SetupParaFile.FullName);
        }
    }

    public class GProcessPara
    {
        public class Operation
        {
            public static DPara GXYStartSpeed = new DPara(nameof(Operation) + nameof(GXYStartSpeed), 0, 0, 100, EUnit.MILLIMETER_PER_SECOND);
            public static DPara GXYFastSpeed = new DPara(nameof(Operation) + nameof(GXYFastSpeed), 250, 10, 1000, EUnit.MILLIMETER_PER_SECOND);
            public static DPara GXYAccel = new DPara(nameof(Operation) + nameof(GXYAccel), 1000, 10, 15000, EUnit.MILLIMETER_PER_SECOND_SQUARED);
            public static DPara GXYDecel = new DPara(nameof(Operation) + nameof(GXYDecel), 1000, 10, 15000, EUnit.MILLIMETER_PER_SECOND_SQUARED);
            public static DPara GXYJerk = new DPara(nameof(Operation) + nameof(GXYJerk), 0, 0, 1, EUnit.PERCENTAGE);
            public static double[] GXYSpeed
            {
                get
                {
                    return new double[5] { GXYStartSpeed.Value, GXYFastSpeed.Value, GXYAccel.Value, GXYDecel.Value, 0 };
                }
            }

            public static DPara GZStartSpeed = new DPara(nameof(Operation) + nameof(GZStartSpeed), 0, 0, 100, EUnit.MILLIMETER_PER_SECOND);
            public static DPara GZFastSpeed = new DPara(nameof(Operation) + nameof(GZFastSpeed), 50, 10, 1000, EUnit.MILLIMETER_PER_SECOND);
            public static DPara GZAccel = new DPara(nameof(Operation) + nameof(GZAccel), 1000, 10, 15000, EUnit.MILLIMETER_PER_SECOND_SQUARED);
            public static DPara GZDecel = new DPara(nameof(Operation) + nameof(GZDecel), 1000, 10, 15000, EUnit.MILLIMETER_PER_SECOND_SQUARED);
            public static DPara GZJerk = new DPara(nameof(Operation) + nameof(GZJerk), 0, 0, 1, EUnit.PERCENTAGE);
            public static double[] GZSpeed
            {
                get
                {
                    double[] d = new double[5] { GZStartSpeed.Value, GZFastSpeed.Value, GZAccel.Value, GZDecel.Value, 0 };
                    return d;
                }
            }

            public static DPara CSlowSpeed = new DPara(nameof(Operation) + nameof(CSlowSpeed), 10, 1, 1000, EUnit.MILLIMETER_PER_SECOND);
            public static DPara CFastSpeed = new DPara(nameof(Operation) + nameof(CFastSpeed), 50, 1, 1000, EUnit.MILLIMETER_PER_SECOND);
            public static DPara CAccel = new DPara(nameof(Operation) + nameof(CAccel), 1000, 10, 15000, EUnit.MILLIMETER_PER_SECOND_SQUARED);
            public static DPara CDecel = new DPara(nameof(Operation) + nameof(CDecel), 0, 10, 15000, EUnit.MILLIMETER_PER_SECOND_SQUARED);
            public static double[] CSlowSpeedProfile
            {
                get
                {
                    double[] d = new double[5] { CSlowSpeed.Value, CSlowSpeed.Value, CAccel.Value, CDecel.Value, 0 };
                    return d;
                }
            }
            public static double[] CFastSpeedProfile
            {
                get
                {
                    double[] d = new double[5] { CFastSpeed.Value, CFastSpeed.Value, CAccel.Value, CDecel.Value, 0 };
                    return d;
                }
            }
        }
        public class Home
        {
            public static DPara GXYStartSpeed = new DPara(nameof(Home) + nameof(GXYStartSpeed), 2, 1, 50, EUnit.MILLIMETER_PER_SECOND);
            public static DPara GXYFastSpeed = new DPara(nameof(Home) + nameof(GXYFastSpeed), 25, 1, 100, EUnit.MILLIMETER_PER_SECOND);
            public static DPara GXYAccel = new DPara(nameof(Home) + nameof(GXYAccel), 500, 10, 5000, EUnit.MILLIMETER_PER_SECOND_SQUARED);
            public static double[] GXYSpeedProfile
            {
                get
                {
                    double[] d = new double[5] { GXYStartSpeed.Value, GXYFastSpeed.Value, GXYAccel.Value, GXYAccel.Value, 0 };
                    return d;
                }
            }

            public static DPara GZStartSpeed = new DPara(nameof(Home) + nameof(GZStartSpeed), 1, 1, 50, EUnit.MILLIMETER_PER_SECOND);
            public static DPara GZFastSpeed = new DPara(nameof(Home) + nameof(GZFastSpeed), 15, 1, 100, EUnit.MILLIMETER_PER_SECOND);
            public static DPara GZAccel = new DPara(nameof(Home) + nameof(GZAccel), 500, 10, 5000, EUnit.MILLIMETER_PER_SECOND_SQUARED);
            public static double[] GZSpeedProfile
            {
                get
                {
                    double[] d = new double[5] { GZStartSpeed.Value, GZFastSpeed.Value, GZAccel.Value, GZAccel.Value, 0 };
                    return d;
                }
            }

            public static DPara CSpeed = new DPara(nameof(Home) + nameof(CSpeed), 50, 1, 100, EUnit.MILLIMETER_PER_SECOND);
            public static DPara CAccel = new DPara(nameof(Home) + nameof(CAccel), 500, 10, 5000, EUnit.MILLIMETER_PER_SECOND_SQUARED);
            public static double[] CSpeedProfile
            {
                get
                {
                    double[] d = new double[5] { CSpeed.Value, CSpeed.Value, CAccel.Value, CAccel.Value, 0 };
                    return d;
                }
            }
        }
        public class Jog
        {
            public static DPara GXYSlowSpeed = new DPara(nameof(Jog) + nameof(GXYSlowSpeed), 1, 0.01, 50, EUnit.MILLIMETER_PER_SECOND);
            public static DPara GXYFastSpeed = new DPara(nameof(Jog) + nameof(GXYFastSpeed), 20, 0.1, 100, EUnit.MILLIMETER_PER_SECOND);
            public static DPara GXYAccel = new DPara(nameof(Jog) + nameof(GXYAccel), 100, 10, 5000, EUnit.MILLIMETER_PER_SECOND_SQUARED);

            public static DPara GZSlowSpeed = new DPara(nameof(Jog) + nameof(GZSlowSpeed), 0.5, 0.01, 50, EUnit.MILLIMETER_PER_SECOND);
            public static DPara GZFastSpeed = new DPara(nameof(Jog) + nameof(GZFastSpeed), 5, 1, 100, EUnit.MILLIMETER_PER_SECOND);
            public static DPara GZAccel = new DPara(nameof(Jog) + nameof(GZAccel), 100, 10, 5000, EUnit.MILLIMETER_PER_SECOND_SQUARED);

            public static DPara CSlowSpeed = new DPara(nameof(Jog) + nameof(CSlowSpeed), 10, 1, 100, EUnit.MILLIMETER_PER_SECOND);
            public static DPara CFastSpeed = new DPara(nameof(Jog) + nameof(CFastSpeed), 50, 1, 100, EUnit.MILLIMETER_PER_SECOND);
            public static DPara CAccel = new DPara(nameof(Jog) + nameof(CAccel), 500, 10, 5000, EUnit.MILLIMETER_PER_SECOND_SQUARED);
        }

        public class HSensor
        {
            public static IPara SettleTime = new IPara(nameof(HSensor) + nameof(SettleTime), 500, 10, 5000, EUnit.MILLISECOND);
        }
        public class Vision
        {
            public static IPara SettleTime = new IPara(nameof(Vision) + nameof(SettleTime), 250, 10, 5000, EUnit.MILLISECOND);
        }

        public class Calibration
        {
            public static DPara[] XYTouchMarkGap = Enumerable.Range(0, Headno).Select(x => new DPara(nameof(Calibration) + nameof(XYTouchMarkGap) + $"Head{(x == 0 ? "L" : "R")}", 0.5, 0, 5, EUnit.MILLIMETER)).ToArray();
            public static DPara[] TouchDotDispGap = Enumerable.Range(0, Headno).Select(x => new DPara(nameof(Calibration) + nameof(TouchDotDispGap) + $"Head{(x == 0 ? "L" : "R")}", 0.5, 0, 15, EUnit.MILLIMETER)).ToArray();

            public static DPara[] ZTouchEncoderRes = Enumerable.Range(0, Headno).Select(x => new DPara(nameof(Calibration) + nameof(ZTouchEncoderRes) + $"Head{(x == 0 ? "L" : "R")}", 0.002, 0.001, 0.1, EUnit.MILLIMETER)).ToArray();

            public static DPara[] DynamicJetGap = Enumerable.Range(0, Headno).Select(x => new DPara(nameof(Calibration) + nameof(DynamicJetGap) + $"Head{(x == 0 ? "L" : "R")}", 1, 0, 10, EUnit.MILLIMETER)).ToArray();
            public static DPara[] DynamicTouchDotSpd = Enumerable.Range(0, Headno).Select(x => new DPara(nameof(Calibration) + nameof(DynamicTouchDotSpd) + $"Head{(x == 0 ? "L" : "R")}", 10, 10, 300, EUnit.MILLIMETER_PER_SECOND)).ToArray();
            public static DPara[] DynamicTouchDotAcc = Enumerable.Range(0, Headno).Select(x => new DPara(nameof(Calibration) + nameof(DynamicTouchDotAcc) + $"Head{(x == 0 ? "L" : "R")}", 50, 50, 1000, EUnit.MILLIMETER_PER_SECOND_SQUARED)).ToArray();
            public static DPara[] DynamicAccelDist = Enumerable.Range(0, Headno).Select(x => new DPara(nameof(Calibration) + nameof(DynamicAccelDist) + $"Head{(x == 0 ? "L" : "R")}", 5, 5, 40, EUnit.MILLIMETER)).ToArray();
            public static DPara[] DynamicPitch = Enumerable.Range(0, Headno).Select(x => new DPara(nameof(Calibration) + nameof(DynamicPitch) + $"Head{(x == 0 ? "L" : "R")}", 1, 0.1, 5, EUnit.MILLIMETER)).ToArray();
            public static IPara[] DynamicDotCount = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(Calibration) + nameof(DynamicDotCount) + $"Head{(x == 0 ? "L" : "R")}", 1, 1, 10, EUnit.COUNT)).ToArray();

            public static TCCalibration.DynamicOffset.EDir[] Dirs = Enumerable.Range(0, Headno).Select(x => new TCCalibration.DynamicOffset.EDir()).ToArray();       
        }

        const int Headno = 2;
        public class NeedleVacClean
        {
            public static IPara[] DownWait = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(NeedleVacClean) + nameof(DownWait) + $"Head{(x == 0 ? "L" : "R")}", 0, 0, 10000, EUnit.MILLISECOND)).ToArray();
            public static IPara[] DispTime = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(NeedleVacClean) + nameof(DispTime) + $"Head{(x == 0 ? "L" : "R")}", 1000, 0, 5000, EUnit.MILLISECOND)).ToArray();
            public static IPara[] VacTime = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(NeedleVacClean) + nameof(VacTime) + $"Head{(x == 0 ? "L" : "R")}", 800, 0, 5000, EUnit.MILLISECOND)).ToArray();
            public static IPara[] PostVacTime = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(NeedleVacClean) + nameof(PostVacTime) + $"Head{(x == 0 ? "L" : "R")}", 500, 0, 5000, EUnit.MILLISECOND)).ToArray();
            public static IPara[] PostWait = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(NeedleVacClean) + nameof(PostWait) + $"Head{(x == 0 ? "L" : "R")}", 0, 0, 10000, EUnit.MILLISECOND)).ToArray();
            public static IPara[] Count = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(NeedleVacClean) + nameof(Count) + $"Head{(x == 0 ? "L" : "R")}", 1, 0, 10, EUnit.COUNT)).ToArray();
            public static DPara[] RelZPos = Enumerable.Range(0, Headno).Select(x => new DPara(nameof(NeedleVacClean) + nameof(RelZPos) + $"Head{(x == 0 ? "L" : "R")}", 0, -10, 10, EUnit.MILLIMETER)).ToArray();
        }
        public class NeedleFlush
        {
            public static IPara[] DownWait = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(NeedleFlush) + nameof(DownWait) + $"Head{(x == 0 ? "L" : "R")}", 0, 0, 10000, EUnit.MILLISECOND)).ToArray();
            public static IPara[] DispTime = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(NeedleFlush) + nameof(DispTime) + $"Head{(x == 0 ? "L" : "R")}", 1000, 0, 5000, EUnit.MILLISECOND)).ToArray();
            public static IPara[] VacTime = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(NeedleFlush) + nameof(VacTime) + $"Head{(x == 0 ? "L" : "R")}", 800, 0, 5000, EUnit.MILLISECOND)).ToArray();
            public static IPara[] PostVacTime = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(NeedleFlush) + nameof(PostVacTime) + $"Head{(x == 0 ? "L" : "R")}", 500, 0, 5000, EUnit.MILLISECOND)).ToArray();
            public static IPara[] PostWait = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(NeedleFlush) + nameof(PostWait) + $"Head{(x == 0 ? "L" : "R")}", 0, 0, 10000, EUnit.MILLISECOND)).ToArray();
            public static IPara[] Count = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(NeedleFlush) + nameof(Count) + $"Head{(x == 0 ? "L" : "R")}", 1, 0, 10, EUnit.COUNT)).ToArray();
            public static DPara[] RelZPos = Enumerable.Range(0, Headno).Select(x => new DPara(nameof(NeedleVacClean) + nameof(RelZPos) + $"Head{(x == 0 ? "L" : "R")}", 0, -10, 10, EUnit.MILLIMETER)).ToArray();
        }
        public class NeedlePurge
        {
            public static IPara[] DownWait = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(NeedlePurge) + nameof(DownWait) + $"Head{(x == 0 ? "L" : "R")}", 0, 0, 10000, EUnit.MILLISECOND)).ToArray();
            public static IPara[] DispTime = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(NeedlePurge) + nameof(DispTime) + $"Head{(x == 0 ? "L" : "R")}", 1000, 0, 5000, EUnit.MILLISECOND)).ToArray();
            public static IPara[] VacTime = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(NeedlePurge) + nameof(VacTime) + $"Head{(x == 0 ? "L" : "R")}", 800, 0, 5000, EUnit.MILLISECOND)).ToArray();
            public static IPara[] PostVacTime = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(NeedlePurge) + nameof(PostVacTime) + $"Head{(x == 0 ? "L" : "R")}", 500, 0, 5000, EUnit.MILLISECOND)).ToArray();
            public static IPara[] PostWait = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(NeedlePurge) + nameof(PostWait) + $"Head{(x == 0 ? "L" : "R")}", 0, 0, 10000, EUnit.MILLISECOND)).ToArray();
            public static IPara[] Count = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(NeedlePurge) + nameof(Count) + $"Head{(x == 0 ? "L" : "R")}", 1, 0, 10, EUnit.COUNT)).ToArray();
            public static DPara[] RelZPos = Enumerable.Range(0, Headno).Select(x => new DPara(nameof(NeedleVacClean) + nameof(RelZPos) + $"Head{(x == 0 ? "L" : "R")}", 0, -10, 10, EUnit.MILLIMETER)).ToArray();
        }

        public class NeedleSpray
        {
            public static IPara[] DownWait = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(NeedlePurge) + nameof(DownWait) + $"Head{(x == 0 ? "L" : "R")}", 0, 0, 10000, EUnit.MILLISECOND)).ToArray();
            public static IPara[] SprayTime = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(NeedlePurge) + nameof(SprayTime) + $"Head{(x == 0 ? "L" : "R")}", 2000, 0, 10000, EUnit.MILLISECOND)).ToArray();
            public static IPara[] PostWait = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(NeedlePurge) + nameof(PostWait) + $"Head{(x == 0 ? "L" : "R")}", 0, 0, 10000, EUnit.MILLISECOND)).ToArray();
            public static IPara[] Count = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(NeedlePurge) + nameof(Count) + $"Head{(x == 0 ? "L" : "R")}", 1, 0, 10, EUnit.COUNT)).ToArray();
            public static DPara[] RelZPos = Enumerable.Range(0, Headno).Select(x => new DPara(nameof(NeedleVacClean) + nameof(RelZPos) + $"Head{(x == 0 ? "L" : "R")}", 0, -10, 10, EUnit.MILLIMETER)).ToArray();
        }

        public class PurgeStage
        {
            public static TLayout Layout = new TLayout();
            public static PointI CurrentCR = new PointI();
            public static IPara Count = new IPara(nameof(PurgeStage) + nameof(Count), 3, 0, 100, EUnit.COUNT);
            public static bool CamOffsetAfterPurge = false;

            public static IPara[] DotTime = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(PurgeStage) + nameof(DotTime) + $"Head{(x == 0 ? "L" : "R")}", 100, 0, 2000, EUnit.MILLISECOND)).ToArray();
            public static IPara[] DotWait = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(PurgeStage) + nameof(DotWait) + $"Head{(x == 0 ? "L" : "R")}", 10, 0, 2000, EUnit.MILLISECOND)).ToArray();
            public static DPara[] RetDist = Enumerable.Range(0, Headno).Select(x => new DPara(nameof(PurgeStage) + nameof(RetDist) + $"Head{(x == 0 ? "L" : "R")}", 1, 0, 2000, EUnit.MILLIMETER)).ToArray();
            public static IPara[] RetWait = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(PurgeStage) + nameof(RetWait) + $"Head{(x == 0 ? "L" : "R")}", 10, 0, 2000, EUnit.MILLISECOND)).ToArray();
            public static DPara[] UpDist = Enumerable.Range(0, Headno).Select(x => new DPara(nameof(PurgeStage) + nameof(UpDist) + $"Head{(x == 0 ? "L" : "R")}", 1, 0, 2000, EUnit.MILLIMETER)).ToArray();
            public static IPara[] UpWait = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(PurgeStage) + nameof(UpWait) + $"Head{(x == 0 ? "L" : "R")}", 0, 0, 2000, EUnit.MILLISECOND)).ToArray();
            public static DPara[] DnSpeed = Enumerable.Range(0, Headno).Select(x => new DPara(nameof(PurgeStage) + nameof(DnSpeed) + $"Head{(x == 0 ? "L" : "R")}", 10, 1, 2000, EUnit.MILLIMETER_PER_SECOND)).ToArray();
            public static IPara[] DnWait = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(PurgeStage) + nameof(DnWait) + $"Head{(x == 0 ? "L" : "R")}", 10, 0, 2000, EUnit.MILLISECOND)).ToArray();
        }
        public class NeedleAirBladeClean
        {
            public static IPara[] DownWait = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(NeedleAirBladeClean) + nameof(DownWait) + $"Head{(x == 0 ? "L" : "R")}", 0, 0, 10000, EUnit.MILLISECOND)).ToArray();
            public static DPara[] RelZPos = Enumerable.Range(0, Headno).Select(x => new DPara(nameof(NeedleAirBladeClean) + nameof(RelZPos) + $"Head{(x == 0 ? "L" : "R")}", 0, -10, 0, EUnit.MILLIMETER)).ToArray();
            public static IPara[] UpSpeed = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(NeedleAirBladeClean) + nameof(UpSpeed) + $"Head{(x == 0 ? "L" : "R")}", 10, 1, 10000, EUnit.MILLIMETER_PER_SECOND)).ToArray();
            public static IPara[] Count = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(NeedleAirBladeClean) + nameof(Count) + $"Head{(x == 0 ? "L" : "R")}", 1, 0, 10, EUnit.COUNT)).ToArray();
        }

        public class Conveyor
        {
            public static IPara UpDnTimeOut = new IPara(nameof(Conveyor) + nameof(UpDnTimeOut), 250, 10, 5000, EUnit.MILLISECOND);
            public static IPara StopperTimeOut = new IPara(nameof(Conveyor) + nameof(StopperTimeOut), 150, 10, 5000, EUnit.MILLISECOND);
            public static IPara MoveTimeOut = new IPara(nameof(Conveyor) + nameof(MoveTimeOut), 2500, 10, 5000, EUnit.MILLISECOND);
            public static IPara VacTimeOut = new IPara(nameof(Conveyor) + nameof(VacTimeOut), 250, 10, 5000, EUnit.MILLISECOND);
            public static IPara VacEnabled = new IPara(nameof(Conveyor) + nameof(VacEnabled), 1, 0, 1, EUnit.NONE);
            public static IPara VacSequence = new IPara(nameof(Conveyor) + nameof(VacSequence), 0, 0, 1, EUnit.NONE);//0: Vac -> ConvDown; 1: ConvDn -> Vac
            public static IPara MoveDelay = new IPara(nameof(Conveyor) + nameof(MoveDelay), 100, 0, 2000, EUnit.MILLISECOND);
            public static IPara SmemaTimeOut = new IPara(nameof(Conveyor) + nameof(SmemaTimeOut), 3000, 3000, 5000, EUnit.MILLISECOND);


            //public static DPara OpSlowSpeed = new DPara(nameof(Operation) + nameof(OpSlowSpeed), 10, 1, 1000, EUnit.MILLIMETER_PER_SECOND);
            //public static DPara OpFastSpeed = new DPara(nameof(Operation) + nameof(OpFastSpeed), 50, 1, 1000, EUnit.MILLIMETER_PER_SECOND);
            //public static DPara OpAccel = new DPara(nameof(Operation) + nameof(OpAccel), 1000, 10, 15000, EUnit.MILLIMETER_PER_SECOND_SQUARED);
            //public static DPara OpDecel = new DPara(nameof(Operation) + nameof(OpDecel), 0, 0, 15000, EUnit.MILLIMETER_PER_SECOND_SQUARED);
            //public static double[] OpSlowSpeedProfile
            //{
            //    get
            //    {
            //        double[] d = new double[5] { OpSlowSpeed.Value, OpSlowSpeed.Value, OpAccel.Value, OpDecel.Value, 0 };
            //        return d;
            //    }
            //}
            //public static double[] OpFastSpeedProfile
            //{
            //    get
            //    {
            //        double[] d = new double[5] { OpFastSpeed.Value, OpFastSpeed.Value, OpAccel.Value, OpAccel.Value, 0 };
            //        return d;
            //    }
            //}
        }
        public class Weighing
        {
            public static IPara[] StartWait = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(Weighing) + nameof(StartWait), 100, 0, 8000, EUnit.MILLISECOND)).ToArray();
            public static IPara[] EndWait = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(Weighing) + nameof(EndWait), 5000, 0, 15000, EUnit.MILLISECOND)).ToArray();

            public static IPara[] DotWait = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(Weighing) + nameof(DotWait), 100, 0, 10000, EUnit.MILLISECOND)).ToArray();
            public static IPara[] ReadWait = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(Weighing) + nameof(ReadWait), 5000, 0, 10000, EUnit.MILLISECOND)).ToArray();

            public static DPara[] ZUpVel = Enumerable.Range(0, Headno).Select(x => new DPara(nameof(Weighing) + nameof(ZUpVel), 5, 0, 150, EUnit.MILLIMETER_PER_SECOND)).ToArray();
            public static DPara[] ZUpDist = Enumerable.Range(0, Headno).Select(x => new DPara(nameof(Weighing) + nameof(ZUpDist), 5, 0, 10, EUnit.MILLIMETER)).ToArray();

            public static IPara[] DotPerSample = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(DotPerSample), 1, 0, 32000, EUnit.COUNT)).ToArray();
            public static IPara[] SampleCount = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(SampleCount), 5, 0, 200, EUnit.COUNT)).ToArray();

            public static DPara Target_Mass = new DPara(nameof(Weighing) + nameof(Target_Mass), 0, 0, 5000, EUnit.MILLIGRAM);
            public static DPara Target_Mass_Percentage = new DPara(nameof(Weighing) + nameof(Target_Mass_Percentage), 1, 0, 50, EUnit.PERCENTAGE);
            public static DPara Target_Mass_Range = new DPara(nameof(Weighing) + nameof(Target_Mass_Range), 0, 0, Target_Mass.Max * Target_Mass_Percentage.Max / 100, EUnit.MILLIGRAM);

            public static DPara Target_FlowRate = new DPara(nameof(Weighing) + nameof(Target_FlowRate), 0, 0, 5000, EUnit.MASS_FLOW_RATE);
            public static DPara Target_FlowRate_Percentage = new DPara(nameof(Weighing) + nameof(Target_FlowRate_Percentage), 1, 0, 50, EUnit.PERCENTAGE);
            public static DPara Target_FlowRate_Range = new DPara(nameof(Weighing) + nameof(Target_FlowRate_Range), 0, 0, Target_FlowRate.Max * Target_FlowRate_Percentage.Max / 100, EUnit.MASS_FLOW_RATE);

            public static DPara Tune_Percentage_UpperLimit = new DPara(nameof(Weighing) + nameof(Tune_Percentage_UpperLimit), 25, 1, 100, EUnit.PERCENTAGE);
            public static DPara Tune_Percentage_LowerLimit = new DPara(nameof(Weighing) + nameof(Tune_Percentage_LowerLimit), 25, 1, 100, EUnit.PERCENTAGE);

            public static IPara IgnoreCount = new IPara(nameof(Weighing) + nameof(IgnoreCount), 1, 0, 50, EUnit.PERCENTAGE);

            public static IPara CleanAfterFill = new IPara(nameof(Weighing) + nameof(CleanAfterFill), 1, 0, 10, EUnit.COUNT);
            public static IPara PurgeAfterFill = new IPara(nameof(Weighing) + nameof(PurgeAfterFill), 1, 0, 10, EUnit.COUNT);
            public static IPara FlushAfterFill = new IPara(nameof(Weighing) + nameof(FlushAfterFill), 1, 0, 10, EUnit.COUNT);

            public static IPara DispTime_DotM = new IPara(nameof(Weighing) + nameof(DispTime_DotM), 1000, 100, 10000, EUnit.MILLISECOND);


            public static DPara[] ActualMassFlowRate = Enumerable.Range(0, Headno).Select(x => new DPara($"{nameof(ActualMassFlowRate)} H{x}", 0, 0, 99999, EUnit.MASS_FLOW_RATE)).ToArray();

            public static bool[] EnableUpdateTCmd = new bool[Headno];

            public static IPara[] RepeatCount = Enumerable.Range(0, Headno).Select(x => new IPara(nameof(SampleCount), 5, 0, 15, EUnit.COUNT)).ToArray();
            public static DPara ResultState = new DPara(nameof(Weighing) + nameof(ResultState), 0, 0, 1, EUnit.COUNT, 0, new string[] { "Average", "Total" });

            public static bool CutTailEnable = false;
            public static DPara XYCutSpeed = new DPara(nameof(Weighing) + nameof(XYCutSpeed), 10, 0, 5000, EUnit.MILLIMETER_PER_SECOND);
            public static DPara ZStepCount = new DPara(nameof(Weighing) + nameof(ZStepCount), 0, 0, 100, EUnit.COUNT, 0);
            public static DPara ZStepDist = new DPara(nameof(Weighing) + nameof(ZStepDist), 0, 0, 100, EUnit.MILLIMETER);
        }


        public class NozzleInspection
        {
            public static int DO_Idx = 0;
            public static int[] DI_Idx = new int[5];

            public static IPara WaitTime = new IPara(nameof(NozzleInspection) + nameof(WaitTime), 1000, 0, 10000, EUnit.MILLISECOND); 
        }

        public class Temp
        {
            public static bool CheckTempBeforeRun = false;
            public static IPara AwaitErrorTime = new IPara(nameof(Temp) + nameof(AwaitErrorTime), 0, 0, 1000, EUnit.SECOND);
            public static IPara IdleStopTime = new IPara(nameof(Temp) + nameof(IdleStopTime), 10, 10, 100000, EUnit.SECOND);
        }

        public class Wafer
        {
            public static IPara AirBlowDuration = new IPara(nameof(Wafer) + nameof(AirBlowDuration), 2000, 1000, 5000, EUnit.MILLISECOND);
            public static IPara PreOnVacuum = new IPara(nameof(Wafer) + nameof(PreOnVacuum), 500, 500, 1000, EUnit.MILLISECOND);

            public static IPara PreExhaustTime = new IPara(nameof(Wafer) + nameof(PreExhaustTime), 500, 5, 5000, EUnit.MILLISECOND);
            public static IPara PreExhaustDelay = new IPara(nameof(Wafer) + nameof(PreExhaustDelay), 2500, 5, 3500, EUnit.MILLISECOND);
            public static IPara PostExhaustTime = new IPara(nameof(Wafer) + nameof(PostExhaustTime), 10, 5, 1000, EUnit.MILLISECOND);

            public static IPara SmemaUpInWaitingTime = new IPara(nameof(Wafer) + nameof(SmemaUpInWaitingTime), 5000, 1000, 100000, EUnit.MILLISECOND);

            public static DPara PrecisorSpeed = new DPara(nameof(Wafer) + nameof(PrecisorSpeed), 50, 10, 500, EUnit.MILLIMETER_PER_SECOND);
            public static DPara PrecisorAccel = new DPara(nameof(Wafer) + nameof(PrecisorAccel), 250, 50, 800, EUnit.MILLIMETER_PER_SECOND_SQUARED);

            public static DPara LifterSpeed = new DPara(nameof(Wafer) + nameof(LifterSpeed), 50, 10, 500, EUnit.MILLIMETER_PER_SECOND);
            public static DPara LifterAccel = new DPara(nameof(Wafer) + nameof(LifterAccel), 250, 50, 800, EUnit.MILLIMETER_PER_SECOND_SQUARED);
            public static DPara LifterStroke = new DPara(nameof(Wafer) + nameof(LifterStroke), 21, 0, 50, EUnit.MILLIMETER);

            public static bool PreAirBlow = true;
            public static bool PreVacuumEnable = true;

            public static DPara WaferThickness = new DPara(nameof(Wafer) + nameof(WaferThickness), 0.1, 0.1, 1, EUnit.MILLIMETER);

            public static IPara NotchAngleCheck = new IPara(nameof(Wafer) + nameof(NotchAngleCheck), 30, 10, 45, EUnit.ANGLE);
            public static DPara NotchAlignSpeed = new DPara(nameof(Wafer) + nameof(NotchAlignSpeed), 10, 5, 50, EUnit.MILLIMETER_PER_SECOND);

            public static DPara NotchEdgeRev = new DPara(nameof(Wafer) + nameof(NotchEdgeRev), 1, 0.05, 2, EUnit.MILLIMETER);

            public static bool IsNotchVisionEnable = false;
            public static DPara NotchVisonScore = new DPara(nameof(Wafer) + nameof(NotchVisonScore), 0.7, 0, 1, EUnit.PERCENTAGE);
            public static IPara NotchVisonRepeatCount = new IPara(nameof(Wafer) + nameof(NotchVisonRepeatCount), 1, 1, 10, EUnit.COUNT);

            public static IPara NotchOverAngle = new IPara(nameof(Wafer) + nameof(NotchOverAngle), 0, 0, 360, EUnit.ANGLE);
            public static IPara OORCounter = new IPara(nameof(Wafer) + nameof(OORCounter), 0, 0, 10, EUnit.COUNT);

            public static bool EnableAutoHeightDetect = false;

            public static DPara PrecisorTimeout = new DPara(nameof(Wafer) + nameof(PrecisorTimeout), 0, 0, 30000, EUnit.MILLISECOND, 0);
        }

        public static bool SaveFile(string filepath)
        {
            //return GDoc.SaveXML(filepath, MethodBase.GetCurrentMethod().DeclaringType);
            return GDoc.SaveINI(filepath, MethodBase.GetCurrentMethod().DeclaringType);
        }
        public static bool SaveFile()
        {
            return SaveFile(GDoc.ProcessParaFile.FullName);
        }
        public static bool LoadFile(string filepath)
        {
            //return GDoc.LoadXML(filepath, MethodBase.GetCurrentMethod().DeclaringType);
            return GDoc.LoadINI(filepath, MethodBase.GetCurrentMethod().DeclaringType);
        }
        public static bool LoadFile()
        {
            return LoadFile(GDoc.ProcessParaFile.FullName);
        }
    }
}
