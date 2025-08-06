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
        private bool isSidebarExpanded = true;

        private IAccountingViewModel _AccountingViewModel
        {
            get { return App.AppHost.Services.GetRequiredService<IAccountingViewModel>(); }
        }

        public AccountingView()
        {
            InitializeComponent();
            DataContext = _AccountingViewModel;
        }

        private void ToggleSidebar_Click(object sender, RoutedEventArgs e)
        {
            var storyboard = isSidebarExpanded
                ? (Storyboard)FindResource("CollapseSidebar")
                : (Storyboard)FindResource("ExpandSidebar");

            storyboard.Begin();
            isSidebarExpanded = !isSidebarExpanded;
        }

        private void TopBarAccountsButton_Click(object sender, RoutedEventArgs e)
        {
            TopBarAccountsButton.ContextMenu.PlacementTarget = TopBarReportsButton;
            TopBarAccountsButton.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            TopBarAccountsButton.ContextMenu.IsOpen = true;
        }

        /// ///////////////////////////////////////////////////////////////



        private void TopBarReportsButton_Click(object sender, RoutedEventArgs e)
        {
            TopBarReportsButton.ContextMenu.PlacementTarget = TopBarReportsButton;
            TopBarReportsButton.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            TopBarReportsButton.ContextMenu.IsOpen = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var adminSettings = new AdminSettingsView();
            adminSettings.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var userSettings = new UserSettingsView(); // double-check spelling if it was intentional
            userSettings.Show();
        }
    }
}
