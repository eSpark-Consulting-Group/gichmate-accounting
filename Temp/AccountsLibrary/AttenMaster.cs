using System;

namespace AccountsLibrary
{
	public class AttenMaster
	{
		public int? AttendanceMasterId { get; set; }

		public int? AtteInvNo { get; set; }

		public DateTime AtteDate { get; set; }

		public string Remarks { get; set; }

		public int? FK_BranchId { get; set; }
	}
}
