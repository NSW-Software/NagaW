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
    public partial class frmFileImport : Form
    {
        public frmFileImport()
        {
            InitializeComponent();
        }

        enum ESelect { All, Features, OneFeature }
        private void frmFileImport_Load(object sender, EventArgs e)
        {
            cbxSelect.DataSource = Enum.GetValues(typeof(ESelect));
            UpdateApertureList();
        }

        private void UpdateDisplay()
        {
            cbxOptimizeAll.Checked = TFFileImport.OptimizeAll;
        }
        private void UpdateApertureList()
        {
            lbxApertureList.DataSource = TFFileImport.Feature.Apertures;
        }
        private void UpdateInfo()
        {
            int i = lbxApertureList.SelectedIndex;
            if (i < 0)
            {
                rtbFeatureDetail.Clear();
                return;
            }

            rtbFeatureDetail.Text = "Origin: " + TFFileImport.Feature.Origin.ToStringForDisplay() + '\r' +
                "Name: " + TFFileImport.Feature.Apertures[i].Name + '\r' +
                "Para: " + TFFileImport.Feature.Apertures[i].Para + '\r' +
                "Size: " + TFFileImport.Feature.Apertures[lbxApertureList.SelectedIndex].Size.ToStringForDisplay() + '\r' +
                "Count: " + $"{lbxFeaturesList.Items.Count}" + " / "+ $"{TFFileImport.Feature.Features.Count}";
        }

        List<int> featureListIndex = new List<int>();
        private void UpdateFeatureList()
        {
            int i = lbxApertureList.SelectedIndex;
            if (i < 0)
            {
                lbxFeaturesList.DataSource = null;
                return;
            }
            lbxFeaturesList.DataSource = null;
            lbxFeaturesList.DataSource = TFFileImport.Feature.Features.Where(x => x.ApertureIndex == i).ToArray();
            featureListIndex = TFFileImport.Feature.Features.Where(x => x.ApertureIndex == i).Select(y => y.ID).ToList();
            UpdateInfo();
        }
        private void lbxApertureList_Click(object sender, EventArgs e)
        {
            UpdateFeatureList();
        }
        private void lbxApertureList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFeatureList();
        }
        private void lblFeatures_Click(object sender, EventArgs e)
        {

        }

        private void btnOriginXY_Click(object sender, EventArgs e)
        {
            int i = lbxFeaturesList.SelectedIndex + (featureListIndex.Count * lbxApertureList.SelectedIndex);
            if (i < 0) return;

            //int idx = featureListIndex[i];
            TFFileImport.Feature.Origin = TFFileImport.Feature.Features[i].Point;
            UpdateInfo();
        }
        private void btnRef1_Click(object sender, EventArgs e)
        {
            int i = lbxFeaturesList.SelectedIndex;
            if (i < 0) return;

            int idx = featureListIndex[i];
            TFFileImport.Feature.Features[idx].DispType = TFFileImport.EDispType.Ref1;
            TFFileImport.Feature.Ref1 = TFFileImport.Feature.Features[idx].Point;
            UpdateFeatureList();
            lbxFeaturesList.SelectedIndex = i;
        }
        private void btnRef2_Click(object sender, EventArgs e)
        {
            int i = lbxFeaturesList.SelectedIndex;
            if (i < 0) return;

            int idx = featureListIndex[i];
            TFFileImport.Feature.Features[idx].DispType = TFFileImport.EDispType.Ref2;
            TFFileImport.Feature.Ref2 = TFFileImport.Feature.Features[idx].Point;
            UpdateFeatureList();
            lbxFeaturesList.SelectedIndex = i;
        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            int i = lbxFeaturesList.SelectedIndex;
            if (i < 0) return;

            switch (cbxSelect.SelectedIndex)
            {
                case (int)ESelect.All:
                    foreach (var v in TFFileImport.Feature.Features)
                    {
                        v.DispType = TFFileImport.EDispType.None;
                    }
                    break;
                case (int)ESelect.Features:
                    foreach (var v in TFFileImport.Feature.Features)
                    {
                        if (v.ApertureIndex == lbxApertureList.SelectedIndex)
                        {
                            v.DispType = TFFileImport.EDispType.None;
                        }
                    }
                    break;
                case (int)ESelect.OneFeature:
                    {
                        int idx = featureListIndex[i];
                        TFFileImport.Feature.Features[idx].DispType = TFFileImport.EDispType.None;
                    }
                    break;
            }
            UpdateFeatureList();
            lbxFeaturesList.SelectedIndex = i;
        }
        private void btnDot_Click(object sender, EventArgs e)
        {
            int i = lbxFeaturesList.SelectedIndex;
            if (i < 0) return;

            switch (cbxSelect.SelectedIndex)
            {
                case (int)ESelect.All:
                    foreach (var v in TFFileImport.Feature.Features)
                    {
                        v.DispType = TFFileImport.EDispType.Dot;
                    }
                    break;
                case (int)ESelect.Features:
                    foreach (var v in TFFileImport.Feature.Features)
                    {
                        if (v.ApertureIndex == lbxApertureList.SelectedIndex)
                        {
                            v.DispType = TFFileImport.EDispType.Dot;
                        }
                    }
                    break;
                case (int)ESelect.OneFeature:
                    {
                        int idx = featureListIndex[i];
                        TFFileImport.Feature.Features[idx].DispType = TFFileImport.EDispType.Dot;
                    }
                    break;
            }
            UpdateFeatureList();
            lbxFeaturesList.SelectedIndex = i;
        }
        private void btnLine_Click(object sender, EventArgs e)
        {
            int i = lbxFeaturesList.SelectedIndex;
            if (i < 0) return;

            switch (cbxSelect.SelectedIndex)
            {
                case (int)ESelect.All:
                    foreach (var v in TFFileImport.Feature.Features)
                    {
                        v.DispType = TFFileImport.EDispType.Line;
                    }
                    break;
                case (int)ESelect.Features:
                    foreach (var v in TFFileImport.Feature.Features)
                    {
                        if (v.ApertureIndex == lbxApertureList.SelectedIndex)
                        {
                            v.DispType = TFFileImport.EDispType.Line;
                        }
                    }
                    break;
                case (int)ESelect.OneFeature:
                    {
                        int idx = featureListIndex[i];
                        TFFileImport.Feature.Features[idx].DispType = TFFileImport.EDispType.Line;
                    }
                    break;
            }
            UpdateFeatureList();
            lbxFeaturesList.SelectedIndex = i;
        }
        private void btnArc_Click(object sender, EventArgs e)
        {
            int i = lbxFeaturesList.SelectedIndex;
            if (i < 0) return;

            switch (cbxSelect.SelectedIndex)
            {
                case (int)ESelect.All:
                    foreach (var v in TFFileImport.Feature.Features)
                    {
                        v.DispType = TFFileImport.EDispType.Arc;
                    }
                    break;
                case (int)ESelect.Features:
                    foreach (var v in TFFileImport.Feature.Features)
                    {
                        if (v.ApertureIndex == lbxApertureList.SelectedIndex)
                        {
                            v.DispType = TFFileImport.EDispType.Arc;
                        }
                    }
                    break;
                case (int)ESelect.OneFeature:
                    {
                        int idx = featureListIndex[i];
                        TFFileImport.Feature.Features[idx].DispType = TFFileImport.EDispType.Arc;
                    }
                    break;
            }
            UpdateFeatureList();
            lbxFeaturesList.SelectedIndex = i;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = GDoc.SMTRecipeDir.FullName;
            ofd.Filter = "NSW Disp Definition|*.ndd";

            if (ofd.ShowDialog() != DialogResult.OK) return;

            if (!TFFileImport.Load(ofd.FileName)) return;

            UpdateApertureList();
            UpdateFeatureList();
            UpdateInfo();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = GDoc.SMTRecipeDir.FullName;
            sfd.Filter ="NSW Disp Definition|*.ndd";

            if (sfd.ShowDialog() != DialogResult.OK) return;

            TFFileImport.Save(sfd.FileName);
        }
        private void btnImport_Click(object sender, EventArgs e)
        {
            frmFileImportSelect frm = new frmFileImportSelect();
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.Cancel) return;

            lbxApertureList.DataSource = TFFileImport.Feature.Apertures;

            int i = lbxApertureList.SelectedIndex;
            lbxFeaturesList.DataSource = null;
            lbxFeaturesList.DataSource = TFFileImport.Feature.Features.Where(x => x.ApertureIndex == i).ToArray();

            UpdateInfo();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            TFFileImport.Feature.Clear();
            UpdateApertureList();
            UpdateFeatureList();
            UpdateInfo();
        }

        private void btnApUp_Click(object sender, EventArgs e)
        {
            int i = lbxApertureList.SelectedIndex;
            if (i <= 0) return;

            TFFileImport.Feature.Apertures.MoveUp(i);

            TFFileImport.Feature.Features.Where(x => x.ApertureIndex == i - 1).ToList().ForEach(f => f.ApertureIndex = -1);
            TFFileImport.Feature.Features.Where(x => x.ApertureIndex == i).ToList().ForEach(f => f.ApertureIndex = i-1);
            TFFileImport.Feature.Features.Where(x => x.ApertureIndex == - 1).ToList().ForEach(f => f.ApertureIndex = i);

            UpdateApertureList();
            lbxApertureList.SelectedIndex = i - 1;
        }
        private void btnApDn_Click(object sender, EventArgs e)
        {
            int i = lbxApertureList.SelectedIndex;
            if (i < 0) return;
            if (i >= TFFileImport.Feature.Apertures.Count - 1) return;

            TFFileImport.Feature.Apertures.MoveDown(i);

            TFFileImport.Feature.Features.Where(x => x.ApertureIndex == i + 1).ToList().ForEach(f => f.ApertureIndex = -1);
            TFFileImport.Feature.Features.Where(x => x.ApertureIndex == i).ToList().ForEach(f => f.ApertureIndex = i + 1);
            TFFileImport.Feature.Features.Where(x => x.ApertureIndex == -1).ToList().ForEach(f => f.ApertureIndex = i);

            UpdateApertureList();
            lbxApertureList.SelectedIndex = i + 1;
        }

        private void btnFtUp_Click(object sender, EventArgs e)
        {
            int i = lbxFeaturesList.SelectedIndex;
            if (i <= 0) return;
            int j = lbxApertureList.SelectedIndex;

            int idx = 0;
            foreach (TFFileImport.TFeatures feat in TFFileImport.Feature.Features)
            {
                if (feat.ApertureIndex == j) break;
                idx++;
            }
            TFFileImport.Feature.Features.MoveUp(idx + i);

            UpdateFeatureList();
            lbxFeaturesList.SelectedIndex = i - 1;
        }
        private void btnFtDn_Click(object sender, EventArgs e)
        {
            int i = lbxFeaturesList.SelectedIndex;
            if (i < 0) return;
            if (i >= lbxFeaturesList.Items.Count - 1) return;
            int j = lbxApertureList.SelectedIndex;

            int idx = 0;
            foreach (TFFileImport.TFeatures feat in TFFileImport.Feature.Features)
            {
                if (feat.ApertureIndex == j) break;
                idx++;
            }
            TFFileImport.Feature.Features.MoveDown(idx + i);

            UpdateFeatureList();
            lbxFeaturesList.SelectedIndex = i + 1;
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            int i = lbxFeaturesList.SelectedIndex;
            if (i < 0) return;
            TFFileImport.Feature.Sort(new PointD(0, 0));
            UpdateFeatureList();
            //UpdateApertureList();
        }

        private void cbxOptimizeAll_CheckedChanged(object sender, EventArgs e)
        {
            TFFileImport.OptimizeAll = cbxOptimizeAll.Checked;
        }
    }
}
