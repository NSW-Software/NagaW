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
            CenterCross = 1,
            //CenterCross3 = 7,
            //CenterReticle,
            //Line = 2,
            Cross = 3,
            Circle = 4,
            Rectangle = 5,
            Text = 6
        }
        public EType Type { get; set; }
        public Point Location { get; set; }
        public SizeF Size { get; set; } = new SizeF(1, 1);
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
                            {
                                g.DrawLine(pen, new PointF(half_w, 0), new PointF(half_w, h));
                                g.DrawLine(pen, new PointF(0, half_h), new PointF(w, half_h));
                                break;
                            }
                        case TEReticle.EType.CenterCross:
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
                        case TEReticle.EType.Circle:
                            {
                                RectangleF Rect = new RectangleF(new PointF(half_w, half_h), r.Size);
                                g.DrawArc(pen, (float)-0.5 + Rect.X - (r.Size.Width / 2), (float)-0.5 + Rect.Y - (r.Size.Height / 2), Rect.Width, Rect.Height, 0, 360);

                                break;
                            }
                        case TEReticle.EType.Rectangle:
                            {
                                RectangleF Rect = new RectangleF(new PointF(half_w, half_h), r.Size);
                                g.DrawRectangle(pen, (float)-0.5 + Rect.X - (r.Size.Width / 2), (float)-0.5 + Rect.Y - (r.Size.Height / 2), Rect.Width, Rect.Height);

                                break;
                            }
                        case TEReticle.EType.Text:
                            {
                                g.DrawString(r.Text, new Font(new frmReticle(camNo).Font.FontFamily, r.LineWidth), pen.Brush, r.Location);
                                break;
                            }
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
