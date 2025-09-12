using System;

namespace AccountsLibrary
{
	public class PurRtnInvSttings
	{
		public int? PurRtnSettId { get; set; }

		public int? PurRtnInvId { get; set; }

		public int? ProId { get; set; }

		public decimal ProQty { get; set; }

		public int? UnitId { get; set; }

		public decimal ProRate { get; set; }

		public decimal Disc { get; set; }

		public decimal DiscAmnt { get; set; }

		public decimal Gross { get; set; }

		public decimal Tax { get; set; }

		public decimal TaxAmnt { get; set; }

		public decimal NetAmnt { get; set; }

		public int? PartyAccId { get; set; }

		public DateTime Date { get; set; }

		public string HSN { get; set; }

		public decimal GST { get; set; }

		public decimal GSTAMOUNT { get; set; }

		public decimal SGST { get; set; }

		public decimal SGSTAmount { get; set; }

		public decimal CGST { get; set; }

		public decimal CGSTAmount { get; set; }

		public decimal IGST { get; set; }

		public decimal IGSTAMOUNT { get; set; }

		public int? FK_BatchId { get; set; }

		public string Barcode { get; set; }
	}
}
