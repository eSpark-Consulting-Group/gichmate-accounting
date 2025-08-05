namespace Ashpro
{
	public class POSDetails
	{
		public int SL { get; set; }

		public string ProductCode { get; set; }

		public int productId { get; set; }

		public string ProductName { get; set; }

		public string LocalName { get; set; }

		public string Barcode { get; set; }

		public decimal Qty { get; set; }

		public string UnitName { get; set; }

		public int UnitId { get; set; }

		public decimal FreeQty { get; set; }

		public decimal Rate { get; set; }

		public decimal Gross { get; set; }

		public decimal Discount { get; set; }

		public decimal DisAmount { get; set; }

		public decimal Taxable { get; set; }

		public decimal Tax { get; set; }

		public decimal TaxAmount { get; set; }

		public decimal NetAmount { get; set; }

		public int FK_BatchId { get; set; }

		public string ItemNote { get; set; }

		public int? FK_EmployeeId { get; set; }

		public decimal EmployeeCommision { get; set; }

		public string ItemSerialNumber { get; set; }

		public string FieldOne { get; set; }

		public string FieldTwo { get; set; }

		public string FieldThree { get; set; }

		public string FieldFour { get; set; }
	}
}
