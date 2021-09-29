using System;
using System.Collections.Generic;
using System.Linq;

namespace NagaW
{
    using MicroEpsilon;

    public class TEMEDAQ
    {
        //  20201201KN Added support ILD1320 and ILD1420
        //  20200207KN Created. Ref NConfocal. Uses MEDAQLib.NET.dll 4.6.0.28719
        //  20201125KN Added support ILD1320/ILD1420

        public enum EType
        {
            NONE = 0,
            //Enum index follow MEDAQLib.NET.dll index 
            SENSOR_ILD1700 = 2,//ME_SENSOR.SENSOR_ILD1700 = 2
            SENSOR_IFD2451 = 30,//ME_SENSOR.SENSOR_IFD2451 = 30
            SENSOR_ILD1320 = 41,//ME_SENSOR.SENSOR_ILD1320 = 41
            SENSOR_ILD1420 = 42,//ME_SENSOR.SENSOR_ILD1320 = 42
            SENSOR_IFD2421 = 46,//ME_SENSOR.SENSOR_IFD2421 = 46
            CL3000,
        }

        public MEDAQLib Sensor = new MEDAQLib(ME_SENSOR.SENSOR_IFD2451);
        public EType Type = EType.NONE;
        public string PortName = "";
        public bool Connected = false;

        public string GetErrString()
        {
            string s = "";
            Sensor.GetError(ref s);
            return s;
        }

        public void Open(EType type, string ComPort_Address)
        {
            try
            {
                Type = type;
                PortName = ComPort_Address;
                Connected = false;

                switch (type)
                {
                    case EType.SENSOR_ILD1700: Sensor = new MEDAQLib(ME_SENSOR.SENSOR_ILD1700); break;
                    case EType.SENSOR_IFD2451: Sensor = new MEDAQLib(ME_SENSOR.SENSOR_IFD2451); break;
                    case EType.SENSOR_ILD1320: Sensor = new MEDAQLib(ME_SENSOR.SENSOR_ILD1320); break;
                    case EType.SENSOR_ILD1420: Sensor = new MEDAQLib(ME_SENSOR.SENSOR_ILD1420); break;
                    case EType.SENSOR_IFD2421: Sensor = new MEDAQLib(ME_SENSOR.SENSOR_IFD2421); break;
                    default:
                        throw new Exception("Sensor not supported.");
                }

                ERR_CODE err = new ERR_CODE();

                if (ComPort_Address.Contains("COM"))
                {
                    err = Sensor.OpenSensorRS232(ComPort_Address);
                    if (err != ERR_CODE.ERR_NOERROR)
                        throw new Exception("Open" + (char)9 + GetErrString());
                    Connected = true;
                }
                else
                if (ComPort_Address.Contains("."))
                {
                    err = Sensor.OpenSensorTCPIP(ComPort_Address);
                    if (err != ERR_CODE.ERR_NOERROR)
                        throw new Exception("Open" + (char)9 + GetErrString());
                    Connected = true;
                }
                else
                {
                    try
                    {
                        Sensor.CloseSensor();
                    }
                    catch { }
                    throw new Exception("Invalid Comport or IP Address.");
                }

                Init();
            }
            catch
            {
                throw;
            }
        }
        public void Close()
        {
            try
            {
                Sensor.CloseSensor();

                Type = EType.NONE;
                Connected = false;
            }
            catch
            {
                throw;
            }
        }

        public string GetSensorName()
        {
            try
            {
                string value = "";
                Sensor.ExecSCmdGetString("Get_Info", "SA_Sensor", ref value);
                return value;
            }
            catch { throw; }
        }
        public string GetSerialNumber()
        {

            try
            {
                string value = "";
                Sensor.ExecSCmdGetString("Get_Info", "SA_SerialNumber", ref value);
                return value;
            }
            catch
            {
                throw;
            }
        }

        //Retrieve latest value. Multiple data is sequencially appended.
        public bool Poll(ref int[] rawData, ref double[] scaledAbs, int length)
        {
            if (!Connected) return false;

            int[] iRawData = new int[length];
            double[] dScaledAbs = new double[length];
            try
            {
                ERR_CODE ErrCode = Sensor.Poll(iRawData, dScaledAbs, length);
                if (ErrCode != ERR_CODE.ERR_NOERROR)
                    throw new Exception("Poll" + (char)9 + GetErrString());

                rawData = new int[iRawData.Length];
                scaledAbs = new double[iRawData.Length];
                for (int i = 0; i < iRawData.Length; i++)
                {
                    rawData[i] = iRawData[i];
                    scaledAbs[i] = Math.Max(dScaledAbs[0], -999.0);
                }
                return true;
            }
            catch
            {
                throw;
            }
        }
        public bool Poll(ref double scaledAbs)
        {
            int[] rawData = { 0 };
            double[] arrScaledAbs = { 0 };
            bool res = Poll(ref rawData, ref arrScaledAbs, 1);
            scaledAbs = arrScaledAbs[0];
            return res;
        }

        //Retrieve buffer data count for transfer.
        public bool DataAvail(ref int dataAvail)
        {
            int data = 0;

            try
            {
                ERR_CODE ErrCode = Sensor.DataAvail(ref data);
                if (ErrCode == ERR_CODE.ERR_NO_SENSORDATA_AVAILABLE)
                {
                    data = 0;
                    return true;
                }

                if (ErrCode != ERR_CODE.ERR_NOERROR)
                    throw new Exception("DataAvail" + (char)9 + GetErrString());

                dataAvail = data;

                return true;
            }
            catch
            {
                throw;
            }
        }
        //Retrieve buffer data count for transfer.
        public bool ClearBuffer()
        {
            try
            {
                Sensor.ExecSCmd("Clear_Buffers");
                return true;
            }
            catch
            {
                throw;
            }
        }
        //Transfer data from buffer
        public bool TransferData(int[] rawData, double[] scaledAbs, ref int DataCount)
        {
            int maxData = rawData.Length;
            try
            {
                ERR_CODE ErrCode = Sensor.TransferData(rawData, scaledAbs, maxData, ref DataCount);
                if (ErrCode != ERR_CODE.ERR_NOERROR)
                {
                    throw new Exception("TransferData" + (char)9 + GetErrString());
                }

                for (int i = 0; i < scaledAbs.Length; i++)
                {
                    scaledAbs[i] = Math.Max(scaledAbs[i], -999.0);
                }

                return true;
            }
            catch
            {
                throw;
            }
        }

        public void Init()
        {
            try
            {
                switch (Type)
                {
                    default: break;
                    case EType.SENSOR_ILD1320:
                    case EType.SENSOR_ILD1420:
                        #region
                        {
                            //set default output as RS422 interface
                            Sensor.SetIntExecSCmd("Set_DataOutInterface", "SP_DataOutInterface", 1);
                            break;
                        }
                        #endregion
                }
            }
            catch { throw; }
        }

        public double SampleRate//kHz
        {
            set
            {
                try
                {
                    switch (Type)
                    {
                        case EType.SENSOR_ILD1700:
                            #region
                            {
                                // find closest to number
                                List<double> list = new List<double> { 0.3125, 0.625, 1.250, 2.500 };
                                double Closest = list.OrderBy(item => Math.Abs(value - item)).First();

                                int speedValue = 0;
                                if (Closest == 0.3125) { speedValue = 3; return; }
                                if (Closest == 0.625) { speedValue = 2; return; }
                                if (Closest == 1.250) { speedValue = 1; return; }
                                if (Closest == 2.500) { speedValue = 0; return; }

                                Sensor.SetIntExecSCmd("Set_Speed", "SP_Speed", speedValue);
                                break;
                            }
                        #endregion
                        case EType.SENSOR_IFD2451:
                            #region
                            {
                                // find closest to number
                                List<double> list = new List<double> { 0.1, 0.2, 0.3, 1.0, 2.5, 5.0, 10.0 };
                                double Closest = list.OrderBy(item => Math.Abs(value - item)).First();

                                Sensor.SetDoubleExecSCmd("Set_Samplerate", "SP_Measrate", Closest);
                                break;
                            }
                        #endregion
                        case EType.SENSOR_ILD1320:
                            #region
                            {
                                // find closest to number
                                List<double> list = new List<double> { 0.25, 0.5, 1.0, 2.0 };
                                double Closest = list.OrderBy(item => Math.Abs(value - item)).First();

                                Sensor.SetDoubleExecSCmd("Set_Samplerate", "SP_Measrate", Closest);
                                break;
                            }
                        #endregion
                        case EType.SENSOR_ILD1420:
                            #region
                            {
                                // find closest to number
                                List<double> list = new List<double> { 0.25, 0.5, 1.0, 2.0, 4.0 };
                                double Closest = list.OrderBy(item => Math.Abs(value - item)).First();

                                Sensor.SetDoubleExecSCmd("Set_Samplerate", "SP_Measrate", Closest);
                                break;
                            }
                        #endregion
                        case EType.SENSOR_IFD2421:
                            #region
                            {
                                if (value < 0.1) value = 0.1;
                                if (value > 6.5) value = 6.5;

                                Sensor.SetDoubleExecSCmd("Set_Samplerate", "SP_Measrate", value);
                                break;
                            }
                            #endregion

                    }
                }
                catch { throw; }
            }
            get
            {
                try
                {
                    switch (Type)
                    {
                        case EType.SENSOR_ILD1700:
                            {
                                int Speed = 0;
                                Sensor.ExecSCmdGetInt("Get_Speed", "SP_Speed", ref Speed);

                                switch (Speed)
                                {
                                    case 0: return 2.5;
                                    case 1: return 1.25;
                                    case 2: return 0.625;
                                    case 3: return 0.3125;
                                    default: throw new Exception("Invalid Measure Rate");
                                }
                            }
                        case EType.SENSOR_IFD2451:
                        case EType.SENSOR_ILD1320:
                        case EType.SENSOR_ILD1420:
                        case EType.SENSOR_IFD2421:
                            try
                            {
                                double d = 0;
                                Sensor.ExecSCmdGetDouble("Get_Samplerate", "SA_Measrate", ref d);
                                return d;
                            }
                            catch
                            {
                                throw;
                            }
                        default:
                            return 0;
                    }
                }
                catch { throw; }
            }
        }
        public double ShutterTime//us
        {
            set
            {
                try
                {
                    switch (Type)
                    {
                        case EType.SENSOR_ILD1700:
                        case EType.SENSOR_ILD1320:
                        case EType.SENSOR_ILD1420:
                            break; //ignore - no setting avail
                        case EType.SENSOR_IFD2451:
                        case EType.SENSOR_IFD2421:
                            double min = 0.075;
                            double max = 10000;
                            if (Type == EType.SENSOR_IFD2421) min = 1;

                            double d = Math.Max(min, value);
                            d = Math.Min(max, value);

                            try
                            {
                                //KN not working for IFD2421
                                Sensor.SetDoubleExecSCmd("Set_ShutterTime", "SP_ShutterTime1", d);
                            }
                            catch
                            {
                                throw;
                            }
                            break;
                    }
                }
                catch { throw; }
            }
            get
            {
                try
                {
                    switch (Type)
                    {
                        case EType.SENSOR_ILD1700:
                        case EType.SENSOR_ILD1320:
                        case EType.SENSOR_ILD1420:
                        default: 
                            return 0;//ignore - no support
                        case EType.SENSOR_IFD2451:
                        case EType.SENSOR_IFD2421:
                            double d = 0;
                            Sensor.ExecSCmdGetDouble("Get_ShutterTime", "SA_ShutterTime1", ref d);
                            return d;
                    }
                }
                catch { throw; }
            }
        }
        public double ShutterTime2//us
        {
            set
            {
                try
                {
                    switch (Type)
                    {
                        case EType.SENSOR_ILD1700:
                        case EType.SENSOR_ILD1320:
                        case EType.SENSOR_ILD1420:
                            break;//ignore - no setting avail
                        case EType.SENSOR_IFD2451:
                        case EType.SENSOR_IFD2421:
                            double min = 0.075;
                            double max = 10000;
                            if (Type == EType.SENSOR_IFD2421) min = 1;

                            double d = Math.Max(min, value);
                            d = Math.Min(max, value);

                            try
                            {
                                Sensor.SetDoubleExecSCmd("Set_ShutterTime", "SP_ShutterTime2", d);
                            }
                            catch
                            {
                                throw;
                            }
                            break;
                    }
                }
                catch { throw; }
            }
            get
            {
                try
                {
                    switch (Type)
                    {
                        case EType.SENSOR_ILD1700:
                        case EType.SENSOR_ILD1320:
                        case EType.SENSOR_ILD1420:
                        default:
                            return 0; //ignore - no support
                        case EType.SENSOR_IFD2451:
                        case EType.SENSOR_IFD2421:
                            double d = 0;
                            Sensor.ExecSCmdGetDouble("Get_ShutterTime", "SA_ShutterTime2", ref d);
                            return d;
                    }
                }
                catch { throw; }
            }
        }

        public bool SetTrigModeSw
        {
            set
            {
                try
                {
                    switch (Type)
                    {
                        case EType.SENSOR_ILD1320:
                        case EType.SENSOR_ILD1420:
                            //0 = Full
                            //1 = Off
                            Sensor.SetIntExecSCmd("Set_LaserPower", "SP_LaserPower", value? 1 : 0);
                            break;
                        default:
                            break;//no support
                        case EType.SENSOR_ILD1700:
                            Sensor.ExecSCmd(value ? "Dat_Out_Off" : "Dat_Out_On");
                            break;
                        case EType.SENSOR_IFD2451:
                            //-1 = Unknown parameter value from sensor
                            //0 = None
                            //1 = Edge
                            //2 = Level(PULSE)
                            //3 = Software
                            //4 = Encoder
                            Sensor.SetIntExecSCmd("Set_TriggerMode", "SP_TriggerMode", value ? 3 : 0);
                            break;
                        case EType.SENSOR_IFD2421:
                            //0 = None
                            //1 = Sync / Trig
                            //2 = TrigIn
                            //3 = Software 
                            //4 = Encoder1(only available at option 208)
                            //5 = Encoder2(only available at option 208)
                            Sensor.SetIntExecSCmd("Set_TriggerSource", "SP_TriggerSource", value ? 3 : 0);
                            break;
                    }

                }
                catch { throw; }
            }
        }
        public int TrigCount
        {
            set
            {
                try
                {
                    switch (Type)
                    {
                        case EType.SENSOR_ILD1700:
                        case EType.SENSOR_ILD1320:
                        case EType.SENSOR_ILD1420:
                            //ignore - no setting avail
                            break;
                        case EType.SENSOR_IFD2451:
                        case EType.SENSOR_IFD2421:
                            Sensor.SetIntExecSCmd("Set_TriggerCount", "SP_TriggerCount", (value == -1) ? 16383 : value);
                            break;
                    }
                }
                catch { throw; }
            }
            get
            {
                try
                {
                    switch (Type)
                    {
                        case EType.SENSOR_ILD1700:
                        case EType.SENSOR_ILD1320:
                        case EType.SENSOR_ILD1420:
                            //ignore - no setting avail
                            break;
                        case EType.SENSOR_IFD2451:
                        case EType.SENSOR_IFD2421:
                            int i = 0;
                            Sensor.ExecSCmdGetInt("Get_TriggerCount", "SA_TriggerCount", ref i);
                            return i;
                    }
                    return 0;
                }
                catch { throw; }
            }
        }
        public void SwTrig()
        {
            try
            {
                switch (Type)
                {
                    case EType.SENSOR_ILD1320:
                    case EType.SENSOR_ILD1420:
                        //0 = Full
                        //1 = Off
                        Sensor.SetIntExecSCmd("Set_LaserPower", "SP_LaserPower", 0);
                        break;
                    default:
                        break;//no support
                    case EType.SENSOR_ILD1700:
                        Sensor.ExecSCmd("Dat_Out_On");
                        break;
                    case EType.SENSOR_IFD2451:
                    case EType.SENSOR_IFD2421:
                        Sensor.ExecSCmd("Software_Trigger");
                        break;
                }
            }
            catch { throw; }
        }

        public double VideoThreshold
        {
            set
            {
                try
                {
                    switch (Type)
                    {
                        default:
                        case EType.SENSOR_ILD1320:
                        case EType.SENSOR_ILD1420:
                        case EType.SENSOR_ILD1700:
                        case EType.SENSOR_IFD2421:
                            //ignore - no support
                            break;
                        case EType.SENSOR_IFD2451:
                            double min = 0;
                            double max = 99;
                            double d = Math.Min(min, value);
                            d = Math.Max(max, value);
                            Sensor.SetDoubleExecSCmd("Set_Threshold", "SP_Threshold", value);
                            break;
                    }
                }
                catch
                {
                    throw;
                }
            }
            get
            {
                try
                {
                    switch (Type)
                    {
                        default:
                        case EType.SENSOR_ILD1320:
                        case EType.SENSOR_ILD1420:
                        case EType.SENSOR_ILD1700:
                        case EType.SENSOR_IFD2421:
                            //ignore - no support
                            return 0;
                        case EType.SENSOR_IFD2451:
                            double d = 0;
                            Sensor.ExecSCmdGetDouble("Get_Threshold", "SA_Threshold", ref d);
                            return d;
                    }
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
