using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NagaW
{
    public partial class frmConfig : Form
    {
        int selectedCam = 0;
        int selectedHSensor = 0;
        int SelectedLightCtrl = 0;
        int SelectedWeigh = 0;
        int SelectedFPress = 0;
        int SelectedDispCtrl = 0;
        int SelectedTempCtrl = 0;

        public frmConfig()
        {
            InitializeComponent();
        }

        private void frmSystemCfg_Load(object sender, EventArgs e)
        {
            Set(tpConfig, new CustomObjectWrapper(Activator.CreateInstance(typeof(GSystemCfg.Config))));
            Set(tpGantry, new CustomObjectWrapper(Activator.CreateInstance(typeof(GSystemCfg.Gantry))));
            Set(tpConveyor, new CustomObjectWrapper(Activator.CreateInstance(typeof(GSystemCfg.Conveyor))));

            Set(tpHSensor, GSystemCfg.HSensor.HSensors);
            Set(tpCamera, GSystemCfg.Camera.Cameras);

            Set(tpLightingCtrl, GSystemCfg.Light.Lights);
            //Set(tpWeighing, GSystemCfg.Weigh.Weighs);
            Set(tpPressureCtrl, GSystemCfg.FPress.FPresses);
            Set(tpTempCtrl, GSystemCfg.Temperature.Temp);
            Set(tpDisplay, new CustomObjectWrapper(Activator.CreateInstance(typeof(GSystemCfg.Display))));

            Set(tpDispCtrl, GSystemCfg.Pump.Pumps);

            Set(tpAdvance, new CustomObjectWrapper(Activator.CreateInstance(typeof(GSystemCfg.Advance))));

            Set(tpMakerData, new CustomObjectWrapper(Activator.CreateInstance(typeof(GSystemCfg.MakerData))));

            tabcontrol1.TabPages.Remove(tpWeighing);

            Size = new Size(800, 600);

            UpdateDisplay();

            GControl.UpdateFormControl(this);
            GControl.LogForm(this);
            timer1.Interval = 10;
            timer1.Enabled = true;
        }
        private void frmConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void Set(TabPage ctrl, params object[] objs)
        {
            bool onlyOne = objs.Length is 1;

            TabControl tctrl = new TabControl
            {
                Appearance = TabAppearance.Buttons,
                ItemSize = new Size(60, 35),
                Dock = DockStyle.Fill
            };

            int i = 0;
            foreach (var o in objs)
            {
                PropertyGrid grid = new PropertyGrid
                {
                    SelectedObject = o,
                    ToolbarVisible = false,
                    Dock = DockStyle.Fill,
                    Tag = i,
                };

                TabPage tp = new TabPage { Text = $"{ctrl.Text} {(onlyOne ? "" : (1 + i++).ToString())}" };
                tp.Controls.Add(grid);
                tctrl.TabPages.Add(tp);

                grid.PropertyValueChanged += (a, b) =>
                {
                    string log = tp.Text + b.ChangedItem.PropertyDescriptor.Name;
                    GLog.WriteLog(ELogType.PARA, log + $" {b.OldValue} => {b.ChangedItem.Value}");

                    switch (b.ChangedItem.PropertyDescriptor.Name)
                    {
                        case nameof(GSystemCfg.Display.ThemeColor): GControl.UpdateAllFlpColor((Color)b.ChangedItem.Value); break;
                        case nameof(GSystemCfg.Pump.Pumps): UpdateDispCtrl(grid, o); break;
                        //case nameof(GSystemCfg.DispCtrl.ExTempCtrl): UpdateDispCtrl(grid, o); break;
                    }
                };
                //tp.Enter += (a, b) => UpdateDispCtrl(grid, o);
                if (objs[0].GetType().Name is "Pump") tp.Enter += (a, b) => UpdateDispCtrl(grid, o);
            }

            ctrl.Controls.Add(tctrl);
            tctrl.BringToFront();

            switch (ctrl.Name)
            {
                case nameof(tpHSensor): tctrl.SelectedIndexChanged += (a, b) => selectedHSensor = tctrl.SelectedIndex; break;
                case nameof(tpCamera): tctrl.SelectedIndexChanged += (a, b) => selectedCam = tctrl.SelectedIndex; break;
                
                case nameof(tpLightingCtrl): tctrl.SelectedIndexChanged += (a, b) => SelectedLightCtrl = tctrl.SelectedIndex; break;
                case nameof(tpWeighing): tctrl.SelectedIndexChanged += (a, b) => SelectedWeigh = tctrl.SelectedIndex; break;

                case nameof(tpPressureCtrl): tctrl.SelectedIndexChanged += (a, b) => SelectedFPress = tctrl.SelectedIndex; break;
                
                case nameof(tpDispCtrl): tctrl.SelectedIndexChanged += (a, b) => SelectedDispCtrl = tctrl.SelectedIndex; break;

                case nameof(tpTempCtrl): tctrl.SelectedIndexChanged += (a, b) => SelectedTempCtrl = tctrl.SelectedIndex; break;
            }

            void UpdateDispCtrl(PropertyGrid grid, object o)
            {
                var dpCtrl = GSystemCfg.Pump.Pumps[(int)grid.Tag];
                bool isOK = dpCtrl.PumpType >= EPumpType.VERMES_3280;

                var c = FInfo(nameof(GSystemCfg.Pump.Comport), out BrowsableAttribute attrib);
                if (c != null) c.SetValue(attrib, isOK);

                var su = FInfo(nameof(GSystemCfg.Pump.StartUpEnable), out BrowsableAttribute attrib2);
                if (su != null) su.SetValue(attrib2, isOK);

                //var temp = FInfo(nameof(GSystemCfg.DispCtrl.ExTempCtrl), out BrowsableAttribute attribt);
                //if (temp != null) temp.SetValue(attribt, isOK);

                //var ext = FInfo(nameof(GSystemCfg.DispCtrl.ExTempCtrlCom), out BrowsableAttribute attrib3);
                //if (ext != null) ext.SetValue(attrib3, dpCtrl.ExTempCtrl && isOK);

                //var ext2 = FInfo(nameof(GSystemCfg.DispCtrl.ExTempCtrlStartUpEnable), out BrowsableAttribute attrib4);
                //if (ext2 != null) ext2.SetValue(attrib4, dpCtrl.ExTempCtrl && isOK);


                grid.SelectedObject = o;

                FieldInfo FInfo(string pname, out BrowsableAttribute att)
                {
                    att = null;
                    var descriptor = TypeDescriptor.GetProperties(typeof(GSystemCfg.Pump))[pname];
                    if (descriptor is null) return null;
                    att = (BrowsableAttribute)descriptor.Attributes[typeof(BrowsableAttribute)];
                    var isBrow = att.GetType().GetField("browsable", BindingFlags.NonPublic | BindingFlags.Instance);
                    return isBrow;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            PropertyGrid pg(TabPage tp, int idx)
            {
                var pgs = GControl.GetChildItems(tp, typeof(PropertyGrid)).ToList();
                return pgs[idx] as PropertyGrid;
            }

            Color open = Color.Lime;
            Color close = Color.Red;

            string connected = "Connected";
            string disconnect = "Disconnected";

            pg(tpHSensor, selectedHSensor).Enabled = !TFHSensors.Sensor[selectedHSensor].IsConnected;
            btnHsensorOpen.BackColor = TFHSensors.Sensor[selectedHSensor].IsConnected ? open : close;
            btnHsensorOpen.Text = TFHSensors.Sensor[selectedHSensor].IsConnected ? connected : disconnect;

            pg(tpCamera, selectedCam).Enabled = !TFCameras.Camera[selectedCam].IsConnected;
            btnCamOpen.BackColor = TFCameras.Camera[selectedCam].IsConnected ? open : close;
            btnCamOpen.Text = TFCameras.Camera[selectedCam].IsConnected ? connected : disconnect;

            var light = TFLightCtrl.Lights;
            pg(tpLightingCtrl, SelectedLightCtrl).Enabled = !light.IsOpen;
            btnLightOpen.BackColor = light.IsOpen ? open : close;
            btnLightOpen.Text = light.IsOpen ? connected : disconnect;

            btnWeighOpen.BackColor = TFWeightScale.IsOpen ? open : close;
            btnWeighOpen.Text = TFWeightScale.IsOpen ? connected : disconnect;

            var fPress = TFPressCtrl.FPress[SelectedFPress];
            pg(tpPressureCtrl, SelectedFPress).Enabled = !fPress.IsOpen;
            btnPressCtrlOpen.BackColor = fPress.IsOpen ? open : close;
            btnPressCtrlOpen.Text = $"PCtrl {SelectedFPress + 1} " + (fPress.IsOpen ? connected : disconnect);

            var temp = TFTempCtrl.TempCtrl;
            pg(tpTempCtrl, SelectedTempCtrl).Enabled = !TFTempCtrl.TempCtrl.IsOpen;
            btnTempOpen.BackColor = TFTempCtrl.TempCtrl.IsOpen ? open : close;
            btnTempOpen.Text = TFTempCtrl.TempCtrl.IsOpen ? connected : disconnect;

            var dispctrl = GSystemCfg.Pump.Pumps[SelectedDispCtrl];

            switch (dispctrl.PumpType)
            {
                case EPumpType.VERMES_3280:
                    {
                        var pumpCtrl = TFPump.Vermes_Pump[SelectedDispCtrl];
                        btnDispCtrlOpen.BackColor = pumpCtrl.IsOpen ? open : close;
                        btnDispCtrlOpen.Text = $"Pump {SelectedDispCtrl + 1} " + (pumpCtrl.IsOpen ? connected : disconnect);

                        //var mhc48 = MHC48_1.MHC48_1s[SelectedDispCtrl];
                        //btnDispExTempCtrlOpen.BackColor = mhc48.IsOpen ? open : close;
                        //btnDispExTempCtrlOpen.Text = $"TempCtrl {SelectedDispCtrl + 1} " + (mhc48.IsOpen ? connected : disconnect);

                        //pg(tpDispCtrl, SelectedDispCtrl).Enabled = !(pumpCtrl.IsOpen || mhc48.IsOpen);
                        pg(tpDispCtrl, SelectedDispCtrl).Enabled = !pumpCtrl.IsOpen;

                        break;
                    }
            }
            btnDispCtrlOpen.Visible = dispctrl.PumpType >= EPumpType.VERMES_3280;
            //btnDispExTempCtrlOpen.Visible = btnDispCtrlOpen.Visible && dispctrl.ExTempCtrl;


        }

        private void btnHsensorOpen_Click(object sender, EventArgs e)
        {
            if (TFHSensors.Sensor[selectedHSensor].IsConnected) TFHSensors.Sensor[selectedHSensor].Close();
            else MsgBox.Processing("Connecting HSensor", () => TFHSensors.Sensor[selectedHSensor].Open());
        }
        private void btnHSensorTest_Click(object sender, EventArgs e)
        {
            //new frmMEDAQ().ShowDialog();
        }

        private void btnCamOpen_Click(object sender, EventArgs e)
        {
            if (TFCameras.Camera[selectedCam].IsConnected) TFCameras.Camera[selectedCam].Disconnect();
            else MsgBox.Processing("Connecting Cam", () => TFCameras.Camera[selectedCam].Connect());
        }

        private void btnLightOpen_Click(object sender, EventArgs e)
        {
            if (TFLightCtrl.Lights.IsOpen) TFLightCtrl.Lights.Close();
            else TFLightCtrl.Lights.Open();
        }

        private void btnWeighOpen_Click(object sender, EventArgs e)
        {
            //if (TFWeightScale.IsOpen) TFWeightScale.Close();
            //else TFWeightScale.Open();
        }
        private void btnWeighTest_Click(object sender, EventArgs e)
        {
            new frmWeightScale().ShowDialog();
        }

        private void btnPressCtrlOpen_Click(object sender, EventArgs e)
        {
            if (TFPressCtrl.FPress[SelectedFPress].IsOpen) TFPressCtrl.FPress[SelectedFPress].Close();
            else TFPressCtrl.FPress[SelectedFPress].Open();
        }
        private void btnPressCtrlTest_Click(object sender, EventArgs e)
        {
            new frmPressCtrlTest().ShowDialog();
        }

        private void btnTempOpen_Click(object sender, EventArgs e)
        {
            if (TFTempCtrl.TempCtrl.IsOpen) TFTempCtrl.TempCtrl.Close();
            else TFTempCtrl.TempCtrl.Open();
        }

        private void btnOpenVermesTempCtrl_Click(object sender, EventArgs e)
        {
            //var mhc48 = MHC48_1.MHC48_1s[SelectedVermesTempCtrl];
            //if (mhc48.IsOpen) mhc48.Close();
            //else mhc48.Open();
        }

        //private void btnDispCtrlOpen_Click(object sender, EventArgs e)
        //{
        //    switch (GSystemCfg.DispCtrl.DispCtrls[SelectedDispCtrl].PumpType)
        //    {
        //        default: return;
        //        case EPumpType.VERMES_3280:
        //            var vp = TFPumpCtrl.Vermer_Pump[SelectedDispCtrl];
        //            if (vp.IsOpen) vp.Close();
        //            else vp.Open();
        //            break;
        //    }
        //}

        private void btnDispExTempCtrlOpen_Click(object sender, EventArgs e)
        {
            //switch (GSystemCfg.DispCtrl.DispCtrls[SelectedDispCtrl].PumpType)
            //{
            //    default: return;
            //    case EPumpType.VERMES_3280:
            //        var mhc = MHC48_1.MHC48_1s[SelectedDispCtrl];
            //        if (mhc.IsOpen) mhc.Close();
            //        else mhc.Open();
            //        break;
            //}
        }

        private void btnDispExTempCtrlOpen_Click_1(object sender, EventArgs e)
        {

        }

        private void btnDispCtrlOpen_Click(object sender, EventArgs e)
        {
            switch (GSystemCfg.Pump.Pumps[SelectedDispCtrl].PumpType)
            {
                default: return;
                case EPumpType.VERMES_3280:
                    var vp = TFPump.Vermes_Pump[SelectedDispCtrl];
                    if (vp.IsOpen) vp.Close();
                    else vp.Open();
                    break;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (MsgBox.ShowDialog("Reset Equipment to Factory Default Settings? Warning: Action cannot be undone.", MsgBoxBtns.OKCancel) != DialogResult.OK) return;

            MsgBox.Processing($"Resetting Equipment Model {GSystemCfg.MakerData.EquipmentModel} to Factory Default.", () => 
            {
                TCSystem.ShutDown();

                if (File.Exists(GDoc.ConfigFile.FullName))
                {
                    string newFileName = GDoc.ConfigFile.FullName.Insert(GDoc.ConfigFile.FullName.IndexOf(Path.GetExtension(GDoc.ConfigFile.FullName)), $"_{ DateTime.Now:yyyyMMdd_HHmmssfff}");
                    File.Move(GDoc.ConfigFile.FullName, newFileName);
                }

                TCSystem.StartUp();
                GSystemCfg.SaveFile();

            });

            frmSystemCfg_Load(sender, e);
            GLog.LogProcess($"Reseted Equipment Model {GSystemCfg.MakerData.EquipmentModel} to Factory Default.");
            MsgBox.ShowDialog("Reset Equipment to Factory Default Completed.", MsgBoxBtns.OK);
        }

        private void btnTempCtrlPanel_Click(object sender, EventArgs e)
        {
            new frmTempCtrl().ShowDialog();
        }


        private void UpdateDisplay()
        {
            cbxCentrePatAlign.Checked = GSystemCfg.Option.PromptMSg_AckPAtAlignment_Centred;
        }

        private void cbxCentrePatAlign_Click(object sender, EventArgs e)
        {
            GSystemCfg.Option.PromptMSg_AckPAtAlignment_Centred = !GSystemCfg.Option.PromptMSg_AckPAtAlignment_Centred;
            UpdateDisplay();
        }

    }
}
