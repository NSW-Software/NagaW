using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NagaW
{
    public partial class frmProcessing : Form
    {
        Action Act;
        Action CancelToken;
        DateTime StartTime;
        public frmProcessing()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            ControlBox = false;
            GControl.EditForm(this);
            Text = "Processing";
        }

        public frmProcessing(Action action, Action cancelaction, string msg) : this()
        {
            Act = action;
            CancelToken = cancelaction;
            btnCancel.Visible = !(cancelaction is null);
            lblMsg.Text = msg;
            StartTime = DateTime.Now;
            TopLevel = TopMost = true;
            timer1.Enabled = true;
        }

        private async void frmProcessing_Shown(object sender, EventArgs e)
        {
            GControl.LogForm(this);
            await Task.Run(() => Act.Invoke());
            Close();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = nameof(StartTime) + "    " + StartTime.ToLongTimeString() + "\r\n"
                + nameof(Stopwatch.Elapsed) + "    " + (DateTime.Now - StartTime).ToString("hh\\:mm\\:ss");
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelToken.Invoke();
        }

        private void frmProcessing_Load(object sender, EventArgs e)
        {
            //Location = new Point(Location.X, 0);
            //When frmMain is not created, prompt in default location Primary Screen Center
            if (Application.OpenForms[0].Name.Contains("frmMain"))
                Location = new Point(Location.X, Application.OpenForms[0].Top);
        }
    }
}
