using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;

namespace NagaW
{
    class GEnum
    {
    }

    public enum ECamType
    {
        Spinnaker,
        MVC_GenTL
    }

    public enum EDynamicDispMode
    {
        False,
        FirstJet,
        Everytime,
    }

    public enum ECalibrationState
    {
        Unknown,
        Fail,
        Completed
    }
    public enum EEquipmentModel
    {
        SD1,
        SD1X,
    }

    public enum Elevel
    {
        TECHNICIAN,
        OPERATOR,
        ENGINEER,
        ADMIN
    }

    public enum ECOM
    {
        NONE,
        IP,
        COM1,
        COM2,
        COM3,
        COM4,
        COM5,
        COM6,
        COM7,
        COM8,
        COM9,
        COM10,
        COM11,
        COM12,
        COM13,
        COM14,
        COM15,
        COM16,
        COM17,
        COM18,
        COM19,
        COM20,
    }

    public enum ELogType
    {
        PARA = 1,
        ALARM = 11,
        NOTIFY = 21,
        EVENT = 31,
        CTRL = 41,
        EXCEP = 51,
        PROCESS = 61,
        SYSTEM = 71,

        SECSGEMS = 101,
    }

    public enum EUnit : byte
    {
        [Description("")]
        NONE = 0,
        [Description("x")]
        COUNT,
        [Description("mm")]
        MILLIMETER,
        [Description("mm/s")]
        MILLIMETER_PER_SECOND,
        [Description("mm/s²")]
        MILLIMETER_PER_SECOND_SQUARED,
        [Description("ms")]
        MILLISECOND,
        [Description("s")]
        SECOND,
        [Description("mL")]
        MILLILITRE,
        [Description("m³")]
        CUBICMETRE,
        [Description("mg")]
        MILLIGRAM,
        [Description("%")]
        PERCENTAGE,
        [Description("MPa")]
        MPA,
        [Description("Bar")]
        BAR,
        [Description("PSI")]
        PSI,
        [Description("°C")]
        DEGREE_CELSIUS,
        [Description("°")]
        ANGLE,
        [Description("CW/CCW")]
        CWCCW,
        [Description("RPM")]
        RPM,
        [Description("RPM²")]
        RPMPERSEC,
        [Description("X")]
        RATE,
        [Description("in")]
        INCH,
        [Description("μL")]
        MICROLITERS,
        [Description("mg/s")]
        MASS_FLOW_RATE,
    }
    public enum EFPressUnit
    {
        MPA,
        BAR,
        PSI,
    }
    public enum EDecimalPlace
    {
        TWO,
        THREE,
        FOUR,
    }

    public enum EZTouchType
    {
        IO,
        LINEAR,
    }

    public enum EConvInterface
    {
        SMEMA_LR_SERIAL, 
        SMEMA_LR_PARALLEL,
        SMEMA_LR_MIRROR,
        CONV_BY_PASS,
        //NONE,
        //SMEMA_IN = 10,
        //SMEMA_OUT = 11,
        //MANUAL_IN = 20,
        //MANUAL_OUT = 21,
        //MANUAL_IN_OUT = 22,
    }
    public enum EPumpType
    {
        None = 0,

        // IO Interface 1~100
        SP = 1,
        //PP4 = 2,
        HM = 3,
        SPLite = 2,
        TP = 10,

        // SerialCom Interface 101~200
        VERMES_3280 = 101,
        //VERMES_3200,
        //VERMES_1560,
        PNEUMATIC_JET,
    }

    public enum EFileType
    {
        INI,
        XML,
    }

    public enum EDynamicJetDir
    {
        LeftToRight,
        RightToLeft,
        BackToFront,
        FrontToBack,
    }

    public enum ESecsGemsDir
    { 
        HostToLocal,
        LocalToHost,
        NSWToLocal,
        LocalToNSW,
    }

    #region
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Serializable]
    public class StaticPropertyDescriptor : PropertyDescriptor
    {
        PropertyInfo p;
        Type owenrType;
        public StaticPropertyDescriptor(PropertyInfo pi, Type owenrType)
            : base(pi.Name,
                  pi.GetCustomAttributes().Cast<Attribute>().ToArray())
        {
            p = pi;
            this.owenrType = owenrType;
        }
        public override bool CanResetValue(object c) => false;
        public override object GetValue(object c) => p.GetValue(null);
        public override void ResetValue(object c) { }
        public override void SetValue(object c, object v) => p.SetValue(null, v);
        public override bool ShouldSerializeValue(object c) => false;
        public override Type ComponentType { get { return owenrType; } }
        //public override bool IsReadOnly { get { return !p.CanWrite; } }
        public override bool IsReadOnly
        {
            get
            {
                var atbs = p.GetCustomAttributes(typeof(ReadOnlyAttribute), false).ToArray();
                if (atbs.Length is 0) return false;

                var a = atbs[0];
                return (a as ReadOnlyAttribute).IsReadOnly;
            }
        }
        public override string DisplayName
        {
            get
            {
                var atbs = p.GetCustomAttributes(typeof(DisplayNameAttribute), false).ToArray();
                if (atbs.Length is 0) return p.Name;

                var a = atbs[0];
                return (a as DisplayNameAttribute).DisplayName;
            }
        }
        public override Type PropertyType { get { return p.PropertyType; } }

    }
    public class CustomObjectWrapper : CustomTypeDescriptor
    {
        public object WrappedObject { get; private set; }
        private IEnumerable<PropertyDescriptor> staticProperties;
        public CustomObjectWrapper(object o)
            : base(TypeDescriptor.GetProvider(o).GetTypeDescriptor(o))
        {
            WrappedObject = o;
        }
        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            var instanceProperties = base.GetProperties(attributes)
                .Cast<PropertyDescriptor>();
            staticProperties = WrappedObject.GetType()
                .GetProperties(BindingFlags.Static | BindingFlags.Public)
                .Select(p => new StaticPropertyDescriptor(p, WrappedObject.GetType()));
            return new PropertyDescriptorCollection(
                instanceProperties.Union(staticProperties).ToArray());
        }
    }
    #endregion
}
