using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace NagaW
{
    public class TEPressCtrl
    {
        public const double Bar = 10;
        public const double PSI = 145.038;

        string Name = "";
        public int Index = 0;

        SerialPort Port { get; set; } = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);
        public TEPressCtrl(int idx)
        {
            Index = idx;
            Name = $"FPress{idx + 1}";
        }
        public TEPressCtrl()
        {
        }
        public bool Open(string comport)
        {
            if (!Port.IsOpen)
            {
                try
                {
                    Port.PortName = comport;
                    Port.Open();
                }
                catch (Exception ex)
                {
                    GAlarm.Prompt(EAlarm.FPRESS_CTRL_OPEN_ERROR, Name + ex);
                    return false;
                }
            }
            return true;
        }
        public bool Open()
        {
            return Open(GSystemCfg.FPress.FPresses[Index].Comport.ToString());
        }
        public bool Close()
        {
            try
            {
                Port.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool IsOpen => Port.IsOpen;
        private void Write(string data)
        {
            Port.DiscardOutBuffer();
            Port.WriteTimeout = 500;
            Port.Write(data + (char)13 + (char)10);
        }
        private void Read(ref string rx)
        {
            Port.DtrEnable = true;
            Port.ReadTimeout = 1000;
            rx = Port.ReadLine();
            Port.DiscardInBuffer();
        }

        private bool Set(int v1024)
        {
            try
            {
                Write("SET " + v1024.ToString());
                string rx = "";
                Read(ref rx);

                if (rx.Contains("OUT OF RANGE"))
                {
                    GAlarm.Prompt(EAlarm.FPRESS_CTRL_OOR_ERROR, Name);
                    return false;
                }
                if (rx.Contains("UNKNOWN"))
                {
                    GAlarm.Prompt(EAlarm.FPRESS_CTRL_UNKNOWN_CMD_ERROR, Name);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                GAlarm.Prompt(EAlarm.FPRESS_CTRL_PORT_READWRITE_ERROR, Name + "\r\n" + ex.Message);
                return false;
            }
        }
        private bool Get(bool monitor, ref int v1024)
        {
            try
            {
                if (monitor)
                    Write("MON");
                else
                    Write("REQ");

                string rx = "";
                Read(ref rx);
                if (rx.Contains("OUT OF RANGE"))
                {
                    GAlarm.Prompt(EAlarm.FPRESS_CTRL_OOR_ERROR, Name);
                    return false;
                }
                if (rx.Contains("UNKNOWN"))
                {
                    GAlarm.Prompt(EAlarm.FPRESS_CTRL_UNKNOWN_CMD_ERROR, Name);
                    return false;
                }
                if (!int.TryParse(rx, out v1024))
                {
                    GAlarm.Prompt(EAlarm.FPRESS_CTRL_UNKNOWN_RESPONSE_ERROR, Name);
                    return false;
                }
                return true;
            }
            catch
            {
                GAlarm.Prompt(EAlarm.FPRESS_CTRL_PORT_READWRITE_ERROR, Name);
                return false;
            }
        }
        public bool Get(ref int value)
        {
            return Get(false, ref value);
        }
        public bool Mon(ref int value)
        {
            return Get(true, ref value);
        }

        public bool Set(double fpressMPA)
        {
            var v1024 = (int)((fpressMPA / 0.9) * 1023);
            return Set(v1024);
        }
        public bool Get(ref double fpressMPA)
        {
            int v1024 = 0;
            bool b = Get(ref v1024);
            fpressMPA = (double)v1024 / (double)1023 * (double)0.9;
            return b;
        }

        public bool Mon(ref double fpressMPA)
        {
            int v1024 = 0;
            bool b = Mon(ref v1024);
            fpressMPA = (double)v1024 / (double)1023 * (double)0.9;
            return b;
        }
    }

    public static class TFPressCtrl
    {
        public const double UpperLimitMPa = 0.7;

        public static TEPressCtrl[] FPress = Enumerable.Range(0, GSystemCfg.FPress.Count).Select(x => new TEPressCtrl(x)).ToArray();
        public static void Open()
        {
            foreach (var fpress in FPress) fpress.Open();
        }
        public static void Close()
        {
            foreach (var fpress in FPress) fpress.Close();
        }
    }


    public static class TCPressCtrl
    {
        public static DateTime[] StartTime = new DateTime[] { DateTime.Now, DateTime.Now };
        static System.Threading.Mutex Mutex = new System.Threading.Mutex();

        public static bool CanCheck = false;
        public static bool Monitoring(int gantry_Idx, PressureSetup setup)
        {


            try
            {
                Mutex.WaitOne();

                if (!setup.isMonitoring) return true;
                if (!CanCheck) return true;
                CanCheck = false;

                //if ((DateTime.Now - StartTime[gantry_Idx]).TotalSeconds < setup.Interval.Value) return true;

                StartTime[gantry_Idx] = DateTime.Now;

                //Fpress
                double fp = 0;
                if (setup.FPress.Value != 0)
                {
                    if (!TFPressCtrl.FPress[gantry_Idx].Mon(ref fp)) return false;
                    GLog.LogProcess($"FPress {gantry_Idx} Monitoring: GetValue:{fp}");
                    if (fp < setup.FPress_NegLmt.Value + setup.FPress.Value || fp > setup.FPress_PosLmt.Value + setup.FPress.Value)
                    {
                        GAlarm.Prompt(EAlarm.FPRESS_CTRL_PV_OUT_OF_RANGE);
                        return false;
                    }
                }

                //Ppress
                double pp = 0;
                if (setup.PPress.Value != 0)
                {
                    if (!TFPressCtrl.FPress[gantry_Idx + 2].Mon(ref pp)) return false;
                    GLog.LogProcess($"PPress {gantry_Idx} Monitoring: GetValue:{pp}");
                    if (pp < setup.PPress_NegLmt.Value + setup.PPress.Value || pp > setup.PPress_PosLmt.Value + setup.PPress.Value)
                    {
                        GAlarm.Prompt(EAlarm.FPRESS_CTRL_PV_OUT_OF_RANGE);
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                Mutex.ReleaseMutex();
            }
            return true;
        }

        public static bool CheckMainAir()
        {
            var status = GMotDef.IN10.Status;
            if (!status) GAlarm.Prompt(EAlarm.MAIN_AIR_PRESSURE_NOT_READY);

            return status;
        }
    }
}
