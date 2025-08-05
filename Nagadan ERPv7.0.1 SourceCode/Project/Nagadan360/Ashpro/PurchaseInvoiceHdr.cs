using System;

namespace Ashpro
{
	public class PurchaseInvoiceHdr
	{
		public int? PurInvId { get; set; }

		public int? PurInvNo { get; set; }

		public string InvType { get; set; }

		public DateTime PurDate { get; set; }

		public int? PurActId { get; set; }

		public int? PartyAccId { get; set; }

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

		public string RefInvNo { get; set; }

		public decimal Taxable { get; set; }

		public int? FK_BranchId { get; set; }

		public decimal CashPayment { get; set; }

		public decimal CardPayment { get; set; }

		public decimal Credit { get; set; }

		public decimal FreeAmount { get; set; }

		public decimal ReturnAmount { get; set; }

		public bool IsClosed { get; set; }

		public string FielOne { get; set; }

		public string FieldTwo { get; set; }

		public string FieldThree { get; set; }

		public string FieldFour { get; set; }

		public int? CashAccountId { get; set; }

		public int? CardAccountId { get; set; }

		public int? OnlineAccountId { get; set; }

		public bool IsSettiled { get; set; }

		public decimal TotalPayment { get; set; }

		public string VoucherNumber { get; set; }

		public int? SequenceNo { get; set; }

		public string Terminal { get; set; }
	}
}
