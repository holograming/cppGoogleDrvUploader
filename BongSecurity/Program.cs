using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace BongSecurity
{
    static class Program
    {

        [DllImport("kernel32.dll",
           EntryPoint = "GetStdHandle",
           SetLastError = true,
           CharSet = CharSet.Auto,
           CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetStdHandle(int nStdHandle);
        [DllImport("kernel32.dll",
            EntryPoint = "AllocConsole",
            SetLastError = true,
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        private static extern int AllocConsole();
        private const int STD_OUTPUT_HANDLE = -11;

        private static ILog log;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo("App.config"));
            log = LogManager.GetLogger("Logger");
            if (Convert.ToBoolean(AppConfiguration.GetAppConfig("DevMode")))
            {
                AllocConsole();
                IntPtr stdHandle = GetStdHandle(STD_OUTPUT_HANDLE);
                SafeFileHandle safeFileHandle = new SafeFileHandle(stdHandle, true);
                FileStream fileStream = new FileStream(safeFileHandle, FileAccess.Write);
                StreamWriter standardOutput = new StreamWriter(fileStream, System.Text.Encoding.Unicode);
                standardOutput.AutoFlush = true;
                Console.SetOut(standardOutput);
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainFrame(args));
        }

        public static void AddLog(string msg)
        {
            log.Info(msg);
            Console.WriteLine(msg);
        }
    }


}
