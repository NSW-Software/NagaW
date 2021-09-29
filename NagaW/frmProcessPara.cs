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
    public partial class frmSetupPara : Form
    {
        public frmSetupPara()
        {
            InitializeComponent();
        }
        private void frmVar_Load(object sender, EventArgs e)
        {
            tabControl1.TabPages.Clear();
            Loadtype();

            //GControl.ConvertTabCtrlToFLP(tabControl1);
            Size = new Size(600, 400);
            GControl.LogForm(this);
        }
        public void Loadtype()
        {
            Updatedgv(typeof(GProcessPara.Home));
            Updatedgv(typeof(GProcessPara.Operation));
            Updatedgv(typeof(GProcessPara.Jog));
            Updatedgv(typeof(GProcessPara.Conveyor));
            Updatedgv(typeof(GProcessPara.HSensor));
            Updatedgv(typeof(GProcessPara.Vision));
        }

        private void Updatedgv(Type type)
        {
            TabPage tabPage = new TabPage(type.Name);
            tabControl1.TabPages.Add(tabPage);

            Refresh();

            DataGridView dgv;
            dgv.Font = this.Font;
            void Refresh()
            {
                #region Define dgv
                dgv = new DataGridView()
                {
                    ReadOnly = true,
                    MultiSelect = false,
                    Dock = DockStyle.Fill,
                    RowHeadersVisible = false,
                    AllowUserToResizeRows = false,
                    AllowUserToResizeColumns = false,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    CellBorderStyle = DataGridViewCellBorderStyle.RaisedHorizontal,
                    ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                };

                dgv.RowTemplate.Height = 30;

                tabPage.Controls.Clear();
                tabPage.Controls.Add(dgv);

                dgv.ColumnCount = 2;

                dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dgv.Columns.OfType<DataGridViewColumn>().ToList().ForEach(x => 
                {
                    x.SortMode = DataGridViewColumnSortMode.NotSortable;
                    x.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    x.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                });

                dgv.Columns[0].Name = "Name";
                dgv.Columns[1].Name = "Value";
                #endregion

                #region Get Set
                type.GetFields(BindingFlags.Public | BindingFlags.Static).ToList().ForEach(x =>
                {
                    DataGridViewRow row = (DataGridViewRow)dgv.Rows[0].Clone();
                    row.Cells[1].Style.ForeColor = row.Cells[0].Style.ForeColor;
                    row.Tag = null;
                    row.Cells[0].Value = x.Name;
                    row.Cells[0].Style.BackColor = Color.Silver;

                    switch (x.GetValue(null))
                    {
                        case IPara ipara:
                            {
                                row.Cells[1].Value = ipara.ToStringForDisplay();
                                if (ipara.IsOutofRange) row.Cells[1].Style.ForeColor = Color.Red;
                                row.Tag = ipara;
                            }
                            break;
                        case DPara dpara:
                            {
                                row.Cells[1].Value = dpara.ToStringForDisplay();
                                if (dpara.IsOutofRange) row.Cells[1].Style.ForeColor = Color.Red;
                                row.Tag = dpara;
                            }
                            break;
                        case PointI pointI:
                            {
                                row.Cells[1].Value = pointI.ToStringForDisplay();
                            }
                            break;
                        case PointD pointD:
                            {
                                row.Cells[1].Value = pointD.ToStringForDisplay();
                            }
                            break;
                        case double dvalue:
                            {
                                row.Cells[1].Value = dvalue;
                            }
                            break;
                        case int ivalue:
                            {
                                row.Cells[1].Value = ivalue;
                            }
                            break;
                        default:return;
                    }
                    dgv.Rows.Add(row);
                });

                dgv.CellClick += (a, b) =>
                {
                    if (b.RowIndex == -1) return;
                    if (b.ColumnIndex != 1) return;

                    var cellindex = new int[] { dgv.CurrentCell.ColumnIndex, dgv.CurrentCell.RowIndex };

                    switch (dgv.CurrentRow.Tag)
                    {
                        case IPara ipara:
                            {
                                if (!GLog.SetPara(ref ipara)) return;

                            }
                            break;
                        case DPara dPara:
                            {
                                if (!GLog.SetPara(ref dPara)) return;
                            }
                            break;
                        default: return;
                    }

                    Refresh();

                    dgv.CurrentCell = dgv[cellindex[0], cellindex[1]];
                };
                #endregion

                dgv.AllowUserToAddRows = false;
            }
        }
    }
}
