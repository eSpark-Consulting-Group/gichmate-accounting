using AccountsPRO.MVVM.View.Accounts;
using AccountsPRO.MVVM.View.Firm;
using AccountsPRO.MVVM.View.Master;
using AccountsPRO.MVVM.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
namespace AccountsPRO.MVVM.View
{
    /// <summary>
    /// Interaction logic for AccountingView.xaml
    /// </summary>
    public partial class MasterTransaction_Click : UserControl
    {
        private bool isSidebarExpanded = true;
        private IAccountingViewModel _AccountingViewModel
        {
            get { return App.AppHost.Services.GetRequiredService<IAccountingViewModel>(); }
        }
        public MasterTransaction_Click()
        {
            InitializeComponent();
            DataContext = _AccountingViewModel;
        }
        //private void ToggleSidebar_Click(object sender, RoutedEventArgs e)
        //{
        //    var storyboard = isSidebarExpanded
        //        ? (Storyboard)FindResource("CollapseSidebar")
        //        : (Storyboard)FindResource("ExpandSidebar");
        //    storyboard.Begin();
        //    isSidebarExpanded = !isSidebarExpanded;
        //}
        private void TopBarAccountsButton_Click(object sender, RoutedEventArgs e)
        {

            TopBarAccountsButton.ContextMenu.PlacementTarget = TopBarAccountsButton;
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
            var userSettings = new UserSettingsView();
            userSettings.Show();
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var firmDetails = new FirmDetails();
            firmDetails.Show();
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
        private void MasterBankButton_click(object sender, RoutedEventArgs e)
        {
            var bankMaster = new Bank();
            bankMaster.Show();
        }
        private void MasterBalanceSheetButton_click(object sender, RoutedEventArgs e)
        {
            var balanceSheet = new BalanceSheet();
            balanceSheet.Show();
        }
        private void MasterJournalEntryButton_Click(object sender, RoutedEventArgs e)
        {
            var journalEntry = new JournalEntry();
            journalEntry.Show();
        }

        private void MasterDoubleJournalEntries_Click(object sender, RoutedEventArgs e)
        {
            var doubleJournalEntries = new DoubleJournalEntries();
            doubleJournalEntries.Show();
        }
        //private void MasterTransactionButton_Click(object sender, RoutedEventArgs e)
        //{
        //    var transactionWithoutInventory = new TransactionWithoutInventory();
        //    transactionWithoutInventory.Show(); 
        //}
        private void MasterUploadTenantsButton_Click(object sender, RoutedEventArgs e)
        {
            var uploadTenants = new UploadTenants();
            uploadTenants.Show();
        }
        private void MasterUploadBank_Click(object sender, RoutedEventArgs e)
        {
            var uploadBankReceipts = new UploadBankReceipts();
            uploadBankReceipts.Show();
        }
        private void SideBarBtn_UserSettingsClick(object sender, RoutedEventArgs e)
        {
            var userSettings = new UserSettingsView();
            userSettings.Show();
        }
        private void SideBarBtn_AdminSettingsClick(object sender, RoutedEventArgs e)
        {
            var adminSettings = new AdminSettingsView();
            adminSettings.Show();
        }
        private void SideBarBtn_UploadTenantsClick(object sender, RoutedEventArgs e)
        {
            var uploadTenants = new UploadTenants();
            uploadTenants.Show();
        }
        private void SideBarBtn_BalanceSheetClick(object sender, RoutedEventArgs e)
        {
            var balanceSheet = new BalanceSheet();
            balanceSheet.Show();
        }
        private void SideBarBtn_BankReceiptsClick(object sender, RoutedEventArgs e)
        {
            var bankReceipts = new UploadBankReceipts();
            bankReceipts.Show();
        }
        private void SideBarBtn_DoubleJournalEntriesClick(object sender, RoutedEventArgs e)
        {
            var doubleJournalEntries = new DoubleJournalEntries();
            doubleJournalEntries.Show();
        }

        private void btn_Help_Click(object sender, RoutedEventArgs e)
        { 
            try
            {
                string url = "https://docs.google.com/document/d/1m-MggHM1FuUZOLv_MJsqdhIuu8RF6fhSM21YWpsjQfc/edit?usp=sharing"; 
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true 
                };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to open document: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}