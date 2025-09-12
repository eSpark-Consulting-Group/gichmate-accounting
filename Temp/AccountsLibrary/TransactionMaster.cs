using System;

namespace AccountsLibrary
{
	public class TransactionMaster
	{
		public string InvoiceNo { get; set; }

		public string InvoiceType { get; set; }

		public DateTime Date { get; set; }

		public decimal AdditionalAmount { get; set; }

		public decimal Frieght { get; set; }

		public decimal GrossAmount { get; set; }

		public decimal TotalDiscount { get; set; }

		public decimal BillDiscount { get; set; }

		public decimal Taxable { get; set; }

		public decimal CessAmount { get; set; }

		public decimal SGSTAmount { get; set; }

		public decimal CGSTAmount { get; set; }

		public decimal IGSTAmount { get; set; }

		public decimal GSTAmount { get; set; }

		public decimal VatAmount { get; set; }

		public decimal GrandTotal { get; set; }

		public string GrandTotalToWords { get; set; }

		public int? FK_BranchId { get; set; }

		public decimal NetAmount { get; set; }

		public string CntrInvNo { get; set; }

		public int? SequenceNo { get; set; }

		public decimal CashPayment { get; set; }

		public decimal CardPayment { get; set; }

		public decimal Credit { get; set; }

		public string Terminal { get; set; }

		public decimal FreeAmount { get; set; }

		public decimal ReturnAmount { get; set; }

		public string RunningBalance { get; set; }

		public string InvoiceBarcode { get; set; }

		public decimal OtherPayment { get; set; }

		public decimal TotalQuantity { get; set; }

		public int OrderNo { get; set; }

		public decimal? Tendered { get; set; }

		public decimal? Changes { get; set; }

		public string InvoiceQRCode { get; set; }

		public string InvoiceMode { get; set; }

		public string SalesMan { get; set; }

		public string GrandTotalToArabicWords { get; set; }

		public int? TockenNo { get; set; }

		public decimal CookingTime { get; set; }

		public string Notes { get; set; }

		public string KOTPrinterName { get; set; }

		public string KOTTitle { get; set; }

		public string RefNumber { get; set; }

		public string PriceCategory { get; set; }

		public string SalesFormType { get; set; }

		public string Remarks { get; set; }
	}
}
