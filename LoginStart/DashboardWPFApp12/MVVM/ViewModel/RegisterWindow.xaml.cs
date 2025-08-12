//using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System;
using System.Windows;

namespace DashboardWPFApp12
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void Btn_Login(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void btn_Submit(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-E09TG4T\\SQLEXPRESS;Initial Catalog=PSVAT5;Integrated Security=True;TrustServerCertificate=True;";

            string firstNameValue = firstName.Text;
            string lastNameValue = lastName.Text;
            string emailValue = email.Text;
            string userNameValue = userName.Text;
            string passwordValue = passwords.Password;

            try
            {
                using SqlConnection con = new SqlConnection(connectionString);
                con.Open();

                string query = @"INSERT INTO Registration (FirstName, LastName, Email, UserName, Passwords)
                                 VALUES (@FirstName, @LastName, @Email, @UserName, @Passwords)";
                using SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@FirstName", firstNameValue);
                cmd.Parameters.AddWithValue("@LastName", lastNameValue);
                cmd.Parameters.AddWithValue("@Email", emailValue);
                cmd.Parameters.AddWithValue("@UserName", userNameValue);
                cmd.Parameters.AddWithValue("@Passwords", passwordValue);
                cmd.ExecuteNonQuery();

                System.Windows.MessageBox.Show("Credentials Saved Successfully");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Failed to save credentials:\n" + ex.Message);
            }
        }
    }
}