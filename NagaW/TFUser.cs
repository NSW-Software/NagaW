using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;
using System.ComponentModel;

namespace NagaW
{
    public static class TFUser
    {
        static TEUser NSW = new TEUser(Elevel.ADMIN, nameof(NSW), "659959", false);
        static TEUser DefaultAdmin = new TEUser(Elevel.ADMIN, nameof(Elevel.ADMIN), nameof(Elevel.ADMIN), false);

        internal static TEUser CurrentUser { get; set; } = new TEUser();
        public static bool Logged { get; private set; } = false;

        public static BindingList<TEUser> UserList = new BindingList<TEUser>() { new TEUser(DefaultAdmin) };

        public static void Add()
        {
            UserList.Add(new TEUser());
            UserList.ResetBindings();
        }
        public static void Remove(int index)
        {
            index = Math.Min(index, UserList.Count);
            UserList.RemoveAt(index);
            UserList.ResetBindings();
        }

        public static bool Login()
        {
            var frm = new frmLogin();
            if (frm.ShowDialog() != DialogResult.OK) return false;

            var frmuser = frm.LoginUser;
            if (frmuser.Level == Elevel.ADMIN && frmuser.Name == NSW.Name && frmuser.Password == NSW.Password) return LoginAsNSW();
            var user = UserList.ToList().Find(u => u.Level == frmuser.Level && u.Name == frmuser.Name && u.Password == frmuser.Password);

            if (user is null || frmuser.Name == string.Empty || frmuser.Password == string.Empty) { MsgBox.ShowDialog("User Not Found"); return Login(); }
            if (user.Locked) { MsgBox.ShowDialog($"{user} locked, contact admin for unlock"); return Login(); }
            CurrentUser = user;

            return Logged = true;
        }
        public static bool LoginAsNSW()
        {
            CurrentUser = NSW;
            return Logged = true;
        }
        public static bool Logout()
        {
            if (MsgBox.ShowDialog(CurrentUser.ToStringForDisplay() + "\r\nLogout?", MsgBoxBtns.OKCancel) != DialogResult.OK) return false;
            CurrentUser = new TEUser();
            Logged = false;
            return true;
        }


        public static bool SaveFile(string filepath)
        {
            return GDoc.SaveXML(filepath, MethodBase.GetCurrentMethod().DeclaringType);
        }
        public static bool SaveFile()
        {
            return SaveFile(GDoc.UserProfileFile.FullName);
        }
        public static bool LoadFile(string filepath)
        {
            return GDoc.LoadXML(filepath, MethodBase.GetCurrentMethod().DeclaringType);
        }
        public static bool LoadFile()
        {
            return LoadFile(GDoc.UserProfileFile.FullName);
        }
    }

    public class TEUser
    {
        [Description("User Access Level.")]
        [DisplayName("Access Level")]
        public Elevel Level { get; set; } = Elevel.TECHNICIAN;
        [Description("User Name.")]
        public string Name { get; set; } = string.Empty;
        [Description("User Password.")]
        public string Password { get; set; } = string.Empty;
        [Description("Prevent user from login\r\nLogin credentials remain saved")]
        [DisplayName("Login Block")]
        public bool Locked { get; set; } = false;
        public TEUser(Elevel lvl, string name, string password, bool locked)
        {
            Level = lvl;
            Name = name;
            Password = password;
            Locked = locked;
        }
        public TEUser()
        {
        }

        public TEUser(TEUser user)
        {
            Level = user.Level;
            Name = user.Name;
            Password = user.Password;
            Locked = user.Locked;
        }
        public string ToStringForDisplay()
        {
            if (string.IsNullOrEmpty(Name)) return "None";
            string @lock = Locked ? "*" : "";
            return $"{Level} | {Name}{@lock}";
        }
        public override string ToString()
        {
            return ToStringForDisplay();
        }
    }

}
