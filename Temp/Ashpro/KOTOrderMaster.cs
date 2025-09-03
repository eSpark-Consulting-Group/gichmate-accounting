using System;

namespace Ashpro
{
	public class KOTOrderMaster
	{
		public int? KOTMasterId { get; set; }

		public int? TockenNo { get; set; }

		public DateTime DateAndTime { get; set; }

		public decimal CookingTime { get; set; }

		public string Terminal { get; set; }

		public int? SerialNo { get; set; }

		public int? FK_FYearId { get; set; }

		public int? FK_CustomerId { get; set; }

		public int? FK_UserId { get; set; }

		public int? FK_StaffId { get; set; }

		public int? TableId { get; set; }

		public string Remarks { get; set; }

		public string Notes { get; set; }

		public int? FK_KOTPrinterId { get; set; }

		public string OrderType { get; set; }

		public decimal TotalQuantity { get; set; }

		public decimal TotalAmount { get; set; }

		public int? FK_BranchId { get; set; }
	}
}
