using System;
using System.Globalization;
using System.Windows.Forms;
using Ashpro.Properties;

namespace Ashpro
{
    public class POSActivation
    {
        public static void POSActivationMethod()
        {
            Utility.uMacId = AniHelper.GetMaccID();
            if (Settings.Default.ActivationKey != null && Settings.Default.Activationlicense != null)
            {
                string activationlicense = Settings.Default.Activationlicense;
                _ = Settings.Default.ActivationStartDate;
                string activationEndDate = Settings.Default.ActivationEndDate;
                string activationMacID = Settings.Default.ActivationMacID;
                Utility.uLicenceNumber = Settings.Default.ActivationKey;
                if (!Utility.uMacId.Contains(activationMacID))
                {
                    ActivationDialog("This Software is not registered. Do you want to register now?");
                }
                else if (activationlicense == "demo")
                {
                    CultureInfo cultureInfo = new CultureInfo("en-US");
                    string s = DateTime.UtcNow.ToString("dd-MM-yyyy", cultureInfo);
                    DateTime endDate = DateTime.ParseExact(activationEndDate, "dd-MM-yyyy", cultureInfo);
                    DateTime curDate = DateTime.ParseExact(s, "dd-MM-yyyy", cultureInfo);
                    Application.Run(new DemoDialogView(endDate, curDate));
                }
                else
                {
                    FormLoadMethod();
                }
            }
            else
            {
                ActivationDialog("This Software is not registered. Do you want to register now?");
            }
        }

        public static void FormLoadMethod()
        {
            if (Settings.Default.strConnetionString == string.Empty)
            {
                if (Utility.uManufacturer != "DeskCloud ERP")
                {
                    Application.Run(new ServerOrClient());
                }
                else
                {
                    Settings.Default.isServer = true;
                    Settings.Default.Save();
                    Application.Run(new DatabaseInitialCreation());
                }
            }
            if (Settings.Default.MainForm == string.Empty)
            {
                Application.Run(new ThemeChanger(_isFromActivation: true));
            }
            else if (!(Settings.Default.MainForm == "POS") && !(Settings.Default.MainForm == "Restaurant"))
            {
                Application.Run(new Login());
            }
            else
            {
                Application.Run(new POSLogin());
            }
        }

        public static void FormLoadMethodTwo()
        {
            if (Settings.Default.strConnetionString == string.Empty)
            {
                ServerOrClient serverOrClient = new ServerOrClient();
                serverOrClient.Show();
            }
            if (Settings.Default.MainForm == string.Empty)
            {
                ThemeChanger themeChanger = new ThemeChanger();
                themeChanger.Show();
            }
            else if (!(Settings.Default.MainForm == "POS") && !(Settings.Default.MainForm == "Restaurant"))
            {
                Login login = new Login();
                login.Show();
            }
            else
            {
                POSLogin pOSLogin = new POSLogin();
                pOSLogin.Show();
            }
        }

        private static void ActivationDialog(string Dialog)
        {
            if (Messages.DialogMessage(Dialog))
            {
                Activation activation = new Activation();
                if (activation.ShowDialog() == DialogResult.OK)
                {
                    FormLoadMethod();
                }
                else
                {
                    Application.Exit();
                }
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
