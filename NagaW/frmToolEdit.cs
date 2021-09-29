using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NagaW
{
    public partial class frmToolEdit : Form
    {
        DataGridView Dgv;
        public frmToolEdit()
        {
            InitializeComponent();
            GControl.EditForm(this);
            Text = "Tool Editor";
        }

        private void frmTools_Load(object sender, EventArgs e)
        {
            GControl.ConvertTabCtrlToFLP(tabControl1);
            InitDgv();
            GControl.UpdateFormControl(this);
        }

        private void InitDgv()
        {
            #region
            Dgv = new DataGridView()
            {
                MultiSelect = false,
                Dock = DockStyle.Fill,
                RowHeadersVisible = false,
                AllowUserToResizeRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                DataSource = TFTool.DispTools,
                AllowUserToAddRows = false,
            };

            Dgv.RowTemplate.Height = 30;

            Dgv.CellContentClick += (a, b) => Dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);

            tabPage1.Controls.Add(Dgv);

            Dgv.Columns[0].ReadOnly = true;
            Dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Dgv.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Dgv.ClearSelection();
            Dgv.BringToFront();
            #endregion
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            int idx = Dgv.CurrentCell.RowIndex;
            if (idx == 0) return;

            TFTool.DispTools.MoveUp(idx);
            TFTool.DispTools.ResetBindings();

            Dgv.CurrentCell = Dgv[0, idx - 1];
        }
        private void btnMoveDn_Click(object sender, EventArgs e)
        {
            int idx = Dgv.CurrentCell.RowIndex;
            if (idx == Dgv.RowCount - 1) return;

            TFTool.DispTools.MoveDown(idx);
            TFTool.DispTools.ResetBindings();

            Dgv.CurrentCell = Dgv[0, idx + 1];
        }


        private void frmTools_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
