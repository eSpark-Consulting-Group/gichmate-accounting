using FlexMold.MVVM.Model;
using FlexMold.MVVM.ViewModel;
using FlexMold.Utility;
using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Media;
using System.Reflection;
using System.Windows.Media;
using TwinCAT;
using TwinCAT.Ads;

namespace FlexMold
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private Logger log = LogManager.GetLogger("Main");
        //MainViewModel _vm;
        //private readonly ITwinCathelper _TwinCathelper;/*{ get { return App.AppHost.Services.GetRequiredService<ITwinCathelper>(); } }*/
        private ISplashScreenViewModel _SplashScreenViewModel { get { return App.AppHost.Services.GetRequiredService<ISplashScreenViewModel>(); } }

        public MainWindow()
        {
            //try { 
            //_TwinCathelper = twinCathelper;
            InitializeComponent();
            //Version v = GetRunningVersion();
            //string About = string.Format(CultureInfo.InvariantCulture, @"YourApp Version {0}.{1}.{2} (r{3})", v.Major, v.Minor, v.Build, v.Revision);
            //this.Title = "FlexMold V:" + GetRunningVersion().ToString();

            //switch (_SplashScreenViewModel.SelectedMachine.MachineType)
            //{
            //    case EMachineType.Motor:
            //        _TwinCathelper.InitializeNotifications();
            //        _TwinCathelper.TSession_ConnectionStateChanged += Session_ConnectionStateChanged;
            //        _TwinCathelper.TConnection_ConnectionStatusChanged += Connection_ConnectionStatusChanged;
            //        _TwinCathelper.TConnection_RouterStateChanged += Connection_RouterStateChanged;
            //        _TwinCathelper.TConnection_AdsStateChanged += Connection_AdsStateChanged;
            //        break;

            //    case EMachineType.Projector:
            //        break;

            //    default:
            //        break;
            //}
            //logCtrl.ItemAdded += OnLogMessageItemAdded;
            //log.Log(LogLevel.Info, $"Application Loaded Sucessfully.....");
            //log.Log(LogLevel.Info, $"Selected Machine Type : {_SplashScreenViewModel.SelectedMachine.MachineType} " +
            //    $"Name : {_SplashScreenViewModel.SelectedMachine.Name} ID : {_SplashScreenViewModel.SelectedMachine.ID}");
            //}
            //catch (Exception ex)
            //{
            //    var log = LogManager.GetLogger("Main");
            //    log.Log(LogLevel.Error, ex.Message);
            //}

        }
        private static Version GetRunningVersion()
        {
            try
            {
                return Assembly.GetEntryAssembly().GetName().Version;
            }
            catch (Exception)
            {
                return Assembly.GetExecutingAssembly().GetName().Version;
            }
        }

        private void Session_ConnectionStateChanged(object sender, EventArgs e)
        {
            try{            
            System.Windows.Application.Current.Dispatcher.BeginInvoke((Action)(() =>
            {
                
            }));
            }
            catch (Exception ex)
            {
                var log = LogManager.GetLogger("Main");
                log.Log(LogLevel.Error, "MainWindow-Action1"+ex.Message);
            }
        }

        private void Connection_AdsStateChanged(object sender, EventArgs e)
        {

            try{
                System.Windows.Application.Current.Dispatcher.BeginInvoke((Action)(() =>
            {
             
            }));
        }
            catch (Exception ex)
            {
                var log = LogManager.GetLogger("Main");
        log.Log(LogLevel.Error, "MainWindow-Action1"+ex.Message);
            }
}

private void Connection_RouterStateChanged(object sender, EventArgs e)
        {
            try { 
            System.Windows.Application.Current.Dispatcher.BeginInvoke((Action)(() =>
            {
                
            }));
        }
            catch (Exception ex)
            {
                var log = LogManager.GetLogger("Main");
        log.Log(LogLevel.Error, "MainWindow-Action1"+ex.Message);
            }

}

private void Connection_ConnectionStatusChanged(object sender, EventArgs e)
        {
            try { 
            System.Windows.Application.Current.Dispatcher.BeginInvoke((Action)(() =>
            {

            }));
            }
            catch (Exception ex)
            {
                var log = LogManager.GetLogger("Main");
                log.Log(LogLevel.Error, "MainWindow-Action1" + ex.Message);
            }

        }


        private async void ConnectionLost(string _msg)
        {
            //var view = new ConnectionLostDialog()
            //{
            //    DataContext = new ConnectionLostViewModel(_msg)
            //};
            //var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);
        }

        private void OnLogMessageItemAdded(object o, EventArgs Args)
        {
            // Do what you want :)
            LogEventInfo logInfo = (NLogEvent)Args;
            if (logInfo.Level >= LogLevel.Error)
                SystemSounds.Beep.Play();
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
                var log = LogManager.GetLogger("Main");
                log.Log(LogLevel.Error, "MainWindow-ClosingHandler");

}

        private void MetroWindow_Closing(object sender, CancelEventArgs e)
        {
            try{
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                var log = LogManager.GetLogger("Main");
                log.Log(LogLevel.Error, "MainWindow-MetroClosing" + ex.Message);
            }

        }

        private void MainTabControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void logCtrl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
