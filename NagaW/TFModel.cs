using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace NagaW
{

    public class TFModel
    {
        public TFModel() 
        { 
            
        }
        public TFModel(int index)
        {
            string name = "";//$"Model {index + 1} ";

        }

        [ReadOnly(true)]
        public SP_Param[] SP_Model { get; set; } = Enumerable.Range(0, 10).Select(x => new SP_Param(x)).ToArray();
        [ReadOnly(true)]
        public HM_Param[] HM_Model { get; set; } = Enumerable.Range(0, 10).Select(x => new HM_Param(x)).ToArray();
        [ReadOnly(true)]
        public Vermes3280_Param[] VM3280_Model { get; set; } = Enumerable.Range(0, 10).Select(x => new Vermes3280_Param(x)).ToArray();

        //public TFModel_SP[] SP_Mode { get; set; } = Enumerable.Range(0, 10).Select(x => new TFModel_SP()).ToArray();
    }

}
