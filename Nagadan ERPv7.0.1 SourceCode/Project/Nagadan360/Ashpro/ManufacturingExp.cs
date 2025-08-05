using System;

namespace Ashpro
{
	public class ManufacturingExp
	{
		public int? Id { get; set; }

		public int? FK_PaymentAccountId { get; set; }

		public int? FK_ExpenseAccountId { get; set; }

		public int? FK_ManufactureHdrId { get; set; }

		public DateTime Date { get; set; }

		public decimal? Amount { get; set; }
	}
}
