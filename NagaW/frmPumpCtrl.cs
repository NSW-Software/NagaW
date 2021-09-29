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
    public partial class frmPumpCtrl : Form
    {
        int index = 0;
        public frmPumpCtrl()
        {
            InitializeComponent();
        }
        public frmPumpCtrl(int index)
        {
            InitializeComponent();
            this.index = index;
        }

        private void frmPumpCtrl_Load(object sender, EventArgs e)
        {
            this.Text = "Pump";

            List<Form> frms = new List<Form>();

            FormClosing += (a, b) => frms.ForEach(f => f.Close());

            int i = 0;
            GSystemCfg.Pump.Pumps.ToList().ForEach(x =>
            {
                if (i >= tabControl1.TabPages.Count) return;
                var tp = tabControl1.TabPages[i];

                void Showform(Form frm)
                {
                    frm.FormBorderStyle = FormBorderStyle.None;
                    frm.Dock = DockStyle.Fill;
                    frm.TopLevel = false;
                    frm.TopMost = false;
                    frm.BackColor = i == 0 ? GSystemCfg.Display.LeftColor : GSystemCfg.Display.RightColor;
                    tp.Controls.Clear();
                    tp.Controls.Add(frm);
                    frm.Show();

                    //this.ClientSize =  frm.Size + new Size((tabControl1.Padding.X + tp.Padding.Left) * 2, (tabControl1.Padding.Y + tp.Padding.Top) * 2 + 40);
                    Size size = frm.Size + new Size((tabControl1.Padding.X + tp.Padding.Left) * 2, (tabControl1.Padding.Y + tp.Padding.Top) * 2 + 40);
                    this.ClientSize = new Size(Math.Max(this.ClientSize.Width, size.Width), Math.Max(this.ClientSize.Height, size.Height));
                }
                tp.Text = $"DispCtrl [" + (i == 0 ? "Left" : "Right") + $"] {x.PumpType}";

                switch (x.PumpType)
                {
                    case EPumpType.None:
                    default:
                        {
                            break;
                        }
                    case EPumpType.PNEUMATIC_JET:
                        {
                            Showform(new frmPumpSetup_PneumaticJet(GRecipes.PneumaticJet_Setups[i], GMotDef.Outputs[(int)x.FPressDO], GMotDef.Outputs[(int)x.PPressDO]
                                , TFPressCtrl.FPress[i], TFPressCtrl.FPress[i + 2]));
                            break;
                        }
                    case EPumpType.VERMES_3280:
                        {
                            Showform(new frmPumpSetup_Vermes32xx(GRecipes.Vermes_Setups[i], TFPump.Vermes_Pump[i], 
                                TFPressCtrl.FPress[i], GMotDef.Outputs[(int)x.DispDO]
                                , GMotDef.Inputs[(int)x.DispDI], GMotDef.Outputs[(int)x.FPressDO]));
                            break;
                        }
                    case EPumpType.SP:
                    case EPumpType.SPLite:
                    case EPumpType.TP:
                        {
                            Showform(new frmPumpSetup_SynchroPulse(i, x.PumpType, GRecipes.SP_Setups[i], GMotDef.Outputs[(int)x.FPressDO]
                                , GMotDef.Outputs[(int)x.PPressDO], GMotDef.Outputs[(int)x.VacDO]
                                , TFPressCtrl.FPress[i], TFPressCtrl.FPress[i + 2]));
                            break;
                        }
                    case EPumpType.HM:
                        {
                            Showform(new frmPumpSetup_HM(GRecipes.HM_Setups[i], GMotDef.Outputs[(int)x.FPressDO], GMotDef.Outputs[(int)x.VacDO], TFPressCtrl.FPress[i]));
                        }
                        break;
                }
                i++;
            });

            tabControl1.SelectedIndex = index;
        }
    }
}
