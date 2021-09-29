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
    public partial class frmLogin : Form
    {
        public TEUser LoginUser;

        public frmLogin()
        {
            InitializeComponent();
            WindowState = FormWindowState.Normal;
            ControlBox = false;
            GControl.EditForm(this);
            Text = "Login";
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            cbxLevel.DataSource = Enum.GetValues(typeof(Elevel));
            GControl.LogForm(this);
        }
        private void btnSignIn_Click(object sender, EventArgs e)
        {
            LoginUser = new TEUser((Elevel)cbxLevel.SelectedItem, tboxName.Text, tboxPassword.Text, false);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnExitProgram_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
