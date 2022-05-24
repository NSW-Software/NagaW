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
    public partial class frmPressureMaster : Form
    {
        public frmPressureMaster()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;

            Text = "Pressure Monitoring";
        }

        int idx = TFGantry.GantrySelect.Index;
        PressureSetup PressureSetup;

        private void UpdateDisplay()
        {
            GControl.UpdateFormControl(this);

            PressureSetup = GRecipes.PressureSetups[idx];

            //btnGLeft.BackColor = idx is 0 ? Color.Lime : SystemColors.Control;
            //btnGRight.BackColor = idx is 1 ? Color.Lime : SystemColors.Control;

            lblFPress.UpdatePara(PressureSetup.FPress);
            lblPPress.UpdatePara(PressureSetup.PPress);

            lblFPressMin.UpdatePara(PressureSetup.FPress_NegLmt);
            lblFPressMax.UpdatePara(PressureSetup.FPress_PosLmt);
            lblPPressMin.UpdatePara(PressureSetup.PPress_NegLmt);
            lblPPressMax.UpdatePara(PressureSetup.PPress_PosLmt);

            cbxMaster.Checked = PressureSetup.Master;
            cbxMonitoring.Checked = PressureSetup.isMonitoring;

            lblInterval.UpdatePara(PressureSetup.Interval);
            lblIntervalMinutes.Text = $"({PressureSetup.Interval.Value / 60}min {PressureSetup.Interval.Value % 60}sec)";
        }
        private void frmPressureMaster_Load(object sender, EventArgs e)
        {
            GControl.LogForm(this);
            UpdateDisplay();
        }

        private void lblFPress_Click(object sender, EventArgs e)
        {
            var para = PressureSetup.FPress;
            GLog.SetPara(ref para);
            UpdateDisplay();
        }

        private void lblPPress_Click(object sender, EventArgs e)
        {
            var para = PressureSetup.PPress;
            GLog.SetPara(ref para);
            UpdateDisplay();
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            PressureSetup.Master = !PressureSetup.Master;
            UpdateDisplay();
        }


        private void btnGLeft_Click(object sender, EventArgs e)
        {
            idx = 0;
            UpdateDisplay();
        }
        private void btnGRight_Click(object sender, EventArgs e)
        {
            idx = 1;
            UpdateDisplay();
        }

        private void lblFPressMin_Click(object sender, EventArgs e)
        {
            var para = PressureSetup.FPress_NegLmt;
            GLog.SetPara(ref para);
            UpdateDisplay();
        }

        private void lblFPressMax_Click(object sender, EventArgs e)
        {
            var para = PressureSetup.FPress_PosLmt;
            GLog.SetPara(ref para);
            UpdateDisplay();
        }

        private void lblPPressMin_Click(object sender, EventArgs e)
        {
            var para = PressureSetup.PPress_NegLmt;
            GLog.SetPara(ref para);
            UpdateDisplay();
        }

        private void lblPPressMax_Click(object sender, EventArgs e)
        {
            var para = PressureSetup.PPress_PosLmt;
            GLog.SetPara(ref para);
            UpdateDisplay();
        }

        private void cbxMonitoring_Click(object sender, EventArgs e)
        {
            PressureSetup.isMonitoring = !PressureSetup.isMonitoring;
            UpdateDisplay();
        }

        private void lblInterval_Click(object sender, EventArgs e)
        {
            var para = PressureSetup.Interval;
            GLog.SetPara(ref para);
            UpdateDisplay();
        }
    }
}
