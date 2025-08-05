using System;

namespace Ashpro
{
	public class PurchaseInvoiceDtl
	{
		public int? PurSettingsId { get; set; }

		public int? PurInvId { get; set; }

		public int? PartyAccId { get; set; }

		public int? ProId { get; set; }

		public decimal ProQty { get; set; }

		public int? UnitId { get; set; }

		public decimal ProRate { get; set; }

		public decimal Disc { get; set; }

		public decimal DiscAmnt { get; set; }

		public decimal Gross { get; set; }

		public decimal NetAmnt { get; set; }

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
