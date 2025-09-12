using System;

namespace AccountsLibrary
{
	public class VchrTranLog
	{
		public int? VchrTranId { get; set; }

		public int? VchrNo { get; set; }

		public DateTime VchrDate { get; set; }

		public decimal VchrAmount { get; set; }

		public string VchrType { get; set; }

		public int? FromAccountId { get; set; }

		public int? ToAccountId { get; set; }

		public string Narration { get; set; }

		public int? SalesManId { get; set; }

		public int? UserId { get; set; }

		public DateTime TranDate { get; set; }

		public string VchrName { get; set; }

		public int? FK_BranchId { get; set; }

		public int? SequenceNo { get; set; }

		public string Terminal { get; set; }
	}
}
