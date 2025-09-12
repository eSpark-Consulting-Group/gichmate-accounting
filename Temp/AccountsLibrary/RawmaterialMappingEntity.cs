namespace AccountsLibrary
{
	public class RawmaterialMappingEntity
	{
		public int? Id { get; set; }

		public int? FK_ProductId { get; set; }

		public decimal Quantity { get; set; }

		public int? FK_UnitId { get; set; }

		public decimal Rate { get; set; }

		public decimal? NetAmount { get; set; }

		public int? FK_ManufactureHdrId { get; set; }

		public int? FK_BatchId { get; set; }
	}
}
