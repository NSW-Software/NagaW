using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace NagaW
{
    public partial class frmLog : Form
    {
        List<TELog> Logs = new List<TELog>();

        Color AlarmClr = Color.Red;
        Color ParaChangeClr = Color.Blue;
        Color EventClr = Color.Green;
        Color NotifyClr = Color.Brown;
        Color ExceptionClr = Color.Purple;
        Color DefaultClr = Color.Black;
        Color ProcessClr = Color.Navy;

        public frmLog()
        {
            InitializeComponent();
            ShowIcon = false;
            WindowState = FormWindowState.Maximized;
            //MaximizeBox = MinimizeBox = false;
            Text = "Log Viewer";
        }
        private void frmLog_Load(object sender, EventArgs e)
        {
            #region Init ts
            dtpickerFrom.Value = DateTime.Today;
            richTextBox1.ReadOnly = true;
            richTextBox1.BackColor = SystemColors.ControlLight;

            foreach (ELogType logtype in Enum.GetValues(typeof(ELogType)))
            {
                ToolStripButton tsbtn = new ToolStripButton(logtype.ToString()) { CheckOnClick = true, Checked = true };
                switch (logtype)
                {
                    case ELogType.ALARM:
                        tsbtn.ForeColor = AlarmClr;
                        break;
                    case ELogType.PARA:
                        tsbtn.ForeColor = ParaChangeClr;
                        break;
                    case ELogType.EVENT:
                        tsbtn.ForeColor = EventClr;
                        break;
                    case ELogType.NOTIFY:
                        tsbtn.ForeColor = NotifyClr;
                        break;
                    case ELogType.EXCEP:
                        tsbtn.ForeColor = ExceptionClr;
                        break;
                    case ELogType.PROCESS:
                        tsbtn.ForeColor = ProcessClr;
                        break;
                    default:
                        tsbtn.ForeColor = DefaultClr;
                        tsbtn.Checked = false;
                        break;
                }
                tsbtn.Click += (a, b) => WriteRichTextBoxwithLog();
                toolStrip1.Items.Add(tsbtn);
            }
            #endregion

            GControl.ConvertTabCtrlToFLP(tabControl1);

            lblLogTitle.Text = GDoc.MachineLogFile.FullName;

            if (!GLog.ReadLog(GDoc.MachineLogFile.FullName, out List<TELog> log))
            {
                MsgBox.ShowDialog($"Fail to read {GDoc.MachineLogDir.FullName}");
                return;
            }

            Logs = new List<TELog>(log);
            WriteRichTextBoxwithLog();
            richTextBox1.ScrollToCaret();
        }
        private void WriteRichTextBoxwithLog()
        {
            var filter = toolStrip1.Items.OfType<ToolStripButton>()
                    .Where(x => x.Checked)
                    .Select(x => x.Text);

            var logs = Logs
                .Where(x => filter.Contains(x.LogType.ToString()))
                .Where(x => x.Time.TimeOfDay >= dtpickerFrom.Value.TimeOfDay && x.Time.TimeOfDay <= dtpickerTo.Value.TimeOfDay).ToList();
            
            richTextBox1.Clear();


            logs.ForEach(x =>
            {
                string log = x.GenerateLogForDisplay();
                //richTextBox1.Select(richTextBox1.Text.Length, log.Length);
                
                //switch (x.LogType)
                //{
                //    case ELogType.ALARM: richTextBox1.SelectionColor = AlarmClr; break;
                //    case ELogType.PARA: richTextBox1.SelectionColor = ParaChangeClr; break;
                //    case ELogType.EVENT: richTextBox1.SelectionColor = EventClr; break;
                //    case ELogType.NOTIFY: richTextBox1.SelectionColor = NotifyClr; break;
                //    case ELogType.EXCEP: richTextBox1.SelectionColor = ExceptionClr; break;
                //    case ELogType.PROCESS: richTextBox1.SelectionColor = ProcessClr; break;
                //    default: richTextBox1.SelectionColor = DefaultClr; break;
                //}
                richTextBox1.AppendText(log + Environment.NewLine);
            });

        }
        private void tsbtnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = GDoc.MachineLogDir.FullName;
            openFileDialog.Filter = GDoc.mLog_ext;
            openFileDialog.ShowDialog();

            if (!GLog.ReadLog(openFileDialog.FileName, out List<TELog> log)) return;

            Logs = new List<TELog>(log);

            dtpickerFrom.Value = DateTime.Today;
            dtpickerTo.Value = new DateTime(2020, 1, 1, 23, 59, 59);
            WriteRichTextBoxwithLog();

            lblLogTitle.Text = openFileDialog.FileName;
        }
        private void btnFilter_Click(object sender, EventArgs e)
        {
            WriteRichTextBoxwithLog();
        }
        private void tsmiCopyLog_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText is null) return;
            if (richTextBox1.SelectedText == string.Empty) return;
            Clipboard.SetText(richTextBox1.SelectedText);
        }
    }
}
