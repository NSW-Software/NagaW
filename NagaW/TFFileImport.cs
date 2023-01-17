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
        public enum EFeatureType { none, Pad, Dot, Line, Arc }
        public enum EDispType { None, Ref1, Ref2, Dot, Line, Arc }
        public enum EApertureType { None, Rectangle, Circle, Obround, Polygon }
        public enum EInterpolation { None, Linear, ClockWise, CounterClockWise, };

        public static bool OptimizeAll = false;

        public class TApertures
        {
            public string ID { get; set; } = "";
            public string Name { get; set; } = "";
            public string Para { get; set; } = "";
            public PointD Size { get; set; } = new PointD(0, 0);//Aperture Size
            public PointD FirstPt { get; set; } = new PointD(0, 0);
            public double SortNo = 0;
            public EApertureType Type { get; set; } = EApertureType.None;
            public TApertures(string id, string name, string para, PointD size, EApertureType type = EApertureType.None)
            {
                this.ID = id;
                this.Name = name;
                this.Para = para;
                this.Size = size;
                this.Type = type;
            }
            public TApertures()
            {
            }
            public override string ToString()
            {
                return ID + " - " + Name;
            }
        }
        public class TFeatures
        {
            public int ID { get; set; } = 0;
            public int ApertureIndex { get; set; } = 0;
            public EFeatureType Type { get; set; } = EFeatureType.none;
            public EDispType DispType { get; set; } = EDispType.None;
            public int DispTemplate { get; set; } = 0;//reserve
            public double SortNo = 0;//used for sorting
            public PointD Point { get; set; } = new PointD(0, 0);
            public PointD Point2 { get; set; } = new PointD(0, 0);
            public PointD PointRef { get; set; } = new PointD(0, 0);
            public double AperSortNo { get; set; } = 0;
            public double Radius { get; set; } = 0;
            public TFeatures(int id, int apIndex, EFeatureType type, PointD point)
            {
                ID = id;
                ApertureIndex = apIndex;
                Type = type;
                Point = point;
            }
            public TFeatures(int id, int apIndex, EFeatureType type, PointD point, double radius)
            {
                ID = id;
                ApertureIndex = apIndex;
                Type = type;
                Point = point;
                Radius = radius;
            }
            public TFeatures(int id, int apIndex, EFeatureType type, PointD point, PointD point2)
            {
                ID = id;
                ApertureIndex = apIndex;
                Type = type;
                Point = point;
                Point2 = point2;
            }
            public TFeatures(int id, int apIndex, EFeatureType type, PointD point, PointD point2, PointD pointref)
            {
                ID = id;
                ApertureIndex = apIndex;
                Type = type;
                Point = point;
                Point2 = point2;
                PointRef = pointref;
            }
            public TFeatures()
            {

            }
            public override string ToString()
            {
                string s = "";
                s += DispType.ToString() + " ";
                s += Point.ToStringForDisplay() + " ";
                if (Type == TFFileImport.EFeatureType.Line)
                {
                    Point2.ToStringForDisplay();
                }
                return s;
            }
        }
        public class TDispFeature
        {
            public EUnit Unit { get; set; } = EUnit.mm;
            public PointI XDigit { get; set; } = new PointI(2, 6);
            public PointI YDigit { get; set; } = new PointI(2, 6);

            public PointD Origin { get; set; } = new PointD(0, 0);
            public PointD Ref1 { get; set; } = new PointD(0, 0);
            public PointD Ref2 { get; set; } = new PointD(0, 0);
            public PointD CR { get; set; } = new PointD(0, 0);
            public PointD CR_Pitch { get; set; } = new PointD(0, 0);
            public TDispFeature()
            {

            }
            public BindingList<TApertures> Apertures { get; set; } = new BindingList<TApertures>();
            public BindingList<TFeatures> Features { get; set; } = new BindingList<TFeatures>();
            public void Clear()
            {
                Unit = EUnit.inch;
                XDigit = new PointI(2, 6);
                YDigit = new PointI(2, 6);

                Origin = new PointD(0, 0);
                Ref1 = new PointD(0, 0);
                Ref2 = new PointD(0, 0);

                Apertures.Clear();
                Features.Clear();
            }
            public void Sort(PointD startPoint)
            {
                double Dist(PointD pt1, PointD pt2)
                {
                    return Math.Sqrt(Math.Pow(pt2.X - pt1.X, 2) + Math.Pow(pt2.Y - pt1.Y, 2));
                }

                List<TFeatures> allList = new List<TFeatures>();
                List<TFeatures>[] featureList = new List<TFeatures>[Feature.Apertures.Count];

                for (int i = 0; i < Apertures.Count; i++)
                {
                    List<TFeatures> sortList = TFFileImport.Feature.Features.Where(x => x.ApertureIndex == i).ToList();// .OrderBy(x => x.SortNo).ToList();
                    List<TFeatures> sortList2 = new List<TFeatures>();
                    List<TFeatures> tsortList = new List<TFeatures>();

                    while (sortList.Count > 0)
                    {
                        sortList.ForEach(f => f.SortNo = /*f.Point.X*/ Dist(f.Point, startPoint));
                        tsortList = sortList.OrderBy(f => f.SortNo).ToList();
                        sortList2.Add(tsortList[0]);
                        startPoint = tsortList[0].Point;
                        tsortList.RemoveAt(0);
                        sortList = new List<TFeatures>(tsortList);
                    }
                    featureList[i] = sortList2;
                    allList = allList.Concat(sortList2).ToList();
                }
                TFFileImport.Feature.Features = new BindingList<TFeatures>(allList);
                TFFileImport.Feature.Features.ResetBindings();

                if (OptimizeAll)
                {
                    List<int> index = new List<int>();
                    List<TFeatures> sortApertureList = new List<TFeatures>();
                    List<TFeatures> taperturesList = new List<TFeatures>();
                    for (int i = 0; i < featureList.Length; i++)
                    {
                        featureList[i].ForEach(f => f.AperSortNo = Dist(featureList[i][0].Point, new PointD(0, 0)));

                        taperturesList = taperturesList.Concat(featureList[i]).ToList();
                    }
                    sortApertureList = taperturesList.OrderBy(f => f.AperSortNo).ToList();

                    int apertureIndex = 0;
                    if (sortApertureList[0].ApertureIndex is 0) index.Add(sortApertureList[0].ApertureIndex);
                    for (int i = 0; i < sortApertureList.Count; i++)
                    {
                        if (apertureIndex != sortApertureList[i].ApertureIndex)
                        {
                            apertureIndex = sortApertureList[i].ApertureIndex;
                            index.Add(apertureIndex);
                        }
                    }

                    double lastNo = sortApertureList[0].AperSortNo;
                    int num = 0;

                    for (int i = 0; i < sortApertureList.Count; i++)
                    {
                        sortApertureList[i].ApertureIndex = num;
                        if (sortApertureList[i].AperSortNo != lastNo)
                        {
                            num++;
                            sortApertureList[i].ApertureIndex = num;
                            lastNo = sortApertureList[i].AperSortNo;
                        }
                    }

                    List<TApertures> list = new List<TApertures>(Apertures);
                    for (int i = 0; i < Apertures.Count; i++)
                    {
                        Apertures[i] = list[index[i]];
                    }

                    //Apertures.ResetBindings();
                    Feature.Features = new BindingList<TFeatures>(sortApertureList);
                    Feature.Features.ResetBindings();
                }
            }
        }

        public class Gerber_RS274X
        {
            public static bool Decode(string fileName, ref TDispFeature feature)
            {
                feature = new TDispFeature();
                string lastX = "";
                string lastY = "";

                #region Key Commands RS-274X
                /*List essential commands only
                %MOIN*%/%MOMM*% (mandatory) - Dimension expression inch/mm
                %FSLAX26Y26*% (mandatory) - Format Specification FSLAX<Format>Y<Format>;LA=Leading  Absolute <Format>=<decimal digit><decimal digit>
                %AMVB_RECTANGLE*xxxx% - Aperture Macro AM<Macro_Name>*<Macro_Content>
                %ADD14C,0.00394*% - Aperture Definition AD<D-code_number><Template>[,<Modifiers>]
                %ADD18TARGET125*% - Aperture Definition AD<Macro>
                G54D10* (deprecated) - Select Aperture G53<D-code_number>
                D10*- Select Aperture <D-code_number>
                X118110Y118110D03* - Flash at X,Y; possible only 1 element; integral max 6, fraction 5~6 
                X118110Y118110D02* - Move to  X,Y; possible only 1 element
                X836613D01* - Interpolation command X,Y; possible only 1 element
                M02* - End of file
                 */
                #endregion
                //Current supports D02, D03, Do not supported D01
                #region Parse Algo
                /*
                 * 1. Split linefeeds
                 * 2. Parse for %MO -> define Dimension 
                 * 3. Parse for %FSLX -> define Format Specification 
                 * 4. Parse for %AD -> define all apertures
                 * 5. Parse for %MO2 -> end of file
                 * 6. Parse for G54 -> assign aperture(optional assign aperture)
                 * 7. Parse for >D10 -> assign aperture
                 * 8. (with 6) Parse for X,Y -> for flash(D03) update X,Y
                 */
                #endregion
                try
                {
                    using (StreamReader reader = new StreamReader(fileName))
                    {
                        string Line = reader.ReadToEnd();
                        string[] lines = Line.Split(new[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries);

                        int apertureIndex = -1;
                        bool g36 = false;
                        //PointD absPos = new PointD(0, 0);
                        foreach (string _line in lines)
                        {
                            string line = _line;
                            Char[] replacements = new Char[] { '%', '*' };
                            foreach (Char c in replacements)
                            {
                                if (line.Contains(c))
                                    line = line.Replace(c.ToString(), String.Empty);
                            }

                            #region Unit
                            if (line.StartsWith("MO"))
                            {
                                if (line.Contains("MM"))
                                    feature.Unit = EUnit.mm;
                                else if (line.Contains("IN")) feature.Unit = EUnit.inch;
                                continue;
                            }
                            #endregion

                            #region Digit
                            if (line.StartsWith("FS"))
                            {
                                string s = line;
                                string[] ss = s.Split(new string[] { "X", "Y" }, StringSplitOptions.RemoveEmptyEntries);
                                //FSLAX26Y26
                                //FSLA,26,26
                                int xi = 0;
                                int xf = 0;
                                int.TryParse(ss[1][0].ToString(), out xi);
                                int.TryParse(ss[1][1].ToString(), out xf);
                                feature.XDigit = new PointI(xi, xf);
                                int yi = 0;
                                int yf = 0;
                                int.TryParse(ss[2][0].ToString(), out yi);
                                int.TryParse(ss[2][1].ToString(), out yf);
                                feature.YDigit = new PointI(yi, yf);
                                continue;
                            }
                            #endregion

                            #region Column Row
                            if (line.StartsWith("SR"))
                            {
                                string s = line;
                                string[] ss = s.Split(new string[] { "X", "Y", "I", "J" }, StringSplitOptions.RemoveEmptyEntries);
                                //SRX1Y1I0J0
                                //SR,1,1,0,0
                                int c = 0;
                                int r = 0;
                                int.TryParse(ss[1].ToString(), out c);
                                int.TryParse(ss[2].ToString(), out r);
                                feature.CR = new PointD(c, r);
                                double c_pitch = 0;
                                double r_pitch = 0;
                                double.TryParse(ss[3].ToString(), out c_pitch);
                                double.TryParse(ss[4].ToString(), out r_pitch);
                                feature.CR_Pitch = feature.Unit == EUnit.inch ? new PointD(c_pitch * 25.4, r_pitch * 25.4) : new PointD(c_pitch, r_pitch);
                                continue;
                            }
                            #endregion

                            #region Aperture Index
                            if (line.StartsWith("AD"))
                            {
                                string s = line;
                                //ADD14C,0.00394x0.0123
                                //ADD14C,0.00394
                                //ADD18TARGET125
                                s = s.Remove(0, 2);
                                //D14C,0.00394x0.0123
                                //D14C,0.00394
                                //D18TARGET125
                                string id = s.Remove(3);
                                //string name = s.Remove(0, 3);
                                //string para = "";
                                //if (name.Contains(","))
                                //{
                                //    string[] split = s.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                //    para = split[1];
                                //}
                                //feature.Apertures.Add(new TApertures(id, name, para));
                                string[] split = s.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                string name = split[0];
                                string para = split.Length > 1 ? split[1] : "";
                                PointD size = new PointD(0, 0);
                                if (para.Length > 0)
                                {
                                    double sx = 0;
                                    double sy = 0;
                                    string[] sSize = para.Split(new string[] { "X" }, StringSplitOptions.RemoveEmptyEntries);
                                    if (name.EndsWith("R"))
                                    {
                                        double.TryParse(sSize[0], out sx);
                                        double.TryParse(sSize[1], out sy);
                                    }
                                    if (name.EndsWith("C"))
                                    {
                                        double.TryParse(sSize[0], out sx);
                                        sy = sx;
                                    }

                                    if (feature.Unit == EUnit.inch)
                                    {
                                        sx *= 25.4;
                                        sy *= 25.4;
                                    }

                                    size = new PointD(sx, sy);
                                }
                                feature.Apertures.Add(new TApertures(id, name, para, size));
                                continue;
                            }
                            #endregion

                            // End Of File
                            if (line.StartsWith("MO2")) break;

                            #region Aperture
                            if (line.StartsWith("G54"))
                            {
                                //G54D10
                                string s = line;
                                if (s.StartsWith("G54")) s = s.Remove(0, 3);
                                var ls = feature.Apertures.Select(x => x.ID);
                                apertureIndex = ls.ToList().IndexOf(s);
                                continue;
                            }
                            if (line.StartsWith("D"))//Dn where n > 0, Select Aperture
                            {
                                //D10, D11
                                string s = line;
                                s = s.Remove(0, 1);
                                int d = 0; int.TryParse(s, out d);
                                var ls = feature.Apertures.Select(x => x.ID);
                                if (d >= 10) apertureIndex = ls.ToList().IndexOf(line);
                                continue;
                            }
                            if (line.StartsWith("X") | line.StartsWith("Y"))
                            {
                                //X859633Y356193D03
                                string sx = "";
                                string sy = "";
                                string D = "";
                                string[] split = line.Split(new string[] { "X", "Y", "D" }, StringSplitOptions.RemoveEmptyEntries);
                                if (line.StartsWith("X"))
                                {
                                    sx = split[0];
                                    lastX = sx;
                                    if (split.Length == 2) D = split[1];
                                    if (split.Length == 3)
                                    {
                                        sy = split[1];
                                        D = split[2];
                                        lastY = sy;
                                    }
                                    sy = lastY;
                                }
                                if (line.StartsWith("Y"))
                                {
                                    sy = split[0];
                                    if (split.Length == 2) D = split[1];
                                    sx = lastX;
                                }
                                //if (D == "01") continue;
                                //x=633,y=356193
                                //x=-633,y=356193
                                //x=123
                                //y=-34533

                                //sx = sx.PadLeft(feature.XDigit.X + feature.XDigit.Y, '0').Insert(2, ".");
                                string sxa = sx.Replace("-", "");
                                sxa = sxa.PadLeft(feature.XDigit.X + feature.XDigit.Y, '0').Insert(feature.XDigit.X, ".");
                                if (sx.StartsWith("-")) sxa = "-" + sxa;
                                double x = 0; double.TryParse(sxa, out x);

                                //sy = sy.PadLeft(feature.YDigit.X + feature.YDigit.Y, '0').Insert(2, ".");
                                string sya = sy.Replace("-", "");
                                sya = sya.PadLeft(feature.YDigit.X + feature.YDigit.Y, '0').Insert(feature.YDigit.X, ".");
                                if (sy.StartsWith("-")) sya = "-" + sya;
                                double y = 0; double.TryParse(sya, out y);

                                if (feature.Unit == EUnit.inch)
                                {
                                    x *= 25.4;
                                    y *= 25.4;
                                }

                                if (feature.Features.Count > 0)
                                {
                                    if (x == 0) x = feature.Features[feature.Features.Count - 1].Point.X;
                                    if (y == 0) y = feature.Features[feature.Features.Count - 1].Point.Y;
                                }

                                if (apertureIndex != -1)
                                {
                                    //for (int i = 0; i < feature.CR.X; i++)
                                    //{
                                    //    for (int j = 0; j < feature.CR.Y; j++)
                                    //    {
                                    //        var x_after = x + (feature.CR_Pitch.X * i);
                                    //        var y_after = y + (feature.CR_Pitch.Y * j);
                                    //        feature.Features.Add(new TFeatures(feature.Features.Count, apertureIndex, EFeatureType.Pad, new PointD(x_after, y_after)));
                                    //    }
                                    //}
                                    feature.Features.Add(new TFeatures(feature.Features.Count, apertureIndex, EFeatureType.Pad, new PointD(x, y)));
                                }
                            }
                            #endregion

                            #region Interpolar
                            if (line.StartsWith("G36")) g36 = true;
                            if (g36 && line.StartsWith("X"))
                            {
                                string sx = "";
                                string sy = "";
                                string[] split = line.Split(new string[] { "X", "Y", "D" }, StringSplitOptions.RemoveEmptyEntries);

                                sx = split[0];
                                sy = split[1];

                                string sxa = sx.Replace("-", "");
                                sxa = sxa.PadLeft(feature.XDigit.X + feature.XDigit.Y, '0').Insert(feature.XDigit.X, ".");
                                if (sx.StartsWith("-")) sxa = "-" + sxa;
                                double x = 0; double.TryParse(sxa, out x);

                                string sya = sy.Replace("-", "");
                                sya = sya.PadLeft(feature.YDigit.X + feature.YDigit.Y, '0').Insert(feature.YDigit.X, ".");
                                if (sy.StartsWith("-")) sya = "-" + sya;
                                double y = 0; double.TryParse(sya, out y);

                                if (feature.Unit == EUnit.inch)
                                {
                                    x *= 25.4;
                                    y *= 25.4;
                                }

                                if (feature.Features.Count > 0)
                                {
                                    if (x == 0) x = feature.Features[feature.Features.Count - 1].Point.X;
                                    if (y == 0) y = feature.Features[feature.Features.Count - 1].Point.Y;
                                }

                                feature.Features.Add(new TFeatures(feature.Features.Count, 0, EFeatureType.Pad, new PointD(x, y)));

                                g36 = false;
                            }
                            if (line.StartsWith("G1") | line.StartsWith("G2"))
                            {
                                string si = "";
                                string sj = "";
                                string sx = "";
                                string sy = "";
                                string D = "";
                                bool ij = false;
                                if (line.Contains("I")) ij = true;
                                string[] split = line.Split(new string[] { "X", "Y", "D", "I", "J" }, StringSplitOptions.RemoveEmptyEntries);

                                sx = split[1];
                                sy = split[2];
                                if (ij) si = split[3];
                                if (ij) sj = split[4];

                                string sxa = sx.Replace("-", "");
                                sxa = sxa.PadLeft(feature.XDigit.X + feature.XDigit.Y, '0').Insert(feature.XDigit.X, ".");
                                if (sx.StartsWith("-")) sxa = "-" + sxa;
                                double x = 0; double.TryParse(sxa, out x);

                                //sy = sy.PadLeft(feature.YDigit.X + feature.YDigit.Y, '0').Insert(2, ".");
                                string sya = sy.Replace("-", "");
                                sya = sya.PadLeft(feature.YDigit.X + feature.YDigit.Y, '0').Insert(feature.YDigit.X, ".");
                                if (sy.StartsWith("-")) sya = "-" + sya;
                                double y = 0; double.TryParse(sya, out y);

                                string sia = si.Replace("-", "");
                                sia = sia.PadLeft(feature.YDigit.X + feature.XDigit.Y, '0').Insert(feature.XDigit.X, ".");
                                if (si.StartsWith("-")) sia = "-" + sia;
                                double i = 0; double.TryParse(sia, out i);

                                string sja = sj.Replace("-", "");
                                sja = sja.PadLeft(feature.YDigit.X + feature.YDigit.Y, '0').Insert(feature.YDigit.X, ".");
                                if (sj.StartsWith("-")) sja = "-" + sja;
                                double j = 0; double.TryParse(sja, out j);

                                if (feature.Unit == EUnit.inch)
                                {
                                    x *= 25.4;
                                    y *= 25.4;
                                    i *= 25.4;
                                    j *= 25.4;
                                }

                                if (feature.Features.Count > 0)
                                {
                                    if (x == 0) x = feature.Features[feature.Features.Count - 1].Point.X;
                                    if (y == 0) y = feature.Features[feature.Features.Count - 1].Point.Y;
                                }

                                //if (apertureIndex != -1)
                                //{
                                    feature.Features.Add(new TFeatures(feature.Features.Count, 0, EFeatureType.Pad, new PointD(x, y)));
                                //}

                                //if (Math.Abs(i) > 0 || Math.Abs(j) > 0)
                                //{
                                //    var new_x = x + i;
                                //    var new_y = y + j;
                                //    feature.Features.Add(new TFeatures(feature.Features.Count, 0, EFeatureType.Pad, new PointD(new_x, new_y)));
                                //}
                                g36 = false;
                            }
                            #endregion
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); };
                return true;
            }

            public static bool Decode1(string filename, ref TDispFeature feature)
            {
                feature = new TDispFeature();
                int apertureIdx = 0;
                string latest_aperture = "";
                PointD latest_point = new PointD(0, 0);
                int tempApID = 0;

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

                            #region Coordinate Format, Digits
                            if (line.StartsWith("FS"))
                            {
                                var temp = line.Split(new char[] { 'X', 'Y' }, StringSplitOptions.RemoveEmptyEntries);
                                int.TryParse(temp[1][0].ToString(), out int x1);
                                int.TryParse(temp[1][1].ToString(), out int x2);
                                feature.XDigit = new PointI(x1, x2);
                                int.TryParse(temp[2][0].ToString(), out int y1);
                                int.TryParse(temp[2][1].ToString(), out int y2);
                                feature.YDigit = new PointI(y1, y2);
                                continue;
                            }
                            #endregion

                            #region Unit
                            if (line.StartsWith("MO"))
                            {
                                if (line.Contains("MM")) feature.Unit = EUnit.mm;
                                else if (line.Contains("IN")) feature.Unit = EUnit.inch;

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
                                            var finalXY = Convert(feature, new double[] { result, result });
                                            PointD size = feature.Unit == EUnit.inch ? new PointD(finalXY[0] * 25.4, finalXY[1] * 25.4) : new PointD(finalXY[0], finalXY[1]);

                                            feature.Apertures.Add(new TApertures(apertureIdx.ToString(), temp[0], temp[1], size, EApertureType.Circle));
                                            apertureIdx++;
                                            break;
                                        }
                                    case 'O':
                                    case 'R':
                                        {
                                            var tempSize = temp[1].Split(new char[] { 'X' }, StringSplitOptions.RemoveEmptyEntries);
                                            double.TryParse(tempSize[0], out double xResult);
                                            double.TryParse(tempSize[1], out double yResult);
                                            var finalXY = Convert(feature, new double[] { xResult, yResult });
                                            PointD size = feature.Unit == EUnit.inch ? new PointD(finalXY[0] * 25.4, finalXY[1] * 25.4) : new PointD(finalXY[0], finalXY[1]);

                                            feature.Apertures.Add(new TApertures(apertureIdx.ToString(), temp[0], temp[1], size, EApertureType.Rectangle));
                                            apertureIdx++;
                                            break;
                                        }
                                    default:
                                        {
                                            feature.Apertures.Add(new TApertures(apertureIdx.ToString(), temp[0], "", new PointD(0, 0)));
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
                                //latest_point = new PointD(0, 0);
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

                                var id = feature.Apertures.Where(a => a.Name.Contains(latest_aperture)).ToList();
                                if (id.Count > 0) int.TryParse(id[0].ID, out tempApID);

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

                                var finalXY = Convert(feature, new double[] { x, y });

                                PointD tempPt = feature.Unit == EUnit.inch ? new PointD(finalXY[0] * 25.4, finalXY[1] * 25.4) : new PointD(finalXY[0], finalXY[1]);

                                PointD lineEndPt = new PointD(tempPt);
                                if (feature.Features.Count - 1 >= 0)
                                {
                                    lineEndPt = /*feature.Features[feature.Features.Count - 1].Type is EFeatureType.Line ?*/ new PointD(feature.Features[feature.Features.Count - 1].Point);//: new PointD(tempPt);
                                }

                                //if (regionFeature)
                                //{
                                switch (interMode)
                                {
                                    case EInterpolation.Linear:
                                        if (d is 2) lineEndPt = new PointD(tempPt);
                                        feature.Features.Add(new TFeatures(feature.Features.Count, tempApID, EFeatureType.Line, new PointD(tempPt), new PointD(lineEndPt)));
                                        break;//continue;
                                    case EInterpolation.CounterClockWise:
                                    case EInterpolation.ClockWise:
                                        double i, j = 0;
                                        if (temp.Length > 3) double.TryParse(temp[3], out j);
                                        double.TryParse(temp[2], out i);
                                        var tempRad = Convert(feature, new double[] { i, j });
                                        var radius = tempRad[0] == 0 ? Math.Abs(tempRad[1]) : Math.Abs(tempRad[0]);
                                        feature.Features.Add(new TFeatures(feature.Features.Count, tempApID, EFeatureType.Arc, new PointD(tempPt), radius));
                                        break;//continue;
                                    default:
                                        feature.Features.Add(new TFeatures(feature.Features.Count, tempApID, EFeatureType.Dot, new PointD(tempPt)));
                                        break;
                                }
                                //}

                                //feature.Features.Add(new TFeatures(feature.Features.Count, tempApID, EFeatureType.Dot, new PointD(tempPt)));
                                continue;
                            }
                            #endregion
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

                return true;
            }

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

            public static double[] Convert(TDispFeature feature, double[] xy)
            {
                string xa = xy[0].ToString().Replace("-", "");
                string ya = xy[1].ToString().Replace("-", "");

                xa = xa.PadLeft(feature.XDigit.X + feature.XDigit.Y, '0').Insert(feature.XDigit.X, ".");
                ya = ya.PadLeft(feature.YDigit.X + feature.YDigit.Y, '0').Insert(feature.YDigit.X, ".");

                if (xy[0].ToString().StartsWith("-")) xa = "-" + xa; if (xy[1].ToString().StartsWith("-")) ya = "-" + ya;
                double.TryParse(xa, out double xResult); double.TryParse(ya, out double yResult);
                return new double[] { xResult, yResult };
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
            public static bool Decode(string fileName, ref TDispFeature feature)
            {
                feature = new TDispFeature();

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

                //Default values
                feature.Unit = EUnit.inch;

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
                                    feature.Unit = EUnit.mm;
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
                                feature.Apertures.Add(new TApertures(id, name, para, new PointD(0, 0)));
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

                                if (feature.Unit == EUnit.inch)
                                {
                                    x *= 25.4;
                                    y *= 25.4;
                                }

                                feature.Features.Add(new TFeatures(feature.Features.Count, s, EFeatureType.Pad, new PointD(x, y)));
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

                                if (feature.Unit == EUnit.inch) { x *= 25.4; y *= 25.4; x2 *= 25.4; y2 *= 25.4; }

                                feature.Features.Add(new TFeatures(feature.Features.Count, s, EFeatureType.Line, new PointD(x, y), new PointD(x2, y2)));
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
            public static bool Decode(string filename, ref TDispFeature feature)
            {
                double unit = 1;
                int aUnit = 0;

                int apertureIndex = -1;
                int featureIndex = -1;
                bool entitysection = false;
                feature = new TDispFeature();
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
                                            feature.Apertures.Add(new TApertures(apertureIndex.ToString(), "ENTITIES", "", new PointD(0, 0)));
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
                                            feature.Features.Add(new TFeatures(featureIndex, apertureIndex, EFeatureType.Line, new PointD(x, y), new PointD(x2, y2)));
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
                                            PointD point = new PointD(0, 0);
                                            for (int i = 0; i < x.Count; i++)
                                            {
                                                feature.Features.Add(new TFeatures(featureIndex, apertureIndex, EFeatureType.Line, new PointD(x[i], y[i]), point = i + 1 < vertices ? new PointD(x[i + 1], y[i + 1]) : new PointD(x[0], y[0])));
                                            }
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
                                            feature.Features.Add(new TFeatures(featureIndex, apertureIndex, EFeatureType.Arc, new PointD(startPt_X, startPt_Y), new PointD(endPt_X, endPt_Y), new PointD(refPt_X, refPt_Y)));
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
                                            feature.Features.Add(new TFeatures(featureIndex, apertureIndex, EFeatureType.Pad, new PointD(x, y)));
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
            public static bool Decode(string filename, ref TDispFeature feature)
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

        public static TDispFeature Feature = new TDispFeature();
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
