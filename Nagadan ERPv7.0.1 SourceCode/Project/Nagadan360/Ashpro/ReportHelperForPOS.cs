using System;
using System.Data;

namespace Ashpro
{
	public static class ReportHelperForPOS
	{
		public static DataTable GetTaxForCloseCash(int iSequenceNumber, string sTerminal, int? IbranchId)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spGetTaxForCloseCash", new
				{
					SequenceNo = iSequenceNumber,
					Terminal = sTerminal,
					BranchId = IbranchId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable ProCategoryForCloseCash(string sFromDate, string sToDate, string SequenceNo, string sTerminal, int? iBranchId)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spProCategoryForCloseCash", new
				{
					FromDate = sFromDate,
					ToDate = sToDate,
					SequenceNo = SequenceNo,
					Terminal = sTerminal,
					BranchId = iBranchId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable GetPaymentReportForPOS(string sFromDate, string sToDate, int? iBranchId = null)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spGetPaymentReportForPOS", new
				{
					FromDate = sFromDate,
					ToDate = sToDate,
					BranchId = iBranchId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable GetTransactionReportForPOS(string sFromDate, string sToDate, int? iBranchId = null)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spGetTransactionReportForPOS", new
				{
					FromDate = sFromDate,
					ToDate = sToDate,
					BranchId = iBranchId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable SalesSummaryReportPOS(string sFromDate, string sToDate, int? iBranchId = null)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spSalesSummaryReportPOS", new
				{
					FromDate = sFromDate,
					ToDate = sToDate,
					BranchId = iBranchId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable TopDebtorsReport(string sDate)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spTopDebitors", new
				{
					Date = sDate
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable TopCreditors(string sDate)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spTopCreditors", new
				{
					Date = sDate
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable TopSoldProduct(string sFromDate, string sToDate)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spTopProductSold", new
				{
					FromDate = sFromDate,
					ToDate = sToDate
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable TopStockedProducts(string sDate)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spTopProductStockReport", new
				{
					Date = sDate
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable SalesTaxReportPOS(string sFromDate, string sToDate, int? iBranchId = null)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spSalesTaxReportPOS", new
				{
					FromDate = sFromDate,
					ToDate = sToDate,
					BranchId = iBranchId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable TaxCategoryReportPOS(string sFromDate, string sToDate, int? iBranchId = null)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spTaxCategoryReportPOS", new
				{
					FromDate = sFromDate,
					ToDate = sToDate,
					BranchId = iBranchId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable StockAndProfitPOS(int? productId, int? iBranchId = null)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spStockAndProfitPOS", new
				{
					ProductId = productId,
					BranchId = iBranchId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable ProductGroupSales(string sFromDate, string sToDate, int? GroupId, int? UserId, int? iBranchId = null)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spProductGroupSales", new
				{
					FromDate = sFromDate,
					ToDate = sToDate,
					ProGroupId = GroupId,
					UserId = UserId,
					BranchId = iBranchId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable ProductProfitSalePOS(string sFromDate, string sToDate, int? iBranchId = null)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spProductProfitSalePOS", new
				{
					FromDate = sFromDate,
					ToDate = sToDate,
					BranchId = iBranchId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable SaleByProductPOS(string sFromDate, string sToDate, int? ProductId, int? GroupId, int? iBranchId = null)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spSaleByProductPOS", new
				{
					FromDate = sFromDate,
					ToDate = sToDate,
					ProductId = ProductId,
					ProductGroupId = GroupId,
					BranchId = iBranchId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable CashClosePOS(string sFromDate, string sToDate, int? UserId, int? iBranchId = null)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spCashClosePOS", new
				{
					FromDate = sFromDate,
					ToDate = sToDate,
					UserId = UserId,
					BranchId = iBranchId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable CashFlow(string sFromDate, string sToDate, int AcctId, int? iBranchId = null)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spCashFlow", new
				{
					FromDate = sFromDate,
					ToDate = sToDate,
					CashAccountId = AcctId,
					BranchId = iBranchId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable CustomerReportPOS(int? CustomerId = null, int? iBranchId = null)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spCustomerReportPOS", new
				{
					CustomerId = CustomerId,
					BranchId = iBranchId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable CustomerLedgerReportPOS(string sFromDate, string sToDate, int AcctId, int? iBranchId = null)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spCustomerLedgerReportPOS", new
				{
					FromDate = sFromDate,
					ToDate = sToDate,
					AccountId = AcctId,
					BranchId = iBranchId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable CustomerSalesReportPOS(string sFromDate, string sToDate, int AcctId, int? iBranchId = null)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spCustomerSalesReportPOS", new
				{
					FromDate = sFromDate,
					ToDate = sToDate,
					AccountId = AcctId,
					BranchId = iBranchId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable CustomerItemSalesReportPOS(string sFromDate, string sToDate, int AcctId, int? iBranchId = null)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spCustomerItemSalesReportPOS", new
				{
					FromDate = sFromDate,
					ToDate = sToDate,
					AccountId = AcctId,
					BranchId = iBranchId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable ProductStockReportPOS(int? GroupId, int? iBranchId = null)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spProductStockReportPOS", new
				{
					ProductGroupId = GroupId,
					BranchId = iBranchId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable ProwisePurchasePO(string sFromDate, string sToDate, int? SupplierId, int? iBranchId = null)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spProwisePurchasePO", new
				{
					FromDate = sFromDate,
					ToDate = sToDate,
					SupplierId = SupplierId,
					BranchId = iBranchId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable ProductDifferencePOS(int? ProductId, int? iBranchId = null)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spProductDifferencePOS", new
				{
					ProductId = ProductId,
					BranchId = iBranchId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable SupplierReportPOS(int? SupplierId, int? iBranchId = null)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spSupplierReportPOS", new
				{
					SupplierId = SupplierId,
					BranchId = iBranchId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable SupplierItemPurchaseReportPOS(string sFromDate, string sToDate, int? SupplierId, int? iBranchId = null)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spProwisePurchasePO", new
				{
					FromDate = sFromDate,
					ToDate = sToDate,
					SupplierId = SupplierId,
					BranchId = iBranchId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable ProductOpeningStockReportPOS(int? GroupId)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spProductOpeningStockReport", new
				{
					ProGroupId = GroupId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable SupplierPurchaseReportPOS(string sFromDate, string sToDate, int? SupplierId, int? iBranchId = null)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spSupplierPurchaseReportPOS", new
				{
					FromDate = sFromDate,
					ToDate = sToDate,
					AccountId = SupplierId,
					BranchId = iBranchId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable ProductSaleCloseCash(int? SequenceNo, int? iBranchId = null)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spProductSaleCloseCash", new
				{
					SequenceNo = SequenceNo,
					BranchId = iBranchId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable SalesReturnItemsForCloseCash(string sFromDate, string sToDate, int? iBranchId = null)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spSalesReturnitems", new
				{
					FromDate = sFromDate,
					ToDate = sToDate,
					Terminal = Utility.uTerminal,
					SequenceNo = Utility.uSerialNumber,
					BranchId = iBranchId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable SuppliersPurchaseForCloseCash(string sFromDate, string sToDate, int? iBranchId = null)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spSupplierPurchase", new
				{
					FromDate = sFromDate,
					ToDate = sToDate,
					Terminal = Utility.uTerminal,
					SequenceNo = Utility.uSerialNumber,
					BranchId = iBranchId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable CashReceiptForCloseCash(string sFromDate, string sToDate, int? iBranchId = null)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spCashReceipt", new
				{
					FromDate = sFromDate,
					ToDate = sToDate,
					Terminal = Utility.uTerminal,
					SequenceNo = Utility.uSerialNumber,
					BranchId = iBranchId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable CashPaymentForCloseCash(string sFromDate, string sToDate, int? iBranchId = null)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spCashPayment", new
				{
					FromDate = sFromDate,
					ToDate = sToDate,
					Terminal = Utility.uTerminal,
					SequenceNo = Utility.uSerialNumber,
					BranchId = iBranchId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static decimal OppeningCashForCloseCash(string sDate, int? iBranchId = null)
		{
			try
			{
				return AshproCommon.GetDecimalDataWithParameter_SP("spOpeningCash", new
				{
					Date = sDate,
					Terminal = Utility.uTerminal,
					SequenceNo = Utility.uSerialNumber,
					BranchId = iBranchId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static decimal ClosingCashForCloseCash(string sFromDate, string sToDate, int? iBranchId = null)
		{
			try
			{
				decimal num = default(decimal);
				num = AshproCommon.GetDecimalDataWithParameter_SP("spOpeningCash", new
				{
					Date = sToDate,
					Terminal = Utility.uTerminal,
					SequenceNo = Utility.uSerialNumber,
					BranchId = iBranchId
				});
				return num - OppeningCashForCloseCash(sFromDate);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static decimal CashReceiptTotalForCloseCash(string sFromDate, string sToDate, int? iBranchId = null)
		{
			try
			{
				return AshproCommon.GetDecimalDataWithParameter_SP("spPaymentOrReceiptCash", new
				{
					FromDate = sFromDate,
					ToDate = sToDate,
					Terminal = Utility.uTerminal,
					SequenceNo = Utility.uSerialNumber,
					Type = 0,
					BranchId = iBranchId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static decimal CashPaymentTotalForCloseCash(string sFromDate, string sToDate, int? iBranchId = null)
		{
			try
			{
				return AshproCommon.GetDecimalDataWithParameter_SP("spPaymentOrReceiptCash", new
				{
					FromDate = sFromDate,
					ToDate = sToDate,
					Terminal = Utility.uTerminal,
					SequenceNo = Utility.uSerialNumber,
					Type = 1,
					BranchId = iBranchId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable UserSalesReportPOS(string sFromDate, string sToDate, int? iBranchId = null)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spUserWiseSale", new
				{
					FromDate = sFromDate,
					ToDate = sToDate,
					BranchId = iBranchId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable VoidItemsReport(string sFromDate, string sToDate, string Terminal = null, int? iSequence = null, int? iBranchId = null)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spVoidKOTReports", new
				{
					FromDate = sFromDate,
					ToDate = sToDate,
					Terminal = Terminal,
					SequenceNo = iSequence,
					BranchId = iBranchId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable PendingKOTReport(string sFromDate, string sToDate, string Terminal = null, int? iSequence = null, int? iBranchId = null)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spPendingKOT", new
				{
					FromDate = sFromDate,
					ToDate = sToDate,
					Terminal = Terminal,
					SequenceNo = iSequence,
					BranchId = iBranchId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable VatSubmissionReport(string sFromDate, string sToDate, int? iBranchId)
		{
			try
			{
				return AshproCommon.GetDataTableWithIdParameter_SP("spVatReport", new
				{
					FromDate = sFromDate,
					ToDate = sToDate,
					BranchId = iBranchId
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
