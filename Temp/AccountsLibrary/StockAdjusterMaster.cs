using System;

namespace AccountsLibrary
{
	public class StockAdjusterMaster
	{
		public int? stockAdjusterMasterId { get; set; }

		public int? StockAdjusterMasterCode { get; set; }

		public DateTime Date { get; set; }

		public string Remarks { get; set; }

		public int? ProtGroup { get; set; }

		public int? ProBrand { get; set; }

		public int? FK_BranchId { get; set; }
	}
}
