using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace NagaW
{
    using Emgu.CV;
    using Emgu.CV.Structure;


    //Clas that ready to be defined as DLL
    public static class MathPro
    {
        public static double Variance(List<double> res)
        {
            if (res.Count is 0) return 0;

            var ave = res.Average();
            var variance = res.Select(x => Math.Pow(x - ave, 2)).Sum() / res.Count;
            return variance;
        }
        public static double Standard_Deviation(List<double> res)
        {
            if (res.Count is 0) return 0;

            var sd = Variance(res);
            var v = Math.Sqrt(sd);

            return v;
        }
        public static double Medium(List<double> res)
        {
            if (res.Count is 1) return res[0];
            //even
            if (res.Count % 2 is 0)
            {
                var idx1 = (res.Count / 2);
                var idx2 = (res.Count / 2) + 1;

                var s = res[idx1 - 1] + res[idx2 - 1];

                return s / 2;
            }
            //odd
            else
            {
                var idx = (res.Count + 1) / 2;
                return res[idx - 1];
            }
        }

        public static double CpU(List<double> res, double upperLimit)
        {
            var mean = res.Average();
            var sd = Standard_Deviation(res);
            var CpU = (upperLimit - mean) / (3 * sd);

            return CpU;
        }
        public static double CpL(List<double> res, double lowerLimit)
        {
            var mean = res.Average();
            var sd = Standard_Deviation(res);
            var CpL = (mean - lowerLimit) / (3 * sd);

            return CpL;
        }
        public static double CpK(List<double> res, double upperLimit, double lowerLimit)
        {
            return Math.Min(CpU(res, upperLimit), CpL(res, lowerLimit));
        }


        public static PointD Translate(PointD point, TAlignData alignData)
        {
            //A = angle_radian
            //
            //[x']   [x][cosA -sinA]
            //[y']   [y][sinA  cosA]
            //
            //x' = x*cosA - y*sinA
            //y' = x*sinA + y*cosA
            //
            //(1)Rotate about a center xc, yc
            //
            //x' - xc = (x - xc)*cosA - (y - yc)*sinA
            //y' - yc = (x - xc)*sinA + (y - yc)*cosA
            //
            //reorder
            //
            //x' = xc + (x - xc)*cosA - (y - yc)*sinA
            //y' = yc + (x - xc)*sinA + (y - yc)*cosA
            //
            //(2)Tranlate offset xo, yo
            //x' += xo
            //y' += yo

            PointD ptOri = point;
            double rad = alignData.Angle_Rad;

            PointD ptRotate = new PointD(ptOri);
            ptRotate.X = alignData.Datum.X + ((ptOri.X - alignData.Datum.X) * Math.Cos(rad)) - ((ptOri.Y - alignData.Datum.Y) * Math.Sin(rad));
            ptRotate.Y = alignData.Datum.Y + ((ptOri.X - alignData.Datum.X) * Math.Sin(rad)) + ((ptOri.Y - alignData.Datum.Y) * Math.Cos(rad));
            PointD ptTranslate = new PointD(ptRotate) + alignData.Offset;

            return ptTranslate;
        }
        public static PointD Translate(PointD point, params TAlignData[] alignDatas)
        {
            //board > cluster > unit
            PointD pt = new PointD(point);
            foreach (var aD in alignDatas) pt = Translate(pt, aD);
            return pt;
        }

        public static PointD InverseTranslate(PointD point, TAlignData alignData)
        {
            //(1)Tranlate offset xo, yo
            //x' -= xo
            //y' -= yo
            //
            //(2)
            //A = angle_radian
            //
            //[x']   [x][cosA -sinA]
            //[y']   [y][sinA  cosA]
            //Inverse Matrix
            //[x]   [x'] __________1___________ [cosA   sinA]
            //[y]   [y'] cosAcosA - (-sinA)sinA [-sinA  cosA]
            //
            //[x]   [x'] ______1______ [cosA    sinA]
            //[y]   [y'] cos²A + sin²A [-sinA   cosA],  cos²A + sin²A = 1
            //
            //[x]   x'cosA + y'sinA
            //[y]   -x'sinA + y'cosA
            //
            //x = xc + (x' - xc)*cosA + (y' - yc)*sinA
            //y = yc - (x' - xc)*sinA + (y' - yc)*cosA

            PointD ptTranslate = new PointD(point);
            ptTranslate -= alignData.Offset;

            double rad = alignData.Angle_Rad;
            PointD ptRotate = new PointD(ptTranslate);
            ptRotate.X = alignData.Datum.X + ((ptTranslate.X - alignData.Datum.X) * Math.Cos(rad)) + ((ptTranslate.Y - alignData.Datum.Y) * Math.Sin(rad));
            ptRotate.Y = alignData.Datum.Y - ((ptTranslate.X - alignData.Datum.X) * Math.Sin(rad)) + ((ptTranslate.Y - alignData.Datum.Y) * Math.Cos(rad));

            return ptRotate;
        }
        public static PointD InverseTranslate(PointD point, params TAlignData[] alignDatas)
        {
            //unit > cluster > board
            PointD pt = new PointD(point);
            foreach (var aD in alignDatas.Reverse()) pt = InverseTranslate(pt, aD);
            return pt;
        }


        public static double TwoPointsLength(PointD xy1, PointD xy2)
        {
            var x = Math.Pow(xy2.X - xy1.X, 2);
            var y = Math.Pow(xy2.Y - xy1.Y, 2);

            var length = Math.Sqrt(x + y);
            return Math.Abs(length);
        }
        public static PointD TwoLineIntersectPoint(PointD l1Startpos, PointD l1EndPos, PointD l2StartPos, PointD l2EndPos)
        {
            double r = 0.00000001;
            double m1 = ((l1EndPos.Y - l1Startpos.Y) is 0 ? r : (l1EndPos.Y - l1Startpos.Y)) / ((l1EndPos.X - l1Startpos.X) is 0 ? r : (l1EndPos.X - l1Startpos.X));
            double m2 = ((l2EndPos.Y - l2StartPos.Y) is 0 ? r : (l2EndPos.Y - l2StartPos.Y)) / ((l2EndPos.X - l2StartPos.X) is 0 ? r : (l2EndPos.X - l2StartPos.X));

            double _m1 = -1 / m1;
            double _m2 = -1 / m2;

            PointD midPt1 = new PointD((l1Startpos + l1EndPos).X / 2, (l1Startpos + l1EndPos).Y / 2);
            PointD midPt2 = new PointD((l2StartPos + l2EndPos).X / 2, (l2StartPos + l2EndPos).Y / 2);

            double c1 = midPt1.Y - (_m1 * midPt1.X);
            double c2 = midPt2.Y - (_m2 * midPt2.X);

            var x = (c2 - c1) / (_m1 - _m2);
            var y = (_m1 * x) + c1;
            return new PointD(x, y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <param name="splitRatio"> 0-1, lower value, closer to pt1. Eg: 0.5</param>
        /// <returns></returns>
        public static PointD HalfWayPoint(PointD pt1, PointD pt2, double splitRatio)
        {
            splitRatio = Math.Max(0, splitRatio);
            splitRatio = Math.Min(0.75, splitRatio);
            PointD intersectPt = new PointD((pt2.X * splitRatio) + (pt1.X * (1 - splitRatio)), (pt2.Y * splitRatio) + (pt1.Y * (1 - splitRatio)));
            return intersectPt;
        }

        public static PointD Center(PointD startPt, PointD passPt, PointD endPt)
        {
            #region formula
            //mr = (y2 - y1)/(x2 - x1)
            //mt = (y3 - y2)/(x3 - x2)

            //x = (mr*mt*(y3-y1) + mr(x2+x3) - mt(x1+x2))/(2(mr - mt))
            //y = -(1 / mr) * (x - (x1 + x2) / 2) + ((y1 + y2) / 2);
            #endregion

            #region calc slope, center and rad
            double mr, mt;
            double xc, yc;

            if ((startPt.X == passPt.X && passPt.X == endPt.X) || (startPt.Y == passPt.Y && passPt.Y == endPt.Y))
            {
                throw new Exception("Points in 1 line. Unable to generate circle or arc.");
            }

            if (startPt.X == passPt.X)
            {
                mr = (endPt.Y - startPt.Y) / (endPt.X - startPt.X);
                mt = (passPt.Y - endPt.Y) / (passPt.X - endPt.X);

                xc = (mr * mt * (endPt.Y - startPt.Y) + mr * (endPt.X + passPt.X) - mt * (startPt.X + endPt.X)) / (2 * (mr - mt));
                yc = -(1 / mr) * (xc - (startPt.X + endPt.X) / 2) + ((startPt.Y + endPt.Y) / 2);
                //rad = (double)Math.Sqrt(Math.Pow(x3 - xc, 2) + Math.Pow(y3 - yc, 2));
            }
            else
                if (startPt.X == endPt.X)
            {
                mr = (passPt.Y - startPt.Y) / (passPt.X - startPt.X);
                mt = (endPt.Y - passPt.Y) / (endPt.X - passPt.X);

                xc = (mr * mt * (endPt.Y - startPt.Y) + mr * (passPt.X + endPt.X) - mt * (startPt.X + passPt.X)) / (2 * (mr - mt));
                yc = -(1 / mr) * (xc - (startPt.X + passPt.X) / 2) + ((startPt.Y + passPt.Y) / 2);
                //rad = (double)Math.Sqrt(Math.Pow(x2 - xc, 2) + Math.Pow(y2 - yc, 2));
            }
            else//if (passPt.X == endPt.X)
            {
                mr = (startPt.Y - passPt.Y) / (startPt.X - passPt.X);
                mt = (endPt.Y - startPt.Y) / (endPt.X - startPt.X);

                xc = (mr * mt * (endPt.Y - passPt.Y) + mr * (startPt.X + endPt.X) - mt * (passPt.X + startPt.X)) / (2 * (mr - mt));
                yc = -(1 / mr) * (xc - (passPt.X + startPt.X) / 2) + ((passPt.Y + startPt.Y) / 2);
                //rad = (double)Math.Sqrt(Math.Pow(x1 - xc, 2) + Math.Pow(y1 - yc, 2));
            }
            #endregion

            return new PointD(xc, yc);
        }
        public static double SweepAngle(PointD centerPt, PointD startPt, PointD passPt, PointD endPt)
        {
            //Return sweep angle in radian, cw - positive sweep angle
            //Atan2 return value is in Radian (-pi ~ pi), angle from the positive X-Axis, ccw positive
            double angleStart = (double)Math.Atan2((startPt.Y - centerPt.Y), (startPt.X - centerPt.X));
            double anglePass = (double)Math.Atan2((passPt.Y - centerPt.Y), (passPt.X - centerPt.X));
            double angleEnd = (double)Math.Atan2((endPt.Y - centerPt.Y), (endPt.X - centerPt.X));

            //Convert to positive angles 
            //if (angleStart < 0) angleStart = Math.PI * 2 - angleStart;
            //if (anglePass < 0) anglePass = Math.PI * 2 - anglePass;
            //if (angleEnd < 0) angleEnd = Math.PI * 2 - angleEnd;

            if (anglePass > angleStart)//ccw
                return -(angleEnd - angleStart);
            else//cw
                return angleStart + (Math.PI * 2 - angleEnd);
        }


        public static bool Height3DMatrix(PointD pt, List<PointXYZ> points, out double val)
        {
            val = 0;
            // _h = mean of h
            // a0 = _a0, a1 = _a1, b = _h - _a0*_x - _a1*_y
            // h = a0*x + a1*y + b
            // h - _h = _a1*(x - _x) + _a1*(y - _y) + b

            double barX = 0; double barY = 0; double barH = 0; double barA0 = 0; double barA1 = 0;

            if (!Height3DMatrixPara(points, ref barX, ref barY, ref barH, ref barA0, ref barA1)) return false;

            val = barH + barA0 * (pt.X - barX) + barA1 * (pt.Y - barY);

            return true;
        }
        private static bool Height3DMatrixPara(List<PointXYZ> points, ref double barX, ref double barY, ref double barH, ref double barA0, ref double barA1)
        {
            var length = points.Count;
            PointXYZ mean = new PointXYZ(0, 0, 0);

            //Mean of points
            for (int i = 0; i < length; i++) mean = new PointXYZ(mean.X + points[i].X, mean.Y + points[i].Y, mean.Z + points[i].Z);

            mean = new PointXYZ(mean.X / length, mean.Y / length, mean.Z / length);

            //Linear system matrix and vector elements
            double xxSum = 0; double xySum = 0; double xhSum = 0; double yySum = 0; double yhSum = 0;
            for (int i = 0; i < length; i++)
            {
                PointXYZ diff = new PointXYZ(points[i].X - mean.X, points[i].Y - mean.Y, points[i].Z - mean.Z);
                xxSum += diff.X * diff.X;
                xySum += diff.X * diff.Y;
                xhSum += diff.X * diff.Z;
                yySum += diff.Y * diff.Y;
                yhSum += diff.Y * diff.Z;
            }

            double det = xxSum * yySum - xySum * xySum;
            if (det != 0)
            {
                barX = mean.X;
                barY = mean.Y;
                barH = mean.Z;
                barA0 = (yySum * xhSum - xySum * yhSum) / det;
                barA1 = (xxSum * yhSum - xySum * xhSum) / det;
                return true;
            }
            else
            {
                barX = 0;
                barY = 0;
                barH = 0;
                barA0 = 0;
                barA1 = 0;
                return false;
            }
        }

        public static bool PatMatch(Image<Gray, byte> img, Image<Gray, byte> regImg, int threshold, Rectangle searchRect, Rectangle patternRect, out double score, out Point offset, out double processTime)
        {
            Stopwatch sw = Stopwatch.StartNew();

            Image<Gray, byte> image;
            Image<Gray, byte> imgTemplate = null;
            Image<Gray, float> imgResult = null;

            offset = new Point();

            processTime = 0;
            score = 0;
            try
            {
                if (threshold >= 0)
                {
                    image = img.ThresholdBinary(new Gray(threshold), new Gray(255));
                    imgTemplate = regImg.ThresholdBinary(new Gray(threshold), new Gray(255));
                }
                else
                {
                    image = img.Clone();
                    imgTemplate = regImg.Clone();
                }

                //  Define search rect to include pattern size for part edge detection
                searchRect.X = Math.Max(0, searchRect.X - patternRect.Width);
                searchRect.Y = Math.Max(0, searchRect.Y - patternRect.Height);
                searchRect.Width = Math.Min(regImg.Width - searchRect.X, searchRect.Width + patternRect.Width * 2);
                searchRect.Height = Math.Min(regImg.Height - searchRect.Y, searchRect.Height + patternRect.Height * 2);

                imgResult = image.Copy(searchRect).MatchTemplate(imgTemplate.Copy(patternRect), Emgu.CV.CvEnum.TemplateMatchingType.SqdiffNormed);

                double[] minCorr;
                double[] maxCorr;
                Point[] minPt;
                Point[] maxPt;
                imgResult.MinMax(out minCorr, out maxCorr, out minPt, out maxPt);

                var patLoc = new Point();

                patLoc.X = searchRect.X + /*(float)*/minPt[0].X;
                patLoc.Y = searchRect.Y + /*(float)*/minPt[0].Y;
                offset.X = patLoc.X - patternRect.X;
                offset.Y = patLoc.Y - patternRect.Y;
                score = (double)(1 - minCorr[0]);

                //  Set score to 0 if out of search reigion
                if (patLoc.X < searchRect.X || patLoc.Y < searchRect.Y ||
                    patLoc.X > searchRect.X + searchRect.Width - patternRect.Width ||
                    patLoc.Y > searchRect.Y + searchRect.Height - patternRect.Height)
                    score = 0;

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (imgTemplate != null) imgTemplate.Dispose();
                if (imgResult != null) imgResult.Dispose();
                processTime = sw.ElapsedMilliseconds;
            }
        }
        public static bool PatRotate(Image<Gray, byte> img, Image<Gray, byte> regImg, int threshold, Rectangle searchRect, Rectangle patternRect, out double score, out Point offset, out double angle, out double processTime)
        {
            Stopwatch sw = Stopwatch.StartNew();

            offset = new Point();
            angle = 0;
            score = 0;

            Image<Gray, byte> imgNew = null;
            Image<Gray, byte> imgRef = null;

            int scaledown = 2;
            double resize_scale = Math.Pow(0.5, scaledown);
            try
            {
                var searchRec2 = new Rectangle((int)(searchRect.Location.X * resize_scale), (int)(searchRect.Location.Y * resize_scale), (int)(searchRect.Width * resize_scale), (int)(searchRect.Height * resize_scale));
                var patternRec2 = new Rectangle((int)(patternRect.Location.X * resize_scale), (int)(patternRect.Location.Y * resize_scale), (int)(patternRect.Width * resize_scale), (int)(patternRect.Height * resize_scale));

                imgNew = threshold > 0 ? img.ThresholdBinary(new Gray(threshold), new Gray(255)) : img.Copy();
                imgRef = threshold > 0 ? regImg.ThresholdBinary(new Gray(threshold), new Gray(255)) : regImg.Copy();

                var imgNew2 = imgNew.Clone();
                var imgRef2 = imgRef.Clone();

                for (int i = 0; i < scaledown; i++)
                {
                    imgNew2 = imgNew2.PyrDown();
                    imgRef2 = imgRef2.PyrDown();
                }

                int div = 1;
                int maxAngle = 360 * div;

                var imgResultList = new Image<Gray, float>[maxAngle];
                var scoreList = new double[maxAngle];
                var taskList = new Task[maxAngle];

                var imgRef_patternRec = imgRef2.Copy(patternRec2);


                Enumerable.Range(0, maxAngle).ToList().ForEach(x =>
                {
                    taskList[x] = Task.Run(() =>
                    {
                        var imgResult = imgNew2.Rotate(((double)x / (double)div), new Gray()).Copy(searchRec2).MatchTemplate(imgRef_patternRec, Emgu.CV.CvEnum.TemplateMatchingType.SqdiffNormed);
                        imgResult.MinMax(out double[] _minCorr, out double[] _maxCorr, out Point[] _minPt, out Point[] _maxPt);

                        scoreList[x] = (float)(1 - _minCorr[0]) * 100;
                        imgResultList[x] = imgResult;
                    });
                });

                Task.WaitAll(taskList);

                scoreList = scoreList.Select(x => x is 100 ? 0 : x).ToArray();
                score = scoreList.Max();
                angle = Array.IndexOf(scoreList, score);

                angle /= (double)div;

                var imgRes = imgNew.Rotate(angle, new Gray()).Copy(searchRect).MatchTemplate(imgRef.Copy(patternRect), Emgu.CV.CvEnum.TemplateMatchingType.SqdiffNormed);
                imgRes.MinMax(out double[] minCorr, out double[] maxCorr, out Point[] minPt, out Point[] maxPt);
                offset.X = minPt[0].X + (searchRect.X - patternRect.X);
                offset.Y = minPt[0].Y + (searchRect.Y - patternRect.Y);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (imgNew != null) imgNew.Dispose();
                if (imgRef != null) imgRef.Dispose();
                processTime = sw.ElapsedMilliseconds;
            }
        }
        public static bool PixelMatch(Image<Gray, byte> img, int threshold, Rectangle[] searchRects, out List<double> whitePixelScore, out double processTime)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            Image<Gray, byte> imgNew = null;
            whitePixelScore = new List<double>();

            try
            {
                imgNew = threshold > 0 ? img.ThresholdBinary(new Gray(threshold), new Gray(255)) : img.Copy();

                foreach (var r in searchRects)
                {
                    var imgg = imgNew.Copy(r);
                    var percentage = (double)imgg.CountNonzero()[0] / (double)(r.Width * r.Height) * 100;
                    imgg.Dispose();
                    whitePixelScore.Add(percentage);
                }

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (imgNew != null) imgNew.Dispose();
                processTime = sw.ElapsedMilliseconds;
            }
        }

        public static bool CircleMatch(Image<Gray, byte> img, int threshold, int dp, Rectangle searchRect, Rectangle patternRect, out Point location, out double area, out double processTime)
        {
            Stopwatch sw = Stopwatch.StartNew();

            Image<Gray, byte> image;
            Image<Gray, byte> imgTemplate = null;
            Image<Gray, float> imgResult = null;

            location = new Point();
            area = 0;
            processTime = 0;

            try
            {
                image = threshold >= 0 ? img.ThresholdBinary(new Gray(threshold), new Gray(255)) : img.Clone();
                //var circles = image.HoughCircles(new Gray(), new Gray(),)


                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (imgTemplate != null) imgTemplate.Dispose();
                if (imgResult != null) imgResult.Dispose();
                processTime = sw.ElapsedMilliseconds;
            }
        }
    }
}
