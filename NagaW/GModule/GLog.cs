using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace NagaW
{
    class GLog
    {
        public static string RecentHistory = string.Empty;

        static ReaderWriterLockSlim Slim = new ReaderWriterLockSlim();
        public static bool WriteLog(ELogType logType, string content, string filepath)
        {

            content = content.Replace("\r", "").Replace("\n", "");
            RecentHistory = $"{logType} => {content}";

            TELog log = new TELog(DateTime.Now, TFUser.CurrentUser.Name, logType, content);

            try
            {
                if (Slim.IsWriteLockHeld) Slim.ExitWriteLock();

                Slim.EnterWriteLock();
                var f = new FileStream(filepath, FileMode.Append, FileAccess.Write, FileShare.Write);
                using (StreamWriter w = new StreamWriter(f)) w.WriteLine(log.GenerateLog());

            }
            catch
            {
                return false;
            }
            finally
            {
                Slim.ExitWriteLock();
            }
            return true;
        }
        public static bool WriteLog(ELogType logType, string content)
        {
            return WriteLog(logType, content, GDoc.MachineLogFile.FullName);
        }

        public static bool ReadLog(string filename, out List<TELog> loglist)
        {
            loglist = new List<TELog>();

            try
            {
                using (StreamReader s = new StreamReader(filename))
                {
                    while (!s.EndOfStream)
                    {
                        var logs = s.ReadLine().Split('\t');
                        loglist.Add(new TELog(DateTime.Parse(logs[0]), logs[1], (ELogType)Enum.Parse(typeof(ELogType), logs[2]), logs[3]));
                    }
                };
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool WriteException(Exception ex)
        {
            return WriteLog(ELogType.EXCEP, ex.TargetSite.DeclaringType.Name + " " + ex.TargetSite.Name + ex.Message.ToString());
        }

        public static bool SetPara(ref IPara para)
        {
            IPara tempara = new IPara(para);

            frmParaEditor frm = new frmParaEditor(para.ToDPara);
            if (frm.ShowDialog() != DialogResult.OK) return false;

            para.Value = (int)frm.Value;
            string outofrange = para.IsOutofRange ? "<Out of range " + para.Min.ToString("f3") + "-" + para.Max.ToString("f3") + ">" : "";

            return WriteLog(ELogType.PARA, $"{para.Name} {tempara.ToStringForDisplay()} => {para.ToStringForDisplay()} {outofrange}");
        }
        public static bool SetPara(ref DPara para)
        {
            DPara tempara = new DPara(para);

            frmParaEditor frm = new frmParaEditor(para);
            if (frm.ShowDialog() != DialogResult.OK) return false;

            para.Value = Math.Round(frm.Value, para.DecimalPlace);
            string outofrange = para.IsOutofRange ? "<Out of range " + para.Min.ToString($"f{para.DecimalPlace}") + "-" + para.Max.ToString($"f{para.DecimalPlace}") + ">" : "";

            return WriteLog(ELogType.PARA, $"{para.Name} {tempara.ToStringForDisplay()} => {para.ToStringForDisplay()} {outofrange}");
        }
        public static bool SetPos(ref PointXYZ pointXYZ, PointXYZ newpointXYZ, string desc, bool prompt = true)
        {
            string oldpos = pointXYZ.ToStringForDisplay();
            string newpos = newpointXYZ.ToStringForDisplay();

            if (prompt)
            {
                if (MsgBox.ShowDialog($"Set {desc}\r\n\nOLD: {oldpos}\r\nNEW: {newpos}", MsgBoxBtns.OKCancel) != DialogResult.OK) return false;
            }

            pointXYZ.X = newpointXYZ.X;
            pointXYZ.Y = newpointXYZ.Y;
            pointXYZ.Z = newpointXYZ.Z;

            return WriteLog(ELogType.PARA, $"{desc} {oldpos} => {newpos}");
        }

        public static bool SetPointD(ref PointD pointd, PointD newpointd, string desc, bool prompt = true)
        {
            string oldpoint = pointd.ToStringForDisplay();
            string newpoint = newpointd.ToStringForDisplay();

            if (prompt)
            {
                if (MsgBox.ShowDialog($"Set {desc}\r\n\nOLD: {oldpoint}\r\nNEW: {newpoint}", MsgBoxBtns.OKCancel) != DialogResult.OK) return false;
            }

            pointd.X = newpointd.X;
            pointd.Y = newpointd.Y;

            return WriteLog(ELogType.PARA, $"{oldpoint} => { newpoint}");
        }

        public static bool LogProcess(string data)
        {
            return WriteLog(ELogType.PROCESS, data);
        }

        static ReaderWriterLockSlim Slim2 = new ReaderWriterLockSlim();
        public static bool WriteLog(string content, string filepath)
        {
            string line = $"{DateTime.Now:O}\t{content}";

            try
            {
                if (Slim2.IsWriteLockHeld) Slim2.ExitWriteLock();

                Slim2.EnterWriteLock();
                var f = new FileStream(filepath, FileMode.Append, FileAccess.Write, FileShare.Write);
                using (StreamWriter w = new StreamWriter(f)) w.WriteLine(line);

            }
            catch
            {
                return false;
            }
            finally
            {
                Slim2.ExitWriteLock();
            }
            return true;
        }
    }

    class TELog
    {
        public DateTime Time { get; private set; }
        public string UserName { get; private set; }
        public ELogType LogType { get; private set; }
        public string Message { get; private set; }
        public TELog(DateTime dateTime, string username, ELogType logType, string msg)
        {
            Time = dateTime;
            UserName = username;
            LogType = logType;
            Message = msg;
        }
        public string GenerateLog()
        {
            return $"{Time:O}\t{UserName}\t{LogType}\t{Message}";
        }
        public string GenerateLogForDisplay()
        {
            return $"{Time}\t{UserName}\t{LogType}\t{Message}";
        }
    }
}
