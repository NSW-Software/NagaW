using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NagaW
{
    class GEvent
    {
        public static void Start(EEvent @event, string msg = "")
        {
            GLog.WriteLog(ELogType.EVENT, @event.ToString() + " " + msg);
        }
    }
    public enum EEvent : ushort
    {
        NONE = 0,

        PROMPT_ALARM,

        STARTUP_SOFTWARE,
        SHUTDOWN_SOFTWARE,

        OPEN_MOTION_CTRL,
        
        START_EXTERNAL_APP,

        HOME_ALL,

        LOAD_RECIPE,
        SAVE_RECIPE,

        NEEDLE_CFP,
        NEEDLE_CFP_LEARN,
        PURGE_STAGE,
        NEEDLE_AIR_BLADE_CLEAN,

        CALIBRATION_NEEDLE_XY,
        CALIBRATION_ZTOUCH_OFFSET,
        CALIBRATION_ZTOUCH_OFFSET_MANUAL,
        CALIBRATION_LASER_OFFSET,
        CALIBRATION_LASER_OFFSET_MANUAL,
        CALIBRATION_TOUCH_DOT,

        GOTO_MC_MAINT_POS,
        GOTO_PUMP_MAINT_POS,
    }
}
