using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace NagaW
{
    using Emgu.CV.UI;

    public partial class frmCamera : Form
    {
        private int scale = 100;
        private int Idx = 0;

        private ImageBox EmguImageBox;
        private TFCamera1 Cam => TFCamera1.Cameras[Idx];
        public frmCamera(int idx)
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;

            this.DoubleBuffered = true;
            EmguImageBox = new ImageBox() { Dock = DockStyle.Fill, Image = Cam.emgucvImage.Clone(), FunctionalMode = ImageBox.FunctionalModeOption.Minimum, };
            pnlImage.Controls.Add(EmguImageBox);
            timer1.Enabled = true;
            Idx = idx;
        }

        private void frmCamera_Load(object sender, EventArgs e)
        {
            Cam.RegisterPicBox(EmguImageBox);

            Cam.Connected += (a, b) =>
            {
                Thread.Sleep(500);
                ZoomFit();
            };

            EmguImageBox.Paint += (a, b) =>
            {
                Size s = Cam.emgucvImage.Size;
                TFReticle.DrawReticle(0, s.Width, s.Height, b.Graphics);
            };

            EmguImageBox.MouseClick += EmguImageBox_MouseClick;

            pnlImage.Resize += (a, b) => ZoomFit();

            ZoomFit();
        }

        private void EmguImageBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            try
            {
                var zs = EmguImageBox.ZoomScale;
                Size s = EmguImageBox.Image.GetInputArray().GetSize();
                var distPerPixel = new double[] { GSystemCfg.Camera.Cameras[Idx].DistPerPixelX, GSystemCfg.Camera.Cameras[Idx].DistPerPixelY };

                var mouseLoc = new PointD((double)(e.Location.X / zs), (double)(e.Location.Y / zs));
                var centrePt = new PointD(s.Width / 2, s.Height / 2);
                var pixelDiff = mouseLoc - centrePt;
                var camdiff = new PointD(pixelDiff.X * distPerPixel[0], -pixelDiff.Y * distPerPixel[1]);

                // GroupCam.OPGroupXYGotoRel(camdiff, true);
                TFGantry.GantrySelect.MoveOpXYRel(camdiff.ToArray);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        static Mutex mtx = new Mutex();
        public void ZoomFit()
        {
            mtx.WaitOne();
            double XScale = (double)pnlImage.Width / Cam.emgucvImage.Width;
            double YScale = (double)pnlImage.Height / Cam.emgucvImage.Height;
            EmguImageBox.SetZoomScale(Math.Min(XScale, YScale), new Point(0, 0));
            scale = 100;
            mtx.ReleaseMutex();
        }
        public void ZoomIn()
        {
            EmguImageBox.SetZoomScale(EmguImageBox.ZoomScale + 0.1, new Point(EmguImageBox.Width / 2, EmguImageBox.Height / 2));
            scale += 10;
        }
        public void ZoomOut()
        {
            EmguImageBox.SetZoomScale(EmguImageBox.ZoomScale - 0.1, new Point(EmguImageBox.Width / 2, EmguImageBox.Height / 2));
            scale -= 10;
        }

        private void tsbtnReticle_Click(object sender, EventArgs e)
        {
            frmReticle frm = new frmReticle(Idx);

            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.MinimizeBox = false;
            frm.MaximizeBox = false;
            frm.TopMost = true;
            frm.TopLevel = true;

            foreach (var c in frm.Controls)
            {
                switch (c)
                {
                    case PropertyGrid pg: pg.PropertyValueChanged += (a, b) => EmguImageBox.Invalidate(); break;
                    case ToolStrip ts: ts.ItemClicked += (a, b) => EmguImageBox.Invalidate(); break;
                }
            }

            frm.Show();
            frm.BringToFront();
        }

        private void tsZoomFit_Click(object sender, EventArgs e)
        {
            ZoomFit();
        }
        private void tsbtnZoomOut_Click(object sender, EventArgs e)
        {
            ZoomOut();
        }
        private void tsbtnZoomIn_Click(object sender, EventArgs e)
        {
            ZoomIn();
        }

        private void tsbtnSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Bitmap Image (.bmp)|*.bmp|Gif Image (.gif)|*.gif|JPEG Image (.jpeg)|*.jpeg|Png Image (.png)|*.png|Tiff Image (.tiff)|*.tiff|Wmf Image (.wmf)|*.wmf";

            if (sfd.ShowDialog() != DialogResult.OK) return;
            Cam.emgucvImage.Save(sfd.FileName);
            System.Diagnostics.Process.Start(sfd.FileName);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tslblScale.Text = $"Scale: {scale}%";
        }
    }
}
