using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace FlexMold.MVVM.View.Master
{
    public partial class AccountsGroup : Window
    {
        private readonly string connectionString = ((System.Configuration.ConnectionStringSettings)
            System.Configuration.ConfigurationManager.ConnectionStrings["PSVAT5"]).ConnectionString;

        public AccountsGroup()
        {
            InitializeComponent();
            Loaded += AccountsGroup_Loaded;
        }

        private void AccountsGroup_Loaded(object sender, RoutedEventArgs e)
        {
            LoadUnderComboBox();
            LoadAccountGroups();
        }

        private void LoadUnderComboBox()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT DISTINCT GroupName FROM AccountGroup ORDER BY GroupName";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                UnderComboBox.Items.Clear();
                while (reader.Read())
                {
                    UnderComboBox.Items.Add(reader["GroupName"].ToString());
                }
                conn.Close();
            }
        }

        private void LoadAccountGroups()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT AGroupId, GroupName, Under FROM AccountGroup";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                ExistingGroupDataGrid.ItemsSource = dt.DefaultView;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string groupName = GroupNameTextBox.Text.Trim();
            string under = UnderComboBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(groupName)) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM AccountGroup WHERE GroupName = @GroupName";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@GroupName", groupName);
                int exists = (int)checkCmd.ExecuteScalar();

                if (exists == 0)
                {
                    string insertQuery = "INSERT INTO AccountGroup (GroupName, Under, Remarks, isBuilIn) VALUES (@GroupName, @Under, '', 0)";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                    insertCmd.Parameters.AddWithValue("@GroupName", groupName);
                    insertCmd.Parameters.AddWithValue("@Under", under);
                    insertCmd.ExecuteNonQuery();
                }
                else
                {
                    string updateQuery = "UPDATE AccountGroup SET Under = @Under WHERE GroupName = @GroupName";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                    updateCmd.Parameters.AddWithValue("@GroupName", groupName);
                    updateCmd.Parameters.AddWithValue("@Under", under);
                    updateCmd.ExecuteNonQuery();
                }

                conn.Close();
            }

            LoadUnderComboBox();
            LoadAccountGroups();
            ClearFields();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            string groupName = GroupNameTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(groupName)) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string checkBuiltin = "SELECT isBuilIn FROM AccountGroup WHERE GroupName = @GroupName";
                SqlCommand checkCmd = new SqlCommand(checkBuiltin, conn);
                checkCmd.Parameters.AddWithValue("@GroupName", groupName);
                object result = checkCmd.ExecuteScalar();

                if (result != null && Convert.ToBoolean(result))
                {
                    MessageBox.Show("Built-in record cannot be deleted.");
                    return;
                }

                string deleteQuery = "DELETE FROM AccountGroup WHERE GroupName = @GroupName";
                SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn);
                deleteCmd.Parameters.AddWithValue("@GroupName", groupName);
                deleteCmd.ExecuteNonQuery();

                conn.Close();
            }

            LoadUnderComboBox();
            LoadAccountGroups();
            ClearFields();
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
            LoadUnderComboBox();
            LoadAccountGroups();
        }

        private void ClearFields()
        {
            GroupNameTextBox.Text = string.Empty;
            UnderComboBox.SelectedIndex = -1;
            ExistingGroupDataGrid.SelectedIndex = -1;
            GroupNameTextBox.Focus();
        }

        private void ExistingGroupDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ExistingGroupDataGrid.SelectedItem is DataRowView row)
            {
                GroupNameTextBox.Text = row["GroupName"].ToString();
                UnderComboBox.Text = row["Under"].ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

           }
}