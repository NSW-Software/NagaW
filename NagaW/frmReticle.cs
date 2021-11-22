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
    public partial class frmReticle : Form
    {
        int selectedCam = 0;
        TEReticle reticle = new TEReticle();
        public frmReticle(int SelectedCam)
        {
            InitializeComponent();
            GControl.EditForm(this);
            this.Text = "Reticle Editor";

            selectedCam = SelectedCam;
        }

        private void frmReticle_Load(object sender, EventArgs e)
        {
            int i = 0;
            foreach (var b in GRecipes.Reticle[selectedCam]) { cbxListReticles.Items.Add($"Reticle {i++}", b.Enable); };

            cbxReticleType.DataSource = Enum.GetValues(typeof(TEReticle.EType));
            if (GRecipes.Reticle[selectedCam].Count == 0)
                GRecipes.Reticle[selectedCam] = new BindingList<TEReticle>(Enumerable.Range(0, GRecipes.MAX_RETICLE).Select(y => new TEReticle()).ToList());
            reticle = GRecipes.Reticle[selectedCam][0];
            UpdateDisplay();
        }
        private void UpdateDisplay()
        {
            cbxReticleType.SelectedItem = reticle.Type;

            nudSizeX.Value = (int)reticle.Size.Width;
            nudSizeY.Value = (int)reticle.Size.Height;

            lblColor.BackColor = reticle.Color;
            lblColor.Text = new ColorConverter().ConvertToString(reticle.Color);

            tbxText.Text = reticle.Text;

            nudLineWidth.Value = reticle.LineWidth;
        }

        private void lblColor_Click(object sender, EventArgs e)
        {
            var clrdlg = new ColorDialog();
            if (clrdlg.ShowDialog() != DialogResult.OK) return;
            reticle.Color = clrdlg.Color;
            UpdateDisplay();
        }

        private void nudSizeX_ValueChanged(object sender, EventArgs e)
        {
            reticle.Size = new SizeF((float)nudSizeX.Value, reticle.Size.Height);
        }
        private void nudSizeY_ValueChanged(object sender, EventArgs e)
        {
            reticle.Size = new SizeF(reticle.Size.Width, (float)nudSizeY.Value);
        }

        private void cbxReticleType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            reticle.Type = (TEReticle.EType)cbxReticleType.SelectedItem;
            UpdateDisplay();
        }

        private void cbxListReticles_SelectedIndexChanged(object sender, EventArgs e)
        {
            reticle = GRecipes.Reticle[selectedCam][cbxListReticles.SelectedIndex];
            UpdateDisplay();
        }

        private void cbxListReticles_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int i = cbxListReticles.SelectedIndex;
            if (i< 0) return;

            GRecipes.Reticle[selectedCam][i].Enable = !cbxListReticles.GetItemChecked(i);
            UpdateDisplay();
        }
        private void nudLineWidth_ValueChanged(object sender, EventArgs e)
        {
            reticle.LineWidth = (int)nudLineWidth.Value;

        }

        private void tbxText_TextChanged(object sender, EventArgs e)
        {
            reticle.Text = (sender as TextBox).Text;
            UpdateDisplay();
        }
    }

    //public class TReticle
    //{
    //    public enum EType
    //    {
    //        None = 0,
    //        //CenterCross = 1,
    //        //CenterCross3 = 7,
    //        //CenterReticle,
    //        //Line = 2,
    //        Cross = 3,
    //        Circle = 4,
    //        Rectangle = 5,
    //        //Text = 6
    //    }
    //    public EType Type { get; set; }
    //    public Point Location { get; set; }
    //    public SizeF Size { get; set; }
    //    public string Text { get; set; } = string.Empty;
    //    public Color Color { get; set; } = Color.Yellow;
    //    public int LineWidth { get; set; } = 1;
    //    public bool Enable { get; set; }
    //    public TReticle()
    //    {
    //    }
    //    public TReticle(TReticle Reticle)
    //    {
    //        Type = Reticle.Type;
    //        Location = Reticle.Location;
    //        Size = Reticle.Size;
    //        Text = Reticle.Text;
    //        Color = Reticle.Color;
    //    }
    //    public TReticle(EType Type, Point Location, SizeF Size, Color Color, string Text = "")
    //    {
    //        this.Type = Type;
    //        this.Location = Location;
    //        this.Size = Size;
    //        this.Text = Text;
    //        this.Color = Color;
    //    }
    //}
}
