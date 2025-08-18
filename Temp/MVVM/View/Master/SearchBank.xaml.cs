using System.Data;
using System.Data.SqlClient;
using System.Windows;
using ToastNotifications.Lifetime.Clear;

namespace FlexMold.MVVM.View.Master
{
    public partial class SearchBank : Window
    {
        public int SelectedAccountId { get; private set; }
        private string connectionString = "Your_Connection_String_Here";

        public SearchBank()
        {
            InitializeComponent();
            LoadAccounts();
        }

        private void LoadAccounts()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT AccountId, AccountName, AccountNo FROM Account";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    AccountsDataGrid.ItemsSource = dt.DefaultView;
                }
            }
            catch
            {
                // Handle error
            }
        }

        
        private void ClearButton(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Clear();
        }

        private void CancelButton(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SelectButton(object sender, RoutedEventArgs e)
        {
            if (AccountsDataGrid.SelectedItem is DataRowView row)
            {
                SelectedAccountId = (int)row["AccountId"];
                DialogResult = true;
                Close();
            }
        }
    }
} 