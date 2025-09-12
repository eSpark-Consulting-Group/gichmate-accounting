namespace AccountsLibrary
{
	public class ProductUnit
	{
		public int? ProUnitId { get; set; }

		public int? ProductId { get; set; }

		public int? UnitId { get; set; }

		public string UnitType { get; set; }

		public string Barcode { get; set; }

		public decimal UnitQty { get; set; }

		public string ProductUnitName { get; set; }

		public bool IsInclusive { get; set; }

		public decimal OpeningStock { get; set; }

		public decimal SalesPrice { get; set; }
	}
}
