using System;

namespace AccountsLibrary
{
	public class StockTransferMaster
	{
		public int? StockTransferId { get; set; }

		public int? StockTransferCode { get; set; }

		public DateTime TransferDate { get; set; }

		public int? FromGodownId { get; set; }

		public string FromGodownName { get; set; }

		public int? ToGodownId { get; set; }

		public string ToGodownName { get; set; }

		public string Remarks { get; set; }

		public int? UserId { get; set; }

		public DateTime EntryDate { get; set; }

		public string CompanyName { get; set; }

		public string CompanyPlace { get; set; }

		public decimal TotalAmnt { get; set; }

		public decimal TotalQty { get; set; }

		public string TranType { get; set; }

		public int? FK_BranchId { get; set; }
	}
}
