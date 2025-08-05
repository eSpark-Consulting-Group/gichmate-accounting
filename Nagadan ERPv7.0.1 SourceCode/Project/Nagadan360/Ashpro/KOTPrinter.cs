namespace Ashpro
{
	public class KOTPrinter
	{
		public int? Id { get; set; }

		public string KOTName { get; set; }

		public string PrinterName { get; set; }

		public string ConnectionType { get; set; }

		public string IPaddress { get; set; }

		public string Mode { get; set; }

		public int FK_BranchId { get; set; }
	}
}
