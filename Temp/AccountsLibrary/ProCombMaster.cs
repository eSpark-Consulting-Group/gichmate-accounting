using System;

namespace AccountsLibrary
{
	public class ProCombMaster
	{
		public int? ProCombMasterId { get; set; }

		public string CombProName { get; set; }

		public int? ProCombVchrNo { get; set; }

		public DateTime Date { get; set; }

		public decimal ProQty { get; set; }

		public int? UnitId { get; set; }

		public decimal ProSaleRate { get; set; }

		public decimal ProPurRate { get; set; }

		public decimal MRP { get; set; }

		public string CombType { get; set; }

		public string Barcode { get; set; }
	}
}
