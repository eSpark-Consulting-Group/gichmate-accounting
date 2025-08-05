namespace Ashpro
{
	public class Batch
	{
		public int? Id { get; set; }

		public string Name { get; set; }

		public int FK_PurInvId { get; set; }

		public string Remarks { get; set; }

		public int FK_BranchId { get; set; }

		public string RefNo { get; set; }
	}
}
