using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NagaW
{
    public class TEReticle
    {
        public enum EType
        {
            None = 0,
            CrossCentre = 1,
            Cross = 3,
            Circle = 4,
            Rectangle = 5,
            Text = 6,
            CrossSplit = 47,
        }

        public EType Type { get; set; }
        public Point Location { get; set; }
        public SizeF Size { get; set; } = new SizeF(50, 50);
        public string Text { get; set; } = string.Empty;
        public Color Color { get; set; } = Color.Yellow;
        public int LineWidth { get; set; } = 1;
        public bool Enable { get; set; }
        public TEReticle()
        {
        }
        public TEReticle(TEReticle Reticle)
        {
            Type = Reticle.Type;
            Location = Reticle.Location;
            Size = Reticle.Size;
            Text = Reticle.Text;
            Color = Reticle.Color;
            LineWidth = Reticle.LineWidth;
            Enable = Reticle.Enable;
        }
        public TEReticle(EType Type, Point Location, SizeF Size, Color Color, string Text = "")
        {
            this.Type = Type;
            this.Location = Location;
            this.Size = Size;
            this.Text = Text;
            this.Color = Color;
        }
    }

    public class TFReticle
    {
        public static void DrawReticle(int camNo, float w, float h, Graphics g)
        {
            try
            {
                foreach (var r in GRecipes.Reticle[camNo])
                {
                    var half_w = (float)((w / 2) - 0.5 + r.Location.X);
                    var half_h = (float)((h / 2) - 0.5 + r.Location.Y);

                    if (!r.Enable) continue;

                    Pen pen = new Pen(r.Color, r.LineWidth);

                    switch (r.Type)
                    {
                        default: break;
                        case TEReticle.EType.Cross:
                            #region
                            {
                                g.DrawLine(pen, new PointF(half_w, 0), new PointF(half_w, h));
                                g.DrawLine(pen, new PointF(0, half_h), new PointF(w, half_h));
                                break;
                            }
                        #endregion
                        case TEReticle.EType.CrossCentre:
                            #region
                            {
                                g.DrawLine(pen, new PointF(half_w, (float)(h * 0.4)), new PointF(half_w, (float)(h * 0.6)));
                                g.DrawLine(pen, new PointF((float)(w * 0.4), half_h), new PointF((float)(w * 0.6), half_h));

                                for (int i = 0; i < 10; i++)
                                {
                                    if (i < 5 && i % 2 is 0)
                                    {
                                        g.DrawLine(pen, new PointF(half_w, h * i / 10), new PointF(half_w, h * (i + 1) / 10));
                                        g.DrawLine(pen, new PointF(w * i / 10, half_h), new PointF(w * (i + 1) / 10, half_h));
                                    }
                                    if (i > 5 && i % 2 != 0)
                                    {
                                        g.DrawLine(pen, new PointF(half_w, h * i / 10), new PointF(half_w, h * (i + 1) / 10));
                                        g.DrawLine(pen, new PointF(w * i / 10, half_h), new PointF(w * (i + 1) / 10, half_h));
                                    }
                                }
                                break;
                            }
                        #endregion
                        case TEReticle.EType.Circle:
                            #region
                            {
                                var camx = (float)GSystemCfg.Camera.Cameras[camNo].DistPerPixelX * 1000;
                                var camy = (float)GSystemCfg.Camera.Cameras[camNo].DistPerPixelX * 1000;
                                SizeF newsize = new SizeF(r.Size.Width / camx, r.Size.Height / camy);

                                RectangleF Rect = new RectangleF(new PointF(half_w, half_h), newsize);
                                g.DrawArc(pen, (float)-0.5 + Rect.X - (newsize.Width / 2), (float)-0.5 + Rect.Y - (newsize.Height / 2), Rect.Width, Rect.Height, 0, 360);

                                break;
                            }
                        #endregion
                        case TEReticle.EType.Rectangle:
                            #region
                            {
                                var camx = (float)GSystemCfg.Camera.Cameras[camNo].DistPerPixelX * 1000;
                                var camy = (float)GSystemCfg.Camera.Cameras[camNo].DistPerPixelX * 1000;
                                SizeF newsize = new SizeF(r.Size.Width / camx, r.Size.Height / camy);

                                RectangleF Rect = new RectangleF(new PointF(half_w, half_h), newsize);
                                g.DrawRectangle(pen, (float)-0.5 + Rect.X - (newsize.Width / 2), (float)-0.5 + Rect.Y - (newsize.Height / 2), Rect.Width, Rect.Height);

                                break;
                            }
                        #endregion
                        case TEReticle.EType.Text:
                            #region
                            {
                                string[] texts = r.Text.Split('@');

                                for (int i = 0; i < texts.Length; i++)
                                {
                                    Point loc = new Point(r.Location.X, r.Location.Y + ((r.LineWidth + 10) * i));
                                    g.DrawString(texts[i], new Font(new frmReticle(camNo).Font.FontFamily, r.LineWidth), pen.Brush, loc);
                                }

                                //g.DrawString(r.Text, new Font(new frmReticle(camNo).Font.FontFamily, r.LineWidth), pen.Brush, r.Location);

                                break;
                            }
                        #endregion
                        case TEReticle.EType.CrossSplit:
                            #region
                            {
                                float offset = 0.5F;

                                var camx = (float)GSystemCfg.Camera.Cameras[0].DistPerPixelX * 1000;
                                float pitchx = r.Size.Width / camx;
                                for (float i = half_w; i < w; i += pitchx)
                                {
                                    g.DrawLine(pen, new PointF(i - offset, half_h + pitchx - offset), new PointF(i - offset, half_h - pitchx - offset));
                                }
                                for (float i = half_w; i > 0; i -= pitchx)
                                {
                                    g.DrawLine(pen, new PointF(i - offset, half_h + pitchx - offset), new PointF(i - offset, half_h - pitchx - offset));
                                }

                                var camy = (float)GSystemCfg.Camera.Cameras[0].DistPerPixelY * 1000;
                                float pitchy = r.Size.Height / camy;
                                for (float i = half_h; i < h; i += pitchy)
                                {
                                    g.DrawLine(pen, new PointF(half_w - pitchy - offset, i - offset), new PointF(half_w + pitchy - offset, i - offset));
                                }
                                for (float i = half_h; i > 0; i -= pitchy)
                                {
                                    g.DrawLine(pen, new PointF(half_w - pitchy - offset, i - offset), new PointF(half_w + pitchy - offset, i - offset));
                                }
                                break;
                            }
                            #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                GLog.WriteException(ex);
            }
        }
    }
}
