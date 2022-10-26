using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

namespace NagaW
{
    using Emgu.CV;
    using Emgu.CV.Structure;

    class TFCommon
    {
        public static bool SensMainAir { get => GMotDef.IN10.Status; }//Hi when air is present
        public static bool CheckMainAirPressure//true = main air ready
        {
            get
            {
                if (!TEZMCAux.Ready(true)) return false;

                if (!SensMainAir)
                {
                    GAlarm.Prompt(EAlarm.MAIN_AIR_PRESSURE_NOT_READY);
                    return false;
                }
                return true;
            }
        }
    }

    class TFTower
    {
        public static TEZMCAux.TOutput TLRed { get => GMotDef.Out32; }
        public static TEZMCAux.TOutput TLYel { get => GMotDef.Out33; }
        public static TEZMCAux.TOutput TLGrn { get => GMotDef.Out34; }
        public static TEZMCAux.TOutput TLBzr { get => GMotDef.Out35; }

        public static void Close()
        {
            TLRed.Status = false;
            TLYel.Status = false;
            TLGrn.Status = false;
            TLBzr.Status = false;
        }
        public static void Init()
        {
            TLRed.Status = false;
            TLYel.Status = true;
            TLGrn.Status = false;
            TLBzr.Status = false;
        }

        public static void Error(bool state)
        {
            TLRed.Status = state;
            TLBzr.Status = state;
        }

        public static void Process(bool state)
        {
            TLYel.Status = !state;
            TLGrn.Status = state;
        }
    }

    class TFGantry
    {
        //use as common gantry
        public static TEZMCAux.TGroup GantryLeft = new TEZMCAux.TGroup(new TEZMCAux.TAxis[] { GMotDef.GXAxis, GMotDef.GYAxis, GMotDef.GZAxis }, "Main Gantry", 0);
        //ignore this
        public static TEZMCAux.TGroup GantryRight = new TEZMCAux.TGroup(new TEZMCAux.TAxis[] { GMotDef.Axis12, GMotDef.Axis13, GMotDef.Axis14 }, "Gantry Right", 1);
        //for common setup
        public static TEZMCAux.TGroup GantrySetup = new TEZMCAux.TGroup(new TEZMCAux.TAxis[] { GMotDef.GXAxis, GMotDef.GVAxis, GMotDef.GZAxis }, "Gantry Setup", 0);

        public static TEZMCAux.TGroup GantrySelect = GantryLeft;


        public static TEZMCAux.TGroup GantryVR = new TEZMCAux.TGroup(new TEZMCAux.TAxis[] { GMotDef.GRAxis, GMotDef.GVAxis, GMotDef.GZAxis }, "GantryVRZ", 0);


        public static TEZMCAux.TAxis[] TouchAxis = Enumerable.Range(0, 2).Select(x => new TEZMCAux.TAxis(x + 4, $"Touch{x}")).ToArray();

        public static bool InitRequired
        {
            get
            {
                bool b = TEZMCAux.Online && TEZMCAux.CommError;
                if (b) GDefine.SystemState = ESystemState.ErrorInit;
                return b;
            }
        }

        public static bool Open()
        {
            GEvent.Start(EEvent.OPEN_MOTION_CTRL);

            if (!TEZMCAux.Open()) return false;

            try
            {
                TEZMCAux.Execute("RUN INITSYSTEM");
            }
            catch
            {
                GDefine.SystemState = ESystemState.ErrorInit;
                GAlarm.Prompt(EAlarm.MOTION_CTRL_INIT_ERROR);
                return false;
            }

            return Task.Run(() =>
            {
                while (TEZMCAux.QueryInt("SysState") != 1 && TEZMCAux.QueryInt("SysState") != 3) { }
                var systate = TEZMCAux.QueryInt("SysState");
                return TEZMCAux.QueryInt("SysState") == 1;
            }).Result;
        }

        public static void Close()
        {
            TEZMCAux.Close();
        }

        public static bool CheckEMO//true = no EMO
        {
            get
            {
                if (!TEZMCAux.Ready(true)) return false;

                if (!GMotDef.EMO_Ready.Status)
                {
                    GAlarm.Prompt(EAlarm.EMO_ACTIVATED);
                    return false;
                }

                return true;
            }
        }

        public static EStatus GLStatus = EStatus.Unknown;
        public static bool GLFindPhase = true;
        public static bool GXYZHome()
        {
            if (!TEZMCAux.Ready()) return false;
            if (!CheckEMO) return false;

            if (GLFindPhase)
            {
                foreach (TEZMCAux.TAxis axis in GantryLeft.Axis)
                {
                    if (axis.HLmtP == false)
                    {
                        GAlarm.Prompt(EAlarm.GLHOME_ERROR, axis.Name + " " + " Positive Limit Not Clear.");
                        return false;
                    }
                    if (axis.HLmtN == false)
                    {
                        GAlarm.Prompt(EAlarm.GLHOME_ERROR, axis.Name + " " + " Negative Limit Not Clear.");
                        return false;
                    }
                }
            }

            var Axis = GantryLeft.Axis.Where(x => x.AlarmCode == 4).Select(u => u.Name).ToArray();
            if (Axis.Length > 0)
            {
                string axis = string.Join(" and ", Axis);
                GDefine.SystemState = ESystemState.ErrorInit;
                GAlarm.Prompt(EAlarm.GLHOME_ERROR, axis + " " + " Communication error");
                return false;
            }

            GLStatus = EStatus.Initing;
            try
            {
                TEZMCAux.Execute($"HOMEMODE(6,{GSystemCfg.Gantry.GLXAxisHomeMode},{GSystemCfg.Gantry.GLYAxisHomeMode},{GSystemCfg.Gantry.GLZAxisHomeMode})");
                TEZMCAux.Execute($"GXYHOME_SPEEDPROF(0,{GProcessPara.Home.GXYSpeedProfile[0]},{GProcessPara.Home.GXYSpeedProfile[1]},{GProcessPara.Home.GXYSpeedProfile[2]},{GProcessPara.Home.GXYSpeedProfile[3]})");
                TEZMCAux.Execute($"GZHOME_SPEEDPROF(0,{GProcessPara.Home.GZSpeedProfile[0]},{GProcessPara.Home.GZSpeedProfile[1]},{GProcessPara.Home.GZSpeedProfile[2]},{GProcessPara.Home.GXYSpeedProfile[3]})");
                TEZMCAux.Execute("RUN GLHOME");

                var sw = Stopwatch.StartNew();
                while (true)
                {
                    //if (TEZMCAux.QueryInt("GLSTATUS") == (int)EStatus.Ready)
                    int status = TEZMCAux.QueryInt("GLSTATUS");
                    if (status == (int)EStatus.Ready)
                    {
                        GLStatus = EStatus.Ready;
                        GLFindPhase = false;
                        return true;
                    }
                    if (status == 4)
                    {
                        GLStatus = EStatus.InitError;
                        GAlarm.Prompt(EAlarm.EMO_ACTIVATED);
                        return false;
                    }

                    if (sw.ElapsedMilliseconds >= GSystemCfg.Gantry.GantryHomeTimeout * 1000)
                    {
                        TEZMCAux.Execute("GLHOMESTOP", true);
                        GAlarm.Prompt(EAlarm.GLHOME_TIMEOUT);
                        GLStatus = EStatus.InitError;
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                GDefine.SystemState = ESystemState.ErrorInit;
                GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                return false;
            }
        }
        public static bool GLReady()
        {
            if (TFGantry.GLStatus != EStatus.Ready)
            {
                frmMsgbox msgbox = new frmMsgbox("Gantry Left Not Ready.", MsgBoxBtns.OK);
                msgbox.ShowDialog();
                return false;
            }

            if (GantryLeft.Error)
            {
                int i = GantryLeft.ErrorMask;
                string strAxis = "";
                if ((i & 0x01) > 0) strAxis += GantryLeft.Axis[0].Name + " ";
                if ((i & 0x02) > 0) strAxis += GantryLeft.Axis[1].Name + " ";
                if ((i & 0x03) > 0) strAxis += GantryLeft.Axis[2].Name + " ";

                frmMsgbox msgbox = new frmMsgbox("Gantry Left  Error. " + strAxis, MsgBoxBtns.OK);
                msgbox.ShowDialog();
                return false;
            }

            if (!GantryLeft.Enabled)
            {
                frmMsgbox msgbox = new frmMsgbox("Gantry Left no enabled.", MsgBoxBtns.OK);
                msgbox.ShowDialog();
                return false;
            }

            return true;
        }
        public static EStatus GRStatus = EStatus.Unknown;
        public static bool GRFindPhase = true;
        public static bool GVRHome()
        {
            if (!TEZMCAux.Ready()) return false;
            if (!CheckEMO) return false;

            if (GRFindPhase)
            {
                foreach (TEZMCAux.TAxis axis in GantrySetup.Axis)
                {
                    if (axis.HLmtP == false)
                    {
                        GAlarm.Prompt(EAlarm.GRHOME_ERROR, axis.Name + " " + " Positive Limit Not Clear.");
                        return false;
                    }
                    if (axis.HLmtN == false)
                    {
                        GAlarm.Prompt(EAlarm.GRHOME_ERROR, axis.Name + " " + " Negative Limit Not Clear.");
                        return false;
                    }
                }
            }

            var Axis = GantrySetup.Axis.Where(x => x.AlarmCode == 4).Select(u => u.Name).ToArray();
            if (Axis.Length > 0)
            {
                string axis = string.Join(" and ", Axis);
                GDefine.SystemState = ESystemState.ErrorInit;
                GAlarm.Prompt(EAlarm.GRHOME_ERROR, axis + " " + " Communication error");
                return false;
            }

            GRStatus = EStatus.Initing;
            try
            {
                TEZMCAux.Execute($"HOMEMODE(9,{GSystemCfg.Gantry.GRXAxisHomeMode},{GSystemCfg.Gantry.GRYAxisHomeMode},{GSystemCfg.Gantry.GRZAxisHomeMode})");
                TEZMCAux.Execute($"GXYHOME_SPEEDPROF(0,{GProcessPara.Home.GXYSpeedProfile[0]},{GProcessPara.Home.GXYSpeedProfile[1]},{GProcessPara.Home.GXYSpeedProfile[2]},{GProcessPara.Home.GXYSpeedProfile[3]})");
                TEZMCAux.Execute($"GZHOME_SPEEDPROF(0,{GProcessPara.Home.GZSpeedProfile[0]},{GProcessPara.Home.GZSpeedProfile[1]},{GProcessPara.Home.GZSpeedProfile[2]},{GProcessPara.Home.GXYSpeedProfile[3]})");
                TEZMCAux.Execute($"PsicorHMSP(0,10,20,500,500)");


                TEZMCAux.Execute("RUN GRHOME");

                var sw = Stopwatch.StartNew();
                while (true)
                {
                    int status = TEZMCAux.QueryInt("GRSTATUS");
                    if (status == (int)EStatus.Ready)
                    {
                        GRStatus = EStatus.Ready;
                        GRFindPhase = false;
                        return true;
                    }
                    if (status == 4)
                    {
                        GRStatus = EStatus.InitError;
                        GAlarm.Prompt(EAlarm.EMO_ACTIVATED);
                        return false;
                    }


                    if (sw.ElapsedMilliseconds >= GSystemCfg.Gantry.GantryHomeTimeout * 1000)
                    {
                        TEZMCAux.Execute("GRHOMESTOP", true);
                        GAlarm.Prompt(EAlarm.GRHOME_TIMEOUT);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                GDefine.SystemState = ESystemState.ErrorInit;
                GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                return false;
            }
        }
        public static bool GRReady()
        {
            if (TFGantry.GRStatus != EStatus.Ready)
            {
                frmMsgbox msgbox = new frmMsgbox("Gantry Right Not Ready.", MsgBoxBtns.OK);
                msgbox.ShowDialog();
                return false;
            }

            if (GantryRight.Error)
            {
                int i = GantryRight.ErrorMask;
                string strAxis = "";
                if ((i & 0x01) > 0) strAxis += GantryRight.Axis[0].Name + " ";
                if ((i & 0x02) > 0) strAxis += GantryRight.Axis[1].Name + " ";
                if ((i & 0x03) > 0) strAxis += GantryRight.Axis[2].Name + " ";

                frmMsgbox msgbox = new frmMsgbox("Gantry Right  Error. " + strAxis, MsgBoxBtns.OK);
                msgbox.ShowDialog();
                return false;
            }

            if (!GantryRight.Enabled)
            {
                frmMsgbox msgbox = new frmMsgbox("Gantry Right no enabled.", MsgBoxBtns.OK);
                msgbox.ShowDialog();
                return false;
            }

            return true;
        }

        public static bool InitAll()
        {
            if (!TEZMCAux.Ready()) return false;
            if (!CheckEMO) return false;
            if (!TFCommon.CheckMainAirPressure) return false;

            try
            {
                TEZMCAux.Execute("GXCOLLISION=500");

                var taskL = Task<bool>.Run(() =>
                {
                    return TFGantry.GXYZHome();
                });

                var taskR = Task<bool>.Run(() =>
                {
                    return TFGantry.GVRHome();
                });

                Task.WaitAll(taskL, taskR);
                if (!taskL.Result || !taskR.Result) return false;

                return true;
            }
            catch (Exception ex)
            {
                GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                return false;
            }
        }

        public static bool Enabled()//Check gantry axes are enabled
        {
            if (!TEZMCAux.Ready()) return false;

            try
            {

                return true;
            }
            catch (Exception ex)
            {
                GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                return false;
            }
        }

        public static void GetErrorCode()
        {
            EZErrorCode ZmotionErrorCode;
            Enum.TryParse(TEZMCAux.QueryInt("ErrorCode").ToString(), out ZmotionErrorCode);
            switch (ZmotionErrorCode)
            {
                case EZErrorCode.SlotScanFailed:
                    GAlarm.Prompt(EAlarm.ETHERCAT_SCAN_FAILED);
                    break;
                case EZErrorCode.AxisUndercount:
                    GAlarm.Prompt(EAlarm.SCANAXIS_LOWER_AXISCOUNT);
                    break;
                case EZErrorCode.AxisOverCount:
                    GAlarm.Prompt(EAlarm.SCANAXIS_OVER_AXISCOUNT);
                    break;
                case EZErrorCode.BusStartFailed:
                    GAlarm.Prompt(EAlarm.STACT_ETHERCAT_BUSFAILED);
                    break;
                case EZErrorCode.ExpansionIOError:
                    GAlarm.Prompt(EAlarm.EXPANSIOBIO_CONNECT_FAILED);
                    break;
                case EZErrorCode.EMOtrigger:
                    GAlarm.Prompt(EAlarm.EMO_ACTIVATED);
                    break;
            }
        }
    }


    public enum ENeedleCleanMode { VacClean, Flush, Purge }
    class TCNeedleFunc
    {
        public class TCleanFlushPurge
        {
            readonly TEZMCAux.TGroup gantry;
            public bool running = false;
            public TCleanFlushPurge(TEZMCAux.TGroup gantry)
            {
                this.gantry = gantry;
            }
            public bool Execute(ENeedleCleanMode cleanMode, int dnwait, int disptime, int vactime, int postvactime, int postwait, int count)
            {
                running = true;
                GEvent.Start(EEvent.NEEDLE_CFP, $"{cleanMode} " + gantry.Name);

                TEZMCAux.TOutput dispIO = null;
                TEZMCAux.TOutput fpressIO = null;
                TEZMCAux.TOutput ppressIO = null;
                TEZMCAux.TOutput vacIO = null;
                TEZMCAux.TOutput cleanVacDO = null;

                var fpressCtrl = TFPressCtrl.FPress[gantry.Index];
                var ppressCtrl = TFPressCtrl.FPress[gantry.Index + 2];

                PointXYZ pos = new PointXYZ();
                double hSensorValue = 0;
                double relZ = 0;

                switch (cleanMode)
                {
                    default: return InComplete();
                    case ENeedleCleanMode.VacClean:
                        pos = new PointXYZ(GSetupPara.NeedleVacClean.Pos[gantry.Index]);
                        hSensorValue = GSetupPara.NeedleVacClean.HSensorValue[gantry.Index];
                        relZ = GProcessPara.NeedleVacClean.RelZPos[gantry.Index].Value;
                        break;
                    case ENeedleCleanMode.Flush:
                        pos = new PointXYZ(pos = GSetupPara.NeedleFlush.Pos[gantry.Index]);
                        hSensorValue = GSetupPara.NeedleFlush.HSensorValue[gantry.Index];
                        relZ = GProcessPara.NeedleFlush.RelZPos[gantry.Index].Value;
                        break;
                    case ENeedleCleanMode.Purge:
                        pos = new PointXYZ(pos = GSetupPara.NeedlePurge.Pos[gantry.Index]);
                        hSensorValue = GSetupPara.NeedlePurge.HSensorValue[gantry.Index];
                        relZ = GProcessPara.NeedlePurge.RelZPos[gantry.Index].Value;
                        break;
                }

                pos.X += GSetupPara.Calibration.NeedleXYOffset[gantry.Index].X;
                pos.Y += GSetupPara.Calibration.NeedleXYOffset[gantry.Index].Y;

                double hSenseDiff = hSensorValue - GSetupPara.Calibration.HSensorValue[gantry.Index];
                pos.Z = GSetupPara.Calibration.ZTouchValue[gantry.Index] - hSenseDiff + relZ;

                var dispCtrl = GSystemCfg.Pump.Pumps[gantry.Index];
                cleanVacDO = GMotDef.Outputs[(int)dispCtrl.CleanVacDO];
                //pre

                //initialize io
                dispIO = GMotDef.Outputs[(int)dispCtrl.DispDO];
                fpressIO = GMotDef.Outputs[(int)dispCtrl.FPressDO];
                ppressIO = GMotDef.Outputs[(int)dispCtrl.PPressDO];
                vacIO = GMotDef.Outputs[(int)dispCtrl.VacDO];


                switch (dispCtrl.PumpType)
                {
                    case EPumpType.VERMES_3280:
                        {
                            var vp = TFPump.Vermes_Pump[gantry.Index];
                            var setup = new Vermes3280_Param(GRecipes.Vermes_Setups[gantry.Index]);
                            setup.Pulses.Value = 0;

                            fpressCtrl.Set(setup.FPress.Value);
                            vp.TriggerAset(setup);

                            fpressIO.Status = true;

                            break;
                        }
                    case EPumpType.SP:
                        {
                            fpressIO.Status = false;
                            ppressIO.Status = false;

                            var setup = new SP_Param(GRecipes.SP_Setups[gantry.Index]);
                            fpressCtrl.Set(setup.FPress.Value);
                            ppressCtrl.Set(setup.PPress.Value);
                            break;
                        }
                    case EPumpType.SPLite:
                    case EPumpType.TP:
                        {
                            fpressIO.Status = false;

                            var setup = new SP_Param(GRecipes.SP_Setups[gantry.Index]);
                            fpressCtrl.Set(setup.FPress.Value);
                            break;
                        }
                    case EPumpType.HM:
                        {
                            var setup = new HM_Param(GRecipes.HM_Setups[gantry.Index]);
                            fpressCtrl.Set(setup.FPress.Value);
                            break;
                        }
                    case EPumpType.PNEUMATIC_JET:
                        {
                            var setup = new PneumaticJet_Param(GRecipes.PneumaticJet_Setups[gantry.Index]);
                            fpressCtrl.Set(setup.FPress.Value);
                            ppressCtrl.Set(setup.VPress.Value);

                            fpressIO.Status = true;
                            ppressIO.Status = false;
                            break;
                        }
                }

                if (!gantry.MoveOpZAbs(0)) return InComplete();

                for (int c = 0; c < count; c++)
                {
                    if (!running) return InComplete();

                    if (!gantry.GotoXYZ(pos)) return InComplete();

                    GLog.LogProcess($"H{gantry.Index + 1} {cleanMode} Count:{c + 1} start");

                    Thread.Sleep(dnwait);

                    var vacClean = Task.Run(() =>
                    {
                        cleanVacDO.Status = true;
                        Thread.Sleep(vactime);
                        cleanVacDO.Status = false;
                    });

                    if (disptime > 0)
                    {
                        switch (dispCtrl.PumpType)
                        {
                            default: break;
                            case EPumpType.VERMES_3280:
                                {
                                    //shot
                                    fpressIO.Status = true;
                                    dispIO.Status = true;
                                    Thread.Sleep(disptime);
                                    dispIO.Status = false;
                                    fpressIO.Status = false;
                                    break;
                                }
                            case EPumpType.SP:
                                {
                                    var setup = new SP_Param(GRecipes.SP_Setups[gantry.Index]);
                                    setup.DispTime.Value = disptime;
                                    TFPump.SP.Shot_One(setup, gantry.Index);
                                    break;
                                }
                            case EPumpType.SPLite:
                            case EPumpType.TP:
                                {
                                    var setup = new SP_Param(GRecipes.SP_Setups[gantry.Index]);
                                    setup.DispTime.Value = disptime;
                                    TFPump.TP.Shot_One(setup, gantry.Index);
                                    break;
                                }
                            case EPumpType.HM:
                                {
                                    var setup = new HM_Param(GRecipes.HM_Setups[gantry.Index]);
                                    setup.DispTime.Value = disptime;
                                    TFPump.HM.Shot_One(gantry.Index, setup, fpressIO, vacIO);
                                    break;
                                }
                            case EPumpType.PNEUMATIC_JET:
                                {
                                    var setup = new PneumaticJet_Param(GRecipes.PneumaticJet_Setups[gantry.Index]);
                                    setup.DispTime.Value = disptime;
                                    TFPump.PnuematicJet.Shot_One(gantry.Index, setup, fpressIO, vacIO);
                                    fpressIO.Status = false;
                                    break;
                                }
                        }
                    }

                    Task.WaitAll(vacClean);

                    Thread.Sleep(postwait);
                    if (!gantry.MoveOpZAbs(0)) return InComplete();

                    cleanVacDO.Status = true;
                    Thread.Sleep(postvactime);
                    cleanVacDO.Status = false;
                }
                return true;

                bool InComplete()
                {
                    gantry.MoveOpZAbs(0);
                    running = false;
                    return false;
                }
            }
            public bool Execute(ENeedleCleanMode cleanMode)
            {
                int dnwait, disptime, vactime, postvactime, postwait, count;

                switch (cleanMode)
                {
                    default:
                    case ENeedleCleanMode.VacClean:
                        dnwait = GProcessPara.NeedleVacClean.DownWait[gantry.Index].Value;
                        disptime = GProcessPara.NeedleVacClean.DispTime[gantry.Index].Value;
                        vactime = GProcessPara.NeedleVacClean.VacTime[gantry.Index].Value;
                        postvactime = GProcessPara.NeedleVacClean.PostVacTime[gantry.Index].Value;
                        postwait = GProcessPara.NeedleVacClean.PostWait[gantry.Index].Value;
                        count = GProcessPara.NeedleVacClean.Count[gantry.Index].Value;
                        break;
                    case ENeedleCleanMode.Flush:
                        dnwait = GProcessPara.NeedleFlush.DownWait[gantry.Index].Value;
                        disptime = GProcessPara.NeedleFlush.DispTime[gantry.Index].Value;
                        vactime = GProcessPara.NeedleFlush.VacTime[gantry.Index].Value;
                        postvactime = GProcessPara.NeedleFlush.PostVacTime[gantry.Index].Value;
                        postwait = GProcessPara.NeedleFlush.PostWait[gantry.Index].Value;
                        count = GProcessPara.NeedleFlush.Count[gantry.Index].Value;
                        break;
                    case ENeedleCleanMode.Purge:
                        dnwait = GProcessPara.NeedlePurge.DownWait[gantry.Index].Value;
                        disptime = GProcessPara.NeedlePurge.DispTime[gantry.Index].Value;
                        vactime = GProcessPara.NeedlePurge.VacTime[gantry.Index].Value;
                        postvactime = GProcessPara.NeedlePurge.PostVacTime[gantry.Index].Value;
                        postwait = GProcessPara.NeedlePurge.PostWait[gantry.Index].Value;
                        count = GProcessPara.NeedlePurge.Count[gantry.Index].Value;
                        break;
                }

                return Execute(cleanMode, dnwait, disptime, vactime, postvactime, postwait, count);
            }
            public bool Learn(ENeedleCleanMode cleanMode)
            {
                GEvent.Start(EEvent.NEEDLE_CFP_LEARN, $"{cleanMode} " + gantry.Name);

                PointXYZ pos = new PointXYZ();
                PointXYZ laserPos = new PointXYZ();

                switch (cleanMode)
                {
                    default: return InComplete();
                    case ENeedleCleanMode.VacClean:
                        pos = new PointXYZ(GSetupPara.NeedleVacClean.Pos[gantry.Index]);
                        laserPos = pos + GSetupPara.Calibration.LaserOffset[gantry.Index] + GSetupPara.NeedleVacClean.HSensorOffset[gantry.Index];
                        break;
                    case ENeedleCleanMode.Flush:
                        pos = new PointXYZ(GSetupPara.NeedleFlush.Pos[gantry.Index]);
                        laserPos = pos + GSetupPara.Calibration.LaserOffset[gantry.Index] + GSetupPara.NeedleFlush.HSensorOffset[gantry.Index];
                        break;
                    case ENeedleCleanMode.Purge:
                        pos = new PointXYZ(GSetupPara.NeedlePurge.Pos[gantry.Index]);
                        laserPos = pos + GSetupPara.Calibration.LaserOffset[gantry.Index] + GSetupPara.NeedlePurge.HSensorOffset[gantry.Index];
                        break;
                }

                if (!gantry.GotoXYZ(laserPos)) return InComplete();

                Thread.Sleep(GProcessPara.HSensor.SettleTime.Value);
                double hSensorValue = 0;
                TFHSensors.Sensor[gantry.Index].GetValue(ref hSensorValue);

                switch (cleanMode)
                {
                    default: return InComplete();
                    case ENeedleCleanMode.VacClean: GSetupPara.NeedleVacClean.HSensorValue[gantry.Index] = hSensorValue; break;
                    case ENeedleCleanMode.Flush: GSetupPara.NeedleFlush.HSensorValue[gantry.Index] = hSensorValue; break;
                    case ENeedleCleanMode.Purge: GSetupPara.NeedlePurge.HSensorValue[gantry.Index] = hSensorValue; break;
                }

                pos.X += GSetupPara.Calibration.NeedleXYOffset[gantry.Index].X;
                pos.Y += GSetupPara.Calibration.NeedleXYOffset[gantry.Index].Y;

                if (!gantry.GotoXYZ(pos)) return InComplete();

                return true;

                bool InComplete()
                {
                    gantry.MoveOpZAbs(0);
                    return false;
                }
            }
        }
        public static TCleanFlushPurge[] CFP = new TCleanFlushPurge[] { new TCleanFlushPurge(TFGantry.GantrySetup), new TCleanFlushPurge(TFGantry.GantryRight) };

        public class TCMaintFunc
        {
            readonly TEZMCAux.TGroup gantry;
            public TCMaintFunc(TEZMCAux.TGroup gantry)
            {
                this.gantry = gantry;
            }
            public bool GotoMcMaint(bool prompt = false)
            {
                GEvent.Start(EEvent.GOTO_MC_MAINT_POS);
                if (!TFLightCtrl.LightPair[gantry.Index].Off()) return false;

                return gantry.GotoXYZ(GSetupPara.Maintenance.MachinePos[gantry.Index], prompt);
            }
            public bool GotoPumpMaint(bool prompt = false)
            {
                GEvent.Start(EEvent.GOTO_PUMP_MAINT_POS);

                if (!TFLightCtrl.LightPair[gantry.Index].Off()) return false;
                return gantry.GotoXYZ(GSetupPara.Maintenance.PumpPos[gantry.Index], prompt);
            }
        }
        public static TCMaintFunc[] Maint = new TCMaintFunc[] { new TCMaintFunc(TFGantry.GantryLeft), new TCMaintFunc(TFGantry.GantrySetup) };

        public class TCPurgeStage
        {
            readonly TEZMCAux.TGroup gantry;
            public TCPurgeStage(TEZMCAux.TGroup gantry)
            {
                this.gantry = gantry;
            }
            public ERunMode RunMode = ERunMode.Camera;
            public bool Running = false;
            public bool Notification_AlmostFull = false;
            public bool Execute(int count)
            {
                Running = true;
                GEvent.Start(EEvent.PURGE_STAGE, gantry.Name);

                if (!Running) return false;

                #region
                TEZMCAux.TOutput dispIO = null;
                TEZMCAux.TOutput fpressIO = null;
                TEZMCAux.TOutput ppressIO = null;
                TEZMCAux.TOutput vacIO = null;

                var fpressCtrl = TFPressCtrl.FPress[gantry.Index];
                var ppressCtrl = TFPressCtrl.FPress[gantry.Index + 2];

                var dispCtrl = GSystemCfg.Pump.Pumps[gantry.Index];

                var dnSpeed = GProcessPara.PurgeStage.DnSpeed[gantry.Index].Value;
                var dotTime = GProcessPara.PurgeStage.DotTime[gantry.Index].Value;
                var dotWait = GProcessPara.PurgeStage.DotWait[gantry.Index].Value;
                var dnWait = GProcessPara.PurgeStage.DnWait[gantry.Index].Value;
                var upDist = GProcessPara.PurgeStage.UpDist[gantry.Index].Value;
                var upWait = GProcessPara.PurgeStage.UpWait[gantry.Index].Value;
                var retDist = GProcessPara.PurgeStage.RetDist[gantry.Index].Value;
                var retWait = GProcessPara.PurgeStage.RetWait[gantry.Index].Value;

                var camPos = new PointXYZ(GSetupPara.PurgeStage.Pos[gantry.Index]);
                var layout = GProcessPara.PurgeStage.Layout;
                var currentCR = new PointI(GProcessPara.PurgeStage.CurrentCR);

                var dispPos = camPos.GetPointD();
                double z = camPos.Z;

                if (layout.RemainCR(currentCR) < count)
                {
                    GAlarm.Prompt(EAlarm.PURGE_STAGE_FULL, "Please clean Purge Stage to continue.");
                    GProcessPara.PurgeStage.CurrentCR = new PointI();
                    Notification_AlmostFull = false;
                    return false;
                }

                if (RunMode > ERunMode.Camera)
                {
                    dispPos += GSetupPara.Calibration.NeedleXYOffset[gantry.Index];
                    z = GSetupPara.Calibration.ZTouchValue[gantry.Index] + GSetupPara.PurgeStage.Gap.Value;
                }
                if (RunMode == ERunMode.Normal)
                {
                    //pre
                    switch (dispCtrl.PumpType)
                    {
                        default: return false;
                        case EPumpType.VERMES_3280:
                            {
                                var vp = TFPump.Vermes_Pump[gantry.Index];
                                var setup = new Vermes3280_Param(GRecipes.Vermes_Setups[gantry.Index]);
                                setup.Pulses.Value = 0;

                                fpressCtrl.Set(setup.FPress.Value);
                                vp.TriggerAset(setup);

                                dispIO = GMotDef.Outputs[(int)dispCtrl.DispDO];
                                fpressIO = GMotDef.Outputs[(int)dispCtrl.FPressDO];
                                fpressIO.Status = true;
                                break;
                            }
                        case EPumpType.SP:
                        case EPumpType.SPLite:
                        case EPumpType.TP:
                            {
                                fpressIO = GMotDef.Outputs[(int)dispCtrl.FPressDO];
                                ppressIO = GMotDef.Outputs[(int)dispCtrl.PPressDO];
                                vacIO = GMotDef.Outputs[(int)dispCtrl.VacDO];

                                fpressIO.Status = false;
                                ppressIO.Status = false;
                                vacIO.Status = false;
                                break;
                            }
                        case EPumpType.HM:
                            {
                                fpressIO = GMotDef.Outputs[(int)dispCtrl.FPressDO];
                                vacIO = GMotDef.Outputs[(int)dispCtrl.VacDO];

                                fpressIO.Status = false;
                                vacIO.Status = false;
                                break;
                            }
                    }
                }
                #endregion

                var startPos = dispPos + layout.RelPos(currentCR);
                if (!gantry.MoveOpZAbs(0)) return false;
                gantry.MoveOpXYAbs(new double[] { startPos.X, startPos.Y }, true);
                if (!Running) goto _End;

                for (int k = 0; k < count; k++)
                {
                    if (!Running) return InComplete();

                    var pos = dispPos + layout.RelPos(currentCR);

                    string sBase = $"BASE({gantry.Axis[0].AxisNo},{gantry.Axis[1].AxisNo},{gantry.Axis[2].AxisNo})";
                    string cmdBuffer = sBase;
                    cmdBuffer += $" FORCE_SPEED ={GProcessPara.Operation.GXYFastSpeed.Value}";

                    if (RunMode > ERunMode.Camera) cmdBuffer += $" MOVEABS({pos.X},{pos.Y},{z + retDist + upDist})";
                    cmdBuffer += $" MOVEABS({pos.X},{pos.Y},{z})";

                    if (dnWait > 0)
                    {
                        cmdBuffer += $" MOVE_DELAY({dnWait})";
                    }

                    //pump on
                    if (RunMode == ERunMode.Normal)
                    {
                        switch (dispCtrl.PumpType)
                        {
                            default: return false;
                            case EPumpType.VERMES_3280:
                                {
                                    cmdBuffer += $" MOVE_OP({dispIO.OutputNo}, 1)";
                                    cmdBuffer += $" MOVE_DELAY({dotTime})";
                                    cmdBuffer += $" MOVE_OP({dispIO.OutputNo}, 0)";
                                    break;
                                }
                            case EPumpType.SP:
                                {
                                    var setup = new SP_Param(GRecipes.SP_Setups[gantry.Index]);
                                    setup.DispTime.Value = dotTime;
                                    cmdBuffer += TFPump.SP.ShotCmd(setup, fpressIO, ppressIO, vacIO);
                                    break;
                                }
                            case EPumpType.SPLite:
                            case EPumpType.TP:
                                {
                                    var setup = new SP_Param(GRecipes.SP_Setups[gantry.Index]);
                                    setup.DispTime.Value = dotTime;
                                    cmdBuffer += TFPump.TP.ShotCmd(setup, fpressIO, ppressIO, vacIO);
                                    break;
                                }
                            case EPumpType.HM:
                                {
                                    var setup = new HM_Param(GRecipes.HM_Setups[gantry.Index]);
                                    cmdBuffer += TFPump.HM.ShotCmd(gantry.Index, setup, fpressIO, vacIO);
                                    break;
                                }
                            case EPumpType.PNEUMATIC_JET:
                                {
                                    var setup = new PneumaticJet_Param(GRecipes.PneumaticJet_Setups[gantry.Index]);
                                    cmdBuffer += TFPump.PnuematicJet.ShotCmd(setup, fpressIO, ppressIO);
                                    break;
                                }
                        }
                    }

                    if (dotWait > 0) { cmdBuffer += $" MOVE_DELAY({dotWait})"; }

                    if (RunMode > ERunMode.Camera)
                    {
                        if (retDist > 0)
                        {
                            cmdBuffer += $" FORCE_SPEED ={dnSpeed}";
                            cmdBuffer += $" MOVESP(0,0,{retDist})";
                            cmdBuffer += $" MOVE_DELAY({retWait})";
                        }
                        if (upDist > 0)
                        {
                            cmdBuffer += $" FORCE_SPEED={dnSpeed}";
                            cmdBuffer += $" MOVESP(0,0,{upDist})";
                            cmdBuffer += $" MOVE_DELAY({upWait})";
                        }
                    }

                    TEZMCAux.DirectCommand(cmdBuffer);
                    while (gantry.Busy) Thread.Sleep(0);

                    currentCR = layout.NextCR(currentCR);
                }
            _End:
                switch (dispCtrl.PumpType)
                {
                    case EPumpType.VERMES_3280:
                        {
                            if (fpressIO != null) fpressIO.Status = false;
                            break;
                        }
                }

                GProcessPara.PurgeStage.CurrentCR = new PointI(currentCR);
                gantry.MoveOpZAbs(GSetupPara.PurgeStage.Pos[gantry.Index].Z);

                if (RunMode == ERunMode.Normal)
                {
                    if (GProcessPara.PurgeStage.CamOffsetAfterPurge)
                    {
                        // gantry.MoveOpZAbs(GRecipes.CamFocusNo[(int)gantry.Index][0]);
                        var pos = (new PointD() - GSetupPara.Calibration.NeedleXYOffset[(int)gantry.Index]).ToArray;
                        MsgBox.Processing(gantry.ToString() + " Goto Cam Offset", () => gantry.MoveOpXYRel(pos));
                    }
                }

                if (!Notification_AlmostFull)
                {
                    Notification_AlmostFull = true;
                    if (layout.CummulativeCR(currentCR) > layout.TotalCR * 0.95)
                    {
                        string msg = gantry.Name + " Purge Stage is almost full.";//notification
                        MsgBox.ShowDialog(msg);
                    }
                }
                return true;

                bool InComplete()
                {
                    gantry.MoveOpZAbs(0);
                    return false;
                }
            }
            public bool Execute()
            {
                return Execute(GProcessPara.PurgeStage.Count.Value);
            }
        }
        public static TCPurgeStage[] PurgeStage = new TCPurgeStage[] { new TCPurgeStage(TFGantry.GantryLeft), new TCPurgeStage(TFGantry.GantryRight) };

        public class TCAirBladeClean
        {
            readonly TEZMCAux.TGroup gantry;
            public bool running = false;
            public TCAirBladeClean(TEZMCAux.TGroup gantry)
            {
                this.gantry = gantry;
            }
            public bool Execute(int dnwait, double relZPos, double upSpeed, int count)
            {
                running = true;
                GEvent.Start(EEvent.NEEDLE_AIR_BLADE_CLEAN, gantry.Name);

                var dispCtrl = GSystemCfg.Pump.Pumps[gantry.Index];

                TEZMCAux.TOutput dispIO = GMotDef.Outputs[(int)dispCtrl.DispDO];
                TEZMCAux.TOutput fpressIO = GMotDef.Outputs[(int)dispCtrl.FPressDO];
                TEZMCAux.TOutput ppressIO = GMotDef.Outputs[(int)dispCtrl.PPressDO];
                TEZMCAux.TOutput vacIO = GMotDef.Outputs[(int)dispCtrl.VacDO];

                TEZMCAux.TOutput cleanVacDO = GMotDef.Outputs[(int)dispCtrl.CleanVacDO];
                TEZMCAux.TOutput cleanAirDO = GMotDef.Outputs[(int)dispCtrl.CleanAirDO];

                var fpressCtrl = TFPressCtrl.FPress[gantry.Index];
                var ppressCtrl = TFPressCtrl.FPress[gantry.Index + 2];

                var needlePos = GSetupPara.NeedleAirBladeClean.Pos[gantry.Index].GetPointD();
                needlePos += GSetupPara.Calibration.NeedleXYOffset[gantry.Index];
                //double z = GSetupPara.NeedleAirBladeClean.Pos[gantry.Index].Z;
                double hSenseDiff = GSetupPara.NeedleAirBladeClean.HSensorValue[gantry.Index] - GSetupPara.Calibration.HSensorValue[gantry.Index];
                double z = GSetupPara.Calibration.ZTouchValue[gantry.Index] - hSenseDiff;

                count = Math.Max(1, count);
                for (int c = 0; c < count; c++)
                {
                    if (!running) return InComplete();

                    if (!gantry.GotoXYZ(new PointXYZ(needlePos.X, needlePos.Y, 0))) return InComplete();
                    if (!gantry.MoveOpZAbs(z)) return InComplete();
                    cleanVacDO.Status = true;

                    if (!gantry.ZAxis.MoveRel(relZPos)) return InComplete();
                    Thread.Sleep(dnwait);
                    cleanAirDO.Status = true;

                    gantry.ZAxis.SetSpeed = upSpeed == 0 ? 5 : upSpeed;
                    if (!gantry.ZAxis.MoveRel(-relZPos)) return InComplete();
                    cleanAirDO.Status = false;
                    cleanVacDO.Status = false;

                    if (!gantry.MoveOpZAbs(0)) return InComplete();
                }
                return true;

                bool InComplete()
                {
                    gantry.MoveOpZAbs(0);

                    dispIO.Status = false;
                    fpressIO.Status = false;
                    ppressIO.Status = false;

                    cleanVacDO.Status = false;
                    cleanAirDO.Status = false;

                    running = false;
                    return false;
                }
            }
            public bool Execute(int count)
            {
                int dnwait = GProcessPara.NeedleAirBladeClean.DownWait[gantry.Index].Value;
                double relZPos = GProcessPara.NeedleAirBladeClean.RelZPos[gantry.Index].Value;
                double upSpeed = GProcessPara.NeedleAirBladeClean.UpSpeed[gantry.Index].Value;

                return Execute(dnwait, relZPos, upSpeed, count);
            }
            public bool Execute()
            {
                int dnwait = GProcessPara.NeedleAirBladeClean.DownWait[gantry.Index].Value;
                double relZPos = GProcessPara.NeedleAirBladeClean.RelZPos[gantry.Index].Value;
                double upSpeed = GProcessPara.NeedleAirBladeClean.UpSpeed[gantry.Index].Value;
                int count = GProcessPara.NeedleAirBladeClean.Count[gantry.Index].Value;

                return Execute(dnwait, relZPos, upSpeed, count);
            }

            public bool Learn()
            {
                GEvent.Start(EEvent.NEEDLE_CFP_LEARN, gantry.Name);

                PointXYZ pos = new PointXYZ(GSetupPara.NeedleAirBladeClean.Pos[gantry.Index]);
                PointXYZ laserPos = new PointXYZ(pos + GSetupPara.Calibration.LaserOffset[gantry.Index] + GSetupPara.NeedleAirBladeClean.HSensorOffset[gantry.Index]);

                if (!gantry.GotoXYZ(laserPos)) return InComplete();

                Thread.Sleep(GProcessPara.HSensor.SettleTime.Value);
                double hSensorValue = 0;
                TFHSensors.Sensor[gantry.Index].GetValue(ref hSensorValue);

                GSetupPara.NeedleAirBladeClean.HSensorValue[gantry.Index] = hSensorValue;

                pos.X += GSetupPara.Calibration.NeedleXYOffset[gantry.Index].X;
                pos.Y += GSetupPara.Calibration.NeedleXYOffset[gantry.Index].Y;

                if (!gantry.GotoXYZ(pos)) return InComplete();

                return true;

                bool InComplete()
                {
                    gantry.MoveOpZAbs(0);
                    return false;
                }
            }
        }
        public static TCAirBladeClean[] AirBladeClean = new TCAirBladeClean[] { new TCAirBladeClean(TFGantry.GantryLeft), new TCAirBladeClean(TFGantry.GantryRight) };

        public class TCNeedleSprayClean
        {
            readonly TEZMCAux.TGroup gantry;
            public bool running = false;

            public TCNeedleSprayClean(TEZMCAux.TGroup gantry)
            {
                this.gantry = gantry;
            }
            public bool Execute(int dnwait, int spraytime, int postwait, int count)
            {
                running = true;

                TEZMCAux.TOutput sprayIO = GMotDef.Out42;


                PointXYZ pos = new PointXYZ(GSetupPara.NeedleSprayClean.Pos[gantry.Index]);
                double hSensorValue = GSetupPara.NeedleSprayClean.HSensorValue[gantry.Index];
                double relZ = GProcessPara.NeedleSpray.RelZPos[gantry.Index].Value;

                pos.X += GSetupPara.Calibration.NeedleXYOffset[gantry.Index].X;
                pos.Y += GSetupPara.Calibration.NeedleXYOffset[gantry.Index].Y;

                double hSenseDiff = hSensorValue - GSetupPara.Calibration.HSensorValue[gantry.Index];
                pos.Z = GSetupPara.Calibration.ZTouchValue[gantry.Index] - hSenseDiff + relZ;


                for (int c = 0; c < count; c++)
                {
                    if (!running) return InComplete();

                    if (!gantry.GotoXYZ(pos)) return InComplete();

                    GLog.LogProcess($"H{gantry.Index + 1} SprayClean Count:{c + 1} start");

                    Thread.Sleep(dnwait);

                    sprayIO.Status = true;
                    Thread.Sleep(spraytime);
                    sprayIO.Status = false;

                    Thread.Sleep(postwait);

                    if (!gantry.MoveOpZAbs(0)) return InComplete();
                }
                return true;

                bool InComplete()
                {
                    gantry.MoveOpZAbs(0);
                    running = false;
                    return false;
                }
            }

            public bool Execute()
            {
                int dnwait, spraytime, postwait, count;

                dnwait = GProcessPara.NeedleSpray.DownWait[gantry.Index].Value;

                spraytime = GProcessPara.NeedleSpray.SprayTime[gantry.Index].Value;
                postwait = GProcessPara.NeedleSpray.PostWait[gantry.Index].Value;
                count = GProcessPara.NeedleSpray.Count[gantry.Index].Value;

                return Execute(dnwait, spraytime, postwait, count);
            }
            public bool Learn()
            {
                GEvent.Start(EEvent.NEEDLE_SPRAY_CLEAN, gantry.Name);

                PointXYZ pos = new PointXYZ(GSetupPara.NeedleSprayClean.Pos[gantry.Index]);
                PointXYZ laserPos = pos + GSetupPara.Calibration.LaserOffset[gantry.Index] + GSetupPara.NeedleSprayClean.HSensorOffset[gantry.Index];

                if (!gantry.GotoXYZ(laserPos)) return InComplete();

                Thread.Sleep(GProcessPara.HSensor.SettleTime.Value);
                double hSensorValue = 0;
                TFHSensors.Sensor[gantry.Index].GetValue(ref hSensorValue);

                GSetupPara.NeedleSprayClean.HSensorValue[gantry.Index] = hSensorValue;

                pos.X += GSetupPara.Calibration.NeedleXYOffset[gantry.Index].X;
                pos.Y += GSetupPara.Calibration.NeedleXYOffset[gantry.Index].Y;

                if (!gantry.GotoXYZ(pos)) return InComplete();

                return true;

                bool InComplete()
                {
                    gantry.MoveOpZAbs(0);
                    return false;
                }
            }
        }
        public static TCNeedleSprayClean[] SprayClean = new TCNeedleSprayClean[] { new TCNeedleSprayClean(TFGantry.GantrySetup), new TCNeedleSprayClean(TFGantry.GantryRight) };

    }


    public class TCCalibration
    {
        public class LaserOffset
        {
            readonly TEZMCAux.TGroup gantry;
            public ECalibrationState CalibrationState = ECalibrationState.Unknown;

            #region param
            PointXYZ laserCamPos = new PointXYZ();
            PointXYZ laserPos = new PointXYZ();
            PointD laserOffset = new PointD();
            LightRGBA lighting = new LightRGBA();
            #endregion

            public LaserOffset(TEZMCAux.TGroup gantry)
            {
                this.gantry = gantry;
            }

            public bool Execute_Manual()
            {
                GEvent.Start(EEvent.CALIBRATION_LASER_OFFSET_MANUAL, gantry.Name);

                CalibrationState = ECalibrationState.Fail;

                laserCamPos = new PointXYZ(GSetupPara.Calibration.LaserCamPos[gantry.Index]);
                laserPos = new PointXYZ(GSetupPara.Calibration.LaserCamPos[gantry.Index]);
                laserOffset = new PointD(GSetupPara.Calibration.LaserOffset[gantry.Index]);
                lighting = new LightRGBA(GSetupPara.Calibration.LaserOffsetLighting[gantry.Index]);

                if (!gantry.GotoXYZ(laserCamPos)) return false;
                if (!TFLightCtrl.LightPair[gantry.Index].Set(lighting)) return false;

                if (MsgBox.ShowDialog(gantry.Name + " Move Camera to Laser Offset Calibration location", MsgBoxBtns.YesNo) != DialogResult.Yes) return false;

                laserCamPos = gantry.PointXYZ;
                lighting = TFLightCtrl.LightPair[gantry.Index].CurrentLight;


                if (!TFLightCtrl.LightPair[gantry.Index].Off()) return false;
                if (!gantry.MoveOpZAbs(0)) return false;

                laserPos.X = laserCamPos.X + laserOffset.X;
                laserPos.Y = laserCamPos.Y + laserOffset.Y;

                if (!gantry.MoveOpXYAbs(laserPos.XYPos)) return false;
                if (MsgBox.ShowDialog(gantry.Name + " Move Laser Point to Laser Offset Calibration location.", MsgBoxBtns.YesNo) != DialogResult.Yes) return false;

                laserPos = gantry.PointXYZ;


                laserOffset.X = laserPos.X - laserCamPos.X;
                laserOffset.Y = laserPos.Y - laserCamPos.Y;

                GSetupPara.Calibration.LaserCamPos[gantry.Index] = new PointXYZ(laserCamPos);
                GSetupPara.Calibration.LaserOffset[gantry.Index] = new PointD(laserOffset);
                GSetupPara.Calibration.LaserOffsetLighting[gantry.Index] = new LightRGBA(lighting);

                CalibrationState = ECalibrationState.Completed;

                return true;
            }

            public bool Execute(bool fast = false)
            {
                GEvent.Start(EEvent.CALIBRATION_LASER_OFFSET, gantry.Name);

                if (fast && CalibrationState != ECalibrationState.Completed)
                {
                    MsgBox.ShowDialog(gantry.Name + " Complete Laser Cal before using Fast Mode");
                    return false;
                }

                CalibrationState = ECalibrationState.Fail;

                laserCamPos = new PointXYZ(GSetupPara.Calibration.LaserCamPos[gantry.Index]);
                laserPos = new PointXYZ(GSetupPara.Calibration.LaserCamPos[gantry.Index]);
                laserOffset = new PointD(GSetupPara.Calibration.LaserOffset[gantry.Index]);
                lighting = new LightRGBA(GSetupPara.Calibration.LaserOffsetLighting[gantry.Index]);

                if (!Initiate(fast)) return IncompleteCal();

                GSetupPara.Calibration.LaserCamPos[gantry.Index] = new PointXYZ(laserCamPos);
                GSetupPara.Calibration.LaserOffset[gantry.Index] = new PointD(laserOffset);
                GSetupPara.Calibration.LaserOffsetLighting[gantry.Index] = new LightRGBA(lighting);

                CalibrationState = ECalibrationState.Completed;

                return true;
            }

            bool Initiate(bool fast)
            {
                if (fast) return PromptMoveLaserToCal();

                if (!gantry.GotoXYZ(laserCamPos)) return false;
                if (!TFLightCtrl.LightPair[gantry.Index].Set(lighting)) return false;

                if (MsgBox.ShowDialog(gantry.Name + " Move Camera to Laser Offset Calibration location", MsgBoxBtns.YesNo) != DialogResult.Yes) return false;

                laserCamPos = gantry.PointXYZ;
                lighting = TFLightCtrl.LightPair[gantry.Index].CurrentLight;

                return PromptMoveLaserToCal();
            }
            bool PromptMoveLaserToCal()
            {
                if (!TFLightCtrl.LightPair[gantry.Index].Off()) return false;
                if (!gantry.MoveOpZAbs(0)) return false;

                laserPos.X = laserCamPos.X + laserOffset.X;
                laserPos.Y = laserCamPos.Y + laserOffset.Y;

                if (!gantry.MoveOpXYAbs(laserPos.XYPos)) return false;
                if (MsgBox.ShowDialog(gantry.Name + " Move Laser Point to Laser Offset Calibration location.", MsgBoxBtns.YesNo) != DialogResult.Yes) return false;

                laserPos = gantry.PointXYZ;

                return SearchEdges();
            }
            bool SearchEdges()
            {
                if (!gantry.MoveOpZAbs(0)) return false;

                double dSearchSpeed = 2.5;
                double dDetectDelta = 0.25;
                double dScanDist = 6;

                List<double> XEdge = new List<double>();
                List<double> YEdge = new List<double>();

                #region Find Edges
                for (int i = 0; i < 4; i++)//***dir XP,YP,XN,YN
                {
                    if (!gantry.MoveOpXYAbs(laserPos.XYPos)) return false;

                    Thread.Sleep(GProcessPara.HSensor.SettleTime.Value);

                    double startHeightValue = 0;
                    if (!TFHSensors.Sensor[gantry.Index].GetValue(ref startHeightValue)) return false;


                    PointD relDist = new PointD();
                    switch (i)
                    {
                        case 0:
                            relDist = new PointD(dScanDist, 0);
                            break;
                        case 1:
                            relDist = new PointD(0, dScanDist);
                            break;
                        case 2:
                            relDist = new PointD(-dScanDist, 0);
                            break;
                        case 3:
                            relDist = new PointD(0, -dScanDist);
                            break;
                    }

                    double[] speedProfile = new double[] { dSearchSpeed, dSearchSpeed, 500, 500, 0 };
                    gantry.MoveXYRel(speedProfile, relDist.ToArray, false);

                    while (true)
                    {
                        double heightValue = 0;
                        if (!TFHSensors.Sensor[gantry.Index].GetValue(ref heightValue)) return false;


                        if (Math.Abs(heightValue - startHeightValue) > dDetectDelta)
                        {
                            double x = gantry.XAxis.ActualPos;
                            double y = gantry.YAxis.ActualPos;
                            switch (i)
                            {
                                case 0:
                                case 2:
                                    XEdge.Add(x);
                                    break;
                                case 1:
                                case 3:
                                    YEdge.Add(y);
                                    break;
                            }
                            gantry.StopDecel();
                            while (gantry.Busy) Thread.Sleep(1);
                            break;
                        }

                        if (!gantry.Busy)
                        {
                            gantry.StopDecel();
                            MessageBox.Show("Find Edge Error.");
                            return false;
                        }
                    }
                }
                #endregion


                laserPos.X = XEdge.Average();
                laserPos.Y = YEdge.Average();

                laserOffset.X = laserPos.X - laserCamPos.X;
                laserOffset.Y = laserPos.Y - laserCamPos.Y;

                GLog.LogProcess($"New Laser offset generated {laserOffset.ToStringForDisplay()}");

                if (!gantry.MoveOpXYAbs(laserPos.XYPos)) return false;

                string msg = gantry.Name + $" Laser offset calibrated values: {laserOffset.ToStringForDisplay()}. Approve new values";
                if (MsgBox.ShowDialog(msg, MsgBoxBtns.OKCancel) != DialogResult.OK) return false;

                return true;
            }
            bool IncompleteCal()
            {
                TFLightCtrl.LightPair[gantry.Index].Off();
                gantry.MoveOpZAbs(0);
                MsgBox.ShowDialog(gantry.Name + $" Laser Cal Cal Incomplete");
                return false;
            }
        }
        public static LaserOffset[] LaserCal = new LaserOffset[] { new LaserOffset(TFGantry.GantryLeft), new LaserOffset(TFGantry.GantryRight) };

        public class NeedleXYOffset
        {
            bool Fast = false;

            public ECalibrationState CalibrationState = ECalibrationState.Unknown;
            readonly TEZMCAux.TGroup gantry;

            PointXYZ needleXYCam = new PointXYZ();
            PointXYZ needleXY = new PointXYZ();
            PointD needleXYOffset = new PointD();
            LightRGBA lighting = new LightRGBA();

            public NeedleXYOffset(TEZMCAux.TGroup gantry)
            {
                this.gantry = gantry;
            }

            //InitParam and Exec
            public bool Execute(bool fast = false)
            {
                Fast = fast;

                GEvent.Start(EEvent.CALIBRATION_NEEDLE_XY, gantry.Name);

                if (Fast && CalibrationState != ECalibrationState.Completed)
                {
                    MsgBox.ShowDialog("Manually Cal XY " + gantry.Name + " before enter Fast Mode");
                    return false;
                }

                CalibrationState = ECalibrationState.Fail;

                needleXYCam = new PointXYZ(GSetupPara.Calibration.NeedleXYCamPos[gantry.Index]);
                needleXY = new PointXYZ(GSetupPara.Calibration.NeedleXYCamPos[gantry.Index]);
                needleXYOffset = new PointD(GSetupPara.Calibration.NeedleXYOffset[gantry.Index]);
                lighting = new LightRGBA(GSetupPara.Calibration.NeedleXYLighting[gantry.Index]);

                if (!Initiate()) return IncompleteCal();

                GSetupPara.Calibration.NeedleXYCamPos[gantry.Index] = new PointXYZ(needleXYCam);
                GSetupPara.Calibration.NeedleXYOffset[gantry.Index] = new PointD(needleXYOffset);
                GSetupPara.Calibration.NeedleXYLighting[gantry.Index] = new LightRGBA(lighting);

                CalibrationState = ECalibrationState.Completed;
                return true;
            }

            //Sequences
            bool Initiate()
            {
                if (Fast) return PromptMoveNeedleToMark();

                if (!gantry.GotoXYZ(needleXYCam)) return false;
                if (!TFLightCtrl.LightPair[gantry.Index].Set(lighting)) return false;

                if (MsgBox.ShowDialog("Move Camera to Needle " + gantry.Name + " Calibration Location", MsgBoxBtns.OKCancel) != DialogResult.OK) return false;

                needleXYCam = gantry.PointXYZ;
                lighting = TFLightCtrl.LightPair[gantry.Index].CurrentLight;

                return PromptMoveNeedleToMark();
            }

            bool PromptMoveNeedleToMark()
            {
                if (!gantry.GotoXYZ(new PointXYZ(needleXYCam.X + needleXYOffset.X, needleXYCam.Y + needleXYOffset.Y, 0))) return false;
                if (!TFLightCtrl.LightPair[gantry.Index].Off()) return false;
                if (MsgBox.ShowDialog("Move Needle " + gantry.Name + " to Calibration Location", MsgBoxBtns.OKCancel) != DialogResult.OK) return false;


                return Comfirmation();
            }
            bool Comfirmation()
            {
                needleXY = gantry.PointXYZ;
                if (!gantry.GotoXYZ(needleXYCam)) return false;

                if (!TFLightCtrl.LightPair[gantry.Index].Set(lighting)) return false;
                if (MsgBox.ShowDialog("Move Camera to " + gantry.Name + " Mark Location.", MsgBoxBtns.OKCancel) != DialogResult.OK) return false;

                needleXYCam = gantry.PointXYZ;
                var newoffset = new PointD(needleXY.X - needleXYCam.X, needleXY.Y - needleXYCam.Y);
                GLog.LogProcess(gantry.Name + " new XY Offset generated => {newoffset.ToStringForDisplay()}");
                if (MsgBox.ShowDialog("Update " + gantry.Name + $" offset value from\r\n{needleXYOffset.ToStringForDisplay()} to {newoffset.ToStringForDisplay()}", MsgBoxBtns.OKCancel) != DialogResult.OK) return false;
                needleXYOffset = new PointD(newoffset);
                lighting = TFLightCtrl.LightPair[gantry.Index].CurrentLight;

                if (!TFLightCtrl.LightPair[gantry.Index].Off()) return false;

                return true;
            }
            bool IncompleteCal()
            {
                TFLightCtrl.LightPair[gantry.Index].Off();
                MsgBox.ShowDialog(gantry.Name + " XY Cal Incomplete");
                return false;
            }
        }
        public static NeedleXYOffset[] NeedleXYOffsets = new NeedleXYOffset[] { new NeedleXYOffset(TFGantry.GantryLeft), new NeedleXYOffset(TFGantry.GantryRight) };

        public class NeedleZTouch
        {
            readonly TEZMCAux.TGroup gantry;
            public enum EZCalMode { Normal, Manual }
            public ECalibrationState CalibrationState = ECalibrationState.Unknown;


            #region
            public const double TouchSensitivity = 0.002;//mm
            public const double OverTravel = 0.5;//mm
            public const double ClearDist = 0.5;//mm
            public const int TouchCount = 3;//count
            public const double MaxError = 0.01;//mm
            public const double PromptDownDist = 5;

            PointXYZ zTouchCamPos = new PointXYZ();
            double hSensorValue = 0;
            double zTouch = 0;
            LightRGBA lighting = new LightRGBA();

            PointD needleXYOffset = new PointD();
            PointD laserOffset = new PointD();

            const int SelectedLight = 0;
            #endregion

            public NeedleZTouch(TEZMCAux.TGroup gantry)
            {
                this.gantry = gantry;
            }
            public bool Execute(EZCalMode calMode, bool fast = false)
            {

                GEvent.Start(EEvent.CALIBRATION_ZTOUCH_OFFSET, gantry.Name);

                if (fast && CalibrationState != ECalibrationState.Completed)
                {
                    MsgBox.ShowDialog(gantry.Name + $" Complete Z Cal before using Fast Mode");
                    return false;
                }

                CalibrationState = ECalibrationState.Fail;

                zTouchCamPos = new PointXYZ(GSetupPara.Calibration.ZTouchCamPos[gantry.Index]);
                hSensorValue = GSetupPara.Calibration.HSensorValue[gantry.Index];
                zTouch = GSetupPara.Calibration.ZTouchValue[gantry.Index];
                lighting = new LightRGBA(GSetupPara.Calibration.ZTouchLighting[gantry.Index]);

                needleXYOffset = new PointD(GSetupPara.Calibration.NeedleXYOffset[gantry.Index]);
                laserOffset = new PointD(GSetupPara.Calibration.LaserOffset[gantry.Index]);

                switch (calMode)
                {
                    case EZCalMode.Manual:
                        {
                            GEvent.Start(EEvent.CALIBRATION_ZTOUCH_OFFSET_MANUAL);

                            if (!gantry.GotoXYZ(zTouchCamPos)) return false;

                            if (!TFLightCtrl.LightPair[gantry.Index].Set(lighting)) return false;
                            if (MsgBox.ShowDialog(gantry.Name + " Move Camera to Needle Touch location.", MsgBoxBtns.OKCancel) != DialogResult.OK) return false;

                            zTouchCamPos = gantry.PointXYZ;
                            lighting = TFLightCtrl.LightPair[gantry.Index].CurrentLight;

                            if (!gantry.MoveOpXYAbs(new double[] { zTouchCamPos.X + laserOffset.X, zTouchCamPos.Y + laserOffset.Y })) return false;
                            Thread.Sleep(GProcessPara.HSensor.SettleTime.Value);
                            if (!TFHSensors.Sensor[gantry.Index].GetValue(ref hSensorValue)) return false;

                            if (!gantry.GotoXYZ(new PointXYZ(zTouchCamPos.X + needleXYOffset.X, zTouchCamPos.Y + needleXYOffset.Y, 0))) return false;

                            if (MsgBox.ShowDialog(gantry.Name + " Move Needle Z to Touch Z location.", MsgBoxBtns.OKCancel) != DialogResult.OK) return false;
                            zTouch = gantry.ZAxis.ActualPos;

                            if (!gantry.MoveOpZAbs(0)) return false;

                            break;
                        }
                    case EZCalMode.Normal:
                        {
                            if (!Initiate(fast)) return IncompleteCal();

                            break;
                        }
                }


                GSetupPara.Calibration.ZTouchCamPos[gantry.Index] = new PointXYZ(zTouchCamPos);
                GSetupPara.Calibration.HSensorValue[gantry.Index] = hSensorValue;
                GSetupPara.Calibration.ZTouchValue[gantry.Index] = zTouch;
                GSetupPara.Calibration.ZTouchLighting[gantry.Index] = new LightRGBA(lighting);
                CalibrationState = ECalibrationState.Completed;

                if (!gantry.MoveOpZAbs(0)) return false;

                GLog.LogProcess(gantry.Name + " TouchZ Cal Completed");
                //MsgBox.ShowDialog(gantry.Name + " TouchZ Cal Completed");
                return true;
            }

            //Sequences
            bool Initiate(bool fast)
            {
                GEvent.Start(EEvent.CALIBRATION_ZTOUCH_OFFSET);

                if (fast) return PromptMoveNeedleToTouchZ();

                if (!gantry.GotoXYZ(zTouchCamPos)) return false;

                if (!TFLightCtrl.LightPair[gantry.Index].Set(lighting)) return false;
                if (MsgBox.ShowDialog(gantry.Name + " Move Camera to Needle Touch location.", MsgBoxBtns.OKCancel) != DialogResult.OK) return false;

                zTouchCamPos = gantry.PointXYZ;
                lighting = TFLightCtrl.LightPair[gantry.Index].CurrentLight;

                return PromptMoveNeedleToTouchZ();
            }
            bool inteMode = true;//intelligent mode
            bool PromptMoveNeedleToTouchZ()
            {
                if (!TFLightCtrl.LightPair[gantry.Index].Off()) return false;

                if (inteMode)
                {
                    List<double> hSensorValues = new List<double>();
                    if (!gantry.MoveOpZAbs(0)) return false;

                    PointD ofst = new PointD(0, 0);
                    for (int i = 0; i < 5; i++)
                    {
                        switch (i)
                        {
                            case 0: ofst = new PointD(-1, -1); break;
                            case 1: ofst = new PointD(1, -1); break;
                            case 2: ofst = new PointD(1, 1); break;
                            case 3: ofst = new PointD(-1, 1); break;
                            case 4: ofst = new PointD(0, 0); break;
                        }

                        if (!gantry.MoveOpXYAbs(new double[] { zTouchCamPos.X + laserOffset.X + ofst.X, zTouchCamPos.Y + laserOffset.Y + ofst.Y })) return false;
                        Thread.Sleep(GProcessPara.HSensor.SettleTime.Value);
                        if (!TFHSensors.Sensor[gantry.Index].GetValue(ref hSensorValue)) return false;
                        hSensorValues.Add(hSensorValue);
                    }

                    double dirtyness = 0.05;

                    double range = hSensorValues.Max() - hSensorValues.Min();
                    if (range > dirtyness)
                    {
                        MsgBox.ShowDialog(gantry.Name + $" Touch Stage Dirty Score {range:f3}. " + '\r' + "Clean Touch Stage and try again.");
                        return false;
                    }
                }

                if (!gantry.GotoXYZ(new PointXYZ(zTouchCamPos.X + laserOffset.X, zTouchCamPos.Y + laserOffset.Y, 0))) return false;
                Thread.Sleep(GProcessPara.HSensor.SettleTime.Value);
                if (!TFHSensors.Sensor[gantry.Index].GetValue(ref hSensorValue)) return false;

                if (!gantry.GotoXYZ(new PointXYZ(zTouchCamPos.X + needleXYOffset.X, zTouchCamPos.Y + needleXYOffset.Y, 0))) return false;

                switch (GSystemCfg.Config.ZTouchType)
                {
                    case EZTouchType.LINEAR:
                        {
                            return SearchTouch_Linear(gantry);
                        }
                    case EZTouchType.IO:
                        throw new Exception("Touch IO not supported.");
                }
                return false;
            }
            bool SearchTouch_Linear(TEZMCAux.TGroup gantrySelect)
            {
                TFGantry.TouchAxis[gantrySelect.Index].ActualPos = 0;
                Thread.Sleep(100);

                double EnoderRes = GProcessPara.Calibration.ZTouchEncoderRes[gantry.Index].Value;

                double[] speedProfileC = new double[] { 1, 5, 100, 100, 0 };
                if (!gantry.ZAxis.MoveAbs(speedProfileC, zTouch - OverTravel, false)) return false;

                _Retry:
                while (true)
                {
                    double mPos = TFGantry.TouchAxis[gantrySelect.Index].ActualPos;
                    if (Math.Abs(mPos) > EnoderRes)
                    //if (mPos > 0.002 || mPos < -0.002)
                    {
                        gantry.ZAxis.StopEmg();
                        gantry.ZAxis.Wait();
                        Thread.Sleep(100);

                        mPos = TFGantry.TouchAxis[gantrySelect.Index].ActualPos;
                        zTouch = gantrySelect.ZAxis.ActualPos - mPos;
                        break;
                    }

                    if (!gantry.ZAxis.Busy)
                    {
                        if (MsgBox.ShowDialog(gantry.Name + " Z Touch not found. Continue?", MsgBoxBtns.YesNo) != DialogResult.Yes) return false;
                        if (!gantry.ZAxis.MoveRel(speedProfileC, -5, false)) return false;
                        goto _Retry;
                    }
                }

                if (!gantry.MoveOpZAbs(0)) return false;

                return true;
            }
            //static bool SearchTouch_IO()
            //{
            //    #region Search Z
            //    double[] speedProfileC = new double[] { 1, 15, 100, 100, 0 };
            //    if (!gantry.ZAxis.MoveAbs(speedProfileC, zTouch - OverTravel, false)) return false;

            //    while (true)
            //    {
            //        if (!TFGantry.ZSensor)
            //        {
            //            if (!gantry.ZAxis.StopEmg()) return false;
            //            if (!gantry.ZAxis.Wait()) return false;
            //            break;
            //        }
            //        if (!gantry.ZAxis.Busy) break;
            //    }

            //    if (!gantry.ZAxis.StopEmg()) return false;
            //    if (!gantry.ZAxis.Wait()) return false;

            //    while (TFGantry.ZSensor)
            //    {
            //        if (MsgBox.ShowDialog("Z Touch not found. Continue?", MessageBoxButtons.YesNo) != DialogResult.Yes) return false;

            //        if (!gantry.ZAxis.MoveRel(speedProfileC, -PromptDownDist, false)) return false;


            //        while (true)
            //        {
            //            if (!TFGantry.ZSensor)
            //            {
            //                if (!gantry.ZAxis.StopEmg()) return false;
            //                if (!gantry.ZAxis.Wait()) return false;
            //                break;
            //            }
            //            if (!gantry.ZAxis.Busy) break;
            //        }
            //    }

            //    if (!gantry.ZAxis.StopEmg()) return false;
            //    if (!gantry.ZAxis.Wait()) return false;
            //    #endregion

            //    List<double> listTouchZ = new List<double>();

            //    var cleardist = 0.2;
            //    var overtravel = 0.2;

            //    for (int k = 0; k < TouchCount; k++)
            //    {
            //        double[] speedProfile1 = new double[] { 0.2, 0.3, 1, 1, 0 };
            //        if (!gantry.ZAxis.MoveRel(speedProfile1, cleardist, true)) return false;

            //        if (!gantry.ZAxis.MoveRel(speedProfileC, -cleardist - overtravel, true)) return false;

            //        double[] speedProfile2 = new double[] { 0.1, 0.15, 1, 1, 0 };
            //        if (!gantry.ZAxis.MoveRel(speedProfile2, cleardist + overtravel, false)) return false;

            //        while (true)
            //        {
            //            if (TFGantry.ZSensor)
            //            {
            //                double pos = gantry.ZAxis.ActualPos;
            //                if (!gantry.ZAxis.StopEmg()) return false;
            //                if (!gantry.ZAxis.Wait()) return false;

            //                listTouchZ.Add(pos);
            //                break;
            //            }
            //            if (!gantry.ZAxis.Busy) break;
            //        }
            //    }

            //    GLog.LogProcess(gantry.Name + $" new ZTouch[IO] value in average generated => {string.Join(",", listTouchZ)}");


            //    if (listTouchZ.Count < TouchCount)
            //    {
            //        MsgBox.ShowDialog("Incomplete " + gantry.Name + $" Touch Z: {listTouchZ.Count}");
            //        return false;
            //    }

            //    if (listTouchZ.Max() - listTouchZ.Min() > MaxError)
            //    {
            //        MsgBox.ShowDialog($"Abnormal " + gantry.Name + $" Z Touch: {string.Join(",", listTouchZ)}");
            //        return false;
            //    }

            //    if (!gantry.MoveOpZAbs(0)) return false;

            //    var avgtouch = listTouchZ.Average();
            //    if (!touchdot)
            //    {
            //        if (MsgBox.ShowDialog($"New value:{avgtouch}, Old value:{zTouch} \r\nProceed new value for " + gantry.Name + " TouchZ?", MessageBoxButtons.YesNo) != DialogResult.Yes) return false;
            //    }

            //    zTouch = avgtouch;
            //    return true;
            //}


            bool IncompleteCal()
            {
                TFLightCtrl.LightPair[gantry.Index].Off();
                gantry.MoveOpZAbs(0);
                MsgBox.ShowDialog(gantry.Name + " TouchZ Cal Incomplete");
                return false;
            }
        }
        public static NeedleZTouch[] NeedleZTouches = new NeedleZTouch[] { new NeedleZTouch(TFGantry.GantryLeft), new NeedleZTouch(TFGantry.GantryRight) };

        public class DynamicOffset
        {
            public ECalibrationState[] CalibrationState = new ECalibrationState[4];
            public enum EDir { XPOS, XNEG, YPOS, YNEG }

            readonly TEZMCAux.TGroup gantry;

            #region param
            double DispGap = 5;

            PointXYZ dynamicXYCam = new PointXYZ();
            LightRGBA lighting = new LightRGBA();

            PointD laseroffset = new PointD();
            double zTouch = 0;
            double hsensorvalue = 0;

            static PointD needleXYOffset = new PointD();
            static EDir direction = EDir.XPOS;

            double dyspeed = 0;
            double dyacc = 0;
            double dyaccdist = 0;

            int dydotcount = 1;
            double dydotpitch;
            #endregion

            public DynamicOffset(TEZMCAux.TGroup gantry)
            {
                this.gantry = gantry;
            }

            public bool Execute()
            {
                zTouch = GSetupPara.Calibration.ZTouchValue[gantry.Index];
                hsensorvalue = GSetupPara.Calibration.HSensorValue[gantry.Index];
                DispGap = GProcessPara.Calibration.DynamicJetGap[gantry.Index].Value;

                laseroffset = GSetupPara.Calibration.LaserOffset[gantry.Index];
                dynamicXYCam = new PointXYZ(GSetupPara.Calibration.DynamicXYCamPos[gantry.Index]);
                needleXYOffset = new PointD(GSetupPara.Calibration.NeedleXYOffset[gantry.Index]);
                lighting = new LightRGBA(GSetupPara.Calibration.DynamicLighting[gantry.Index]);

                dyspeed = GProcessPara.Calibration.DynamicTouchDotSpd[gantry.Index].Value;
                dyacc = GProcessPara.Calibration.DynamicTouchDotAcc[gantry.Index].Value;
                dyaccdist = GProcessPara.Calibration.DynamicAccelDist[gantry.Index].Value;

                dydotcount = GProcessPara.Calibration.DynamicDotCount[gantry.Index].Value;
                dydotpitch = GProcessPara.Calibration.DynamicPitch[gantry.Index].Value;
                direction = GProcessPara.Calibration.Dirs[gantry.Index];

                CalibrationState[(int)direction] = ECalibrationState.Fail;

                if (!MovetoCamPos()) return gantry.MoveOpZAbs(0);

                CalibrationState[(int)direction] = ECalibrationState.Completed;

                return true;
            }
            bool MovetoCamPos()
            {
                if (!gantry.GotoXYZ(dynamicXYCam, false)) return false;
                TFLightCtrl.LightPair[gantry.Index].Set(lighting);
                if (MsgBox.ShowDialog("Define Cam Pos, OK to continue", MsgBoxBtns.OKCancel) != DialogResult.OK) return false;
                dynamicXYCam = gantry.PointXYZ;
                lighting = TFLightCtrl.LightPair[gantry.Index].CurrentLight;

                return Initialize();
            }
            bool Initialize()
            {
                double hvalue = 0;

                if (!gantry.MoveOpXYAbs((dynamicXYCam.GetPointD() + laseroffset).ToArray)) return false;
                Thread.Sleep(GProcessPara.HSensor.SettleTime.Value);

                if (!TFHSensors.Sensor[gantry.Index].GetValue(ref hvalue)) return false;

                if (MsgBox.ShowDialog($"Laser Value: {hvalue}\nProceed?", MsgBoxBtns.OKCancel) != DialogResult.OK) return false;
                if (Math.Abs(hvalue) > 900)
                {
                    GAlarm.Prompt(EAlarm.CONFOCAL_VALUE_ERROR);
                    return false;
                }

                if (!gantry.MoveOpXYAbs((dynamicXYCam.GetPointD() + needleXYOffset).ToArray)) return false;

                double absz = zTouch - (hvalue - hsensorvalue) + DispGap;

                //if (!gantry.ZAxis.SetParam(.GZSetParam(zAxis)) return false;
                if (!gantry.MoveOpZAbs(absz)) return false;

                try
                {
                    TEZMCAux.TOutput trigIO = null;
                    TEZMCAux.TOutput fpressIO = null;
                    TEZMCAux.TOutput ppressIO = null;

                    var isPnuematic = false;
                    switch (GSystemCfg.Pump.Pumps[gantry.Index].PumpType)
                    {
                        case EPumpType.VERMES_3280:
                            {

                                TFPressCtrl.FPress[gantry.Index].Set(GRecipes.Vermes_Setups[gantry.Index].FPress.Value);
                                if (!TFPump.Vermes_Pump[gantry.Index].TriggerAset(GRecipes.Vermes_Setups[gantry.Index])) return false;

                                trigIO = GMotDef.Outputs[(int)GSystemCfg.Pump.Pumps[gantry.Index].DispDO];
                                fpressIO = GMotDef.Outputs[(int)GSystemCfg.Pump.Pumps[gantry.Index].FPressDO];
                                fpressIO.Status = true;
                                break;

                            }
                        case EPumpType.PNEUMATIC_JET:
                            {
                                isPnuematic = true;
                                TFPressCtrl.FPress[gantry.Index].Set(GRecipes.PneumaticJet_Setups[gantry.Index].FPress.Value);

                                fpressIO = GMotDef.Outputs[(int)GSystemCfg.Pump.Pumps[gantry.Index].FPressDO];
                                trigIO = ppressIO = GMotDef.Outputs[(int)GSystemCfg.Pump.Pumps[gantry.Index].PPressDO];
                                fpressIO.Status = true;
                                trigIO.Status = false;

                                break;
                            }
                        default:
                            {
                                MsgBox.ShowDialog($"Invalid Pump Type registered in Pump {gantry.Name}");
                                return false;
                            }
                    }

                    //var dir = direction;
                    // v^2 = u^2 - 2as
                    //u default : 0
                    //var s = (dyspeed * dyspeed / (2 * dyacc)) + dyaccdist;

                    var accdist = dyaccdist;
                    var pitch = dydotpitch;

                    double[] arr_accdist = new double[4];
                    double[] arr_pitch = new double[4];

                    switch (direction)
                    {
                        case EDir.XNEG: arr_accdist[0] = -accdist; arr_pitch[0] = -pitch; break;
                        case EDir.XPOS: arr_accdist[0] = accdist; arr_pitch[0] = pitch; break;
                        case EDir.YNEG: arr_accdist[1] = -accdist; arr_pitch[1] = -pitch; break;
                        case EDir.YPOS: arr_accdist[1] = accdist; arr_pitch[1] = pitch; break;
                    }



                    if (!gantry.MoveOpXYRel(new double[] { -arr_accdist[0] - (arr_pitch[0] * dydotcount), -arr_accdist[1] - (arr_pitch[1] * dydotcount) })) return false;

                    Thread.Sleep(500);

                    var abspt = gantry.PointXY;

                    string cmdbuffer = string.Empty;
                    //string sBase = gantry.Index is 0 ? "BASE(6,7,8) " : "BASE(9,10,11) ";
                    string sBase = gantry.sBase;
                    cmdbuffer = sBase;

                    cmdbuffer += $"MERGE=1 ";
                    cmdbuffer += $"ACCEL={dyacc} ";
                    cmdbuffer += $"FORCE_SPEED={dyspeed} ";
                    TEZMCAux.DirectCommand(cmdbuffer);
                    cmdbuffer = sBase;
                    abspt += new PointD(arr_accdist[0], arr_accdist[1]);

                    List<double> table_points = new List<double>();


                    for (var i = 0; i < dydotcount; i++)
                    {
                        abspt += new PointD(arr_pitch[0], arr_pitch[1]);
                        table_points.Add((int)direction > 1 ? abspt.Y : abspt.X);
                        table_points.Add((int)direction > 1 ? abspt.Y + (arr_pitch[1] / 2) : abspt.X + (arr_pitch[0] / 2));
                    }

                    int table_startIdx = (gantry.Index + 1) * 10000;
                    int table_endIdx = table_startIdx + table_points.Count - 1;

                    for (int i = 0; i < table_points.Count; i++)
                    {
                        var tablecmd = $"TABLE({table_startIdx + i},{table_points[i]}) ";
                        TEZMCAux.DirectCommand(tablecmd);
                    }
                    cmdbuffer = $"BASE({(direction > EDir.XNEG ? gantry.YAxis.AxisNo : gantry.XAxis.AxisNo)}) ";
                    cmdbuffer += $"HW_PSWITCH2(2) ";

                    cmdbuffer += $"HW_PSWITCH2({1},{trigIO.OutputNo},1,{table_startIdx},{table_endIdx},{(table_points[1] - table_points[0] > 0 ? 1 : 0)}) ";


                    if (isPnuematic)
                    {
                        var pneumaticJet_Params = new PneumaticJet_Param(GRecipes.PneumaticJet_Setups[gantry.Index]);
                        double cycletime = pneumaticJet_Params.DispTime.Value + pneumaticJet_Params.OffTime.Value;
                        double optime = pneumaticJet_Params.DispTime.Value;

                        cmdbuffer += $"HW_TIMER(2, {Math.Max(2, cycletime * 1000)},{Math.Max(1, optime * 1000)},1,OFF,{trigIO.OutputNo}) ";
                    }

                    TEZMCAux.Execute(cmdbuffer);

                    cmdbuffer = sBase;
                    abspt += new PointD(arr_accdist[0], arr_accdist[1]);
                    cmdbuffer += $"MOVEABSSP({abspt.X},{abspt.Y}) ";
                    cmdbuffer += $"MOVE_OP({fpressIO.OutputNo},0) ";
                    TEZMCAux.DirectCommand(cmdbuffer);

                    Thread.Sleep(100);

                    while (gantry.Busy) Thread.Sleep(0);

                    cmdbuffer = $"BASE({(direction > EDir.XNEG ? gantry.YAxis.AxisNo : gantry.XAxis.AxisNo)}) ";
                    cmdbuffer += $"MERGE=0 ";
                    cmdbuffer += $"HW_PSWITCH2(2) ";
                    TEZMCAux.Execute(cmdbuffer);

                    if (!gantry.MoveOpZAbs(0)) return false;

                    return Comfirmation();
                }
                catch
                {
                    return false;
                }
            }
            bool Comfirmation()
            {
                if (!gantry.GotoXYZ(dynamicXYCam)) return false;
                TFLightCtrl.LightPair[gantry.Index].Set(lighting);

                if (MsgBox.ShowDialog($"Move Camere to {gantry.Name} Mark Location.", MsgBoxBtns.OKCancel) != DialogResult.OK) return false;
                TFLightCtrl.LightPair[gantry.Index].Off();

                var xycam = gantry.PointXYZ.GetPointD();

                GLog.LogProcess($"{gantry.Name} New XY offset from TouchDot generated => {(dynamicXYCam.GetPointD() - xycam).ToStringForDisplay()}");
                GSetupPara.Calibration.DynamicOffsets[gantry.Index, (int)direction] = dynamicXYCam.GetPointD() - xycam;
                GSetupPara.Calibration.DynamicXYCamPos[gantry.Index] = new PointXYZ(dynamicXYCam);
                GSetupPara.Calibration.DynamicLighting[gantry.Index] = new LightRGBA(lighting);
                return true;
            }

        }
        public static DynamicOffset[] DynamicOffsets = new DynamicOffset[] { new DynamicOffset(TFGantry.GantryLeft), new DynamicOffset(TFGantry.GantryRight) };

        public class FullCal
        {
            public enum ECalMode { TouchMark, TouchDot }

            readonly TEZMCAux.TGroup gantry;

            #region param
            int gantryIdx => gantry.Index;
            double zTouch = 0;
            PointD needleXYOffset = new PointD();
            PointXYZ zTouchCamPos = new PointXYZ();

            PointXYZ needleXYCam = new PointXYZ();
            double XYTouchMarkGap = 0.5;
            LightRGBA lighting = new LightRGBA();
            #endregion

            public void Execute(ECalMode calMode)
            {
                if (!NeedleZTouches[gantryIdx].Execute(NeedleZTouch.EZCalMode.Normal)) return;

                zTouch = GSetupPara.Calibration.ZTouchValue[gantryIdx];
                needleXYOffset = GSetupPara.Calibration.NeedleXYOffset[gantryIdx];
                zTouchCamPos = GSetupPara.Calibration.ZTouchCamPos[gantryIdx];

                needleXYCam = GSetupPara.Calibration.NeedleXYCamPos[gantryIdx];
                XYTouchMarkGap = GProcessPara.Calibration.XYTouchMarkGap[gantryIdx].Value;
                lighting = GSetupPara.Calibration.NeedleXYLighting[gantryIdx];

                NeedleXYOffsets[gantryIdx].CalibrationState = ECalibrationState.Fail;

                switch (calMode)
                {
                    case ECalMode.TouchMark:
                        {
                            if (!TouchMark())
                            {
                                if (!gantry.MoveOpZAbs(0)) return;
                                return;
                            }
                            break;
                        }
                    case ECalMode.TouchDot:
                        {
                            if (!TouchDot())
                            {
                                if (!gantry.MoveOpZAbs(0)) return;
                                return;
                            }
                            break;
                        }
                }

                GSetupPara.Calibration.NeedleXYOffset[gantryIdx] = new PointD(needleXYOffset);
                NeedleXYOffsets[gantryIdx].CalibrationState = ECalibrationState.Completed;
                GLog.LogProcess(gantry.Name + $" New XY offset from {calMode} generated => {needleXYOffset.ToStringForDisplay()}");

                if (!gantry.MoveOpZAbs(0)) return;
            }
            bool TouchDot()
            {
                var dispgap = GProcessPara.Calibration.TouchDotDispGap[gantryIdx].Value;
                var absTouchDotZ = zTouch + Math.Abs(dispgap);
                if (!gantry.MoveOpZAbs(absTouchDotZ)) return false;

                var dispCtrl = GSystemCfg.Pump.Pumps[gantryIdx];

                var fpressIO = GMotDef.Outputs[(int)dispCtrl.FPressDO];
                var ppressIO = GMotDef.Outputs[(int)dispCtrl.PPressDO];
                var vacIO = GMotDef.Outputs[(int)dispCtrl.VacDO];
                var trigIO = GMotDef.Outputs[(int)dispCtrl.DispDO];


                switch (dispCtrl.PumpType)
                {
                    case EPumpType.VERMES_3280:
                        {
                            var setup = GRecipes.Vermes_Setups[gantryIdx];

                            TFPressCtrl.FPress[gantryIdx].Set(setup.FPress.Value);
                            if (!TFPump.Vermes_Pump[gantry.Index].TriggerAset(setup)) return false;

                            Thread.Sleep(500);
                            fpressIO.Status = true;
                            trigIO.Status = true;
                            Thread.Sleep(500);
                            trigIO.Status = false;
                            fpressIO.Status = false;
                        }
                        break;
                    case EPumpType.PNEUMATIC_JET:
                        {
                            var setup = GRecipes.PneumaticJet_Setups[gantryIdx];

                            TFPressCtrl.FPress[gantryIdx].Set(setup.FPress.Value);
                            TFPressCtrl.FPress[gantryIdx + 2].Set(setup.VPress.Value);

                            fpressIO.Status = true;
                            ppressIO.Status = false;

                            Thread.Sleep(500);

                            TFPump.PnuematicJet.Shot_One(gantryIdx, setup, fpressIO, ppressIO);

                            fpressIO.Status = false;

                            break;
                        }
                    case EPumpType.HM:
                        {
                            var setup = GRecipes.HM_Setups[gantryIdx];
                            TFPressCtrl.FPress[gantryIdx].Set(setup.FPress.Value);

                            TFPump.HM.Shot_One(gantryIdx, setup, fpressIO, vacIO);

                            break;
                        }

                    case EPumpType.SP:
                    case EPumpType.SPLite:
                        {
                            var setup = GRecipes.SP_Setups[gantryIdx];
                            TFPressCtrl.FPress[gantryIdx].Set(setup.FPress.Value);
                            TFPressCtrl.FPress[gantryIdx + 2].Set(setup.PPress.Value);

                            TFPump.SP.Shot_One(gantryIdx, setup, fpressIO, ppressIO, vacIO);
                            break;
                        }
                    default:
                        {
                            MsgBox.ShowDialog($"Invalid Pump Type registered in DispCtrl{gantryIdx + 1}");
                            return false;
                        }
                }

                gantry.MoveOpZAbs(0);

                var needleXY = gantry.PointXY;

                gantry.GotoXYZ(new PointXYZ(needleXY.X - needleXYOffset.X, needleXY.Y - needleXYOffset.Y, zTouchCamPos.Z));

                TFLightCtrl.LightPair[gantryIdx].Set(GSetupPara.Calibration.NeedleXYLighting[gantryIdx]);
                if (MsgBox.ShowDialog("Move Camere to " + gantry.Name + " Mark Location.", MsgBoxBtns.OKCancel) != DialogResult.OK) return false;
                TFLightCtrl.LightPair[gantryIdx].Off();

                var needleXYCam = gantry.PointXYZ;
                needleXYOffset = new PointD(needleXY.X - needleXYCam.X, needleXY.Y - needleXYCam.Y);

                return true;
            }
            bool TouchMark()
            {


                if (!gantry.MoveOpXYAbs((needleXYCam.GetPointD() + needleXYOffset).ToArray)) return false;
                var needleXY = gantry.PointXYZ;
                if (!gantry.MoveOpZAbs(zTouch + XYTouchMarkGap)) return false;

                Thread.Sleep(500);
                if (!gantry.MoveOpZAbs(0)) return false;

                if (!gantry.GotoXYZ(needleXYCam)) return false;
                if (!TFLightCtrl.LightPair[gantry.Index].Set(lighting)) return false;
                if (MsgBox.ShowDialog("Move Camera to " + gantry.Name + " Mark Location.", MsgBoxBtns.OKCancel) != DialogResult.OK) return false;

                needleXYCam = gantry.PointXYZ;
                var newoffset = new PointD(needleXY.X - needleXYCam.X, needleXY.Y - needleXYCam.Y);
                needleXYOffset = new PointD(newoffset);
                if (!TFLightCtrl.LightPair[gantry.Index].Off()) return false;

                return true;
            }

            public FullCal(TEZMCAux.TGroup gantry)
            {
                this.gantry = gantry;
            }
        }
        public static FullCal[] FullCals = new FullCal[] { new FullCal(TFGantry.GantryLeft), new FullCal(TFGantry.GantryRight) };
    }


    public class TCExternalFunc
    {
        public class NozzleInspection
        {
            static Mutex Mutex = new Mutex();

            TEZMCAux.TGroup gantry;

            public NozzleInspection(TEZMCAux.TGroup gantry)
            {
                this.gantry = gantry;
            }

            public bool Execute(TEZMCAux.TOutput trigIO, TEZMCAux.TInput statusOK_IO, TEZMCAux.TInput statusNG_IO, int wait_time_ms, PointXYZ pos)
            {
                try
                {
                    var crtpos = gantry.PointXYZ;

                    if (!gantry.MoveOpZAbs(0)) return false;

                    Mutex.WaitOne();

                    if (!gantry.GotoXYZ(pos)) return false;

                    trigIO.Status = true;
                    Thread.Sleep(wait_time_ms);

                    var state = statusOK_IO.Status && !statusNG_IO.Status;

                    trigIO.Status = false;

                    if (!gantry.MoveOpZAbs(0)) return false;
                    if (!gantry.MoveOpXYAbs(crtpos.XYPos)) return false;
                    return state;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    Mutex.ReleaseMutex();
                }
            }
            public bool Execute()
            {
                return Execute(GMotDef.Outputs[GProcessPara.NozzleInspection.DO_Idx], GMotDef.Inputs[GProcessPara.NozzleInspection.DI_Idx[0]],
                    GMotDef.Inputs[GProcessPara.NozzleInspection.DI_Idx[1]], GProcessPara.NozzleInspection.WaitTime.Value, GSetupPara.NozzleInsp.Pos[gantry.Index]);
            }
        }
        public static NozzleInspection[] NoozleInsps = new NozzleInspection[] { new NozzleInspection(TFGantry.GantryLeft), new NozzleInspection(TFGantry.GantryRight) };
    }

    public class TCWafer
    {
        static readonly TEZMCAux.TGroup gantry = TFGantry.GantryLeft;

        public static readonly TEZMCAux.TOutput WaferVacLow = GMotDef.Out43;
        public static readonly TEZMCAux.TOutput ChuckVac = GMotDef.Out44;
        public static readonly TEZMCAux.TOutput WaferVacHigh = GMotDef.Out45;
        public static readonly TEZMCAux.TOutput WaferExh = GMotDef.Out46;
        public static readonly TEZMCAux.TOutput AirBlow = GMotDef.Out51;

        public static readonly TEZMCAux.TInput ChuckVacSens = GMotDef.IN41;
        public static readonly TEZMCAux.TInput WaferVacHighSens = GMotDef.IN42;
        public static readonly TEZMCAux.TInput WaferVacLowSens = GMotDef.IN35;

        public static TEZMCAux.TInput SMEMA_UP_IN = GMotDef.IN32;
        public static TEZMCAux.TInput SMEMA_DN_IN = GMotDef.IN33;
        public static TEZMCAux.TOutput SMEMA_UP_OUT = GMotDef.Out36;
        public static TEZMCAux.TOutput SMEMA_DN_OUT = GMotDef.Out37;

        public static bool SMEMA_ING = false;

        public static bool IsWaferDetected => WaferVacHighSens.Status || WaferVacLowSens.Status;

        public static TEZMCAux.TOutput SvIonizer = GMotDef.Out41;

        public static bool IsNotch = false;

        //lifer motor stroke 21 - lifter Z-dimension stroke 12
        public static bool LifterUp()
        {
            int axisno = GMotDef.Lifter.AxisNo;

            TEZMCAux.Execute($"BASE({axisno}) SPEED={GProcessPara.Wafer.LifterSpeed.Value}");

            GMotDef.GRAxis.SetParam(0, 30, 500, 500);

            if (!GMotDef.GRAxis.MoveAbs(0)) return false;

            double dist = GProcessPara.Wafer.LifterStroke.Value;
            TEZMCAux.Execute($"BASE({axisno}) MOVEABS({dist})");
            while (GMotDef.Lifter.Busy) Thread.Sleep(0);
            return true;
        }
        public static bool PreciserOn(bool airon = false)
        {
            GMotDef.GRAxis.SetParam(0, 30, 500, 500);

            if (!GMotDef.GRAxis.MoveAbs(0)) return false;
            string sbase = $"BASE({GMotDef.Preciser_0.AxisNo},{GMotDef.Preciser_1.AxisNo},{GMotDef.Preciser_2.AxisNo}) ";
            sbase += $"SPEED={GProcessPara.Wafer.PrecisorSpeed.Value},ACCEL={GProcessPara.Wafer.PrecisorAccel.Value},DECEL={GProcessPara.Wafer.PrecisorAccel.Value} ";
            TEZMCAux.Execute(sbase);
            sbase = $"BASE({GMotDef.Preciser_0.AxisNo},{GMotDef.Preciser_1.AxisNo},{GMotDef.Preciser_2.AxisNo}) ";
            sbase += $"MOVEABS({GSetupPara.Wafer.PrecisorPos_1.Value},{GSetupPara.Wafer.PrecisorPos_2.Value},{GSetupPara.Wafer.PrecisorPos_3.Value}) ";
            TEZMCAux.Execute(sbase);
            while (GMotDef.Preciser_0.Busy) Thread.Sleep(0);
            while (GMotDef.Preciser_1.Busy) Thread.Sleep(0);
            while (GMotDef.Preciser_2.Busy) Thread.Sleep(0);

            return true;
        }

        public static bool PrecisorHoming()
        {
            try
            {
                int homemode = 4;

                for (int i = 0; i < 3; i++)
                {
                    TEZMCAux.Execute($"BASE({i}) SPEED={50}");
                    TEZMCAux.Execute($"DATUM({0})AXIS({i})");
                    TEZMCAux.Execute($"DATUM({homemode})AXIS({i})");
                }
                while (GMotDef.Preciser_0.Busy) Thread.Sleep(0);
                while (GMotDef.Preciser_1.Busy) Thread.Sleep(0);
                while (GMotDef.Preciser_2.Busy) Thread.Sleep(0);

            }
            catch
            {
                return false;
            }
            return true;

        }
        public static bool LifterHoming()
        {
            int timeout_ms = 10000;

            try
            {
                int axisno = GMotDef.Lifter.AxisNo;
                int homemode = 4;

                TEZMCAux.Execute($"BASE({axisno}) SPEED={GProcessPara.Wafer.LifterSpeed.Value}");
                TEZMCAux.Execute($"DATUM({0})AXIS({axisno})");
                TEZMCAux.Execute($"DATUM({homemode})AXIS({axisno})");

                Stopwatch sw = new Stopwatch();
                sw.Start();
                while (GMotDef.Lifter.Busy)
                {
                    Thread.Sleep(0);
                    if (sw.ElapsedMilliseconds > timeout_ms)
                    {
                        throw new Exception("Check speed or lifter motor off issue");
                    }
                }
            }
            catch (Exception ex)
            {
                GAlarm.Prompt(EAlarm.WAFER_LIFTER_HOMING_FAIL, ex);
                return false;
            }
            return true;
        }

        static bool PreAirBlow()
        {
            if (!GProcessPara.Wafer.PreAirBlow) return true;

            AirBlow.Status = false;
            if (!gantry.GotoXYZ(GSetupPara.Wafer.AirBlowPos)) return false;
            AirBlow.Status = true;
            Thread.Sleep(GProcessPara.Wafer.AirBlowDuration.Value);
            AirBlow.Status = false;

            return true;
        }
        static bool ChuckVacToggle(bool state)
        {
            ChuckVac.Status = state;
            Thread.Sleep(500);
            return ChuckVacSens.Status == state;
        }

        static bool CatchWafer()
        {
            IsNotch = false;

            try
            {

                if (!PrecisorHoming()) return false;
                if (!LifterUp()) return false;

                WaferVacHigh.Status = GProcessPara.Wafer.PreVacuumEnable;
                WaferVacLow.Status = GProcessPara.Wafer.PreVacuumEnable;

                if (!LifterHoming()) return false;

                WaferVacHigh.Status = false;
                WaferVacLow.Status = false;

                Task.Run(() =>
                {
                    Thread.Sleep(GProcessPara.Wafer.PreExhaustDelay.Value);

                    WaferExh.Status = true;
                    Thread.Sleep(GProcessPara.Wafer.PreExhaustTime.Value);
                    WaferExh.Status = false;
                });

                if (!PreciserOn()) return false;

                if (!gantry.GotoXYZ(GSetupPara.Wafer.AirBlowPos)) return false;

                WaferVacHigh.Status = false;
                WaferVacLow.Status = false;

                AirBlow.Status = true;
                Thread.Sleep(GProcessPara.Wafer.AirBlowDuration.Value - GProcessPara.Wafer.PreOnVacuum.Value);
                WaferVacHigh.Status = true;
                WaferVacLow.Status = true;
                Thread.Sleep(GProcessPara.Wafer.PreOnVacuum.Value);
                AirBlow.Status = false;

                if (!PrecisorHoming()) return false;

                Thread.Sleep(100);

                if (!WaferVacHighSens.Status || !WaferVacLowSens.Status)
                {
                    GAlarm.Prompt(EAlarm.WAFER_NO_DETECTED, "Check Wafer in positon");
                    return false;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        static bool ReleaseWafer()
        {
            IsNotch = false;

            try
            {
                if (!PrecisorHoming()) return false;

                WaferVacHigh.Status = false;
                WaferVacLow.Status = false;
                WaferExh.Status = true;
                Thread.Sleep(GProcessPara.Wafer.PostExhaustTime.Value);
                WaferExh.Status = false;

                if (!LifterUp()) return false;

            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool Manual_Load()
        {
            try
            {
                foreach (var board in Inst.Board) board.ClearData();

                GMotDef.GRAxis.SetParam(0, 30, 500, 500);

                if (!ChuckVacToggle(true)) return false;

                WaferVacHigh.Status = true;
                WaferVacLow.Status = true;

                Thread.Sleep(100);

                if (WaferVacHighSens.Status || WaferVacLowSens.Status)
                {
                    WaferVacHigh.Status = false;
                    WaferVacLow.Status = false;

                    GAlarm.Prompt(EAlarm.WAFER_DETECTED, "Remove wafer from table chuck");
                    return false;
                }

                WaferVacHigh.Status = false;
                WaferVacLow.Status = false;

                if (!PreAirBlow()) return false;

                if (!gantry.GotoXYZ(GSetupPara.Wafer.ManualLoadPos)) return false;

                if (!GMotDef.GRAxis.MoveAbs(0)) return false;

                if (!LifterUp()) return false;

                if (MsgBox.ShowDialog("Place product on chuck table.\r\nOk - Continue\r\nCancel - stop", MsgBoxBtns.OKCancel) != DialogResult.OK) return LifterHoming();

                if (!CatchWafer()) return false;

                MsgBox.ShowDialog("Manual Load Complated");
            }
            catch
            {
                return false;
            }
            return true;
        }
        public static bool Manual_Unload()
        {
            try
            {
                foreach (var board in Inst.Board) board.ClearData();

                GMotDef.GRAxis.SetParam(0, 30, 500, 500);

                if (!gantry.GotoXYZ(GSetupPara.Wafer.ManualLoadPos)) return false;
                if (!GMotDef.GRAxis.MoveAbs(0)) return false;

                if (!ReleaseWafer()) return false;

                MsgBox.ShowDialog("Unload product.");

                if (!LifterHoming()) return false;

                MsgBox.ShowDialog("Finish unload.");

                //if (!ChuckVacToggle(false)) return false;

            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool AutoLoad(int timeout_ms = 5000)
        {
            try
            {
                foreach (var board in Inst.Board) board.ClearData();

                GMotDef.GRAxis.SetParam(0, 30, 500, 500);

                if (!ChuckVacToggle(true)) return false;

                if (WaferVacHighSens.Status)
                {
                    MsgBox.ShowDialog("Object Sensed, Operation Fail");
                    return false;
                }

                SMEMA_ING = true;

                if (!PreAirBlow()) return false;

                if (!gantry.GotoXYZ(GSetupPara.Wafer.AutoLoadPos)) return false;

                if (!LifterUp()) return false;

                SMEMA_UP_OUT.Status = true;

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                while (!SMEMA_UP_IN.Status)
                {
                    Thread.Sleep(0);
                    if (!SMEMA_ING)
                    {
                        SMEMA_UP_OUT.Status = false;
                        return false;
                    }
                    if (stopwatch.ElapsedMilliseconds > timeout_ms)
                    {
                        SMEMA_UP_OUT.Status = false;
                        MsgBox.ShowDialog($"Load Wafer Timeout.\nSetting Time {timeout_ms} ms");
                        return false;
                    }
                }

                if (!CatchWafer()) return false;

                SMEMA_UP_OUT.Status = false;

            }
            catch
            {
                return false;
            }
            finally
            {
                SMEMA_UP_OUT.Status = false;
                SMEMA_ING = false;
            }
            return true;
        }
        public static bool AutoUnload()
        {
            try
            {
                foreach (var board in Inst.Board) board.ClearData();

                GMotDef.GRAxis.SetParam(0, 30, 500, 500);

                SMEMA_ING = true;

                if (!gantry.GotoXYZ(GSetupPara.Wafer.AutoLoadPos)) return false;

                if (!ReleaseWafer()) return false;

                SMEMA_DN_OUT.Status = true;
                Thread.Sleep(200);
                SMEMA_DN_OUT.Status = false;

                while (!SMEMA_DN_IN.Status)
                {
                    Thread.Sleep(0);
                    if (!SMEMA_ING)
                    {
                        SMEMA_DN_OUT.Status = false;
                        return false;
                    }
                }

                SMEMA_DN_OUT.Status = false;

                if (!LifterHoming()) return false;

                //if (!ChuckVacToggle(false)) return false;

            }
            catch
            {
                return false;
            }
            finally
            {
                SMEMA_DN_OUT.Status = false;
                SMEMA_ING = false;
            }
            return true;

        }

        static TEZMCAux.TAxis RAxis = GMotDef.GRAxis;

        public static bool StopNotch = false;
        public static bool NotchAlignmentLaser(double stepheight = 0, int angle = 0, double speed = 0)
        {

            try
            {
                IsNotch = false;

                if (!IsWaferDetected)
                {
                    GAlarm.Prompt(EAlarm.WAFER_NOTCH_ALIGNMENT_FAIL, "No Wafer Detected");
                    return false;
                }

                if (stepheight <= 0) stepheight = GProcessPara.Wafer.WaferThickness.Value * 0.8;
                if (angle <= 0) angle = GProcessPara.Wafer.NotchAngleCheck.Value;
                if (speed <= 0) speed = GProcessPara.Wafer.NotchAlignSpeed.Value;

                //List<double> hvaluelist = new List<double>();

                if (!LifterHoming()) return false;
                if (!PrecisorHoming()) return false;

                var xypos = GSetupPara.Wafer.TeachNotchCamPos.GetPointD() + GSetupPara.Calibration.LaserOffset[gantry.Index];
                if (!gantry.GotoXYZ(new PointXYZ(xypos.X, xypos.Y, 0))) return false;

                double rotaryactualpos = RAxis.ActualPos;

                double notch_edge_1 = 0;//left notch edge

                //apply detection every 45 degree, 360/45 = 8 times shift checking
                for (int i = 0; i < 361; i += angle)
                {
                    if (StopNotch)
                    {
                        StopNotch = false;
                        return false;
                    }
                    if (!findnotchedge()) return false;

                    while (gantry.Busy) Thread.Sleep(0);
                    while (RAxis.Busy) Thread.Sleep(0);
                    Thread.Sleep(50);
                    //couter-clockwise

                    GLog.LogProcess($"Notch alignment Start R Pos:{RAxis.ActualPos}, degree:{i}");

                    double firsthvalue = double.NaN;
                    double hvalue = -1;

                    if (!RAxis.MoveRel(new double[] { 0, speed, 500, 500, 0 }, angle, false)) return false;

                    bool exec = true;
                    while (exec = TFHSensors.Sensor[gantry.Index].GetValue(ref hvalue))
                    {
                        if (double.IsNaN(firsthvalue))
                        {
                            GLog.LogProcess($"Notch alignment firstvalue: {hvalue}");

                            firsthvalue = hvalue;
                            continue;
                        }

                        //***\____
                        if (notch_edge_1 is 0)
                        {
                            //5-3=2
                            if (((hvalue - firsthvalue) > stepheight) || (hvalue <= -50))
                            {
                                notch_edge_1 = RAxis.ActualPos;
                                RAxis.StopEmg();
                                RAxis.SetParam(0, 30, 500, 500);

                                GLog.LogProcess($"Notch > alignment successfully: {hvalue}");
                                return IsNotch = true;
                            }

                            //3-5=-2
                            if ((firsthvalue - hvalue) < -stepheight)
                            {
                                RAxis.StopEmg();
                                RAxis.SetParam(0, 30, 500, 500);
                                RAxis.MoveRel(-1);

                                notch_edge_1 = RAxis.ActualPos;

                                GLog.LogProcess($"Notch < alignment successfully: {hvalue}");
                                return IsNotch = true;
                            }
                        }

                        if (notch_edge_1 != 0) break;
                        if (!RAxis.Busy) break;
                    }
                    if (!exec) throw new Exception("no value");
                    if (notch_edge_1 is 0) continue;
                }

                //GAlarm.Prompt(EAlarm.WAFER_NOTCH_ALIGNMENT_FAIL);
                return false;
            }

            catch (Exception ex)
            {
                GAlarm.Prompt(EAlarm.WAFER_NOTCH_ALIGNMENT_FAIL, ex);
                return false;
            }

            finally
            {
                RAxis.SetParam(0, 30, 500, 500);
                RAxis.StopEmg();
            }

            bool findnotchedge(/*double stepheight*/)
            {
                #region
                try
                {
                    double hvalue = 0;
                    bool findedge = false;
                    List<double> hvaluelist = new List<double>();

                    var xypos = GSetupPara.Wafer.TeachNotchCamPos.GetPointD() + GSetupPara.Calibration.LaserOffset[gantry.Index];

                    //*******\________
                    #region
                    var edgeRev = GProcessPara.Wafer.NotchEdgeRev.Value;

                    if (!gantry.GotoXYZ(new PointXYZ(xypos.X, xypos.Y + edgeRev, 0))) return false;

                    var YAxis = gantry.YAxis;

                    YAxis.MoveRel(new double[] { 0, 2, 1000, 1000, 0 }, -5, false);
                    while (TFHSensors.Sensor[gantry.Index].GetValue(ref hvalue))
                    {
                        hvaluelist.Add(hvalue);
                        if (hvaluelist.Count > 1)
                        {
                            if ((hvalue - hvaluelist.FirstOrDefault() > stepheight) || (hvalue <= -50))
                            {
                                //get edge pos
                                var edgepos = YAxis.ActualPos;
                                //stop movement
                                YAxis.StopEmg();
                                while (YAxis.Busy) Thread.Sleep(0);
                                Thread.Sleep(100);
                                YAxis.MoveAbs(edgepos + 1, true);
                                while (gantry.Busy) Thread.Sleep(0);
                                Thread.Sleep(100);

                                findedge = true;
                                return true;
                            }
                        }
                        if (!YAxis.Busy) break;
                    }
                    #endregion

                    return findedge;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    gantry.SetParam(GProcessPara.Operation.GXYStartSpeed.Value, GProcessPara.Operation.GXYFastSpeed.Value, GProcessPara.Operation.GXYAccel.Value, GProcessPara.Operation.GXYDecel.Value, GProcessPara.Operation.GXYJerk.Value);
                }
                #endregion
            }
        }

        public static bool NotchAlignmentVision()
        {
            try
            {
                if (GRecipes.PatRecog[0].Count < 10) throw new Exception("Learn Vision before execute");
                TPatRect patRect = GRecipes.PatRecog[0][10];
                double patScore = GProcessPara.Wafer.NotchVisonScore.Value;
                Image<Gray, byte> refimg = patRect.RegImage[0];

                gantry.MoveAbs(GSetupPara.Wafer.TeachNotchCamPos);
                Thread.Sleep(GProcessPara.Vision.SettleTime.Value);
                TFLightCtrl.LightPair[gantry.Index].Set(GSetupPara.Wafer.PatLightRGBA);

                var fCam = TFCamera1.Cameras[gantry.Index];
                fCam.Snap();
                var img = fCam.emgucvImage.Clone();
                fCam.Live();

                PointD p1 = new PointD();
                PointD p2 = new PointD();
                double score = 0;
                TFVision.PatMatch(img, refimg, patRect.ImgThld[0], new Rectangle[] { patRect.SearchRect[0], patRect.PatRect[0] }, ref p1, ref p2, ref score);

                if (score > patScore) return true;


            }
            catch (Exception ex)
            {
                GAlarm.Prompt(EAlarm.WAFER_NOTCH_ALIGNMENT_FAIL, ex);
                return false;
            }

            return false;
        }

        public static bool NotchAlignment()
        {
            if (!GProcessPara.Wafer.IsNotchVisionEnable) return NotchAlignmentLaser();

            if (NotchAlignmentVision()) return true;

            int count = GProcessPara.Wafer.NotchVisonRepeatCount.Value;

            for (int i = 0; i < count; i++)
            {
                NotchAlignmentLaser();

                if (NotchAlignmentVision()) break;
            }

            return true;
        }
    }


    public class TFSafety
    {
        public static TEZMCAux.TOutput DoorLock => GMotDef.Out11;
        public static TEZMCAux.TInput DoorClosed => GMotDef.IN14;
        public static TEZMCAux.TInput DoorLocked => GMotDef.IN15;
        public static TEZMCAux.TInput TeachAutoSens => GMotDef.IN16;

        public static bool LockDoor()
        {
            try
            {
                DoorLock.Status = false;

                while (!DoorClosed.Status)
                {
                    if (MsgBox.ShowDialog("Pls Close the Door", MsgBoxBtns.OKCancel) == DialogResult.Cancel) return false;
                }

                GLog.LogProcess("Door Closed");

                DoorLock.Status = true;

                var delay = GSystemCfg.Config.SafetyDoorDelaySens_Seconds * 1000;
                if (delay <= 0) delay = 5000;
                Thread.Sleep(delay);

                if (!DoorLocked.Status) throw new Exception($"{DoorLocked.Name} Sense Fail.");

                GLog.LogProcess("Door Locked");

                return true;
            }
            catch (Exception ex)
            {
                GAlarm.Prompt(EAlarm.DOOR_LOCK_FAIL, ex);
                return false;
            }

        }
        public static bool ReleaseDoor()
        {
            while (!TeachAutoSens.Status)
            {
                if (MsgBox.ShowDialog("Pls Set Key to Teach Mode", MsgBoxBtns.OKCancel) == DialogResult.Cancel)
                {
                    if (MsgBox.ShowDialog("Unlock door under Auto Mode \nwill turn machine down,\nStill continue?", MsgBoxBtns.OKCancel) == DialogResult.OK) break;
                }
            }

            GLog.LogProcess("Door Unlocked");
            DoorLock.Status = false;

            return true;
        }
    }
}