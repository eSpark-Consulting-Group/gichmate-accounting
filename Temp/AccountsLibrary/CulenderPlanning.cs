using System;

namespace AccountsLibrary
{
	public class CulenderPlanning
	{
		public int? CPlaningId { get; set; }

		public DateTime CplanninDate { get; set; }

		public byte[] Description { get; set; }

		public int? FK_BranchId { get; set; }
	}
}
