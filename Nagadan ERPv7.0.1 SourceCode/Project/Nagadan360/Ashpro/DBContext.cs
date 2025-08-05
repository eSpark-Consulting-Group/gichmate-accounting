using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Ashpro
{
	public class DBContext
	{
		public List<AccountEntity> Accounts(object entity = null, string sCon = null)
		{
			return ContextHelper.GetAccounts("Account", entity, sCon);
		}

		public List<AccountGroupEntity> AccountGroup(object entity = null, string sCon = null)
		{
			return ContextHelper.GetAccountGroups("AccountGroup", entity, sCon);
		}

		public List<AttenMaster> AttenMasters(object entity = null, string sCon = null)
		{
			return ContextHelper.GetAttenMasters("AttenMaster", entity, sCon);
		}

		public List<AttenSettings> AttenSettings(object entity = null, string sCon = null)
		{
			return ContextHelper.GetAttenSettings("AttenSettings", entity, sCon);
		}

		public List<BarcodeHDR> BarcodeHDR(object entity = null, string sCon = null)
		{
			return ContextHelper.GetBarcodeHDRs("BarcodeHDR", entity, sCon);
		}

		public List<BarcodeDTL> BarcodeDTL(object entity = null, string sCon = null)
		{
			return ContextHelper.GetBarcodeDTLs("BarcodeDTL", entity, sCon);
		}

		public List<BarcodeScale> BarcodeScale(object entity = null, string sCon = null)
		{
			return ContextHelper.GetBarcodeScales("BarcodeScale", entity, sCon);
		}

		public List<Batch> Batches(object entity = null, string sCon = null)
		{
			return ContextHelper.GetBatchs("Batch", entity, sCon);
		}

		public List<Branch> Branches(object entity = null, string sCon = null)
		{
			return ContextHelper.GetBranchs("Branch", entity, sCon);
		}

		public List<BrandEntity> Brands(object entity = null, string sCon = null)
		{
			return ContextHelper.GetBrands("Brand", entity, sCon);
		}

		public List<Company> Companies(object entity = null, string sCon = null)
		{
			return ContextHelper.GetInstitutes("Institute", entity, sCon);
		}

		public List<ChequeDetails> ChequeDetails(object entity = null, string sCon = null)
		{
			return ContextHelper.GetChequeDetailss("ChequeDetails", entity, sCon);
		}

		public List<CostCenter> CostCenters(object entity = null, string sCon = null)
		{
			return ContextHelper.GetCostCenters("CostCenter", entity, sCon);
		}

		public List<CulenderPlanning> CulenderPlannings(object entity = null, string sCon = null)
		{
			return ContextHelper.GetCulenderPlannings("CulenderPlanning", entity, sCon);
		}

		public List<CurrencyInfo> CurrencyInfos(object entity = null, string sCon = null)
		{
			return ContextHelper.GetCurrencyInfos("CurrencyInfo", entity, sCon);
		}

		public List<CustomQuery> CustomQueries(object entity = null, string sCon = null)
		{
			return ContextHelper.GetCustomQuerys("CustomQuery", entity, sCon);
		}

		public List<DebitCreditCardMasterEntity> DebitCreditCardMasters(object entity = null, string sCon = null)
		{
			return ContextHelper.GetDebitCreditCardMasters("DebitCreditCardMaster", entity, sCon);
		}

		public List<EmployeeDetailsEntity> EmployeeDetails(object entity = null, string sCon = null)
		{
			return ContextHelper.GetEmployeeDetailss("EmployeeDetails", entity, sCon);
		}

		public List<FinancialYear> FinancialYears(object entity = null, string sCon = null)
		{
			return ContextHelper.GetFinancialYears("FinancialYear", entity, sCon);
		}

		public List<GdwnStckTranLog> GdwnStckTranLogs(object entity = null, string sCon = null)
		{
			return ContextHelper.GetGdwnStckTranLogs("GdwnStckTranLog", entity, sCon);
		}

		public List<GodownEntity> Godowns(object entity = null, string sCon = null)
		{
			return ContextHelper.GetGodowns("Godown", entity, sCon);
		}

		public List<ItemNote> ItemNotes(object entity = null, string sCon = null)
		{
			return ContextHelper.GetItemNotes("ItemNote", entity, sCon);
		}

		public List<KOTOrderMaster> KOTOrderMasters(object entity = null, string sCon = null)
		{
			return ContextHelper.GetKOTOrderMasters("KOTOrderMaster", entity, sCon);
		}

		public List<KOTOrderSettings> KOTOrderSettingss(object entity = null, string sCon = null)
		{
			return ContextHelper.GetKOTOrderSettingss("KOTOrderSettings", entity, sCon);
		}

		public List<KOTPrinter> KOTPrinters(object entity = null, string sCon = null)
		{
			return ContextHelper.GetKOTPrinters("KOTPrinter", entity, sCon);
		}

		public List<ManufactureDtls> ManufactureDtls(object entity = null, string sCon = null)
		{
			return ContextHelper.GetManufactureDtls("ManufactureDtls", entity, sCon);
		}

		public List<ManufactureHdr> ManufactureHdrs(object entity = null, string sCon = null)
		{
			return ContextHelper.GetManufactureHdrs("ManufactureHdr", entity, sCon);
		}

		public List<ManufacturingExp> ManufacturingExps(object entity = null, string sCon = null)
		{
			return ContextHelper.GetManufacturingExps("ManufacturingExp", entity, sCon);
		}

		public List<Masters> Masters(DataTable data)
		{
			return ContextHelper.GetMasters(data);
		}

		public List<MasterValue> MasterValues(object entity = null, string sCon = null)
		{
			return ContextHelper.GetMasterValues("MasterValue", entity, sCon);
		}

		public List<MenuDetails> MenuDetails(object entity = null, string sCon = null)
		{
			return ContextHelper.GetMenuDetails("MenuDetails", entity, sCon);
		}

		public List<SalesMaster> POSHoldMasters(object entity = null, string sCon = null)
		{
			return ContextHelper.GetPOSHoldMasters("POSHoldMaster", entity, sCon);
		}

		public List<SalesDetails> POSHoldSettings(object entity = null, string sCon = null)
		{
			return ContextHelper.GetPOSHoldSettings("POSHoldSettings", entity, sCon);
		}

		public List<PrevilegeCard> PrevilegeCards(object entity = null, string sCon = null)
		{
			return ContextHelper.GetPrevilegeCards("PrevilegeCard", entity, sCon);
		}

		public List<PriceCategoryEntity> PriceCategories(object entity = null, string sCon = null)
		{
			return ContextHelper.GetPriceCategories("PriceCategory", entity, sCon);
		}

		public List<DotmatrixMaster> DotmatrixMasters(object entity = null, string sCon = null)
		{
			return ContextHelper.GetPrintDesignMasters("PrintDesignMaster", entity, sCon);
		}

		public List<DotmatrixSettings> DotmatrixSettings(object entity = null, string sCon = null)
		{
			return ContextHelper.GetPrintDesignSettingss("PrintDesignSettings", entity, sCon);
		}

		public List<ProCombMaster> ProCombMasters(object entity = null, string sCon = null)
		{
			return ContextHelper.GetProCombMasters("ProCombMaster", entity, sCon);
		}

		public List<ProCombSettings> ProCombSettings(object entity = null, string sCon = null)
		{
			return ContextHelper.GetProCombSettings("ProCombSettings", entity, sCon);
		}

		public List<ProductEntity> Products(object entity = null, DataTable data = null, string sCon = null)
		{
			return ContextHelper.GetProducts("Product", entity, data, sCon);
		}

		public async Task<List<ExProductPOS>> POSProducts(object entity = null)
		{
			return await Task.Run(() => ContextHelper.GetPOSProducts("spProductLoadForPOS", entity));
		}

		public List<ProductBarcodeEntity> ProductBarcodes(object entity = null, string sCon = null)
		{
			return ContextHelper.GetProductBarcodes("ProductBarcode", entity, sCon);
		}

		public List<ProductBarcodeScale> ProductBarcodeScales(object entity = null, string sCon = null)
		{
			return ContextHelper.GetProductBarcodeScales("ProductBarcodeScale", entity, sCon);
		}

		public List<ProductGroupEntity> ProductGroups(object entity = null, string sCon = null)
		{
			return ContextHelper.GetProductGroups("ProductGroup", entity, sCon);
		}

		public List<ProductPriceCategory> ProductPriceCategories(object entity = null, string sCon = null)
		{
			return ContextHelper.GetProductPriceCategorys("ProductPriceCategory", entity, sCon);
		}

		public List<ProductPrinter> ProductPrinters(object entity = null, string sCon = null)
		{
			return ContextHelper.GetProductPrinters("ProductPrinter", entity, sCon);
		}

		public List<ProductUnit> Productunits(object entity = null, string sCon = null)
		{
			return ContextHelper.GetProductUnits("ProductUnit", entity, sCon);
		}

		public List<PurchaseInvoiceHdr> PurchaseInvoiceHdrs(object entity = null, string sCon = null)
		{
			return ContextHelper.GetPurInvMasters("PurInvMaster", entity, sCon);
		}

		public List<PurchaseInvoiceDtl> PurInvSettings(object entity = null, string sCon = null)
		{
			return ContextHelper.GetPurInvSettings("PurInvSettings", entity, sCon);
		}

		public List<PurRtnInvMaster> PurRtnInvMasters(object entity = null, string sCon = null)
		{
			return ContextHelper.GetPurRtnInvMasters("PurRtnInvMaster", entity, sCon);
		}

		public List<PurRtnInvSttings> PurRtnInvSttings(object entity = null, string sCon = null)
		{
			return ContextHelper.GetPurRtnInvSttings("PurRtnInvSttings", entity, sCon);
		}

		public List<RateFormat> RateFormats(object entity = null, string sCon = null)
		{
			return ContextHelper.GetRateFormats("RateFormat", entity, sCon);
		}

		public List<RawmaterialMappingEntity> RawmaterialMappings(object entity = null, string sCon = null)
		{
			return ContextHelper.GetRawmaterialMappings("RawmaterialMapping", entity, sCon);
		}

		public List<ReceiptAndPayment> ReceiptAndPayments(object entity = null, string sCon = null)
		{
			return ContextHelper.GetReceiptAndPayments("ReceiptAndPayment", entity, sCon);
		}

		public List<ReminderTB> Reminders(object entity = null, string sCon = null)
		{
			return ContextHelper.GetReminderTBs("ReminderTB", entity, sCon);
		}

		public List<RestaurantTable> RestaurantTables(object entity = null, string sCon = null)
		{
			return ContextHelper.GetRestaurantTables("RestaurantTable", entity, sCon);
		}

		public List<RouteEntity> Routes(object entity = null, string sCon = null)
		{
			return ContextHelper.GetRoutes("Root", entity, sCon);
		}

		public List<SalaryProcessMaster> SalaryProcesses(object entity = null, string sCon = null)
		{
			return ContextHelper.GetSalaryProcessMasters("SalaryProcessMaster", entity, sCon);
		}

		public List<SlryPrcsSettings> SlryPrcsSettings(object entity = null, string sCon = null)
		{
			return ContextHelper.GetSlryPrcsSettings("SlryPrcsSettings", entity, sCon);
		}

		public List<SalesCounterEntity> SalesCounter(object entity = null, string sCon = null)
		{
			return ContextHelper.GetSalesCounters("SalesCounter", entity, sCon);
		}

		public List<SalesMaster> SalesMasters(object entity = null, string sCon = null)
		{
			// Add branch filter to entity if not present
			if (entity == null)
			{
				entity = new { FK_BranchId = Utility.iBranchId };
			}
			else
			{
				// Use reflection or dynamic to add/override FK_BranchId
			}
			return ContextHelper.GetSalesEstimateMasters("SalesEstimateMaster", entity, sCon);
		}

		public List<SalesDetails> SalesDetails(object entity = null, string sCon = null)
		{
			return ContextHelper.GetSalesEstimateSettings("SalesEstimateSettings", entity, sCon);
		}

		public List<SalesOrderMaster> SalesOrderMasters(object entity = null, string sCon = null)
		{
			return ContextHelper.GetSalesOrderMasters("SalesOrderMaster", entity, sCon);
		}

		public List<SalesOrderSettings> SalesOrderSettings(object entity = null, string sCon = null)
		{
			return ContextHelper.GetSalesOrderSettings("SalesOrderSettings", entity, sCon);
		}

		public List<SalesRtnMaster> SalesRtnMasters(object entity = null, string sCon = null)
		{
			return ContextHelper.GetSalesRtnMasters("SalesRtnMaster", entity, sCon);
		}

		public List<SalesRtnDetails> SalesRtnDetails(object entity = null, string sCon = null)
		{
			return ContextHelper.GetSlsRtnSettings("SlsRtnSettings", entity, sCon);
		}

		public List<SelectedFYear> SelectedFYears(object entity = null, string sCon = null)
		{
			return ContextHelper.GetSelectedFYears("SelectedFYear", entity, sCon);
		}

		public List<SelfEntity> Shelfs(object entity = null, string sCon = null)
		{
			return ContextHelper.GetSelfs("Self", entity, sCon);
		}

		public List<SettingsEntity> Settings(object entity = null, string sCon = null)
		{
			return ContextHelper.GetSettings("Settings", entity, sCon);
		}

		public List<StockAdjusterMaster> StockAdjusterMasters(object entity = null, string sCon = null)
		{
			return ContextHelper.GetStockAdjusterMasters("StockAdjusterMaster", entity, sCon);
		}

		public List<StockAdjustSettings> StockAdjustSettings(object entity = null, string sCon = null)
		{
			return ContextHelper.GetStockAdjustSettings("StockAdjustSettings", entity, sCon);
		}

		public List<StockTranLog> StockTranLogs(object entity = null, string sCon = null)
		{
			return ContextHelper.GetStockTranLogs("StockTranLog", entity, sCon);
		}

		public List<StockTransferMaster> StockTransferMasters(object entity = null, string sCon = null)
		{
			return ContextHelper.GetStockTransferMasters("StockTransferMaster", entity, sCon);
		}

		public List<StockTranSettings> StockTranSettings(object entity = null, string sCon = null)
		{
			return ContextHelper.GetStockTranSettings("StockTranSettings", entity, sCon);
		}

		public List<TaxCategoryEntity> TaxCategories(object entity = null, string sCon = null)
		{
			return ContextHelper.GetTaxCategorys("TaxCategory", entity, sCon);
		}

		public List<TransactionLog> TransactionLogs(object entity = null, string sCon = null)
		{
			return ContextHelper.GetTransactionLogs("TransactionLog", entity, sCon);
		}

		public List<Translator> Translators(object entity = null, string sCon = null)
		{
			return ContextHelper.GetTranslators("Translator", entity, sCon);
		}

		public List<UnitEntity> Units(object entity = null, string sCon = null)
		{
			return ContextHelper.GetUnits("Unit", entity, sCon);
		}

		public List<User> Users(object entity = null, string sCon = null)
		{
			return ContextHelper.GetUsers("User", entity, sCon);
		}

		public List<UserPrevilegeHdr> UserPrevilegeHdrs(object entity = null, string sCon = null)
		{
			return ContextHelper.GetUserPrevilegeHdrs("UserPrevilegeHdr", entity, sCon);
		}

		public List<UserPrevilegeDtl> UserPrevilegeDtls(object entity = null, string sCon = null)
		{
			return ContextHelper.GetUserPrevilegeDtls("UserPrevilegeDtl", entity, sCon);
		}

		public List<VchrTranLog> VchrTransaction(object entity = null, string sCon = null)
		{
			return ContextHelper.GetVchrTranLogs("VchrTranLog", entity, sCon);
		}

		public List<VehicleEntity> Vehicles(object entity = null, string sCon = null)
		{
			return ContextHelper.GetVehicles("Vehicle", entity, sCon);
		}
	}
}
