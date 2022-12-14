using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;

namespace NagaW
{
    public class TEFuncImport
    {
        public TFunction Function { get; set; } = new TFunction();
        public PointD StartPos { get; set; } = new PointD(0, 0);
        public TEFuncImport() { }
        public TEFuncImport(TFunction function)
        {
            Function = function;
        }
        public TEFuncImport(TFunction function, PointD startpos)
        {
            Function = function;
            StartPos = startpos;
        }
    }

    class TFFuncImport
    {
        public static BindingList<TEFuncImport> Functions = new BindingList<TEFuncImport>();

        public static bool Save()
        {
            return GDoc.SaveXML(GDoc.SMTRecipeFile.FullName, MethodBase.GetCurrentMethod().DeclaringType);
        }
        public static bool Load()
        {
            Functions.Clear();
            return GDoc.LoadXML(GDoc.SMTRecipeFile.FullName, MethodBase.GetCurrentMethod().DeclaringType);
        }
    }

    class TFFuncStat
    {
        public static int SelectedIdx = 0;
        public static PointD Scale = new PointD(0, 0);
        public static IPara Orientation = new IPara("Orientation", 0, 0, 360, EUnit.ANGLE);
    }
}
