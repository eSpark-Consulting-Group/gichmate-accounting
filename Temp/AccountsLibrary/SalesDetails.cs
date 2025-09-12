using System;

namespace AccountsLibrary
{
	public class SalesDetails
	{
		public int? SalesSettingsId { get; set; }

		public int? SalesInvId { get; set; }

		public int? PartyAccountId { get; set; }

		public int? ProductId { get; set; }

		public decimal ProQty { get; set; }

		public int? UnitId { get; set; }

		public decimal ProRate { get; set; }

		public decimal ProDiscont { get; set; }

		public decimal DiscAmnt { get; set; }

		public decimal GrossAmnt { get; set; }

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

		public decimal ItemCalory { get; set; }

		public int? EmployeeID { get; set; }

		public decimal EmployeeCommision { get; set; }

		public int? AgentId { get; set; }

		public decimal AgentCommission { get; set; }

		public string ProductName { get; set; }

		public string ItemSerialNumber { get; set; }

		public string FieldOne { get; set; }

		public string FieldTwo { get; set; }

		public string FieldThree { get; set; }

		public string FieldFour { get; set; }
	}
}
