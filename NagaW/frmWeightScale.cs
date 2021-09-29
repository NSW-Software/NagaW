using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace NagaW
{
    public partial class frmWeightScale : Form
    {
        public frmWeightScale()
        {
            InitializeComponent();

           cbxComport.DataSource = Enum.GetValues(typeof(ECOM));
            cbxComport.SelectedItem = GSystemCfg.Weight.Weights[0].Comport;
        }

        private void frmWeightScale_Load(object sender, EventArgs e)
        {
            Text = "Weight Scale";

            UpdateControl();
        }

        private void UpdateControl()
        {
            btnConnect.Text = TEWeigh.IsOpen ? "Disconnect" : "Connect";
            cbxComport.Enabled = !TEWeigh.IsOpen;

            btnPool.Text = bPool ? "Pool Stop" : "Pool Start";
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (TFWeightScale.IsOpen)
            {
                TFWeightScale.Close();
            }
            else
            {
                TFWeightScale.Open(GSystemCfg.Weight.Weights[0].Comport.ToString());
            }
            UpdateControl();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnTare_Click(object sender, EventArgs e)
        {
            bPool = false;
            TFWeightScale.Tare();
        }
        private void btnZero_Click(object sender, EventArgs e)
        {
            bPool = false;
            TFWeightScale.Zero();
        }
        private void btnReadStable_Click(object sender, EventArgs e)
        {
            bPool = false;
            try
            {
                double gValue = 0;
                if (!TFWeightScale.ReadStable(ref gValue))
                    lblValue.Text = "Err";
                else
                    lblValue.Text = gValue.ToString("f6") + " g";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        bool bPool = false;
        private void btnPool_Click(object sender, EventArgs e)
        {
            bPool = !bPool;
            UpdateControl();
        }

        private void cbxComport_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GSystemCfg.Weight.Weights[0].Comport = (ECOM)cbxComport.SelectedItem;
        }

        private void tmrPool_Tick(object sender, EventArgs e)
        {
            try
            {
                if (bPool)
                {
                    double gValue = 0;
                    if (!TFWeightScale.ReadImme(ref gValue))
                    {
                        bPool = false;
                        lblValue.Text = "Err";
                    }
                    else
                    lblValue.Text = gValue.ToString("f6") + " g";
                }
            }
            catch (Exception ex)
            {
                bPool = false;
                MessageBox.Show(ex.Message.ToString());
            }
            UpdateControl();
        }
    }
}
