using AccountsLibrary.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AccountsPro.MVVM.View.Firm
{
    public partial class CreateUser : Window
    {
        private int? _currentUserId = null;

        public CreateUser()
        {
            InitializeComponent();

            LoadPrivileges();
            LoadUsers();
        }

  
        private void LoadPrivileges()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                using (SqlCommand cmd = new SqlCommand("SELECT Id, Name FROM UserPrevilegeHdr", conn))
                {
                    conn.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());

                    txtComboBox.ItemsSource = dt.DefaultView;
                    txtComboBox.DisplayMemberPath = "Name";
                    txtComboBox.SelectedValuePath = "Id";

                    if (txtComboBox.Items.Count > 0)
                        txtComboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading privileges: " + ex.Message);
            }
        }

        private void LoadUsers()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT u.UserId, u.UserName, u.PassWord AS Password, u.PrevilageId AS PrivilegeID, 
                               p.Name AS PrivilegeName
                        FROM [User] u
                        LEFT JOIN UserPrevilegeHdr p ON u.PrevilageId = p.Id
                        WHERE UPPER(u.UserName) <> 'ADMIN'";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgUserPrivilege.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading users: " + ex.Message);
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text) || string.IsNullOrWhiteSpace(txtPassword.Password))
            {
                MessageBox.Show("Username and Password are required.");
                return;
            }

            if (txtComboBox.SelectedValue == null)
            {
                MessageBox.Show("Please select a privilege.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                using (SqlCommand cmd = new SqlCommand(
                    "INSERT INTO [User] (UserName, PassWord, PrevilageId) VALUES (@UserName, @Password, @PrivilegeId)", conn))
                {
                    cmd.Parameters.AddWithValue("@UserName", txtUserName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Password.Trim());
                    cmd.Parameters.AddWithValue("@PrivilegeId", Convert.ToInt32(txtComboBox.SelectedValue));

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("User saved successfully.");
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving user: " + ex.Message);
            }
        }

        //private void btnDelete_Click(object sender, RoutedEventArgs e)
        //{
        //    if (dgUserPrivilege.SelectedItem == null)
        //    {
        //        MessageBox.Show("Please select a user to delete.");
        //        return;
        //    }

        //    DataRowView drv = (DataRowView)dgUserPrivilege.SelectedItem;
        //    int userId = Convert.ToInt32(drv["UserId"]);

        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
        //        using (SqlCommand cmd = new SqlCommand("DELETE FROM [User] WHERE UserId = @UserId", conn))
        //        {
        //            cmd.Parameters.AddWithValue("@UserId", userId);

        //            conn.Open();
        //            cmd.ExecuteNonQuery();
        //        }

        //        MessageBox.Show("User deleted successfully.");
        //        LoadUsers();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error deleting user: " + ex.Message);
        //    }
        //}

    }
}
