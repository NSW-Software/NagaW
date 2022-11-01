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

    public enum EWeighMode { Measure, Calibration }
    public enum EWeighType { Mass, MassFlowRate }
    public enum EWeighLearnType { Static_FlowRate, Adaptive_FlowRate }

    public enum EWeighSVParam { FPress, FPressPPress, RisingTime, OpeningTime, FallingTime, NeedleLift, Delay, DispRPM }

    class TCWeighFunc
    {
        static Mutex Mtx = new Mutex();

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
        public bool Finish = false;
        public BindingList<TEWeighData> Result = new BindingList<TEWeighData>();

        public bool Execute(int dotPerSample, int SampleCount, EWeighType wType, EWeighMode wMode, ref TCmd cmd, EWeighSVParam wSVPara = EWeighSVParam.FPress)
        {
            var gantry = GantryIdx is 0 ? TFGantry.GantrySetup : TFGantry.GantryRight;
            var gantryidx = gantry.Index;

            Finish = false;
            Stop = false;
            Result.Clear();

            #region Init variables

            if (dotPerSample <= 0) dotPerSample = GProcessPara.Weighing.DotPerSample[gantryidx].Value;
            if (SampleCount <= 0) SampleCount = GProcessPara.Weighing.SampleCount[gantryidx].Value;
            var repeatCount = GProcessPara.Weighing.RepeatCount[gantryidx].Value is 0 ? 1 : GProcessPara.Weighing.RepeatCount[gantryidx].Value;

            var dispCtrl = GSystemCfg.Pump.Pumps[gantryidx];
            var dualhead = new bool[] { gantryidx is 0, gantryidx is 1 };

            TCmd Wcmd = new TCmd(cmd);

            Vermes3280_Param vermes_setup = new Vermes3280_Param(GRecipes.Vermes_Setups[gantryidx]);
            HM_Param hm_setup = new HM_Param(GRecipes.HM_Setups[gantryidx]);
            SP_Param sp_setup = new SP_Param(GRecipes.SP_Setups[gantryidx]);

            double tunevariable = 0;

            switch (Wcmd.Cmd)
            {
                case ECmd.HM_SETUP:
                    {
                        hm_setup = new HM_Param(Wcmd.Para);
                        tunevariable = hm_setup.FPress.Value;
                        break;
                    }
                case ECmd.TP_SETUP:
                case ECmd.SPLITE_SETUP:
                case ECmd.SP_SETUP:
                    {
                        sp_setup = new SP_Param(Wcmd.Para);
                        tunevariable = sp_setup.FPress.Value;
                        break;
                    }
                case ECmd.VERMES_3280_SETUP:
                    {
                        vermes_setup = new Vermes3280_Param(Wcmd.Para);
                        tunevariable = vermes_setup.FPress.Value;
                    }
                    break;
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
            double target_massFR = GProcessPara.Weighing.Target_FlowRate.Value;

            double mass_range = GProcessPara.Weighing.Target_Mass_Range.Value;
            double massFR_range = GProcessPara.Weighing.Target_FlowRate_Range.Value;

            double tune_percentage_Min = GProcessPara.Weighing.Tune_Percentage_LowerLimit.Value;
            double tune_percentage_Max = GProcessPara.Weighing.Tune_Percentage_UpperLimit.Value;

            var cleanAF = GProcessPara.Weighing.CleanAfterFill.Value;
            var flushAF = GProcessPara.Weighing.FlushAfterFill.Value;
            var purgeAF = GProcessPara.Weighing.PurgeAfterFill.Value;


            //mass cal
            var pp4_material_density = new List<double>();

            //FlowRate Cal
            var mdot_dispTime = GProcessPara.Weighing.DispTime_DotM.Value;

            double actual_mdot = 0;
            int successCount = 0;
            #endregion

            try
            {
                Mtx.WaitOne();

                if (!gantry.MoveOpXYAbs(w_pos.XYPos)) return false;

                while (Result.Count < SampleCount)
                {
                    #region Processing
                    var wdata = new TEWeighData() { EWeighCalMode = wType };

                    if (Stop) goto _stop;

                    //Pre Set Profile
                    switch (dispCtrl.PumpType)
                    {
                        case EPumpType.VERMES_3280:
                            {
                                vermes_setup = vermes_setup is null ? new Vermes3280_Param(GRecipes.Vermes_Setups[gantryidx]) : new Vermes3280_Param(vermes_setup);
                                vermes_setup.Pulses.Value = wType > EWeighType.Mass ? 0 : dotPerSample;
                                vermes_setup.Delay.Value = wType > EWeighType.Mass ? vermes_setup.Delay.Value : w_dotwait_ms;

                                if (!TFPump.Vermes_Pump[gantryidx].TriggerAset(vermes_setup)) return false;
                                if (!TFPressCtrl.FPress[gantryidx].Set(vermes_setup.FPress.Value)) return false;

                                GMotDef.Outputs[(int)dispCtrl.VacDO].Status = false;
                                GMotDef.Outputs[(int)dispCtrl.FPressDO].Status = true;

                                wdata.TuneVariableUnit = GSystemCfg.Display.PressUnit;
                                wdata.TunePara = vermes_setup.FPress.Value;
                                break;
                            }
                        case EPumpType.SP:
                        case EPumpType.SPLite:
                        case EPumpType.TP:
                            {
                                if (!TFPressCtrl.FPress[gantryidx].Set(sp_setup.FPress.Value)) return false;
                                if (!TFPressCtrl.FPress[gantryidx + 2].Set(sp_setup.PPress.Value)) return false;

                                wdata.TuneVariableUnit = GSystemCfg.Display.PressUnit;
                                wdata.TunePara = sp_setup.FPress.Value;
                                break;
                            }
                        case EPumpType.HM:
                            {
                                if (!TFPressCtrl.FPress[gantryidx].Set(hm_setup.FPress.Value)) return false;

                                wdata.TuneVariableUnit = GSystemCfg.Display.PressUnit;
                                wdata.TunePara = hm_setup.DispRPM.Value;
                                break;
                            }
                    }

                    if (!gantry.MoveOpZAbs(w_pos.Z)) return false;
                    Thread.Sleep(w_startwait);

                    double beforeW = 0;
                    Thread.Sleep(w_readwait);
                    if (!TFWeightScale.ReadStable(ref beforeW)) goto _stop;

                    //Disp
                    switch (dispCtrl.PumpType)
                    {
                        case EPumpType.VERMES_3280:
                            {
                                var trig = GMotDef.Outputs[(int)dispCtrl.DispDO];

                                switch (wType)
                                {
                                    case EWeighType.Mass:
                                        {
                                            trig.Status = true;
                                            while (!GMotDef.Inputs[(int)dispCtrl.DispDI].Status) Thread.Sleep(1);
                                            trig.Status = false;

                                            break;
                                        }
                                    case EWeighType.MassFlowRate:
                                        {
                                            trig.Status = true;
                                            Thread.Sleep(mdot_dispTime);
                                            trig.Status = false;
                                            break;
                                        }
                                }
                                break;
                            }
                        case EPumpType.SP:
                            {
                                switch (wType)
                                {
                                    case EWeighType.Mass:
                                        {
                                            var setup = new SP_Param(sp_setup);
                                            TFPump.SP.Shot_One(setup, gantryidx);
                                            break;
                                        }
                                    case EWeighType.MassFlowRate:
                                        {
                                            var setup = new SP_Param(sp_setup);
                                            setup.DispTime.Value = mdot_dispTime;
                                            TFPump.SP.Shot_One(setup, gantryidx);
                                            break;
                                        }
                                }

                                break;
                            }
                        case EPumpType.HM:
                            {
                                switch (wType)
                                {
                                    case EWeighType.Mass:
                                        {
                                            var setup = new HM_Param(hm_setup);
                                            TFPump.HM.Shot_One(setup, gantryidx);
                                            break;
                                        }
                                    case EWeighType.MassFlowRate:
                                        {
                                            var setup = new HM_Param(hm_setup);
                                            setup.DispTime.Value = mdot_dispTime;
                                            TFPump.HM.Shot_One(setup, gantryidx);
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
                        gantry.ZAxis.SetParam(0, w_zupvel, 100);
                        if (!gantry.ZAxis.MoveRel(w_zupdist, true)) return false;
                    }

                    double afterW = 0;
                    Thread.Sleep(w_readwait);
                    if (!TFWeightScale.ReadStable(ref afterW)) goto _stop;

                    double actual_mass = 1000 * (double)((afterW - beforeW) / dotPerSample);

                    wdata.ActualMass = actual_mass;

                    if (wType == EWeighType.MassFlowRate)
                    {
                        actual_mdot = /*actual_mass*/ ((afterW - beforeW) * 1000) / (double)(mdot_dispTime / 1000);
                        wdata.FlowRate = actual_mdot;
                    }

                    #endregion

                    #region Calibrate
                    if (wMode == EWeighMode.Calibration)
                    {
                        bool massFRSuccess = actual_mdot >= target_massFR - massFR_range && actual_mdot <= target_massFR + massFR_range;
                        bool massSuccess = actual_mass >= target_mass - mass_range && actual_mass <= target_mass + mass_range;

                        switch (wType)
                        {
                            case EWeighType.MassFlowRate:
                                {
                                    if (massFRSuccess)
                                    {
                                        Result.Add(wdata);
                                        successCount++;
                                        if (successCount >= repeatCount)
                                        {
                                            goto _stop;
                                        }
                                        continue;
                                    }
                                    break;
                                }
                            case EWeighType.Mass:
                                {
                                    if (massSuccess)
                                    {
                                        Result.Add(wdata);
                                        successCount++;
                                        if (successCount >= repeatCount)
                                        {
                                            goto _stop;
                                        }
                                        continue;
                                    }
                                    break;
                                }
                        }
                        //if (actual_mdot >= target_massFR - massFR_range && actual_mdot <= target_massFR + massFR_range)
                        //{
                        //    Result.Add(wdata);
                        //    successCount++;
                        //    if (successCount >= repeatCount)
                        //    {
                        //        goto _stop;
                        //    }
                        //    continue;
                        //}

                        bool IsLowerThanTarget = wType == EWeighType.Mass ? actual_mass <= target_mass : actual_mdot <= target_massFR;

                        #region Change Para
                        switch (dispCtrl.PumpType)
                        {
                            case EPumpType.VERMES_3280:
                                {
                                    wdata.TunePara = vermes_setup.FPress.Value;

                                    var fpress = vermes_setup.FPress.Value;
                                    var minfpress = fpress - (fpress * tune_percentage_Min / 100);
                                    var maxfpress = fpress + (fpress * tune_percentage_Max / 100);
                                    fpress = IsLowerThanTarget ? (fpress + maxfpress) / 2 : (fpress + minfpress) / 2;
                                    vermes_setup.FPress.Value = fpress;

                                    Wcmd.Para[6] = fpress;
                                    GLog.LogProcess($"Cal{dispCtrl.PumpType}_ SampleCount:{Result.Count}/{SampleCount} {nameof(fpress)} [{wdata.TunePara} > {fpress}]");
                                    break;
                                }
                            case EPumpType.SPLite:
                            case EPumpType.TP:
                                {
                                    wdata.TunePara = sp_setup.FPress.Value;

                                    var fpress = sp_setup.FPress.Value;
                                    var minfpress = fpress - (fpress * tune_percentage_Min / 100);
                                    var maxfpress = fpress + (fpress * tune_percentage_Max / 100);
                                    fpress = IsLowerThanTarget ? (fpress + maxfpress) / 2 : (fpress + minfpress) / 2;
                                    sp_setup.FPress.Value = fpress;

                                    Wcmd.Para[6] = fpress;
                                    break;
                                }
                            case EPumpType.SP:
                                {
                                    wdata.TunePara = sp_setup.FPress.Value;

                                    var fpress = sp_setup.FPress.Value;
                                    var ppress = sp_setup.PPress.Value;

                                    var minfpress = fpress - (fpress * tune_percentage_Min / 100);
                                    var maxfpress = fpress + (fpress * tune_percentage_Max / 100);

                                    fpress = IsLowerThanTarget ? (fpress + maxfpress) / 2 : (fpress + minfpress) / 2;
                                    ppress += fpress - sp_setup.FPress.Value;

                                    sp_setup.FPress.Value = fpress;
                                    sp_setup.PPress.Value = ppress;

                                    Wcmd.Para[6] = fpress;
                                    Wcmd.Para[7] = ppress;
                                    break;
                                }
                            case EPumpType.HM:
                                {
                                    wdata.TunePara = hm_setup.DispRPM.Value;

                                    var dispRPM = hm_setup.DispRPM.Value;

                                    var minDispRPM = (int)(dispRPM - (dispRPM * tune_percentage_Min / 100));
                                    var maxDispRPM = (int)(dispRPM + (dispRPM * tune_percentage_Max / 100));

                                    dispRPM = IsLowerThanTarget ? (dispRPM + maxDispRPM) / 2 : (dispRPM + minDispRPM) / 2;
                                    hm_setup.DispRPM.Value = dispRPM;

                                    Wcmd.Para[1] = dispRPM;
                                    break;
                                }
                        }
                        #endregion

                    }
                    #endregion
                    else Result.Add(wdata);
                }

                _stop:

                #region Stop
                GMotDef.Outputs[(int)dispCtrl.FPressDO].Status = false;
                GMotDef.Outputs[(int)dispCtrl.PPressDO].Status = false;
                GMotDef.Outputs[(int)dispCtrl.VacDO].Status = true;

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
                Thread.Sleep(10);
                GMotDef.Outputs[(int)dispCtrl.VacDO].Status = false;


                #endregion

                var flowrate = Result.Count is 0 ? 0 : Result.Select(x => x.FlowRate).Average();
                if (wType == EWeighType.MassFlowRate) GProcessPara.Weighing.ActualMassFlowRate[gantryidx].Value = flowrate;
                cmd = GProcessPara.Weighing.EnableUpdateTCmd[gantryidx] ? Wcmd : cmd;

                Finish = true;

                return gantry.GotoXYZ(new PointXYZ());

                //return true;
            }
            catch (Exception ex)
            {
                Finish = false;
                GLog.WriteException(ex);
                MsgBox.ShowDialog(ex.Message.ToString());
                return false;
            }
            finally
            {
                Mtx.ReleaseMutex();
            }
        }

        public TCWeighFunc(int gantryIdx)
        {
            GantryIdx = gantryIdx;
        }

        public static TCWeighFunc[] WeighCals = new TCWeighFunc[2] { new TCWeighFunc(TFGantry.GantryLeft.Index), new TCWeighFunc(TFGantry.GantryRight.Index) };
    }

    class TEWeighData
    {
        public double ActualMass { get; set; }
        public double FlowRate { get; set; }
        public double TunePara { get; set; }
        public EUnit TuneVariableUnit { get; set; }

        public EWeighType EWeighCalMode { get; set; }

        public double Mean { get; set; }
        public TEWeighData()
        {
        }
        public TEWeighData(TEWeighData weighData)
        {
            this.ActualMass = weighData.ActualMass;
            this.TunePara = weighData.TunePara;
            this.TuneVariableUnit = weighData.TuneVariableUnit;
        }

        public override string ToString()
        {
            return $"\t{TunePara:f3}\t{TuneVariableUnit}\t{(EWeighCalMode == EWeighType.Mass ? ActualMass : FlowRate):f4}\t{(EWeighCalMode == EWeighType.Mass ? "mg" : "mg/s")}";
        }
    }

}
