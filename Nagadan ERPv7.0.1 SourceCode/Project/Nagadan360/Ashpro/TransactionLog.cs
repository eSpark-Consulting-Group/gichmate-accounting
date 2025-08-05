using System;

namespace Ashpro
{
	public class TransactionLog
	{
		public int? TranId { get; set; }

		public int? VchrId { get; set; }

		public int VchrNo { get; set; }

		public string VchrType { get; set; }

		public DateTime VchrDate { get; set; }

		public int? AccountId { get; set; }

		public int? OppAccountId { get; set; }

		public decimal DebitAmount { get; set; }

		public decimal CreditAmount { get; set; }

		public string Narration { get; set; }

		public int? UserID { get; set; }

		public DateTime TranDate { get; set; }

		public string Status { get; set; }

		public string RefNo { get; set; }

		public string VchrName { get; set; }

		public int? FK_BranchId { get; set; }

		public int? SequenceNo { get; set; }

		public string Terminal { get; set; }
	}
}
