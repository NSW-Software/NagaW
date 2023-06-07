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
    using Emgu.CV.UI;

    public class TFCamera1
    {
        public event EventHandler<EventArgs> Connected;
        public event EventHandler<EventArgs> DisConnected;

        private MVC_GenTL MVC_GenTL = new MVC_GenTL(); // this API does support Flir Camera

        private ECamType Type;
        public int Index { get; private set; } = 0;
        public Image<Gray, byte> emgucvImage
        {
            get
            {
                switch (Type)
                {
                    default:
                    case ECamType.Spinnaker:
                    case ECamType.MVC_GenTL: return MVC_GenTL.mImage;
                }
            }
        }

        public TFCamera1(int idx)
        {
            Index = idx;
        }

        public bool Connect(ECamType type, string ip, double exposureTime_ms = 8000, double gain = 1)
        {
            try
            {
                Type = type;

                switch (Type)
                {
                    default:
                    case ECamType.Spinnaker:
                    case ECamType.MVC_GenTL:
                        {
                            if (!MVC_GenTL.IsConnected)
                            {
                                //string ctifile = @"C:\Program Files (x86)\Common Files\MVS\Runtime\Win32_i86\MvProducerGEV.cti";

                                MVC_GenTL.OpenDevice($"Cam{Index + 1}", ip);

                                if (!MVC_GenTL.IsConnected)
                                {
                                    GAlarm.Prompt(EAlarm.CAMERA_CONNECT_NOT_FOUND);
                                    MVC_GenTL.CloseDevice();
                                    return false;
                                }

                                MVC_GenTL.Exposure = exposureTime_ms;
                                MVC_GenTL.Gain = gain;

                                MVC_GenTL.StartGrab();

                                Connected?.Invoke(this, new EventArgs());
                                return true;
                            }
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                try
                {
                    MVC_GenTL.StopGrab();
                    MVC_GenTL.CloseDevice();
                }
                catch { }
                GAlarm.Prompt(EAlarm.CAMERA_CONNECT_NOT_FOUND, ex);
            }

            return false;
        }
        public bool Connect(bool enableStartUp = false)
        {
            var cfg = GSystemCfg.Camera.Cameras[Index];
            return !enableStartUp || cfg.StartUpEnable ? Connect(cfg.CamType, cfg.IPAddress, cfg.Exposure, cfg.Gain) : false;
        }

        public void Disconnect()
        {
            try
            {
                switch (Type)
                {
                    case ECamType.Spinnaker:
                    case ECamType.MVC_GenTL:
                        {
                            if (!MVC_GenTL.IsConnected) return;
                            MVC_GenTL.StopGrab();
                            MVC_GenTL.CloseDevice();

                            DisConnected?.Invoke(this, new EventArgs());
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                GAlarm.Prompt(EAlarm.CAMERA_CONNECT_NOT_FOUND, ex);
            }
        }

        public void Reconnect()
        {
            Disconnect();
            Thread.Sleep(1000);
            Connect();
        }

        public void RegisterPicBox(ImageBox imageBox)
        {
            try
            {
                switch (Type)
                {
                    case ECamType.Spinnaker:
                    case ECamType.MVC_GenTL: MVC_GenTL.RegisterPictureBox(imageBox); break;
                    default: break;
                }
            }
            catch (Exception ex)
            {
                GAlarm.Prompt(EAlarm.CAMERA_CONNECT_NOT_FOUND, ex);
            }
        }

        public bool IsConnected
        {
            get
            {
                try
                {
                    switch (Type)
                    {
                        case ECamType.Spinnaker:
                        case ECamType.MVC_GenTL: return MVC_GenTL.IsConnected;
                    }

                }
                catch
                {
                    return false;
                }
                return false;
            }
        }

        public double Gain
        {
            set
            {
                try
                {
                    switch (Type)
                    {
                        case ECamType.Spinnaker:
                        case ECamType.MVC_GenTL: MVC_GenTL.Gain = value; break;
                    }
                }
                catch (Exception ex)
                {
                    GAlarm.Prompt(EAlarm.CAMERA_CONNECT_NOT_FOUND, ex);
                }

            }
            get
            {
                try
                {
                    switch (Type)
                    {
                        default: return 0;
                        case ECamType.Spinnaker:
                        case ECamType.MVC_GenTL: return MVC_GenTL.Gain;
                    }
                }
                catch (Exception ex)
                {
                    GAlarm.Prompt(EAlarm.CAMERA_CONNECT_NOT_FOUND, ex);
                    return 0;
                }
            }
        }
        public double Exposure
        {
            set
            {
                try
                {
                    switch (Type)
                    {
                        case ECamType.Spinnaker:
                        case ECamType.MVC_GenTL: MVC_GenTL.Exposure = value; break;
                    }
                }
                catch (Exception ex)
                {
                    GAlarm.Prompt(EAlarm.CAMERA_CONNECT_NOT_FOUND, ex);
                }

            }
            get
            {
                try
                {
                    switch (Type)
                    {
                        default: return 0;
                        case ECamType.Spinnaker:
                        case ECamType.MVC_GenTL: return MVC_GenTL.Exposure;
                    }
                }
                catch (Exception ex)
                {
                    GAlarm.Prompt(EAlarm.CAMERA_CONNECT_NOT_FOUND, ex);
                    return 0;
                }
            }
        }

        public bool Live()
        {
            try
            {
                switch (Type)
                {
                    case ECamType.Spinnaker:
                    case ECamType.MVC_GenTL: MVC_GenTL.StartGrab(); break;
                }
            }
            catch (Exception ex)
            {
                GAlarm.Prompt(EAlarm.CAMERA_CONNECT_NOT_FOUND, ex);
                return false;
            }

            return true;
        }
        public bool GrabStop()
        {
            try
            {
                switch (Type)
                {
                    case ECamType.Spinnaker:
                    case ECamType.MVC_GenTL: MVC_GenTL.StopGrab(); break;
                }
                return true;
            }
            catch (Exception ex)
            {
                GAlarm.Prompt(EAlarm.CAMERA_CONNECT_NOT_FOUND, ex);
                return false;
            }
        }
        public bool Snap()
        {
            try
            {
                switch (Type)
                {
                    case ECamType.Spinnaker:
                    case ECamType.MVC_GenTL: MVC_GenTL.GrabOneImage(); break;
                }
                return true;
            }
            catch (Exception ex)
            {
                GAlarm.Prompt(EAlarm.CAMERA_CONNECT_NOT_FOUND, ex);
                return false;
            }
        }

        public bool TrigMode
        {
            get
            {
                try
                {
                    switch (Type)
                    {
                        default:
                        case ECamType.Spinnaker:
                        case ECamType.MVC_GenTL: return MVC_GenTL.TriggerMode;
                    }
                }
                catch (Exception ex)
                {
                    GAlarm.Prompt(EAlarm.CAMERA_CONNECT_NOT_FOUND, ex);
                    return false;
                }

            }
            set
            {
                try
                {
                    switch (Type)
                    {
                        default:
                        case ECamType.Spinnaker:
                        case ECamType.MVC_GenTL: MVC_GenTL.TriggerMode = value; break;
                    }
                }
                catch (Exception ex)
                {
                    GAlarm.Prompt(EAlarm.CAMERA_CONNECT_NOT_FOUND, ex);
                }
            }
        }

        public bool TrigSourceSw
        {
            get
            {
                try
                {
                    switch (Type)
                    {
                        default:
                        case ECamType.Spinnaker:
                        case ECamType.MVC_GenTL: return false;
                    }
                }
                catch (Exception ex)
                {
                    GAlarm.Prompt(EAlarm.CAMERA_CONNECT_NOT_FOUND, ex);
                    return false;
                }
            }
            set
            {
                try
                {
                    switch (Type)
                    {
                        default:
                        case ECamType.Spinnaker:
                        case ECamType.MVC_GenTL: MVC_GenTL.SoftwareTrigger(); break;
                    }
                }
                catch (Exception ex)
                {
                    GAlarm.Prompt(EAlarm.CAMERA_CONNECT_NOT_FOUND, ex);
                }
            }
        }
        public bool TrigSourceHw
        {
            get
            {
                try
                {
                    switch (Type)
                    {
                        default:
                        case ECamType.Spinnaker:
                        case ECamType.MVC_GenTL: return MVC_GenTL.TriggerSourceHw;
                    }
                }
                catch (Exception ex)
                {
                    GAlarm.Prompt(EAlarm.CAMERA_CONNECT_NOT_FOUND, ex);
                    return false;
                }

            }
            set
            {
                try
                {
                    switch (Type)
                    {
                        default:
                        case ECamType.Spinnaker:
                        case ECamType.MVC_GenTL: MVC_GenTL.TriggerSourceHw = value; break;
                    }
                }
                catch (Exception ex)
                {
                    GAlarm.Prompt(EAlarm.CAMERA_CONNECT_NOT_FOUND, ex);
                }
            }
        }

        public bool GrabGetFocusValue(ref uint FV)
        {
            Emgu.CV.Image<Emgu.CV.Structure.Gray, byte> img = null;

            try
            {
                switch (Type)
                {
                    default:
                    case ECamType.Spinnaker:
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
            catch (Exception ex)
            {
                GAlarm.Prompt(EAlarm.CAMERA_CONNECT_NOT_FOUND, ex);
                return false;
            }
            finally
            {
                img.Dispose();
            }
        }



        public static TFCamera1[] Cameras = Enumerable.Range(0, GSystemCfg.Camera.Cameras.Length).Select(x => new TFCamera1(x)).ToArray();
    }
}
