namespace AccountsLibrary
{
	public class RestaurantTable
	{
		public int? TableId { get; set; }

		public string TableName { get; set; }

		public int? ChairCount { get; set; }

		public string FloorName { get; set; }

		public int? XPosition { get; set; }

		public int? YPostion { get; set; }

		public byte[] Image { get; set; }

		public int? FK_EmplyeeId { get; set; }
	}
}
