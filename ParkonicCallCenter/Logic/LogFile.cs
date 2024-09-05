using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using log4net;
using log4net.Config;

namespace ParkonicCallCenter.Logic
{
    public static class LogFile
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LogFile));

        public static void UpdateLogFile(string Msg)
        {
            XmlConfigurator.Configure();
            log.Info(Msg);

            //await Task.Run(() =>
            //{
            //    try
            //    {
            //        string LogFileName = "ParkonicCallCenterLog.Log";
            //        string path = string.Format("{0}\\{1}", Application.StartupPath, LogFileName);
            //        int RemoveLines = 10000;

            //        FileInfo txtFile = new FileInfo(path);
            //        if (!txtFile.Exists)
            //            txtFile.Create();

            //        using (StreamWriter sw = txtFile.AppendText())
            //        {
            //            sw.Write(string.Format("\n{0}\t{1}", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff"), Msg));
            //        }

            //        if ((txtFile.Length / 1024.0 / 1024.0) > 5.0)
            //        {
            //            List<string> txtRead = File.ReadAllLines(path).ToList();
            //            if (txtRead.Count > RemoveLines)
            //                txtRead.RemoveRange(0, RemoveLines);

            //            File.WriteAllLines(path, txtRead);
            //        }
            //    }
            //    catch (Exception ee)
            //    {
            //        //MessageBox.Show(string.Format("Error : {0}", ee.Message));
            //    }
            //});
        }

    }
}
