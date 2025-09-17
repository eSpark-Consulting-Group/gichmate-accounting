using AccountsLibrary.Properties;
using FlexMold;
using FlexMold.MVVM.View.Firm;
using FlexMold.MVVM.ViewModel;
using System;
using System.Data.SqlClient;
using System.Windows;

namespace AccountsPro.MVVM.View
{
    public partial class StartupPage : Window
    {
        public StartupPage()
        {
            InitializeComponent();
            LoadBranches();
        }

        private void btn_Start_Click(object sender, RoutedEventArgs e)
        {
            string userName = txtUserName.Text.Trim();
            string password = txtPassword.Password.Trim();
            string branchMain = cmbBranch.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(branchMain))
            {
                MessageBox.Show("Please fill in all fields.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(Settings.Default.strConnetionString))
                {
                    con.Open();

                    string query = @"SELECT COUNT(*) 
                                       FROM [User] 
                                       WHERE UserName = @UserName 
                                       AND PassWord = @Password "
                                       ;

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@UserName", userName);
                        cmd.Parameters.AddWithValue("@Password", password);
                        //cmd.Parameters.AddWithValue("@Branch", branchMain);
                        
                        object result = cmd.ExecuteScalar();
                        int count = Convert.ToInt32(result);

                        if (count > 0)
                        {

                            using (SqlCommand cmdfisc = new SqlCommand("SELECT COUNT(*) FyearId FROM financialyear", con))
                            {
                                object result2 = cmdfisc.ExecuteScalar();
                                int count2 = Convert.ToInt32(result2);

                                if (count2 < 1)
                                    MessageBox.Show("FiscalYear Is Not Currently Set!!!");
                            }


                            var mainWindow = new MainWindow();
                            mainWindow.Show();

                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Invalid username, password, or branch.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void LoadBranches()
        {
            try
            {
                using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(Settings.Default.strConnetionString))
                {
                    con.Open();
                    string query = "SELECT Name FROM Branch";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbBranch.Items.Add(reader["Name"].ToString());
                        }
                    }
                }

                if (cmbBranch.Items.Count > 0)
                    cmbBranch.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading branches: " + ex.Message);
            }
        }
    }
}
