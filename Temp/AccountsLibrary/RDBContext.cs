using System.Collections.Generic;
using System.Data;

namespace AccountsLibrary
{
	internal class RDBContext
	{
		public List<SalesSummaryEntity> SalesSummaryEntities(DataTable dt, string Company)
		{
			return RContextHelper.GetSalesSummaries(dt, Company);
		}

		public List<TopCreditors> TopCreditors(DataTable dt, string Company)
		{
			return RContextHelper.GetTopCreditors(dt, Company);
		}

		public List<TopDeptors> TopDeptors(DataTable dt, string Company)
		{
			return RContextHelper.GetTopDeptors(dt, Company);
		}

		public List<TopSoldProdduct> TopSoldProdducts(DataTable dt, string Company)
		{
			return RContextHelper.GetTopSoldProdducts(dt, Company);
		}

		public List<TopStockedProduct> TopStockedProducts(DataTable dt, string Company)
		{
			return RContextHelper.GetTopStockedProducts(dt, Company);
		}
	}
}
