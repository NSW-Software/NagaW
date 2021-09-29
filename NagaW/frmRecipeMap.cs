using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace NagaW
{
    public partial class frmRecipeMap : Form
    {
        TEZMCAux.TGroup gantry = null;
        Inst.TBoard instBoard = null;

        public TMAP Map = null;
        bool Instant = false;
        double MapScale = 5;
        List<TMapInfo> MapInfos = new List<TMapInfo>();

        PointI runClusterCR = new PointI(0, 0);
        PointI runUnitCR = new PointI(0, 0);
        PointI selectedClusterCR = new PointI(0, 0);
        PointI selectedUnitCR = new PointI(0, 0);
        public frmRecipeMap()
        {
            InitializeComponent();
        }
        public frmRecipeMap(TEZMCAux.TGroup gantry, TMAP map) : this()
        {
            TFunction Function = new TFunction();
            this.gantry = gantry;
            instBoard = Inst.Board[gantry.Index];

            Map = map;
        }
        public frmRecipeMap(TEZMCAux.TGroup gantry, bool InstMode) : this()
        {
            TFunction Function = new TFunction();
            this.gantry = gantry;
            instBoard = Inst.Board[gantry.Index];

            Instant = InstMode;

            Timer tmr = new Timer();
            tmr.Interval = 500;
            tmr.Tick += (a, b) =>
            {
                Map = instBoard.MAP;

                int tableBase = gantry.Index * 10;
                runClusterCR = new PointI(TEZMCAux.Table(tableBase + 2), TEZMCAux.Table(tableBase + 3));
                runUnitCR = new PointI(TEZMCAux.Table(tableBase + 0), TEZMCAux.Table(tableBase + 1));

                Color on = Color.Lime;
                Color off = Color.Transparent;
                tsbtnGoto.BackColor = GotoPos ? on : off;
            };
            tmr.Enabled = true;


            selectedClusterCR = runClusterCR;
            selectedUnitCR = runUnitCR;
            UpdateDisplay();
        }

        private void frmRecipeMap_Load(object sender, EventArgs e)
        {
            picBox.Location = new Point(15, 15);
            AlignedPos = true;
            this.DoubleBuffered = true;
            GControl.LogForm(this);

            picBox.Paint += (a, b) =>
            {
                RefreshMap(b.Graphics);
            };
            RefreshUI();
        }
        private void UpdateDisplay()
        {
            tsslUnit.Text = $"Current Cluster: {selectedClusterCR.X + 1},{selectedClusterCR.Y + 1} Unit: {selectedUnitCR.X + 1},{selectedUnitCR.Y + 1}";
        }
        private void MoveCR()
        {
            int layoutNo = GRecipes.Functions[gantry.Index][instBoard.FuncNo].LayoutNo;

            PointD unitRel = GRecipes.MultiLayout[gantry.Index][layoutNo].Unit.RelPos(instBoard.UnitCR);
            PointD clusterRel = GRecipes.MultiLayout[gantry.Index][layoutNo].Cluster.RelPos(instBoard.ClusterCR);
            PointD origin = GRecipes.Board[gantry.Index].StartPos.GetPointD() + clusterRel + unitRel;

            gantry.MoveOpXYAbs(origin.ToArray);
        }

        private void lblGotoClusterC_Click(object sender, EventArgs e)
        {
            var maxc = GRecipes.MultiLayout[gantry.Index][GRecipes.Functions[gantry.Index][instBoard.FuncNo].LayoutNo].Cluster.CR.X;
            IPara para = new IPara("Goto Cluster Column", instBoard.ClusterCR.X + 1, 1, maxc, EUnit.COUNT);
            if (GLog.SetPara(ref para)) instBoard.ClusterCR.X = para.Value - 1;
        }
        private void lblGotoClusterR_Click(object sender, EventArgs e)
        {
            var maxr = GRecipes.MultiLayout[gantry.Index][GRecipes.Functions[gantry.Index][instBoard.FuncNo].LayoutNo].Cluster.CR.Y;
            IPara para = new IPara("Goto Cluster Row", instBoard.ClusterCR.Y + 1, 1, maxr, EUnit.COUNT);
            if (GLog.SetPara(ref para)) instBoard.ClusterCR.Y = para.Value - 1;
        }
        private void btnLayoutCRGoto_Click(object sender, EventArgs e)
        {
            MoveCR();
        }
        private void lblGotoUnitC_Click(object sender, EventArgs e)
        {
            var maxc = GRecipes.MultiLayout[gantry.Index][GRecipes.Functions[gantry.Index][instBoard.FuncNo].LayoutNo].Unit.CR.X;

            IPara para = new IPara("Goto Unit Column", instBoard.UnitCR.X + 1, 1, maxc, EUnit.COUNT);
            if (GLog.SetPara(ref para)) instBoard.UnitCR.X = para.Value - 1;
        }
        private void lblGotoUnitR_Click(object sender, EventArgs e)
        {
            var maxr = GRecipes.MultiLayout[gantry.Index][GRecipes.Functions[gantry.Index][instBoard.FuncNo].LayoutNo].Unit.CR.Y;

            IPara para = new IPara("Goto Unit Row", instBoard.UnitCR.Y + 1, 1, maxr, EUnit.COUNT);
            if (GLog.SetPara(ref para)) instBoard.UnitCR.Y = para.Value - 1;
        }
        private void btnLayoutCRReset_Click(object sender, EventArgs e)
        {
            instBoard.UnitCR = new PointI();
            instBoard.ClusterCR = new PointI();
        }

        public void UpdateMapFromOutside(TMAP map)
        {
            Map = map;
            RefreshUI();
        }
        private void RefreshMap(Graphics g)
        {
            if (Map is null) return;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var cluster = new TLayout(Map.ClusterL);
            var unit = new TLayout(Map.UnitL);

            double scale = 10;
            scale = Math.Max(0, this.MapScale);

            var sw = Stopwatch.StartNew();
            #region Do Scale
            List<PointF> pF = new List<PointF>();
            {
                var clusterCR = new PointI();
                while (true)
                {
                    cluster.PitchCol.Y = 0; cluster.PitchRow.X = 0;
                    var c_relpos = cluster.RelPos(clusterCR);
                    float c_x = (float)(c_relpos.X * scale), c_y = -1 * (float)(c_relpos.Y * scale);

                    var unitCR = new PointI();
                    while (true)
                    {
                        unit.PitchCol.Y = 0; unit.PitchRow.X = 0;
                        var relpos = unit.RelPos(new PointI(unitCR.X, unitCR.Y));

                        PointF location = new PointF((float)(relpos.X * scale) + c_x, -1 * (float)(relpos.Y * scale) + c_y);
                        pF.Add(location);

                        unitCR = unit.NextCR(unitCR);
                        if (unitCR.IsZero) break;
                    }
                    clusterCR = cluster.NextCR(clusterCR);
                    if (clusterCR.IsZero) break;
                }
            }
            var TopLeft = pF.OrderBy(x => x.X).OrderBy(x => x.Y).FirstOrDefault();
            var BtmRight = pF.OrderBy(x => x.X).OrderBy(x => x.Y).LastOrDefault();

            #endregion
            Console.Write("Do Scale\t");
            Console.WriteLine(sw.ElapsedMilliseconds);

            sw = Stopwatch.StartNew();
            #region Do Paint

            Pen paint = new Pen(Color.Navy);
            Pen p_linePath = new Pen(Color.DarkMagenta) { Width = (float)(scale * 0.35) };
            Pen p_unitIndex = new Pen(Color.BlueViolet);
            Font f_unitIndex = new Font(Font.FontFamily, (float)(scale * 0.7));

            SolidBrush sb_unitIndex = new SolidBrush(Color.Navy);

            {
                MapInfos.Clear();
                var clusterCR = new PointI();
                while (true)
                {
                    var c_relpos = cluster.RelPos(clusterCR);
                    float c_x = (float)(c_relpos.X * scale), c_y = -1 * (float)(c_relpos.Y * scale);

                    var unitCR = new PointI();
                    int unit_no = 0;

                    int index = Map.GetClusterIndex(clusterCR);
                    while (true)
                    {
                        //Indicate current run CR
                        paint.Width = clusterCR.IsEqual(runClusterCR) && unitCR.IsEqual(runUnitCR) ? 5 : 1;

                        var relpos = unit.RelPos(new PointI(unitCR.X, unitCR.Y));

                        PointF location = new PointF((float)(relpos.X * scale) + c_x, -1 * (float)(relpos.Y * scale) + c_y);
                        location.X -= TopLeft.X; location.Y -= TopLeft.Y;   //offset

                        SizeF size = new SizeF(Math.Abs((float)(unit.PitchCol.X * 0.85 * scale)), Math.Abs((float)(unit.PitchRow.Y * 0.85 * scale)));
                        RectangleF rec = new RectangleF(location, size);

                        Color clr = Color.Lime;

                        var unitState = Map.GetState(clusterCR, unitCR);
                        switch (unitState)
                        {
                            case EDispState.NG: clr = Color.Red; break;

                            case EDispState.INSTANT_SKIP: clr = Color.FromArgb(150, Color.RosyBrown); break;
                            case EDispState.DEFAULT_SKIP: clr = Color.FromArgb(175, Color.Gray); break;

                            case EDispState.READY: clr = Color.AliceBlue; break;
                            case EDispState.COMPLETE: clr = Color.Lime; break;
                            case EDispState.PROCESSING: clr = Color.Yellow; break;
                        }

                        if (unitState == EDispState.DEFAULT_SKIP || unitState == EDispState.INSTANT_SKIP)
                        {
                            g.DrawLine(paint, location, new PointF(location.X + size.Width, location.Y + size.Height));
                            g.DrawLine(paint, new PointF(location.X + size.Width, location.Y), new PointF(location.X, location.Y + size.Height));
                        }

                        g.DrawRectangle(paint, location.X, location.Y, size.Width, size.Height);
                        g.FillRectangle(new SolidBrush(clr), rec);

                        string c = $"{index + 1}\r\n";
                        string info = $"{unit_no++ + 1}";

                        var map = new TMapInfo
                        {
                            Rec = rec,
                            ClusterCR = clusterCR,
                            UnitCR = unitCR
                        };
                        MapInfos.Add(map);

                        unitCR = unit.NextCR(unitCR);
                        if (unitCR.IsZero) break;
                    }
                    clusterCR = cluster.NextCR(clusterCR);
                    if (clusterCR.IsZero) break;
                }
            }
            #endregion
            Console.Write("Do Paint\t");
            Console.WriteLine(sw.ElapsedMilliseconds);

            sw = Stopwatch.StartNew();
            #region Select Rect
            g.FillRectangle(new SolidBrush(Color.FromArgb(128, 0, 0, 100)), SelectRec);

            int iskip = (int)EDispState.INSTANT_SKIP;
            int dskip = (int)EDispState.DEFAULT_SKIP;
            int ready = (int)EDispState.READY;

            if (!Selecting)
            {
                MapInfos.ToList().ForEach(x =>
                {
                    if (x.Rec.Right > SelectRec.Left && x.Rec.Left < SelectRec.Right
                    && x.Rec.Top < SelectRec.Bottom && x.Rec.Bottom > SelectRec.Top)
                    {
                        if (Sync)
                        {
                            foreach (var m in Map.Maps)
                            {
                                if (Instant)
                                {
                                    if (m.Map[x.UnitCR.X, x.UnitCR.Y] == ready) m.Map[x.UnitCR.X, x.UnitCR.Y] = iskip;
                                    else if (m.Map[x.UnitCR.X, x.UnitCR.Y] == iskip) m.Map[x.UnitCR.X, x.UnitCR.Y] = ready;
                                }
                                else
                                {
                                    m.Map[x.UnitCR.X, x.UnitCR.Y] = m.Map[x.UnitCR.X, x.UnitCR.Y] == ready ? dskip : ready;
                                }
                            }
                        }
                        else
                        {
                            var map = Map.Maps[Map.GetClusterIndex(x.ClusterCR)];

                            if (Instant)
                            {
                                if (map.Map[x.UnitCR.X, x.UnitCR.Y] == ready) map.Map[x.UnitCR.X, x.UnitCR.Y] = iskip;
                                else if (map.Map[x.UnitCR.X, x.UnitCR.Y] == iskip) map.Map[x.UnitCR.X, x.UnitCR.Y] = ready;

                            }
                            else
                            {
                                map.Map[x.UnitCR.X, x.UnitCR.Y] = map.Map[x.UnitCR.X, x.UnitCR.Y] == ready ? dskip : ready;
                            }
                        }
                    }
                });
                SelectRec = new Rectangle();
                Selecting = true;
                RefreshUI();
            }

            #endregion
            Console.Write("Do Recta\t");
            Console.WriteLine(sw.ElapsedMilliseconds);

            //SizeF s = new SizeF(BtmRight.X - TopLeft.X + (int)(5 * MapScale) + (int)unit.PitchCol.X, (BtmRight.Y - TopLeft.Y) + (int)(5 * MapScale));
            SizeF s = new SizeF(BtmRight.X - TopLeft.X + (int)(MapScale*Math.Abs(unit.PitchCol.X)), (BtmRight.Y - TopLeft.Y) + (int)(MapScale * Math.Abs(unit.PitchRow.Y)));

            picBox.Size = s.ToSize();
        }
        public void RefreshUI()
        {
            picBox.Invalidate();
        }

        Rectangle SelectRec = new Rectangle();
        Point PtDown = new Point();
        Point PtMove = new Point();
        bool Selecting;
        private void picBox_MouseDown(object sender, MouseEventArgs e)
        {
            PtDown = e.Location;
            SelectRec = new Rectangle(PtDown.X, PtDown.Y, 0, 0);
            Selecting = true;

            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        GotoPos = !GotoPos;
                        return;
                    }
                case MouseButtons.Left:
                    {
                        Selecting = true;

                        if (GotoPos)
                        {
                            if (Map is null) return;

                            TMapInfo map = MapInfos.Where(x => x.Rec.Right > SelectRec.Left && x.Rec.Left < SelectRec.Right && x.Rec.Top < SelectRec.Bottom && x.Rec.Bottom > SelectRec.Top).FirstOrDefault();

                            if (map is null) return;

                            this.Enabled = false;

                            var clusterCR = map.ClusterCR;
                            var unitCR = map.UnitCR;

                            var instLayout = Inst.Board[gantry.Index].CurrentMLayout;

                            PointD clusterRel = instLayout.Cluster.RelPos(clusterCR);
                            PointD unitRel = instLayout.Unit.RelPos(unitCR);

                            var aligndata = Inst.Board[gantry.Index].LayerData[instLayout.Index].GetUnitAlign(new PointI[] { clusterCR, unitCR });
                            if (AlignedPos) unitRel = new TFunction().Translate(unitRel, Inst.Board[gantry.Index].LayerData[instLayout.Index].GetUnitAlign(new PointI[] { clusterCR, unitCR }));

                            PointD origin = GRecipes.Board[gantry.Index].StartPos.GetPointD() + clusterRel + unitRel;

                            if (!gantry.MoveOpZAbs(GRecipes.Board[gantry.Index].StartPos.Z)) return;
                            gantry.MoveOpXYAbs(origin.ToArray);
                            this.Enabled = true;

                            selectedClusterCR = clusterCR;
                            selectedUnitCR = unitCR;
                            UpdateDisplay();

                            Selecting = false;
                        }

                        break;
                    }

            }
        }
        private void picBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (GotoPos) return;
            if (e.Button != MouseButtons.Left) return;
            PtMove = e.Location;

            SelectRec = new Rectangle(Math.Min(PtDown.X, PtMove.X), Math.Min(PtDown.Y, PtMove.Y), Math.Max(PtDown.X, PtMove.X) - Math.Min(PtDown.X, PtMove.X), Math.Max(PtDown.Y, PtMove.Y) - Math.Min(PtDown.Y, PtMove.Y));
            RefreshUI();
        }
        private void picBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (GotoPos) return;
            Selecting = false;
            RefreshUI();
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //UpdateMapping(e.Graphics);
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    this.Refresh();
        //}

        //private void lblStartC_Click(object sender, EventArgs e)
        //{
        //    var maxr = GRecipes.MultiLayout[gantry.Index][GRecipes.Functions[gantry.Index][instBoard.FunNo].LayoutNo].Cluster.CR.X;
        //    IPara para = new IPara("Start Col", TCDisp.Run[gantry.Index].StartClusterCR.X + 1, 1, maxr, EUnit.COUNT);
        //    if (GLog.SetPara(ref para)) TCDisp.Run[gantry.Index].StartClusterCR.X = para.Value - 1;
        //}

        //private void lblStartR_Click(object sender, EventArgs e)
        //{
        //    var maxc = GRecipes.MultiLayout[gantry.Index][GRecipes.Functions[gantry.Index][instBoard.FunNo].LayoutNo].Cluster.CR.Y;
        //    IPara para = new IPara("Start Row", TCDisp.Run[gantry.Index].StartClusterCR.Y + 1, 1, maxc, EUnit.COUNT);
        //    if (GLog.SetPara(ref para)) TCDisp.Run[gantry.Index].StartClusterCR.Y = para.Value - 1;
        //}

        //private void lblStartUnitC_Click(object sender, EventArgs e)
        //{
        //    var maxr = GRecipes.MultiLayout[gantry.Index][GRecipes.Functions[gantry.Index][instBoard.FunNo].LayoutNo].Unit.CR.X;
        //    IPara para = new IPara("Start Col", TCDisp.Run[gantry.Index].StartUnitCR.X + 1, 1, maxr, EUnit.COUNT);
        //    if (GLog.SetPara(ref para)) TCDisp.Run[gantry.Index].StartUnitCR.X = para.Value - 1;
        //}

        //private void lblStartUnitR_Click(object sender, EventArgs e)
        //{
        //    var maxc = GRecipes.MultiLayout[gantry.Index][GRecipes.Functions[gantry.Index][instBoard.FunNo].LayoutNo].Unit.CR.Y;
        //    IPara para = new IPara("Start Row", TCDisp.Run[gantry.Index].StartUnitCR.Y + 1, 1, maxc, EUnit.COUNT);
        //    if (GLog.SetPara(ref para)) TCDisp.Run[gantry.Index].StartUnitCR.Y = para.Value - 1;
        //}

        bool GotoPos;
        bool Sync;
        bool AlignedPos;
        private void tsbtnClearAll_Click(object sender, EventArgs e)
        {
            if (Map is null) return;

            if (Instant) Map.SetAll(EDispState.READY, EDispState.INSTANT_SKIP);
            else Map.SetAll(EDispState.READY);
            RefreshUI();
        }

        private void tsbtnZoomMinus_Click(object sender, EventArgs e)
        {
            MapScale /= 1.5;
            RefreshUI();
        }

        private void tsbtnZoomFit_Click(object sender, EventArgs e)
        {
            MapScale = (this.Width / 150) - 2;
            RefreshUI();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            MapScale *= 1.5;
            RefreshUI();
        }

        private void tsbtnGoto_Click(object sender, EventArgs e)
        {
            GotoPos = !GotoPos;
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }

    public class TMapInfo
    {
        public RectangleF Rec { get; set; } = new RectangleF();
        public PointI ClusterCR { get; set; } = new PointI();
        public PointI UnitCR { get; set; } = new PointI();
        public TMapInfo()
        {
        }
    }
}
