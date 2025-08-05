using Ashpro.License.Helper;
using Ashpro.Properties;
using AshproMessage;
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace Ashpro
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(defaultValue: false);
            CultureInfo cultureInfo3 = (Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language)));
            MessageBoxManager.OK = MessageKeys.OK;
            MessageBoxManager.Cancel = MessageKeys.Cancel;
            MessageBoxManager.Retry = MessageKeys.Retry;
            MessageBoxManager.Ignore = MessageKeys.Ignore;
            MessageBoxManager.Abort = MessageKeys.Abort;
            MessageBoxManager.Yes = MessageKeys.Yes;
            MessageBoxManager.No = MessageKeys.No;
            MessageBoxManager.Register();
            Utility.uManufacturer = "DeskCloud ERP";
            string strConnetionString = string.Empty;
            if (Utility.uManufacturer == "DeskCloud ERP")
            {
                strConnetionString = "Server = .\\SQLEXPRESS; Database = PSVAT5; Integrated Security = true";
            }
            Settings.Default.strConnetionString = strConnetionString;
            Settings.Default.isServer = false;
            Settings.Default.Save();
            Utility.uTerminal = Environment.MachineName;
            Utility.iBranchId = Settings.Default.iBranchId;
            RegisterFont.RegisterOwnFont("KeaniaOne-Regular.ttf");
            RegisterFont.RegisterOwnFont("LibreBarcode39-Regular.ttf");
            if (Settings.Default.CounterName == string.Empty)
            {
                Settings.Default.CounterName = Environment.MachineName.Substring(Environment.MachineName.Length - 2);
                Settings.Default.Save();
            }

            Splash splash = new Splash();
            if (splash.ShowDialog() == DialogResult.OK)
            {
                var cached = CachedLicense.LoadLicense();

                // Use the async method and wait for result synchronously
                var result = LicenseManager.ValidateLicenseWithServer(cached?.LicenseKey).GetAwaiter().GetResult();

                if (result?.Valid == true && DateTime.Parse(result.ExpiresOn) > DateTime.Now)
                {
                    CachedLicense.SaveLicense(new CachedLicense
                    {
                        LicenseKey = cached.LicenseKey,
                        ExpiresOn = DateTime.Parse(result.ExpiresOn)
                    });

                    Application.Run(new Login());
                }
                else
                {
                    Application.Run(new LicenseForm());
                }
            }
        }
    }
}