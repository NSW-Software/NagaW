using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using MvCamCtrl.NET;
using System.Reflection;

using Emgu.CV;
using Emgu.CV.Structure;

namespace NagaW
{
    using Emgu.CV;
    using Emgu.CV.UI;
    using Emgu.CV.Structure;

    public class MVC_GenTL
    {
        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        public static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);

        private MyCamera m_MyCamera = new MyCamera();
        bool m_bConnected = false;
        bool m_bGrabbing = false;
        Thread m_hReceiveThread = null;
        MyCamera.MV_IMAGE_BASIC_INFO imageBasicInfo = new MyCamera.MV_IMAGE_BASIC_INFO();
        MyCamera.MV_FRAME_OUT_INFO_EX m_stFrameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();

        //Buffer for getting image from driver
        UInt32 m_nBufSizeForDriver = 0;
        IntPtr m_BufForDriver = IntPtr.Zero;
        private Object BufForDriverLock = new Object();
        private Object lockObject = new Object();

        private string m_CamName = "";
        private string m_Brand = "";
        private string m_SerialNo = "";

        //Show error message
        private string GetErrorMsg(string csMessage, int nErrorNum)
        {
            string errorMsg;
            if (nErrorNum == 0)
            {
                errorMsg = csMessage;
            }
            else
            {
                errorMsg = csMessage + ": Error =" + String.Format("{0:X}", nErrorNum);
            }

            switch (nErrorNum)
            {
                case MyCamera.MV_E_HANDLE: errorMsg += " Error or invalid handle "; break;
                case MyCamera.MV_E_SUPPORT: errorMsg += " Not supported function "; break;
                case MyCamera.MV_E_BUFOVER: errorMsg += " Cache is full "; break;
                case MyCamera.MV_E_CALLORDER: errorMsg += " Function calling order error "; break;
                case MyCamera.MV_E_PARAMETER: errorMsg += " Incorrect parameter "; break;
                case MyCamera.MV_E_RESOURCE: errorMsg += " Applying resource failed "; break;
                case MyCamera.MV_E_NODATA: errorMsg += " No data "; break;
                case MyCamera.MV_E_PRECONDITION: errorMsg += " Precondition error, or running environment changed "; break;
                case MyCamera.MV_E_VERSION: errorMsg += " Version mismatches "; break;
                case MyCamera.MV_E_NOENOUGH_BUF: errorMsg += " Insufficient memory "; break;
                case MyCamera.MV_E_UNKNOW: errorMsg += " Unknown error "; break;
                case MyCamera.MV_E_GC_GENERIC: errorMsg += " General error "; break;
                case MyCamera.MV_E_GC_ACCESS: errorMsg += " Node accessing condition error "; break;
                case MyCamera.MV_E_ACCESS_DENIED: errorMsg += " No permission "; break;
                case MyCamera.MV_E_BUSY: errorMsg += " Device is busy, or network disconnected "; break;
                case MyCamera.MV_E_NETER: errorMsg += " Network error "; break;
            }

            errorMsg = m_CamName + " " + errorMsg;

            return errorMsg;
        }

        public bool OpenDevice(string camName, string camSerialNo, string ctiFile)
        {
            //Note: Flir cti x86 and x64 file error loading in mix environment. 
            m_bConnected = false;

            MyCamera.MV_GENTL_IF_INFO_LIST m_stIFInfoList = new MyCamera.MV_GENTL_IF_INFO_LIST();
            MyCamera.MV_GENTL_IF_INFO stIFInfo = new MyCamera.MV_GENTL_IF_INFO();
            #region Enumeration Interface
            int nRet = MyCamera.MV_CC_EnumInterfacesByGenTL_NET(ref m_stIFInfoList, ctiFile);
            if (nRet != 0)
            {
                throw new Exception(GetErrorMsg("Enumerated interfaces fail!", nRet));
            }

            MyCamera.MV_GENTL_DEV_INFO_LIST m_stDeviceList = new MyCamera.MV_GENTL_DEV_INFO_LIST();
            MyCamera.MV_GENTL_DEV_INFO m_stDevice = new MyCamera.MV_GENTL_DEV_INFO();
            for (UInt32 i = 0; i < m_stIFInfoList.nInterfaceNum; i++)
            {
                stIFInfo = (MyCamera.MV_GENTL_IF_INFO)Marshal.PtrToStructure(m_stIFInfoList.pIFInfo[i], typeof(MyCamera.MV_GENTL_IF_INFO));

                #region Enumeration Device
                nRet = MyCamera.MV_CC_EnumDevicesByGenTL_NET(ref stIFInfo, ref m_stDeviceList);
                if (0 != nRet)
                {
                    throw new Exception(GetErrorMsg("Enumerate devices fail!", nRet));
                }

                bool found = false;
                for (int j = 0; j < m_stDeviceList.nDeviceNum; j++)
                {
                    m_stDevice = (MyCamera.MV_GENTL_DEV_INFO)Marshal.PtrToStructure(m_stDeviceList.pDeviceInfo[j], typeof(MyCamera.MV_GENTL_DEV_INFO));
                    if (m_stDevice.chDeviceID.Contains(camSerialNo))
                    {
                        found = true;
                        m_CamName = camName;
                        m_Brand = m_stDevice.chVendorName;
                        m_SerialNo = m_stDevice.chSerialNumber;
                        break;
                    }
                    if (j == m_stDeviceList.nDeviceNum - 1) throw new Exception("Device Not Found");
                }
                #endregion

                if (found) break;
                if (i == m_stIFInfoList.nInterfaceNum - 1) throw new Exception("Interface Not Found");
            }
            #endregion

            //Create device
            nRet = m_MyCamera.MV_CC_CreateDeviceByGenTL_NET(ref m_stDevice);
            if (MyCamera.MV_OK != nRet)
            {
                throw new Exception($"Create device failed {nRet:x8}");
            }
            //NDispWin.Event.CAMERA_INFO.Set($"Camera Connected", $"{m_CamName} {m_stDevice.chVendorName},{m_stDevice.chModelName},{m_stDevice.chSerialNumber},{m_stDevice.chDeviceVersion}");

            //Open device
            nRet = m_MyCamera.MV_CC_OpenDevice_NET();
            if (MyCamera.MV_OK != nRet)
            {
                m_MyCamera.MV_CC_DestroyDevice_NET();
                throw new Exception(GetErrorMsg("Device open fail!", nRet));
            }
            m_MyCamera.MV_CC_GetImageInfo_NET(ref imageBasicInfo);

            m_bConnected = true;

            if (m_Brand.Contains("Hikrobot"))
            {
                nRet = m_MyCamera.MV_CC_SetIntValue_NET("LineDebouncerTime", 4);
                if (nRet != MyCamera.MV_OK)
                {
                    throw new Exception(GetErrorMsg("Set LineDebouncerTime failed!", nRet));
                }
            }

            //Set Continues Aquisition Mode
            AcquisitionMode = MyCamera.MV_CAM_ACQUISITION_MODE.MV_ACQ_MODE_CONTINUOUS;

            //Set Trigger Mode Off
            TriggerMode = false;

            return true;
        }
        public bool OpenDevice(string camName, string camIpAddress)
        {
            m_bConnected = false;

            MyCamera.MV_CC_DEVICE_INFO stDevInfo = new MyCamera.MV_CC_DEVICE_INFO();
            MyCamera.MV_GIGE_DEVICE_INFO stGigEDev = new MyCamera.MV_GIGE_DEVICE_INFO();//Device Information

            stDevInfo.nTLayerType = MyCamera.MV_GIGE_DEVICE;

            MyCamera.MV_CC_DEVICE_INFO_LIST m_stDeviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();

            m_stDeviceList.nDeviceNum = 0;
            int nRet = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE, ref m_stDeviceList);
            if (0 != nRet)
            {
                throw new Exception(GetErrorMsg("Enumerate devices fail!", nRet));
            }

            uint u_camIp = 0;
            try
            {
                var parts = camIpAddress.Split('.');
                int nIp1 = Convert.ToInt32(parts[0]);
                int nIp2 = Convert.ToInt32(parts[1]);
                int nIp3 = Convert.ToInt32(parts[2]);
                int nIp4 = Convert.ToInt32(parts[3]);
                u_camIp = (uint)((nIp1 << 24) | (nIp2 << 16) | (nIp3 << 8) | nIp4);
            }
            catch
            {
                throw new Exception("Invalid NIC IPAddress.");
            }

            for (int i = 0; i < m_stDeviceList.nDeviceNum; i++)
            {
                stDevInfo = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_stDeviceList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                stGigEDev = (MyCamera.MV_GIGE_DEVICE_INFO)MyCamera.ByteToStruct(stDevInfo.SpecialInfo.stGigEInfo, typeof(MyCamera.MV_GIGE_DEVICE_INFO));

                if (stGigEDev.nCurrentIp == u_camIp)
                {
                    m_CamName = camName;
                    m_Brand = stGigEDev.chManufacturerName;
                    m_SerialNo = stGigEDev.chSerialNumber;
                    break;
                }
                if (i == m_stDeviceList.nDeviceNum - 1) throw new Exception("Device Not Found");
            }

            //Create device
            nRet = m_MyCamera.MV_CC_CreateDevice_NET(ref stDevInfo);
            if (MyCamera.MV_OK != nRet)
            {
                throw new Exception($"Create device failed {nRet:x8}");
            }
            //NDispWin.Event.CAMERA_INFO.Set($"Camera Connected", $"{m_CamName} {stGigEDev.chManufacturerName},{stGigEDev.chModelName},{stGigEDev.chSerialNumber},{stGigEDev.chDeviceVersion},NicIP {stGigEDev.nNetExport}, CamIP {stGigEDev.nCurrentIp}");

            //Open device
            nRet = m_MyCamera.MV_CC_OpenDevice_NET();
            if (MyCamera.MV_OK != nRet)
            {
                m_MyCamera.MV_CC_DestroyDevice_NET();
                throw new Exception(GetErrorMsg("Device open fail!", nRet));
            }
            m_MyCamera.MV_CC_GetImageInfo_NET(ref imageBasicInfo);

            m_bConnected = true;

            if (m_Brand.Contains("Hikrobot"))
            {
                nRet = m_MyCamera.MV_CC_SetIntValue_NET("LineDebouncerTime", 4);
                if (nRet != MyCamera.MV_OK)
                {
                    throw new Exception(GetErrorMsg("Set LineDebouncerTime failed!", nRet));
                }
            }

            //Set Continues Aquisition Mode
            AcquisitionMode = MyCamera.MV_CAM_ACQUISITION_MODE.MV_ACQ_MODE_CONTINUOUS;

            //Set Trigger Mode Off
            TriggerMode = false;

            return true;
        }
        public void CloseDevice()
        {
            //Reset flow flag bit
            m_bGrabbing = false;
            if (m_hReceiveThread != null) m_hReceiveThread.Join();

            if (m_BufForDriver != IntPtr.Zero)
            {
                Marshal.Release(m_BufForDriver);
            }

            //Close Device
            m_MyCamera.MV_CC_CloseDevice_NET();
            m_MyCamera.MV_CC_DestroyDevice_NET();
            m_bConnected = false;
            //NDispWin.Event.CAMERA_INFO.Set($"Camera Disconnected", $"{m_CamName} {m_Brand},{m_SerialNo}");
        }
        public bool IsConnected => m_bConnected;

        MyCamera.MV_DISPLAY_FRAME_INFO stDisplayInfo = new MyCamera.MV_DISPLAY_FRAME_INFO();
        MyCamera.MV_FRAME_OUT stFrameInfo = new MyCamera.MV_FRAME_OUT();

        MyCamera.MV_CC_INPUT_FRAME_INFO stInputFrameInfo = new MyCamera.MV_CC_INPUT_FRAME_INFO();

        public void ReceiveThreadProcess()
        {
            int nRet = MyCamera.MV_OK;

            while (m_bGrabbing)
            {
                lock (lockObject)
                {
                    nRet = m_MyCamera.MV_CC_GetImageBuffer_NET(ref stFrameInfo, 1000);
                    if (nRet == MyCamera.MV_OK)
                    {
                        lock (BufForDriverLock)
                        {
                            if (m_BufForDriver == IntPtr.Zero || stFrameInfo.stFrameInfo.nFrameLen > m_nBufSizeForDriver)
                            {
                                if (m_BufForDriver != IntPtr.Zero)
                                {
                                    Marshal.Release(m_BufForDriver);
                                    m_BufForDriver = IntPtr.Zero;
                                }

                                m_BufForDriver = Marshal.AllocHGlobal((Int32)stFrameInfo.stFrameInfo.nFrameLen);
                                if (m_BufForDriver == IntPtr.Zero)
                                {
                                    return;
                                }
                                m_nBufSizeForDriver = stFrameInfo.stFrameInfo.nFrameLen;
                            }

                            m_stFrameInfo = stFrameInfo.stFrameInfo;
                            CopyMemory(m_BufForDriver, stFrameInfo.pBufAddr, stFrameInfo.stFrameInfo.nFrameLen);
                        }

                        if (m_bRecording)
                        {
                            stInputFrameInfo.pData = stFrameInfo.pBufAddr;
                            stInputFrameInfo.nDataLen = stFrameInfo.stFrameInfo.nFrameLen;
                            nRet = m_MyCamera.MV_CC_InputOneFrame_NET(ref stInputFrameInfo);
                            if (MyCamera.MV_OK != nRet)
                            {
                                //NDispWin.Event.CAMERA_INFO.Set(MethodBase.GetCurrentMethod().Name.ToString(), GetErrorMsg("Input one frame failed.!", nRet));
                            }
                        }

                        stDisplayInfo.pData = stFrameInfo.pBufAddr;
                        stDisplayInfo.nDataLen = stFrameInfo.stFrameInfo.nFrameLen;
                        stDisplayInfo.nWidth = stFrameInfo.stFrameInfo.nWidth;
                        stDisplayInfo.nHeight = stFrameInfo.stFrameInfo.nHeight;
                        stDisplayInfo.enPixelType = stFrameInfo.stFrameInfo.enPixelType;

                        if (stDisplayInfo.hWnd != IntPtr.Zero)
                        {
                            m_MyCamera.MV_CC_DisplayOneFrame_NET(ref stDisplayInfo);
                        }
                        else
                        {
                            int i = 0;
                            try
                            {
                                int stride = stDisplayInfo.nWidth + (stDisplayInfo.nWidth % 4);
                                mImage = new Image<Gray, byte>(stDisplayInfo.nWidth, stDisplayInfo.nHeight, stride, stDisplayInfo.pData);
                                m_emguBox.Image = mImage;
                                m_emguBox.Invalidate();
                            }
                            catch (Exception ex)
                            {
                                //NDispWin.Event.CAMERA_INFO.Set(MethodBase.GetCurrentMethod().Name.ToString(), m_CamName + " " + ex.Message.ToString());
                                //NDispWin.Event.CAMERA_INFO.Set($"Data", $"{i},{stDisplayInfo.nWidth},{stDisplayInfo.nHeight},{stDisplayInfo.pData}");
                                m_MyCamera.MV_CC_FreeImageBuffer_NET(ref stFrameInfo);
                                System.Threading.Thread.Sleep(10);
                            }
                        }
                        m_MyCamera.MV_CC_FreeImageBuffer_NET(ref stFrameInfo);
                    }
                    else
                    {
                        if (m_bGrabbing)
                        {

                        }
                            //NDispWin.Event.CAMERA_INFO.Set(MethodBase.GetCurrentMethod().Name.ToString(), GetErrorMsg("Get Image Buffer Fail.!", nRet));
                    }
                }
            }
        }
        public void ReceiveProcess()
        {
            int nRet = MyCamera.MV_OK;

            lock (lockObject)
            {
                nRet = m_MyCamera.MV_CC_GetImageBuffer_NET(ref stFrameInfo, 1000);
                if (nRet == MyCamera.MV_OK)
                {
                    stDisplayInfo.pData = stFrameInfo.pBufAddr;
                    stDisplayInfo.nDataLen = stFrameInfo.stFrameInfo.nFrameLen;
                    stDisplayInfo.nWidth = stFrameInfo.stFrameInfo.nWidth;
                    stDisplayInfo.nHeight = stFrameInfo.stFrameInfo.nHeight;
                    stDisplayInfo.enPixelType = stFrameInfo.stFrameInfo.enPixelType;

                    if (stDisplayInfo.hWnd != IntPtr.Zero)
                    {
                        m_MyCamera.MV_CC_DisplayOneFrame_NET(ref stDisplayInfo);
                    }
                    else
                    {
                        int i = 0;
                        try
                        {
                            int stride = stDisplayInfo.nWidth + (stDisplayInfo.nWidth % 4);
                            i++;
                            mImage = new Image<Gray, byte>(stDisplayInfo.nWidth, stDisplayInfo.nHeight, stride, stDisplayInfo.pData);
                            i++;
                            m_emguBox.Image = mImage;
                            i++;
                            m_emguBox.Invalidate();
                            i++;
                        }
                        catch (Exception ex)
                        {
                            //NDispWin.Event.CAMERA_INFO.Set(MethodBase.GetCurrentMethod().Name.ToString(), m_CamName + " " + ex.Message.ToString());
                            //NDispWin.Event.CAMERA_INFO.Set($"Data", $"{i},{stDisplayInfo.nWidth},{stDisplayInfo.nHeight},{stDisplayInfo.pData}");
                            m_MyCamera.MV_CC_FreeImageBuffer_NET(ref stFrameInfo);
                            System.Threading.Thread.Sleep(10);
                        }
                    }
                    m_MyCamera.MV_CC_FreeImageBuffer_NET(ref stFrameInfo);
                }
                else
                {
                    if (m_bGrabbing)
                    {

                    }
                        //NDispWin.Event.CAMERA_INFO.Set(MethodBase.GetCurrentMethod().Name.ToString(), GetErrorMsg("Get Image Buffer Fail.!", nRet));
                }
            }
        }

        public bool StartGrab()
        {
            if (!m_bConnected) return false;
            if (m_bGrabbing) return true;

            //return false;
            //Set position bit true
            m_bGrabbing = true;

            m_hReceiveThread = new Thread(ReceiveThreadProcess);
            m_hReceiveThread.Start();

            //Initialize Frame Info
            m_stFrameInfo.nFrameLen = 0;
            m_stFrameInfo.enPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_Undefined;
            //Start Grabbing
            int nRet = m_MyCamera.MV_CC_StartGrabbing_NET();
            if (MyCamera.MV_OK != nRet)
            {
                m_bGrabbing = false;
                m_hReceiveThread.Join();
                throw new Exception(GetErrorMsg("Start Grabbing Fail!", nRet));
            }
            //m_hReceiveThread = new Thread(ReceiveThreadProcess);
            //m_hReceiveThread.Start();

            return true;
        }
        public bool StopGrab()
        {
            if (!m_bGrabbing) return true;

            //Set flag bit false
            m_bGrabbing = false;
            if (m_hReceiveThread != null) m_hReceiveThread.Abort();

            //Stop Grabbing
            int nRet = m_MyCamera.MV_CC_StopGrabbing_NET();
            if (nRet != MyCamera.MV_OK)
            {
                throw new Exception(GetErrorMsg("Stop Grabbing Fail!", nRet));
            }
            return true;
        }
        public bool IsGrabbing => m_bGrabbing;
        public Image<Gray, byte> mImage = new Image<Gray, byte>(808, 606);
        public void GrabOneImage()
        {
            bool grabbing = m_bGrabbing;

            if (grabbing) StopGrab();

            m_MyCamera.MV_CC_FreeImageBuffer_NET(ref stFrameInfo);

            int nRet = MyCamera.MV_OK;
            nRet = m_MyCamera.MV_CC_StartGrabbing_NET();
            if (MyCamera.MV_OK != nRet)
            {
                m_bGrabbing = false;
                throw new Exception(GetErrorMsg("Start Grabbing Fail!", nRet));
            }
            ReceiveProcess();

            int stride = stDisplayInfo.nWidth + (stDisplayInfo.nWidth % 4);
            mImage = new Image<Gray, byte>(stDisplayInfo.nWidth, stDisplayInfo.nHeight, stride, stDisplayInfo.pData);

            nRet = m_MyCamera.MV_CC_StopGrabbing_NET();
            if (nRet != MyCamera.MV_OK)
            {
                throw new Exception(GetErrorMsg("Stop Grabbing Fail!", nRet));
            }
        }
        public void StartGrab2()
        {
            if (m_bGrabbing) StopGrab();

            m_bGrabbing = true;
            m_MyCamera.MV_CC_FreeImageBuffer_NET(ref stFrameInfo);

            int nRet = MyCamera.MV_OK;
            nRet = m_MyCamera.MV_CC_StartGrabbing_NET();
            if (MyCamera.MV_OK != nRet)
            {
                m_bGrabbing = false;
                throw new Exception(GetErrorMsg("Start Grabbing Fail!", nRet));
            }
            Thread.Sleep(100);
        }
        public void SaveBuffer(string fileName)
        {
            MyCamera.MV_SAVE_IMG_TO_FILE_PARAM stSaveFileParam = new MyCamera.MV_SAVE_IMG_TO_FILE_PARAM();

            lock (BufForDriverLock)
            {
                if (m_stFrameInfo.nFrameLen == 0)
                {
                    throw new Exception(MethodBase.GetCurrentMethod().Name.ToString() + " " + m_CamName + " Invalid Frame Length.");
                }
                stSaveFileParam.enImageType = MyCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Bmp;// MV_Image_Jpeg;
                stSaveFileParam.enPixelType = m_stFrameInfo.enPixelType;
                stSaveFileParam.pData = m_BufForDriver;
                stSaveFileParam.nDataLen = m_stFrameInfo.nFrameLen;
                stSaveFileParam.nHeight = m_stFrameInfo.nHeight;
                stSaveFileParam.nWidth = m_stFrameInfo.nWidth;
                stSaveFileParam.nQuality = 100;
                stSaveFileParam.iMethodValue = 0;
                stSaveFileParam.pImagePath = fileName;
                int nRet = m_MyCamera.MV_CC_SaveImageToFile_NET(ref stSaveFileParam);
                if (MyCamera.MV_OK != nRet)
                {
                    throw new Exception(MethodBase.GetCurrentMethod().Name.ToString() + " " + GetErrorMsg("Save fail!", nRet));
                }
            }
        }

        bool m_bRecording = false;
        public bool StartRecord(string fileName)
        {
            if (!m_bConnected) return false;
            if (!m_bGrabbing)
            {
                StartGrab();
                return false;
            }

            int nRet = MyCamera.MV_OK;
            MyCamera.MV_CC_RECORD_PARAM stRecordPar = new MyCamera.MV_CC_RECORD_PARAM();

            MyCamera.MVCC_INTVALUE stParam = new MyCamera.MVCC_INTVALUE();
            nRet = m_MyCamera.MV_CC_GetIntValue_NET("Width", ref stParam);
            if (MyCamera.MV_OK != nRet)
            {
                throw new Exception(GetErrorMsg("Get Width failed!", nRet));
            }
            stRecordPar.nWidth = (ushort)stParam.nCurValue;

            nRet = m_MyCamera.MV_CC_GetIntValue_NET("Height", ref stParam);
            if (MyCamera.MV_OK != nRet)
            {
                throw new Exception(GetErrorMsg("Get Height failed!", nRet));
            }
            stRecordPar.nHeight = (ushort)stParam.nCurValue;

            MyCamera.MVCC_ENUMVALUE stEnumValue = new MyCamera.MVCC_ENUMVALUE();
            nRet = m_MyCamera.MV_CC_GetEnumValue_NET("PixelFormat", ref stEnumValue);
            if (MyCamera.MV_OK != nRet)
            {
                throw new Exception(GetErrorMsg("Get PixelFormat failed!", nRet));
            }
            stRecordPar.enPixelType = (MyCamera.MvGvspPixelType)stEnumValue.nCurValue;

            MyCamera.MVCC_FLOATVALUE stFloatValue = new MyCamera.MVCC_FLOATVALUE();


            if (m_Brand.Contains("FLIR"))
            {
                nRet = m_MyCamera.MV_CC_GetFloatValue_NET("AcquisitionResultingFrameRate", ref stFloatValue);
                if (MyCamera.MV_OK != nRet)
                {
                    throw new Exception(GetErrorMsg("Get AcquisitionResultingFrameRate failed!", nRet));
                }
            }
            else
            {
                nRet = m_MyCamera.MV_CC_GetFloatValue_NET("ResultingFrameRate", ref stFloatValue);
                if (MyCamera.MV_OK != nRet)
                {
                    throw new Exception(GetErrorMsg("Get ResultingFrameRate failed!", nRet));
                }
            }
            stRecordPar.fFrameRate = stFloatValue.fCurValue;
            stRecordPar.nBitRate = 1000;

            // ch:录像格式(仅支持AVI) | en:Record Format(AVI is only supported)
            stRecordPar.enRecordFmtType = MyCamera.MV_RECORD_FORMAT_TYPE.MV_FormatType_AVI;
            stRecordPar.strFilePath = fileName;
            nRet = m_MyCamera.MV_CC_StartRecord_NET(ref stRecordPar);
            if (MyCamera.MV_OK != nRet)
            {
                throw new Exception(GetErrorMsg("Start Record failed!", nRet));
            }
            m_bRecording = true;

            return true;
        }
        public bool StopRecord()
        {
            int nRet = MyCamera.MV_OK;

            m_bRecording = false;

            // ch:停止录像 | en:Stop record
            nRet = m_MyCamera.MV_CC_StopRecord_NET();
            if (MyCamera.MV_OK != nRet)
            {
                throw new Exception(GetErrorMsg("Stop Record failed!", nRet));
            }

            return true;
        }
        public bool IsRecording => m_bRecording;

        //Register picturebox for display - mehtod 1: manual rendering
        ImageBox m_emguBox = new ImageBox();
        public void RegisterPictureBox(ImageBox pictureBox)
        {
            lock (lockObject)
            {
                m_emguBox = pictureBox;
            }
        }
        //Register PictureBox for display - Mehtod 2: Handled by API. Image is always strectched.
        public void RegisterPictureBoxHandle(PictureBox picBox)
        {
            stDisplayInfo.hWnd = picBox.Handle;
        }

        public MyCamera.MV_CAM_ACQUISITION_MODE AcquisitionMode
        {
            /*
             * MV public enum MV_CAM_ACQUISITION_MODE {MV_ACQ_MODE_SINGLE = 0, MV_ACQ_MODE_MUTLI = 1, MV_ACQ_MODE_CONTINUOUS = 2}
             * Flir public enum AcquisitionModeEnums {Continuous = 0, SingleFrame = 1, MultiFrame = 2, NUM_ACQUISITIONMODE = 3}
             */
            get
            {
                //Get Aquisition Mode
                MyCamera.MVCC_ENUMVALUE eValue = new MyCamera.MVCC_ENUMVALUE();
                int nRet = m_MyCamera.MV_CC_GetEnumValue_NET("AcquisitionMode", ref eValue);
                if (MyCamera.MV_OK != nRet)
                {
                    throw new Exception(GetErrorMsg("Get " + MethodBase.GetCurrentMethod().Name.ToString(), nRet));
                }

                switch (m_Brand)// Dev device.chVendorName)
                {
                    case "FLIR":
                    case "Point Grey Research":
                        switch (eValue.nCurValue)
                        {
                            case 0:
                                return MyCamera.MV_CAM_ACQUISITION_MODE.MV_ACQ_MODE_CONTINUOUS;
                            case 1:
                                return MyCamera.MV_CAM_ACQUISITION_MODE.MV_ACQ_MODE_SINGLE;
                            case 2:
                                return MyCamera.MV_CAM_ACQUISITION_MODE.MV_ACQ_MODE_MUTLI;
                            default:
                                throw new Exception(MethodBase.GetCurrentMethod().Name.ToString() + " Unrecognized AcquisitionMode.");
                        }
                    default:
                        return (MyCamera.MV_CAM_ACQUISITION_MODE)eValue.nCurValue;
                }
            }
            set
            {
                //Set Aquisition Mode
                bool grabbing = m_bGrabbing;
                if (grabbing) StopGrab();

                uint mode = (uint)value;
                switch (m_Brand)//(device.chVendorName)
                {
                    case "FLIR":
                    case "Point Grey Research":
                        switch (value)
                        {
                            case MyCamera.MV_CAM_ACQUISITION_MODE.MV_ACQ_MODE_CONTINUOUS: mode = 0; break;
                            case MyCamera.MV_CAM_ACQUISITION_MODE.MV_ACQ_MODE_SINGLE: mode = 1; break;
                            case MyCamera.MV_CAM_ACQUISITION_MODE.MV_ACQ_MODE_MUTLI: mode = 2; break;
                        }
                        break;
                }

                int nRet = m_MyCamera.MV_CC_SetEnumValue_NET("AcquisitionMode", mode);
                if (MyCamera.MV_OK != nRet)
                {
                    throw new Exception(GetErrorMsg("Set " + MethodBase.GetCurrentMethod().Name.ToString(), nRet));
                }
                if (grabbing) StartGrab();
            }
        }
        public bool ExposureAuto
        {
            /*
             * MV public enum MV_CAM_EXPOSURE_AUTO_MODE {MV_EXPOSURE_AUTO_MODE_OFF = 0, MV_EXPOSURE_AUTO_MODE_ONCE = 1, MV_EXPOSURE_AUTO_MODE_CONTINUOUS = 2}
             * FLIR public enum ExposureAutoEnums {Off = 0, Once = 1, Continuous = 2, NUM_EXPOSUREAUTO = 3}
             */
            get
            {
                MyCamera.MVCC_ENUMVALUE stEnumValue = new MyCamera.MVCC_ENUMVALUE();
                int nRet = m_MyCamera.MV_CC_GetEnumValue_NET("ExposureAuto", ref stEnumValue);
                if (nRet != MyCamera.MV_OK)
                {
                    throw new Exception(GetErrorMsg("Get " + MethodBase.GetCurrentMethod().Name.ToString(), nRet));
                }
                return false;//***need to get enum value
            }
            set
            {
                int nRet = m_MyCamera.MV_CC_SetEnumValue_NET("ExposureAuto", value ? (uint)MyCamera.MV_CAM_EXPOSURE_AUTO_MODE.MV_EXPOSURE_AUTO_MODE_CONTINUOUS : (uint)MyCamera.MV_CAM_EXPOSURE_AUTO_MODE.MV_EXPOSURE_AUTO_MODE_OFF);
                if (MyCamera.MV_OK != nRet)
                {
                    throw new Exception(GetErrorMsg("Set " + MethodBase.GetCurrentMethod().Name.ToString(), nRet));
                }
            }
        }
        public double Exposure//ns
        {
            get
            {
                MyCamera.MVCC_FLOATVALUE stParam = new MyCamera.MVCC_FLOATVALUE();
                int nRet = m_MyCamera.MV_CC_GetFloatValue_NET("ExposureTime", ref stParam);
                if (nRet != MyCamera.MV_OK)
                {
                    throw new Exception(GetErrorMsg("Get " + MethodBase.GetCurrentMethod().Name.ToString(), nRet));
                }
                return stParam.fCurValue;
            }
            set
            {
                ExposureAuto = false;
                int nRet = m_MyCamera.MV_CC_SetFloatValue_NET("ExposureTime", (float)value);
                if (nRet != MyCamera.MV_OK)
                {
                    throw new Exception(GetErrorMsg("Set " + MethodBase.GetCurrentMethod().Name.ToString(), nRet));
                }
            }
        }
        public bool GainAuto
        {
            /*
             * MV public enum MV_CAM_GAIN_MODE {MV_GAIN_MODE_OFF = 0, MV_GAIN_MODE_ONCE = 1, MV_GAIN_MODE_CONTINUOUS = 2}
             * FLIR public enum GainAutoEnums {Off = 0, Once = 1, Continuous = 2, NUM_GAINAUTO = 3}
            */
            get
            {
                MyCamera.MVCC_ENUMVALUE stEnumValue = new MyCamera.MVCC_ENUMVALUE();
                int nRet = m_MyCamera.MV_CC_GetEnumValue_NET("ExposureAuto", ref stEnumValue);
                if (nRet != MyCamera.MV_OK)
                {
                    throw new Exception(GetErrorMsg("Get " + MethodBase.GetCurrentMethod().Name.ToString(), nRet));
                }
                return (stEnumValue.nCurValue == (uint)MyCamera.MV_CAM_EXPOSURE_AUTO_MODE.MV_EXPOSURE_AUTO_MODE_CONTINUOUS);
            }
            set
            {
                int nRet = m_MyCamera.MV_CC_SetEnumValue_NET("GainAuto", value ? (uint)MyCamera.MV_CAM_GAIN_MODE.MV_GAIN_MODE_CONTINUOUS : (uint)MyCamera.MV_CAM_GAIN_MODE.MV_GAIN_MODE_OFF);
                if (nRet != MyCamera.MV_OK)
                {
                    throw new Exception(GetErrorMsg("Set " + MethodBase.GetCurrentMethod().Name.ToString(), nRet));
                }
            }
        }
        public double Gain
        {
            get
            {
                MyCamera.MVCC_FLOATVALUE stParam = new MyCamera.MVCC_FLOATVALUE();
                int nRet = m_MyCamera.MV_CC_GetFloatValue_NET("Gain", ref stParam);
                if (nRet != MyCamera.MV_OK)
                {
                    throw new Exception(GetErrorMsg("Get " + MethodBase.GetCurrentMethod().Name.ToString(), nRet));
                }
                return stParam.fCurValue;
            }
            set
            {
                GainAuto = false;
                int nRet = m_MyCamera.MV_CC_SetFloatValue_NET("Gain", (float)value);
                if (nRet != MyCamera.MV_OK)
                {
                    throw new Exception(GetErrorMsg("Set " + MethodBase.GetCurrentMethod().Name.ToString(), nRet));
                }
            }
        }

        public bool TriggerMode
        {
            /*
             * MV public enum MV_CAM_TRIGGER_MODE {MV_TRIGGER_MODE_OFF = 0, MV_TRIGGER_MODE_ON = 1}
             * FLIR public enum TriggerModeEnums {Off = 0, On = 1, NUM_TRIGGERMODE = 2}
             */
            get
            {
                MyCamera.MVCC_ENUMVALUE stEnumValue = new MyCamera.MVCC_ENUMVALUE();
                int nRet = m_MyCamera.MV_CC_GetEnumValue_NET("TriggerMode", ref stEnumValue);
                if (nRet != MyCamera.MV_OK)
                {
                    throw new Exception(GetErrorMsg("Get " + MethodBase.GetCurrentMethod().Name.ToString(), nRet));
                }
                return stEnumValue.nCurValue == (uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_ON;
            }
            set
            {
                int nRet = m_MyCamera.MV_CC_SetEnumValue_NET("TriggerMode", value ? (uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_ON : (uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_OFF);
                if (nRet != MyCamera.MV_OK)
                {
                    throw new Exception(GetErrorMsg("Set " + MethodBase.GetCurrentMethod().Name.ToString(), nRet));
                }
            }
        }
        enum ETriggerSource { Line0, Software }
        public bool TriggerSourceHw
        {
            /*
             * MV public enum MV_CAM_TRIGGER_SOURCE
             *   {MV_TRIGGER_SOURCE_LINE0 = 0, MV_TRIGGER_SOURCE_LINE1 = 1, MV_TRIGGER_SOURCE_LINE2 = 2, MV_TRIGGER_SOURCE_LINE3 = 3,
             *   MV_TRIGGER_SOURCE_COUNTER0 = 4, MV_TRIGGER_SOURCE_SOFTWARE = 7, MV_TRIGGER_SOURCE_FrequencyConverter = 8}
             * FLIR public enum TriggerSourceEnums
             *    {Software = 0, Line0 = 1, Line1 = 2, Line2 = 3, Line3 = 4,
             *    UserOutput0 = 5, UserOutput1 = 6, UserOutput2 = 7, UserOutput3 = 8, Counter0Start = 9, Counter1Start = 10,
             *    Counter0End = 11, Counter1End = 12, LogicBlock0 = 13, LogicBlock1 = 14, Action0 = 15, NUM_TRIGGERSOURCE = 16}
             */
            get
            {
                MyCamera.MVCC_ENUMVALUE stEnumValue = new MyCamera.MVCC_ENUMVALUE();
                int nRet = m_MyCamera.MV_CC_GetEnumValue_NET("TriggerSource", ref stEnumValue);
                if (nRet != MyCamera.MV_OK)
                {
                    throw new Exception(GetErrorMsg("Get " + MethodBase.GetCurrentMethod().Name.ToString(), nRet));
                }
                return (stEnumValue.nCurValue == (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE0);
            }
            set
            {
                int nRet = m_MyCamera.MV_CC_SetEnumValue_NET("TriggerSource", value ? (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE0 : (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_SOFTWARE);
                if (nRet != MyCamera.MV_OK)
                {
                    throw new Exception(GetErrorMsg("Set " + MethodBase.GetCurrentMethod().Name.ToString(), nRet));
                }
            }
        }
        public bool SoftwareTrigger()
        {
            //Software Trigger for Flir is not working
            int nRet = m_MyCamera.MV_CC_SetCommandValue_NET("TriggerSoftware");
            if (nRet != MyCamera.MV_OK)
            {
                throw new Exception(GetErrorMsg(MethodBase.GetCurrentMethod().Name.ToString(), nRet));
            }
            return true;
        }

        public uint ImageWidthMax
        {
            get
            {
                return imageBasicInfo.nWidthMax;
            }
        }
        public uint ImageWidth
        {
            get
            {
                MyCamera.MVCC_INTVALUE_EX stParam = new MyCamera.MVCC_INTVALUE_EX();
                int nRet = m_MyCamera.MV_CC_GetIntValueEx_NET("Width", ref stParam);
                if (nRet != MyCamera.MV_OK)
                {
                    throw new Exception(GetErrorMsg("Get " + MethodBase.GetCurrentMethod().Name.ToString(), nRet));
                }
                return (uint)stParam.nCurValue;
            }
            set
            {
                bool grabbing = m_bGrabbing;
                if (grabbing) StopGrab();

                value = (uint)((double)value / imageBasicInfo.nWidthInc) * imageBasicInfo.nWidthInc;
                value = Math.Min(value, imageBasicInfo.nWidthMax);
                value = Math.Max(value, 0);

                int nRet = m_MyCamera.MV_CC_SetIntValueEx_NET("Width", value);
                if (nRet != MyCamera.MV_OK)
                {
                    throw new Exception(GetErrorMsg("Set " + MethodBase.GetCurrentMethod().Name.ToString(), nRet));
                }

                if (grabbing) StartGrab();
            }
        }
        public uint ImageHeightMax
        {
            get
            {
                return imageBasicInfo.nHeightMax;
            }
        }
        public uint ImageWidthMin
        {
            get { return imageBasicInfo.nWidthMin; }
        }
        public uint ImageHeightMin
        {
            get { return imageBasicInfo.nHeightMin; }
        }
        public uint ImageHeight
        {
            get
            {
                MyCamera.MVCC_INTVALUE_EX stParam = new MyCamera.MVCC_INTVALUE_EX();
                int nRet = m_MyCamera.MV_CC_GetIntValueEx_NET("Height", ref stParam);
                if (nRet != MyCamera.MV_OK)
                {
                    throw new Exception(GetErrorMsg("Get " + MethodBase.GetCurrentMethod().Name.ToString(), nRet));
                }
                return (uint)stParam.nCurValue;
            }
            set
            {
                bool grabbing = m_bGrabbing;
                if (grabbing) StopGrab();

                value = (uint)((double)value / imageBasicInfo.nHeightInc) * imageBasicInfo.nHeightInc;
                value = Math.Min(value, imageBasicInfo.nHeightMax);
                value = Math.Max(value, 0);

                int nRet = m_MyCamera.MV_CC_SetIntValueEx_NET("Height", value);
                if (nRet != MyCamera.MV_OK)
                {
                    throw new Exception(GetErrorMsg("Set " + MethodBase.GetCurrentMethod().Name.ToString(), nRet));
                }

                if (grabbing) StartGrab();
            }
        }

        public uint OffsetX
        {
            get
            {
                return stFrameInfo.stFrameInfo.nOffsetX;
            }
            set
            {
                value = (uint)((double)value / imageBasicInfo.nWidthInc) * imageBasicInfo.nWidthInc;
                value = Math.Min(value, imageBasicInfo.nWidthMax);
                value = Math.Max(value, 0);

                int nRet = m_MyCamera.MV_CC_SetIntValueEx_NET("OffsetX", value);
                if (nRet != MyCamera.MV_OK)
                {
                    throw new Exception(GetErrorMsg("Set " + MethodBase.GetCurrentMethod().Name.ToString(), nRet));
                }
            }
        }
        public uint OffsetY
        {
            get
            {
                return stFrameInfo.stFrameInfo.nOffsetY;
            }
            set
            {
                value = (uint)((double)value / imageBasicInfo.nHeightInc) * imageBasicInfo.nHeightInc;
                value = Math.Min(value, imageBasicInfo.nHeightMax);
                value = Math.Max(value, 0);

                int nRet = m_MyCamera.MV_CC_SetIntValueEx_NET("OffsetY", value);
                if (nRet != MyCamera.MV_OK)
                {
                    throw new Exception(GetErrorMsg("Set " + MethodBase.GetCurrentMethod().Name.ToString(), nRet));
                }
            }
        }

        public bool ReverseX
        {
            set
            {
                int nRet = m_MyCamera.MV_CC_SetBoolValue_NET("ReverseX", value);
                if (nRet != MyCamera.MV_OK)
                {
                    throw new Exception(GetErrorMsg("Set " + MethodBase.GetCurrentMethod().Name.ToString(), nRet));
                }
            }
        }
        public bool ReverseY
        {
            set
            {
                int nRet = m_MyCamera.MV_CC_SetBoolValue_NET("ReverseY", value);
                if (nRet != MyCamera.MV_OK)
                {
                    throw new Exception(GetErrorMsg("Set " + MethodBase.GetCurrentMethod().Name.ToString(), nRet));
                }
            }
        }
    }
}
