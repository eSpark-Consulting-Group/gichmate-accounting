using System;

namespace AccountsLibrary
{
	public class SalesMaster
	{
		public int? SalesInvId { get; set; }

		public int? SalesInvNo { get; set; }

		public string InvType { get; set; }

		public DateTime SalesDate { get; set; }

		public int? SalesAccountId { get; set; }

		public int? PartyAccountId { get; set; }

		public int? RouteId { get; set; }

		public int? VehId { get; set; }

		public int? StafId { get; set; }

		public int? PriceCategoryId { get; set; }

		public decimal AditionalAmount { get; set; }

		public decimal Frieght { get; set; }

		public decimal TotalDiscount { get; set; }

		public decimal BillDiscount { get; set; }

		public decimal GrandDiscount { get; set; }

		public int? JEId { get; set; }

		public DateTime EntryDate { get; set; }

		public string Remarks { get; set; }

		public decimal IGSTAmount { get; set; }

		public decimal SGSTAmount { get; set; }

		public decimal CGSTAmount { get; set; }

		public decimal TaxableAmount { get; set; }

		public int? FYearID { get; set; }

		public decimal FloodCess { get; set; }

		public decimal TotalTaxAmount { get; set; }

		public int? FK_BranchId { get; set; }

		public string CntrInvNo { get; set; }

		public int? SequenceNo { get; set; }

		public decimal CashPayment { get; set; }

		public decimal CardPayment { get; set; }

		public decimal Credit { get; set; }

		public string FielOne { get; set; }

		public string FieldTwo { get; set; }

		public string FieldThree { get; set; }

		public string FieldFour { get; set; }

		public string Terminal { get; set; }

		public bool IsClosed { get; set; }

		public decimal FreeAmount { get; set; }

		public decimal ReturnAmount { get; set; }

		public decimal OtherPayment { get; set; }

		public int? CashAccountId { get; set; }

		public int? CardAccountId { get; set; }

		public int? OnlineAccountId { get; set; }

		public bool IsSettiled { get; set; }

		public decimal TotalPayment { get; set; }

		public string VoucherNumber { get; set; }
	}
}
