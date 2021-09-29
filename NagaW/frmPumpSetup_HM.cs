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
    public partial class frmPumpSetup_HM : Form
    {
        HM_Param HM_Setup = new HM_Param();
        TEZMCAux.TOutput FpressIO = null;
        TEZMCAux.TOutput VacIO = null;

        TEPressCtrl FPressCtrl = null;

        int Index = 0;
        public frmPumpSetup_HM()
        {
            InitializeComponent();
        }

        public frmPumpSetup_HM(HM_Param hms, TEZMCAux.TOutput fpressIO, TEZMCAux.TOutput vacIO, TEPressCtrl fpressCtrl) : this()
        {
            this.HM_Setup = hms;
            this.FpressIO = fpressIO;
            this.VacIO = vacIO;
            this.FPressCtrl = fpressCtrl;
        }

        private void frmHeliMasterSetup_Load(object sender, EventArgs e)
        {
            UpdateDisplay();

            Index = Array.IndexOf(TFPressCtrl.FPress, FPressCtrl);

            timer1.Enabled = true;
        }
        private void UpdateDisplay()
        {
            lblDispAmt.UpdatePara(HM_Setup.DispTime);
            lblBSuckAmt.UpdatePara(HM_Setup.BSuckTime);
            lblDispSpeed.UpdatePara(HM_Setup.DispRPM);
            lblBSuckSpeed.UpdatePara(HM_Setup.BSuckRPM);

            lblDispAcc.UpdatePara(HM_Setup.DispAccel);
            //lblBSuckAcc.UpdatePara(HM_Setup.BSuckAccel);

            lblVacDur.UpdatePara(HM_Setup.VacDur);
            lblFPress.UpdatePara(HM_Setup.FPress);

            lblShotCount.UpdatePara(ShoutCount);
        }

        private void lblDispAmt_Click(object sender, EventArgs e)
        {
            var para = HM_Setup.DispTime;
            GLog.SetPara(ref para);
            UpdateDisplay();
        }
        private void lblBSuckAmt_Click(object sender, EventArgs e)
        {
            var para = HM_Setup.BSuckTime;
            GLog.SetPara(ref para);
            UpdateDisplay();
        }
        private void lblDispSpeed_Click(object sender, EventArgs e)
        {
            var para = HM_Setup.DispRPM;
            GLog.SetPara(ref para);
            UpdateDisplay();
        }
        private void lblBSuckSpeed_Click(object sender, EventArgs e)
        {
            var para = HM_Setup.BSuckRPM;
            GLog.SetPara(ref para);
            UpdateDisplay();
        }
        private void lblDispAcc_Click(object sender, EventArgs e)
        {
            var para = HM_Setup.DispAccel;
            GLog.SetPara(ref para);
            UpdateDisplay();
        }
        private void lblBSuckAcc_Click(object sender, EventArgs e)
        {
            var para = HM_Setup.BSuckAccel;
            GLog.SetPara(ref para);
            UpdateDisplay();
        }

        private void btnTriggerH_MouseDown(object sender, MouseEventArgs e)
        {
            GControl.UI_Disable(btnTriggerH);
            TEZMCAux.DirectCommand(TFPump.HM.PurgeStartCmd(Index, HM_Setup, FpressIO, VacIO));
        }
        private void btnTriggerH_MouseUp(object sender, MouseEventArgs e)
        {
            TEZMCAux.DirectCommand(TFPump.HM.PurgeStopCmd(Index, HM_Setup, FpressIO, VacIO));
            GControl.UI_Enable();
        }

        private void lblVacDur_Click(object sender, EventArgs e)
        {
            var para = HM_Setup.VacDur;
            GLog.SetPara(ref para);
            UpdateDisplay();
        }
        private void lblFPress_Click(object sender, EventArgs e)
        {
            var para = HM_Setup.FPress;
            if (!GLog.SetPara(ref para)) return;

            FPressCtrl.Set(para.Value);
            UpdateDisplay();
        }

        private void btFPress_Click(object sender, EventArgs e)
        {
            FpressIO.Status = !FpressIO.Status;
        }
        private void btnVacuum_Click(object sender, EventArgs e)
        {
            VacIO.Status = !VacIO.Status;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Color on = Color.Red;
            Color off = SystemColors.ControlDark;

            btnFPress.BackColor = FpressIO.Status ? on : off;
            btnVacuum.BackColor = VacIO.Status ? on : off;
        }

        bool stopshot = true;
        string shottxt = "";
        private async void btnShot_Click(object sender, EventArgs e)
        {
            if (!stopshot)
            {
                stopshot = true;
                return;
            }

            GControl.UI_Disable(btnShot);

            stopshot = false;
            shottxt = btnShot.Text;

            await Task.Run(() =>
            {
                for (int i = 0; i < ShoutCount.Value; i++)
                {
                    TFPump.HM.Shot_One(Index, HM_Setup, FpressIO, VacIO);
                    btnShot.Invoke(new Action(() => btnShot.Text = $"{shottxt} (x{i + 1})"));
                    if (stopshot) break;
                }
            });
            btnShot.Text = shottxt;
            stopshot = true;
            GControl.UI_Enable();
        }

        static bool trig = false;
        private void btnTrigger_Click(object sender, EventArgs e)
        {
            if (!trig)
            {
                GControl.UI_Disable(btnTrigger);
                var cmd = TFPump.HM.PurgeStartCmd(Index, HM_Setup, FpressIO, VacIO);
                TEZMCAux.DirectCommand(cmd);
            }
            else
            {
                var cmd = TFPump.HM.PurgeStopCmd(Index, HM_Setup, FpressIO, VacIO);
                TEZMCAux.DirectCommand(cmd);
                GControl.UI_Enable();
            }
            trig = !trig;
        }
        static IPara ShoutCount = new IPara("HM_ShotCount", 1, 1, 20, EUnit.COUNT);

        private void lblShotCount_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref ShoutCount);
            UpdateDisplay();
        }
    }
}
