using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NagaW
{
    using Emgu.CV;
    using Emgu.CV.Structure;
    using Emgu.CV.Util;

    partial class frmImageSelectBox : Form
    {
        enum EImageSource { Current, Registered };
        EImageSource sourceImage = EImageSource.Current;

        enum EROI { None, Search, Pattern };
        EROI roi = EROI.None;

        public Emgu.CV.Image<Emgu.CV.Structure.Gray, byte> nowImage;
        public Emgu.CV.Image<Emgu.CV.Structure.Gray, byte> regImage;
        public Emgu.CV.Image<Emgu.CV.Structure.Gray, byte> selectedImage;
        Emgu.CV.Image<Emgu.CV.Structure.Gray, byte> imgDisplay;

        private SelectBox selectBox;
        private bool readOnly = false;
        private bool showSelectBox = true;
        private bool showBox = false;
        private static double scale = 1;
        private static Point ofst = new Point(0, 0);

        public int Threshold = -1;
        private string instruction = "";
        private Rectangle[] rects;
        readonly private Rectangle[] rectsOri;

        public frmImageSelectBox()
        {
            InitializeComponent();
        }
        public frmImageSelectBox(Image<Emgu.CV.Structure.Gray, byte> nowImg, Image<Emgu.CV.Structure.Gray, byte> regImg, string instruction, Rectangle[] rects, string[] rectNames):this()
        {
            tscbxImage.ComboBox.DataSource = Enum.GetValues(typeof(EImageSource));
            tscbxROI.ComboBox.DataSource = Enum.GetValues(typeof(EROI));

            imgboxEmgu.Dock = DockStyle.Fill;
            imgboxEmgu.MouseMove += new MouseEventHandler(imgboxEmgu_MouseMove);
            imgboxEmgu.MouseDown += new MouseEventHandler(imgboxEmgu_MouseDown);
            imgboxEmgu.MouseUp += new MouseEventHandler(imgboxEmgu_MouseUp);

            this.selectBox = new SelectBox(imgboxEmgu, new Rectangle(120, 120, 50, 50));
            this.selectBox.AddHandle(new HandleMove());
            this.selectBox.AddHandle(new HandleResizeNWSE());
            this.selectBox.AddHandle(new HandleResizeSouth());
            this.selectBox.AddHandle(new HandleResizeEast());
            this.selectBox.OnBoxChanged += new EventHandler(selectBox_OnBoxChanged);

            nowImage = nowImg.Copy();
            if (regImg != null) regImage = regImg.Copy();
            selectedImage = nowImage.Copy();
            imgboxEmgu.Size = selectedImage.Size;


            this.rects = rects;
            this.rectsOri = new Rectangle[rects.Length];
            for (int i = 0; i < rects.Length; i++)
            {
                this.rectsOri[i] = rects[i];
            }
            this.instruction = instruction;
        }


        private void frmVisionSelectBox_Load(object sender, EventArgs e)
        {
            Text = "Image Select Box";
            tbarThreshold.Value = Threshold; ;

            RefreshImgBoxEmgu();

            lbl_Instruction.Text = instruction;

            ZoomFit();
            GControl.LogForm(this);
        }

        public Rectangle SelectionRect
        {
            get { return selectBox.Rect; }
            set
            {
                selectBox.Rect = value;
                Invalidate();
            }
        }
        public bool ReadOnly
        {
            get { return readOnly; }

            set
            {
                readOnly = value;
                Invalidate();
            }
        }

        #region Select Box Handles 
        void imgboxEmgu_MouseUp(object sender, MouseEventArgs e)
        {
            if (readOnly) return;
            selectBox.OnMouseUp(e);
        }
        void imgboxEmgu_MouseDown(object sender, MouseEventArgs e)
        {
            if (readOnly) return;
            selectBox.OnMouseDown(e);
        }
        void imgboxEmgu_MouseMove(object sender, MouseEventArgs e)
        {
            if (readOnly) return;
            selectBox.OnMouseMove(e);
        }

        public event EventHandler SelectionChanged;
        void selectBox_OnBoxChanged(object sender, EventArgs e)
        {
            if (SelectionChanged != null)
                SelectionChanged(this, e);
        }

        /// <summary>
        /// Select Box
        /// </summary>
        private class SelectBox
        {
            public Rectangle Rect;
            public Color color = Color.Lime;
            private List<SelectBoxHandle> handles = new List<SelectBoxHandle>();
            Control parent;
            SelectBoxHandle activeHandle = null;

            public SelectBox(Control parent, Rectangle rect)
            {
                this.Rect = rect;
                this.parent = parent;
            }

            public void AddHandle(SelectBoxHandle handle)
            {
                handle.SelectBox = this;
                handles.Add(handle);
            }

            public virtual void OnPaint(PaintEventArgs pe, bool drawHandles)
            {
                //Pen p = new Pen(Brushes.Lime, 2.0f);
                Pen p = new Pen(color, 2.0f);
                pe.Graphics.DrawRectangle(p, this.Rect);

                if (drawHandles)
                {
                    foreach (SelectBoxHandle sbh in handles)
                        sbh.OnPaint(pe);
                }
            }

            public bool HitTest(int x, int y)
            {
                return this.Rect.Contains(x, y);
            }

            public virtual void OnMouseMove(MouseEventArgs e)
            {
                bool cursorChanged = false;

                foreach (SelectBoxHandle sbh in handles)
                {
                    int x = (int)((double)(e.X) / scale) + ofst.X;
                    int y = (int)((double)(e.Y) / scale) + ofst.Y;
                    if (sbh.HitTest(x, y))
                    {
                        parent.Cursor = sbh.Cursor;
                        cursorChanged = true;
                    }
                }

                if (!cursorChanged)
                {
                    parent.Cursor = Cursors.Default;
                }

                if (activeHandle != null)
                {
                    activeHandle.OnDragging(e);
                }
            }

            public virtual void OnMouseDown(MouseEventArgs e)
            {
                foreach (SelectBoxHandle sbh in handles)
                {
                    int x = (int)((double)(e.X) / scale) + ofst.X;
                    int y = (int)((double)(e.Y) / scale) + ofst.Y;
                    if (sbh.HitTest(x, y))
                    {
                        activeHandle = sbh;
                    }
                }

                if (activeHandle != null)
                {
                    activeHandle.OnDragStart(e);
                }
            }

            public virtual void OnMouseUp(MouseEventArgs e)
            {
                if (activeHandle != null)
                {
                    activeHandle.OnDragEnd(e);
                    activeHandle = null;

                    if (OnBoxChanged != null)
                        OnBoxChanged(this, null);
                }
            }

            public Control Parent
            {
                get { return parent; }
            }

            public event EventHandler OnBoxChanged;
        }

        /// <summary>
        /// SelectBox Handle
        /// </summary>
        private abstract class SelectBoxHandle
        {
            public const int INFLATE_SIZE = 2;
            private SelectBox sb = null;

            public SelectBox SelectBox
            {
                get { return this.sb; }
                set { this.sb = value; }
            }

            public abstract Rectangle HandleRect { get; }
            public abstract Cursor Cursor { get; }
            public abstract void OnPaint(PaintEventArgs pe);

            public bool HitTest(int x, int y)
            {
                Rectangle inflated = HandleRect;
                inflated.Inflate(INFLATE_SIZE, INFLATE_SIZE);
                return (inflated.Contains(x, y));
            }

            public virtual void OnDragStart(MouseEventArgs e) { }
            public virtual void OnDragEnd(MouseEventArgs e) { }
            public virtual void OnDragging(MouseEventArgs e) { }
        }

        private abstract class HandleResize : SelectBoxHandle
        {
            public const int HANDLE_SIZE = 10;

            public override void OnPaint(PaintEventArgs pe)
            {
                pe.Graphics.FillRectangle(Brushes.White, HandleRect);
                pe.Graphics.DrawRectangle(Pens.Black, HandleRect);
            }

            public override Rectangle HandleRect
            {
                get
                {
                    return new Rectangle(new Point(
                      Position.X - HANDLE_SIZE / 2,
                      Position.Y - HANDLE_SIZE / 2),
                      new Size(HANDLE_SIZE, HANDLE_SIZE));
                }
            }

            protected abstract Point Position { get; }
        }
        private class HandleResizeNWSE : HandleResize
        {
            public override Cursor Cursor { get { return Cursors.SizeNWSE; } }
            protected override Point Position { get { return new Point(SelectBox.Rect.Right, SelectBox.Rect.Bottom); } }

            public override void OnDragging(MouseEventArgs e)
            {
                int x = (int)(double)(e.X / scale) + ofst.X;
                int y = (int)(double)(e.Y / scale) + ofst.Y;
                SelectBox.Rect.Width = x - SelectBox.Rect.X;
                SelectBox.Rect.Height = y - SelectBox.Rect.Y;
                SelectBox.Parent.Invalidate();
                SelectBox.Parent.Refresh();

            }
        }
        private class HandleResizeEast : HandleResize
        {
            public override Cursor Cursor { get { return Cursors.SizeWE; } }
            protected override Point Position { get { return new Point(SelectBox.Rect.Right, SelectBox.Rect.Top + SelectBox.Rect.Height / 2); } }
            public override void OnDragging(MouseEventArgs e)
            {
                int x = (int)(double)(e.X / scale) + ofst.X;
                SelectBox.Rect.Width = x - SelectBox.Rect.X;
                SelectBox.Parent.Invalidate();
                SelectBox.Parent.Refresh();
            }
        }
        private class HandleResizeSouth : HandleResize
        {
            protected override Point Position
            {
                get
                {
                    return new Point(SelectBox.Rect.Left + SelectBox.Rect.Width / 2,
                      SelectBox.Rect.Bottom);
                }
            }

            public override Cursor Cursor { get { return Cursors.SizeNS; } }

            public override void OnDragging(MouseEventArgs e)
            {
                int y = (int)(double)(e.Y / scale) + ofst.Y;
                SelectBox.Rect.Height = y - SelectBox.Rect.Y;
                SelectBox.Parent.Invalidate();
                SelectBox.Parent.Refresh();
            }
        }
        private class HandleMove : SelectBoxHandle
        {
            public override Rectangle HandleRect
            {
                get
                {
                    Rectangle sbr = SelectBox.Rect;
                    Rectangle mine = new Rectangle(sbr.X, sbr.Y, sbr.Width, sbr.Height);
                    return mine;
                }
            }

            public override void OnPaint(PaintEventArgs pe) { return; }
            public override Cursor Cursor { get { return Cursors.SizeAll; } }


            Point dragStart;

            public override void OnDragStart(MouseEventArgs e)
            {
                //dragStart = new Point(e.X - SelectBox.Rect.X, e.Y - SelectBox.Rect.Y);
                int x = (int)(double)(e.X / scale) + ofst.X;
                int y = (int)(double)(e.Y / scale) + ofst.Y;
                dragStart = new Point(x - SelectBox.Rect.X, y - SelectBox.Rect.Y);
            }

            public override void OnDragging(MouseEventArgs e)
            {
                //SelectBox.Rect.X = e.X - dragStart.X;
                //SelectBox.Rect.Y = e.Y - dragStart.Y;
                int x = (int)((double)(e.X) / scale) + ofst.X;
                int y = (int)((double)(e.Y) / scale) + ofst.Y;
                SelectBox.Rect.X = x - dragStart.X;
                SelectBox.Rect.Y = y - dragStart.Y;

                SelectBox.Parent.Invalidate();
                SelectBox.Parent.Refresh();
            }
        }
        #endregion

        private void RefreshImgBoxEmgu()
        {
            int threshold = tbarThreshold.Value;
            tsbtnThreshold.Text = "Threshold = " + threshold.ToString();

            imgDisplay = threshold >= 0 ? selectedImage.ThresholdBinary(new Emgu.CV.Structure.Gray(threshold), new Emgu.CV.Structure.Gray(255)) : selectedImage.Copy();
            imgboxEmgu.Image = imgDisplay;
        }

        private void imgboxEmgu_Paint(object sender, PaintEventArgs e)
        {
            scale = imgboxEmgu.ZoomScale;
            ofst.X = imgboxEmgu.HorizontalScrollBar.Value;
            ofst.Y = imgboxEmgu.VerticalScrollBar.Value;

            if (rects != null)
            {
                for (int i = 0; i < rects.Count(); i++)
                {
                    if (i == (int)roi - 1) continue;

                    Pen p = new Pen(Color.Yellow, 1);
                    e.Graphics.DrawRectangle(p, rects[i]);
                }
            }

            base.OnPaint(e);
            if (showSelectBox)
            {
                selectBox.color = Color.Lime;
                selectBox.OnPaint(e, !readOnly);
            }

            if (showBox)
            {
                showBox = false;
                selectBox.color = Color.Blue;
                selectBox.OnPaint(e, false);
            }
            //Color crossColor = Color.Green;
            //int crossThickness = 5;
            //int crossSize = 20;
            //bool showCross = true;

            //Pen p = new Pen(crossColor, crossThickness);

            //if (showCross)
            //{
            //    Point midPoint = new Point(
            //        selectBox.Rect.Left + selectBox.Rect.Width / 2,
            //        selectBox.Rect.Top + selectBox.Rect.Height / 2);

            //    e.Graphics.DrawLine(
            //        p,
            //        midPoint.X, midPoint.Y - crossSize,
            //        midPoint.X, midPoint.Y + crossSize);

            //    e.Graphics.DrawLine(p, midPoint.X - crossSize, midPoint.Y, midPoint.X + crossSize, midPoint.Y);
            //}
        }
        private void imgboxEmgu_SizeChanged(object sender, EventArgs e)
        {
            ZoomFit();
        }

        private void tsbtnThreshold_Click(object sender, EventArgs e)
        {
             tbarThreshold.Visible = !tbarThreshold.Visible;
        }
        private void tbarThreshold_Scroll(object sender, EventArgs e)
        {
            Threshold = tbarThreshold.Value;
            tsbtnThreshold.Text = "Threshold = " + Threshold.ToString();

            RefreshImgBoxEmgu();
        }
        private void tsbResetROI_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Set Search and Pattern Window to default?", "Reset ROI", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) return;

            Rectangle rect = new Rectangle(100, 100, 200, 200);
            for (int i = 0; i < rects.Count(); i++)
            {
                rect.X += i * 20;
                rect.Y += i * 20;
                rects[i] = rect;
            }

            imgboxEmgu.Invalidate();
        }
        private void tsbZoomM_Click(object sender, EventArgs e)
        {
            imgboxEmgu.SetZoomScale(imgboxEmgu.ZoomScale - 0.2, new Point(imgboxEmgu.Width / 2, imgboxEmgu.Height / 2));
        }
        private void ZoomFit()
        {
            if (imgDisplay == null) return;

            double XScale = (double)imgboxEmgu.Width / imgDisplay.Width;
            double YScale = (double)imgboxEmgu.Height / imgDisplay.Height;
            imgboxEmgu.SetZoomScale(Math.Min(XScale, YScale), new Point(0, 0));
        }
        private void tsbZoomF_Click(object sender, EventArgs e)
        {
            ZoomFit();
        }
        private void tsbZoomP_Click(object sender, EventArgs e)
        {
            imgboxEmgu.SetZoomScale(imgboxEmgu.ZoomScale + 0.2, new Point(imgboxEmgu.Width / 2, imgboxEmgu.Height / 2));
        }

        private void tsbOK_Click(object sender, EventArgs e)
        {
            tscbxImage.ComboBox.SelectedItem = EImageSource.Registered;
            tscbxROI.ComboBox.SelectedItem = EROI.None;

            this.DialogResult = DialogResult.OK;
        }
        private void tsbCancel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < rects.Length; i++)
            {
                this.rects[i] = this.rectsOri[i];
            }
            this.DialogResult = DialogResult.Cancel;
        }

        private void tsbtnMatchImage_Click(object sender, EventArgs e)
        {
            tscbxImage.SelectedItem = EImageSource.Current;
            tscbxROI.ComboBox.SelectedItem = EROI.None;

            PointD pLoc = new PointD(0, 0);
            PointD pLOfst = new PointD(0, 0);
            double score = 0;
            TFVision.PatMatch(nowImage, regImage, Threshold, rects, ref pLoc, ref pLOfst, ref score);

            SelectionRect = new Rectangle((int)pLoc.X, (int)pLoc.Y, rects[1].Width, rects[1].Height);
            showBox = true;

            imgboxEmgu.Invalidate();
            tslblScore.Text = $"Score: {score}";
        }
        private void tsbtnLearnImage_Click(object sender, EventArgs e)
        {
            tscbxImage.ComboBox.SelectedItem = EImageSource.Registered;
            tscbxROI.ComboBox.SelectedItem = EROI.None;

            imgboxEmgu.Invalidate();
        }

        private void tsbtnLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            string InitDir = @"c:\";
            openFileDialog1.InitialDirectory = InitDir;
            openFileDialog1.Filter = "Image Files (*.tif;*.tiff;*.gif;*.bmp;*.jpg;*.jpeg;*.jp2;*.png;)|*.tif;*.tiff;*.gif;*.bmp;*.jpg;*.jpeg;*.jp2;*.png;|All files (*.*)|*.*||";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                nowImage = new Emgu.CV.Image<Emgu.CV.Structure.Gray, byte>(openFileDialog1.FileName);

                if (regImage == null)
                {
                    DialogResult dr = MessageBox.Show("Image is not registered. Register current image?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    switch (dr)
                    {
                        case DialogResult.Yes:
                            regImage = nowImage.Copy();
                            break;
                        case DialogResult.No:
                            break;
                    }
                }

                tscbxImage.SelectedItem = EImageSource.Current;
                tscbxImage_SelectedIndexChanged(sender, e);
                ZoomFit();
            }
        }

        private void tscbxImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            sourceImage = (EImageSource)tscbxImage.ComboBox.SelectedIndex;

            if (nowImage == null) return;

            if (sourceImage == EImageSource.Registered && regImage == null)
            {
                DialogResult dr = MessageBox.Show("Image is not registered. Register current image?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (dr)
                {
                    case DialogResult.Yes:
                        regImage = nowImage.Copy();
                        break;
                    case DialogResult.No:
                        sourceImage = EImageSource.Current;
                        break;
                    case DialogResult.Cancel:
                        return;
                }
            }

            switch (sourceImage)
            {
                case EImageSource.Registered:
                    selectedImage = regImage.Copy();
                    break;
                default:
                    selectedImage = nowImage.Copy();
                    break;
            }

            tsbtnUpdate.Enabled = sourceImage == EImageSource.Current;

            RefreshImgBoxEmgu();
        }
        private void tscbxROI_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (roi > (int)EROI.None)
            {
                rects[(int)roi - 1] = SelectionRect;
            }
            roi = (EROI)tscbxROI.ComboBox.SelectedIndex;

            if (roi > (int)EROI.None)
            {
                tscbxImage.SelectedItem = EImageSource.Registered;
                SelectionRect = rects[(int)roi - 1];
            }

            showSelectBox = tscbxROI.SelectedIndex > 0;

            imgboxEmgu.Invalidate();
        }

        private void tsbtnUpdate_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Update Current Image as Registered Image?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            switch (dr)
            {
                case DialogResult.Yes:
                    regImage = nowImage.Copy();
                    tscbxImage.SelectedItem = EImageSource.Registered;
                    break;
                default:
                    break;
            }
        }
    }
}
