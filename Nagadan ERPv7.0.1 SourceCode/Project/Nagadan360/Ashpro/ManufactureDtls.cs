namespace Ashpro
{
	public class ManufactureDtls
	{
		public int? Id { get; set; }

		public int? FK_ManufactureHdrId { get; set; }

		public int? FK_ProductId { get; set; }

		public decimal? Quantity { get; set; }

		public int? FK_UnitId { get; set; }

		public decimal PurchasePrice { get; set; }

		public decimal SalesePrice { get; set; }

		public decimal? RawCost { get; set; }

		public decimal? Expenses { get; set; }

		public decimal? Profit { get; set; }

		public decimal? FinalAmount { get; set; }

		public decimal? NewPurchaseRate { get; set; }

		public decimal? NewSalesRate { get; set; }

		public int? FK_BatchId { get; set; }
	}
}
