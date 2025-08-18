using FlexMold.MVVM.View.Accounts;
using FlexMold.MVVM.View.Firm;
using FlexMold.MVVM.View.Master;
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
           
            TopBarAccountsButton.ContextMenu.PlacementTarget = TopBarAccountsButton; // ✅ anchor to itself
            TopBarAccountsButton.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            TopBarAccountsButton.ContextMenu.HorizontalOffset = 0;
            TopBarAccountsButton.ContextMenu.VerticalOffset = 0;
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var reusableReceipt = new ReusableReceipt();
            reusableReceipt.Show();
        }

        private void TopBarMasterButton_Click(object sender, RoutedEventArgs e)
        {
            TopBarMasterButton.ContextMenu.PlacementTarget = TopBarMasterButton;
            TopBarMasterButton.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            TopBarMasterButton.ContextMenu.IsOpen = true;
        }

        private void MasterGroupButton_click(object sender, RoutedEventArgs e)
        {
            var accountsGroup = new AccountsGroup();
            accountsGroup.Show();
        }

        private void MasterLedgerButton_click(object sender, RoutedEventArgs e)
        {
            var accountsLedger = new AccountsLedger();
            accountsLedger.Show();

        }

        private void MasterExpenseButton_click(object sender, RoutedEventArgs e)
        {
            var expenseAccount = new ExpenseAccount();
            expenseAccount.Show();
        }

        private void MasterBankButton_click(object name, RoutedEventArgs e)
        {
            var bankMaster = new Bank();
            bankMaster.Show();
        }

    }
}
