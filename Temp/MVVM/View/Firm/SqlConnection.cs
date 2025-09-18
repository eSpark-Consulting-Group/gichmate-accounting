namespace AccountsPRO.MVVM.View.Firm
{
    internal class SqlConnection
    {
        public SqlConnection(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; }
    }
}