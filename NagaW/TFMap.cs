using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace NagaW
{
    public enum EMapType
    {
        NONE,
        WAFER_TXT,
    }
    public class TFMap
    {
        public static bool Decode(string filepath, EMapType type, out TMultiLayout layout, out TMAP unitmap, out string mapName, out TFunction function, out PointI ref1colrow) 
        {
            layout = new TMultiLayout();
            unitmap = new TMAP();
            mapName = string.Empty;
            function = new TFunction();
            ref1colrow = new PointI();

            try
            {
                switch (type)
                {
                    case EMapType.WAFER_TXT:
                        {
                            #region
                            var content = File.ReadAllLines(filepath).ToList();
                            var pitch = content[1].Replace("pitch xy = ", "").Split('x').ToList().Select(x => x.Replace("um", "")).Select(x => x.Trim()).ToArray();
                            content.RemoveAll(x => x is "");
                            var map = content.GetRange(4, content.Count - 4);

                            int col = map[0].Length;
                            int row = map.Count;

                            PointD unitpitch = new PointD(double.Parse(pitch[0]) / 1000, -double.Parse(pitch[1]) / 1000);

                            TLayout unitlayout = new TLayout();
                            unitlayout.CR = new PointI(col, row);
                            unitlayout.PitchCol = new PointD(unitpitch.X, 0);
                            unitlayout.PitchRow = new PointD(0, unitpitch.Y);

                            layout.Layouts[1] = unitlayout;

                            TMAP mAP = new TMAP(layout);

                            for (int c = 0; c < col; c++)
                            {
                                for (int r = 0; r < row; r++)
                                {
                                    string state = map[r][c].ToString();
                                    if (char.IsLetter(state.ToCharArray()[0]) /*state is "A"*/) mAP.SetState(new PointI(), new PointI(c, r), EDispState.READY);
                                    else mAP.SetState(new PointI(), new PointI(c, r), EDispState.DEFAULT_SKIP);
                                }
                            }
                            unitmap = new TMAP(mAP);
                            #endregion

                            mapName = $"{Path.GetFileNameWithoutExtension(filepath)} {content[2]} {content[3]}";
                            string info = $"FILENAME:{filepath}\r\nWAFER_DATA:{content[1]}\r\nCol,Rol={col},{row}\r\n{content[2]}\r\n{content[3]}\r\nLoad this map?";
                            if (MessageBox.Show(new Form() { TopMost = true, TopLevel = true }, info, "Ack", MessageBoxButtons.OKCancel) != DialogResult.OK) return false;

                            var firstidx = content[2].IndexOf('(') + 1;
                            var lastidx = content[2].IndexOf(')');
                            var xycount_1 = content[2].Substring(firstidx, lastidx - firstidx).Split(',');
                            //flip XY as map data inverted
                            int countx = 1; int county = 0;
                            PointI ref1 = new PointI(int.Parse(xycount_1[countx]), int.Parse(xycount_1[county]));
                            PointD ref1XY = new PointD((ref1.X - 1) * unitpitch.X, (ref1.Y - 1) * unitpitch.Y);
                            ref1colrow = new PointI(ref1);

                            var firstidx2 = content[3].IndexOf('(') + 1;
                            var lastidx2 = content[3].IndexOf(')');
                            var xycount_2 = content[3].Substring(firstidx2, lastidx2 - firstidx2).Split(',');
                            //flip XY2 as map data inverted
                            PointI ref2 = new PointI(int.Parse(xycount_2[countx]), int.Parse(xycount_2[county]));
                            PointD ref2XY = new PointD((ref2.X - 1) * unitpitch.X, (ref2.Y - 1) * unitpitch.Y);


                            TCmd cmd = new TCmd(ECmd.PAT_ALIGN_ROTARY);
                            cmd.Para[0] = ref1XY.X;
                            cmd.Para[1] = ref1XY.Y;

                            cmd.Para[3] = ref2XY.X;
                            cmd.Para[4] = ref2XY.Y;
                            cmd.Para[9] = 1;

                            function.Cmds.Add(cmd);
                            function.Name = $"PatAlign_Board_{content[1]}";

                            break;
                        }
                    default:
                        {
                            MessageBox.Show("Unknown map type");
                            return false;
                        }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }
    }
}
