namespace AccountsLibrary
{
	public class FinancialYear
	{
		public int? FyearId { get; set; }

		public string FyearName { get; set; }

		public string DateFrom { get; set; }

		public string DateTo { get; set; }

		public bool Old { get; set; }

		public bool Closed { get; set; }
	}
}
