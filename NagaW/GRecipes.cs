﻿using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;
using System.Drawing;
using System.Data.SqlTypes;
using System.Threading.Tasks;

namespace NagaW
{
    public enum ERunPath { X_SPath, X_ZPath, Y_SPath, Y_ZPath }
    public enum ELayoutFirstLast { Null, First, Last, Intermediate }
    public enum ELayoutSequence { Null, FirstOfRow, LastOfRow, LastOfCol, FirstOfCol, Intermediate }
    public enum ELayoutNextMoveDir { Null, X_Plus, X_Minus, Y_Plus, Y_Minus, Z_Path }

    public enum EAction { Skip, Fail, Accept, Retry }
    public class TBoard
    {
        public PointXYZ StartPos { get; set; } = new PointXYZ();  // Abs
        public double Height { get; set; } = new double();  // Abs
        public LightRGBA LightDefault { get; set; } = new LightRGBA(25, 25, 25, 25);
        public LightRGBA Light1 { get; set; } = new LightRGBA(0, 0, 0, 0);
        public LightRGBA Light2 { get; set; } = new LightRGBA(0, 0, 0, 0);

        public void Reset()
        {
            StartPos = new PointXYZ();
            Height = 0;
        }
    }
    public class TLayout
    {
        #region
        public const int MIN_CR = 1;
        public const int MAX_CLUSTER_CR = 10;
        public const int MAX_UNIT_CR = 250;
        public const int MIN_PITCH = 0;
        public const int MAX_PITCH = 1000;

        public PointD StartPos { get; set; } = new PointD();
        public PointI CR { get; set; } = new PointI(MIN_CR, MIN_CR);
        public PointD PitchCol { get; set; } = new PointD();
        public PointD PitchRow { get; set; } = new PointD();

        public ERunPath RunPath { get; set; } = ERunPath.X_SPath;
        public TLayout()
        {
        }
        public TLayout(TLayout layout)
        {
            StartPos = new PointD(layout.StartPos);
            CR = new PointI(layout.CR);
            PitchCol = new PointD(layout.PitchCol);
            PitchRow = new PointD(layout.PitchRow);
            RunPath = layout.RunPath;
        }

        public void ResetAll()
        {
            StartPos = new PointD();
            CR = new PointI(MIN_CR, MIN_CR);
            PitchCol = new PointD();
            PitchRow = new PointD();
            RunPath = new ERunPath();
        }

        public PointI PrevCR(PointI currentCR)
        {
            PointI nextCR = new PointI(currentCR);

            switch (RunPath)
            {

                case ERunPath.X_SPath:
                    {
                        if (currentCR.Y % 2 == 0)//odd row
                        {
                            if (currentCR.X > 0)//prev col
                            {
                                nextCR.X--;
                            }
                            else//currentCR.X is 0
                            {
                                if (currentCR.Y > 0)//reverse next col
                                {
                                    nextCR.X = CR.X - 1;
                                    nextCR.Y--;
                                }
                                else//currentCR.Y >= layoutCR.Y
                                    nextCR = new PointI(CR.X - 1, CR.Y - 1);//end of layout
                            }
                        }
                        else//even row
                        {
                            if (currentCR.X < CR.X - 1)//next col
                            {
                                nextCR.X++;
                            }
                            else//currentCR.X <= 0
                            {
                                if (currentCR.Y > 0)//reverse next col
                                {
                                    nextCR.X = CR.X - 1;
                                    nextCR.Y++;
                                }
                                else//currentCR.Y is 0
                                    nextCR = new PointI(CR.X - 1, CR.Y - 1);//end of layout
                            }
                        }
                        break;
                    }
                case ERunPath.X_ZPath:
                    {
                        if (currentCR.X > 0)//prev col
                        {
                            nextCR.X--;
                        }
                        else//currentCR.X is 0
                        {
                            if (currentCR.Y > 0)//start next row
                            {
                                nextCR.X = CR.X - 1;
                                nextCR.Y--;
                            }
                            else
                                nextCR = new PointI(CR.X - 1, CR.Y - 1);//end of layout
                        }
                        break;
                    }
                case ERunPath.Y_SPath:
                    {
                        if (currentCR.X % 2 == 0)//odd col
                        {
                            if (currentCR.Y > 0)//prev row
                            {
                                nextCR.Y--;
                            }
                            else//currentCR.X is 0
                            {
                                if (currentCR.X > 0)//reverse next row
                                {
                                    nextCR.Y = CR.Y - 1;
                                    nextCR.X--;
                                }
                                else//currentCR.Y >= layoutCR.Y
                                    nextCR = new PointI(CR.X - 1, CR.Y - 1);//end of layout
                            }
                        }
                        else//even row
                        {
                            if (currentCR.Y < CR.Y - 1)//next row
                            {
                                nextCR.Y++;
                            }
                            else//currentCR.Y is 0
                            {
                                if (currentCR.X > 0)//reverse next row
                                {
                                    nextCR.Y = CR.Y - 1;
                                    nextCR.X++;
                                }
                                else//currentCR.X is 0
                                    nextCR = new PointI(CR.X - 1, CR.Y - 1);//end of layout
                            }
                        }
                        break;
                    }
                case ERunPath.Y_ZPath:
                    {
                        if (currentCR.Y > 0)//prev row
                        {
                            nextCR.Y--;
                        }
                        else//currentCR.Y is 0
                        {
                            if (currentCR.X > 0)//start next col
                            {
                                nextCR.Y = CR.Y - 1;
                                nextCR.X--;
                            }
                            else
                                nextCR = new PointI(CR.X - 1, CR.Y - 1);//end of layout
                        }
                        break;
                    }
            }



            return nextCR;
        }

        //Get next Col and Row from input currenCR
        public PointI NextCR(PointI currentCR)
        {
            PointI nextCR = new PointI(currentCR);
            switch (RunPath)
            {
                case ERunPath.X_SPath:
                    {
                        if (currentCR.Y % 2 == 0)//odd row
                        {
                            if (currentCR.X < CR.X - 1)//next col
                            {
                                nextCR.X++;
                            }
                            else//currentCR.X >= layoutCR.X
                            {
                                if (currentCR.Y < CR.Y - 1)//reverse next col
                                {
                                    nextCR.X = CR.X - 1;
                                    nextCR.Y++;
                                }
                                else//currentCR.Y >= layoutCR.Y
                                    nextCR = new PointI(0, 0);//end of layout
                            }
                        }
                        else//even row
                        {
                            if (currentCR.X > 0)//next col
                            {
                                nextCR.X--;
                            }
                            else//currentCR.X <= 0
                            {
                                if (currentCR.Y < CR.Y - 1)//reverse next col
                                {
                                    nextCR.X = 0;
                                    nextCR.Y++;
                                }
                                else//currentCR.Y >= layoutCR.Y
                                    nextCR = new PointI(0, 0);//end of layout
                            }
                        }
                        break;
                    }
                case ERunPath.X_ZPath:
                    {
                        if (currentCR.X < CR.X - 1)//next col
                        {
                            nextCR.X++;
                        }
                        else//currentCR.X >= subTotalCR.X
                        {
                            if (currentCR.Y < CR.Y - 1)//start next row
                            {
                                nextCR.X = 0;
                                nextCR.Y++;
                            }
                            else
                                nextCR = new PointI(0, 0);//end of layout
                        }
                        break;
                    }
                case ERunPath.Y_SPath:
                    {
                        if (currentCR.X % 2 == 0)//odd col
                        {
                            if (currentCR.Y < CR.Y - 1)//next row
                            {
                                nextCR.Y++;
                            }
                            else//currentCR.Y >= layoutCR.Y
                            {
                                if (currentCR.X < CR.X - 1)//reverse next col
                                {
                                    nextCR.X++;
                                    nextCR.Y = CR.Y - 1;
                                }
                                else//currentCR.X >= layoutCR.X
                                    nextCR = new PointI(0, 0);//end of layout
                            }
                        }
                        else//even col
                        {
                            if (currentCR.Y > 0)//next row
                            {
                                nextCR.Y--;
                            }
                            else//currentCR.Y <= 0
                            {
                                if (currentCR.X < CR.X - 1)//reverse next col
                                {
                                    nextCR.X++;
                                    nextCR.Y = 0;
                                }
                                else//currentCR.X <= layoutCR.X
                                    nextCR = new PointI(0, 0);//end of layout
                            }
                        }
                        break;
                    }
                case ERunPath.Y_ZPath:
                    {
                        if (currentCR.Y < CR.Y - 1)//next row
                        {
                            nextCR.Y++;
                        }
                        else//currentCR.Y >= layoutCR.Y
                        {
                            if (currentCR.X < CR.X - 1)//start next col
                            {
                                nextCR.X++;
                                nextCR.Y = 0;
                            }
                            else//currentCR.X >= layoutCR.X
                                nextCR = new PointI(0, 0);//end of layout
                        }
                        break;
                    }
            }
            return nextCR;
        }
        //Return ELayoutFirstLast of the currentCR accoding to RunPath.
        public ELayoutFirstLast FirstLastCR(PointI currentCR)
        {
            if (currentCR.X == 0 && currentCR.Y == 0) return ELayoutFirstLast.First;
            switch (RunPath)
            {
                case ERunPath.X_SPath:
                    if (currentCR.Y >= CR.Y - 1)
                    {
                        if (currentCR.Y % 2 == 0)//odd row
                        {
                            if (currentCR.X >= CR.X - 1) return ELayoutFirstLast.Last;
                        }
                        else//even row
                        {
                            if (currentCR.X == 0) return ELayoutFirstLast.Last;
                        }
                    }
                    break;
                case ERunPath.X_ZPath:
                    if (currentCR.Y >= CR.Y - 1)
                    {
                        if (currentCR.X >= CR.X - 1) return ELayoutFirstLast.Last;
                    }
                    break;
                case ERunPath.Y_SPath:
                    if (currentCR.X >= CR.X - 1)
                    {
                        if (currentCR.X % 2 == 0)//odd col
                        {
                            if (currentCR.Y >= CR.Y - 1) return ELayoutFirstLast.Last;
                        }
                        else//even col
                        {
                            if (currentCR.Y == 0) return ELayoutFirstLast.Last;
                        }
                    }
                    break;
                case ERunPath.Y_ZPath:
                    if (currentCR.X >= CR.X - 1)
                    {
                        if (currentCR.Y >= CR.Y - 1) return ELayoutFirstLast.Last;
                    }
                    break;
            }

            return ELayoutFirstLast.Intermediate;
        }
        //Return ELayoutSequence of the currentCR accoding to RunPath.
        public ELayoutSequence SequenceCR(PointI currentCR)
        {
            switch (RunPath)
            {
                case ERunPath.X_SPath:
                    if (currentCR.Y % 2 == 0)//odd row
                    {
                        if (currentCR.X == 0) return ELayoutSequence.FirstOfRow;
                        if (currentCR.X >= CR.X - 1) return ELayoutSequence.LastOfRow;
                    }
                    else//even row
                    {
                        if (currentCR.X >= CR.X - 1) return ELayoutSequence.FirstOfRow;
                        if (currentCR.X == 0) return ELayoutSequence.LastOfRow;
                    }
                    break;
                case ERunPath.X_ZPath:
                    if (currentCR.X == 0) return ELayoutSequence.FirstOfRow;
                    if (currentCR.Y >= CR.Y - 1)
                    {
                        if (currentCR.X >= CR.X - 1) return ELayoutSequence.LastOfRow;
                    }
                    break;
                case ERunPath.Y_SPath:
                    if (currentCR.X % 2 == 0)//odd col
                    {
                        if (currentCR.Y == 0) return ELayoutSequence.FirstOfCol;
                        if (currentCR.Y >= CR.Y - 1) return ELayoutSequence.LastOfCol;
                    }
                    else//even col
                    {
                        if (currentCR.Y >= CR.Y - 1) return ELayoutSequence.FirstOfCol;
                        if (currentCR.Y == 0) return ELayoutSequence.LastOfCol;
                    }
                    break;
                case ERunPath.Y_ZPath:
                    if (currentCR.Y == 0) return ELayoutSequence.FirstOfCol;
                    if (currentCR.X >= CR.X - 1)
                    {
                        if (currentCR.Y >= CR.Y - 1) return ELayoutSequence.LastOfCol;
                    }
                    break;
            }

            return ELayoutSequence.Intermediate;
        }
        //Return ELayoutNextMoveDir, next move direction of the currentCR accoding to RunPath.
        public ELayoutNextMoveDir NextMoveDir(PointI currentCR)
        {
            switch (RunPath)
            {
                case ERunPath.X_SPath:
                    if (currentCR.Y % 2 == 0)//odd row
                    {
                        if (currentCR.X < CR.X - 1)
                            return (PitchCol.X > 0 ? ELayoutNextMoveDir.X_Plus : ELayoutNextMoveDir.X_Minus);
                        else
                            return (PitchRow.Y > 0 ? ELayoutNextMoveDir.Y_Plus : ELayoutNextMoveDir.Y_Minus);
                    }
                    else//even row
                    {
                        if (currentCR.X > 0)
                            return (PitchCol.X > 0 ? ELayoutNextMoveDir.X_Minus : ELayoutNextMoveDir.X_Plus);
                        else
                            return (PitchRow.Y > 0 ? ELayoutNextMoveDir.Y_Plus : ELayoutNextMoveDir.Y_Minus);
                    }
                case ERunPath.X_ZPath:
                    if (currentCR.X < CR.X - 1)
                        return (PitchCol.X > 0 ? ELayoutNextMoveDir.X_Plus : ELayoutNextMoveDir.X_Minus);
                    else
                        return ELayoutNextMoveDir.Z_Path;
                case ERunPath.Y_SPath:
                    if (currentCR.X % 2 == 0)//odd col
                    {
                        if (currentCR.Y < CR.Y - 1)
                            return (PitchCol.Y > 0 ? ELayoutNextMoveDir.Y_Plus : ELayoutNextMoveDir.Y_Minus);
                        else
                            return (PitchRow.X > 0 ? ELayoutNextMoveDir.X_Plus : ELayoutNextMoveDir.X_Minus);
                    }
                    else//even col
                    {
                        if (currentCR.Y > 0)
                            return (PitchCol.Y > 0 ? ELayoutNextMoveDir.Y_Minus : ELayoutNextMoveDir.Y_Plus);
                        else
                            return (PitchRow.X > 0 ? ELayoutNextMoveDir.X_Plus : ELayoutNextMoveDir.X_Minus);
                    }
                case ERunPath.Y_ZPath:
                    if (currentCR.Y < CR.Y - 1)
                        return (PitchCol.Y > 0 ? ELayoutNextMoveDir.Y_Plus : ELayoutNextMoveDir.Y_Minus);
                    else
                        return ELayoutNextMoveDir.Z_Path;
            }
            return ELayoutNextMoveDir.Null;
        }
        public PointD RelPos(PointI pointCR)
        {
            var pos = new PointD(PitchCol.X * (pointCR.X), PitchCol.Y * (pointCR.X)) + new PointD(PitchRow.X * (pointCR.Y), PitchRow.Y * (pointCR.Y));
            return StartPos + pos;
        }

        public bool IsRowEnd(PointI cr)
        {
            switch (RunPath)
            {
                case ERunPath.X_SPath:
                    {
                        if (cr.Y % 2 is 0)
                        {
                            return cr.X == this.CR.X - 1;
                        }
                        else
                        {
                            return cr.X is 0;
                        }
                    }

                case ERunPath.X_ZPath: return cr.X == this.CR.X - 1;
                default: return false;
            }
        }

        public bool IsColEnd(PointI cr)
        {
            switch (RunPath)
            {
                case ERunPath.Y_SPath:
                    {
                        if (cr.X % 2 is 0)
                        {
                            return cr.Y == this.CR.Y - 1;
                        }
                        else
                        {
                            return cr.Y is 0;
                        }

                    }
                case ERunPath.Y_ZPath:
                    {
                        return cr.Y == this.CR.Y - 1;
                    }
                default: return false;
            }
        }

        public int TotalCR
        {
            get
            {
                return (CR.X * CR.Y);
            }
        }

        public int CummulativeCR(PointI currentCR)
        {
            PointI nextCR = new PointI(currentCR);
            switch (RunPath)
            {
                case ERunPath.X_SPath:
                    {
                        if (currentCR.Y % 2 == 0)//odd row
                        {
                            return (currentCR.Y * CR.X) + (currentCR.X + 1);
                        }
                        else//even row
                        {
                            return (currentCR.Y * CR.X) + (CR.X - currentCR.X);
                        }
                    }
                case ERunPath.X_ZPath:
                    return (currentCR.Y * CR.X) + (currentCR.X + 1);
                case ERunPath.Y_SPath:
                    {
                        if (currentCR.X % 2 == 0)//odd col
                        {
                            return (currentCR.X * CR.Y) + (currentCR.Y + 1);
                        }
                        else//even col
                        {
                            return (currentCR.X * CR.Y) + (currentCR.Y - currentCR.Y);
                        }
                    }
                case ERunPath.Y_ZPath:
                    return (currentCR.X * CR.Y) + (currentCR.Y + 1);
            }
            return 0;
        }
        public int RemainCR(PointI currentCR)
        {
            return TotalCR - CummulativeCR(currentCR);
        }
        #endregion
    }
    public class TMultiLayout
    {
        #region
        public const int MAX_LAYER = 1;
        public int Index { get; set; }
        public string Name { get; set; } = string.Empty;
        public BindingList<TLayout> Layouts { get; set; } = new BindingList<TLayout>() { new TLayout(), new TLayout() };
        public TLayout Cluster { get => Layouts[0]; }
        public TLayout Unit { get => Layouts[1]; }
        public TMultiLayout()
        {

        }
        public TMultiLayout(int no)
        {
            Index = no;
        }
        public void Copy(TMultiLayout mLayout)
        {
            Layouts = new BindingList<TLayout>() { new TLayout(mLayout.Layouts[0]), new TLayout(mLayout.Layouts[1]) };
        }
        public override string ToString()
        {
            return $"{Index:d3} {Name}";
        }
        public PointI CR(PointI clusterCR, PointI unitCR)
        {
            int c = (clusterCR.X * (Layouts[0].CR.X + 1)) + unitCR.X;
            int r = (clusterCR.Y * (Layouts[0].CR.Y + 1)) + unitCR.Y;
            return new PointI(c, r);
        }
        #endregion
    }

    public enum ECmd : ulong
    {
        //***Cmd                        //  Para0       Para1       Para2       Para3       Para4       Para5       Para6       Para7       Para8       Para9
        NONE = 0,

        //**Reserved for NagaN** SELECT_HEAD = 5,        //  H2/H1
        SPEED = 10,                     //  Speed       Accel
        COMMENT,
        ADJUST_OFFSET = 15,             //  FRX         FRY

        //****Pump Setup >=20~49
        VERMES_3280_SETUP = 20,         //  RisingTime  OpeningTime FallingTime NeedleLift  PulseNo     Delay       FPress
        SP_SETUP,                       //  DispTime    PulseOnDly  PulseOffDly                                     FPress      PPress      VacDur
        HM_SETUP,                       //  DispTime    DispRPM     BSuckTime   BSuckRPM    AccDec                  FPress                  VacDur
        SPLITE_SETUP,                   //  DispTime                                                                FPress                  VacDur
        TP_SETUP = 35,                  //  DispTime                                                                FPress                  VacDur
        PNEUMATICS_JET_SETUP,           //  disptime    DelayTime                                                   FPress      VPress

        //TEMP_SETUP,                     //                                                  Frequency
        //PRESSURE_MONITORING,            //                                                  Frequency

        //***Setups >= 50~199
        DOWN_SETUP = 50,                //  DispGap     Speed       Wait        
        RET_SETUP = 55,                 //  RetDist     Speed       Wait
        UP_SETUP = 56,                  //  UpDist      Speed       Wait
        CLUSTER_GAP_SETUP = 60,         //  RelZ
        DOT_SETUP = 100,                //  DispTime    Wait
        LINE_SETUP = 120,               //  LStartDelay LSpeed      LEndDelay   Wait        (spare)     StartGap    StartLen  
        CUT_TAIL_SETUP = 130,//180,     //  ECutTailTypeLength	    Height	    Speed
        DYNAMIC_JET_SETUP = 140,        //  Pre-Disp?   AccelDist    

        //REPEAT_SETUP,                   //  PitchX      PitchY                                                      CountX      CountY

        PAT_ALIGN_SETUP = 150,//660,    //  SettleTime
        HEIGHT_SETUP = 160,//570,       //  SettleTime  RangeULmt

        //***Path Motions >= 200~499
        DOT = 200,                      //  FRX         FRY                                             (resv)       Disp?
        LINE_START = 220,               //  FRX         FRY                                             (resv)       Disp?
        LINE_PASS = 230,                //  LRX         LRY                                             (resv)       Disp?       Radius      CornerSp    EarlyCut
        CIRC_CENTER = 240,              //  CRX         CRY                                             (resv)       Disp?       CCW=0,CW>0
        CIRC_PASS = 241,                //  PRX         PRY                     PRX2        PRY2        (resv)       Disp?
        ARC_PASS = 242,                 //  PRX         PRY                     ERX2        ERY2        (resv)       Disp?
        LINE_END = 250,                 //  LRX         LRY                                                         Disp?       ()          ()          EarlyCut

        DYNAMIC_JET_DOT = 260,          //  FRX         FRY                                                         Disp?
        DYNAMIC_JET_DOT_SW,             //  FRX         FRY                                                         Disp?       JetDir 

        PAT_ALIGN_UNIT = 504,           //  FRX         FRY         FRZ         FRX2        FRY2        FRZ2         MinScore    MaxXYOffset MaxAngle    OptionBits
        PAT_ALIGN_CLUSTER = 505,        //  FRX         FRY         FRZ         FRX2        FRY2        FRZ2         MinScore    MaxXYOffset MaxAngle    OptionBits
        PAT_ALIGN_BOARD = 506,          //  FRX         FRY         FRZ         FRX2        FRY2        FRZ2         MinScore    MaxXYOffset MaxAngle    OptionBits

        PAT_ALIGN_ROTARY = 507,         //  FRX         FRY         FRZ         FRX2        FRY2        FRZ2        MinScore    MaxXYOffset MaxAngle    OptionBits

        DYNAMIC_SAVE_IMAGE = 514,       //  FRX         FRY                                                         Speed           

        HEIGHT_ALIGN_UNIT = 554,        //  FRX         FRY
        HEIGHT_ALIGN_CLUSTER = 555,     //  FRX         FRY
        HEIGHT_ALIGN_BOARD = 556,       //  FRX         FRY
        HEIGHT_SET = 580,               //

        //****Disp Setup >= 700~799
        NEEDLE_VAC_CLEAN = 700,         //  Count       PerUnit                 DnWait      DispTime    VacTime     PostVacTime PostWait
        NEEDLE_FLUSH,                   //  Count       PerUnit                 DnWait      DispTime    VacTime     PostVacTime PostWait
        NEEDLE_PURGE,                   //  Count       PerUnit                 DnWait      DispTime    VacTime     PostVacTime PostWait
        NEEDLE_AB_CLEAN,                //  Count       PerUnit                 
        NEEDLE_SPRAY_CLEAN,             //  Count       PerUnit                 DnWait                  SprayTime               PostWait


        PURGE_STAGE = 750,              //  Count       PerUnit

        LINE_SPEED = 800,               //  LSpeed
        LINE_GAP_ADJUST = 801,          //  RZ          Length

        NOZZLE_INSPECTION = 1001,       //  PerUnit
        GOTO_POSITION,                  //  Position

        NOTCH_ALIGNMENT,                //  X           Y           Z           SettleTime  Tolerance
    }

    public enum ECutTailType { None, Fwd, Bwd, SqFwd, SqBwd, Rev, SqRev };
    public class TCmd
    {
        public const int MAX_CMD = 10000;
        public const int MAX_PARA = 10;
        public ECmd Cmd { get; set; } = ECmd.NONE;
        [Browsable(false)]
        public int ID { get; set; } = 0;
        public double[] Para { get; set; } = Enumerable.Range(0, MAX_PARA).Select(x => new double()).ToArray();
        public TCmd()
        {
        }
        public TCmd(ECmd command)
        {
            this.Cmd = command;
        }
        public TCmd(TCmd tCommand)
        {
            this.Cmd = tCommand.Cmd;
            this.Para = Enumerable.Range(0, MAX_PARA).Select(x => tCommand.Para[x]).ToArray();
        }

        public string Param_Edit
        {
            get
            {
                string Info = string.Empty;

                switch (Cmd)
                {
                    default: break;

                    case ECmd.SPEED: Info = $"Speed:{Para[0]} Accel:{Para[1]}"; break;
                    case ECmd.ADJUST_OFFSET: Info = new PointD(Para[0], Para[1]).ToStringForDisplay(); break;

                    case ECmd.VERMES_3280_SETUP: Info = (((Para[0] + Para[1] + Para[2]) * Para[4]) + (Para[5] * Math.Max(Para[4] - 1, 1))).ToString() + " ms"/*$"RisingTime:{Para[0]} OpeningTime:{Para[1]} FallingTime:{Para[2]} NeedleLift:{Para[3]} PulseNo:{Para[4]} Delay:{Para[5]} FPress:{Para[6]}"*/; break;
                    case ECmd.PNEUMATICS_JET_SETUP:
                    case ECmd.SP_SETUP:
                    case ECmd.SPLITE_SETUP:
                    case ECmd.TP_SETUP: Info = $"Open Time:{Para[0]} FPress:{Para[6]}"; break;
                    case ECmd.HM_SETUP: Info = $"RPM:{Para[1]} FPress:{Para[6]}"; break;

                    //case ECmd.TEMP_SETUP:
                    //case ECmd.PRESSURE_MONITORING: Info = $"CheckPerUnit{Para[4]}"; break;

                    case ECmd.DOWN_SETUP: Info = $"DispGap:{Para[0]} Speed:{Para[1]} Wait:{Para[2]}"; break;
                    case ECmd.RET_SETUP: Info = $"RetDist:{Para[0]} Speed:{Para[1]} Wait:{Para[2]}"; break;
                    case ECmd.UP_SETUP: Info = $"UpDist:{Para[0]} Speed:{Para[1]} Wait:{Para[2]}"; break;
                    case ECmd.CLUSTER_GAP_SETUP: Info = $"Gap:{Para[0]}"; break;
                    case ECmd.DOT_SETUP: Info = $"DispTime:{Para[0]} Wait:{Para[1]}"; break;
                    case ECmd.LINE_SETUP: Info = $"LStartDelay:{Para[0]} LSpeed:{Para[1]} LEndDelay:{Para[2]} Wait:{Para[3]} TailLength:{Para[4]}"; break;
                    case ECmd.CUT_TAIL_SETUP: Info = $"Type:{(ECutTailType)(int)Para[0]} Length:{Para[1]} Height:{Para[2]} Speed{Para[3]}"; break;
                    case ECmd.DYNAMIC_JET_SETUP: Info = $"{(Para[0] is 0 ? "" : "Pre-Disp;")}"; break;


                    case ECmd.DOT:
                    case ECmd.LINE_START:
                    case ECmd.LINE_PASS:
                    case ECmd.LINE_END: Info = $"{(Para[6] is 0 ? "*" : "")} " + new PointD(Para[0], Para[1]).ToStringForDisplay(); break;
                    case ECmd.CIRC_CENTER: Info = $"{(Para[6] is 0 ? "*" : "")} " + $"C: {Para[0]:f3}, {Para[1]:f3}"; break;
                    case ECmd.CIRC_PASS: Info = $"{(Para[6] is 0 ? "*" : "")} " + $"P1: {Para[0]:f3}, {Para[1]:f3} P2: {Para[3]:f3}, {Para[4]:f3}"; break;
                    case ECmd.ARC_PASS: Info = $"{(Para[6] is 0 ? "*" : "")} " + $"P: {Para[0]:f3}, {Para[1]:f3} E: {Para[3]:f3}, {Para[4]:f3}"; break;

                    case ECmd.DYNAMIC_JET_DOT:
                    case ECmd.DYNAMIC_JET_DOT_SW: Info = $"{(Para[6] is 0 ? "*" : "")} " + new PointD(Para[0], Para[1]).ToStringForDisplay() + " " + Enum.GetNames(typeof(EDynamicJetDir))[(int)Para[7]]; break;

                    case ECmd.PAT_ALIGN_ROTARY:
                    case ECmd.PAT_ALIGN_UNIT:
                    case ECmd.PAT_ALIGN_CLUSTER:
                    case ECmd.PAT_ALIGN_BOARD: Info = $"1Pt:{new PointD(Para[0], Para[1]).ToStringForDisplay()} {(Para[9] > 0 ? $"2Pt:{new PointD(Para[4], Para[5]).ToStringForDisplay()}" : "")} MinScore:{Para[6]} MinOffset:{Para[7]} MinAngle:{Para[7]}"; break;
                    case ECmd.PAT_ALIGN_SETUP: Info = $"SettleT:{Para[0]}"; break;

                    case ECmd.HEIGHT_ALIGN_UNIT:
                    case ECmd.HEIGHT_ALIGN_CLUSTER:
                    case ECmd.HEIGHT_ALIGN_BOARD: Info = new PointD(Para[0], Para[1]).ToStringForDisplay(); break;
                    case ECmd.HEIGHT_SETUP: Info = $"SettleT:{Para[0]} RangeULmt:{Para[1]}"; break;

                    case ECmd.NEEDLE_VAC_CLEAN:
                    case ECmd.NEEDLE_FLUSH:
                    case ECmd.NEEDLE_PURGE: Info = $"Count:{Para[0]} PerUnit:{Para[1]} DnWait:{Para[3]} DispTime:{Para[4]} VacTime:{Para[5]} PostVacTime:{Para[6]} PostWait:{Para[7]}"; break;
                    case ECmd.NEEDLE_AB_CLEAN: Info = $"Count:{Para[0]} PerUnit:{Para[1]}"; break;
                    case ECmd.NEEDLE_SPRAY_CLEAN: Info = $"Count:{Para[0]} PerUnit:{Para[1]} DnWait:{Para[3]} SprayTime:{Para[5]} PostWait:{Para[7]}"; break;
                    case ECmd.PURGE_STAGE: Info = $"Count:{Para[0]} PerUnit:{Para[1]}"; break;

                    case ECmd.LINE_SPEED: Info = $"LSpeed:{Para[0]}"; break;
                    case ECmd.LINE_GAP_ADJUST: Info = $"RelGap:{Para[0]} RelDist:{Para[1]}"; break;

                    case ECmd.GOTO_POSITION: Info = typeof(GSetupPara.Maintenance).GetFields(BindingFlags.Static | BindingFlags.Public).Select(x => x.Name).ToArray()[(int)Para[0]]; break;

                }
                return Info;
            }
        }
        public string Goto
        {
            get
            {
                string s = string.Empty;

                if (Cmd >= ECmd.DOT && Cmd < ECmd.NEEDLE_VAC_CLEAN) s = "[Goto]";
                if (Cmd == ECmd.HEIGHT_SET) s = string.Empty;
                return s;
            }
        }

        [Description(GDoc.newVerFlag)]
        [DisplayName("Run")]
        public bool Enable { get; set; } = true;
    }
    public class TFunction
    {
        public int gantryIdx { get; set; }
        public string Name { get; set; } = string.Empty;

        internal TEZMCAux.TGroup gantry
        {
            get
            {
                //return gantryIdx == 0 ? TFGantry.GantryLeft : TFGantry.GantryRight;
                return TFGantry.GantryLeft;
            }
        }
        internal string sBase
        {
            get
            {
                return $"BASE({ gantry.Axis[0].AxisNo },{ gantry.Axis[1].AxisNo},{ gantry.Axis[2].AxisNo}) ";
            }
        }

        public double DispGap { get; set; } = 1;
        public double DnWait { get; set; } = 10;
        public double MoveSpeed { get; set; } = 50;
        public double MoveAccel { get; set; } = 500;
        public double RetGap { get; set; } = 10;

        public double DotTime { get; set; } = 500;
        public double DotWait { get; set; } = 10;

        public double LineSDelay { get; set; } = 10;
        public double LineSpeed { get; set; } = 10;
        public double LineEDelay { get; set; } = 10;
        public double LineWait { get; set; } = 10;

        public void CopyCommonPara(TFunction sourceFunction)
        {
            this.Name = sourceFunction.Name;

            this.DispGap = sourceFunction.DispGap;
            this.DnWait = sourceFunction.DnWait;
            this.MoveSpeed = sourceFunction.MoveSpeed;
            this.MoveAccel = sourceFunction.MoveAccel;
            this.RetGap = sourceFunction.RetGap;

            this.DotTime = sourceFunction.DotTime;
            this.DotWait = sourceFunction.DotWait;

            this.LineSDelay = sourceFunction.LineSDelay;
            this.LineSpeed = sourceFunction.LineSpeed;
            this.LineEDelay = sourceFunction.LineEDelay;
            this.LineWait = sourceFunction.LineWait;
        }

        public int LayoutNo { get; set; }
        public BindingList<TCmd> Cmds { get; set; } = new BindingList<TCmd>();
        public TFunction()
        {

        }
        public TFunction(int gantryIndex)
        {
            gantryIdx = gantryIndex;
        }
        public override string ToString()
        {
            return $"{Name}";
        }

        bool running = false;

        int NeedleCleanCount, NeedleFlushCount, NeedlePurgeCount, NeedlePurgeStageCount, NeedleABCleanCount, NeedleSprayCount = 0;

        int NoozleInspecCount = 0;
        int PressCtrlCheckCount = 0;

        static Vermes3280_Param[] vm3280Param = new Vermes3280_Param[] { new Vermes3280_Param(), new Vermes3280_Param() };
        static SP_Param[] spParam = new SP_Param[] { new SP_Param(), new SP_Param() };
        static HM_Param[] hmParam = new HM_Param[] { new HM_Param(), new HM_Param() };
        static PneumaticJet_Param[] pneumaticJet_Params = Enumerable.Range(0, 2).Select(x => new PneumaticJet_Param()).ToArray();

        internal bool FunctionFirstExecution { get; set; } = false;//Flag to indicate first execution for function

        public int RunSequence = 0;//Running counter for execution loops. Reset at function start. Inc each cluster loop. Used for unit complete flag
        int StopRunSequence = 0;//Stop Run Sequence no when stop is intiiated. Used for comparison of function completion.

        THeightData HeightSetData = new THeightData();//Height Set Data

        PointI current_Cluster_CR = null;
        bool moveClusterGap = false;
        double moveClusterGapAbsZ = 0;

        int CheckTemp_Count = 0;
        public bool bDySaveImage = false;

        public bool bufferModeTrackUnit = false;//current command is bufferred with unit tracking, used for Dot, Lines, Arc

        public bool goposition_executed = false;

        public bool Execute(ERunMode runMode, PointD boardOrigin, PointD relCluster, PointD relUnit, double productZAbs, int layoutNo, PointI clusterCR, PointI unitCR, List<PointI> EndclusterCRs, List<PointI> EndunitCRs, TMAP map)
        {
            EDispState state = EDispState.COMPLETE;
            if (map.GetState(clusterCR, unitCR) != EDispState.READY) return true;
            //if (!TCPressCtrl.CheckMainAir()) return false;
            var pressuremaster = GRecipes.PressureSetups[gantry.Index];

            running = true;

            Inst.TBoard instBoard = Inst.Board[gantry.Index];

            if (FunctionFirstExecution)
            {
                HeightSetData = new THeightData();
                NeedleCleanCount = NeedleFlushCount = NeedlePurgeCount = NeedlePurgeStageCount = NeedleABCleanCount = NeedleSprayCount = 0;
            }

            bool moveNextDispCmdAtXYPlane = false;//move to disp command position at XY Plane
            if (FunctionFirstExecution) moveNextDispCmdAtXYPlane = true;

            var offsetXY = new PointD();

            //disp param
            double dispGap = DispGap, dnSpeed = MoveSpeed, dnWait = DnWait;
            double dotTime = DotTime, dotWait = DotWait;
            double moveSpeed = MoveSpeed, moveAccel = MoveAccel;
            double lineSDelay = LineSDelay, lineSpeed = LineSpeed, lineEDelay = LineEDelay, lineWait = LineWait, lineStartGap = 0, lineStartLength = 0;
            double retDist = RetGap, retSpeed = MoveSpeed, retWait = 10;
            double upDist = 0, upSpeed = MoveSpeed, upWait = 10;
            double cutTailType = 0, cutTailLength = 0, cutTailHeight = 0, cutTailSpeed = 0;
            double lineRelGap = 0, lineRelGapLength = 0;
            double clusterGap = 0;

            TEZMCAux.TOutput trigCam = gantryIdx == 0 ? GMotDef.Out4 : GMotDef.Out9;

            TEZMCAux.TOutput trig = null;
            TEZMCAux.TOutput fpressIO = null;
            TEZMCAux.TOutput ppressIO = null;
            TEZMCAux.TOutput vacIO = null;
            GSystemCfg.Pump dispCtrl = GSystemCfg.Pump.Pumps[gantry.Index];


            //pressure

            #region pump init
            switch (dispCtrl.PumpType)
            {
                case EPumpType.PNEUMATIC_JET:
                    {
                        var setup = new PneumaticJet_Param(GRecipes.PneumaticJet_Setups[gantry.Index]);
                        fpressIO = GMotDef.Outputs[(int)dispCtrl.FPressDO];
                        trig = ppressIO = GMotDef.Outputs[(int)dispCtrl.PPressDO];

                        if (FunctionFirstExecution)
                        {
                            fpressIO.Status = true;
                            ppressIO.Status = false;

                            if (pneumaticJet_Params[gantry.Index].SameAs(setup)) break;
                            if (!TFPressCtrl.FPress[gantry.Index].Set(setup.FPress.Value)) return false;
                            if (!TFPressCtrl.FPress[gantry.Index + 2].Set(setup.VPress.Value)) return false;

                            pneumaticJet_Params[gantry.Index] = new PneumaticJet_Param(setup);
                            pressuremaster.FPress = setup.FPress;
                            pressuremaster.PPress = setup.VPress;
                        }

                        break;
                    }
                case EPumpType.SP:
                case EPumpType.SPLite:
                case EPumpType.TP:
                    {
                        var setup = new SP_Param(GRecipes.SP_Setups[gantry.Index]);
                        fpressIO = GMotDef.Outputs[(int)dispCtrl.FPressDO];
                        ppressIO = GMotDef.Outputs[(int)dispCtrl.PPressDO];
                        vacIO = GMotDef.Outputs[(int)dispCtrl.VacDO];
                        if (FunctionFirstExecution)
                        {
                            fpressIO.Status = false;
                            ppressIO.Status = false;
                            vacIO.Status = false;

                            if (spParam[gantry.Index].SameAs(setup)) break;
                            if (!TFPressCtrl.FPress[gantry.Index].Set(setup.FPress.Value)) return false;
                            if (dispCtrl.PumpType == EPumpType.SP && !TFPressCtrl.FPress[gantry.Index + 2].Set(setup.PPress.Value)) return false;
                            spParam[gantry.Index] = new SP_Param(setup);

                            pressuremaster.FPress = setup.FPress;
                            pressuremaster.PPress = setup.PPress;
                        }

                        break;
                    }
                case EPumpType.VERMES_3280:
                    {
                        var setup = new Vermes3280_Param(GRecipes.Vermes_Setups[gantry.Index]);
                        (trig = GMotDef.Outputs[(int)dispCtrl.DispDO]).Status = false;
                        (fpressIO = GMotDef.Outputs[(int)dispCtrl.FPressDO]).Status = true;
                        if (FunctionFirstExecution)
                        {
                            trig.Status = false;
                            fpressIO.Status = true;

                            if (vm3280Param[gantry.Index].SameAs(setup)) break;
                            if (!TFPump.Vermes_Pump[gantry.Index].TriggerAset(setup)) return false;
                            if (!TFPressCtrl.FPress[gantry.Index].Set(setup.FPress.Value)) return false;
                            vm3280Param[gantry.Index] = new Vermes3280_Param(setup);
                            pressuremaster.FPress = setup.FPress;

                            Thread.Sleep(150);
                        }
                        break;
                    }
                case EPumpType.HM:
                    {
                        var setup = new HM_Param(GRecipes.HM_Setups[gantry.Index]);
                        fpressIO = GMotDef.Outputs[(int)dispCtrl.FPressDO];
                        vacIO = GMotDef.Outputs[(int)dispCtrl.VacDO];
                        if (FunctionFirstExecution)
                        {
                            vacIO.Status = false;
                            //fpressIO.Status = true;//cannot on, will drip

                            if (hmParam[gantry.Index].SameAs(setup)) break;
                            if (!TFPressCtrl.FPress[gantry.Index].Set(setup.FPress.Value)) return false;
                            hmParam[gantry.Index] = new HM_Param(setup);
                            pressuremaster.FPress = setup.FPress;
                        }
                    }
                    break;
            }
            if(dispCtrl.PumpType> EPumpType.None)
            {
                if (!TCPressCtrl.Monitoring(gantryIdx, pressuremaster)) return false;
            }
            #endregion

            //pat align
            var settleTime = 0;

            //***Height align variables
            int hsensorSettleTime = GProcessPara.HSensor.SettleTime.Value;
            //double heightRange = 0.1;//mm
            double heightRangeULimit = 0.1;//mm, ange Upper Limit
            List<double> hsensorValue = new List<double>();
            int C_count = 0;
            //***


            //***dynamicjet_useparam
            bool firstjet = true;
            EDynamicDispMode predisp = EDynamicDispMode.False;
            EDynamicDispMode postdisp = EDynamicDispMode.False;
            
            PointI serpentine_CR = new PointI(unitCR);
            double dynamic_accelDist = 0;

            PointD dy_clusterRel = new PointD();
            PointD dy_serp_clusterRel = new PointD();

            PointD dy_unitRel = new PointD();
            PointD dy_serp_unitRel = new PointD();
            double dyspeed = 50;
            double dyAcc = 500;

            ERunPath dyrunpath = ERunPath.X_ZPath;
            //***
            string cmdBuffer = sBase;
            PointXYZ lastAbsPos = new PointXYZ(0, 0, 0);
            PointD lastFuncRelPos = new PointD(0, 0);
            double productZRun = 0;
            bool MoveToStartGap2(TCmd cmd, bool merge = false)
            {
                if (!GMotDef.GVAxis.MoveAbs(0)) return false;

                #region
                //PointI absUnitCR = GRecipes.MultiLayout[gantry.Index][layoutNo].CR(clusterCR, unitCR);

                PointI[] ij = new PointI[] { clusterCR, unitCR };
                PointD ptFuncRel = Translate(new PointD(cmd.Para[0], cmd.Para[1]) + instBoard.CurrentMLayout.Unit.RelPos(unitCR), instBoard.LayerData[layoutNo].GetUnitAlign(ij));
                lastFuncRelPos = ptFuncRel;

                PointD ptAbs = boardOrigin + relCluster + ptFuncRel;
                if (runMode > ERunMode.Camera) { ptAbs = ptAbs + GSetupPara.Calibration.NeedleXYOffset[gantry.Index]; }
                lastAbsPos.X = ptAbs.X;
                lastAbsPos.Y = ptAbs.Y;
                lastAbsPos.X += offsetXY.X;
                lastAbsPos.Y += offsetXY.Y;

                cmdBuffer = sBase + " ";
                cmdBuffer += $"MERGE={(merge ? "1" : "0")} ";
                cmdBuffer += $"SPEED={GProcessPara.Operation.GXYSpeed[1]} ";
                cmdBuffer += $"ACCEL={GProcessPara.Operation.GXYSpeed[2]} ";
                cmdBuffer += $"DECEL={GProcessPara.Operation.GXYSpeed[2]} ";
                TEZMCAux.DirectCommand(cmdBuffer);

                cmdBuffer = sBase;

                #region Move Cluster Gap
                if (current_Cluster_CR is null || FunctionFirstExecution) current_Cluster_CR = clusterCR;
                if (!current_Cluster_CR.IsEqual(clusterCR)) moveClusterGap = true;

                if (runMode > ERunMode.Camera)
                {
                    if (moveClusterGap && clusterGap > 0)
                    {
                        moveNextDispCmdAtXYPlane = true;
                        moveClusterGap = false;
                        current_Cluster_CR = clusterCR;

                        double[] gaps = new double[] { clusterGap, retDist, upDist };
                        clusterGap = gaps.Max();
                        double d = gantry.ZAxis.ActualPos;
                        double relZ = (d + clusterGap < 0) ? clusterGap : Math.Abs(d);
                        moveClusterGapAbsZ = d + relZ;
                        cmdBuffer += $"MOVE(0,0,{relZ:f6}) ";
                        cmdBuffer += $"MOVE_DELAY({0}) ";
                    }
                }
                #endregion

                if (runMode == ERunMode.Camera)
                {
                    cmdBuffer += $"MOVEABS({lastAbsPos.X:f6},{lastAbsPos.Y:f6},{GRecipes.Board[gantry.Index].StartPos.Z:f6}) ";
                }
                if (runMode > ERunMode.Camera)
                //else
                {
                    //if (FunctionFirstExecution) moveNextDispCmdAtXYPlane = true;

                    if (moveNextDispCmdAtXYPlane)
                    {
                        moveNextDispCmdAtXYPlane = false;
                        //double d = gantry.ZAxis.ActualPos;
                        //cmdBuffer += $"MOVEABS({lastAbsPos.X:f6},{lastAbsPos.Y:f6},{d}) ";
                        cmdBuffer += $"MOVEABS({lastAbsPos.X:f6},{lastAbsPos.Y:f6},{moveClusterGapAbsZ}) ";
                        cmdBuffer += $"MOVE_DELAY({0}) ";
                    }

                    if (HeightSetData.Status == EHeightAlignStatus.Aligned)
                    {
                        double hSenseDiff = HeightSetData.SensorValue - GSetupPara.Calibration.HSensorValue[gantry.Index];
                        productZAbs = GSetupPara.Calibration.ZTouchValue[gantry.Index] - hSenseDiff;
                        productZRun = productZAbs;
                        lastAbsPos.Z = productZRun + dispGap + retDist;
                        cmdBuffer += $"MOVEABS({lastAbsPos.X:f6},{lastAbsPos.Y:f6},{lastAbsPos.Z:f6}) ";
                    }
                    else
                        switch (instBoard.LayerData[layoutNo].GetUnitHeight(ij).Status)
                        {
                            case EHeightAlignStatus.None:
                                productZRun = productZAbs;
                                lastAbsPos.Z = productZRun + dispGap + retDist;
                                cmdBuffer += $"MOVEABS({lastAbsPos.X:f6},{lastAbsPos.Y:f6},{lastAbsPos.Z:f6}) ";
                                break;
                            case EHeightAlignStatus.Processing:
                            case EHeightAlignStatus.Aligned:
                                //Height Sensor value is WD,30 + 0@NS
                                double hSenseDiff = instBoard.LayerData[layoutNo].GetUnitHeight(ij).SensorValue - GSetupPara.Calibration.HSensorValue[gantry.Index];
                                productZAbs = GSetupPara.Calibration.ZTouchValue[gantry.Index] - hSenseDiff;
                                productZRun = productZAbs;
                                lastAbsPos.Z = productZRun + dispGap + retDist;
                                cmdBuffer += $"MOVEABS({lastAbsPos.X:f6},{lastAbsPos.Y:f6},{lastAbsPos.Z:f6}) ";
                                break;
                            case EHeightAlignStatus.NG://skip this unit
                                return true;
                            default:
                                GDefine.SystemState = ESystemState.ErrorRestart;
                                GAlarm.Prompt(EAlarm.CONFOCAL_RANGE_ERROR);
                                return false;
                        }
                }
                TEZMCAux.DirectCommand(cmdBuffer);
                return true;
                #endregion
            }

            if (!running) return true;

            var CmdEnabled = Cmds.Where(x => x.Enable).ToList();

            foreach (var cmd in CmdEnabled)
            {
                switch (cmd.Cmd)
                {
                    default: return false;
                    case ECmd.SPEED:
                        #region
                        {
                            moveSpeed = cmd.Para[0];
                            moveAccel = cmd.Para[1];
                            //group.SetParam(0, cmd.Para[0], cmd.Para[1]);
                            break;
                        }
                    #endregion
                    case ECmd.ADJUST_OFFSET:
                        #region
                        {
                            offsetXY.X = cmd.Para[0];
                            offsetXY.Y = cmd.Para[1];
                            break;
                        }
                    #endregion

                    case ECmd.VERMES_3280_SETUP:
                        #region
                        {
                            if (dispCtrl.PumpType != EPumpType.VERMES_3280)
                            {
                                GAlarm.Prompt(EAlarm.INVALID_PUMP, gantry.Name);
                                return false;
                            }

                            var setup = new Vermes3280_Param(cmd.Para[0], cmd.Para[1], cmd.Para[2], (int)cmd.Para[3], (int)cmd.Para[4], cmd.Para[5], cmd.Para[6]);
                            if (vm3280Param[gantry.Index].SameAs(setup)) break;
                            if (!TFPump.Vermes_Pump[gantry.Index].TriggerAset(setup)) return false;
                            var fpress = pressuremaster.Master ? pressuremaster.FPress : setup.FPress;

                            if (!TFPressCtrl.FPress[gantry.Index].Set(fpress.Value)) return false;
                            vm3280Param[gantry.Index] = new Vermes3280_Param(setup);
                            pressuremaster.FPress = fpress;
                            break;
                        }
                    #endregion
                    case ECmd.HM_SETUP:
                        #region
                        {
                            if (dispCtrl.PumpType != EPumpType.HM)
                            {
                                GAlarm.Prompt(EAlarm.INVALID_PUMP, gantry.Name);
                                return false;
                            }

                            var setup = new HM_Param(cmd.Para);

                            if (hmParam[gantry.Index].SameAs(setup)) break;
                            hmParam[gantry.Index] = new HM_Param(setup);
                            var fpress = pressuremaster.Master ? pressuremaster.FPress : setup.FPress;

                            if (!TFPressCtrl.FPress[gantry.Index].Set(fpress.Value)) return false;
                            pressuremaster.FPress = fpress;
                            break;
                        }
                    #endregion
                    case ECmd.SP_SETUP:
                        #region
                        {
                            if (dispCtrl.PumpType != EPumpType.SP)
                            {
                                GAlarm.Prompt(EAlarm.INVALID_PUMP, gantry.Name);
                                return false;
                            }

                            var setup = new SP_Param(cmd.Para[0], cmd.Para[6], cmd.Para[7], cmd.Para[1], cmd.Para[2], 0, cmd.Para[8]);
                            dotTime = setup.DispTime.Value;
                            if (spParam[gantry.Index].SameAs(setup)) break;

                            spParam[gantry.Index] = new SP_Param(setup);

                            var fpress = pressuremaster.Master ? pressuremaster.FPress : setup.FPress;
                            var ppress = pressuremaster.Master ? pressuremaster.PPress : setup.PPress;

                            if (!TFPressCtrl.FPress[gantry.Index].Set(fpress.Value)) return false;
                            if (!TFPressCtrl.FPress[gantry.Index + 2].Set(ppress.Value)) return false;
                            pressuremaster.FPress = fpress;
                            pressuremaster.PPress = ppress;
                            break;
                        }
                    #endregion
                    case ECmd.SPLITE_SETUP:
                    case ECmd.TP_SETUP:
                        #region
                        {
                            if (!(dispCtrl.PumpType == EPumpType.SPLite || dispCtrl.PumpType == EPumpType.TP))
                            {
                                GAlarm.Prompt(EAlarm.INVALID_PUMP, gantry.Name);
                                return false;
                            }

                            var setup = new SP_Param(cmd.Para[0], cmd.Para[6], cmd.Para[7], cmd.Para[1], cmd.Para[2], 0, cmd.Para[8]);
                            dotTime = setup.DispTime.Value;
                            if (spParam[gantry.Index].SameAs(setup)) break;
                            spParam[gantry.Index] = new SP_Param(setup);

                            var fpress = pressuremaster.Master ? pressuremaster.FPress : setup.FPress;

                            if (!TFPressCtrl.FPress[gantry.Index].Set(fpress.Value)) return false;
                            pressuremaster.FPress = fpress;
                            break;
                        }
                    #endregion

                    case ECmd.PNEUMATICS_JET_SETUP:
                        #region
                        {
                            if (dispCtrl.PumpType != EPumpType.PNEUMATIC_JET)
                            {
                                GAlarm.Prompt(EAlarm.INVALID_PUMP, gantry.Name);
                                return false;
                            }

                            var setup = new PneumaticJet_Param(cmd.Para[6], cmd.Para[7], cmd.Para[0], cmd.Para[1]);
                            dotTime = setup.DispTime.Value;
                            if (pneumaticJet_Params[gantry.Index].SameAs(setup)) break;
                            pneumaticJet_Params[gantry.Index] = new PneumaticJet_Param(setup);

                            var fpress = pressuremaster.Master ? pressuremaster.FPress : setup.FPress;
                            var ppress = pressuremaster.Master ? pressuremaster.PPress : setup.VPress;

                            if (!TFPressCtrl.FPress[gantry.Index].Set(fpress.Value)) return false;
                            if (!TFPressCtrl.FPress[gantry.Index + 2].Set(ppress.Value)) return false;

                            pressuremaster.FPress = fpress;
                            pressuremaster.PPress = ppress;
                            break;
                        }
                    #endregion

                    case ECmd.DOT_SETUP:
                        #region
                        {
                            dotTime = cmd.Para[0];
                            dotWait = cmd.Para[1];
                            break;
                        }
                    #endregion
                    case ECmd.LINE_SETUP:
                        #region
                        {
                            lineSDelay = cmd.Para[0];
                            lineSpeed = cmd.Para[1];
                            lineEDelay = cmd.Para[2];
                            lineWait = cmd.Para[3];
                            lineStartGap = cmd.Para[5];
                            lineStartLength = cmd.Para[6];
                            break;
                        }
                    #endregion
                    case ECmd.DOWN_SETUP:
                        #region
                        {
                            dispGap = cmd.Para[0];
                            dnSpeed = cmd.Para[1];
                            dnWait = cmd.Para[2];
                            break;
                        }
                    #endregion
                    case ECmd.RET_SETUP:
                        #region
                        {
                            retDist = cmd.Para[0];
                            retSpeed = cmd.Para[1];
                            retWait = cmd.Para[2];
                            break;
                        }
                    #endregion
                    case ECmd.UP_SETUP:
                        #region
                        {
                            upDist = cmd.Para[0];
                            upSpeed = cmd.Para[1];
                            upWait = cmd.Para[2];
                            break;
                        }
                    #endregion
                    case ECmd.CLUSTER_GAP_SETUP:
                        #region
                        {
                            clusterGap = cmd.Para[0];
                            break;
                        }
                    #endregion
                    case ECmd.CUT_TAIL_SETUP:
                        #region
                        {
                            cutTailType = cmd.Para[0];
                            cutTailLength = cmd.Para[1];
                            cutTailHeight = cmd.Para[2];
                            cutTailSpeed = cmd.Para[3];
                            break;
                        }
                    #endregion
                    case ECmd.DYNAMIC_JET_SETUP:
                        #region
                        {
                            predisp = (EDynamicDispMode)(int)cmd.Para[0];
                            postdisp = (EDynamicDispMode)(int)cmd.Para[2];
                            dynamic_accelDist = cmd.Para[1];

                            dyspeed = cmd.Para[3];
                            dyAcc = cmd.Para[4];
                            break;
                        }
                    #endregion

                    case ECmd.DOT:
                        #region
                        {
                            bufferModeTrackUnit = true;

                            if (!MoveToStartGap2(cmd)) return false;

                            cmdBuffer = sBase;
                            cmdBuffer += $"FORCE_SPEED ={dnSpeed} ";
                            if (runMode > ERunMode.Camera)
                            {
                                cmdBuffer += $"MOVEABSSP({lastAbsPos.X:f6},{lastAbsPos.Y:f6},{productZRun + dispGap:f6}) ";
                            }

                            if (dnWait > 0) cmdBuffer += $"MOVE_DELAY({dnWait}) ";

                            if (runMode == ERunMode.Normal)
                            {
                                var isDisp = cmd.Para[6] is 0;
                                switch (dispCtrl.PumpType)
                                {
                                    case EPumpType.VERMES_3280:
                                        {
                                            if (isDisp) cmdBuffer += $"MOVE_OP({trig.OutputNo}, 1) ";
                                            cmdBuffer += $"MOVE_DELAY({dotTime}) ";
                                            if (isDisp) cmdBuffer += $"MOVE_OP({trig.OutputNo}, 0) ";
                                            break;
                                        }
                                    case EPumpType.SP:
                                        {
                                            spParam[gantry.Index].DispTime.Value = (int)dotTime;
                                            if (isDisp) cmdBuffer += TFPump.SP.ShotCmd(spParam[gantry.Index], fpressIO, ppressIO, vacIO);
                                            break;
                                        }
                                    case EPumpType.SPLite:
                                    case EPumpType.TP:
                                        {
                                            spParam[gantry.Index].DispTime.Value = (int)dotTime;
                                            if (isDisp) cmdBuffer += TFPump.TP.ShotCmd(spParam[gantry.Index], fpressIO, ppressIO, vacIO);
                                            break;
                                        }
                                    case EPumpType.HM:
                                        {
                                            if (isDisp) cmdBuffer += TFPump.HM.ShotCmd(gantryIdx, hmParam[gantryIdx], fpressIO, vacIO);
                                            break;
                                        }
                                    case EPumpType.PNEUMATIC_JET:
                                        {
                                            if (isDisp) cmdBuffer += TFPump.PnuematicJet.ShotCmd(pneumaticJet_Params[gantryIdx], fpressIO, ppressIO);
                                            break;
                                        }
                                }
                            }

                            if (dotWait > 0) cmdBuffer += $"MOVE_DELAY({dotWait}) ";

                            if (runMode > ERunMode.Camera)
                            {
                                if (retDist > 0)
                                {
                                    cmdBuffer += $"FORCE_SPEED ={retSpeed} ";
                                    cmdBuffer += $"MOVESP(0,0,{retDist:f6}) ";
                                    cmdBuffer += $"MOVE_DELAY({retWait}) ";
                                }
                                if (upDist > 0)
                                {
                                    cmdBuffer += $"FORCE_SPEED={upSpeed} ";
                                    cmdBuffer += $"MOVESP(0,0,{upDist:f6}) ";
                                    cmdBuffer += $"MOVE_DELAY({upWait}) ";
                                }
                            }
                            TEZMCAux.DirectCommand(cmdBuffer);
                            break;
                        }
                    #endregion

                    case ECmd.LINE_START:
                        #region
                        {
                            bufferModeTrackUnit = true;

                            if (SuccedingTCmd(ECmd.LINE_END) is null) return false;

                            if (!MoveToStartGap2(cmd)) return false;

                            ECmd[] nextCmd = new ECmd[] { ECmd.LINE_PASS, ECmd.LINE_END, ECmd.CIRC_CENTER, ECmd.CIRC_PASS, ECmd.ARC_PASS, ECmd.LINE_SPEED, ECmd.LINE_SETUP, ECmd.LINE_SPEED, ECmd.LINE_GAP_ADJUST };
                            if (!nextCmd.Contains(NextTCmd().Cmd))
                            {
                                GAlarm.Prompt(EAlarm.RECIPE_INVALID_NEXT_CMD, $"Cmd {CmdEnabled.IndexOf(cmd)} " + cmd.Cmd.ToString());
                                return false;
                            }

                            cmdBuffer = sBase;
                            cmdBuffer += $"MERGE=1 ";
                            cmdBuffer += $"CORNER_MODE=0 ";
                            //cmdBuffer += $"INTERP_FACTOR=1,1,1 ";
                            cmdBuffer += $"ACCEL={moveAccel} ";
                            cmdBuffer += $"DECEL={moveAccel} ";

                            TEZMCAux.DirectCommand(cmdBuffer);

                            cmdBuffer = sBase;
                            cmdBuffer += $"FORCE_SPEED={dnSpeed} ";
                            cmdBuffer += $"STARTMOVE_SPEED=1000 ";
                            cmdBuffer += $"ENDMOVE_SPEED=1000 ";
                            //TEZMCAux.DirectCommand(cmdBuffer);

                            //cmdBuffer = sBase;
                            if (runMode > ERunMode.Camera)
                            {
                                if (lineStartGap > 0)
                                    cmdBuffer += $"MOVEABSSP({lastAbsPos.X:f6},{lastAbsPos.Y:f6},{productZRun + lineStartGap:f6}) ";
                                else
                                    cmdBuffer += $"MOVEABSSP({lastAbsPos.X:f6},{lastAbsPos.Y:f6},{productZRun + dispGap:f6}) ";
                            }

                            if (dnWait > 0)
                                cmdBuffer += $"MOVE_DELAY({dnWait}) ";
                            //TEZMCAux.DirectCommand(cmdBuffer);
                            //cmdBuffer = sBase;

                            //pump on here
                            if (runMode == ERunMode.Normal)
                            {
                                var isDisp = cmd.Para[6] is 0;
                                switch (dispCtrl.PumpType)
                                {
                                    case EPumpType.VERMES_3280:
                                        {
                                            if (isDisp) cmdBuffer += $"MOVE_OP({trig.OutputNo}, 1) ";
                                            break;
                                        }
                                    case EPumpType.SP:
                                        {
                                            if (isDisp) cmdBuffer += TFPump.SP.StartDispCmd(spParam[gantry.Index], fpressIO, ppressIO, vacIO);
                                            break;
                                        }
                                    case EPumpType.SPLite:
                                    case EPumpType.TP:
                                        {
                                            if (isDisp) cmdBuffer += TFPump.TP.StartDispCmd(spParam[gantry.Index], fpressIO, ppressIO, vacIO);
                                            break;
                                        }
                                    case EPumpType.HM:
                                        {
                                            if (isDisp) cmdBuffer += TFPump.HM.PurgeStartCmd(gantry.Index, hmParam[gantry.Index], fpressIO, vacIO);
                                            break;
                                        }
                                    case EPumpType.PNEUMATIC_JET:
                                        {
                                            if (isDisp) cmdBuffer += TFPump.PnuematicJet.StartDispCmd(pneumaticJet_Params[gantryIdx], fpressIO, ppressIO);
                                            break;
                                        }
                                }
                            }

                            if (lineSDelay > 0) cmdBuffer += $"MOVE_DELAY({lineSDelay}) ";

                            TEZMCAux.DirectCommand(cmdBuffer);
                            break;
                        }
                    #endregion
                    case ECmd.LINE_PASS:
                        #region
                        {
                            ECmd[] prevCmd = new ECmd[] { ECmd.LINE_START, ECmd.LINE_PASS, ECmd.CIRC_CENTER, ECmd.CIRC_PASS, ECmd.ARC_PASS, ECmd.LINE_SPEED, ECmd.LINE_SETUP, ECmd.LINE_SPEED, ECmd.LINE_GAP_ADJUST };
                            if (!prevCmd.Contains(PrevTCmd().Cmd))
                            {
                                GAlarm.Prompt(EAlarm.RECIPE_INVALID_PREV_CMD, $"Cmd {CmdEnabled.IndexOf(cmd)} " + cmd.Cmd.ToString());
                                return false;
                            }
                            ECmd[] nextCmd = new ECmd[] { ECmd.LINE_PASS, ECmd.CIRC_CENTER, ECmd.CIRC_PASS, ECmd.ARC_PASS, ECmd.LINE_END, ECmd.LINE_SPEED, ECmd.LINE_SETUP, ECmd.LINE_SPEED, ECmd.LINE_GAP_ADJUST };
                            if (!nextCmd.Contains(NextTCmd().Cmd))
                            {
                                GAlarm.Prompt(EAlarm.RECIPE_INVALID_NEXT_CMD, $"Cmd {CmdEnabled.IndexOf(cmd)} " + cmd.Cmd.ToString());
                                return false;
                            }

                            PointI[] ij = new PointI[2] { clusterCR, unitCR };
                            PointD ptNew = Translate(new PointD(cmd.Para[0], cmd.Para[1]) + relUnit, instBoard.LayerData[layoutNo].GetUnitAlign(ij));
                            PointD relPos = (ptNew - lastFuncRelPos);
                            lastFuncRelPos = ptNew;

                            cmdBuffer = sBase;
                            var lineEarlyCutLength = cmd.Para[9];
                            var cornerSpeed = cmd.Para[8] > 0 ? cmd.Para[8] : 1000;
                            var cornerRadius = cmd.Para[7];
                            if (cornerRadius > 0)
                            {
                                cmdBuffer += $"CORNER_MODE=32 ";
                                cmdBuffer += $"ZSMOOTH={cornerRadius:f3} ";
                                //cmdBuffer += $"CORNER_MODE={32 + 8} ";
                                //cmdBuffer += $"ZSMOOTH={cornerRadius:f3} ";
                                //cmdBuffer += $"FULL_SP_RADIUS=0.1 ";
                            }

                            if (runMode == ERunMode.Normal)
                            {
                                //var isDisp = cmd.Para[6] is 0;
                                var stopDisp = PrevTCmd().Para[6] is 0 && cmd.Para[6] is 1;

                                switch (dispCtrl.PumpType)
                                {
                                    case EPumpType.VERMES_3280:
                                        {
                                            if (stopDisp) cmdBuffer += $"MOVE_OP({trig.OutputNo}, 0) ";
                                            break;
                                        }
                                    case EPumpType.SP:
                                        {
                                            if (stopDisp) cmdBuffer += TFPump.SP.EndDispCmd(spParam[gantry.Index], fpressIO, ppressIO, vacIO);
                                            break;
                                        }
                                    case EPumpType.SPLite:
                                    case EPumpType.TP:
                                        {
                                            if (stopDisp) cmdBuffer += TFPump.TP.EndDispCmd(spParam[gantry.Index], fpressIO, ppressIO, vacIO);
                                            break;
                                        }
                                    case EPumpType.HM:
                                        {
                                            if (stopDisp) cmdBuffer += TFPump.HM.PurgeStopCmd(gantry.Index, hmParam[gantry.Index], fpressIO, vacIO);
                                            break;
                                        }
                                    case EPumpType.PNEUMATIC_JET:
                                        {
                                            if (stopDisp) cmdBuffer += TFPump.PnuematicJet.EndDispCmd(pneumaticJet_Params[gantryIdx], fpressIO, ppressIO);
                                            break;
                                        }
                                }
                            }

                            if (lineStartGap > 0 && PrevTCmd().Cmd == ECmd.LINE_START)
                            {
                                cmdBuffer += $"FORCE_SPEED={lineSpeed} ";
                                double gap = dispGap - lineStartGap;
                                double lineLength = Math.Sqrt(Math.Pow(relPos.X, 2) + Math.Pow(relPos.Y, 2));
                                //if (lineStartLength > lineLength)
                                //{
                                //    cmdBuffer += $"ENDMOVE_SPEED={cornerSpeed} ";
                                //    cmdBuffer += $"MOVESP({relPos.X:f6},{relPos.Y:f6},{gap:f6}) ";
                                //}
                                //else
                                //{
                                //    double ratio = lineStartLength / lineLength;
                                //    PointD relPos1 = new PointD(relPos.X * ratio, relPos.Y * ratio);
                                //    PointD relPos2 = new PointD(relPos.X - relPos1.X, relPos.Y - relPos1.Y);
                                //    cmdBuffer += $"MOVESP({relPos1.X:f6},{relPos1.Y:f6},{gap:f6}) ";
                                //    cmdBuffer += $"ENDMOVE_SPEED={cornerSpeed} ";
                                //    cmdBuffer += $"MOVESP({relPos2.X:f6},{relPos2.Y:f6},0) ";
                                //}
                                if (lineStartLength >= lineLength)
                                {
                                    if (lineEarlyCutLength == 0)
                                    {
                                        cmdBuffer += $"ENDMOVE_SPEED={cornerSpeed} ";
                                        cmdBuffer += $"MOVESP({relPos.X:f6},{relPos.Y:f6},{gap:f6}) ";
                                    }
                                    else
                                    if (lineEarlyCutLength > lineLength)
                                    {
                                        pauseDisp();
                                        cmdBuffer += $"ENDMOVE_SPEED={cornerSpeed} ";
                                        cmdBuffer += $"MOVESP({relPos.X:f6},{relPos.Y:f6},{gap:f6}) ";
                                        resumeDisp();
                                    }
                                    else//(lineEarlyCutLength > 0)
                                    {
                                        double ratio = (lineLength - lineEarlyCutLength) / lineLength;
                                        PointD relPos1 = new PointD(relPos.X * ratio, relPos.Y * ratio);
                                        PointD relPos2 = new PointD(relPos.X - relPos1.X, relPos.Y - relPos1.Y);
                                        double relGap1 = gap * ratio;
                                        double relGap2 = gap - relGap1;
                                        cmdBuffer += $"MOVESP({relPos1.X:f6},{relPos1.Y:f6},{relGap1:f6}) ";
                                        pauseDisp();
                                        cmdBuffer += $"ENDMOVE_SPEED={cornerSpeed} ";
                                        cmdBuffer += $"MOVESP({relPos2.X:f6},{relPos2.Y:f6},{relGap2:f6}) ";
                                        resumeDisp();
                                    }
                                }
                                else//lineStartLength < lineLength
                                {
                                    if (lineEarlyCutLength == 0)
                                    {
                                        double ratio = lineStartLength / lineLength;
                                        PointD relPos1 = new PointD(relPos.X * ratio, relPos.Y * ratio);
                                        PointD relPos2 = new PointD(relPos.X - relPos1.X, relPos.Y - relPos1.Y);
                                        cmdBuffer += $"MOVESP({relPos1.X:f6},{relPos1.Y:f6},{gap:f6}) ";
                                        cmdBuffer += $"ENDMOVE_SPEED={cornerSpeed} ";
                                        cmdBuffer += $"MOVESP({relPos2.X:f6},{relPos2.Y:f6},0) ";
                                    }
                                    else
                                    if (lineEarlyCutLength > lineLength)
                                    {
                                        pauseDisp();
                                        double ratio = lineStartLength / lineLength;
                                        PointD relPos1 = new PointD(relPos.X * ratio, relPos.Y * ratio);
                                        PointD relPos2 = new PointD(relPos.X - relPos1.X, relPos.Y - relPos1.Y);
                                        cmdBuffer += $"MOVESP({relPos1.X:f6},{relPos1.Y:f6},{gap:f6}) ";
                                        cmdBuffer += $"ENDMOVE_SPEED={cornerSpeed} ";
                                        cmdBuffer += $"MOVESP({relPos2.X:f6},{relPos2.Y:f6},0) ";
                                        resumeDisp();
                                    }
                                    else//(lineEarlyCutLength > 0)
                                    {
                                        if (lineStartLength < lineLength - lineEarlyCutLength)
                                        {
                                            double ratio1 = lineStartLength / lineLength;
                                            PointD relPos1 = new PointD(relPos.X * ratio1, relPos.Y * ratio1);

                                            double ratio2 = (lineLength - lineEarlyCutLength) / lineLength;
                                            PointD relPos2 = new PointD(relPos.X * ratio2, relPos.Y * ratio2) - relPos1;

                                            PointD relPos3 = new PointD(relPos - relPos1 - relPos2);

                                            cmdBuffer += $"MOVESP({relPos1.X:f6},{relPos1.Y:f6},{gap:f6}) ";
                                            cmdBuffer += $"MOVESP({relPos2.X:f6},{relPos2.Y:f6},0) ";
                                            cmdBuffer += $"ENDMOVE_SPEED={cornerSpeed} ";
                                            pauseDisp();
                                            cmdBuffer += $"MOVESP({relPos3.X:f6},{relPos3.Y:f6},0) ";
                                            resumeDisp();
                                        }
                                        else//(lineStartLength >= lineLength - lineEarlyCutLength)
                                        {
                                            double ratio1 = (lineLength - lineEarlyCutLength) / lineLength;
                                            PointD relPos1 = new PointD(relPos.X * ratio1, relPos.Y * ratio1);
                                            double relGap1 = gap * ratio1;

                                            double ratio2 = lineStartLength / lineLength;
                                            PointD relPos2 = new PointD(relPos.X * ratio2, relPos.Y * ratio2) - relPos1;
                                            double relGap2 = gap - relGap1;

                                            PointD relPos3 = new PointD(relPos - relPos1 - relPos2);

                                            cmdBuffer += $"MOVESP({relPos1.X:f6},{relPos1.Y:f6},{relGap1:f6}) ";
                                            pauseDisp();
                                            cmdBuffer += $"MOVESP({relPos2.X:f6},{relPos2.Y:f6},{relGap2:f6}) ";
                                            cmdBuffer += $"ENDMOVE_SPEED={cornerSpeed} ";
                                            cmdBuffer += $"MOVESP({relPos3.X:f6},{relPos3.Y:f6},0) ";
                                            resumeDisp();
                                        }
                                    }
                                }
                            }
                            //else
                            //if (lineRelGap > 0)
                            //{
                            //    cmdBuffer += $"FORCE_SPEED={lineSpeed} ";
                            //    double lineLength = Math.Sqrt(Math.Pow(relPos.X, 2) + Math.Pow(relPos.Y, 2));
                            //    if (lineRelGapLength > lineLength)
                            //    {
                            //        cmdBuffer += $"ENDMOVE_SPEED={cornerSpeed} ";
                            //        cmdBuffer += $"MOVESP({relPos.X:f6},{relPos.Y:f6},{lineRelGap:f6}) ";
                            //    }
                            //    else
                            //    {
                            //        double ratio = lineRelGapLength / lineLength;
                            //        PointD relPos1 = new PointD(relPos.X * ratio, relPos.Y * ratio);
                            //        PointD relPos2 = new PointD(relPos.X - relPos1.X, relPos.Y - relPos1.Y);
                            //        cmdBuffer += $"MOVESP({relPos1.X:f6},{relPos1.Y:f6},{lineRelGap:f6}) ";
                            //        cmdBuffer += $"ENDMOVE_SPEED={cornerSpeed} ";
                            //        cmdBuffer += $"MOVESP({relPos2.X:f6},{relPos2.Y:f6},0) ";
                            //    }
                            //    lineRelGap = lineRelGapLength = 0;
                            //}
                            else//default
                            {
                                cmdBuffer += $"FORCE_SPEED={lineSpeed} ";
                                double lineLength = Math.Sqrt(Math.Pow(relPos.X, 2) + Math.Pow(relPos.Y, 2));

                                if (lineEarlyCutLength == 0)
                                {
                                    cmdBuffer += $"ENDMOVE_SPEED={cornerSpeed} ";
                                    cmdBuffer += $"MOVESP({relPos.X:f6},{relPos.Y:f6},0) ";
                                }
                                else
                                if (lineEarlyCutLength > lineLength)
                                {
                                    pauseDisp();
                                    cmdBuffer += $"ENDMOVE_SPEED={cornerSpeed} ";
                                    cmdBuffer += $"MOVESP({relPos.X:f6},{relPos.Y:f6},0) ";
                                    resumeDisp();
                                }
                                else//(lineEarlyCutLength > 0)
                                {
                                    double ratio = (lineLength - lineEarlyCutLength) / lineLength;
                                    PointD relPos1 = new PointD(relPos.X * ratio, relPos.Y * ratio);
                                    PointD relPos2 = new PointD(relPos.X - relPos1.X, relPos.Y - relPos1.Y);
                                    cmdBuffer += $"MOVESP({relPos1.X:f6},{relPos1.Y:f6},0) ";
                                    pauseDisp();
                                    cmdBuffer += $"ENDMOVE_SPEED={cornerSpeed} ";
                                    cmdBuffer += $"MOVESP({relPos2.X:f6},{relPos2.Y:f6},0) ";
                                    resumeDisp();
                                }
                            }

                            TEZMCAux.DirectCommand(cmdBuffer);

                            void pauseDisp()
                            {
                                if (runMode == ERunMode.Normal)
                                {
                                    var isDisp = cmd.Para[6] is 0;
                                    if (!isDisp) return;

                                    switch (dispCtrl.PumpType)
                                    {
                                        case EPumpType.VERMES_3280:
                                            {
                                                cmdBuffer += $"MOVE_OP({trig.OutputNo}, 0) ";
                                                break;
                                            }
                                        case EPumpType.SP:
                                            {
                                                cmdBuffer += TFPump.SP.PauseDispCmd(spParam[gantry.Index], fpressIO, ppressIO, vacIO);
                                                break;
                                            }
                                        case EPumpType.SPLite:
                                        case EPumpType.TP:
                                            {
                                                cmdBuffer += TFPump.TP.PauseDispCmd(spParam[gantry.Index], fpressIO, ppressIO, vacIO);
                                                break;
                                            }
                                        case EPumpType.HM:
                                            {
                                                cmdBuffer += TFPump.HM.PurgePauseCmd(gantry.Index, hmParam[gantry.Index], fpressIO, vacIO);
                                                break;
                                            }
                                        case EPumpType.PNEUMATIC_JET:
                                            {
                                                cmdBuffer += TFPump.PnuematicJet.EndDispCmd(pneumaticJet_Params[gantryIdx], fpressIO, ppressIO);

                                                break;
                                            }
                                    }
                                }
                            }
                            void resumeDisp()
                            {
                                if (runMode == ERunMode.Normal)
                                {
                                    var isDisp = cmd.Para[6] is 0;
                                    if (!isDisp) return;

                                    switch (dispCtrl.PumpType)
                                    {
                                        case EPumpType.VERMES_3280:
                                            {
                                                cmdBuffer += $"MOVE_OP({trig.OutputNo}, 1) ";
                                                break;
                                            }
                                        case EPumpType.SP:
                                            {
                                                cmdBuffer += TFPump.SP.ResumeDispCmd(spParam[gantry.Index], fpressIO, ppressIO, vacIO);
                                                break;
                                            }
                                        case EPumpType.SPLite:
                                        case EPumpType.TP:
                                            {
                                                cmdBuffer += TFPump.TP.ResumeDispCmd(spParam[gantry.Index], fpressIO, ppressIO, vacIO);
                                                break;
                                            }
                                        case EPumpType.HM:
                                            {
                                                cmdBuffer += TFPump.HM.PurgeResumeCmd(gantry.Index, hmParam[gantry.Index], fpressIO, vacIO);
                                                break;
                                            }
                                        case EPumpType.PNEUMATIC_JET:
                                            {
                                                cmdBuffer += TFPump.PnuematicJet.EndDispCmd(pneumaticJet_Params[gantryIdx], fpressIO, ppressIO);

                                                break;
                                            }
                                    }
                                }
                            }
                            break;
                        }
                    #endregion
                    case ECmd.CIRC_CENTER:
                    case ECmd.CIRC_PASS:
                    case ECmd.ARC_PASS:
                        #region
                        {
                            ECmd[] prevCmd = new ECmd[] { ECmd.LINE_START, ECmd.LINE_PASS, ECmd.CIRC_CENTER, ECmd.CIRC_PASS, ECmd.ARC_PASS, ECmd.LINE_SPEED, ECmd.LINE_SETUP, ECmd.LINE_SPEED, ECmd.LINE_GAP_ADJUST };
                            if (!prevCmd.Contains(PrevTCmd().Cmd))
                            {
                                GAlarm.Prompt(EAlarm.RECIPE_INVALID_PREV_CMD, $"Command no: {CmdEnabled.IndexOf(cmd)}");
                                return false;
                            }
                            ECmd[] nextCmd = new ECmd[] { ECmd.LINE_PASS, ECmd.LINE_END, ECmd.CIRC_CENTER, ECmd.CIRC_PASS, ECmd.ARC_PASS, ECmd.LINE_SPEED, ECmd.LINE_SETUP, ECmd.LINE_SPEED, ECmd.LINE_GAP_ADJUST };
                            if (!nextCmd.Contains(NextTCmd().Cmd))
                            {
                                GAlarm.Prompt(EAlarm.RECIPE_INVALID_NEXT_CMD, $"Command no: {CmdEnabled.IndexOf(cmd)}");
                                return false;
                            }

                            PointI[] ij = new PointI[2] { clusterCR, unitCR };
                            PointD ptCenter = Translate(new PointD(cmd.Para[0], cmd.Para[1]) + relUnit, instBoard.LayerData[layoutNo].GetUnitAlign(ij));
                            PointD relCenter = (ptCenter - lastFuncRelPos);
                            //lastFuncRelPos = ptCenter;
                            PointD relPass1 = new PointD();
                            PointD relPass2 = new PointD();
                            var circCCW = cmd.Para[7] is 0 ? 0 : 1;//0=ccw

                            if (cmd.Cmd == ECmd.CIRC_PASS || cmd.Cmd == ECmd.ARC_PASS)
                            {
                                PointD ptPass1 = Translate(new PointD(cmd.Para[0], cmd.Para[1]) + relUnit, instBoard.LayerData[layoutNo].GetUnitAlign(ij));
                                relPass1 = (ptPass1 - lastFuncRelPos);
                                PointD ptPass2 = Translate(new PointD(cmd.Para[3], cmd.Para[4]) + relUnit, instBoard.LayerData[layoutNo].GetUnitAlign(ij));
                                relPass2 = (ptPass2 - lastFuncRelPos);
                                if (cmd.Cmd == ECmd.ARC_PASS)
                                {
                                    lastFuncRelPos = ptPass2;
                                }

                                relCenter = Circle.Center(new PointD(0, 0), relPass1, relPass2);
                                circCCW = Circle.SweepAngle(relCenter, new PointD(0, 0), relPass1, relPass2) < 0 ? 0 : 1;
                            }

                            cmdBuffer = sBase;
                            if (runMode == ERunMode.Normal)
                            {
                                //var isDisp = cmd.Para[6] is 0;
                                var stopDisp = PrevTCmd().Para[6] is 0 && cmd.Para[6] is 1;

                                switch (dispCtrl.PumpType)
                                {
                                    case EPumpType.VERMES_3280:
                                        {
                                            if (stopDisp) cmdBuffer += $"MOVE_OP({trig.OutputNo}, 0) ";
                                            break;
                                        }
                                    case EPumpType.SP:
                                        {
                                            if (stopDisp) cmdBuffer += TFPump.SP.EndDispCmd(spParam[gantry.Index], fpressIO, ppressIO, vacIO);
                                            break;
                                        }
                                    case EPumpType.SPLite:
                                    case EPumpType.TP:
                                        {
                                            if (stopDisp) cmdBuffer += TFPump.TP.EndDispCmd(spParam[gantry.Index], fpressIO, ppressIO, vacIO);
                                            break;
                                        }
                                    case EPumpType.HM:
                                        {
                                            if (stopDisp) cmdBuffer += TFPump.HM.PurgeStopCmd(gantry.Index, hmParam[gantry.Index], fpressIO, vacIO);
                                            break;
                                        }
                                    case EPumpType.PNEUMATIC_JET:
                                        {
                                            if (stopDisp) cmdBuffer += TFPump.PnuematicJet.EndDispCmd(pneumaticJet_Params[gantry.Index], fpressIO, ppressIO);
                                            break;
                                        }
                                }
                            }
                            cmdBuffer += $"FORCE_SPEED={lineSpeed} ";

                            switch (cmd.Cmd)
                            {
                                case ECmd.CIRC_CENTER:
                                case ECmd.CIRC_PASS:
                                    cmdBuffer += $"MOVECIRCSP(0,0,{relCenter.X:f6},{relCenter.Y:f6},{circCCW}) ";
                                    break;
                                case ECmd.ARC_PASS:
                                    cmdBuffer += $"MOVECIRCSP({relPass2.X:f6},{relPass2.Y:f6},{relCenter.X:f6},{relCenter.Y:f6},{circCCW}) ";
                                    break;
                            }

                            TEZMCAux.DirectCommand(cmdBuffer);
                            break;
                        }
                    #endregion
                    case ECmd.LINE_END:
                        #region
                        {
                            ECmd[] prevCmd = new ECmd[] { ECmd.LINE_START, ECmd.LINE_PASS, ECmd.CIRC_CENTER, ECmd.CIRC_PASS, ECmd.ARC_PASS, ECmd.LINE_SPEED, ECmd.LINE_SETUP, ECmd.LINE_SPEED, ECmd.LINE_GAP_ADJUST };
                            if (!prevCmd.Contains(PrevTCmd().Cmd))
                            {
                                GAlarm.Prompt(EAlarm.RECIPE_INVALID_PREV_CMD, $"Command no: {CmdEnabled.IndexOf(cmd)}");
                                return false;
                            }

                            PointI[] ij = new PointI[2] { clusterCR, unitCR };
                            PointD ptNew = Translate(new PointD(cmd.Para[0], cmd.Para[1]) + relUnit, instBoard.LayerData[layoutNo].GetUnitAlign(ij));
                            PointD relPos = ptNew - lastFuncRelPos;
                            lastFuncRelPos = ptNew;

                            var lineEarlyCutLength = cmd.Para[9];
                            var isDisp = cmd.Para[6] is 0;

                            cmdBuffer = sBase;
                            cmdBuffer += $"ENDMOVE_SPEED=1000 ";
                            if (runMode == ERunMode.Normal && !isDisp) endDisp();

                            if (lineEarlyCutLength == 0)
                            {
                                cmdBuffer += $"FORCE_SPEED={lineSpeed} ";
                                cmdBuffer += $"MOVESP({relPos.X:f6},{relPos.Y:f6},0) ";
                            }
                            else
                            {
                                double lineLength = Math.Sqrt(Math.Pow(relPos.X, 2) + Math.Pow(relPos.Y, 2));
                                if (lineEarlyCutLength > lineLength)
                                {
                                    //pump off here
                                    if (runMode == ERunMode.Normal && isDisp) endDisp();
                                    cmdBuffer += $"FORCE_SPEED={lineSpeed} ";
                                    cmdBuffer += $"MOVESP({relPos.X:f6},{relPos.Y:f6},0) ";
                                }
                                else//Tail Length
                                {
                                    double ratio = (lineLength - lineEarlyCutLength) / lineLength;
                                    PointD relPos1 = new PointD(relPos.X * ratio, relPos.Y * ratio);
                                    PointD relPos2 = new PointD(relPos.X - relPos1.X, relPos.Y - relPos1.Y);
                                    cmdBuffer += $"FORCE_SPEED={lineSpeed} ";
                                    cmdBuffer += $"MOVESP({relPos1.X:f6},{relPos1.Y:f6},0) ";

                                    //pump off here
                                    if (runMode == ERunMode.Normal && isDisp) endDisp();

                                    cmdBuffer += $"FORCE_SPEED={lineSpeed} ";
                                    cmdBuffer += $"MOVESP({relPos2.X:f6},{relPos2.Y:f6},0) ";
                                }
                            }

                            if (lineEDelay > 0) cmdBuffer += $"MOVE_DELAY({lineEDelay}) ";
                            if (runMode == ERunMode.Normal) endDisp();

                            void endDisp()
                            {
                                var stopDisp = PrevTCmd().Para[6] is 0;
                                if (!stopDisp) return;

                                switch (dispCtrl.PumpType)
                                {
                                    case EPumpType.VERMES_3280:
                                        {
                                            cmdBuffer += $"MOVE_OP({trig.OutputNo}, 0) ";
                                            break;
                                        }
                                    case EPumpType.SP:
                                        {
                                            cmdBuffer += TFPump.SP.EndDispCmd(spParam[gantry.Index], fpressIO, ppressIO, vacIO);
                                            break;
                                        }
                                    case EPumpType.SPLite:
                                    case EPumpType.TP:
                                        {
                                            cmdBuffer += TFPump.TP.EndDispCmd(spParam[gantry.Index], fpressIO, ppressIO, vacIO);
                                            break;
                                        }
                                    case EPumpType.HM:
                                        {
                                            cmdBuffer += TFPump.HM.PurgeStopCmd(gantry.Index, hmParam[gantry.Index], fpressIO, vacIO);
                                            break;
                                        }
                                    case EPumpType.PNEUMATIC_JET:
                                        {
                                            cmdBuffer += TFPump.PnuematicJet.EndDispCmd(pneumaticJet_Params[gantryIdx], fpressIO, ppressIO);

                                            break;
                                        }
                                }
                            }

                            if (cutTailType > (int)ECutTailType.None)
                            {
                                double lineLength = Math.Sqrt(Math.Pow(relPos.X, 2) + Math.Pow(relPos.Y, 2));
                                double tailRatio = cutTailLength / lineLength;
                                PointD cutTailPos = new PointD(relPos.X * tailRatio, relPos.Y * tailRatio);

                                cmdBuffer += $"FORCE_SPEED={cutTailSpeed} ";
                                switch (cutTailType)
                                {
                                    case (int)ECutTailType.None: break;
                                    case (int)ECutTailType.Fwd:
                                        cmdBuffer += $"MOVESP({cutTailPos.X:f6},{cutTailPos.Y:f6},{cutTailHeight:f6}) ";
                                        lastFuncRelPos.X += cutTailPos.X;
                                        lastFuncRelPos.Y += cutTailPos.Y;
                                        break;
                                    case (int)ECutTailType.Bwd:
                                        cmdBuffer += $"MOVESP({-cutTailPos.X:f6},{-cutTailPos.Y:f6},{cutTailHeight:f6}) ";
                                        lastFuncRelPos.X -= cutTailPos.X;
                                        lastFuncRelPos.Y -= cutTailPos.Y;
                                        break;
                                    case (int)ECutTailType.SqFwd:
                                        cmdBuffer += $"MOVESP(0,0,{cutTailHeight:f6}) ";
                                        cmdBuffer += $"MOVESP({cutTailPos.X:f6},{cutTailPos.Y:f6},0) ";
                                        lastFuncRelPos.X += cutTailPos.X;
                                        lastFuncRelPos.Y += cutTailPos.Y;
                                        break;
                                    case (int)ECutTailType.SqBwd:
                                        cmdBuffer += $"MOVESP(0,0,{cutTailHeight:f6}) ";
                                        cmdBuffer += $"MOVESP({-cutTailPos.X:f6},{-cutTailPos.Y:f6},0) ";
                                        lastFuncRelPos.X -= cutTailPos.X;
                                        lastFuncRelPos.Y -= cutTailPos.Y;
                                        break;
                                    case (int)ECutTailType.Rev:
                                        cmdBuffer += $"MOVESP({cutTailPos.X:f6},{cutTailPos.Y:f6},0) ";
                                        cmdBuffer += $"MOVESP({-cutTailPos.X:f6},{-cutTailPos.Y:f6},{cutTailHeight:f6}) ";
                                        break;
                                    case (int)ECutTailType.SqRev:
                                        cmdBuffer += $"MOVESP({cutTailPos.X:f6},{cutTailPos.Y:f6},0) ";
                                        cmdBuffer += $"MOVESP(0,0,{cutTailHeight:f6}) ";
                                        cmdBuffer += $"MOVESP({-cutTailPos.X:f6},{-cutTailPos.Y:f6}, 0) ";
                                        break;
                                }
                            }

                            if (lineWait > 0) cmdBuffer += $"MOVE_DELAY({lineWait}) ";

                            if (runMode > ERunMode.Camera)
                            {
                                if (retDist > 0)
                                {
                                    cmdBuffer += $"FORCE_SPEED={retSpeed} ";
                                    cmdBuffer += $"MOVESP(0,0,{retDist:f6}) ";
                                    cmdBuffer += $"MOVE_DELAY({retWait}) ";
                                }
                                if (upDist > 0)
                                {
                                    cmdBuffer += $"FORCE_SPEED={upSpeed} ";
                                    cmdBuffer += $"MOVESP(0,0,{upDist:f6}) ";
                                    cmdBuffer += $"MOVE_DELAY({upWait}) ";
                                }
                            }
                            TEZMCAux.DirectCommand(cmdBuffer);


                            break;
                        }
                    #endregion

                    case ECmd.DYNAMIC_JET_DOT_SW:
                        #region
                        {
                            var isPneumatic = dispCtrl.PumpType == EPumpType.PNEUMATIC_JET;

                            //  Define Direction
                            //  Define 1st unit
                            //  MoveTo SelectedUnit
                            //  Define Relunitpos
                            //  Define OK/NG units
                            //  DISP
                            //  lastcmd > update mapping

                            #region Define Param
                            cmdBuffer = sBase;
                            retDist = 0;
                            PointI temp_unitCR = new PointI(unitCR);
                            PointD dyoffset = new PointD();
                            var abs_point = new PointD();
                            var accel_relDist = new PointD();
                            EDynamicJetDir jetDir = (EDynamicJetDir)(int)cmd.Para[7];

                            if (firstjet)
                            {
                                dyrunpath = jetDir >= EDynamicJetDir.BackToFront ? ERunPath.Y_ZPath : ERunPath.X_ZPath;
                                instBoard.CurrentMLayout.Unit.RunPath = dyrunpath;
                                GRecipes.MultiLayout[gantry.Index][layoutNo].Unit.RunPath = dyrunpath;
                            }
                            else
                            {
                                var instantrunpath = jetDir >= EDynamicJetDir.BackToFront ? ERunPath.Y_ZPath : ERunPath.X_ZPath;
                                if (dyrunpath != instantrunpath)
                                {
                                    GAlarm.Prompt(EAlarm.DYNAMIC_JET_INVAID_DIRECTION, "Cant mix X and Y dir in a recipe");
                                    return false;
                                }
                            }
                            bool isRow = dyrunpath == ERunPath.X_SPath || dyrunpath == ERunPath.X_ZPath;

                            bool[] array;
                            switch (jetDir)
                            {
                                default:
                                case EDynamicJetDir.LeftToRight:
                                case EDynamicJetDir.RightToLeft: array = map.GetRowArray(clusterCR, unitCR); break;
                                case EDynamicJetDir.FrontToBack:
                                case EDynamicJetDir.BackToFront: array = map.GetColArray(clusterCR, unitCR); break;
                            }

                            if (firstjet)
                            {
                                PointD localUnitRel = Translate(relUnit, instBoard.LayerData[layoutNo].GetUnitAlign(new PointI[] { clusterCR, unitCR }));

                                var isNext = true;
                                if (isRow && instBoard.CurrentMLayout.Unit.IsRowEnd(unitCR)) isNext = false;
                                if (!isRow && instBoard.CurrentMLayout.Unit.IsColEnd(unitCR)) isNext = false;

                                var nextunit = isNext ? instBoard.CurrentMLayout.Unit.NextCR(unitCR) : instBoard.CurrentMLayout.Unit.PrevCR(unitCR);
                                var relUnit2 = instBoard.CurrentMLayout.Unit.RelPos(nextunit);
                                PointD localUnitRel2 = Translate(relUnit2, instBoard.LayerData[layoutNo].GetUnitAlign(new PointI[] { clusterCR, nextunit }));
                                dy_unitRel = localUnitRel2 - localUnitRel;

                                if (!isNext) dy_unitRel = new PointD() - dy_unitRel;
                            }

                            EDynamicJetDir layoutdir = isRow ? dy_unitRel.X > 0 ? EDynamicJetDir.LeftToRight : EDynamicJetDir.RightToLeft : dy_unitRel.Y > 0 ? EDynamicJetDir.FrontToBack : EDynamicJetDir.BackToFront;

                            dy_serp_unitRel = layoutdir == jetDir ? dy_unitRel : new PointD() - dy_unitRel;

                            double ratio = (double)Math.Sqrt((double)Math.Pow(dy_serp_unitRel.X, (double)2) + (double)Math.Pow(dy_serp_unitRel.Y, (double)2));
                            double ratiocount = Math.Max(dynamic_accelDist / ratio, 1);
                            accel_relDist = new PointD(dy_serp_unitRel.X * ratiocount, dy_serp_unitRel.Y * ratiocount);
                            dyoffset = new PointD(GSetupPara.Calibration.DynamicOffsets[gantryIdx, isRow ? dy_serp_unitRel.X >= 0 ? 0 : 1 : (dy_serp_unitRel.Y >= 0) ? 2 : 3]);

                            #endregion

                            #region InitPos
                            if (layoutdir != jetDir)
                            {
                                int lastidx = Array.LastIndexOf(array, true);
                                unitCR = new PointI(isRow ? lastidx : unitCR.X, isRow ? unitCR.Y : lastidx);
                            }

                            var rev_accel_relDist = new PointD() - accel_relDist;

                            offsetXY += rev_accel_relDist;
                            if (!MoveToStartGap2(cmd, true)) return false;
                            offsetXY -= rev_accel_relDist;

                            cmdBuffer = sBase;
                            abs_point = new PointD(lastAbsPos.GetPointD());
                            //unitCR = new PointI(temp_unitCR);
                            #endregion

                            #region Motion
                            cmdBuffer += $"MERGE=1 ";
                            cmdBuffer += $"FORCE_SPEED={moveSpeed} ";
                            cmdBuffer += $"CORNER_MODE=32 ";
                            var radius = 1;
                            cmdBuffer += $"ZSMOOTH={radius} ";
                            TEZMCAux.DirectCommand(cmdBuffer);
                            cmdBuffer += sBase;

                            //come to pre-disp positon and exclude one-step of relative position of disp
                            List<double> table_points = new List<double>();

                            abs_point += dyoffset + accel_relDist;

                            if (predisp == EDynamicDispMode.Everytime || firstjet && FunctionFirstExecution && predisp == EDynamicDispMode.FirstJet)
                            {
                                var prevpoint = new PointD(abs_point);
                                for (int i = 0; i < (int)ratiocount; i++)
                                {
                                    var stoppt = prevpoint - new PointD(dy_serp_unitRel.X / 2, dy_serp_unitRel.Y / 2);
                                    table_points.Insert(0, isRow ? stoppt.X : stoppt.Y);

                                    prevpoint -= dy_serp_unitRel;
                                    table_points.Insert(0, isRow ? prevpoint.X : prevpoint.Y);
                                }
                            }

                            var dd = new PointD(abs_point);
                            abs_point -= dy_serp_unitRel;
                            //firstjet = false;

                            int startIdx = isRow ? unitCR.X : unitCR.Y;
                            int finalIdx = startIdx;

                            var arr_dispIdx = new List<int>();
                            var arr = isRow ? map.GetRowArray(clusterCR, serpentine_CR) : map.GetColArray(clusterCR, serpentine_CR);

                            while (true)
                            {
                                abs_point += dy_serp_unitRel;
                                var stoppt = new PointD(abs_point) + new PointD(dy_serp_unitRel.X / 2, dy_serp_unitRel.Y / 2);

                                if (arr[finalIdx]) arr_dispIdx.Add(finalIdx);

                                if (runMode == ERunMode.Normal)
                                {
                                    var nextpoint = isRow ? abs_point.X : abs_point.Y;
                                    if (arr[finalIdx])
                                    {
                                        table_points.Add(nextpoint);
                                        table_points.Add(isRow ? stoppt.X : stoppt.Y);
                                    }
                                }

                                arr[finalIdx] = false;
                                if (arr.Where(x => x).Count() is 0) break;

                                //apply logic
                                if (jetDir == layoutdir) finalIdx++; else finalIdx--;
                            }

                            //abs_point += accel_relDist;

                            if (postdisp == EDynamicDispMode.Everytime || firstjet && FunctionFirstExecution && postdisp == EDynamicDispMode.FirstJet)
                            {
                                var prevpoint = new PointD(abs_point);
                                for (int i = 0; i < (int)ratiocount; i++)
                                {
                                    prevpoint += dy_serp_unitRel;
                                    table_points.Add(isRow ? prevpoint.X : prevpoint.Y);

                                    var stoppt = prevpoint + new PointD(dy_serp_unitRel.X / 2, dy_serp_unitRel.Y / 2);
                                    table_points.Add(isRow ? stoppt.X : stoppt.Y);
                                }
                            }

                            abs_point += accel_relDist;
                            firstjet = false;

                            #endregion

                            #region Jet
                            if (runMode == ERunMode.Normal)
                            {
                                int table_startIdx = (gantryIdx + 1) * 10000;
                                int table_endIdx = table_startIdx + table_points.Count - 1;

                                for (int i = 0; i < table_points.Count; i++)
                                {
                                    cmdBuffer = $"TABLE({table_startIdx + i},{table_points[i]}) ";
                                    TEZMCAux.DirectCommand(cmdBuffer);
                                }

                                cmdBuffer = $"BASE({(isRow ? gantry.XAxis.AxisNo : gantry.YAxis.AxisNo)}) ";
                                cmdBuffer += $"HW_PSWITCH2(1,{trig.OutputNo},1,{table_startIdx},{table_endIdx},{(table_points[1] - table_points[0] > 0 ? 1 : 0)}) ";

                                if (isPneumatic)
                                {
                                    double cycletime = pneumaticJet_Params[gantry.Index].DispTime.Value + pneumaticJet_Params[gantry.Index].OffTime.Value;
                                    double optime = pneumaticJet_Params[gantry.Index].DispTime.Value;


                                    cmdBuffer += $"HW_TIMER(2, {Math.Max(2, cycletime * 1000)},{Math.Max(1, optime * 1000)},1,OFF,{trig.OutputNo}) ";
                                }

                                TEZMCAux.Execute(cmdBuffer);
                                cmdBuffer = sBase;
                            }

                            cmdBuffer += $"MOVEABSSP({abs_point.X},{abs_point.Y}) ";

                            while (gantry.Busy) Thread.Sleep(0);

                            TEZMCAux.DirectCommand(cmdBuffer);
                            Thread.Sleep(0);
                            while (gantry.Busy) Thread.Sleep(0);

                            cmdBuffer = $"BASE({(isRow ? gantry.XAxis.AxisNo : gantry.YAxis.AxisNo)}) ";
                            cmdBuffer += $"HW_PSWITCH2(2) ";
                            cmdBuffer += $"HW_TIMER(0,0,0,1,0,{trig.OutputNo}) ";
                            TEZMCAux.Execute(cmdBuffer);
                            #endregion

                            #region Ending + SetState
                            unitCR = new PointI(temp_unitCR);

                            if (CmdEnabled.Where(x => x.Cmd == cmd.Cmd).LastOrDefault() == cmd)
                            {
                                foreach (var i in arr_dispIdx)
                                {
                                    var unit = isRow ? new PointI(i, unitCR.Y) : new PointI(unitCR.X, i);
                                    map.SetState(clusterCR, new PointI(unit), EDispState.COMPLETE);
                                }
                                unitCR = isRow ? new PointI(arr.Count() - 1, unitCR.Y) : new PointI(unitCR.X, arr.Count() - 1);
                            }
                            #endregion

                            break;
                        }
                    #endregion

                    case ECmd.PAT_ALIGN_SETUP:
                        #region
                        {
                            settleTime = cmd.Para[0] > 0 ? (int)cmd.Para[0] : GProcessPara.Vision.SettleTime.Value;
                            break;
                        }
                    #endregion
                    case ECmd.PAT_ALIGN_UNIT:
                        #region
                        {
                            PointI[] ij = new PointI[2] { clusterCR, unitCR };

                            PointD localUnitRel = Translate(relUnit, instBoard.LayerData[layoutNo].GetUnitAlign(ij));
                            PointD unitOrigin = boardOrigin + relCluster + localUnitRel;
                            TAlignData alignData = new TAlignData();

                            while (gantry.Axis[0].Busy)
                            {
                                if (!running) return false;
                                Thread.Sleep(0);
                            }
                            switch (PatAlignExecute(gantry, unitOrigin, cmd, ref alignData, settleTime))
                            {
                                case EAction.Skip:
                                    {
                                        if (runMode == ERunMode.Camera) TFCameras.Camera[gantry.Index].FlirCamera.Live();
                                        state = EDispState.NG;
                                        return true;
                                    }
                                case EAction.Fail:
                                    {
                                        if (runMode == ERunMode.Camera) TFCameras.Camera[gantry.Index].FlirCamera.Live();
                                        return false;
                                    }
                                case EAction.Accept:
                                    {
                                        if (runMode == ERunMode.Camera) TFCameras.Camera[gantry.Index].FlirCamera.Live();
                                        break;
                                    }
                            }

                            instBoard.LayerData[layoutNo].SetUnitAlign(ij, alignData);
                            instBoard.MAP.SetState(ij[0], ij[1], state);
                            break;
                        }
                    #endregion
                    case ECmd.PAT_ALIGN_CLUSTER:
                        #region
                        {
                            PointI[] ij = new PointI[2] { clusterCR, unitCR };

                            if (instBoard.LayerData[layoutNo].GetUnitAlign(ij).Status >= EPatAlignStatus.ClusterOK) break;

                            PointD clusterRel = GRecipes.MultiLayout[gantry.Index][layoutNo].Cluster.RelPos(clusterCR);
                            PointD originAbs = boardOrigin + clusterRel;

                            //var newrelunit = GRecipes.MultiLayout[gantry.Index][layoutNo].Unit.RelPos(new PointI());
                            //PointD ptRel = Translate(new PointD(cmd.Para[0], cmd.Para[1]) + newrelunit, instBoard.LayerData[layoutNo].GetUnitAlign(ij));
                            //PointD clusterRel = GRecipes.MultiLayout[gantry.Index][layoutNo].Cluster.RelPos(instBoard.ClusterCR);
                            //PointD originAbs = boardOrigin + relCluster + ptRel;

                            TAlignData alignData = new TAlignData();

                            while (gantry.Axis[0].Busy)
                            {
                                if (!running) return false;
                                Thread.Sleep(0);
                            }
                            switch (PatAlignExecute(gantry, originAbs, cmd, ref alignData, settleTime))
                            {
                                case EAction.Skip:
                                    {
                                        if (runMode == ERunMode.Camera) TFCameras.Camera[gantry.Index].FlirCamera.Live();
                                        state = EDispState.NG;
                                        return true;
                                    }
                                case EAction.Fail:
                                    {
                                        if (runMode == ERunMode.Camera) TFCameras.Camera[gantry.Index].FlirCamera.Live();
                                        return false;
                                    }
                                case EAction.Accept:
                                    {
                                        if (runMode == ERunMode.Camera) TFCameras.Camera[gantry.Index].FlirCamera.Live();
                                        break;
                                    }
                            }

                            if (alignData.Status == EPatAlignStatus.OK) alignData.Status = EPatAlignStatus.ClusterOK;

                            EndclusterCRs.Add(new PointI(clusterCR));
                            var currentunitCR = new PointI();
                            while (true)
                            {
                                instBoard.LayerData[layoutNo].SetUnitAlign(new PointI[] { clusterCR, currentunitCR }, alignData);
                                //instBoard.MAP.SetState(clusterCR, currentunitCR, state);

                                if (instBoard.MAP.GetState(clusterCR, currentunitCR) == EDispState.READY)
                                {
                                    instBoard.MAP.SetState(clusterCR, currentunitCR, state);
                                }

                                EndunitCRs.Add(new PointI(currentunitCR));
                                currentunitCR = GRecipes.MultiLayout[gantry.Index][LayoutNo].Unit.NextCR(currentunitCR);
                                if (currentunitCR.IsZero) break;
                            }
                            break;
                        }
                    #endregion
                    case ECmd.PAT_ALIGN_BOARD:
                        #region
                        {
                            PointI[] ij = new PointI[2] { clusterCR, unitCR };

                            if (instBoard.LayerData[layoutNo].GetUnitAlign(ij).Status >= EPatAlignStatus.BoardOK) break;
                            PointD clusterRel = GRecipes.MultiLayout[gantry.Index][layoutNo].Cluster.RelPos(new PointI());
                            PointD originAbs = boardOrigin + clusterRel;
                            TAlignData alignData = new TAlignData();

                            while (gantry.Axis[0].Busy)
                            {
                                if (!running) return false;
                                Thread.Sleep(0);
                            }
                            switch (PatAlignExecute(gantry, originAbs, cmd, ref alignData, settleTime))
                            {
                                case EAction.Skip:
                                    {
                                        if (runMode == ERunMode.Camera) TFCameras.Camera[gantry.Index].FlirCamera.Live();
                                        state = EDispState.NG;
                                        return true;
                                    }
                                case EAction.Fail:
                                    {
                                        if (runMode == ERunMode.Camera) TFCameras.Camera[gantry.Index].FlirCamera.Live();
                                        return false;
                                    }
                                case EAction.Accept:
                                    {
                                        if (runMode == ERunMode.Camera) TFCameras.Camera[gantry.Index].FlirCamera.Live();
                                        break;
                                    }
                            }

                            var mlayout = GRecipes.MultiLayout[gantry.Index][LayoutNo];
                            var currentClusterCR = new PointI();

                            if (alignData.Status == EPatAlignStatus.OK) alignData.Status = EPatAlignStatus.BoardOK;

                            while (true)
                            {
                                var ad = new TAlignData(alignData);

                                var clusterRelPos = instBoard.CurrentMLayout.Cluster.RelPos(currentClusterCR);
                                var relDatum = alignData.Datum - clusterRelPos;
                                ad.Datum = relDatum;
                                EndclusterCRs.Add(new PointI(currentClusterCR));

                                var currentunitCR = new PointI();
                                while (true)
                                {
                                    instBoard.LayerData[layoutNo].SetUnitAlign(new PointI[] { currentClusterCR, currentunitCR }, ad);

                                    if (instBoard.MAP.GetState(currentClusterCR, currentunitCR) == EDispState.READY)
                                    {
                                        instBoard.MAP.SetState(currentClusterCR, currentunitCR, state);
                                    }

                                    EndunitCRs.Add(new PointI(currentunitCR));
                                    currentunitCR = mlayout.Unit.NextCR(currentunitCR);
                                    if (currentunitCR.IsZero) break;
                                }

                                currentClusterCR = mlayout.Cluster.NextCR(currentClusterCR);
                                if (currentClusterCR.IsZero) break;
                            }
                            break;
                        }
                    #endregion

                    case ECmd.DYNAMIC_SAVE_IMAGE:
                        #region
                        {
                            PointI[] ij = new PointI[] { clusterCR, unitCR };
                            PointD localUnitRel = Translate(relUnit, instBoard.LayerData[layoutNo].GetUnitAlign(ij));
                            PointD unitOrigin = boardOrigin + relCluster + localUnitRel;
                            PointD unitPos = new PointD(unitOrigin.X + cmd.Para[0], unitOrigin.Y + cmd.Para[1]);

                            double dySpeed = cmd.Para[6];
                            double adMoveDist = 2;

                            cmdBuffer = sBase;

                            switch (GRecipes.MultiLayout[gantry.Index][layoutNo].Layouts[1].FirstLastCR(unitCR))
                            {
                                case ELayoutFirstLast.First:
                                    TFCameras.Camera[gantryIdx].FlirCamera.Stop();

                                    TFCameras.Camera[gantryIdx].FlirCamera.Gain = GSystemCfg.Camera.Cameras[gantryIdx].DynamicGain;
                                    TFCameras.Camera[gantryIdx].FlirCamera.Exposure = GSystemCfg.Camera.Cameras[gantryIdx].DynamicExposure;

                                    TFCameras.Camera[gantryIdx].FlirCamera.TrigMode = true;
                                    TFCameras.Camera[gantryIdx].FlirCamera.TrigSourceHw = true;
                                    TFCameras.Camera[gantryIdx].FlirCamera.Grab(3000);
                                    TFCameras.Camera[gantryIdx].FlirCamera.saveImage = 0;

                                    cmdBuffer += $"MERGE=1 ";
                                    cmdBuffer += $"ACCEL={GProcessPara.Operation.GXYSpeed[2]} ";
                                    cmdBuffer += $"DECEL={GProcessPara.Operation.GXYSpeed[2]} ";
                                    cmdBuffer += $"FORCE_SPEED={dySpeed} ";
                                    cmdBuffer += $"CORNER_MODE=32 ";
                                    var radius = 1;
                                    cmdBuffer += $"ZSMOOTH={radius} ";
                                    break;
                            }

                            PointD prePos = new PointD(unitPos);
                            switch (GRecipes.MultiLayout[gantry.Index][layoutNo].Layouts[1].SequenceCR(unitCR))
                            {
                                case ELayoutSequence.FirstOfRow:
                                    switch (GRecipes.MultiLayout[gantry.Index][layoutNo].Layouts[1].NextMoveDir(unitCR))
                                    {
                                        case ELayoutNextMoveDir.X_Minus:
                                            prePos = unitPos + new PointD(adMoveDist, 0);
                                            break;
                                        case ELayoutNextMoveDir.X_Plus:
                                            prePos = unitPos + new PointD(-adMoveDist, 0);
                                            break;
                                    }
                                    cmdBuffer += $"MOVEABSSP({prePos.X:f6},{prePos.Y:f6},{GRecipes.Board[gantry.Index].StartPos.Z:f6}) ";
                                    break;
                                case ELayoutSequence.FirstOfCol:
                                    switch (GRecipes.MultiLayout[gantry.Index][layoutNo].Layouts[1].NextMoveDir(unitCR))
                                    {
                                        case ELayoutNextMoveDir.Y_Minus:
                                            prePos = unitPos + new PointD(0, adMoveDist);
                                            break;
                                        case ELayoutNextMoveDir.Y_Plus:
                                            prePos = unitPos + new PointD(0, -adMoveDist);
                                            break;
                                    }
                                    cmdBuffer += $"MOVEABSSP({prePos.X:f6},{prePos.Y:f6},{GRecipes.Board[gantry.Index].StartPos.Z:f6}) ";
                                    break;
                            }

                            cmdBuffer += $"MOVEABSSP({unitPos.X},{unitPos.Y},{GRecipes.Board[gantry.Index].StartPos.Z}) ";
                            cmdBuffer += $"MOVE_OP2({trigCam.OutputNo}, 1, 10) ";


                            PointD postPos = new PointD(unitPos);
                            switch (GRecipes.MultiLayout[gantry.Index][layoutNo].Layouts[1].SequenceCR(unitCR))
                            {
                                case ELayoutSequence.LastOfRow:
                                    switch (GRecipes.MultiLayout[gantry.Index][layoutNo].Layouts[1].NextMoveDir(unitCR))
                                    {
                                        case ELayoutNextMoveDir.X_Minus:
                                            prePos = unitPos + new PointD(-adMoveDist, 0);
                                            break;
                                        case ELayoutNextMoveDir.X_Plus:
                                            prePos = unitPos + new PointD(adMoveDist, 0);
                                            break;
                                    }
                                    cmdBuffer += $"MOVEABSSP({postPos.X:f6},{postPos.Y:f6},{GRecipes.Board[gantry.Index].StartPos.Z:f6}) ";
                                    break;
                                case ELayoutSequence.LastOfCol:
                                    switch (GRecipes.MultiLayout[gantry.Index][layoutNo].Layouts[1].NextMoveDir(unitCR))
                                    {
                                        case ELayoutNextMoveDir.X_Minus:
                                            prePos = unitPos + new PointD(0, adMoveDist);
                                            break;
                                        case ELayoutNextMoveDir.X_Plus:
                                            prePos = unitPos + new PointD(0, -adMoveDist);
                                            break;
                                    }
                                    cmdBuffer += $"MOVEABSSP({postPos.X:f6},{postPos.Y:f6},{GRecipes.Board[gantry.Index].StartPos.Z:f6}) ";
                                    break;
                            }


                            //if (!bDySaveImage)
                            //{
                            //    TFCameras.Camera[gantryIdx].FlirCamera.TrigMode = true;
                            //    TFCameras.Camera[gantryIdx].FlirCamera.Grab(3000);
                            //    TFCameras.Camera[gantryIdx].FlirCamera.saveImage = 0;
                            //    bDySaveImage = true;

                            //    cmdBuffer += $"MERGE=1 ";
                            //    cmdBuffer += $"FORCE_SPEED={dySpeed} ";
                            //    cmdBuffer += $"MOVEABSSP({unitOrigin.X + cmd.Para[0]},{unitOrigin.Y + cmd.Para[1]},{GRecipes.Board[gantry.Index].StartPos.Z}) ";
                            //    cmdBuffer += $"MOVE_Delay({100}) ";
                            //}
                            //else
                            //{
                            //    cmdBuffer += $"MOVEABSSP({unitOrigin.X + cmd.Para[0]},{unitOrigin.Y + cmd.Para[1]},{GRecipes.Board[gantry.Index].StartPos.Z}) ";
                            //    cmdBuffer += $"MOVE_OP2({trigCam.OutputNo}, 1, 10) ";
                            //}
                            TEZMCAux.DirectCommand(cmdBuffer);

                            if (GRecipes.MultiLayout[gantry.Index][layoutNo].Layouts[1].FirstLastCR(unitCR) == ELayoutFirstLast.Last)
                            {
                                while (gantry.Axis[0].Busy)
                                {
                                    if (!running) return false;
                                    Thread.Sleep(0);
                                }
                                TFCameras.Camera[gantryIdx].FlirCamera.TrigMode = false;
                                TFCameras.Camera[gantryIdx].FlirCamera.Gain = GSystemCfg.Camera.Cameras[gantryIdx].Gain;
                                TFCameras.Camera[gantryIdx].FlirCamera.Exposure = GSystemCfg.Camera.Cameras[gantryIdx].Exposure;
                            }

                            break;
                        }
                    #endregion

                    case ECmd.HEIGHT_SETUP:
                        #region
                        {
                            hsensorSettleTime = cmd.Para[0] > 0 ? (int)cmd.Para[0] : GProcessPara.HSensor.SettleTime.Value;
                            heightRangeULimit = cmd.Para[1] > 0 ? cmd.Para[1] : 1;
                            break;
                        }
                    #endregion
                    case ECmd.HEIGHT_ALIGN_UNIT:
                        #region
                        {
                            PointI[] ij = new PointI[2] { clusterCR, unitCR };

                            PointD localUnitRel = Translate(new PointD(cmd.Para[0], cmd.Para[1]) + relUnit, instBoard.LayerData[layoutNo].GetUnitAlign(ij));
                            PointD unitOrigin = boardOrigin + relCluster + localUnitRel;

                            unitOrigin = unitOrigin + GSetupPara.Calibration.LaserOffset[gantry.Index];

                            THeightData heightData = new THeightData();
                            cmd.Para[3] = hsensorSettleTime;

                            while (gantry.Axis[0].Busy)
                            {
                                if (!running) return false;
                                Thread.Sleep(0);
                            }
                            if (!HeightAlignExecute(gantry, unitOrigin, cmd, ref heightData)) return false;

                            hsensorValue.Add(heightData.SensorValue);

                            switch (HeightCal(hsensorValue, heightRangeULimit, clusterCR, unitCR, out double avr))
                            {
                                case EAction.Accept: break;
                                case EAction.Fail: return false;
                                case EAction.Skip: state = EDispState.NG; return true;
                            }
                            heightData.SensorValue = avr;
                            instBoard.LayerData[layoutNo].SetUnitHeight(ij, heightData);
                            instBoard.MAP.SetState(ij[0], ij[1], state);
                            break;
                        }
                    #endregion
                    case ECmd.HEIGHT_ALIGN_CLUSTER:
                        #region
                        {
                            PointI[] ij = new PointI[2] { clusterCR, unitCR };

                            if (instBoard.LayerData[layoutNo].GetUnitHeight(ij).Status >= EHeightAlignStatus.Aligned) break;

                            var newrelunit = GRecipes.MultiLayout[gantry.Index][layoutNo].Unit.RelPos(new PointI());
                            PointD ptRel = Translate(new PointD(cmd.Para[0], cmd.Para[1]) + newrelunit, instBoard.LayerData[layoutNo].GetUnitAlign(ij));
                            PointD originAbs = boardOrigin + relCluster + ptRel;

                            originAbs += GSetupPara.Calibration.LaserOffset[gantry.Index];

                            THeightData heightData = new THeightData();
                            cmd.Para[3] = hsensorSettleTime;

                            while (gantry.Axis[0].Busy)
                            {
                                if (!running) return false;
                                Thread.Sleep(0);
                            }
                            if (!HeightAlignExecute(gantry, originAbs, cmd, ref heightData)) return false;
                            hsensorValue.Add(heightData.SensorValue);

                            switch (HeightCal(hsensorValue, heightRangeULimit, clusterCR, unitCR, out double avr))
                            {
                                case EAction.Accept: break;
                                case EAction.Fail: return false;
                                case EAction.Skip: state = EDispState.NG; return true;
                            }
                            heightData.SensorValue = avr;

                            EndclusterCRs.Add(new PointI(clusterCR));
                            for (int i = 0; i < GRecipes.MultiLayout[gantry.Index][layoutNo].Unit.CR.X; i++)
                                for (int j = 0; j < GRecipes.MultiLayout[gantry.Index][layoutNo].Unit.CR.Y; j++)
                                {
                                    instBoard.LayerData[layoutNo].SetUnitHeight(new PointI[2] { clusterCR, new PointI(i, j) }, heightData);

                                    if (instBoard.MAP.GetState(clusterCR, new PointI(i, j)) == EDispState.READY)
                                    {
                                        instBoard.MAP.SetState(clusterCR, new PointI(i, j), state);
                                    }

                                    EndunitCRs.Add(new PointI(i, j));
                                }

                            instBoard.LayerData[layoutNo].GetUnitHeight(ij).Status = EHeightAlignStatus.Processing;
                            if (C_count++ >= CmdEnabled.Where(x => x.Cmd == cmd.Cmd).Count())
                            {
                                instBoard.LayerData[layoutNo].GetUnitHeight(ij).Status = EHeightAlignStatus.Aligned;
                                hsensorValue.Clear();
                            }
                            break;
                        }
                    #endregion
                    case ECmd.HEIGHT_ALIGN_BOARD:
                        #region
                        {
                            PointI[] ij = new PointI[2] { clusterCR, unitCR };

                            if (instBoard.LayerData[layoutNo].GetUnitHeight(ij).Status >= EHeightAlignStatus.Aligned) break;

                            var newrelcluster = GRecipes.MultiLayout[gantry.Index][layoutNo].Cluster.RelPos(new PointI());
                            var newrelunit = GRecipes.MultiLayout[gantry.Index][layoutNo].Unit.RelPos(new PointI());
                            PointD ptRel = Translate(new PointD(cmd.Para[0], cmd.Para[1]) + newrelunit, instBoard.LayerData[layoutNo].GetUnitAlign(ij));
                            PointD originAbs = boardOrigin + newrelcluster + ptRel;

                            originAbs += GSetupPara.Calibration.LaserOffset[gantry.Index];

                            THeightData heightData = new THeightData();
                            cmd.Para[3] = hsensorSettleTime;

                            while (gantry.Axis[0].Busy)
                            {
                                if (!running) return false;
                                Thread.Sleep(0);
                            }
                            if (!HeightAlignExecute(gantry, originAbs, cmd, ref heightData)) return false;

                            hsensorValue.Add(heightData.SensorValue);
                            switch (HeightCal(hsensorValue, heightRangeULimit, clusterCR, unitCR, out double avr))
                            {
                                case EAction.Accept: break;
                                case EAction.Fail: return false;
                                case EAction.Skip: state = EDispState.NG; return true;
                            }

                            heightData.SensorValue = avr;


                            var mlayout = instBoard.CurrentMLayout;
                            var currentClusterCR = new PointI(/*InstBoard.ClusterCR*/);

                            while (true)
                            {
                                EndclusterCRs.Add(new PointI(currentClusterCR));

                                for (int i = 0; i < GRecipes.MultiLayout[gantry.Index][layoutNo].Unit.CR.X; i++)
                                    for (int j = 0; j < GRecipes.MultiLayout[gantry.Index][layoutNo].Unit.CR.Y; j++)
                                    {
                                        instBoard.LayerData[layoutNo].SetUnitHeight(new PointI[2] { currentClusterCR, new PointI(i, j) }, heightData);

                                        if (instBoard.MAP.GetState(currentClusterCR, new PointI(i, j)) == EDispState.READY)
                                        {
                                            instBoard.MAP.SetState(currentClusterCR, new PointI(i, j), state);
                                        }

                                        EndunitCRs.Add(new PointI(i, j));
                                    }

                                currentClusterCR = mlayout.Cluster.NextCR(currentClusterCR);

                                if (currentClusterCR.IsZero) break;
                            }

                            instBoard.LayerData[layoutNo].GetUnitHeight(ij).Status = EHeightAlignStatus.Processing;
                            if (C_count++ >= CmdEnabled.Where(x => x.Cmd == cmd.Cmd).Count())
                            {
                                instBoard.LayerData[layoutNo].GetUnitHeight(ij).Status = EHeightAlignStatus.Aligned;
                                hsensorValue.Clear();
                            }
                            break;
                        }
                    #endregion
                    case ECmd.HEIGHT_SET:
                        #region
                        {
                            int cmdIndex = CmdEnabled.IndexOf(cmd);
                            int nextCmdIndex = cmdIndex + 1;
                            if (CmdEnabled[nextCmdIndex].Cmd == ECmd.LINE_START || CmdEnabled[nextCmdIndex].Cmd == ECmd.DOT)
                            { }
                            else
                            {
                                GAlarm.Prompt(EAlarm.RECIPE_HEIGHTSET_INVALID_NEXT_CMD);
                                return false;
                            }

                            PointI[] ij = new PointI[2] { clusterCR, unitCR };

                            PointD localUnitRel = Translate(new PointD(CmdEnabled[nextCmdIndex].Para[0], CmdEnabled[nextCmdIndex].Para[1]) + relUnit, instBoard.LayerData[layoutNo].GetUnitAlign(ij));
                            PointD unitOrigin = boardOrigin + relCluster + localUnitRel;

                            unitOrigin = unitOrigin + GSetupPara.Calibration.LaserOffset[gantry.Index];

                            THeightData heightData = new THeightData();
                            cmd.Para[3] = hsensorSettleTime;

                            while (gantry.Axis[0].Busy)
                            {
                                if (!running) return false;
                                Thread.Sleep(0);
                            }
                            if (!HeightAlignExecute(gantry, unitOrigin, cmd, ref heightData)) return false;

                            HeightSetData = new THeightData(heightData);
                            break;
                        }
                    #endregion

                    case ECmd.NEEDLE_VAC_CLEAN:
                    case ECmd.NEEDLE_FLUSH:
                    case ECmd.NEEDLE_PURGE:
                        #region
                        {
                            var fpress_state = fpressIO.Status;

                            var n_dnwait = (int)cmd.Para[3];
                            var n_disptime = (int)cmd.Para[4];
                            var n_vactime = (int)cmd.Para[5];
                            var n_postvactime = (int)cmd.Para[6];
                            var n_postwait = (int)cmd.Para[7];

                            var n_count = Math.Max((int)cmd.Para[0], 1);
                            var n_perUnit = (int)cmd.Para[1];

                            if (n_perUnit <= 0) break;

                            #region
                            var mode = cmd.Cmd == ECmd.NEEDLE_PURGE ? ENeedleCleanMode.Purge : cmd.Cmd == ECmd.NEEDLE_FLUSH ? ENeedleCleanMode.Flush : ENeedleCleanMode.VacClean;

                            bool run = false;

                            switch (mode)
                            {
                                case ENeedleCleanMode.VacClean:
                                    {
                                        if (NeedleCleanCount++ % n_perUnit != 0) break;
                                        run = true;

                                        if (n_dnwait <= 0) n_dnwait = GProcessPara.NeedleVacClean.DownWait[gantry.Index].Value;
                                        if (n_disptime <= 0) n_disptime = GProcessPara.NeedleVacClean.DispTime[gantry.Index].Value;
                                        if (n_vactime <= 0) n_vactime = GProcessPara.NeedleVacClean.VacTime[gantry.Index].Value;
                                        if (n_postvactime <= 0) n_postvactime = GProcessPara.NeedleVacClean.PostVacTime[gantry.Index].Value;
                                        if (n_postwait <= 0) n_postwait = GProcessPara.NeedleVacClean.PostWait[gantry.Index].Value;
                                        if (n_count <= 0) n_count = GProcessPara.NeedleVacClean.Count[gantry.Index].Value;

                                        break;
                                    }
                                case ENeedleCleanMode.Flush:
                                    {
                                        if (NeedleFlushCount++ % n_perUnit != 0) break;
                                        run = true;

                                        if (n_dnwait <= 0) n_dnwait = GProcessPara.NeedleFlush.DownWait[gantry.Index].Value;
                                        if (n_disptime <= 0) n_disptime = GProcessPara.NeedleFlush.DispTime[gantry.Index].Value;
                                        if (n_vactime <= 0) n_vactime = GProcessPara.NeedleFlush.VacTime[gantry.Index].Value;
                                        if (n_postvactime <= 0) n_postvactime = GProcessPara.NeedleFlush.PostVacTime[gantry.Index].Value;
                                        if (n_postwait <= 0) n_postwait = GProcessPara.NeedleFlush.PostWait[gantry.Index].Value;
                                        if (n_count <= 0) n_count = GProcessPara.NeedleFlush.Count[gantry.Index].Value;

                                        break;
                                    }
                                case ENeedleCleanMode.Purge:
                                    {
                                        if (NeedlePurgeCount++ % n_perUnit != 0) break;
                                        run = true;

                                        if (n_dnwait <= 0) n_dnwait = GProcessPara.NeedlePurge.DownWait[gantry.Index].Value;
                                        if (n_disptime <= 0) n_disptime = GProcessPara.NeedlePurge.DispTime[gantry.Index].Value;
                                        if (n_vactime <= 0) n_vactime = GProcessPara.NeedlePurge.VacTime[gantry.Index].Value;
                                        if (n_postvactime <= 0) n_postvactime = GProcessPara.NeedlePurge.PostVacTime[gantry.Index].Value;
                                        if (n_postwait <= 0) n_postwait = GProcessPara.NeedlePurge.PostWait[gantry.Index].Value;
                                        if (n_count <= 0) n_count = GProcessPara.NeedlePurge.Count[gantry.Index].Value;

                                        break;
                                    }
                            }

                            if (!run) break;

                            Task.Run(() =>
                            {
                                TCNeedleFunc.CFP[gantry.Index].running = true;
                                while (TCDisp.Run[gantry.Index].bRun && TCNeedleFunc.CFP[gantry.Index].running) Thread.Sleep(0);
                                TCNeedleFunc.CFP[gantry.Index].running = false;
                            });
                            #endregion

                            while (gantry.Axis[0].Busy)
                            {
                                RefreshMap();
                                if (!running) return false;
                                Thread.Sleep(0);
                            }
                            moveNextDispCmdAtXYPlane = true;
                            if (!TCNeedleFunc.CFP[gantry.Index].Execute(mode, n_dnwait, n_disptime, n_vactime, n_postvactime, n_postwait, n_count)) return false;

                            vm3280Param[gantry.Index] = new Vermes3280_Param();

                            fpressIO.Status = fpress_state;

                            if (!GMotDef.GVAxis.MoveAbs(0)) return false;

                            break;
                        }
                    #endregion
                    case ECmd.NEEDLE_SPRAY_CLEAN:
                        #region
                        {
                            var n_dnwait = (int)cmd.Para[3];
                            var n_spraytime = (int)cmd.Para[5];
                            var n_postwait = (int)cmd.Para[7];

                            var n_count = Math.Max((int)cmd.Para[0], 1);
                            var n_perUnit = (int)cmd.Para[1];

                            if (n_perUnit <= 0) break;

                            bool run = false;
                            if (NeedleSprayCount++ % n_perUnit != 0) break;
                            run = true;
                            if (!run) break;

                            Task.Run(() =>
                            {
                                TCNeedleFunc.SprayClean[gantry.Index].running = true;
                                while (TCDisp.Run[gantry.Index].bRun && TCNeedleFunc.SprayClean[gantry.Index].running) Thread.Sleep(0);
                                TCNeedleFunc.SprayClean[gantry.Index].running = false;
                            });

                            if (!TCNeedleFunc.SprayClean[gantry.Index].Execute(n_dnwait,n_spraytime, n_postwait, n_count)) return false;

                            if (!GMotDef.GVAxis.MoveAbs(0)) return false;

                            break;
                        }
                    #endregion
                    case ECmd.PURGE_STAGE:
                        #region
                        {
                            TCNeedleFunc.PurgeStage[gantry.Index].RunMode = runMode;

                            int n_count = (int)cmd.Para[0];
                            if (n_count <= 0) n_count = GProcessPara.PurgeStage.Count.Value;
                            if (n_count <= 0) break;

                            var n_perUnit = (int)cmd.Para[1];
                            if (n_perUnit <= 0) break;

                            if (NeedlePurgeStageCount++ % n_perUnit != 0) break;

                            while (gantry.Axis[0].Busy)
                            {
                                RefreshMap();
                                if (!running) return false;
                                Thread.Sleep(0);
                            }
                            moveNextDispCmdAtXYPlane = true;
                            if (!TCNeedleFunc.PurgeStage[gantry.Index].Execute(n_count)) return false;

                            break;
                        }
                    #endregion
                    case ECmd.NEEDLE_AB_CLEAN:
                        #region
                        {
                            int n_count = (int)cmd.Para[0];
                            if (n_count <= 0) n_count = GProcessPara.NeedleVacClean.Count[gantry.Index].Value;
                            if (n_count <= 0) break;

                            var n_perUnit = (int)cmd.Para[1];
                            if (n_perUnit <= 0) break;

                            if (NeedleABCleanCount++ % n_perUnit != 0) break;

                            while (gantry.Axis[0].Busy)
                            {
                                RefreshMap();
                                if (!running) return false;
                                Thread.Sleep(0);
                            }
                            moveNextDispCmdAtXYPlane = true;
                            if (!TCNeedleFunc.AirBladeClean[gantry.Index].Execute(n_count)) return false;

                            break;
                        }
                    #endregion

                    case ECmd.LINE_SPEED:
                        #region
                        {
                            lineSpeed = cmd.Para[0];
                            break;
                        }
                    #endregion
                    case ECmd.LINE_GAP_ADJUST:
                        #region
                        {
                            lineRelGap = cmd.Para[0];
                            lineRelGapLength = cmd.Para[1];
                            break;
                        }
                    #endregion
                    case ECmd.NOZZLE_INSPECTION:
                        #region
                        {
                            int perunit = (int)cmd.Para[0];
                            bool exec = perunit is 0 ? FunctionFirstExecution : NoozleInspecCount++ % perunit != 0;

                            if (!exec) break;
                            if (!TCExternalFunc.NoozleInsps[gantry.Index].Execute()) return false;
                            break;
                        }
                    #endregion

                    case ECmd.GOTO_POSITION:
                        #region
                        {
                            if (goposition_executed) return true;

                            goposition_executed = true;

                            int gantry_idx = (int)gantry.Index;
                            int pos_idx = (int)cmd.Para[0];

                            var pos = pos_idx is 0 ? GSetupPara.Maintenance.MachinePos[gantry_idx] : GSetupPara.Maintenance.PumpPos[gantry_idx];

                            if (!gantry.GotoXYZ(pos)) return false;
                            break;
                        }
                    #endregion

                    case ECmd.PAT_ALIGN_ROTARY:
                        #region
                        {
                            //noneed to move rotary
                            //if (!GMotDef.GRAxis.MoveAbs(0)) return false;

                            PointI[] ij = new PointI[2] { clusterCR, unitCR };

                            if (instBoard.LayerData[layoutNo].GetUnitAlign(ij).Status >= EPatAlignStatus.BoardOK) break;
                            PointD clusterRel = GRecipes.MultiLayout[gantry.Index][layoutNo].Cluster.RelPos(new PointI());
                            PointD originAbs = boardOrigin + clusterRel;
                            TAlignData alignData = new TAlignData();

                            while (gantry.Axis[0].Busy)
                            {
                                if (!running) return false;
                                Thread.Sleep(0);
                            }
                            switch (PatAlignExecute(gantry, originAbs, cmd, ref alignData, settleTime))
                            {
                                case EAction.Skip:
                                    {
                                        if (runMode == ERunMode.Camera) TFCameras.Camera[gantry.Index].FlirCamera.Live();
                                        state = EDispState.NG;
                                        return true;
                                    }
                                case EAction.Fail:
                                    {
                                        if (runMode == ERunMode.Camera) TFCameras.Camera[gantry.Index].FlirCamera.Live();
                                        return false;
                                    }
                                case EAction.Accept:
                                    {
                                        if (runMode == ERunMode.Camera) TFCameras.Camera[gantry.Index].FlirCamera.Live();
                                        break;
                                    }
                            }

                            if (!GMotDef.GRAxis.MoveRel(alignData.Angle_Deg)) return false;
                            instBoard.ThetaPos = alignData.Angle_Deg;

                            alignData = new TAlignData();
                            var cmd2 = new TCmd(cmd);
                            cmd2.Para[9] = 0;

                            switch (PatAlignExecute(gantry, originAbs, cmd2, ref alignData, settleTime))
                            {
                                case EAction.Skip:
                                    {
                                        if (runMode == ERunMode.Camera) TFCameras.Camera[gantry.Index].FlirCamera.Live();
                                        state = EDispState.NG;
                                        return true;
                                    }
                                case EAction.Fail:
                                    {
                                        if (runMode == ERunMode.Camera) TFCameras.Camera[gantry.Index].FlirCamera.Live();
                                        return false;
                                    }
                                case EAction.Accept:
                                    {
                                        if (runMode == ERunMode.Camera) TFCameras.Camera[gantry.Index].FlirCamera.Live();
                                        break;
                                    }
                            }

                            var mlayout = GRecipes.MultiLayout[gantry.Index][LayoutNo];
                            var currentClusterCR = new PointI();

                            if (alignData.Status == EPatAlignStatus.OK) alignData.Status = EPatAlignStatus.BoardOK;

                            while (true)
                            {
                                var ad = new TAlignData(alignData);

                                var clusterRelPos = instBoard.CurrentMLayout.Cluster.RelPos(currentClusterCR);
                                var relDatum = alignData.Datum - clusterRelPos;
                                ad.Datum = relDatum;
                                EndclusterCRs.Add(new PointI(currentClusterCR));

                                var currentunitCR = new PointI();
                                while (true)
                                {
                                    instBoard.LayerData[layoutNo].SetUnitAlign(new PointI[] { currentClusterCR, currentunitCR }, ad);

                                    if (instBoard.MAP.GetState(currentClusterCR, currentunitCR) == EDispState.READY)
                                    {
                                        instBoard.MAP.SetState(currentClusterCR, currentunitCR, state);
                                    }

                                    EndunitCRs.Add(new PointI(currentunitCR));
                                    currentunitCR = mlayout.Unit.NextCR(currentunitCR);
                                    if (currentunitCR.IsZero) break;
                                }

                                currentClusterCR = mlayout.Cluster.NextCR(currentClusterCR);
                                if (currentClusterCR.IsZero) break;
                            }
                            
                            break;
                        }
                        #endregion
                }
                Thread.Sleep(1);

                #region
                //TCmd PrecedingTCmd(ECmd ccmd)
                //{
                //    int idx = CmdEnabled.IndexOf(cmd);
                //    if (idx is 0) return null;

                //    int i = 1;
                //    while (true)
                //    {
                //        try
                //        {
                //            var a = CmdEnabled[idx - i++];
                //            if (a.Cmd == ccmd) return a;
                //        }
                //        catch { return null; }
                //    }
                //}
                TCmd SuccedingTCmd(ECmd ccmd)
                {
                    int idx = CmdEnabled.IndexOf(cmd);
                    if (idx >= CmdEnabled.Count) return null;

                    int i = 1;
                    while (true)
                    {
                        try
                        {
                            var a = CmdEnabled[idx + i++];
                            if (a.Cmd == ccmd) return a;
                        }
                        catch { return null; }
                    }
                }
                TCmd PrevTCmd()
                {
                    int idx = CmdEnabled.IndexOf(cmd);
                    if (idx is 0) return null;
                    return CmdEnabled[idx - 1];
                }
                TCmd NextTCmd()
                {
                    int idx = CmdEnabled.IndexOf(cmd);
                    if (idx >= CmdEnabled.Count) return null;
                    return CmdEnabled[idx + 1];
                }

                #endregion
            }

            //if (!bDySaveImage)
            if (bufferModeTrackUnit)
            {
                cmdBuffer = sBase + " ";

                cmdBuffer += $"MOVE_WAIT(MSPEED,{gantryIdx + 2}, 0, 0) ";

                int tableBase = gantryIdx * 10;
                cmdBuffer += $"MOVE_TABLE({tableBase + 0}, {unitCR.X}) ";
                cmdBuffer += $"MOVE_TABLE({tableBase + 1}, {unitCR.Y}) ";
                cmdBuffer += $"MOVE_TABLE({tableBase + 2}, {clusterCR.X}) ";
                cmdBuffer += $"MOVE_TABLE({tableBase + 3}, {clusterCR.Y}) ";
                cmdBuffer += $"MOVE_TABLE({tableBase + 4}, {++RunSequence}) ";
                cmdBuffer += $"MOVE_WAIT(MODBUS_BIT,{gantryIdx}, 1, 0) ";
                TEZMCAux.DirectCommand(cmdBuffer);
            }

            if (gantry.Error)
            {
                int i = gantry.ErrorMask;
                string strAxis = "";
                if ((i & 0x01) > 0) strAxis += gantry.Axis[0].Name + " ";
                if ((i & 0x02) > 0) strAxis += gantry.Axis[1].Name + " ";
                if ((i & 0x03) > 0) strAxis += gantry.Axis[2].Name + " ";
                GAlarm.Prompt(EAlarm.GANTRY_ERROR, strAxis);
                return false;
            }

            while (true)
            {
                Thread.Sleep(0);
                int ibuffer = TEZMCAux.RemainBuffer(gantry.Axis[0].AxisNo);
                if (ibuffer < 250) Thread.Sleep(100);//Switch thread. Prevent instant thread hold
                if (ibuffer > GSystemCfg.MakerData.ZMotion_MinimumBufferRemain) break;

                var sw = System.Diagnostics.Stopwatch.StartNew();
                while (true)
                {
                    Thread.Sleep(0);
                    if (sw.ElapsedMilliseconds >= GSystemCfg.MakerData.ZMotion_BufferFullSleepDuration) break;
                    if (!running) return true;
                }
            }
            Thread.Sleep(0);

            RefreshMap();

            FunctionFirstExecution = false;

            return true;
        }

        public int Wait()//Wait Buffer clear or Stop Condition. Returns number of moves remaining in the buffer
        {
            int buffer = 0;
            while (true)
            {
                if (gantry.Error)
                {
                    gantry.Cancel();
                    int i = gantry.ErrorMask;
                    string strAxis = "";
                    if ((i & 0x01) > 0) strAxis += gantry.Axis[0].Name + " ";
                    if ((i & 0x02) > 0) strAxis += gantry.Axis[1].Name + " ";
                    if ((i & 0x03) > 0) strAxis += gantry.Axis[2].Name + " ";

                    GAlarm.Prompt(EAlarm.GANTRY_ERROR, strAxis);
                }

                if (!running)
                {
                    int tableBase = gantry.Index * 10;
                    if (TEZMCAux.Table(tableBase + 4) != StopRunSequence)
                    {
                        RefreshMap();
                        break;
                    }
                }

                buffer = gantry.Axis[0].MoveBuffered;
                if (buffer == 0)
                    break;

                RefreshMap();
                Thread.Sleep(50);
            }
            return buffer;
        }
        public void Stop()
        {
            if (running)
            {
                int tableBase = gantry.Index * 10;
                StopRunSequence = TEZMCAux.Table(tableBase + 4);
            }

            TEZMCAux.Execute($"MODBUS_BIT({gantryIdx})=1");
            running = false;
        }
        public void Cancel()
        {
            string cmdBuffer = sBase;
            cmdBuffer += $"CANCEL(2) ";
            TEZMCAux.DirectCommand(cmdBuffer);
        }
        public void RefreshMap()
        {
            var frmMap = Application.OpenForms.OfType<frmRecipeMap>().FirstOrDefault();
            if (frmMap != null) frmMap.RefreshUI();
        }

        public void ClearData()
        {
            NeedleCleanCount =
            NeedleFlushCount =
            NeedlePurgeCount =
            NeedlePurgeStageCount =
            NeedleSprayCount =
            NeedleABCleanCount = 0;
            goposition_executed = false;
            //InstSelectHead = 0;
            //Vm3280_param = new Vermes3280_Setup();
            //Sp_param = new SP_Setup();
            //Hm_param = new HM_Setup();
            //CurrentClusterCR = null;
            //H = new List<double>();
        }

        public EAction PatAlignExecute(TEZMCAux.TGroup gantryGroup, PointD originAbs, TCmd cmd, ref TAlignData alignData, int settleTime = 0)
        {
            string desc = $"\r\rAccept: accept current position as ref point\rRetry: remain or select new position to redo pat alignment\rIgnore: skip current unit for entire process\rAbort: Stop entire process";

            if (settleTime <= 0) settleTime = GProcessPara.Vision.SettleTime.Value;


            var distPerPixelX = GSystemCfg.Camera.Cameras[0].DistPerPixelX;
            var distPerPixelY = GSystemCfg.Camera.Cameras[0].DistPerPixelY;

            Emgu.CV.Image<Emgu.CV.Structure.Gray, byte> img = null;

            try
            {
                //on light
                TFLightCtrl.LightPair[gantryGroup.Index].Set(GRecipes.Lighting[gantryGroup.Index][cmd.ID]);

                alignData.Status = EPatAlignStatus.None;

                PointD ptOri1 = new PointD(cmd.Para[0], cmd.Para[1]);
                PointD ptOri2 = new PointD(cmd.Para[3], cmd.Para[4]);

                PointD offset1 = new PointD();
                PointD offset2 = new PointD();

            _Redo:
                //gantry.MoveOpZAbs(GSystemCfg.Camera.Cameras[gantry.Index].DefaultFocusZ + cmd.Para[2]);
                gantry.MoveOpZAbs(GRecipes.Board[gantry.Index].StartPos.Z);
                gantryGroup.MoveOpXYAbs((originAbs + ptOri1 + offset1).ToArray);
                Thread.Sleep(settleTime);

                TFCameras.Camera[gantryGroup.Index].FlirCamera.Snap();
                img = TFCameras.Camera[gantryGroup.Index].FlirCamera.emgucvImage.Clone();
                //TFCameras.Camera[gantryGroup.Index].FlirCamera.Live();

                int id = cmd.ID;
                if (GRecipes.PatRecog[gantryGroup.Index][id].RegImage[0] == null)
                {
                    alignData.Status = EPatAlignStatus.Error;
                    GAlarm.Prompt(EAlarm.VISION_MATCH_IMAGE_REGISTER_ERROR, gantryGroup.Name);
                    //return false;
                    return EAction.Fail;
                }

                PointD pLoc = new PointD(0, 0);
                PointD pLOfst = new PointD(0, 0);
                double score = 0;
                if (!TFVision.PatMatch(img, GRecipes.PatRecog[gantryGroup.Index][id].RegImage[0], GRecipes.PatRecog[gantryGroup.Index][id].ImgThld[0], new Rectangle[] { GRecipes.PatRecog[gantryGroup.Index][id].SearchRect[0], GRecipes.PatRecog[gantryGroup.Index][id].PatRect[0] }, ref pLoc, ref pLOfst, ref score)) return EAction.Fail;
                PointD ofst = new PointD(pLOfst.X * distPerPixelX, -pLOfst.Y * distPerPixelY);

                double minScore = cmd.Para[6];
                double maxOfst = cmd.Para[7];
                double maxAngle = cmd.Para[8];

                if (score < minScore)
                {
                    GLog.LogProcess($"PatternAlign 1 Score {score:f2}");
                    var xy = new PointD(gantryGroup.Axis[0].ActualPos, gantryGroup.Axis[1].ActualPos);
                    GControl.UI_Enable();

                    TFCameras.Camera[gantry.Index].FlirCamera.Live();

                    switch (MsgBox.ShowDialog($"PA 1 Vision Match Low Score\r\nSetScore:{minScore}\r\nPA score:{score:f2}" + desc, MsgBoxBtns.OkAbortRetryIgnore))
                    {
                        default:
                            {
                                alignData.Status = EPatAlignStatus.FailScore;
                                GAlarm.Prompt(EAlarm.VISION_MATCH_LOW_SCORE_ERROR);
                                return EAction.Fail;
                            }
                        case DialogResult.Retry:
                            {
                                offset1 = new PointD(gantryGroup.Axis[0].ActualPos, gantryGroup.Axis[1].ActualPos) - xy;
                                goto _Redo;
                            }
                        case DialogResult.Ignore: return EAction.Skip;
                        case DialogResult.OK:
                            {
                                offset1 = new PointD(gantryGroup.Axis[0].ActualPos, gantryGroup.Axis[1].ActualPos) - xy;
                                ofst = new PointD();
                                goto _End;
                            }
                    }
                }

                TFCameras.Camera[gantry.Index].FlirCamera.Live();

                GLog.LogProcess($"PatternAlign 1 Score {score:f2} Offset {ofst.ToStringForDisplay()}");
                if (Math.Abs(ofst.X) > maxOfst || Math.Abs(ofst.Y) > maxOfst)
                {
                    var xy = new PointD(gantryGroup.Axis[0].ActualPos, gantryGroup.Axis[1].ActualPos);
                    GControl.UI_Enable();

                    switch (MsgBox.ShowDialog($"PA 1 Offset Fail\r\nSetOffsetTol:{maxOfst}\r\nPA Offset:{Math.Abs(ofst.X)},{Math.Abs(ofst.Y)}\r\n" + desc, MsgBoxBtns.OkRetryAbort))
                    {
                        default:
                            {
                                alignData.Status = EPatAlignStatus.FailOffset;
                                GAlarm.Prompt(EAlarm.VISION_MATCH_OFFSET_ERROR);
                                return EAction.Fail;
                            }
                        case DialogResult.Retry:
                            {
                                offset1 = new PointD(gantryGroup.Axis[0].ActualPos, gantryGroup.Axis[1].ActualPos) - xy;
                                goto _Redo;
                            }
                        case DialogResult.Ignore: return EAction.Skip;
                        case DialogResult.OK:
                            {
                                offset1 = new PointD(gantryGroup.Axis[0].ActualPos, gantryGroup.Axis[1].ActualPos) - xy;
                                ofst = new PointD();
                                //break;
                                goto _End;
                            }
                    }
                }

            _End:
                PointD ptNew1 = ptOri1 + ofst + offset1;

                alignData.Datum = ptOri1;
                alignData.Offset = ptNew1 - ptOri1;
                alignData.Scores.Clear();
                alignData.Scores.Add(score);

                #region Point 2

                if (cmd.Para[9] > 0)
                {
                    PointD ptNew2 = originAbs + ptNew1 + (ptOri2 - ptOri1) + offset2;

                    gantry.MoveOpZAbs(GSystemCfg.Camera.Cameras[gantry.Index].DefaultFocusZ + cmd.Para[5]);
                    gantryGroup.MoveOpXYAbs(ptNew2.ToArray);
                    Thread.Sleep(GProcessPara.Vision.SettleTime.Value);

                    TFCameras.Camera[gantryGroup.Index].FlirCamera.Snap();
                    img = TFCameras.Camera[gantryGroup.Index].FlirCamera.emgucvImage.Clone();
                    //TFCameras.Camera[gantryGroup.Index].FlirCamera.Live();

                    id = cmd.ID;
                    if (GRecipes.PatRecog[gantryGroup.Index][id].RegImage[1] == null)
                    {
                        alignData.Status = EPatAlignStatus.Error;
                        GAlarm.Prompt(EAlarm.VISION_MATCH_IMAGE_REGISTER_ERROR);
                        return EAction.Fail;
                    }

                    pLoc = new PointD(0, 0);
                    pLOfst = new PointD(0, 0);
                    score = 0;

                    var secpointIdx = cmd.Para[9] is 10 ? 0 : 1;
                    if (!TFVision.PatMatch(img, GRecipes.PatRecog[gantryGroup.Index][id].RegImage[secpointIdx], GRecipes.PatRecog[gantryGroup.Index][id].ImgThld[secpointIdx], new Rectangle[] { GRecipes.PatRecog[gantryGroup.Index][id].SearchRect[secpointIdx], GRecipes.PatRecog[gantryGroup.Index][id].PatRect[secpointIdx] }, ref pLoc, ref pLOfst, ref score)) return EAction.Fail;
                    ofst = new PointD(pLOfst.X * distPerPixelX, -pLOfst.Y * distPerPixelY);

                    if (score < minScore)
                    {
                        GLog.LogProcess($"PatternAlign 2 Score {score:f2}");
                        //alignData.Status = EPatAlignStatus.FailScore2;
                        //GAlarm.Prompt(EAlarm.VISION_MATCH_LOW_SCORE_ERROR, gantryGroup.Name);
                        //return false;
                        var xy = new PointD(gantryGroup.Axis[0].ActualPos, gantryGroup.Axis[1].ActualPos);
                        GControl.UI_Enable();

                        TFCameras.Camera[gantry.Index].FlirCamera.Live();

                        switch (MsgBox.ShowDialog($"PA 2 vision match low score\r\nSetScore:{minScore}\r\nPA score:{score}" + desc, MsgBoxBtns.OkRetryAbort))
                        {
                            default:
                                {
                                    alignData.Status = EPatAlignStatus.FailScore2;
                                    GAlarm.Prompt(EAlarm.VISION_MATCH_LOW_SCORE_ERROR);
                                    return EAction.Fail;
                                }
                            case DialogResult.Retry:
                                {
                                    offset2 = new PointD(gantryGroup.Axis[0].ActualPos, gantryGroup.Axis[1].ActualPos) - xy;
                                    goto _Redo;
                                }
                            case DialogResult.Ignore: return EAction.Skip;
                            case DialogResult.OK:
                                {
                                    offset2 = new PointD(gantryGroup.Axis[0].ActualPos, gantryGroup.Axis[1].ActualPos) - xy;
                                    ofst = new PointD();
                                    goto _End2;
                                }
                        }
                    }

                    if (Math.Abs(ofst.X) > maxOfst || Math.Abs(ofst.Y) > maxOfst)
                    {
                        GLog.LogProcess($"PatternAlign 2 Score {score:f2} Offset {ofst.ToStringForDisplay()}");

                        var xy = new PointD(gantryGroup.Axis[0].ActualPos, gantryGroup.Axis[1].ActualPos);
                        GControl.UI_Enable();

                        TFCameras.Camera[gantry.Index].FlirCamera.Live();

                        switch (MsgBox.ShowDialog($"PA 2 Offset Fail\r\nSetOffsetTol:{maxOfst}\r\nPA Offset:{Math.Abs(ofst.X)},{Math.Abs(ofst.Y)}\r\n" + desc, MsgBoxBtns.OkRetryAbort))
                        {
                            default:
                                {
                                    alignData.Status = EPatAlignStatus.FailOffset2;
                                    GAlarm.Prompt(EAlarm.VISION_MATCH_OFFSET_ERROR);
                                    return EAction.Fail;
                                }
                            case DialogResult.Retry:
                                {
                                    offset2 = new PointD(gantryGroup.Axis[0].ActualPos, gantryGroup.Axis[1].ActualPos) - xy;
                                    goto _Redo;
                                }
                            case DialogResult.Ignore: return EAction.Skip;
                            case DialogResult.OK:
                                {
                                    offset2 = new PointD(gantryGroup.Axis[0].ActualPos, gantryGroup.Axis[1].ActualPos) - xy;
                                    ofst = new PointD();
                                    //break;
                                    goto _End2;
                                }
                        }
                    }
                _End2:
                    ptNew2 = ptNew1 + (ptOri2 - ptOri1) + ofst + offset2;

                    double angle1 = Math.Atan2(ptOri2.Y - ptOri1.Y, ptOri2.X - ptOri1.X);
                    double angle2 = Math.Atan2(ptNew2.Y - ptNew1.Y, ptNew2.X - ptNew1.X);

                    GLog.LogProcess($"Angle1 {angle1:f3}");
                    GLog.LogProcess($"Angle2 {angle2:f3}");

                    //if (angle2 < 0) angle2 = 2 * Math.PI + angle2;

                    alignData.Scores.Add(score);
                    var rad = angle2 - angle1;

                    if (rad > Math.PI) rad = (2 * Math.PI) - rad;
                    if (rad < -Math.PI) rad = (2 * Math.PI) + rad;

                    alignData.Angle_Rad = rad;

                    var angle = alignData.Angle_Rad * 180 / Math.PI;

                    TFCameras.Camera[gantry.Index].FlirCamera.Live();

                    GLog.LogProcess($"PatternAlign 2 Score {score:f2} Offset {ofst.ToStringForDisplay()} Angle {angle:f3}");

                    if (Math.Abs(angle) > maxAngle)
                    {
                        GControl.UI_Enable();

                        switch (MsgBox.ShowDialog($"PA Vision Match Angle Error{angle:f3}" + desc, MsgBoxBtns.OkRetryAbort))
                        {
                            default:
                                {
                                    alignData.Status = EPatAlignStatus.FailAngle;
                                    GAlarm.Prompt(EAlarm.VISION_MATCH_ANGLE_ERROR);
                                    return EAction.Fail;
                                }
                            case DialogResult.Retry: goto _Redo;
                            case DialogResult.Ignore: return EAction.Skip;
                            case DialogResult.OK: break;
                        }
                    }

                }
                #endregion

                alignData.Status = EPatAlignStatus.OK;
                //return true;
                return EAction.Accept;
            }
            finally
            {
                img.Dispose();
                //TFLightCtrl.LightPair[gantryGroup.Index].Off();
            }
        }
        public PointD Translate(PointD point, TAlignData alignData)
        {
            //A = angle_radian
            //
            //[x']   [x][cosA -sinA]
            //[  ] = [ ][          ]
            //[y']   [y][sinA  cosA]
            //
            //x' = x*cosA - y*sinA
            //y' = x*sinA + y*cosA
            //
            //(1)Rotate about a center xc, yc
            //
            //x' = xc + (x - xc)*cosA - (y - yc)*sinA
            //y' = yc + (x - xc)*sinA + (y - yc)*cosA
            //
            //(2)Tranlate offset xo, yo
            //x' += xo
            //y' += yo

            PointD ptOri = point;
            double angle = alignData.Angle_Rad;

            PointD ptRotate = new PointD(ptOri);
            ptRotate.X = alignData.Datum.X + (ptRotate.X - alignData.Datum.X) * Math.Cos(angle) - (ptRotate.Y - alignData.Datum.Y) * Math.Sin(angle);
            ptRotate.Y = alignData.Datum.Y + (ptRotate.X - alignData.Datum.X) * Math.Sin(angle) + (ptRotate.Y - alignData.Datum.Y) * Math.Cos(angle);

            PointD ptTranslate = new PointD(ptRotate) + alignData.Offset;

            return ptTranslate;
        }

        public bool HeightAlignExecute(TEZMCAux.TGroup gantryGroup, PointD originAbs, TCmd cmd, ref THeightData heightData)
        {
            TFLightCtrl.LightPair[gantryGroup.Index].Off();

            heightData.Status = EHeightAlignStatus.NG;

            //PointD ptOri1 = new PointD(cmd.Para[0], cmd.Para[1]);
            //gantryGroup.MoveOpXYAbs((originAbs + ptOri1).ToArray);
            gantryGroup.MoveOpXYAbs((originAbs).ToArray);

            var settletime = cmd.Para[3] > 0 ? (int)cmd.Para[3] : GProcessPara.HSensor.SettleTime.Value;

            Thread.Sleep(settletime);

            double hSensorValue = 0;
            if (!TFHSensors.Sensor[gantryGroup.Index].GetValue(ref hSensorValue))
            {
                heightData.Status = EHeightAlignStatus.Error;
                return false;
            }



            if (hSensorValue <= -999 || hSensorValue >= 999)
            {
                heightData.Status = EHeightAlignStatus.Error;
                GDefine.SystemState = ESystemState.ErrorRestart;
                GAlarm.Prompt(EAlarm.CONFOCAL_RANGE_ERROR, gantryGroup.Name);
                return false;
            }

            heightData.SensorValue = Math.Round(hSensorValue, 5);
            heightData.Status = EHeightAlignStatus.Aligned;
            return true;
        }
        public EAction HeightCal(List<double> hsensor, double rangeLimit, PointI clusterCR, PointI unitCR, out double average)
        {
            //average = 0;
            //GLog.LogProcess($"Height Align Data {string.Join(",", hsensor)}");
            //GLog.LogProcess($"Height Align Data Average {hsensor.Average()}");
            //GLog.LogProcess($"Height Align Data Range {Math.Abs(hsensor.Max() - hsensor.Min())}");

            //if (hsensor.Count > 0)
            //{
            //    if (Math.Abs(hsensor.Max() - hsensor.Min()) > offset)
            //    {
            //        GAlarm.Prompt(EAlarm.HEIGHT_ALIGN_OVER_OFFSET);
            //        return false;
            //    }
            //}
            //average = hsensor.Average();
            //return true;
            average = 0;

            try
            {
                var c = new PointI(clusterCR); var u = new PointI(unitCR);
                c.X += 1; c.Y += 1; u.X += 1; u.Y += 1;
                string pos = $"C:[{c.ToStringForDisplay()}]  U:[{u.ToStringForDisplay()}]";

                var range = Math.Abs(hsensor.Max() - hsensor.Min());

                string processLog = $"Height Align Unit:[{pos}] \r\nData:[{string.Join(",", hsensor)}] \r\nAverage:[{hsensor.Average():f4}] \r\nRange:[{range:f4}]";
                GLog.LogProcess(processLog);

                if (hsensor.Count > 0)
                {
                    if (range > rangeLimit)
                    {
                        switch (MsgBox.ShowDialog($"Height value out of range.\r\nRange: {range:f4}\r\nMaxRange: {rangeLimit:f4}", MsgBoxBtns.AcceptIgnoreAbort))
                        {
                            case DialogResult.OK:
                                {
                                    average = hsensor.Average();
                                    return EAction.Accept;
                                }
                            case DialogResult.Ignore: return EAction.Skip;
                            case DialogResult.Abort: break;

                        }
                        GAlarm.Prompt(EAlarm.HEIGHT_ALIGN_OVER_OFFSET, pos + "\r\n" + $"{hsensor.Last():f4}");
                        return EAction.Fail;
                    }
                }
                average = hsensor.Average();
                return EAction.Accept;
            }
            catch
            {
                return EAction.Fail;
            }
        }
    }

    public class Circle
    {
        public static PointD Center(PointD startPt, PointD passPt, PointD endPt)
        {
            #region formula
            //mr = (y2 - y1)/(x2 - x1)
            //mt = (y3 - y2)/(x3 - x2)

            //x = (mr*mt*(y3-y1) + mr(x2+x3) - mt(x1+x2))/(2(mr - mt))
            //y = -(1 / mr) * (x - (x1 + x2) / 2) + ((y1 + y2) / 2);
            #endregion

            #region calc slope, center and rad
            double mr, mt;
            double xc, yc;

            if ((startPt.X == passPt.X && passPt.X == endPt.X) || (startPt.Y == passPt.Y && passPt.Y == endPt.Y))
            {
                throw new Exception("Points in 1 line. Unable to generate circle or arc.");
            }

            if (startPt.X == passPt.X)
            {
                mr = (endPt.Y - startPt.Y) / (endPt.X - startPt.X);
                mt = (passPt.Y - endPt.Y) / (passPt.X - endPt.X);

                xc = (mr * mt * (endPt.Y - startPt.Y) + mr * (endPt.X + passPt.X) - mt * (startPt.X + endPt.X)) / (2 * (mr - mt));
                yc = -(1 / mr) * (xc - (startPt.X + endPt.X) / 2) + ((startPt.Y + endPt.Y) / 2);
                //rad = (double)Math.Sqrt(Math.Pow(x3 - xc, 2) + Math.Pow(y3 - yc, 2));
            }
            else
                if (startPt.X == endPt.X)
            {
                mr = (passPt.Y - startPt.Y) / (passPt.X - startPt.X);
                mt = (endPt.Y - passPt.Y) / (endPt.X - passPt.X);

                xc = (mr * mt * (endPt.Y - startPt.Y) + mr * (passPt.X + endPt.X) - mt * (startPt.X + passPt.X)) / (2 * (mr - mt));
                yc = -(1 / mr) * (xc - (startPt.X + passPt.X) / 2) + ((startPt.Y + passPt.Y) / 2);
                //rad = (double)Math.Sqrt(Math.Pow(x2 - xc, 2) + Math.Pow(y2 - yc, 2));
            }
            else//if (passPt.X == endPt.X)
            {
                mr = (startPt.Y - passPt.Y) / (startPt.X - passPt.X);
                mt = (endPt.Y - startPt.Y) / (endPt.X - startPt.X);

                xc = (mr * mt * (endPt.Y - passPt.Y) + mr * (startPt.X + endPt.X) - mt * (passPt.X + startPt.X)) / (2 * (mr - mt));
                yc = -(1 / mr) * (xc - (passPt.X + startPt.X) / 2) + ((passPt.Y + startPt.Y) / 2);
                //rad = (double)Math.Sqrt(Math.Pow(x1 - xc, 2) + Math.Pow(y1 - yc, 2));
            }
            #endregion

            return new PointD(xc, yc);
        }
        public static double SideOfLine(PointD startPt, PointD endPt, PointD pt)
        {
            return ((pt.X - startPt.X) * (endPt.Y - startPt.Y)) - ((pt.Y - startPt.Y) * (endPt.X - startPt.X));
        }
        public static double SweepAngle(PointD centerPt, PointD startPt, PointD passPt, PointD endPt)
        {
            //Return sweep angle in radian, cw - positive sweep angle
            //Atan2 return value is in Radian (-pi ~ pi), angle from the positive X-Axis, ccw positive
            double angleStart = (double)Math.Atan2((startPt.Y - centerPt.Y), (startPt.X - centerPt.X));
            double anglePass = (double)Math.Atan2((passPt.Y - centerPt.Y), (passPt.X - centerPt.X));
            double angleEnd = (double)Math.Atan2((endPt.Y - centerPt.Y), (endPt.X - centerPt.X));

            //Convert to positive angles 
            if (angleStart < 0) angleStart = Math.PI * 2 - angleStart;
            if (anglePass < 0) anglePass = Math.PI * 2 - anglePass;
            if (angleEnd < 0) angleEnd = Math.PI * 2 - angleEnd;

            if (anglePass > angleStart)//ccw
                return -(angleEnd - angleStart);
            else//cw
                return angleStart + (Math.PI * 2 - angleEnd);
        }
    }

    public class TPatRect
    {
        public Emgu.CV.Image<Emgu.CV.Structure.Gray, byte>[] RegImage { get; set; } = new Emgu.CV.Image<Emgu.CV.Structure.Gray, byte>[2] { new Emgu.CV.Image<Emgu.CV.Structure.Gray, byte>(1, 1), new Emgu.CV.Image<Emgu.CV.Structure.Gray, byte>(1, 1) };
        public int[] ImgThld { get; set; } = new int[] { -1, -1 };
        public Rectangle[] SearchRect { get; set; } = new Rectangle[] { new Rectangle(100, 100, 200, 200), new Rectangle(100, 100, 200, 200) };
        public Rectangle[] PatRect { get; set; } = new Rectangle[] { new Rectangle(150, 150, 50, 50), new Rectangle(150, 150, 50, 50) };

        public TPatRect()
        {
        }
        public TPatRect(TPatRect sourcePatRect)
        {
            RegImage = Enumerable.Range(0, 2).Select(x => sourcePatRect.RegImage[x].Clone()).ToArray();
            ImgThld = Enumerable.Range(0, 2).Select(x => sourcePatRect.ImgThld[x]).ToArray();
            SearchRect = Enumerable.Range(0, 2).Select(x => sourcePatRect.SearchRect[x]).ToArray();
            PatRect = Enumerable.Range(0, 2).Select(x => sourcePatRect.PatRect[x]).ToArray();
        }
    }

    class GRecipes
    {
        const int GantryCount = 2;

        public static TBoard[] Board = Enumerable.Range(0, 2).Select(x => new TBoard()).ToArray();
        public static BindingList<TMultiLayout>[] MultiLayout = new BindingList<TMultiLayout>[] { new BindingList<TMultiLayout>(Enumerable.Range(0, TMultiLayout.MAX_LAYER).Select(x => new TMultiLayout(x)).ToList()), new BindingList<TMultiLayout>(Enumerable.Range(0, TMultiLayout.MAX_LAYER).Select(x => new TMultiLayout(x)).ToList()) };
        public static BindingList<TFunction>[] Functions = new BindingList<TFunction>[] { new BindingList<TFunction>() { new TFunction(0) }, new BindingList<TFunction>() { new TFunction(1) } };
        public static BindingList<TPatRect>[] PatRecog = new BindingList<TPatRect>[] { new BindingList<TPatRect>() { new TPatRect() }, new BindingList<TPatRect>() { new TPatRect() } };
        public static BindingList<double>[] CamFocusNo = Enumerable.Range(0, 2).Select(x => new BindingList<double>(Enumerable.Repeat((double)0, 10).ToList())).ToArray();
        public static BindingList<LightRGBA>[] Lighting = Enumerable.Range(0, 2).Select(x => new BindingList<LightRGBA>(Enumerable.Range(0, 10).Select(y => new LightRGBA()).ToList())).ToArray();
        internal static int MAX_RETICLE = 10;
        public static BindingList<TEReticle>[] Reticle = Enumerable.Range(0, 2).Select(x => new BindingList<TEReticle>(Enumerable.Range(0, MAX_RETICLE).Select(y => new TEReticle()).ToList())).ToArray();
        public static BindingList<TMAP>[] Maps = Enumerable.Range(0, 2).Select(x => new BindingList<TMAP>()).ToArray();

        //pump
        public static Vermes3280_Param[] Vermes_Setups = Enumerable.Range(0, 2).Select(x => new Vermes3280_Param(x)).ToArray();
        public static SP_Param[] SP_Setups = Enumerable.Range(0, 2).Select(x => new SP_Param(x)).ToArray();
        public static HM_Param[] HM_Setups = Enumerable.Range(0, 2).Select(x => new HM_Param(x)).ToArray();
        public static PneumaticJet_Param[] PneumaticJet_Setups = Enumerable.Range(0, 2).Select(x => new PneumaticJet_Param(x)).ToArray();


        public static LightRGBA[] Current_LightRGBA = Enumerable.Range(0, 2).Select(x => new LightRGBA()).ToArray();

        //others
        public static BindingList<TEParamCtrl> ParamCtrls = new BindingList<TEParamCtrl>();
        public static Temp_Setup[] Temp_Setups = Enumerable.Range(0, GSystemCfg.Temperature.ChannelCount).Select(x => new Temp_Setup()).ToArray();

        public static List<ECmd> CmdsDictionary = Enum.GetValues(typeof(ECmd)).OfType<ECmd>().ToList();

        public static List<PressureSetup> PressureSetups = Enumerable.Range(0, 2).Select(x => new PressureSetup()).ToList();


        public static bool Save(string filepath)
        {
            for (int x = 0; x < 2; x++)
            {
                while (MultiLayout[x].Count > 1) MultiLayout[x].RemoveAt(MultiLayout[x].Count - 1);
                while (Maps[x].Count > 1) Maps[x].RemoveAt(Maps[x].Count - 1);
            }

            GDoc.RecipeNameWithPath = filepath.Replace(GDoc.RecipeDir.FullName, "");

            GEvent.Start(EEvent.SAVE_RECIPE, GDoc.RecipeNameWithPath);

            if (Path.GetExtension(filepath) == ".ini")
                return GDoc.SaveINI(filepath, MethodBase.GetCurrentMethod().DeclaringType);
            else
                return GDoc.SaveXML(filepath, MethodBase.GetCurrentMethod().DeclaringType);
        }
        public static bool Save(bool prompt = true)
        {
            if (prompt)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.InitialDirectory = GDoc.RecipeDir.FullName;
                save.Filter = GSystemCfg.Advance.RecipeFileType == EFileType.INI ? GDoc.ini_ext + "|" + GDoc.xml_ext : GDoc.xml_ext + "|" + GDoc.ini_ext;
                save.FileName = Path.GetFileNameWithoutExtension(GDoc.RecipeNameWithPath);

                if (save.ShowDialog() != DialogResult.OK) return false;

                return Save(save.FileName);
            }
            else
                return Save(GDoc.RecipeDir.FullName + GDoc.RecipeNameWithPath);
        }

        public static bool Load(string filepath)
        {
            IsLoading = true;
            GDoc.RecipeNameWithPath = filepath.Replace(GDoc.RecipeDir.FullName, "");

            GEvent.Start(EEvent.LOAD_RECIPE, GDoc.RecipeNameWithPath);

            if (Path.GetExtension(filepath) == ".ini")
                GDoc.LoadINI(filepath, MethodBase.GetCurrentMethod().DeclaringType);
            else
                GDoc.LoadXML(filepath, MethodBase.GetCurrentMethod().DeclaringType);

            IsLoading = false;

            //Write Param
            for (int k = 0; k < GSystemCfg.Pump.Count; k++)
            {
                var dispCtrl = GSystemCfg.Pump.Pumps[k];
                switch (dispCtrl.PumpType)
                {
                    case EPumpType.VERMES_3280:
                        {
                            TFPump.Vermes_Pump[k].TriggerAset(Vermes_Setups[k]);
                            TFPressCtrl.FPress[k].Set(Vermes_Setups[k].FPress.Value);
                            break;
                        }
                    case EPumpType.TP:
                    case EPumpType.SPLite:
                        {
                            TFPressCtrl.FPress[k].Set(SP_Setups[k].FPress.Value);
                            break;
                        }
                }
            }

            for (int g = 0; g < GantryCount; g++)
            {
                TFLightCtrl.LightPair[g].Set(new LightRGBA(Current_LightRGBA[g]));
            }

            for (int g = 0; g < GantryCount; g++)
            {
                Inst.Board[g].ClearData();
            }


            for (int i = 0; i < GSystemCfg.Temperature.ChannelCount; i++)
            {
                var channel = GSystemCfg.Temperature.Temp.Channels[i];
                if (channel.Enable) TFTempCtrl.TempCtrl.SetValue(GSystemCfg.Temperature.Temp.Channels[i].Address, Temp_Setups[i].SetValue.Value);
            }

            if (PressureSetups is null| PressureSetups.Count is 0) PressureSetups = Enumerable.Range(0, 2).Select(x => new PressureSetup()).ToList();

            return true;
        }

        public static bool Load()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.InitialDirectory = GDoc.RecipeDir.FullName;
            open.Filter = GSystemCfg.Advance.RecipeFileType == EFileType.INI ? GDoc.ini_ext + "|" + GDoc.xml_ext : GDoc.xml_ext + "|" + GDoc.ini_ext;
            if (open.ShowDialog() != DialogResult.OK) return false;

            return Load(open.FileName);
        }
        public static bool LoadLastWrite()
        {
            string searchPattern = GSystemCfg.Advance.RecipeFileType == EFileType.INI ? "*.ini" : "*.xml";

            var file = GDoc.RecipeDir.GetFiles(searchPattern, SearchOption.AllDirectories).OrderByDescending(f => f.LastWriteTime).FirstOrDefault();
            if (file != null) return Load(file.FullName);
            return false;
        }

        internal static bool IsLoading { get; private set; } = false;
    }
}