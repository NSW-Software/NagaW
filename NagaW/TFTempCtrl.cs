using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;

namespace NagaW
{
    public enum ETempModel { TX_Series, TK_Series }
    public class TETempCtrl
    {
        #region enum
        enum ERTUProtocol
        {
            Read_Coil_Status = 1,
            Read_Input_Status,
            Read_Holding_Registers,
            Read_Input_Register,
            Force_Single_Core,
            Present_Single_Registers,
            Present_Multiple_Registers = 16,
        }

        //2.1 Read Coil Status (Func 01) R/W
        enum EReadCoilStatus
        {
            RUN_STOP = 1,
            AT,
            ALARM_RESET,
        }

        //2.1 Force Single Coil (Func 05) R/W
        enum EForceSingleCoil
        {
            RUN_STOP = 1,
            AT,
            ALARM_RESET,
        }

        //2.2 Read Discrete Inputs(Func 02) R
        enum EReadDiscreteInputs
        {
            UNIT_INDICATOR_C = 1,
            UNIT_INDICATOR_F,
            OUTPUT_INDICATOR,
            AT_INDICATOR,
            AL1_INDICATOR,
            AL2_INDICATOR,
        }

        //2.3 Read Input Register (Func 04) R
        enum EReadInputRegister
        {
            PRODUCT_NUMBER_H = 101,
            PRODUCT_NUMBER_L,
            PRESENT_VALUE = 1001,
            SETTING_VALUE = 1004,
        }

        //2.4 Present Single Register (Func 03) R/W
        enum EPresentSingleRegister
        {
            SETTING_VALUE = 1,

            MANUAL_RESET_TX = 57,
            MANUAL_RESET_TK = 57,

            INPUT_SENSOR_TX = 101,
            CONTROL_OUTPUT_MODE_TX = 107,

            INPUT_SENSOR_TK = 151,
            CONTROL_OUTPUT_MODE_TK = 163,
        }

        public enum EControlOutputMode
        {
            HEAT,
            COOL,
            HC,
        }
        public enum EInputSensorTX
        {
            KCA_H,
            KCA_L,
            JIC_H,
            JIC_L,
            LIC_H,
            LIC_L,
            TCC_H,
            TCC_L,
            RPR,
            SPR,
            DPT_H,
            DPT_L,
            CUS_H,
            CUS_L,
            CU5,
            CU10,
            JPT_H,
            JPT_L,
            DPT5,
            NI12,
        }

        public enum EInputSensorTK
        {
            KCA_H,
            KCA_L,
            JIC_H,
            JIC_L,
            ECRH,
            ERCL,
            TCCH,
            TCCL,
            BPR,
            RPR,
            SPR,
            NNN,
            CTT,
            GTT,
            LIC_H,
            LIC_L,
            UCCH,
            UCCL,
            PLII,
            CU5,
            CU10,
            JPT_H,
            JPT_L,
            DPT5,
            DPT_H,
            DPT_L,
            NI12,
        }
        #endregion

        SerialPort Port = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.Two);
        public TETempCtrl()
        {
        }

        public bool Open(string comport)
        {
            try
            {
                if (!IsOpen)
                {
                    Port.PortName = comport;
                    Port.WriteTimeout = 1000;
                    Port.ReadTimeout = 1000;

                    Port.Open();
                }
            }
            catch
            {
                GAlarm.Prompt(EAlarm.TEMPCTRL_OPEN_ERROR);
                return false;
            }

            TCTempCtrl.StartIdle();

            return Init();

            //return true;

        }
        public bool Open()
        {
            return Open(GSystemCfg.Temperature.Temp.Comport.ToString());
        }
        public bool IsOpen
        {
            get
            {
                return Port.IsOpen;
            }
        }
        public void Close()
        {
            TCTempCtrl.StopIdle();
            Port.Close();
        }

        public bool Init()
        {
            foreach (var temp in GSystemCfg.Temperature.Temp.Channels)
            {
                var addr = temp.Address;
                var inputsensor = temp.ChannelInputMode;
                var channeloutput = temp.ChannelOutputMode;

                switch (temp.TempCtrlModel)
                {
                    case ETempModel.TX_Series:
                        {
                            SetControlOutputTX(addr, channeloutput);
                            SetInputSensorTX(addr, inputsensor);
                        }
                        break;
                    case ETempModel.TK_Series:
                        {
                            SetControlOutputTK(addr, channeloutput);
                            SetInputSensorTK(addr, (EInputSensorTK)Enum.Parse(typeof(EInputSensorTK), inputsensor.ToString()));
                        }
                        break;
                }
            }
            return true;
        }

        static Mutex Mtx = new Mutex();

        public bool Write(byte[] cmd)
        {
            if (!Port.IsOpen) return false;
            try
            {
                Mtx.WaitOne();

                Port.DiscardOutBuffer();
                Port.Write(cmd, 0, cmd.Length);

                return true;
            }
            catch
            {
                GAlarm.Prompt(EAlarm.TEMPCTRL_COMMUNICATION_ERROR);
                return false;
            }
            finally
            {
                Mtx.ReleaseMutex();
            }
        }
        public bool Read(out byte[] res)
        {
            res = null;
            if (!Port.IsOpen) return false;

            try
            {
                Mtx.WaitOne();
                res = new byte[Port.BytesToRead];

                Port.Read(res, 0, res.Length);
                Port.DiscardInBuffer();
                return true;

            }
            catch
            {
                GAlarm.Prompt(EAlarm.TEMPCTRL_COMMUNICATION_ERROR);
                return false;
            }
            finally
            {
                Mtx.ReleaseMutex();
            }
        }
        static byte[] CalcCRC(byte[] message)
        {

            ushort num1 = ushort.MaxValue;
            byte num2 = byte.MaxValue;
            byte num3 = byte.MaxValue;


            for (int i = 0; i < message.Length - 2; ++i)
            {
                num1 ^= message[i];
                //MessageBox.Show(String.Join("", num1));
                for (int j = 0; j < 8; j++)
                {
                    char ch = (char)((uint)num1 & 1U);
                    if (ch == '\u0001')
                    {
                        num1 = (ushort)((int)num1 >> 1);

                        num1 ^= 0xA001;



                    }
                    else
                    {
                        num1 = (ushort)((int)num1 >> 1);

                    }
                }

            }
            //MessageBox.Show(String.Join("", num1));
            byte[] numArray = new byte[2]
            {
                (byte)0,num2=(byte)((int) num1 >>8 & (int) byte.MaxValue)
            };
            numArray[0] = num3 = (byte)((uint)num1 & (uint)byte.MaxValue);

            return numArray;

            // MessageBox.Show(num1.ToString());

        }


        //************************************************** ReadCoilStatus ***************************************************************
        bool ReadCoilStatus(int slave_address, EReadCoilStatus coilStatus, out bool status)
        {
            status = false;

            try
            {
                var cmd = new byte[8];

                cmd[0] = (byte)slave_address;
                cmd[1] = (byte)ERTUProtocol.Read_Coil_Status;

                var reg = (int)coilStatus - 1;
                cmd[2] = (byte)(reg >> 8);
                cmd[3] = (byte)(reg);

                cmd[4] = 0;
                cmd[5] = 1;

                var crc = CalcCRC(cmd);
                cmd[6] = crc[0];
                cmd[7] = crc[1];

                if (!Write(cmd)) return false;
                Thread.Sleep(100);
                if (!Read(out byte[] res)) return false;

                //get byte returned
                var byteCount = res[2];
                //extract byte information start form certain index with specific length
                var data = res.ToList().GetRange(3, byteCount).ToArray();
                //convert to binary(8bit) string for decode
                string binary8 = Convert.ToString(data[0], 2).PadLeft(8, '0');
                //output: 00000000 : 00000001
                status = binary8.Contains("1");

                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool IsRunning(int addr)
        {
            if (!ReadCoilStatus(addr, EReadCoilStatus.RUN_STOP, out bool status)) return false;
            //0:run, 1:stop
            return !status;
        }


        //************************************************** ReadInputRegister ************************************************************
        bool ReadInputRegister(int slave_address, EReadInputRegister inputRegister, out string result)
        {
            result = string.Empty;

            try
            {
                var cmd = new byte[8];

                cmd[0] = (byte)slave_address;
                cmd[1] = (byte)ERTUProtocol.Read_Input_Register;

                var reg = (int)inputRegister - 1;
                cmd[2] = (byte)(reg >> 8);
                cmd[3] = (byte)reg;

                cmd[4] = 0;
                cmd[5] = 1;

                var crc = CalcCRC(cmd);
                cmd[6] = crc[0];
                cmd[7] = crc[1];

                if (!Write(cmd)) return false;
                Thread.Sleep(100);
                if (!Read(out byte[] res)) return false;

                //get byte returned
                var byteCount = res[2];
                //extract byte information start form certain index with specific length
                var data = res.ToList().GetRange(3, byteCount).ToArray();

                string hex = BitConverter.ToString(data.ToArray()).Replace("-", string.Empty);
                result = int.Parse(hex, System.Globalization.NumberStyles.HexNumber).ToString();

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public bool Read_PresentValue(int addr, out int pv)
        {
            pv = 0;
            if (!ReadInputRegister(addr, EReadInputRegister.PRESENT_VALUE, out string res)) return false;
            return int.TryParse(res, out pv);
        }
        public bool Read_SettingValue(int addr, out int sv)
        {
            sv = -1;
            if (!ReadInputRegister(addr, EReadInputRegister.SETTING_VALUE, out string res)) return false;
            return int.TryParse(res, out sv);

        }


        //************************************************** ForceSingleCore **************************************************************
        bool ForceSingleCore(int slave_address, EForceSingleCoil singleCoil, bool status)
        {
            try
            {
                var cmd = new byte[8];

                cmd[0] = (byte)slave_address;
                cmd[1] = (byte)ERTUProtocol.Force_Single_Core;

                var reg = (int)singleCoil - 1;
                cmd[2] = (byte)(reg >> 8);
                cmd[3] = (byte)reg;

                cmd[4] = (byte)(status ? 255 : 0);
                cmd[5] = 0/*(byte)(status ? 0 : 255)*/;

                var crc = CalcCRC(cmd);
                cmd[6] = crc[0];
                cmd[7] = crc[1];

                if (!Write(cmd)) return false;
                Thread.Sleep(100);
                if (!Read(out byte[] res)) return false;

                if (res.Length != cmd.Length) return false;
                for (int i = 0; i < cmd.Length; i++)
                {
                    if (cmd[i] != res[i]) return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Run(int addr)
        {
            //0:run, 1:stop
            return ForceSingleCore(addr, EForceSingleCoil.RUN_STOP, false);
        }
        public bool Stop(int addr)
        {
            //0:run, 1:stop
            return ForceSingleCore(addr, EForceSingleCoil.RUN_STOP, true);
        }


        //************************************************** PresentSingleRegister ********************************************************
        bool PresentSingleRegister(int slave_address, EPresentSingleRegister singleRegister, int value)
        {
            try
            {
                var cmd = new byte[8];

                cmd[0] = (byte)slave_address;
                cmd[1] = (byte)ERTUProtocol.Present_Single_Registers;


                var reg = (int)singleRegister - 1;
                cmd[2] = (byte)(reg >> 8);
                cmd[3] = (byte)reg;

                cmd[4] = (byte)(value >> 8);
                cmd[5] = (byte)value;

                var crc = CalcCRC(cmd);
                cmd[6] = crc[0];
                cmd[7] = crc[1];

                if (!Write(cmd)) return false;
                Thread.Sleep(100);
                if (!Read(out byte[] res)) return false;

                if (res.Length != cmd.Length) return false;
                for (int i = 0; i < cmd.Length; i++)
                {
                    if (cmd[i] != res[i]) return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetValue(int addr, int sv)
        {
            return PresentSingleRegister(addr, EPresentSingleRegister.SETTING_VALUE, sv);
        }

        public bool SetControlOutputTX(int addr, EControlOutputMode outputMode)
        {
            return PresentSingleRegister(addr, EPresentSingleRegister.CONTROL_OUTPUT_MODE_TX, (int)outputMode);
        }
        public bool SetControlOutputTK(int addr, EControlOutputMode outputMode)
        {
            return PresentSingleRegister(addr, EPresentSingleRegister.CONTROL_OUTPUT_MODE_TK, (int)outputMode);
        }

        public bool SetInputSensorTX(int addr, EInputSensorTX inputSensor)
        {
            return PresentSingleRegister(addr, EPresentSingleRegister.INPUT_SENSOR_TX, (int)inputSensor);
        }
        public bool SetInputSensorTK(int addr, EInputSensorTK inputSensor)
        {
            return PresentSingleRegister(addr, EPresentSingleRegister.INPUT_SENSOR_TK, (int)inputSensor);
        }

        public bool ManualResetTX(int addr, int value)
        {
            return PresentSingleRegister(addr, EPresentSingleRegister.MANUAL_RESET_TX, value);
        }
        public bool ManualResetTK(int addr, int value)
        {
            return PresentSingleRegister(addr, EPresentSingleRegister.MANUAL_RESET_TK, value);
        }
    }

    public class TFTempCtrl
    {
        public static TETempCtrl TempCtrl = new TETempCtrl();
    }

    public class TCTempCtrl
    {
        readonly static Mutex Mtx = new Mutex();
        readonly static System.Timers.Timer Timer1 = new System.Timers.Timer(1000 * GProcessPara.Temp.IdleStopTime.Min);

        public static void StartIdle()
        {
            Timer1.Enabled = true;

            Timer1.Elapsed -= Timer1_Elapsed;
            Timer1.Elapsed += Timer1_Elapsed;
        }

        private static void Timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                Mtx.WaitOne();

                var pastTime = DateTime.Now - TEZMCAux.LastMoveTime;
                int TimeToIdle = GProcessPara.Temp.IdleStopTime.Value;
                if (TimeToIdle is 0) return;

                if (pastTime.TotalSeconds > TimeToIdle)
                {
                    foreach (var temp in GSystemCfg.Temperature.Temp.Channels)
                    {
                        if (temp.Enable)
                        {
                            //GLog.LogProcess($"Checking Temp{temp.Address}, status Enable[{temp.Enable}]");

                            GLog.LogProcess($"Closing Temp{temp.Address}");
                            TFTempCtrl.TempCtrl.Stop(temp.Address);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GLog.WriteException(ex);
            }
            finally
            {
                Mtx.ReleaseMutex();
            }
        }


        public static void StopIdle()
        {
            Timer1.Elapsed -= Timer1_Elapsed;
            Timer1.Enabled = false;
        }

        static bool stopchecking = true;
        static bool Flag_Allpass;

        public static bool Monitoring()
        {
            if (!GProcessPara.Temp.CheckTempBeforeRun) return true;

            if (!TFTempCtrl.TempCtrl.IsOpen)
            {
                if (!TFTempCtrl.TempCtrl.Open()) return false;
            }

            stopchecking = false;
            Flag_Allpass = false;


            var starttime = DateTime.Now;
            var awaitperiodsecond = GProcessPara.Temp.AwaitErrorTime;

            Action act = new Action(() =>
            {
                try
                {
                    TEZMCAux.LastMoveTime = DateTime.Now;

                    var instantenable = new List<bool>(GSystemCfg.Temperature.Temp.Channels.Select(x => x.Enable));
                    var flagtogo = Enumerable.Repeat(true, 8).ToList();

                    while (true)
                    {
                        // await temp reach setup value until a certain time
                        if ((DateTime.Now - starttime).TotalSeconds > awaitperiodsecond.Value)
                        {
                            GAlarm.Prompt(EAlarm.TEMP_AWAITING_TIMEOUT_ERROR);
                            break;
                        }

                        //cancel flag
                        if (stopchecking) break;
                        Thread.Sleep(1000);

                        for (int i = 0; i < GSystemCfg.Temperature.Temp.Channels.Count; i++)
                        {
                            var addr = GSystemCfg.Temperature.Temp.Channels[i].Address;

                            if (instantenable[i])
                            {
                                var setup = GRecipes.Temp_Setups[i];
                                var sv = setup.SetValue.Value;
                                var poslimit = setup.PosLimit.Value;
                                var neglimit = setup.NegLimit.Value;

                                TFTempCtrl.TempCtrl.Run(addr);
                                TFTempCtrl.TempCtrl.SetValue(addr, sv);
                                instantenable[i] = TFTempCtrl.TempCtrl.Read_PresentValue(addr, out int pv);

                                if (instantenable[i]) flagtogo[i] = pv >= sv + neglimit && pv <= sv + poslimit;

                            }
                        }

                        //all pass flag
                        Flag_Allpass = flagtogo.Where(x => x).Count() == flagtogo.Count();
                        if (Flag_Allpass) break;
                    }
                }
                catch
                {
                    Flag_Allpass = false;
                }
            });

            MsgBox.Processing($"Awaiting Temp to reach setup value ({awaitperiodsecond.ToStringForDisplay()})", act, () => stopchecking = true);

            return Flag_Allpass;
        }
    }
}
