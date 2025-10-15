using AccountsLibrary;
using AccountsLibrary.Properties;
using ControlzEx.Standard;
using Humanizer;
using Microsoft.Data.SqlClient;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using OfficeOpenXml;
using PdfSharp.Drawing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace AccountsPro.MVVM.View.PropertyManagement
{
    public partial class UtilityCostAllocation : UserControl
    {
        private bool _suppressSelectionChanged = false;
        private string TenantName = "";
        private int UTR_ID = 0;
        private DateTime chosenMonth = DateTime.Now;
        public UtilityCostAllocation()
        {
            InitializeComponent();
            Loaded += UtilityCostAllocation_Loaded;

            MonthsComboBox.SelectionChanged += Filter_Changed;
            UnitNoComboBox.SelectionChanged += Filter_Changed;
            TenantsDataGrid.SelectionChanged += TenantsDataGrid_SelectionChanged;

            ExpectedRentTextBox.TextChanged += Field_TextChanged;
            WaterCostTextBox.TextChanged += Field_TextChanged;
            SanitationCostTextBox.TextChanged += Field_TextChanged;
        }

        private void UtilityCostAllocation_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateMonths();
            PopulateUnits();
            LoadTenantsData();
        }

        private void PopulateMonths()
        {
            try
            {
                MonthsComboBox.Items.Clear();
                MonthsComboBox.Items.Add("All");
                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    conn.Open();
                    string q = @"SELECT DISTINCT 
                                    DATEPART(YEAR, TRY_CONVERT(date, DATAFORMONTH)) AS Yr,
                                    DATEPART(MONTH, TRY_CONVERT(date, DATAFORMONTH)) AS Mn
                                 FROM UploadsTenantRent
                                 WHERE TRY_CONVERT(date, DATAFORMONTH) IS NOT NULL AND COALESCE(Vchrid,0)=0
                                 ORDER BY Yr DESC, Mn DESC";
                    using (SqlCommand cmd = new SqlCommand(q, conn))
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            int yr = rdr.GetInt32(0);
                            int mn = rdr.GetInt32(1);
                            MonthsComboBox.Items.Add(new DateTime(yr, mn, 1).ToString("MMM yyyy", CultureInfo.InvariantCulture));
                        }
                    }
                }
                MonthsComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading months: {ex.Message}");
            }
        }

        private void PopulateUnits()
        {
            try
            {
                var units = new List<string> { "All" };
                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    conn.Open();
                    string q = "SELECT DISTINCT [UNIT NO] FROM UploadsTenantRent WHERE COALESCE(Vchrid,0)=0 ORDER BY [UNIT NO]";
                    using (SqlCommand cmd = new SqlCommand(q, conn))
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                            if (!rdr.IsDBNull(0)) units.Add(rdr.GetString(0));
                    }
                }
                UnitNoComboBox.ItemsSource = units;
                UnitNoComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading units: {ex.Message}");
            }
        }

        private void Filter_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (_suppressSelectionChanged) return;
            LoadTenantsData();
        }

        private void LoadTenantsData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    conn.Open();
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;

                    var whereParts = new List<string>();

                    if (MonthsComboBox.SelectedItem != null && MonthsComboBox.SelectedItem.ToString() != "All")
                    {
                        if (DateTime.TryParseExact(MonthsComboBox.SelectedItem.ToString(), "MMM yyyy",
                            CultureInfo.InvariantCulture, DateTimeStyles.None, out chosenMonth))
                        {
                            whereParts.Add("YEAR(TRY_CONVERT(date, DATAFORMONTH))=@yr AND MONTH(TRY_CONVERT(date, DATAFORMONTH))=@mn");
                            cmd.Parameters.AddWithValue("@yr", chosenMonth.Year);
                            cmd.Parameters.AddWithValue("@mn", chosenMonth.Month);
                        }
                    }

                    if (UnitNoComboBox.SelectedItem != null && UnitNoComboBox.SelectedItem.ToString() != "All")
                    {
                        whereParts.Add("[UNIT NO]=@UnitNo");
                        cmd.Parameters.AddWithValue("@UnitNo", UnitNoComboBox.SelectedItem.ToString());
                    }

                    string whereClause = whereParts.Count > 0 ? "WHERE " + string.Join(" AND ", whereParts) + " AND COALESCE(Vchrid,0)=0 " : " WHERE COALESCE(Vchrid,0)=0 ";
                    cmd.CommandText = $@"
                        SELECT [UTR_ID],[UNIT NO],[TENANT NAME],[EXPECTED RENT] AS [ACTUAL CHARGES],[WATER],SANITATION, ARREAS AS ARREARS,
                               [RENT RECEIVED],DEPOSIT,[TOTAL MONEY PAID],DATAFORMONTH,UPLOADTIME,[EXTRA COMMENTS]
                        FROM UploadsTenantRent
                        {whereClause}
                        ORDER BY DATAFORMONTH DESC,[UNIT NO]";

                    var dt = new System.Data.DataTable();
                    new SqlDataAdapter(cmd).Fill(dt);
                    TenantsDataGrid.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tenant data: {ex.Message}");
            }
        }

        private void TenantsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (_suppressSelectionChanged) return;
                if (TenantsDataGrid.SelectedItem == null) return;
                if (!(TenantsDataGrid.SelectedItem is DataRowView drv))
                    return;
                _suppressSelectionChanged = true;
                // Check column existence before accessing
                if (drv.DataView.Table.Columns.Contains("UTR_ID"))
                {
                    UTR_ID = int.TryParse(drv["UTR_ID"]?.ToString(), out int idVal) ? idVal : 0;
                }
                else
                {
                    // Fallback for Compare or test tables without UTR_ID
                    UTR_ID = 0;
                }
                // Safe access helpers
                string GetValue(string col) =>
                    drv.DataView.Table.Columns.Contains(col) ? drv[col]?.ToString() ?? "" : "";
                MonthsComboBox.SelectedItem = GetValue("DATAFORMONTH");
                UnitNoComboBox.SelectedItem = GetValue("UNIT NO");
                TenantName = GetValue("TENANT NAME");
                ExpectedRentTextBox.Text = GetValue("ACTUAL CHARGES");
                WaterCostTextBox.Text = GetValue("WATER");
                SanitationCostTextBox.Text = GetValue("SANITATION");
                ArrearsTextBox.Text = GetValue("ARREARS");
                RentReceivedTextBox.Text = GetValue("RENT RECEIVED");
                DepositTextBox.Text = GetValue("DEPOSIT");
                ExtraComments.Text = GetValue("EXTRA COMMENTS");
                ComputeTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error selecting record: {ex.Message}");
            }
            finally
            {
                _suppressSelectionChanged = false;
            }
        }

        private void Field_TextChanged(object sender, TextChangedEventArgs e) => ComputeTotal();

        private void ComputeTotal()
        {
            decimal.TryParse(ExpectedRentTextBox.Text, out decimal a);
            decimal.TryParse(WaterCostTextBox.Text, out decimal b);
            decimal.TryParse(SanitationCostTextBox.Text, out decimal c);
            TotalPerUnitTextBox.Text = (a + b + c).ToString("0.00");
        }

        private decimal GetDecimal(System.Windows.Controls.TextBox box)
        {
            return decimal.TryParse(box.Text, out decimal val) ? val : 0;
        }

        // Helper: safely read decimal from DataRow
        private decimal SafeGetDecimalFromRow(DataRow dr, string col)
        {
            if (dr.Table.Columns.Contains(col) && dr[col] != DBNull.Value)
            {
                decimal.TryParse(dr[col].ToString(), out decimal result);
                return result;
            }
            return 0m;
        }

        // Helper: safely read string from DataRow
        private string SafeGetStringFromRow(DataRow dr, string col)
        {
            if (dr.Table.Columns.Contains(col) && dr[col] != DBNull.Value)
                return dr[col].ToString();
            return string.Empty;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UnitNoComboBox.SelectedItem == null || UnitNoComboBox.SelectedItem.ToString() == "All")
                {
                    MessageBox.Show("Please select a valid Unit No.");
                    return;
                }

                if (MonthsComboBox.SelectedItem == null || MonthsComboBox.SelectedItem.ToString() == "All")
                {
                    MessageBox.Show("Please select a valid Month.");
                    return;
                }

                if (!DateTime.TryParseExact(MonthsComboBox.SelectedItem.ToString(), "MMM yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out chosenMonth))
                {
                    MessageBox.Show("Invalid month format.");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    conn.Open();
                    string sql = @"
                        INSERT INTO UploadsTenantRent
                        ([UNIT NO],[EXPECTED RENT],[WATER],SANITATION,ARREAS,[RENT RECEIVED],DEPOSIT,DATAFORMONTH,UPLOADTIME)
                        VALUES(@UnitNo,@Rent,@Water,@Sanitation,@Arrears,@Received,@Deposit,@Month,GETDATE())";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@UnitNo", UnitNoComboBox.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Rent", GetDecimal(ExpectedRentTextBox));
                        cmd.Parameters.AddWithValue("@Water", GetDecimal(WaterCostTextBox));
                        cmd.Parameters.AddWithValue("@Sanitation", GetDecimal(SanitationCostTextBox));
                        cmd.Parameters.AddWithValue("@Arrears", GetDecimal(ArrearsTextBox));
                        cmd.Parameters.AddWithValue("@Received", GetDecimal(RentReceivedTextBox));
                        cmd.Parameters.AddWithValue("@Deposit", GetDecimal(DepositTextBox));
                        cmd.Parameters.AddWithValue("@Month", new DateTime(chosenMonth.Year, chosenMonth.Month, 1));
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Record saved successfully.");
                LoadTenantsData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving record: {ex.Message}");
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UnitNoComboBox.SelectedItem == null || UnitNoComboBox.SelectedItem.ToString() == "All")
                {
                    MessageBox.Show("Please select a Unit No before updating.");
                    return;
                }

                if (MonthsComboBox.SelectedItem == null || MonthsComboBox.SelectedItem.ToString() == "All")
                {
                    MessageBox.Show("Please select a Month before updating.");
                    return;
                }

                if (!DateTime.TryParseExact(MonthsComboBox.SelectedItem.ToString(), "MMM yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out chosenMonth))
                {
                    MessageBox.Show("Invalid month selected.");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    conn.Open();
                    string sql = @"
                        UPDATE UploadsTenantRent
                        SET [EXPECTED RENT]=@Rent,[WATER]=@Water,SANITATION=@Sanitation,ARREAS=@Arrears,[RENT RECEIVED]=@Received,
                            DEPOSIT=@Deposit,UPLOADTIME=GETDATE(),
                            [TOTAL MONEY PAID] = @TotalMoneyPaid
                            WHERE [UNIT NO]=@UnitNo
                          AND YEAR(TRY_CONVERT(date,DATAFORMONTH))=@yr
                          AND MONTH(TRY_CONVERT(date,DATAFORMONTH))=@mn AND COALESCE(Vchrid,0)=0 ";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Rent", GetDecimal(ExpectedRentTextBox));
                        cmd.Parameters.AddWithValue("@Water", GetDecimal(WaterCostTextBox));
                        cmd.Parameters.AddWithValue("@Sanitation", GetDecimal(SanitationCostTextBox));
                        cmd.Parameters.AddWithValue("@Arrears", GetDecimal(ArrearsTextBox));
                        cmd.Parameters.AddWithValue("@Received", GetDecimal(RentReceivedTextBox));
                        cmd.Parameters.AddWithValue("@Deposit", GetDecimal(DepositTextBox));
                        cmd.Parameters.AddWithValue("@TotalMoneyPaid", GetDecimal(TotalPerUnitTextBox));
                        cmd.Parameters.AddWithValue("@UnitNo", UnitNoComboBox.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@yr", chosenMonth.Year);
                        cmd.Parameters.AddWithValue("@mn", chosenMonth.Month);

                        int rows = cmd.ExecuteNonQuery();
                        MessageBox.Show(rows > 0 ? "Record updated successfully." : "No record found to update.");
                    }
                }

                LoadTenantsData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating record: {ex.Message}");
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            MonthsComboBox.SelectedIndex = -1;
            UnitNoComboBox.SelectedIndex = -1;
            ExpectedRentTextBox.Clear();
            WaterCostTextBox.Clear();
            SanitationCostTextBox.Clear();
            RentReceivedTextBox.Clear();
            DepositTextBox.Clear();
            TotalPerUnitTextBox.Text = "0.00";
            IsPaidCheckBox.IsChecked = false;
        }

        private void NumericOnly(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[0-9.]+$");
        }


        private void UtilityPerUnitBtn_Click(object sender, RoutedEventArgs e)
        {
            var win = new UtilityPerUnitCostCharges();
            win.Show();
        }

        private void EntityLoadMethod(VchrTranLog vchrTranLog,decimal TotalPerUnit, int iVchrNo,int? iVchrTranId, string VoucherType,string NarrationBox)
        {
            vchrTranLog.VchrDate = DateTime.Now;//(DateTime)DatePickBox.SelectedDate;
            vchrTranLog.VchrTranId = iVchrTranId;
            vchrTranLog.VchrNo = iVchrNo;
            vchrTranLog.VchrAmount = TotalPerUnit;
            vchrTranLog.VchrType = VoucherType;
            vchrTranLog.VchrName = VoucherType;
            vchrTranLog.SalesManId = Settings.Default.intLogOnId;
            vchrTranLog.FromAccountId = 40;//TENANT MONEY PIAD = 40
            vchrTranLog.ToAccountId = 26;//ACCOUNTS RECEIVABLE = 26
            vchrTranLog.TranDate = DateTime.Now.Date;
            vchrTranLog.UserId = Settings.Default.intLogOnId;
            vchrTranLog.Narration = NarrationBox;
            vchrTranLog.FK_BranchId = Settings.Default.iBranchId;
        }

        private void ProcessPrepaymentVouchers()
        {
            var AllUnProcessedRecsDT = new System.Data.DataTable();
            var SingleNextMonthRecsDT = new System.Data.DataTable();

            using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
            {
                string query = "SELECT [UTR_ID],[UNIT NO],[TENANT NAME],COALESCE([EXPECTED RENT],0) AS [EXPECTED RENT],COALESCE([WATER],0) AS [WATER],COALESCE([SANITATION],0) AS [SANITATION],COALESCE([RENT RECEIVED],0) AS [RENT RECEIVED],COALESCE([DEPOSIT],0) AS [DEPOSIT],COALESCE([ARREAS PAID (RENT)],0) AS [ARREAS PAID (RENT)],COALESCE([ARREAS PAID(UTILITIES)],0) AS [ARREAS PAID(UTILITIES)],COALESCE([PREPAYMENT],0) AS [PREPAYMENT],COALESCE([TOTAL MONEY PAID],0) AS [TOTAL MONEY PAID],COALESCE([ARREAS B/F],0) AS [ARREAS B/F],COALESCE([ARREAS],0) AS [ARREAS],COALESCE([ARREAS C/F],0) AS [ARREAS C/F],[BANK PAID],[EXTRA COMMENTS],[FILE PATH],[UPLOADTIME],[DATAFORMONTH],[IsVchrProcesd],[GAS],[ELECTRICITY],[INTERNET],[TV],[LAUNDRY],[MISC] " +
                    " FROM UploadsTenantRent WHERE " +
                    //"([EXPECTED RENT] + WATER + SANITATION) < ([RENT RECEIVED]+ [ARREAS PAID (RENT)] + [ARREAS PAID(UTILITIES)] + [PREPAYMENT] + [DEPOSIT]) AND " +
                    " [TENANT NAME] NOT LIKE '%VACANT%' AND Coalesce(IsVchrProcesd,0) = 0 AND COALESCE(Vchrid,0)=0";//AND COALESCE(Vchrid,0)=0
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(AllUnProcessedRecsDT);
                conn.Close();
            }

            foreach (System.Data.DataRow dr in AllUnProcessedRecsDT.Rows)
            {
                //NarrationBox = dr["UNIT NO"].ToString() + "--" + dr["TENANT NAME"].ToString() + "--" + dr["EXTRA COMMENTS"].ToString();
                decimal nextMonthCalc = 0m;
                //decimal rentCalc = decimal.Parse(dr["EXPECTED RENT"].ToString()) - (decimal.Parse(dr["RENT RECEIVED"].ToString())+ decimal.Parse(dr["DEPOSIT"].ToString()));

                decimal TOTAL_CHARGES = decimal.Parse(dr["EXPECTED RENT"].ToString()) + decimal.Parse(dr["WATER"].ToString()) + decimal.Parse(dr["SANITATION"].ToString());

                decimal TOTAL_MONEY_PAID = decimal.Parse(dr["RENT RECEIVED"].ToString()) + decimal.Parse(dr["ARREAS PAID (RENT)"].ToString())+ decimal.Parse(dr["ARREAS PAID(UTILITIES)"].ToString()) +
                                           decimal.Parse(dr["PREPAYMENT"].ToString()) + decimal.Parse(dr["DEPOSIT"].ToString());

                decimal TOTAL_DUE = decimal.Parse(dr["ARREAS B/F"].ToString()) + TOTAL_CHARGES;

                decimal ARREARS = TOTAL_DUE - TOTAL_MONEY_PAID;
                
                decimal ARREARS_CF = ARREARS;

                DateTime.TryParseExact(dr["DATAFORMONTH"].ToString(), "MMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out chosenMonth);


                if (ARREARS < 0)// Add next month prepayment data
                {
                    using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                    {
                        string query = "SELECT [UTR_ID],[UNIT NO],[TENANT NAME],COALESCE([EXPECTED RENT],0) AS [EXPECTED RENT],COALESCE([WATER],0) AS [WATER],COALESCE([SANITATION],0) AS [SANITATION],COALESCE([RENT RECEIVED],0) AS [RENT RECEIVED],COALESCE([DEPOSIT],0) AS [DEPOSIT],COALESCE([ARREAS PAID (RENT)],0) AS [ARREAS PAID (RENT)],COALESCE([ARREAS PAID(UTILITIES)],0) AS [ARREAS PAID(UTILITIES)],COALESCE([PREPAYMENT],0) AS [PREPAYMENT],COALESCE([TOTAL MONEY PAID],0) AS [TOTAL MONEY PAID],COALESCE([ARREAS B/F],0) AS [ARREAS B/F],COALESCE([ARREAS],0) AS [ARREAS],COALESCE([ARREAS C/F],0) AS [ARREAS C/F],[BANK PAID],[EXTRA COMMENTS],[FILE PATH],[UPLOADTIME],[DATAFORMONTH],[IsVchrProcesd],[GAS],[ELECTRICITY],[INTERNET],[TV],[LAUNDRY],[MISC] " +
                            " FROM UploadsTenantRent WHERE [UNIT NO] = '" + dr["UNIT NO"].ToString() + "' AND [DATAFORMONTH] LIKE '" + new DateTime(chosenMonth.Year, chosenMonth.Month + 1, 1).ToString("MMM yyyy") + "' AND Coalesce(IsVchrProcesd,0) = 0 ";//AND COALESCE(Vchrid,0)=0
                        SqlCommand cmd = new SqlCommand(query, conn);
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(query, conn);
                        da.Fill(SingleNextMonthRecsDT);
                        conn.Close();
                    }

                    if (SingleNextMonthRecsDT.Rows.Count > 0)//IF RECORD EXIST FOR NEXT MONTH THEN UPDATE IT HERE
                    {
                        MessageBox.Show("Next Month Prepayment data already found for unit=" + dr["UNIT NO"].ToString()+ " TENANT_NAME=" + dr["TENANT NAME"].ToString() +" Paid="+ TOTAL_MONEY_PAID);
                    }
                    else//IF NO RECORD EXIST FOR NEXT MONTH THEN INSERT IT HERE
                    {
                        using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                        {
                            conn.Open();
                            //string sql = @"
                            //    INSERT INTO UploadsTenantRent
                            //    ([UNIT NO],[EXPECTED RENT],[WATER],SANITATION,[RENT RECEIVED],DEPOSIT,DATAFORMONTH,UPLOADTIME)
                            //    VALUES(@UnitNo,@Rent,@Water,@Sanitation,@Received,@Deposit,@Month,GETDATE())";
                            string sql = @"
                        INSERT INTO UploadsTenantRent
                        ([UNIT NO],[TENANT NAME],[EXPECTED RENT],[WATER],[SANITATION],[RENT RECEIVED],[DEPOSIT],[ARREAS PAID (RENT)],[ARREAS PAID(UTILITIES)],[PREPAYMENT],
                            [TOTAL MONEY PAID],[ARREAS B/F],[ARREAS],[ARREAS C/F],[BANK PAID],[EXTRA COMMENTS],[FILE PATH],[UPLOADTIME],[DATAFORMONTH],[IsVchrProcesd],[GAS],
	                        [ELECTRICITY],[INTERNET],[TV],[LAUNDRY],[MISC])
                        VALUES(@UnitNo,@TenantName,@Rent,@Water,@Sanitation,@Received,@Deposit,@RentArrears,
                                @PaidArrears,@Prepayment,@TotalPaid,@ArrearsBF,@Arrears,@ArrearsCF,@BankPaid,@ExtraComments,@filePath,@uploadTime
                                ,@Month,0,@Gas,@Electric,@internet,@TV,@laundry,@misc)";


                            using (SqlCommand cmd = new SqlCommand(sql, conn))
                            {
                                cmd.Parameters.AddWithValue("@UnitNo", dr["UNIT NO"].ToString());
                                cmd.Parameters.AddWithValue("@TenantName", dr["TENANT NAME"].ToString());
                                cmd.Parameters.AddWithValue("@Rent", decimal.Parse(dr["EXPECTED RENT"].ToString()));
                                cmd.Parameters.AddWithValue("@Water", decimal.Parse(dr["WATER"].ToString()));
                                cmd.Parameters.AddWithValue("@Sanitation", decimal.Parse(dr["SANITATION"].ToString()));
                                cmd.Parameters.AddWithValue("@Received", 0m);//dr["RENT RECEIVED"].ToString()
                                cmd.Parameters.AddWithValue("@Deposit", 0m);//dr["DEPOSIT"].ToString()
                                cmd.Parameters.AddWithValue("@RentArrears", 0m);//dr["ARREAS PAID (RENT)"].ToString()
                                cmd.Parameters.AddWithValue("@PaidArrears", 0m);//dr["ARREAS PAID(UTILITIES)"].ToString()
                                cmd.Parameters.AddWithValue("@Prepayment", Math.Abs(ARREARS_CF));//dr["PREPAYMENT"].ToString()
                                cmd.Parameters.AddWithValue("@TotalPaid", 0m);
                                cmd.Parameters.AddWithValue("@ArrearsBF", 0m);//dr["ARREAS B/F"].ToString()
                                cmd.Parameters.AddWithValue("@Arrears", 0m);//dr["ARREAS"].ToString()
                                cmd.Parameters.AddWithValue("@ArrearsCF", 0m);//dr["ARREAS C/F"].ToString()
                                cmd.Parameters.AddWithValue("@BankPaid", dr["BANK PAID"].ToString());
                                cmd.Parameters.AddWithValue("@ExtraComments", dr["EXTRA COMMENTS"].ToString() + "--ProcessUploaded");
                                cmd.Parameters.AddWithValue("@filePath", dr["FILE PATH"].ToString());
                                cmd.Parameters.AddWithValue("@uploadTime", DateTime.Now);
                                cmd.Parameters.AddWithValue("@Month", new DateTime(chosenMonth.Year, chosenMonth.Month + 1, 1).ToString("MMM yyyy")); //dr["DATAFORMONTH"].ToString()//new DateTime(chosenMonth.Year, chosenMonth.Month + 1, 1)
                                cmd.Parameters.AddWithValue("@Gas", 0m);
                                cmd.Parameters.AddWithValue("@Electric", 0m);
                                cmd.Parameters.AddWithValue("@internet", 0m);
                                cmd.Parameters.AddWithValue("@TV", 0m);
                                cmd.Parameters.AddWithValue("@laundry", 0m);
                                cmd.Parameters.AddWithValue("@misc", 0m);

                                //cmd.Parameters.AddWithValue("@Month", new DateTime(chosenMonth.Year, chosenMonth.Month+1, 1));
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
                else //There are some arrears here
                {
                        using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                        {
                            string query = "SELECT [UTR_ID],[UNIT NO],[TENANT NAME],COALESCE([EXPECTED RENT],0) AS [EXPECTED RENT],COALESCE([WATER],0) AS [WATER],COALESCE([SANITATION],0) AS [SANITATION],COALESCE([RENT RECEIVED],0) AS [RENT RECEIVED],COALESCE([DEPOSIT],0) AS [DEPOSIT],COALESCE([ARREAS PAID (RENT)],0) AS [ARREAS PAID (RENT)],COALESCE([ARREAS PAID(UTILITIES)],0) AS [ARREAS PAID(UTILITIES)],COALESCE([PREPAYMENT],0) AS [PREPAYMENT],COALESCE([TOTAL MONEY PAID],0) AS [TOTAL MONEY PAID],COALESCE([ARREAS B/F],0) AS [ARREAS B/F],COALESCE([ARREAS],0) AS [ARREAS],COALESCE([ARREAS C/F],0) AS [ARREAS C/F],[BANK PAID],[EXTRA COMMENTS],[FILE PATH],[UPLOADTIME],[DATAFORMONTH],[IsVchrProcesd],[GAS],[ELECTRICITY],[INTERNET],[TV],[LAUNDRY],[MISC] " +
                                " FROM UploadsTenantRent WHERE [UNIT NO] = '" + dr["UNIT NO"].ToString() + "' AND [DATAFORMONTH] LIKE '" + new DateTime(chosenMonth.Year, chosenMonth.Month + 1, 1).ToString("MMM yyyy") + "' AND Coalesce(IsVchrProcesd,0) = 0 ";//AND COALESCE(Vchrid,0)=0
                            SqlCommand cmd = new SqlCommand(query, conn);
                            conn.Open();
                            SqlDataAdapter da = new SqlDataAdapter(query, conn);
                            da.Fill(SingleNextMonthRecsDT);
                            conn.Close();
                        }

                        if (SingleNextMonthRecsDT.Rows.Count > 0)//IF RECORD EXIST FOR NEXT MONTH THEN UPDATE IT HERE
                        {
                            MessageBox.Show("Next Month Prepayment data already found for unit=" + dr["UNIT NO"].ToString() + " TENANT_NAME=" + dr["TENANT NAME"].ToString() + " Paid=" + TOTAL_MONEY_PAID);
                        }
                        else//IF NO RECORD EXIST FOR NEXT MONTH THEN INSERT IT HERE
                        {
                            using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                            {
                                conn.Open();
                                //string sql = @"
                                //    INSERT INTO UploadsTenantRent
                                //    ([UNIT NO],[EXPECTED RENT],[WATER],SANITATION,[RENT RECEIVED],DEPOSIT,DATAFORMONTH,UPLOADTIME)
                                //    VALUES(@UnitNo,@Rent,@Water,@Sanitation,@Received,@Deposit,@Month,GETDATE())";
                                string sql = @"
                        INSERT INTO UploadsTenantRent
                        ([UNIT NO],[TENANT NAME],[EXPECTED RENT],[WATER],[SANITATION],[RENT RECEIVED],[DEPOSIT],[ARREAS PAID (RENT)],[ARREAS PAID(UTILITIES)],[PREPAYMENT],
                            [TOTAL MONEY PAID],[ARREAS B/F],[ARREAS],[ARREAS C/F],[BANK PAID],[EXTRA COMMENTS],[FILE PATH],[UPLOADTIME],[DATAFORMONTH],[IsVchrProcesd],[GAS],
	                        [ELECTRICITY],[INTERNET],[TV],[LAUNDRY],[MISC])
                        VALUES(@UnitNo,@TenantName,@Rent,@Water,@Sanitation,@Received,@Deposit,@RentArrears,
                                @PaidArrears,@Prepayment,@TotalPaid,@ArrearsBF,@Arrears,@ArrearsCF,@BankPaid,@ExtraComments,@filePath,@uploadTime
                                ,@Month,0,@Gas,@Electric,@internet,@TV,@laundry,@misc)";


                                using (SqlCommand cmd = new SqlCommand(sql, conn))
                                {
                                    cmd.Parameters.AddWithValue("@UnitNo", dr["UNIT NO"].ToString());
                                    cmd.Parameters.AddWithValue("@TenantName", dr["TENANT NAME"].ToString());
                                    cmd.Parameters.AddWithValue("@Rent", decimal.Parse(dr["EXPECTED RENT"].ToString()));
                                    cmd.Parameters.AddWithValue("@Water", decimal.Parse(dr["WATER"].ToString()));
                                    cmd.Parameters.AddWithValue("@Sanitation", decimal.Parse(dr["SANITATION"].ToString()));
                                    cmd.Parameters.AddWithValue("@Received", 0m);//dr["RENT RECEIVED"].ToString()
                                    cmd.Parameters.AddWithValue("@Deposit", 0m);//dr["DEPOSIT"].ToString()
                                    cmd.Parameters.AddWithValue("@RentArrears", 0m);//dr["ARREAS PAID (RENT)"].ToString()
                                    cmd.Parameters.AddWithValue("@PaidArrears", 0m);//dr["ARREAS PAID(UTILITIES)"].ToString()
                                    cmd.Parameters.AddWithValue("@Prepayment", 0m);//dr["PREPAYMENT"].ToString()
                                    cmd.Parameters.AddWithValue("@TotalPaid", 0m);
                                    cmd.Parameters.AddWithValue("@ArrearsBF", Math.Abs(ARREARS_CF));//dr["ARREAS B/F"].ToString()
                                    cmd.Parameters.AddWithValue("@Arrears", Math.Abs(ARREARS_CF));//dr["ARREAS"].ToString()
                                    cmd.Parameters.AddWithValue("@ArrearsCF", 0m);//dr["ARREAS C/F"].ToString()
                                    cmd.Parameters.AddWithValue("@BankPaid", dr["BANK PAID"].ToString());
                                    cmd.Parameters.AddWithValue("@ExtraComments", dr["EXTRA COMMENTS"].ToString() + "--ProcessUploaded");
                                    cmd.Parameters.AddWithValue("@filePath", dr["FILE PATH"].ToString());
                                    cmd.Parameters.AddWithValue("@uploadTime", DateTime.Now);
                                    cmd.Parameters.AddWithValue("@Month", new DateTime(chosenMonth.Year, chosenMonth.Month + 1, 1).ToString("MMM yyyy")); //dr["DATAFORMONTH"].ToString()//new DateTime(chosenMonth.Year, chosenMonth.Month + 1, 1)
                                    cmd.Parameters.AddWithValue("@Gas", 0m);
                                    cmd.Parameters.AddWithValue("@Electric", 0m);
                                    cmd.Parameters.AddWithValue("@internet", 0m);
                                    cmd.Parameters.AddWithValue("@TV", 0m);
                                    cmd.Parameters.AddWithValue("@laundry", 0m);
                                    cmd.Parameters.AddWithValue("@misc", 0m);

                                    //cmd.Parameters.AddWithValue("@Month", new DateTime(chosenMonth.Year, chosenMonth.Month+1, 1));
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                    }

                //Update this voucher if processed
                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    conn.Open();
                    string sql = @"
                        UPDATE UploadsTenantRent
                        SET IsVchrProcesd=1
                        WHERE [UTR_ID]=@UTR_ID";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@UTR_ID", int.Parse(dr["UTR_ID"].ToString()));
                        int rows = cmd.ExecuteNonQuery();
                    }
                }


            }
            LoadTenantsData();
            MessageBox.Show("Total (" + AllUnProcessedRecsDT.Rows.Count + ") Records Processed ");
        }

        private void ProcessBtn_Click(object sender, RoutedEventArgs e)
        {
            //Process Button
            //this is a process button
            //process Button that shows total or changes state of DataGrid
            //Also, do calculation according to Unit prices set, which are multiplied by units used that will be added
            //all of the above functions will work after the process button is clicked
            //Prepayment for the next month will be added

            MessageBoxResult mrs = MessageBox.Show("Before proceeding please make sure existing records have correct data!" + Environment.NewLine +"It will effect the Balance Sheet?","Confirm to proceed Accountable Process?", MessageBoxButton.YesNo);

            if (mrs == MessageBoxResult.No)
            {
                MessageBox.Show("Accountable process Not started by user!");
                return; }


            if (AniHelper.dBContext == null)
                AniHelper.dBContext = new DBContext();

            string VoucherType = "Journal Voucher";

            int? iVchrTranId = 0;
            string NarrationBox = UnitNoComboBox.SelectedValue+"--"+ TenantName + "--"+ ExtraComments.Text;
            int FromAccountId = 40, ToAccountId=26;
            decimal amount = Decimal.Parse(TotalPerUnitTextBox.Text);
            decimal dmlClosingBalance=0m;
            bool blCredit = false;
            String lblTransactionType = "";
            var AllUnProcessedRecsDT = new System.Data.DataTable();

            (dmlClosingBalance, lblTransactionType) = AniHelper.AccountBalanceFindMethod(0m, 0m, FromAccountId.ToString(), chosenMonth.AddHours(24.0).AddSeconds(-1.0));
            
            if (dmlClosingBalance < 0m)
                blCredit = true;

            //ProcessPrepaymentVouchers();

            using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
            {
                string query = "SELECT * FROM UploadsTenantRent where COALESCE(Vchrid,0)=0 AND Coalesce(IsVchrProcesd,0) = 1";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(query, conn);                
                da.Fill(AllUnProcessedRecsDT);
                conn.Close();
            }

            if (AniHelper.vchrTranLog == null)
                AniHelper.vchrTranLog = new VchrTranLog();

            AniHelper.vchrTranLog.SalesManId = 1;
            AniHelper.vchrTranLog.UserId = 1;


            foreach (System.Data.DataRow dr in AllUnProcessedRecsDT.Rows)
            {
                int iVchrNo = (AniHelper.dBContext.VchrTransaction(new
                {
                    VchrType = VoucherType,
                    FK_BranchId = Settings.Default.iBranchId
                }).Max((VchrTranLog x) => x.VchrNo) + 1) ?? 1;

                NarrationBox = dr["UNIT NO"].ToString() + "--" + dr["TENANT NAME"].ToString() + "--" + dr["EXTRA COMMENTS"].ToString();

                amount = Decimal.Parse(dr["TOTAL MONEY PAID"].ToString());
                EntityLoadMethod(AniHelper.vchrTranLog, amount, iVchrNo, iVchrTranId, VoucherType, NarrationBox);

                AniHelper.vchrTranLog.VchrTranId = null;
                //AniHelper.vchrTranLog = (from x in AniHelper.dBContext.VchrTransaction()
                //                         where x.VchrType == VoucherType && x.VchrNo == iVchrNo
                //                         select x).FirstOrDefault();



                if (AshproCommon.InsertToDatabaseObj(AniHelper.vchrTranLog, "VchrTranLog"))
                {
                    iVchrTranId = (from x in AniHelper.dBContext.VchrTransaction()
                                   where x.VchrNo == AniHelper.vchrTranLog.VchrNo && x.VchrType == AniHelper.vchrTranLog.VchrType
                                   select x).FirstOrDefault().VchrTranId;

                    

                    if (blCredit)
                    {
                        POSHelper.TranLogMethod(amount, 0m, FromAccountId, ToAccountId, iVchrTranId, iVchrNo, AniHelper.vchrTranLog.VchrType, AniHelper.vchrTranLog.UserId, "Main", AniHelper.vchrTranLog.VchrType, chosenMonth.ToString());
                        POSHelper.TranLogMethod(0m, amount, ToAccountId, FromAccountId, iVchrTranId, iVchrNo, AniHelper.vchrTranLog.VchrType, AniHelper.vchrTranLog.UserId, "Sub", AniHelper.vchrTranLog.VchrType, chosenMonth.ToString());
                    }
                    else
                    {
                        POSHelper.TranLogMethod(0m, amount, FromAccountId, ToAccountId, iVchrTranId, iVchrNo, AniHelper.vchrTranLog.VchrType, AniHelper.vchrTranLog.UserId, "Main", AniHelper.vchrTranLog.VchrType, chosenMonth.ToString());
                        POSHelper.TranLogMethod(amount, 0m, ToAccountId, FromAccountId, iVchrTranId, iVchrNo, AniHelper.vchrTranLog.VchrType, AniHelper.vchrTranLog.UserId, "Sub", AniHelper.vchrTranLog.VchrType, chosenMonth.ToString());
                    }
                    //btnClear_Click(null, null);

                    using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                    {
                        conn.Open();
                        string sql = @"
                        UPDATE UploadsTenantRent
                        SET VchrId=@VchrId
                        WHERE [UTR_ID]=@UTR_ID";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@VchrId", iVchrTranId);
                            cmd.Parameters.AddWithValue("@UTR_ID", int.Parse(dr["UTR_ID"].ToString()));
                            int rows = cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            LoadTenantsData();
            MessageBox.Show("Total ("+ AllUnProcessedRecsDT.Rows.Count + ") Records Processed ");
        }


        private void ImportExcelBtn_Click(object sender, RoutedEventArgs e)
        {
            //this is an import excel button,
            //user can upload from excel, only values which are:
            //WaterUsed, SanitationUsed, GasUsed, ElectricityUsed, InternetUsed, TVUsed, LaundryUsed, MiscUsed
            MessageBoxResult mrs = MessageBox.Show("New records will be created for existing PrePaid vouchers,while Advanced records found will NOT be Updated!"+Environment.NewLine
                +"Please make sure next month records with correct data before proceeding?" ,"Confirm to proceed Pre-Payment!",MessageBoxButton.YesNo);
            
                if(mrs== MessageBoxResult.Yes)
                    ProcessPrepaymentVouchers();

        }



        //below added fucntion will create a csv file
        private void ExportToCsv_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dv = TenantsDataGrid.ItemsSource as DataView;
                if (dv == null)
                {
                    MessageBox.Show("No data to export.");
                    return;
                }
                var dt = dv.ToTable();
                var dlg = new SaveFileDialog()
                {
                    Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                    FileName = $"UtilityCostPerUnit_{DateTime.Now:ddMMMM}.csv"
                };
                if (dlg.ShowDialog() == true)
                {
                    using (var sw = new StreamWriter(dlg.FileName, false, Encoding.UTF8))
                    {
                        var columnNames = dt.Columns.Cast<DataColumn>().Select(c => EscapeForCsv(c.ColumnName));
                        sw.WriteLine(string.Join(",", columnNames));
                        foreach (DataRow row in dt.Rows)
                        {
                            var fields = dt.Columns.Cast<DataColumn>().Select(c => EscapeForCsv(row[c].ToString()));
                            sw.WriteLine(string.Join(",", fields));
                        }
                    }
                    MessageBox.Show("Export complete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting CSV: {ex.Message}");
            }


        }
        private static string EscapeForCsv(string s)
        {
            if (s == null) return "";
            if (s.Contains("\"")) s = s.Replace("\"", "\"\"");
            if (s.Contains(",") || s.Contains("\"") || s.Contains("\n") || s.Contains("\r"))
                return $"\"{s}\"";
            return s;
        }

        private void IsPaidCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    conn.Open();

                    //Now records will show those where expected rent is greater than paid, it shows that it isn't paid in full
                    //It will show records where Expected rent, water, sanitation bills are greater than the value of total money paid for the user to take action
                    string sql = @"                               
                                       SELECT [UTR_ID],[UNIT NO],
                                       [TENANT NAME],
                                       [EXPECTED RENT],
                                       WATER,
                                       SANITATION,
                                       [RENT RECEIVED],
                                       DEPOSIT,
                                       PREPAYMENT,
                                       [ARREAS B/F],
                                       [ARREAS PAID(UTILITIES)],
                                       [TOTAL MONEY PAID],
                                       [DATAFORMONTH],
                                       [ARREAS C/F],
                                       DATAFORMONTH,
                                       [EXTRA COMMENTS]
 	                                FROM UploadsTenantRent
                                WHERE Coalesce(IsVchrProcesd,0) = 0   AND [EXPECTED RENT] + WATER + SANITATION > [TOTAL MONEY PAID] AND COALESCE(Vchrid,0)=0"; 
                   
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        System.Data.DataTable dt = new System.Data.DataTable();
                        adapter.Fill(dt);
                        TenantsDataGrid.ItemsSource = dt.DefaultView; 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error showing paid records: {ex.Message}");
            }
        }

        private void IsPaidCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    conn.Open();

                    string sql = @"
                               SELECT [UTR_ID],[UNIT NO],[TENANT NAME],[EXPECTED RENT],[WATER],SANITATION,
                               [RENT RECEIVED],DEPOSIT,[TOTAL MONEY PAID],DATAFORMONTH,UPLOADTIME,
                               [EXTRA COMMENTS]
                               FROM UploadsTenantRent
                               WHERE [EXPECTED RENT] > 0 AND COALESCE(Vchrid,0)=0"; 

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        System.Data.DataTable dt = new System.Data.DataTable();
                        adapter.Fill(dt);
                        TenantsDataGrid.ItemsSource = dt.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tenant data: {ex.Message}");
            }
        }

        private void Compare_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    conn.Open();
                    // This query joins tenant data with calculated utility bills
                    string sql = @"
                SELECT 
                    t.[UNIT NO],
                    t.[TENANT NAME],
                    t.[WATER],
                    (u.WATER * u.WATERUnitRange) AS EXPECTED1,
                    t.[SANITATION],                    
                    (u.SANITATION * u.SANITATIONUnitRange) AS EXPECTED2,
                    (u.GAS * u.GASUnitRange) AS GasBill,
                    (u.ELECTRICITY * u.ELECTRICITYUnitRange) AS ElectricityBill,
                    (u.INTERNET * u.INTERNETUnitRange) AS InternetBill,
                    (u.TV * u.TVUnitRange) AS TVBill,
                    (u.LAUNDRY * u.LAUNDRYUnitRange) AS LaundryBill,
                    (u.MISC * u.MISCUnitRange) AS MISCBill,
                    (
                       t.[EXPECTED RENT] + 
                       (u.WATER * u.WATERUnitRange) + 
                       (u.SANITATION * u.SANITATIONUnitRange) +
                       (u.GAS * u.GASUnitRange) +
                       (u.ELECTRICITY * u.ELECTRICITYUnitRange) +
                       (u.INTERNET * u.INTERNETUnitRange) +
                       (u.TV * u.TVUnitRange) +
                       (u.LAUNDRY * u.LAUNDRYUnitRange) +
                       (u.MISC * u.MISCUnitRange)
                    ) AS TotalDue
                FROM UploadsTenantRent t
                CROSS JOIN UtilityUnitCost u;  
                                                      ";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        System.Data.DataTable dt = new System.Data.DataTable();
                        adapter.Fill(dt);
                        TenantsDataGrid.ItemsSource = dt.DefaultView;
                    }
                }
                MessageBox.Show("Please Press 'Clear' to View Previous Records");
            }

            
            catch (Exception ex)
            {
                MessageBox.Show("Error processing data: " + ex.Message);
            }
        }
    }
}
