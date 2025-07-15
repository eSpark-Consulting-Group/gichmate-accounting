//using System.Data.SqlClient;
using System.Windows;
using Microsoft.Data.SqlClient;

namespace DashboardWPFApp12
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void btn_Submit(object sender, RoutedEventArgs e)
        {
           
            string ConnectionString = "Data Source=DESKTOP-E09TG4T\\SQLEXPRESS;Initial Catalog=PSVAT5;Integrated Security=True;TrustServerCertificate=True;";            //2.Establish Connection
            SqlConnection con = new SqlConnection(ConnectionString);

          
            con.Open();


            string FirstName = firstName.Text;
            string LastName = lastName.Text;
            string Email = email.Text;
            string UserName = userName.Text;
            string Passwords = passwords.Password;


            string Query = "INSERT INTO Registration(FirstName, LastName, Email, UserName, Passwords) \r\nVALUES('" + FirstName + "', '" + LastName + "', '" + Email + "', '" + UserName + "', '"+Passwords+"')";

          
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.ExecuteNonQuery();

            con.Close();
            MessageBox.Show("Credentials Saved Successfully");
        }

        private void Btn_Login(object sender, RoutedEventArgs e)
        {
            var loginWin = new MainWindow();
            loginWin.Show();
            this.Close();
        }
    }
}