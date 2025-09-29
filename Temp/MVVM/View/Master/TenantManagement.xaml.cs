using AccountsLibrary.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace AccountsPro.MVVM.View.Master
{
    public partial class TenantManagement : Window
    {
        public TenantManagement()
        {
            InitializeComponent();
            LoadMonthsFromDatabase();
            LoadArrearsFromDatabase();
        }

        private void LoadMonthsFromDatabase()
        {
            var months = new List<string>();

            try
            {
                using var conn = new SqlConnection(Settings.Default.strConnetionString);
                conn.Open();

                using var cmd = new SqlCommand(@"
                    SELECT DISTINCT CAST([DATAFORMONTH] AS NVARCHAR(20)) AS MonthName
                    FROM [dbo].[UploadsTenantRent]
                    WHERE [DATAFORMONTH] IS NOT NULL
                    ORDER BY MonthName ASC;", conn);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    months.Add(reader.GetString(0));
                }

                MonthsComboBox.ItemsSource = months;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading months: " + ex.Message);
            }
        }

        //Test
        //ArrearsAgingComboBox
        private void LoadArrearsFromDatabase()
        {

            try
            {
                using var conn = new SqlConnection(Settings.Default.strConnetionString);
                conn.Open();

                string agingText = ArrearsAgingComboBox.SelectedItem?.ToString();
                int agingLimit = 30; //default

                if (!string.IsNullOrEmpty(agingText) && int.TryParse(agingText, out int parsed))
                    agingLimit = parsed;

                    string query = @"
                        SELECT [UNIT NO], [TENANT NAME], [EXPECTED RENT], [RENT RECEIVED], [ARREAS],
                               DATEDIFF(DAY, CAST([DATAFORMONTH] AS DATE), GETDATE()) AS AgingDays,
                               DATAFORMONTH
                        FROM [dbo].[UploadsTenantRent]
                        WHERE DATEDIFF(DAY, CAST([DATAFORMONTH] AS DATE), GETDATE()) <= @AgingLimit
                          AND [ARREAS] > 0
                        ORDER BY AgingDays DESC";


                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AgingLimit", agingLimit);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                TenantsDataGrid.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading arrears aging : " + ex.Message);
            }
        
        
        }



        //public string GetSelectedMonth()
        //{
        //    return MonthsComboBox.SelectedItem as string;
        //}
    }
}