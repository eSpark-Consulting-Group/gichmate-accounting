using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DashboardWPFApp12
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Btn_Start(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Do you want to Log In?", "User wants to Log In",
                MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                //string password = Microsoft.VisualBasic.Interaction.InputBox("Enter Password:", "Authentication", "", -1, -1);
                //if (password != "741") { System.Windows.MessageBox.Show("Incorrect password!", "Access Denied"); return; }

                DashboardWindow dashboardWindow = new DashboardWindow();
                dashboardWindow.Show();
                this.Close();
            }
        }


        private void Btn_Register(object sender, RoutedEventArgs e)
{
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Close();
}

        private void Btn_Login(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn_Menu(object sender, RoutedEventArgs e)
        {
            MenuWindow menuWindow = new MenuWindow();
            menuWindow.Show();
            this.Close();
        }

        private void btn_Dashboard(object sender, RoutedEventArgs e)
        {
            DashboardWindow dashboardWindow = new DashboardWindow();
            dashboardWindow.Show();
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
    }
}