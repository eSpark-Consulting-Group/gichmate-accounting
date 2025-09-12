using System;
using System.Collections.Generic;
using System.Data;
using AccountsPro.SubProjects.AccountsLibraryFramework;

namespace AccountsLibrary
{
	internal class RContextHelper
	{
		public static List<SalesSummaryEntity> GetSalesSummaries(DataTable dt, string Company)
		{
			List<SalesSummaryEntity> list = new List<SalesSummaryEntity>();
			foreach (DataRow row in dt.Rows)
			{
				SalesSummaryEntity item = new SalesSummaryEntity
				{
					Date = row["Date"].ToString(),
					Taxable = row["Taxable"].ToString(),
					Tax = row["Tax"].ToString(),
					Net = row["Net"].ToString(),
					Returns = row["Return"].ToString(),
					Company = Company
				};
				list.Add(item);
			}
			return list;
		}

		public static List<TopCreditors> GetTopCreditors(DataTable dt, string Company)
		{
			List<TopCreditors> list = new List<TopCreditors>();
			foreach (DataRow row in dt.Rows)
			{
				TopCreditors item = new TopCreditors
				{
					Company = Company,
					ToDate = DateTime.Now.Date.ToShortDateString(),
					CreditorsName = row["CreditorsName"].ToString(),
					CreditorsBalance = row["CreditorsBalance"].ToString().ToDecimal().ToString(CommonSettings.strCurencyFormat)
				};
				list.Add(item);
			}
			return list;
		}

		public static List<TopDeptors> GetTopDeptors(DataTable dt, string Company)
		{
			List<TopDeptors> list = new List<TopDeptors>();
			foreach (DataRow row in dt.Rows)
			{
				TopDeptors item = new TopDeptors
				{
					Company = Company,
					ToDate = DateTime.Now.Date.ToShortDateString(),
					DebtorsName = row["DebtorsName"].ToString(),
					DebtorsBalance = row["DebtorsBalance"].ToString().ToDecimal().ToString(CommonSettings.strCurencyFormat)
				};
				list.Add(item);
			}
			return list;
		}

		public static List<TopSoldProdduct> GetTopSoldProdducts(DataTable dt, string Company)
		{
			List<TopSoldProdduct> list = new List<TopSoldProdduct>();
			foreach (DataRow row in dt.Rows)
			{
				TopSoldProdduct item = new TopSoldProdduct
				{
					Company = Company,
					ProductName = row["ProductName"].ToString(),
					SoldQuantity = row["SoldQuantity"].ToString().ToDecimal().ToString(CommonSettings.strQuantityFormat),
					OnTheDate = DateTime.Now.Date.ToShortDateString(),
					NetAmount = row["NetAmount"].ToString().ToDecimal().ToString(CommonSettings.strCurencyFormat)
				};
				list.Add(item);
			}
			return list;
		}

		public static List<TopStockedProduct> GetTopStockedProducts(DataTable dt, string Company)
		{
			List<TopStockedProduct> list = new List<TopStockedProduct>();
			foreach (DataRow row in dt.Rows)
			{
				TopStockedProduct item = new TopStockedProduct
				{
					Company = Company,
					ProductName = row["ProductName"].ToString(),
					ProductQuantity = row["ProductQuantity"].ToString().ToDecimal().ToString(CommonSettings.strQuantityFormat),
					ToDate = DateTime.Now.Date.ToShortDateString(),
					ProStockValue = row["ProStockValue"].ToString().ToDecimal().ToString(CommonSettings.strCurencyFormat)
				};
				list.Add(item);
			}
			return list;
		}
	}
}
