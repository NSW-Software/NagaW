using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NagaW
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Mutex mutex = new Mutex(true, Application.ProductName, out bool created);
            if (!created)
            {
                MessageBox.Show($"{Application.ProductName} is running");
                return;
            }
            mutex.WaitOne();

            TCSystem.StartUp();
            Application.Run(new frmMain());

            TCSystem.ShutDown();
            mutex.ReleaseMutex();
        }
    }
}
