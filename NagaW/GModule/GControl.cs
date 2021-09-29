using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.Hosting;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Runtime.InteropServices;
//using SpinnakerNET;
//using Emgu.CV.XFeatures2D;

namespace NagaW
{
    public enum ELanguage { Default, Korea, ChineseS }

    public static class GControl
    {
        public static IEnumerable<Control> GetChildItems(Control control, params Type[] ctrls)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SkipWhile(x => x is Form)
                .SelectMany(c => GetChildItems(c, ctrls))
                .Concat(controls)
                .Where(c => ctrls.Length is 0 || ctrls.Contains(c.GetType()));
        }
        public static IEnumerable<ToolStripItem> GetToolStripItems(ToolStrip ts)
        {
            var tsitems = ts.Items.Cast<ToolStripItem>();

            return tsitems.SelectMany(x => GetToolStripItems(x)).Concat(tsitems);

            IEnumerable<ToolStripItem> GetToolStripItems(ToolStripItem tsi)
            {
                IEnumerable<ToolStripItem> tsicollection = null;
                switch (tsi)
                {
                    case ToolStripDropDownButton tsddbtn:
                        tsicollection = tsddbtn.DropDownItems.Cast<ToolStripItem>();
                        return tsicollection.SelectMany(t => GetToolStripItems(t)).Concat(tsicollection);

                    case ToolStripMenuItem tsmi:
                        tsicollection = tsmi.DropDownItems.Cast<ToolStripItem>();
                        return tsicollection.SelectMany(t => GetToolStripItems(t)).Concat(tsicollection);

                    default: return new List<ToolStripItem>();
                }
            }
        }

        public static void ShowOnce(Form frm)
        {
            var f = Application.OpenForms.OfType<Form>().Where(x => x.GetType() == frm.GetType()).FirstOrDefault();
            if (f is null)
            {
                frm.TopMost = frm.TopLevel = true;
                frm.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
                f.BringToFront();
            }
        }

        public static void EditForm(Form frm)
        {
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.MaximizeBox = frm.MinimizeBox = false;
            frm.ShowIcon = false;
            frm.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        public static void UI_Disable(params dynamic[] except_ctrl)
        {
            Application.OpenForms.OfType<Form>().Where(x => !(x is frmPrompt)).Where(f => !except_ctrl.Contains(f)).ToList().ForEach(f =>
            {
                GetChildItems(f).ToList().ForEach(c =>
                {
                    f.Invoke(new Action(() =>
                    {
                        if (c is ToolStrip)
                        {
                            GetToolStripItems(c as ToolStrip).ToList().ForEach(t => t.Enabled = except_ctrl.Contains(t));
                            return;
                        }
                        if (!c.HasChildren) c.Enabled = except_ctrl.Contains(c);
                    }));
                });
            });
        }
        public static void UI_DisableExceptJog()
        {
            var d = Application.OpenForms.OfType<frmJogCtrl>().ToList();
            var jog = Application.OpenForms.OfType<frmJogCtrl>().FirstOrDefault();
            UI_Disable(jog);
        }
        public static void UI_DisableExceptCamJog()
        {
            var cam = Application.OpenForms.OfType<frmFlirCamera>().FirstOrDefault();
            var jog = Application.OpenForms.OfType<frmJogCtrl>().FirstOrDefault();
            UI_Disable(cam, jog);
        }
        public static void UI_Enable()
        {
            Application.OpenForms.OfType<Form>().ToList().ForEach(f =>
            {
                GetChildItems(f).ToList().ForEach(c =>
                {
                    f.Invoke(new Action(() =>
                    {
                        if (c is ToolStrip) GetToolStripItems(c as ToolStrip).ToList().ForEach(t => t.Enabled = true);
                        c.Enabled = true;
                    }));
                });
            });
        }

        public static void UpdateAllFlpColor(Color clr)
        {
            foreach (Form f in Application.OpenForms)
            {
                var flps = GetChildItems(f, typeof(FlowLayoutPanel));
                flps.Where(x => x.AccessibleName is "bitch").ToList().ForEach(b => b.BackColor = clr);
            }
        }
        public static void ConvertTabCtrlToFLP(TabControl tctrl)
        {
            var frm = tctrl.FindForm();
            frm.Visible = false;

            Color themeclr = GSystemCfg.Display.ThemeColor;
            Size btnsize = new Size(90, 28);

            var flp = new FlowLayoutPanel()
            {
                AutoSize = true,
                //AutoScroll = true,
                Dock = DockStyle.Top,
                BackColor = themeclr,
                Margin = new Padding(),
                Padding = new Padding(),
                AccessibleName = "bitch"
            };

            var btnlist = new List<Button>();

            tctrl.SizeMode = TabSizeMode.Fixed;
            tctrl.Appearance = TabAppearance.Buttons;
            tctrl.Padding = new Point();
            tctrl.Margin = new Padding();
            tctrl.ItemSize = new Size(0, 1);
            tctrl.Visible = false;

            foreach (TabPage tp in tctrl.TabPages)
            {
                tp.Margin = new Padding(0);
                tp.Padding = new Padding(0);
                tp.BorderStyle = BorderStyle.None;
                tp.AutoScroll = true;

                Button btn = new Button();

                btn.Size = btnsize;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Margin = new Padding();
                btn.Name = "btn" + tp.Text;
                btn.Text = tp.Text;
                btn.BackColor = Color.Transparent;
                btnlist.Add(btn);
                //btn.AutoSize = true;
                btn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                btn.Click += (a, b) =>
                {
                    tctrl.SelectedTab = tp;
                    tctrl.Visible = true;
                    btnlist.ForEach(bb =>
                    {
                        bb.BackColor = Color.Transparent;
                        bb.Size = btnsize;
                    });
                    btn.BackColor = tp.BackColor;
                };
                flp.Controls.Add(btn);
            }

            btnlist[0].PerformClick();

            frm.Controls.Add(flp);
            frm.Visible = true;
        }
        public static void EditCommonUsedDgv(DataGridView dgv)
        {
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.RaisedHorizontal;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.AllowUserToAddRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        public static void TriggerUpdateDisplayForAll()
        {
            Application.OpenForms.OfType<Form>().ToList().ForEach(f =>
            {
                f.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(m => m.Name.ToLower() == "updatedisplay").ToList().ForEach(m =>
                {
                    try
                    {
                        m.Invoke(f, null);
                    }
                    catch { }
                });
            });
        }

        static Type[] UpdateCtrlTypes = new Type[]
        {
            typeof(Label),
            typeof(Button),
            typeof(CheckBox),
            typeof(ComboBox),
            typeof(GroupBox),
            typeof(ToolStrip),
            typeof(StatusStrip),
        };
        static void Save(TEDisplay configUI)
        {
            IniFile iniFile = new IniFile(GDoc.DisplayCtrlDir.FullName + configUI.FrmName + ".ini");
            iniFile.Write(configUI.CtrlName, nameof(TEDisplay), new bool[] { configUI.R_Ope, configUI.R_Tech, configUI.R_Eng, configUI.H_Ope, configUI.H_Tech, configUI.H_Eng });
        }
        static void Load(TEDisplay configUI)
        {
            IniFile iniFile = new IniFile(GDoc.DisplayCtrlDir.FullName + configUI.FrmName + ".ini");

            bool[] a = new bool[] { configUI.R_Ope, configUI.R_Tech, configUI.R_Eng, configUI.H_Ope, configUI.H_Tech, configUI.H_Eng };
            var dd = iniFile.ReadBoolArray(configUI.CtrlName, nameof(TEDisplay), ref a);

            configUI.R_Ope = a[0];
            configUI.R_Tech = a[1];
            configUI.R_Eng = a[2];
            configUI.H_Ope = a[3];
            configUI.H_Tech = a[4];
            configUI.H_Eng = a[5];
        }
        public static void UpdateFormControl(Form frm)
        {
            GetChildItems(frm, UpdateCtrlTypes).ToList().ForEach(ctrl =>
            {
                if (ctrl is ToolStrip)
                {
                    GetToolStripItems(ctrl as ToolStrip).ToList().ForEach(tsitem =>
                    {
                        #region

                        var configUIts = new TEDisplay(frm.Name, tsitem.Name);
                        Load(configUIts);
                        tsitem.AccessibleDescription = frm.Name;

                        switch (TFUser.CurrentUser.Level)
                        {
                            case Elevel.TECHNICIAN:
                                tsitem.Visible = !configUIts.H_Tech;
                                tsitem.Enabled = !configUIts.R_Tech;
                                break;
                            case Elevel.OPERATOR:
                                tsitem.Visible = !configUIts.H_Ope;
                                tsitem.Enabled = !configUIts.R_Ope;
                                break;
                            case Elevel.ENGINEER:
                                tsitem.Visible = !configUIts.H_Eng;
                                tsitem.Enabled = !configUIts.R_Eng;
                                break;
                            case Elevel.ADMIN:
                                tsitem.Visible = true;
                                tsitem.Enabled = true;
                                break;
                        }

                        tsitem.MouseDown -= Target_MouseDown;
                        tsitem.MouseDown += Target_MouseDown;
                        #endregion
                    });

                    return; //return to block toolstrip as UI
                }

                #region

                var configUI = new TEDisplay(frm.Name, ctrl.Name);
                Load(configUI);

                frm.Invoke(new Action(() =>
                {
                    switch (TFUser.CurrentUser.Level)
                    {
                        case Elevel.TECHNICIAN:
                            ctrl.Visible = !configUI.H_Tech;
                            ctrl.Enabled = !configUI.R_Tech;
                            break;
                        case Elevel.OPERATOR:
                            ctrl.Visible = !configUI.H_Ope;
                            ctrl.Enabled = !configUI.R_Ope;
                            break;
                        case Elevel.ENGINEER:
                            ctrl.Visible = !configUI.H_Eng;
                            ctrl.Enabled = !configUI.R_Eng;
                            break;
                        case Elevel.ADMIN:
                            ctrl.Visible = true;
                            ctrl.Enabled = true;
                            break;
                    }
                }));

                ctrl.MouseDown -= Target_MouseDown;
                ctrl.MouseDown += Target_MouseDown;
                #endregion
            });
        }
        static void Target_MouseDown(object sender, MouseEventArgs e)
        {
            if (TFUser.CurrentUser.Level != Elevel.ADMIN) return;
            if (e.Button == MouseButtons.Left) return;


            switch (sender)
            {
                case Control ctrl:
                    {
                        TEDisplay configUI = new TEDisplay(ctrl.FindForm().Name, ctrl.Name);
                        Load(configUI);

                        ctrl.Enabled = false;

                        ShowCMS(configUI, ctrl.GetType().Name, $"Text: {ctrl.Text}", (a, b) =>
                        {
                            ctrl.Enabled = true;
                            Save(configUI);
                        });
                    }
                    break;
                case ToolStripItem tsitem:
                    {
                        TEDisplay configUI = new TEDisplay(tsitem.AccessibleDescription, tsitem.Name);
                        Load(configUI);

                        tsitem.Enabled = false;

                        ShowCMS(configUI, tsitem.GetType().Name, $"Text: {tsitem.Text}", (a, b) =>
                        {
                            tsitem.Enabled = true;
                            Save(configUI);
                        });
                    }
                    break;
            }
            #region cms
            void ShowCMS(TEDisplay configUI, string type, string text, ToolStripDropDownClosingEventHandler closing_event)
            {
                Size size = new Size(160, 23);
                ContextMenuStrip cms = new ContextMenuStrip();

                ToolStripItem[] tslist = new ToolStripItem[]
                {
                    new ToolStripLabel(type) { ForeColor = Color.Navy },
                    new ToolStripLabel(text) { ForeColor = Color.Navy },
                    new ToolStripSeparator(),
                    new ToolStripLabel("ReadOnly by") { ForeColor = Color.Navy, Font = new Font("Tahoma", 9, FontStyle.Bold) },
                    new ToolStripSeparator(),
                    new ToolStripButton(nameof(Elevel.TECHNICIAN), null, (a, b) => configUI.R_Tech = (a as ToolStripButton).Checked) { CheckOnClick = true, Checked = configUI.R_Tech, Size = size },
                    new ToolStripButton(nameof(Elevel.OPERATOR), null, (a, b) => configUI.R_Ope = (a as ToolStripButton).Checked) { CheckOnClick = true, Checked = configUI.R_Ope, Size = size },
                    new ToolStripButton(nameof(Elevel.ENGINEER), null, (a, b) => configUI.R_Eng = (a as ToolStripButton).Checked) { CheckOnClick = true, Checked = configUI.R_Eng, Size = size },
                    new ToolStripLabel("Hide From") { ForeColor = Color.Navy, Font = new Font("Tahoma", 9, FontStyle.Bold) },
                    new ToolStripSeparator(),
                    new ToolStripButton(nameof(Elevel.TECHNICIAN), null, (a, b) => configUI.H_Tech = (a as ToolStripButton).Checked) { CheckOnClick = true, Checked = configUI.H_Tech, Size = size },
                    new ToolStripButton(nameof(Elevel.OPERATOR), null, (a, b) => configUI.H_Ope = (a as ToolStripButton).Checked) { CheckOnClick = true, Checked = configUI.H_Ope, Size = size },
                    new ToolStripButton(nameof(Elevel.ENGINEER), null, (a, b) => configUI.H_Eng = (a as ToolStripButton).Checked) { CheckOnClick = true, Checked = configUI.H_Eng, Size = size },
                    new ToolStripLabel("Preview UI In Mode"){ ForeColor = Color.Navy, Font = new Font("Tahoma", 9, FontStyle.Bold) },
                    new ToolStripSeparator(),
                    new ToolStripButton(nameof(Elevel.TECHNICIAN), null, (a, b) => PreviewMode(Elevel.TECHNICIAN)),
                    new ToolStripButton(nameof(Elevel.OPERATOR), null, (a, b) => PreviewMode(Elevel.OPERATOR)),
                    new ToolStripButton(nameof(Elevel.ENGINEER), null, (a, b) => PreviewMode(Elevel.ENGINEER)),
                };

                cms.Font = new Font("Tahoma", 9);
                cms.Items.AddRange(tslist);
                cms.Click += (a, b) => cms.AutoClose = false;
                cms.MouseLeave += (a, b) => cms.AutoClose = true;
                cms.Closing += closing_event;
                cms.Show(Cursor.Position);

                void PreviewMode(Elevel elevel)
                {
                    cms.Close();
                    var currentuser = TFUser.CurrentUser;
                    TFUser.CurrentUser = new TEUser() { Level = elevel };
                    Application.OpenForms.Cast<Form>().ToList().ForEach(x => UpdateFormControl(x));
                    MsgBox.ShowDialog($"Exit {elevel} preview mode");
                    TFUser.CurrentUser = currentuser;
                    Application.OpenForms.Cast<Form>().ToList().ForEach(x => UpdateFormControl(x));
                }
            }
            #endregion
        }

        public static void LogForm(Form frm)
        {
            GetChildItems(frm).ToList().ForEach(ctrl =>
            {
                switch (ctrl)
                {
                    case Button btn:
                        {
                            btn.MouseDown += (a, b) => GLog.WriteLog(ELogType.CTRL, $"{frm.Name}_{btn.Name}");
                        }
                        break;
                    case ComboBox combo:
                        {
                            combo.VisibleChanged += (a, b) => combo.AccessibleName = combo.SelectedItem.ToString();
                            combo.SelectionChangeCommitted += (a, b) =>
                            {
                                GLog.WriteLog(ELogType.CTRL, $"{frm.Name}_{combo.Name}_{combo.Tag}_>_{combo.AccessibleName}");
                                combo.AccessibleName = combo.SelectedItem.ToString();
                            };
                        }
                        break;
                    case Label lbl:
                        {
                            lbl.MouseDown += (a, b) => GLog.WriteLog(ELogType.CTRL, $"{frm.Name}_{lbl.Name}");
                        }
                        break;
                    case CheckBox checkBox:
                        {
                            checkBox.Click += (a, b) => GLog.WriteLog(ELogType.CTRL, $"{frm.Name}_{checkBox.Name}_{checkBox.Checked}");
                        }
                        break;
                    case ToolStrip toolStrip:
                        {
                            GetToolStripItems(toolStrip).ToList().ForEach(y =>
                            {
                                y.MouseDown += (a, b) => GLog.WriteLog(ELogType.CTRL, $"{frm.Name}_{y.Name}");
                            });
                        }
                        break;
                    case PropertyGrid propertyGrid:
                        {
                            propertyGrid.PropertyValueChanged += (a, b) => GLog.WriteLog(ELogType.CTRL, $"{b.ChangedItem.PropertyDescriptor.Name}: {b.OldValue} => {b.ChangedItem.Value}");
                        }
                        break;
                }
            });
        }


        static Type[] LangUsedControl = new Type[]
        {
            typeof(Label),
            typeof(Button),
            typeof(TabPage),
            typeof(GroupBox),
            typeof(CheckBox),
        };

        public static void SaveLang(Form frm, ELanguage lang)
        {
            IniFile iniFile = new IniFile(GDoc.LanguageDir.FullName + lang.ToString() + "\\" + frm.Name + ".lang");

            foreach (var ctrl in GetChildItems(frm, LangUsedControl))
            {
                if (string.IsNullOrEmpty(ctrl.Name)) continue;
                iniFile.Write(ctrl.Name, ctrl.Text, string.Empty);
            }
        }
        public static void SaveLang(Form frm)
        {
            SaveLang(frm, GSystemCfg.Display.Language);
        }
        public static void LoadLang(Form frm, ELanguage lang)
        {
            IniFile iniFile = new IniFile(GDoc.LanguageDir.FullName + lang.ToString() + "\\" + frm.Name + ".lang");

            foreach (var ctrl in GetChildItems(frm, LangUsedControl))
            {
                if (string.IsNullOrEmpty(ctrl.Name)) continue;

                string ctrltext = ctrl.Text;
                if (iniFile.ReadString(ctrl.Name, ctrl.Text, ref ctrltext))
                {
                    Action act = new Action(() => ctrl.Text = string.IsNullOrEmpty(ctrltext) ? ctrl.Text : ctrltext);
                    frm.Invoke(act);
                };
            }
        }
        public static void LoadLang(Form frm)
        {
            LoadLang(frm, GSystemCfg.Display.Language);
        }
        public static void SaveLangForAll()
        {
            var frms = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => x.BaseType == typeof(Form))
                .Select(x => (Form)Activator.CreateInstance(x));

            frms.ToList().ForEach(x => SaveLang(x, ELanguage.Default));
        }

        public static string ToStringForDisplay(this EUnit unit)
        {
            var unitfield = typeof(EUnit).GetField(unit.ToString());
            var attributes = unitfield.GetCustomAttributes(typeof(DescriptionAttribute), false)[0];
            return (attributes as DescriptionAttribute).Description;
        }
        public static void MoveUp(this IList list, int index)
        {
            if (index <= 0) return;

            var t = list[index];
            list.RemoveAt(index);
            list.Insert(index - 1, t);
        }
        public static void MoveDown(this IList list, int index)
        {
            if (index >= list.Count - 1) return;

            var t = list[index];
            list.RemoveAt(index);
            list.Insert(index + 1, t);
        }

        public static void UpdatePara(this Label lbl, IPara para)
        {
            lbl.BackColor = Color.White;
            lbl.ForeColor = para.IsOutofRange && GSystemCfg.Display.NotifyOutOfRange ? Color.Red : Color.Navy;
            lbl.TextAlign = ContentAlignment.MiddleLeft;
            lbl.BorderStyle = BorderStyle.Fixed3D;
            lbl.Text = para.ToStringForDisplay();
        }
        public static void UpdatePara(this Label lbl, DPara para)
        {
            lbl.BackColor = Color.White;
            lbl.ForeColor = para.IsOutofRange && GSystemCfg.Display.NotifyOutOfRange ? Color.Red : Color.Navy;
            lbl.TextAlign = ContentAlignment.MiddleLeft;
            lbl.BorderStyle = BorderStyle.Fixed3D;

            //lbl.Text = para.ToStringForDisplay();

            var dpara = new DPara(para);
            dpara = dpara.ConvertTo(GSystemCfg.Display.PressUnit);
            lbl.Text = dpara.ToStringForDisplay();
        }


        public static string TimeSpanToString(this TimeSpan time)
        {
            return $"{time.TotalHours:00}H : {time.Minutes:00}M : {time.Seconds:00}S";
        }


        public static Array Create2DArray(Type type, int idx0, int idx1)
        {
            var array = Array.CreateInstance(type, idx0, idx1);

            for (int m0 = 0; m0 < array.GetLength(0); m0++)
            {
                for (int m1 = 0; m1 < array.GetLength(1); m1++)
                {
                    array.SetValue(Activator.CreateInstance(type), m0, m1);
                }
            }

            return array;
        }
    }

    public class TEDisplay
    {
        public string FrmName;
        public string CtrlName;

        public bool R_Tech = false;
        public bool R_Ope = false;
        public bool R_Eng = false;

        public bool H_Tech = false;
        public bool H_Ope = false;
        public bool H_Eng = false;

        public TEDisplay(string frmname, string ctrlname)
        {
            FrmName = frmname;
            CtrlName = ctrlname;
        }
    }
    class MsgBox
    {
        public static DialogResult ShowDialog(string msg, MsgBoxBtns btns)
        {
            var dr = new frmMsgbox(msg, btns).ShowDialog();
            string log = $"Msg:{msg} Dialogresult:{dr}";
            GLog.WriteLog(ELogType.NOTIFY, log);
            return dr;
        }
        public static DialogResult ShowDialog(string msg)
        {
            return ShowDialog(msg, MsgBoxBtns.OK);
        }

        public static void Processing(string msg, Action action, Action cancelaction)
        {
            if (Application.OpenForms.Count is 0)
            {
                new frmProcessing(action, cancelaction, msg).ShowDialog();
            }
            else
            {
                Application.OpenForms[0].Invoke(new Action(() => new frmProcessing(action, cancelaction, msg).ShowDialog()));
            }
        }
        public static void Processing(string msg, Action action)
        {
            Processing(msg, action, null);
        }
    }
}
