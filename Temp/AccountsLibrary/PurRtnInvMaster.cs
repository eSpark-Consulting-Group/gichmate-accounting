using System;

namespace AccountsLibrary
{
	public class PurRtnInvMaster
	{
		public int? PurRtnInvId { get; set; }

		public int? PurRtnInvNo { get; set; }

		public DateTime PurRtnDate { get; set; }

		public int? PurRtnAcId { get; set; }

		public int? PartyAcId { get; set; }

		public int? PriceCategoryId { get; set; }

		public decimal AdditionalAmnt { get; set; }

		public decimal Frieght { get; set; }

		public decimal TotalDiscount { get; set; }

		public decimal BillDisc { get; set; }

		public decimal TotalTax { get; set; }

		public decimal TotalAmnt { get; set; }

		public int? JEId { get; set; }

		public DateTime EntryDate { get; set; }

		public string Remarks { get; set; }

		public decimal IGSTAmount { get; set; }

		public decimal SGSTAmount { get; set; }

		public decimal CGSTAmount { get; set; }

		public decimal Taxable { get; set; }

		public int? FK_BranchId { get; set; }

		public int? SequenceNo { get; set; }

		public string Terminal { get; set; }
	}
}
