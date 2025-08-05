using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace AshproMessage
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	public class MessageKeys
	{
		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static ResourceManager ResourceManager
		{
			get
			{
				if (resourceMan == null)
				{
					ResourceManager resourceManager = (resourceMan = new ResourceManager("AshproMessage.MessageKeys", typeof(MessageKeys).Assembly));
				}
				return resourceMan;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static CultureInfo Culture
		{
			get
			{
				return resourceCulture;
			}
			set
			{
				resourceCulture = value;
			}
		}

		public static string Abort => ResourceManager.GetString("Abort", resourceCulture);

		public static string Bank => ResourceManager.GetString("Bank", resourceCulture);

		public static string BuiltinMessage => ResourceManager.GetString("BuiltinMessage", resourceCulture);

		public static string Cancel => ResourceManager.GetString("Cancel", resourceCulture);

		public static string CashAccount => ResourceManager.GetString("CashAccount", resourceCulture);

		public static string CashFlowReport => ResourceManager.GetString("CashFlowReport", resourceCulture);

		public static string CategorySalesReport => ResourceManager.GetString("CategorySalesReport", resourceCulture);

		public static string Chart_Of_Account => ResourceManager.GetString("Chart_Of_Account", resourceCulture);

		public static string CloseCashReport => ResourceManager.GetString("CloseCashReport", resourceCulture);

		public static string CloseConfirmationMessage => ResourceManager.GetString("CloseConfirmationMessage", resourceCulture);

		public static string Customer => ResourceManager.GetString("Customer", resourceCulture);

		public static string CustomerDiaryReport => ResourceManager.GetString("CustomerDiaryReport", resourceCulture);

		public static string CustomerItemSalesReport => ResourceManager.GetString("CustomerItemSalesReport", resourceCulture);

		public static string CustomerReport => ResourceManager.GetString("CustomerReport", resourceCulture);

		public static string Customers => ResourceManager.GetString("Customers", resourceCulture);

		public static string CustomerSalesReport => ResourceManager.GetString("CustomerSalesReport", resourceCulture);

		public static string DeleteButton => ResourceManager.GetString("DeleteButton", resourceCulture);

		public static string DeleteConfirmationMessage => ResourceManager.GetString("DeleteConfirmationMessage", resourceCulture);

		public static string DeleteMessage => ResourceManager.GetString("DeleteMessage", resourceCulture);

		public static string Expense_Account => ResourceManager.GetString("Expense_Account", resourceCulture);

		public static string Group => ResourceManager.GetString("Group", resourceCulture);

		public static string Ignore => ResourceManager.GetString("Ignore", resourceCulture);

		public static string NewButton => ResourceManager.GetString("NewButton", resourceCulture);

		public static string No => ResourceManager.GetString("No", resourceCulture);

		public static string OK => ResourceManager.GetString("OK", resourceCulture);

		public static string OpeningStockReport => ResourceManager.GetString("OpeningStockReport", resourceCulture);

		public static string POSBarcodeSelection => ResourceManager.GetString("POSBarcodeSelection", resourceCulture);

		public static string POSBarcodeSelectionCaption => ResourceManager.GetString("POSBarcodeSelectionCaption", resourceCulture);

		public static string POSBarcodeSelectionLabel => ResourceManager.GetString("POSBarcodeSelectionLabel", resourceCulture);

		public static string POSItemMesearment => ResourceManager.GetString("POSItemMesearment", resourceCulture);

		public static string POSItemMesearmentCaption => ResourceManager.GetString("POSItemMesearmentCaption", resourceCulture);

		public static string POSItemMesearmentLabel => ResourceManager.GetString("POSItemMesearmentLabel", resourceCulture);

		public static string POSSalesSummaryReport => ResourceManager.GetString("POSSalesSummaryReport", resourceCulture);

		public static string POSSalesTaxCategoryReport => ResourceManager.GetString("POSSalesTaxCategoryReport", resourceCulture);

		public static string POSSalesTaxReport => ResourceManager.GetString("POSSalesTaxReport", resourceCulture);

		public static string ProductSalesProfitReport => ResourceManager.GetString("ProductSalesProfitReport", resourceCulture);

		public static string ProductStockReport => ResourceManager.GetString("ProductStockReport", resourceCulture);

		public static string ProTranSummaryReport => ResourceManager.GetString("ProTranSummaryReport", resourceCulture);

		public static string ProWisePurchaseReport => ResourceManager.GetString("ProWisePurchaseReport", resourceCulture);

		public static string ReferenceExistsMessage => ResourceManager.GetString("ReferenceExistsMessage", resourceCulture);

		public static string ReferenceExistsMessageForUpdate => ResourceManager.GetString("ReferenceExistsMessageForUpdate", resourceCulture);

		public static string Restaurant => ResourceManager.GetString("Restaurant", resourceCulture);

		public static string Retry => ResourceManager.GetString("Retry", resourceCulture);

		public static string SalesByProductReport => ResourceManager.GetString("SalesByProductReport", resourceCulture);

		public static string SaleTransactionReport => ResourceManager.GetString("SaleTransactionReport", resourceCulture);

		public static string SaveButton => ResourceManager.GetString("SaveButton", resourceCulture);

		public static string SavedMessage => ResourceManager.GetString("SavedMessage", resourceCulture);

		public static string Search => ResourceManager.GetString("Search", resourceCulture);

		public static string StockAndProfitReport => ResourceManager.GetString("StockAndProfitReport", resourceCulture);

		public static string Supplier => ResourceManager.GetString("Supplier", resourceCulture);

		public static string SupplierItemPurchaseReport => ResourceManager.GetString("SupplierItemPurchaseReport", resourceCulture);

		public static string SupplierLedgerReport => ResourceManager.GetString("SupplierLedgerReport", resourceCulture);

		public static string SupplierPurchaseReport => ResourceManager.GetString("SupplierPurchaseReport", resourceCulture);

		public static string SupplierReport => ResourceManager.GetString("SupplierReport", resourceCulture);

		public static string UpdateMessage => ResourceManager.GetString("UpdateMessage", resourceCulture);

		public static string UserName => ResourceManager.GetString("UserName", resourceCulture);

		public static string UserSale => ResourceManager.GetString("UserSale", resourceCulture);

		public static string WarningMessage => ResourceManager.GetString("WarningMessage", resourceCulture);

		public static string Yes => ResourceManager.GetString("Yes", resourceCulture);

		public static string GetReportMessage(string Key)
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(MessageKeys));
			return componentResourceManager.GetString(Key);
		}

		internal MessageKeys()
		{
		}
	}
}
