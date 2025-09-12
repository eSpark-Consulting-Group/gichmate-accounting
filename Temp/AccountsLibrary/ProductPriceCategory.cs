namespace AccountsLibrary
{
	public class ProductPriceCategory
	{
		public int? ProPrceCatId { get; set; }

		public int? ProductId { get; set; }

		public int? PCategoryId { get; set; }

		public int? UnitId { get; set; }

		public int? PGroupId { get; set; }

		public decimal SPrice { get; set; }

		public decimal SPDisc { get; set; }

		public decimal PPrice { get; set; }

		public decimal PDisc { get; set; }

		public string Barcode { get; set; }
	}
}
