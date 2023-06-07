using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NagaW
{
    public partial class frmPumpSetup_Vermes32xx : Form
    {
        readonly Vermes3280_Param V_Setup = new Vermes3280_Param();
        readonly Vermes_3280_SerialCom V_Pump = new Vermes_3280_SerialCom();
        readonly TEPressCtrl FPress = new TEPressCtrl();

        readonly TEZMCAux.TOutput TrigOutput;
        readonly TEZMCAux.TInput TrigInput;
        readonly TEZMCAux.TOutput FPressIO;
        readonly TEZMCAux.TOutput VacIO;

        public frmPumpSetup_Vermes32xx()
        {
            InitializeComponent();
        }

        public frmPumpSetup_Vermes32xx(Vermes3280_Param vms, Vermes_3280_SerialCom vmspump, TEPressCtrl fpress, TEZMCAux.TOutput trigout, TEZMCAux.TInput trigin, TEZMCAux.TOutput fpressio, TEZMCAux.TOutput vacio) : this()
        {
            V_Setup = vms;
            V_Pump = vmspump;
            FPress = fpress;

            TrigOutput = trigout;
            TrigInput = trigin;
            FPressIO = fpressio;
            VacIO = vacio;
        }

        private void frmVermesCtrl_Load(object sender, EventArgs e)
        {
            UpdateDisplay();
            timer1.Enabled = true;
            GControl.LogForm(this);
        }
        public void UpdateDisplay()
        {
            GControl.UpdateFormControl(this);

            lblSetupRT.UpdatePara(V_Setup.RisingTime);
            lblSetupOT.UpdatePara(V_Setup.OpenTime);
            lblSetupFT.UpdatePara(V_Setup.FallingTime);
            lblSetupNL.UpdatePara(V_Setup.NeedleLift);
            lblSetupPulse.UpdatePara(V_Setup.Pulses);
            lblSetupDelay.UpdatePara(V_Setup.Delay);

            var idx = Array.IndexOf(TFPressCtrl.FPress, FPress);
            //var fptype = GSystemCfg.FPress.FPresses[idx].PressureUnit;
            lblFPress.UpdatePara(V_Setup.FPress);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            btnVermesIOTrig.BackColor = TrigOutput.Status ? Color.Lime : SystemColors.ControlDark;
        }

        bool valveIsUp = false;
        int startTime = 0;
        private void btnValveUpDn_MouseDown(object sender, MouseEventArgs e)
        {
            if (valveIsUp)
            {
                //btnValveUpDn_MouseUp(sender, e);
                return;
            }

            //GControl.UI_Disable(btnValveUpDn);
            startTime = Environment.TickCount;

            FPress.Set(V_Setup.FPress.Value);
            FPressIO.Status = true;

            btnValveUpDn.BackColor = Color.Lime;
            btnValveUpDn.Text = "Valve Down";
            V_Pump.ValveUp();
            valveIsUp = true;
        }
        private void btnValveUpDn_MouseUp(object sender, MouseEventArgs e)
        {
            //GControl.UI_Enable();
            if (Environment.TickCount < startTime + 250)
            {
                return;
            }

            V_Pump.ValveDown();
            valveIsUp = false;

            btnValveUpDn.BackColor = SystemColors.ControlLight;
            btnValveUpDn.Text = "Valve Up";
            FPressIO.Status = false;
        }

        private void lblSetupRT_Click(object sender, EventArgs e)
        {
            var para = V_Setup.RisingTime;
            if (!GLog.SetPara(ref para)) return;
            UpdateDisplay();
            V_Pump.TriggerAset(V_Setup);
        }
        private void lblSetupOT_Click(object sender, EventArgs e)
        {
            var para = V_Setup.OpenTime;
            if (!GLog.SetPara(ref para)) return;
            UpdateDisplay();
            V_Pump.TriggerAset(V_Setup);
        }
        private void lblSetupFT_Click(object sender, EventArgs e)
        {
            var para = V_Setup.FallingTime;
            if (!GLog.SetPara(ref para)) return;
            UpdateDisplay();
            V_Pump.TriggerAset(V_Setup);
        }
        private void lblSetupNL_Click(object sender, EventArgs e)
        {
            var para = V_Setup.NeedleLift;
            if (!GLog.SetPara(ref para)) return;
            UpdateDisplay();
            V_Pump.TriggerAset(V_Setup);
        }
        private void lblSetupPulse_Click(object sender, EventArgs e)
        {
            var para = V_Setup.Pulses;
            if (!GLog.SetPara(ref para)) return;
            UpdateDisplay();
            V_Pump.TriggerAset(V_Setup);
        }
        private void lblSetupDelay_Click(object sender, EventArgs e)
        {
            var para = V_Setup.Delay;
            if (!GLog.SetPara(ref para)) return;
            UpdateDisplay(); 
            V_Pump.TriggerAset(V_Setup);
        }

        private void lblFPress_Click(object sender, EventArgs e)
        {
            var idx = Array.IndexOf(TFPressCtrl.FPress, FPress);

            var para = new DPara(V_Setup.FPress);

            if (!GLog.SetPara(ref para)) return;
            
            V_Setup.FPress.Value = para.Value;
            FPress.Set(para.Value);
            UpdateDisplay();
        }

        private void btnVermesIOTrig_Click(object sender, EventArgs e)
        {
            FPressIO.Status = !TrigOutput.Status;
            TrigOutput.Status = !TrigOutput.Status;

            if (!FPressIO.Status)
            {
                VacIO.Status = true;
                Thread.Sleep(50);
                VacIO.Status = false;
            }
        }

        private void btnAdjust_Click(object sender, EventArgs e)
        {
            V_Pump.Adjust(TrigOutput);
        }

    }
}
