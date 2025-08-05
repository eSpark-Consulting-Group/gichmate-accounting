namespace Ashpro
{
	public class SalesOrderSettings
	{
		public int? SalesOrderMasterId { get; set; }

		public int? PartyAccountId { get; set; }

		public int? ProductId { get; set; }

		public decimal ProQty { get; set; }

		public int? UnitId { get; set; }

		public decimal ProRate { get; set; }

		public decimal ProDiscont { get; set; }

		public decimal DiscAmnt { get; set; }

		public decimal GrossAmnt { get; set; }

		public decimal NetAmnt { get; set; }

		public decimal Taxable { get; set; }

		public decimal Tax { get; set; }

		public decimal TaxAmount { get; set; }

		public int? FK_BatchId { get; set; }

		public string Barcode { get; set; }
	}
}
