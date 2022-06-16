using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace NagaW
{
    public partial class frmFileImportSelect : Form
    {
        //public TFFileImport.TDispFeature feature;
        public frmFileImportSelect()
        {
            InitializeComponent();
            cbxFileType.DataSource = Enum.GetNames(typeof(TFFileImport.EFileType));
        }

        #region
        const int MAX_PATH = 255;
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetShortPathName(
            [MarshalAs(UnmanagedType.LPTStr)]
         string path,
            [MarshalAs(UnmanagedType.LPTStr)]
         StringBuilder shortPath,
            int shortPathLength
            );
        private static string GetShortPath(string path)
        {
            var shortPath = new StringBuilder(MAX_PATH);
            GetShortPathName(path, shortPath, MAX_PATH);
            return shortPath.ToString();
        }
        #endregion

        private void frmImport_Load(object sender, EventArgs e)
        {

        }

        private void cbxFileType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            pnlODB.Visible = (cbxFileType.SelectedIndex == (int)TFFileImport.EFileType.OBD_v7);
        }

        string stepsFolder = "";
        string layersFolder = "";
        string restoreDir = "";
        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = restoreDir;

            try
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    tbxFolder.Text = GetShortPath(fbd.SelectedPath);
                    restoreDir = fbd.SelectedPath;

                    stepsFolder = fbd.SelectedPath + @"\steps\";
                    var stepNames = Directory.GetDirectories(stepsFolder).Select(x => Path.GetFileName(x));
                    cbxStepName.DataSource = stepNames.ToArray();

                    layersFolder = stepsFolder + stepNames.ToArray()[0].ToString() + @"\layers\";
                    var layerNames = Directory.GetDirectories(layersFolder).Select(x => Path.GetFileName(x));
                    cbxLayerName.DataSource = layerNames.ToArray();

                    fileName = layersFolder + layerNames.ToArray()[0].ToString() + @"\features";
                    tbxFilename.Text = GetShortPath(fileName);
                }
                else
                {
                    tbxFolder.Text = "";
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter =
                "Gerber_RS274X (*.gbr;*.gbw;*.gbx;*.pho;*.art;*.gdo|*.gbr;*.gbw;*.gbx;*.pho;*.art;*.gdo|" +
                "All files (*.*)|*.*";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.Cancel) return;

            fileName = ofd.FileName;
            tbxFilename.Text = GetShortPath(fileName);
        }

        private void cbxStepName_SelectedValueChanged(object sender, EventArgs e)
        {
            layersFolder = stepsFolder + cbxStepName.Text + @"\layers\";
            var layerNames = Directory.GetDirectories(layersFolder).Select(x => Path.GetFileName(x));
            cbxLayerName.DataSource = layerNames.ToArray();
        }
        private void cbxLayerName_SelectedValueChanged(object sender, EventArgs e)
        {
            fileName = layersFolder + cbxLayerName.Text + @"\features";
            tbxFilename.Text = GetShortPath(fileName);
        }

        string fileName = "";
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!File.Exists(fileName)) MessageBox.Show("File not found.");

            switch (cbxFileType.SelectedIndex)
            {
                case (int)TFFileImport.EFileType.Gerber_RS274X:
                    if (!TFFileImport.Gerber_RS274X.Decode(fileName, ref TFFileImport.Feature)) return;
                    break;
                case (int)TFFileImport.EFileType.OBD_v7:
                    if (!TFFileImport.ODBPP.Decode(fileName, ref TFFileImport.Feature)) return;
                    break;
                case (int)TFFileImport.EFileType.DXF:
                    if (!TFFileImport.DXF.Decode(fileName, ref TFFileImport.Feature)) return;
                    break;
                case (int)TFFileImport.EFileType.Excel_New:
                    if (!TFFileImport.Excel.Decode(fileName, ref TFFileImport.Feature)) return;
                    break;
            }

            DialogResult = DialogResult.OK;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
