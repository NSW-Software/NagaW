using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading;

namespace NagaW
{
    using Emgu.CV;
    using Emgu.CV.Structure;
    using Emgu.CV.Util;

    class TFCamera
    {
        int Index = 0;
        public TFCamera(int idx)
        {
            Index = idx;
        }

        public TEFlirCamera FlirCamera = new TEFlirCamera();
        public MVC_GenTL MVC_GenTL = new MVC_GenTL();

        public ECamType CamType;

        public Image<Gray, byte> emgucvImage
        {
            get
            {
                switch (CamType)
                {
                    default:
                    case ECamType.Spinnaker: return FlirCamera.emgucvImage;
                    case ECamType.MVC_GenTL: return MVC_GenTL.mImage;
                }
            }
        }

        public bool Connect(string IPAddress, ECamType eCamType = ECamType.Spinnaker)
        {
            CamType = eCamType;
            switch (eCamType)
            {
                default:
                case ECamType.Spinnaker:
                    {
                        try
                        {
                            if (!FlirCamera.IsConnected)
                            {
                                FlirCamera.Connect(IPAddress);
                            }

                            if (!FlirCamera.IsConnected)
                            {
                                GAlarm.Prompt(EAlarm.CAMERA_CONNECT_NOT_FOUND);
                                return false;
                            }

                            FlirCamera.Exposure = 8000;
                            FlirCamera.Gain = 1;

                            return true;
                        }
                        catch (Exception)
                        {
                            GAlarm.Prompt(EAlarm.CAMERA_UNRECOVERABLE_ERROR_RESTART);
                            return false;
                        }

                    }
                    break;
                case ECamType.MVC_GenTL:
                    {
                        #region
                        try
                        {
                            if (!MVC_GenTL.IsConnected)
                            {
                                string ctifile = @"C:\Program Files (x86)\Common Files\MVS\Runtime\Win32_i86\MvProducerGEV.cti";

                                MVC_GenTL.OpenDevice($"Cam{Index + 1}", IPAddress);

                                if (!MVC_GenTL.IsConnected)
                                {
                                    GAlarm.Prompt(EAlarm.CAMERA1_CONNECT_NOT_FOUND);
                                    MVC_GenTL.CloseDevice();
                                    return false;
                                }

                                MVC_GenTL.Exposure = 8000;
                                MVC_GenTL.Gain = 1;

                                MVC_GenTL.StartGrab();
                                //ImgHN[CamNo] = (int)MVC_GenTL.ImageHeight;
                                //ImgWN[CamNo] = (int)MVC_GenTL.ImageWidth;
                                //genTLCamera[0].StartGrab();

                                return true;
                            }
                        }
                        catch (Exception Ex)
                        {
                            try
                            {
                                MVC_GenTL.StopGrab();
                                MVC_GenTL.CloseDevice();
                            }
                            catch { }

                            return false;
                        }
                        #endregion
                    }
                    break;
            }
            return false;
        }
        public bool Connect()
        {
            if (!Connect(GSystemCfg.Camera.Cameras[Index].IPAddress, GSystemCfg.Camera.Cameras[Index].CamType)) return false;

            Gain = GSystemCfg.Camera.Cameras[Index].Gain;
            Exposure = GSystemCfg.Camera.Cameras[Index].Exposure;

            return true;
        }
        public void Disconnect()
        {
            switch (CamType)
            {
                case ECamType.Spinnaker:
                    {
                        if (!FlirCamera.IsConnected) return;

                        try
                        {
                            FlirCamera.DisConnect();
                        }
                        catch (Exception)
                        {
                        }
                    }
                    break;
                case ECamType.MVC_GenTL:
                    {
                        if (!MVC_GenTL.IsConnected) return;

                        try
                        {
                            MVC_GenTL.StopGrab();
                            emgucvImage.Dispose();
                            MVC_GenTL.CloseDevice();
                        }
                        catch (Exception)
                        {
                        }
                    }
                    break;
            }

        }
        public bool IsConnected
        {
            get
            {
                try
                {
                    switch (CamType)
                    {
                        case ECamType.Spinnaker: return FlirCamera.IsConnected;
                        case ECamType.MVC_GenTL: return MVC_GenTL.IsConnected;
                    }

                }
                catch (Exception)
                {
                    GAlarm.Prompt(EAlarm.CAMERA_UNRECOVERABLE_ERROR_RESTART);
                }
                return false;
            }
        }

        public double Gain
        {
            set
            {
                switch (CamType)
                {
                    case ECamType.Spinnaker: FlirCamera.Gain = value; break;
                    case ECamType.MVC_GenTL: MVC_GenTL.Gain = value; break;
                }
            }
            get
            {
                switch (CamType)
                {
                    default: return 0;
                    case ECamType.Spinnaker: return FlirCamera.Gain;
                    case ECamType.MVC_GenTL: return MVC_GenTL.Gain;
                }
            }
        }
        public double Exposure
        {
            set
            {
                switch (CamType)
                {
                    case ECamType.Spinnaker: FlirCamera.Exposure = value; break;
                    case ECamType.MVC_GenTL: MVC_GenTL.Exposure = value; break;
                }
            }
            get
            {
                switch (CamType)
                {
                    default: return 0;
                    case ECamType.Spinnaker: return FlirCamera.Exposure;
                    case ECamType.MVC_GenTL: return MVC_GenTL.Exposure;
                }
            }
        }

        public bool Live()
        {
            try
            {
                switch (CamType)
                {
                    case ECamType.Spinnaker: FlirCamera.Live(); break;
                    case ECamType.MVC_GenTL: MVC_GenTL.StartGrab(); break;
                }
            }
            catch
            {
                GAlarm.Prompt(EAlarm.CAMERA_UNRECOVERABLE_ERROR_RESTART);
                return false;
            }

            return true;
        }
        public bool GrabStop()
        {
            try
            {
                switch (CamType)
                {
                    case ECamType.Spinnaker: FlirCamera.GrabStop(); break;
                    case ECamType.MVC_GenTL: MVC_GenTL.StopGrab(); break;
                }
                return true;
            }
            catch (Exception ex)
            {
                GAlarm.Prompt(EAlarm.CAMERA_UNRECOVERABLE_ERROR_RESTART, ex.Message);
                return false;
            }
        }
        public bool Snap()
        {
            try
            {
                switch (CamType)
                {
                    case ECamType.Spinnaker: return FlirCamera.Snap();
                    case ECamType.MVC_GenTL: MVC_GenTL.GrabOneImage(); break;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TrigMode
        {
            get
            {
                switch (CamType)
                {
                    default:
                    case ECamType.Spinnaker: return FlirCamera.TrigMode;
                    case ECamType.MVC_GenTL: return MVC_GenTL.TriggerMode;
                }
            }
            set
            {
                switch (CamType)
                {
                    default:
                    case ECamType.Spinnaker: FlirCamera.TrigMode = value; break;
                    case ECamType.MVC_GenTL: MVC_GenTL.TriggerMode = value; break;
                }
            }
        }

        public bool TrigSourceSw
        {
            get
            {
                switch (CamType)
                {
                    default:
                    case ECamType.Spinnaker: return FlirCamera.TrigSourceSw;
                    case ECamType.MVC_GenTL: return false;
                }
            }
            set
            {
                switch (CamType)
                {
                    default:
                    case ECamType.Spinnaker: FlirCamera.TrigSourceSw = value; break;
                    case ECamType.MVC_GenTL: MVC_GenTL.SoftwareTrigger(); break;
                }
            }
        }
        public bool TrigSourceHw
        {
            get
            {
                switch (CamType)
                {
                    default:
                    case ECamType.Spinnaker: return FlirCamera.TrigSourceHw;
                    case ECamType.MVC_GenTL: return MVC_GenTL.TriggerSourceHw;
                }
            }
            set
            {
                switch (CamType)
                {
                    default:
                    case ECamType.Spinnaker: FlirCamera.TrigSourceSw = value; break;
                    case ECamType.MVC_GenTL: MVC_GenTL.TriggerSourceHw = value; break;
                }
            }
        }

        public bool GrabGetFocusValue(ref uint FV)
        {
            Emgu.CV.Image<Emgu.CV.Structure.Gray, byte> img = null;

            try
            {
                switch (CamType)
                {
                    default:
                    case ECamType.Spinnaker:
                        {
                            FlirCamera.Snap();
                            img = FlirCamera.emgucvImage.Clone();
                        }
                        break;
                    case ECamType.MVC_GenTL:
                        {
                            MVC_GenTL.GrabOneImage();
                            img = MVC_GenTL.mImage.Clone();
                        }
                        break;
                }


                int W = 200;
                int H = 200;
                img.ROI = new Rectangle((img.Width - W) / 2, (img.Height - H) / 2, W, H);

                byte[,,] data = img.Data;
                uint num = 0;
                for (int i = img.ROI.Top; i < (img.ROI.Bottom - 1); i++)
                {
                    for (int j = img.ROI.Left; j < (img.ROI.Right - 1); j++)
                    {
                        byte num4 = data[i, j, 0];
                        byte num5 = data[i + 1, j + 1, 0];
                        uint num6 = (uint)(num4 - num5);
                        num += num6 * num6;
                    }
                }
                img.ROI = Rectangle.Empty;
                FV = num;

                return true;
            }
            catch
            {
                GAlarm.Prompt(EAlarm.CAMERA_UNRECOVERABLE_ERROR_RESTART);
                return false;
            }
            finally
            {
                img.Dispose();
            }
        }
    }
    class TFCameras
    {
        public static TFCamera[] Camera = Enumerable.Range(0, GSystemCfg.Camera.Count).Select(x => new TFCamera(x)).ToArray();
    }

    class TCAutoFocus
    {
        public static bool Execute(TEZMCAux.TGroup gantrySelect)
        {
            try
            {
                double dCoarseDist = 0.5;
                int tCoarseMoveDelay = 5;
                int Coarse_Max_Ite = (int)(5 / 0.2);

                double dFineDist = 0.010;
                int tFineMoveDelay = 10;
                int Fine_Max_Ite = (int)((dCoarseDist * 2) / dFineDist);

                uint MaxFV = 0;
                uint FV = 0;

                TFCamera1.Cameras[gantrySelect.Index].GrabGetFocusValue(ref MaxFV);
                //TFCameras.Camera[gantrySelect.Index].GrabGetFocusValue(ref MaxFV);
                if (MaxFV < 100000)
                {
                    GAlarm.Prompt(EAlarm.FOCUS_VALUE_CONTRAST_LOW);
                    return false;
                }

                #region Coarse Search
                int Ite = 0;
                gantrySelect.ZAxis.SetParam(0.1, 10, 500);
                if (!gantrySelect.ZAxis.MoveRel(dCoarseDist, true)) return false;
                if (!gantrySelect.ZAxis.Wait()) return false;
                var sw = Stopwatch.StartNew(); while (sw.ElapsedMilliseconds < tCoarseMoveDelay) { Thread.Sleep(1); }

                TFCamera1.Cameras[gantrySelect.Index].GrabGetFocusValue(ref FV);
                //TFCameras.Camera[gantrySelect.Index].GrabGetFocusValue(ref FV);
                Ite++;

                if (FV > MaxFV)//Image is more focus in plus direction
                {
                    while (true)
                    {
                        MaxFV = Math.Max(FV, MaxFV);

                        if (!gantrySelect.ZAxis.MoveRel(dCoarseDist, true)) return false;
                        if (!gantrySelect.ZAxis.Wait()) return false;

                        sw = Stopwatch.StartNew();
                        while (sw.ElapsedMilliseconds < tCoarseMoveDelay) { Thread.Sleep(1); }

                        TFCamera1.Cameras[gantrySelect.Index].GrabGetFocusValue(ref FV);
                        //TFCameras.Camera[gantrySelect.Index].GrabGetFocusValue(ref FV);
                        Ite++;

                        if (FV < MaxFV)//Image focus is less, continue fine from previous 2 coarse step
                        {
                            if (!gantrySelect.ZAxis.MoveRel(-dCoarseDist * 2, true)) return false;
                            if (!gantrySelect.ZAxis.Wait()) return false;
                            break;
                        }
                        if (Ite >= Coarse_Max_Ite)
                        {
                            GAlarm.Prompt(EAlarm.AUTO_FOCUS_EXCEED_COARSE_ITERATION);
                            return false;
                        }
                    }
                }
                else//Image is more focus in minus direction
                {
                    while (true)
                    {
                        MaxFV = Math.Max(FV, MaxFV);

                        if (!gantrySelect.ZAxis.MoveRel(-dCoarseDist, true)) return false;
                        if (!gantrySelect.ZAxis.Wait()) return false;

                        sw = Stopwatch.StartNew();
                        while (sw.ElapsedMilliseconds < tCoarseMoveDelay) { Thread.Sleep(1); }

                        TFCamera1.Cameras[gantrySelect.Index].GrabGetFocusValue(ref FV);
                        //TFCameras.Camera[gantrySelect.Index].GrabGetFocusValue(ref FV);
                        Ite++;

                        if (FV < MaxFV)//Image focus is less, continue fine from here
                        {
                            break;
                        }
                        if (Ite >= Coarse_Max_Ite)
                        {
                            GAlarm.Prompt(EAlarm.AUTO_FOCUS_EXCEED_COARSE_ITERATION);
                            return false;
                        }
                    }
                }
                #endregion

                #region Fine Search - Search in Plus dir only
                Ite = 0;
                FV = 0;
                TFCamera1.Cameras[gantrySelect.Index].GrabGetFocusValue(ref MaxFV);
                //TFCameras.Camera[gantrySelect.Index].GrabGetFocusValue(ref MaxFV);

                while (true)
                {
                    MaxFV = Math.Max(MaxFV, FV);

                    if (!gantrySelect.ZAxis.MoveRel(dFineDist, true)) return false;
                    if (!gantrySelect.ZAxis.Wait()) return false;
                    sw = Stopwatch.StartNew(); while (sw.ElapsedMilliseconds < tFineMoveDelay) { Thread.Sleep(1); }

                    TFCamera1.Cameras[gantrySelect.Index].GrabGetFocusValue(ref FV);
                    //TFCameras.Camera[gantrySelect.Index].GrabGetFocusValue(ref FV);
                    Ite++;

                    if (FV < MaxFV)
                    {
                        //Best focus value found
                        if (!gantrySelect.ZAxis.MoveRel(-dFineDist, true)) return false;
                        if (!gantrySelect.ZAxis.Wait()) return false;
                        sw = Stopwatch.StartNew(); while (sw.ElapsedMilliseconds < tFineMoveDelay) { Thread.Sleep(1); }
                        break;
                    }
                    if (Ite >= Fine_Max_Ite)
                    {
                        GAlarm.Prompt(EAlarm.AUTO_FOCUS_EXCEED_FINE_ITERATION);
                        return false;
                    }
                }
                #endregion

                TFCamera1.Cameras[gantrySelect.Index].Live();
                //TFCameras.Camera[gantrySelect.Index].Live();

                return true;
            }
            catch
            {
                GAlarm.Prompt(EAlarm.AUTO_FOCUS_UNRECOVERABLE_ERROR);
                return false;
            }
        }
    }

    class TFVision
    {
        public static bool PatLearn(Image<Gray, byte> nowImg, ref Image<Gray, byte> regImg, ref int threshold, ref Rectangle[] rects)
        {
            try
            {
                frmImageSelectBox frmSelectBox = new frmImageSelectBox(nowImg, regImg, "Define Search and Pattern Windows.", rects, new string[] { "Search Window", "Pattern Window" });

                frmSelectBox.TopMost = true;
                frmSelectBox.Threshold = threshold;
                frmSelectBox.Shown += (a, b) =>
                {
                    if (GSystemCfg.Option.PromptMSg_AckPAtAlignment_Centred) MsgBox.ShowDialog("Centre the alignment point");
                };
                DialogResult dr = frmSelectBox.ShowDialog();


                if (dr == DialogResult.OK)
                {
                    regImg = frmSelectBox.regImage.Copy();
                    threshold = frmSelectBox.Threshold;
                }

                return true;
            }
            catch (Exception ex)
            {
                GAlarm.Prompt(EAlarm.VISION_LEARN_PATTERN_ERROR, ex.Message.ToString());
                return false;
            }
            finally
            {
                if (nowImg != null) nowImg.Dispose();
            }
        }
        public static bool PatMatch(Image<Gray, byte> img, Image<Gray, byte> regImg, int threshold, Rectangle[] rect, ref PointD patLoc, ref PointD patOffset, ref double score)
        {
            Image<Gray, byte> image = null;
            Image<Gray, byte> imgTemplate = null;
            Image<Gray, float> imgResult = null;
            try
            {
                if (threshold >= 0)
                {
                    image = img.ThresholdBinary(new Gray(threshold), new Gray(255));
                    imgTemplate = regImg.ThresholdBinary(new Gray(threshold), new Gray(255));
                }
                else
                {
                    image = img.Copy();
                    imgTemplate = regImg.Copy();
                }

                //  Define search rect to include pattern size for part edge detection
                Rectangle searchRect = rect[0];
                searchRect.X = Math.Max(0, rect[0].X - rect[1].Width);
                searchRect.Y = Math.Max(0, rect[0].Y - rect[1].Height);
                searchRect.Width = Math.Min(regImg.Width - searchRect.X, rect[0].Width + rect[1].Width * 2);
                searchRect.Height = Math.Min(regImg.Height - searchRect.Y, rect[0].Height + rect[1].Height * 2);

                imgResult = image.Copy(searchRect).MatchTemplate(imgTemplate.Copy(rect[1]), Emgu.CV.CvEnum.TemplateMatchingType.SqdiffNormed);

                double[] minCorr;
                double[] maxCorr;
                Point[] minPt;
                Point[] maxPt;
                imgResult.MinMax(out minCorr, out maxCorr, out minPt, out maxPt);

                patLoc.X = searchRect.X + (float)minPt[0].X;
                patLoc.Y = searchRect.Y + (float)minPt[0].Y;
                patOffset.X = patLoc.X - rect[1].X;
                patOffset.Y = patLoc.Y - rect[1].Y;
                score = (float)(1 - minCorr[0]);

                //  Set score to 0 if out of search reigion
                if (patLoc.X < rect[0].X || patLoc.Y < rect[0].Y ||
                    patLoc.X > rect[0].X + rect[0].Width - rect[1].Width ||
                    patLoc.Y > rect[0].Y + rect[0].Height - rect[1].Height)
                    score = 0;

                return true;
            }
            catch (Exception ex)
            {
                TFCamera1.Cameras[0].Reconnect();
                GAlarm.Prompt(EAlarm.VISION_UNDEFINED_ERROR, ex.Message.ToString());
                return false;
            }
            finally
            {
                if (imgTemplate != null) imgTemplate.Dispose();
                if (imgResult != null) imgResult.Dispose();
            }
        }

        public static PointD Translate(PointD point, TAlignData alignData)
        {
            //A = angle_radian
            //
            //[x']   [x][cosA -sinA]
            //[  ] = [ ][          ]
            //[y']   [y][sinA  cosA]
            //
            //x' = x*cosA - y*sinA
            //y' = x*sinA + y*cosA
            //
            //(1)Rotate about a center xc, yc
            //
            //x' = xc + (x - xc)*cosA - (y - yc)*sinA
            //y' = yc + (x - xc)*sinA + (y - yc)*cosA
            //
            //(2)Tranlate offset xo, yo
            //x' += xo
            //y' += yo

            PointD ptOri = point;
            double angle = alignData.Angle_Rad;

            PointD ptRotate = new PointD(ptOri);
            ptRotate.X = alignData.Datum.X + (ptRotate.X - alignData.Datum.X) * Math.Cos(angle) - (ptRotate.Y - alignData.Datum.Y) * Math.Sin(angle);
            ptRotate.Y = alignData.Datum.Y + (ptRotate.X - alignData.Datum.X) * Math.Sin(angle) + (ptRotate.Y - alignData.Datum.Y) * Math.Cos(angle);

            PointD ptTranslate = new PointD(ptRotate) + alignData.Offset;

            return ptTranslate;
        }
    }
}
