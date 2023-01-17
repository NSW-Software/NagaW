using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NagaW
{
    public enum ETool
    {
        HomeAll,
        Machine_Pos,
        Pump_Pos,
        VacChuck,
        VacExhaust,
        Cam_Offset,
        Goto_Laser,
        Teach_XY,
        Teach_Z,
        Touch_Dot,
        VacClean,
        Flush,
        Purge,
        Purge_Stage,

        Pump_Maint,

        Manual_Load = 100,
        Manual_Unload,
    }

    public class TFTool
    {
        public static BindingList<TETool> DispTools { get; set; } = new BindingList<TETool>(Enum.GetValues(typeof(ETool))
            .OfType<ETool>()
            .Select(x => new TETool(x))
            .ToList());

        enum EGanytrySelect { GantryL, GantryR };
        static EGanytrySelect GantrySelect = EGanytrySelect.GantryL;
        public static void Execute(ETool t)
        {
            switch (t)
            {
                case ETool.HomeAll:
                    {
                        MsgBox.Processing("Init all in Progress.", () =>
                        {
                            TCSystem.InitAll();
                        });
                    }
                    break;
                case ETool.Machine_Pos:
                    {
                        if (dualMode)
                        {
                            MsgBox.Processing("Goto Machine Pos", () =>
                          {
                                var g1 = Task.Run(() => { TCNeedleFunc.Maint[0].GotoMcMaint(false); });
                                var g2 = Task.Run(() => { TCNeedleFunc.Maint[1].GotoMcMaint(false); });
                                Task.WaitAll(g1, g2);
                            });
                        }
                        else
                            MsgBox.Processing("Goto Machine Pos", () => TCNeedleFunc.Maint[(int)GantrySelect].GotoMcMaint(false));
                    }
                    break;
                case ETool.Pump_Pos:
                    {
                        if (dualMode)
                        {
                            MsgBox.Processing("Goto Machine Pos", () =>
                            {
                                var g1 = Task.Run(() => { TCNeedleFunc.Maint[0].GotoPumpMaint(false); });
                                var g2 = Task.Run(() => { TCNeedleFunc.Maint[1].GotoPumpMaint(false); });
                                Task.WaitAll(g1, g2);
                            });
                        }
                        else
                            MsgBox.Processing("Goto Machine Pos", () => TCNeedleFunc.Maint[(int)GantrySelect].GotoPumpMaint(false));
                    }
                    break;
                case ETool.VacChuck:
                    {
                        if (dualMode)
                        {
                            TCWafer.WaferVacHigh.Status = !TCWafer.WaferVacHigh.Status;
                            TCWafer.WaferVacLow.Status = !TCWafer.WaferVacLow.Status;
                            //TFRConv.SvChuckVac.Status = !TFRConv.SvChuckVac.Status;
                            //TFLConv.SvChuckVac.Status = !TFLConv.SvChuckVac.Status;
                        }
                        else
                        {
                            //TEZMCAux.TOutput svVac = GantrySelect == EGanytrySelect.GantryR ? TFRConv.SvChuckVac : TFLConv.SvChuckVac;
                            //svVac.Status = !svVac.Status;
                            TCWafer.WaferVacHigh.Status = !TCWafer.WaferVacHigh.Status;
                            TCWafer.WaferVacLow.Status = !TCWafer.WaferVacLow.Status;
                        }
                    }
                    break;
                case ETool.VacExhaust:
                    {
                        TCWafer.WaferExh.Status = !TCWafer.WaferExh.Status;
                        break;
                    }
                case ETool.Cam_Offset:
                    {
                        TEZMCAux.TGroup gantrySelect = GantrySelect == EGanytrySelect.GantryR ? TFGantry.GantryRight : TFGantry.GantryLeft;

                        TFLightCtrl.LightPair[(int)GantrySelect].Set(GRecipes.Board[(int)GantrySelect].LightDefault);
                        TFGantry.GantrySelect.MoveOpZAbs(GRecipes.Board[(int)GantrySelect].StartPos.Z);
                        var pos = (new PointD() - GSetupPara.Calibration.NeedleXYOffset[(int)GantrySelect]).ToArray;

                        MsgBox.Processing(GantrySelect.ToString() + " Goto Cam Offset", () => gantrySelect.MoveOpXYRel(pos));
                    }
                    break;
                case ETool.Goto_Laser:
                    {
                        TEZMCAux.TGroup gantrySelect = GantrySelect == EGanytrySelect.GantryR ? TFGantry.GantryRight : TFGantry.GantryLeft;
                        var pos = GSetupPara.Calibration.LaserOffset[(int)GantrySelect];

                        MsgBox.Processing(GantrySelect.ToString() + " Goto Laser", () => gantrySelect.MoveOpXYRel(pos.ToArray));
                    }
                        break;
                case ETool.Teach_XY:
                    {
                        TEZMCAux.TGroup gantrySelect = GantrySelect == EGanytrySelect.GantryR ? TFGantry.GantryRight : TFGantry.GantryLeft;
                        TCCalibration.NeedleXYOffsets[gantrySelect.Index].Execute(true);
                    }
                    break;
                case ETool.Teach_Z:
                    {
                        TEZMCAux.TGroup gantrySelect = GantrySelect == EGanytrySelect.GantryR ? TFGantry.GantryRight : TFGantry.GantryLeft;
                        TCCalibration.NeedleZTouches[gantrySelect.Index].Execute(TCCalibration.NeedleZTouch.EZCalMode.Normal, true);
                    }
                    break;
                case ETool.Touch_Dot:
                    {
                        //TEZMCAux.TGroup gantrySelect = select == ESelect.GantryR ? TFGantry.GantryRight : TFGantry.GantryLeft;
                        //TCCalNeedleZTouch.TouchDot(gantrySelect);
                    }
                    break;
                case ETool.VacClean:
                    {
                        if (dualMode)
                        {
                            MsgBox.Processing("Vac Clean", () =>
                            {
                                var g1 = Task.Run(() => { TCNeedleFunc.CFP[0].Execute(ENeedleCleanMode.VacClean); });
                                var g2 = Task.Run(() => { TCNeedleFunc.CFP[1].Execute(ENeedleCleanMode.VacClean); });
                                Task.WaitAll(g1, g2);
                            }, () =>
                            {
                                TCNeedleFunc.CFP[0].running = false;
                                TCNeedleFunc.CFP[1].running = false;
                            });
                        }
                        else
                            MsgBox.Processing(GantrySelect.ToString() + " Vac Clean", () => TCNeedleFunc.CFP[(int)GantrySelect].Execute(ENeedleCleanMode.VacClean), () => TCNeedleFunc.CFP[(int)GantrySelect].running = false);
                    }
                    break;
                case ETool.Flush:
                    {
                        if (dualMode)
                        {
                            MsgBox.Processing("Flush", () =>
                            {
                                var g1 = Task.Run(() => { TCNeedleFunc.CFP[0].Execute(ENeedleCleanMode.Flush); });
                                var g2 = Task.Run(() => { TCNeedleFunc.CFP[1].Execute(ENeedleCleanMode.Flush); });
                                Task.WaitAll(g1, g2);
                            }, () =>
                            {
                                TCNeedleFunc.CFP[0].running = false;
                                TCNeedleFunc.CFP[1].running = false;
                            });
                        }
                        else
                            MsgBox.Processing(GantrySelect.ToString() + " Flush", () => TCNeedleFunc.CFP[(int)GantrySelect].Execute(ENeedleCleanMode.Flush), () => TCNeedleFunc.CFP[(int)GantrySelect].running = false);
                    }
                    break;
                case ETool.Purge:
                    {
                        if (dualMode)
                        {
                            MsgBox.Processing("Purge", () =>
                            {
                                var g1 = Task.Run(() => { TCNeedleFunc.CFP[0].Execute(ENeedleCleanMode.Purge); });
                                var g2 = Task.Run(() => { TCNeedleFunc.CFP[1].Execute(ENeedleCleanMode.Purge); });
                                Task.WaitAll(g1, g2);
                            }, () =>
                            {
                                TCNeedleFunc.CFP[0].running = false;
                                TCNeedleFunc.CFP[1].running = false;
                            });
                        }
                        else
                            MsgBox.Processing(GantrySelect.ToString() + " Purge", () => TCNeedleFunc.CFP[(int)GantrySelect].Execute(ENeedleCleanMode.Purge), () => TCNeedleFunc.CFP[(int)GantrySelect].running = false);
                    }
                    break;
                case ETool.Purge_Stage:
                    {
                        if (dualMode)
                        {
                            MsgBox.Processing("Purge Stage", () =>
                            {
                                var g1 = Task.Run(() => { TCNeedleFunc.PurgeStage[0].Execute(); });
                                var g2 = Task.Run(() => { TCNeedleFunc.PurgeStage[1].Execute(); });
                                Task.WaitAll(g1, g2);
                            },
                            () => 
                            { 
                                TCNeedleFunc.PurgeStage[0].Running = false;
                                TCNeedleFunc.PurgeStage[1].Running = false;
                            });
                        }
                        else
                            MsgBox.Processing(GantrySelect.ToString() + " Purge Stage", () => TCNeedleFunc.PurgeStage[(int)GantrySelect].Execute(), () => TCNeedleFunc.PurgeStage[(int)GantrySelect].Running = false);
                        break;
                    }
                case ETool.Manual_Load:
                    {
                        TCWafer.Manual_Load();
                        break;
                    }
                case ETool.Manual_Unload:
                    {
                        TCWafer.Manual_Unload();
                        break;
                    }
                case ETool.Pump_Maint:
                    {
                        MsgBox.Processing("Goto Machine Pos", () => TCNeedleFunc.Maint[(int)GantrySelect].GotoPumpMaint(false));

                        TCCalibration.LaserCal[0].CalibrationState = ECalibrationState.Fail;
                        TCCalibration.NeedleXYOffsets[0].CalibrationState = ECalibrationState.Fail;
                        TCCalibration.NeedleZTouches[0].CalibrationState = ECalibrationState.Fail;

                        MsgBox.ShowDialog("Please Do Calibration.", MsgBoxBtns.OK);
                        break;
                    }
            }
        }
        static bool dualMode = false;
        public static void UpdateTool(ToolStrip ts)
        {
            ts.Items.Clear();
            ts.Padding = new Padding();
            ts.Margin = new Padding();
            ts.AutoSize = false;
            ts.Height = 55;

            Size btnsize = new Size(80, 40);

            //ToolStripButton tsbtnSelect = new ToolStripButton();
            //ToolStripButton tsbtnDual = new ToolStripButton();

            //#region Select Button
            //EditTsbtn(tsbtnSelect, GantrySelect.ToString());
            //tsbtnSelect.Alignment = ToolStripItemAlignment.Left;
            //updateBtnSelect();
            //tsbtnSelect.Click += (a, b) =>
            //{
            //    dualMode = false;
            //    if ((int)GantrySelect >= Enum.GetNames(typeof(EGanytrySelect)).Length - 1)
            //        GantrySelect = EGanytrySelect.GantryL;
            //    else
            //        GantrySelect++;
            //    updateBtnSelect();
            //    //updateBtnDual();
            //};
            //ts.Items.Add(tsbtnSelect);
            //ts.Items.Add(new ToolStripSeparator());

            //void updateBtnSelect()
            //{
            //    tsbtnSelect.Text = GantrySelect.ToString();
            //    ts.BackColor = GantrySelect == EGanytrySelect.GantryL ? GSystemCfg.Display.LeftColor : GSystemCfg.Display.RightColor;
            //}
            //#endregion

            //#region Dual Button - Momentary Dual Head Select
            //EditTsbtn(tsbtnDual, "Single");
            //tsbtnDual.Alignment = ToolStripItemAlignment.Left;
            //updateBtnDual();
            //tsbtnDual.Click += (a, b) =>
            //{
            //    dualMode = !dualMode;
            //    updateBtnSelect();
            //    updateBtnDual();
            //};
            //ts.Items.Add(tsbtnDual);
            //ts.Items.Add(new ToolStripSeparator());

            //void updateBtnDual()
            //{
            //    tsbtnDual.Text = dualMode ? "Single" : "Dual";
            //    if (dualMode)
            //    {
            //        tsbtnSelect.Text = "Gantry(L + R)";
            //        ts.BackColor = GSystemCfg.Display.DualColor;
            //    }
            //}
            //#endregion

            foreach (var t in DispTools)
            {
                if (!t.IsActive) continue;

                string tname = t.Tool.ToString();

                ToolStripButton tsbtn = new ToolStripButton();
                EditTsbtn(tsbtn, tname);

                tsbtn.Click += (a, b) =>
                {
                    if (t.Promptb4Run)
                    {
                        if (MsgBox.ShowDialog($"Execute Tool [{tname}]?", MsgBoxBtns.YesNo) != DialogResult.Yes) return;
                    }
                    Execute(t.Tool);
                    dualMode = false;
                    //updateBtnSelect();
                    //updateBtnDual();
                };
                ts.Items.Add(tsbtn);
                ts.Items.Add(new ToolStripSeparator());
            }

            ToolStripButton tsbtnEdit = new ToolStripButton();
            EditTsbtn(tsbtnEdit, "Edit");
            tsbtnEdit.Alignment = ToolStripItemAlignment.Right;
            tsbtnEdit.Click += (a, b) =>
            {
                new frmToolEdit().ShowDialog();
                UpdateTool(ts);
            };
            ts.Items.Add(tsbtnEdit);

            GControl.UpdateFormControl(ts.FindForm());

            void EditTsbtn(ToolStripButton tsbtn, string name)
            {
                tsbtn.Text = name.Replace("_", "");
                tsbtn.AutoSize = true;
                tsbtn.Size = btnsize;
                tsbtn.Name = "tsbtn" + name;
                tsbtn.BackColor = Color.Transparent;
                tsbtn.Margin = new Padding();
                tsbtn.Padding = new Padding();
            }
        }
        public static void UpdateTool(ToolStrip ts, int index)
        {
            GantrySelect = (EGanytrySelect)index;
            UpdateTool(ts);
        }

        public static bool Save(string filepath)
        {
            return GDoc.SaveINI(filepath, MethodBase.GetCurrentMethod().DeclaringType);
        }
        public static bool Save()
        {
            return Save(GDoc.ToolFile.FullName);
        }
        public static bool Load(string filepath)
        {
            return GDoc.LoadINI(filepath, MethodBase.GetCurrentMethod().DeclaringType);
        }
        public static bool Load()
        {
            return Load(GDoc.ToolFile.FullName);
        }
    }

    public class TETool
    {
        [DisplayName("Function")]
        public ETool Tool { get; set; }
        [DisplayName("Is Activated")]
        public bool IsActive { get; set; } = false;
        [DisplayName("Prompt Before Run")]
        public bool Promptb4Run { get; set; } = true;
        public TETool(ETool tool)
        {
            Tool = tool;
        }
        public TETool()
        {
        }
    }
}
