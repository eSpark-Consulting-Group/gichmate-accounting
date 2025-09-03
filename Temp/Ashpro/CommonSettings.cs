using System;
using System.Collections.Generic;
using System.Linq;
using AshFramework;

namespace Ashpro
{
	public class CommonSettings
	{
		public static List<SettingsEntity> entSettings;

		private static SettingsEntity settingsEntity;

		public static bool blnTaxEnabled = false;

		public static bool blnGSTEnabled = true;

		public static bool blnCessEnabled = false;

		public static bool blnShowVatInInvoice = false;

		public static bool blnShowVatAmountInInvoice = false;

		public static bool blnShowGSTInInvoice = false;

		public static bool blnShowGSTAmountInInvoice = false;

		public static bool blnSalesEstimateEnable = false;

		public static bool blnAutoBackUpWhileClosing = false;

		public static bool blnShowPOS = false;

		public static bool blnShowTransWitInvtry = false;

		public static bool blnShowFtPurchase = false;

		public static bool blnEnableLogOut = false;

		public static bool blnEnableAppLock = false;

		public static bool blnEnableBatch = false;

		public static bool blnEnableMultiBranch = false;

		public static bool blnEnableMultiCompany = false;

		public static bool blnPosBarcodeBased = true;

		public static bool blnInvoicePrintingByRDLC = true;

		public static bool blnUpdateRateFromPurchase = true;

		public static bool blnShowPurchasePrice = true;

		public static bool? blnDefaultCellIsBarcode = false;

		public static bool blnEnableCutomerDisplay = false;

		public static bool blnShowDiscountInInvoice = false;

		public static bool blnEnableBarbarshopMode = false;

		public static bool blnShowPriceOnProduct = true;

		public static bool blnShowSecondDisplay = true;

		public static bool blnTaxInclusive = true;

		public static string strDefaultCompany = null;

		public static string strQuantityFormat = null;

		public static string strCurencyFormat = null;

		public static string strDateFormat = null;

		public static string strDefaultSalesType = null;

		public static string strDefaultPurchaseType = null;

		public static string strEmailProvider = null;

		public static string strClientName = null;

		public static string strClientPort = null;

		public static string strEmailAddress = null;

		public static string strUserName = null;

		public static string strPassword = null;

		public static string strCessPercentage = null;

		public static string strBackupPath = null;

		public static string strRatebasedBarcode;

		public static string strWeightbasedBarcode;

		public static string strDefaultCashAccountId = null;

		public static string strDefaultBankAccountId = null;

		public static string strDefaultOnlineAccountId = null;

		public static string strOnlinePayment = null;

		public static string strDefaultCurrency = null;

		public static string strAddressOneLabel = "Road";

		public static string strAddressTwoLabel = "Building";

		public static string strAddressThreeLabel = "Block";

		public static string strAddressFourLabel = "Flat";

		public static bool blnEditInvoiceNumber = false;

		public static bool blnPntInvWlSaving = false;

		public static bool blnSndMlWlSaving = false;

		public static bool blnCnfmtnWlEditing = false;

		public static bool blnCnfmtnWlDeleting = false;

		public static bool blnPwdWlDeleting = false;

		public static bool blnShwMsgWlDplctItm = false;

		public static bool blnPrvntDplctItm = false;

		public static bool blnShowMsgWlNegativeStock = false;

		public static bool blnShowDiscountAmntInInv = false;

		public static bool blnShowGrossAmntInInv = false;

		public static bool blnShowHSNInInv = false;

		public static bool blnShowSGSTInInv = false;

		public static bool blnShowSGSTAmntInInv = false;

		public static bool blnShowCGSTInInv = false;

		public static bool blnShowCGSTAmntInInv = false;

		public static bool blnShowIGSTInInv = false;

		public static bool blnShowIGSTAmntInInv = false;

		public static bool blnEditInvoiceNumberSE = false;

		public static bool blnPntInvWlSavingSE = false;

		public static bool blnSndMlWlSavingSE = false;

		public static bool blnCnfmtnWlEditingSE = false;

		public static bool blnCnfmtnWlDeletingSE = false;

		public static bool blnPwdWlDeletingSE = false;

		public static bool blnShwMsgWlDplctItmSE = false;

		public static bool blnPrvntDplctItmSE = false;

		public static bool blnShowMsgWlNStockSE = false;

		public static bool blnEditInvoiceNumberSO = false;

		public static bool blnPntInvWlSavingSO = false;

		public static bool blnSndMlWlSavingSO = false;

		public static bool blnCnfmtnWlEditingSO = false;

		public static bool blnCnfmtnWlDeletingSO = false;

		public static bool blnPwdWlDeletingSO = false;

		public static bool blnShwMsgWlDplctItmSO = false;

		public static bool blnPrvntDplctItmSO = false;

		public static bool blnEditInvoiceNumberSQ = false;

		public static bool blnPntInvWlSavingSQ = false;

		public static bool blnSndMlWlSavingSQ = false;

		public static bool blnCnfmtnWlEditingSQ = false;

		public static bool blnCnfmtnWlDeletingSQ = false;

		public static bool blnPwdWlDeletingSQ = false;

		public static bool blnShwMsgWlDplctItmSQ = false;

		public static bool blnPrvntDplctItmSQ = false;

		public static bool blnShowMsgWlNStockSQ = false;

		public static bool blnEditInvoiceNumberSR = false;

		public static bool blnPntInvWlSavingSR = false;

		public static bool blnSndMlWlSavingSR = false;

		public static bool blnCnfmtnWlEditingSR = false;

		public static bool blnCnfmtnWlDeletingSR = false;

		public static bool blnPwdWlDeletingSR = false;

		public static bool blnShwMsgWlDplctItmSR = false;

		public static bool blnPrvntDplctItmSR = false;

		public static bool blnEditInvoiceNumberP = false;

		public static bool blnPntInvWlSavingP = false;

		public static bool blnSndMlWlSavingP = false;

		public static bool blnCnfmtnWlEditingP = false;

		public static bool blnCnfmtnWlDeletingP = false;

		public static bool blnPwdWlDeletingP = false;

		public static bool blnShwMsgWlDplctItmP = false;

		public static bool blnPrvntDplctItmP = false;

		public static bool blnEditInvoiceNumberPO = false;

		public static bool blnPntInvWlSavingPO = false;

		public static bool blnSndMlWlSavingPO = false;

		public static bool blnCnfmtnWlEditingPO = false;

		public static bool blnCnfmtnWlDeletingPO = false;

		public static bool blnPwdWlDeletingPO = false;

		public static bool blnShwMsgWlDplctItmPO = false;

		public static bool blnPrvntDplctItmPO = false;

		public static bool blnEditInvoiceNumberPR = false;

		public static bool blnPntInvWlSavingPR = false;

		public static bool blnSndMlWlSavingPR = false;

		public static bool blnCnfmtnWlEditingPR = false;

		public static bool blnCnfmtnWlDeletingPR = false;

		public static bool blnPwdWlDeletingPR = false;

		public static bool blnShwMsgWlDplctItmPR = false;

		public static bool blnPrvntDplctItmPR = false;

		public static bool blnShowMsgWlNStockPR = false;

		public static bool blnEnableToEditInvoice = false;

		public static bool blnEnableToDeleteInvoice = false;

		public static bool blnEnableOnlineReport = false;

		public static bool blnEnableSRFromInvoice = false;

		public static void LoadSettings()
		{
			try
			{
				settingsEntity = new SettingsEntity();
				entSettings = new List<SettingsEntity>();
				DBContext dBContext = new DBContext();
				entSettings = dBContext.Settings().ToList();
				blnSalesEstimateEnable = entSettings.Where((SettingsEntity x) => x.Name == "blnSalesEstimateEnable").SingleOrDefault().BitValue;
				blnTaxEnabled = entSettings.Where((SettingsEntity x) => x.Name == "blnTaxEnabled").SingleOrDefault().BitValue;
				blnGSTEnabled = entSettings.Where((SettingsEntity x) => x.Name == "blnGSTEnabled").SingleOrDefault().BitValue;
				blnCessEnabled = entSettings.Where((SettingsEntity x) => x.Name == "blnCessEnabled").SingleOrDefault().BitValue;
				blnShowVatInInvoice = entSettings.Where((SettingsEntity x) => x.Name == "blnShowVatInInvoice").SingleOrDefault().BitValue;
				blnShowVatAmountInInvoice = entSettings.Where((SettingsEntity x) => x.Name == "blnShowVatAmountInInvoice").SingleOrDefault().BitValue;
				blnShowGSTInInvoice = entSettings.Where((SettingsEntity x) => x.Name == "blnShowGSTInInvoice").SingleOrDefault().BitValue;
				blnShowGSTAmountInInvoice = entSettings.Where((SettingsEntity x) => x.Name == "blnShowGSTAmountInInvoice").SingleOrDefault().BitValue;
				blnAutoBackUpWhileClosing = entSettings.Where((SettingsEntity x) => x.Name == "blnAutoBackUpWhileClosing").SingleOrDefault().BitValue;
				blnShowPOS = entSettings.Where((SettingsEntity x) => x.Name == "blnShowPOS").SingleOrDefault().BitValue;
				blnShowTransWitInvtry = entSettings.Where((SettingsEntity x) => x.Name == "blnShowTransWitInvtry").SingleOrDefault().BitValue;
				blnShowFtPurchase = entSettings.Where((SettingsEntity x) => x.Name == "blnShowFtPurchase").SingleOrDefault().BitValue;
				blnEnableLogOut = entSettings.Where((SettingsEntity x) => x.Name == "blnEnableLogOut").SingleOrDefault().BitValue;
				blnEnableAppLock = entSettings.Where((SettingsEntity x) => x.Name == "blnEnableAppLock").SingleOrDefault().BitValue;
				blnEnableBatch = entSettings.Where((SettingsEntity x) => x.Name == "blnEnableBatch").SingleOrDefault().BitValue;
				blnEnableMultiBranch = entSettings.Where((SettingsEntity x) => x.Name == "blnEnableMultiBranch").SingleOrDefault().BitValue;
				blnEnableMultiCompany = entSettings.Where((SettingsEntity x) => x.Name == "blnEnableMultiCompany").SingleOrDefault().BitValue;
				blnPosBarcodeBased = entSettings.Where((SettingsEntity x) => x.Name == "blnPosBarcodeBased").SingleOrDefault().BitValue;
				blnInvoicePrintingByRDLC = entSettings.Where((SettingsEntity x) => x.Name == "blnInvoicePrintingByRDLC").SingleOrDefault().BitValue;
				blnUpdateRateFromPurchase = entSettings.Where((SettingsEntity x) => x.Name == "blnUpdateRateFromPurchase").SingleOrDefault().BitValue;
				blnShowPurchasePrice = entSettings.Where((SettingsEntity x) => x.Name == "blnShowPurchasePrice").SingleOrDefault().BitValue;
				blnDefaultCellIsBarcode = entSettings.Where((SettingsEntity x) => x.Name == "blnDefaultCellIsBarcode").FirstOrDefault().BitValue;
				blnEnableCutomerDisplay = entSettings.Where((SettingsEntity x) => x.Name == "blnEnableCutomerDisplay").FirstOrDefault().BitValue;
				settingsEntity = entSettings.Where((SettingsEntity x) => x.Name == "blnShowDiscountInInvoice").FirstOrDefault();
				blnShowDiscountInInvoice = settingsEntity != null && settingsEntity.BitValue;
				settingsEntity = entSettings.Where((SettingsEntity x) => x.Name == "blnEnableBarbarshopMode").FirstOrDefault();
				blnEnableBarbarshopMode = settingsEntity != null && settingsEntity.BitValue;
				settingsEntity = entSettings.Where((SettingsEntity x) => x.Name == "blnShowPriceOnProduct").FirstOrDefault();
				blnShowPriceOnProduct = settingsEntity != null && settingsEntity.BitValue;
				settingsEntity = entSettings.Where((SettingsEntity x) => x.Name == "blnShowSecondDisplay").FirstOrDefault();
				blnShowSecondDisplay = settingsEntity != null && settingsEntity.BitValue;
				settingsEntity = entSettings.Where((SettingsEntity x) => x.Name == "blnEnableToEditInvoice").FirstOrDefault();
				blnEnableToEditInvoice = settingsEntity != null && settingsEntity.BitValue;
				settingsEntity = entSettings.Where((SettingsEntity x) => x.Name == "blnEnableToDeleteInvoice").FirstOrDefault();
				blnEnableToDeleteInvoice = settingsEntity != null && settingsEntity.BitValue;
				settingsEntity = entSettings.Where((SettingsEntity x) => x.Name == "blnTaxInclusive").FirstOrDefault();
				blnTaxInclusive = settingsEntity != null && settingsEntity.BitValue;
				settingsEntity = entSettings.Where((SettingsEntity x) => x.Name == "blnEnableOnlineReport").FirstOrDefault();
				blnEnableOnlineReport = settingsEntity != null && settingsEntity.BitValue;
				settingsEntity = entSettings.Where((SettingsEntity x) => x.Name == "blnEnableSRFromInvoice").FirstOrDefault();
				blnEnableSRFromInvoice = settingsEntity != null && settingsEntity.BitValue;
				strDefaultCompany = entSettings.Where((SettingsEntity x) => x.Name == "strDefaultCompany").SingleOrDefault().StringValue;
				strQuantityFormat = entSettings.Where((SettingsEntity x) => x.Name == "strQuantityFormat").SingleOrDefault().StringValue;
				strCurencyFormat = entSettings.Where((SettingsEntity x) => x.Name == "strCurencyFormat").SingleOrDefault().StringValue;
				strDateFormat = entSettings.Where((SettingsEntity x) => x.Name == "strDateFormat").SingleOrDefault().StringValue;
				strDefaultSalesType = entSettings.Where((SettingsEntity x) => x.Name == "strDefaultSalesType").SingleOrDefault().StringValue;
				strDefaultPurchaseType = entSettings.Where((SettingsEntity x) => x.Name == "strDefaultPurchaseType").SingleOrDefault().StringValue;
				strCessPercentage = entSettings.Where((SettingsEntity x) => x.Name == "strCessPercentage").SingleOrDefault().StringValue;
				strBackupPath = entSettings.Where((SettingsEntity x) => x.Name == "strBackupPath").FirstOrDefault().StringValue;
				strRatebasedBarcode = entSettings.Where((SettingsEntity x) => x.Name == "strRatebasedBarcode").SingleOrDefault().StringValue;
				strWeightbasedBarcode = entSettings.Where((SettingsEntity x) => x.Name == "strWeightbasedBarcode").FirstOrDefault().StringValue;
				settingsEntity = entSettings.Where((SettingsEntity x) => x.Name == "strDefaultCashAccountId").FirstOrDefault();
				strDefaultCashAccountId = ((settingsEntity == null) ? null : settingsEntity.StringValue);
				settingsEntity = entSettings.Where((SettingsEntity x) => x.Name == "strDefaultBankAccountId").FirstOrDefault();
				strDefaultBankAccountId = ((settingsEntity == null) ? null : settingsEntity.StringValue);
				settingsEntity = entSettings.Where((SettingsEntity x) => x.Name == "strDefaultOnlineAccountId").FirstOrDefault();
				strDefaultOnlineAccountId = ((settingsEntity == null) ? null : settingsEntity.StringValue);
				settingsEntity = entSettings.Where((SettingsEntity x) => x.Name == "strOnlinePayment").FirstOrDefault();
				strOnlinePayment = ((settingsEntity == null) ? null : settingsEntity.StringValue);
				settingsEntity = entSettings.Where((SettingsEntity x) => x.Name == "strDefaultCurrency").FirstOrDefault();
				strDefaultCurrency = ((settingsEntity == null) ? null : settingsEntity.StringValue);
				settingsEntity = entSettings.Where((SettingsEntity x) => x.Name == "strAddressOneLabel").FirstOrDefault();
				strAddressOneLabel = ((settingsEntity == null) ? strAddressOneLabel : settingsEntity.StringValue);
				settingsEntity = entSettings.Where((SettingsEntity x) => x.Name == "strAddressTwoLabel").FirstOrDefault();
				strAddressTwoLabel = ((settingsEntity == null) ? strAddressTwoLabel : settingsEntity.StringValue);
				settingsEntity = entSettings.Where((SettingsEntity x) => x.Name == "strAddressThreeLabel").FirstOrDefault();
				strAddressThreeLabel = ((settingsEntity == null) ? strAddressThreeLabel : settingsEntity.StringValue);
				settingsEntity = entSettings.Where((SettingsEntity x) => x.Name == "strAddressFourLabel").FirstOrDefault();
				strAddressFourLabel = ((settingsEntity == null) ? strAddressFourLabel : settingsEntity.StringValue);
				strEmailProvider = entSettings.Where((SettingsEntity x) => x.Name == "strEmailProvider").SingleOrDefault().StringValue;
				strClientName = entSettings.Where((SettingsEntity x) => x.Name == "strClientName").SingleOrDefault().StringValue;
				strClientPort = entSettings.Where((SettingsEntity x) => x.Name == "strClientPort").SingleOrDefault().StringValue;
				strEmailAddress = entSettings.Where((SettingsEntity x) => x.Name == "strEmailAddress").SingleOrDefault().StringValue;
				strUserName = entSettings.Where((SettingsEntity x) => x.Name == "strUserName").SingleOrDefault().StringValue;
				strPassword = entSettings.Where((SettingsEntity x) => x.Name == "strPassword").SingleOrDefault().StringValue;
				blnEditInvoiceNumber = entSettings.Where((SettingsEntity x) => x.Name == "blnEditInvoiceNumber").SingleOrDefault().BitValue;
				blnPntInvWlSaving = entSettings.Where((SettingsEntity x) => x.Name == "blnPntInvWlSaving").SingleOrDefault().BitValue;
				blnSndMlWlSaving = entSettings.Where((SettingsEntity x) => x.Name == "blnSndMlWlSaving").SingleOrDefault().BitValue;
				blnCnfmtnWlEditing = entSettings.Where((SettingsEntity x) => x.Name == "blnCnfmtnWlEditing").SingleOrDefault().BitValue;
				blnCnfmtnWlDeleting = entSettings.Where((SettingsEntity x) => x.Name == "blnCnfmtnWlDeleting").SingleOrDefault().BitValue;
				blnPwdWlDeleting = entSettings.Where((SettingsEntity x) => x.Name == "blnPwdWlDeleting").SingleOrDefault().BitValue;
				blnShwMsgWlDplctItm = entSettings.Where((SettingsEntity x) => x.Name == "blnShwMsgWlDplctItm").SingleOrDefault().BitValue;
				blnPrvntDplctItm = entSettings.Where((SettingsEntity x) => x.Name == "blnPrvntDplctItm").SingleOrDefault().BitValue;
				blnShowMsgWlNegativeStock = entSettings.Where((SettingsEntity x) => x.Name == "blnShowMsgWlNegativeStock").SingleOrDefault().BitValue;
				blnShowDiscountAmntInInv = entSettings.Where((SettingsEntity x) => x.Name == "blnShowDiscountAmntInInv").SingleOrDefault().BitValue;
				blnShowGrossAmntInInv = entSettings.Where((SettingsEntity x) => x.Name == "blnShowGrossAmntInInv").SingleOrDefault().BitValue;
				blnShowHSNInInv = entSettings.Where((SettingsEntity x) => x.Name == "blnShowHSNInInv").SingleOrDefault().BitValue;
				blnShowSGSTInInv = entSettings.Where((SettingsEntity x) => x.Name == "blnShowSGSTInInv").SingleOrDefault().BitValue;
				blnShowSGSTAmntInInv = entSettings.Where((SettingsEntity x) => x.Name == "blnShowSGSTAmntInInv").SingleOrDefault().BitValue;
				blnShowCGSTInInv = entSettings.Where((SettingsEntity x) => x.Name == "blnShowCGSTInInv").SingleOrDefault().BitValue;
				blnShowCGSTAmntInInv = entSettings.Where((SettingsEntity x) => x.Name == "blnShowCGSTAmntInInv").SingleOrDefault().BitValue;
				blnShowIGSTInInv = entSettings.Where((SettingsEntity x) => x.Name == "blnShowIGSTInInv").SingleOrDefault().BitValue;
				blnShowIGSTAmntInInv = entSettings.Where((SettingsEntity x) => x.Name == "blnShowIGSTAmntInInv").SingleOrDefault().BitValue;
				blnEditInvoiceNumberSE = entSettings.Where((SettingsEntity x) => x.Name == "blnEditInvoiceNumberSE").SingleOrDefault().BitValue;
				blnPntInvWlSavingSE = entSettings.Where((SettingsEntity x) => x.Name == "blnPntInvWlSavingSE").SingleOrDefault().BitValue;
				blnSndMlWlSavingSE = entSettings.Where((SettingsEntity x) => x.Name == "blnSndMlWlSavingSE").SingleOrDefault().BitValue;
				blnCnfmtnWlEditingSE = entSettings.Where((SettingsEntity x) => x.Name == "blnCnfmtnWlEditingSE").SingleOrDefault().BitValue;
				blnCnfmtnWlDeletingSE = entSettings.Where((SettingsEntity x) => x.Name == "blnCnfmtnWlDeletingSE").SingleOrDefault().BitValue;
				blnPwdWlDeletingSE = entSettings.Where((SettingsEntity x) => x.Name == "blnPwdWlDeletingSE").SingleOrDefault().BitValue;
				blnShwMsgWlDplctItmSE = entSettings.Where((SettingsEntity x) => x.Name == "blnShwMsgWlDplctItmSE").SingleOrDefault().BitValue;
				blnPrvntDplctItmSE = entSettings.Where((SettingsEntity x) => x.Name == "blnPrvntDplctItmSE").SingleOrDefault().BitValue;
				blnShowMsgWlNStockSE = entSettings.Where((SettingsEntity x) => x.Name == "blnShowMsgWlNStockSE").SingleOrDefault().BitValue;
				blnEditInvoiceNumberSO = entSettings.Where((SettingsEntity x) => x.Name == "blnEditInvoiceNumberSO").SingleOrDefault().BitValue;
				blnPntInvWlSavingSO = entSettings.Where((SettingsEntity x) => x.Name == "blnPntInvWlSavingSO").SingleOrDefault().BitValue;
				blnSndMlWlSavingSO = entSettings.Where((SettingsEntity x) => x.Name == "blnSndMlWlSavingSO").SingleOrDefault().BitValue;
				blnCnfmtnWlEditingSO = entSettings.Where((SettingsEntity x) => x.Name == "blnCnfmtnWlEditingSO").SingleOrDefault().BitValue;
				blnCnfmtnWlDeletingSO = entSettings.Where((SettingsEntity x) => x.Name == "blnCnfmtnWlDeletingSO").SingleOrDefault().BitValue;
				blnPwdWlDeletingSO = entSettings.Where((SettingsEntity x) => x.Name == "blnPwdWlDeletingSO").SingleOrDefault().BitValue;
				blnShwMsgWlDplctItmSO = entSettings.Where((SettingsEntity x) => x.Name == "blnShwMsgWlDplctItmSO").SingleOrDefault().BitValue;
				blnPrvntDplctItmSO = entSettings.Where((SettingsEntity x) => x.Name == "blnPrvntDplctItmSO").SingleOrDefault().BitValue;
				blnEditInvoiceNumberSQ = entSettings.Where((SettingsEntity x) => x.Name == "blnEditInvoiceNumberSQ").SingleOrDefault().BitValue;
				blnPntInvWlSavingSQ = entSettings.Where((SettingsEntity x) => x.Name == "blnPntInvWlSavingSQ").SingleOrDefault().BitValue;
				blnSndMlWlSavingSQ = entSettings.Where((SettingsEntity x) => x.Name == "blnSndMlWlSavingSQ").SingleOrDefault().BitValue;
				blnCnfmtnWlEditingSQ = entSettings.Where((SettingsEntity x) => x.Name == "blnCnfmtnWlEditingSQ").SingleOrDefault().BitValue;
				blnCnfmtnWlDeletingSQ = entSettings.Where((SettingsEntity x) => x.Name == "blnCnfmtnWlDeletingSQ").SingleOrDefault().BitValue;
				blnPwdWlDeletingSQ = entSettings.Where((SettingsEntity x) => x.Name == "blnPwdWlDeletingSQ").SingleOrDefault().BitValue;
				blnShwMsgWlDplctItmSQ = entSettings.Where((SettingsEntity x) => x.Name == "blnShwMsgWlDplctItmSQ").SingleOrDefault().BitValue;
				blnPrvntDplctItmSQ = entSettings.Where((SettingsEntity x) => x.Name == "blnPrvntDplctItmSQ").SingleOrDefault().BitValue;
				blnShowMsgWlNStockSQ = entSettings.Where((SettingsEntity x) => x.Name == "blnShowMsgWlNStockSQ").SingleOrDefault().BitValue;
				blnEditInvoiceNumberSR = entSettings.Where((SettingsEntity x) => x.Name == "blnEditInvoiceNumberSR").SingleOrDefault().BitValue;
				blnPntInvWlSavingSR = entSettings.Where((SettingsEntity x) => x.Name == "blnPntInvWlSavingSR").SingleOrDefault().BitValue;
				blnSndMlWlSavingSR = entSettings.Where((SettingsEntity x) => x.Name == "blnSndMlWlSavingSR").SingleOrDefault().BitValue;
				blnCnfmtnWlEditingSR = entSettings.Where((SettingsEntity x) => x.Name == "blnCnfmtnWlEditingSR").SingleOrDefault().BitValue;
				blnCnfmtnWlDeletingSR = entSettings.Where((SettingsEntity x) => x.Name == "blnCnfmtnWlDeletingSR").SingleOrDefault().BitValue;
				blnPwdWlDeletingSR = entSettings.Where((SettingsEntity x) => x.Name == "blnPwdWlDeletingSR").SingleOrDefault().BitValue;
				blnShwMsgWlDplctItmSR = entSettings.Where((SettingsEntity x) => x.Name == "blnShwMsgWlDplctItmSR").SingleOrDefault().BitValue;
				blnPrvntDplctItmSR = entSettings.Where((SettingsEntity x) => x.Name == "blnPrvntDplctItmSR").SingleOrDefault().BitValue;
				blnEditInvoiceNumberP = entSettings.Where((SettingsEntity x) => x.Name == "blnEditInvoiceNumberP").SingleOrDefault().BitValue;
				blnPntInvWlSavingP = entSettings.Where((SettingsEntity x) => x.Name == "blnPntInvWlSavingP").SingleOrDefault().BitValue;
				blnSndMlWlSavingP = entSettings.Where((SettingsEntity x) => x.Name == "blnSndMlWlSavingP").SingleOrDefault().BitValue;
				blnCnfmtnWlEditingP = entSettings.Where((SettingsEntity x) => x.Name == "blnCnfmtnWlEditingP").SingleOrDefault().BitValue;
				blnCnfmtnWlDeletingP = entSettings.Where((SettingsEntity x) => x.Name == "blnCnfmtnWlDeletingP").SingleOrDefault().BitValue;
				blnPwdWlDeletingP = entSettings.Where((SettingsEntity x) => x.Name == "blnPwdWlDeletingP").SingleOrDefault().BitValue;
				blnShwMsgWlDplctItmP = entSettings.Where((SettingsEntity x) => x.Name == "blnShwMsgWlDplctItmP").SingleOrDefault().BitValue;
				blnPrvntDplctItmP = entSettings.Where((SettingsEntity x) => x.Name == "blnPrvntDplctItmP").SingleOrDefault().BitValue;
				blnEditInvoiceNumberPO = entSettings.Where((SettingsEntity x) => x.Name == "blnEditInvoiceNumberPO").SingleOrDefault().BitValue;
				blnPntInvWlSavingPO = entSettings.Where((SettingsEntity x) => x.Name == "blnPntInvWlSavingPO").SingleOrDefault().BitValue;
				blnSndMlWlSavingPO = entSettings.Where((SettingsEntity x) => x.Name == "blnSndMlWlSavingPO").SingleOrDefault().BitValue;
				blnCnfmtnWlEditingPO = entSettings.Where((SettingsEntity x) => x.Name == "blnCnfmtnWlEditingPO").SingleOrDefault().BitValue;
				blnCnfmtnWlDeletingPO = entSettings.Where((SettingsEntity x) => x.Name == "blnCnfmtnWlDeletingPO").SingleOrDefault().BitValue;
				blnPwdWlDeletingPO = entSettings.Where((SettingsEntity x) => x.Name == "blnPwdWlDeletingPO").SingleOrDefault().BitValue;
				blnShwMsgWlDplctItmPO = entSettings.Where((SettingsEntity x) => x.Name == "blnShwMsgWlDplctItmPO").SingleOrDefault().BitValue;
				blnPrvntDplctItmPO = entSettings.Where((SettingsEntity x) => x.Name == "blnPrvntDplctItmPO").SingleOrDefault().BitValue;
				blnEditInvoiceNumberPR = entSettings.Where((SettingsEntity x) => x.Name == "blnEditInvoiceNumberPR").SingleOrDefault().BitValue;
				blnPntInvWlSavingPR = entSettings.Where((SettingsEntity x) => x.Name == "blnPntInvWlSavingPR").SingleOrDefault().BitValue;
				blnSndMlWlSavingPR = entSettings.Where((SettingsEntity x) => x.Name == "blnSndMlWlSavingPR").SingleOrDefault().BitValue;
				blnCnfmtnWlEditingPR = entSettings.Where((SettingsEntity x) => x.Name == "blnCnfmtnWlEditingPR").SingleOrDefault().BitValue;
				blnCnfmtnWlDeletingPR = entSettings.Where((SettingsEntity x) => x.Name == "blnCnfmtnWlDeletingPR").SingleOrDefault().BitValue;
				blnPwdWlDeletingPR = entSettings.Where((SettingsEntity x) => x.Name == "blnPwdWlDeletingPR").SingleOrDefault().BitValue;
				blnShwMsgWlDplctItmPR = entSettings.Where((SettingsEntity x) => x.Name == "blnShwMsgWlDplctItmPR").SingleOrDefault().BitValue;
				blnPrvntDplctItmPR = entSettings.Where((SettingsEntity x) => x.Name == "blnPrvntDplctItmPR").SingleOrDefault().BitValue;
				blnShowMsgWlNStockPR = entSettings.Where((SettingsEntity x) => x.Name == "blnShowMsgWlNStockPR").SingleOrDefault().BitValue;
			}
			catch (Exception)
			{
			}
		}

		public static bool UpdateSettings(List<SettingsEntity> settingsEntities, string strConnection)
		{
			foreach (SettingsEntity settingsEntity in settingsEntities)
			{
				AshproCommon.UpdateToDatabaseObj(settingsEntity, "Settings", "Id", settingsEntity.Id.toInt32());
			}
			LoadSettings();
			return true;
		}

		public static List<MasterValue> GetFormType()
		{
			if (blnGSTEnabled)
			{
				return AshproCommon.GetListMethod<MasterValue>("Select * from MasterValue Where MasterType='GSTFormTypes'");
			}
			return AshproCommon.GetListMethod<MasterValue>("Select * from MasterValue Where MasterType='FormType'");
		}
	}
}
