using System;

namespace Ashpro
{
	public class VchrTransaction
	{
		public int VchrTranId { get; set; }

		public int SL { get; set; }

		public int VchrNo { get; set; }

		public DateTime VchrDate { get; set; }

		public decimal VchrAmount { get; set; }

		public string FromAccount { get; set; }

		public string ToAccount { get; set; }

		public string Narration { get; set; }

		public string SalesMan { get; set; }

		public int? FK_BranchId { get; set; }
	}
}
