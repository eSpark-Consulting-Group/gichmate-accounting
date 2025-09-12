namespace AccountsLibrary
{
	public class TaxCategoryEntity
	{
		public int? TCategoryId { get; set; }

		public string TCategoryName { get; set; }

		public decimal SalesVat { get; set; }

		public decimal PurVat { get; set; }

		public decimal SaleCst { get; set; }

		public decimal PurCst { get; set; }

		public decimal SaleCess { get; set; }

		public decimal PurCess { get; set; }

		public decimal SalesXDuty { get; set; }

		public decimal PurXDuty { get; set; }
	}
}
