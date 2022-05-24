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
    public partial class frmRecipeCommonPara : Form
    {
        //TEZMCAux.TGroup gantry;

        DPara[] Para = new DPara[10];
        TCmd Tcmd = new TCmd();

        public frmRecipeCommonPara()
        {
            InitializeComponent();
        }
        public frmRecipeCommonPara(TEZMCAux.TGroup gantry, TCmd cmd, string title) : this()
        {
            //this.gantry = gantry;

            Tcmd = cmd;
            string index = title;

            lblTitle.Text = $"{index} " + cmd.Cmd.ToString();
            index = lblTitle.Text;

            for (int i = 0; i < 10; i++) Para[i] = new DPara();

            double minDist = 0;
            double maxDist = 20;
            double minSpeed = 1;
            double maxSpeed = 5000;
            double minAccel = 0;
            double maxAccel = 5000;
            double minTime = 0;
            double maxTime = 5000;

            switch (cmd.Cmd)
            {
                case ECmd.SPEED:
                    Para[0] = new DPara($"{index} Speed", cmd.Para[0] = Math.Max(minSpeed, cmd.Para[0]), minSpeed, maxSpeed, EUnit.MILLIMETER_PER_SECOND);
                    Para[1] = new DPara($"{index} Accel", cmd.Para[1], minAccel, maxAccel, EUnit.MILLIMETER_PER_SECOND_SQUARED);
                    break;
                case ECmd.ADJUST_OFFSET:
                    Para[0] = new DPara($"{index} Adjust X", cmd.Para[0], -999.999, 999.999, EUnit.MILLIMETER);
                    Para[1] = new DPara($"{index} Adjust Y", cmd.Para[1], -999.999, 999.999, EUnit.MILLIMETER);
                    break;
                case ECmd.VERMES_3280_SETUP:
                    var vm_setup = new Vermes3280_Param();
                    Para[0] = new DPara($"{index} Rising Time", cmd.Para[0] = Math.Max(0.01, cmd.Para[0]), 0.01, 300, EUnit.MILLISECOND, 2);
                    Para[1] = new DPara($"{index} Opening Time", cmd.Para[1], 0, 3000, EUnit.MILLISECOND, 2);
                    Para[2] = new DPara($"{index} Falling Time", cmd.Para[2] = Math.Max(0.01, cmd.Para[2]), 0.01, 300, EUnit.MILLISECOND, 2);
                    Para[3] = new DPara($"{index} Needle Lift", cmd.Para[3] = Math.Max(1, cmd.Para[3]), 1, 100, EUnit.PERCENTAGE, 0);
                    Para[4] = new DPara($"{index} Pulse No", cmd.Para[4] = Math.Max(0, cmd.Para[4]), 0, 32000, EUnit.COUNT, 0);
                    Para[5] = new DPara($"{index} Delay", cmd.Para[5] = Math.Max(1, cmd.Para[5]), 1, 1000, EUnit.MILLISECOND, 1);
                    Para[6] = new DPara($"{index} FPress", cmd.Para[6], 0, vm_setup.FPress.Max, EUnit.MPA);
                    break;
                case ECmd.SP_SETUP:
                    var sp_setup = new SP_Param();
                    Para[0] = new DPara($"{index} Disp Time", cmd.Para[0], 0, sp_setup.DispTime.Max, EUnit.MILLISECOND, 0);
                    Para[1] = new DPara($"{index} Pulse On Delay", cmd.Para[1], sp_setup.PulseOnDelay.Min, sp_setup.PulseOnDelay.Max, EUnit.MILLISECOND, 0);
                    Para[2] = new DPara($"{index} Pulse Off Delay", cmd.Para[2], sp_setup.PulseOffDelay.Min, sp_setup.PulseOffDelay.Max, EUnit.MILLISECOND, 0);
                    Para[6] = new DPara($"{index} FPress", cmd.Para[6], 0, sp_setup.FPress.Max, EUnit.MPA);
                    Para[7] = new DPara($"{index} PPress", cmd.Para[7], 0, sp_setup.FPress.Max, EUnit.MPA);
                    Para[8] = new DPara($"{index} VacDur", cmd.Para[8], 0, sp_setup.VacDur.Max, EUnit.MILLISECOND);
                    break;
                case ECmd.SPLITE_SETUP:
                case ECmd.TP_SETUP:
                    var sp_setup2 = new SP_Param();
                    Para[0] = new DPara($"{index} Disp Time", cmd.Para[0], 0, sp_setup2.DispTime.Max, EUnit.MILLISECOND, 0);
                    Para[6] = new DPara($"{index} FPress", cmd.Para[6], 0, sp_setup2.FPress.Max, EUnit.MPA);
                    Para[8] = new DPara($"{index} VacDur", cmd.Para[8], 0, sp_setup2.VacDur.Max, EUnit.MILLISECOND);
                    break;
                case ECmd.HM_SETUP:
                    var hm_setup = new HM_Param();
                    Para[0] = new DPara($"DispTime", cmd.Para[0] = Math.Max(hm_setup.DispTime.Min, cmd.Para[0]), hm_setup.DispTime.Min, hm_setup.DispTime.Max, EUnit.MILLISECOND, 0);
                    Para[1] = new DPara($"DispSpd", cmd.Para[1] = Math.Max(hm_setup.DispRPM.Min, cmd.Para[1]), hm_setup.DispRPM.Min, hm_setup.DispRPM.Max, EUnit.RPM);
                    Para[2] = new DPara($"BSuckTime", cmd.Para[2] = Math.Max(hm_setup.BSuckTime.Min, cmd.Para[2]), hm_setup.BSuckTime.Min, hm_setup.BSuckTime.Max, EUnit.MILLISECOND, 0);
                    Para[3] = new DPara($"BSuckSpd", cmd.Para[3] = Math.Max(hm_setup.BSuckRPM.Min, cmd.Para[3]), hm_setup.BSuckRPM.Min, hm_setup.BSuckRPM.Max, EUnit.RPM);
                    Para[4] = new DPara($"AccDec", cmd.Para[4] = Math.Max(hm_setup.DispAccel.Min, cmd.Para[4]), hm_setup.DispAccel.Min, hm_setup.DispAccel.Max, EUnit.RPMPERSEC);
                    //Para[5] = new DPara($"BSuckAcc", cmd.Para[5] = Math.Max(hm_setup.BSuckAccel.Min, cmd.Para[5]), hm_setup.BSuckAccel.Min, hm_setup.BSuckAccel.Max, EUnit.RPMPerSec);
                    Para[6] = new DPara($"FPress", cmd.Para[6] = Math.Max(hm_setup.FPress.Min, cmd.Para[6]), hm_setup.FPress.Min, hm_setup.FPress.Max, EUnit.MPA);
                    Para[8] = new DPara($"VacDur", cmd.Para[8] = Math.Max(hm_setup.VacDur.Min, cmd.Para[8]), hm_setup.VacDur.Min, hm_setup.VacDur.Max, EUnit.MILLISECOND, 0);
                    break;
                case ECmd.PNEUMATICS_JET_SETUP:
                    var jet_setup = new PneumaticJet_Param();
                    Para[0] = new DPara($"{index} Open Time", cmd.Para[0], 0, jet_setup.DispTime.Max, EUnit.MILLISECOND, 0);
                    Para[1] = new DPara($"{index} Off Time", Math.Max(1, cmd.Para[1]), jet_setup.OffTime.Min, jet_setup.OffTime.Max, EUnit.MILLISECOND, 0);

                    Para[6] = new DPara($"{index} FPress", cmd.Para[6], 0, jet_setup.FPress.Max, EUnit.MPA);
                    Para[7] = new DPara($"{index} JPress", cmd.Para[7], 0, jet_setup.VPress.Max, EUnit.MPA);
                    break;

                case ECmd.DOT_SETUP:
                    Para[0] = new DPara($"{index} Disp Time", cmd.Para[0], minTime, maxTime, EUnit.MILLISECOND, 0);
                    Para[1] = new DPara($"{index} Wait", cmd.Para[1], minTime, maxTime, EUnit.MILLISECOND, 0);
                    break;
                case ECmd.LINE_SETUP:
                    Para[0] = new DPara($"{index} Start Delay", cmd.Para[0], minTime, maxTime, EUnit.MILLISECOND);
                    Para[1] = new DPara($"{index} Line Speed", cmd.Para[1] = Math.Max(minSpeed, cmd.Para[1]), minSpeed, maxSpeed, EUnit.MILLIMETER_PER_SECOND);
                    Para[2] = new DPara($"{index} End Delay", cmd.Para[2], minTime, maxTime, EUnit.MILLISECOND);
                    Para[3] = new DPara($"{index} Wait", cmd.Para[3], minTime, maxTime, EUnit.MILLISECOND);
                    //Para[4] = new DPara($"{index} Early Cut Length", cmd.Para[4], 0, 25, EUnit.MILLIMETER);
                    Para[5] = new DPara($"{index} Start Gap", cmd.Para[5], 0, 5, EUnit.MILLIMETER);
                    Para[6] = new DPara($"{index} Start Length", cmd.Para[6], 0, 10, EUnit.MILLIMETER);
                    break;
                case ECmd.DOWN_SETUP:
                    Para[0] = new DPara($"{index} Disp Gap", cmd.Para[0], minDist, maxDist, EUnit.MILLIMETER);
                    Para[1] = new DPara($"{index} Dn Speed", cmd.Para[1] = Math.Max(minSpeed, cmd.Para[1]), minSpeed, maxSpeed, EUnit.MILLIMETER_PER_SECOND);
                    Para[2] = new DPara($"{index} Dn Wait", cmd.Para[2], minTime, maxTime, EUnit.MILLISECOND);
                    break;
                case ECmd.RET_SETUP:
                    Para[0] = new DPara($"{index} Ret Gap", cmd.Para[0], minDist, maxDist, EUnit.MILLIMETER);
                    Para[1] = new DPara($"{index} Ret Speed", cmd.Para[1] = Math.Max(minSpeed, cmd.Para[1]), minSpeed, maxSpeed, EUnit.MILLIMETER_PER_SECOND);
                    Para[2] = new DPara($"{index} Ret Wait", cmd.Para[2], minTime, maxTime, EUnit.MILLISECOND);
                    break;
                case ECmd.UP_SETUP:
                    Para[0] = new DPara($"{index} Up Gap", cmd.Para[0], minDist, maxDist, EUnit.MILLIMETER);
                    Para[1] = new DPara($"{index} Up Speed", cmd.Para[1] = Math.Max(minSpeed, cmd.Para[1]), minSpeed, maxSpeed, EUnit.MILLIMETER_PER_SECOND);
                    Para[2] = new DPara($"{index} Up Wait", cmd.Para[2], minTime, maxTime, EUnit.MILLISECOND);
                    break;
                case ECmd.CUT_TAIL_SETUP:
                    Para[0] = new DPara($"{index} Type", cmd.Para[0], 0, Enum.GetValues(typeof(ECutTailType)).Length, EUnit.NONE, 0, Enum.GetNames(typeof(ECutTailType)));
                    Para[1] = new DPara($"{index} Length", cmd.Para[1], 0, 25, EUnit.MILLIMETER);
                    Para[2] = new DPara($"{index} Height", cmd.Para[2], 0, 5, EUnit.MILLIMETER);
                    Para[3] = new DPara($"{index} Speed", cmd.Para[3] = Math.Max(minSpeed, cmd.Para[3]), minSpeed, maxSpeed, EUnit.MILLIMETER_PER_SECOND);
                    break;

                case ECmd.DYNAMIC_JET_SETUP:
                    Para[0] = new DPara($"{index} Pre-Disp", cmd.Para[0], 0, 2, EUnit.NONE, 0, Enum.GetNames(typeof(EDynamicDispMode)));
                    Para[1] = new DPara($"{index} AccelDist", cmd.Para[1], 0, 50, EUnit.MILLIMETER);
                    Para[2] = new DPara($"{index} Post-Disp", cmd.Para[2], 0, 2, EUnit.NONE, 0, Enum.GetNames(typeof(EDynamicDispMode)));
                    Para[3] = new DPara($"{index} Serpentine", cmd.Para[3], 0, 1, EUnit.NONE, 0, new string[] { "No", "Yes" });
                    break;

                case ECmd.CLUSTER_GAP_SETUP:
                    Para[0] = new DPara($"{index} Cluster Gap", cmd.Para[0], minDist, maxDist, EUnit.MILLIMETER);
                    break;
                case ECmd.PAT_ALIGN_SETUP:
                    Para[0] = new DPara($"{index} SettleTime", cmd.Para[0], 0, 2000, EUnit.MILLISECOND, 0);
                    Para[1] = new DPara($"{index} MultiSearch", cmd.Para[1], 0, 1, EUnit.NONE, 0, new string[] { "No", "Yes" });
                    break;
                case ECmd.HEIGHT_SETUP:
                    Para[0] = new DPara($"{index} SettleTime", cmd.Para[0], 0, 2000, EUnit.MILLISECOND, 0);
                    Para[1] = new DPara($"{index} Range ULimit", cmd.Para[1], 0, 5, EUnit.MILLIMETER);
                    break;
                case ECmd.NEEDLE_VAC_CLEAN:
                case ECmd.NEEDLE_FLUSH:
                case ECmd.NEEDLE_PURGE:
                    Para[0] = new DPara($"{index} Count", cmd.Para[0], 0, 10, EUnit.COUNT, 0);
                    Para[1] = new DPara($"{index} Per Unit", cmd.Para[1], 0, 50000, EUnit.COUNT, 0);
                    Para[3] = new DPara($"{index} DnWait", cmd.Para[3], 0, GProcessPara.NeedleVacClean.DownWait[0].Max, EUnit.MILLISECOND, 0);
                    Para[4] = new DPara($"{index} DispTime", cmd.Para[4], 0, GProcessPara.NeedleVacClean.DispTime[0].Max, EUnit.MILLISECOND, 0);
                    Para[5] = new DPara($"{index} VacTime", cmd.Para[5], 0, GProcessPara.NeedleVacClean.VacTime[0].Max, EUnit.MILLISECOND, 0);
                    Para[6] = new DPara($"{index} PostVacTime", cmd.Para[6], 0, GProcessPara.NeedleVacClean.PostVacTime[0].Max, EUnit.MILLISECOND, 0);
                    Para[7] = new DPara($"{index} PostWait", cmd.Para[7], 0, GProcessPara.NeedleVacClean.PostWait[0].Max, EUnit.MILLISECOND, 0);
                    break;
                case ECmd.NEEDLE_SPRAY_CLEAN:
                    Para[0] = new DPara($"{index} Count", cmd.Para[0], 0, 10, EUnit.COUNT, 0);
                    Para[1] = new DPara($"{index} Per Unit", cmd.Para[1], 0, 50000, EUnit.COUNT, 0);
                    Para[3] = new DPara($"{index} DnWait", cmd.Para[3], 0, GProcessPara.NeedleSpray.DownWait[0].Max, EUnit.MILLISECOND, 0);
                    Para[5] = new DPara($"{index} SprayTime", cmd.Para[5], 0, GProcessPara.NeedleSpray.SprayTime[0].Max, EUnit.MILLISECOND, 0);
                    Para[7] = new DPara($"{index} PostWait", cmd.Para[7], 0, GProcessPara.NeedleSpray.PostWait[0].Max, EUnit.MILLISECOND, 0);
                    break;
                case ECmd.PURGE_STAGE:
                case ECmd.NEEDLE_AB_CLEAN:
                    Para[0] = new DPara($"{index} Count", cmd.Para[0], 0, 10, EUnit.COUNT, 0);
                    Para[1] = new DPara($"{index} Per Unit", cmd.Para[1], 0, 5000, EUnit.COUNT, 0);
                    break;
                case ECmd.LINE_SPEED:
                    Para[0] = new DPara($"{index} Line Speed", cmd.Para[0] = Math.Max(minSpeed, cmd.Para[0]), minSpeed, maxSpeed, EUnit.MILLIMETER_PER_SECOND);
                    break;
                case ECmd.LINE_GAP_ADJUST:
                    Para[0] = new DPara($"{index} Rel Gap", cmd.Para[0], -5, 5, EUnit.MILLIMETER, 3);
                    Para[1] = new DPara($"{index} Rel Dist", cmd.Para[1], 0, 25, EUnit.MILLIMETER, 3);
                    break;
                case ECmd.NOZZLE_INSPECTION:
                    Para[0] = new DPara($"{index} Per Unit", cmd.Para[0], 0, 50000, EUnit.COUNT, 0);
                    break;
                case ECmd.GOTO_POSITION:
                    var fieldinfo = typeof(GSetupPara.Maintenance).GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
                    Para[0] = new DPara($"{index} Position", cmd.Para[0], 0, fieldinfo.Length, EUnit.NONE, 0, fieldinfo.Select(x => x.Name).ToArray());
                    break;
            }

            lblPara0.Visible = Para[0].Name.Length > 0;
            lblParaDesc0.Visible = Para[0].Name.Length > 0;
            lblPara1.Visible = Para[1].Name.Length > 0;
            lblParaDesc1.Visible = Para[1].Name.Length > 0;
            lblPara2.Visible = Para[2].Name.Length > 0;
            lblParaDesc2.Visible = Para[2].Name.Length > 0;
            lblPara3.Visible = Para[3].Name.Length > 0;
            lblParaDesc3.Visible = Para[3].Name.Length > 0;
            lblPara4.Visible = Para[4].Name.Length > 0;
            lblParaDesc4.Visible = Para[4].Name.Length > 0;
            lblPara5.Visible = Para[5].Name.Length > 0;
            lblParaDesc5.Visible = Para[5].Name.Length > 0;
            lblPara6.Visible = Para[6].Name.Length > 0;
            lblParaDesc6.Visible = Para[6].Name.Length > 0;
            lblPara7.Visible = Para[7].Name.Length > 0;
            lblParaDesc7.Visible = Para[7].Name.Length > 0;
            lblPara8.Visible = Para[8].Name.Length > 0;
            lblParaDesc8.Visible = Para[8].Name.Length > 0;
            lblPara9.Visible = Para[9].Name.Length > 0;
            lblParaDesc9.Visible = Para[9].Name.Length > 0;

            lblParaDesc0.Text = Para[0].Name.Replace(index, "");
            lblParaDesc1.Text = Para[1].Name.Replace(index, "");
            lblParaDesc2.Text = Para[2].Name.Replace(index, "");
            lblParaDesc3.Text = Para[3].Name.Replace(index, "");
            lblParaDesc4.Text = Para[4].Name.Replace(index, "");
            lblParaDesc5.Text = Para[5].Name.Replace(index, "");
            lblParaDesc6.Text = Para[6].Name.Replace(index, "");
            lblParaDesc7.Text = Para[7].Name.Replace(index, "");
            lblParaDesc8.Text = Para[8].Name.Replace(index, "");
            lblParaDesc9.Text = Para[9].Name.Replace(index, "");
        }

        private void frmRecipeCommonPara_Load(object sender, EventArgs e)
        {
            UpdateDisplay();
            GControl.LogForm(this);
        }

        private void UpdateDisplay()
        {
            //GControl.UpdateFormControl(this);

            //lblPara0.Visible = Para[0].Name.Length > 0;
            //lblParaDesc0.Visible = Para[0].Name.Length > 0;
            //lblPara1.Visible = Para[1].Name.Length > 0;
            //lblParaDesc1.Visible = Para[1].Name.Length > 0;
            //lblPara2.Visible = Para[2].Name.Length > 0;
            //lblParaDesc2.Visible = Para[2].Name.Length > 0;
            //lblPara3.Visible = Para[3].Name.Length > 0;
            //lblParaDesc3.Visible = Para[3].Name.Length > 0;
            //lblPara4.Visible = Para[4].Name.Length > 0;
            //lblParaDesc4.Visible = Para[4].Name.Length > 0;
            //lblPara5.Visible = Para[5].Name.Length > 0;
            //lblParaDesc5.Visible = Para[5].Name.Length > 0;
            //lblPara6.Visible = Para[6].Name.Length > 0;
            //lblParaDesc6.Visible = Para[6].Name.Length > 0;
            //lblPara7.Visible = Para[7].Name.Length > 0;
            //lblParaDesc7.Visible = Para[7].Name.Length > 0;
            //lblPara8.Visible = Para[8].Name.Length > 0;
            //lblParaDesc8.Visible = Para[8].Name.Length > 0;
            //lblPara9.Visible = Para[9].Name.Length > 0;
            //lblParaDesc9.Visible = Para[9].Name.Length > 0;

            lblPara0.UpdatePara(Para[0]);
            lblPara1.UpdatePara(Para[1]);
            lblPara2.UpdatePara(Para[2]);
            lblPara3.UpdatePara(Para[3]);
            lblPara4.UpdatePara(Para[4]);
            lblPara5.UpdatePara(Para[5]);
            lblPara6.UpdatePara(Para[6]);
            lblPara7.UpdatePara(Para[7]);
            lblPara8.UpdatePara(Para[8]);
            lblPara9.UpdatePara(Para[9]);
        }

        private void lblPara0_Click(object sender, EventArgs e)
        {
            if (GLog.SetPara(ref Para[0])) Tcmd.Para[0] = Para[0].Value;
            UpdateDisplay();
        }
        private void lblPara1_Click(object sender, EventArgs e)
        {
            if (GLog.SetPara(ref Para[1])) Tcmd.Para[1] = Para[1].Value;
            UpdateDisplay();
        }
        private void lblPara2_Click(object sender, EventArgs e)
        {
            if (GLog.SetPara(ref Para[2])) Tcmd.Para[2] = Para[2].Value;
            UpdateDisplay();
        }
        private void lblPara3_Click(object sender, EventArgs e)
        {
            if (GLog.SetPara(ref Para[3])) Tcmd.Para[3] = Para[3].Value;
            UpdateDisplay();
        }
        private void lblPara4_Click(object sender, EventArgs e)
        {
            if (GLog.SetPara(ref Para[4])) Tcmd.Para[4] = Para[4].Value;
            UpdateDisplay();
        }
        private void lblPara5_Click(object sender, EventArgs e)
        {
            if (GLog.SetPara(ref Para[5])) Tcmd.Para[5] = Para[5].Value;
            UpdateDisplay();
        }
        private void lblPara6_Click(object sender, EventArgs e)
        {
            if (GLog.SetPara(ref Para[6])) Tcmd.Para[6] = Para[6].Value;
            UpdateDisplay();
        }
        private void lblPara7_Click(object sender, EventArgs e)
        {
            if (GLog.SetPara(ref Para[7])) Tcmd.Para[7] = Para[7].Value;
            UpdateDisplay();
        }
        private void lblPara8_Click(object sender, EventArgs e)
        {
            if (GLog.SetPara(ref Para[8])) Tcmd.Para[8] = Para[8].Value;
            UpdateDisplay();
        }
        private void lblPara9_Click(object sender, EventArgs e)
        {
            if (GLog.SetPara(ref Para[9])) Tcmd.Para[9] = Para[9].Value;
            UpdateDisplay();
        }
    }
}
