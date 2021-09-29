using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace NagaW
{
    public partial class frmAdmin : Form
    {
        public frmAdmin()
        {
            InitializeComponent();
        }

        private void frmUserControl_Load(object sender, EventArgs e)
        {
            listBox1.DataSource = TFUser.UserList;
            GControl.ConvertTabCtrlToFLP(tabControl1);
            GControl.LogForm(this);

        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            TFUser.UserList.ResetBindings();
        }

        private void frmAdmin_FormClosing(object sender, FormClosingEventArgs e)
        {
            listBox1.DataSource = null;
            propertyGrid1.SelectedObject = null;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            TFUser.Add();
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is null) return;

            int idx = listBox1.SelectedIndex;
            if (MsgBox.ShowDialog($"Remove User{TFUser.UserList[idx].ToStringForDisplay()}", MsgBoxBtns.OKCancel) != DialogResult.OK) return;

            TFUser.Remove(idx);
            if (TFUser.UserList.Count is 0) propertyGrid1.SelectedObject = null;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (TFUser.UserList.Count is 0) return;
                if (listBox1.SelectedItem is null) return;
                if (listBox1.SelectedIndex < 0) propertyGrid1.SelectedObject = null;
                else
                {
                    var user = TFUser.UserList[Math.Max(0, listBox1.SelectedIndex)];
                    if (propertyGrid1.SelectedObject != null) propertyGrid1.SelectedObject = null;
                    propertyGrid1.SelectedObject = user;

                }
            }
            catch { }

        }
    }
}
