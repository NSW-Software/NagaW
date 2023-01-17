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
    public partial class frmRecipeModel : Form
    {
        TFModel[] Models = null;// = Enumerable.Range(0, 2).Select(x => new TFModel(x)).ToArray();

        public frmRecipeModel()
        {
            InitializeComponent();
        }
        public frmRecipeModel(TFModel[] models)
        {
            Models = models;
        }
        private void frmRecipeModel_Load(object sender, EventArgs e)
        {
            CreateDGV();

            //GControl.ConvertTabCtrlToFLP(tabControl1);
        }

        //Property Grid
        private void Set()
        {
            TabControl tctrl = new TabControl
            {
                Appearance = TabAppearance.Buttons,
                ItemSize = new Size(60, 35),
                Dock = DockStyle.Fill
            };

            var model = GRecipes.Models;
            PropertyGrid grid = new PropertyGrid
            {
                SelectedObject = model,
                ToolbarVisible = false,
                Dock = DockStyle.Fill,
            };

            TabPage tp = new TabPage { Text = $"Model" };
            tp.Controls.Add(grid);
            tctrl.TabPages.Add(tp);

            /*tabPage1*/this.Controls.Add(tctrl);
            tctrl.BringToFront();
        }

        //Data Grid View
        private void CreateDGV()
        {
            try
            {
                var model = GRecipes.Models;
                switch (GSystemCfg.Pump.Pumps[0].PumpType)
                {
                    case EPumpType.SP: dgv1.DataSource = model[0].SP_Model; break;
                    case EPumpType.HM: dgv1.DataSource = model[0].HM_Model; break;
                    case EPumpType.VERMES_3280: dgv1.DataSource = model[0].VM3280_Model; break;
                    default: break;
                }

                for (int i = 0; i < dgv1.Rows.Count; i++) dgv1.Rows[i].HeaderCell.Value = String.Format("{0}", i);

                switch (GSystemCfg.Pump.Pumps[1].PumpType)
                {
                    case EPumpType.SP: dgv2.DataSource = model[1].SP_Model; break;
                    case EPumpType.HM: dgv2.DataSource = model[1].HM_Model; break;
                    case EPumpType.VERMES_3280: dgv2.DataSource = model[1].VM3280_Model; break;
                    default: break;
                }

                for (int i = 0; i < dgv2.Rows.Count; i++) dgv2.Rows[i].HeaderCell.Value = String.Format("{0}", i);
            }
            catch { }
        }

        private void dgv_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                var cell = dgv1.SelectedCells;
                var col = cell[0].ColumnIndex;
                var row = cell[0].RowIndex;

                switch (dgv1.Rows[row].Cells[col].Value)
                {
                    case DPara dpara: if (!GLog.SetPara(ref dpara)) return; break;
                    case IPara ipara: if (!GLog.SetPara(ref ipara)) return; break;
                }
            }
            catch { }
        }

        private void dgv2_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                var cell = dgv2.SelectedCells;
                var col = cell[0].ColumnIndex;
                var row = cell[0].RowIndex;

                switch (dgv2.Rows[row].Cells[col].Value)
                {
                    case DPara dpara: if (!GLog.SetPara(ref dpara)) return; break;
                    case IPara ipara: if (!GLog.SetPara(ref ipara)) return; break;
                }
            }
            catch { }
        }
    }
}
