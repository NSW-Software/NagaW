using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NagaW
{
    class TFLConv
    {
        public static TEZMCAux.TOutput SvStopperUp { get => GMotDef.Out39; }
        public static TEZMCAux.TOutput SvConvUp { get => GMotDef.Out40; }
        public static TEZMCAux.TOutput SvConvDn { get => GMotDef.Out41; }
        public static TEZMCAux.TOutput SvChuckVac { get => GMotDef.Out42; }

        public static TEZMCAux.TInput SensInPsnt { get => GMotDef.IN36; }
        public static TEZMCAux.TInput SensBdPsnt { get => GMotDef.IN37; }
        public static TEZMCAux.TInput SensStopperUp { get => GMotDef.IN38; }
        public static TEZMCAux.TInput SensConvUp { get => GMotDef.IN39; }
        public static TEZMCAux.TInput SensConvDn { get => GMotDef.IN40; }
        public static TEZMCAux.TInput SensChuckVac { get => GMotDef.IN41; }

        public static TEZMCAux.TInput BdReady { get => GMotDef.IN32; }
        public static TEZMCAux.TOutput McReady { get => GMotDef.Out36; }

        public static TEZMCAux.TInput MirrorSmemaOutMcReady { get => GMotDef.IN35; }
        public static TEZMCAux.TOutput MirrorSmemaOutBdReady {get => GMotDef.Out52; }

        public static bool ByPassBdReady = false;

        public static bool ByPassMcReady = false;

        private static bool Move(bool posDir, bool fastSpeed = true)
        {
            try
            {
                if (fastSpeed)
                    GMotDef.ConvL.SpeedProfile(GProcessPara.Operation.CFastSpeedProfile);
                else
                    GMotDef.ConvL.SpeedProfile(GProcessPara.Operation.CSlowSpeedProfile);

                if (posDir)
                    GMotDef.ConvL.JogAxisP = true;
                else
                    GMotDef.ConvL.JogAxisN = true;

                return true;
            }
            catch (Exception ex)
            {
                GDefine.SystemState = ESystemState.ErrorInit;
                GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                return false;
            }
        }
        public static bool MoveRevFast()
        {
            return Move(false);
        }
        public static bool MoveRevSlow()
        {
            return Move(false, false);
        }
        public static bool MoveFwdFast()
        {
            return Move(true);
        }
        public static bool MoveFwdSlow()
        {
            return Move(true, false);
        }
        public static bool Stop()
        {
            try
            {
                GMotDef.ConvL.JogAxisP = false;
                return true;
            }
            catch (Exception ex)
            {
                GDefine.SystemState = ESystemState.ErrorInit;
                GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                return false;
            }
        }

        public static bool ConvDn()
        {
            try
            {
                bool vacEnabled = GProcessPara.Conveyor.VacEnabled.Value > 0;
                if (vacEnabled && GProcessPara.Conveyor.VacSequence.Value == 0) SvChuckVac.Status = true;

                SvConvUp.Status = false;
                SvConvDn.Status = true;

                var sw = Stopwatch.StartNew();
                while (!SensConvDn.Status) 
                {
                    if (sw.ElapsedMilliseconds > GProcessPara.Conveyor.UpDnTimeOut.Value)
                    {
                        GAlarm.Prompt(EAlarm.CONV_LEFT_DN_TIMEOUT);
                        return false;
                    }
                    Thread.Sleep(0);
                }

                if (vacEnabled && GProcessPara.Conveyor.VacSequence.Value == 1) SvChuckVac.Status = true;

                var sw2 = Stopwatch.StartNew();
                while (!SensChuckVac.Status && vacEnabled)
                {
                    if (sw2.ElapsedMilliseconds > GProcessPara.Conveyor.VacTimeOut.Value)
                    {
                        SvChuckVac.Status = false;
                        GAlarm.Prompt(EAlarm.CONV_LEFT_VAC_TIMEOUT);
                        return false;
                    }
                    Thread.Sleep(0);
                }
                return true;
            }
            catch (Exception ex)
            {
                GDefine.SystemState = ESystemState.ErrorInit;
                GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                return false;
            }
        }
        public static bool ConvUp()
        {
            try
            {
                bool enableVac = GProcessPara.Conveyor.VacEnabled.Value > 0;
                SvChuckVac.Status = false;

                SvConvUp.Status = true;
                SvConvDn.Status = false;


                var sw2 = Stopwatch.StartNew();
                while (SensChuckVac.Status && enableVac)
                {
                    if (sw2.ElapsedMilliseconds > GProcessPara.Conveyor.VacTimeOut.Value)
                    {
                        SvChuckVac.Status = false;
                        GAlarm.Prompt(EAlarm.CONV_LEFT_VAC_TIMEOUT);
                        return false;
                    }
                    Thread.Sleep(0);
                }

                var sw = Stopwatch.StartNew();
                while (!SensConvUp.Status) 
                {
                    if (sw.ElapsedMilliseconds > GProcessPara.Conveyor.UpDnTimeOut.Value)
                    {
                        GAlarm.Prompt(EAlarm.CONV_LEFT_UP_TIMEOUT);
                        return false;
                    }
                    Thread.Sleep(0);
                }
                return true;
            }
            catch (Exception ex)
            {
                GDefine.SystemState = ESystemState.ErrorInit;
                GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                return false;
            }
        }

        public static bool StopperUp()
        {
            try
            {
                SvStopperUp.Status = true;

                var sw = Stopwatch.StartNew();
                while (!SensStopperUp.Status) 
                {
                    if (sw.ElapsedMilliseconds > GProcessPara.Conveyor.StopperTimeOut.Value)
                    {
                        GAlarm.Prompt(EAlarm.CONV_LEFT_STOPPER_UP_TIMEOUT);
                        return false;
                    }
                    Thread.Sleep(0);
                }
                return true;
            }
            catch (Exception ex)
            {
                GDefine.SystemState = ESystemState.ErrorInit;
                GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                return false;
            }
        }
        public static bool StopperDn()
        {
            try
            {
                SvStopperUp.Status = false;

                var sw = Stopwatch.StartNew();
                while (SensStopperUp.Status) 
                {
                    if (sw.ElapsedMilliseconds > GProcessPara.Conveyor.StopperTimeOut.Value)
                    {
                        GAlarm.Prompt(EAlarm.CONV_LEFT_STOPPER_DN_TIMEOUT);
                        return false;
                    }
                    Thread.Sleep(0);
                }
                return true;
            }
            catch (Exception ex)
            {
                GDefine.SystemState = ESystemState.ErrorInit;
                GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                return false;
            }
        }

        public static EStatus Status = EStatus.Unknown;
        public static EBdStatus BdStatus = EBdStatus.Unknown;
        public static bool Init()
        {
            Status = EStatus.Initing;
            BdStatus = EBdStatus.Unknown;
            McReady.Status = false;
            MirrorSmemaOutBdReady.Status = false;
            if (!TEZMCAux.Ready()) return Error();
            if (!TFGantry.CheckEMO) return Error();

            string cmdBuffer = $"BASE({GMotDef.ConvL.AxisNo}) ATYPE=1 UNITS={GSystemCfg.Conveyor.PPU}";
            TEZMCAux.Execute(cmdBuffer, false);

            BdStatus = EBdStatus.Busy;

            if (!StopperDn() || !ConvUp()) return Error();

            if (SensInPsnt.Status)
            {
                GAlarm.Prompt(EAlarm.CONV_Remove_Board_On_IN_PSNT);
                return Error();
            }

            if (!MoveRevFast()) { Stop(); return Error(); }

            var sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < GProcessPara.Conveyor.MoveTimeOut.Value)
            {
                if (SensInPsnt.Status)
                {
                    Stop();
                    break;
                }
                Thread.Sleep(0);
            }
            Stop();
            GMotDef.ConvL.Wait();

            if (SensInPsnt.Status)
            {
                GAlarm.Prompt(EAlarm.CONV_Remove_Board_On_IN_PSNT);
                //Status = EStatus.InitError;
                return Error();
            }

            if (SensBdPsnt.Status)
            {
                GAlarm.Prompt(EAlarm.CONV_Remove_Board_On_LEFT_PSNT);
                return Error();
            }

            BdStatus = EBdStatus.None;
            Status = EStatus.Ready;

            return true;

            bool Error()
            {
                BdStatus = EBdStatus.Unknown;
                Status = EStatus.InitError;
                return false;
            }
        }

        public static bool LoadToPro()//load to process station, not check SensIn
        {
            BdStatus = EBdStatus.Busy;

            if (!ConvUp() || !StopperUp() || !MoveFwdFast()) return Error();

            var sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < GProcessPara.Conveyor.MoveTimeOut.Value)
            {
                if (SensBdPsnt.Status)
                {
                    if (!MoveFwdSlow()) { Stop(); return Error(); };
                    break;
                }
                Thread.Sleep(0);
            }
            var sw2 = Stopwatch.StartNew();
            while (sw2.ElapsedMilliseconds < GProcessPara.Conveyor.MoveDelay.Value) { Thread.Sleep(0); };
            Stop();
            GMotDef.ConvL.Wait();

            if (sw.ElapsedMilliseconds >= GProcessPara.Conveyor.MoveTimeOut.Value)
            {
                GAlarm.Prompt(EAlarm.CONV_LEFT_LOAD_PSNT_TIMEOUT);
                return Error();
            }

            if (!ConvDn()) return Error();
            if (!StopperDn()) return Error();

            BdStatus = EBdStatus.WaitProcess;
            return true;
            //        _Error:
            bool Error()
            {
                Stop();
                BdStatus = EBdStatus.Unknown;
                return false;
            }
        }
        public static bool ReturnToLeft()
        {
            BdStatus = EBdStatus.Busy;

            if (!ConvUp() || !StopperDn() || !MoveRevFast()) return Error();

            var sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < GProcessPara.Conveyor.MoveTimeOut.Value)
            {
                if (SensInPsnt.Status)
                {
                    Stop();
                    break;
                }
                Thread.Sleep(0);
            }
            Stop();
            GMotDef.ConvL.Wait();

            if (sw.ElapsedMilliseconds >= GProcessPara.Conveyor.MoveTimeOut.Value)
            {
                GAlarm.Prompt(EAlarm.CONV_LEFT_RETURN_LEFT_TIMEOUT);
                return Error();
            }

            BdStatus = EBdStatus.WaitUnload;
            return true;

            //_Error:

            bool Error()
            {
                Stop();
                BdStatus = EBdStatus.Unknown;
                return false;
            }
        }
        public static bool ReturnToRight()
        {
            BdStatus = EBdStatus.Busy;

            TFRConv.BdStatus = EBdStatus.Busy;


            if (!ConvUp()) return Abort();
            if (!StopperDn()) return Abort();
            if (!TFRConv.ConvUp()) return Abort();
            if (!TFRConv.StopperDn()) return Abort();

            if (!MoveFwdFast()) return Abort();
            if (!TFRConv.MoveFwdFast()) return Abort();

            var sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < GProcessPara.Conveyor.MoveTimeOut.Value)
            {
                if (TFRConv.SensOutPsnt.Status)
                {
                    TFRConv.Stop();
                    break;
                }
                Thread.Sleep(0);
            }
            Stop();
            if (!TFRConv.Stop()) return Abort();
            GMotDef.ConvL.Wait();
            GMotDef.ConvR.Wait();

            if (sw.ElapsedMilliseconds >= GProcessPara.Conveyor.MoveTimeOut.Value)
            {
                GAlarm.Prompt(EAlarm.CONV_RIGHT_MOVE_TIMEOUT);
                return false;
            }

            BdStatus = EBdStatus.None;

            TFRConv.BdStatus = EBdStatus.WaitUnload;
            return true;


            //_Abort:
            bool Abort()
            {
                Stop();
                if (!TFRConv.Stop()) //return false;
                    GMotDef.ConvL.Wait();
                GMotDef.ConvR.Wait();

                BdStatus = EBdStatus.Unknown;

                TFRConv.BdStatus = EBdStatus.Unknown;

                return false;
            }

        }
        public static bool UnLoadToRight()
        {


            if (!ReturnToRight()) return Abort();

            while (!TFRConv.SensOutPsnt.Status)
            {
                TFRConv.BdStatus = EBdStatus.None;
            }
            //if (!TFRConv.SmemaSendOut()) return Abort();
            return true;
            //_Abort:
            bool Abort()
            {
                return false;
            }
        }

        public static bool SmemaLoadIn(bool bypassLoadToPro = false, float toggledTimeOut = 3000 )
        {
            BdStatus = EBdStatus.None;

            if (!TFLConv.ConvUp()) return SmemaLoadInFail();

            McReady.Status = true;
            var toggledTimer = Stopwatch.StartNew();
            while (!BdReady.Status && !ByPassBdReady)
            {
                if (toggledTimer.ElapsedMilliseconds >= toggledTimeOut)
                {
                    McReady.Status = false;
                    return true;
                }
                Thread.Sleep(10);
            }

            if (!MoveFwdFast()) return SmemaLoadInFail();
            var sw = Stopwatch.StartNew();
            while (!SensInPsnt.Status)
            {
                if (sw.ElapsedMilliseconds >= GProcessPara.Conveyor.SmemaTimeOut.Value)
                {
                    GAlarm.Prompt(EAlarm.CONV_LEFT_SMEMA_LOAD_IN_PSNT_TIMEOUT);
                    return SmemaLoadInFail();
                }
                Thread.Sleep(10);
            }

            if (!bypassLoadToPro)
            {
                if (!LoadToPro()) return SmemaLoadInFail();
                McReady.Status = false;
                return true;
            }
            else
            {
                while(SensInPsnt.Status)
                {
                    if(!BdReady.Status && !ByPassBdReady|| sw.ElapsedMilliseconds >= GProcessPara.Conveyor.SmemaTimeOut.Value)
                    {
                        GAlarm.Prompt(EAlarm.CONV_LEFT_SMEMA_COMMUNICATION_FAILED);
                        return SmemaLoadInFail();
                    }
                }
                McReady.Status = false;
                return true;
            }
           
            bool SmemaLoadInFail()
            {
                Stop();
                BdStatus = EBdStatus.Unknown;
                McReady.Status = false;//ignore this risk
                return false;
            }
        }
        public static bool SmemaSendOut(float toggledTimeOut = 3000)
        {
            MirrorSmemaOutBdReady .Status = true;
            var toggledTimer = Stopwatch.StartNew();
            while (!MirrorSmemaOutMcReady.Status && !ByPassMcReady)
            {
                if (toggledTimer.ElapsedMilliseconds >= toggledTimeOut)
                {
                    MirrorSmemaOutBdReady .Status = false;
                    return true;
                }
                Thread.Sleep(10);
            }

            BdStatus = EBdStatus.Busy;

            if (!MoveRevFast()) return SmemaSendOutFail();
            var sw3 = Stopwatch.StartNew();
            while (SensInPsnt.Status)
            {
                if (sw3.ElapsedMilliseconds >= GProcessPara.Conveyor.SmemaTimeOut.Value)
                {
                    GAlarm.Prompt(EAlarm.CONV_LEFT_SMEMA_SEND_OUT_IN_PSNT_TIMEOUT);
                    return SmemaSendOutFail();
                }
                Thread.Sleep(10);
            }

            Stop();
            GMotDef.ConvL.Wait();

            BdStatus = EBdStatus.None;
            MirrorSmemaOutBdReady .Status = false;
            return true;

            bool SmemaSendOutFail()
            {
                Stop();
                MirrorSmemaOutBdReady .Status = false;

                BdStatus = EBdStatus.Unknown;
                return false;
            }
        }
    }

    class TFRConv
    {
        public static TEZMCAux.TOutput SvStopperUp { get => GMotDef.Out45; }
        public static TEZMCAux.TOutput SvConvUp { get => GMotDef.Out46; }
        public static TEZMCAux.TOutput SvConvDn { get => GMotDef.Out47; }
        public static TEZMCAux.TOutput SvChuckVac { get => GMotDef.Out48; }

        public static TEZMCAux.TInput SensOutPsnt { get => GMotDef.IN43; }
        public static TEZMCAux.TInput SensBdPsnt { get => GMotDef.IN42; }
        public static TEZMCAux.TInput SensStopperUp { get => GMotDef.IN44; }
        public static TEZMCAux.TInput SensConvUp { get => GMotDef.IN45; }
        public static TEZMCAux.TInput SensConvDn { get => GMotDef.IN46; }
        public static TEZMCAux.TInput SensChuckVac { get => GMotDef.IN47; }

        public static TEZMCAux.TInput McReady { get => GMotDef.IN33; }
        public static TEZMCAux.TOutput BdReady { get => GMotDef.Out37; }

        public static TEZMCAux.TInput MirrorSmemaInBdReady { get => GMotDef.IN34; }
        public static TEZMCAux.TOutput MirrorSmemaInMcReady { get => GMotDef.Out51; }

        public static bool ByPassMcReady = false;
        public static bool ByPassBdReady = false;
        private static bool Move(bool posDir, bool fastSpeed = true)
        {
            try
            {
                if (fastSpeed)
                    GMotDef.ConvR.SpeedProfile(GProcessPara.Operation.CFastSpeedProfile);
                else
                    GMotDef.ConvR.SpeedProfile(GProcessPara.Operation.CSlowSpeedProfile);

                if (posDir)
                    GMotDef.ConvR.JogAxisP = true;
                else
                    GMotDef.ConvR.JogAxisN = true;

                return true;
            }
            catch (Exception ex)
            {
                GDefine.SystemState = ESystemState.ErrorInit;
                GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                return false;
            }
        }
        public static bool MoveRevFast()
        {
            return Move(false);
        }
        public static bool MoveRevSlow()
        {
            return Move(false, false);
        }
        public static bool MoveFwdFast()
        {
            return Move(true);
        }
        public static bool MoveFwdSlow()
        {
            return Move(true, false);
        }
        public static bool Stop()
        {
            try
            {
                GMotDef.ConvR.JogAxisP = false;
                return true;
            }
            catch (Exception ex)
            {
                GDefine.SystemState = ESystemState.ErrorInit;
                GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                return false;
            }
        }

        public static bool ConvDn()
        {
            try
            {
                bool vacEnabled = GProcessPara.Conveyor.VacEnabled.Value > 0;
                if (vacEnabled && GProcessPara.Conveyor.VacSequence.Value == 0) SvChuckVac.Status = true;

                SvConvUp.Status = false;
                SvConvDn.Status = true;

                var sw = Stopwatch.StartNew();
                while (!SensConvDn.Status)
                {
                    if (sw.ElapsedMilliseconds > GProcessPara.Conveyor.UpDnTimeOut.Value)
                    {
                        GAlarm.Prompt(EAlarm.CONV_RIGHT_DN_TIMEOUT);
                        return false;
                    }
                    Thread.Sleep(0);
                }

                if (vacEnabled && GProcessPara.Conveyor.VacSequence.Value == 1) SvChuckVac.Status = true;

                if (vacEnabled)
                {
                    var sw2 = Stopwatch.StartNew();
                    while (!SensChuckVac.Status)
                    {
                        if (sw2.ElapsedMilliseconds > GProcessPara.Conveyor.VacTimeOut.Value)
                        {
                            SvChuckVac.Status = false;
                            GAlarm.Prompt(EAlarm.CONV_RIGHT_VAC_TIMEOUT);
                            return false;
                        }
                        Thread.Sleep(0);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                GDefine.SystemState = ESystemState.ErrorInit;
                GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                return false;
            }
        }
        public static bool ConvUp()
        {
            try
            {
                bool enableVac = GProcessPara.Conveyor.VacEnabled.Value > 0;
                SvChuckVac.Status = false;

                SvConvUp.Status = true;
                SvConvDn.Status = false;

                var sw2 = Stopwatch.StartNew();
                while (SensChuckVac.Status && enableVac)
                {
                    if (sw2.ElapsedMilliseconds > GProcessPara.Conveyor.VacTimeOut.Value)
                    {
                        SvChuckVac.Status = false;
                        GAlarm.Prompt(EAlarm.CONV_RIGHT_VAC_TIMEOUT);
                        return false;
                    }
                    Thread.Sleep(0);
                }

                var sw = Stopwatch.StartNew();
                int timeout = Math.Max(GProcessPara.Conveyor.UpDnTimeOut.Value, GProcessPara.Conveyor.VacTimeOut.Value);
                while (!SensConvUp.Status) 
                {
                    if (sw.ElapsedMilliseconds > timeout)
                    {
                        GAlarm.Prompt(EAlarm.CONV_RIGHT_UP_TIMEOUT);
                        return false;
                    }
                    Thread.Sleep(0);
                }
                return true;
            }
            catch (Exception ex)
            {
                GDefine.SystemState = ESystemState.ErrorInit;
                GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                return false;
            }
        }

        public static bool StopperUp()
        {
            try
            {
                SvStopperUp.Status = true;

                var sw = Stopwatch.StartNew();
                while (!SensStopperUp.Status);
                {
                    if (sw.ElapsedMilliseconds > GProcessPara.Conveyor.StopperTimeOut.Value)
                    {
                        GAlarm.Prompt(EAlarm.CONV_RIGHT_STOPPER_UP_TIMEOUT);
                        return false;
                    }
                    Thread.Sleep(0);
                }
                return true;
            }
            catch (Exception ex)
            {
                GDefine.SystemState = ESystemState.ErrorInit;
                GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                return false;
            }
        }
        public static bool StopperDn()
        {
            try
            {
                SvStopperUp.Status = false;

                var sw = Stopwatch.StartNew();
                while (SensStopperUp.Status) 
                {
                    if (sw.ElapsedMilliseconds > GProcessPara.Conveyor.StopperTimeOut.Value)
                    {
                        GAlarm.Prompt(EAlarm.CONV_RIGHT_STOPPER_DN_TIMEOUT);
                        return false;
                    }
                    Thread.Sleep(0);
                }
                return true;
            }
            catch (Exception ex)
            {
                GDefine.SystemState = ESystemState.ErrorInit;
                GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                return false;
            }
        }

        public static EStatus Status = EStatus.Unknown;
        public static EBdStatus BdStatus = EBdStatus.Unknown;

        public static bool Init()
        {
            Status = EStatus.Initing;
            BdStatus = EBdStatus.Unknown;
            BdReady.Status = false;
            MirrorSmemaInMcReady.Status = false;
            if (!TEZMCAux.Ready()) return Error();
            if (!TFGantry.CheckEMO) return Error();

            string cmdBuffer = $"BASE({GMotDef.ConvR.AxisNo}) ATYPE=1 UNITS={GSystemCfg.Conveyor.PPU}";
            TEZMCAux.Execute(cmdBuffer, false);

            BdStatus = EBdStatus.Busy;

            if (!StopperDn() || !ConvUp()) return Error();

            if (SensOutPsnt.Status)
            {
                GAlarm.Prompt(EAlarm.CONV_Remove_Board_On_OUT_PSNT);
                return Error();
            }

            if (!MoveFwdFast()) { Stop(); return Error(); };

            var sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < GProcessPara.Conveyor.MoveTimeOut.Value)
            {
                if (SensOutPsnt.Status)
                {
                    break;
                }
                Thread.Sleep(0);
            }
            Stop();
            GMotDef.ConvR.Wait();

            if (SensOutPsnt.Status)
            {
                GAlarm.Prompt(EAlarm.CONV_Remove_Board_On_OUT_PSNT);
                return Error();
            }

            if (SensBdPsnt.Status)
            {
                GAlarm.Prompt(EAlarm.CONV_Remove_Board_On_RIGHT_PSNT);
            }

            BdStatus = EBdStatus.None;
            Status = EStatus.Ready;
            return true;
            //_Error:

            bool Error()
            {
                Status = EStatus.InitError;
                BdStatus = EBdStatus.Unknown;
                return false;
            }
        }

        public static bool LoadRightToPro()
        {
            BdStatus = EBdStatus.Busy;

            if (!ConvUp()) return Error();
            if (!StopperDn()) return Error();
            if (!MoveRevFast()) return Error();

            var sw = Stopwatch.StartNew();
            bool sensed = false;
            while (sw.ElapsedMilliseconds < GProcessPara.Conveyor.MoveTimeOut.Value)
            {
                if (SensBdPsnt.Status)
                {
                    sensed = true;
                }
                if (sensed && !SensBdPsnt.Status)
                {
                    break;
                }
                Thread.Sleep(0);
            }
            Stop();
            GMotDef.ConvR.Wait();

            if (sw.ElapsedMilliseconds >= GProcessPara.Conveyor.MoveTimeOut.Value)
            {
                GAlarm.Prompt(EAlarm.CONV_RIGHT_MOVE_TIMEOUT);
                return Error();
            }

            if (!StopperUp()) return Error();
            if (!MoveFwdFast()) return Error();

            sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < GProcessPara.Conveyor.MoveTimeOut.Value)
            {
                if (SensBdPsnt.Status)
                {
                    if (!MoveFwdSlow()) return Error();
                    break;
                }
         
                Thread.Sleep(0);
            }
            var sw2 = Stopwatch.StartNew();
            /*sw.Restart(); */
            while (sw2.ElapsedMilliseconds < GProcessPara.Conveyor.MoveDelay.Value) { Thread.Sleep(0); };
            Stop();
            GMotDef.ConvR.Wait();

            if (sw.ElapsedMilliseconds >= GProcessPara.Conveyor.MoveTimeOut.Value)
            {
                GAlarm.Prompt(EAlarm.CONV_RIGHT_MOVE_TIMEOUT);
                return false;
            }

            ConvDn();
            if (!StopperDn()) return Error();

            BdStatus = EBdStatus.WaitProcess;
            return true;

            bool Error()
            {
                Stop();
                BdStatus = EBdStatus.Unknown;
                return false;
            }
        }
        public static bool LoadLeftToPro() // whithout checking Left In Prsnt 
        {
            TFLConv.BdStatus = EBdStatus.Busy;

            BdStatus = EBdStatus.Busy;

            //if (!TFLConv.SensBdPsnt.Status) return false;

            if (!ConvUp()) return Error();
            if (!StopperUp()) return Error();
            if (!MoveFwdFast()) return Error();

            if (!TFLConv.ConvUp()) return Error();
            if (!TFLConv.StopperDn()) return Error();
            if (!TFLConv.MoveFwdFast()) return Error();

            var sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < GProcessPara.Conveyor.MoveTimeOut.Value)
            {
                if (SensBdPsnt.Status )
                {
                    if (!MoveFwdSlow()) return Error();
                    break;
                }
                Thread.Sleep(0);
            }
            var sw2 = Stopwatch.StartNew();
            while (sw2.ElapsedMilliseconds < GProcessPara.Conveyor.MoveDelay.Value) { Thread.Sleep(0); };
            Stop();
            if (!TFLConv.Stop()) return Error();
            GMotDef.ConvL.Wait();
            GMotDef.ConvR.Wait();

            if (sw.ElapsedMilliseconds >= GProcessPara.Conveyor.MoveTimeOut.Value)
            {
                GAlarm.Prompt(EAlarm.CONV_RIGHT_MOVE_TIMEOUT);
                return Error();
            }

            ConvDn();
            if (!StopperDn()) return Error();

            BdStatus = EBdStatus.WaitProcess;

            TFLConv.BdStatus = EBdStatus.None;
            return true;

            //_Abort:
            bool Error()
            {
                Stop();
                if (!TFLConv.Stop()) //return false;
                    BdStatus = EBdStatus.Unknown;

                TFLConv.BdStatus = EBdStatus.Unknown;
                TFRConv.BdStatus = EBdStatus.Unknown;
                GMotDef.ConvL.Wait();
                GMotDef.ConvR.Wait();
                return false;
            }
        }
        public static bool ReturnToRight()
        {
            BdStatus = EBdStatus.Busy;

            if (!ConvUp()) return Error();
            if (!StopperDn()) return Error();
            if (!MoveFwdFast()) return Error();

            var sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < GProcessPara.Conveyor.MoveTimeOut.Value)
            {
                if (SensOutPsnt.Status)
                {
                    Stop();
                    break;
                }
                Thread.Sleep(0);
            }
            Stop();
            GMotDef.ConvR.Wait();

            if (sw.ElapsedMilliseconds >= GProcessPara.Conveyor.MoveTimeOut.Value)
            {
                GAlarm.Prompt(EAlarm.CONV_RIGHT_MOVE_TIMEOUT);
                return Error();
            }

            BdStatus = EBdStatus.WaitUnload;
            return true;

            bool Error()
            {
                Stop();
                BdStatus = EBdStatus.Unknown;
                return false;
            }
        }
        public static bool ReturnToLeft()
        {
            BdStatus = EBdStatus.Busy;
            TFLConv.BdStatus = EBdStatus.Busy;


            if (!ConvUp() || !MoveRevFast() || !TFLConv.ConvUp() || !TFLConv.StopperDn() || !TFLConv.MoveRevFast()) return Error();

            var sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < GProcessPara.Conveyor.MoveTimeOut.Value)
            {
                if (TFLConv.SensInPsnt.Status)
                {
                    TFLConv.Stop();
                    break;
                }
                Thread.Sleep(0);
            }
            Stop();
            TFLConv.Stop();
            GMotDef.ConvL.Wait();
            GMotDef.ConvR.Wait();

            if (sw.ElapsedMilliseconds >= GProcessPara.Conveyor.MoveTimeOut.Value)
            {
                GAlarm.Prompt(EAlarm.CONV_LEFT_RETURN_LEFT_TIMEOUT);
                return Error();
            }

            BdStatus = EBdStatus.None;
            TFLConv.BdStatus = EBdStatus.WaitUnload;
            return true;

            //_Error:

            bool Error()
            {
                Stop();
                BdStatus = EBdStatus.Unknown;
                TFLConv.BdStatus = EBdStatus.Unknown;
                return false;
            }
        }

        public static bool SmemaSendOut(float toggledTimeOut = 3000)
        {
            SvChuckVac.Status = false;

            BdReady.Status = true;
            var toggledTimer = Stopwatch.StartNew();
            while (!McReady.Status && !ByPassMcReady)
            {
                if (toggledTimer.ElapsedMilliseconds >= toggledTimeOut)
                {
                    BdReady.Status = false;
                    return true;
                }
                Thread.Sleep(10);
            }

            BdStatus = EBdStatus.Busy;

            if (!MoveFwdFast()) return SmemaSendOutFail();
            var sw3 = Stopwatch.StartNew();
            while (SensOutPsnt.Status)
            {
                if (sw3.ElapsedMilliseconds >= GProcessPara.Conveyor.SmemaTimeOut.Value)
                {
                    GAlarm.Prompt(EAlarm.CONV_RIGHT_OUT_PSNT_TIMEOUT);
                    return SmemaSendOutFail();
                }
                Thread.Sleep(10);
            }

            Stop();
            GMotDef.ConvR.Wait();

            BdStatus = EBdStatus.None;
            BdReady.Status = false;
            return true;

            bool SmemaSendOutFail()
            {
                Stop();
                BdReady.Status = false;

                BdStatus = EBdStatus.Unknown;
                return false;
            }
        }
        public static bool SmemaLoadIn(bool bypassLoadToPro = false, float toggledTimeOut = 3000)
        {
            BdStatus = EBdStatus.None;

            if (!TFRConv.ConvUp()) return SmemaLoadInFail();

            MirrorSmemaInMcReady.Status = true;
            var toggledTimer = Stopwatch.StartNew();
            while (!MirrorSmemaInBdReady.Status && !ByPassBdReady)
            {
                if (toggledTimer.ElapsedMilliseconds >= toggledTimeOut)
                {
                    MirrorSmemaInMcReady.Status = false;
                    return true;
                }
                Thread.Sleep(10);
            }

            if (!MoveRevFast()) return SmemaLoadInFail();
            var sw = Stopwatch.StartNew();
            while (!SensOutPsnt.Status)
            {
                if (sw.ElapsedMilliseconds >= GProcessPara.Conveyor.SmemaTimeOut.Value)
                {
                    GAlarm.Prompt(EAlarm.CONV_RIGHT_SMEMA_LOAD_IN_OUT_PSNT_TIMEOUT);
                    return SmemaLoadInFail();
                }
                Thread.Sleep(10);
            }

            if (!bypassLoadToPro)
            {
                if (!LoadRightToPro()) return SmemaLoadInFail();
                MirrorSmemaInMcReady.Status = false;
                return true;
            }
            else
            {
                while(SensOutPsnt.Status)
                {
                   if(!MirrorSmemaInBdReady.Status && !ByPassBdReady || sw.ElapsedMilliseconds >= GProcessPara.Conveyor.SmemaTimeOut.Value)
                    {
                        GAlarm.Prompt(EAlarm.CONV_RIGHT_SMEMA_COMMUNICATION_FAILED);
                        return SmemaLoadInFail();
                    }
                }
                MirrorSmemaInMcReady.Status = false;
                return true;
            }
            bool SmemaLoadInFail()
            {
                Stop();
                BdStatus = EBdStatus.Unknown;
                MirrorSmemaInMcReady.Status = false;//ignore this risk
                return false;
            }
        }
    }

    class TFConv
    {
        public static int testRunTime = 1000;

        public static bool InitAll()
        {
            if (!TEZMCAux.Ready()) return false;
            if (!TFGantry.CheckEMO) return false;

            if (GSystemCfg.Conveyor.Conv_Interface == EConvInterface.CONV_BY_PASS)
            {
                TFLConv.Status = EStatus.Ready;
                TFRConv.Status = EStatus.Ready;
                return true;
            }

            try
            {
                var taskL = Task<bool>.Run(() => { return TFLConv.Init(); });
                var taskR = Task<bool>.Run(() => { return TFRConv.Init(); });

                Task.WaitAll(taskL, taskR);
                if (!taskL.Result || !taskR.Result) return false;

                if (cToken != null)
                {
                    if (!cToken.IsCancellationRequested) cToken.Cancel();
                    cToken.Dispose();
                }
                return true;
            }
            catch (Exception ex)
            {
                GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                return false;
            }
        }

        public static bool TestRun()
        {
            if (!TEZMCAux.Ready()) return false;
            if (!TFGantry.CheckEMO) return false;

            switch (GSystemCfg.Conveyor.Conv_Interface)
            {
                #region Smema left to right Serial
                case EConvInterface.SMEMA_LR_SERIAL:

                    if (TFLConv.BdStatus == EBdStatus.None)
                    {
                        if (TFLConv.SensInPsnt.Status)
                        {
                            if (!TFLConv.LoadToPro()) return Error();
                        }
                        else
                        {
                            if (!TFLConv.SmemaLoadIn(false, 1000)) return Error();
                        }
                    }
                    if (TFLConv.BdStatus == EBdStatus.Ready && TFRConv.BdStatus == EBdStatus.None)
                    {
                        if (!TFRConv.LoadLeftToPro()) return Error();
                    }
                    if (TFRConv.BdStatus == EBdStatus.Ready)
                    {
                        if (!TFRConv.ReturnToRight()) return Error();
                    }
                    if (TFRConv.BdStatus == EBdStatus.WaitUnload)
                    {
                        if (!TFRConv.SmemaSendOut(1000)) return Error();
                        if (!TFRConv.SensOutPsnt.Status)
                        {
                            TFRConv.BdStatus = EBdStatus.None;
                        }
                    }
                    return true;
                #endregion

                #region Smema left to right mirror 
                case EConvInterface.SMEMA_LR_MIRROR:

                    if (TFLConv.BdStatus == EBdStatus.None)
                    {
                        if (TFLConv.SensInPsnt.Status)
                        {
                            if (!TFLConv.LoadToPro()) return Error();
                        }
                        else
                        {
                            if (!TFLConv.SmemaLoadIn(false, 1000)) return Error();
                        }
                    }

                    if (TFRConv.BdStatus == EBdStatus.None)
                    {
                        if (TFRConv.SensOutPsnt.Status)
                        {
                            if (!TFRConv.LoadRightToPro()) return Error();
                        }
                        else
                        {
                            if (!TFRConv.SmemaLoadIn(false, 1000)) return Error();
                        }


                    }



                    if (TFLConv.BdStatus == EBdStatus.Ready)
                    {
                        if (!TFLConv.SensInPsnt.Status)
                        {
                            if (!TFLConv.ReturnToLeft()) return Error();
                        }
                        else
                        {
                            GAlarm.Prompt(EAlarm.CONV_Remove_Board_On_IN_PSNT, "UNKNOW BOARD PRESENT");
                            return Error();
                        }
                    }



                    if (TFRConv.BdStatus == EBdStatus.Ready)
                    {
                        if (!TFRConv.SensOutPsnt.Status)
                        {
                            if (!TFRConv.ReturnToRight()) return Error();
                        }
                        else
                        {
                            GAlarm.Prompt(EAlarm.CONV_Remove_Board_On_IN_PSNT, "UNKNOW BOARD PRESENT");
                            return Error();
                        }
                    }


                    if (TFLConv.BdStatus == EBdStatus.WaitUnload)
                    {
                        if (!TFLConv.SensInPsnt.Status)
                        {
                            TFLConv.BdStatus = EBdStatus.None;
                        }
                        else
                        {
                            if (!TFLConv.SmemaSendOut(1000)) return Error();
                        }
                    }

                    if (TFRConv.BdStatus == EBdStatus.WaitUnload)
                    {
                        if (!TFRConv.SensOutPsnt.Status)
                        {
                            TFRConv.BdStatus = EBdStatus.None;
                        }
                        else
                        {
                            if (!TFRConv.SmemaSendOut(1000)) return Error();
                        }
                    }

                    return true;

                #endregion

                #region SMEMA left to rigth parallel 
                case EConvInterface.SMEMA_LR_PARALLEL:

                    if (TFRConv.BdStatus == EBdStatus.None && TFLConv.BdStatus == EBdStatus.None)
                    {
                        if (TFLConv.SensInPsnt.Status)
                        {
                            if (!TFRConv.LoadLeftToPro()) return Error();
                        }
                        else
                        {
                            if (!TFLConv.SmemaLoadIn(true, 1000)) return Error();
                            if (TFLConv.BdStatus == EBdStatus.Busy)
                                if (!TFRConv.LoadLeftToPro()) return Error();
                        }
                    }
                    else if (TFLConv.BdStatus == EBdStatus.None)
                    {
                        if (TFLConv.SensInPsnt.Status)
                        {
                            if (!TFLConv.LoadToPro()) return Error();
                        }
                        else
                        {
                            if (!TFLConv.SmemaLoadIn(false, 1000)) return Error();
                        }
                    }

                    if (TFLConv.BdStatus == EBdStatus.Ready && TFRConv.BdStatus == EBdStatus.None)
                    {
                        if (!TFRConv.SensOutPsnt.Status)
                            if (!TFLConv.ReturnToRight()) return Error();
                    }
                    if (TFRConv.BdStatus == EBdStatus.Ready)
                    {
                        if (!TFRConv.ReturnToRight()) return Error();
                    }
                    if (TFRConv.BdStatus == EBdStatus.WaitUnload)
                    {
                        if (!TFRConv.SmemaSendOut(1000)) return Error();
                        if (!TFRConv.SensOutPsnt.Status)
                        {
                            TFRConv.BdStatus = EBdStatus.None;
                        }
                    }

                    return true;
                #endregion

                case EConvInterface.CONV_BY_PASS:
                    TFLConv.BdStatus = EBdStatus.WaitProcess;
                    TFRConv.BdStatus = EBdStatus.WaitProcess;
                    return true;
            }
            return false;
            bool Error()
            {
                return false;
            }
        }

       public static  CancellationTokenSource cToken = null;
       public static  List<Task> TaskConv = new List<Task>();


        public static bool StartConv()
        {

            if (!TEZMCAux.Ready()) return false;
            if (!TFGantry.CheckEMO) return false;

            TaskConv.Clear();
            if (cToken != null && !cToken.IsCancellationRequested) return false;
            cToken = new CancellationTokenSource();

            switch (GSystemCfg.Conveyor.Conv_Interface)
            {

                #region Smema left to right Serial
                case EConvInterface.SMEMA_LR_SERIAL:
                    
                    try
                    {
                        TaskConv.Add(Task.Run(() =>
                        {
                            while(!cToken.IsCancellationRequested)
                            {
                                //if (TFLConv.SensInPsnt.Status && TFLConv.BdStatus == EBdStatus.None)
                                //{
                                //    if (!TFLConv.LoadToPro()) ErrorTokenStop();
                                //}
                                 if (TFLConv.BdStatus == EBdStatus.Ready && TFRConv.BdStatus == EBdStatus.None)
                                {
                                    if (!TFRConv.LoadLeftToPro()) ErrorTokenStop();
                                }
                                else if (TFRConv.BdStatus == EBdStatus.Ready)
                                {
                                    if (!TFRConv.ReturnToRight()) ErrorTokenStop();
                                }
                                else if (TFRConv.BdStatus == EBdStatus.WaitUnload)
                                {
                                    if (!TFRConv.SensOutPsnt.Status) TFRConv.BdStatus = EBdStatus.None;
                                }
                                Thread.Sleep(100);
                            }

                        }));

                        TaskConv.Add(Task.Run(() => { if (!SmemaLoadIn(cToken.Token)) ErrorTokenStop(); }));

                        TaskConv.Add(Task.Run(() =>
                        {
                            while (!cToken.IsCancellationRequested)
                            {
                                if (!SmemaLoadOut(cToken.Token)) ErrorTokenStop();
                                Thread.Sleep(100);
                            }
                        }));

                        return true;
                    }
                    catch(Exception e)
                    {
                        ErrorTokenStop();
                        return false;
                    }
                #endregion

                #region Smema left to right mirror 
                case EConvInterface.SMEMA_LR_MIRROR:

                    try
                    {

                        TaskConv.Add(Task.Run(() =>
                        {
                            while (!cToken.IsCancellationRequested)
                            {
                                //if (TFLConv.SensInPsnt.Status && TFLConv.BdStatus == EBdStatus.None)
                                //{
                                //    if (!TFLConv.LoadToPro()) ErrorTokenStop();
                                //}
                                if(TFLConv.BdStatus == EBdStatus.Ready && !TFLConv.SensInPsnt.Status)
                                {
                                    if (!TFLConv.ReturnToLeft()) ErrorTokenStop();

                                }
                                if (!TFLConv.SensInPsnt.Status && TFLConv.BdStatus == EBdStatus.WaitUnload)
                                {
                                    TFLConv.BdStatus = EBdStatus.None;
                                }
                               // Thread.Sleep(100);
                            }
                        }));


                        TaskConv.Add(Task.Run(() => { if (!SmemaLoadIn(cToken.Token)) ErrorTokenStop(); }));

                        TaskConv.Add(Task.Run(() =>
                        {
                            while (!cToken.IsCancellationRequested)
                            {
                                if (TFLConv.BdStatus == EBdStatus.WaitUnload)
                                {
                                    if (!TFLConv.SmemaSendOut()) ErrorTokenStop();
                                }
                                //Thread.Sleep(100);
                            }   
                        }));





                       
                        TaskConv.Add(Task.Run(() =>
                        {
                            while (!cToken.IsCancellationRequested)
                            {
                                //if (TFRConv.SensOutPsnt.Status && TFRConv.BdStatus == EBdStatus.None )
                                //{
                                //    if (!TFRConv.LoadRightToPro()) ErrorTokenStop();
                                //}
                                if (TFRConv.BdStatus == EBdStatus.Ready && !TFRConv.SensOutPsnt.Status)
                                {
                                    if (!TFRConv.ReturnToRight()) ErrorTokenStop();
                                }
                                if (!TFRConv.SensOutPsnt.Status && TFRConv.BdStatus == EBdStatus.WaitUnload)
                                {
                                    TFRConv.BdStatus = EBdStatus.None;
                                }
                                //Thread.Sleep(100);
                            }

                        }));

                        TaskConv.Add(Task.Run(() =>
                        {
                            while (!cToken.IsCancellationRequested)
                            {
                                if (TFRConv.BdStatus == EBdStatus.None)
                                    if (!TFRConv.SmemaLoadIn()) ErrorTokenStop();
                                //Thread.Sleep(100);
                            }
                        }));

                        TaskConv.Add(Task.Run(() =>
                        {
                            while (!cToken.IsCancellationRequested)
                            {
                                if (TFRConv.BdStatus == EBdStatus.WaitUnload)
                                {
                                    if (!TFRConv.SmemaSendOut()) ErrorTokenStop();
                                }
                                //Thread.Sleep(100);
                            }
                        }));


                        return true;
                    }
                    catch (Exception e)
                    {
                        ErrorTokenStop();
                        return false;
                    }
                
                #endregion

                #region SMEMA left to rigth parallel 
                case EConvInterface.SMEMA_LR_PARALLEL:

                    try
                    {
 

                        TaskConv.Add(Task.Run(() =>
                        {
                            while (!cToken.IsCancellationRequested)
                            {
                                if (TFLConv.SensInPsnt.Status && TFRConv.BdStatus == EBdStatus.None && TFLConv.BdStatus == EBdStatus.None)
                                {
                                        if (!TFRConv.LoadLeftToPro()) ErrorTokenStop();

                                } 
                                else if(TFLConv.SensInPsnt.Status && TFLConv.BdStatus == EBdStatus.None && TFRConv.BdStatus != EBdStatus.None)
                                {
                                    if (!TFLConv.LoadToPro()) ErrorTokenStop();
                                }
                                Thread.Sleep(100);
                            }
                        }));

                        TaskConv.Add(Task.Run(() =>
                        {
                            while (!cToken.IsCancellationRequested)
                            {
                                if (TFRConv.BdStatus == EBdStatus.Ready)
                                {
                                    if (!TFRConv.ReturnToRight()) ErrorTokenStop();
                                }
                                else if(TFLConv.BdStatus == EBdStatus.Ready && TFRConv.BdStatus == EBdStatus.None)
                                {
                                    if (!TFLConv.ReturnToRight()) ErrorTokenStop();
                                }
                                else if(TFRConv.BdStatus == EBdStatus.WaitUnload && !TFRConv.SensOutPsnt.Status)
                                {
                                    TFRConv.BdStatus = EBdStatus.None;
                                }
                                Thread.Sleep(100);
                            }

                        }));

                        TaskConv.Add(Task.Run(() => { if (!SmemaLoadIn(cToken.Token)) ErrorTokenStop(); }));

                        TaskConv.Add(Task.Run(() =>
                        {
                            while (!cToken.IsCancellationRequested)
                            {
                                if (!SmemaLoadOut(cToken.Token)) ErrorTokenStop();
                                Thread.Sleep(100);
                            }
                        }));

                        return true;
                    }
                    catch (Exception e)
                    {
                        ErrorTokenStop();
                        return false;
                    }
                #endregion

                case EConvInterface.CONV_BY_PASS:
                    TFLConv.BdStatus = EBdStatus.WaitProcess;
                    TFRConv.BdStatus = EBdStatus.WaitProcess;
                    return true;
            }
            return false;
            
            bool SmemaLoadIn(CancellationToken Token)
            {
                while(!Token.IsCancellationRequested)
                {
                    if(TFLConv.BdStatus == EBdStatus.None)
                    {
                        if (!TFLConv.SmemaLoadIn(false))return false;
                    }
                    Thread.Sleep(500);
                }
                return true;
            }

            bool SmemaLoadOut(CancellationToken Token)
            {
                while (!Token.IsCancellationRequested)
                {
                    if (TFRConv.BdStatus == EBdStatus.WaitUnload)
                    {
                        if (!TFRConv.SmemaSendOut()) return false;
                    }
                    Thread.Sleep(500);
                }
                return true;
            }

            void ErrorTokenStop()
            {
                cToken.Cancel();
            }
        }

        public static void StopConv()
        {
            if(cToken !=null)
            cToken.Cancel();
        }

        public static void AutoBdReady()
        {
            var sw = Stopwatch.StartNew();
            var SW2 = Stopwatch.StartNew();
            var sw3 = Stopwatch.StartNew();
            switch (GSystemCfg.Conveyor.Conv_Interface)
            {
                case EConvInterface.SMEMA_LR_SERIAL:
                     sw = Stopwatch.StartNew();
                    while (TFLConv.BdStatus == EBdStatus.WaitProcess)
                    {
                        if (sw.ElapsedMilliseconds >= testRunTime)
                            TFLConv.BdStatus = EBdStatus.Ready;
                    }
                     SW2 = Stopwatch.StartNew();
                    while (TFRConv.BdStatus == EBdStatus.WaitProcess)
                    {
                        if (SW2.ElapsedMilliseconds >= testRunTime)
                            TFRConv.BdStatus = EBdStatus.Ready;
                    }
                     sw3 = Stopwatch.StartNew();
                    while(TFRConv.BdStatus ==EBdStatus.WaitUnload)
                    {
                        //if (SW2.ElapsedMilliseconds >= testRunTime)
                            //if (!TFRConv.ReturnToLeft()) return; 
                            //else
                            //    TFLConv.BdStatus = EBdStatus.None;
                    }
                    break;

                case EConvInterface.SMEMA_LR_PARALLEL:
                     sw = Stopwatch.StartNew();
                    while (TFLConv.BdStatus == EBdStatus.WaitProcess)
                    {
                        if (sw.ElapsedMilliseconds >= testRunTime)
                            TFLConv.BdStatus = EBdStatus.Ready;
                    }
                     SW2 = Stopwatch.StartNew();
                    while (TFRConv.BdStatus == EBdStatus.WaitProcess)
                    {
                        if (SW2.ElapsedMilliseconds >= testRunTime)
                            TFRConv.BdStatus = EBdStatus.Ready;
                    }
                    break;

                case EConvInterface.SMEMA_LR_MIRROR:
                    sw = Stopwatch.StartNew();
                    while (TFLConv.BdStatus == EBdStatus.WaitProcess)
                    {
                        if (sw.ElapsedMilliseconds >= testRunTime)
                            TFLConv.BdStatus = EBdStatus.Ready;
                    }
                    sw = Stopwatch.StartNew();
                    while (TFLConv.BdStatus == EBdStatus.WaitUnload)
                    {
                        if (sw.ElapsedMilliseconds >= testRunTime)
                            TFLConv.BdStatus = EBdStatus.None;
                    }

                    SW2 = Stopwatch.StartNew();
                    while (TFRConv.BdStatus == EBdStatus.WaitProcess)
                    {
                        if (SW2.ElapsedMilliseconds >= testRunTime)
                            TFRConv.BdStatus = EBdStatus.Ready;
                    }

                    SW2 = Stopwatch.StartNew();
                    while (TFRConv.BdStatus == EBdStatus.WaitUnload)
                    {
                        if (SW2.ElapsedMilliseconds >= testRunTime)
                            TFRConv.BdStatus = EBdStatus.None;
                    }
                    break;


            }
        }

        public static void AutoBdWaitUnloadtoNone()
        {
            var sw = Stopwatch.StartNew();
            var SW2 = Stopwatch.StartNew();
            switch (GSystemCfg.Conveyor.Conv_Interface)
            {
                              
                case EConvInterface.SMEMA_LR_SERIAL:
                case EConvInterface.SMEMA_LR_PARALLEL:
     
                case EConvInterface.SMEMA_LR_MIRROR:
                    sw = Stopwatch.StartNew();
                    
                    sw = Stopwatch.StartNew();
                    while (TFLConv.BdStatus == EBdStatus.WaitUnload)
                    {
                        if (sw.ElapsedMilliseconds >= testRunTime)
                            TFLConv.BdStatus = EBdStatus.None;
                    }

                    SW2 = Stopwatch.StartNew();
                    while (TFRConv.BdStatus == EBdStatus.WaitUnload)
                    {
                        if (SW2.ElapsedMilliseconds >= testRunTime)
                            TFRConv.BdStatus = EBdStatus.None;
                    }
                    break;

                case EConvInterface.CONV_BY_PASS:
                    while (TFLConv.BdStatus == EBdStatus.Ready)
                    {
                        TFLConv.BdStatus = EBdStatus.WaitProcess;
                    }

                    while (TFRConv.BdStatus == EBdStatus.Ready)
                    {
                        TFRConv.BdStatus = EBdStatus.WaitProcess;
                    }
                    break;
            }
        }
    }

}


