using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;

namespace NagaW
{
    public class TELight_LC18
    {
        public TELight_LC18()
        {
        }

        static Mutex mutex = new Mutex();
        static SerialPort Port { get; set; } = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);
        int boardAdd = 0;//User address 0 as default
        public bool Open(string comport, int baudrate)
        {
            if (!Port.IsOpen)
            {
                try
                {
                    Port.PortName = comport;
                    Port.BaudRate = baudrate;
                    Port.Open();
                    Port.DiscardInBuffer();

                    #region Read version and verify communcation is OK
                    Port.WriteTimeout = 1000;
                    Port.Write($"@{boardAdd}1RV" + "{}");
                    System.Threading.Thread.Sleep(10);
                    Port.DtrEnable = true;
                    Port.ReadTimeout = 1000;
                    string rx = Port.ReadTo("}");

                    if (!rx.Contains("{"))
                    {
                        GAlarm.Prompt(EAlarm.LIGHT_CTRL_READVERSION_ERROR, $"BoardID{boardAdd}");
                        return false;
                    }
                    string ver = rx.Remove(0, rx.IndexOf("{") + 1);
                    GLog.WriteLog(ELogType.SYSTEM, "LightCtrl Connected v" + ver + ".");
                    #endregion

                    int boardCount = 1;
                    if (GSystemCfg.Light.Lights.RightBoardID != GSystemCfg.Light.Lights.LeftBoardID) boardCount++;

                    const int CONST_MODE = 0;

                    for (int bd = 0; bd < boardCount; bd++)
                    {
                        for (int ch = 0; ch < 4; ch++)
                            SetMode(boardAdd, ch + 1, CONST_MODE);
                    }

                    if (boardCount > 0)
                    {
                        int boardID = 0;
                        SetMultiplier(boardID, 1, GSystemCfg.Light.Lights.MultiplierBd0Ch1);
                        SetMultiplier(boardID, 2, GSystemCfg.Light.Lights.MultiplierBd0Ch2);
                        SetMultiplier(boardID, 3, GSystemCfg.Light.Lights.MultiplierBd0Ch3);
                        SetMultiplier(boardID, 4, GSystemCfg.Light.Lights.MultiplierBd0Ch4);
                    }
                    if (boardCount > 1)
                    {
                        int boardID = 1;
                        SetMultiplier(boardID, 1, GSystemCfg.Light.Lights.MultiplierBd1Ch1);
                        SetMultiplier(boardID, 2, GSystemCfg.Light.Lights.MultiplierBd1Ch2);
                        SetMultiplier(boardID, 3, GSystemCfg.Light.Lights.MultiplierBd1Ch3);
                        SetMultiplier(boardID, 4, GSystemCfg.Light.Lights.MultiplierBd1Ch4);
                    }

                    TFLightCtrl.LightPair[0].BoardID = GSystemCfg.Light.Lights.LeftBoardID;
                    TFLightCtrl.LightPair[0].StartChannel = GSystemCfg.Light.Lights.LeftStartChannel;
                    TFLightCtrl.LightPair[0].ChannelCount = GSystemCfg.Light.Lights.LeftChannelCount;

                    TFLightCtrl.LightPair[1].BoardID = GSystemCfg.Light.Lights.RightBoardID;
                    TFLightCtrl.LightPair[1].StartChannel = GSystemCfg.Light.Lights.RightStartChannel;
                    TFLightCtrl.LightPair[1].ChannelCount = GSystemCfg.Light.Lights.RightChannelCount;
                }
                catch(Exception ex)
                {
                    GAlarm.Prompt(EAlarm.LIGHT_CTRL_CONNECT_ERROR, $"BoardID{boardAdd}");
                    //Port.Close();
                    return false;
                }
            }
            return true;
        }
        public bool Open()
        {
            return Open(GSystemCfg.Light.Lights.Comport.ToString(), GSystemCfg.Light.Lights.Baudrate);
        }
        public bool Close()
        {
            try
            {
                if (Port.IsOpen)
                    for (int ch = 0; ch < 4; ch++)
                    {
                        SetIntensity(boardAdd, ch + 1, 0);
                        System.Threading.Thread.Sleep(1);
                    }

                Port.Close();
                GLog.WriteLog(ELogType.SYSTEM, "LightCtrl Closed.");
            }
            catch
            {
            }
            return true;
        }

        public bool IsOpen => Port.IsOpen;
        private static bool SetMultiplier(int boardAdd, int channel, int multiplier)
        {
            mutex.WaitOne();
            try
            {
                //Port.DiscardInBuffer();
                Port.Write($"@{boardAdd}{channel}SR" + "{" + $"{multiplier}" + "}");
                System.Threading.Thread.Sleep(1);
                string rx = Port.ReadExisting();
                //Port.DiscardInBuffer();

                return true;
            }
            catch
            {
                GAlarm.Prompt(EAlarm.LIGHT_CTRL_SET_MODE_ERROR, $"BoardID{boardAdd}");
                return false;
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }
        private static bool SetMode(int boardAdd, int channel, int mode)
        {
            mutex.WaitOne();
            try
            {
                //Port.DiscardInBuffer();
                Port.Write($"@{boardAdd}{channel}SM" + "{" + $"{mode}" + "}");
                System.Threading.Thread.Sleep(1);
                string rx = Port.ReadExisting();
                //Port.DiscardInBuffer();

                return true;
            }
            catch
            {
                GAlarm.Prompt(EAlarm.LIGHT_CTRL_SET_MODE_ERROR, $"BoardID{boardAdd}");
                return false;
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }
        public static bool SetIntensity(int boardAdd, int channel, int value)
        {
            mutex.WaitOne();
            try
            {
                Port.WriteTimeout = 100;
                //Port.DiscardInBuffer();
                int input = value > byte.MaxValue ? byte.MaxValue : value < 0 ? input = 0 : value;
                Port.Write($"@{boardAdd}{channel}SI" + "{" + $"{input}" + "}");
                Thread.Sleep(1);
                string rx = Port.ReadExisting();
                //Port.DiscardInBuffer();

                return true;
            }
            catch
            {
                GAlarm.Prompt(EAlarm.LIGHT_CTRL_SET_INTENSITY_ERROR, $"BoardID{boardAdd}");
                return false;
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }

        public LightRGBA[] CurrentLight = new LightRGBA[] { new LightRGBA(0, 0, 0, 0), new LightRGBA(0, 0, 0, 0) };

        public class TLightCh
        {
            internal int BoardID = 0;//0~15
            internal int StartChannel = 1;//1~4
            internal int ChannelCount = 1;//1~4
            public bool IsOff = false;
            public LightRGBA CurrentLight = new LightRGBA(0, 0, 0, 0);
            public TLightCh(int boardID, int startChannel, int channelCount)
            {
                BoardID = boardID;
                StartChannel = startChannel;
                ChannelCount = channelCount;
                IsOff = false;
            }
            public bool Set(LightRGBA para)
            {
                IsOff = false;
                CurrentLight = new LightRGBA(para);
                for (int i = 0; i < ChannelCount; i++)
                {
                    if (!SetIntensity(BoardID, StartChannel + i, (int)para.ToArray[i])) return false;
                    Thread.Sleep(10);
                }
                return true;
            }
            public bool Off()
            {
                IsOff = true;
                for (int i = 0; i < ChannelCount; i++)
                {
                    if (!SetIntensity(BoardID, StartChannel + i, 0)) return false;
                    Thread.Sleep(5);
                }
                return true;
            }
            public bool On()
            {
                IsOff = false;
                return Set(CurrentLight);
            }
        }
        //to refactor when source settle down
        //public TLightCh[] Light = new TLightCh[] { new TLightCh(0, 1, 2), new TLightCh(0, 3, 2) };
    }

    public class TFLightCtrl
    {
        //public static TELight_LC14[] Lights = Enumerable.Range(0, GSystemCfg.Light.Count).Select(x => new TELight(x)).ToArray();
        public static TELight_LC18 Lights = new TELight_LC18();
        //public static int SelectionLightNo = 0;//Selected light no 0=Left, 1=Right 
        public static TELight_LC18.TLightCh[] LightPair = new TELight_LC18.TLightCh[] { new TELight_LC18.TLightCh(0, 1, 2), new TELight_LC18.TLightCh(0, 3, 2) };
        public static TELight_LC18.TLightCh lightPair = LightPair[0];
    }
}
