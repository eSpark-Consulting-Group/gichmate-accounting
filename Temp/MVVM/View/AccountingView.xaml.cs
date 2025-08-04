using FlexMold.MVVM.View.Accounts;
using FlexMold.MVVM.View.Firm;
using FlexMold.MVVM.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace FlexMold.MVVM.View
{
    /// <summary>
    /// Interaction logic for AccountingView.xaml
    /// </summary>
    public partial class AccountingView : UserControl
    {
        private bool isSidebarExpanded = false;

        private IAccountingViewModel _AccountingViewModel { get { return App.AppHost.Services.GetRequiredService<IAccountingViewModel>(); } }
        public AccountingView()
        {
            DataContext = _AccountingViewModel;
            InitializeComponent();
        }

        private void ToggleSidebar_Click(object sender, RoutedEventArgs e)
        {
            var storyboard = isSidebarExpanded
                ? (Storyboard)FindResource("CollapseSidebar")
                : (Storyboard)FindResource("ExpandSidebar");

            storyboard.Begin();
            isSidebarExpanded = !isSidebarExpanded;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var adminSettings = new AdminSettingsView();
            adminSettings.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var userSettings = new UserSettingsVikew();
            userSettings.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var accountsScreens = new AccountsScreens();
            accountsScreens.Show();
        }
    }
}