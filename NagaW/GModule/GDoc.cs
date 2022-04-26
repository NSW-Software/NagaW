using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Xml;
using System.Drawing;
using System.Collections;
using System.Threading;
using Emgu.CV;

namespace NagaW
{
    public class GDoc
    {
        //Dir
        public static DirectoryInfo RootDir => Directory.CreateDirectory("C:\\NagaW\\");
        public static DirectoryInfo RecipeDir => Directory.CreateDirectory(RootDir.FullName + "Recipe\\");
        public static DirectoryInfo SettingDir => Directory.CreateDirectory(RootDir.FullName + "Setting\\");
        public static DirectoryInfo MiscDir => Directory.CreateDirectory(RootDir.FullName + "Misc\\");
        public static DirectoryInfo LanguageDir => Directory.CreateDirectory(RootDir.FullName + "Language\\");
        public static DirectoryInfo AlarmHelpDir => Directory.CreateDirectory(RootDir.FullName + "AlarmHelp\\");
        public static DirectoryInfo UserDir => Directory.CreateDirectory(RootDir.FullName + "User\\");
        public static DirectoryInfo DisplayCtrlDir => Directory.CreateDirectory(RootDir.FullName + "Display\\");

        static DirectoryInfo rootLogDir => Directory.CreateDirectory(RootDir.FullName + "Log\\");
        public static DirectoryInfo MachineLogDir => Directory.CreateDirectory(rootLogDir.FullName + "MachineLog\\");
        public static DirectoryInfo WeighDataLogDir => Directory.CreateDirectory(rootLogDir.FullName + "WeighDataLog\\");
        public static DirectoryInfo MotionLogDir => Directory.CreateDirectory(rootLogDir.FullName + "MotionLog\\");

        //File
        public static FileInfo ToolFile => new FileInfo(SettingDir.FullName + "Tool.ini");
        public static FileInfo ConfigFile => new FileInfo(SettingDir.FullName + "Config.ini");
        public static FileInfo SetupParaFile => new FileInfo(SettingDir.FullName + "SetupPara.ini");
        public static FileInfo ProcessParaFile => new FileInfo(SettingDir.FullName + "ProcessPara.ini");
        public static FileInfo ExtraParaFile => new FileInfo(SettingDir.FullName + "ExtraPara.ini");

        public static FileInfo DIOFile => new FileInfo(SettingDir.FullName + "DIO.ini");

        public static FileInfo UserProfileFile => new FileInfo(UserDir.FullName + "UserProfile.xml");
        public static FileInfo MiscFile => new FileInfo(MiscDir.FullName + "Misc.ini");

        public static FileInfo MachineLogFile => new FileInfo(MachineLogDir.FullName + DateTime.Now.ToString("yyyy-MM-dd") + ".mlog");
        public static FileInfo WeighDataFile => new FileInfo(WeighDataLogDir.FullName + DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
        public static FileInfo MotionLogFile => new FileInfo(MotionLogDir.FullName + DateTime.Now.ToString("yyyy-MM-dd") + ".txt");

        public static string RecipeNameWithPath = string.Empty;
        public static string Extension_Converter(string filetype)
        {
            return $"{filetype}|*.{filetype}";
        }
        public static string mLog_ext => Extension_Converter("mlog");
        public static string xml_ext => Extension_Converter("xml");
        public static string ini_ext => Extension_Converter("ini");
        public static string txt_ext => Extension_Converter("txt");

        const BindingFlags Flags = BindingFlags.Public | BindingFlags.Static;

        readonly static Type[] DefinedTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsVisible && !t.IsEnum && t.BaseType != typeof(Form)).ToArray();

        public static bool SaveXML(string filename, params Type[] types)
        {
            try
            {
                #region Initialize
                using (XmlWriter writer = XmlWriter.Create(filename, new XmlWriterSettings() { Indent = true }))
                {
                    writer.WriteStartDocument();
                    writer.WriteComment(newVerFlag);
                    writer.WriteComment($"Writed {DateTime.Now}");
                    writer.WriteComment(filename);
                    writer.WriteComment(TFUser.CurrentUser.ToStringForDisplay());
                    writer.WriteStartElement("root");
                    foreach (Type type in types)
                    {
                        if (type.IsEnum) continue;
                        if (!assign(type, writer))
                            throw new Exception($"Fail to Save para of Type {type}");
                    }
                    writer.WriteEndElement();
                }
                #endregion

                #region Assign
                bool assign(Type type, XmlWriter writer)
                {
                    writer.WriteStartElement("Section");
                    writer.WriteAttributeString("Name", type.Name);
                    foreach (var member in type.GetMembers(Flags))
                    {
                        switch (member.MemberType)
                        {
                            case MemberTypes.Field:
                                {
                                    var f = member as FieldInfo;
                                    if (f.IsLiteral || f.IsInitOnly) break;
                                    encode(writer, f.GetValue(null), member.Name);
                                }
                                break;
                            case MemberTypes.Property:
                                {
                                    var p = member as PropertyInfo;
                                    if (!p.CanWrite || !p.CanRead) break;
                                    encode(writer, p.GetValue(null), member.Name);
                                }
                                break;
                            case MemberTypes.NestedType:
                                if (!assign(member as Type, writer)) return false;
                                break;
                        }
                    }
                    writer.WriteEndElement();
                    return true;
                }
                #endregion

                #region Encode
                bool encode(XmlWriter writer, object obj, string name)
                {
                    void StartElement()
                    {
                        writer.WriteStartElement("Key");
                        writer.WriteAttributeString("nameof", name);
                    }
                    switch (obj)
                    {
                        default:
                            #region
                            {
                                if (DefinedTypes.Contains(obj.GetType()))
                                {
                                    StartElement();
                                    foreach (PropertyDescriptor p in TypeDescriptor.GetProperties(obj))
                                    {
                                        if (!p.IsReadOnly) encode(writer, p.GetValue(obj), p.Name);
                                    }
                                    writer.WriteEndElement();
                                }
                                break;
                            }
                        #endregion
                        case Emgu.CV.Image<Emgu.CV.Structure.Gray, byte> img:
                            #region
                            {
                                try
                                {
                                    StartElement();

                                    byte[] bytes = (byte[])new ImageConverter().ConvertTo(img.ToBitmap(), typeof(byte[]));
                                    string data = Convert.ToBase64String(bytes);
                                    writer.WriteString(data);
                                }
                                catch { }
                                finally
                                {
                                    writer.WriteEndElement();
                                }
                                break;
                            }
                        #endregion
                        case Array array:
                            #region
                            {
                                StartElement();
                                switch (array.Rank)
                                {
                                    case 1:
                                        {
                                            writer.WriteAttributeString("length", array.Length.ToString());
                                            for (int m0 = 0; m0 < array.GetLength(0); m0++)
                                            {
                                                encode(writer, array.GetValue(m0), $"{name}[{m0}]");
                                            }
                                            break;
                                        }

                                    case 2:
                                        {
                                            writer.WriteAttributeString("length", $"[{array.GetLength(0)},{array.GetLength(1)}]");
                                            {
                                                for (int m0 = 0; m0 < array.GetLength(0); m0++)
                                                {
                                                    for (int m1 = 0; m1 < array.GetLength(1); m1++)
                                                    {
                                                        encode(writer, array.GetValue(m0, m1), $"{name}[{m0},{m1}]");
                                                    }
                                                }
                                            }
                                            break;
                                        }
                                }
                                writer.WriteEndElement();
                                break;
                            }
                        #endregion
                        case IList list:
                            #region
                            {
                                StartElement();
                                writer.WriteAttributeString("count", list.Count.ToString());
                                for (int i = 0; i < list.Count; i++)
                                {
                                    encode(writer, list[i], $"{name}[{i}]");
                                }
                                writer.WriteEndElement();
                                break;
                            }
                        #endregion
                        case Color color:
                            #region
                            {
                                StartElement();
                                writer.WriteString(new ColorConverter().ConvertToString(color));
                                writer.WriteEndElement();
                                break;
                            }
                        #endregion
                        case Rectangle rec:
                            #region
                            {
                                StartElement();
                                writer.WriteString(new RectangleConverter().ConvertToString(rec));
                                writer.WriteEndElement();
                                break;
                            }
                        #endregion
                        case Point pt:
                            #region
                            {
                                StartElement();
                                writer.WriteString(new PointConverter().ConvertToString(pt));
                                writer.WriteEndElement();
                                break;
                            }
                        #endregion
                        case Size sz:
                            #region
                            {
                                StartElement();
                                writer.WriteString(new SizeConverter().ConvertToString(sz));
                                writer.WriteEndElement();
                                break;
                            }
                        #endregion
                        case SizeF szf:
                            #region
                            {
                                StartElement();
                                writer.WriteString(new SizeFConverter().ConvertToString(szf));
                                writer.WriteEndElement();
                                break;
                            }
                        #endregion
                        case TimeSpan ts:
                            #region
                            {
                                StartElement();
                                writer.WriteString(new TimeSpanConverter().ConvertToString(ts));
                                writer.WriteEndElement();
                                break;
                            }
                        #endregion
                        case Enum evalue:
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
                            #region
                            {
                                StartElement();
                                writer.WriteString(obj.ToString());
                                writer.WriteEndElement();
                                break;
                            }
                            #endregion
                    }
                    return true;
                }
                #endregion
            }
            catch (Exception ex)
            {
                GAlarm.Prompt(EAlarm.SAVE_FILE_ERROR, filename + "\r\n" + ex.Message);
                return false;
            }
            return true;
        }
        public static bool LoadXML(string filename, params Type[] types)
        {
            bool newVer = false;

            try
            {
                #region Initialize
                using (XmlReader reader = XmlReader.Create(filename))
                {
                    while (reader.Read())
                    {
                        bool brk = false;
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.XmlDeclaration:
                            case XmlNodeType.Whitespace:
                            case XmlNodeType.Comment:
                                {
                                    string svalue = reader.Value;
                                    //newVer2 = newVer2 ? newVer2 : svalue is newVerFlag2;
                                    newVer = brk = svalue is newVerFlag;

                                    break;
                                }
                            default: brk = true; break;
                        }
                        if (brk) break;
                    }

                    foreach (Type type in types)
                    {
                        if (type.IsEnum) continue;
                        if (!assign(type, reader))
                            throw new Exception($"Fail to Load para of Type {type}");
                    }
                }
                #endregion

                #region Assign
                bool assign(Type type, XmlReader reader)
                {
                    if (!reader.ReadToFollowing("Section")) return false;
                    if (reader.GetAttribute("Name") != type.Name) return false;

                    foreach (var member in type.GetMembers(Flags))
                    {
                        switch (member.MemberType)
                        {
                            case MemberTypes.Field:
                                {
                                    var f = member as FieldInfo;
                                    if (f.IsLiteral || f.IsInitOnly) break;
                                    if (!decode(reader, f.GetValue(null), member.Name, out object obj)) return false;
                                    f.SetValue(null, obj);
                                }
                                break;
                            case MemberTypes.Property:
                                {
                                    var p = member as PropertyInfo;
                                    if (!p.CanWrite || !p.CanRead) break;
                                    if (!decode(reader, p.GetValue(null), member.Name, out object obj)) return false;
                                    p.SetValue(null, obj);
                                }
                                break;
                            case MemberTypes.NestedType:
                                if (!assign(member as Type, reader)) return false;
                                break;
                        }
                    }
                    return true;
                }
                #endregion

                #region Decode

                bool decode(XmlReader reader, object obj, string name, out object newobject)
                {
                    newobject = obj;

                    bool ReadElement()
                    {
                        if (!reader.ReadToFollowing("Key")) throw new Exception($"ReadToFollowing Fail");
                        string readattr = string.Empty;
                        if ((readattr = reader.GetAttribute("nameof")) != name) throw new Exception($"GetAttribute Fail Param> Read:{readattr} Name:{name}");

                        return true;
                    }

                    switch (obj)
                    {
                        default:
                            #region
                            {
                                if (DefinedTypes.Contains(obj.GetType()))
                                {
                                    if (!ReadElement()) return false;

                                    foreach (PropertyDescriptor p in TypeDescriptor.GetProperties(obj))
                                    {

                                        if (!p.IsReadOnly)
                                        {
                                            if ((p.Description is newVerFlag && !newVer) /*|| (p.Description is newVerFlag2 && !newVer2)*/)
                                            {
                                                p.SetValue(obj, p.GetValue(obj));
                                            }
                                            else
                                            {
                                                if (decode(reader, p.GetValue(obj), p.Name, out object newobj)) p.SetValue(obj, newobj);
                                            }
                                        }
                                    }
                                }
                                break;
                            }
                        #endregion
                        case Emgu.CV.Image<Emgu.CV.Structure.Gray, byte> img:
                            #region
                            {
                                if (!ReadElement()) return false;

                                byte[] bytes = Convert.FromBase64String(reader.ReadElementContentAsString());
                                var bitmap = new Bitmap(new MemoryStream(bytes));
                                try
                                {
                                    string mapfile = MiscDir.FullName + "tempImg.bmp";
                                    bitmap.Save(mapfile);
                                    newobject = new Image<Emgu.CV.Structure.Gray, byte>(mapfile);
                                }
                                finally
                                {
                                    bitmap.Dispose();
                                }
                                break;
                            }
                        #endregion
                        case Array array:
                            #region
                            {
                                if (!ReadElement()) return false;
                                switch (array.Rank)
                                {
                                    case 1:
                                        {
                                            for (int i = 0; i < array.GetLength(0); i++)
                                            {
                                                decode(reader, array.GetValue(i), $"{name}[{i}]", out object o);
                                                array.SetValue(o, i);
                                            }
                                        }
                                        break;
                                    case 2:
                                        {
                                            var length = reader.GetAttribute("length").Replace("[", "").Replace("]", "").Split(',');

                                            int l1 = int.Parse(length[0]);
                                            int l2 = int.Parse(length[1]);

                                            array = Array.CreateInstance(array.GetType().GetElementType(), l1, l2);
                                            for (int m0 = 0; m0 < array.GetLength(0); m0++)
                                            {
                                                for (int m1 = 0; m1 < array.GetLength(1); m1++)
                                                {
                                                    decode(reader, array.GetValue(m0, m1), $"{name}[{m0},{m1}]", out object o);
                                                    array.SetValue(o, m0, m1);
                                                }
                                            }

                                            newobject = array;
                                        }
                                        break;
                                }
                                break;
                            }
                        #endregion
                        case IList list:
                            #region
                            {
                                if (!ReadElement()) return false;
                                list.Clear();

                                int count = int.Parse(reader.GetAttribute("count"));
                                Type type = list.GetType().GetGenericArguments()[0];

                                for (int i = 0; i < count; i++)
                                {
                                    decode(reader, Activator.CreateInstance(type), $"{name}[{i}]", out object newo);
                                    list.Add(newo);
                                }
                                break;
                            }
                        #endregion
                        case Color color:
                            #region
                            {
                                if (!ReadElement()) return false;
                                newobject = new ColorConverter().ConvertFromString(reader.ReadElementContentAsString());
                                break;
                            }
                        #endregion
                        case Rectangle rec:
                            #region
                            {
                                if (!ReadElement()) return false;
                                newobject = new RectangleConverter().ConvertFromString(reader.ReadElementContentAsString());
                                break;
                            }
                        #endregion
                        case Point pt:
                            #region
                            {
                                if (!ReadElement()) return false;
                                newobject = new PointConverter().ConvertFromString(reader.ReadElementContentAsString());
                                break;
                            }
                        #endregion
                        case Size sz:
                            #region
                            {
                                if (!ReadElement()) return false;
                                newobject = new SizeConverter().ConvertFromString(reader.ReadElementContentAsString());
                                break;
                            }
                        #endregion
                        case SizeF szf:
                            #region
                            {
                                if (!ReadElement()) return false;
                                newobject = new SizeFConverter().ConvertFromString(reader.ReadElementContentAsString());
                                break;
                            }
                        #endregion
                        case TimeSpan ts:
                            #region
                            {
                                if (!ReadElement()) return false;
                                newobject = new TimeSpanConverter().ConvertFromString(reader.ReadElementContentAsString());
                                break;
                            }
                        #endregion
                        case Enum evalue:
                            #region
                            {
                                if (!ReadElement()) return false;
                                try
                                {
                                    newobject = Enum.Parse(evalue.GetType(), reader.ReadElementContentAsString());
                                }
                                catch
                                {   
                                    newobject = Enum.ToObject(evalue.GetType(), 0);
                                }
                                break;
                            }
                        #endregion
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
                            #region
                            {
                                if (!ReadElement()) return false;
                                newobject = Convert.ChangeType(reader.ReadElementContentAsString(), obj.GetType());
                                break;
                            }
                            #endregion
                    }

                    return true;
                }
                #endregion
            }
            catch (Exception ex)
            {
                GAlarm.Prompt(EAlarm.LOAD_FILE_ERROR, filename + "\r\n" + ex.Message);
                return false;
            }
            return true;
        }


        public static bool SaveINI(string filename, params Type[] types)
        {
            try
            {
                #region Initialize
                IniFile iniFile = new IniFile(filename);
                foreach (Type type in types)
                {
                    if (type.IsEnum) continue;
                    if (!assign(type, iniFile))
                        throw new Exception($"Fail to Save para of Type {type}");
                }
                #endregion

                #region Assign
                bool assign(Type type, IniFile ini)
                {
                    foreach (var member in type.GetMembers(Flags))
                    {
                        switch (member.MemberType)
                        {
                            case MemberTypes.Field:
                                {
                                    var f = member as FieldInfo;
                                    if (f.IsLiteral || f.IsInitOnly) break;
                                    encode(ini, f.GetValue(null), type.Name, member.Name);
                                }
                                break;
                            case MemberTypes.Property:
                                {
                                    var p = member as PropertyInfo;
                                    if (!p.CanWrite || !p.CanRead) break;
                                    encode(ini, p.GetValue(null), type.Name, member.Name);
                                }
                                break;
                            case MemberTypes.NestedType:
                                if (!assign(member as Type, ini)) return false;
                                break;
                        }
                    }
                    return true;
                }
                #endregion

                #region Encode
                bool encode(IniFile ini, object obj, string section, string key)
                {
                    switch (obj)
                    {
                        default:
                            #region
                            {
                                if (DefinedTypes.Contains(obj.GetType()))
                                {
                                    foreach (PropertyDescriptor p in TypeDescriptor.GetProperties(obj))
                                    {
                                        if (!p.IsReadOnly) encode(ini, p.GetValue(obj), section, key + p.Name);
                                    }
                                }
                                break;
                            }
                        #endregion
                        case Array array:
                            #region
                            {
                                switch (array.Rank)
                                {
                                    case 1:
                                        {
                                            for (int m0 = 0; m0 < array.GetLength(0); m0++)
                                            {
                                                encode(ini, array.GetValue(m0), section, $"{key}[{m0}]");
                                            }
                                            break;
                                        }


                                    case 2:
                                        {
                                            for (int m0 = 0; m0 < array.GetLength(0); m0++)
                                            {
                                                for (int m1 = 0; m1 < array.GetLength(1); m1++)
                                                {
                                                    encode(ini, array.GetValue(m0, m1), section, $"{key}[{m0},{m1}]");
                                                }
                                            }
                                            break;
                                        }
                                }

                                break;
                            }
                        #endregion
                        case IList list:
                            #region
                            {
                                for (int i = 0; i < list.Count; i++)
                                {
                                    encode(ini, list[i], section, $"{key}[{i}]");
                                }
                                break;
                            }
                        #endregion
                        case Color color:
                            #region
                            {
                                ini.Write(section, key, new ColorConverter().ConvertToString(color));
                                break;
                            }
                        #endregion
                        case Enum evalue:
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
                            #region
                            {
                                ini.Write(section, key, obj.ToString());
                                break;
                            }
                            #endregion
                    }

                    return true;
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }
        public static bool LoadINI(string filename, params Type[] types)
        {
            try
            {
                #region Initialize
                IniFile iniFile = new IniFile(filename);
                foreach (Type type in types)
                {
                    if (type.IsEnum) continue;
                    if (!assign(type, iniFile))
                        throw new Exception($"Fail to Save para of Type {type}");
                }
                #endregion

                #region Assign
                bool assign(Type type, IniFile ini)
                {
                    foreach (var member in type.GetMembers(Flags))
                    {
                        switch (member.MemberType)
                        {
                            case MemberTypes.Field:
                                {
                                    var f = member as FieldInfo;
                                    if (f.IsLiteral || f.IsInitOnly) break;
                                    if (!decode(ini, f.GetValue(null), type.Name, member.Name, out object obj)) return false;
                                    f.SetValue(null, obj);
                                }
                                break;
                            case MemberTypes.Property:
                                {
                                    var p = member as PropertyInfo;
                                    if (!p.CanWrite || !p.CanRead) break;
                                    if (!decode(ini, p.GetValue(null), type.Name, member.Name, out object obj)) return false;
                                    p.SetValue(null, obj);
                                }
                                break;
                            case MemberTypes.NestedType:
                                if (!assign(member as Type, ini)) return false;
                                break;
                        }
                    }
                    return true;
                }
                #endregion

                #region Decode
                bool decode(IniFile ini, object obj, string section, string key, out object newobject)
                {
                    newobject = obj;
                    string value = string.Empty;

                    switch (obj)
                    {
                        default:
                            #region
                            {
                                if (DefinedTypes.Contains(obj.GetType()))
                                {
                                    foreach (PropertyDescriptor p in TypeDescriptor.GetProperties(obj))
                                    {
                                        if (!p.IsReadOnly)
                                        {
                                            if (decode(ini, p.GetValue(obj), section, key + p.Name, out object newobj)) p.SetValue(obj, newobj);
                                        }
                                    }
                                }
                                break;
                            }
                        #endregion
                        case Array array:
                            #region
                            {
                                switch (array.Rank)
                                {
                                    case 1:
                                        {
                                            for (int i = 0; i < array.GetLength(0); i++)
                                            {
                                                decode(ini, array.GetValue(i), section, $"{key}[{i}]", out object o);
                                                array.SetValue(o, i);
                                            }
                                            break;
                                        }
                                    case 2:
                                        {
                                            for (int m0 = 0; m0 < array.GetLength(0); m0++)
                                            {
                                                for (int m1 = 0; m1 < array.GetLength(1); m1++)
                                                {
                                                    decode(ini, array.GetValue(m0, m1), section, $"{key}[{m0},{m1}]", out object o);
                                                    array.SetValue(o, m0, m1);
                                                }
                                            }
                                            break;
                                        }
                                }
                                break;
                            }
                        #endregion
                        case IList list:
                            #region
                            {
                                int count = list.Count;
                                //list.Clear();

                                Type type = list.GetType().GetGenericArguments()[0];

                                for (int i = 0; i < count; i++)
                                {
                                    decode(ini, list[i]/*Activator.CreateInstance(type)*/, section, $"{key}[{i}]", out object newo);
                                    //list.Add(newo);
                                    list[i] = newo;
                                }
                                break;
                            }
                        #endregion
                        case Color color:
                            #region
                            {
                                if (!ini.ReadString(section, key, ref value)) break;
                                newobject = new ColorConverter().ConvertFromString(value);
                                break;
                            }
                        #endregion
                        case Enum evalue:
                            #region
                            {
                                try
                                {
                                    if (!ini.ReadString(section, key, ref value)) break;
                                    newobject = Enum.Parse(evalue.GetType(), value);
                                }
                                catch
                                {
                                    newobject = Enum.ToObject(evalue.GetType(), 0);
                                }
                                break;
                            }
                        #endregion
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
                            #region
                            {
                                if (!ini.ReadString(section, key, ref value)) break;
                                newobject = Convert.ChangeType(value, obj.GetType());
                                break;
                            }
                            #endregion
                    }

                    return true;
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        public const string newVerFlag = "newVer";
    }


    public class GAuto
    {
        public static DateTime LastStartTime = DateTime.Now;
        public static DateTime LastEndTime = DateTime.Now;
        public static DateTime TotalStopTime;
        public static TimeSpan TotalProcudctionTime;

        public static bool run = false;

        public static TimeSpan ElapsedTime
        {
            get
            {
                TimeSpan time;
                if (run) time = (DateTime.Now - LastStartTime);
                else time = new TimeSpan();
                return time;
            }
        }

        public static int UPH
        {
            get
            {
                //if (!run) return 0;
                //else return InstBoard.ClusterCR.X * InstBoard.ClusterCR.Y * InstBoard.UnitCR.X * InstBoard.UnitCR.Y;
                return 0;
            }
        }

        public static void Start()
        {
            if (!run)
            {
                run = true;
                LastStartTime = DateTime.Now;
            }
        }
        public static void Stop()
        {
            if (run)
            {
                run = false;
                LastEndTime = DateTime.Now;
                TotalProcudctionTime += DateTime.Now - LastStartTime;
                
                if (TFConv.cToken != null)
                {
                    TFConv.cToken.Cancel();
                }
            }
        }

        public static void Reset()
        {
            LastStartTime = DateTime.Now;
            LastEndTime = DateTime.Now;
            TotalProcudctionTime = new TimeSpan();
        }
    }

    #region IniFile
    public class IniFile
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string variable, string filepath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filepath);

        private string Filepath;
        private char ArraySeperator = ',';
        private char StringArraySeperator = '@';

        public IniFile(string filepath)
        {
            try
            {
                Filepath = filepath;
                string folderpath = Path.GetDirectoryName(Filepath);
                if (!Directory.Exists(folderpath)) Directory.CreateDirectory(folderpath);
            }
            catch
            {
            }

        }

        public void Write(string section, string key, string variable)
        {
            WritePrivateProfileString(section, key, variable, Filepath);
        }
        public void Write(string section, string key, string[] variable)
        {
            string value = string.Join(StringArraySeperator.ToString(), variable);
            Write(section, key, value);
        }
        public void Write(string section, string key, bool variable)
        {
            Write(section, key, variable ? "1" : "0");
        }
        public void Write(string section, string key, bool[] variable)
        {
            string value = string.Join(ArraySeperator.ToString(), variable.Select(x => x ? "1" : "0").ToArray());
            Write(section, key, value);
        }
        public void Write(string section, string key, int variable)
        {
            Write(section, key, variable.ToString());
        }
        public void Write(string section, string key, int[] variable)
        {
            string value = string.Join(ArraySeperator.ToString(), variable.Select(p => p.ToString()).ToArray());
            Write(section, key, value);
        }
        public void Write(string section, string key, double variable)
        {
            Write(section, key, variable.ToString());
        }
        public void Write(string section, string key, double[] variable)
        {
            string value = string.Join(ArraySeperator.ToString(), variable.Select(p => p.ToString()).ToArray());
            Write(section, key, value);
        }


        public bool ReadString(string section, string key, ref string value)
        {
            StringBuilder sb = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, "", sb, 255, Filepath);

            if (i == 0) return false;
            value = sb.ToString();

            return true;
        }
        public bool ReadStringArray(string section, string key, ref string[] value)
        {
            StringBuilder sb = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, "", sb, 255, Filepath);
            if (i == 0) return false;
            try
            {
                value = sb.ToString().Split(StringArraySeperator);
            }
            catch { return false; }

            return true;
        }
        public bool ReadBool(string section, string key, ref bool value)
        {
            StringBuilder sb = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, "", sb, 255, Filepath);

            if (i == 0) return false;
            uint v;
            if (!uint.TryParse(sb.ToString(), out v)) return false;
            value = v > 0;
            return true;
        }
        public bool ReadBoolArray(string section, string key, ref bool[] value)
        {
            StringBuilder sb = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, "", sb, 255, Filepath);
            if (i == 0) return false;
            try
            {
                value = sb.ToString().Split(ArraySeperator).Select(x => x != "0").ToArray();
            }
            catch { return false; }

            return true;
        }
        public bool ReadInt(string section, string key, ref int value)
        {
            StringBuilder sb = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, "", sb, 255, Filepath);

            if (i == 0) return false;
            if (!int.TryParse(sb.ToString(), out value)) return false;

            return true;
        }
        public bool ReadIntArray(string section, string key, ref int[] value)
        {
            StringBuilder sb = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, "", sb, 255, Filepath);
            if (i == 0) return false;
            try
            {
                value = Array.ConvertAll(sb.ToString().Split(ArraySeperator), int.Parse);
            }
            catch { return false; }

            return true;
        }
        public bool ReadDouble(string section, string key, ref double value)
        {
            StringBuilder sb = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, "", sb, 255, Filepath);

            if (i == 0) return false;
            if (!double.TryParse(sb.ToString(), out value)) return false;

            return true;
        }
        public bool ReadDoubleArray(string section, string key, ref double[] value)
        {
            StringBuilder sb = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, "", sb, 255, Filepath);
            if (i == 0) return false;
            try
            {
                value = Array.ConvertAll(sb.ToString().Split(ArraySeperator), double.Parse);
            }
            catch { return false; }

            return true;
        }
    }
    #endregion
}
