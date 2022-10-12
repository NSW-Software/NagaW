using System;
using System.Linq;

namespace NagaW
{
    public class TFHSensor
    {

        static TFCL3 CL3 = new TFCL3();
        private TEMEDAQ HSensor = new TEMEDAQ();

        public int Index = 0;
        public TFHSensor(int idx)
        {
            Index = idx;
        }

        static TEMEDAQ.EType Type = TEMEDAQ.EType.NONE;

        public bool Open(TEMEDAQ.EType type, string ipAddress_Comport)
        {
            try
            {
                Type = type;

                switch (type)
                {
                    case TEMEDAQ.EType.CL3000:
                        {
                            CL3.Open(ipAddress_Comport);
                            break;
                        }
                    default:
                        {
                            HSensor.Open(type, ipAddress_Comport);
                            HSensor.ClearBuffer();
                            HSensor.SetTrigModeSw = false;

                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                GDefine.SystemState = ESystemState.ErrorRestart;
                switch (Type)
                {
                    case TEMEDAQ.EType.SENSOR_IFD2421:
                    case TEMEDAQ.EType.SENSOR_IFD2451:
                        GAlarm.Prompt(EAlarm.CONFOCAL_CONNECT_ERROR);
                        break;
                    case TEMEDAQ.EType.CL3000:
                        GAlarm.Prompt(EAlarm.LASERSENSOR_CONNECT_ERROR, ex);
                        break;
                    default:
                        GAlarm.Prompt(EAlarm.LASERSENSOR_CONNECT_ERROR);
                        break;

                }
                return false;
            }

            return true;
        }
        public bool Open()
        {
            return Open(GSystemCfg.HSensor.HSensors[Index].Type,
                GSystemCfg.HSensor.HSensors[Index].Comport == ECOM.IP ? GSystemCfg.HSensor.HSensors[Index].IPAddress : GSystemCfg.HSensor.HSensors[Index].Comport.ToString());
        }

        public bool Close()
        {
            try
            {
                switch (Type)
                {
                    case TEMEDAQ.EType.CL3000: CL3.Close(); break;
                    default: HSensor.Close(); break;

                }
                Type = TEMEDAQ.EType.NONE;
            }
            catch
            {
            }
            return true;
        }
        public bool GetValue(ref double value)
        {
            try
            {
                switch (Type)
                {
                    case TEMEDAQ.EType.CL3000: CL3.GetValue(Index, out value); break;
                    default: HSensor.Poll(ref value); break;
                }

                if (Math.Abs(value) is 999.999)
                {
                    GAlarm.Prompt(EAlarm.LASERSENSOR_VALUE_ERROR, "Out of Distance.");
                    return false;
                }

                return true;
            }
            catch
            {
                GDefine.SystemState = ESystemState.ErrorRestart;
                switch (Type)
                {
                    case TEMEDAQ.EType.SENSOR_IFD2421:
                    case TEMEDAQ.EType.SENSOR_IFD2451:
                        GAlarm.Prompt(EAlarm.CONFOCAL_VALUE_ERROR);
                        break;
                    default:
                        GAlarm.Prompt(EAlarm.LASERSENSOR_VALUE_ERROR);
                        break;
                }
                return false;
            }
        }
        public bool IsConnected
        {
            get
            {
                return Type == TEMEDAQ.EType.CL3000 ? CL3.IsConnected : HSensor.Connected;
            }
        }
    }
    public class TFHSensors
    {
        public static TFHSensor[] Sensor = Enumerable.Range(0, GSystemCfg.HSensor.Count).Select(x => new TFHSensor(x)).ToArray();
    }
}
