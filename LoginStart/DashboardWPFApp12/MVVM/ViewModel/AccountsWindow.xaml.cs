using System.Windows;
using System.Windows.Input;

namespace DashboardWPFApp12
{
    public partial class AccountsWindow : Window
    {
        public AccountsWindow()
        {
            InitializeComponent();

        }

        private void Btn_Login(object sender, RoutedEventArgs e)
        {
            var loginWin = new MainWindow();
            loginWin.Show();
            this.Close();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btn_Dashboard(object sender, RoutedEventArgs e)
        {
            DashboardWindow dashboardWindow = new DashboardWindow();
            dashboardWindow.Show();
            this.Close();
        }

        private void btn_Menu(object sender, RoutedEventArgs e)
        {
            MenuWindow menuWindow = new MenuWindow();
            menuWindow.Show();
            this.Close();
        }

        private void btn_Accounts(object sender, RoutedEventArgs e)
        {
            AccountsWindow accountsWindow = new AccountsWindow();
            accountsWindow.Show();
            this.Close();
        }

        private void btn_Profiles(object sender, RoutedEventArgs e)
        {
            ProfilesWindow profilesWindow = new ProfilesWindow();
            profilesWindow.Show();
            this.Close();
        }

        private void btn_LoginMenu_Click(object sender, RoutedEventArgs e)
        {
            var loginWin = new MainWindow();
            loginWin.Show();
            this.Close();
        }
    }
}