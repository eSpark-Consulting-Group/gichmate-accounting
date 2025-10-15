using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AccountsLibrary.Properties;
using AccountsPro.SubProjects.AccountsLibraryFramework;

namespace AccountsLibrary
{
	public class POSHelper
	{
		private static DBContext db = new DBContext();

		public static decimal dmlRate { get; set; }

		public static decimal dmlDiscount { get; set; }

		public static PartyDetails GetParty(AccountEntity account)
		{
			return new PartyDetails
			{
				Name = account.AccountName,
				AddressOne = account.AddressOne,
				AddressTwo = account.AddressTwo,
				AddressThree = account.AddressThree,
				AddressFour = account.AddressFour,
				Phone = account.PhoneOne,
				Mobile = account.MobileNo,
				Fax = account.FaxNo,
				TaxNoOne = account.TinNo,
				TaxNoTwo = account.CstNo,
				Email = account.EMailOne
			};
		}

		public static TransactionMaster GetTMaster(SalesMaster sMaster, bool isCashAccount = false)
		{
			DBContext dBContext = new DBContext();
			Company company = dBContext.Companies().FirstOrDefault();
			TransactionMaster transactionMaster = new TransactionMaster();
			GlobalFunctions.CopyData(sMaster, transactionMaster);
			transactionMaster.InvoiceNo = sMaster.SalesInvNo.ToString();
			transactionMaster.CntrInvNo = sMaster.CntrInvNo;
			transactionMaster.InvoiceType = sMaster.InvType;
			transactionMaster.Date = sMaster.SalesDate;
			transactionMaster.AdditionalAmount = sMaster.AditionalAmount;
			transactionMaster.Taxable = sMaster.TaxableAmount;
			transactionMaster.CessAmount = sMaster.FloodCess;
			transactionMaster.GSTAmount = sMaster.TotalTaxAmount;
			transactionMaster.VatAmount = sMaster.TotalTaxAmount;
			transactionMaster.NetAmount = sMaster.GrandDiscount - (sMaster.AditionalAmount + sMaster.Frieght + sMaster.FloodCess + sMaster.BillDiscount);
			transactionMaster.GrandTotal = sMaster.GrandDiscount;
			if (CommonSettings.strDefaultCurrency != string.Empty)
			{
				ToWord toWord = new ToWord(Math.Abs(sMaster.GrandDiscount), dBContext.CurrencyInfos(new
				{
					CurrencyID = CommonSettings.strDefaultCurrency
				}).FirstOrDefault());
				transactionMaster.GrandTotalToWords = toWord.ConvertToEnglish();
				transactionMaster.GrandTotalToArabicWords = toWord.ConvertToArabic();
			}
			DateTime dateTime = DateTime.Now.Date.AddHours(24.0).AddSeconds(-1.0);
			string text2 = (transactionMaster.RunningBalance = (isCashAccount ? "0" : AniHelper.AccountBalanceFindMethod(sMaster.PartyAccountId.ToInt32(), AniHelper.GetDate(dateTime))));
			transactionMaster.InvoiceBarcode = sMaster.SalesInvNo.ToString();
			transactionMaster.InvoiceQRCode = AniHelper.GenerateBase64(company.InstituteName, company.GSTNo, sMaster.SalesDate.ToString("yyyy-MM-ddTHH:mm:ss"), sMaster.GrandDiscount.ToString("N2"), sMaster.TotalTaxAmount.ToString("N2"));
			transactionMaster.RefNumber = sMaster.FielOne;
			transactionMaster.Remarks = sMaster.Remarks;
			return transactionMaster;
		}

		public static TransactionMaster GetTMaster(PurchaseInvoiceHdr pMaster)
		{
			return new TransactionMaster
			{
				InvoiceNo = pMaster.RefInvNo,
				InvoiceType = pMaster.InvType,
				Date = pMaster.PurDate,
				AdditionalAmount = pMaster.AdditionalAmnt,
				Frieght = pMaster.Frieght,
				TotalDiscount = pMaster.TotalDiscount,
				BillDiscount = pMaster.BillDisc,
				Taxable = pMaster.Taxable,
				CessAmount = 0m,
				IGSTAmount = pMaster.IGSTAmount,
				SGSTAmount = pMaster.SGSTAmount,
				CGSTAmount = pMaster.CGSTAmount,
				GSTAmount = pMaster.TotalTax,
				VatAmount = pMaster.TotalTax,
				GrandTotal = pMaster.TotalAmnt,
				GrandTotalToWords = AniHelper.GetAmountInWords(Math.Round(pMaster.TotalAmnt).ToInt32()) + " Only.",
				RefNumber = pMaster.RefInvNo,
				Remarks = pMaster.Remarks
			};
		}

		public static TransactionMaster GetTMaster(SalesRtnMaster srMaster, bool isCashAccount = false)
		{
			DBContext dBContext = new DBContext();
			Company company = dBContext.Companies().FirstOrDefault();
			TransactionMaster transactionMaster = new TransactionMaster();
			transactionMaster.InvoiceNo = srMaster.SlRtnInvNo.ToString();
			transactionMaster.InvoiceType = "Sales Return";
			transactionMaster.Date = srMaster.SlsRtnDate;
			transactionMaster.AdditionalAmount = srMaster.AditionalAmnt;
			transactionMaster.Frieght = srMaster.Frieght;
			transactionMaster.TotalDiscount = srMaster.TotalDiscount;
			transactionMaster.BillDiscount = srMaster.BillDiscount;
			transactionMaster.Taxable = srMaster.Taxable;
			transactionMaster.CessAmount = 0m;
			transactionMaster.IGSTAmount = srMaster.IGSTAmount;
			transactionMaster.SGSTAmount = srMaster.SGSTAmount;
			transactionMaster.CGSTAmount = srMaster.CGSTAmount;
			transactionMaster.GSTAmount = srMaster.TotalTax;
			transactionMaster.VatAmount = srMaster.TotalTax;
			transactionMaster.GrandTotal = srMaster.TotalAmount;
			if (CommonSettings.strDefaultCurrency != string.Empty)
			{
				ToWord toWord = new ToWord(Math.Abs(transactionMaster.GrandTotal), dBContext.CurrencyInfos(new
				{
					CurrencyID = CommonSettings.strDefaultCurrency
				}).FirstOrDefault());
				transactionMaster.GrandTotalToWords = toWord.ConvertToEnglish();
				transactionMaster.GrandTotalToArabicWords = toWord.ConvertToArabic();
			}
			DateTime dateTime = DateTime.Now.Date.AddHours(24.0).AddSeconds(-1.0);
			string text2 = (transactionMaster.RunningBalance = (isCashAccount ? "0" : AniHelper.AccountBalanceFindMethod(srMaster.PartyAccountId.ToInt32(), AniHelper.GetDate(dateTime))));
			transactionMaster.InvoiceBarcode = srMaster.SlRtnInvNo.ToString();
			transactionMaster.InvoiceQRCode = AniHelper.GenerateBase64(company.InstituteName, company.GSTNo, srMaster.SlsRtnDate.ToString("yyyy-MM-ddTHH:mm:ss"), srMaster.TotalTax.ToString("N2"), srMaster.TotalAmount.ToString("N2"));
			transactionMaster.RefNumber = srMaster.RefInvNo;
			transactionMaster.Remarks = srMaster.Remarks;
			return transactionMaster;
		}

		public static TransactionMaster GetTMaster(PurRtnInvMaster prMaster)
		{
			return new TransactionMaster
			{
				InvoiceNo = prMaster.PurRtnInvNo.ToString(),
				InvoiceType = "Purchase Return",
				Date = prMaster.PurRtnDate,
				AdditionalAmount = prMaster.AdditionalAmnt,
				Frieght = prMaster.Frieght,
				TotalDiscount = prMaster.TotalDiscount,
				BillDiscount = prMaster.BillDisc,
				Taxable = prMaster.Taxable,
				CessAmount = 0m,
				IGSTAmount = prMaster.IGSTAmount,
				SGSTAmount = prMaster.SGSTAmount,
				CGSTAmount = prMaster.CGSTAmount,
				GSTAmount = prMaster.TotalTax,
				VatAmount = prMaster.TotalTax,
				GrandTotal = prMaster.TotalAmnt,
				GrandTotalToWords = AniHelper.GetAmountInWords(Math.Round(prMaster.TotalAmnt).ToInt32()) + " Only.",
				Remarks = prMaster.Remarks
			};
		}

		public static TransactionMaster GetTMaster(SalesOrderMaster soMaster)
		{
			TransactionMaster transactionMaster = new TransactionMaster();
			transactionMaster.InvoiceNo = soMaster.VchrNo.ToString();
			transactionMaster.InvoiceType = soMaster.InvType;
			transactionMaster.Date = soMaster.InvDate;
			transactionMaster.AdditionalAmount = soMaster.AdditionalAmnt;
			transactionMaster.Frieght = soMaster.Freight;
			transactionMaster.TotalDiscount = soMaster.BillDiscount;
			transactionMaster.BillDiscount = soMaster.AddDiscount;
			transactionMaster.Taxable = soMaster.Taxable;
			transactionMaster.GSTAmount = soMaster.TotalTax;
			transactionMaster.VatAmount = soMaster.TotalTax;
			transactionMaster.GrandTotal = soMaster.GrandTotal;
			transactionMaster.Remarks = soMaster.Remarks;
			if (CommonSettings.strDefaultCurrency != string.Empty)
			{
				ToWord toWord = new ToWord(Math.Abs(soMaster.GrandTotal), db.CurrencyInfos(new
				{
					CurrencyID = CommonSettings.strDefaultCurrency
				}).FirstOrDefault());
				transactionMaster.GrandTotalToWords = toWord.ConvertToEnglish();
				transactionMaster.GrandTotalToArabicWords = toWord.ConvertToArabic();
			}
			return transactionMaster;
		}

		public static List<Transaction> GetTransactionFromPOSDetails(List<POSDetails> pOSDetails, DBContext db, string sType, bool isRestuaren = false)
		{
			try
			{
				int num = 1;
				List<Transaction> list = new List<Transaction>();
				foreach (POSDetails pOSDetail in pOSDetails)
				{
					ProductEntity productEntity = new ProductEntity();
					productEntity = db.Products(new
					{
						ProductId = pOSDetail.productId
					}).FirstOrDefault();
					if (productEntity == null)
					{
						continue;
					}
					Transaction transaction = new Transaction();
					transaction.SL = num;
					transaction.ProductId = pOSDetail.productId;
					transaction.ProductCode = pOSDetail.ProductCode.ToInt32();
					transaction.ProductName = pOSDetail.ProductName;
					transaction.ProductName = pOSDetail.ProductName;
					transaction.LocalName = pOSDetail.LocalName;
					transaction.Barcode = pOSDetail.Barcode;
					transaction.Quantity = pOSDetail.Qty;
					transaction.Unit = pOSDetail.UnitName;
					transaction.Rate = pOSDetail.Rate;
					transaction.GrossAmount = pOSDetail.Gross;
					transaction.Discount = pOSDetail.Discount;
					transaction.DiscountAmount = pOSDetail.DisAmount;
					transaction.Vat = pOSDetail.Tax;
					transaction.VatAmount = pOSDetail.TaxAmount;
					transaction.Gst = transaction.Vat;
					transaction.GstAmount = transaction.VatAmount;
					transaction.Hsn = productEntity.HSNNo;
					if (CommonSettings.blnGSTEnabled)
					{
						if (sType.Contains("Interstate"))
						{
							transaction.IGst = transaction.Gst;
							transaction.IGstAmount = transaction.GstAmount;
							transaction.Sgst = 0m;
							transaction.SGstAmount = 0m;
							transaction.CGst = 0m;
							transaction.CGstAmount = 0m;
						}
						else
						{
							transaction.IGst = 0m;
							transaction.IGstAmount = 0m;
							transaction.Sgst = transaction.Gst / 2m;
							transaction.SGstAmount = transaction.GstAmount / 2m;
							transaction.CGst = transaction.Gst / 2m;
							transaction.CGstAmount = transaction.GstAmount / 2m;
						}
					}
					else
					{
						transaction.IGst = 0m;
						transaction.IGstAmount = 0m;
						transaction.Sgst = 0m;
						transaction.SGstAmount = 0m;
						transaction.CGst = 0m;
						transaction.CGstAmount = 0m;
					}
					transaction.NETAmount = pOSDetail.NetAmount;
					transaction.Taxable = transaction.NETAmount - transaction.GstAmount;
					transaction.ItemImage = pOSDetail.productId.ToString();
					transaction.ItemQRCode = transaction.Barcode + "#" + transaction.Quantity.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Rate.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.GrossAmount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Discount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.DiscountAmount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Taxable.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Gst.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.GstAmount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.NETAmount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Unit;
					transaction.ItemNote = pOSDetail.ItemNote;
					transaction.ItemSerialNumber = pOSDetail.ItemSerialNumber;
					list.Add(transaction);
					num++;
				}
				return list;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static List<Transaction> GetTransactionsFromSDetails(List<SalesDetails> salesDetails, DBContext db)
		{
			try
			{
				int iRowIndex = 1;
				List<Transaction> transactions = new List<Transaction>();
				salesDetails.ForEach(delegate(SalesDetails x)
				{
					ProductEntity productEntity = new ProductEntity();
					productEntity = db.Products(new { x.ProductId }).FirstOrDefault();
					if (productEntity != null)
					{
						Transaction transaction = new Transaction
						{
							SL = iRowIndex,
							ProductId = x.ProductId,
							ProductCode = productEntity.ProductCode.toInt32()
						};
						if (x.ProductName != string.Empty)
						{
							transaction.ProductName = x.ProductName;
						}
						else
						{
							ProductUnit productUnit = db.Productunits(new { x.ProductId, x.UnitId }).FirstOrDefault();
							transaction.ProductName = ((productUnit == null) ? productEntity.ProductName : productUnit.ProductUnitName);
						}
						transaction.LocalName = productEntity.LocalName;
						transaction.Barcode = x.Barcode;
						transaction.Quantity = x.ProQty;
						transaction.Unit = db.Units(new { x.UnitId }).FirstOrDefault().UnitName;
						transaction.UnitId = x.UnitId;
						transaction.Rate = x.ProRate;
						transaction.GrossAmount = x.GrossAmnt;
						transaction.Discount = x.ProDiscont;
						transaction.DiscountAmount = x.DiscAmnt;
						transaction.Vat = x.GST;
						transaction.VatAmount = x.GSTAMOUNT;
						transaction.Gst = transaction.Vat;
						transaction.GstAmount = transaction.VatAmount;
						transaction.Hsn = x.HSN;
						transaction.Sgst = x.SGST;
						transaction.SGstAmount = x.SGSTAmount;
						transaction.CGst = x.CGST;
						transaction.CGstAmount = x.CGSTAmount;
						transaction.IGst = x.IGST;
						transaction.IGstAmount = x.IGSTAMOUNT;
						transaction.NETAmount = x.NetAmnt;
						transaction.Taxable = transaction.NETAmount - transaction.GstAmount;
						transaction.ItemImage = x.ProductId.ToString();
						transaction.ItemQRCode = transaction.Barcode + "#" + transaction.Quantity.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Rate.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.GrossAmount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Discount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.DiscountAmount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Taxable.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Gst.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.GstAmount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.NETAmount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Unit;
						transactions.Add(transaction);
						iRowIndex++;
					}
				});
				return transactions;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public static List<VchrTransaction> GetVTransactionsFromVDetails(List<VchrTranLog> entVTranLog, DBContext db)
		{
			try
			{
				int num = 1;
				List<VchrTransaction> list = new List<VchrTransaction>();
				foreach (VchrTranLog item in entVTranLog)
				{
					VchrTransaction vchrTransaction = new VchrTransaction();
					vchrTransaction.VchrTranId = item.VchrTranId.ToInt32();
					vchrTransaction.SL = num;
					vchrTransaction.VchrNo = item.VchrNo.ToInt32();
					vchrTransaction.VchrDate = item.VchrDate;
					vchrTransaction.VchrAmount = item.VchrAmount;
					AccountEntity accountEntity = db.Accounts(new
					{
						AccountId = item.FromAccountId
					}).FirstOrDefault();
					vchrTransaction.FromAccount = ((accountEntity == null) ? "" : accountEntity.AccountName);
					AccountEntity accountEntity2 = db.Accounts(new
					{
						AccountId = item.FromAccountId
					}).FirstOrDefault();
					vchrTransaction.ToAccount = ((accountEntity2 == null) ? "" : accountEntity2.AccountName);
					vchrTransaction.Narration = item.Narration;
					vchrTransaction.SalesMan = db.EmployeeDetails(new
					{
						EmpID = item.SalesManId
					}).FirstOrDefault().EmpName;
					vchrTransaction.FK_BranchId = item.FK_BranchId;
					list.Add(vchrTransaction);
					num++;
				}
				return list;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public static List<Transaction> GetTransactionsFromPDetails(List<PurchaseInvoiceDtl> purDetails, DBContext db)
		{
			try
			{
				int num = 1;
				List<Transaction> list = new List<Transaction>();
				foreach (PurchaseInvoiceDtl purDetail in purDetails)
				{
					ProductEntity productEntity = new ProductEntity();
					productEntity = db.Products(new
					{
						ProductId = purDetail.ProId
					}).FirstOrDefault();
					if (productEntity == null)
					{
						continue;
					}
					int? num2 = purDetail.UnitId;
					if (num2.HasValue)
					{
						int? num3 = num2;
						if (!((num3.GetValueOrDefault() == 0) & num3.HasValue))
						{
							goto IL_00a6;
						}
					}
					ProductUnit productUnit = db.Productunits(new { purDetail.Barcode }).FirstOrDefault();
					num2 = ((productUnit == null) ? productEntity.UnitId : productUnit.UnitId);
					goto IL_00a6;
					IL_00a6:
					Transaction transaction = new Transaction();
					transaction.SL = num;
					transaction.ProductId = purDetail.ProId;
					transaction.ProductCode = productEntity.ProductCode.toInt32();
					ProductUnit productUnit2 = db.Productunits(new
					{
						ProductId = purDetail.ProId,
						UnitId = num2
					}).FirstOrDefault();
					transaction.ProductName = ((productUnit2 == null) ? productEntity.ProductName : productUnit2.ProductUnitName);
					transaction.Barcode = purDetail.Barcode;
					transaction.Quantity = purDetail.ProQty;
					transaction.UnitId = num2;
					transaction.Unit = db.Units(new
					{
						UnitId = num2
					}).FirstOrDefault().UnitName;
					transaction.Rate = purDetail.ProRate;
					transaction.GrossAmount = purDetail.Gross;
					transaction.Discount = purDetail.Disc;
					transaction.DiscountAmount = purDetail.DiscAmnt;
					transaction.Taxable = transaction.GrossAmount - transaction.DiscountAmount;
					transaction.Vat = purDetail.GST;
					transaction.VatAmount = purDetail.GSTAMOUNT;
					transaction.Gst = transaction.Vat;
					transaction.GstAmount = transaction.VatAmount;
					transaction.Hsn = purDetail.HSN;
					transaction.Sgst = purDetail.SGST;
					transaction.SGstAmount = purDetail.SGSTAmount;
					transaction.CGst = purDetail.CGST;
					transaction.CGstAmount = purDetail.CGSTAmount;
					transaction.IGst = purDetail.IGST;
					transaction.IGstAmount = purDetail.IGSTAMOUNT;
					transaction.NETAmount = purDetail.NetAmnt;
					list.Add(transaction);
					num++;
				}
				return list;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public static List<Transaction> GetTransactionsFromSRDetails(List<SalesRtnDetails> salesRtnDetails, DBContext db)
		{
			try
			{
				int iRowIndex = 1;
				List<Transaction> transactions = new List<Transaction>();
				salesRtnDetails.ForEach(delegate(SalesRtnDetails x)
				{
					ProductEntity productEntity = new ProductEntity();
					productEntity = db.Products(new { x.ProductId }).FirstOrDefault();
					if (productEntity != null)
					{
						Transaction transaction = new Transaction();
						transaction.SL = iRowIndex;
						transaction.ProductId = x.ProductId;
						transaction.ProductCode = productEntity.ProductCode.ToInt32();
						transaction.ProductName = db.Productunits(new { x.ProductId, x.UnitId }).FirstOrDefault().ProductUnitName;
						transaction.Barcode = x.Barcode;
						transaction.Quantity = x.ProQty;
						transaction.Unit = db.Units(new { x.UnitId }).FirstOrDefault().UnitName;
						transaction.Rate = x.ProRate;
						transaction.GrossAmount = x.GrossAmnt;
						transaction.Discount = x.ProDis;
						transaction.DiscountAmount = x.DiscAmnt;
						transaction.Taxable = transaction.GrossAmount - transaction.DiscountAmount;
						transaction.Vat = x.GST;
						transaction.VatAmount = x.GSTAMOUNT;
						transaction.Gst = transaction.Vat;
						transaction.GstAmount = transaction.VatAmount;
						transaction.Hsn = x.HSN;
						transaction.Sgst = x.SGST;
						transaction.SGstAmount = x.SGSTAmount;
						transaction.CGst = x.CGST;
						transaction.CGstAmount = x.CGSTAmount;
						transaction.IGst = x.IGST;
						transaction.IGstAmount = x.IGSTAMOUNT;
						transaction.NETAmount = x.NetAmnt;
						transactions.Add(transaction);
						iRowIndex++;
					}
				});
				return transactions;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public static List<Transaction> GetTransactionsFromPRDetails(List<PurRtnInvSttings> purRtnDetails, DBContext db)
		{
			try
			{
				int iRowIndex = 1;
				List<Transaction> transactions = new List<Transaction>();
				purRtnDetails.ForEach(delegate(PurRtnInvSttings x)
				{
					ProductEntity productEntity = new ProductEntity();
					productEntity = db.Products(new
					{
						ProductId = x.ProId
					}).FirstOrDefault();
					if (productEntity != null)
					{
						Transaction transaction = new Transaction();
						transaction.SL = iRowIndex;
						transaction.ProductId = x.ProId;
						transaction.ProductCode = productEntity.ProductCode.ToInt32();
						transaction.ProductName = db.Productunits(new
						{
							ProductId = x.ProId,
							UnitId = x.UnitId
						}).FirstOrDefault().ProductUnitName;
						transaction.Barcode = x.Barcode;
						transaction.Quantity = x.ProQty;
						transaction.Unit = db.Units(new { x.UnitId }).FirstOrDefault().UnitName;
						transaction.Rate = x.ProRate;
						transaction.GrossAmount = x.Gross;
						transaction.Discount = x.Disc;
						transaction.DiscountAmount = x.DiscAmnt;
						transaction.Taxable = transaction.GrossAmount - transaction.DiscountAmount;
						transaction.Vat = x.GST;
						transaction.VatAmount = x.GSTAMOUNT;
						transaction.Gst = transaction.Vat;
						transaction.GstAmount = transaction.VatAmount;
						transaction.Hsn = x.HSN;
						transaction.Sgst = x.SGST;
						transaction.SGstAmount = x.SGSTAmount;
						transaction.CGst = x.CGST;
						transaction.CGstAmount = x.CGSTAmount;
						transaction.IGst = x.IGST;
						transaction.IGstAmount = x.IGSTAMOUNT;
						transaction.NETAmount = x.NetAmnt;
						transactions.Add(transaction);
						iRowIndex++;
					}
				});
				return transactions;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public static List<Transaction> GetTransactionsFromSODetails(List<SalesOrderSettings> soRtnDetails, DBContext db)
		{
			try
			{
				int iRowIndex = 1;
				List<Transaction> transactions = new List<Transaction>();
				soRtnDetails.ForEach(delegate(SalesOrderSettings x)
				{
					ProductEntity productEntity = new ProductEntity();
					productEntity = db.Products(new { x.ProductId }).FirstOrDefault();
					if (productEntity != null)
					{
						Transaction item = new Transaction
						{
							SL = iRowIndex,
							ProductId = x.ProductId,
							ProductCode = productEntity.ProductCode.ToInt32(),
							ProductName = db.Productunits(new { x.ProductId, x.UnitId }).FirstOrDefault().ProductUnitName,
							Barcode = productEntity.Barcode,
							Quantity = x.ProQty,
							Unit = db.Units(new { x.UnitId }).FirstOrDefault().UnitName,
							Rate = x.ProRate,
							GrossAmount = x.GrossAmnt,
							Discount = x.ProDiscont,
							DiscountAmount = x.DiscAmnt,
							Taxable = x.Taxable,
							Vat = x.Tax,
							VatAmount = x.TaxAmount,
							Gst = x.Tax,
							GstAmount = x.TaxAmount,
							NETAmount = x.NetAmnt
						};
						transactions.Add(item);
						iRowIndex++;
					}
				});
				return transactions;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public static void GetPurchaseTransaction(Transaction transaction, int iRowIndex, string sType)
		{
			try
			{
				DBContext dBContext = new DBContext();
				ProductEntity productEntity = new ProductEntity();
				productEntity = dBContext.Products(new { transaction.ProductId }).FirstOrDefault();
				transaction.SL = iRowIndex + 1;
				transaction.ProductCode = productEntity.ProductCode.ToInt32();
				transaction.ProductName = transaction.ProductName ?? productEntity.ProductName;
				transaction.LocalName = transaction.LocalName ?? productEntity.LocalName;
				transaction.Hsn = productEntity.HSNNo;
				transaction.Quantity = 1m;
				transaction.GrossAmount = transaction.Rate * transaction.Quantity;
				if (transaction.Discount > 0m)
				{
					transaction.DiscountAmount = transaction.GrossAmount * (transaction.Discount / 100m);
				}
				else
				{
					transaction.DiscountAmount = 0m;
				}
				if (CommonSettings.blnEnableBarbarshopMode)
				{
					decimal? salesmanCommision = productEntity.SalesmanCommision;
					if ((salesmanCommision.GetValueOrDefault() > default(decimal)) & salesmanCommision.HasValue)
					{
						//EmployeeSelectionView employeeSelectionView = new EmployeeSelectionView();
						//if (employeeSelectionView.ShowDialog() == DialogResult.OK)
						//{
						//	transaction.FK_EmployeeId = employeeSelectionView.cmbEmployee.SelectedValue.ToInt32();
						//	decimal num = transaction.GrossAmount - transaction.DiscountAmount;
						//	transaction.EmployeeCommision = num * (productEntity.SalesmanCommision.ToDecimal() / 100m);
						//}
					}
				}
				if (CommonSettings.blnTaxEnabled)
				{
					transaction.Taxable = transaction.GrossAmount - transaction.DiscountAmount;
					transaction.Vat = AshproCommon.ValueFindMethod("Select SalesVat From TaxCategory Where TCategoryId= '" + productEntity.TCategoryId + "'");
					AniHelper.GetInclusiveBaseAmount(transaction.Taxable, transaction.Vat);
					if (transaction.Vat > 0m)
					{
						transaction.VatAmount = transaction.Taxable * (transaction.Vat / 100m);
					}
					else
					{
						transaction.VatAmount = 0m;
					}
					if (CommonSettings.blnGSTEnabled)
					{
						transaction.Gst = transaction.Vat;
						transaction.GstAmount = transaction.VatAmount;
						transaction.Hsn = productEntity.HSNNo;
						if (!(sType == "Local") && !(sType == "Wholesale"))
						{
							transaction.IGst = AshproCommon.ValueFindMethod("Select SalesXDuty From TaxCategory Where TCategoryId= '" + productEntity.TCategoryId + "'");
							transaction.Sgst = 0m;
							transaction.CGst = 0m;
						}
						else
						{
							transaction.Sgst = AshproCommon.ValueFindMethod("Select SaleCst From TaxCategory Where TCategoryId= '" + productEntity.TCategoryId + "'");
							transaction.CGst = AshproCommon.ValueFindMethod("Select SaleCess From TaxCategory Where TCategoryId= '" + productEntity.TCategoryId + "'");
							transaction.IGst = 0m;
						}
						if (transaction.Sgst > 0m)
						{
							transaction.SGstAmount = transaction.Taxable * (transaction.Sgst / 100m);
						}
						else
						{
							transaction.SGstAmount = 0m;
						}
						if (transaction.CGst > 0m)
						{
							transaction.CGstAmount = transaction.Taxable * (transaction.CGst / 100m);
						}
						else
						{
							transaction.CGstAmount = 0m;
						}
						if (transaction.IGst > 0m)
						{
							transaction.IGstAmount = transaction.Taxable * (transaction.IGst / 100m);
						}
						else
						{
							transaction.IGstAmount = 0m;
						}
					}
					transaction.NETAmount = transaction.Taxable + transaction.VatAmount;
				}
				else
				{
					transaction.NETAmount = transaction.GrossAmount - transaction.DiscountAmount;
				}
				transaction.ItemImage = transaction.ProductId.ToString();
				transaction.ItemQRCode = transaction.Barcode + "#" + transaction.Quantity.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Rate.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.GrossAmount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Discount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.DiscountAmount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Taxable.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Gst.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.GstAmount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.NETAmount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Unit;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public static void GetTransactions(Transaction transaction, int iRowIndex, string sType, bool isEstimate = false)
		{
			try
			{
				DBContext dBContext = new DBContext();
				ProductEntity productEntity = new ProductEntity();
				productEntity = dBContext.Products(new { transaction.ProductId }).FirstOrDefault();
				transaction.SL = iRowIndex + 1;
				transaction.ProductCode = productEntity.ProductCode.ToInt32();
				transaction.ProductName = transaction.ProductName ?? productEntity.ProductName;
				transaction.LocalName = transaction.LocalName ?? productEntity.LocalName;
				transaction.Hsn = productEntity.HSNNo;
				transaction.Quantity = 1m;
				transaction.GrossAmount = transaction.Rate * transaction.Quantity;
				if (transaction.Discount > 0m)
				{
					transaction.DiscountAmount = transaction.GrossAmount * (transaction.Discount / 100m);
				}
				else
				{
					transaction.DiscountAmount = 0m;
				}
				if (CommonSettings.blnEnableBarbarshopMode)
				{
					decimal? salesmanCommision = productEntity.SalesmanCommision;
					if ((salesmanCommision.GetValueOrDefault() > default(decimal)) & salesmanCommision.HasValue)
					{
						//EmployeeSelectionView employeeSelectionView = new EmployeeSelectionView();
						//if (employeeSelectionView.ShowDialog() == DialogResult.OK)
						//{
						//	transaction.FK_EmployeeId = employeeSelectionView.cmbEmployee.SelectedValue.ToInt32();
						//	decimal num = transaction.GrossAmount - transaction.DiscountAmount;
						//	transaction.EmployeeCommision = num * (productEntity.SalesmanCommision.ToDecimal() / 100m);
						//}
					}
				}
				if (productEntity.FieldOne.ToBool())
				{
					transaction.ItemSerialNumber = AniHelper.InputBoxMethod(Utility.uManufacturer, "Please enter the Serial Number", "Serial Number :");
				}
				if (!isEstimate && CommonSettings.blnTaxEnabled)
				{
					transaction.Taxable = transaction.GrossAmount - transaction.DiscountAmount;
					transaction.Vat = AshproCommon.ValueFindMethod("Select SalesVat From TaxCategory Where TCategoryId= '" + productEntity.TCategoryId + "'");
					decimal num2 = AniHelper.GetInclusiveBaseAmount(transaction.Taxable, transaction.Vat);
					if (transaction.Vat > 0m)
					{
						if (!productEntity.isInclusive.toBool())
						{
							transaction.VatAmount = transaction.Taxable * (transaction.Vat / 100m);
						}
						else
						{
							transaction.VatAmount = AniHelper.GetInclusiveAmount(transaction.Taxable, transaction.Vat);
						}
					}
					else
					{
						transaction.VatAmount = 0m;
					}
					if (CommonSettings.blnGSTEnabled)
					{
						transaction.Gst = transaction.Vat;
						transaction.GstAmount = transaction.VatAmount;
						transaction.Hsn = productEntity.HSNNo;
						if (!(sType == "Local") && !(sType == "Wholesale"))
						{
							transaction.IGst = AshproCommon.ValueFindMethod("Select SalesXDuty From TaxCategory Where TCategoryId= '" + productEntity.TCategoryId + "'");
							transaction.Sgst = 0m;
							transaction.CGst = 0m;
						}
						else
						{
							transaction.Sgst = AshproCommon.ValueFindMethod("Select SaleCst From TaxCategory Where TCategoryId= '" + productEntity.TCategoryId + "'");
							transaction.CGst = AshproCommon.ValueFindMethod("Select SaleCess From TaxCategory Where TCategoryId= '" + productEntity.TCategoryId + "'");
							transaction.IGst = 0m;
						}
						if (transaction.Sgst > 0m)
						{
							if (!productEntity.isInclusive.toBool())
							{
								transaction.SGstAmount = transaction.Taxable * (transaction.Sgst / 100m);
							}
							else
							{
								transaction.SGstAmount = num2 * (transaction.Sgst / 100m);
							}
						}
						else
						{
							transaction.SGstAmount = 0m;
						}
						if (transaction.CGst > 0m)
						{
							if (!productEntity.isInclusive.toBool())
							{
								transaction.CGstAmount = transaction.Taxable * (transaction.CGst / 100m);
							}
							else
							{
								transaction.CGstAmount = num2 * (transaction.CGst / 100m);
							}
						}
						else
						{
							transaction.CGstAmount = 0m;
						}
						if (transaction.IGst > 0m)
						{
							if (!productEntity.isInclusive.toBool())
							{
								transaction.IGstAmount = transaction.Taxable * (transaction.IGst / 100m);
							}
							else
							{
								transaction.IGstAmount = num2 * (transaction.IGst / 100m);
							}
						}
						else
						{
							transaction.IGstAmount = 0m;
						}
					}
					if (!productEntity.isInclusive.toBool())
					{
						transaction.NETAmount = transaction.Taxable + transaction.VatAmount;
					}
					else
					{
						transaction.NETAmount = transaction.Taxable;
						transaction.Taxable = transaction.NETAmount - transaction.VatAmount;
					}
				}
				else
				{
					transaction.NETAmount = transaction.GrossAmount - transaction.DiscountAmount;
				}
				transaction.ItemImage = transaction.ProductId.ToString();
				transaction.ItemQRCode = transaction.Barcode + "#" + transaction.Quantity.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Rate.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.GrossAmount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Discount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.DiscountAmount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Taxable.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Gst.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.GstAmount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.NETAmount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Unit;
			}
			catch (Exception)
			{
				throw;
			}
		}

		//public static void GetPurchaseTransactionsFromBarcode(Transaction transaction, ProductBarcodeEntity productBarcode, int sPCategoryID, int iRowIndex, string sType)
		//{
		//	try
		//	{
		//		DBContext dBContext = new DBContext();
		//		ProductEntity productEntity = new ProductEntity();
		//		productEntity = dBContext.Products(new
		//		{
		//			ProductId = productBarcode.FK_ProductId
		//		}).FirstOrDefault();
		//		transaction.SL = iRowIndex + 1;
		//		transaction.ProductId = productEntity.ProductId;
		//		transaction.ProductCode = productEntity.ProductCode.ToInt32();
		//		transaction.Barcode = productBarcode.Barcode;
		//		transaction.Hsn = productEntity.HSNNo;
		//		transaction.Quantity = 1m;
		//		transaction.UnitId = productBarcode.FK_UnitId;
		//		transaction.ProductName = ((productBarcode.ProductBarcodeName == string.Empty) ? dBContext.Productunits(new { transaction.ProductId, transaction.UnitId }).FirstOrDefault().ProductUnitName : productBarcode.ProductBarcodeName);
		//		transaction.Unit = AshproCommon.IdFindMethod("Select UnitName From Unit Where UnitId='" + productBarcode.FK_UnitId + "'");
		//		SellingPriceAndDiscFindMethod(sPCategoryID, "Purchase Invoice", productBarcode.Barcode);
		//		transaction.Rate = dmlRate;
		//		transaction.GrossAmount = transaction.Rate * transaction.Quantity;
		//		transaction.Discount = dmlDiscount;
		//		if (dmlDiscount > 0m)
		//		{
		//			transaction.DiscountAmount = transaction.GrossAmount * (dmlDiscount / 100m);
		//		}
		//		else
		//		{
		//			transaction.DiscountAmount = 0m;
		//		}
		//		if (CommonSettings.blnTaxEnabled)
		//		{
		//			transaction.Taxable = transaction.GrossAmount - transaction.DiscountAmount;
		//			transaction.Vat = AshproCommon.ValueFindMethod("Select SalesVat From TaxCategory Where TCategoryId= '" + productEntity.TCategoryId + "'");
		//			AniHelper.GetInclusiveBaseAmount(transaction.Taxable, transaction.Vat);
		//			if (transaction.Vat > 0m)
		//			{
		//				transaction.VatAmount = transaction.Taxable * (transaction.Vat / 100m);
		//			}
		//			else
		//			{
		//				transaction.VatAmount = 0m;
		//			}
		//			if (CommonSettings.blnGSTEnabled)
		//			{
		//				transaction.Gst = transaction.Vat;
		//				transaction.GstAmount = transaction.VatAmount;
		//				transaction.Hsn = productEntity.HSNNo;
		//				if (!(sType == "Local") && !(sType == "Wholesale"))
		//				{
		//					transaction.IGst = AshproCommon.ValueFindMethod("Select SalesXDuty From TaxCategory Where TCategoryId= '" + productEntity.TCategoryId + "'");
		//					transaction.Sgst = 0m;
		//					transaction.CGst = 0m;
		//				}
		//				else
		//				{
		//					transaction.Sgst = AshproCommon.ValueFindMethod("Select SaleCst From TaxCategory Where TCategoryId= '" + productEntity.TCategoryId + "'");
		//					transaction.CGst = AshproCommon.ValueFindMethod("Select SaleCess From TaxCategory Where TCategoryId= '" + productEntity.TCategoryId + "'");
		//					transaction.IGst = 0m;
		//				}
		//				if (transaction.Sgst > 0m)
		//				{
		//					transaction.SGstAmount = transaction.Taxable * (transaction.Sgst / 100m);
		//				}
		//				else
		//				{
		//					transaction.SGstAmount = 0m;
		//				}
		//				if (transaction.CGst > 0m)
		//				{
		//					transaction.CGstAmount = transaction.Taxable * (transaction.CGst / 100m);
		//				}
		//				else
		//				{
		//					transaction.CGstAmount = 0m;
		//				}
		//				if (transaction.IGst > 0m)
		//				{
		//					transaction.IGstAmount = transaction.Taxable * (transaction.IGst / 100m);
		//				}
		//				else
		//				{
		//					transaction.IGstAmount = 0m;
		//				}
		//			}
		//			transaction.NETAmount = transaction.Taxable + transaction.VatAmount;
		//		}
		//		else
		//		{
		//			transaction.NETAmount = transaction.GrossAmount - transaction.DiscountAmount;
		//		}
		//		transaction.ItemImage = transaction.ProductId.ToString();
		//		transaction.ItemQRCode = transaction.Barcode + "#" + transaction.Quantity.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Rate.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.GrossAmount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Discount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.DiscountAmount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Taxable.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Gst.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.GstAmount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.NETAmount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Unit;
		//	}
		//	catch (Exception ex)
		//	{
		//		throw ex;
		//	}
		//}

		//public static void GetTransactionsFromBarcode(Transaction transaction, ProductBarcodeEntity productBarcode, int sPCategoryID, int iRowIndex, string sType, string fType = "Sales Invoice", bool isEstimate = false)
		//{
		//	try
		//	{
		//		DBContext dBContext = new DBContext();
		//		ProductEntity productEntity = new ProductEntity();
		//		productEntity = dBContext.Products(new
		//		{
		//			ProductId = productBarcode.FK_ProductId
		//		}).FirstOrDefault();
		//		transaction.SL = iRowIndex + 1;
		//		transaction.ProductId = productEntity.ProductId;
		//		transaction.ProductCode = productEntity.ProductCode.ToInt32();
		//		transaction.Barcode = productBarcode.Barcode;
		//		transaction.Hsn = productEntity.HSNNo;
		//		transaction.Quantity = 1m;
		//		transaction.UnitId = productBarcode.FK_UnitId;
		//		transaction.ProductName = ((productBarcode.ProductBarcodeName == string.Empty) ? dBContext.Productunits(new { transaction.ProductId, transaction.UnitId }).FirstOrDefault().ProductUnitName : productBarcode.ProductBarcodeName);
		//		transaction.Unit = AshproCommon.IdFindMethod("Select UnitName From Unit Where UnitId='" + productBarcode.FK_UnitId + "'");
		//		SellingPriceAndDiscFindMethod(sPCategoryID, fType, productBarcode.Barcode);
		//		transaction.Rate = dmlRate;
		//		transaction.GrossAmount = transaction.Rate * transaction.Quantity;
		//		transaction.Discount = dmlDiscount;
		//		if (dmlDiscount > 0m)
		//		{
		//			transaction.DiscountAmount = transaction.GrossAmount * (dmlDiscount / 100m);
		//		}
		//		else
		//		{
		//			transaction.DiscountAmount = 0m;
		//		}
		//		if (CommonSettings.blnEnableBarbarshopMode)
		//		{
		//			decimal? salesmanCommision = productEntity.SalesmanCommision;
		//			if (((salesmanCommision.GetValueOrDefault() > default(decimal)) & salesmanCommision.HasValue) && fType == "Sales Invoice")
		//			{
		//				//EmployeeSelectionView employeeSelectionView = new EmployeeSelectionView();
		//				//if (employeeSelectionView.ShowDialog() == DialogResult.OK)
		//				//{
		//				//	transaction.FK_EmployeeId = employeeSelectionView.cmbEmployee.SelectedValue.ToInt32();
		//				//	decimal num = transaction.GrossAmount - transaction.DiscountAmount;
		//				//	transaction.EmployeeCommision = num * (productEntity.SalesmanCommision.ToDecimal() / 100m);
		//				//}
		//			}
		//		}
		//		if (!isEstimate && CommonSettings.blnTaxEnabled)
		//		{
		//			transaction.Taxable = transaction.GrossAmount - transaction.DiscountAmount;
		//			transaction.Vat = AshproCommon.ValueFindMethod("Select SalesVat From TaxCategory Where TCategoryId= '" + productEntity.TCategoryId + "'");
		//			decimal num2 = AniHelper.GetInclusiveBaseAmount(transaction.Taxable, transaction.Vat);
		//			if (transaction.Vat > 0m)
		//			{
		//				if (!productEntity.isInclusive.toBool())
		//				{
		//					transaction.VatAmount = transaction.Taxable * (transaction.Vat / 100m);
		//				}
		//				else
		//				{
		//					transaction.VatAmount = AniHelper.GetInclusiveAmount(transaction.Taxable, transaction.Vat);
		//				}
		//			}
		//			else
		//			{
		//				transaction.VatAmount = 0m;
		//			}
		//			if (CommonSettings.blnGSTEnabled)
		//			{
		//				transaction.Gst = transaction.Vat;
		//				transaction.GstAmount = transaction.VatAmount;
		//				transaction.Hsn = productEntity.HSNNo;
		//				if (!(sType == "Local") && !(sType == "Wholesale"))
		//				{
		//					transaction.IGst = AshproCommon.ValueFindMethod("Select SalesXDuty From TaxCategory Where TCategoryId= '" + productEntity.TCategoryId + "'");
		//					transaction.Sgst = 0m;
		//					transaction.CGst = 0m;
		//				}
		//				else
		//				{
		//					transaction.Sgst = AshproCommon.ValueFindMethod("Select SaleCst From TaxCategory Where TCategoryId= '" + productEntity.TCategoryId + "'");
		//					transaction.CGst = AshproCommon.ValueFindMethod("Select SaleCess From TaxCategory Where TCategoryId= '" + productEntity.TCategoryId + "'");
		//					transaction.IGst = 0m;
		//				}
		//				if (transaction.Sgst > 0m)
		//				{
		//					if (!productEntity.isInclusive.toBool())
		//					{
		//						transaction.SGstAmount = transaction.Taxable * (transaction.Sgst / 100m);
		//					}
		//					else
		//					{
		//						transaction.SGstAmount = num2 * (transaction.Sgst / 100m);
		//					}
		//				}
		//				else
		//				{
		//					transaction.SGstAmount = 0m;
		//				}
		//				if (transaction.CGst > 0m)
		//				{
		//					if (!productEntity.isInclusive.toBool())
		//					{
		//						transaction.CGstAmount = transaction.Taxable * (transaction.CGst / 100m);
		//					}
		//					else
		//					{
		//						transaction.CGstAmount = num2 * (transaction.CGst / 100m);
		//					}
		//				}
		//				else
		//				{
		//					transaction.CGstAmount = 0m;
		//				}
		//				if (transaction.IGst > 0m)
		//				{
		//					if (!productEntity.isInclusive.toBool())
		//					{
		//						transaction.IGstAmount = transaction.Taxable * (transaction.IGst / 100m);
		//					}
		//					else
		//					{
		//						transaction.IGstAmount = num2 * (transaction.IGst / 100m);
		//					}
		//				}
		//				else
		//				{
		//					transaction.IGstAmount = 0m;
		//				}
		//			}
		//			if (!productEntity.isInclusive.toBool())
		//			{
		//				transaction.NETAmount = transaction.Taxable + transaction.VatAmount;
		//			}
		//			else
		//			{
		//				transaction.NETAmount = transaction.Taxable;
		//				transaction.Taxable = transaction.NETAmount - transaction.VatAmount;
		//			}
		//		}
		//		else
		//		{
		//			transaction.NETAmount = transaction.GrossAmount - transaction.DiscountAmount;
		//		}
		//		transaction.ItemImage = transaction.ProductId.ToString();
		//		transaction.ItemQRCode = transaction.Barcode + "#" + transaction.Quantity.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Rate.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.GrossAmount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Discount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.DiscountAmount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Taxable.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Gst.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.GstAmount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.NETAmount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Unit;
		//	}
		//	catch (Exception ex)
		//	{
		//		throw ex;
		//	}
		//}

		//public static void GetTransactionFromItemQR(Transaction t, int iRowIndex, string sType)
		//{
		//	try
		//	{
		//		DBContext dBContext = new DBContext();
		//		ProductBarcodeEntity productBarcodeEntity = new ProductBarcodeEntity();
		//		string[] array = t.Barcode.Split('#');
		//		t.Barcode = array[0];
		//		t.Quantity = array[1].ToDecimal();
		//		t.Rate = array[2].ToDecimal();
		//		t.GrossAmount = array[3].ToDecimal();
		//		t.Discount = array[4].ToDecimal();
		//		t.DiscountAmount = array[5].ToDecimal();
		//		t.Taxable = array[6].ToDecimal();
		//		t.Gst = array[7].ToDecimal();
		//		t.GstAmount = array[8].ToDecimal();
		//		t.NETAmount = array[9].ToDecimal();
		//		t.Unit = array[10];
		//		productBarcodeEntity = dBContext.ProductBarcodes(new { t.Barcode }).FirstOrDefault();
		//		ProductEntity productEntity = new ProductEntity();
		//		productEntity = dBContext.Products(new
		//		{
		//			ProductId = productBarcodeEntity.FK_ProductId
		//		}).FirstOrDefault();
		//		t.SL = iRowIndex + 1;
		//		t.ProductId = productEntity.ProductId;
		//		t.ProductCode = productEntity.ProductCode.ToInt32();
		//		t.ProductName = productEntity.ProductName;
		//		t.Hsn = productEntity.HSNNo;
		//		t.UnitId = productBarcodeEntity.FK_UnitId;
		//		t.UnitId = AshproCommon.IdFindMethod("Select UnitId  From Unit Where UnitName ='N" + t.Unit + "'");
		//		t.Vat = t.Gst;
		//		t.VatAmount = t.GstAmount;
		//		if (!CommonSettings.blnGSTEnabled)
		//		{
		//			return;
		//		}
		//		decimal num = AniHelper.GetInclusiveBaseAmount(t.Taxable, t.Vat);
		//		t.Hsn = productEntity.HSNNo;
		//		if (!(sType == "Local") && !(sType == "Wholesale"))
		//		{
		//			t.IGst = AshproCommon.ValueFindMethod("Select SalesXDuty From TaxCategory Where TCategoryId= '" + productEntity.TCategoryId + "'");
		//			t.Sgst = 0m;
		//			t.CGst = 0m;
		//		}
		//		else
		//		{
		//			t.Sgst = AshproCommon.ValueFindMethod("Select SaleCst From TaxCategory Where TCategoryId= '" + productEntity.TCategoryId + "'");
		//			t.CGst = AshproCommon.ValueFindMethod("Select SaleCess From TaxCategory Where TCategoryId= '" + productEntity.TCategoryId + "'");
		//			t.IGst = 0m;
		//		}
		//		if (t.Sgst > 0m)
		//		{
		//			if (!productEntity.isInclusive.toBool())
		//			{
		//				t.SGstAmount = t.Taxable * (t.Sgst / 100m);
		//			}
		//			else
		//			{
		//				t.SGstAmount = num * (t.Sgst / 100m);
		//			}
		//		}
		//		else
		//		{
		//			t.SGstAmount = 0m;
		//		}
		//		if (t.CGst > 0m)
		//		{
		//			if (!productEntity.isInclusive.toBool())
		//			{
		//				t.CGstAmount = t.Taxable * (t.CGst / 100m);
		//			}
		//			else
		//			{
		//				t.CGstAmount = num * (t.CGst / 100m);
		//			}
		//		}
		//		else
		//		{
		//			t.CGstAmount = 0m;
		//		}
		//		if (t.IGst > 0m)
		//		{
		//			if (!productEntity.isInclusive.toBool())
		//			{
		//				t.IGstAmount = t.Taxable * (t.IGst / 100m);
		//			}
		//			else
		//			{
		//				t.IGstAmount = num * (t.IGst / 100m);
		//			}
		//		}
		//		else
		//		{
		//			t.IGstAmount = 0m;
		//		}
		//	}
		//	catch (Exception)
		//	{
		//		throw;
		//	}
		//}

		public static void GetPurchaseTransactionsAfterEdit(Transaction transaction, bool isEstimate = false, string sCon = null)
		{
			try
			{
				DBContext dBContext = new DBContext();
				new ProductEntity();
				dBContext.Products(new { transaction.ProductId }, null, sCon).FirstOrDefault();
				transaction.GrossAmount = transaction.Rate * transaction.Quantity;
				if (transaction.Discount > 0m)
				{
					transaction.DiscountAmount = transaction.GrossAmount * (transaction.Discount / 100m);
				}
				else
				{
					transaction.DiscountAmount = 0m;
				}
				if (!isEstimate)
				{
					transaction.Taxable = transaction.GrossAmount - transaction.DiscountAmount;
					AniHelper.GetInclusiveBaseAmount(transaction.Taxable, transaction.Vat);
					if (transaction.Vat > 0m)
					{
						transaction.VatAmount = transaction.Taxable * (transaction.Vat / 100m);
					}
					else
					{
						transaction.VatAmount = 0m;
					}
					transaction.Gst = transaction.Vat;
					transaction.GstAmount = transaction.VatAmount;
					if (transaction.Sgst > 0m)
					{
						transaction.SGstAmount = transaction.Taxable * (transaction.Sgst / 100m);
					}
					else
					{
						transaction.SGstAmount = 0m;
					}
					if (transaction.CGst > 0m)
					{
						transaction.CGstAmount = transaction.Taxable * (transaction.CGst / 100m);
					}
					else
					{
						transaction.CGstAmount = 0m;
					}
					if (transaction.IGst > 0m)
					{
						transaction.IGstAmount = transaction.Taxable * (transaction.IGst / 100m);
					}
					else
					{
						transaction.IGstAmount = 0m;
					}
					transaction.NETAmount = transaction.Taxable + transaction.VatAmount;
				}
				else
				{
					transaction.NETAmount = transaction.GrossAmount - transaction.DiscountAmount;
				}
				transaction.ItemImage = transaction.ProductId.ToString();
				transaction.ItemQRCode = transaction.Barcode + "#" + transaction.Quantity.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Rate.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.GrossAmount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Discount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.DiscountAmount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Taxable.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Gst.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.GstAmount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.NETAmount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Unit;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public static void GetTransactionsAfterEdit(Transaction transaction, bool isEstimate = false, string sCon = null)
		{
			try
			{
				DBContext dBContext = new DBContext();
				ProductEntity productEntity = new ProductEntity();
				productEntity = dBContext.Products(new { transaction.ProductId }, null, sCon).FirstOrDefault();
				transaction.GrossAmount = transaction.Rate * transaction.Quantity;
				if (transaction.Discount > 0m)
				{
					transaction.DiscountAmount = transaction.GrossAmount * (transaction.Discount / 100m);
				}
				else
				{
					transaction.DiscountAmount = 0m;
				}
				if (!isEstimate)
				{
					transaction.Taxable = transaction.GrossAmount - transaction.DiscountAmount;
					decimal num = AniHelper.GetInclusiveBaseAmount(transaction.Taxable, transaction.Vat);
					if (transaction.Vat > 0m)
					{
						if (!productEntity.isInclusive.toBool())
						{
							transaction.VatAmount = transaction.Taxable * (transaction.Vat / 100m);
						}
						else
						{
							transaction.VatAmount = AniHelper.GetInclusiveAmount(transaction.Taxable, transaction.Vat);
						}
					}
					else
					{
						transaction.VatAmount = 0m;
					}
					transaction.Gst = transaction.Vat;
					transaction.GstAmount = transaction.VatAmount;
					if (transaction.Sgst > 0m)
					{
						if (!productEntity.isInclusive.toBool())
						{
							transaction.SGstAmount = transaction.Taxable * (transaction.Sgst / 100m);
						}
						else
						{
							transaction.SGstAmount = num * (transaction.Sgst / 100m);
						}
					}
					else
					{
						transaction.SGstAmount = 0m;
					}
					if (transaction.CGst > 0m)
					{
						if (!productEntity.isInclusive.toBool())
						{
							transaction.CGstAmount = transaction.Taxable * (transaction.CGst / 100m);
						}
						else
						{
							transaction.CGstAmount = num * (transaction.CGst / 100m);
						}
					}
					else
					{
						transaction.CGstAmount = 0m;
					}
					if (transaction.IGst > 0m)
					{
						if (!productEntity.isInclusive.toBool())
						{
							transaction.IGstAmount = transaction.Taxable * (transaction.IGst / 100m);
						}
						else
						{
							transaction.IGstAmount = num * (transaction.IGst / 100m);
						}
					}
					else
					{
						transaction.IGstAmount = 0m;
					}
					if (!productEntity.isInclusive.toBool())
					{
						transaction.NETAmount = transaction.Taxable + transaction.VatAmount;
					}
					else
					{
						transaction.NETAmount = transaction.Taxable;
						transaction.Taxable = transaction.NETAmount - transaction.VatAmount;
					}
				}
				else
				{
					transaction.NETAmount = transaction.GrossAmount - transaction.DiscountAmount;
				}
				transaction.ItemImage = transaction.ProductId.ToString();
				transaction.ItemQRCode = transaction.Barcode + "#" + transaction.Quantity.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Rate.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.GrossAmount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Discount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.DiscountAmount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Taxable.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Gst.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.GstAmount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.NETAmount.ToString(CommonSettings.strCurencyFormat) + "#" + transaction.Unit;
			}
			catch (Exception)
			{
				throw;
			}
		}

		//public static int GetPOSDetails(ProductEntity productEntity, List<POSDetails> pDetails, int sPCategoryID, string fType = "Sales Invoice", decimal? Qty = null, string Barcode = null, int? iUnitId = null, decimal? dRate = null, string sProName = null)
		//{
		//	try
		//	{
		//		POSDetails pOSDetails = new POSDetails();
		//		string empty = string.Empty;
		//		Barcode = Barcode ?? productEntity.Barcode;
		//		if (productEntity.IsAllowMultiple.toBool())
		//		{
		//			pOSDetails.SL = pDetails.Count + 1;
		//			pOSDetails.productId = productEntity.ProductId.ToInt32();
		//			pOSDetails.ProductCode = productEntity.ProductCode.ToString();
		//			empty = sProName ?? productEntity.ProductName;
		//			pOSDetails.LocalName = productEntity.LocalName;
		//			pOSDetails.Barcode = Barcode;
		//			pOSDetails.FreeQty = 0m;
		//			pOSDetails.Qty = Qty ?? 1m;
		//			pOSDetails.UnitId = ((!iUnitId.HasValue) ? productEntity.UnitId.ToInt32() : iUnitId.ToInt32());
		//			pOSDetails.UnitName = AshproCommon.IdFindMethod("Select UnitName From Unit Where UnitId='" + pOSDetails.UnitId + "'");
		//			if (!dRate.HasValue)
		//			{
		//				SellingPriceAndDiscFindMethod(sPCategoryID, fType, Barcode);
		//				if (dmlRate == 0m)
		//				{
		//					dmlRate = productEntity.SellingPrice;
		//					dmlDiscount = productEntity.SaleDis;
		//				}
		//				if (dmlRate == 0m)
		//				{
		//					POSInputBox pOSInputBox = new POSInputBox();
		//					pOSInputBox.isNameVisible = true;
		//					if (pOSInputBox.ShowDialog() == DialogResult.OK)
		//					{
		//						dmlRate = pOSInputBox.txtInput.Value;
		//						if (pOSInputBox.txtName.Text != string.Empty)
		//						{
		//							empty = pOSInputBox.txtName.Text;
		//						}
		//						productEntity.isInclusive = pOSInputBox.cbInclusive.Checked;
		//					}
		//				}
		//			}
		//			else
		//			{
		//				dmlRate = dRate.ToDecimal();
		//			}
		//			if (productEntity.isRateBasedWeight.toBool() && !productEntity.IsScale.toBool())
		//			{
		//				POSInputBox pOSInputBox2 = new POSInputBox();
		//				if (pOSInputBox2.ShowDialog() == DialogResult.OK)
		//				{
		//					_ = pOSInputBox2.txtInput.Value;
		//					pOSDetails.Qty = ((pOSInputBox2.txtInput.Value == 0m) ? 0m : (pOSInputBox2.txtInput.Value / dmlRate));
		//				}
		//			}
		//			pOSDetails.ProductName = empty;
		//			pOSDetails.Rate = dmlRate;
		//			pOSDetails.Gross = pOSDetails.Rate * pOSDetails.Qty;
		//			pOSDetails.Discount = dmlDiscount;
		//			pOSDetails.DisAmount = pOSDetails.Rate * (pOSDetails.Discount / 100m);
		//			pOSDetails.Taxable = pOSDetails.Gross - pOSDetails.DisAmount;
		//			pOSDetails.Tax = db.TaxCategories(new { productEntity.TCategoryId }).FirstOrDefault().SalesVat;
		//			if (!productEntity.isInclusive.toBool())
		//			{
		//				pOSDetails.TaxAmount = pOSDetails.Taxable * (pOSDetails.Tax / 100m);
		//				pOSDetails.NetAmount = pOSDetails.Taxable + pOSDetails.TaxAmount;
		//			}
		//			else
		//			{
		//				pOSDetails.TaxAmount = AniHelper.GetInclusiveAmount(pOSDetails.Taxable, pOSDetails.Tax);
		//				pOSDetails.NetAmount = pOSDetails.Taxable;
		//				pOSDetails.Taxable = pOSDetails.NetAmount - pOSDetails.TaxAmount;
		//			}
		//			pDetails.Add(pOSDetails);
		//		}
		//		else if (pDetails.Where((POSDetails x) => x.Barcode == Barcode).Count() == 0)
		//		{
		//			pOSDetails.SL = pDetails.Count + 1;
		//			pOSDetails.productId = productEntity.ProductId.ToInt32();
		//			pOSDetails.ProductCode = productEntity.ProductCode.ToString();
		//			empty = sProName ?? productEntity.ProductName;
		//			pOSDetails.LocalName = productEntity.LocalName;
		//			pOSDetails.Barcode = Barcode;
		//			pOSDetails.FreeQty = 0m;
		//			pOSDetails.Qty = Qty ?? 1m;
		//			pOSDetails.UnitId = iUnitId ?? productEntity.UnitId.ToInt32();
		//			pOSDetails.UnitName = AshproCommon.IdFindMethod("Select UnitName From Unit Where UnitId='" + pOSDetails.UnitId + "'");
		//			if (!dRate.HasValue)
		//			{
		//				SellingPriceAndDiscFindMethod(sPCategoryID, fType, Barcode);
		//				if (dmlRate == 0m)
		//				{
		//					dmlRate = productEntity.SellingPrice;
		//					dmlDiscount = productEntity.SaleDis;
		//				}
		//				if (dmlRate == 0m)
		//				{
		//					POSInputBox pOSInputBox3 = new POSInputBox();
		//					pOSInputBox3.isNotInput = false;
		//					pOSInputBox3.isNameVisible = true;
		//					if (pOSInputBox3.ShowDialog() == DialogResult.OK)
		//					{
		//						dmlRate = pOSInputBox3.txtInput.Value;
		//						empty = pOSInputBox3.txtName.Text;
		//						productEntity.isInclusive = pOSInputBox3.cbInclusive.Checked;
		//					}
		//				}
		//			}
		//			else
		//			{
		//				dmlRate = dRate.ToDecimal();
		//			}
		//			if (productEntity.isRateBasedWeight.toBool() && !productEntity.IsScale.toBool())
		//			{
		//				POSInputBox pOSInputBox4 = new POSInputBox();
		//				if (pOSInputBox4.ShowDialog() == DialogResult.OK)
		//				{
		//					_ = pOSInputBox4.txtInput.Value;
		//					pOSDetails.Qty = ((pOSInputBox4.txtInput.Value == 0m) ? 0m : (pOSInputBox4.txtInput.Value / dmlRate));
		//				}
		//			}
		//			empty = (pOSDetails.ProductName = ((empty == string.Empty) ? productEntity.ProductName : empty));
		//			pOSDetails.Rate = dmlRate;
		//			pOSDetails.Gross = pOSDetails.Rate * pOSDetails.Qty;
		//			pOSDetails.Discount = dmlDiscount;
		//			pOSDetails.DisAmount = pOSDetails.Rate * (pOSDetails.Discount / 100m);
		//			pOSDetails.Taxable = pOSDetails.Gross - pOSDetails.DisAmount;
		//			pOSDetails.Tax = db.TaxCategories(new { productEntity.TCategoryId }).FirstOrDefault().SalesVat;
		//			if (!productEntity.isInclusive.toBool())
		//			{
		//				pOSDetails.TaxAmount = pOSDetails.Taxable * (pOSDetails.Tax / 100m);
		//				pOSDetails.NetAmount = pOSDetails.Taxable + pOSDetails.TaxAmount;
		//			}
		//			else
		//			{
		//				pOSDetails.TaxAmount = AniHelper.GetInclusiveAmount(pOSDetails.Taxable, pOSDetails.Tax);
		//				pOSDetails.NetAmount = pOSDetails.Taxable;
		//				pOSDetails.Taxable = pOSDetails.NetAmount - pOSDetails.TaxAmount;
		//			}
		//			pDetails.Add(pOSDetails);
		//		}
		//		else if (pDetails != null)
		//		{
		//			pOSDetails = pDetails.Where((POSDetails x) => x.Barcode == Barcode).FirstOrDefault();
		//			pOSDetails.Qty += 1m;
		//			pOSDetails.Gross = pOSDetails.Rate * pOSDetails.Qty;
		//			pOSDetails.DisAmount = pOSDetails.Rate * (pOSDetails.Discount / 100m);
		//			pOSDetails.Taxable = pOSDetails.Gross - pOSDetails.DisAmount;
		//			if (!productEntity.isInclusive.toBool())
		//			{
		//				pOSDetails.TaxAmount = pOSDetails.Taxable * (pOSDetails.Tax / 100m);
		//				pOSDetails.NetAmount = pOSDetails.Taxable + pOSDetails.TaxAmount;
		//			}
		//			else
		//			{
		//				pOSDetails.TaxAmount = AniHelper.GetInclusiveAmount(pOSDetails.Taxable, pOSDetails.Tax);
		//				pOSDetails.NetAmount = pOSDetails.Taxable;
		//				pOSDetails.Taxable = pOSDetails.NetAmount - pOSDetails.TaxAmount;
		//			}
		//		}
		//		if (CommonSettings.blnEnableBarbarshopMode)
		//		{
		//			decimal? salesmanCommision = productEntity.SalesmanCommision;
		//			if ((salesmanCommision.GetValueOrDefault() > default(decimal)) & salesmanCommision.HasValue)
		//			{
		//				EmployeeSelectionView employeeSelectionView = new EmployeeSelectionView();
		//				if (employeeSelectionView.ShowDialog() == DialogResult.OK)
		//				{
		//					pOSDetails.FK_EmployeeId = employeeSelectionView.cmbEmployee.SelectedValue.ToInt32();
		//					decimal taxable = pOSDetails.Taxable;
		//					pOSDetails.EmployeeCommision = taxable * (productEntity.SalesmanCommision.ToDecimal() / 100m);
		//				}
		//			}
		//		}
		//		if (productEntity.FieldOne.ToBool())
		//		{
		//			pOSDetails.ItemSerialNumber = AniHelper.InputBoxMethod(Utility.uManufacturer, "Please enter the Serial Number", "Serial Number :");
		//		}
		//		return pOSDetails.SL - 1;
		//	}
		//	catch (Exception)
		//	{
		//		throw;
		//	}
		//}

		public static decimal SellingPriceAndDiscFindMethod(int PCategoryId, string fType, string Barcode)
		{
			dmlRate = 0m;
			dmlDiscount = 0m;
			DataTable dataTable = new DataTable();
			if (fType == "Sales Invoice")
			{
				dataTable = AshproCommon.GetDataTable("SELECT SPrice,SPDisc FROM ProductPriceCategory where Barcode='" + Barcode + "' and PCategoryId='" + PCategoryId + "'");
			}
			else if (fType == "Purchase Invoice")
			{
				dataTable = AshproCommon.GetDataTable("SELECT PPrice,PDisc FROM ProductPriceCategory where Barcode='" + Barcode + "' and PCategoryId='" + PCategoryId + "'");
			}
			foreach (DataRow row in dataTable.Rows)
			{
				if (row[0] != DBNull.Value)
				{
					dmlRate = row[0].ToString().ToDecimal();
				}
				if (row[1] != DBNull.Value)
				{
					dmlDiscount = row[1].ToString().ToDecimal();
				}
			}
			return dmlRate;
		}

		public static List<SalesDetails> SalesDetailsMethod(List<POSDetails> entPOSDetails, int iParyId, string SType)
		{
			DBContext dBContext = new DBContext();
			List<SalesDetails> list = new List<SalesDetails>();
			foreach (POSDetails entPOSDetail in entPOSDetails)
			{
				ProductEntity productEntity = dBContext.Products(new
				{
					ProductId = entPOSDetail.productId
				}).FirstOrDefault();
				SalesDetails salesDetails = new SalesDetails();
				salesDetails.SalesInvId = 1;
				salesDetails.ProductId = entPOSDetail.productId;
				salesDetails.ProductName = entPOSDetail.ProductName;
				salesDetails.Barcode = entPOSDetail.Barcode;
				salesDetails.PartyAccountId = iParyId;
				salesDetails.ProQty = entPOSDetail.Qty;
				salesDetails.UnitId = entPOSDetail.UnitId;
				salesDetails.ProRate = entPOSDetail.Rate;
				salesDetails.ProDiscont = entPOSDetail.Discount;
				salesDetails.DiscAmnt = entPOSDetail.DisAmount;
				salesDetails.GrossAmnt = entPOSDetail.Gross;
				salesDetails.NetAmnt = entPOSDetail.NetAmount;
				salesDetails.Date = DateTime.Now.Date;
				salesDetails.HSN = productEntity.HSNNo;
				salesDetails.GST = entPOSDetail.Tax;
				salesDetails.GSTAMOUNT = entPOSDetail.TaxAmount;
				salesDetails.FK_BatchId = dBContext.Batches().FirstOrDefault().Id;
				if (CommonSettings.blnGSTEnabled)
				{
					if (SType.Contains("Interstate"))
					{
						salesDetails.IGST = salesDetails.GST;
						salesDetails.IGSTAMOUNT = salesDetails.GSTAMOUNT;
						salesDetails.SGST = 0m;
						salesDetails.SGSTAmount = 0m;
						salesDetails.CGST = 0m;
						salesDetails.CGSTAmount = 0m;
					}
					else
					{
						salesDetails.IGST = 0m;
						salesDetails.IGSTAMOUNT = 0m;
						salesDetails.SGST = salesDetails.GST / 2m;
						salesDetails.SGSTAmount = salesDetails.GSTAMOUNT / 2m;
						salesDetails.CGST = salesDetails.GST / 2m;
						salesDetails.CGSTAmount = salesDetails.GSTAMOUNT / 2m;
					}
				}
				else
				{
					salesDetails.IGST = 0m;
					salesDetails.IGSTAMOUNT = 0m;
					salesDetails.SGST = 0m;
					salesDetails.SGSTAmount = 0m;
					salesDetails.CGST = 0m;
					salesDetails.CGSTAmount = 0m;
				}
				salesDetails.ItemCalory = (decimal)productEntity.FieldTwo.ToInt32() * salesDetails.ProQty;
				salesDetails.EmployeeID = entPOSDetail.FK_EmployeeId;
				salesDetails.EmployeeCommision = entPOSDetail.EmployeeCommision;
				salesDetails.ItemSerialNumber = entPOSDetail.ItemSerialNumber;
				list.Add(salesDetails);
			}
			return list;
		}

		public static bool InvoiceHoldMethod(SalesMaster salesMaster, List<POSDetails> pOSDetails)
		{
			try
			{
				List<SalesDetails> items = SalesDetailsMethod(pOSDetails, salesMaster.PartyAccountId.ToInt32(), salesMaster.InvType);
				DataSet dataSet = new DataSet();
				DataTable dataTable = new DataTable();
				dataTable = AniHelper.ListToDataTable(items);
				dataTable.TableName = "type";
				dataSet.Tables.Add(dataTable);
				salesMaster.CntrInvNo += salesMaster.SalesInvNo;
				AshproCommon.InsertMethod_SP(salesMaster, dataSet, "spInsertPOSHold");
			}
			catch (Exception ex)
			{
				_ = ex.Message;
				return false;
			}
			return true;
		}

		public static bool TranLogMethod(decimal dmlDebit, decimal dmlCredit, int iOnAccountId, int iOpAccountId, int? iVoucherID, int? iVouchrNo, string sVoucherType, int? iUserId, string sStatus, string vName = null, string VchrDate = null, string Narration = null)
		{
			if (VchrDate == null)
			{
				VchrDate = AniHelper.GetDate(DateTime.Now);
			}
			TransactionLog transactionLog = new TransactionLog();
			transactionLog.TranId = null;
			transactionLog.VchrId = iVoucherID;
			transactionLog.VchrDate = VchrDate.toDateTime();
			transactionLog.VchrType = sVoucherType;
			transactionLog.VchrNo = iVouchrNo.ToInt32();
			transactionLog.AccountId = iOnAccountId;
			transactionLog.OppAccountId = iOpAccountId;
			transactionLog.DebitAmount = dmlDebit;
			transactionLog.CreditAmount = dmlCredit;
			transactionLog.Narration = Narration ?? sVoucherType;
			transactionLog.UserID = iUserId;
			transactionLog.TranDate = DateTime.Now.Date;
			transactionLog.Status = sStatus;
			transactionLog.RefNo = iVouchrNo.ToString();
			transactionLog.SequenceNo = Utility.uSerialNumber;
			transactionLog.Terminal = Utility.uTerminal;
			if (vName == null)
			{
				transactionLog.VchrName = sVoucherType;
			}
			else
			{
				transactionLog.VchrName = vName;
			}
			transactionLog.FK_BranchId = Settings.Default.iBranchId;
			try
			{
				AshproCommon.InsertToDatabaseObj(transactionLog, "TransactionLog");
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		public static void SaveSalesInvoice(SalesMaster salesMaster, List<SalesDetails> salesDetails, int IGodwonId, string strVoucherType, string sCon)
		{
			DataSet dataSet = new DataSet();
			DataTable dataTable = new DataTable();
			dataTable = AniHelper.ListToDataTable(salesDetails);
			dataTable.TableName = "type";
			dataSet.Tables.Add(dataTable);
			GodownEntity godownEntity = db.Godowns(new
			{
				GodwnId = IGodwonId
			}).FirstOrDefault();
			int? num = AshproCommon.InsertMethod_SP(salesMaster, dataSet, "spSalesInvoice", new
			{
				TransactionType = "insert",
				GodownId = IGodwonId,
				GodownName = godownEntity.GodownName,
				IsReturn = 0
			}, sCon);
			int iVchrNo = db.SalesMasters(new
			{
				SalesInvId = num
			}, sCon).FirstOrDefault().SalesInvNo.toInt32();
			List<POSDetails> pOSDetailsFromSDetails = GetPOSDetailsFromSDetails(salesDetails);
			SaveStockTranLoagMethod(pOSDetailsFromSDetails, num.ToInt32(), iVchrNo, strVoucherType, salesMaster.InvType, salesMaster.PartyAccountId.toInt32(), strVoucherType, salesMaster.JEId.toInt32(), IGodwonId, isIn: false, AniHelper.GetDate(salesMaster.SalesDate), sCon);
		}

		public static bool SaveStockTranLoagMethod(List<POSDetails> pOSDetails, int iVchrId, int iVchrNo, string sVchrType, string sVoucherFormType, int iPartyAcctId, string sNarration, int iUserId, int? _IGodwnId = null, bool isIn = false, string sVchrDate = null, string sCon = null)
		{
			try
			{
				foreach (POSDetails pOSDetail in pOSDetails)
				{
					decimal num = default(decimal);
					decimal num2 = default(decimal);
					ProductEntity productEntity = new ProductEntity();
					productEntity = AshproCommon.GetObjectDetails<ProductEntity>("Select * From Product Where ProductCode='" + pOSDetail.ProductCode + "'", sCon);
					if (!(productEntity.ProductType != "Service"))
					{
						continue;
					}
					int iGdwnId = ((!_IGodwnId.HasValue) ? productEntity.GodwnId.ToInt32() : _IGodwnId.ToInt32());
					decimal num3 = pOSDetail.Qty + pOSDetail.FreeQty;
					if (productEntity.ProductType == "Combained Product")
					{
						DataTable dataTable = new DataTable();
						dataTable = AshproCommon.GetDataTable("Select ProductId,ProQty,UnitId,Barcode FROM ProCombSettings where ProCombMasterId='" + AshproCommon.IdFindMethod("Select ProCombMasterId From ProCombMaster Where CombProName='" + pOSDetail.ProductName + "' ") + "'");
						foreach (DataRow row in dataTable.Rows)
						{
							string barcode = row[3].ToString();
							decimal num4 = row[1].ToString().ToDecimal();
							decimal num5 = num3 * num4;
							if (!isIn)
							{
								if (num5 < 0m)
								{
									num2 = default(decimal);
									num = Math.Abs(num5);
								}
								else
								{
									num = default(decimal);
									num2 = num5;
								}
								STranLogMethod(iVchrId, iVchrNo, sVchrType, sVoucherFormType, iPartyAcctId, sNarration, iUserId, num, num2, row[0].ToString().ToInt32(), row[2].ToString().ToInt32(), iGdwnId, sVchrDate, barcode);
							}
							else
							{
								STranLogMethod(iVchrId, iVchrNo, sVchrType, sVoucherFormType, iPartyAcctId, sNarration, iUserId, num3, 0m, row[0].ToString().ToInt32(), row[2].ToString().ToInt32(), iGdwnId, sVchrDate, barcode);
							}
						}
						continue;
					}
					string barcode2 = pOSDetail.Barcode;
					int num6 = productEntity.ProductId.ToInt32();
					int unitId = pOSDetail.UnitId;
					int num7 = productEntity.UnitId.ToInt32();
					ProductUnit productUnit = db.Productunits(new
					{
						ProductId = num6,
						UnitId = unitId
					}).FirstOrDefault();
					if (unitId != num7)
					{
						barcode2 = productEntity.Barcode;
					}
					decimal num8 = 1m;
					if (productUnit != null)
					{
						num8 = productUnit.UnitQty;
					}
					num3 *= num8;
					if (!isIn)
					{
						if (num3 < 0m)
						{
							num = Math.Abs(num3);
							num2 = default(decimal);
						}
						else
						{
							num = default(decimal);
							num2 = num3;
						}
						STranLogMethod(iVchrId, iVchrNo, sVchrType, sVoucherFormType, iPartyAcctId, sNarration, iUserId, num, num2, num6, num7, iGdwnId, sVchrDate, barcode2);
					}
					else
					{
						STranLogMethod(iVchrId, iVchrNo, sVchrType, sVoucherFormType, iPartyAcctId, sNarration, iUserId, num3, 0m, num6, num7, iGdwnId, sVchrDate, barcode2);
					}
				}
				return true;
			}
			catch (Exception ex)
			{
				_ = ex.Message;
				return false;
			}
		}

		public static void VoucherTranLog(int iVchrNo, DateTime tVchrDate, decimal dAmount, string sVchrType, string sNarration, int iFromAcctId, int iToAcctId, int iSalesMan, int iUserId, string sVchrName)
		{
			VchrTranLog vchrTranLog = new VchrTranLog();
			vchrTranLog.VchrTranId = null;
			vchrTranLog.VchrNo = iVchrNo;
			vchrTranLog.VchrDate = tVchrDate;
			vchrTranLog.VchrAmount = dAmount;
			vchrTranLog.VchrType = sVchrType;
			vchrTranLog.FromAccountId = iFromAcctId;
			vchrTranLog.ToAccountId = iToAcctId;
			vchrTranLog.Narration = sNarration;
			vchrTranLog.SalesManId = iSalesMan;
			vchrTranLog.UserId = iUserId;
			vchrTranLog.TranDate = DateTime.Now.Date;
			vchrTranLog.VchrName = sVchrName;
			vchrTranLog.FK_BranchId = Settings.Default.iBranchId;
			AshproCommon.InsertToDatabaseObj(vchrTranLog, "VchrTranLog");
		}

		public static void STranLogMethod(int iVchrId, int iVchrNo, string sVchrType, string sVoucherFormType, int iPartyAcctId, string sNarration, int iUserId, decimal dmlInQty, decimal dmlOutQty, int iProId, int iUnitid, int iGdwnId, string sVoucherDate = null, string Barcode = null)
		{
			if (sVoucherDate == null)
			{
				sVoucherDate = AniHelper.GetDate(DateTime.Now);
			}
			StockTranLog stockTranLog = new StockTranLog();
			stockTranLog.STranId = null;
			stockTranLog.ProductId = iProId;
			stockTranLog.VchrId = iVchrId;
			stockTranLog.VchrNo = iVchrNo;
			stockTranLog.VchrDate = sVoucherDate.toDateTime();
			stockTranLog.VchrType = sVchrType;
			stockTranLog.VchrFormType = sVoucherFormType;
			stockTranLog.AccountId = iPartyAcctId;
			stockTranLog.InQuantity = dmlInQty;
			stockTranLog.OutQuantity = dmlOutQty;
			stockTranLog.UnitId = iUnitid;
			stockTranLog.Narration = sNarration;
			stockTranLog.UserId = iUserId;
			stockTranLog.TranDate = DateTime.Now.Date;
			stockTranLog.FK_BranchId = Settings.Default.iBranchId;
			stockTranLog.Barcode = Barcode;
			if (AshproCommon.InsertToDatabaseObj(stockTranLog, "StockTranLog"))
			{
				GdwnStckTranLog gdwnStckTranLog = new GdwnStckTranLog();
				gdwnStckTranLog.GSTranId = null;
				gdwnStckTranLog.GodwnId = iGdwnId;
				gdwnStckTranLog.GodownName = AshproCommon.IdFindMethod("Select GodownName From  Godown Where GodwnId='" + iGdwnId + "' ");
				gdwnStckTranLog.ProductId = iProId;
				gdwnStckTranLog.ProductName = AshproCommon.IdFindMethod("Select ProductName From Product Where ProductId='" + iProId + "'");
				gdwnStckTranLog.VchrId = iVchrId;
				gdwnStckTranLog.VchrNo = iVchrNo;
				gdwnStckTranLog.VchrDate = sVoucherDate.toDateTime();
				gdwnStckTranLog.VchrType = sVoucherFormType;
				gdwnStckTranLog.InQuantity = dmlInQty;
				gdwnStckTranLog.OutQuantity = dmlOutQty;
				gdwnStckTranLog.UnitId = iUnitid;
				gdwnStckTranLog.Narration = sNarration;
				gdwnStckTranLog.UserId = iUserId;
				gdwnStckTranLog.TranDate = DateTime.Now.Date;
				gdwnStckTranLog.FK_BranchId = Settings.Default.iBranchId;
				gdwnStckTranLog.Barcode = Barcode;
				AshproCommon.InsertToDatabaseObj(gdwnStckTranLog, "GdwnStckTranLog");
			}
		}

		public static List<Transaction> GetTransactionFromSOrder(List<SalesOrderSettings> salesOrderSettings)
		{
			List<Transaction> list = new List<Transaction>();
			int num = 1;
			foreach (SalesOrderSettings salesOrderSetting in salesOrderSettings)
			{
				Transaction transaction = new Transaction();
				transaction.SL = num;
				transaction.ProductCode = AshproCommon.IdFindMethod("Select ProductCode From Product Where ProductId='" + salesOrderSetting.ProductId + "'", isVoucherNo: false);
				transaction.ProductName = db.Productunits(new { salesOrderSetting.ProductId, salesOrderSetting.UnitId }).FirstOrDefault().ProductUnitName;
				transaction.Unit = AshproCommon.IdFindMethod("Select UnitName From Unit Where UnitId='" + salesOrderSetting.UnitId + "'");
				transaction.Quantity = salesOrderSetting.ProQty;
				transaction.Rate = salesOrderSetting.ProRate;
				transaction.GrossAmount = salesOrderSetting.GrossAmnt;
				transaction.Discount = salesOrderSetting.ProDiscont;
				transaction.DiscountAmount = salesOrderSetting.DiscAmnt;
				transaction.Taxable = salesOrderSetting.NetAmnt;
				transaction.Hsn = AshproCommon.IdFindMethod("Select HSNNo From Product Where ProductId='" + salesOrderSetting.ProductId + "'");
				int num2 = AshproCommon.IdFindMethod("Select TCategoryId From Product Where ProductId='" + salesOrderSetting.ProductId + "'", isVoucherNo: false);
				transaction.Gst = AshproCommon.ValueFindMethod("Select SalesVat From TaxCategory Where TCategoryId='" + num2 + "'");
				transaction.GstAmount = transaction.Taxable * (transaction.Gst / 100m);
				transaction.Vat = transaction.Gst;
				transaction.VatAmount = transaction.GstAmount;
				transaction.Sgst = AshproCommon.ValueFindMethod("Select SaleCst From TaxCategory Where TCategoryId='" + num2 + "'");
				transaction.SGstAmount = transaction.GstAmount * (transaction.Sgst / 100m);
				transaction.CGst = AshproCommon.ValueFindMethod("Select SaleCess From TaxCategory Where TCategoryId='" + num2 + "'");
				transaction.CGstAmount = transaction.GstAmount * (transaction.CGst / 100m);
				transaction.IGst = AshproCommon.ValueFindMethod("Select SalesXDuty From TaxCategory Where TCategoryId='" + num2 + "'");
				transaction.IGstAmount = transaction.GstAmount * (transaction.IGst / 100m);
				transaction.NETAmount = transaction.Taxable + transaction.GstAmount;
				list.Add(transaction);
				num++;
			}
			return list;
		}

		public static List<POSDetails> GetPOSDetailsFrmTrans(List<Transaction> transactions)
		{
			List<POSDetails> entpOSDetails = new List<POSDetails>();
			transactions.ForEach(delegate(Transaction x)
			{
				POSDetails item = new POSDetails
				{
					SL = x.SL,
					Barcode = x.Barcode,
					ProductCode = x.ProductCode.ToString(),
					ProductName = x.ProductName,
					Qty = x.Quantity,
					FreeQty = 0m,
					UnitId = x.UnitId.toInt32(),
					UnitName = x.Unit,
					Rate = x.Rate,
					Gross = x.GrossAmount,
					Discount = x.Discount,
					DisAmount = x.DiscountAmount,
					Taxable = x.Taxable,
					Tax = x.Gst,
					TaxAmount = x.GstAmount,
					NetAmount = x.NETAmount
				};
				entpOSDetails.Add(item);
			});
			return entpOSDetails;
		}

		public static POSDetails POSDetailsFrmTrans(Transaction x)
		{
			POSDetails pOSDetails = new POSDetails();
			pOSDetails.SL = x.SL;
			pOSDetails.ProductCode = x.ProductCode.ToString();
			pOSDetails.ProductName = x.ProductName;
			pOSDetails.Qty = x.Quantity;
			pOSDetails.FreeQty = 0m;
			pOSDetails.UnitName = x.Unit;
			pOSDetails.Rate = x.Rate;
			pOSDetails.Gross = x.GrossAmount;
			pOSDetails.Discount = x.Discount;
			pOSDetails.DisAmount = x.DiscountAmount;
			pOSDetails.Taxable = x.Taxable;
			pOSDetails.Tax = x.Gst;
			pOSDetails.TaxAmount = x.GstAmount;
			pOSDetails.NetAmount = x.NETAmount;
			return pOSDetails;
		}

		public static List<BarcodeData> GetBarcodeDataFromTransaction(List<Transaction> transactions, DBContext db = null)
		{
			if (db == null)
			{
				db = new DBContext();
			}
			List<BarcodeData> list = new List<BarcodeData>();
			Company company = new Company();
			company = db.Companies().FirstOrDefault();
			foreach (Transaction transaction in transactions)
			{
				BarcodeData barcodeData = new BarcodeData();
				ProductEntity productEntity = new ProductEntity();
				productEntity = db.Products(new { transaction.ProductId }).FirstOrDefault();
				barcodeData.Sl_No = transaction.SL;
				barcodeData.Product_Code = transaction.ProductCode.ToString();
				barcodeData.Product_Name = transaction.ProductName;
				barcodeData.Unit = transaction.Unit;
				barcodeData.Brand = db.Brands(new { productEntity.BrandId }).FirstOrDefault().BrandName;
				barcodeData.Group_Name = db.ProductGroups(new { productEntity.PGroupId }).FirstOrDefault().PGroupName;
				barcodeData.Specification = productEntity.Specification;
				barcodeData.Description = productEntity.description;
				barcodeData.Purchase_Rate = transaction.Rate;
				barcodeData.Sales_Rate = productEntity.SellingPrice;
				barcodeData.MRP = productEntity.MRP;
				barcodeData.Barcode = transaction.Barcode;
				barcodeData.Quantity = transaction.Quantity.ToInt32();
				barcodeData.Company_Name = company.InstituteName;
				barcodeData.Address = company.Address;
				barcodeData.Place = company.Place;
				barcodeData.Country = company.Country;
				barcodeData.Pincode = company.Pincod;
				barcodeData.PhoneNumber = company.PhoneNo;
				barcodeData.Mobile = company.MobNo;
				barcodeData.Website = company.website;
				barcodeData.EmailId = company.EmailId;
				//barcodeData.ExpairyDate = db.ProductBarcodes().FirstOrDefault((ProductBarcodeEntity y) => y.Barcode == barcodeData.Barcode).ExpairyDate.ToString();
				//barcodeData.ManufacturingDate = db.ProductBarcodes().FirstOrDefault((ProductBarcodeEntity y) => y.Barcode == barcodeData.Barcode).ManufDate.ToString();
				barcodeData.PriceCode = AniHelper.GetPriceCode(transaction.Rate.ToString(CommonSettings.strCurencyFormat));
				barcodeData.Rate_In_Arabic = barcodeData.Sales_Rate.ToString(CommonSettings.strCurencyFormat).ToArabic();
				barcodeData.ItemCalory = productEntity.FieldTwo.ToInt32();
				list.Add(barcodeData);
			}
			return list;
		}

		public static List<SalesDetails> GetSDetailsFrmTransaction(List<Transaction> entTransactions, int iPartyId, DateTime date, bool isEstimate = false)
		{
			List<SalesDetails> entsalesdtls = new List<SalesDetails>();
			entTransactions.ForEach(delegate(Transaction x)
			{
				SalesDetails salesDetails = new SalesDetails
				{
					PartyAccountId = iPartyId,
					ProductId = x.ProductId,
					ProductName = x.ProductName,
					ProQty = x.Quantity,
					UnitId = x.UnitId,
					ProRate = x.Rate,
					ProDiscont = x.Discount,
					DiscAmnt = x.DiscountAmount,
					GrossAmnt = x.GrossAmount,
					NetAmnt = x.NETAmount,
					Date = date,
					Barcode = x.Barcode,
					EmployeeID = x.FK_EmployeeId,
					EmployeeCommision = x.EmployeeCommision
				};
				if (!isEstimate)
				{
					salesDetails.HSN = x.Hsn;
					salesDetails.GST = x.Gst;
					salesDetails.GSTAMOUNT = x.GstAmount;
					salesDetails.CGST = x.CGst;
					salesDetails.CGSTAmount = x.CGstAmount;
					salesDetails.SGST = x.Sgst;
					salesDetails.SGSTAmount = x.SGstAmount;
					salesDetails.IGST = x.IGst;
					salesDetails.IGSTAMOUNT = x.IGstAmount;
				}
				salesDetails.FK_BatchId = db.Batches().FirstOrDefault().Id;
				entsalesdtls.Add(salesDetails);
			});
			return entsalesdtls;
		}

		public static List<PurchaseInvoiceDtl> GetPDetailsFrmTransaction(List<Transaction> entTransactions, int iPartyId, DateTime date)
		{
			List<PurchaseInvoiceDtl> list = new List<PurchaseInvoiceDtl>();
			try
			{
				foreach (Transaction entTransaction in entTransactions)
				{
					PurchaseInvoiceDtl purchaseInvoiceDtl = new PurchaseInvoiceDtl();
					purchaseInvoiceDtl.PurSettingsId = null;
					purchaseInvoiceDtl.PartyAccId = iPartyId;
					purchaseInvoiceDtl.ProId = entTransaction.ProductId;
					purchaseInvoiceDtl.Barcode = entTransaction.Barcode;
					purchaseInvoiceDtl.ProQty = entTransaction.Quantity;
					purchaseInvoiceDtl.UnitId = entTransaction.UnitId;
					purchaseInvoiceDtl.ProRate = entTransaction.Rate;
					purchaseInvoiceDtl.Disc = entTransaction.Discount;
					purchaseInvoiceDtl.DiscAmnt = entTransaction.DiscountAmount;
					purchaseInvoiceDtl.Gross = entTransaction.GrossAmount;
					purchaseInvoiceDtl.NetAmnt = entTransaction.NETAmount;
					purchaseInvoiceDtl.Date = date;
					purchaseInvoiceDtl.HSN = entTransaction.Hsn;
					purchaseInvoiceDtl.GST = entTransaction.Gst;
					purchaseInvoiceDtl.GSTAMOUNT = entTransaction.GstAmount;
					purchaseInvoiceDtl.CGST = entTransaction.CGst;
					purchaseInvoiceDtl.CGSTAmount = entTransaction.CGstAmount;
					purchaseInvoiceDtl.SGST = entTransaction.Sgst;
					purchaseInvoiceDtl.SGSTAmount = entTransaction.SGstAmount;
					purchaseInvoiceDtl.IGST = entTransaction.IGst;
					purchaseInvoiceDtl.IGSTAMOUNT = entTransaction.IGstAmount;
					list.Add(purchaseInvoiceDtl);
				}
			}
			catch (Exception)
			{
				throw;
			}
			return list;
		}

		public static List<POSDetails> GetPOSDetailsFromSDetails(List<SalesDetails> salesDetails)
		{
			List<POSDetails> list = new List<POSDetails>();
			foreach (SalesDetails salesDetail in salesDetails)
			{
				POSDetails pOSDetails = new POSDetails();
				ProductEntity productEntity = db.Products(new { salesDetail.ProductId }).FirstOrDefault();
				pOSDetails.SL = 1;
				pOSDetails.productId = salesDetail.ProductId.toInt32();
				pOSDetails.Barcode = salesDetail.Barcode;
				pOSDetails.ProductCode = productEntity.ProductCode.ToString();
				string text = salesDetail.ProductName;
				if (text == string.Empty || text == null)
				{
					text = db.Productunits(new { salesDetail.Barcode }).FirstOrDefault().ProductUnitName;
				}
				pOSDetails.ProductName = text;
				//string localName = db.ProductBarcodes(new { salesDetail.Barcode }).FirstOrDefault().LocalName;
				//pOSDetails.LocalName = ((localName == string.Empty) ? productEntity.LocalName : localName);
				pOSDetails.Qty = salesDetail.ProQty;
				pOSDetails.UnitId = salesDetail.UnitId.toInt32();
				pOSDetails.UnitName = db.Units(new { salesDetail.UnitId }).FirstOrDefault().UnitName;
				pOSDetails.FreeQty = 0m;
				pOSDetails.Rate = salesDetail.ProRate;
				pOSDetails.Gross = salesDetail.GrossAmnt;
				pOSDetails.Discount = salesDetail.ProDiscont;
				pOSDetails.DisAmount = salesDetail.DiscAmnt;
				pOSDetails.Taxable = salesDetail.NetAmnt - salesDetail.GSTAMOUNT;
				pOSDetails.Tax = salesDetail.GST;
				pOSDetails.TaxAmount = salesDetail.GSTAMOUNT;
				pOSDetails.NetAmount = salesDetail.NetAmnt;
				list.Add(pOSDetails);
			}
			return list;
		}

		public static List<SalesRtnDetails> GetSRDetailsFrmTransaction(List<Transaction> entTransactions, int iPartyId, DateTime date, bool isEstimate = false)
		{
			List<SalesRtnDetails> entsalesRtndtls = new List<SalesRtnDetails>();
			entTransactions.ForEach(delegate(Transaction x)
			{
				SalesRtnDetails salesRtnDetails = new SalesRtnDetails
				{
					PartyAccountId = iPartyId,
					ProductId = x.ProductId,
					Barcode = x.Barcode,
					ProQty = x.Quantity,
					UnitId = x.UnitId,
					ProRate = x.Rate,
					ProDis = x.Discount,
					DiscAmnt = x.DiscountAmount,
					GrossAmnt = x.GrossAmount,
					NetAmnt = x.NETAmount,
					Date = date
				};
				if (!isEstimate)
				{
					salesRtnDetails.HSN = x.Hsn;
					salesRtnDetails.GST = x.Gst;
					salesRtnDetails.GSTAMOUNT = x.GstAmount;
					salesRtnDetails.CGST = x.CGst;
					salesRtnDetails.CGSTAmount = x.CGstAmount;
					salesRtnDetails.SGST = x.Sgst;
					salesRtnDetails.SGSTAmount = x.SGstAmount;
					salesRtnDetails.IGST = x.IGst;
					salesRtnDetails.IGSTAMOUNT = x.IGstAmount;
				}
				entsalesRtndtls.Add(salesRtnDetails);
			});
			return entsalesRtndtls;
		}

		public static List<PurRtnInvSttings> GetPRDetailsFrmTransaction(DBContext db, List<Transaction> entTransactions, int iPartyId, DateTime date)
		{
			List<PurRtnInvSttings> list = new List<PurRtnInvSttings>();
			try
			{
				foreach (Transaction entTransaction in entTransactions)
				{
					PurRtnInvSttings item = new PurRtnInvSttings
					{
						PurRtnSettId = null,
						PartyAccId = iPartyId,
						ProId = entTransaction.ProductId,
						Barcode = entTransaction.Barcode,
						ProQty = entTransaction.Quantity,
						UnitId = entTransaction.UnitId,
						ProRate = entTransaction.Rate,
						Disc = entTransaction.Discount,
						DiscAmnt = entTransaction.DiscountAmount,
						Gross = entTransaction.GrossAmount,
						NetAmnt = entTransaction.NETAmount,
						Date = date,
						HSN = entTransaction.Hsn,
						GST = entTransaction.Gst,
						GSTAMOUNT = entTransaction.GstAmount,
						Tax = entTransaction.Gst,
						TaxAmnt = entTransaction.GstAmount,
						CGST = entTransaction.CGst,
						CGSTAmount = entTransaction.CGstAmount,
						SGST = entTransaction.Sgst,
						SGSTAmount = entTransaction.SGstAmount,
						IGST = entTransaction.IGst,
						IGSTAMOUNT = entTransaction.IGstAmount
					};
					list.Add(item);
				}
			}
			catch (Exception)
			{
				throw;
			}
			return list;
		}

		public static List<SalesOrderSettings> GetSODetailsFrmTransaction(DBContext db, List<Transaction> entTransactions, int iPartyId, DateTime date, bool isEstimate = false)
		{
			List<SalesOrderSettings> entsalesOrderdtls = new List<SalesOrderSettings>();
			entTransactions.ForEach(delegate(Transaction x)
			{
				SalesOrderSettings item = new SalesOrderSettings
				{
					PartyAccountId = iPartyId,
					ProductId = x.ProductId,
					ProQty = x.Quantity,
					UnitId = x.UnitId,
					ProRate = x.Rate,
					ProDiscont = x.Discount,
					DiscAmnt = x.DiscountAmount,
					GrossAmnt = x.GrossAmount,
					NetAmnt = x.NETAmount,
					Taxable = x.Taxable,
					Tax = x.Vat,
					TaxAmount = x.VatAmount
				};
				entsalesOrderdtls.Add(item);
			});
			return entsalesOrderdtls;
		}
	}
}
