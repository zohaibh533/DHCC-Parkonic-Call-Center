using System;
using System.Threading;
using System.Windows.Forms;

namespace ParkonicCallCenter
{
    static class Program
    {
        private static Mutex mutex = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            const string appName = "ParkonicCallCenterApp";
            bool createdNew;
            mutex = new Mutex(true, appName, out createdNew);

            if (!createdNew)
            {
                string message = "There is already Parkonic Call Center application is running.\nPlease close that application before starting a new one.";
                MessageBox.Show(message, "Application is already running", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Environment.Exit(0);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new UI.frmLogin());
            }
        }
    }
}
