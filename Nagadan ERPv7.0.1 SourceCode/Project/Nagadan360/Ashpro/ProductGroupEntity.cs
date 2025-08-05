namespace Ashpro
{
	public class ProductGroupEntity
	{
		public int? PGroupId { get; set; }

		public string PGroupName { get; set; }

		public string Under { get; set; }

		public decimal SalesInsentive { get; set; }

		public decimal SalesDis { get; set; }

		public decimal PurchDis { get; set; }

		public string Remarks { get; set; }

		public int? ListOrder { get; set; }

		public bool isShowInGrid { get; set; }
	}
}
