namespace AccountsLibrary
{
	public class ExManufactureDtls : ManufactureDtls
	{
		public int SL { get; set; }

		public int ProductCode { get; set; }

		public string Barcode { get; set; }

		public string ProductName { get; set; }

		public string UnitName { get; set; }

		public decimal? FinalCost { get; set; }
	}
}
