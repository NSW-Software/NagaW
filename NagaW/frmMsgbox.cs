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
    public enum MsgBoxBtns
    {
        OK,
        OKCancel,
        YesNo,
        YesNoCancel,
        RetryCancel,
        AbortRetryIgnore,
        OkAbortRetryIgnore,

        AcceptIgnoreAbort,
        OkRetryAbort,
    }

    public partial class frmMsgbox : Form
    {
        public frmMsgbox()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            TopMost = TopLevel = true;
            ControlBox = false;
            lblMsg.Dock = DockStyle.Fill;
            this.Size = new Size(380, 300);

            Text = "Message";
        }
        public frmMsgbox(string msg, MsgBoxBtns btns) : this()
        {
            lblMsg.Text = msg;
            panel1.Controls.OfType<Button>().ToList().ForEach(x => 
            {
                x.Visible = false;
                x.TabStop = false;
            });

            //Accept = OK share OK button and return DialogResult.OK
            //Skip = Ignore share Skip button and return DialogResult.Ignore
            switch (btns)
            {
                case MsgBoxBtns.OK:
                    btnOK.Visible = true;
                    AcceptButton = btnOK;
                    break;
                case MsgBoxBtns.OKCancel:
                    btnOK.Visible = btnCancel.Visible = true;
                    AcceptButton = btnOK;
                    break;
                case MsgBoxBtns.YesNo:
                    btnYes.Visible = btnNo.Visible = true;
                    AcceptButton = btnYes;
                    break;
                case MsgBoxBtns.YesNoCancel:
                    btnYes.Visible = btnNo.Visible = btnCancel.Visible = true;
                    AcceptButton = btnYes;
                    break;
                case MsgBoxBtns.RetryCancel:
                    btnRetry.Visible = btnCancel.Visible = true;
                    AcceptButton = btnRetry;
                    break;
                case MsgBoxBtns.AbortRetryIgnore:
                    btnRetry.Visible = btnIgnore.Visible = btnAbort.Visible = true;
                    AcceptButton = btnRetry;
                    break;
                case MsgBoxBtns.OkAbortRetryIgnore:
                    btnOK.Visible = true;
                    btnOK.Text = "Accept";
                    btnIgnore.Text = "Skip";
                    goto case MsgBoxBtns.AbortRetryIgnore;
                case MsgBoxBtns.AcceptIgnoreAbort:
                    btnAbort.Visible = btnOK.Visible = btnIgnore.Visible = true;
                    btnIgnore.Text = "Skip";
                    btnOK.Text = "Accept";
                    break;
                case MsgBoxBtns.OkRetryAbort:
                    btnAbort.Visible = btnOK.Visible = btnRetry.Visible = true;
                    btnOK.Text = "Accept";
                    break;
            }
        }
        private void frmMsgbox_Load(object sender, EventArgs e)
        {
            //Location = new Point(Location.X, 0);
            //When frmMain is not created, prompt in default location Primary Screen Center

            if (Application.OpenForms[0].Name.Contains("frmMain")) Location = new Point(Location.X, Application.OpenForms[0].Top);
            BringToFront();
            GControl.LogForm(this);
            TopMost = true;
            TopLevel = true;
            timer1.Enabled = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        private void btnRetry_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Retry;
            Close();
        }
        private void btnYes_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }
        private void btnNo_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }
        private void btnAbort_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            Close();
        }
        private void btnIgnore_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Ignore;
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            BringToFront();
            TopMost = true;
            TopLevel = true;
        }
    }
}
