using System;

namespace AccountsLibrary
{
	public class SalesRtnMaster
	{
		public int? SlsRtrnId { get; set; }

		public int? SlRtnInvNo { get; set; }

		public DateTime SlsRtnDate { get; set; }

		public int? SlsRtnAccountId { get; set; }

		public int? PartyAccountId { get; set; }

		public int? RouteId { get; set; }

		public int? VehId { get; set; }

		public int? StaffId { get; set; }

		public int? PriceCategoryId { get; set; }

		public decimal AditionalAmnt { get; set; }

		public decimal Frieght { get; set; }

		public decimal TotalDiscount { get; set; }

		public decimal BillDiscount { get; set; }

		public decimal TotalAmount { get; set; }

		public int? JeId { get; set; }

		public DateTime EntryDate { get; set; }

		public string Remarks { get; set; }

		public decimal IGSTAmount { get; set; }

		public decimal SGSTAmount { get; set; }

		public decimal CGSTAmount { get; set; }

		public int FYearID { get; set; }

		public decimal Taxable { get; set; }

		public decimal TotalTax { get; set; }

		public int? FK_BranchId { get; set; }

		public int? SequenceNo { get; set; }

		public string Terminal { get; set; }

		public string RefInvNo { get; set; }
	}
}
