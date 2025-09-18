using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Media;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace AccountsPRO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private Logger log = LogManager.GetLogger("Main");
        //MainViewModel _vm;

        public MainWindow()
        {
            try
            {
                InitializeComponent();
            }
            catch(Exception ee) { MessageBox.Show("MainApp error: >>> "+ee.Message); }

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
