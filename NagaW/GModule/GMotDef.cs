using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace NagaW
{
    class GMotDef
    {
        public static TEZMCAux.TAxis ConvL = new TEZMCAux.TAxis(0, "Conv Left");
        public static TEZMCAux.TAxis ConvR = new TEZMCAux.TAxis(1, "Conv Right");

        public static TEZMCAux.TAxis Preciser_0 = new TEZMCAux.TAxis(0, "Preciser 0");
        public static TEZMCAux.TAxis Preciser_1 = new TEZMCAux.TAxis(1, "Preciser 1");
        public static TEZMCAux.TAxis Preciser_2 = new TEZMCAux.TAxis(2, "Preciser 2");

        public static TEZMCAux.TAxis Lifter = new TEZMCAux.TAxis(3, "Lifter");

        public static TEZMCAux.TAxis GXAxis = new TEZMCAux.TAxis(6, "GXAxis");
        public static TEZMCAux.TAxis GYAxis = new TEZMCAux.TAxis(7, "GYAxis");
        public static TEZMCAux.TAxis GZAxis = new TEZMCAux.TAxis(8, "GZAxis");

        public static TEZMCAux.TAxis GVAxis = new TEZMCAux.TAxis(9, "GVAxis");
        public static TEZMCAux.TAxis GRAxis = new TEZMCAux.TAxis(10, "GRAxis");

        public static TEZMCAux.TAxis Axis12 = new TEZMCAux.TAxis(12, "GRAxis");
        public static TEZMCAux.TAxis Axis13 = new TEZMCAux.TAxis(13, "GRAxis");
        public static TEZMCAux.TAxis Axis14 = new TEZMCAux.TAxis(14, "GRAxis");

        public static TEZMCAux.TInput IN0 = new TEZMCAux.TInput(0, "LeftPump-PressureSw");
        public static TEZMCAux.TInput IN1 = new TEZMCAux.TInput(1, "LeftPump-VacuumSw");
        public static TEZMCAux.TInput IN2 = new TEZMCAux.TInput(2, "LeftPump-FluidDetection");
        public static TEZMCAux.TInput IN3 = new TEZMCAux.TInput(3, "(Spare)");
        public static TEZMCAux.TInput IN4 = new TEZMCAux.TInput(4, "LeftHead-Vermes Ready");
        public static TEZMCAux.TInput IN5 = new TEZMCAux.TInput(5, "RightPump-PressureSw");
        public static TEZMCAux.TInput IN6 = new TEZMCAux.TInput(6, "RightPump-VacuumSw");
        public static TEZMCAux.TInput IN7 = new TEZMCAux.TInput(7, "RightPump-FluidDetection");
        public static TEZMCAux.TInput IN8 = new TEZMCAux.TInput(8, "(Spare)");
        public static TEZMCAux.TInput IN9 = new TEZMCAux.TInput(9, "RightHead-Vermes Ready");
        public static TEZMCAux.TInput IN10 = new TEZMCAux.TInput(10, "Main Air");
        public static TEZMCAux.TInput IN11 = new TEZMCAux.TInput(11, "EMO Released");
        public static TEZMCAux.TInput IN12 = new TEZMCAux.TInput(12, "LeftClean-Vacuum");
        public static TEZMCAux.TInput IN13 = new TEZMCAux.TInput(13, "RightClean-Vacuum");
        public static TEZMCAux.TInput IN14 = new TEZMCAux.TInput(14, "Door Closed");
        public static TEZMCAux.TInput IN15 = new TEZMCAux.TInput(15, "Door Locked");
        public static TEZMCAux.TInput IN16 = new TEZMCAux.TInput(16, "Door Bypass-KeySw");
        public static TEZMCAux.TInput IN17 = new TEZMCAux.TInput(17, "LeftPump-AirFlow");
        public static TEZMCAux.TInput IN18 = new TEZMCAux.TInput(18, "RightPump-AirFlow");
        public static TEZMCAux.TInput IN19 = new TEZMCAux.TInput(19, "LeftPump-TempAlarm");
        public static TEZMCAux.TInput IN20 = new TEZMCAux.TInput(20, "(Spare)");
        public static TEZMCAux.TInput IN21 = new TEZMCAux.TInput(21, "RightPump-TempAlarm");
        public static TEZMCAux.TInput IN22 = new TEZMCAux.TInput(22, "LeftClean-Vacuum");
        public static TEZMCAux.TInput IN23 = new TEZMCAux.TInput(23, "RightClean-Vacuum");
        public static TEZMCAux.TInput IN24 = new TEZMCAux.TInput(24, "(not used)");
        public static TEZMCAux.TInput IN25 = new TEZMCAux.TInput(25, "(not used)");
        public static TEZMCAux.TInput IN26 = new TEZMCAux.TInput(26, "(not used)");
        public static TEZMCAux.TInput IN27 = new TEZMCAux.TInput(27, "(not used)");
        public static TEZMCAux.TInput IN28 = new TEZMCAux.TInput(28, "(not used)");
        public static TEZMCAux.TInput IN29 = new TEZMCAux.TInput(29, "(not used)");
        public static TEZMCAux.TInput IN30 = new TEZMCAux.TInput(30, "(not used)");
        public static TEZMCAux.TInput IN31 = new TEZMCAux.TInput(31, "(not used)");
        public static TEZMCAux.TInput IN32 = new TEZMCAux.TInput(32, "SmemaUpline-BdReady");
        public static TEZMCAux.TInput IN33 = new TEZMCAux.TInput(33, "SmemaDnline-McReady");
        public static TEZMCAux.TInput IN34 = new TEZMCAux.TInput(34, "SmemaUpline2-McReady");
        public static TEZMCAux.TInput IN35 = new TEZMCAux.TInput(35, "SmemaDnline2-BdReady");
        public static TEZMCAux.TInput IN36 = new TEZMCAux.TInput(36, "LeftConv-BoardIn");
        public static TEZMCAux.TInput IN37 = new TEZMCAux.TInput(37, "LeftConv-BoardPresent");
        public static TEZMCAux.TInput IN38 = new TEZMCAux.TInput(38, "LeftConv-StopperUp");
        public static TEZMCAux.TInput IN39 = new TEZMCAux.TInput(39, "LeftConv-Up");
        public static TEZMCAux.TInput IN40 = new TEZMCAux.TInput(40, "LeftConv-Down");
        public static TEZMCAux.TInput IN41 = new TEZMCAux.TInput(41, "LeftConv-SensChuckVacuum");
        public static TEZMCAux.TInput IN42 = new TEZMCAux.TInput(42, "RightConv-BoardPresent");
        public static TEZMCAux.TInput IN43 = new TEZMCAux.TInput(43, "RightConv-BoardOut");
        public static TEZMCAux.TInput IN44 = new TEZMCAux.TInput(44, "RightConv-StopperUp");
        public static TEZMCAux.TInput IN45 = new TEZMCAux.TInput(45, "RightConv-Up");
        public static TEZMCAux.TInput IN46 = new TEZMCAux.TInput(46, "RightConv-Down");
        public static TEZMCAux.TInput IN47 = new TEZMCAux.TInput(47, "RightConv-SensChuckVacuum");
        public static TEZMCAux.TInput IN48 = new TEZMCAux.TInput(48, "(not used)");
        public static TEZMCAux.TInput IN49 = new TEZMCAux.TInput(49, "(not used)");
        public static TEZMCAux.TInput IN50 = new TEZMCAux.TInput(50, "(not used)");
        public static TEZMCAux.TInput IN51 = new TEZMCAux.TInput(51, "(not used)");
        public static TEZMCAux.TInput IN52 = new TEZMCAux.TInput(52, "(not used)");
        public static TEZMCAux.TInput IN53 = new TEZMCAux.TInput(53, "(not used)");
        public static TEZMCAux.TInput IN54 = new TEZMCAux.TInput(54, "(not used)");
        public static TEZMCAux.TInput IN55 = new TEZMCAux.TInput(55, "(not used)");
        public static TEZMCAux.TInput IN56 = new TEZMCAux.TInput(56, "(not used)");
        public static TEZMCAux.TInput IN57 = new TEZMCAux.TInput(57, "(not used)");
        public static TEZMCAux.TInput IN58 = new TEZMCAux.TInput(58, "(not used)");
        public static TEZMCAux.TInput IN59 = new TEZMCAux.TInput(59, "(not used)");
        public static TEZMCAux.TInput IN60 = new TEZMCAux.TInput(60, "(not used)");
        public static TEZMCAux.TInput IN61 = new TEZMCAux.TInput(61, "(not used)");
        public static TEZMCAux.TInput IN62 = new TEZMCAux.TInput(62, "(not used)");
        public static TEZMCAux.TInput IN63 = new TEZMCAux.TInput(63, "(not used)");

        public static TEZMCAux.TOutput Out0 = new TEZMCAux.TOutput(0, "LeftPump-FPressure");
        public static TEZMCAux.TOutput Out1 = new TEZMCAux.TOutput(1, "LeftPump-Vacuum");
        public static TEZMCAux.TOutput Out2 = new TEZMCAux.TOutput(2, "LeftPump-PPressure");
        public static TEZMCAux.TOutput Out3 = new TEZMCAux.TOutput(3, "LeftHead-VermesTrigger");
        public static TEZMCAux.TOutput Out4 = new TEZMCAux.TOutput(4, "LeftCamera-GPIO");
        public static TEZMCAux.TOutput Out5 = new TEZMCAux.TOutput(5, "RightPump-FPressure");
        public static TEZMCAux.TOutput Out6 = new TEZMCAux.TOutput(6, "RightPump-Vacuum");
        public static TEZMCAux.TOutput Out7 = new TEZMCAux.TOutput(7, "RightPump-PPressure");
        public static TEZMCAux.TOutput Out8 = new TEZMCAux.TOutput(8, "RightHead-VermesTrigger");
        public static TEZMCAux.TOutput Out9 = new TEZMCAux.TOutput(9, "RightCamera_GPIO");
        public static TEZMCAux.TOutput Out10 = new TEZMCAux.TOutput(10, "(Spare)");
        public static TEZMCAux.TOutput Out11 = new TEZMCAux.TOutput(11, "Door Lock");
        public static TEZMCAux.TOutput Out12 = new TEZMCAux.TOutput(12, "(Spare)");
        public static TEZMCAux.TOutput Out13 = new TEZMCAux.TOutput(13, "(Spare)");
        public static TEZMCAux.TOutput Out14 = new TEZMCAux.TOutput(14, "(Spare)");
        public static TEZMCAux.TOutput Out15 = new TEZMCAux.TOutput(15, "(Spare)");

        public static TEZMCAux.TOutput Out16 = new TEZMCAux.TOutput(16, "(not used)");
        public static TEZMCAux.TOutput Out17 = new TEZMCAux.TOutput(17, "not used");
        public static TEZMCAux.TOutput Out18 = new TEZMCAux.TOutput(18, "not used");
        public static TEZMCAux.TOutput Out19 = new TEZMCAux.TOutput(19, "not used");
        public static TEZMCAux.TOutput Out20 = new TEZMCAux.TOutput(20, "not used");
        public static TEZMCAux.TOutput Out21 = new TEZMCAux.TOutput(21, "not used");
        public static TEZMCAux.TOutput Out22 = new TEZMCAux.TOutput(22, "not used");
        public static TEZMCAux.TOutput Out23 = new TEZMCAux.TOutput(23, "not used");
        public static TEZMCAux.TOutput Out24 = new TEZMCAux.TOutput(24, "not used");
        public static TEZMCAux.TOutput Out25 = new TEZMCAux.TOutput(25, "not used");
        public static TEZMCAux.TOutput Out26 = new TEZMCAux.TOutput(26, "not used");
        public static TEZMCAux.TOutput Out27 = new TEZMCAux.TOutput(27, "not used");
        public static TEZMCAux.TOutput Out28 = new TEZMCAux.TOutput(28, "not used");
        public static TEZMCAux.TOutput Out29 = new TEZMCAux.TOutput(29, "not used");
        public static TEZMCAux.TOutput Out30 = new TEZMCAux.TOutput(30, "not used");
        public static TEZMCAux.TOutput Out31 = new TEZMCAux.TOutput(31, "not used");

        public static TEZMCAux.TOutput Out32 = new TEZMCAux.TOutput(32, "TwLight-Red");
        public static TEZMCAux.TOutput Out33= new TEZMCAux.TOutput(33, "TwLight-Yellow");
        public static TEZMCAux.TOutput Out34 = new TEZMCAux.TOutput(34, "TwLight-Green");
        public static TEZMCAux.TOutput Out35 = new TEZMCAux.TOutput(35, "Buzzer");
        public static TEZMCAux.TOutput Out36= new TEZMCAux.TOutput(36, "SmemaUpline-McReady");
        public static TEZMCAux.TOutput Out37= new TEZMCAux.TOutput(37, "SmemaDnline-BdReady");
        public static TEZMCAux.TOutput Out38 = new TEZMCAux.TOutput(38, "(Spare)");
        public static TEZMCAux.TOutput Out39 = new TEZMCAux.TOutput(39, "LeftConv-Stopper");
        public static TEZMCAux.TOutput Out40 = new TEZMCAux.TOutput(40, "LeftConv-Up");
        public static TEZMCAux.TOutput Out41 = new TEZMCAux.TOutput(41, "LeftConv-Down");
        public static TEZMCAux.TOutput Out42 = new TEZMCAux.TOutput(42, "LeftConv-Vacuum");
        public static TEZMCAux.TOutput Out43 = new TEZMCAux.TOutput(43, "Left-CleanPressure");
        public static TEZMCAux.TOutput Out44 = new TEZMCAux.TOutput(44, "Left-CleanVaccum");
        public static TEZMCAux.TOutput Out45 = new TEZMCAux.TOutput(45, "RightConv-Stopper");
        public static TEZMCAux.TOutput Out46 = new TEZMCAux.TOutput(46, "RightConv-Up");
        public static TEZMCAux.TOutput Out47 = new TEZMCAux.TOutput(47, "RightConv-Down");
        public static TEZMCAux.TOutput Out48 = new TEZMCAux.TOutput(48, "RightConv-Vacuum");
        public static TEZMCAux.TOutput Out49 = new TEZMCAux.TOutput(49, "Right-CleanPressure");
        public static TEZMCAux.TOutput Out50 = new TEZMCAux.TOutput(50, "Right-CleanVacuum");
        public static TEZMCAux.TOutput Out51 = new TEZMCAux.TOutput(51, "SmemaUpline2-BdReady");
        public static TEZMCAux.TOutput Out52 = new TEZMCAux.TOutput(52, "SmemaDnline2-McReady");
        public static TEZMCAux.TOutput Out53 = new TEZMCAux.TOutput(53, "(Spare)");
        public static TEZMCAux.TOutput Out54 = new TEZMCAux.TOutput(54, "(Spare)");
        public static TEZMCAux.TOutput Out55 = new TEZMCAux.TOutput(55, "(Spare)");
        public static TEZMCAux.TOutput Out56 = new TEZMCAux.TOutput(56, "(Spare)");
        public static TEZMCAux.TOutput Out57 = new TEZMCAux.TOutput(57, "(Spare)");
        public static TEZMCAux.TOutput Out58 = new TEZMCAux.TOutput(58, "(Spare)");
        public static TEZMCAux.TOutput Out59 = new TEZMCAux.TOutput(59, "(Spare)");
        public static TEZMCAux.TOutput Out60 = new TEZMCAux.TOutput(60, "(Spare)");
        public static TEZMCAux.TOutput Out61 = new TEZMCAux.TOutput(61, "(Spare)");
        public static TEZMCAux.TOutput Out62 = new TEZMCAux.TOutput(62, "(Spare)");
        public static TEZMCAux.TOutput Out63 = new TEZMCAux.TOutput(63, "(Spare)");
        public static TEZMCAux.TInput[] Inputs
        {
            get
            {
                return typeof(GMotDef).GetFields(BindingFlags.Public | BindingFlags.Static)
                    .Where(x => x.FieldType == typeof(TEZMCAux.TInput))
                    .Select(x => (TEZMCAux.TInput)x.GetValue(null)).ToArray();
            }
        }
        public static TEZMCAux.TOutput[] Outputs
        {
            get
            {
                return typeof(GMotDef).GetFields(BindingFlags.Public | BindingFlags.Static)
                    .Where(x => x.FieldType == typeof(TEZMCAux.TOutput))
                    .Select(x => (TEZMCAux.TOutput)x.GetValue(null)).ToArray();
            }
        }

        public static TEZMCAux.TInput EMO_Ready { get => GMotDef.IN11; }

        public static bool SaveFile(string filename)
        {
            //return GDoc.SaveXML(filename, MethodBase.GetCurrentMethod().DeclaringType);
            return GDoc.SaveINI(filename, MethodBase.GetCurrentMethod().DeclaringType);
        }
        public static bool SaveFile()
        {
            return SaveFile(GDoc.DIOFile.FullName);
        }

        public static bool LoadFile(string filename)
        {
            //return GDoc.LoadXML(filename, MethodBase.GetCurrentMethod().DeclaringType);
            return GDoc.LoadINI(filename, MethodBase.GetCurrentMethod().DeclaringType);
        }
        public static bool LoadFile()
        {
            return LoadFile(GDoc.DIOFile.FullName);
        }
    }

    public enum EDOutput
    {
        DO00,
        DO01,
        DO02,
        DO03,
        DO04,
        DO05,
        DO06,
        DO07,
        DO08,
        DO09,

        DO10,
        DO11,
        DO12,
        DO13,
        DO14,
        DO15,
        DO16,
        DO17,
        DO18,
        DO19,

        DO20,
        DO21,
        DO22,
        DO23,
        DO24,
        DO25,
        DO26,
        DO27,
        DO28,
        DO29,

        DO30,
        DO31,
        DO32,
        DO33,
        DO34,
        DO35,
        DO36,
        DO37,
        DO38,
        DO39,

        DO40,
        DO41,
        DO42,
        DO43 = 43,
        DO44 = 44,
        DO45,
        DO46,
        DO47,
        DO48,
        DO49 = 49,
        DO50 = 50
    }

    public enum EDInput
    {
        DI00,
        DI01,
        DI02,
        DI03,
        DI04,
        DI05,
        DI06,
        DI07,
        DI08,
        DI09
    }
}
