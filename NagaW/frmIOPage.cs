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
    public partial class frmIOPage : Form
    {
        public frmIOPage()
        {
            InitializeComponent();
        }

        private void frmIOPage_Load(object sender, EventArgs e)
        {
            UpdateList(true);
            UpdateList(false);
            tmrDisplay.Enabled = true;

            foreach (Button btn in groupboxOutput.Controls) btn.Click += (a, b) => OutputTrg(a, b);
            foreach (Label lbl in groupboxInput.Controls) lbl.Click += (a, b) => InputTrig(a, b);
        }

        public int[] inputlist = new int[16];
        public int[] outputList = new int[16];
        public int startInput = 0;
        public int startOutput = 0;
        public void UpdateList(bool updateInput)
        {
            for (int i = 0; i < 16; i++)
            {
                if (updateInput) inputlist[i] = startInput + i;
                else outputList[i] = startOutput + i;
            }
        }

        private void UpdateDisplay()
        {
            btnEdit.BackColor = Edit ? Color.Orange : SystemColors.Control;

            btnInput1.BackColor = startInput == 0 ? Color.Lime : Color.LightGray;
            btnInput2.BackColor = startInput == 16 ? Color.Lime : Color.LightGray;
            btnInput3.BackColor = startInput == 32 ? Color.Lime : Color.LightGray;
            btnInput4.BackColor = startInput == 48 ? Color.Lime : Color.LightGray;
            btnOutput1.BackColor = startOutput == 0 ? Color.Lime : Color.LightGray;
            btnOutput2.BackColor = startOutput == 32 ? Color.Lime : Color.LightGray;
            btnOutput3.BackColor = startOutput == 48 ? Color.Lime : Color.LightGray;

            foreach (Control lblInput in groupboxInput.Controls)
            {
                try
                {
                    int inNo = inputlist[Convert.ToInt16(lblInput.Name.Replace("In", ""))];
                    lblInput.BackColor = Edit ? Color.Orange : GMotDef.Inputs[inNo].Status ? Color.Lime : Color.LightGray;
                    lblInput.Text = $"DI {GMotDef.Inputs[inNo].InputNo:00} - " + GMotDef.Inputs[inNo].Name;
                }
                catch
                {
                    lblInput.BackColor = Color.LightGray;
                    lblInput.Text = "-";
                }
            }
            foreach (Control lblOutput in groupboxOutput.Controls)
            {
                try
                {
                    int outNo = outputList[Convert.ToInt16(lblOutput.Name.Replace("Out", ""))];
                    lblOutput.BackColor = Edit ? Color.Orange : GMotDef.Outputs[outNo].Status ? Color.Red : Color.LightGray;
                    lblOutput.Text = $"DO {GMotDef.Outputs[outNo].OutputNo:00} - " + GMotDef.Outputs[outNo].Name;
                }
                catch
                {
                    lblOutput.BackColor = Color.LightGray;
                    lblOutput.Text = "-";
                }
            }
        }

        private void tmrDisplay_Tick(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void btn_Input1_Click(object sender, EventArgs e)
        {
            startInput = 0;
            UpdateList(true);
        }
        private void btn_Input2_Click(object sender, EventArgs e)
        {
            startInput = 16;
            UpdateList(true);
        }
        private void btn_Input3_Click(object sender, EventArgs e)
        {
            startInput = 32;
            UpdateList(true);
        }
        private void btnInput4_Click(object sender, EventArgs e)
        {
            startInput = 48;
            UpdateList(true);
        }

        private void btn_Output1_Click(object sender, EventArgs e)
        {
            startOutput = 0;
            UpdateList(false);
        }
        private void btn_Output2_Click(object sender, EventArgs e)
        {
            startOutput = 32;
            UpdateList(false);
        }
        private void btn_Output3_Click(object sender, EventArgs e)
        {
            startOutput = 48;
            UpdateList(false);
        }

        public void OutputTrg(object sender, EventArgs e)
        {
            int outputbtn = Convert.ToInt16(((Button)sender).Name.Replace("Out", ""));
            var output = GMotDef.Outputs[outputList[outputbtn]];

            if (Edit)
            {
                EditIO(output.ToString(), output);
                UpdateDisplay();
            }
            else
            {
                output.Status = !output.Status;
            }
        }
        public void InputTrig(object sender, EventArgs e)
        {
            int Inputlbl = Convert.ToInt16(((Label)sender).Name.Replace("In", ""));
            var Input = GMotDef.Inputs[inputlist[Inputlbl]];

            if (Edit)
            {
                EditIO(Input.ToString(), Input);
                UpdateDisplay();
            }
        }


        bool Edit;
        private void btnEdit_Click(object sender, EventArgs e)
        {
            Edit = !Edit;
            UpdateDisplay();
        }


        void EditIO(string title, object obj)
        {
            Form frm = new Form();
            PropertyGrid grid = new PropertyGrid();
            grid.Size = new Size(400, 200);
            grid.SelectedObject = obj;
            frm.Controls.Add(grid);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.MinimizeBox = false;
            frm.MaximizeBox = false;
            frm.FormBorderStyle = FormBorderStyle.FixedSingle;
            frm.AutoSize = true;
            frm.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            frm.Text = $"Edit {title} ";
            frm.ShowDialog();
        }

    }
}
