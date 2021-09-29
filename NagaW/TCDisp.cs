using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NagaW
{
    public enum ERunMode { Camera = 0, Dry = 1, Normal = 2 }
    public enum ERunSelect { All, Func, /*Cluster, Unit,*/ Continuous }
    public enum EHeightAlignStatus { None, Processing, Aligned, Error, NG }
    public enum EPatAlignStatus { None, BoardOK, ClusterOK, OK, FailScore, FailOffset, FailScore2, FailOffset2, FailAngle, Error, NG };

    //public enum EDispState
    //{
    //    READY = 0,
        
    //    INSTANT_SKIP = 5,
    //    NG = 55,
        
    //    FAIL = 101,
    //    SKIP = 200,
    //    COMPLETE = 250,
    //}

    public class TAlignData
    {
        public EPatAlignStatus Status = EPatAlignStatus.None;
        public PointD Datum = new PointD(0, 0);
        public PointD Offset = new PointD(0, 0);
        public double Angle_Rad = 0;
        public List<double> Scores = new List<double>();
        public TAlignData()
        {
            Status = EPatAlignStatus.None;
            Datum = new PointD(0, 0);
            Offset = new PointD(0, 0);
            Angle_Rad = 0;
            Scores = new List<double>();
        }
        public TAlignData(TAlignData alignData)
        {
            Status = alignData.Status;
            Datum = alignData.Datum;
            Offset = alignData.Offset;
            Angle_Rad = alignData.Angle_Rad;
            Scores = alignData.Scores;
        }
        public double Angle_Deg
        {
            get
            {
                return Angle_Rad * 180 / Math.PI;
            }
        }
        public double Score
        {
            get
            {
                return Scores.Min();
            }
        }
    }
    public class THeightData
    {
        public EHeightAlignStatus Status = EHeightAlignStatus.None;
        public double SensorValue = 0;
        public THeightData()
        {
            Status = EHeightAlignStatus.None;
            SensorValue = 0;
        }
        public THeightData(THeightData heightData)
        {
            Status = heightData.Status;
            SensorValue = heightData.SensorValue;
        }
        public override string ToString()
        {
            return $"{Status} {SensorValue}";
        }
    }
    public class TUnit
    {
        private EDispState[,] _DispState = new EDispState[TLayout.MAX_CLUSTER_CR * TLayout.MAX_UNIT_CR, TLayout.MAX_CLUSTER_CR * TLayout.MAX_UNIT_CR];

        //Fiducial
        private readonly TAlignData[,,,] Cluster_Unit_AlignData = new TAlignData[TLayout.MAX_CLUSTER_CR, TLayout.MAX_CLUSTER_CR, TLayout.MAX_UNIT_CR, TLayout.MAX_UNIT_CR];

        //Height - 1D
        private readonly THeightData[,,,] Cluster_Unit_HeightData = new THeightData[TLayout.MAX_CLUSTER_CR, TLayout.MAX_CLUSTER_CR, TLayout.MAX_UNIT_CR, TLayout.MAX_UNIT_CR];

        public void DispState(PointI contiguouCR, EDispState dispState)
        {
            _DispState[contiguouCR.X, contiguouCR.Y] = dispState;
        }
        public EDispState DispState(PointI contiguouCR)
        {
            return _DispState[contiguouCR.X, contiguouCR.Y];
        }

        public TUnit()
        {
            Clear();
        }
        //GetSet Fiducial
        public TAlignData GetUnitAlign(PointI[] clusterCR_unitCR)
        {
            var data = Cluster_Unit_AlignData[clusterCR_unitCR[0].X, clusterCR_unitCR[0].Y, clusterCR_unitCR[1].X, clusterCR_unitCR[1].Y];
            if (data is null) data = new TAlignData();
            return data;
        }
        public void SetUnitAlign(PointI[] clusterCR_unitCR, TAlignData alignData)
        {
            Cluster_Unit_AlignData[clusterCR_unitCR[0].X, clusterCR_unitCR[0].Y, clusterCR_unitCR[1].X, clusterCR_unitCR[1].Y] = new TAlignData(alignData);
        }

        //GetSet Height
        public THeightData GetUnitHeight(PointI[] clusterCR_unitCR)
        {
            var data = Cluster_Unit_HeightData[clusterCR_unitCR[0].X, clusterCR_unitCR[0].Y, clusterCR_unitCR[1].X, clusterCR_unitCR[1].Y];
            if (data is null) data = new THeightData();
            return data;
        }
        public void SetUnitHeight(PointI[] clusterCR_unitCR, THeightData heightData)
        {
            Cluster_Unit_HeightData[clusterCR_unitCR[0].X, clusterCR_unitCR[0].Y, clusterCR_unitCR[1].X, clusterCR_unitCR[1].Y] = new THeightData(heightData);
        }

        //Reset Buffer
        public void Clear()
        {
            for (int i = 0; i < TLayout.MAX_CLUSTER_CR; i++)
                for (int j = 0; j < TLayout.MAX_CLUSTER_CR; j++)
                    for (int k = 0; k < TLayout.MAX_UNIT_CR; k++)
                        for (int l = 0; l < TLayout.MAX_UNIT_CR; l++)
                        {
                            Cluster_Unit_AlignData[i, j, k, l] = null;
                        }

            for (int i = 0; i < TLayout.MAX_CLUSTER_CR; i++)
                for (int j = 0; j < TLayout.MAX_CLUSTER_CR; j++)
                    for (int k = 0; k < TLayout.MAX_UNIT_CR; k++)
                        for (int l = 0; l < TLayout.MAX_UNIT_CR; l++)
                        {
                            Cluster_Unit_HeightData[i, j, k, l] = null;
                        }
        }
    }

    public class T2DMap
    {
        public int[,] Map { get; set; } = new int[0, 0];
        public T2DMap()
        {
        }
        public T2DMap(T2DMap map, int c, int r)
        {
            int old_c = map.Map.GetLength(0);
            int old_r = map.Map.GetLength(1);

            Map = new int[c, r];

            for (int i = 0; i < Math.Min(c, old_c); i++)
            {
                for (int j = 0; j < Math.Min(r, old_r); j++)
                {
                    Map.SetValue(map.Map[i, j], i, j);
                }
            }
        }
    }
    public enum EDispState
    {
        READY = 0,
        DEFAULT_SKIP = 1,

        INSTANT_SKIP = 5,
        NG = 55,

        PROCESSING = 100,
        COMPLETE = 255,

        //HEAD2LAYOUT = 500,
    }
    public class TMAP
    {
        public TLayout ClusterL { get; set; } = new TLayout();
        public TLayout UnitL { get; set; } = new TLayout();
        public BindingList<T2DMap> Maps { get; set; } = new BindingList<T2DMap>();

        public int[,] Bigmap = new int[0, 0];

        public TMAP(TMultiLayout multiLayout)
        {
            ClusterL = new TLayout(multiLayout.Cluster);
            UnitL = new TLayout(multiLayout.Unit);

            InitMap();
        }
        public TMAP(TMAP map)
        {
            this.ClusterL = new TLayout(map.ClusterL);
            this.UnitL = new TLayout(map.UnitL);

            InitMap();
            if (map.Maps.Count is 0) map.InitMap();
            if (map.Maps.Count is 0) return;
            for (int i = 0; i < Maps.Count; i++)
            {
                Maps[i] = new T2DMap(map.Maps[Math.Min(i, map.Maps.Count - 1)], UnitL.CR.X, UnitL.CR.Y);
            }
        }

        public TMAP()
        {
        }

        public void InitMap()
        {
            Bigmap = new int[ClusterL.CR.X, ClusterL.CR.Y];
            Maps.Clear();

            int i = 0;
            var clusterCR = new PointI();
            while (true)
            {
                Maps.Add(new T2DMap() { Map = new int[UnitL.CR.X, UnitL.CR.Y] });
                Bigmap.SetValue(i++, clusterCR.X, clusterCR.Y);

                clusterCR = ClusterL.NextCR(clusterCR);
                if (clusterCR.IsZero) break;
            }
        }
        public bool[] GetRowArray(PointI clusterCR, PointI unitCR)
        {
            var row_index = unitCR.Y;
            bool[] rowarray = Enumerable.Range(0, UnitL.CR.X).Select(x => false).ToArray();
            var index = GetClusterIndex(clusterCR);

            for (int c = 0; c < UnitL.CR.X; c++)
            {
                if (Maps[index].Map[c, row_index] == (int)EDispState.READY)
                {
                    rowarray[c] = true;
                }
            }
            return rowarray;
        }
        public bool[] GetColArray(PointI clusterCR, PointI unitCR)
        {
            var col_index = unitCR.X;
            bool[] colarray = Enumerable.Range(0, UnitL.CR.Y).Select(x => false).ToArray();
            var index = GetClusterIndex(clusterCR);

            for (int r = 0; r < UnitL.CR.Y; r++)
            {
                if (Maps[index].Map[col_index, r] == (int)EDispState.READY)
                {
                    colarray[r] = true;
                }
            }
            return colarray;
        }
        public int GetClusterIndex(PointI clusterCR)
        {
            return Bigmap[clusterCR.X, clusterCR.Y];
        }

        public EDispState GetState(PointI clusterCR, PointI unitCR)
        {
            var index = GetClusterIndex(clusterCR);
            var value = Maps[index].Map[unitCR.X, unitCR.Y];
            return (EDispState)value;
        }
        public void SetState(PointI clusterCR, PointI unitCR, EDispState state)
        {
            var index = GetClusterIndex(clusterCR);
            Maps[index].Map[unitCR.X, unitCR.Y] = (int)state;
        }
        public void SetStateFromState(EDispState org_dispState, EDispState new_dispState)
        {
            for (int c = 0; c < UnitL.CR.X; c++)
            {
                for (int r = 0; r < UnitL.CR.Y; r++)
                {
                    foreach (var m in Maps)
                    {
                        if (m.Map[c, r] == (int)org_dispState) m.Map[c, r] = (int)new_dispState;
                    }
                }
            }
        }

        public void SetAll(EDispState state)
        {
            foreach (var map in Maps)
            {
                for (int c = 0; c < UnitL.CR.X; c++)
                {
                    for (int r = 0; r < UnitL.CR.Y; r++)
                    {
                        map.Map[c, r] = (int)state;
                    }
                }
            }
        }
        public void SetAll(EDispState state, params EDispState[] onlywhen)
        {
            foreach (var map in Maps)
            {
                for (int c = 0; c < UnitL.CR.X; c++)
                {
                    for (int r = 0; r < UnitL.CR.Y; r++)
                    {
                        if (onlywhen.Contains((EDispState)map.Map[c, r])) map.Map[c, r] = (int)state;
                    }
                }
            }
        }
    }

    class Inst
    {
        public class TBoard
        {
            int GantryNo = 0;
            public TBoard(int gantryNo)
            {
                GantryNo = gantryNo;
            }
            public int FuncNo = 0;
            public int tempFuncNo = 0;
            public ERunMode RunMode = ERunMode.Camera;

            public int LayoutNo
            {
                get
                {
                    return GRecipes.Functions[GantryNo][FuncNo].LayoutNo;
                }
            }
            public TMultiLayout CurrentMLayout
            {
                get
                {
                    return GRecipes.MultiLayout[GantryNo][LayoutNo];
                }
            }


            public PointI ClusterCR = new PointI(0, 0);
            public PointI UnitCR = new PointI(0, 0);

            public double[] tempZ = new double[] { 0, 0 };

            public TAlignData BoardAlignData = null;
            public TUnit[] LayerData = Enumerable.Range(0, TMultiLayout.MAX_LAYER).Select(x => new TUnit()).ToArray();

            public TMAP MAP = null;

            public double ThetaPos = 0;
            public void ClearData()
            {
                FuncNo = 0;

                ClusterCR = new PointI(0, 0);
                UnitCR = new PointI(0, 0);

                BoardAlignData = null;
                LayerData.ToList().ForEach(x => x.Clear());
                ThetaPos = 0;
                try//temporary, need to check why exception
                {
                    MAP = new TMAP(GRecipes.Maps[GantryNo][0]);
                }
                catch { };
            }
        }
        public static TBoard[] Board = Enumerable.Range(0, 2).Select(x => new TBoard(x)).ToArray();
    }

    class TCDisp
    {
        public static ERunSelect RunSelect = ERunSelect.All;
        public class TRun
        {
            public bool bRun = false;
            int gantryNo = 0;
            Inst.TBoard InstBoard;
            public TRun(int gantryNo)
            {
                this.gantryNo = gantryNo;
                InstBoard = Inst.Board[this.gantryNo];
            }

            int fun = 0;

            PointI bufferClusterCR = new PointI(0, 0);//buffering ClusterCR
            PointI bufferUnitCR = new PointI(0, 0);
            public void Stop()
            {
                //GRecipes.Functions[gantryNo][Inst.Board[gantryNo].FunNo].Stop();
                GRecipes.Functions[gantryNo][fun].Stop();
            }
            public void CancelBuffer()
            {
                GRecipes.Functions[gantryNo][fun].Cancel();
            }

            public bool OneUnit(int funcNo)
            {
                fun = funcNo;
                int layoutNo = GRecipes.Functions[gantryNo][funcNo].LayoutNo;

                PointD unitRel = GRecipes.MultiLayout[gantryNo][layoutNo].Unit.RelPos(bufferUnitCR);
                PointD clusterRel = GRecipes.MultiLayout[gantryNo][layoutNo].Cluster.RelPos(bufferClusterCR);

                PointI unit = GRecipes.MultiLayout[gantryNo][layoutNo].CR(bufferClusterCR, bufferUnitCR);
                if (InstBoard.LayerData[layoutNo].DispState(unit) != EDispState.READY)
                {
                    bufferUnitCR = GRecipes.MultiLayout[gantryNo][layoutNo].Unit.NextCR(bufferUnitCR);
                    return true;
                }

                List<PointI> EndUnitCRs = new List<PointI>();
                List<PointI> EndClusterCR = new List<PointI>();
                if (!GRecipes.Functions[gantryNo][funcNo].Execute(Inst.Board[gantryNo].RunMode, GRecipes.Board[gantryNo].StartPos.GetPointD(), clusterRel, unitRel, GRecipes.Board[gantryNo].Height, layoutNo, bufferClusterCR, bufferUnitCR, EndClusterCR, EndUnitCRs, Inst.Board[gantryNo].MAP))
                {
                    bRun = false;
                    return false;
                }
                bufferUnitCR = GRecipes.MultiLayout[gantryNo][layoutNo].Unit.NextCR(bufferUnitCR);

                var frmMap = Application.OpenForms.OfType<frmRecipeMap>().FirstOrDefault();
                if (frmMap != null) frmMap.RefreshUI();

                return true;
            }
            public bool OneCluster(int funcNo)
            {
                if (!TCTempCtrl.Monitoring()) return false;
                TCPressCtrl.CanCheck = true;

                int layoutNo = GRecipes.Functions[gantryNo][funcNo].LayoutNo;

                bRun = true;
                while (bRun)
                {
                    if (!OneUnit(funcNo))
                    {
                        bRun = false;
                        break;
                    }
                    if (bufferUnitCR.IsZero) break;
                }


                if (GRecipes.Functions[gantryNo][funcNo].Wait() > 0) bRun = false;

                GRecipes.Functions[gantryNo][funcNo].bDySaveImage = false;

                int tableBase = gantryNo * 10;
                PointI unitCR = new PointI(TEZMCAux.Table(tableBase + 0), TEZMCAux.Table(tableBase + 1));
                PointI clusterCR = new PointI(TEZMCAux.Table(tableBase + 2), TEZMCAux.Table(tableBase + 3));
                Inst.Board[gantryNo].UnitCR = GRecipes.MultiLayout[gantryNo][layoutNo].Unit.NextCR(unitCR);
                Inst.Board[gantryNo].ClusterCR = clusterCR;
                GRecipes.Functions[gantryNo][funcNo].Cancel();

                if (!bRun) return false;

                return true;
            }
            public bool OneFunction(int funcNo)
            {
                GRecipes.Functions[gantryNo][funcNo].FunctionFirstExecution = true;
                GRecipes.Functions[gantryNo][funcNo].RunSequence = 0;
                int tableBase = gantryNo * 10;
                TEZMCAux.Execute($"MOVE_TABLE({tableBase + 4}, {GRecipes.Functions[gantryNo][funcNo].RunSequence})");
                TEZMCAux.Execute($"MODBUS_BIT({gantryNo})=0");

                int layoutNo = GRecipes.Functions[gantryNo][funcNo].LayoutNo;

                bRun = true;
                while (bRun)
                {
                    if (!OneCluster(funcNo))
                    {
                        bRun = false;
                        return false;
                    }
                    bufferClusterCR = GRecipes.MultiLayout[gantryNo][layoutNo].Cluster.NextCR(bufferClusterCR);
                    System.Threading.Thread.Sleep(0);
                    if (bufferClusterCR.IsZero && bufferUnitCR.IsZero) break;
                    if (!bRun) return false;
                }

                return true;
            }

            public bool All()
            {
                bufferUnitCR = Inst.Board[gantryNo].UnitCR;
                bufferClusterCR = Inst.Board[gantryNo].ClusterCR;

                bRun = true;
                while (bRun)
                {
                    //if (!OneFunction())
                    if (!OneFunction(InstBoard.FuncNo))
                    {
                        bRun = false;
                        return false;
                    }

                    GRecipes.Functions[gantryNo][InstBoard.FuncNo].ClearData();
                    InstBoard.MAP.SetStateFromState(EDispState.COMPLETE, EDispState.READY);

                    if (InstBoard.FuncNo >= GRecipes.Functions[gantryNo].Count - 1)
                        InstBoard.FuncNo = 0;
                    else
                        InstBoard.FuncNo++;

                    if (InstBoard.FuncNo == 0)
                    {
                        InstBoard.ClearData();
                        //StartClusterCR = new PointI(0, 0);
                        //StartUnitCR = new PointI(0, 0);
                        break;
                    }

                    if (!bRun) return true;
                }

                return true;
            }
            public bool Continuous()
            {
                bufferUnitCR = Inst.Board[gantryNo].UnitCR;
                bufferClusterCR = Inst.Board[gantryNo].ClusterCR;

                bRun = true;
                while (bRun)
                {
                    if (!All()) return false;

                    if (InstBoard.FuncNo == 0)
                    {
                        //**End of Run All
                        InstBoard.ClearData();
                    }

                    if (!bRun) return true;
                }
                return true;
            }

            public bool Disp()
            {
                switch (RunSelect)
                {
                    case ERunSelect.All:
                        if (!All()) bRun = false;
                        break;
                    case ERunSelect.Func:
                        if (!OneFunction(InstBoard.tempFuncNo)) bRun = false;
                        break;
                    case ERunSelect.Continuous:
                        if (!Continuous()) bRun = false;
                        break;
                }

                GSystemCfg.Pump.Pumps.Select(x => x.FPressDO).ToList().ForEach(x => GMotDef.Outputs[(int)x].Status = false);

                return true;
            }
        }
        public static TRun[] Run = Enumerable.Range(0, 2).Select(x => new TRun(x)).ToArray();
    }
}
