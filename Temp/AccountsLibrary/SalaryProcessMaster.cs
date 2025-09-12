using System;

namespace AccountsLibrary
{
	public class SalaryProcessMaster
	{
		public int? SPrcsMasterID { get; set; }

		public int? SPrcsVchrNo { get; set; }

		public DateTime SPrcsDate { get; set; }

		public string SlryPrd { get; set; }

		public DateTime DateFrom { get; set; }

		public DateTime DateTo { get; set; }

		public string StaffType { get; set; }

		public string Description { get; set; }

		public int? CashOrBankAcctId { get; set; }

		public string CashOrBankAcctName { get; set; }

		public int? FK_BranchId { get; set; }
	}
}
