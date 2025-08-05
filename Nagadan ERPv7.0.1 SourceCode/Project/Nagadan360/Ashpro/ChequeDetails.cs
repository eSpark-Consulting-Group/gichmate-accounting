using System;

namespace Ashpro
{
	public class ChequeDetails
	{
		public int? CheqDtlsId { get; set; }

		public int VchrId { get; set; }

		public int? SL { get; set; }

		public int? VchrNo { get; set; }

		public string VchrType { get; set; }

		public string BankAccount { get; set; }

		public string PartyAccount { get; set; }

		public int? AccountId { get; set; }

		public int? BankAcctId { get; set; }

		public decimal Amount { get; set; }

		public string Bank { get; set; }

		public string Branch { get; set; }

		public string CheqNo { get; set; }

		public string CheqDate { get; set; }

		public string Status { get; set; }

		public string RefNo { get; set; }

		public string Remarks { get; set; }

		public DateTime TranDate { get; set; }

		public int? JeId { get; set; }

		public string Type { get; set; }

		public DateTime CollectedDate { get; set; }

		public DateTime Date { get; set; }

		public decimal ChqRtnAmount { get; set; }

		public int? FK_BranchId { get; set; }
	}
}
