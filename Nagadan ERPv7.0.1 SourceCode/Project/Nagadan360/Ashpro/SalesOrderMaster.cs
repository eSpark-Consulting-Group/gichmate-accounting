using System;

namespace Ashpro
{
	public class SalesOrderMaster
	{
		public int? SalesOrderMasterId { get; set; }

		public int? VchrNo { get; set; }

		public int? CustomerId { get; set; }

		public int? SalesAccountId { get; set; }

		public int? SalesManId { get; set; }

		public int? RouteId { get; set; }

		public int? PriceCategoryId { get; set; }

		public string Remarks { get; set; }

		public decimal NetAmount { get; set; }

		public decimal AdditionalAmnt { get; set; }

		public decimal Freight { get; set; }

		public decimal BillDiscount { get; set; }

		public decimal AddDiscount { get; set; }

		public decimal GrandTotal { get; set; }

		public string InvType { get; set; }

		public int? JEid { get; set; }

		public DateTime EntryDate { get; set; }

		public DateTime InvDate { get; set; }

		public bool Status { get; set; }

		public decimal Taxable { get; set; }

		public decimal TotalTax { get; set; }

		public int? FK_BranchId { get; set; }
	}
}
