using System;

namespace AccountsLibrary
{
	public class PrevilegeCard
	{
		public int? PrevilageCardId { get; set; }

		public string ConsumerCode { get; set; }

		public int? CustomerId { get; set; }

		public int? PriceCategoryId { get; set; }

		public int? NomineeAccountId { get; set; }

		public DateTime ExpairyDate { get; set; }

		public DateTime RenewalDate { get; set; }

		public DateTime DOBofCustomer { get; set; }

		public string Remarks { get; set; }

		public int? FK_BranchId { get; set; }
	}
}
