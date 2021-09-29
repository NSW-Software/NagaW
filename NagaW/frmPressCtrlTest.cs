using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NagaW
{
    public partial class frmPressCtrlTest : Form
    {
        public frmPressCtrlTest()
        {
            InitializeComponent();
        }

        private void frmPressCtrlTest_Load(object sender, EventArgs e)
        {
            Text = "Pressure Controller";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            for(int i =0; i < 4; i++)
            if (TFPressCtrl.FPress[i].IsOpen)
                cmb_PressCtrl.Items.Add(TFPressCtrl.FPress[i] + i.ToString() );

            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            lbl_SetPressCtrl.Text = value.Value.ToString("f2"); ;

            btn_leftPumpPress.BackColor = GMotDef.Out0.Status ? Color.Red : Color.LightGray;
            btn_RightPumpPress.BackColor = GMotDef.Out5.Status ? Color.Red : Color.LightGray;
        }

        DPara value = new DPara(nameof(value), 0, 0, 0.9, EUnit.MPA);
        private void lbl_SetPressCtrl_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref value);
            UpdateDisplay();
        }
        private void btn_Set_Click(object sender, EventArgs e)
        {
            TFPressCtrl.FPress[cmb_PressCtrl.SelectedIndex].Set(value.Value);
        }
        private void btn_getPressValue_Click(object sender, EventArgs e)
        {
            double getValue = 0;
            TFPressCtrl.FPress[cmb_PressCtrl.SelectedIndex].Get(ref getValue);
            lbl_GetPressCtrl.Text = getValue.ToString("f2");
        }

        private void btn_leftPumpPress_Click(object sender, EventArgs e)
        {
            bool tmp = GMotDef.Out0.Status == true ? GMotDef.Out0.Status = false : GMotDef.Out0.Status = true;
            UpdateDisplay();
        }
        private void btn_RightPumpPress_Click(object sender, EventArgs e)
        {
            bool tmp = GMotDef.Out5.Status == true ? GMotDef.Out5.Status = false : GMotDef.Out5.Status = true;
            UpdateDisplay();
        }
    }
}
