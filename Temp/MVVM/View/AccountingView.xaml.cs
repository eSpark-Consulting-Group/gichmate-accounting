using AccountsLibrary;
using AccountsPro.MVVM.View;
using AccountsPro.MVVM.View.Firm;
using AccountsPro.MVVM.View.Master;
using AccountsPRO.MVVM.View.Accounts;
using AccountsPRO.MVVM.View.Firm;
using AccountsPRO.MVVM.View.Master;
using AccountsPRO.MVVM.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
namespace AccountsPRO.MVVM.View
{
    public partial class MasterTransaction : UserControl
    {
        private bool isSidebarExpanded = true;

        public static System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        private void timerReoloaded_starttimer(object sender, EventArgs e)
        {
            this.txtUserLoggedIn.Text = AniHelper.LoggedInUser;
            dispatcherTimer.Stop();
        }


        private IAccountingViewModel _AccountingViewModel
        {
            get { return App.AppHost.Services.GetRequiredService<IAccountingViewModel>(); }
            
        }


        //public static string LoggedInUser
        //{
        //    get { return AniHelper.LoggedInUser; }
        //    set { AniHelper.LoggedInUser= value;
        //        //this.txtUserLoggedIn.Text = value;
        //        dispatcherTimer.Start();
        //    }
        //}

        public MasterTransaction()
        {
            InitializeComponent();
            DataContext = _AccountingViewModel;
            AniHelper.LoggedInUser = "Demo";
            this.txtUserLoggedIn.Text = AniHelper.LoggedInUser;

            dispatcherTimer.Tick += timerReoloaded_starttimer;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        }
        private void TopBarAccountsButton_Click(object sender, RoutedEventArgs e)
        {

            TopBarAccountsButton.ContextMenu.PlacementTarget = TopBarAccountsButton;
            TopBarAccountsButton.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            TopBarAccountsButton.ContextMenu.HorizontalOffset = 0;
            TopBarAccountsButton.ContextMenu.VerticalOffset = 0;
            TopBarAccountsButton.ContextMenu.IsOpen = true;
        }

        private void TopBarPropertyManagementButton_Click(object sender, RoutedEventArgs e)
        {

            TopBarPropertyManagementButton.ContextMenu.PlacementTarget = TopBarPropertyManagementButton;
            TopBarPropertyManagementButton.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            TopBarPropertyManagementButton.ContextMenu.HorizontalOffset = 0;
            TopBarPropertyManagementButton.ContextMenu.VerticalOffset = 0;
            TopBarPropertyManagementButton.ContextMenu.IsOpen = true;
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
            var createUser = new CreateUser();
            createUser.Show();
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

        private void SideBarBtn_TenantsManagementClick(object sender, RoutedEventArgs e)
        {
            var tenantManagement = new TenantManagement();
            tenantManagement.Show();
        }


        private void btn_Help_Click(object sender, RoutedEventArgs e)
        { 
            try
            {
                //Manual.pdf is in Bin -> /Net8.0Windows folder
                //just putting the file there will help us view it as a pdf from the given command below!
                String exepath = Directory.GetParent(Assembly.GetExecutingAssembly().FullName).FullName;
                Process.Start(new ProcessStartInfo(exepath+"\\Manual.pdf") { UseShellExecute = true });
                //Process.Start(exepath + "\\Manual.pdf");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to open document: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CreateUserBtn_Click(object sender, RoutedEventArgs e)
        {
            var createUser = new CreateUser();
            createUser.Show();
        }

        private void LogOutBtn_Click(object sender, RoutedEventArgs e)
        {
             Application.Current.Shutdown();           
           
        }

        
    }
}