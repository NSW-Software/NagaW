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
    public partial class frmParaEditor : Form
    {
        public double Value;

        string ParaName;

        double RealMin;
        double RealMax;

        double Min;
        double Max;
        bool Bypass;

        bool masterflag_edit = true;

        bool EditMode = false;
        DPara dpara;

        public frmParaEditor()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
            TopMost = TopLevel = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            //ControlBox = false;
            ShowIcon = false;
            BackColor = GSystemCfg.Display.ThemeColor;
            Text = "";
            pnlEditLimit.Visible = false;
        }

        public frmParaEditor(DPara para) : this()
        {
            dpara = new DPara(para).ConvertTo(GSystemCfg.Display.PressUnit);

            ParaName = dpara.Name;
            Text += dpara.Name;
            lblValue.Text = dpara.Value.ToString();
            RealMin = Min = dpara.Min;
            RealMax = Max = dpara.Max;
            tslblUnit.Text += " " + dpara.Unit.ToStringForDisplay();


        }

        private void frmParaEditor_Load(object sender, EventArgs e)
        {
            if (TFParamCtrl.Load(ParaName, out TEParamCtrl paramCtrl))
            {
                Min = paramCtrl.Min; 
                Max = paramCtrl.Max;
            }

            tscbx.Visible = dpara.Collection != null;
            if (tscbx.Visible)
            {
                int i = 0;
                tscbx.ComboBox.DataSource = dpara.Collection.Select(x => $"{i++} - {x}").ToArray();
                masterflag_edit = tsbtnMin.Visible = tsbtnMax.Visible = false;
                tscbx.ComboBox.SelectionChangeCommitted += (a, b) => lblValue.Text = tscbx.SelectedIndex.ToString();
            }
            UpdateDisplay();

            foreach (Button btn in GControl.GetChildItems(this, typeof(Button))) btn.MouseUp += (a, b) => lblValue.Focus();
        }

        private void UpdateDisplay()
        {
            nudMin.Minimum = (decimal)RealMin;
            nudMin.Maximum = (decimal)RealMax;
            nudMin.Value = (decimal)Min;

            nudMax.Minimum = (decimal)RealMin;
            nudMax.Maximum = (decimal)RealMax;
            nudMax.Value = (decimal)Max;

            tsbtnMin.Text = "Min:" + Min.ToString();
            tsbtnMax.Text = "Max:" + Max.ToString();

            tsbtnEdit.Visible = (TFUser.CurrentUser.Level >= Elevel.ADMIN) && masterflag_edit;

            pnlEditLimit.Visible = EditMode;

        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(lblValue.Text, out double value)) 
            {
                MsgBox.ShowDialog("Invalid input");
                return;
            }

            if (value > Max || value < Min)
            {
                string msg = $"{value} out of range {Min} - {Max}";
                if (Bypass)
                {
                    if (MsgBox.ShowDialog(msg + "\r\nProceed?", MsgBoxBtns.OKCancel) != DialogResult.OK) return;
                }
                else
                {
                    MsgBox.ShowDialog(msg);
                    return;
                }
            }

            dpara.Value = Math.Round(value, dpara.DecimalPlace);
            dpara = dpara.ConvertFrom(GSystemCfg.Display.PressUnit);
            Value = dpara.Value;

            DialogResult = DialogResult.OK;
            Close();
        }
        private void btnNo_Click(object sender, EventArgs e)
        {
            if (pnlEditLimit.Visible) return;
            switch((sender as Button).Text)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    {
                        lblValue.Text += (sender as Button).Text;
                    }
                    break;
                case ".":
                    {
                        if (lblValue.Text.Contains(".")) return;
                        lblValue.Text += ".";
                    }
                    break;
                case "C":
                    {
                        lblValue.Text = string.Empty;
                    }
                    break;
                case "+/-":
                    {
                        lblValue.Text = lblValue.Text.Contains("-") ? lblValue.Text.Replace("-", string.Empty) : "-" + lblValue.Text;
                    }
                    break;
                case "<":
                    {
                        if (lblValue.Text == string.Empty) return;
                        lblValue.Text = lblValue.Text.Remove(lblValue.Text.Length - 1);
                    }
                    break;
            }
        }
        private void frmCalculator_KeyDown(object sender, KeyEventArgs e)
        {
            if (pnlEditLimit.Visible) return;

            switch (e.KeyCode)
            {
                case Keys.D0:
                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                case Keys.D7:
                case Keys.D8:
                case Keys.D9:
                    {
                        lblValue.Text += ((char)e.KeyCode).ToString();
                    }
                    break;
                case Keys.NumPad0:
                case Keys.NumPad1:
                case Keys.NumPad2:
                case Keys.NumPad3:
                case Keys.NumPad4:
                case Keys.NumPad5:
                case Keys.NumPad6:
                case Keys.NumPad7:
                case Keys.NumPad8:
                case Keys.NumPad9:
                    {
                        lblValue.Text += ((char)(e.KeyCode - 48)).ToString();
                    }
                    break;
                case Keys.Decimal:
                case Keys.OemPeriod:
                    {
                        if (lblValue.Text.Contains(".")) return;
                        lblValue.Text += ".";
                    }
                    break;
                case Keys.Oemplus:
                case Keys.Add:
                    {
                        lblValue.Text = lblValue.Text.Replace("-", string.Empty);
                    }
                    break;
                case Keys.OemMinus:
                case Keys.Subtract:
                    {
                        if (!lblValue.Text.Contains("-")) lblValue.Text = "-" + lblValue.Text; 
                    }
                    break;
                case Keys.Back:
                    {
                        if (lblValue.Text == string.Empty) return;
                        lblValue.Text = lblValue.Text.Remove(lblValue.Text.Length-1);
                    }
                    break;
                case Keys.Delete:
                    {
                        lblValue.Text = string.Empty;
                    }
                    break;
                case Keys.B:
                    {
                        if (e.Alt && e.Shift && e.Control) Bypass = !Bypass;
                        lblValue.ForeColor = Bypass ? Color.Red : Color.Navy;
                    }
                    break;
            }

        }

        private void tsbtnMax_Click(object sender, EventArgs e)
        {
            lblValue.Text = Max.ToString();
        }
        private void tsbtnMin_Click(object sender, EventArgs e)
        {
            lblValue.Text = Min.ToString();
        }

        private void tsbtnEdit_Click(object sender, EventArgs e)
        {
            if (TFUser.CurrentUser.Level < Elevel.ADMIN) return;
            EditMode = !EditMode;
            UpdateDisplay();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            double min = (double)nudMin.Value;
            double max = (double)nudMax.Value;

            string msg = $"Apply change below?\rMin {Min} to {min}\rMax {Max} to {max}";

            if (MessageBox.Show(msg, "Comfirmation", MessageBoxButtons.OKCancel) != DialogResult.OK) return;

            Min = min;
            Max = max;

            TFParamCtrl.Save(new TEParamCtrl(ParaName, Min, Max));

            EditMode = false;

            UpdateDisplay();
        }

        Stopwatch swDown = new Stopwatch();
        private void btnClear_MouseDown(object sender, MouseEventArgs e)
        {
            swDown.Restart();
        }

        private void btnClear_MouseUp(object sender, MouseEventArgs e)
        {
            if (swDown.ElapsedMilliseconds > 500)
            {
                lblValue.Text = string.Empty;
            }
        }
    }




    public class TEParamCtrl
    {
        public string Name { get; set; } = string.Empty;
        public double Min { get; set; } = 0;
        public double Max { get; set; } = 0;

        public TEParamCtrl(string name, double min, double max)
        {
            Name = name;
            Min = min;
            Max = max;
        }
        public TEParamCtrl()
        {
        }
        public TEParamCtrl(TEParamCtrl paramCtrl) : this(paramCtrl.Name, paramCtrl.Min, paramCtrl.Max)
        {
        }
    }


    public static class TFParamCtrl
    {
        public static bool Load(string search, out TEParamCtrl param, IList<TEParamCtrl> paramCtrls_source)
        {
            param = new TEParamCtrl();

            var para = paramCtrls_source.FirstOrDefault(x => x.Name == search);
            if (para is null) return false;

            param = new TEParamCtrl(para);
            return true;
        }
        public static bool Load(string search, out TEParamCtrl param)
        {
            return Load(search, out param, GRecipes.ParamCtrls);
        }

        public static bool Save(TEParamCtrl param, IList<TEParamCtrl> paramCtrls_source)
        {
            var para = paramCtrls_source.FirstOrDefault(x => x.Name == param.Name);
            if (para is null) paramCtrls_source.Add(param);
            else paramCtrls_source[paramCtrls_source.IndexOf(para)] = new TEParamCtrl(param);


            return true;
        }
        public static bool Save(TEParamCtrl param)
        {
            return Save(param, GRecipes.ParamCtrls);
        }
    }
}
