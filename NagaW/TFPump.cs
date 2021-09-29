using Emgu.CV.Flann;
using SpinnakerNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NagaW
{
    public class TFPump
    {
        public static Vermes_3280_SerialCom[] Vermes_Pump = Enumerable.Range(0, GSystemCfg.Pump.Count).Select(x => new Vermes_3280_SerialCom(x)).ToArray();
        public static void Close()
        {
            for (int k = 0; k < GSystemCfg.Pump.Count; k++)
            {

                var pump = GSystemCfg.Pump.Pumps[k];

                GMotDef.Outputs[(int)pump.FPressDO].Status = false;
                GMotDef.Outputs[(int)pump.PPressDO].Status = false;
                GMotDef.Outputs[(int)pump.VacDO].Status = false;

                switch (pump.PumpType)
                {
                    case EPumpType.VERMES_3280: Vermes_Pump[k].Close(); break;
                }
            }
        }


        static string sBase(int gantryidx)
        {
            return gantryidx is 0 ? TFGantry.GantryLeft.sBase : TFGantry.GantryRight.sBase;
        }

        public static class SP
        {
            public static string StartDispCmd(SP_Param setup, TEZMCAux.TOutput fpressIO, TEZMCAux.TOutput ppressIO, TEZMCAux.TOutput vacIO)
            {
                double pulseOnDly = setup.PulseOnDelay.Value;
                string cmd = "";
                if (pulseOnDly == 0)
                {
                    cmd += $"MOVE_OP({vacIO.OutputNo}, 0) ";
                    cmd += $"MOVE_OP({fpressIO.OutputNo}, 1) ";
                    cmd += $"MOVE_OP({ppressIO.OutputNo}, 1) ";
                }

                //Pulse Pressure ON is earlier
                if (pulseOnDly < 0)
                {
                    //Use MOVE_OP2 to prevent motion decel
                    cmd += $"MOVE_OP({vacIO.OutputNo},0) ";
                    cmd += $"MOVE_OP({ppressIO.OutputNo},1) ";
                    cmd += $"MOVE_OP2({fpressIO.OutputNo},0,{Math.Abs(pulseOnDly)}) ";
                }
                //Pulse Pressure ON is same of later
                if (pulseOnDly > 0)
                {
                    cmd += $"MOVE_OP({vacIO.OutputNo},0) ";
                    cmd += $"MOVE_OP({fpressIO.OutputNo}, 1) ";
                    cmd += $"MOVE_OP2({ppressIO.OutputNo}, 0, {Math.Abs(pulseOnDly)} ) ";
                }
                return cmd;
            }
            public static string EndDispCmd(SP_Param setup, TEZMCAux.TOutput fpressIO, TEZMCAux.TOutput ppressIO, TEZMCAux.TOutput vacIO)
            {
                double pulseOffDly = setup.PulseOffDelay.Value;
                double vacTime = setup.VacDur.Value;
                string cmd = "";
                if (pulseOffDly == 0)
                {
                    cmd += $"MOVE_OP({fpressIO.OutputNo}, 0) ";
                    cmd += $"MOVE_OP({ppressIO.OutputNo}, 0) ";
                    if (vacTime == 0)
                        cmd += $"MOVE_OP({vacIO.OutputNo}, 1) ";
                    else
                        cmd += $"MOVE_OP2({vacIO.OutputNo}, 1, {vacTime}) ";
                }
                //Pulse Pressure OFF is earlier
                if (pulseOffDly < 0)
                {
                    cmd += $"MOVE_OP({ppressIO.OutputNo}, 0) ";
                    cmd += $"MOVE_OP2({fpressIO.OutputNo}, 1,{Math.Abs(pulseOffDly)}) ";
                    cmd += $"MOVE_OP({vacIO.OutputNo}, 1) ";
                }
                //Pulse Pressure OFF is same of later
                if (pulseOffDly > 0)
                {
                    cmd += $"MOVE_OP({fpressIO.OutputNo}, 0) ";
                    cmd += $"MOVE_OP2({ppressIO.OutputNo}, 1,{Math.Abs(pulseOffDly)}) ";
                    cmd += $"MOVE_OP({vacIO.OutputNo}, 1) ";
                }

                return cmd;
            }
            public static string PauseDispCmd(SP_Param setup, TEZMCAux.TOutput fpressIO, TEZMCAux.TOutput ppressIO, TEZMCAux.TOutput vacIO)
            {
                string cmd = "";
                cmd += $"MOVE_OP({fpressIO.OutputNo}, 0) ";
                cmd += $"MOVE_OP({ppressIO.OutputNo}, 0) ";
                return cmd;
            }
            public static string ResumeDispCmd(SP_Param setup, TEZMCAux.TOutput fpressIO, TEZMCAux.TOutput ppressIO, TEZMCAux.TOutput vacIO)
            {
                string cmd = "";
                cmd += $"MOVE_OP({fpressIO.OutputNo}, 1) ";
                cmd += $"MOVE_OP({ppressIO.OutputNo}, 1) ";
                return cmd;
            }
            public static string ShotCmd(SP_Param setup, TEZMCAux.TOutput fpressIO, TEZMCAux.TOutput ppressIO, TEZMCAux.TOutput vacIO)
            {
                string cmd = "";
                cmd += StartDispCmd(setup, fpressIO, ppressIO, vacIO);
                cmd += $"MOVE_DELAY({setup.DispTime.Value}) ";
                cmd += EndDispCmd(setup, fpressIO, ppressIO, vacIO);
                return cmd;
            }
            public static void Shot_One(int ctrlIndex, SP_Param setup, TEZMCAux.TOutput fpressIO, TEZMCAux.TOutput ppressIO, TEZMCAux.TOutput vacIO)
            {
                int baseAxis = ctrlIndex == 0 ? TFGantry.GantryLeft.XAxis.AxisNo : TFGantry.GantryRight.XAxis.AxisNo;

                string cmd = $"BASE({baseAxis}) ";
                cmd += ShotCmd(setup, fpressIO, ppressIO, vacIO);
                TEZMCAux.Execute(cmd);

                while (true)
                {
                    try
                    {
                        int res = TEZMCAux.QueryInt($"IDLE({baseAxis})");
                        if (res == -1) return;
                    }
                    catch (Exception ex)
                    {
                        GDefine.SystemState = ESystemState.ErrorInit;
                        GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, ex);
                    }
                }
            }
            public static void Shot_One(SP_Param setup, int dispCtrl)
            {
                var i = GSystemCfg.Pump.Pumps[dispCtrl];
                var fpress = GMotDef.Outputs[(int)i.FPressDO];
                var ppress = GMotDef.Outputs[(int)i.PPressDO];
                var vac = GMotDef.Outputs[(int)i.VacDO];

                Shot_One(dispCtrl, setup, fpress, ppress, vac);
            }
        }
        public static class TP
        {
            public static string StartDispCmd(SP_Param setup, TEZMCAux.TOutput fpressIO, TEZMCAux.TOutput ppressIO, TEZMCAux.TOutput vacIO)
            {
                string cmd = $"MOVE_OP({vacIO.OutputNo}, 0)";
                cmd += $"MOVE_OP({fpressIO.OutputNo}, 1)";
                return cmd;
            }
            public static string EndDispCmd(SP_Param setup, TEZMCAux.TOutput fpressIO, TEZMCAux.TOutput ppressIO, TEZMCAux.TOutput vacIO)
            {
                double vacDur = setup.VacDur.Value;

                string cmd = $"MOVE_OP({fpressIO.OutputNo}, 0) ";
                if (vacDur == 0)
                    cmd += $"MOVE_OP({vacIO.OutputNo}, 1) ";
                else
                    cmd += $"MOVE_OP2({vacIO.OutputNo}, 1, {vacDur}) ";
                return cmd;
            }
            public static string PauseDispCmd(SP_Param setup, TEZMCAux.TOutput fpressIO, TEZMCAux.TOutput ppressIO, TEZMCAux.TOutput vacIO)
            {
                string cmd = $"MOVE_OP({fpressIO.OutputNo}, 0) ";
                return cmd;
            }
            public static string ResumeDispCmd(SP_Param setup, TEZMCAux.TOutput fpressIO, TEZMCAux.TOutput ppressIO, TEZMCAux.TOutput vacIO)
            {
                string cmd = $"MOVE_OP({fpressIO.OutputNo}, 1) ";
                return cmd;
            }
            public static string ShotCmd(SP_Param setup, TEZMCAux.TOutput fpressIO, TEZMCAux.TOutput ppressIO, TEZMCAux.TOutput vacIO)
            {
                string cmd = "";
                cmd += StartDispCmd(setup, fpressIO, ppressIO, vacIO);
                cmd += $"MOVE_DELAY({setup.DispTime.Value}) ";
                cmd += EndDispCmd(setup, fpressIO, ppressIO, vacIO);
                return cmd;
            }
            public static void Shot_One(int ctrlIndex, SP_Param setup, TEZMCAux.TOutput fpressIO, TEZMCAux.TOutput ppressIO, TEZMCAux.TOutput vacIO)
            {
                int baseAxis = ctrlIndex == 0 ? TFGantry.GantryLeft.XAxis.AxisNo : TFGantry.GantryRight.XAxis.AxisNo;

                string cmd = $"BASE({baseAxis}) ";
                cmd += ShotCmd(setup, fpressIO, ppressIO, vacIO);
                //cmd += "MOVE_PAUSE(3) ";
                //cmd += "MOVE_RESUME ";
                TEZMCAux.Execute(cmd);

                while (true)
                {
                    try
                    {
                        int res = TEZMCAux.QueryInt($"IDLE({baseAxis})");
                        if (res == -1) return;
                    }
                    catch (Exception ex)
                    {
                        GDefine.SystemState = ESystemState.ErrorInit;
                        GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, ex);
                    }
                }
            }
            public static void Shot_One(SP_Param setup, int dispCtrl)
            {
                var i = GSystemCfg.Pump.Pumps[dispCtrl];
                var fpress = GMotDef.Outputs[(int)i.FPressDO];
                var ppress = GMotDef.Outputs[(int)i.PPressDO];
                var vac = GMotDef.Outputs[(int)i.VacDO];

                Shot_One(dispCtrl, setup, fpress, ppress, vac);
            }
        }
        public static class HM
        {
            static double RPM_To_SpeedPPU(double rpm)
            {
                //var stepAngle = 0.72;
                //var degreePerRev = 360;
                //var secs_60 = 60;

                //var ppuPerSec = rpm / (stepAngle / degreePerRev * secs_60);

                //var speed = ppuPerSec / 10000;
                var speed = rpm / 60;

                return speed;
            }

            public static string PurgeStartCmd(int gantryIdx, HM_Param setup, TEZMCAux.TOutput fpressIO, TEZMCAux.TOutput vacIO)
            {
                string LR = gantryIdx is 0 ? "L" : "R";
                string cmd = sBase(gantryIdx);

                double dpSpd = RPM_To_SpeedPPU(setup.DispRPM.Value);
                double dpAcc = RPM_To_SpeedPPU(setup.DispAccel.Value);
                double bsSpd = RPM_To_SpeedPPU(setup.BSuckRPM.Value);

                TEZMCAux.Execute($"HM{LR}_SPEEDPROF(0, {dpSpd},{bsSpd},{dpAcc},{dpAcc})");
                cmd += $"MOVE_TASK({10 + (5 * gantryIdx)},HM{LR}_RUN) ";
                cmd += $"MOVE_OP({vacIO.OutputNo},0) ";
                cmd += $"MOVE_OP({fpressIO.OutputNo},1) ";

                return cmd;
            }
            public static string PurgeStopCmd(int gantryIdx, HM_Param setup, TEZMCAux.TOutput fpressIO, TEZMCAux.TOutput vacIO)
            {
                string LR = gantryIdx is 0 ? "L" : "R";
                string cmd = sBase(gantryIdx);

                double vacDur = setup.VacDur.Value;
                double bsDur = setup.BSuckTime.Value;

                cmd += $"HM{LR}_BSuckDuration={bsDur} ";
                cmd += $"MOVE_OP({fpressIO.OutputNo},0) ";
                if (vacDur > 0) cmd += $"MOVE_OP2({vacIO.OutputNo}, 1,{Math.Abs((int)vacDur)}) ";
                cmd += $"MOVE_TASK({11 + (5 * gantryIdx)},HM{LR}_BSUCK) ";

                return cmd;
            }
            public static string PurgePauseCmd(int gantryIdx, HM_Param setup, TEZMCAux.TOutput fpressIO, TEZMCAux.TOutput vacIO)
            {
                string LR = gantryIdx is 0 ? "L" : "R";
                string cmd = sBase(gantryIdx);

                cmd += $"MOVE_OP({fpressIO.OutputNo},0) ";
                cmd += $"MOVE_TASK({11 + (5 * gantryIdx)},HM{LR}_STOP) ";

                return cmd;
            }
            public static string PurgeResumeCmd(int gantryIdx, HM_Param setup, TEZMCAux.TOutput fpressIO, TEZMCAux.TOutput vacIO)
            {
                string LR = gantryIdx is 0 ? "L" : "R";
                string cmd = sBase(gantryIdx);

                cmd += $"MOVE_OP({fpressIO.OutputNo},1) ";
                cmd += $"MOVE_TASK({10 + (5 * gantryIdx)},HM{LR}_RUN) ";

                return cmd;
            }
            public static string ShotCmd(int gantryIdx, HM_Param setup, TEZMCAux.TOutput fpressIO, TEZMCAux.TOutput vacIO)
            {
                string cmd = string.Empty;
                cmd += PurgeStartCmd(gantryIdx, setup, fpressIO, vacIO);
                cmd += $"MOVE_DELAY({setup.DispTime.Value}) ";
                cmd += PurgeStopCmd(gantryIdx, setup, fpressIO, vacIO);

                return cmd;
            }
            static Mutex Mtx = new Mutex();
            public static void Shot_One(int gantryIdx, HM_Param setup, TEZMCAux.TOutput fpressIO, TEZMCAux.TOutput vacIO)
            {
                Mtx.WaitOne();
                string cmd = ShotCmd(gantryIdx, setup, fpressIO, vacIO);
                TEZMCAux.DirectCommand(cmd);
                Thread.Sleep(setup.DispTime.Value + Math.Max(setup.BSuckTime.Value, setup.VacDur.Value) + 5);
                Mtx.ReleaseMutex();
            }
        }

        public static class PnuematicJet
        {
            public static string StartDispCmd(PneumaticJet_Param jet_Param, TEZMCAux.TOutput fpressIO, TEZMCAux.TOutput ppressIO)
            {
                string cmd = string.Empty;
                cmd += $"MOVE_OP({ppressIO.OutputNo},1) ";
                cmd += $"MOVE_OP({fpressIO.OutputNo},1) ";
                return cmd;
            }

            public static string EndDispCmd(PneumaticJet_Param jet_Param, TEZMCAux.TOutput fpressIO, TEZMCAux.TOutput ppressIO)
            {
                var offtime = jet_Param.OffTime.Value;
                string cmd = string.Empty;
                cmd += $"MOVE_OP({ppressIO.OutputNo},0) ";
                if (offtime > 0) cmd += cmd += $"MOVE_DELAY({offtime}) ";

                return cmd;
            }

            public static string ShotCmd(PneumaticJet_Param jet_Param, TEZMCAux.TOutput fpressIO, TEZMCAux.TOutput ppressIO)
            {
                var ontime = jet_Param.DispTime.Value;
                var offtime = jet_Param.OffTime.Value;

                string cmd = string.Empty;
                cmd += $"MOVE_OP({fpressIO.OutputNo},1) ";
                cmd += $"MOVE_OP2({ppressIO.OutputNo}, 1,{ontime}) ";
                if (offtime > 0) cmd += $"MOVE_DELAY({offtime}) ";

                return cmd;
            }

            public static double DistanceForDisp(PneumaticJet_Param jet_Param, double speed)
            {
                //  v=s/t; s=v*t

                return Math.Abs(speed * jet_Param.DispTime.Value);
            }

            static Mutex Mutex = new Mutex();
            public static void Shot_One(int gantryIdx, PneumaticJet_Param jet_Param, TEZMCAux.TOutput fpressIO, TEZMCAux.TOutput vpressIO)
            {
                var cycletime = jet_Param.DispTime.Value + jet_Param.OffTime.Value;

                Mutex.WaitOne();
                string cmd = gantryIdx is 0 ? TFGantry.GantryLeft.sBase : TFGantry.GantryRight.sBase;
                cmd += ShotCmd(jet_Param, fpressIO, vpressIO);
                TEZMCAux.DirectCommand(cmd);
                Thread.Sleep((int)cycletime);
                Mutex.ReleaseMutex();
            }
        }
    }

    public class Vermes_3280_SerialCom
    {
        #region
        readonly static double[] Ri_Min = new double[101]
        {
                0.01,                                               //(in %)
                0.01,0.01,0.01,0.02,0.02,0.02,0.03,0.03,0.03,0.03,  //1-10
                0.04,0.04,0.04,0.05,0.05,0.05,0.06,0.06,0.06,0.06,  //11-20
                0.07,0.07,0.07,0.08,0.08,0.08,0.09,0.09,0.09,0.09,  //21-30
                0.1,0.1,0.1,0.11,0.11,0.11,0.12,0.12,0.12,0.12,     //31-40
                0.13,0.13,0.13,0.14,0.14,0.14,0.15,0.15,0.15,0.15,  //41-50
                0.16,0.16,0.16,0.17,0.17,0.17,0.18,0.18,0.18,0.18,  //51-60
                0.19,0.19,0.19,0.2,0.2,0.2,0.21,0.21,0.21,0.21,     //61-70
                0.22,0.22,0.22,0.23,0.23,0.23,0.24,0.24,0.24,0.24,  //71-80
                0.25,0.25,0.25,0.26,0.26,0.26,0.27,0.27,0.27,0.27,  //81-90
                0.28,0.28,0.28,0.29,0.29,0.29,0.3,0.3,0.3,0.3       //91-100
        };
        readonly static double[] Fa_Min = new double[101]
        {
                0.01,                                               //(in %)
                0.01,0.01,0.01,0.01,0.01,0.01,0.01,0.01,0.01,0.01,  //1-10
                0.02,0.02,0.02,0.02,0.02,0.02,0.02,0.02,0.02,0.02,  //11-20
                0.03,0.03,0.03,0.03,0.03,0.03,0.03,0.03,0.03,0.03,  //21-30
                0.04,0.04,0.04,0.04,0.04,0.04,0.04,0.04,0.04,0.04,  //31-40
                0.05,0.05,0.05,0.05,0.05,0.05,0.05,0.05,0.05,0.05,  //41-50
                0.06,0.06,0.06,0.06,0.06,0.06,0.06,0.06,0.06,0.06,  //51-60
                0.07,0.07,0.07,0.07,0.07,0.07,0.07,0.07,0.07,0.07,  //61-70
                0.08,0.08,0.08,0.08,0.08,0.08,0.08,0.08,0.08,0.08,  //71-80
                0.09,0.09,0.09,0.09,0.09,0.09,0.09,0.09,0.09,0.09,  //81-90
                0.1,0.1,0.1,0.1,0.1,0.1,0.1,0.1,0.1,0.1             //91-100
        };
        #endregion

        string Name = string.Empty;
        public int Index { get; set; }

        SerialPort Port = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);

        public Vermes_3280_SerialCom()
        {
        }
        public Vermes_3280_SerialCom(int idx)
        {
            Index = idx;
            Name = $"Pump{idx + 1}";
        }

        public bool Open(string com)
        {
            try
            {
                Port.PortName = com;
                Port.Open();

                if (!WriteThenRead("*IDN?", out string res))//write Identification Query to check connection
                {
                    Close();
                    return false;
                }

            }
            catch (Exception ex)
            {
                GAlarm.Prompt(EAlarm.PUMP_VERMESM3280_CONNECT_FAIL, Name + "\r\n" + ex.Message);
                return false;
            }
            return true;
        }
        public bool Open()
        {
            return Open(GSystemCfg.Pump.Pumps[Index].Comport.ToString());
        }
        public void Close()
        {
            Port.Close();
        }

        public bool IsOpen => Port.IsOpen;

        Mutex comMutex = new Mutex();
        bool Write(string input)
        {
            comMutex.WaitOne();
            try
            {
                string msg = input + "\n\r";//Vermes command are in the order of LF folled by CR

                Port.DiscardOutBuffer();
                Port.DiscardInBuffer();

                Port.WriteTimeout = 1000;
                Port.Write(msg);
            }
            catch(Exception ex)
            {
                GAlarm.Prompt(EAlarm.PUMP_VERMESM3280_COMMUNICATION_ERROR, ex);
                return false;
            }
            finally
            {
                comMutex.ReleaseMutex();
            }
            return true;
        }
        bool Read(out string res)
        {
            comMutex.WaitOne();
            res = string.Empty;
            try
            {
                Port.ReadTimeout = 1000;
                //res = Port.ReadLine();
                //res = Port.ReadExisting();
                res = Port.ReadTo("\n\r");
            }
            catch(Exception ex)
            {
                GAlarm.Prompt(EAlarm.PUMP_VERMESM3280_COMMUNICATION_ERROR, ex);
                return false;
            }
            finally
            {
                comMutex.ReleaseMutex();
            }
            return true;
        }

        public bool WriteThenRead(string input, out string res)
        {
            res = string.Empty;

            try
            {
                if (!Port.IsOpen)
                {
                    GAlarm.Prompt(EAlarm.PUMP_VERMESM3280_CONNECT_FAIL, Name);
                    return false;
                }

                //Port.DiscardInBuffer();
                //Port.DiscardOutBuffer();

                if (!Write(input)) return false;
                if (!Read(out string res2)) return false;
                res = res2;
            }
            catch (Exception ex)
            {
                GAlarm.Prompt(EAlarm.PUMP_VERMESM3280_COMMUNICATION_ERROR, Name + "\r\n" + ex.Message);
                return false;
            }
            return true;
        }
        public bool WriteThenRead(string input, string compared_msg)
        {
            if (!WriteThenRead(input, out string res)) return false;
            if (res != compared_msg)
            {
                GAlarm.Prompt(EAlarm.PUMP_VERMESM3280_COMMUNICATION_ERROR, $"Res invalid.\r\nExpected{compared_msg}\r\nFeedback{res}");
                return false;
            }
            return true;
        }

        //Vermes command supports
        //Command       3280    3200 
        //VALVE:UP      Yes     Yes
        //VALVE:DOWN    Yes     Yes
        //VALVE:AOPEN   Yes     Yes (initiates dispensing cycle)
        //TRIGGER:SET   Yes     Yes (units for Falling are *0.01), returns OK
        //TRIGGER:ASET  Yes     Yes (units for Risng and Falling are *0.01), returns OK
        //STRIGGER:SET  Yes     No (units for Falling are *0.01), returns setting values
        //STRIGGER:ASET Yes     No (units for Risng and Falling are *0.01), returns setting values
        //SETUP:SAVE    Yes     No (units for Falling are *0.01), returns OK
        //SETUP:ASAVE   Yes     No (units for Risng and Falling are *0.01), returns OK

        public bool ValveUp()
        {
            return WriteThenRead("VALVE:UP", "OK");
        }
        public bool ValveDown()
        {
            return WriteThenRead("VALVE:DOWN", "OK");
        }
        public bool Shot()
        {
            return WriteThenRead("VALVE:AOPEN", "OK");
        }

        private void ModifyValueA(ref double rising, ref double opentime, ref double falling, ref int needlelift, ref int pulses, ref double delay)
        {
            rising = (Ri_Min[needlelift] >= rising ? Ri_Min[needlelift] : rising) * 100;
            opentime = (needlelift > 80 ? opentime > 15 ? 15 : opentime : opentime) * 10;
            falling = (Fa_Min[needlelift] >= falling ? Fa_Min[needlelift] : falling) * 100;
            delay *= 10;
        }
        private void ModifyValueA(ref double[] param)
        {
            int nl = (int)param[3];
            int pulse = (int)param[4];
            ModifyValueA(ref param[0], ref param[1], ref param[2], ref nl, ref pulse, ref param[5]);
            param[3] = nl;
            param[4] = pulse;
        }

        //removed Replace with TriggerASet - 20210218 - KNK
        //Reason: Supports 3200 models
        //public bool STriggerAset(double rising, double opentime, double falling, int needlelift, int pulses, double delay)
        //{
        //    ModifyValueA(ref rising, ref opentime, ref falling, ref needlelift, ref pulses, ref delay);
        //    string input = $"STRIGGER:ASET:{rising},{opentime},{falling},{needlelift},{pulses},{delay},1";
        //    string res = $"{rising},{opentime},{falling},{needlelift},{pulses},{delay}";
        //    return WriteThenRead(input, res);
        //}
        //public bool STriggerAset(params double[] param)
        //{
        //    if (param.Count() != 6) return false;
        //    return STriggerAset(param[0], param[1], param[2], (int)param[3], (int)param[4], param[5]);
        //}
        public bool TriggerAset(double rising, double opentime, double falling, int needlelift, int pulses, double delay)
        {
            ModifyValueA(ref rising, ref opentime, ref falling, ref needlelift, ref pulses, ref delay);
            string input = $"TRIGGER:ASET:{rising},{opentime},{falling},{needlelift},{pulses},{delay},1";
            string res = "OK";
            return WriteThenRead(input, res);
        }
        public bool TriggerAset(params double[] param)
        {
            if (param.Count() != 6) return false;
            return TriggerAset(param[0], param[1], param[2], (int)param[3], (int)param[4], param[5]);
        }
        public bool TriggerAset(Vermes3280_Param setup)
        {
            return TriggerAset(setup.ToArray);
        }

        //removed SetupASave - 20210218 - KN
        //Reason 1: TriggerASet (with option 1) already save the parameter
        //Reason 2: operation para is program dependent - no need to save
        //public bool SetupASave(int setupno, double rising, double opentime, double falling, int needlelift, int pulses, double delay)
        //{
        //    ModifyValueA(ref rising, ref opentime, ref falling, ref needlelift, ref pulses, ref delay);
        //    string input = $"SETUP:ASAVE:{setupno}:{rising},{opentime},{falling},{needlelift},{pulses},{delay}";
        //    return WriteThenRead(input, "OK");
        //}
        //public bool SetupASave(int setupno, params double[] param)
        //{
        //    if (param.Count() < 6) return false;
        //    return SetupASave(setupno, param[0], param[1], param[2], (int)param[3], (int)param[4], param[5]);
        //}
        //public bool SetupASave(Vermes3280_Param setup, int setupno = 0)
        //{
        //    return SetupASave(setupno, setup.ToArray);
        //}

        //public bool SValveAOpenS(int setupno, params double[] param)
        //{
        //    ModifyValueA(ref param);
        //    return WriteThenRead($"SVALVE:AOPENS{setupno}", string.Join(",", param));
        //}
        //public bool SValveAOpenS(Vermes3280_Param setup, int setupno = 0)
        //{
        //    return SValveAOpenS(setupno, setup.ToArray);
        //}

        frmVermesTerminal terminal = null;
        public void Adjust(TEZMCAux.TOutput dispIO)
        {
            try
            {
                terminal = new frmVermesTerminal();
                terminal.btnCancel.Enabled = false;
                terminal.btnEnter.Click += (a, b) =>
                {
                    dispIO.Status = true;
                    Thread.Sleep(10);//short trigger signal
                    dispIO.Status = false;
                };
                terminal.btnCancel.Click += (a, b) =>
                {
                    //dispIO.Status = true;
                    //Thread.Sleep(150);//long trigger signal
                    //dispIO.Status = false;
                    terminal.Close();
                };
                terminal.btnDone.Click += (a, b) =>
                {
                    terminal.Close();
                };
                Port.DataReceived += Port_DataReceived;
                terminal.Load += (a, b) =>
                {
                    string cmd = "ADJUST:START";
                    Write(cmd);
                    terminal.richtbx.Text = $"{cmd}";
                };
                terminal.ShowDialog();
            }
            catch
            {
            }
            finally
            {
                dispIO.Status = true;
                Thread.Sleep(150);
                dispIO.Status = false;
                Port.DataReceived -= Port_DataReceived;
                terminal = null;
            }
        }
        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //3280 message sequence
            //Adjust Screw OUT Press Enter, Adjust Screw OUT  Press Enter
            //500 Shots Please Wait,    500 Shots      Please Wait
            //Adjust Screw IN until GREEN LED ON, Adjust Screw IN until green LED
            //Adjust Screw IN until red LED ON, Adjust Screw IN until red LED ON
            //Adjust Success

            //3200 message sequence
            //Unscrew nozzle press enter
            //1020~1039 mix 1,2
            //Adjust LED GREEN

            Read(out string res);
            try
            {
                terminal.Invoke(new Action(() =>
                {
                    if (res.Length > 1)//Vermes3200 returns mixture of cal values and unknown single charater. Ignore single character.
                        terminal.richtbx.Text = res;
                    if (!terminal.btnCancel.Enabled)
                    {
                        terminal.btnCancel.Enabled = !res.Contains("Wait");
                    }
                    terminal.btnDone.Enabled = res.Contains("Adjust LED GREEN");
                }));
            }
            catch { }
        }
    }
}
