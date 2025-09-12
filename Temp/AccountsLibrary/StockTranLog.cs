using System;

namespace AccountsLibrary
{
	public class StockTranLog
	{
		public int? STranId { get; set; }

		public int? ProductId { get; set; }

		public int? VchrId { get; set; }

		public int VchrNo { get; set; }

		public DateTime VchrDate { get; set; }

		public string VchrType { get; set; }

		public string VchrFormType { get; set; }

		public int? AccountId { get; set; }

		public decimal InQuantity { get; set; }

		public decimal OutQuantity { get; set; }

		public int? UnitId { get; set; }

		public string Narration { get; set; }

		public int? UserId { get; set; }

		public DateTime TranDate { get; set; }

		public int? FK_BatchId { get; set; }

		public int? FK_BranchId { get; set; }

		public string Barcode { get; set; }
	}
}
