using System;
using System.IO.Ports;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.ComponentModel;


namespace NagaW
{
    internal class TEWeigh
    {
        public static SerialPort Port = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);

        public static bool Open(string comport)
        {
            
            Port.PortName = comport;

            if (!Port.IsOpen)
            {
                try
                {
                    Port.WriteTimeout = 500;
                    Port.ReadTimeout = 1000;
                    Port.DtrEnable = true;
                    Port.Open();
                }
                catch
                {
                    return false;
                    throw;
                }
            }
            return true;
        }
        public static bool Open()
        {
            return Open(GSystemCfg.Weight.Weights[0].Comport.ToString());
        }

        public static bool Close()
        {
            try
            {
                Port.Close();
            }
            catch
            {
                return false;
                throw;
            }
            return true;
        }
        public static bool IsOpen
        {
            get
            {
                return Port.IsOpen;
            }
        }

        public static bool TxMsg(string msg)
        {
            try
            {
                if (!Port.IsOpen) return false;

                string txMsg = msg + "\r\n";
                Port.Write(txMsg);
                Port.DiscardOutBuffer();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("TxMsg " + msg + " " + ex.Message.ToString());
            }
        }
        public static bool RxMsg(ref string msg)
        {
            try
            {
                if (!Port.IsOpen) return false;

                msg = "";
                msg = Port.ReadLine();
                Port.DiscardInBuffer();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("RxMsg " + (char)9 + ex.Message.ToString());
            }
        }
    }

    class TFWeightScale
    {
        internal class MT_SICS_VMS
        {
            public const string SET_UNIT = "M21"; 
            public const string READ_STABLE = "S";
            public const string READ_IMME = "SI";
            public const string TARE = "T";
            public const string ZERO = "Z";
        }

        public static bool Open(string Comport)
        {
            if (!TEWeigh.Open(Comport))
            {
                GAlarm.Prompt(EAlarm.WEIGHT_SCALE_CONNECT_FAIL);
                return false;
            }
            return true;
        }
        public static bool Open()
        {
            return Open(GSystemCfg.Weight.Weights[0].Comport.ToString());
        }
        public static bool Close()
        {
            TEWeigh.Close();
            return true;
        }
        public static bool IsOpen
        {
            get
            {
                return TEWeigh.IsOpen;
            }
        }

        public static bool PromptError(string rxMsg)
        {
            if (rxMsg.StartsWith("Z I") || rxMsg.StartsWith("T I") || rxMsg.StartsWith("S I"))
            {
                //throw new Exception("Balance busy or timeOut.");
                GAlarm.Prompt(EAlarm.WEIGHT_SCALE_BUSY);

            }
            if (rxMsg.StartsWith("S L"))
            {
                //throw new Exception("Incorrect parameter.");
                GAlarm.Prompt(EAlarm.WEIGHT_SCALE_INCORRECT_PARA);

            }
            if (rxMsg.StartsWith("T +") || rxMsg.StartsWith("S +"))
            {
                //throw new Exception("Balance overload.");
                GAlarm.Prompt(EAlarm.WEIGHT_SCALE_BALANCE_OVERLOAD);

            }
            if (rxMsg.StartsWith("T -") || rxMsg.StartsWith("S -"))
            {
                //throw new Exception("Balance underload.");
                GAlarm.Prompt(EAlarm.WEIGHT_SCALE_BALANCE_UNDERLOAD);

            }
            if (rxMsg.StartsWith("Z +") || rxMsg.StartsWith("Z -"))
            {
                //throw new Exception("Upper limit of zero setting range exceeded.");
                GAlarm.Prompt(EAlarm.WEIGHT_SCALE_ZERO_EXCEED_LIMIT);

            }
            return false;
        }
        public static bool Tare()
        {
            try
            {
                TEWeigh.Port.DiscardInBuffer();
                TEWeigh.TxMsg(MT_SICS_VMS.TARE);
                TEWeigh.Port.ReadTimeout = 1000;
                string rxMsg = "";
                TEWeigh.RxMsg(ref rxMsg);

                if (rxMsg.StartsWith("T S")) return true;//Success

                PromptError(rxMsg);
                return false;
            }
            catch (Exception)
            {
                //throw new Exception("Weight Tare " + ex.Message.ToString());
                GAlarm.Prompt(EAlarm.WEIGHT_SCALE_OTHER_ERROR_REINIT);

                return false;
            }
        }
        public static bool Zero()
        {

            try
            {
                TEWeigh.Port.DiscardInBuffer();
                TEWeigh.TxMsg(MT_SICS_VMS.ZERO);
                TEWeigh.Port.ReadTimeout = 10000;
                string rxMsg = "";
                TEWeigh.RxMsg(ref rxMsg);

                if (rxMsg.StartsWith("Z A")) return true;//Success

                PromptError(rxMsg);
                return false;
            }
            catch (Exception)
            {
                //throw new Exception("Weight Zero " + ex.Message.ToString());
                GAlarm.Prompt(EAlarm.WEIGHT_SCALE_OTHER_ERROR_REINIT);

                return false;
            }
        }
        public static bool ReadStable(ref double gValue)
        {
            try
            {
                TEWeigh.Port.DiscardInBuffer();
                TEWeigh.TxMsg(MT_SICS_VMS.READ_STABLE);
                TEWeigh.Port.ReadTimeout = 20000;
                string rxMsg = "";
                TEWeigh.RxMsg(ref rxMsg);

                //sample response
                //"S S      100.00 g"
                //"S I" -> Cmd not execute
                //"S L" -> Incorrect Para
                //"S +" -> Overload
                //"S -" -> Underload
                //"S S <ErrorCode>" -> Error Code

                if (rxMsg.StartsWith("S S"))
                {
                    if (rxMsg.Contains("Error"))
                        throw new Exception("Error " + rxMsg + ".");
                    else
                    {
                        string S = rxMsg.ToUpper();
                        S = S.Replace("S", "");
                        S = S.Replace("D", "");
                        S = S.Replace("G", "");
                        S = S.Replace(" ", "");

                        if (!double.TryParse(S, out gValue))
                        {
                            GAlarm.Prompt(EAlarm.WEIGHT_SCALE_CONVERT_ERROR);
                            return false;
                        }
                        return true;
                    }
                }

                PromptError(rxMsg);
                return false;
            }
            catch (Exception)
            {
                //throw new Exception("Weight ReadStable " + Ex.Message.ToString());
                GAlarm.Prompt(EAlarm.WEIGHT_SCALE_OTHER_ERROR_REINIT);

                return false;
            }
        }
        public static bool ReadImme(ref double gValue)
        {
            try
            {
                TEWeigh.Port.DiscardInBuffer();
                TEWeigh.TxMsg(MT_SICS_VMS.READ_IMME);
                TEWeigh.Port.ReadTimeout = 1000;
                string rxMsg = "";
                TEWeigh.RxMsg(ref rxMsg);

                //sample response
                //"S S      100.00 g"
                //"S I" -> Cmd not execute
                //"S L" -> Incorrect Para
                //"S +" -> Overload
                //"S -" -> Underload
                //"S S <ErrorCode>" -> Error Code

                if (rxMsg.StartsWith("S S") || rxMsg.StartsWith("S D"))
                {
                    if (rxMsg.Contains("Error"))
                        throw new Exception("Error " + rxMsg + ".");
                    else
                    {
                        string S = rxMsg.ToUpper();
                        S = S.Replace("S", "");
                        S = S.Replace("D", "");
                        S = S.Replace("G", "");
                        S = S.Replace(" ", "");

                        if (!double.TryParse(S, out gValue))
                        {
                            GAlarm.Prompt(EAlarm.WEIGHT_SCALE_CONVERT_ERROR);
                            return false;
                        }
                        return true;
                    }
                }

                PromptError(rxMsg);
                return false;
            }
            catch (Exception)
            {
                //throw new Exception("Weight ReadImme:" + Ex.Message.ToString());
                GAlarm.Prompt(EAlarm.WEIGHT_SCALE_OTHER_ERROR_REINIT);
                return false;
            }
        }
    }
    class TCWeighMeas
    {
        public static bool Stop = false;
        public static BindingList<double> Result = new BindingList<double>();

        public static Mutex Mutex = new Mutex();
        public static bool Execute(int gantryIdx, int dotsPerSample = 0, int SampleCount = 0)
        {
            var gantry = gantryIdx is 0 ? TFGantry.GantrySetup : TFGantry.GantryRight;
            Stop = false;
            Result.Clear();

            var dispCtrl = GSystemCfg.Pump.Pumps[gantryIdx];

            PointXYZ w_pos = GSetupPara.Weighing.Pos[gantryIdx, (int)GSystemCfg.Pump.Pumps[gantryIdx].PumpType];
            int w_startwait = GProcessPara.Weighing.StartWait[gantryIdx].Value;
            int w_endwait = GProcessPara.Weighing.EndWait[gantryIdx].Value;
            int w_dotwait_ms = GProcessPara.Weighing.DotWait[gantryIdx].Value;
            int w_readwait = GProcessPara.Weighing.ReadWait[gantryIdx].Value;
            double w_zupdist = GProcessPara.Weighing.ZUpDist[gantryIdx].Value;
            double w_zupvel = GProcessPara.Weighing.ZUpVel[gantryIdx].Value;
            if (dotsPerSample <= 0) dotsPerSample = GProcessPara.Weighing.DotPerSample[gantryIdx].Value;
            if (SampleCount <= 0) SampleCount = GProcessPara.Weighing.SampleCount[gantryIdx].Value;


            try
            {
                Mutex.WaitOne();
                if (!gantry.MoveOpXYAbs(w_pos.XYPos)) return false;

                switch (dispCtrl.PumpType)
                {
                    case EPumpType.VERMES_3280:
                        {
                            var setup = new Vermes3280_Param(GRecipes.Vermes_Setups[gantryIdx]);
                            setup.Pulses.Value = dotsPerSample;
                            setup.Delay.Value = w_dotwait_ms;

                            if (!TFPump.Vermes_Pump[gantryIdx].TriggerAset(setup)) return false;

                            GMotDef.Outputs[(int)dispCtrl.FPressDO].Status = true;
                            break;
                        }
                }

                while (Result.Count < SampleCount)
                {
                    if (Stop) goto _stop;

                    gantry.ZAxis.SpeedProfile(GProcessPara.Operation.GZSpeed);
                    if (!gantry.MoveOpZAbs(w_pos.Z)) return false;
                    Thread.Sleep(w_startwait);

                    double beforeW = 0;
                    Thread.Sleep(w_readwait); ;
                    if (!TFWeightScale.ReadStable(ref beforeW)) goto _stop;

                    switch (dispCtrl.PumpType)
                    {
                        case EPumpType.VERMES_3280:
                            {
                                var trig = GMotDef.Outputs[(int)dispCtrl.DispDO];
                                trig.Status = true;
                                while (!GMotDef.Inputs[(int)dispCtrl.DispDI].Status) Thread.Sleep(0);
                                trig.Status = false;

                                break;
                            }
                        //case EPumpType.PP4:
                        //    {
                        //        var setup = new PP4_Setup(GRecipe.PP4_Setups[gantryIdx]);

                        //        var instantcount = 0;
                        //        while (instantcount++ > dotsPerSample)
                        //        {
                        //            if (!TFPump.PP4.CheckStrokeThenSingleShot(setup, dualhead)) goto _stop;
                        //            if (w_dotwait_ms > 0) Thread.Sleep(w_dotwait_ms);
                        //        }

                        //        break;
                        //    }
                        case EPumpType.SP:
                            {
                                //var setup = new SP_Setup(GRecipe.SP_Setups[headno]);

                                //var instantcount = 0;
                                //while (instantcount++ > dotsPerSample)
                                //{
                                //    TFPump.SP.Single_Shot(setup, headno);
                                //    if (w_dotwait_ms > 0) Thread.Sleep(w_dotwait_ms);
                                //}
                                break;
                            }
                        case EPumpType.HM:
                            {
                                //no disp
                                break;
                            }
                    }

                    Thread.Sleep(w_endwait);

                    if (w_zupdist > 0)
                    {

                        gantry.ZAxis.SetParam(0, w_zupvel, 100);
                        if (!gantry.MoveOpZRel(w_zupdist)) return false;
                    }

                    double afterW = 0;
                    Thread.Sleep(w_readwait);
                    if (!TFWeightScale.ReadStable(ref afterW)) goto _stop;

                    double value = 1000 * ((afterW - beforeW) / dotsPerSample);
                    Result.Add(value);
                }

            _stop:

                GMotDef.Outputs[(int)dispCtrl.FPressDO].Status = false;
                GMotDef.Outputs[(int)dispCtrl.PPressDO].Status = false;
                GMotDef.Outputs[(int)dispCtrl.VacDO].Status = true;

                switch (dispCtrl.PumpType)
                {
                    case EPumpType.VERMES_3280:
                        {
                            TFPump.Vermes_Pump[gantryIdx].TriggerAset(GRecipes.Vermes_Setups[gantryIdx]);
                            break;
                        }
                }

                gantry.ZAxis.SpeedProfile(GProcessPara.Operation.GZSpeed);
                if (!gantry.MoveOpZAbs(0)) return false;
                return true;
            }
            catch (Exception ex)
            {
                GLog.WriteException(ex);
                MsgBox.ShowDialog(ex.Message.ToString());
                return false;
            }
            finally
            {
                Mutex.ReleaseMutex();
            }
        }
    }
    class TCWeighCal
    {
        public enum EWeighCalMode { Mass, MassFlowRate }

        //  ********************[Calibration Formula]********************

        //  [Mass Calibration]
        //  Q = Vt; Q = volumetric flow rate, V = volume, t = time
        //  Q = vA; v = flow velocity, A = cross-section area

        //  [Mass Flow Rate Calibration]
        //  m_dot = dm/t;   m_dot = massflowrate, dm = rate of change of mass, t = time ------(1)
        //  v = d/t;                                                                    ------(2)
        //  sub (2) in (1)
        //  m_dot = dm/(d/v);   if(dm = m2-m1, m1 =0, dm ~ m2)   
        //  m_dot = (m*v)d;
        //  to find v, v = m_dot*d/m;
        public int GantryIdx { get; private set; } = 0;
        public bool Stop = false;
        public BindingList<TEWeighData> Result = new BindingList<TEWeighData>();

        public bool Execute(int dotPerSample, int SampleCount, EWeighCalMode calMode, ref TCmd cmd, ref double flowrate)
        {
            var gantry = GantryIdx is 0 ? TFGantry.GantryLeft : TFGantry.GantryRight;
            var gantryidx = gantry.Index;

            Stop = false;
            Result.Clear();

            #region Init variables
            if (dotPerSample <= 0) dotPerSample = GProcessPara.Weighing.DotPerSample[gantryidx].Value;
            if (SampleCount <= 0) SampleCount = GProcessPara.Weighing.SampleCount[gantryidx].Value;

            var dispCtrl = GSystemCfg.Pump.Pumps[gantryidx];
            var dualhead = new bool[] { gantryidx is 0, gantryidx is 1 };

            //PP4_Setup pp4_setup = new PP4_Setup(GRecipes.PP4_Setups[headno]);
            Vermes3280_Param vermes_setup = new Vermes3280_Param(GRecipes.Vermes_Setups[gantryidx]);
            HM_Param hm_setup = new HM_Param(GRecipes.HM_Setups[gantryidx]);
            SP_Param sp_setup = new SP_Param(GRecipes.SP_Setups[gantryidx]);

            switch (cmd.Cmd)
            {
                case ECmd.HM_SETUP: hm_setup = new HM_Param(cmd.Para); break;
                //case ECmd.PP4_SETUP: pp4_setup = new PP4_Setup(pp4_setup, cmd.Para); break;
                case ECmd.SP_SETUP: sp_setup = new SP_Param(cmd.Para); break;
                case ECmd.VERMES_3280_SETUP: vermes_setup = new Vermes3280_Param(cmd.Para); break;
                default: return false;
            }

            PointXYZ w_pos = GSetupPara.Weighing.Pos[gantryidx, (int)GSystemCfg.Pump.Pumps[gantryidx].PumpType];

            int w_startwait = GProcessPara.Weighing.StartWait[gantryidx].Value;
            int w_endwait = GProcessPara.Weighing.EndWait[gantryidx].Value;
            int w_dotwait_ms = GProcessPara.Weighing.DotWait[gantryidx].Value;
            int w_readwait = GProcessPara.Weighing.ReadWait[gantryidx].Value;
            double w_zupdist = GProcessPara.Weighing.ZUpDist[gantryidx].Value;
            double w_zupvel = GProcessPara.Weighing.ZUpVel[gantryidx].Value;

            double target_mass = GProcessPara.Weighing.Target_Mass.Value;

            double tune_percentage_Min = GProcessPara.Weighing.Tune_Percentage_LowerLimit.Value;
            double tune_percentage_Max = GProcessPara.Weighing.Tune_Percentage_UpperLimit.Value;

            var cleanAF = GProcessPara.Weighing.CleanAfterFill.Value;
            var flushAF = GProcessPara.Weighing.FlushAfterFill.Value;
            var purgeAF = GProcessPara.Weighing.PurgeAfterFill.Value;

            //var zAxis = GMotDef.DualZAxis[gantryidx];

            //mass cal
            var pp4_material_density = new List<double>();

            //FlowRate Cal
            var mdot_dispTime = GProcessPara.Weighing.DispTime_DotM.Value;
            #endregion

            try
            {
                //if (!TFGantry.ChooseHead(gantryidx)) return false;
                if (!gantry.MoveOpXYAbs(w_pos.XYPos)) return false;

                while (Result.Count < SampleCount)
                {
                    #region Processing
                    var wdata = new TEWeighData();
                    //Result.Add(wdata);

                    var instantcount = 0;
                
                
                _Process:
                    if (Stop) goto _stop;

                    //Pre Set Profile
                    switch (dispCtrl.PumpType)
                    {
                        case EPumpType.VERMES_3280:
                            {
                                vermes_setup = vermes_setup is null ? new Vermes3280_Param(GRecipes.Vermes_Setups[gantryidx]) : new Vermes3280_Param(vermes_setup);
                                vermes_setup.Pulses.Value = calMode > EWeighCalMode.Mass ? 0 : dotPerSample;
                                vermes_setup.Delay.Value = calMode > EWeighCalMode.Mass ? 0.1 : w_dotwait_ms;

                                if (!TFPump.Vermes_Pump[gantryidx].TriggerAset(vermes_setup)) return false;
                                if (!TFPressCtrl.FPress[gantryidx].Set(vermes_setup.FPress.Value)) return false;

                                GMotDef.Outputs[(int)dispCtrl.FPressDO].Status = true;
                                break;
                            }
                    }

                    //if (!TFGantry.GZSetParam(zAxis)) return false;
                    //if (!TFGantry.GZGoto(zAxis, w_pos.Z)) return false;

                    if (!gantry.MoveOpZAbs(w_pos.Z)) ;
                    Thread.Sleep(w_startwait);

                    double beforeW = 0;
                    Thread.Sleep(w_readwait); ;
                    if (!TFWeightScale.ReadStable(ref beforeW)) goto _stop;

                    //Disp
                    switch (dispCtrl.PumpType)
                    {
                        case EPumpType.VERMES_3280:
                            {
                                var trig = GMotDef.Outputs[(int)dispCtrl.DispDO];

                                switch (calMode)
                                {
                                    case EWeighCalMode.Mass:
                                        {
                                            trig.Status = true;
                                            while (!GMotDef.Inputs[(int)dispCtrl.DispDI].Status) Thread.Sleep(0);
                                            trig.Status = false;

                                            break;
                                        }
                                    case EWeighCalMode.MassFlowRate:
                                        {
                                            trig.Status = true;
                                            Thread.Sleep(mdot_dispTime);
                                            trig.Status = false;
                                            break;
                                        }
                                }
                                break;
                            }
                            //case EPumpType.PP4:
                            //    {
                            //        //var instantcount = 0;
                            //        while (instantcount++ < dotPerSample)
                            //        {
                            //            if (TFPump.PP4.CheckStrokeThenFill(pp4_setup, dualhead))
                            //            {
                            //                if (flushAF > 0)
                            //                {
                            //                    if (!TCNeedleFunc.Execute(ENeedleCleanMode.Flush, dualhead, 0, 0, 0, 0, 0, flushAF)) return false;
                            //                }
                            //                if (cleanAF > 0)
                            //                {
                            //                    if (!TCNeedleFunc.Execute(ENeedleCleanMode.VacClean, dualhead, 0, 0, 0, 0, 0, cleanAF)) return false;
                            //                }
                            //                if (purgeAF > 0)
                            //                {
                            //                    if (!TCNeedleFunc.Execute(ENeedleCleanMode.Purge, dualhead, 0, 0, 0, 0, 0, purgeAF)) return false;
                            //                }

                            //                if (cleanAF + flushAF + purgeAF > 0)
                            //                {
                            //                    if (!TFGantry.ChooseHead(0)) return false;
                            //                    if (!TFGantry.GXYMoveAbs(w_pos.XYPos)) return false;
                            //                    goto _Process;
                            //                }
                            //            }
                            //            else return false;

                            //            if (!TFPump.PP4.SingleShot(pp4_setup, dualhead)) goto _stop;
                            //            if (w_dotwait_ms > 0) Thread.Sleep(w_dotwait_ms);
                            //        }
                            //        break;
                            //    }
                    }

                    Thread.Sleep(w_endwait);

                    if (w_zupdist > 0)
                    {
                        //if (!TFGantry.GZSetParam(zAxis, 0, w_zupvel, 100)) return false;
                        //if (!TFGantry.GZMoveRel(zAxis, w_zupdist)) return false;
                        //while (zAxis.Busy) Thread.Sleep(0);

                        gantry.ZAxis.SetParam(0, w_zupvel, 100);
                        if (!gantry.MoveOpZRel(w_zupdist)) return false;
                    }

                    double afterW = 0;
                    Thread.Sleep(w_readwait);
                    if (!TFWeightScale.ReadStable(ref afterW)) goto _stop;


                    double actual_mass = 1000 * ((afterW - beforeW) / dotPerSample);

                    wdata.ActualMass = actual_mass;
                    wdata.CumulativeMass = afterW;
                    //wdata.Mean = Result.Select(x => x.ActualMass).Average();
                    //wdata.SD = Math.Sqrt(Math.Pow(wdata.ActualMass - wdata.Mean, 2) / Result.Count);


                    if (calMode > EWeighCalMode.Mass)
                    {
                        double actual_mdot = actual_mass / (mdot_dispTime / 1000);
                        wdata.FlowRate = actual_mdot;
                    }
                    #endregion

                    #region Calibrate

                    switch (calMode)
                    {
                        #region Mass
                        case EWeighCalMode.Mass:
                            {
                                switch (dispCtrl.PumpType)
                                {
                                    case EPumpType.SP:
                                    case EPumpType.TP:
                                        {
                                            wdata.SV_Before = sp_setup.FPress.Value;

                                            var fpress = sp_setup.FPress.Value;
                                            var minfpress = fpress - (fpress * tune_percentage_Min / 100);
                                            var maxfpress = fpress + (fpress * tune_percentage_Max / 100);
                                            fpress = actual_mass > target_mass ? (fpress + maxfpress) / 2 : (fpress + minfpress) / 2;
                                            sp_setup.FPress.Value = fpress;

                                            wdata.SV_After = fpress;

                                            cmd.Para[6] = fpress;

                                            GLog.LogProcess($"Cal{dispCtrl.PumpType}_{gantryidx} SampleCount:{Result.Count}/{SampleCount} {nameof(fpress)} [{wdata.SV_Before} > {wdata.SV_After}]");
                                            break;
                                        }
                                    case EPumpType.VERMES_3280:
                                        {
                                            wdata.SV_Before = vermes_setup.FPress.Value;

                                            var fpress = vermes_setup.FPress.Value;
                                            var minfpress = fpress - (fpress * tune_percentage_Min / 100);
                                            var maxfpress = fpress + (fpress * tune_percentage_Max / 100);
                                            fpress = actual_mass > target_mass ? (fpress + maxfpress) / 2 : (fpress + minfpress) / 2;
                                            vermes_setup.FPress.Value = fpress;

                                            wdata.SV_After = fpress;

                                            cmd.Para[6] = fpress;
                                            GLog.LogProcess($"Cal{dispCtrl.PumpType}_{gantryidx} SampleCount:{Result.Count}/{SampleCount} {nameof(fpress)} [{wdata.SV_Before} > {wdata.SV_After}]");
                                            break;
                                        }
                                    case EPumpType.HM:
                                        {
                                            break;
                                        }
                                        //case EPumpType.PP4:
                                        //    {
                                        //        // density formula
                                        //        // p = m/v
                                        //        // when p = constant, m1/v1 = m2=v2
                                        //        // get average of density to tune the target mass

                                        //        wdata.SV_Before = pp4_setup.DispAmount.Value;

                                        //        var dispAmt = pp4_setup.DispAmount.Value;
                                        //        pp4_material_density.Add(actual_mass / dispAmt);

                                        //        var volume = target_mass / pp4_material_density.Average();
                                        //        volume = Math.Min(volume, dispAmt + (dispAmt * tune_percentage_Max / 100));
                                        //        volume = Math.Max(volume, dispAmt - (dispAmt * tune_percentage_Min / 100));

                                        //        pp4_setup.DispAmount.Value = volume;
                                        //        wdata.SV_After = volume;

                                        //        cmd.Para[0] = volume;
                                        //        GLog.LogProcess($"Cal{dispCtrl.PumpType}_{gantryidx} SampleCount:{Result.Count}/{SampleCount} {nameof(volume)} [{wdata.SV_Before} > {wdata.SV_After}]");
                                        //        break;
                                        //    }
                                }

                                break;
                            }
                        #endregion

                        #region MassFlowRate
                        case EWeighCalMode.MassFlowRate:
                            {
                                break;
                            }
                            #endregion
                    }
                    #endregion

                    Result.Add(wdata);
                }

            _stop:

                #region Stop
                GMotDef.Outputs[(int)dispCtrl.FPressDO].Status = false;
                GMotDef.Outputs[(int)dispCtrl.PPressDO].Status = false;

                gantry.ZAxis.SpeedProfile(GProcessPara.Operation.GZSpeed);

                if (!gantry.MoveOpZAbs(0)) return false;

                switch (dispCtrl.PumpType)
                {
                    case EPumpType.VERMES_3280:
                        {
                            if (!TFPump.Vermes_Pump[gantryidx].TriggerAset(GRecipes.Vermes_Setups[gantryidx])) return false;
                            break;
                        }
                }

                GMotDef.Outputs[(int)dispCtrl.VacDO].Status = true;


                #endregion

                flowrate = Result.Count is 0 ? 0 : Result.Select(x => x.FlowRate).Average();

                return true;
            }
            catch (Exception ex)
            {
                GLog.WriteException(ex);
                MsgBox.ShowDialog(ex.Message.ToString());
                return false;
            }
        }
        public TCWeighCal(int gantryIdx)
        {
            GantryIdx = gantryIdx;
        }
        public TCWeighCal[] WeighCals = new TCWeighCal[2] { new TCWeighCal(TFGantry.GantryLeft.Index), new TCWeighCal(TFGantry.GantryRight.Index) };
    }



    class TEWeighData
    {
        public double ActualMass { get; set; }
        public double CumulativeMass { get; set; }
        public double SV_Before { get; set; }
        public double SV_After { get; set; }
        public EUnit EUnit { get; set; }
        public double FlowRate { get; set; }

        public double Mean { get; set; }
        public double Standard_Deviation { get; set; }
        public TEWeighData()
        {
        }
        public TEWeighData(TEWeighData weighData)
        {
            this.ActualMass = weighData.ActualMass;
            this.CumulativeMass = weighData.CumulativeMass;
            this.SV_Before = weighData.SV_Before;
            this.SV_After = weighData.SV_After;
            this.EUnit = weighData.EUnit;
            this.FlowRate = weighData.FlowRate;
        }

        public override string ToString()
        {
            return ActualMass.ToString() + " " + EUnit.ToStringForDisplay();
        }
    }

}
