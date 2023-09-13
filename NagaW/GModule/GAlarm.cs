using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace NagaW
{
    class GAlarm
    {
        static Mutex Mtx = new Mutex();

        public static void Prompt(EAlarm alarm, string msg = "")
        {
            Task.Run(() =>
            {
                Mtx.WaitOne();

                TFTower.Error(true);
                //GEvent.Start(EEvent.PROMPT_ALARM, alarm.ToString() + " " + msg);
                GLog.WriteLog(ELogType.ALARM, $"{alarm} {msg}");
                new frmPrompt(SystemIcons.Error, Color.Red, $"{(int)alarm:d4}", alarm.ToString() + "\r\n\n" + msg).ShowDialog();
                TFTower.Error(false);

                string data = $"10100000,{alarm},{msg}";
                TFSecsGems.SendMsg(GemTaro.SECSII.SFCode.S5F1, data);
                TEZMCAux.SideDoorDetected = false;

                Mtx.ReleaseMutex();
            });
        }
        public static void Prompt(EAlarm alarm, Exception ex)
        {
            Prompt(alarm, ex.Message);
        }

        public static void Help(EAlarm eAlarm)
        {
            //pdf find word
            string pdf = GDoc.AlarmHelpDir.FullName + ((int)eAlarm).ToString("d4") + ".pdf";
            try
            {
                System.Diagnostics.Process.Start(pdf);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + pdf);
            }
        }
    }

    public enum EAlarm : ulong
    {
        UNKNOWN_ERROR,

        MOTION_CTRL_OPEN_NETWORK_FAIL,//open controller network fail
        MOTION_CTRL_OFFLINE_ERROR,//controller init error
        MOTION_CTRL_INIT_ERROR,//controller init error
        MOTION_CTRL_COMM_ERROR,//controller comm error
        MOTION_CTRL_NOT_READY,//controller is not ready

        EMO_ACTIVATED,//EMO activated or not reset
        MAIN_AIR_PRESSURE_NOT_READY,

        SAVE_FILE_ERROR,
        LOAD_FILE_ERROR,

        GLHOME_ERROR,
        GLHOME_TIMEOUT,
        GRHOME_ERROR,
        GRHOME_TIMEOUT,
        CLHOME_ERROR,
        CLHOME_TIMEOUT,
        CRHOME_ERROR,
        CRHOME_TIMEOUT,
        GANTRY_ERROR,//one or more axis in error state

        AXIS_UNRECOVERABLE_ERROR_RESTART,

        #region module
        CAMERA_UNRECOVERABLE_ERROR_RESTART,
        CAMERA_CONNECT_NOT_FOUND,
        CAMERA1_UNRECOVERABLE_ERROR_RESTART,
        CAMERA1_CONNECT_NOT_FOUND,
        #endregion

        #region Vision
        FOCUS_VALUE_CONTRAST_LOW,
        AUTO_FOCUS_EXCEED_COARSE_ITERATION,
        AUTO_FOCUS_EXCEED_FINE_ITERATION,
        AUTO_FOCUS_UNRECOVERABLE_ERROR,

        VISION_LEARN_PATTERN_ERROR,
        VISION_MATCH_PATTERN_ERROR,
        VISION_MATCH_IMAGE_REGISTER_ERROR,
        VISION_MATCH_LOW_SCORE_ERROR,
        VISION_MATCH_OFFSET_ERROR,
        VISION_MATCH_ANGLE_ERROR,
        VISION_UNDEFINED_ERROR,
        #endregion

        #region Height Sensor
        CONFOCAL_CONNECT_ERROR,
        CONFOCAL_CONTROL_ERROR,
        CONFOCAL_RANGE_ERROR,
        CONFOCAL_VALUE_ERROR,
        LASERSENSOR_CONNECT_ERROR,
        LASERSENSOR_CONTROL_ERROR,
        LASERSENSOR_RANGE_ERROR,
        LASERSENSOR_VALUE_ERROR,
        #endregion

        #region Recipe
        RECIPE_HEIGHTSET_INVALID_NEXT_CMD,
        RECIPE_INVALID_PREV_CMD,
        RECIPE_INVALID_NEXT_CMD,
        RECIPE_INVALID_PARA,
        #endregion

        #region Lighting Ctrl
        LIGHT_CTRL_CONNECT_ERROR,
        LIGHT_CTRL_READVERSION_ERROR,
        LIGHT_CTRL_SET_MODE_ERROR,
        LIGHT_CTRL_SET_INTENSITY_ERROR,
        #endregion

        #region PressCtrl
        FPRESS_CTRL_PORT_READWRITE_ERROR,
        FPRESS_CTRL_OPEN_ERROR,
        FPRESS_CTRL_OOR_ERROR,
        FPRESS_CTRL_UNKNOWN_CMD_ERROR,
        FPRESS_CTRL_UNKNOWN_RESPONSE_ERROR,

        FPRESS_CTRL_PV_OUT_OF_RANGE,


        REGULATOR_CONNECT_NOT_FOUND,
        REGULATOR_CONNECT_FAIL,
        REGULATOR_DISCONNECT_FAIL,
        REGULATOR_FUNCTION_ERROR,
        #endregion

        TEMPCTRL_OPEN_ERROR,
        TEMPCTRL_IS_NOT_OPEN,
        TEMPCTRL_COMMUNICATION_ERROR,
        COMPORT_COMMUNICATION_ERROR,
        PUMP_TEMP_PV_OUT_OF_RANGE,
        TEMP_AWAITING_TIMEOUT_ERROR,

        #region Pump
        INVALID_PUMP,

        PUMP_VERMESM3280_CONNECT_FAIL,
        PUMP_VERMESM3280_INVALID_RESPONSE,
        PUMP_VERMESM3280_COMMUNICATION_ERROR,

        PURGE_STAGE_FULL,

        #endregion

        #region Weight Scale
        WEIGHT_SCALE_OTHER_ERROR_REINIT,
        WEIGHT_SCALE_CONNECT_FAIL,
        WEIGHT_SCALE_CONVERT_ERROR,
        WEIGHT_SCALE_BUSY,
        WEIGHT_SCALE_INCORRECT_PARA,
        WEIGHT_SCALE_BALANCE_OVERLOAD,
        WEIGHT_SCALE_BALANCE_UNDERLOAD,
        WEIGHT_SCALE_ZERO_EXCEED_LIMIT,
        #endregion

        #region Process
        HEIGHT_ALIGN_OVER_OFFSET,

        DYNAMIC_JET_INVAID_DIRECTION,


        #endregion

        #region Conveyor
        Conv_Not_Init,

        CONV_Remove_Board_On_IN_PSNT,
        CONV_Remove_Board_On_LEFT_PSNT,
        CONV_LEFT_DN_TIMEOUT,
        CONV_LEFT_UP_TIMEOUT,
        CONV_LEFT_STOPPER_DN_TIMEOUT,
        CONV_LEFT_STOPPER_UP_TIMEOUT,
        //CONV_LEFT_MOVE_TIMEOUT,
        CONV_LEFT_VAC_TIMEOUT,

        CONV_LEFT_SMEMA_COMMUNICATION_FAILED,
        CONV_LEFT_SMEMA_LOAD_IN_PSNT_TIMEOUT,
        CONV_LEFT_SMEMA_SEND_OUT_IN_PSNT_TIMEOUT,
        CONV_LEFT_LOAD_PSNT_TIMEOUT,
        CONV_LEFT_RETURN_LEFT_TIMEOUT,

        CONV_RIGHT_SMEMA_LOAD_IN_OUT_PSNT_TIMEOUT,
        CONV_RIGHT_SMEMA_COMMUNICATION_FAILED,

        CONV_Remove_Board_On_OUT_PSNT,
        CONV_Remove_Board_On_RIGHT_PSNT,
        CONV_RIGHT_DN_TIMEOUT,
        CONV_RIGHT_UP_TIMEOUT,
        CONV_RIGHT_STOPPER_DN_TIMEOUT,
        CONV_RIGHT_STOPPER_UP_TIMEOUT,
        CONV_RIGHT_MOVE_TIMEOUT,
        CONV_RIGHT_VAC_TIMEOUT,

        CONV_RIGHT_OUT_PSNT_TIMEOUT,
        CONV_RIGHT_PSNT_TIMEOUT,


        #endregion

        ETHERCAT_SCAN_FAILED,
        SCANAXIS_LOWER_AXISCOUNT,
        SCANAXIS_OVER_AXISCOUNT,
        STACT_ETHERCAT_BUSFAILED,
        EXPANSIOBIO_CONNECT_FAILED,
        CCDCAMERA_OPENFAIL,

        START_EXTERNAL_APP_FAIL,


        WAFER_PRECISOR_FAILTO_OFF,
        WAFER_PRECISOR_FAILTO_ON,
        WAFER_DETECTED,
        WAFER_NO_DETECTED,
        WAFER_NOTCH_ALIGNMENT_FAIL,
        WAFER_LIFTER_HOMING_FAIL,
        WAFER_PRECISOR_HOMING_FAIL,

        #region Safety
        DOOR_LOCK_FAIL,
        JOG_EXCEED_SAFETY_ZONE,
        SIDE_DOOR_PSNT,
        #endregion

        DXF_FILE_ERROR,

        IO_CHECK_FAILED,
    }
}
