namespace Ashpro
{
	public class RouteEntity
	{
		public int? RootId { get; set; }

		public string RootName { get; set; }

		public string WeekDay { get; set; }

		public int? SalesManId { get; set; }

		public int? DriverId { get; set; }

		public int? VehicleId { get; set; }

		public int? FK_BranchId { get; set; }
	}
}
