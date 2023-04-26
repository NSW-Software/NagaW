using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Reflection;
using System.ComponentModel;
using System.Timers;
using SpinnakerNET.GenApi;

namespace NagaW
{
    using SList = List<dynamic>;

    class TFSecsGems
    {
        internal static GemTaro.GemSystem GemSystem = new GemTaro.GemSystem(GemTaro.TCPIP.EEntityMode.Passive, "", 5000, 0);

        public static bool Connected = false;

        public static bool Open(GemTaro.TCPIP.EEntityMode mode, string ip, int port, int deviceID)
        {
            try
            {
                GemSystem.GemHost.Entitymode = mode;
                GemSystem.GemHost.IP = ip;
                GemSystem.GemHost.Port = port;

                GemSystem.Secsii.DeviceID = deviceID;

                GemSystem.Gem_Start();

                GemSystem.SFMessageReceived += GemSystem_SFMessageReceived;

                Connected = true;
                TFSecsEvent.LoadFile();

                return true;
            }
            catch (Exception ex)
            {
                GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, "SecsGems Open Failed.\n" + ex.Message);
                return false;
            }
        }

        private static void GemSystem_SFMessageReceived(object sender, GemTaro.GemSystem.SFReceived e)
        {
            DecisionMaking_SF(e.SF, e.RawData);
        }

        public static bool Open()
        {
            return Open(GSystemCfg.SecsGem.EntityMode, GSystemCfg.SecsGem.IPAddress, GSystemCfg.SecsGem.Port, GSystemCfg.SecsGem.DeviceID);
        }
        public static bool Close()
        {
            try
            {
                GemSystem.Gem_Stop();

                Connected = false;

                return true;
            }
            catch (Exception ex)
            {
                GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, "SecsGems Close Failed.\n" + ex.Message);
                return false;
            }
        }

        static ReaderWriterLockSlim slim;
        public static void DecisionMaking_SF(GemTaro.SECSII.SFCode sfcode, List<byte> rawdata)
        {
            try
            {
                switch (sfcode)
                {
                    default: break;

                    #region S1
                    case GemTaro.SECSII.SFCode.S1F1:
                        {
                            var list = new SList()
                            {
                                Application.ProductName,
                                Application.ProductVersion
                            };

                            GemSystem.GemHost_Send(GemTaro.SECSII.SFCode.S1F2, list, false);
                            GLog.LogSecsGems(ESecsGemsDir.HostToLocal, sfcode.ToString());
                            break;
                        }
                    case GemTaro.SECSII.SFCode.S1F2:
                        {
                            GLog.LogSecsGems(ESecsGemsDir.HostToLocal, sfcode.ToString());
                            break;
                        }
                    case GemTaro.SECSII.SFCode.S1F3:
                        {
                            GemSystem.Secsii.DecodeBody(ref rawdata, out SList slist);

                            var newList = SearchDataS1F3(slist);

                            GemSystem.GemHost_Send(GemTaro.SECSII.SFCode.S1F4, newList, false);
                            GLog.LogSecsGems(ESecsGemsDir.HostToLocal, sfcode.ToString(), Log.TranslateText(slist));
                            break;
                        }
                    case GemTaro.SECSII.SFCode.S1F5:
                        {
                            GemSystem.Secsii.DecodeBody(ref rawdata, out SList slist);

                            var variable = slist[0];
                            var retVal = SearchDataS1F5(variable);

                            GemSystem.GemHost_Send(GemTaro.SECSII.SFCode.S1F6, retVal, false);
                            GLog.LogSecsGems(ESecsGemsDir.HostToLocal, sfcode.ToString(), Log.TranslateText(slist));
                            break;
                        }
                    case GemTaro.SECSII.SFCode.S1F9:
                        {
                            string msg = "";
                            GemSystem.Secsii.DecodeBody(ref rawdata, out SList slist);

                            var smemaUpIn = GMotDef.Inputs.Where(x => x.Name is "SmemaUpline-BdReady").ToList();
                            var smemaDnIn = GMotDef.Inputs.Where(x => x.Name is "SmemaDnline-McReady").ToList();
                            var smemaUpOut = GMotDef.Outputs.Where(x => x.Name is "SmemaUpline-McReady").ToList();
                            var smemaDnOut = GMotDef.Outputs.Where(x => x.Name is "SmemaDnline-BdReady").ToList();

                            if (smemaUpIn[0].Status) msg = "Master Ready to Send Board";
                            else if (smemaDnIn[0].Status) msg = "Slave Ready to Receive Board";

                            GemSystem.GemHost_Send(GemTaro.SECSII.SFCode.S1F10, msg, false);
                            GLog.LogSecsGems(ESecsGemsDir.HostToLocal, sfcode.ToString(), Log.TranslateText(slist));
                            break;
                        }
                    case GemTaro.SECSII.SFCode.S1F11:
                        {
                            //GemSystem.Secsii.DecodeBody(ref rawdata, out SList slist);

                            var newlist = SearchDataS1F11();

                            GemSystem.GemHost_Send(GemTaro.SECSII.SFCode.S1F12, newlist, false);
                            GLog.LogSecsGems(ESecsGemsDir.HostToLocal, sfcode.ToString());
                            break;
                        }
                    case GemTaro.SECSII.SFCode.S1F13:
                        {
                            var list = new SList()
                            {
                                (byte)0,
                                new SList()
                                {
                                    Application.ProductName,
                                    Application.ProductVersion
                                }
                            };
                            GemSystem.GemHost_Send(GemTaro.SECSII.SFCode.S1F14, list, false);
                            GLog.LogSecsGems(ESecsGemsDir.HostToLocal, sfcode.ToString());
                            break;
                        }
                    case GemTaro.SECSII.SFCode.S1F15:
                        {
                            GemSystem.SetOnlineOffline();
                            GemSystem.GemHost_Send(GemTaro.SECSII.SFCode.S1F16, (byte)0, false);
                            GLog.LogSecsGems(ESecsGemsDir.HostToLocal, sfcode.ToString());
                            break;
                        }
                    case GemTaro.SECSII.SFCode.S1F17:
                        {
                            GemSystem.SetOnlineRemote();
                            GemSystem.GemHost_Send(GemTaro.SECSII.SFCode.S1F18, (byte)0, false);
                            GLog.LogSecsGems(ESecsGemsDir.HostToLocal, sfcode.ToString());
                            break;
                        }
                    case GemTaro.SECSII.SFCode.S1F23:
                        {
                            var newlist = SearchDataS1F23();

                            GemSystem.GemHost_Send(GemTaro.SECSII.SFCode.S1F24, newlist, false);
                            GLog.LogSecsGems(ESecsGemsDir.HostToLocal, sfcode.ToString());
                            break;
                        }
                    #endregion

                    #region S2
                    case GemTaro.SECSII.SFCode.S2F13:
                        {
                            GemSystem.Secsii.DecodeBody(ref rawdata, out SList slist);

                            var retList = SearchDataS2F13();

                            GemSystem.GemHost_Send(GemTaro.SECSII.SFCode.S2F14, retList, false);
                            GLog.LogSecsGems(ESecsGemsDir.HostToLocal, sfcode.ToString());
                            break;
                        }
                    case GemTaro.SECSII.SFCode.S2F21:
                        {
                            GemSystem.Secsii.DecodeBody(ref rawdata, out SList slist);

                            GemSystem.GemHost_Send(GemTaro.SECSII.SFCode.S2F22, (byte)0, false);
                            //GemSystem.SetOnlineRemote();

                            GLog.LogSecsGems(ESecsGemsDir.HostToLocal, sfcode.ToString());
                            break;
                        }
                    case GemTaro.SECSII.SFCode.S2F23:
                        {
                            GemSystem.Secsii.DecodeBody(ref rawdata, out SList slist);

                            bool terminate = slist[2] is 0;

                            if (terminate)
                            {
                                if (TFTraceData.TraceDatas.Where(x => x.ID == slist[0]).Count() > 0)
                                {
                                    var data = TFTraceData.TraceDatas.Select(x => x.ID == slist[0]).ToList();
                                    TFTraceData.TraceDatas.Remove(data[0]);
                                }
                            }
                            else
                            {
                                if (TFTraceData.TraceDatas.Where(x => x.ID == slist[0]).Count() > 0)
                                {
                                    var data = TFTraceData.TraceDatas.Select(x => x.ID == slist[0]).ToList();
                                    TFTraceData.TraceDatas.Remove(data[0]);
                                }
                                TETraceData traceData = new TETraceData();
                                traceData.ID = slist[0];
                                traceData.DataSamplePeriod = slist[1];
                                traceData.TotalSamples = slist[2];
                                traceData.ReportGroupSize = slist[3];

                                for (int i = 4; i < slist.Count; i++) traceData.SVID.Add(slist[i]);

                                traceData.Timer = new System.Threading.Timer(_ => OnCallBack(traceData), null, 0, (int)traceData.DataSamplePeriod.TotalSeconds);

                                TFTraceData.TraceDatas.Add(new TETraceData(traceData));
                            }

                            GemSystem.GemHost_Send(GemTaro.SECSII.SFCode.S2F24, false);

                            void OnCallBack(TETraceData data)
                            {
                                if (data.CurrentSample < data.TotalSamples)
                                {
                                    SList tempList = new SList();
                                    var list = SearchSVData();
                                    for (int i = 0; i < list.Count; i++)
                                    {
                                        for (int j = 0; j < data.SVID.Count; j++)
                                        {
                                            if (data.SVID[j] == list[i].Item1) tempList.Add(data.SVID[j]);
                                        }
                                    }

                                    var returnSList = new SList()
                                    {
                                        data.ID,
                                        tempList.Count(),
                                        DateTime.Now.ToString("yy:MM:dd:hh:mm:ss"),
                                        new SList(tempList),
                                    };

                                    GemSystem.GemHost_Send(GemTaro.SECSII.SFCode.S6F1, returnSList, true);
                                    data.CurrentSample++;
                                }
                                else
                                {
                                    data.Timer.Change(Timeout.Infinite, Timeout.Infinite);
                                }
                            }

                            break;
                        }
                    case GemTaro.SECSII.SFCode.S2F31:
                        {
                            GemSystem.Secsii.DecodeBody(ref rawdata, out dynamic time);

                            var datetime = DateTime.Now.ToString("h:mm:ss tt");
                            GemSystem.GemHost_Send(GemTaro.SECSII.SFCode.S2F32, datetime, false);
                            GLog.LogSecsGems(ESecsGemsDir.HostToLocal, sfcode.ToString(), time.ToString());
                            break;
                        }
                    case GemTaro.SECSII.SFCode.S2F33:
                        {
                            GemSystem.Secsii.DecodeBody(ref rawdata, out SList slist);
                            GemSystem.GemHost_Send(GemTaro.SECSII.SFCode.S2F34, (byte)0, false);
                            GLog.LogSecsGems(ESecsGemsDir.HostToLocal, sfcode.ToString(), Log.TranslateText(slist));
                            break;
                        }
                    case GemTaro.SECSII.SFCode.S2F35:
                        {
                            GemSystem.Secsii.DecodeBody(ref rawdata, out SList slist);
                            GemSystem.GemHost_Send(GemTaro.SECSII.SFCode.S2F36, (byte)0, false);
                            GLog.LogSecsGems(ESecsGemsDir.HostToLocal, sfcode.ToString(), Log.TranslateText(slist));
                            break;
                        }
                    case GemTaro.SECSII.SFCode.S2F37:
                        {
                            GemSystem.Secsii.DecodeBody(ref rawdata, out SList slist);

                            bool prompt = slist[0];

                            foreach (var e in slist[1])
                            {
                                for (int i = 0; i < TFSecsEvent.EventPrompt.Count; i++)
                                {
                                    if (e == TFSecsEvent.EventPrompt[i].Event) TFSecsEvent.EventPrompt[i].Prompt = prompt;
                                }
                            }
                            TFSecsEvent.SaveFile();

                            GemSystem.GemHost_Send(GemTaro.SECSII.SFCode.S2F38, (byte)0, false);
                            GLog.LogSecsGems(ESecsGemsDir.HostToLocal, sfcode.ToString());
                            break;
                        }
                    case GemTaro.SECSII.SFCode.S2F41:
                        {
                            break;
                        }
                    #endregion

                    #region S5
                    case GemTaro.SECSII.SFCode.S5F1:
                        {
                            GemSystem.Secsii.DecodeBody(ref rawdata, out SList slist);

                            GemSystem.GemHost_Send(GemTaro.SECSII.SFCode.S5F1, rawdata, false);
                            GLog.LogSecsGems(ESecsGemsDir.LocalToHost, sfcode.ToString());
                            break;
                        }
                    case GemTaro.SECSII.SFCode.S5F5:
                        {
                            var newlist = SearchDataS5F5();

                            GemSystem.GemHost_Send(GemTaro.SECSII.SFCode.S5F6, newlist, false);
                            GLog.LogSecsGems(ESecsGemsDir.HostToLocal, sfcode.ToString());

                            break;
                        }
                    #endregion

                    #region S6
                    case GemTaro.SECSII.SFCode.S6F6:
                        {
                            GemSystem.Secsii.DecodeBody(ref rawdata, out dynamic grant6);
                            GLog.LogSecsGems(ESecsGemsDir.HostToLocal, sfcode.ToString(), Log.TranslateText(grant6));
                            break;
                        }
                    case GemTaro.SECSII.SFCode.S6F11:
                        {
                            GemSystem.Secsii.DecodeBody(ref rawdata, out dynamic ackc6);

                            GemSystem.GemHost_Send(GemTaro.SECSII.SFCode.S6F12, rawdata, false);
                            GLog.LogSecsGems(ESecsGemsDir.LocalToHost, sfcode.ToString(), Log.TranslateText(ackc6));
                            break;
                        }
                    #endregion

                    #region S7
                    case GemTaro.SECSII.SFCode.S7F1:
                        {
                            GemSystem.Secsii.DecodeBody(ref rawdata, out SList slist);

                            if (slist.Count >= 2)
                            {
                                var ppid = Path.GetFileNameWithoutExtension(slist[0]);
                                var length = slist[1];
                                var filelist = Directory.GetFiles(GDoc.RecipeDir.FullName).Select(x => Path.GetFileNameWithoutExtension(x)).ToList();

                                // 1 : 0
                                int ppgnt_ack = filelist.Contains((string)ppid) ? 0 : 3;

                                bool load = filelist.Contains((string)ppid);
                                var file = GDoc.RecipeDir.FullName + "\\" + ppid + ".xml";
                                if (load) GRecipes.Load(file);

                                GemSystem.GemHost_Send(GemTaro.SECSII.SFCode.S7F2, (byte)ppgnt_ack, false);
                                GLog.LogSecsGems(ESecsGemsDir.HostToLocal, sfcode.ToString(), Log.TranslateText(slist));
                            }
                            break;
                        }
                    case GemTaro.SECSII.SFCode.S7F2:
                        {
                            GemSystem.Secsii.DecodeBody(ref rawdata, out dynamic ppgnt);
                            GemSystem.GemHost_Send(GemTaro.SECSII.SFCode.S7F2, false);
                            GLog.LogSecsGems(ESecsGemsDir.HostToLocal, sfcode.ToString(), Log.TranslateText(ppgnt));
                            break;
                        }
                    case GemTaro.SECSII.SFCode.S7F3:
                        {
                            GemSystem.Secsii.DecodeBody(ref rawdata, out SList slist);

                            if (slist.Count >= 2)
                            {
                                try
                                {
                                    var ppid = Path.GetFileNameWithoutExtension(slist[0]);
                                    var ppbody = slist[1];
                                    var file = GDoc.RecipeDir.FullName + "\\" + ppid + ".xml";

                                    slim.EnterWriteLock();
                                    StreamWriter a = new StreamWriter(file);
                                    a.Write(ppbody as string);
                                    a.Close();
                                    slim.ExitWriteLock();

                                    GemSystem.GemHost_Send(GemTaro.SECSII.SFCode.S7F4, (byte)0, false);
                                    GLog.LogSecsGems(ESecsGemsDir.HostToLocal, sfcode.ToString() + file + "Successfully loaded");
                                }
                                catch (Exception ex)
                                {
                                    GLog.LogSecsGems(ESecsGemsDir.HostToLocal, sfcode.ToString() + ex.Message.ToString(), "");
                                }
                            }
                            break;
                        }
                    case GemTaro.SECSII.SFCode.S7F5:
                        {
                            GemSystem.Secsii.DecodeBody(ref rawdata, out dynamic ppid);

                            var file = Directory.GetFiles(GDoc.RecipeDir.FullName).ToList().Find(x => Path.GetFileName(x) == ppid);
                            if (file is null)
                            {
                                GemSystem.GemHost_Send(GemTaro.SECSII.SFCode.S7F6, new SList(), false);
                            }
                            else
                            {
                                StreamReader a = new StreamReader(file);

                                var list = new SList()
                                {
                                    ppid,
                                    a.ReadToEnd()
                                };
                                GemSystem.GemHost_Send(GemTaro.SECSII.SFCode.S7F6, list, false);
                            }
                            GLog.LogSecsGems(ESecsGemsDir.HostToLocal, sfcode.ToString(), Log.TranslateText(ppid));
                            break;
                        }
                    case GemTaro.SECSII.SFCode.S7F17:
                        {
                            GemSystem.Secsii.DecodeBody(ref rawdata, out SList list);

                            var filelist = Directory.GetFiles(GDoc.RecipeDir.FullName);
                            foreach (var file in filelist)
                            {
                                if (list.Contains(Path.GetFileNameWithoutExtension(file))) File.Delete(file);
                            }

                            GemSystem.GemHost_Send(GemTaro.SECSII.SFCode.S7F18, (byte)0, false);
                            GLog.LogSecsGems(ESecsGemsDir.HostToLocal, sfcode.ToString(), Log.TranslateText(list));
                            break;
                        }
                    case GemTaro.SECSII.SFCode.S7F19:
                        {
                            var filelist = Directory.GetFiles(GDoc.RecipeDir.FullName).Cast<dynamic>().Select(x => Path.GetFileNameWithoutExtension(x) + ".xml").ToList();

                            GemSystem.GemHost_Send(GemTaro.SECSII.SFCode.S7F20, filelist, true);
                            GLog.LogSecsGems(ESecsGemsDir.HostToLocal, sfcode.ToString(), "");
                            break;
                        }
                    #endregion

                    #region S10
                    case GemTaro.SECSII.SFCode.S10F1:
                        {
                            SList newList = new SList();

                            GemSystem.GemHost_Send(GemTaro.SECSII.SFCode.S10F1, newList, false);
                            GLog.LogSecsGems(ESecsGemsDir.LocalToHost, sfcode.ToString());
                            break;
                        }
                    case GemTaro.SECSII.SFCode.S10F3:
                        {
                            try
                            {
                                GemSystem.Secsii.DecodeBody(ref rawdata, out SList list);

                                string msg = list[1];

                                frmMsgbox msgbox = new frmMsgbox(msg + "\n" + GemTaro.GExtension.Description(GemTaro.SECSII.SFCode.S10F3), MsgBoxBtns.OK);
                                GLog.LogSecsGems(ESecsGemsDir.HostToLocal, sfcode.ToString(), Log.TranslateText(list));
                            }
                            catch { }
                            GemSystem.GemHost_Send(GemTaro.SECSII.SFCode.S10F4, (byte)0, false);
                            break;
                        }
                    case GemTaro.SECSII.SFCode.S10F5:
                        {
                            try
                            {
                                GemSystem.Secsii.DecodeBody(ref rawdata, out SList list);

                                string msg = "";
                                foreach (var m in list[1])
                                {
                                    msg += m;
                                }

                                frmMsgbox msgbox = new frmMsgbox(msg + "\n" + GemTaro.GExtension.Description(GemTaro.SECSII.SFCode.S10F5), MsgBoxBtns.OK);

                                GemSystem.GemHost_Send(GemTaro.SECSII.SFCode.S10F6, (byte)0, false);
                                GLog.LogSecsGems(ESecsGemsDir.HostToLocal, sfcode.ToString());
                            }
                            catch { }
                            break;
                        }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, "Invalid Decision Making\n" + ex.Message);
                return;
            }
        }

        public static void SendMsg(GemTaro.SECSII.SFCode sfcode, string data)
        {
            if (!Connected) return;
            try
            {
                string[] splitdata = data.Split(',');
                switch (sfcode)
                {
                    case GemTaro.SECSII.SFCode.S5F1:
                        {
                            var list = new SList()
                            {
                                splitdata[0],
                                splitdata[1],
                                splitdata[2],
                            };

                            GemSystem.GemHost_Send(GemTaro.SECSII.SFCode.S5F1, list, true);
                            GLog.LogSecsGems(ESecsGemsDir.LocalToHost, sfcode.ToString());
                            break;
                        }
                    case GemTaro.SECSII.SFCode.S6F11:
                        {
                            var list = new SList()
                            {
                                splitdata[0],
                                splitdata[1],
                                new SList()
                                {
                                    new SList()
                                    {
                                        0,
                                        new SList()
                                        {
                                            splitdata[2],
                                        },
                                    },
                                },
                            };

                            GemSystem.GemHost_Send(GemTaro.SECSII.SFCode.S6F11, list, true);
                            GLog.LogSecsGems(ESecsGemsDir.LocalToHost, sfcode.ToString());
                            break;
                        }
                    case GemTaro.SECSII.SFCode.S10F1:
                        {
                            var list = new SList()
                            {
                                splitdata[0],
                                splitdata[1],
                            };

                            GemSystem.GemHost_Send(GemTaro.SECSII.SFCode.S10F1, list, true);
                            GLog.LogSecsGems(ESecsGemsDir.LocalToHost, sfcode.ToString());
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                GAlarm.Prompt(EAlarm.UNKNOWN_ERROR, "Invalid Message Sent - SecsGem" + ex.Message);
                return;
            }
        }

        public static SList SearchDataS1F3(dynamic data)
        {
            #region ExtractInfo
            SList slist = new SList();
            int index = 1;

            var members = new List<MemberInfo>();
            members = new Type[] { typeof(GProcessPara) }.SelectMany(x => x.GetAllMembers(BindingFlags.Static | BindingFlags.Public)).ToList();

            foreach (var m in members)
            {
                if (m.Name == data)
                {
                    object o = null;
                    #region GetObject
                    switch (m.MemberType)
                    {
                        case MemberTypes.Field:
                            {
                                var f = m as FieldInfo;
                                if (f.IsLiteral || f.IsInitOnly) break;
                                o = f.GetValue(null);
                                break;
                            }
                        case MemberTypes.Property:
                            {
                                var p = m as PropertyInfo;
                                if (!p.CanWrite || !p.CanRead) break;
                                o = p.GetValue(null);
                                break;
                            }
                    }
                    #endregion

                    string value = "";
                    switch (o)
                    {
                        case IPara ipara:
                            {
                                //value += ipara.Name;
                                value += $"{ipara.Value}{ipara.Unit}";
                                break;
                            }
                        case DPara dpara:
                            {
                                //value += dpara.Name;
                                value += $"{dpara.Value}{dpara.Unit}";
                                break;
                            }
                        case IPara[] iparas:
                            {
                                var ipara = iparas[0];
                                //value += ipara.Name;
                                value += $"{ipara.Value}{ipara.Unit}";
                                break;
                            }
                        case DPara[] dparas:
                            {
                                var dpara = dparas[0];
                                //value += dpara.Name;
                                value += $"{dpara.Value}{dpara.Unit}";
                                break;
                            }
                        case Enum evalue:
                            {
                                //value += o.GetType().Name;

                                //var desc = string.Join("; ", Enum.GetNames(o.GetType()));
                                //value += $"({desc})";
                                value += Enum.GetNames(o.GetType());
                                break;
                            }
                        case string svalue:
                        case byte btvalue:
                        case short shvalue:
                        case int ivalue:
                        case long lvalue:
                        case sbyte sbtvalue:
                        case ushort ushvalue:
                        case uint uivalue:
                        case ulong ulvalue:
                        case double dvalue:
                        case float fvalue:
                        case bool bvalue:
                        case char cvalue:
                        case decimal dcvalue:
                        case DateTime dtvalue:
                            {
                                //value += o.GetType().Name;
                                value += o.ToString();
                                break;
                            }

                        default: continue;
                    }
                    slist.Add(value);
                }
            }
            #endregion

            return slist;
        }
        public static SList SearchDataS1F5(dynamic data)
        {
            SList slist = new SList();
            //int index = 1;

            var members = new List<MemberInfo>();
            members = new Type[] { typeof(GProcessPara), typeof(GSetupPara), typeof(SP_Param), typeof(Vermes3280_Param), typeof(HM_Param), typeof(PneumaticJet_Param), typeof(Temp_Setup), typeof(PressureSetup) }.SelectMany(x => x.GetAllMembers(BindingFlags.Static | BindingFlags.Public)).ToList();

            foreach (var m in members)
            {
                if (m.Name == data)
                {
                    object o = null;
                    #region GetObject
                    switch (m.MemberType)
                    {
                        case MemberTypes.Field:
                            {
                                var f = m as FieldInfo;
                                if (f.IsLiteral || f.IsInitOnly) break;
                                o = f.GetValue(null);
                                break;
                            }
                        case MemberTypes.Property:
                            {
                                var p = m as PropertyInfo;
                                if (!p.CanWrite || !p.CanRead) break;
                                o = p.GetValue(null);
                                break;
                            }
                    }
                    #endregion

                    string value = "";
                    switch (o)
                    {
                        case IPara ipara:
                            {
                                value = $"{ipara.Unit}";
                                break;
                            }
                        case DPara dpara:
                            {
                                value = $"{dpara.Unit}";
                                break;
                            }
                        case IPara[] iparas:
                            {
                                var ipara = iparas[0];
                                value = $"{ipara.Unit}";
                                break;
                            }
                        case DPara[] dparas:
                            {
                                var dpara = dparas[0];
                                value = $"{dpara.Unit}";
                                break;
                            }
                        default: continue;
                    }
                    slist.Add(value);
                }
            }
            return slist;
        }
        public static SList SearchDataS1F11()
        {
            #region ExtractInfo
            SList slist = new SList();
            int index = 1;

            var members = new List<MemberInfo>();
            members = new Type[] { typeof(GProcessPara) }.SelectMany(x => x.GetAllMembers(BindingFlags.Static | BindingFlags.Public)).ToList();

            foreach (var m in members)
            {
                object o = null;
                #region GetObject
                switch (m.MemberType)
                {
                    case MemberTypes.Field:
                        {
                            var f = m as FieldInfo;
                            if (f.IsLiteral || f.IsInitOnly) break;
                            o = f.GetValue(null);
                            break;
                        }
                    case MemberTypes.Property:
                        {
                            var p = m as PropertyInfo;
                            if (!p.CanWrite || !p.CanRead) break;
                            o = p.GetValue(null);
                            break;
                        }
                }
                #endregion

                string value = "";
                switch (o)
                {
                    case IPara ipara:
                        {
                            value += $"{ipara.Unit}";
                            break;
                        }
                    case DPara dpara:
                        {
                            value += $"{dpara.Unit}";
                            break;
                        }
                    case IPara[] iparas:
                        {
                            var ipara = iparas[0];
                            value += $"{ipara.Unit}";
                            break;
                        }
                    case DPara[] dparas:
                        {
                            var dpara = dparas[0];
                            value += $"{dpara.Unit}";
                            break;
                        }
                    case Enum evalue:
                        {
                            value += Enum.GetNames(o.GetType());
                            break;
                        }
                    case string svalue:
                    case byte btvalue:
                    case short shvalue:
                    case int ivalue:
                    case long lvalue:
                    case sbyte sbtvalue:
                    case ushort ushvalue:
                    case uint uivalue:
                    case ulong ulvalue:
                    case double dvalue:
                    case float fvalue:
                    case bool bvalue:
                    case char cvalue:
                    case decimal dcvalue:
                    case DateTime dtvalue:
                        {
                            //value += o.GetType().Name;
                            value += o.GetType();
                            break;
                        }

                    default: continue;
                }

                var newList = new SList()
                    {
                        index,
                        m.DeclaringType.Name + m.Name,
                        value,
                    };
                slist.Add(newList);

            }
            #endregion

            return slist;
        }
        public static SList SearchDataS1F23()
        {
            #region ExtractInfo
            SList slist = new SList();
            int index = 1;

            var members = new List<MemberInfo>();
            members = new Type[] { typeof(EEvent) }.SelectMany(x => x.GetAllMembers(BindingFlags.Static | BindingFlags.Public)).ToList();

            foreach (var m in members)
            {
                object o = null;
                #region GetObject
                switch (m.MemberType)
                {
                    case MemberTypes.Field:
                        {
                            var f = m as FieldInfo;
                            if (f.IsLiteral || f.IsInitOnly) break;
                            o = f.GetValue(null);
                            break;
                        }
                    case MemberTypes.Property:
                        {
                            var p = m as PropertyInfo;
                            if (!p.CanWrite || !p.CanRead) break;
                            o = p.GetValue(null);
                            break;
                        }
                }
                #endregion

                string value = "";
                switch (o)
                {
                    case IPara ipara:
                        {
                            value += $"{ipara.Unit}";
                            break;
                        }
                    case DPara dpara:
                        {
                            value += $"{dpara.Unit}";
                            break;
                        }
                    case IPara[] iparas:
                        {
                            var ipara = iparas[0];
                            value += $"{ipara.Unit}";
                            break;
                        }
                    case DPara[] dparas:
                        {
                            var dpara = dparas[0];
                            value += $"{dpara.Unit}";
                            break;
                        }
                    case Enum evalue:
                        {
                            value += Enum.GetNames(o.GetType());
                            break;
                        }
                    case string svalue:
                    case byte btvalue:
                    case short shvalue:
                    case int ivalue:
                    case long lvalue:
                    case sbyte sbtvalue:
                    case ushort ushvalue:
                    case uint uivalue:
                    case ulong ulvalue:
                    case double dvalue:
                    case float fvalue:
                    case bool bvalue:
                    case char cvalue:
                    case decimal dcvalue:
                    case DateTime dtvalue:
                        {
                            //value += o.GetType().Name;
                            value += o.GetType();
                            break;
                        }

                    default: continue;
                }

                var newList = new SList()
                    {
                        index,
                        m.DeclaringType.Name + m.Name,
                        value,
                    };
                slist.Add(newList);

            }
            #endregion

            return slist;
        }
        public static SList SearchDataS2F13()
        {

            #region ExtractInfo
            SList slist = new SList();
            int index = 1;

            var members = new List<MemberInfo>();
            members = new Type[] { typeof(GSetupPara) }.SelectMany(x => x.GetAllMembers(BindingFlags.Static | BindingFlags.Public)).ToList();

            foreach (var m in members)
            {
                object o = null;
                #region GetObject
                switch (m.MemberType)
                {
                    case MemberTypes.Field:
                        {
                            var f = m as FieldInfo;
                            if (f.IsLiteral || f.IsInitOnly) break;
                            o = f.GetValue(null);
                            break;
                        }
                    case MemberTypes.Property:
                        {
                            var p = m as PropertyInfo;
                            if (!p.CanWrite || !p.CanRead) break;
                            o = p.GetValue(null);
                            break;
                        }
                }
                #endregion

                string value = "";
                switch (o)
                {
                    case IPara ipara:
                        {
                            value += ipara.Value.ToString();
                            value += $",{ipara.Unit}";
                            break;
                        }
                    case DPara dpara:
                        {
                            value += dpara.Value.ToString("f3");
                            value += $",{dpara.Unit}";
                            break;
                        }
                    case IPara[] iparas:
                        {
                            var ipara = iparas[0];
                            value += ipara.Value.ToString();
                            value += $",{ipara.Unit}";
                            break;
                        }
                    case DPara[] dparas:
                        {
                            var dpara = dparas[0];
                            value += dpara.Value.ToString("f3");
                            value += $",{dpara.Unit}";
                            break;
                        }
                    case Enum evalue:
                        {
                            value += o;
                            value += "," + o.GetType().Name;

                            var desc = string.Join("; ", Enum.GetNames(o.GetType()));
                            value += $"({desc})";
                            break;
                        }
                    case string svalue:
                    case byte btvalue:
                    case short shvalue:
                    case int ivalue:
                    case long lvalue:
                    case sbyte sbtvalue:
                    case ushort ushvalue:
                    case uint uivalue:
                    case ulong ulvalue:
                    case double dvalue:
                    case float fvalue:
                    case bool bvalue:
                    case char cvalue:
                    case decimal dcvalue:
                    case DateTime dtvalue:
                        {
                            value += o;
                            value += "," + o.GetType().Name;
                            break;
                        }

                    default: continue;
                }
                slist.Add($"{index++},{m.DeclaringType.Name + m.Name}," + value);
            }
            #endregion

            return slist;
        }
        public static SList SearchDataS2F23()
        {
            #region ExtractInfo
            SList slist = new SList();
            int index = 1;

            var members = new List<MemberInfo>();
            members = new Type[] { typeof(GSetupPara) }.SelectMany(x => x.GetAllMembers(BindingFlags.Static | BindingFlags.Public)).ToList();

            foreach (var m in members)
            {
                object o = null;
                #region GetObject
                switch (m.MemberType)
                {
                    case MemberTypes.Field:
                        {
                            var f = m as FieldInfo;
                            if (f.IsLiteral || f.IsInitOnly) break;
                            o = f.GetValue(null);
                            break;
                        }
                    case MemberTypes.Property:
                        {
                            var p = m as PropertyInfo;
                            if (!p.CanWrite || !p.CanRead) break;
                            o = p.GetValue(null);
                            break;
                        }
                }
                #endregion

                string value = "";
                switch (o)
                {
                    case IPara ipara:
                        {
                            value += ipara.Value.ToString();
                            value += $",{ipara.Unit}";
                            break;
                        }
                    case DPara dpara:
                        {
                            value += dpara.Value.ToString("f3");
                            value += $",{dpara.Unit}";
                            break;
                        }
                    case IPara[] iparas:
                        {
                            var ipara = iparas[0];
                            value += ipara.Value.ToString();
                            value += $",{ipara.Unit}";
                            break;
                        }
                    case DPara[] dparas:
                        {
                            var dpara = dparas[0];
                            value += dpara.Value.ToString("f3");
                            value += $",{dpara.Unit}";
                            break;
                        }
                    case Enum evalue:
                        {
                            value += o;
                            value += "," + o.GetType().Name;

                            var desc = string.Join("; ", Enum.GetNames(o.GetType()));
                            value += $"({desc})";
                            break;
                        }
                    case string svalue:
                    case byte btvalue:
                    case short shvalue:
                    case int ivalue:
                    case long lvalue:
                    case sbyte sbtvalue:
                    case ushort ushvalue:
                    case uint uivalue:
                    case ulong ulvalue:
                    case double dvalue:
                    case float fvalue:
                    case bool bvalue:
                    case char cvalue:
                    case decimal dcvalue:
                    case DateTime dtvalue:
                        {
                            value += o;
                            value += "," + o.GetType().Name;
                            break;
                        }

                    default: continue;
                }
                slist.Add($"{index++},{m.DeclaringType.Name + m.Name}," + value);
            }
            #endregion

            return slist;
        }
        public static SList SearchDataS5F5()
        {
            #region ExtractInfo
            SList slist = new SList();
            int index = 1;

            var members = new List<MemberInfo>();
            members = new Type[] { typeof(GAlarm) }.SelectMany(x => x.GetAllMembers(BindingFlags.Static | BindingFlags.Public)).ToList();

            foreach (var m in members)
            {
                object o = null;
                #region GetObject
                switch (m.MemberType)
                {
                    case MemberTypes.Field:
                        {
                            var f = m as FieldInfo;
                            if (f.IsLiteral || f.IsInitOnly) break;
                            o = f.GetValue(null);
                            break;
                        }
                    case MemberTypes.Property:
                        {
                            var p = m as PropertyInfo;
                            if (!p.CanWrite || !p.CanRead) break;
                            o = p.GetValue(null);
                            break;
                        }
                }
                #endregion

                string value = "";
                switch (o)
                {
                    case IPara ipara:
                        {
                            value += ipara.Value.ToString();
                            value += $",{ipara.Unit}";
                            break;
                        }
                    case DPara dpara:
                        {
                            value += dpara.Value.ToString("f3");
                            value += $",{dpara.Unit}";
                            break;
                        }
                    case IPara[] iparas:
                        {
                            var ipara = iparas[0];
                            value += ipara.Value.ToString();
                            value += $",{ipara.Unit}";
                            break;
                        }
                    case DPara[] dparas:
                        {
                            var dpara = dparas[0];
                            value += dpara.Value.ToString("f3");
                            value += $",{dpara.Unit}";
                            break;
                        }
                    case Enum evalue:
                        {
                            value += o;
                            value += "," + o.GetType().Name;

                            var desc = string.Join("; ", Enum.GetNames(o.GetType()));
                            value += $"({desc})";
                            break;
                        }
                    case string svalue:
                    case byte btvalue:
                    case short shvalue:
                    case int ivalue:
                    case long lvalue:
                    case sbyte sbtvalue:
                    case ushort ushvalue:
                    case uint uivalue:
                    case ulong ulvalue:
                    case double dvalue:
                    case float fvalue:
                    case bool bvalue:
                    case char cvalue:
                    case decimal dcvalue:
                    case DateTime dtvalue:
                        {
                            value += o;
                            value += "," + o.GetType().Name;
                            break;
                        }

                    default: continue;
                }
                slist.Add($"{index++},{m.DeclaringType.Name + m.Name}," + value);
            }
            #endregion

            return slist;
        }


        public static List<(string, double)> SearchSVData()
        {
            #region ExtractInfo
            List<(string, double)> list = new List<(string, double)>();
            int index = 1;

            var members = new List<MemberInfo>();
            members = new Type[] { typeof(GProcessPara) }.SelectMany(x => x.GetAllMembers(BindingFlags.Static | BindingFlags.Public)).ToList();

            foreach (var m in members)
            {
                object o = null;
                #region GetObject
                switch (m.MemberType)
                {
                    case MemberTypes.Field:
                        {
                            var f = m as FieldInfo;
                            if (f.IsLiteral || f.IsInitOnly) break;
                            o = f.GetValue(null);
                            break;
                        }
                    case MemberTypes.Property:
                        {
                            var p = m as PropertyInfo;
                            if (!p.CanWrite || !p.CanRead) break;
                            o = p.GetValue(null);
                            break;
                        }
                }
                #endregion

                switch (o)
                {
                    case IPara ipara:
                        {
                            list.Add((ipara.Name, ipara.Value));
                            break;
                        }
                    case DPara dpara:
                        {
                            list.Add((dpara.Name, dpara.Value));
                            break;
                        }
                    case IPara[] iparas:
                        {
                            list.Add((iparas[0].Name, iparas[0].Value));
                            break;
                        }
                    case DPara[] dparas:
                        {
                            list.Add((dparas[0].Name, dparas[0].Value));
                            break;
                        }
                    case Enum evalue:
                        {
                            list.Add((evalue.ToString(), 0));
                            break;
                        }
                    case string svalue:
                    case byte btvalue:
                    case short shvalue:
                    case int ivalue:
                    case long lvalue:
                    case sbyte sbtvalue:
                    case ushort ushvalue:
                    case uint uivalue:
                    case ulong ulvalue:
                    case double dvalue:
                    case float fvalue:
                    case bool bvalue:
                    case char cvalue:
                    case decimal dcvalue:
                    case DateTime dtvalue:
                        {
                            //list.Add((o.GetType().Name, o.GetType().));
                            break;
                        }

                    default: continue;
                }

            }
            #endregion

            return list;
        }
    }

    sealed class Log
    {
        public string DateTime = string.Empty;
        public string Description = string.Empty;
        public string Structure = string.Empty;
        public string Rawdata = string.Empty;
        public ESecsGemsDir Dir = ESecsGemsDir.HostToLocal;
        public Log(ESecsGemsDir Dir, string description, string structure)
        {
            DateTime = System.DateTime.Now.ToString("o");

            Description = Dir.ToString() + "\t" + description;
            Structure = structure;
        }

        protected static string TranslateText(List<dynamic> list, int count = 0)
        {
            string structure = string.Empty;
            string space = "    ";
            string expand = string.Empty;
            for (int i = 0; i < count; i++) { expand += space; }

            structure += expand + $"<List,{list.Count}>\r\n";

            foreach (var para in list)
            {
                if (para is List<dynamic>) structure += TranslateText(para, count + 1);
                else structure += $"{expand + space}{TranslateText(para)}";
            }
            structure += expand + $"</>\r\n";
            return structure;
        }
        internal static string TranslateText(List<dynamic> list)
        {
            return TranslateText(list, 0);
        }
        internal static string TranslateText(dynamic value)
        {
            string name = string.Empty;
            switch ((value).GetType().Name)
            {
                case nameof(String):
                    name = GemTaro.SECSII.ItemFormat.ASCII.ToString();
                    break;
                case nameof(Byte):
                    name = GemTaro.SECSII.ItemFormat.Bin.ToString();
                    break;
                case nameof(UInt16):
                    name = GemTaro.SECSII.ItemFormat.UI2.ToString();
                    break;
                case nameof(UInt32):
                    name = GemTaro.SECSII.ItemFormat.UI4.ToString();
                    break;
                case nameof(UInt64):
                    name = GemTaro.SECSII.ItemFormat.UI8.ToString();
                    break;
                case nameof(Int16):
                    name = GemTaro.SECSII.ItemFormat.I2.ToString();
                    break;
                case nameof(Int32):
                    name = GemTaro.SECSII.ItemFormat.I4.ToString();
                    break;
                case nameof(Int64):
                    name = GemTaro.SECSII.ItemFormat.I8.ToString();
                    break;
                case nameof(Single):
                    name = GemTaro.SECSII.ItemFormat.Float.ToString();
                    break;
                case nameof(Double):
                    name = GemTaro.SECSII.ItemFormat.Double.ToString();
                    break;
            }
            if (string.IsNullOrEmpty(name)) name = value.GetType().Name;
            return $"<{name}>{value}\r\n";
        }

        public override string ToString()
        {
            return DateTime + "\t" + (Description.Length > 200 ? Description.Substring(0, 200) + "..." : Description);
        }
    }

    class TFSecsEvent
    {
        public static List<TESecsEvent> EventPrompt = Enumerable.Range(0, Enum.GetValues(typeof(EEvent)).Length).Select(x => new TESecsEvent()).ToList();

        public static bool SaveFile(string filepath)
        {
            return GDoc.SaveINI(filepath, MethodBase.GetCurrentMethod().DeclaringType);
        }
        public static bool SaveFile()
        {
            return SaveFile(GDoc.SecsGemEventFile.FullName);
        }
        public static bool LoadFile(string filepath)
        {
            return GDoc.LoadINI(filepath, MethodBase.GetCurrentMethod().DeclaringType);
        }
        public static bool LoadFile()
        {
            return LoadFile(GDoc.SecsGemEventFile.FullName);
        }
    }

    public class TESecsEvent
    {
        public EEvent Event { get; set; } = EEvent.NONE;
        public bool Prompt { get; set; } = true;

        public TESecsEvent()
        {

        }
    }

    class TFTraceData
    {
        public static BindingList<TETraceData> TraceDatas = new BindingList<TETraceData>();
    }

    public class TETraceData
    {
        public int ID { get; set; } = 0;
        public TimeSpan DataSamplePeriod { get; set; } = new TimeSpan();
        public int TotalSamples { get; set; } = 0;
        public int ReportGroupSize { get; set; } = 0;
        public List<string> SVID { get; set; } = new List<string>();
        public System.Threading.Timer Timer { get; set; }
        public int CurrentSample { get; set; } = 0;

        public TETraceData()
        {

        }
        public TETraceData(TETraceData data)
        {
            this.ID = data.ID;
            this.DataSamplePeriod = data.DataSamplePeriod;
            this.TotalSamples = data.TotalSamples;
            this.ReportGroupSize = data.ReportGroupSize;
            this.SVID = data.SVID;
            this.Timer = data.Timer;
            this.CurrentSample = 0;
        }
    }
}
