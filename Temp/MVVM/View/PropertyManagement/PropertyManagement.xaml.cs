using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using AccountsLibrary.Properties; 
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace AccountsPro.MVVM.View.PropertyManagement
{
    public partial class PropertyManagement : UserControl
    {
        private DataTable tenantsTable;

        public PropertyManagement()
        {
            InitializeComponent();

            PropertyManagementDataGrid.SelectionChanged += PropertyManagementDataGrid_SelectionChanged;
            UnitNoComboBox.SelectionChanged += UnitNoComboBox_SelectionChanged;

            LoadAllTenants();
            PopulateUnitComboBox();
        }

        #region Load / Populate
        private void LoadAllTenants()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    conn.Open();
                    string sql = @"
                        SELECT 
                            [UNIT NO],
                            [TENANT NAME],
                            [TENANT IDENTITY],
                            [TENANT PHONE],
                            [TENANT EMAIL],
                            [TENANT STARTDATE],
                            [TENANT ENDDATE],
                            [TENANT ISACTIVE] AS OCCUPIED,
                            [UPDATETIME]
                        FROM TenantsUnitRelation
                        ORDER BY [UNIT NO],[TENANT NAME]";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        tenantsTable = new DataTable();
                        da.Fill(tenantsTable);
                        PropertyManagementDataGrid.ItemsSource = tenantsTable.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tenants: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PopulateUnitComboBox()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    conn.Open();
                    string sql = @"
                        SELECT DISTINCT [UNIT NO]
                        FROM UploadsTenantRent
                        ORDER BY [UNIT NO]";

                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        UnitNoComboBox.ItemsSource = dt.DefaultView;
                        UnitNoComboBox.DisplayMemberPath = "UNIT NO";
                        UnitNoComboBox.SelectedValuePath = "UNIT NO";

                        if (dt.Rows.Count > 0)
                            UnitNoComboBox.SelectedIndex = 0;
                        else
                            UnitNoComboBox.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error populating UnitNoComboBox: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Button Handlers
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string unitNo = GetUnitNoFromControl();
                if (string.IsNullOrWhiteSpace(unitNo))
                {
                    MessageBox.Show("Please select or enter a Unit No before saving.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (StartDatePickerBox.SelectedDate.HasValue && EndDatePickerBox.SelectedDate.HasValue)
                {
                    if (StartDatePickerBox.SelectedDate > EndDatePickerBox.SelectedDate)
                    {
                        MessageBox.Show("Start Date cannot be greater than End Date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                if (UnitExists(unitNo))
                {
                    MessageBox.Show("A record for this Unit No already exists. Use Update instead.", "Duplicate", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    conn.Open();
                    string sql = @"
                        INSERT INTO TenantsUnitRelation
                        ([UNIT NO],[TENANT NAME],[TENANT IDENTITY],[TENANT PHONE],
                         [TENANT EMAIL],[TENANT STARTDATE],[TENANT ENDDATE],[TENANT ISACTIVE],[UPDATETIME])
                        VALUES
                        (@UnitNo,@TenantName,@TenantIdentity,@TenantPhone,
                         @TenantEmail,@StartDate,@EndDate,@IsActive,@UpdateTime)";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        AddCommonParameters(cmd, unitNo);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Saved successfully.", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearFields();
                LoadAllTenants();
                PopulateUnitComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving tenant: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string unitNo = GetUnitNoFromControl();
                if (string.IsNullOrWhiteSpace(unitNo))
                {
                    MessageBox.Show("Please provide a Unit No to update.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (StartDatePickerBox.SelectedDate.HasValue && EndDatePickerBox.SelectedDate.HasValue)
                {
                    if (StartDatePickerBox.SelectedDate > EndDatePickerBox.SelectedDate)
                    {
                        MessageBox.Show("Start Date cannot be greater than End Date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                if (!UnitExists(unitNo))
                {
                    MessageBox.Show("Record not found for this Unit No. Use Save to create a new record.", "Not found", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    conn.Open();
                    string sql = @"
                        UPDATE TenantsUnitRelation SET
                            [TENANT NAME] = @TenantName,
                            [TENANT IDENTITY] = @TenantIdentity,
                            [TENANT PHONE] = @TenantPhone,
                            [TENANT EMAIL] = @TenantEmail,
                            [TENANT STARTDATE] = @StartDate,
                            [TENANT ENDDATE] = @EndDate,
                            [TENANT ISACTIVE] = @IsActive,
                            [UPDATETIME] = @UpdateTime
                        WHERE [UNIT NO] = @UnitNo";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        AddCommonParameters(cmd, unitNo);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Updated successfully.", "Updated", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearFields();
                LoadAllTenants();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating tenant: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
            PropertyManagementDataGrid.SelectedIndex = -1;
        }
        #endregion

        #region Helpers
        private string GetUnitNoFromControl()
        {
            try
            {
                if (UnitNoComboBox.SelectedValue != null)
                    return UnitNoComboBox.SelectedValue.ToString().Trim();
                if (!string.IsNullOrWhiteSpace(UnitNoComboBox.Text))
                    return UnitNoComboBox.Text.Trim();
            }
            catch { }
            return string.Empty;
        }

        private void AddCommonParameters(SqlCommand cmd, string unitNo)
        {
            cmd.Parameters.AddWithValue("@UnitNo", string.IsNullOrWhiteSpace(unitNo) ? (object)DBNull.Value : unitNo);
            cmd.Parameters.AddWithValue("@TenantName", string.IsNullOrWhiteSpace(TenantNameTextBox.Text) ? (object)DBNull.Value : TenantNameTextBox.Text.Trim());
            cmd.Parameters.AddWithValue("@TenantIdentity", string.IsNullOrWhiteSpace(TenantIdentityBox.Text) ? (object)DBNull.Value : TenantIdentityBox.Text.Trim());
            cmd.Parameters.AddWithValue("@TenantPhone", string.IsNullOrWhiteSpace(PhoneTextBox.Text) ? (object)DBNull.Value : PhoneTextBox.Text.Trim());
            cmd.Parameters.AddWithValue("@TenantEmail", string.IsNullOrWhiteSpace(TenantEmailTextBox.Text) ? (object)DBNull.Value : TenantEmailTextBox.Text.Trim());

            object startDate = StartDatePickerBox.SelectedDate.HasValue ? (object)StartDatePickerBox.SelectedDate.Value : (object)DBNull.Value;
            object endDate = EndDatePickerBox.SelectedDate.HasValue ? (object)EndDatePickerBox.SelectedDate.Value : (object)DBNull.Value;

            cmd.Parameters.AddWithValue("@StartDate", startDate ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@EndDate", endDate ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@IsActive", TenantActiveCheckBox.IsChecked == true);
            cmd.Parameters.AddWithValue("@UpdateTime", DateTime.Now); // ✅ Fixed here
        }

        private bool UnitExists(string unitNo)
        {
            if (string.IsNullOrWhiteSpace(unitNo)) return false;
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    conn.Open();
                    string sql = "SELECT COUNT(1) FROM TenantsUnitRelation WHERE [UNIT NO] = @UnitNo";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@UnitNo", unitNo);
                        int count = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
                        return count > 0;
                    }
                }
            }
            catch { return false; }
        }

        private void ClearFields()
        {
            UnitNoComboBox.SelectedIndex = -1;
            TenantIdentityBox.Text = string.Empty;
            TenantNameTextBox.Text = string.Empty;
            PhoneTextBox.Text = string.Empty;
            TenantEmailTextBox.Text = string.Empty;
            StartDatePickerBox.SelectedDate = null;
            EndDatePickerBox.SelectedDate = null;
            TenantActiveCheckBox.IsChecked = false;
        }
        #endregion

        #region Grid / Selection
        private void PropertyManagementDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (PropertyManagementDataGrid.SelectedItem == null) return;
                var rowView = PropertyManagementDataGrid.SelectedItem as DataRowView;
                if (rowView == null) return;

                UnitNoComboBox.SelectedValue = rowView["UNIT NO"] != DBNull.Value ? rowView["UNIT NO"].ToString() : string.Empty;
                TenantNameTextBox.Text = rowView["TENANT NAME"] != DBNull.Value ? rowView["TENANT NAME"].ToString() : string.Empty;
                TenantIdentityBox.Text = rowView["TENANT IDENTITY"] != DBNull.Value ? rowView["TENANT IDENTITY"].ToString() : string.Empty;
                PhoneTextBox.Text = rowView["TENANT PHONE"] != DBNull.Value ? rowView["TENANT PHONE"].ToString() : string.Empty;
                TenantEmailTextBox.Text = rowView["TENANT EMAIL"] != DBNull.Value ? rowView["TENANT EMAIL"].ToString() : string.Empty;

                if (DateTime.TryParse(Convert.ToString(rowView["TENANT STARTDATE"]), out var start))
                    StartDatePickerBox.SelectedDate = start;
                else StartDatePickerBox.SelectedDate = null;

                if (DateTime.TryParse(Convert.ToString(rowView["TENANT ENDDATE"]), out var end))
                    EndDatePickerBox.SelectedDate = end;
                else EndDatePickerBox.SelectedDate = null;

                if (bool.TryParse(Convert.ToString(rowView["OCCUPIED"]), out var active))
                    TenantActiveCheckBox.IsChecked = active;
                else TenantActiveCheckBox.IsChecked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error handling selection: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        // Only alphabets and spaces
        private void LettersOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[a-zA-Z ]+$");
        }

        // Only digits
        private void NumericOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
        }



        private void UnitNoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (tenantsTable == null) return;
                DataView dv = tenantsTable.DefaultView;

                if (UnitNoComboBox.SelectedValue == null || string.IsNullOrWhiteSpace(UnitNoComboBox.SelectedValue.ToString()))
                {
                    dv.RowFilter = string.Empty;
                }
                else
                {
                    string unit = UnitNoComboBox.SelectedValue.ToString().Replace("'", "''");
                    dv.RowFilter = $"[UNIT NO] LIKE '%{unit}%'";
                }

                PropertyManagementDataGrid.ItemsSource = dv;
                PropertyManagementDataGrid.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error filtering tenants: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}
