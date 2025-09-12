namespace AccountsLibrary
{
	public class Transaction
	{
		public int SL { get; set; }

		public int? ProductId { get; set; }

		public int ProductCode { get; set; }

		public string ProductName { get; set; }

		public string LocalName { get; set; }

		public string Barcode { get; set; }

		public decimal Quantity { get; set; }

		public string Unit { get; set; }

		public decimal Rate { get; set; }

		public decimal GrossAmount { get; set; }

		public decimal Discount { get; set; }

		public decimal DiscountAmount { get; set; }

		public decimal Taxable { get; set; }

		public decimal Vat { get; set; }

		public decimal VatAmount { get; set; }

		public decimal Gst { get; set; }

		public decimal GstAmount { get; set; }

		public string Hsn { get; set; }

		public decimal Sgst { get; set; }

		public decimal SGstAmount { get; set; }

		public decimal CGst { get; set; }

		public decimal CGstAmount { get; set; }

		public decimal IGst { get; set; }

		public decimal IGstAmount { get; set; }

		public decimal NETAmount { get; set; }

		public int? FK_BatchId { get; set; }

		public int? FK_EmployeeId { get; set; }

		public int? UnitId { get; set; }

		public string ItemQRCode { get; set; }

		public string ItemImage { get; set; }

		public string ItemNote { get; set; }

		public decimal EmployeeCommision { get; set; }

		public string ItemSerialNumber { get; set; }

		public string FieldOne { get; set; }

		public string FieldTwo { get; set; }

		public string FieldThree { get; set; }

		public string FieldFour { get; set; }
	}
}
