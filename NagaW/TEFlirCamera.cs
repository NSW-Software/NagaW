using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace NagaW
{
    using Emgu.CV;
    using Emgu.CV.Structure;
    using SpinnakerNET;
    using SpinnakerNET.GenApi;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    class TEFlirCamera
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDllDirectory(string lpPathName);

        ManagedSystem camSystem = null;// new ManagedSystem();
        IList<IManagedCamera> camList = null;

        IManagedCamera mCamera = null;
        bool m_isGrabbing = false;
        int m_iCamWidthMax = 1280;
        int m_iCamHeightMax = 1024;
        int m_iCamWidth = 1280;
        int m_iCamHeight = 1024;
        ulong m_grabTimeOut = 500;

        public Image<Gray, Byte> emgucvImage = new Image<Gray, byte>(808, 608, new Gray(0));
        public Image<Bgr, Byte> emgucvCImage = new Image<Bgr, byte>(808, 608, new Bgr(0, 0, 0));

        ManagedImage m_imageMono = new ManagedImage();
        ManagedImage m_imageColor = new ManagedImage();

        public double dFPS = 0;
        public Emgu.CV.UI.ImageBox imgBoxEmgu;

        public TEFlirCamera()
        {
            Init();
        }
        ~TEFlirCamera()
        {
            DeInit();
        }

        public void Init()
        {
            emgucvInit(808, 608);

            if (camSystem != null) DeInit();

            camSystem = new ManagedSystem();
            camList = camSystem.GetCameras();
        }
        public void DeInit()
        {
            if (camList != null) camList.Clear();
            if (camSystem != null) camSystem.Dispose();
            camSystem = null;
        }

        public int CamCount
        {
            get
            {
                if (camList == null) return 0;
                return camList.Count;
            }
        }

        public void emgucvInit(int iWidth, int iHeight)
        {
            emgucvImage = new Image<Gray, byte>(iWidth, iHeight, new Gray(0));
            emgucvCImage = new Image<Bgr, byte>(iWidth, iHeight, new Bgr(0, 0, 0));

            unsafe
            {
                var data = emgucvImage.Data;
                int stride = emgucvImage.MIplImage.WidthStep;
                fixed (byte* pData = data)
                {
                    byte* go = pData;
                    for (int j = 0; j < emgucvImage.MIplImage.Height; j++)
                    {
                        for (int i = 0; i < emgucvImage.MIplImage.Width; i++)
                        {
                            go[i] = (byte)(i % 255);
                        }
                        go += stride;
                    }

                    m_imageMono.ResetImage((uint)iWidth, (uint)iHeight, 0, 0, PixelFormatEnums.Mono8, pData);
                }

                var dataC = emgucvCImage.Data;
                int strideC = emgucvCImage.MIplImage.WidthStep;
                fixed (byte* pData = dataC)
                {
                    byte* go = pData;
                    for (int j = 0; j < emgucvCImage.MIplImage.Height; j++)
                    {
                        for (int i = 0; i < emgucvCImage.MIplImage.Width * 3; i++)
                        {
                            go[i] = (byte)(i % 255);
                        }
                        go += strideC;
                    }

                    m_imageColor.ResetImage((uint)iWidth, (uint)iHeight, 0, 0, PixelFormatEnums.BGR8, pData);
                }
            }
        }

        public void Connect(int camIndex)
        {
            if (CamCount == 0) throw new Exception("No camera detected. ");

            if (camIndex > CamCount) throw new Exception("Invalid camera index. ");

            try
            {
                int iIndex = 0;
                foreach (IManagedCamera managedCamera in camList)
                {
                    if (iIndex == camIndex)
                    {
                        mCamera = managedCamera;
                        mCamera.Init();

                        m_iCamWidthMax = (int)mCamera.WidthMax;
                        m_iCamHeightMax = (int)mCamera.HeightMax;
                        m_iCamWidth = (int)mCamera.Width;
                        m_iCamHeight = (int)mCamera.Height;

                        //not used, future use
                        //string strPixelFormat = mCamera.PixelFormat.ToString();
                        //INodeMap nodeMap = m_Camera.GetNodeMap();

                        //Acquisition mode to continuous
                        mCamera.AcquisitionMode.Value = AcquisitionModeEnums.Continuous.ToString();

                        //trigger enabled
                        //m_Camera.TriggerMode.Value = TriggerModeEnums.On.ToString();

                        //Set software trigger
                        mCamera.TriggerSource.Value = TriggerSourceEnums.Software.ToString();

                        //select hardware trigger Line0
                        mCamera.LineSelector.Value = LineSelectorEnums.Line0.ToString();

                        break;
                    }
                    iIndex++;
                }
                if (!mCamera.IsInitialized() && !mCamera.IsValid()) throw new Exception("Camera connect fail. ");

                emgucvInit(m_iCamWidth, m_iCamHeight);
            }
            catch (Exception ex)
            {
                throw new Exception("Connect ExError " + ex.Message.ToString() + ". ");
            }
        }
        public void Connect(string ipAddress)
        {
            int iIndex = 0;
            foreach (IManagedCamera managedCamera in camList)
            {
                Integer iIPAddress = managedCamera.TLDevice.GevDeviceIPAddress;
                string sIPAddress = ((iIPAddress >> 24) & 0xFF) + "." + ((iIPAddress >> 16) & 0xFF) + "." + ((iIPAddress >> 8) & 0xFF) + "." + (iIPAddress & 0xFF);

                if (sIPAddress == ipAddress)
                {
                    Connect(iIndex);
                    break;
                }

                iIndex++;
            }
        }
        public void DisConnect()
        {
            if (mCamera == null) return;

            if (m_isGrabbing)
            {
                m_isGrabbing = false;

                if (taskGrab != null && !taskGrab.IsCompleted)
                {
                    Task.WaitAll(taskGrab);
                }
            }

            if (mCamera.IsStreaming()) mCamera.EndAcquisition();

            //Disable trigger mode
            mCamera.TriggerMode.Value = TriggerModeEnums.Off.ToString();
            mCamera.DeInit();
        }

        public bool IsConnected
        {
            get
            {
                if (mCamera == null) return false;
                //return this.m_isConnected;

                bool b = mCamera.IsValid();
                bool b2 = mCamera.IsStreaming();
                bool b3 = mCamera.IsInitialized();

                return b && b3;//mCamera.IsInitialized();
            }
        }
        public bool IsGrabbing()
        {
            if (mCamera == null) return false;
            return m_isGrabbing;
        }

        public bool TrigMode//enable trigger mode
        {
            set
            {
                if (mCamera == null) return;
                if (!mCamera.IsInitialized()) return;

                mCamera.TriggerMode.Value = value ? TriggerModeEnums.On.ToString() : TriggerModeEnums.Off.ToString();
            }
            get
            {
                if (mCamera == null) return false;
                if (!mCamera.IsInitialized()) return false;

                return (mCamera.TriggerMode.Value == TriggerModeEnums.On.ToString());
            }
        }
        public bool TrigSourceHw
        {
            set
            {
                if (mCamera == null) return;
                if (!mCamera.IsInitialized()) return;

                mCamera.TriggerSource.Value = value ? TriggerSourceEnums.Line0.ToString() : TriggerSourceEnums.Software.ToString();
            }
            get
            {
                if (mCamera == null) return false;
                if (!mCamera.IsInitialized()) return false;

                return (mCamera.TriggerSource.Value == TriggerSourceEnums.Line0.ToString());
            }
        }
        public bool TrigSourceSw
        {
            set
            {
                if (mCamera == null) return;
                if (!mCamera.IsInitialized()) return;

                mCamera.TriggerSource.Value = value ? TriggerSourceEnums.Software.ToString() : TriggerSourceEnums.Line0.ToString();
            }
            get
            {
                if (mCamera == null) return false;
                if (!mCamera.IsInitialized()) return false;

                return (mCamera.TriggerSource.Value == TriggerSourceEnums.Software.ToString());
            }
        }
        public void SwTrig(uint timeout)
        {
            Grab(timeout);
            if (mCamera.TriggerSoftware.IsWritable) mCamera.TriggerSoftware.Execute();
        }

        private bool GetFrame(ulong timeout)//ms
        {
            bool bStatus = true;
            try
            {
                if (timeout == 0)
                {
                    using (IManagedImage rawImage = mCamera.GetNextImage())
                    {
                        if (rawImage.IsIncomplete)
                        {
                            Debug.WriteLine("Image incomplete with image status {0}...", rawImage.ImageStatus);
                            bStatus = false;
                        }
                        else
                        {
                            // Convert image to mono 8
                            using (IManagedImage convertedImage = rawImage.Convert(PixelFormatEnums.Mono8))
                            {
                                unsafe
                                {
                                    byte* data = m_imageMono.NativeData;
                                    byte* dataSource = convertedImage.NativeData;

                                    IntPtr[] source = new IntPtr[m_imageMono.Height];
                                    IntPtr[] dest = new IntPtr[m_imageMono.Height];
                                    Parallel.For(0, m_imageMono.Height, y =>
                                    {
                                        source[y] = (IntPtr)(dataSource + (convertedImage.Stride * y));
                                        dest[y] = (IntPtr)(data + (m_imageMono.Stride * y));
                                    });
                                    Parallel.For(0, m_imageMono.Height, y =>
                                    {
                                        Buffer.MemoryCopy(source[y].ToPointer(), dest[y].ToPointer(), m_imageMono.Width, m_imageMono.Width);
                                    });
                                }
                            }

                            // Convert image to color
                            using (IManagedImage convertedImage = rawImage.Convert(PixelFormatEnums.BGR8))
                            {
                                unsafe
                                {
                                    byte* data = m_imageColor.NativeData;
                                    byte* dataSource = convertedImage.NativeData;

                                    IntPtr[] source = new IntPtr[m_imageColor.Height];
                                    IntPtr[] dest = new IntPtr[m_imageColor.Height];
                                    Parallel.For(0, m_imageColor.Height, y =>
                                    {
                                        source[y] = (IntPtr)(dataSource + (convertedImage.Stride * y));
                                        dest[y] = (IntPtr)(data + (m_imageColor.Stride * y));
                                    });

                                    Parallel.For(0, m_imageColor.Height, y =>
                                    {
                                        Buffer.MemoryCopy(source[y].ToPointer(), dest[y].ToPointer(), m_imageColor.Width * 3, m_imageColor.Width * 3);
                                    });
                                }
                            }
                        }
                    }
                }
                else
                {
                    using (IManagedImage rawImage = mCamera.GetNextImage(timeout))
                    {
                        if (rawImage.IsIncomplete)
                        {
                            Debug.WriteLine("Image incomplete with image status {0}...", rawImage.ImageStatus);
                            bStatus = false;
                        }
                        else
                        {
                            // Convert image to mono 8
                            using (IManagedImage convertedImage = rawImage.Convert(PixelFormatEnums.Mono8))
                            {
                                unsafe
                                {
                                    byte* data = m_imageMono.NativeData;
                                    byte* dataSource = convertedImage.NativeData;

                                    IntPtr[] source = new IntPtr[m_imageMono.Height];
                                    IntPtr[] dest = new IntPtr[m_imageMono.Height];
                                    Parallel.For(0, m_imageMono.Height, y =>
                                    {
                                        source[y] = (IntPtr)(dataSource + (convertedImage.Stride * y));
                                        dest[y] = (IntPtr)(data + (m_imageMono.Stride * y));
                                    });
                                    Parallel.For(0, m_imageMono.Height, y =>
                                    {
                                        Buffer.MemoryCopy(source[y].ToPointer(), dest[y].ToPointer(), m_imageMono.Width, m_imageMono.Width);
                                    });
                                }
                            }

                            // Convert image to color
                            using (IManagedImage convertedImage = rawImage.Convert(PixelFormatEnums.BGR8))
                            {
                                unsafe
                                {
                                    byte* data = m_imageColor.NativeData;
                                    byte* dataSource = convertedImage.NativeData;

                                    IntPtr[] source = new IntPtr[m_imageColor.Height];
                                    IntPtr[] dest = new IntPtr[m_imageColor.Height];
                                    Parallel.For(0, m_imageColor.Height, y =>
                                    {
                                        source[y] = (IntPtr)(dataSource + (convertedImage.Stride * y));
                                        dest[y] = (IntPtr)(data + (m_imageColor.Stride * y));
                                    });

                                    Parallel.For(0, m_imageColor.Height, y =>
                                    {
                                        Buffer.MemoryCopy(source[y].ToPointer(), dest[y].ToPointer(), m_imageColor.Width * 3, m_imageColor.Width * 3);
                                    });
                                }
                            }
                        }
                    }
                }
            }
            catch (SpinnakerException ex)
            {
                Debug.WriteLine("Error: {0}", ex.Message);
                bStatus = false;
            }

            return bStatus;
        }

        public int saveImage = 0;
        private void TaskGrab()
        {
            bool bStatus = false;
            Stopwatch swSnap = new Stopwatch();
            int iCountFPS = 0;

            while (m_isGrabbing)
            {
                if (iCountFPS >= 25)
                {
                    swSnap.Restart();
                    iCountFPS = 0;
                }
                else
                    swSnap.Start();
                
                iCountFPS++;

                bStatus = GetFrame(m_grabTimeOut);

                swSnap.Stop();

                if (iCountFPS >= 25) dFPS = (1000.0 / swSnap.ElapsedMilliseconds) * iCountFPS;

                if (bStatus)
                {
                    if (imgBoxEmgu != null)
                    {
                        imgBoxEmgu.Invalidate();
                        if (mCamera.TriggerMode.Value == TriggerModeEnums.On.ToString())
                            //((Image<Bgr, byte>)imgBoxEmgu.Image).Save(@"c:\Image\Image" + $"{saveImage++}.png");
                        ((Image<Bgr, byte>)imgBoxEmgu.Image).Save(@"c:\Image\" + $"{DateTime.Now:yyyyMMdd_HHmmssfff}_Image{saveImage++}.png");
                    }
                }
            }
        }

        Task taskGrab = null;
        public void GrabStop()
        {
            if (mCamera == null) return;

            m_isGrabbing = false;

            if (taskGrab != null && taskGrab.Status == TaskStatus.Running)
            {
                Task.WaitAll(taskGrab);
            }

            if (mCamera.IsStreaming())
            {
                try
                {
                    mCamera.EndAcquisition();
                }
                catch { }
            }
        }
        public void Grab(uint TimeOut)//grab according to TriggerMode
        {
            if (mCamera == null) return;
            if (!mCamera.IsInitialized()) return;
            if (m_isGrabbing) return;

            m_isGrabbing = true;

            if (!mCamera.IsStreaming())
                mCamera.BeginAcquisition();

            m_grabTimeOut = TimeOut;
            taskGrab = new Task(new Action(TaskGrab));
            taskGrab.Start();
        }
        public bool GrabOne(uint TimeOut)//grab 1 frame according to TriggerMode
        {
            if (mCamera == null) return false;
            if (!mCamera.IsInitialized()) return false;
            if (m_isGrabbing) return false;

            m_isGrabbing = true;

            if (!mCamera.IsStreaming()) mCamera.BeginAcquisition();

            bool bStatus = GetFrame(TimeOut);
            if (bStatus && imgBoxEmgu != null) imgBoxEmgu.Invalidate();

            m_isGrabbing = false;

            return bStatus;
        }

        public void Live()//TrigMode = false, Grab Continuous
        {
            try
            {
                GrabStop();
                TrigMode = false;
                Grab(500);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        public bool Snap()//TrigMode = false, GrabOne
        {
            try
            {
                GrabStop();
                TrigMode = false;
                return GrabOne(500);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return false;
            }
        }
        public void Stop()//GrabStop
        {
            GrabStop();
        }

        #region Properties
        public double Exposure//ns
        {
            get
            {
                return mCamera.ExposureTime.Value;
            }
            set
            {
                if (mCamera.ExposureAuto.IsWritable)
                    mCamera.ExposureAuto.Value = ExposureAutoEnums.Off.ToString();

                mCamera.ExposureMode.Value = ExposureModeEnums.Timed.ToString();
                double dToSet = value > mCamera.ExposureTime.Max ? mCamera.ExposureTime.Max : value;
                dToSet = dToSet < mCamera.ExposureTime.Min ? mCamera.ExposureTime.Min : value;
                mCamera.ExposureTime.Value = dToSet;
            }
        }
        public bool ExposureAuto
        {
            set
            {
                if (mCamera.ExposureAuto.IsWritable)
                    mCamera.ExposureAuto.Value = value ? ExposureAutoEnums.Continuous.ToString() : ExposureAutoEnums.Off.ToString();
            }
            get
            {
                return mCamera.ExposureAuto.Value == ExposureAutoEnums.Continuous.ToString();
            }
        }

        public double Gain//Gain_dB
        {
            get
            {
                return mCamera.Gain.Value;
            }
            set
            {
                mCamera.GainAuto.Value = GainAutoEnums.Off.ToString();
                double dToSet = value > mCamera.Gain.Max ? mCamera.Gain.Max : value;
                dToSet = dToSet < mCamera.Gain.Min ? mCamera.Gain.Min : dToSet;
                mCamera.Gain.Value = dToSet;
            }
        }
        public bool GainAuto
        {
            get
            {
                return mCamera.GainAuto.Value == GainAutoEnums.Continuous.ToString();
            }
            set
            {
                mCamera.GainAuto.Value = value ? GainAutoEnums.Continuous.ToString() : GainAutoEnums.Off.ToString();
            }
        }
        #endregion
    }
}
