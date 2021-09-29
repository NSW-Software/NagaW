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
    public partial class frmPumpSetup_PneumaticJet : Form
    {
        int Index;

        PneumaticJet_Param Jet_Param = null;
        TEZMCAux.TOutput FPressIO = null;
        TEZMCAux.TOutput PPressIO = null;

        TEPressCtrl FPressCtrl = null;
        TEPressCtrl PPressCtrl = null;

        public frmPumpSetup_PneumaticJet()
        {
            InitializeComponent();
        }

        public frmPumpSetup_PneumaticJet(PneumaticJet_Param jet_Param, TEZMCAux.TOutput fpressio, TEZMCAux.TOutput vpressio, TEPressCtrl fpressctrl, TEPressCtrl ppressctrl) : this()
        {
            Jet_Param = jet_Param;
            FPressIO = fpressio;
            PPressIO = vpressio;
            FPressCtrl = fpressctrl;
            PPressCtrl = ppressctrl;

            Index = Array.IndexOf(TFPressCtrl.FPress, fpressctrl);
        }
        private void frmPumpSetup_PneumaticJet_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            UpdateDisplay();
            GControl.LogForm(this);

            FPressCtrl.Set(Jet_Param.FPress.Value);
            PPressCtrl.Set(Jet_Param.VPress.Value);
        }
        private void frmPumpSetup_PnuematicJet_FormClosing(object sender, FormClosingEventArgs e)
        {
            FPressIO.Status = PPressIO.Status = false;
        }
        public void UpdateDisplay()
        {
            lblFPress.UpdatePara(Jet_Param.FPress);
            lblVPress.UpdatePara(Jet_Param.VPress);

            lblDisptime.UpdatePara(Jet_Param.DispTime);
            lblOfftime.UpdatePara(Jet_Param.OffTime);
        }


        private void btnFPressIO_Click(object sender, EventArgs e)
        {
            FPressIO.Status = !FPressIO.Status;
        }

        private void btnVPressIO_Click(object sender, EventArgs e)
        {
            PPressIO.Status = !PPressIO.Status;
        }



        private void lblFPress_Click(object sender, EventArgs e)
        {
            var para = Jet_Param.FPress;
            GLog.SetPara(ref para);
            FPressCtrl.Set(para.Value);
            UpdateDisplay();

        }

        private void lblVPress_Click(object sender, EventArgs e)
        {
            var para = Jet_Param.VPress;
            GLog.SetPara(ref para);
            PPressCtrl.Set(para.Value);
            UpdateDisplay();

        }

        private void lblDisptime_Click(object sender, EventArgs e)
        {
            var para = Jet_Param.DispTime;
            GLog.SetPara(ref para);
            UpdateDisplay();

        }

        private void lblOfftime_Click(object sender, EventArgs e)
        {
            var para = Jet_Param.OffTime;
            GLog.SetPara(ref para);
            UpdateDisplay();

        }


        private async void btnShot_Click(object sender, EventArgs e)
        {
            GControl.UI_Disable();
            await Task.Run(() => TFPump.PnuematicJet.Shot_One(Index, Jet_Param, FPressIO, PPressIO));
            GControl.UI_Enable();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            btnFPressIO.BackColor = FPressIO.Status ? Color.Lime : SystemColors.Control;
            btnVPressIO.BackColor = PPressIO.Status ? Color.Lime : SystemColors.Control;
        }
    }
}
