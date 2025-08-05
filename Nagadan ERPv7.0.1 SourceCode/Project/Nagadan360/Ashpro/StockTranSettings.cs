namespace Ashpro
{
	public class StockTranSettings
	{
		public int? StockTranSettingsId { get; set; }

		public int? StockTransferId { get; set; }

		public int? ProductId { get; set; }

		public string ProductName { get; set; }

		public decimal ProQuantity { get; set; }

		public int? UnitId { get; set; }

		public string CompanyName { get; set; }

		public string CompanyPlace { get; set; }

		public decimal ProRate { get; set; }

		public decimal Total { get; set; }

		public int? FK_BatchId { get; set; }
	}
}
