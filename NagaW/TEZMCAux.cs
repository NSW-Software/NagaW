using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;
using System.Reflection;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.ComponentModel;

namespace NagaW
{
    public class TEZMCAux
    {
        public static bool Enable = true;//master hardware flag; false - bypass hardware interface
        public static bool Online = false;//master online flag; true - hardware is online and interfacing
        public static bool CommError = true;//comm error; true hardware is online and require init

        //public static bool Ready = false;//master ready flag; true - hardware is inited and read to interface

        public static DateTime LastMoveTime = DateTime.Now;

        private static bool pingIpAddress //  Added for checking network Zmotion. 
        {
            get
            {
                Ping ping = new Ping();
                try
                {

                    PingReply pingReply = ping.Send(ipAddress);
                    return pingReply.Status.ToString() == "Success";
                }
                catch
                {
                    return false;
                }
            }
        }

        #region API interface with zauxdll.dll
        /*************************************************************
         * Description:    //与控制器建立链接
         * Input:          //IP地址，字符串的方式输入
         * Output:         //卡链接handle
         * Return:         //错误码
         * *************************************************************/
        //int32 __stdcall ZAux_OpenEth(char *ipaddr, ZMC_HANDLE * phandle);
        [DllImport("zauxdll.dll", EntryPoint = "ZAux_OpenEth", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 ZAux_OpenEth(string ipaddr, out IntPtr phandle);

        /*************************************************************
         * Description:    //封装 Excute 函数, 以便接收错误
         * Input:          //卡链接handle
         * Output:         //
         * Return:         //错误码
         * *************************************************************/
        ////int32 __stdcall ZAux_Execute(ZMC_HANDLE handle, const char* pszCommand, char* psResponse, uint32 uiResponseLength);
        [DllImport("zauxdll.dll", EntryPoint = "ZAux_Execute", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 ZAux_Execute(IntPtr handle, string pszCommand, byte[] psResponse, UInt32 uiResponseLength);

        /*************************************************************
         * Description:    //封装 Excute 函数, 以便接收错误
         * Input:          //卡链接handle
         * Output:         //
         * Return:         //错误码
         * *************************************************************/
        ////int32 __stdcall ZAux_DirectCommand(ZMC_HANDLE handle, const char* pszCommand, char* psResponse, uint32 uiResponseLength);
        [DllImport("zauxdll.dll", EntryPoint = "ZAux_DirectCommand", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 ZAux_DirectCommand(IntPtr handle, string pszCommand, byte[] psResponse, UInt32 uiResponseLength);


        /*************************************************************
         * Description:    //关闭控制器链接
         * Input:          //卡链接handle
         * Output:         //
         * Return:         //错误码
         * *************************************************************/
        //int32 __stdcall ZAux_Close(ZMC_HANDLE  handle);
        [DllImport("zauxdll.dll", EntryPoint = "ZAux_Close", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 ZAux_Close(IntPtr handle);

        /*************************************************************
         * Description:    //读取输入信号
         * Input:          //卡链接handle  
         * Output:         //
         * Return:         //错误码
         * *************************************************************/
        ////int32 __stdcall ZAux_Direct_GetIn(ZMC_HANDLE handle, int ionum , uint32 *piValue);
        [DllImport("zauxdll.dll", EntryPoint = "ZAux_Direct_GetIn", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 ZAux_Direct_GetIn(IntPtr handle, int ionum, ref UInt32 piValue);

        /*************************************************************
        Description:    //打开输出信号
        Input:          //卡链接handle  
        Output:         //
        Return:         //错误码
        *************************************************************/
        ////int32 __stdcall ZAux_Direct_SetOp(ZMC_HANDLE handle, int ionum, uint32 iValue);
        [DllImport("zauxdll.dll", EntryPoint = "ZAux_Direct_SetOp", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 ZAux_Direct_SetOp(IntPtr handle, int ionum, UInt32 iValue);

        /*************************************************************
        Description:    //读取输出口状态
        Input:          //卡链接handle  
        Output:         //
        Return:         //错误码
        *************************************************************/
        ////int32 __stdcall ZAux_Direct_GetOp(ZMC_HANDLE handle, int ionum, uint32 *piValue);
        [DllImport("zauxdll.dll", EntryPoint = "ZAux_Direct_GetOp", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 ZAux_Direct_GetOp(IntPtr handle, int ionum, ref UInt32 piValue);

        /*************************************************************
        Description:    //读取模拟量输入信号
        Input:          //卡链接handle  
        Output:         //
        Return:         //错误码
        *************************************************************/
        ////int32 __stdcall ZAux_Direct_GetAD(ZMC_HANDLE handle, int ionum , float *pfValue);
        [DllImport("zauxdll.dll", EntryPoint = "ZAux_Direct_GetAD", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 ZAux_Direct_GetAD(IntPtr handle, int ionum, ref float pfValue);

        /*************************************************************
        Description:    //打开模拟量输出信号
        Input:          //卡链接handle  
        Output:         //
        Return:         //错误码
        *************************************************************/
        ////int32 __stdcall ZAux_Direct_SetDA(ZMC_HANDLE handle, int ionum, float fValue);
        [DllImport("zauxdll.dll", EntryPoint = "ZAux_Direct_SetDA", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 ZAux_Direct_SetDA(IntPtr handle, int ionum, float fValue);

        /*************************************************************
        Description:    //读取模拟输出口状态
        Input:          //卡链接handle  
        Output:         //
        Return:         //错误码
        *************************************************************/
        ////int32 __stdcall ZAux_Direct_GetDA(ZMC_HANDLE handle, int ionum, float *pfValue);
        [DllImport("zauxdll.dll", EntryPoint = "ZAux_Direct_GetDA", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 ZAux_Direct_GetDA(IntPtr handle, int ionum, ref float pfValue);

        /*************************************************************
        Description:    //设置输入口反转
        Input:          //卡链接handle  
        Output:         //
        Return:         //错误码
        *************************************************************/
        ////int32 __stdcall ZAux_Direct_SetInvertIn(ZMC_HANDLE handle, int ionum, int bifInvert);
        [DllImport("zauxdll.dll", EntryPoint = "ZAux_Direct_SetInvertIn", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 ZAux_Direct_SetInvertIn(IntPtr handle, int ionum, int bifInvert);

        /*************************************************************
        Description:    //读取输入口反转状态
        Input:          //卡链接handle  
        Output:         //
        Return:         //错误码
        *************************************************************/
        ////int32 __stdcall ZAux_Direct_GetInvertIn(ZMC_HANDLE handle, int ionum, int *piValue);
        [DllImport("zauxdll.dll", EntryPoint = "ZAux_Direct_GetInvertIn", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 ZAux_Direct_GetInvertIn(IntPtr handle, int ionum, ref int piValue);

        /*************************************************************
        Description:    //设置pwm频率
        Input:          //卡链接handle  
        Output:         //
        Return:         //错误码
        *************************************************************/
        ////int32 __stdcall ZAux_Direct_SetPwmFreq(ZMC_HANDLE handle, int ionum, float fValue);
        [DllImport("zauxdll.dll", EntryPoint = "ZAux_Direct_SetPwmFreq", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 ZAux_Direct_SetPwmFreq(IntPtr handle, int ionum, float fValue);

        /*************************************************************
        Description:    //读取pwm频率
        Input:          //卡链接handle  
        Output:         //
        Return:         //错误码
        *************************************************************/
        ////int32 __stdcall ZAux_Direct_GetPwmFreq(ZMC_HANDLE handle, int ionum, float *pfValue);
        [DllImport("zauxdll.dll", EntryPoint = "ZAux_Direct_GetPwmFreq", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 ZAux_Direct_GetPwmFreq(IntPtr handle, int ionum, ref float pfValue);

        /*************************************************************
        Description:    //设置pwm占空比
        Input:          //卡链接handle  
        Output:         //
        Return:         //错误码
        *************************************************************/
        ////int32 __stdcall ZAux_Direct_SetPwmDuty(ZMC_HANDLE handle, int ionum, float fValue);
        [DllImport("zauxdll.dll", EntryPoint = "ZAux_Direct_SetPwmDuty", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 ZAux_Direct_SetPwmDuty(IntPtr handle, int ionum, float fValue);

        /*************************************************************
        Description:    //读取pwm占空比
        Input:          //卡链接handle  
        Output:         //
        Return:         //错误码
        *************************************************************/
        ////int32 __stdcall ZAux_Direct_GetPwmDuty(ZMC_HANDLE handle, int ionum, float *pfValue);
        [DllImport("zauxdll.dll", EntryPoint = "ZAux_Direct_GetPwmDuty", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 ZAux_Direct_GetPwmDuty(IntPtr handle, int ionum, ref float pfValue);



        //Error string are copied from manual 2.1.2 0107 and provided excel file
        public static string ErrorString(int errCode)
        {
            switch (errCode)
            {
                case 210: return "File is too large";
                case 212: return "State error.";
                case 213: return "Download file upload errors, packet loss.";
                case 214: return "Download the file length checking errors.";
                case 215: return "Buffer length is not enough.";
                case 219: return "Download the conflict, launched simultaneously download multiple files.";
                case 220: return "Filename error, there are special characters.";
                case 221: return "Filename error, more than the length.";
                case 222: return "File does not exist";
                case 223: return "Password protection limits.";
                case 224: return "Password protection limits 2.";
                case 217: return "Controller functions are not supported or prohibited.";
                case 218: return "Call transfer parameter error.";

                case 260: return "Hardware Error.";
                case 261: return "Disk is not formatted.";
                case 262: return "RTC error.";
                case 263: return "NORFLASH error.";
                case 264: return "RAM error.";
                case 265: return "NANDFLASH error.";
                case 266: return "U disk error.";
                case 267: return "FPGA error.";
                case 268: return "Ethernet hardware error.";
                case 271: return "Backup power supply error.";
                case 272: return "Daughter card does not exist.";
                case 273: return "File is missing.";
                case 274: return "System File Error.";
                case 275: return "Generating no master, daughter card.";
                case 276: return "File checksum error.";
                case 277: return "File errors does not start.";
                case 278: return "ZAR check APPPASS error.";
                case 279: return "ZAR ID check error.";
                case 280: return "BAS file exceeds the maximum number.";
                case 281: return "Conflict daughter card ID, or the main conflict..";
                case 282: return "Unsupported Features.";
                case 284: return "The controller does not match with zar.";
                case 285: return "Image File Error.";
                case 286: return "Font file error.";
                case 288: return "More abnormal, leading to the next boot error..";

                case 1000: return "Moving module returns the offset error.";
                case 1002: return "No moving buffer.";
                case 1004: return "Movement from the shaft.";
                case 1005: return "It does not support motor function.";
                case 1006: return "Arc position error.";
                case 1007: return "Parameter error ellipse AB.";
                case 1008: return "The motion module input parameter error.";
                case 1009: return "Movement, can not operate.";
                case 1010: return "Repeat pause.";
                case 1011: return "IDLE do not pause other operations.";
                case 1012: return "The current movement is not suspended support.";
                case 1013: return "Can not find pause point.";
                case 1014: return "ATYPE not supported.";
                case 1015: return "ZCAN of conflict ATYPE.";
                case 1016: return "Axis unsupported features.";
                case 1017: return "FRAME data error correction.";
                case 1018: return "FRAME correction data is too small.";
                case 1019: return "FRAME data correction data is too small to meet the conditions.";
                case 1020: return "FRAME auxiliary parameter correction data is too small.";
                case 1021: return "FRAME correction data interval is too small, smaller than the number of joint shafts.";
                case 1022: return "FRAME coordinate input error.";
                case 1023: return "FRAME state coordinates the modifications can not be forced.";
                case 1024: return "FRAME abnormal inverse solution.";
                case 1025: return "FRAME state is not.";
                case 1026: return "FRAME HAND error.";
                case 1027: return "Attitude interpolation can not switch.";
                case 1028: return "Special joint axis with the virtual axis as in claim equivalents.";
                case 1030: return "        CORNERMODE 7 bits set up but does not support this motion.";
                case 1031: return "CORNERMODE 7 bits set the state but not the FRAME.";
                case 1032: return "AXIS_ADDRESS error.";
                case 2000: return "Offset module ZBASIC.";
                case 2021: return "Manually stop.";
                case 2022: return "Other tasks due to errors in this task to stop.";
                case 2023: return "Attempt to modify read-only parameter.";
                case 2024: return "Array bounds.";
                case 2025: return "More than the number of variables Controller Specifications.";
                case 2026: return "More than the number of array controller specifications.";
                case 2027: return "Array Controller Specifications space than.";
                case 2028: return "SUB exceeds Controller Specifications.";
                case 2029: return "Identifier naming error.";
                case 2030: return "Identifier name is too long.";
                case 2031: return "No closing parenthesis.";
                case 2032: return "The characters do not know.";
                case 2033: return "Expressions encountered do not know the name.";
                case 2043: return "Command does not know the identifier of the current line an identifying name.";
                case 2044: return "Stack Overflow.";
                case 2045: return "Mathematical expression is too complex, different specifications of the controller is not the same.";
                case 2046: return "I did not find the end of reference numerals.";
                case 2047: return "Command does not return value, not be used for expression evaluation.";
                case 2048: return "Function must return a value, not at the beginning of the local line.";
                case 2049: return "Special instruction must separate line.";
                case 2050: return "Or array parameters need to be indexed.";
                case 2051: return "Variables can not use the index.";
                case 2052: return "Redefinition array length and inconsistent.";
                case 2053: return "Defines the length of the array parameter error, negative or too large.";
                case 2054: return "SUB identifiers have been defined as the process, can not do other purposes.";
                case 2055: return "Identifiers have been defined as parameters, can not do other purposes.";
                case 2056: return "Identifiers reserved and can not be used.";
                case 2057: return "Unrecognizable character appears.";
                case 2058: return "SUB repeat the call stack.";
                case 2060: return "Syntax error.";
                case 2062: return "Function parameters Range Error.";
                case 2063: return "Too many arguments.";
                case 2064: return "Too few arguments.";
                case 2065: return "Missing operand.";
                case 2066: return "Behind the operator missing operand.";
                case 2067: return "Front operator missing operand.";
                case 2068: return "The operator does not know.";
                case 2069: return "Operators need binocular.";
                case 2070: return "CALL must be called SUB.";
                case 2072: return "Require assignment symbol.";
                case 2073: return "Empty file.";
                case 2074: return "SUB-defined identifier name conflicts.";
                case 2075: return "To start the task has been running.";
                case 2076: return "To use multiple parameters separated by commas.";
                case 2077: return "Parentheses are not matching, no left parenthesis.";
                case 2078: return "IF nesting too much judgment.";
                case 2079: return "Loops nested too much.";
                case 2080: return "Interpolating axes too.";
                case 2081: return "CONST constants can not be modified.";
                case 2082: return "Command can not be sent from the PC online.";
                case 2083: return "Too SUB-defined parameters.";
                case 2084: return "SUB parameters, can not be used GOTO statement.";
                case 2085: return "Local identifier defined too.";
                case 2086: return "LOCAL variable names and variable names or other identifiers file name conflicts.";
                case 2087: return "LOCAL does not support the array definition.";
                case 2088: return "GSUB defined parameters letters repeated.";
                case 2089: return "GSUB defined parameters can only be a single letter.";
                case 2090: return "You can not modify read-only parameter.";
                case 2091: return "GSUB_IFPARA function uses the wrong occasion.";
                case 2092: return "Division by zero.";
                case 2093: return "Over buffer.";
                case 2094: return "Command-line blocking game too long.";
                case 2095: return "Parameter same name.";
                case 2096: return "Values ​​are not initialized on the use of.";
                case 2097: return "Axis number conflict.";
                case 2099: return "internal error.";
                case 2100: return "Excessive number of SCANEDGE.";
                case 2101: return "ZINDEX type mismatch.";
                case 2901: return "System errors, defined identifiers too. Including variables, arrays, process, process parameters and so on.";
                case 3201: return "Over buffer.";
                case 3202: return "Unexpected end of file.";
                case 3204: return "Internal state error.";
                case 3205: return "Unsupported Features.";
                case 3206: return "Internal call parameters error.";
                case 3301: return "Three points in a circular arc line.";
                case 3302: return "Two parallel straight lines, there is no intersection.";
                case 3401: return "MODBUS master end parameter error, is generally longer than.";
                case 3402: return "Message Response Timeout.";
                case 3407: return "Modbus return parameter error.";
                case 3408: return "Modbus does not support the return.";
                case 3421: return "mobus return function code is not supported from the end.";
                case 3422: return "mobus return address space from side error.";
                case 3423: return "modbus not return data length from the end.";
                case 3424: return "modbus returned too long from an end.";
                case 3501: return "ZCAN return without children card.";
                case 3502: return "ZCAN daughter card returns the corresponding axis without.";
                case 4000: return "Error 4000-4500 PLC module.";
                case 4002: return "Parameter error.";
                case 4003: return "Unknown type.";
                case 4004: return "Unknown function.";
                case 4005: return "Push too STL.";
                case 4006: return "Push too much.";
                case 4007: return "The program is too complex, BLOCK much.";
                case 4008: return "No push BLOCK.";
                case 4009: return "No push STL.";
                case 4010: return "Do not push.";
                case 4014: return "File content errors.";
                case 4015: return "RET must be back in the STL.";
                case 4016: return "Of range.";
                case 4017: return "Below the range.";
                case 4018: return "L is not defined.";
                case 4019: return "G code does not support the function.";
                case 4020: return "GOTO can not cross PLC and BASIC.";
                case 4021: return "PLC is only one main task.";
                case 4022: return "Grammatical errors.";
                case 4023: return "FOR NEXT errors, mismatched.";
                case 4024: return "FOR NEXT error, no NEXT.";
                case 4026: return "FOR MC mix.";
                case 4027: return "FOR STL mix.";
                case 4030: return "PLC must use the main task.";
                case 4031: return "Used must be interrupted.";
                case 4032: return "Small number of parameters.";
                case 4033: return "The number of multi-parameter.";
                case 4034: return "To be a multiple of 8.";
                case 4035: return "The register indicates an error.";
                case 4036: return "Register type error.";
                case 4037: return "More than the number of LV.";
                case 4038: return "Read-only.";
                case 4500: return "4500-5000 PLC PC side error.";
                case 4503: return "Not enough memory.";
                case 4504: return "We returned to the bus.";
                case 4505: return "Reflux.";
                case 4506: return "AND type can not be directly connected to the bus.";
                case 4510: return "Suspended.";
                case 4511: return "The far right must be the type of output.";
                case 5000: return "Error 5000-5500 HMI module.";
                case 5001: return "Error No. LCD.";
                case 5002: return "LCD Number Conflicts.";
                case 5003: return "Does not support object.";
                case 5004: return "Not enough memory.";
                case 5005: return "Error Control Hierarchy.";
                case 5006: return "No more than window.";
                case 5007: return "Invalid window number.";
                case 5010: return "Object attribute is missing.";
                case 5011: return "A plurality of input window display elements.";
                case 5012: return "ACTION type error.";
                case 5013: return "Too Many Event.";
                case 5014: return "Return to the previous window failure.";
                case 5015: return "Basic window can not close.";
                case 5016: return "Font is not found in the corresponding character.";
                case 5017: return "You must be used in HMI tasks.";
                case 5020: return "Control ID Conflict.";
                case 5021: return "Error No. LCD.";
                case 5022: return "Can not find available LCD.";
                case 5023: return "LCD does not open.";
                case 5024: return "LCD No data.";
                case 5025: return "Program reset.";
                case 5026: return "lcd has been opened.";
                case 5027: return "Not a network LCD.";
                case 5028: return "It does not support compression.";
                case 5029: return "Color depth is not supported.";
                case 5030: return "Unsupported Data Types.";
                case 5031: return "Device number error.";
                case 5032: return "LCD_SEL can not be used.";
                case 5033: return "REDRAW can no longer set the stage DRAW.";
                case 5034: return "DRAW DRAW function only in phase.";
                case 5035: return "Operation can no longer call the DRAW stage.";
                case 5036: return "Internal LCD resolution is fixed.";
                case 5037: return "LCD resolution exceeds.";
                case 5038: return "Library filename error.";
                case 5039: return "Too many characters.";
                case 5501: return "PC - side PLC file compilation error.";
                case 5503: return "Not enough memory.";
                case 5504: return "We returned to the bus.";
                case 5505: return "Reflux.";
                case 5506: return "AND type instruction can not be directly connected to the bus.";
                case 5510: return "Floating the right, then there is no output instruction.";
                case 5511: return "The far right type of instruction is not output.";
                case 5512: return "The far right can not be connected together.";
                case 5513: return "Output type of instruction must be the rightmost.";
                case 5514: return "It does not support the type of instruction.";
                case 5517: return "No value register.";
                case 5518: return "DOT value exceeds the range.";
                case 5519: return "Index register exceeds the range.";
                case 5520: return "Excessive number of characters.";
                case 5521: return "Register type error.";
                case 5522: return "Register value error.";
                case 5523: return "Excessive number of registers.";
                case 5524: return "Number of registers too little.";
                case 5525: return "Using the wrong STL.";
                case 5526: return "Using the wrong RET.";
                case 5527: return "Repeat RET.";
                case 5528: return "END LBL position error or.";
                case 5529: return "Function can not be directly connected to the bus.";
                case 5530: return "Do not push the stack.";
                case 5531: return "Too many MPP.";
                case 5532: return "Using the wrong type register.";
                case 5533: return "ANB error, insufficient number of blocks.";
                case 5534: return "ORB error, insufficient number of blocks.";
                case 5535: return "ANB error, the output operation can not be merged.";
                case 5536: return "ORB error, the output operation can not be merged.";
                case 5537: return "AND direct bus connection.";
                case 5538: return "OR directly connected to the bus.";
                case 5539: return "OR OUT instruction is not behind.";
                case 5540: return "STL can not be shared and MC.";
                case 5541: return "MC can not directly access the bus.";
                case 5542: return "_ @ To register brackets.";
                case 5543: return "Notes error.";
                case 5544: return "Ladder excessive number of columns.";
                case 5545: return "Output bus type can not be directly connected.";
                case 6000: return "ECAT module error, slot number error.";
                case 6001: return "Internal error, function is not supported.";
                case 6005: return "Parameter error.";
                case 6006: return "The number of device types supported over the limit.";
                case 6012: return "Insufficient resources.";
                case 6013: return "Slave response timeout.";
                case 6014: return "Buffer is not enough.";
                case 6015: return "Response packet error wkc.";
                case 6016: return "Long answer SDO.";
                case 6017: return "SDO response error.";
                case 6018: return "SDO response data length error.";
                case 6019: return "WKC timeout.";
                case 6020: return "state switch timeout.";
                case 6021: return "SDO ABORT.";
                case 6023: return "NODE profile error.";
                case 6024: return "Axis profile error.";
                case 6025: return "More than the number of axes.";
                case 6029: return "PDO list exceeds the number of.";
                case 6031: return "More than the number of devices.";
                case 6042: return "Device does not support.";
                case 6045: return "E - mail timeout.";
                case 6047: return "Data type error.";
                case 6049: return "Module does not support the sub - module.";
                case 6050: return "Sub - module exceeds the number of modules.";
                case 6051: return "The module does not recognize sub - module.";
                case 6208: return "RTEX drive ID conflict.";
                case 6209: return "Oversweep general cable contacting problems.";
                case 6210: return "RTEX failed to initialize.";
                case 6211: return "RTEX error scan results.";
                case 20000: return "PC side offset error.";
                case 20002: return "Parameter wrong.";
                case 20003: return "time out.";
                case 20006: return "Operating system error.";
                case 20007: return "Serial port open failed.";
                case 20008: return "Network open failed.";
                case 20009: return "Handle Error.";
                case 20010: return "Send Error.";
                case 20011: return "File Error.";
                case 20012: return "File length error.";
                case 20013: return "File name is too long.";
                case 20014: return "file does not exist.";
                case 20015: return "ZLB library file errors.";
                case 20016: return "File does not compile.";
                case 20020: return "Firmware file does not match.";
                case 20021: return "Unsupported Features.";
                case 20030: return "Input buffer length is not enough.";
                case 20100: return "Answer buffer length is not enough.";
                case 30000: return "These are the wrong code ZAUX auxiliary library generated.";
                default: return "Unknown Errorcode.";
            }
        }
        #endregion

        static Mutex mutex = new Mutex();

        internal static IntPtr g_handle = IntPtr.Zero;
        internal static string ipAddress = "192.168.0.11";

        //public static int LogDownCount = 0;//log if counter-- is > 0;
        internal static void WriteLog(string content)
        {
            if (GSystemCfg.MakerData.ZMotion_LogCountDown == 0) return;

            GLog.WriteLog(content, GDoc.MotionLogFile.FullName);
            if (GSystemCfg.MakerData.ZMotion_LogCountDown > 0) GSystemCfg.MakerData.ZMotion_LogCountDown--;
        }


        static byte[] psResponse = new byte[1024];
        static UInt32 uiResponseLength = 1024;
        private static void execute(string cmd, ref string stringResult, bool silent = false, bool forceLog = false)//Execute Task
        {
            if (!CheckSideDoor()) return;

            CheckMoveFlag(cmd);

            if (!Enable || !Online) return;

            int errCode = 0;
            try
            {
                if (g_handle == IntPtr.Zero) throw new Exception("Invalid Handle.");

                errCode = ZAux_Execute(g_handle, cmd, psResponse, uiResponseLength);

                int pos = 0;
                foreach (var b in psResponse)
                {
                    if (b == 10) break;
                    pos++;
                }

                stringResult = System.Text.Encoding.ASCII.GetString(psResponse, 0, pos).Trim();
                if (errCode != 0) throw new Exception(ErrorString(errCode));
            }
            catch
            {
                Online = pingIpAddress;
                if (!Online)
                {
                    GAlarm.Prompt(EAlarm.MOTION_CTRL_OFFLINE_ERROR);
                    if (!silent) throw;
                }
                GAlarm.Prompt(EAlarm.MOTION_CTRL_COMM_ERROR, $"cmd ErrCode {errCode} {ErrorString(errCode)}");
                if (!silent) throw;
            }
            finally
            {
                if (forceLog || !cmd.StartsWith("?"))
                    WriteLog($"ZAux_Execute\t{errCode}\t{cmd}\tResult\t{stringResult}");
            }
        }
        private static void directCommand(string cmd, ref string stringResult, bool silent = false, bool forceLog = false)//Direct Command
        {
            if (!CheckSideDoor()) return;

            CheckMoveFlag(cmd);

            if (!Enable || !Online) return;

            int errCode = 0;
            try
            {
                if (g_handle == IntPtr.Zero) throw new Exception("Invalid Handle.");

                errCode = ZAux_DirectCommand(g_handle, cmd, psResponse, uiResponseLength);

                int pos = 0;
                foreach (var b in psResponse)
                {
                    if (b == 32) break;
                    pos++;
                }

                stringResult = System.Text.Encoding.ASCII.GetString(psResponse, 0, pos);

                if (errCode != 0) throw new Exception(ErrorString(errCode));
            }
            catch
            {
                Online = pingIpAddress;
                if (!Online)
                {
                    GAlarm.Prompt(EAlarm.MOTION_CTRL_OFFLINE_ERROR);
                    if (!silent) throw;
                }
                GAlarm.Prompt(EAlarm.MOTION_CTRL_COMM_ERROR, $"cmd ErrCode {errCode}");
                if (!silent) throw;
            }
            finally
            {
                if (forceLog || !cmd.StartsWith("?"))
                    WriteLog($"ZAux_Execute\t{errCode}\t{cmd}\tResult\t{stringResult}");
            }
        }

        public static bool Open()
        {
            if (!Enable) return true;

            try
            {
                Online = pingIpAddress;

                WriteLog($"ZAux_OpenEth\t{0}\t{ipAddress}");
                int errCode = ZAux_OpenEth(ipAddress, out g_handle);
                if (errCode != 0)
                {
                    Online = false;
                    throw new Exception(ErrorString(errCode));
                }
                if (g_handle != IntPtr.Zero)
                {
                    Online = true;
                    CommError = false;

                    //byte[] buffer = new byte[1024];

                    string ctype = "";// System.Text.Encoding.ASCII.GetString(buffer);
                    execute("?CONTROL", ref ctype, true, true);

                    string cid = "";//System.Text.Encoding.ASCII.GetString(buffer);
                    execute("?SERIAL_NUMBER", ref cid, true, true);

                    string version = "";//System.Text.Encoding.ASCII.GetString(buffer);
                    execute("?VERSION", ref version, true, true);

                    string fwVersion = "";//System.Text.Encoding.ASCII.GetString(buffer);
                    execute("?FwVersion", ref fwVersion, true, true);

                    string fwVersionDate = "";//System.Text.Encoding.ASCII.GetString(buffer);
                    execute("?FwVersionDate", ref fwVersionDate, true, true);


                    return true;
                }
                else
                    return false;
            }
            catch
            {
                GAlarm.Prompt(EAlarm.MOTION_CTRL_OPEN_NETWORK_FAIL);
                return false;
            }
            finally
            { }
        }
        public static bool Opened
        {
            get
            {
                return (g_handle != IntPtr.Zero);
            }
        }
        public static void Close()
        {
            if (!Enable) return;

            try
            {
                Online = false;
                WriteLog($"ZAux_Close");
                ZAux_Close(g_handle);
                g_handle = IntPtr.Zero;
            }
            catch { }
        }

        public static bool Ready(bool silent = false)
        {
            if (!TEZMCAux.Enable) return true;

            if (!TEZMCAux.Online)
            {
                GAlarm.Prompt(EAlarm.MOTION_CTRL_OFFLINE_ERROR);
                return false;
            }

            if (TEZMCAux.CommError)
            {
                GAlarm.Prompt(EAlarm.MOTION_CTRL_COMM_ERROR);
                return false;
            }

            return true;
        }

        public static void Execute(string cmd, bool silent = false)
        {//silent used to ignore errors, eg. stopping motion 
            mutex.WaitOne();
            try
            {
                string s = "";
                execute(cmd, ref s, silent);
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }
        public static bool DirectCommand(string cmd)
        {
            mutex.WaitOne();
            try
            {
                string s = "";
                directCommand(cmd, ref s);
                return true;
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }

        public static bool CheckMoveFlag(string cmd) 
        {
            cmd = cmd.ToUpper();

            switch (cmd)
            {
                case string move when cmd.Contains("MOVE"):
                case string forward when cmd.Contains("FORWARD"):
                case string reverse when cmd.Contains("REVERSE"):
                    {
                        LastMoveTime = DateTime.Now;
                        return true;
                    }
            }
            return false;
        }
        public static bool Send(string cmd, int para)
        {
            mutex.WaitOne();
            try
            {
                string s = "";
                directCommand(cmd + $"={para}", ref s);
                return true;
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }
        public static bool Send(string cmd, double para)
        {
            mutex.WaitOne();
            try
            {
                string s = "";
                directCommand(cmd + $"={para}", ref s);
                return true;
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }

        internal static int QueryInt(string cmd)
        {
            mutex.WaitOne();
            try
            {
                string str = "";
                directCommand("?" + cmd, ref str);
                int.TryParse(str, out int res);

                return res;
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }
        public static double QueryDouble(string cmd)
        {
            mutex.WaitOne();
            try
            {
                string str = "";
                directCommand("?" + cmd, ref str);
                double.TryParse(str, out double res);

                return res;
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }

        public static int MotionError//Motion Error Status, Bitwise Axis 0-16
        {
            get
            {
                return QueryInt("MOTION_ERROR");
            }
        }

        public static int RemainBuffer(int axisnumber)
        {
            return QueryInt($"REMAIN_BUFFER(31)Axis({axisnumber})");
        }
        public static int Table(int tableNo)
        {
            return (int)QueryDouble($"TABLE({tableNo})");
        }

        public static bool CheckSideDoor()
        {
            if (GSystemCfg.Safety.SideDoorCheck)
            {
                var input = GMotDef.Inputs[(int)GSystemCfg.Safety.SideDoorSens];
                if (input.Status)
                {
                    GAlarm.Prompt(EAlarm.SIDE_DOOR_PSNT, "Side Door Sensor Triggered. Please Check");
                    return false;
                }
            }
            return true;
        }

        public class TAxis
        {
            public int AxisNo = 0;
            public string Name = "";

            public TAxis(int axisNo, string name)
            {
                this.AxisNo = axisNo;
                this.Name = name;
            }
            public bool SvOn
            {
                get
                {
                    int res = 0;
                    try
                    {
                        res = TEZMCAux.QueryInt($"AXIS_ENABLE({AxisNo})");
                    }
                    catch (Exception ex)
                    {
                        GDefine.SystemState = ESystemState.ErrorInit;
                        GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                    }
                    return (res > 0);
                }
                set
                {
                    try
                    {
                        TEZMCAux.Send($"AXIS_ENABLE({AxisNo})", value ? 1 : 0);
                    }
                    catch (Exception ex)
                    {
                        GDefine.SystemState = ESystemState.ErrorInit;
                        GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                    }
                }
            }
            public bool Alarm
            {
                get
                {
                    int res = 0;
                    try
                    {
                        res = TEZMCAux.QueryInt($"AXISSTATUS({AxisNo})");
                    }
                    catch (Exception ex)
                    {
                        GDefine.SystemState = ESystemState.ErrorInit;
                        GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                    }
                    return (res > 0);
                }
            }
            public int AlarmCode
            {
                get
                {
                    int res = 0;
                    try
                    {
                        res = TEZMCAux.QueryInt($"AXISSTATUS({AxisNo})");
                    }
                    catch (Exception ex)
                    {
                        GDefine.SystemState = ESystemState.ErrorInit;
                        GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                    }
                    return res;
                }
            }
            public bool AlmClr
            {
                /* DRIVE_CLEAR(para)
                 * 0: Clear present alert
                 * 1: Clear history
                 * 2: Clear external input alert
                 */
                set
                {
                    try
                    {
                        TEZMCAux.Execute($"DRIVE_CLEAR(0) AXIS({AxisNo})");
                        TEZMCAux.Execute($"DATUM(0) AXIS({AxisNo})");
                    }
                    catch (Exception ex)
                    {
                        GDefine.SystemState = ESystemState.ErrorInit;
                        GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                    }
                }
            }
            public double ActualPos
            {
                get
                {
                    double mPos = 0;
                    try
                    {
                        mPos = TEZMCAux.QueryDouble($"MPOS({AxisNo})");
                    }
                    catch (Exception ex)
                    {
                        GDefine.SystemState = ESystemState.ErrorInit;
                        GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                    }
                    return mPos;
                }
                set
                {
                    try
                    {
                        TEZMCAux.Send($"MPOS({AxisNo})", value);
                    }
                    catch (Exception ex)
                    {
                        GDefine.SystemState = ESystemState.ErrorInit;
                        GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                    }
                }
            }
            public double CmdPos
            {
                get
                {
                    double dPos = 0;
                    try
                    {
                        dPos = TEZMCAux.QueryDouble($"DPOS({AxisNo})");
                    }
                    catch (Exception ex)
                    {
                        GDefine.SystemState = ESystemState.ErrorInit;
                        GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                    }
                    return dPos;
                }
                set
                {
                    try
                    {
                        TEZMCAux.Send($"DPOS({AxisNo})", value);
                    }
                    catch (Exception ex)
                    {
                        GDefine.SystemState = ESystemState.ErrorInit;
                        GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                    }
                }
            }
            public bool HLmtP
            {
                get
                {
                    int add;
                    int res = 0;
                    try
                    {
                        add = TEZMCAux.QueryInt($"FWD_IN({AxisNo})");
                        if (add < 0) return false;
                        res = TEZMCAux.QueryInt($"IN({add})");
                    }
                    catch (Exception ex)
                    {
                        GDefine.SystemState = ESystemState.ErrorInit;
                        GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                    }
                    return (res > 0);
                }
            }
            public bool HLmtN
            {
                get
                {
                    int add;
                    int res = 0;
                    try
                    {
                        add = TEZMCAux.QueryInt($"REV_IN({AxisNo})");
                        if (add < 0) return false;
                        res = TEZMCAux.QueryInt($"IN({add})");
                    }
                    catch (Exception ex)
                    {
                        GDefine.SystemState = ESystemState.ErrorInit;
                        GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                    }
                    return (res > 0);
                }
            }

            public bool Ready
            {
                get
                {
                    try
                    {
                        if (QueryInt($"AXISSTATUS({AxisNo})") > 0) return false;
                    }
                    catch (Exception ex)
                    {
                        GDefine.SystemState = ESystemState.ErrorInit;
                        GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                        return false;
                    }
                    return true;
                }
            }
            public bool Enabled
            {
                get
                {
                    try
                    {
                        if (QueryInt($"AXIS_ENABLE({AxisNo})") == 0) return false;
                    }
                    catch (Exception ex)
                    {
                        GDefine.SystemState = ESystemState.ErrorInit;
                        GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                        return false;
                    }
                    return true;
                }
            }

            public void SetParam(double velLow, double velHigh, double accel, double decel = 0, double jerk = 0)
            {
                try
                {
                    TEZMCAux.Send($"LSPEED AXIS({AxisNo})", velLow);
                    TEZMCAux.Send($"SPEED AXIS({AxisNo})", velHigh);
                    TEZMCAux.Send($"ACCEL AXIS({AxisNo})", accel);
                    if (decel == 0) decel = accel;
                    TEZMCAux.Send($"DECEL AXIS({AxisNo})", decel);
                }
                catch (Exception ex)
                {
                    GDefine.SystemState = ESystemState.ErrorInit;
                    GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                }
            }
            public void SpeedProfile(double[] speedProfile)
            {
                SetParam(speedProfile[0], speedProfile[1], speedProfile[2], speedProfile[3]);
            }
            public double SetSpeed
            {
                set
                {
                    try
                    {
                        TEZMCAux.Send($"SPEED AXIS({AxisNo})", value);
                    }
                    catch (Exception ex)
                    {
                        GDefine.SystemState = ESystemState.ErrorInit;
                        GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                    }
                }
            }

            public double JogSpeed
            {
                set
                {
                    try
                    {
                        TEZMCAux.Send($"JOGSPEED AXIS({AxisNo})", value);
                    }
                    catch (Exception ex)
                    {
                        GDefine.SystemState = ESystemState.ErrorInit;
                        GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                    }
                }
            }
            public bool JogAxisP
            {
                set
                {
                    try
                    {
                        if (value)
                        {
                            TEZMCAux.Execute($"FORWARD AXIS({AxisNo})");
                        }
                        else
                        {
                            TEZMCAux.Execute($"CANCEL(2) AXIS({AxisNo})");
                        }
                    }
                    catch (Exception ex)
                    {
                        GDefine.SystemState = ESystemState.ErrorInit;
                        GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                    }
                }
            }
            public bool JogAxisN
            {
                set
                {
                    try
                    {
                        if (value)
                        {
                            TEZMCAux.Execute($"REVERSE AXIS({AxisNo})");
                        }
                        else
                        {
                            TEZMCAux.Execute($"CANCEL(2) AXIS({AxisNo})");
                        }
                    }
                    catch (Exception ex)
                    {
                        GDefine.SystemState = ESystemState.ErrorInit;
                        GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                    }
                }
            }

            public bool Stop()
            {
                try
                {
                    TEZMCAux.Execute($"BASE({AxisNo}) CANCEL(2)");
                    return true;
                }
                catch (Exception ex)
                {
                    GDefine.SystemState = ESystemState.ErrorInit;
                    GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                    return false;
                }
            }
            public bool StopEmg()
            {
                try
                {
                    TEZMCAux.Execute($"BASE({AxisNo}) CANCEL(3)");
                    return true;
                }
                catch (Exception ex)
                {
                    GDefine.SystemState = ESystemState.ErrorInit;
                    GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                    return false;
                }
            }
            public bool Busy
            {
                get
                {
                    if (!Enable || !Online) return false;

                    int res = -1;//0: in motion, -1 motion ends
                    try
                    {
                        res = TEZMCAux.QueryInt($"IDLE({AxisNo})");
                    }
                    catch (Exception ex)
                    {
                        GDefine.SystemState = ESystemState.ErrorInit;
                        GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                    }
                    return res == 0;
                }
            }
            public bool Wait()
            {
                try
                {
                    while (Busy) { Thread.Sleep(0); }
                    return true;
                }
                catch (Exception ex)
                {
                    GDefine.SystemState = ESystemState.ErrorInit;
                    GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                    return false;
                }
            }

            public bool MoveRel(double[] speedProfile, double value, bool wait)
            {
                try
                {
                    if (speedProfile != null) SetParam(speedProfile[0], speedProfile[1], speedProfile[2], speedProfile[3], speedProfile[4]);
                    TEZMCAux.Execute($"BASE({AxisNo}) MOVE({value:f6})");
                    if (wait) Wait();
                    return true;
                }
                catch (Exception ex)
                {
                    GDefine.SystemState = ESystemState.ErrorInit;
                    GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                    return false;
                }
            }
            public bool MoveRel(double value, bool wait = true)
            {
                return MoveRel(null, value, wait);
            }
            public bool MoveAbs(double[] speedProfile, double value, bool wait)
            {
                try
                {
                    if (speedProfile != null) SetParam(speedProfile[0], speedProfile[1], speedProfile[2], speedProfile[3], speedProfile[4]);
                    TEZMCAux.Execute($"BASE({AxisNo}) MOVEABS({value:f6})");
                    if (wait) Wait();
                    return true;
                }
                catch (Exception ex)
                {
                    GDefine.SystemState = ESystemState.ErrorInit;
                    GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                    return false;
                }
            }
            public bool MoveAbs(double value, bool wait = true)
            {
                return MoveAbs(null, value, wait);
            }

            public bool WaitPosChange(int stopAxisNo, ref bool changed, ref double encoderPos)
            {
                try
                {
                    double mPos = TEZMCAux.QueryDouble($"MPOS({AxisNo})");
                    double lowPos = mPos - 0.002;
                    double upPos = mPos + 0.002;
                    TEZMCAux.Execute($"BASE({AxisNo}) WAIT UNTIL " +
                    //$"IDLE({stopAxisNo}) OR " +
                    //$"MPOS > {upPos} OR MPOS < {lowPos}");
                    $"MPOS <> {upPos}");
                    TEZMCAux.Execute($"BASE({stopAxisNo}) CANCEL(3)");
                    if (!Wait()) return false;

                    encoderPos = TEZMCAux.QueryDouble($"MPOS({AxisNo})");
                    changed = encoderPos > mPos + 0.001 || encoderPos < mPos - 0.001;

                    return true;
                }
                catch (Exception ex)
                {
                    GDefine.SystemState = ESystemState.ErrorInit;
                    GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                    return false;
                }
            }

            public int MoveBuffered
            {
                get
                {
                    int res = 0;
                    try
                    {
                        res = TEZMCAux.QueryInt($"MOVES_BUFFERED({AxisNo})");
                    }
                    catch (Exception ex)
                    {
                        GDefine.SystemState = ESystemState.ErrorInit;
                        GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                    }
                    return res;
                }
            }
            public double RemainDist
            {
                get
                {
                    double res = 0;
                    try
                    {
                        res = TEZMCAux.QueryDouble($"REMAIN({AxisNo})");
                    }
                    catch (Exception ex)
                    {
                        GDefine.SystemState = ESystemState.ErrorInit;
                        GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                    }
                    return res;
                }
            }

            public int MType//Return current Move Type
            {
                get
                {
                    int res = 0;
                    try
                    {
                        res = TEZMCAux.QueryInt($"MTYPE({AxisNo})");
                    }
                    catch (Exception ex)
                    {
                        GDefine.SystemState = ESystemState.ErrorInit;
                        GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                    }
                    return res;
                }
            }
            public int NType//Return current Next Move Type in Buffer
            {
                get
                {
                    int res = 0;
                    try
                    {
                        res = TEZMCAux.QueryInt($"NTYPE({AxisNo})");
                    }
                    catch (Exception ex)
                    {
                        GDefine.SystemState = ESystemState.ErrorInit;
                        GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                    }
                    return res;
                }
            }
        }

        public class TInput
        {
            public int InputNo { get; set; } = 0;
            public string Name { get; set; } = "";
            public EDI_Label Label { get; set; }
            public TInput(int inputNo, string name)
            {
                this.InputNo = inputNo;
                this.Name = name;
            }
            [ReadOnly(true)]
            public bool Status
            {
                get
                {
                    int res = 0;
                    try
                    {
                        res = TEZMCAux.QueryInt($"IN({InputNo})");
                    }
                    catch (Exception ex)
                    {
                        GDefine.SystemState = ESystemState.ErrorInit;
                        GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                    }
                    return (res > 0);
                }
            }
            public override string ToString()
            {
                return $"DI {InputNo:d2} [{Name}]";
            }
        }
        public class TOutput
        {
            public int OutputNo { get; set; } = 0;
            public string Name { get; set; } = "";
            public EDO_Label Label { get; set; }
            public TOutput(int outputNo, string name)
            {
                this.OutputNo = outputNo;
                this.Name = name;
            }
            [ReadOnly(true)]
            internal bool Status
            {
                get
                {
                    int res = 0;
                    try
                    {
                        res = TEZMCAux.QueryInt($"OP({OutputNo})");
                    }
                    catch (Exception ex)
                    {
                        GDefine.SystemState = ESystemState.ErrorInit;
                        GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                    }
                    return (res > 0);
                }
                set
                {
                    try
                    {
                        TEZMCAux.Execute("OP(" + $"{OutputNo}" + "," + (value ? "1" : "0") + ")");
                    }
                    catch (Exception ex)
                    {
                        GDefine.SystemState = ESystemState.ErrorInit;
                        GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                    }
                }
            }
            public override string ToString()
            {
                return $"DO {OutputNo:d2} [{Name}]";
            }
        }

        public class TGroup
        {
            public TAxis[] Axis;
            public string Name = "";
            public int Index;
            public TGroup(TAxis[] axis, string name, int index = 0)
            {
                //AxisNo = (int[])axisNo.Clone();
                Axis = (TAxis[])axis.Clone();
                Name = name;
                Index = index;
            }
            public TAxis XAxis { get => Axis[0]; }
            public TAxis YAxis { get => Axis[1]; }
            public TAxis ZAxis { get => Axis[2]; }

            public void SetParam(double velLow, double velHigh, double accel, double decel = 0, double jerk = 0)
            {
                mutex.WaitOne();
                try
                {
                    TEZMCAux.Send($"BASE({Axis[0].AxisNo},{Axis[1].AxisNo},{Axis[2].AxisNo}) SPEED", velHigh);
                    TEZMCAux.Send($"ACCEL", accel);
                    if (decel == 0) decel = accel;
                    TEZMCAux.Send($"DECEL", decel);
                }
                catch (Exception ex)
                {
                    GDefine.SystemState = ESystemState.ErrorInit;
                    GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
            public void SpeedProfile(double[] speedProfile)
            {
                SetParam(speedProfile[0], speedProfile[1], speedProfile[2], speedProfile[3], speedProfile[4]);
            }

            public int ErrorMask { get; private set; } = 0x00;
            public bool Error
            {
                get
                {
                    mutex.WaitOne();
                    ErrorMask = 0x00;
                    try
                    {
                        int i = QueryInt("MOTION_ERROR");
                        ErrorMask = i >> Axis[0].AxisNo;
                        return (ErrorMask & 0x07) > 0;
                    }
                    catch (Exception ex)
                    {
                        GDefine.SystemState = ESystemState.ErrorInit;
                        GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                        return false;
                    }
                    finally
                    {
                        mutex.ReleaseMutex();
                    }
                }
            }
            public bool Ready
            {
                get
                {
                    mutex.WaitOne();
                    try
                    {
                        foreach (var x in Axis)
                        {
                            if (QueryInt($"AXISSTATUS({x.AxisNo})") > 0) return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        GDefine.SystemState = ESystemState.ErrorInit;
                        GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                        return false;
                    }
                    finally
                    {
                        mutex.ReleaseMutex();
                    }
                    return true;
                }
            }

            public bool Enabled
            {
                get
                {
                    mutex.WaitOne();
                    try
                    {
                        foreach (var x in Axis)
                        {
                            if (!x.Enabled) return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        GDefine.SystemState = ESystemState.ErrorInit;
                        GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                        return false;
                    }
                    finally
                    {
                        mutex.ReleaseMutex();
                    }
                    return true;
                }
            }

            public void MoveAbs(PointXYZ pos)
            {
                mutex.WaitOne();
                try
                {
                    //=> fancy code TT TEZMCAux.Execute($"BASE({AxisNo[0]},{AxisNo[1]},{AxisNo[2]})");
                    //string axisStr = (AxisNo == null) ? null : AxisNo.Skip(1).Aggregate(AxisNo[0].ToString(), (s, i) => s + "," + i.ToString());
                    Execute($"BASE({Axis[0].AxisNo},{Axis[1].AxisNo},{Axis[2].AxisNo}) MOVEABS({pos.X:f6},{pos.Y:f6},{pos.Z:f6})");
                }
                catch (Exception ex)
                {
                    GDefine.SystemState = ESystemState.ErrorInit;
                    GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
            public bool Busy
            {
                get
                {
                    if (Axis[0].Busy) return true;
                    if (Axis[1].Busy) return true;
                    if (Axis[2].Busy) return true;
                    return false;
                }
            }
            public void StopDecel()
            {
                try
                {
                    TEZMCAux.Execute($"BASE({Axis[0].AxisNo},{Axis[1].AxisNo},{Axis[2].AxisNo}) RAPIDSTOP(0)");
                }
                catch (Exception ex)
                {
                    GDefine.SystemState = ESystemState.ErrorInit;
                    GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                }
            }

            public bool MoveXYAbs(double[] speedProfile, double[] pos, bool wait)
            {
                try
                {
                    if (!Ready) return false;
                    SpeedProfile(speedProfile);
                    TEZMCAux.Execute($"BASE({Axis[0].AxisNo},{Axis[1].AxisNo}) MOVEABS({pos[0]:f6},{pos[1]:f6})");
                    if (wait) while (Busy) Thread.Sleep(0);
                    if (!Ready) return false;
                }
                catch (Exception ex)
                {
                    GDefine.SystemState = ESystemState.ErrorInit;
                    GAlarm.Prompt(EAlarm.AXIS_UNRECOVERABLE_ERROR_RESTART, ex);
                    return false;
                }
                return true;
            }
            public bool MoveOpXYAbs(double[] absXY, bool wait)
            {
                return MoveXYAbs(GProcessPara.Operation.GXYSpeed, absXY, wait);
            }
            public bool MoveOpXYAbs(double[] absXY)
            {
                return MoveXYAbs(GProcessPara.Operation.GXYSpeed, absXY, true);
            }

            public bool MoveXYRel(double[] speedProfile, double[] pos, bool wait)
            {
                try
                {
                    if (!Ready) return false;
                    SpeedProfile(speedProfile);
                    TEZMCAux.Execute($"BASE({Axis[0].AxisNo},{Axis[1].AxisNo}) MOVE({pos[0]:f6},{pos[1]:f6})");
                    if (wait) while (Busy) Thread.Sleep(0);
                    if (!Ready) return false;
                }
                catch (Exception ex)
                {
                    GDefine.SystemState = ESystemState.ErrorInit;
                    GAlarm.Prompt(EAlarm.AXIS_UNRECOVERABLE_ERROR_RESTART, ex);
                    return false;
                }
                return true;
            }
            public bool MoveOpXYRel(double[] relXY)
            {
                return MoveXYRel(GProcessPara.Operation.GXYSpeed, relXY, true);
            }

            public bool MoveOpZAbs(double absZ)
            {
                try
                {
                    if (!ZAxis.Ready) return false;

                    ZAxis.MoveAbs(GProcessPara.Operation.GZSpeed, absZ, true);
                    while (ZAxis.Busy) { Thread.Sleep(0); }

                    if (!ZAxis.Ready) return false;

                    return true;
                }
                catch
                {
                    GDefine.SystemState = ESystemState.ErrorInit;
                    GAlarm.Prompt(EAlarm.AXIS_UNRECOVERABLE_ERROR_RESTART, Axis[2].Name);
                    return false;
                }
            }
            public bool MoveOpZRel(double relZ)
            {
                try
                {
                    if (!ZAxis.Ready) return false;

                    ZAxis.MoveRel(GProcessPara.Operation.GZSpeed, relZ, true);
                    while (ZAxis.Busy) { Thread.Sleep(0); }

                    if (!ZAxis.Ready) return false;

                    return true;
                }
                catch
                {
                    GDefine.SystemState = ESystemState.ErrorInit;
                    GAlarm.Prompt(EAlarm.AXIS_UNRECOVERABLE_ERROR_RESTART, Axis[2].Name);
                    return false;
                }
            }

            public bool GotoXYZ(PointXYZ pointXYZ, bool promptZDown)
            {
                try
                {
                    if (!MoveOpZAbs(0)) return false;
                    if (!MoveOpXYAbs(pointXYZ.XYPos, true)) return false;
                    if (pointXYZ.Z < -5 && promptZDown) if (MsgBox.ShowDialog("Move Z Down?", MsgBoxBtns.YesNo) == DialogResult.No) return true;
                    if (!MoveOpZAbs(pointXYZ.Z)) return false;
                }
                catch
                {
                    GDefine.SystemState = ESystemState.ErrorInit;
                    GAlarm.Prompt(EAlarm.AXIS_UNRECOVERABLE_ERROR_RESTART);
                    return false;
                }
                return true;
            }
            public bool GotoXYZ(PointXYZ pointXYZ)
            {
                return GotoXYZ(pointXYZ, false);
            }

            public void MovePause()
            {
                try
                {
                    DirectCommand($"BASE({Axis[0].AxisNo},{Axis[1].AxisNo},{Axis[2].AxisNo}) MOVE_PAUSE(1)");
                }
                catch (Exception ex)
                {
                    GDefine.SystemState = ESystemState.ErrorInit;
                    GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                }
            }
            public void Cancel()
            {
                try
                {
                    DirectCommand($"BASE({Axis[0].AxisNo},{Axis[1].AxisNo},{Axis[2].AxisNo}) CANCEL(2)");
                }
                catch (Exception ex)
                {
                    GDefine.SystemState = ESystemState.ErrorInit;
                    GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, MethodBase.GetCurrentMethod().Name.ToString() + " " + ex.Message.ToString());
                }
            }

            public PointD PointXY
            {
                get
                {
                    return new PointD(Axis[0].ActualPos, Axis[1].ActualPos);
                }
            }
            public PointXYZ PointXYZ
            {
                get
                {
                    return new PointXYZ(Axis[0].ActualPos, Axis[1].ActualPos, Axis[2].ActualPos);
                }
            }

            public string sBase
            {
                get
                {
                    // => extract axis no
                    // => tostring()
                    // => join as a string

                    // (e.g.) BASE(0,1,2)

                    return $"BASE({string.Join(",", Axis.Select(x => x.AxisNo.ToString()))}) ";
                }
            }
        }
    }
}