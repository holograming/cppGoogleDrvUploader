using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using System.IO;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace BongSecurity
{
    static class Program
    {
        private static ILog log = LogManager.GetLogger("Program");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainFrame(args));
        }

        public static void AddLog(string msg)
        {
            log.Debug(msg);
        }
    }
}
