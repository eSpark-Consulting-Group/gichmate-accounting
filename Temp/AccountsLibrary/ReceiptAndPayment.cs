using System;

namespace AccountsLibrary
{
	public class ReceiptAndPayment
	{
		public int? Id { get; set; }

		public bool IsSettiled { get; set; }

		public string CntrInvNo { get; set; }

		public DateTime SalesDate { get; set; }

		public decimal GrandDiscount { get; set; }

		public decimal TotalPayment { get; set; }

		public decimal Credit { get; set; }

		public decimal Payment { get; set; }

		public decimal Balance { get; set; }

		public string VchrNo { get; set; }

		public int? InvoiceId { get; set; }

		public int? VchrId { get; set; }

		public string InvoiceType { get; set; }
	}
}
