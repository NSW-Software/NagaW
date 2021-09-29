using NagaW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace NagaW
{
    class TCSystem
    {
        public static void FactoryDefault()
        {
            //default values
            switch (GSystemCfg.MakerData.EquipmentModel)
            {
                default://EEquipmentModel.SD1X:
                    GSystemCfg.HSensor.HSensors[0].StartUpEnable = true;
                    GSystemCfg.HSensor.HSensors[0].Type = TEMEDAQ.EType.SENSOR_ILD1320;
                    GSystemCfg.HSensor.HSensors[0].Comport = ECOM.COM7;
                    GSystemCfg.HSensor.HSensors[1].StartUpEnable = true;
                    GSystemCfg.HSensor.HSensors[1].Type = TEMEDAQ.EType.SENSOR_ILD1320;
                    GSystemCfg.HSensor.HSensors[1].Comport = ECOM.COM8;
                    break;
                case EEquipmentModel.SD1X:
                    GSystemCfg.HSensor.HSensors[0].StartUpEnable = true;
                    GSystemCfg.HSensor.HSensors[0].Type = TEMEDAQ.EType.CL3000;
                    GSystemCfg.HSensor.HSensors[0].Comport = ECOM.IP;
                    GSystemCfg.HSensor.HSensors[0].IPAddress = "192.168.1.11";
                    GSystemCfg.HSensor.HSensors[1].StartUpEnable = false;
                    GSystemCfg.HSensor.HSensors[1].Type = TEMEDAQ.EType.NONE;
                    GSystemCfg.HSensor.HSensors[1].Comport = ECOM.NONE;
                    break;
            }

            GSystemCfg.Camera.Cameras[0].StartUpEnable = true;
            GSystemCfg.Camera.Cameras[0].IPAddress = "192.168.0.101";
            GSystemCfg.Camera.Cameras[1].StartUpEnable = true;
            GSystemCfg.Camera.Cameras[1].IPAddress = "192.168.0.102";

            switch (GSystemCfg.MakerData.EquipmentModel)
            {
                default://EEquipmentModel.SD1X:
                    GSystemCfg.Camera.Cameras[0].DistPerPixelX = 0.006;
                    GSystemCfg.Camera.Cameras[0].DistPerPixelY = 0.006;
                    GSystemCfg.Camera.Cameras[1].DistPerPixelX = 0.006;
                    GSystemCfg.Camera.Cameras[1].DistPerPixelY = 0.006;
                    break;
                case EEquipmentModel.SD1X:
                    GSystemCfg.Camera.Cameras[0].DistPerPixelX = 0.00345;
                    GSystemCfg.Camera.Cameras[0].DistPerPixelY = 0.00345;
                    GSystemCfg.Camera.Cameras[1].DistPerPixelX = 0.00345;
                    GSystemCfg.Camera.Cameras[1].DistPerPixelY = 0.00345;
                    break;
            } 

        GSystemCfg.Light.Lights.StartUpEnable = true;
            switch (GSystemCfg.MakerData.EquipmentModel)
            {
                default://EEquipmentModel.SD1X:
                    GSystemCfg.Light.Lights.LeftBoardID = 0;
                    GSystemCfg.Light.Lights.LeftStartChannel = 1;
                    GSystemCfg.Light.Lights.LeftChannelCount = 2;
                    GSystemCfg.Light.Lights.RightBoardID = 0;
                    GSystemCfg.Light.Lights.RightStartChannel = 3;
                    GSystemCfg.Light.Lights.RightChannelCount = 2;
                    break;
                case EEquipmentModel.SD1X:
                    GSystemCfg.Light.Lights.LeftBoardID = 0;
                    GSystemCfg.Light.Lights.LeftStartChannel = 1;
                    GSystemCfg.Light.Lights.LeftChannelCount = 4;
                    GSystemCfg.Light.Lights.RightBoardID = 1;
                    GSystemCfg.Light.Lights.RightStartChannel = 1;
                    GSystemCfg.Light.Lights.RightChannelCount = 4;
                    break;
            }

            GSystemCfg.FPress.FPresses[0].StartUpEnable = true;
            GSystemCfg.FPress.FPresses[0].Comport = ECOM.COM1;
            GSystemCfg.FPress.FPresses[1].StartUpEnable = true;
            GSystemCfg.FPress.FPresses[1].Comport = ECOM.COM2;

            switch (GSystemCfg.MakerData.EquipmentModel)
            {
                default://EEquipmentModel.SD1X:
                    GSystemCfg.FPress.FPresses[2].StartUpEnable = false;
                    GSystemCfg.FPress.FPresses[3].StartUpEnable = false;
                    break;
                case EEquipmentModel.SD1X:
                    GSystemCfg.FPress.FPresses[2].StartUpEnable = true;
                    GSystemCfg.FPress.FPresses[2].Comport = ECOM.COM7;
                    GSystemCfg.FPress.FPresses[3].StartUpEnable = true;
                    GSystemCfg.FPress.FPresses[3].Comport = ECOM.COM8;
                    break;
            }

            GSystemCfg.Pump.Pumps[0].PumpType = EPumpType.SPLite;
            GSystemCfg.Pump.Pumps[0].DispDI = EDInput.DI04;
            GSystemCfg.Pump.Pumps[0].CleanAirDO = EDOutput.DO43;
            GSystemCfg.Pump.Pumps[0].CleanVacDO = EDOutput.DO44;
            GSystemCfg.Pump.Pumps[0].DispDO = EDOutput.DO03;
            GSystemCfg.Pump.Pumps[0].FPressDO = EDOutput.DO00;
            GSystemCfg.Pump.Pumps[0].VacDO = EDOutput.DO01;
            GSystemCfg.Pump.Pumps[0].PPressDO = EDOutput.DO02;
            GSystemCfg.Pump.Pumps[0].Comport = ECOM.COM9;

            GSystemCfg.Pump.Pumps[1].PumpType = EPumpType.SPLite;
            GSystemCfg.Pump.Pumps[1].DispDI = EDInput.DI09;
            GSystemCfg.Pump.Pumps[1].CleanAirDO = EDOutput.DO49;
            GSystemCfg.Pump.Pumps[1].CleanVacDO = EDOutput.DO50;
            GSystemCfg.Pump.Pumps[1].DispDO = EDOutput.DO08;
            GSystemCfg.Pump.Pumps[1].FPressDO = EDOutput.DO05;
            GSystemCfg.Pump.Pumps[1].VacDO = EDOutput.DO06;
            GSystemCfg.Pump.Pumps[1].PPressDO = EDOutput.DO07;
            GSystemCfg.Pump.Pumps[1].Comport = ECOM.COM10;
        }

        public static bool StartUp()
        {
            GEvent.Start(EEvent.STARTUP_SOFTWARE);

            FactoryDefault();

            #region Load Machine Cfg
            GSystemCfg.LoadFile();
            GMotDef.LoadFile();
            GProcessPara.LoadFile();
            GSetupPara.LoadFile();
            TFTool.Load();
            TFUser.LoadFile();
            #endregion

            RunExternalApp();

            try
            {
                if (!TFGantry.Open())
                {
                    TFGantry.GetErrorCode();
                    if (MsgBox.ShowDialog("Open Motion Controller Failed. Continue with Offline Mode?", MsgBoxBtns.YesNo) == DialogResult.Yes) goto _LoadRecipe;
                    Environment.Exit(0);
                }
            }
            catch
            {
                if (MsgBox.ShowDialog("Open Motion Controller Failed. Continue with Offline Mode?", MsgBoxBtns.YesNo) == DialogResult.Yes) goto _LoadRecipe;
                Environment.Exit(0);
            }

            #region Load Modules
            MsgBox.Processing("Start Up, Pls Wait...", () =>
            {
                if (GSystemCfg.Light.Lights.StartUpEnable) TFLightCtrl.Lights.Open();
                for (int k = 0; k < GSystemCfg.FPress.Count; k++) if (GSystemCfg.FPress.FPresses[k].StartUpEnable) TFPressCtrl.FPress[k].Open();
                
                if (GSystemCfg.Temperature.Temp.StartUpEnable) TFTempCtrl.TempCtrl.Open();
                foreach (var temp in GSystemCfg.Temperature.Temp.Channels) TFTempCtrl.TempCtrl.Run(temp.Address);

                for (int k = 0; k < GSystemCfg.HSensor.Count; k++) if (GSystemCfg.HSensor.HSensors[k].StartUpEnable) TFHSensors.Sensor[k].Open();
                for (int k = 0; k < GSystemCfg.Camera.Count; k++) if (GSystemCfg.Camera.Cameras[k].StartUpEnable) try { TFCameras.Camera[k].Connect(); } catch { };


                for (int k = 0; k < GSystemCfg.Pump.Count; k++)
                {
                    var dpCtrl = GSystemCfg.Pump.Pumps[k];
                    switch (dpCtrl.PumpType)
                    {
                        case EPumpType.VERMES_3280:
                            if (dpCtrl.StartUpEnable)
                            {
                                TFPump.Vermes_Pump[k].Open();
                                //if (dpCtrl.ExTempCtrlStartUpEnable) MHC48_1.MHC48_1s[k].Open();
                            }
                            break;
                        case EPumpType.SP:
                        //case EPumpType.VERMES_1560:
                        //case EPumpType.HM:
                        //case EPumpType.PP4:
                        //case EPumpType.VERMES_3200:
                        default:
                            break;
                    }

                }
            });
            #endregion

            #region Initialization
            if (GSystemCfg.Config.StartUpInitialize && MsgBox.ShowDialog("Initialize?", MsgBoxBtns.OKCancel) == DialogResult.OK) MsgBox.Processing("Homing in Progress.", () =>
            {
                InitAll();
            });

            _LoadRecipe:
            if (GSystemCfg.Config.StartUpLoadRecipe) GRecipes.LoadLastWrite();
            return true;
            #endregion
        }
        public static bool ShutDown()
        {
            GEvent.Start(EEvent.SHUTDOWN_SOFTWARE);
            GSystemCfg.SaveFile();
            GMotDef.SaveFile();
            GSetupPara.SaveFile();
            GProcessPara.SaveFile();
            TFUser.SaveFile();
            TFTool.Save();

            MsgBox.Processing("Shut Down, Pls Wait...", () =>
            {
                TFLightCtrl.Lights.Close();
                TFPressCtrl.Close();

                foreach (var temp in GSystemCfg.Temperature.Temp.Channels) TFTempCtrl.TempCtrl.Stop(temp.Address);
                TFTempCtrl.TempCtrl.Close();

                for (int k = 0; k < GSystemCfg.HSensor.Count; k++) TFHSensors.Sensor[k].Close();
                for (int k = 0; k < GSystemCfg.Camera.Count; k++) TFCameras.Camera[k].Disconnect();
                
                TFPump.Close();

            });

            TFGantry.Close();

            return true;
        }

        public static bool InitAll()
        {
            if (!TEZMCAux.Ready()) return false;
            if (!TFGantry.CheckEMO) return false;

            //clear disp buffer
            foreach(var board in Inst.Board)
            {
                board.ClearData();
            }

            GDefine.SystemState = ESystemState.Initing;

            try
            {
                TFGantry.InitAll();

                bool ok = TFGantry.GLStatus == EStatus.Ready && TFGantry.GRStatus == EStatus.Ready;
                
                TFConv.InitAll();
  
                ok = ok && TFLConv.Status == EStatus.Ready && TFRConv.Status == EStatus.Ready;

                if (ok) GDefine.SystemState = ESystemState.Ready;
                return true;
            }
            catch (Exception ex)
            {
                GDefine.SystemState = ESystemState.ErrorInit;
                GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString()); 
                return false;
            }
        }

        public static void RunExternalApp()
        {
            //string PathCamera = "";
            //IniFile iniFile = new IniFile(GDoc.CamPathFile.FullName);
            //if (iniFile.ReadString("CCDCamera", "CameraPath", ref PathCamera))
            //{
            //    string camApp = GDoc.MonCameraDir + "Camera.exe";

            //    if (!File.Exists(camApp)) return;//No available App
                
            //    try
            //    {
            //        var processExists = Process.GetProcesses().Any(p => p.ProcessName.Equals("Camera"));
            //        if (processExists)
            //        {
            //            //Application already running, return
            //            return;
            //        }
            //        Process.Start(PathCamera + "\\Camera.exe");
            //    }
            //    catch(Exception e)
            //    {
            //        RunFail(e.Message);
            //    }
            //}
            //else
            //{
            //    iniFile.Write("CCDCamera", "CameraPath", "");
            //    RunFail("Path is missing");
            //}

            //void RunFail(string Message = "")
            //{
            //    GAlarm.Prompt(EAlarm.CCDCAMERA_OPENFAIL, Message);
            //}

            string extAppName = GSystemCfg.Advance.ExternalApp;
            if (!File.Exists(extAppName)) return;//No available App

            try
            {
                var processExists = Process.GetProcesses().Any(p => p.ProcessName.Equals(Path.GetFileNameWithoutExtension(extAppName)));
                if (processExists) return;//Application already running, return

                GEvent.Start(EEvent.START_EXTERNAL_APP, extAppName);

                MsgBox.Processing("Starting " + Path.GetFileNameWithoutExtension(extAppName) + ", Pls Wait...", () => Process.Start(extAppName));
            }
            catch
            {
                GAlarm.Prompt(EAlarm.START_EXTERNAL_APP_FAIL, extAppName);
            }
        }
    }
}
