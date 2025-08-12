using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DashboardWPFApp12
{
    public partial class MenuWindow : Window
    {
        private bool isSidebarExpanded = false;

        public MenuWindow()
        {
            InitializeComponent();
            
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            if (e.ButtonState == MouseButtonState.Pressed)
                DragMove();
        }

        private void Btn_Login(object sender, RoutedEventArgs e)
        {
            var loginWin = new MainWindow();
            loginWin.Show();
            this.Close();
        }

        private void btn_Dashboard(object sender, RoutedEventArgs e)
        {
            var dashboardWindow = new DashboardWindow();
            dashboardWindow.Show();
            this.Close();
        }

        private void btn_Accounts(object sender, RoutedEventArgs e)
        {
            var accountsWindow = new AccountsWindow();
            accountsWindow.Show();
            this.Close();
        }

        private void btn_Profiles(object sender, RoutedEventArgs e)
        {
            var profilesWindow = new ProfilesWindow();
            profilesWindow.Show();
            this.Close();
        }

        private void ToggleSidebar_Click(object sender, RoutedEventArgs e)
        {
            var storyboard = isSidebarExpanded
                ? (Storyboard)FindResource("CollapseSidebar")
                : (Storyboard)FindResource("ExpandSidebar");

            storyboard.Begin();
            isSidebarExpanded = !isSidebarExpanded;
        }

        private void btn_View(object sender, RoutedEventArgs e) { }
        private void btn_Notifications(object sender, RoutedEventArgs e) { }
        private void btn_Themes(object sender, RoutedEventArgs e) { }
        private void btn_UseAs(object sender, RoutedEventArgs e) { }

        private void btn_Reports(object sender, RoutedEventArgs e)
        {
            var mainReports = new ReportWindow();
            mainReports.Show();
        }
    }
}