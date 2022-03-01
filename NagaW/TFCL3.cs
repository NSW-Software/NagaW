using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NagaW
{

    public class TFCL3
    {
        public bool IsConnected;

        public const int DeviceID = 0;
        public void Open(string IPaddress)
        {
            try
            {
                IsConnected = false;

                KeyenceAPI.CL3IF_ETHERNET_SETTING ethernetSetting = new KeyenceAPI.CL3IF_ETHERNET_SETTING();
                ethernetSetting.ipAddress = new byte[4];

                var ip_byte = IPaddress.Split('.').Select(x => byte.Parse(x)).ToArray();

                ethernetSetting.ipAddress = ip_byte;
                ethernetSetting.reserved = new byte[2];
                //ethernetSetting.portNo = (ushort)
                ethernetSetting.reserved[0] = 0x00;
                ethernetSetting.reserved[1] = 0x00;
                ethernetSetting.portNo = (ushort)24685;

                int returnCode = KeyenceAPI.CL3IF_OpenEthernetCommunication(DeviceID, ref ethernetSetting, 10000);
                if (returnCode != KeyenceAPI.CL3IF_RC_OK) throw new Exception("Fail to Open");

                IsConnected = true;
            }
            catch
            {
                throw;
            }
        }

        public void Close()
        {
            try
            {
                IsConnected = false;

                int returnCode = KeyenceAPI.CL3IF_CloseCommunication(DeviceID);
                if (returnCode != KeyenceAPI.CL3IF_RC_OK) throw new Exception("Fail to Close Connection");
            }
            catch
            {
                throw;
            }
        }

        private double lastvalue;
        public void GetValue(int outno, out double value)
        {
            value = -1;
            using (KeyenceAPI.PinnedObject pin = new KeyenceAPI.PinnedObject(new byte[512000]))
            {
                KeyenceAPI.CL3IF_MEASUREMENT_DATA measurementData = new KeyenceAPI.CL3IF_MEASUREMENT_DATA();
                measurementData.outMeasurementData = new KeyenceAPI.CL3IF_OUTMEASUREMENT_DATA[8];

                int rescode = KeyenceAPI.CL3IF_GetMeasurementData(0, pin.Pointer);
                if (rescode != KeyenceAPI.CL3IF_RC_OK) throw new Exception("Fail to Get Measurement Value");

                measurementData.addInfo = (KeyenceAPI.CL3IF_ADD_INFO)Marshal.PtrToStructure(pin.Pointer, typeof(KeyenceAPI.CL3IF_ADD_INFO));

                int readPosition = Marshal.SizeOf(typeof(KeyenceAPI.CL3IF_ADD_INFO));
                for (int i = 0; i < Math.Min(outno + 1, 8); i++)
                {
                    measurementData.outMeasurementData[i] = (KeyenceAPI.CL3IF_OUTMEASUREMENT_DATA)Marshal.PtrToStructure(pin.Pointer + readPosition, typeof(KeyenceAPI.CL3IF_OUTMEASUREMENT_DATA));
                    readPosition += Marshal.SizeOf(typeof(KeyenceAPI.CL3IF_OUTMEASUREMENT_DATA));
                }

                value = (double)measurementData.outMeasurementData[outno].measurementValue;
                value /= (double)10000;

                if (value is 0) value = lastvalue;
                else lastvalue = value;
            }
        }
    }

    class KeyenceAPI
    {
        #region
        public sealed class PinnedObject : IDisposable
        {
            private GCHandle _Handle;

            public IntPtr Pointer
            {
                get { return _Handle.AddrOfPinnedObject(); }
            }

            public PinnedObject(object target)
            {
                _Handle = GCHandle.Alloc(target, GCHandleType.Pinned);
            }

            public void Dispose()
            {
                _Handle.Free();
                _Handle = new GCHandle();
            }
        }
        public enum CL3IF_OUTNO
        {
            CL3IF_OUTNO_01 = 0x0001, // OUT1
            CL3IF_OUTNO_02 = 0x0002, // OUT2
            CL3IF_OUTNO_03 = 0x0004, // OUT3
            CL3IF_OUTNO_04 = 0x0008, // OUT4
            CL3IF_OUTNO_05 = 0x0010, // OUT5
            CL3IF_OUTNO_06 = 0x0020, // OUT6
            CL3IF_OUTNO_07 = 0x0040, // OUT7
            CL3IF_OUTNO_08 = 0x0080, // OUT8
            CL3IF_OUTNO_ALL = 0x00FF // ALL
        };
        [StructLayout(LayoutKind.Sequential)]
        public struct CL3IF_ETHERNET_SETTING
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] ipAddress;
            public ushort portNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] reserved;
        };
        [StructLayout(LayoutKind.Explicit)]
        public struct CL3IF_HOLDMODE_PARAM_NORMAL
        {
            [FieldOffset(0)]
            public byte reserved_1;
            [FieldOffset(1)]
            public byte reserved_2;
            [FieldOffset(2)]
            public byte reserved_3;
            [FieldOffset(3)]
            public byte reserved_4;
        };
        [StructLayout(LayoutKind.Explicit)]
        public struct CL3IF_HOLDMODE_PARAM_HOLD
        {
            [FieldOffset(0)]
            public byte updateCondition;
            [FieldOffset(1)]
            public byte reserved;
            [FieldOffset(2)]
            public ushort numberOfSamplings;
        };
        [StructLayout(LayoutKind.Explicit)]
        public struct CL3IF_HOLDMODE_PARAM_AUTOHOLD
        {
            [FieldOffset(0)]
            public int level;
            [FieldOffset(4)]
            public int hysteresis;
        };
        [StructLayout(LayoutKind.Explicit)]
        public struct CL3IF_HOLDMODE_PARAM
        {
            [FieldOffset(0)]
            public CL3IF_HOLDMODE_PARAM_NORMAL paramNormal;
            [FieldOffset(0)]
            public CL3IF_HOLDMODE_PARAM_HOLD paramHold;
            [FieldOffset(0)]
            public CL3IF_HOLDMODE_PARAM_AUTOHOLD paramAutoHold;
        };
        public enum CL3IF_HOLDMODE
        {
            CL3IF_HOLDMODE_NORMAL,          // Normal
            CL3IF_HOLDMODE_PEAK,            // Peak hold
            CL3IF_HOLDMODE_BOTTOM,          // Bottom hold
            CL3IF_HOLDMODE_PEAK_TO_PEAK,    // Peak to peak hold
            CL3IF_HOLDMODE_SAMPLE,          // Sample hold
            CL3IF_HOLDMODE_AVERAGE,         // Average hold
            CL3IF_HOLDMODE_AUTOPEAK,        // Auto Peak hold
            CL3IF_HOLDMODE_AUTOBOTTOM       // Auto bottom hold
        };
        public struct CL3IF_VERSION_INFO
        {
            public int majorNumber;
            public int minorNumber;
            public int revisionNumber;
            public int buildNumber;
        }
        public struct CL3IF_ADD_INFO
        {
            public uint triggerCount;
            public int pulseCount;
        }
        public struct CL3IF_MEASUREMENT_DATA
        {
            public CL3IF_ADD_INFO addInfo;
            public CL3IF_OUTMEASUREMENT_DATA[] outMeasurementData;
        };

        public struct CL3IF_OUTMEASUREMENT_DATA
        {
            public int measurementValue;
            public byte valueInfo;
            public byte judgeResult;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] reserved;

        }

        public const int CL3IF_RC_OK = 0;
        public const int CL3IF_RC_ERR_INITIALIZE = 100;
        public const int CL3IF_RC_ERR_NOT_PARAM = 101;
        public const int CL3IF_RC_ERR_USB = 102;
        public const int CL3IF_RC_ERR_ETHERNET = 103;
        public const int CL3IF_RC_ERR_CONNECT = 105;
        public const int CL3IF_RC_ERR_TIMEOUT = 106;
        public const int CL3IF_RC_ERR_CHECKSUM = 110;
        public const int CL3IF_RC_ERR_LIMIT_CONTROL_ERROR = 120;
        public const int CL3IF_RC_ERR_UNKNOWN = 127;

        public const int CL3IF_RC_ERR_STATE_ERROR = 81;
        public const int CL3IF_RC_ERR_PARAMETER_NUMBER_ERROR = 82;
        public const int CL3IF_RC_ERR_PARAMETER_RANGE_ERROR = 83;
        public const int CL3IF_RC_ERR_UNIQUE_ERROR1 = 84;
        public const int CL3IF_RC_ERR_UNIQUE_ERROR2 = 85;
        public const int CL3IF_RC_ERR_UNIQUE_ERROR3 = 86;

        public const int CL3IF_MAX_OUT_COUNT = 8;
        public const int CL3IF_MAX_HEAD_COUNT = 6;
        public const int CL3IF_MAX_DEVICE_COUNT = 3;
        public const int CL3IF_ALL_SETTINGS_DATA_LENGTH = 16612;
        public const int CL3IF_PROGRAM_SETTINGS_DATA_LENGTH = 1724;
        public const int CL3IF_LIGHT_WAVE_DATA_LENGTH = 512;
        public const int CL3IF_MAX_LIGHT_WAVE_COUNT = 4;

        #endregion

        private const string DllName = @"CL3_IF.dll";

        internal static List<int> ConvertOutTargetList(CL3IF_OUTNO outTarget)
        {
            byte mask = 1;
            List<int> outList = new List<int>();
            for (int i = 0; i < CL3IF_MAX_OUT_COUNT; i++)
            {
                if (((ushort)outTarget & mask) != 0)
                {
                    outList.Add(i + 1);
                }
                mask = (byte)(mask << 1);
            }
            return outList;
        }

        [DllImport(DllName)]
        internal static extern int CL3IF_OpenUsbCommunication(int deviceId, uint timeout);
        [DllImport(DllName)]
        internal static extern int CL3IF_OpenEthernetCommunication(int deviceId, ref CL3IF_ETHERNET_SETTING ethernetSetting, uint timeout);
        [DllImport(DllName)]
        internal static extern int CL3IF_CloseCommunication(int deviceId);
        [DllImport(DllName)]
        internal static extern int CL3IF_GetMeasurementData(int deviceId, IntPtr measurementData);

        [DllImport(DllName)]
        internal static extern int CL3IF_StartStorage(int deviceId);
        [DllImport(DllName)]
        internal static extern int CL3IF_StopStorage(int deviceId);
        [DllImport(DllName)]
        internal static extern int CL3IF_ClearStorageData(int deviceId);
        [DllImport(DllName)]
        internal static extern int CL3IF_GetStorageData(int deviceId, uint index, uint requestDataCount, out uint nextIndex, out uint obtainedDataCount, out CL3IF_OUTNO outTarget, IntPtr measurementData);
        [DllImport(DllName)]
        internal static extern int CL3IF_SetHold(int deviceId, byte programNo, byte outNo, CL3IF_HOLDMODE holdMode, CL3IF_HOLDMODE_PARAM param);
    }
}






