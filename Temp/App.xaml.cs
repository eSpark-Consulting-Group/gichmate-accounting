using AccountsPro.MVVM.View;
using AccountsPRO.MVVM.View;
using AccountsPRO.MVVM.View.Accounts;
using AccountsPRO.MVVM.ViewModel;
using AccountsPRO.MVVM.ViewModel.SplashScreen;
using AccountsPRO.Utility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Markup;

namespace AccountsPRO
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }

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

            // Resolve and show StartupPage
            var startupScreen = AppHost.Services.GetRequiredService<StartupPage>();
            startupScreen.Show();

            // Optionally show main window instead:
            // var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
            // mainWindow.Show();

            base.OnStartup(e);
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
