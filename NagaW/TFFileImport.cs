using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace NagaW
{
    //using ExcelFile_XSSF = NPOI.XSSF.UserModel;
    //using ExcelFile_HSSF = NPOI.HSSF.UserModel;

    public class TFFileImport
    {
        public enum EFileType { Gerber_RS274X, OBD_v7, DXF, Excel_New };
        public enum EUnit { undefined, mm, inch }
        public enum EInterpolation { None, Linear, ClockWise, CounterClockWise, };

        public static bool OptimizeAll = false;

        public class Gerber_RS274X
        {
            public static bool Decode(string filename, ref List<TFunction> function)
            {
                int apertureIdx = 0;
                int tempApID = 0;
                string latest_aperture = "";
                PointD latest_point = new PointD(0, 0);
                PointI xDigit = new PointI(0, 0);
                PointI yDigit = new PointI(0, 0);
                EUnit unit = EUnit.mm;

                bool regionFeature = false;
                var interMode = EInterpolation.None;
                try
                {
                    using (StreamReader reader = new StreamReader(filename))
                    {
                        var filedata = reader.ReadToEnd();
                        var filedatas = filedata.Split(new string[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (var data in filedatas)
                        {
                            string line = data;
                            Char[] replacements = new Char[] { '%', '*' };
                            foreach (Char c in replacements)
                            {
                                if (line.Contains(c))
                                    line = line.Replace(c.ToString(), String.Empty);
                            }

                            if (line.StartsWith("M02")) break;

                            if (line.StartsWith("LP")) continue;

                            #region Coordinate Format, Digits
                            if (line.StartsWith("FS"))
                            {
                                var temp = line.Split(new char[] { 'X', 'Y' }, StringSplitOptions.RemoveEmptyEntries);
                                int.TryParse(temp[1][0].ToString(), out int x1);
                                int.TryParse(temp[1][1].ToString(), out int x2);
                                xDigit = new PointI(x1, x2);
                                int.TryParse(temp[2][0].ToString(), out int y1);
                                int.TryParse(temp[2][1].ToString(), out int y2);
                                yDigit = new PointI(y1, y2);
                                continue;
                            }
                            #endregion

                            #region Unit
                            if (line.StartsWith("MO"))
                            {
                                if (line.Contains("MM")) unit = EUnit.mm;
                                else if (line.Contains("IN")) unit = EUnit.inch;

                                continue;
                            }
                            #endregion

                            #region Aperture
                            if (line.StartsWith("AD"))
                            {
                                line = line.Remove(0, 2);
                                var temp = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                var type = temp[0][3];
                                switch (type)
                                {
                                    case 'C':
                                        {
                                            double.TryParse(temp[1], out double result);
                                            var finalXY = Convert(xDigit, yDigit, new double[] { result, result });
                                            PointD size = unit == EUnit.inch ? new PointD(finalXY[0] * 25.4, finalXY[1] * 25.3) : new PointD(finalXY[0], finalXY[1]);

                                            function.Add(new TFunction() { Name = temp[0] });
                                            //feature.Apertures.Add(new TApertures(apertureIdx.ToString(), temp[0], temp[1], size, EApertureType.Circle));
                                            apertureIdx++;
                                            break;
                                        }
                                    case 'O':
                                    case 'R':
                                        {
                                            var tempSize = temp[1].Split(new char[] { 'X' }, StringSplitOptions.RemoveEmptyEntries);
                                            double.TryParse(tempSize[0], out double xResult);
                                            double.TryParse(tempSize[1], out double yResult);
                                            var finalXY = Convert(xDigit, yDigit, new double[] { xResult, yResult });
                                            PointD size = unit == EUnit.inch ? new PointD(finalXY[0] * 25.4, finalXY[1] * 25.4) : new PointD(finalXY[0], finalXY[1]);

                                            function.Add(new TFunction() { Name = temp[0] });
                                            //feature.Apertures.Add(new TApertures(apertureIdx.ToString(), temp[0], temp[1], size, EApertureType.Rectangle));
                                            apertureIdx++;
                                            break;
                                        }
                                    default:
                                        {
                                            function.Add(new TFunction() { Name = temp[0] });
                                            //feature.Apertures.Add(new TApertures(apertureIdx.ToString(), temp[0], "", new PointD(0, 0)));
                                            apertureIdx++;
                                            break;
                                        }
                                }
                                continue;
                            }
                            #endregion

                            //Check Aperture
                            if (line.StartsWith("D"))
                            {
                                latest_aperture = line;
                                interMode = EInterpolation.None;
                                continue;
                            }

                            #region Region Statement
                            if (line.StartsWith("G"))
                            {
                                switch (line)
                                {
                                    case "G36": regionFeature = true; break;
                                    case "G37": regionFeature = false; break;
                                    case "G01": interMode = EInterpolation.Linear; break;
                                    case "G02": interMode = EInterpolation.ClockWise; break;
                                    case "G03": interMode = EInterpolation.CounterClockWise; break;
                                }
                                continue;
                            }
                            #endregion

                            #region Coordinate
                            if (latest_aperture != "")
                            {
                                double x = 0; double y = 0; double d = 0;
                                bool x_contain = line.Contains("X");
                                bool y_contain = line.Contains("Y");
                                var temp = line.Split(new char[] { 'X', 'Y', 'I', 'J', 'D' }, StringSplitOptions.RemoveEmptyEntries);

                                var id = function.Where(a => a.Name.Contains(latest_aperture)).ToList();
                                if (id.Count() > 0) tempApID = function.IndexOf(id[0]);

                                if (x_contain && y_contain)
                                {
                                    double.TryParse(temp[0], out x);
                                    double.TryParse(temp[1], out y);
                                    double.TryParse(temp[2], out d);
                                }
                                else if (x_contain && !y_contain)
                                {
                                    double.TryParse(temp[0], out x);
                                    double.TryParse(temp[1], out d);
                                    y = latest_point.Y;
                                }
                                else
                                {
                                    double.TryParse(temp[0], out y);
                                    double.TryParse(temp[1], out d);
                                    x = latest_point.X;
                                    //x = 0;
                                }
                                latest_point = new PointD(x, y);

                                var finalXY = Convert(xDigit, yDigit, new double[] { x, y });

                                PointD tempPt = unit == EUnit.inch ? new PointD(finalXY[0] * 25.4, finalXY[1] * 25.4) : new PointD(finalXY[0], finalXY[1]);

                                switch (interMode)
                                {
                                    case EInterpolation.CounterClockWise:
                                    case EInterpolation.ClockWise:
                                        double i, j = 0;
                                        if (temp.Length > 3) double.TryParse(temp[3], out j);
                                        double.TryParse(temp[2], out i);
                                        var tempRad = Convert(xDigit, yDigit, new double[] { i, j });
                                        var radius = tempRad[0] == 0 ? Math.Abs(tempRad[1]) : Math.Abs(tempRad[0]);
                                        TCmd cmd = new TCmd(ECmd.DOT); cmd.Para[0] = tempPt.X; cmd.Para[1] = tempPt.Y;
                                        function[tempApID].Cmds.Add(new TCmd(cmd));
                                        //feature.Features.Add(new TFeatures(feature.Features.Count, tempApID, EFeatureType.Arc, new PointD(tempPt), radius));
                                        break;
                                    case EInterpolation.Linear:
                                    default:
                                        TCmd tempTcmd = new TCmd(ECmd.NONE);
                                        var tempCount = function[tempApID].Cmds.Count - 1;

                                        var prevCmd = tempCount >= 0 ? function[tempApID].Cmds[tempCount] : new TCmd(ECmd.NONE);
                                        if (d is 1)
                                        {
                                            if (prevCmd.Cmd is ECmd.DOT)
                                            {
                                                tempTcmd = new TCmd(ECmd.LINE_START); tempTcmd.Para[0] = prevCmd.Para[0]; tempTcmd.Para[1] = prevCmd.Para[1];
                                                function[tempApID].Cmds[tempCount] = new TCmd(tempTcmd);
                                            }
                                            tempTcmd = new TCmd(ECmd.LINE_PASS); tempTcmd.Para[0] = tempPt.X; tempTcmd.Para[1] = tempPt.Y;
                                            function[tempApID].Cmds.Add(new TCmd(tempTcmd));
                                        }
                                        else
                                        {
                                            tempTcmd = new TCmd(ECmd.DOT); tempTcmd.Para[0] = tempPt.X; tempTcmd.Para[1] = tempPt.Y;
                                            function[tempApID].Cmds.Add(new TCmd(tempTcmd));

                                            if (prevCmd.Cmd is ECmd.LINE_PASS)
                                            {
                                                tempTcmd = new TCmd(ECmd.LINE_END); tempTcmd.Para[0] = prevCmd.Para[0]; tempTcmd.Para[1] = prevCmd.Para[1];
                                                function[tempApID].Cmds[tempCount] = new TCmd(tempTcmd);
                                            }
                                        }

                                        break;
                                }

                                continue;
                            }
                            #endregion
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

                return true;
            }

            public static double[] Convert(PointI xDigit, PointI yDigit, double[] xy)
            {
                string xa = xy[0].ToString().Replace("-", "");
                string ya = xy[1].ToString().Replace("-", "");

                xa = xa.PadLeft(xDigit.X + xDigit.Y, '0').Insert(xDigit.X, ".");
                ya = ya.PadLeft(yDigit.X + yDigit.Y, '0').Insert(yDigit.X, ".");

                if (xy[0].ToString().StartsWith("-")) xa = "-" + xa; if (xy[1].ToString().StartsWith("-")) ya = "-" + ya;
                double.TryParse(xa, out double xResult); double.TryParse(ya, out double yResult);
                return new double[] { xResult, yResult };
            }
        }

        public class ODBPP
        {
            public static bool Decode(string fileName, ref List<TFunction> function)
            {
                #region Key 
                /* List essential commands only, # - comments
                 * #
                 * #Units
                 * #U
                 * U MM
                 * .....
                 * #
                 * #Feature symbol names
                 * #
                 * $0 r120 
                 * $1 rect20x60 M # rect 20 by 60 microns
                 * $2 rect3x5 I #rect 3 by 5 mils
                 * $3 r10
                 * .....
                 * #
                 * #Layer features
                 * #
                 * P -0.198 1.62 16 P 0 3;3=2,4=0
                 * L 3.834 -1.16 3.86728 -1.16 2 P 0 ;1,3=0
                 */
                #endregion
                #region Parse Algo
                /*
                 * 1. Split linefeeds
                 * 2. Parse for Uxxx -> define unit, contains INCH or MM
                 * 3. Parse for $ -> define features symbols names
                 * 4. Parse for P -> define pad position D <x> <y> <apt_def> <polarity> <dcode> <orient_def>
                 * 5. Parse for L -> define line position L <xs> <ys> <xe> <ye> <sym_num> <polarity> <dcode>
                 */
                #endregion

                EUnit unit = EUnit.inch;
                int tempApID = -1;
                TCmd tempTcmd = new TCmd(ECmd.NONE);

                try
                {
                    using (StreamReader reader = new StreamReader(fileName))
                    {
                        string Line = reader.ReadToEnd();
                        string[] lines = Line.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                        //int apertureIndex = -1;

                        foreach (string _line in lines)
                        {
                            string line = _line;

                            if (line.StartsWith("U"))
                            {
                                if (line.Contains("MM"))
                                    unit = EUnit.mm;
                                continue;
                            }
                            if (line.StartsWith("$"))
                            {
                                string s = line;
                                //$1 rect20x60 M # rect 20 by 60 microns
                                s = s.Remove(0, 1);
                                //1 rect20x60 M # rect 20 by 60 microns
                                string[] split = s.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                                string id = split[0];
                                string name = split[1];
                                string para = split[1];
                                function.Add(new TFunction() { Name = name });

                                tempApID++;
                                continue;
                            }
                            if (line.StartsWith("P"))
                            {
                                string sx = "";
                                string sy = "";
                                string symbol = "";
                                //P -0.198 1.62 16 P 0 3;3=2,4=0
                                string[] split = line.Split(new string[] { " ", ";" }, StringSplitOptions.RemoveEmptyEntries);
                                sx = split[1];
                                sy = split[2];
                                symbol = split[3];

                                double x = 0; double.TryParse(sx, out x);
                                double y = 0; double.TryParse(sy, out y);
                                int s = 0; int.TryParse(symbol, out s);

                                if (unit == EUnit.inch)
                                {
                                    x *= 25.4;
                                    y *= 25.4;
                                }

                                tempTcmd = new TCmd(ECmd.DOT); tempTcmd.Para[0] = x; tempTcmd.Para[1] = y;
                                function[tempApID].Cmds.Add(tempTcmd);
                            }
                            if (line.StartsWith("L"))
                            {
                                //L 3.834 -1.16 3.86728 -1.16 2 P 0; 1,3 = 0
                                string sx = "";
                                string sy = "";
                                string sx2 = "";
                                string sy2 = "";
                                string symbol = "";
                                string[] split = line.Split(new string[] { " ", ";" }, StringSplitOptions.RemoveEmptyEntries);
                                sx = split[1];
                                sy = split[2];
                                sx2 = split[3];
                                sy2 = split[4];
                                symbol = split[5];

                                double x = 0; double.TryParse(sx, out x);
                                double y = 0; double.TryParse(sy, out y);
                                double x2 = 0; double.TryParse(sx, out x2);
                                double y2 = 0; double.TryParse(sy, out y2);
                                int s = 0; int.TryParse(symbol, out s);

                                if (unit == EUnit.inch) { x *= 25.4; y *= 25.4; x2 *= 25.4; y2 *= 25.4; }

                                tempTcmd = new TCmd(ECmd.LINE_START); tempTcmd.Para[0] = x; tempTcmd.Para[1] = y;
                                function[tempApID].Cmds.Add(tempTcmd);

                                tempTcmd = new TCmd(ECmd.LINE_END); tempTcmd.Para[0] = x2; tempTcmd.Para[1] = y2;
                                function[tempApID].Cmds.Add(tempTcmd);

                                continue;
                            }
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); };
                return true;
            }
        }

        public class DXF
        {
            class LineValue
            {
                public int Value { get; set; }
                public string Code { get; set; }
                public LineValue(int value, string code)
                {
                    Value = value;
                    Code = code;
                }
            }
            static StreamReader DXFReader = null;
            public static bool Decode(string filename, ref List<TFunction> function)
            {
                double unit = 1;
                int aUnit = 0;

                int tempApID = 0;
                int apertureIndex = -1;
                int featureIndex = -1;
                bool entitysection = false;
                TCmd tempTcmd = new TCmd(ECmd.NONE);

                try
                {
                    DXFReader = new StreamReader(filename);
                    LineValue line = ReadPair();

                    while (line.Code != "EOF"/*(!DXFReader.EndOfStream)*/)
                    {
                        if (line.Value == 0)
                        {
                            if (!entitysection)
                            {
                                switch (line.Code)
                                {
                                    case "SECTION":
                                        line = ReadPair();
                                        string entity = "";
                                        while (line.Value != 0)
                                        {
                                            if (line.Value == 2)
                                            {
                                                entity = line.Code;
                                                break;
                                            }
                                            line = ReadPair();
                                        }
                                        if (entity == "ENTITIES")
                                        {
                                            apertureIndex++;
                                            entitysection = true;
                                            function.Add(new TFunction() { Name = apertureIndex.ToString() });
                                        }
                                        break;
                                    default:
                                        line = ReadPair();
                                        break;
                                }
                            }
                            else
                            {
                                switch (line.Code)
                                {
                                    case "LINE":
                                        {
                                            featureIndex++;
                                            int id = 0;
                                            double x = 0;
                                            double y = 0;
                                            double x2 = 0;
                                            double y2 = 0;
                                            line = ReadPair();
                                            while (line.Value != 0)
                                            {
                                                switch (line.Value)
                                                {
                                                    case 8:
                                                        //id = int.Parse(line.Code);
                                                        break;
                                                    case 10:
                                                        x = Convert.ToDouble(line.Code) * unit;
                                                        break;
                                                    case 20:
                                                        y = Convert.ToDouble(line.Code) * unit;
                                                        break;
                                                    case 11:
                                                        x2 = Convert.ToDouble(line.Code) * unit;
                                                        break;
                                                    case 21:
                                                        y2 = Convert.ToDouble(line.Code) * unit;
                                                        break;
                                                }
                                                line = ReadPair();
                                            }

                                            var funcID = function.Where(a => a.Name.Contains(apertureIndex.ToString())).ToList();
                                            if (funcID.Count() > 0) tempApID = function.IndexOf(funcID[0]);

                                            tempTcmd = new TCmd(ECmd.LINE_START); tempTcmd.Para[0] = x; tempTcmd.Para[1] = y;
                                            function[tempApID].Cmds.Add(new TCmd(tempTcmd));

                                            tempTcmd = new TCmd(ECmd.LINE_END); tempTcmd.Para[0] = x2; tempTcmd.Para[1] = y2;
                                            function[tempApID].Cmds.Add(new TCmd(tempTcmd));

                                            break;
                                        }
                                    case "LWPOLYLINE":
                                        {
                                            featureIndex++;
                                            int vertices = 0;
                                            List<double> x = new List<double>();
                                            List<double> y = new List<double>();
                                            line = ReadPair();
                                            while (line.Value != 0)
                                            {
                                                switch (line.Value)
                                                {
                                                    case 90:
                                                        vertices = int.Parse(line.Code);
                                                        break;
                                                    case 10:
                                                        x.Add(Convert.ToDouble(line.Code));
                                                        break;
                                                    case 20:
                                                        y.Add(Convert.ToDouble(line.Code));
                                                        break;
                                                }
                                                line = ReadPair();
                                            }

                                            var funcID = function.Where(a => a.Name.Contains(apertureIndex.ToString())).ToList();
                                            if (funcID.Count() > 0) tempApID = function.IndexOf(funcID[0]);

                                            tempTcmd = new TCmd(ECmd.LINE_START); tempTcmd.Para[0] = x[0]; tempTcmd.Para[1] = y[0];
                                            function[tempApID].Cmds.Add(new TCmd(tempTcmd));

                                            for (int i = 1; i < x.Count; i++)
                                            {
                                                tempTcmd = new TCmd(ECmd.LINE_PASS); tempTcmd.Para[0] = x[i]; tempTcmd.Para[1] = y[i];
                                                function[tempApID].Cmds.Add(new TCmd(tempTcmd));
                                            }

                                            function[tempApID].Cmds[function[tempApID].Cmds.Count - 1].Cmd = ECmd.LINE_END;

                                            break;
                                        }
                                    case "CIRCLE":
                                        {
                                            break;
                                        }
                                    case "ARC":
                                        {
                                            featureIndex++;
                                            int id = 0;
                                            double centreX = 0;
                                            double centreY = 0;
                                            double radius = 0;
                                            double startAngle = 0;
                                            double endAngle = 0;
                                            line = ReadPair();
                                            while (line.Value != 0)
                                            {
                                                switch (line.Value)
                                                {
                                                    case 8:
                                                        //id = int.Parse(line.Code);
                                                        break;
                                                    case 10:
                                                        centreX = Convert.ToDouble(line.Code) * unit;
                                                        break;
                                                    case 20:
                                                        centreY = Convert.ToDouble(line.Code) * unit;
                                                        break;
                                                    case 40:
                                                        radius = Convert.ToDouble(line.Code) * unit;
                                                        break;
                                                    case 50:
                                                        startAngle = Convert.ToDouble(line.Code);
                                                        break;
                                                    case 51:
                                                        endAngle = Convert.ToDouble(line.Code);
                                                        break;
                                                }
                                                line = ReadPair();
                                            }
                                            double sRad = RadianValue(aUnit, startAngle);
                                            double eRad = RadianValue(aUnit, endAngle)/* - sRad*/;
                                            //eRad = eRad < 0 ? -1 * eRad : eRad;
                                            var refAngle = endAngle > startAngle ? (endAngle + startAngle) / 2 : ((endAngle + 360) + startAngle) / 2;
                                            double refRad = RadianValue(aUnit, refAngle) /*- sRad*/;
                                            double startPt_X = centreX + (Math.Cos(sRad) * radius);
                                            double startPt_Y = centreY + (Math.Sin(sRad) * radius);
                                            double endPt_X = centreX + (Math.Cos(eRad) * radius);
                                            double endPt_Y = centreY + (Math.Sin(eRad) * radius);
                                            double refPt_X = centreX + (Math.Cos(refRad) * radius);
                                            double refPt_Y = centreY + (Math.Sin(refRad) * radius);

                                            tempTcmd = new TCmd(ECmd.LINE_START); tempTcmd.Para[0] = startPt_X; tempTcmd.Para[1] = startPt_Y;
                                            function[tempApID].Cmds.Add(new TCmd(tempTcmd));

                                            tempTcmd = new TCmd(ECmd.ARC_PASS); tempTcmd.Para[0] = refPt_X; tempTcmd.Para[1] = refPt_X; tempTcmd.Para[3] = endPt_X; tempTcmd.Para[4] = endPt_Y;
                                            function[tempApID].Cmds.Add(new TCmd(tempTcmd));

                                            tempTcmd = new TCmd(ECmd.LINE_END); tempTcmd.Para[0] = endPt_X; tempTcmd.Para[1] = endPt_Y; 
                                            function[tempApID].Cmds.Add(new TCmd(tempTcmd));
                                            break;
                                        }
                                    case "POINT":
                                        {
                                            featureIndex++;
                                            double x = 0;
                                            double y = 0;
                                            line = ReadPair();
                                            while (line.Value != 0)
                                            {
                                                switch (line.Value)
                                                {
                                                    case 10:
                                                        x = Convert.ToDouble(line.Code) * unit;
                                                        break;
                                                    case 20:
                                                        y = Convert.ToDouble(line.Code) * unit;
                                                        break;
                                                }
                                                line = ReadPair();
                                            }
                                            tempTcmd = new TCmd(ECmd.DOT); tempTcmd.Para[0] = x; tempTcmd.Para[1] = y;
                                            function[tempApID].Cmds.Add(new TCmd(tempTcmd));

                                            break;
                                        }
                                    default:
                                        {
                                            line = ReadPair();
                                            break;
                                        }
                                }
                            }
                        }
                        else if (line.Value == 9)
                        {
                            switch (line.Code)
                            {
                                case "$INSUNITS":
                                    line = ReadPair();
                                    unit = ReadUnits(line);
                                    break;
                                case "$AUNITS":
                                    line = ReadPair();
                                    aUnit = int.Parse(line.Code);
                                    break;
                                default:
                                    line = ReadPair();
                                    break;
                            }
                        }
                        else line = ReadPair();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    GAlarm.Prompt(EAlarm.DXF_FILE_ERROR, ex.Message);
                    return false;
                }
            }

            private static LineValue ReadPair()
            {
                string line = "";
                string code = "";
                int value;

                line = DXFReader.ReadLine();
                if (line != null) line = line.Trim();
                if (!int.TryParse(line, out value))
                {
                    throw new Exception("Invalid Code.");
                }

                code = DXFReader.ReadLine();
                if (code != null) code = code.Trim();
                return new LineValue(value, code);
            }
            private static double ReadUnits(LineValue line)
            {
                switch (line.Code)
                {
                    case "1":
                        return 25.4;
                    case "2":
                        return 304.8;
                    case "3":
                        return 1609344;
                    case "4":
                        return 1;
                    case "5":
                        return 10;
                    case "6":
                        return 1000;
                    case "7":
                        return 1000000;
                }
                return 0;
            }
            private static double RadianValue(int aUnit, double angle)
            {
                switch (aUnit)
                {
                    case 0:
                        return angle * Math.PI / 180;
                    case 3:
                        return angle;
                }
                return angle;
            }
        }

        public class Excel
        {
            //public static bool Decode(string filename, ref TDispFeature feature)
            //{
            //    ExcelFile.Application xlApp;
            //    ExcelFile.Workbook xlWorkBook;
            //    ExcelFile.Worksheet xlWorkSheet;
            //    ExcelFile.Range range;

            //    PointD point = new PointD();
            //    int row = 0;
            //    int col = 0;

            //    int apertureIndex = 0;
            //    int featureIndex = 0;
            //    feature = new TDispFeature();

            //    try
            //    {
            //        xlApp = new ExcelFile.Application();
            //        xlWorkBook = xlApp.Workbooks.Open(filename);
            //        xlWorkSheet = xlWorkBook.Worksheets.Item[1];

            //        range = xlWorkSheet.UsedRange;
            //        row = range.Rows.Count;
            //        col = range.Columns.Count;

            //        feature.Apertures.Add(new TApertures(apertureIndex.ToString(), "Function 1", "", new PointD(0, 0)));

            //        for (int i = 2; i < row; i++)
            //        {
            //            double x = Convert.ToDouble((range.Cells[i, 2] as ExcelFile.Range).Value2);
            //            double y = Convert.ToDouble((range.Cells[i, 3] as ExcelFile.Range).Value2);

            //            point = new PointD(x, y);

            //            feature.Features.Add(new TFeatures(featureIndex, apertureIndex, EFeatureType.Pad, point));

            //            if (i % 400 is 0)
            //            {
            //                apertureIndex++;
            //                featureIndex = 0;
            //                feature.Apertures.Add(new TApertures(apertureIndex.ToString(), $"Function {apertureIndex + 1}", "", new PointD(0, 0)));
            //            }
            //            featureIndex++;
            //        }

            //    }
            //    catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); };

            //    return true;
            //}
            public static bool Decode(string filename, ref List<TFunction> function)
            {
                //ExcelFile_XSSF.XSSFWorkbook xssfWorkbook;
                //ExcelFile_XSSF.XSSFSheet xssfSheet;

                //PointD point = new PointD();
                //int row = 0;
                //int col = 0;

                //int apertureIndex = 0;
                //int featureIndex = 0;
                //feature = new TDispFeature();

                //try
                //{
                //    xssfWorkbook = new ExcelFile_XSSF.XSSFWorkbook(filename);
                //    var sheet = xssfWorkbook.GetSheetAt(0);

                //    if (sheet != null) row = sheet.LastRowNum + 1;

                //    feature.Apertures.Add(new TApertures(apertureIndex.ToString(), "Excel 1", "", new PointD(0, 0)));

                //    for (int i = 1; i < row; i++)
                //    {
                //        double x = sheet.GetRow(i).GetCell(1).NumericCellValue;
                //        double y = sheet.GetRow(i).GetCell(2).NumericCellValue;

                //        point = new PointD(x, y);

                //        feature.Features.Add(new TFeatures(featureIndex, apertureIndex, EFeatureType.Pad, point));

                //        if (i % 400 is 0)
                //        {
                //            apertureIndex++;
                //            featureIndex = 0;
                //            feature.Apertures.Add(new TApertures(apertureIndex.ToString(), $"Excel {apertureIndex + 1}", "", new PointD(0, 0)));
                //        }
                //        featureIndex++;
                //    }

                //}
                //catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); };

                return true;
            }
        }

        public static bool Save(string fileName)
        {
            return GDoc.SaveXML(fileName, MethodBase.GetCurrentMethod().DeclaringType);
        }
        public static bool Load(string fileName)
        {
            return GDoc.LoadXML(fileName, MethodBase.GetCurrentMethod().DeclaringType);
        }
    }
}
