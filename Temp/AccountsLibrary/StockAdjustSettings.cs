namespace AccountsLibrary
{
	public class StockAdjustSettings
	{
		public int? stockAdjustSettingsId { get; set; }

		public int? stockAdjusterMasterId { get; set; }

		public int? productId { get; set; }

		public decimal currSystemStock { get; set; }

		public decimal curActualStock { get; set; }

		public int? unitId { get; set; }

		public int? FK_BatchId { get; set; }

		public string Barcode { get; set; }
	}
}
