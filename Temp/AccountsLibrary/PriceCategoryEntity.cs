namespace AccountsLibrary
{
	public class PriceCategoryEntity
	{
		public int? PCategoryId { get; set; }

		public string PCategoryName { get; set; }

		public decimal Percentage { get; set; }

		public decimal SDiscountPercentage { get; set; }
	}
}
