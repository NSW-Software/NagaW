using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NagaW
{
    public partial class frmPrompt : Form
    {
        public static TEZMCAux.TOutput TLRed { get => GMotDef.Out32; }
        public static TEZMCAux.TOutput TLYellow { get => GMotDef.Out33; }
        public static TEZMCAux.TOutput TLGreen { get => GMotDef.Out34; }
        public static TEZMCAux.TOutput TLBuzzer { get => GMotDef.Out35; }

        public frmPrompt()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MinimizeBox = MaximizeBox = false;
            TopLevel = TopMost = true;
            GControl.EditForm(this);
            ShowIcon = true;
        }
        public frmPrompt(Icon icon, Color clr, string title, string desc) : this()
        {
            Icon = icon;
            Text = title;
            BackColor = clr;
            rtbx.Text = desc;
        }

        private void frmPrompt_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            //When frmMain is not created, prompt in default location Primary Screen Center

            var screenbound = Screen.AllScreens[0].Bounds;
            if (Application.OpenForms[0].Name.Contains("frmMain")) Location = new Point(screenbound.Right/*Application.OpenForms[0].Right*/ - Width, screenbound.Bottom/* Application.OpenForms[0].Bottom*/ - Height);
            TLRed.Status = true;
            TLBuzzer.Status = true;
            GControl.LogForm(this);
        }

        private void frmPrompt_Shown(object sender, EventArgs e)
        {
            SetTopLevel(true);
            TopMost = true;
            BringToFront();

            rtbx.Text += $"\r\n{DateTime.Now}";
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            TLRed.Status = false;
            TLBuzzer.Status = false;
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //this.TopLevel = true;
            //this.TopMost = true;
            //BringToFront();
        }
    }
}
