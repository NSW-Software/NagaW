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
    public partial class frmRecipeCmdEditor : Form
    {
        public frmRecipeCmdEditor()
        {
            InitializeComponent();
            Text = "Cmd Editor";
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void frmRecipeCmdEditor_Load(object sender, EventArgs e)
        {
            var Cmds = GRecipes.CmdsDictionary;
            lboxCmdSelected.Items.AddRange(Cmds.Select(x => x.ToString()).ToArray());
            lboxCmdDic.Items.AddRange(Enum.GetValues(typeof(ECmd)).OfType<ECmd>().Where(x => !Cmds.Contains(x)).Select(x => x.ToString()).ToArray());
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            lblDicCmd.Text = $"Dictionary ({lboxCmdDic.Items.Count})";
            lblSelectedCmd.Text = $"Selected ({lboxCmdSelected.Items.Count})";
        }
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            lboxCmdDic.Items.Clear();
            lboxCmdSelected.Items.Clear();
            lboxCmdSelected.Items.AddRange(Enum.GetNames(typeof(ECmd)));
            UpdateDisplay();
        }
        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            lboxCmdSelected.Items.Clear();
            lboxCmdDic.Items.Clear();
            lboxCmdDic.Items.AddRange(Enum.GetNames(typeof(ECmd)));
            UpdateDisplay();
        }

        private void btnDirToSel_Click(object sender, EventArgs e)
        {
            int idx = lboxCmdDic.SelectedIndex;
            if (idx < 0) return;
            string cmd = (string)lboxCmdDic.Items[idx];
            lboxCmdDic.Items.Remove(cmd);
            lboxCmdSelected.Items.Add(cmd);

            lboxCmdDic.SelectedIndex = Math.Min(idx, lboxCmdDic.Items.Count - 1);
            UpdateDisplay();
        }

        private void btnSelToDir_Click(object sender, EventArgs e)
        {
            int idx = lboxCmdSelected.SelectedIndex;
            if (idx < 0) return;
            string cmd = (string)lboxCmdSelected.Items[idx];
            lboxCmdSelected.Items.Remove(cmd);
            lboxCmdDic.Items.Add(cmd);

            lboxCmdSelected.SelectedIndex = Math.Min(idx, lboxCmdSelected.Items.Count - 1);
            UpdateDisplay();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            int idx = lboxCmdSelected.SelectedIndex;
            int new_idx = Math.Max(idx - 1, 0);
            if (idx < 0) return;
            string cmd = (string)lboxCmdSelected.Items[idx];

            lboxCmdSelected.Items.RemoveAt(idx);
            lboxCmdSelected.Items.Insert(new_idx, cmd);
            lboxCmdSelected.SelectedIndex = new_idx;

            UpdateDisplay();
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            int idx = lboxCmdSelected.SelectedIndex;
            int new_idx = Math.Min(lboxCmdSelected.Items.Count - 1, idx + 1);
            if (idx < 0) return;
            string cmd = (string)lboxCmdSelected.Items[idx];

            lboxCmdSelected.Items.RemoveAt(idx);
            lboxCmdSelected.Items.Insert(new_idx, cmd);
            lboxCmdSelected.SelectedIndex = new_idx;

            UpdateDisplay();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            GRecipes.CmdsDictionary = lboxCmdSelected.Items.OfType<string>().Where(x => x != ECmd.NONE.ToString()).Select(x => (ECmd)Enum.Parse(typeof(ECmd), x)).ToList();
            var dftcmd = ECmd.NONE;
            if (!GRecipes.CmdsDictionary.Contains(dftcmd)) GRecipes.CmdsDictionary.Insert(0, dftcmd);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
