namespace Ashpro
{
	public class SalesCounterEntity
	{
		public int? SalesCounterId { get; set; }

		public string SalesCounterName { get; set; }

		public int? CashAccountId { get; set; }

		public string Remarks { get; set; }

		public int? FK_BranchId { get; set; }
	}
}
