using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace NagaW
{
    using Emgu.CV;
    using Emgu.CV.Structure;
    using System.ComponentModel;
    using System.Linq;

    public partial class frmFlirCamera : Form
    {
        public frmFlirCamera()
        {
            InitializeComponent();

            pnlImage.Dock = DockStyle.Fill;
            imgBoxEmgu.Dock = DockStyle.Fill;
        }

        private void frmFlirCamera_Load(object sender, EventArgs e)
        {
            UpdateControls();

            Link(0);
            GControl.LogForm(this);
        }
        private void frmFlirCamera_ResizeEnd(object sender, EventArgs e)
        {
            ZoomFit();
        }

        int selectedCam = 0;
        public void Link(int camNo)
        {
            selectedCam = camNo;
            TFCameras.Camera[selectedCam].FlirCamera.imgBoxEmgu = imgBoxEmgu;
            imgBoxEmgu.Image = TFCameras.Camera[selectedCam].FlirCamera.emgucvCImage;

            if (selectedCam == 0)
            {
                TFCameras.Camera[1].FlirCamera.Stop();
                TFCameras.Camera[0].FlirCamera.Live();
            };
            if (selectedCam == 1)
            {
                TFCameras.Camera[0].FlirCamera.Stop();
                TFCameras.Camera[1].FlirCamera.Live();
            };


            BackColor = camNo == 0 ? GSystemCfg.Display.LeftColor : GSystemCfg.Display.RightColor;

            ZoomFit();
        }

        private void UpdateControls()
        {
            try
            {
                enableTrigModeToolStripMenuItem.Checked = TFCameras.Camera[selectedCam].FlirCamera.TrigMode;
                sourceSwToolStripMenuItem.Checked = TFCameras.Camera[selectedCam].FlirCamera.TrigSourceSw;
                sourceHwToolStripMenuItem.Checked = TFCameras.Camera[selectedCam].FlirCamera.TrigSourceHw;
            }
            catch { };
        }


        private void liveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TFCameras.Camera[selectedCam].FlirCamera.Live();
            imgBoxEmgu.Image = TFCameras.Camera[selectedCam].FlirCamera.emgucvCImage;
            UpdateControls();
        }
        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TFCameras.Camera[selectedCam].FlirCamera.Stop();
            imgBoxEmgu.Image = TFCameras.Camera[selectedCam].FlirCamera.emgucvCImage;
            UpdateControls();
        }
        private void trigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TFCameras.Camera[selectedCam].FlirCamera.SwTrig(500);
            imgBoxEmgu.Image = TFCameras.Camera[selectedCam].FlirCamera.emgucvCImage;
            UpdateControls();
        }

        public void ZoomFit()
        {
            if (TFCameras.Camera[selectedCam].FlirCamera == null) return;

            double XScale = (double)pnlImage.Width / TFCameras.Camera[selectedCam].FlirCamera.emgucvImage.Width;
            double YScale = (double)pnlImage.Height / TFCameras.Camera[selectedCam].FlirCamera.emgucvImage.Height;
            imgBoxEmgu.SetZoomScale(Math.Min(XScale, YScale), new Point(0, 0));
        }
        public void ZoomActual()
        {
            imgBoxEmgu.SetZoomScale(1, new Point(0, 0));
        }
        public void ZoomIn()
        {
            imgBoxEmgu.SetZoomScale(imgBoxEmgu.ZoomScale + 0.2, new Point(imgBoxEmgu.Width / 2, imgBoxEmgu.Height / 2));
        }
        public void ZoomOut()
        {
            imgBoxEmgu.SetZoomScale(imgBoxEmgu.ZoomScale - 0.2, new Point(imgBoxEmgu.Width / 2, imgBoxEmgu.Height / 2));
        }
        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZoomOut();
        }
        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZoomIn();
        }
        private void zoomFitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZoomFit();
        }

        private void enableTrigModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TFCameras.Camera[selectedCam].FlirCamera.TrigMode = !TFCameras.Camera[selectedCam].FlirCamera.TrigMode;
                //TFCameras.Camera[selectedCam].FlirCamera.Grab(500);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            UpdateControls();
        }
        private void toolStripComboBox1_DropDownClosed(object sender, EventArgs e)
        {

        }

        private void imgBoxEmgu_Paint(object sender, PaintEventArgs e)
        {
            Text = TFCameras.Camera[selectedCam].FlirCamera.dFPS.ToString("f1") + " Hz";

            double w = imgBoxEmgu.Image.GetInputArray().GetSize().Width;
            double h = imgBoxEmgu.Image.GetInputArray().GetSize().Height;

            TFReticle.DrawReticle(selectedCam, (float)w, (float)h, e.Graphics);

            //try
            //{
            //    foreach (var r in GRecipes.Reticle[selectedCam])
            //    {
            //        if (!r.Enable) continue;

            //        Pen pen = new Pen(r.Color, r.LineWidth);
            //        switch (r.Type)
            //        {
            //            default: break;
            //            case TReticle.EType.Cross:
            //                {
            //                    e.Graphics.DrawLine(pen, new Point((int)(w / 2), 0), new Point((int)(w / 2), (int)h));
            //                    e.Graphics.DrawLine(pen, new Point(0, (int)(h / 2)), new Point((int)w, (int)(h / 2)));

            //                    break;
            //                }
            //            case TReticle.EType.Circle:
            //                {
            //                    RectangleF Rect = new RectangleF(new PointF((float)w / 2, (float)h / 2), r.Size);
            //                    e.Graphics.DrawArc(pen, (float)-0.5 + Rect.X - (r.Size.Width / 2), (float)-0.5 + Rect.Y - (r.Size.Height / 2), Rect.Width, Rect.Height, 0, 360);

            //                    break;
            //                }
            //            case TReticle.EType.Rectangle:
            //                {
            //                    RectangleF Rect = new RectangleF(new PointF((float)w / 2, (float)h / 2), r.Size);
            //                    e.Graphics.DrawRectangle(pen, (float)-0.5 + Rect.X - (r.Size.Width / 2), (float)-0.5 + Rect.Y - (r.Size.Height / 2), Rect.Width, Rect.Height);
            //                    break;
            //                }
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    //GLog.Log(ex);
            //    GLog.WriteException(ex);
            //}

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            string InitDir = @"c:\Images\";
            if (!Directory.Exists(InitDir)) try { Directory.CreateDirectory(InitDir); } catch { }
            openFileDialog1.InitialDirectory = InitDir;
            openFileDialog1.Filter = "Image Files (*.tif;*.tiff;*.gif;*.bmp;*.jpg;*.jpeg;*.jp2;*.png;)|*.tif;*.tiff;*.gif;*.bmp;*.jpg;*.jpeg;*.jp2;*.png;|All files (*.*)|*.*||";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                TFCameras.Camera[selectedCam].FlirCamera.GrabStop();
                UpdateControls();

                imgBoxEmgu.Image = new Image<Gray, byte>(openFileDialog1.FileName);
                ZoomFit();

                imgBoxEmgu.Refresh();
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog openFileDialog1 = new SaveFileDialog();

            string InitDir = @"c:\Images\";
            if (!Directory.Exists(InitDir)) try { Directory.CreateDirectory(InitDir); } catch { }
            openFileDialog1.InitialDirectory = InitDir;
            openFileDialog1.Filter = "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|BMP (*.bmp)|*.bmp|PNG (*.png)|*.png|All files (*.*)|*.*||";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ((Image<Bgr, byte>)imgBoxEmgu.Image).Save(openFileDialog1.FileName);
            }
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
            UpdateControls();
        }

        private void imgBoxEmgu_MouseDown(object sender, MouseEventArgs e)
        {
            var camcfg = GSystemCfg.Camera.Cameras[0];
            var distPerPixelX = camcfg.DistPerPixelX;
            var distPerPixelY = camcfg.DistPerPixelY;

            if (this.imgBoxEmgu.Image == null) return;

            if (e.Button == MouseButtons.Left)
            {
                return;
            }

            if (e.Button == MouseButtons.Right)
            {
                PointD mouseloc = new PointD(imgBoxEmgu.HorizontalScrollBar.Value + (double)(e.Location.X / imgBoxEmgu.ZoomScale), imgBoxEmgu.VerticalScrollBar.Value + (double)(e.Location.Y / imgBoxEmgu.ZoomScale));
                double scale = imgBoxEmgu.ZoomScale;
                int X = imgBoxEmgu.HorizontalScrollBar.Value;
                int Y = imgBoxEmgu.VerticalScrollBar.Value;
                PointD centerLoc = new PointD(imgBoxEmgu.Image.GetInputArray().GetSize().Width / 2, imgBoxEmgu.Image.GetInputArray().GetSize().Height / 2);

                PointD pixelOfst = mouseloc - centerLoc;
                PointD actOfst = new PointD(pixelOfst.X * distPerPixelX, -pixelOfst.Y * distPerPixelY);

                try
                {
                    TFGantry.GantrySelect.MoveOpXYRel(actOfst.ToArray);
                } 
                catch (Exception ex) { MessageBox.Show(ex.Message, ToString()); };
            }
        }

        private void tsmiReticle_Click(object sender, EventArgs e)
        {
            var frmR = Application.OpenForms.OfType<frmReticle>().FirstOrDefault();
            if (frmR is null)
            {
                frmR = new frmReticle(selectedCam)
                {
                    TopLevel = true,
                    TopMost = true,
                };
                frmR.Show();
            }
            else
            {
                frmR.BringToFront();
                return;
            }


            Timer tmr = new Timer();
            tmr.Tick += (a, b) => this.Refresh();
            tmr.Enabled = true;
            frmR.FormClosing += (a, b) =>
            {
                tmr.Enabled = false;
                tmr.Dispose();
                this.Refresh();
            };
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            selectedCam = 1;
            Link(1);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            selectedCam = 0;
            Link(0);
        }

        private void sourceSwToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TFCameras.Camera[selectedCam].FlirCamera.TrigSourceSw = true;
            UpdateControls();
        }

        private void sourceHwToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TFCameras.Camera[selectedCam].FlirCamera.TrigSourceHw = true;
            UpdateControls();
        }
    }
}
