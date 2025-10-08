using AccountsPro.MVVM.View;
using AccountsPro.SubProjects.AccountsLibraryFramework;
using AccountsPro.MVVM.View.PropertyManagement;
using AccountsPRO.MVVM.View;
using AccountsPRO.MVVM.View.Accounts;
using AccountsPRO.MVVM.ViewModel;
using AccountsPRO.MVVM.ViewModel.SplashScreen;
using AccountsPRO.Utility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Markup;

namespace AccountsPRO
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public static IHost? AppHost { get; private set; }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public App()
        {
            try
            {
                AppHost = Host.CreateDefaultBuilder()
                    .ConfigureServices((hostContext, services) =>
                    {
                        // Windows & Startup page
                        services.AddSingleton<MainWindow>();
                        services.AddSingleton<StartupPage>();

                        // ViewModels
                        services.AddSingleton<IAccountingViewModel, AccountingViewModel>();

                    // Views

                    //Account User Controls
                    services.AddSingleton<DefaultAccountScreenView>();
                    services.AddSingleton<BankPaymentView>();
                    services.AddSingleton<BankReceiptView>();
                    services.AddSingleton<BankReconciliationView>();
                    services.AddSingleton<CashPaymentView>();
                    services.AddSingleton<CashReceiptView>();
                    services.AddSingleton<CreditNoteView>();
                    services.AddSingleton<DebitNoteView>();
                    services.AddSingleton<PaymentAndReceiptView>();
                    services.AddSingleton<PostDatedChequePaymentView>();
                    services.AddSingleton<PostDatedChequeReceiptView>();
                    services.AddSingleton<ReceiptView>();
                    services.AddSingleton<UploadTenantsUserControl>();

                    //propertymanagement
                    services.AddSingleton<PropertyManagement>();
                    services.AddSingleton<ArrearsAgingAnalysis>();
                    services.AddSingleton<PrepaymentTracking>();
                    services.AddSingleton<UtilityCostAllocation>();
                    services.AddSingleton<HistoricalBalance>();
                        //

                    })
                    .Build();
            }
            catch (Exception ex)
            {
                var log = LogManager.GetLogger("Main");
                log.Log(LogLevel.Error, ex.Message);
            }
        }

        // Corrected: This now resolves StartupPage (not MainWindow)
        private StartupPage StartupPageInstance
        {
            get { return App.AppHost!.Services.GetRequiredService<StartupPage>(); }
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();

            var vCulture = new CultureInfo("en-US");

            if (CultureInfo.CurrentCulture.Name.Contains("DE"))
            {
                vCulture.NumberFormat.NumberDecimalSeparator = ",";
                vCulture.NumberFormat.CurrencyDecimalSeparator = ",";
            }
            else
            {
                vCulture.NumberFormat.NumberDecimalSeparator = ".";
                vCulture.NumberFormat.CurrencyDecimalSeparator = ".";
            }

            Thread.CurrentThread.CurrentCulture = vCulture;
            Thread.CurrentThread.CurrentUICulture = vCulture;
            CultureInfo.DefaultThreadCurrentCulture = vCulture;
            CultureInfo.DefaultThreadCurrentUICulture = vCulture;

            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));



            try
            {
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
                                //MessageBox.Show("The program needs to run with administrative rights only!");
                            }

                            // Shutdown the current process
                            //Application.Exit();
                        }
                        else
                        {
                            // We are running as administrator
                            //Application.Run(new BaseForm());
                            // Resolve and show StartupPage
                            var startupScreen = AppHost.Services.GetRequiredService<StartupPage>();
                            startupScreen.Show();
                            base.OnStartup(e);

                        }
                        mtex.ReleaseMutex();
                    }
                }

            }

            catch (Exception eep)
            {
                //MessageBox.Show(eep.Message + " >>> " + eep.StackTrace, "Error while application start", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            // Optionally show main window instead:
            // var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
            // mainWindow.Show();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            try
            {
                await AppHost!.StopAsync();
                base.OnExit(e);
            }
            catch (Exception ex)
            {
                var log = LogManager.GetLogger("Main");
                log.Log(LogLevel.Error, ex.Message);
            }
        }
    }
}
