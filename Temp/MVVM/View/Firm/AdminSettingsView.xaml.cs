using AccountsLibrary.Properties;
using FlexMold.MVVM.View.Accounts;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace FlexMold.MVVM.View.Firm
{
    /// <summary>
    /// Interaction logic for AdminSettingsView.xaml
    /// </summary>
    public partial class AdminSettingsView : Window
    {
        public AdminSettingsView()
        {
            InitializeComponent();
        }

       
       private void Btn_Update(object sender, RoutedEventArgs e)
{

        string userNameValue = userName.Text.Trim();
        string oldPasswordValue = oldPassword.Text.Trim();
        string newPasswordValue = newPassword.Text.Trim();
        string confirmPasswordValue = confirmPassword.Text.Trim();

    if (string.IsNullOrWhiteSpace(userNameValue) || string.IsNullOrWhiteSpace(oldPasswordValue) ||
        string.IsNullOrWhiteSpace(newPasswordValue) || string.IsNullOrWhiteSpace(confirmPasswordValue))
    {
        MessageBox.Show("Please fill in all fields.");
        return;
    }

    if (newPasswordValue != confirmPasswordValue)
    {
                MessageBox.Show("New Password and Confirmation Password do not match");
    }

    try
    {
        using (Microsoft.Data.SqlClient.SqlConnection con = new Microsoft.Data.SqlClient.SqlConnection(Settings.Default.strConnetionString))
        {
            con.Open();

            // Check if username and old password match
            string checkQuery = @"SELECT COUNT(*) FROM [User] WHERE UserName = @UserName AND PassWord = @OldPassword";
            using (Microsoft.Data.SqlClient.SqlCommand checkCmd = new Microsoft.Data.SqlClient.SqlCommand(checkQuery, con))
            {
                checkCmd.Parameters.AddWithValue("@UserName", userNameValue);
                checkCmd.Parameters.AddWithValue("@OldPassword", oldPasswordValue);

                int matchCount = (int)checkCmd.ExecuteScalar();

                if (matchCount == 0)
                {
                    MessageBox.Show("Incorrect username or old password.");
                    return;
                }
            }

            // Update the password
            string updateQuery = @"UPDATE [User] SET PassWord = @NewPassword WHERE UserName = @UserName";
            using (Microsoft.Data.SqlClient.SqlCommand updateCmd = new Microsoft.Data.SqlClient.SqlCommand(updateQuery, con))
            {
                updateCmd.Parameters.AddWithValue("@UserName", userNameValue);
                updateCmd.Parameters.AddWithValue("@NewPassword", newPasswordValue);

                int rowsAffected = updateCmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Password updated successfully.");
                }
                else
                {
                    MessageBox.Show("Password update failed.");
                }
            }
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error:\n" + ex.Message);
    }
}

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            userName.Clear();
            oldPassword.Clear();
            newPassword.Clear();
            confirmPassword.Clear();
        }

      

        //
        //
        //


        /*//Now, we must add validation code here at Old password area, in case the user writes the wrong password, it should prompt
            or should ccreate a messagebox saying that the password is incorrect.

            Furthermore, the oldPassword will be manually created and whenever user matches the password when updating, he'll be able to 
            update it respectfully.
       */
    }
    }

