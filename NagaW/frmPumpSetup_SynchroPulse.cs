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
    public partial class frmPumpSetup_SynchroPulse : Form
    {
        int ctrlIndex = 0;
 
        EPumpType pumpType = EPumpType.None;
        SP_Param SP_Setup = new SP_Param();

        TEZMCAux.TOutput FPressIO = null;
        TEZMCAux.TOutput PPressIO = null;
        TEZMCAux.TOutput VacIO = null;

        TEPressCtrl FPressCtrl = new TEPressCtrl();
        TEPressCtrl PPressCtrl = new TEPressCtrl();

        public frmPumpSetup_SynchroPulse()
        {
            InitializeComponent();
        }

        public frmPumpSetup_SynchroPulse(int ctrlIndex, EPumpType pumpType, SP_Param sp, TEZMCAux.TOutput fpressio, TEZMCAux.TOutput ppressio
            , TEZMCAux.TOutput vacio, TEPressCtrl fpress, TEPressCtrl ppress) : this()
        {
            this.ctrlIndex = ctrlIndex;

            this.pumpType = pumpType;
            pnlSP.Visible = pumpType == EPumpType.SP;
            pnlSPDelay.Visible = pumpType == EPumpType.SP;

            SP_Setup = sp;
            FPressIO = fpressio; PPressIO = ppressio; VacIO = vacio;
            timer1.Enabled = true;
            FPressCtrl = fpress; PPressCtrl = ppress;
        }

        private void frmSynchroPulseSetup_Load(object sender, EventArgs e)
        {
            UpdateDisplay();
            GControl.LogForm(this);
        }

        public void UpdateDisplay()
        {
            GControl.UpdateFormControl(this);

            lblFPress.UpdatePara(SP_Setup.FPress);
            lblPPress.UpdatePara(SP_Setup.PPress);
            lblDispTime.UpdatePara(SP_Setup.DispTime);
            lblPulseOnDelayEarly.Text = "PulseOn (" + (SP_Setup.PulseOnDelay.Value < 0 ? "Early" : "Delay") + ")";
            lblPulseOnDelay.UpdatePara(SP_Setup.PulseOnDelay);
            lblPulseOffDelayEarly.Text = "PulseOff (" + (SP_Setup.PulseOnDelay.Value < 0 ? "Early" : "Delay") + ")";
            lblPulseOffDelay.UpdatePara(SP_Setup.PulseOffDelay);
            lblFPressH.UpdatePara(SP_Setup.FPressH);
            lblVacdur.UpdatePara(SP_Setup.VacDur);
        }

        private void lblFPress_Click(object sender, EventArgs e)
        {
            DPara para = SP_Setup.FPress;
            GLog.SetPara(ref para);
            UpdateDisplay();
            FPressCtrl.Set(para.Value);
        }
        private void lblPPress_Click(object sender, EventArgs e)
        {
            DPara para = SP_Setup.PPress;
            GLog.SetPara(ref para);
            UpdateDisplay();
            PPressCtrl.Set(para.Value);
        }
        private void lblDispTime_Click(object sender, EventArgs e)
        {
            DPara para = SP_Setup.DispTime;
            GLog.SetPara(ref para);
            UpdateDisplay();
        }
        private void lblPulseOnDelay_Click(object sender, EventArgs e)
        {
            DPara para = SP_Setup.PulseOnDelay;
            GLog.SetPara(ref para);
            UpdateDisplay();
        }
        private void lblPulseOffDelay_Click(object sender, EventArgs e)
        {
            DPara para = SP_Setup.PulseOffDelay;
            GLog.SetPara(ref para);
            UpdateDisplay();
        }
        private void lblFPressH_Click(object sender, EventArgs e)
        {
            DPara para = SP_Setup.FPressH;
            GLog.SetPara(ref para);
            UpdateDisplay();
        }
        private void lblVacdur_Click(object sender, EventArgs e)
        {
            DPara para = SP_Setup.VacDur;
            GLog.SetPara(ref para);
            UpdateDisplay();
        }
        private void btnFPressH_MouseDown(object sender, MouseEventArgs e)
        {
            FPressCtrl.Set(SP_Setup.FPressH.Value);
            FPressIO.Status = true;
            //PPressIO.Status = false;
        }
        private void btnFPressH_MouseUp(object sender, MouseEventArgs e)
        {
            FPressIO.Status = false;
            //PPressIO.Status = true;
            FPressCtrl.Set(SP_Setup.FPress.Value);
        }

        private void btnFPress_Click(object sender, EventArgs e)
        {
            FPressIO.Status = !FPressIO.Status;
            //PPressIO.Status = !PPressIO.Status;
            UpdateDisplay();
        }
        private void btnPPress_Click(object sender, EventArgs e)
        {
            //PPressIO.Status = !PPressIO.Status;
            PPressIO.Status = !PPressIO.Status;
            UpdateDisplay();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            btnFPress.BackColor = FPressIO.Status ? Color.Lime : SystemColors.Control;
            btnPPress.BackColor = PPressIO.Status ? Color.Lime : SystemColors.Control;
            btnVac.BackColor = VacIO.Status ? Color.Lime : SystemColors.Control;
        }

        private void btnShot_Click(object sender, EventArgs e)
        {
            try
            {
                switch (pumpType)
                {
                    case EPumpType.SP:
                        TFPump.SP.Shot_One(ctrlIndex, SP_Setup, FPressIO, PPressIO, VacIO);
                        break;
                    case EPumpType.SPLite:
                    case EPumpType.TP:
                        TFPump.TP.Shot_One(ctrlIndex, SP_Setup, FPressIO, PPressIO, VacIO);
                        break;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnVac_Click(object sender, EventArgs e)
        {
            VacIO.Status = !VacIO.Status;
        }
    }
}
