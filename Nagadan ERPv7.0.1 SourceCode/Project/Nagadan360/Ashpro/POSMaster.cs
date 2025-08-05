using System;

namespace Ashpro
{
	public class POSMaster
	{
		public int? SalesInvNo { get; set; }

		public string InvType { get; set; }

		public DateTime SalesDate { get; set; }

		public string SalesAccount { get; set; }

		public string PartyAccount { get; set; }

		public string SalesMan { get; set; }

		public decimal AditionalAmount { get; set; }

		public decimal Frieght { get; set; }

		public decimal TotalDiscount { get; set; }

		public decimal BillDiscount { get; set; }

		public decimal GrandTotal { get; set; }

		public string JEName { get; set; }

		public DateTime EntryDate { get; set; }

		public string Remarks { get; set; }

		public decimal IGSTAmount { get; set; }

		public decimal SGSTAmount { get; set; }

		public decimal CGSTAmount { get; set; }

		public decimal TaxableAmount { get; set; }

		public string Financialyear { get; set; }

		public decimal FloodCess { get; set; }

		public decimal TotalTaxAmount { get; set; }

		public string BranchName { get; set; }

		public string CntrInvNo { get; set; }

		public int? SequenceNo { get; set; }

		public decimal? CashPayment { get; set; }

		public decimal? CardPayment { get; set; }

		public decimal? Credit { get; set; }

		public string Terminal { get; set; }

		public decimal FreeAmount { get; set; }

		public decimal ReturnAmount { get; set; }

		public decimal? OtherPayment { get; set; }
	}
}
