namespace Ashpro
{
	public class ExStockAdjustSettings : StockAdjustSettings
	{
		public int SL { get; set; }

		public int ProductCode { get; set; }

		public string ProductName { get; set; }

		public string UnitName { get; set; }

		public decimal Excess { get; set; }

		public decimal Short { get; set; }

		public decimal Rate { get; set; }

		public decimal TotalAmount { get; set; }
	}
}
