using System;

namespace Ashpro
{
	public class ManufactureHdr
	{
		public int? Id { get; set; }

		public int? InvNo { get; set; }

		public DateTime Date { get; set; }

		public int? FK_GodownId { get; set; }

		public string Remarks { get; set; }

		public int? FK_UserId { get; set; }

		public DateTime EntryDate { get; set; }

		public int? FK_BranchId { get; set; }
	}
}
