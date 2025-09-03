namespace Ashpro
{
	public class KOTOrderSettings
	{
		public int? KOTSettingsId { get; set; }

		public int? FK_KOTMasterId { get; set; }

		public int? ProductId { get; set; }

		public decimal Quantity { get; set; }

		public int? UnitId { get; set; }

		public decimal Rate { get; set; }

		public decimal NETAmount { get; set; }

		public string Barcode { get; set; }

		public string ItemNote { get; set; }

		public int CookingTime { get; set; }

		public int? ProPriority { get; set; }
	}
}
