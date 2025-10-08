using ServiceStack.Text;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace AccountsPro.SubProjects.AccountsLibraryFramework
{
	internal static class Program
	{

        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);


        [STAThread]
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private static void Main()
		{
            try
            {
                //// Targets where to log to: File and Console
                //var logfile = new NLog.Targets.FileTarget("logfile")
                //{
                //    FileName = (Dashboard_Main.AssemblyDirectory + "\\logfile.txt").Replace("\\\\", "\\"),
                //    ArchiveFileName = "logfile.{#}.txt",
                //    ArchiveNumbering = NLog.Targets.ArchiveNumberingMode.Date,
                //    ArchiveEvery = NLog.Targets.FileArchivePeriod.Day,
                //    ArchiveDateFormat = "yyyyMMdd"
                //};
                //if (LogsLevel == 0) config.AddRule(LogLevel.Info, LogLevel.Info, logfile);

                //config.Reload();
                //NLog.LogManager.Configuration = config;
                //loger = NLog.LogManager.GetCurrentClassLogger();

                //loger.Log(LogLevel.Info, " Starting PromptPrivacy Database Indexer ....... " + DateTime.Now);

                bool instanceCountOne = false;
                Mutex mtex;
                using (mtex = new Mutex(true, "AccountsPro", out instanceCountOne))
                {
                    if (instanceCountOne)
                    {
                        var wi = WindowsIdentity.GetCurrent();
                        var wp = new WindowsPrincipal(wi);

                        bool runAsAdmin = wp.IsInRole(WindowsBuiltInRole.Administrator);

                        if (!runAsAdmin)
                        {
                            // It is not possible to launch a ClickOnce app as administrator directly,
                            // so instead we launch the app as administrator in a new process.
                            var processInfo = new ProcessStartInfo(Assembly.GetExecutingAssembly().CodeBase);

                            // The following properties run the new process as administrator
                            processInfo.UseShellExecute = true;
                            processInfo.Verb = "runas";

                            try
                            {
                                Process.Start(processInfo);
                            }
                            catch (Exception)
                            {
                                // The user did not allow the application to run as administrator
                                MessageBox.Show("The program needs to run with administrative rights only!");
                            }

                            // Shutdown the current process
                            Application.Exit();
                        }
                        else
                        {
                            // We are running as administrator
                            Application.EnableVisualStyles();
                            Application.SetCompatibleTextRenderingDefault(defaultValue: false);

                            Application.Run(new BaseForm());


                        }
                        mtex.ReleaseMutex();
                    }
                }

            }

            catch (Exception eep)
            {
                MessageBox.Show(eep.Message + " >>> " + eep.StackTrace, "Error while application start", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
	}
}
