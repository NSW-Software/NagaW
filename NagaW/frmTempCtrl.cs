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
    public partial class frmTempCtrl : Form
    {
        TETempCtrl TempCtrl;

        List<Label> Lblpv_list = new List<Label>();
        List<Button> BtnRunStop = new List<Button>();
        List<Button> BtnEnable = new List<Button>();

        Temp_Setup[] Temp_Setups = null;

        public frmTempCtrl()
        {
            InitializeComponent();
            Text = "Temperature Control";
        }

        private void frmTempCtrl_Load(object sender, EventArgs e)
        {
            TempCtrl = TFTempCtrl.TempCtrl;

            Temp_Setups = GRecipes.Temp_Setups;

            timer1.Enabled = true;
            timer1.Interval = 1000;

            CreateTempCtrlUI();
            UpdateDisplay();
        }

        private void CreateTempCtrlUI()
        {
            Panel panel = new Panel() { Name = "TempPanel", Dock = DockStyle.Fill };

            var shortsize = new Size(60, 28);
            var starndarsize = new Size(100, 28);
            var margin = new Padding(3);
            #region Title
            Label empty = new Label() { Text = "", AutoSize = false, Size = starndarsize, Margin = margin };
            Label pv = new Label() { Text = "PV", AutoSize = false, Size = starndarsize, TextAlign = ContentAlignment.MiddleLeft, Margin = margin };
            Label sv = new Label() { Text = "SV", AutoSize = false, Size = shortsize, TextAlign = ContentAlignment.MiddleLeft, Margin = margin };

            Label llmt = new Label() { Text = "NegLimit", AutoSize = false, Size = shortsize, TextAlign = ContentAlignment.MiddleLeft, Margin = margin };
            Label ulmt = new Label() { Text = "PosLimit", AutoSize = false, Size = shortsize, TextAlign = ContentAlignment.MiddleLeft, Margin = margin };
            Label run = new Label() { Text = "Run", AutoSize = false, Size = starndarsize, TextAlign = ContentAlignment.MiddleCenter, Margin = margin };

            FlowLayoutPanel flpp = new FlowLayoutPanel() { Dock = DockStyle.Top, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink };

            flpp.Controls.AddRange(new Control[] { empty, sv, pv, new Label() { Size = starndarsize, Margin = margin }, llmt, ulmt, run });

            panel.Controls.Add(flpp);
            flpp.BringToFront();
            #endregion

            for (int i = 0; i < GSystemCfg.Temperature.ChannelCount; i++)
            {
                var chn = GSystemCfg.Temperature.Temp.Channels[i];

                Label lblname = new Label() { Name = $"lblname{i}", Text = chn.Name, AutoSize = false, Size = starndarsize, BorderStyle = BorderStyle.Fixed3D, TextAlign = ContentAlignment.MiddleLeft, Margin = margin };

                Label lblsv = new Label() { Name = $"lblsv{i}", Text = "", AutoSize = false, Size = shortsize, BorderStyle = BorderStyle.Fixed3D, BackColor = Color.White, Tag = i, Margin = margin };
                Label lblpv = new Label() { Name = $"lblpv{i}", Text = "", AutoSize = false, Size = starndarsize, BorderStyle = BorderStyle.Fixed3D, TextAlign = ContentAlignment.MiddleLeft, Margin = margin };

                Button btnrun = new Button() { Name = $"btnrun{i}", Text = "Run", Size = starndarsize, Tag = i, Margin = margin };
                Button btnEnable = new Button() { Name = $"btnEnable{i}", Text = "Enable", Size = starndarsize, Tag = i, Margin = margin };

                Label lblposlmt = new Label() { Name = $"lblll{i}", Text = "", AutoSize = false, Size = shortsize, BorderStyle = BorderStyle.Fixed3D, BackColor = Color.White, TextAlign = ContentAlignment.MiddleLeft, Margin = margin };
                Label lblneglmt = new Label() { Name = $"lblul{i}", Text = "", AutoSize = false, Size = shortsize, BorderStyle = BorderStyle.Fixed3D, BackColor = Color.White, TextAlign = ContentAlignment.MiddleLeft, Margin = margin };

                lblsv.UpdatePara(Temp_Setups[i].SetValue);
                lblposlmt.UpdatePara(Temp_Setups[i].PosLimit);
                lblneglmt.UpdatePara(Temp_Setups[i].NegLimit);

                Lblpv_list.Add(lblpv);
                BtnRunStop.Add(btnrun);
                BtnEnable.Add(btnEnable);

                FlowLayoutPanel flp = new FlowLayoutPanel() { Dock = DockStyle.Top, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink };
                flp.Controls.AddRange(new Control[] { lblname, lblsv, lblpv, btnrun, lblneglmt, lblposlmt, btnEnable });

                panel.Controls.Add(flp);
                flp.BringToFront();

                btnrun.Click += (a, b) =>
                {
                    var idx = (int)(a as Button).Tag + 1;

                    if (!GSystemCfg.Temperature.Temp.Channels[idx - 1].Enable)
                    {
                        MessageBox.Show("Enable controller before Run it");
                        return;
                    }

                    if (TFTempCtrl.TempCtrl.IsRunning(idx)) TFTempCtrl.TempCtrl.Stop(idx);
                    else
                    {
                        TEZMCAux.LastMoveTime = DateTime.Now;
                        TFTempCtrl.TempCtrl.Run(idx);
                    }
                };

                btnEnable.Click += (a, b) =>
                {
                    var idx = (int)(a as Button).Tag;

                    GSystemCfg.Temperature.Temp.Channels[idx].Enable = !GSystemCfg.Temperature.Temp.Channels[idx].Enable;
                    var enable = GSystemCfg.Temperature.Temp.Channels[idx].Enable;
                    if (!enable)
                        if (TFTempCtrl.TempCtrl.IsRunning(idx + 1)) TFTempCtrl.TempCtrl.Stop(idx + 1);

                    btnEnable.Text = enable ? "Enabled" : "Disabled";
                    btnEnable.BackColor = enable ? SystemColors.Control : Color.Gray;
                };

                lblsv.Click += (a, b) =>
                {
                    var idx = (int)lblsv.Tag;
                    var para = Temp_Setups[idx].SetValue;
                    GLog.SetPara(ref para);
                    TempCtrl.SetValue(GSystemCfg.Temperature.Temp.Channels[idx].Address, para.Value);
                    lblsv.UpdatePara(para);
                };

                lblposlmt.Click += (a, b) =>
                {
                    var idx = (int)lblsv.Tag;
                    var para = Temp_Setups[idx].PosLimit;
                    GLog.SetPara(ref para);
                    lblposlmt.UpdatePara(para);
                    
                };

                lblneglmt.Click += (a, b) =>
                {
                    var idx = (int)lblsv.Tag;
                    var para = Temp_Setups[idx].NegLimit;
                    GLog.SetPara(ref para);
                    lblneglmt.UpdatePara(para);
                };
            }

            this.Controls.Add(panel);
            panel.BringToFront();
        }

        private void UpdateDisplay()
        {
            cbxTempPrompt.Checked = GProcessPara.Temp.CheckTempBeforeRun;
            lblAwaitErrorTime.UpdatePara(GProcessPara.Temp.AwaitErrorTime);
            lblAwaitTimeMin.Text = $"{GProcessPara.Temp.AwaitErrorTime.Value / 60}min {GProcessPara.Temp.AwaitErrorTime.Value % 60}sec";
            lblTimeToIdle.UpdatePara(GProcessPara.Temp.IdleStopTime);
            lblTimetoidleMinute.Text = $"{GProcessPara.Temp.IdleStopTime.Value / 60}min {GProcessPara.Temp.IdleStopTime.Value % 60}sec";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for(int i = 0; i < GSystemCfg.Temperature.ChannelCount; i++)
            {
                string pv = "NoConnection";

                var enable = GSystemCfg.Temperature.Temp.Channels[i].Enable;
                BtnEnable[i].Text = enable ? "Enabled" : "Disabled";
                BtnEnable[i].BackColor = enable ? SystemColors.Control : Color.Gray;
                BtnRunStop[i].BackColor = Color.Red;
                BtnRunStop[i].Text = "Stopped";

                if (enable)
                {
                    var addr = GSystemCfg.Temperature.Temp.Channels[i].Address;
                    GSystemCfg.Temperature.Temp.Channels[i].Enable = TempCtrl.Read_PresentValue(addr, out int value);

                    pv = new IPara(value, EUnit.DEGREE_CELSIUS).ToStringForDisplay();

                    bool state = TFTempCtrl.TempCtrl.IsRunning(addr);
                    if (state) BtnRunStop[i].Text = "Running";
                    if (state) BtnRunStop[i].BackColor = Color.Lime;
                }

                Lblpv_list[i].Text = pv;
            }
        }

        private void frmTempCtrl_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
            timer1.Stop();
        }

        private void cbxTempPrompt_Click(object sender, EventArgs e)
        {
            GProcessPara.Temp.CheckTempBeforeRun = !GProcessPara.Temp.CheckTempBeforeRun;
            UpdateDisplay();
        }

        private void lblAwaitErrorTime_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Temp.AwaitErrorTime);
            UpdateDisplay();
        }

        private void lblTimeToIdle_Click(object sender, EventArgs e)
        {
            GLog.SetPara(ref GProcessPara.Temp.IdleStopTime);
            UpdateDisplay();
        }
    }
}
