using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;

namespace Ashpro
{
	public class POSView : Form
	{
		private SerialPort sport;

		private Rectangle resolution;

		private Control focusedControl;

		private List<POSDetails> entPOSDetails;

		private POSDetails entPosDetail;

		private SalesMaster salesMaster;

		private List<SalesDetails> salesDetails;

		private int iFYearId;

		private int iTmr = 0;

		private int? iMasterId;

		private DataGridViewTextBoxEditingControl textbox;

		private int iRouteId;

		private int iVehicleId;

		private int iFYId;

		private int iCashAcctId;

		private DotmatrixMaster entDMaster;

		private List<DotmatrixSettings> entDSettings = new List<DotmatrixSettings>();

		private ExCompany entcompany;

		private PartyDetails entPartyDtl;

		private TransactionMaster entsaleshdr;

		private List<Transaction> entsalesdtls = new List<Transaction>();

		private AccountEntity account;

		private DBContext db;

		private string strVoucherType = "Sales Voucher";

		private string sBranchDetails = string.Empty;

		private int iSalesManID;

		private int iPartyAccountID;

		private int iSalesAccountId;

		private int iUserId;

		private int iNormalCashAccountId;

		private decimal dmlDiscountAmount;

		private bool isReturnAllowed = false;

		private int? iCardAccount;

		private int? iOtherAccount;

		private decimal dCashPayment;

		private decimal dCardPayment;

		private decimal dOtherPayment;

		private decimal? dTendered = null;

		private decimal? dChanges = null;

		private IContainer components = null;

		private Panel pnlTitle;

		private Button btnMinimize;

		private Button btnClose;

		private PictureBox picLogo;

		private Panel panel2;

		private AshUpDown nudInvoiceNo;

		private Button btnCustomer;

		private Button btnLookup;

		private Panel panel3;

		private Panel panel5;

		private Button btnSeperator2;

		private Panel pnlSide;

		private Button button2;

		private Button button1;

		private Button button64;

		private Button btnRemoveRow;

		private Button btnMinus;

		private Button btnPlus;

		private DataGridView dgItems;

		private Label lblDateTime;

		private Label lblSalesAccount;

		private Label lblUser;

		private Label label3;

		private Label lblDepot;

		private Label label1;

		private AshNumericLabel lblGrandTotal;

		private AshNumericLabel lblTotalQty;

		private Label label8;

		private AshNumericLabel lblRoundOff;

		private AshNumericLabel lblTaxTotal;

		private AshNumericLabel lblDiscount;

		private AshNumericLabel lblGross;

		private Label label12;

		private Label label11;

		private Label label10;

		private Label label9;

		private AshTextBoxNormal txtAddDiscount;

		private AshTextBoxNormal txtAddDiscountAmnt;

		private Label label14;

		private Label label13;

		private NumKeyBoardPOS numKeyBoardPOS1;

		private Panel panel6;

		private Button btnNew;

		private Button btnSave;

		private Button btnTouch;

		private Button btnKeyBoard;

		private Button btnDelete;

		private Button btnPrint;

		private Button btnSearchBill;

		private Button btnSalesman;

		private Button btnPayment;

		private Button btnSalesType;

		private Button btnPCategory;

		private Button btnDepot;

		private Button btnSalesAccount;

		private Button button3;

		private Button btnRetrieveBill;

		private Button btnHoldBill;

		private Timer tmrGrandTotal;

		private Timer tmrDateTime;

		private BindingSource bdsItems;

		private Label lblSalesType;

		private Label label2;

		private Label lblPriceCategory;

		private Label lblParty;

		private Label label7;

		private Label lblSalesman;

		private Label label6;

		private Label label4;

		private AshNumericLabel lblBalance;

		private AshNumericLabel lblPaidAmount;

		private Label label15;

		private Label label5;

		private CheckBox cbPrint;

		private DataGridViewTextBoxColumn col_SL;

		private DataGridViewTextBoxColumn col_ProductCode;

		private DataGridViewTextBoxColumn col_ProductName;

		private DataGridViewTextBoxColumn col_Qty;

		private DataGridViewTextBoxColumn col_Unit;

		private DataGridViewTextBoxColumn col_FreeQty;

		private DataGridViewTextBoxColumn col_Rate;

		private DataGridViewTextBoxColumn col_Gross;

		private DataGridViewTextBoxColumn col_Discount;

		private DataGridViewTextBoxColumn col_DisAmount;

		private DataGridViewTextBoxColumn col_Taxable;

		private DataGridViewTextBoxColumn col_Tax;

		private DataGridViewTextBoxColumn col_TaxAmount;

		private DataGridViewTextBoxColumn col_NetAmount;

		private Label lblsystem;

		private Panel pnlMain;

		private Panel panel8;

		private Panel panel7;

		private Panel panel4;

		public AshTextBoxNormal txtBarcode;

		public TabPage myTab { get; set; }

		public POSView(DashboardPOS dashboardPOS)
		{
			InitializeComponent();
			base.Width = Utility.uResolutionWidth - dashboardPOS.btnPanelShow.Width * 2;
			base.Height = Utility.uResolutionHeight - dashboardPOS.pnlTitle.Height;
			txtBarcode.GotFocus += TxtBarcode_GotFocus;
			txtAddDiscount.GotFocus += TxtBarcode_GotFocus;
			txtAddDiscountAmnt.GotFocus += TxtBarcode_GotFocus;
			btnLookup.GotFocus += BtnLookup_GotFocus;
			dgItems.GotFocus += DgItems_GotFocus;
		}

		public void DgvLoadMethod(List<POSDetails> pOSDetails, int iRowIndex)
		{
			dgItems.DataSource = null;
			bdsItems.DataSource = null;
			bdsItems.DataSource = entPOSDetails;
			dgItems.DataSource = bdsItems;
			TotalFindMethod();
			dgItems.ClearSelection();
			dgItems.FirstDisplayedScrollingRowIndex = iRowIndex;
			dgItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgItems.CurrentCell = dgItems.Rows[iRowIndex].Cells[col_ProductName.Name];
		}

		private void dgItemRateColumnEditMethod(decimal dRate, bool isInclusive = false)
		{
			if (entPosDetail == null)
			{
				entPosDetail = entPOSDetails.FirstOrDefault();
			}
			entPosDetail.Rate = dRate;
			entPosDetail.Gross = entPosDetail.Rate * entPosDetail.Qty;
			entPosDetail.DisAmount = entPosDetail.Gross * (entPosDetail.Discount / 100m);
			entPosDetail.Taxable = entPosDetail.Gross - entPosDetail.DisAmount;
			if (!isInclusive)
			{
				entPosDetail.TaxAmount = entPosDetail.Taxable * (entPosDetail.Tax / 100m);
				entPosDetail.NetAmount = entPosDetail.Taxable + entPosDetail.TaxAmount;
			}
			else
			{
				entPosDetail.TaxAmount = AniHelper.GetInclusiveAmount(entPosDetail.Taxable, entPosDetail.Tax);
				entPosDetail.NetAmount = entPosDetail.Taxable;
				entPosDetail.Taxable = entPosDetail.NetAmount - entPosDetail.TaxAmount;
			}
		}

		private void dgItemQtyColumnEditMethod(bool isInclusive = false)
		{
			entPosDetail.Gross = entPosDetail.Rate * entPosDetail.Qty;
			entPosDetail.DisAmount = entPosDetail.Gross * (entPosDetail.Discount / 100m);
			entPosDetail.Taxable = entPosDetail.Gross - entPosDetail.DisAmount;
			if (!isInclusive)
			{
				entPosDetail.TaxAmount = entPosDetail.Taxable * (entPosDetail.Tax / 100m);
				entPosDetail.NetAmount = entPosDetail.Taxable + entPosDetail.TaxAmount;
			}
			else
			{
				entPosDetail.TaxAmount = AniHelper.GetInclusiveAmount(entPosDetail.Taxable, entPosDetail.Tax);
				entPosDetail.NetAmount = entPosDetail.Taxable;
				entPosDetail.Taxable = entPosDetail.NetAmount - entPosDetail.TaxAmount;
			}
		}

		private void ScaleWithNoRateMethod(ProductEntity product, ref decimal? dmlRate, ref decimal? dmlQty, ref int? iUnitId)
		{
			if (!product.IsScale.toBool())
			{
				return;
			}
			if (!product.isRateBasedWeight.toBool())
			{
				string text = ((txtBarcode.Text == string.Empty) ? product.Barcode : txtBarcode.Text);
				iUnitId = AshproCommon.IdFindMethod("Select FK_UnitId From ProductBarcode Where Barcode='" + text + "'");
				dmlRate = POSHelper.SellingPriceAndDiscFindMethod(lblDepot.Tag.ToInt32(), "Sales Invoice", text);
				decimal? num = dmlRate;
				if ((num.GetValueOrDefault() == default(decimal)) & num.HasValue)
				{
					dmlRate = product.SellingPrice;
				}
				num = dmlRate;
				if ((num.GetValueOrDefault() == default(decimal)) & num.HasValue)
				{
					POSInputBox pOSInputBox = new POSInputBox();
					pOSInputBox.isScaleWithNoRate = true;
					pOSInputBox.isNameVisible = true;
					if (pOSInputBox.ShowDialog() == DialogResult.OK)
					{
						dmlRate = pOSInputBox.txtInput.Value;
						if (pOSInputBox.txtName.Text != string.Empty)
						{
							dmlQty = pOSInputBox.txtName.Text.ToDecimal();
						}
						product.isInclusive = pOSInputBox.cbInclusive.Checked;
					}
				}
				else
				{
					dmlQty = AniHelper.InputBoxMethod("Item Mesearment", "Please enter weight sacale.", "Mesearment").ToDecimal();
				}
				return;
			}
			POSInputBox pOSInputBox2 = new POSInputBox();
			pOSInputBox2.isScaleWithNoRate = true;
			pOSInputBox2.isNameVisible = true;
			if (pOSInputBox2.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			if (pOSInputBox2.txtInput.Value > 0m && pOSInputBox2.txtName.Text.ToDecimal() > 0m)
			{
				dmlRate = pOSInputBox2.txtInput.Value;
				dmlQty = pOSInputBox2.txtName.Text.ToDecimal();
			}
			else if (pOSInputBox2.txtInput.Value > 0m)
			{
				string text2 = ((txtBarcode.Text == string.Empty) ? product.Barcode : txtBarcode.Text);
				iUnitId = AshproCommon.IdFindMethod("Select FK_UnitId From ProductBarcode Where Barcode='" + text2 + "'");
				dmlRate = POSHelper.SellingPriceAndDiscFindMethod(lblDepot.Tag.ToInt32(), "Sales Invoice", text2);
				decimal? num2;
				if (!(pOSInputBox2.txtInput.Value == 0m))
				{
					decimal value = pOSInputBox2.txtInput.Value;
					decimal? num = dmlRate;
					num2 = (decimal?)value / num;
				}
				else
				{
					num2 = default(decimal);
				}
				dmlQty = num2;
			}
			else
			{
				dmlQty = pOSInputBox2.txtName.Text.ToDecimal();
			}
		}

		private void BarcodeAddmethod()
		{
			try
			{
				if (txtBarcode.Text == string.Empty)
				{
					txtBarcode.Focus();
					return;
				}
				if (salesMaster != null && salesMaster.IsClosed)
				{
					Messages.ErrorMessage("This Invoice is closed. So you can't edit the same.");
					return;
				}
				ProductEntity product = new ProductEntity();
				product = AshproCommon.GetObjectWithParameter_SP<ProductEntity>("spGetProductWithBarcode", new
				{
					Barcode = txtBarcode.Text
				});
				if (product != null && product.ProductName != null)
				{
					decimal? dmlRate = null;
					decimal? dmlQty = null;
					int? iUnitId = null;
					if (txtBarcode.Text.Length == 13)
					{
						string text = txtBarcode.Text.Substring(0, 3);
						if (text == CommonSettings.strRatebasedBarcode)
						{
							string text2 = txtBarcode.Text.Substring(7, 3);
							string text3 = txtBarcode.Text.Substring(10, 3);
							dmlRate = (text2 + "." + text3).ToDecimal();
						}
						else if (text == CommonSettings.strWeightbasedBarcode)
						{
							string text4 = txtBarcode.Text.Substring(7, 3);
							string text5 = txtBarcode.Text.Substring(10, 3);
							dmlQty = (text4 + "." + text5).ToDecimal();
						}
						else if (product != null)
						{
							ScaleWithNoRateMethod(product, ref dmlRate, ref dmlQty, ref iUnitId);
						}
					}
					else if (product != null)
					{
						ScaleWithNoRateMethod(product, ref dmlRate, ref dmlQty, ref iUnitId);
					}
					if (product != null)
					{
						int? num = iUnitId;
						iUnitId = (num.HasValue ? ((object)num.GetValueOrDefault()) : AshproCommon.IdFindMethod("Select FK_UnitId From ProductBarcode Where Barcode='" + txtBarcode.Text + "'"));
						string sProName = AshproCommon.IdFindMethod("Select ProductUnitName From ProductUnit Where Barcode='" + txtBarcode.Text + "'");
						int pOSDetails = POSHelper.GetPOSDetails(product, entPOSDetails, lblDepot.Tag.ToInt32(), "Sales Invoice", dmlQty, txtBarcode.Text, iUnitId, dmlRate, sProName);
						entPosDetail = entPOSDetails.Where((POSDetails x) => x.ProductCode == product.ProductCode.ToString()).FirstOrDefault();
						BindDgv();
						TotalFindMethod();
						dgItems.ClearSelection();
						dgItems.FirstDisplayedScrollingRowIndex = pOSDetails;
						dgItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
						dgItems.CurrentCell = dgItems.Rows[pOSDetails].Cells[col_ProductName.Name];
						txtBarcode.Focus();
					}
				}
				else
				{
					string text6 = txtBarcode.Text.Substring(0, 7);
					product = AshproCommon.GetObjectWithParameter_SP<ProductEntity>("spGetProductWithBarcode", new
					{
						Barcode = text6
					});
					if (product != null && product.ProductName != null)
					{
						string text7 = txtBarcode.Text.Substring(0, 3);
						decimal? dRate = null;
						decimal? qty = null;
						if (text7 == CommonSettings.strRatebasedBarcode)
						{
							string text8 = txtBarcode.Text.Substring(7, 3);
							string text9 = txtBarcode.Text.Substring(10, 3);
							int num2 = 2;
							if (CommonSettings.strCurencyFormat != string.Empty)
							{
								num2 = CommonSettings.strCurencyFormat.Substring(1, 1).ToInt32();
							}
							if (num2 == 2)
							{
								text9 = text9.Substring(0, 2);
							}
							dRate = (text8 + "." + text9).ToDecimal();
						}
						else if (text7 == CommonSettings.strWeightbasedBarcode)
						{
							string text10 = txtBarcode.Text.Substring(7, 3);
							string text11 = txtBarcode.Text.Substring(10, 3);
							int num3 = 2;
							if (CommonSettings.strQuantityFormat != string.Empty)
							{
								num3 = CommonSettings.strQuantityFormat.Substring(1, 1).ToInt32();
							}
							if (num3 == 2)
							{
								text11 = text11.Substring(0, 2);
							}
							qty = (text10 + "." + text11).ToDecimal();
						}
						if (product != null)
						{
							int value = AshproCommon.IdFindMethod("Select FK_UnitId From ProductBarcode Where Barcode='" + text6 + "'");
							int pOSDetails2 = POSHelper.GetPOSDetails(product, entPOSDetails, lblDepot.Tag.ToInt32(), "Sales Invoice", qty, text6, value, dRate);
							entPosDetail = entPOSDetails.Where((POSDetails x) => x.ProductCode == product.ProductCode.ToString()).FirstOrDefault();
							BindDgv();
							TotalFindMethod();
							dgItems.ClearSelection();
							dgItems.FirstDisplayedScrollingRowIndex = pOSDetails2;
							dgItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
							dgItems.CurrentCell = dgItems.Rows[pOSDetails2].Cells[col_ProductName.Name];
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			txtBarcode.Focus();
		}

		private bool LoadInitialSettings()
		{
			string text = Settings.Default.intLogOnId.ToString();
			lblUser.Text = AshproCommon.IdFindMethod("Select UserName From [User] Where UserId='" + text + "'");
			lblUser.Tag = text;
			lblSalesType.Tag = CommonSettings.strDefaultSalesType;
			lblSalesType.Text = CommonSettings.strDefaultSalesType;
			AshproCommon.FillCombo(lblDepot, "Select GodwnId as Id,GodownName as Name From Godown Where GodownName='PRIMARY'");
			AshproCommon.FillCombo(lblSalesAccount, "Select AccountId as Id,AccountName as Name From Account Where AccountName = 'SALES ACCOUNT'");
			AshproCommon.FillCombo(lblParty, "Select AccountId as Id,AccountName as Name From Account Where AccountName = 'CASH ACCOUNT'");
			AshproCommon.FillCombo(lblSalesman, "Select EmpID as Id,EmpName as Name From EmployeeDetails Where EmployeeID='1001' and FK_BranchId='" + Settings.Default.iBranchId + "' ");
			AshproCommon.FillCombo(lblPriceCategory, "Select PCategoryId as Id, PCategoryName as Name From PriceCategory Where PCategoryName='REGULAR' ");
			iRouteId = AshproCommon.IdFindMethod("Select RootId From Root Where RootName='ALL' and FK_BranchId='" + Settings.Default.iBranchId + "'", isVoucherNo: false);
			iVehicleId = AshproCommon.IdFindMethod("Select VanId From Vehicle Where VehName='ALL' and FK_BranchId='" + Settings.Default.iBranchId + "'", isVoucherNo: false);
			iFYId = AshproCommon.IdFindMethod("Select FyearId from SelectedFYear", isVoucherNo: false);
			return true;
		}

		private void TotalFindMethod()
		{
			decimal num = txtAddDiscountAmnt.Text.ToDecimal();
			decimal num2 = entPOSDetails.Sum((POSDetails x) => x.NetAmount) - num;
			decimal num3 = Math.Round(num2, 0);
			decimal num4 = num2 - num3;
			lblTotalQty.Text = entPOSDetails.Sum((POSDetails x) => x.Qty).ToString(CommonSettings.strCurencyFormat);
			lblGross.Text = entPOSDetails.Sum((POSDetails x) => x.Gross).ToString(CommonSettings.strCurencyFormat);
			lblDiscount.Text = entPOSDetails.Sum((POSDetails x) => x.DisAmount).ToString(CommonSettings.strCurencyFormat);
			lblTaxTotal.Text = entPOSDetails.Sum((POSDetails x) => x.TaxAmount).ToString(CommonSettings.strCurencyFormat);
			lblGrandTotal.Text = num3.ToString(CommonSettings.strCurencyFormat);
			lblRoundOff.Text = num4.ToString(CommonSettings.strCurencyFormat);
			tmrGrandTotal.Enabled = true;
		}

		private decimal FreeAmountTotalFindMethod()
		{
			decimal result = default(decimal);
			try
			{
				foreach (POSDetails item in entPOSDetails.Where((POSDetails x) => x.Rate == 0m).ToList())
				{
					result += AshproCommon.ValueFindMethod("SELECT SPrice FROM ProductPriceCategory where ProductId='" + item.productId + "' and PCategoryId='" + lblDepot.Tag.ToInt32() + "' and UnitId='" + item.UnitId + "'");
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
			return result;
		}

		private void SalesMasterMethod(int InvoiceNo, bool isPOSHold = false)
		{
			salesMaster = new SalesMaster();
			salesMaster.SalesInvId = iMasterId;
			salesMaster.SalesInvNo = InvoiceNo;
			salesMaster.InvType = CommonSettings.strDefaultSalesType;
			salesMaster.SalesDate = DateTime.Now;
			salesMaster.SalesAccountId = iSalesAccountId;
			salesMaster.PartyAccountId = iPartyAccountID;
			salesMaster.RouteId = iRouteId;
			salesMaster.VehId = iVehicleId;
			salesMaster.StafId = iSalesManID;
			salesMaster.JEId = Settings.Default.intLogOnId;
			salesMaster.PriceCategoryId = lblDepot.Tag.ToInt32();
			salesMaster.AditionalAmount = 0m;
			salesMaster.Frieght = 0m;
			salesMaster.BillDiscount = txtAddDiscountAmnt.Value;
			salesMaster.TotalDiscount = dmlDiscountAmount;
			salesMaster.GrandDiscount = lblGrandTotal.Value;
			salesMaster.EntryDate = DateTime.Now.Date;
			salesMaster.Remarks = "POS Entry";
			salesMaster.TaxableAmount = entPOSDetails.Sum((POSDetails x) => x.Taxable);
			salesMaster.FYearID = iFYearId;
			salesMaster.TotalTaxAmount = entPOSDetails.Sum((POSDetails x) => x.TaxAmount);
			salesMaster.FloodCess = 0m;
			if (CommonSettings.blnGSTEnabled)
			{
				if (CommonSettings.strDefaultSalesType.Contains("Interstate"))
				{
					salesMaster.IGSTAmount = salesMaster.TotalTaxAmount;
					salesMaster.SGSTAmount = 0m;
					salesMaster.CGSTAmount = 0m;
				}
				else
				{
					salesMaster.IGSTAmount = 0m;
					salesMaster.SGSTAmount = salesMaster.TotalTaxAmount / 2m;
					salesMaster.CGSTAmount = salesMaster.TotalTaxAmount / 2m;
				}
			}
			salesMaster.FK_BranchId = Settings.Default.iBranchId;
			salesMaster.CardPayment = dCardPayment;
			if (!isPOSHold && dCashPayment == 0m && lblBalance.Value == lblGrandTotal.Value && iPartyAccountID == iNormalCashAccountId)
			{
				salesMaster.CashPayment = lblBalance.Value;
			}
			else
			{
				salesMaster.CashPayment = dCashPayment;
			}
			if (iPartyAccountID != iNormalCashAccountId)
			{
				salesMaster.Credit = lblBalance.Value;
			}
			salesMaster.CntrInvNo = lblsystem.Text.Substring(lblsystem.Text.Length - 2) + salesMaster.SalesInvNo;
			salesMaster.SequenceNo = Utility.uSerialNumber;
			salesMaster.Terminal = Utility.uTerminal;
			salesMaster.IsClosed = false;
			salesMaster.FreeAmount = FreeAmountTotalFindMethod();
			if (iPartyAccountID == iNormalCashAccountId)
			{
				salesMaster.ReturnAmount = ((lblGrandTotal.Value > 0m) ? 0m : lblGrandTotal.Value);
			}
			else
			{
				salesMaster.ReturnAmount = 0m;
			}
			salesMaster.OtherPayment = dOtherPayment;
			salesMaster.CashAccountId = iCashAcctId;
			salesMaster.CardAccountId = iCardAccount;
			salesMaster.OnlineAccountId = iOtherAccount;
			salesMaster.IsSettiled = ((!(salesMaster.Credit > 0m)) ? true : false);
		}

		private void ReloadMethod(SalesMaster salesMaster, bool isSales = false)
		{
			try
			{
				salesDetails = new List<SalesDetails>();
				if (!isSales)
				{
					iMasterId = AshproCommon.IdFindMethod("Select Id From POSHoldMaster Where InvType='" + salesMaster.InvType + "' and SalesInvNo='" + salesMaster.SalesInvNo + "' " + sBranchDetails, isVoucherNo: false);
					int? num = iMasterId;
					salesDetails = AshproCommon.GetListMethod<SalesDetails>("Select * From POSHoldSettings Where SalesInvId='" + num + "'");
				}
				else
				{
					iMasterId = AshproCommon.IdFindMethod("Select SalesInvId From SalesEstimateMaster Where InvType='" + salesMaster.InvType + "' and SalesInvNo='" + salesMaster.SalesInvNo + "' and FYearID='" + iFYId + "'" + sBranchDetails, isVoucherNo: false);
					int? num = iMasterId;
					salesDetails = AshproCommon.GetListMethod<SalesDetails>("Select * From SalesEstimateSettings Where SalesInvId='" + num + "'");
				}
				LoadMethod(salesMaster, salesDetails);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void BindDgv()
		{
			dgItems.DataSource = null;
			bdsItems.DataSource = null;
			bdsItems.DataSource = entPOSDetails;
			dgItems.DataSource = bdsItems;
			txtBarcode.Text = string.Empty;
		}

		private void LoadMethod(SalesMaster salesMaster, List<SalesDetails> salesDetails)
		{
			entPOSDetails = new List<POSDetails>();
			entPOSDetails = POSHelper.GetPOSDetailsFromSDetails(salesDetails);
			for (int i = 0; i < entPOSDetails.Count; i++)
			{
				entPOSDetails[i].SL = i + 1;
			}
			decimal num = default(decimal);
			bdsItems.DataSource = null;
			dgItems.DataSource = null;
			bdsItems.DataSource = entPOSDetails;
			dgItems.DataSource = bdsItems;
			decimal num2 = salesMaster.GrandDiscount + salesMaster.BillDiscount;
			decimal billDiscount = salesMaster.BillDiscount;
			if (billDiscount > 0m)
			{
				num = billDiscount * 100m / num2;
			}
			txtAddDiscount.Text = num.ToString(CommonSettings.strCurencyFormat);
			txtAddDiscountAmnt.Text = billDiscount.ToString(CommonSettings.strCurencyFormat);
			lblPriceCategory.Tag = salesMaster.PriceCategoryId;
			lblPriceCategory.Text = AshproCommon.IdFindMethod("Select PCategoryName From PriceCategory Where PCategoryId = '" + salesMaster.PriceCategoryId + "'");
			lblParty.Tag = salesMaster.PartyAccountId;
			lblParty.Text = AshproCommon.IdFindMethod("Select AccountName From Account Where AccountId = '" + salesMaster.PartyAccountId + "'");
			lblSalesman.Tag = salesMaster.StafId;
			lblSalesman.Text = AshproCommon.IdFindMethod("Select EmpName From EmployeeDetails Where EmpID = '" + salesMaster.StafId + "'");
			lblSalesAccount.Tag = salesMaster.SalesAccountId;
			lblSalesAccount.Text = AshproCommon.IdFindMethod("Select AccountName From Account Where AccountId = '" + salesMaster.SalesAccountId + "'");
			nudInvoiceNo.Value = salesMaster.SalesInvNo.toInt32();
			lblSalesType.Text = salesMaster.InvType;
			TotalFindMethod();
		}

		private void RowDeleteMethod()
		{
			if (dgItems.CurrentRow != null && MessageBox.Show("Are You Sure To Delete This Item", Utility.uManufacturer, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No)
			{
				entPosDetail = (POSDetails)dgItems.CurrentRow.DataBoundItem;
				entPOSDetails.Remove(entPosDetail);
				for (int i = 0; i < entPOSDetails.Count; i++)
				{
					entPOSDetails[i].SL = i + 1;
				}
				BindDgv();
				TotalFindMethod();
				txtBarcode.Focus();
			}
		}

		private void DeleteSubMethod()
		{
			string[] obj = new string[6] { "Delete From GdwnStckTranLog Where VchrId='", null, null, null, null, null };
			int? num = iMasterId;
			obj[1] = num.ToString();
			obj[2] = "' and VchrType ='";
			obj[3] = strVoucherType;
			obj[4] = "' and Narration='POS Entry'";
			obj[5] = sBranchDetails;
			AshproCommon.DeleteMethod(string.Concat(obj));
			string[] obj2 = new string[6] { "Delete From StockTranLog Where VchrId='", null, null, null, null, null };
			num = iMasterId;
			obj2[1] = num.ToString();
			obj2[2] = "' and VchrType ='";
			obj2[3] = strVoucherType;
			obj2[4] = "' and Narration='POS Entry'";
			obj2[5] = sBranchDetails;
			AshproCommon.DeleteMethod(string.Concat(obj2));
			DataTable dataTable = new DataTable();
			string[] obj3 = new string[6] { "Select TranId From TransactionLog Where VchrId='", null, null, null, null, null };
			num = iMasterId;
			obj3[1] = num.ToString();
			obj3[2] = "' and VchrType='";
			obj3[3] = strVoucherType;
			obj3[4] = "'";
			obj3[5] = sBranchDetails;
			dataTable = AshproCommon.GetDataTable(string.Concat(obj3));
			foreach (DataRow row in dataTable.Rows)
			{
				AshproCommon.DeleteFromDatabase("TransactionLog", "TranId", row[0].ToString().ToInt32());
			}
			string[] obj4 = new string[6] { "Select TranId From TransactionLog Where VchrId='", null, null, null, null, null };
			num = iMasterId;
			obj4[1] = num.ToString();
			obj4[2] = "'and VchrNo='";
			obj4[3] = nudInvoiceNo.Value.ToInt32().ToString();
			obj4[4] = "' and VchrType='Journal Voucher' and VchrName='TAX PAYABLE'";
			obj4[5] = sBranchDetails;
			dataTable = AshproCommon.GetDataTable(string.Concat(obj4));
			foreach (DataRow row2 in dataTable.Rows)
			{
				AshproCommon.DeleteFromDatabase("TransactionLog", "TranId", row2[0].ToString().ToInt32());
			}
			AshproCommon.DeleteFromDatabase("SalesEstimateSettings", "SalesInvId", iMasterId.ToInt32());
		}

		private void SaveMethodSub(int InvoiceNo)
		{
			if (AshproCommon.InsertToDatabase(salesDetails.Cast<object>().ToList(), "SalesEstimateSettings"))
			{
				POSHelper.TranLogMethod(0m, salesMaster.GrandDiscount, Convert.ToInt32(lblSalesAccount.Tag.ToString()), Convert.ToInt32(lblParty.Tag.ToString()), iMasterId.toInt32(), InvoiceNo, strVoucherType, Convert.ToInt32(lblUser.Tag.ToString()), "Main", lblSalesType.Text);
				POSHelper.TranLogMethod(salesMaster.GrandDiscount, 0m, Convert.ToInt32(lblParty.Tag.ToString()), Convert.ToInt32(lblSalesAccount.Tag.ToString()), iMasterId.toInt32(), InvoiceNo, strVoucherType, Convert.ToInt32(lblUser.Tag.ToString()), "Sub", lblSalesType.Text);
				if (CommonSettings.blnTaxEnabled && lblTaxTotal.Value > 0m)
				{
					int num = AshproCommon.IdFindMethod("Select AccountId From Account Where AccountName = 'TAX PAYABLE'", isVoucherNo: false);
					POSHelper.TranLogMethod(salesMaster.TotalTaxAmount, 0m, Convert.ToInt32(lblSalesAccount.Tag.ToString()), num, iMasterId.toInt32(), InvoiceNo, "Journal Voucher", Convert.ToInt32(lblUser.Tag.ToString()), "Main", "TAX PAYABLE");
					POSHelper.TranLogMethod(0m, salesMaster.TotalTaxAmount, num, Convert.ToInt32(lblSalesAccount.Tag.ToString()), iMasterId.toInt32(), InvoiceNo, "Journal Voucher", Convert.ToInt32(lblUser.Tag.ToString()), "Sub", "TAX PAYABLE");
				}
				if (CommonSettings.blnCessEnabled && salesMaster.FloodCess > 0m)
				{
					int num2 = AshproCommon.IdFindMethod("Select AccountId From Account Where AccountName = 'TAX PAYABLE'", isVoucherNo: false);
					POSHelper.TranLogMethod(salesMaster.FloodCess, 0m, lblSalesAccount.Tag.ToInt32(), num2, iMasterId.toInt32(), salesMaster.SalesInvNo, "Journal Voucher", Settings.Default.intLogOnId, "Main", "TAX PAYABLE");
					POSHelper.TranLogMethod(0m, salesMaster.FloodCess, num2, lblSalesAccount.Tag.ToInt32(), iMasterId.toInt32(), salesMaster.SalesInvNo, "Journal Voucher", Settings.Default.intLogOnId, "Sub", "TAX PAYABLE");
				}
				POSHelper.SaveStockTranLoagMethod(entPOSDetails, iMasterId.toInt32(), InvoiceNo, lblSalesType.Text, strVoucherType, lblParty.Tag.ToInt32(), "POS Entry", lblUser.Tag.ToInt32(), lblDepot.Tag.ToInt32());
				decimal num3 = Convert.ToDecimal(lblPaidAmount.Text);
				if (num3 > 0m)
				{
					int num4 = AshproCommon.IdFindMethod("select max(VchrNo) as maxValue from VchrTranLog where VchrType='Receipt Voucher' and VchrName='Cash Receipt'", isVoucherNo: true);
					POSHelper.VoucherTranLog(num4, DateTime.Now.Date, num3, "Receipt Voucher", "Cash Received On Sale For Pos", Convert.ToInt32(lblParty.Tag.ToString()), iCashAcctId, Convert.ToInt32(lblSalesman.Tag.ToString()), Convert.ToInt32(lblUser.Tag.ToString()), "Cash Receipt");
					int iVoucherID = AshproCommon.IdFindMethod("Select VchrTranId From VchrTranLog Where VchrNo='" + num4 + "' and VchrType ='Receipt Voucher'", isVoucherNo: false);
					POSHelper.TranLogMethod(num3, 0m, iCashAcctId, Convert.ToInt32(lblParty.Tag.ToString()), iVoucherID, num4, "Receipt Voucher", Convert.ToInt32(lblUser.Tag.ToString()), "Main");
					POSHelper.TranLogMethod(0m, num3, Convert.ToInt32(lblParty.Tag.ToString()), iCashAcctId, iVoucherID, num4, "Receipt Voucher", Convert.ToInt32(lblUser.Tag.ToString()), "Sub");
				}
			}
		}

		private void btnClearMethod()
		{
			entPOSDetails.Clear();
			entPOSDetails = new List<POSDetails>();
			bdsItems.DataSource = null;
			dgItems.DataSource = null;
			entPosDetail = new POSDetails();
			AniHelper.ClearMethod(this);
			iMasterId = 0;
			txtBarcode.Focus();
		}

		private void CellContentMethod()
		{
			if (dgItems.Rows.Count != 0 && dgItems.CurrentCell.OwningColumn.Name == col_Unit.Name)
			{
				SelectCustomer selectCustomer = new SelectCustomer("Unit");
				int num = (selectCustomer.ProductId = entPosDetail.productId);
				if (selectCustomer.ShowDialog() == DialogResult.OK)
				{
					string barcode = "";
					entPosDetail.Barcode = barcode;
					entPosDetail.UnitName = selectCustomer.sName.Trim();
					entPosDetail.ProductName = db.Productunits(new
					{
						ProductId = entPosDetail.productId,
						UnitId = selectCustomer.iId
					}).FirstOrDefault().ProductUnitName;
					POSHelper.SellingPriceAndDiscFindMethod(lblDepot.Tag.ToInt32(), "Sales Invoice", barcode);
					entPosDetail.Rate = POSHelper.dmlRate;
					entPosDetail.Discount = POSHelper.dmlDiscount;
					entPosDetail.Gross = entPosDetail.Rate * entPosDetail.Qty;
					entPosDetail.DisAmount = entPosDetail.Rate * (entPosDetail.Discount / 100m);
					entPosDetail.Taxable = entPosDetail.Gross - entPosDetail.DisAmount;
					int? tCategoryId = db.Products(new
					{
						ProductId = entPosDetail.productId
					}).FirstOrDefault().TCategoryId;
					POSDetails pOSDetails = entPosDetail;
					Type typeFromHandle = typeof(Convert);
					int? num2 = tCategoryId;
					pOSDetails.Tax = Convert.ToDecimal(AshproCommon.IdFindMethod("Select SalesVat From TaxCategory Where TCategoryId='" + num2 + "'"));
					entPosDetail.TaxAmount = entPosDetail.Taxable * (entPosDetail.Tax / 100m);
					entPosDetail.NetAmount = entPosDetail.Taxable + entPosDetail.TaxAmount;
					TotalFindMethod();
					dgItems.Refresh();
				}
			}
		}

		private void DeleteHoldedBill()
		{
			try
			{
				AshproCommon.DeleteFromDatabase("POSHoldSettings", "SalesInvId", iMasterId.ToInt32());
				AshproCommon.DeleteFromDatabase("POSHoldMaster", "Id", iMasterId.ToInt32());
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			iMasterId = null;
		}

		private void DgItems_GotFocus(object sender, EventArgs e)
		{
			focusedControl = (DataGridView)sender;
			focusedControl.Focus();
		}

		private void TxtBarcode_GotFocus(object sender, EventArgs e)
		{
			focusedControl = (TextBox)sender;
			focusedControl.Focus();
		}

		private void BtnLookup_GotFocus(object sender, EventArgs e)
		{
			txtBarcode.Focus();
		}

		private void numKeyBoardPOS1_atNumberClick(object source)
		{
			try
			{
				focusedControl.Focus();
				if (focusedControl.Text != string.Empty && focusedControl is AshTextBoxNormal)
				{
					AshTextBoxNormal ashTextBoxNormal = (AshTextBoxNormal)focusedControl;
					ashTextBoxNormal.Select(ashTextBoxNormal.Text.Length, 0);
				}
				else if (focusedControl is DataGridView)
				{
					dgItems.BeginEdit(selectAll: true);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void numKeyBoardPOS1_atQtyClick(object source)
		{
			if (dgItems.CurrentRow != null)
			{
				dgItems.Focus();
				dgItems.CurrentCell = dgItems.CurrentRow.Cells[col_Qty.Name];
			}
		}

		private void numKeyBoardPOS1_atEnterClick(object source)
		{
			txtBarcode.Focus();
		}

		private void numKeyBoardPOS1_atRateClick(object source)
		{
			if (dgItems.CurrentRow != null)
			{
				dgItems.Focus();
				dgItems.CurrentCell = dgItems.CurrentRow.Cells[col_Rate.Name];
			}
		}

		private void numKeyBoardPOS1_atClearClick(object source)
		{
		}

		private void numKeyBoardPOS1_atBackClick(object source)
		{
		}

		private void dgItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			CellContentMethod();
		}

		private void dgItems_CellContentClick(object sender, MouseEventArgs e)
		{
			CellContentMethod();
		}

		private void dgItems_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			if (e.Control is DataGridViewTextBoxEditingControl)
			{
				textbox = (DataGridViewTextBoxEditingControl)e.Control;
				if (textbox != null)
				{
					textbox.KeyPress += textbox_KeyPress;
				}
			}
		}

		private void dgItems_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				RowDeleteMethod();
			}
			else if (e.KeyCode == Keys.F6)
			{
				entPosDetail = new POSDetails();
				entPosDetail = (POSDetails)dgItems.CurrentRow.DataBoundItem;
				ProductTracing productTracing = new ProductTracing();
				productTracing.TypeVoucher = "Sales";
				productTracing.PartyAccntId = lblParty.Tag.ToInt32();
				productTracing.ProductId = entPosDetail.productId;
				productTracing.ShowDialog();
			}
		}

		private void dgItems_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
		}

		private void dgItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			entPosDetail = (POSDetails)dgItems.CurrentRow.DataBoundItem;
			if (dgItems.CurrentCell.OwningColumn.Name == col_Qty.Name)
			{
				bool isInclusive = AshproCommon.IdFindMethod("Select isInclusive From Product Where ProductId = '" + entPosDetail.productId + "'");
				dgItemQtyColumnEditMethod(isInclusive);
			}
			else if (dgItems.CurrentCell.OwningColumn.Name == col_Rate.Name)
			{
				dgItemRateColumnEditMethod(entPosDetail.Rate);
			}
			else if (dgItems.CurrentCell.OwningColumn.Name == col_Discount.Name)
			{
				entPosDetail.DisAmount = entPosDetail.Gross * (entPosDetail.Discount / 100m);
				entPosDetail.Taxable = entPosDetail.Gross - entPosDetail.DisAmount;
				entPosDetail.TaxAmount = entPosDetail.Taxable * (entPosDetail.Tax / 100m);
				entPosDetail.NetAmount = entPosDetail.Taxable + entPosDetail.TaxAmount;
			}
			else if (dgItems.CurrentCell.OwningColumn.Name == col_Tax.Name)
			{
				entPosDetail.TaxAmount = entPosDetail.Taxable * (entPosDetail.Tax / 100m);
				entPosDetail.NetAmount = entPosDetail.Taxable + entPosDetail.TaxAmount;
			}
			TotalFindMethod();
			dgItems.Refresh();
		}

		private void textbox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((dgItems.CurrentCell.OwningColumn.Name == col_Qty.Name || dgItems.CurrentCell.OwningColumn.Name == col_Rate.Name) && !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.' || (sender as TextBox).Text.Contains('.')) && (e.KeyChar != '.' || (sender as TextBox).SelectionLength != (sender as TextBox).TextLength) && (e.KeyChar != '-' || (sender as TextBox).Text.Contains('-')) && (e.KeyChar != '-' || (sender as TextBox).SelectionLength != (sender as TextBox).TextLength))
			{
				e.Handled = true;
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Messages.CloseConfirmationMessage(this);
		}

		private void POSView_Load(object sender, EventArgs e)
		{
			base.Controls.Remove(pnlTitle);
			if (myTab != null)
			{
				base.Width = myTab.Width + 120;
				base.Height = myTab.Height + 120;
			}
			db = new DBContext();
			lblsystem.Text = Environment.MachineName;
			tmrDateTime.Start();
			entPOSDetails = new List<POSDetails>();
			dgItems.AutoGenerateColumns = false;
			dgItems.DataSource = null;
			bdsItems.DataSource = null;
			bdsItems.DataSource = entPOSDetails;
			dgItems.DataSource = bdsItems;
			col_Rate.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Gross.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Discount.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_DisAmount.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Taxable.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Tax.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_TaxAmount.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_NetAmount.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			if (LoadInitialSettings())
			{
				iFYearId = AshproCommon.IdFindMethod("Select FyearId from SelectedFYear");
				if (CommonSettings.blnEnableMultiBranch)
				{
					nudInvoiceNo.Value = ((from x in db.SalesMasters()
						where x.InvType == lblSalesType.Text && x.FYearID == iFYearId && x.FK_BranchId == Settings.Default.iBranchId
						select x).Max((SalesMaster x) => x.SalesInvNo) + 1) ?? 1;
				}
				else
				{
					nudInvoiceNo.Value = ((from x in db.SalesMasters()
						where x.InvType == lblSalesType.Text && x.FYearID == iFYearId
						select x).Max((SalesMaster x) => x.SalesInvNo) + 1) ?? 1;
				}
				base.ActiveControl = txtBarcode;
			}
			else
			{
				Close();
			}
			if (CommonSettings.blnEnableMultiBranch)
			{
				sBranchDetails = " and FK_BranchId = '" + Settings.Default.iBranchId + "'";
			}
		}

		private void btnMinimize_Click(object sender, EventArgs e)
		{
			base.WindowState = FormWindowState.Minimized;
		}

		private void btnLookup_Click(object sender, EventArgs e)
		{
			SelectCustomer selectCustomer = new SelectCustomer("Product");
			if (selectCustomer.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			ProductEntity product = db.Products(new
			{
				ProductId = selectCustomer.iId
			}).FirstOrDefault();
			decimal? dmlRate = null;
			int? iUnitId = null;
			if (product != null)
			{
				decimal? dmlQty = null;
				ScaleWithNoRateMethod(product, ref dmlRate, ref dmlQty, ref iUnitId);
				int pOSDetails = POSHelper.GetPOSDetails(product, entPOSDetails, lblDepot.Tag.ToInt32(), "Sales Invoice", dmlQty, product.Barcode, iUnitId, dmlRate);
				entPosDetail = entPOSDetails.Where((POSDetails x) => x.productId == product.ProductId).FirstOrDefault();
				BindDgv();
				TotalFindMethod();
				dgItems.ClearSelection();
				dgItems.FirstDisplayedScrollingRowIndex = pOSDetails;
				dgItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
				dgItems.CurrentCell = dgItems.Rows[pOSDetails].Cells[col_ProductName.Name];
				txtBarcode.Focus();
			}
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			if (CommonSettings.blnEnableMultiBranch)
			{
				nudInvoiceNo.Value = ((from x in db.SalesMasters()
					where x.InvType == lblSalesType.Text && x.FYearID == iFYearId && x.FK_BranchId == Settings.Default.iBranchId
					select x).Max((SalesMaster x) => x.SalesInvNo) + 1) ?? 1;
			}
			else
			{
				nudInvoiceNo.Value = ((from x in db.SalesMasters()
					where x.InvType == lblSalesType.Text && x.FYearID == iFYearId
					select x).Max((SalesMaster x) => x.SalesInvNo) + 1) ?? 1;
			}
			btnClearMethod();
		}

		private void btnCustomer_Click(object sender, EventArgs e)
		{
			SelectCustomer selectCustomer = new SelectCustomer("Customer");
			if (selectCustomer.ShowDialog() == DialogResult.OK)
			{
				lblParty.Text = selectCustomer.sName.Trim();
				lblParty.Tag = selectCustomer.iId;
				lblParty.Refresh();
			}
		}

		private void tmrDateTime_Tick(object sender, EventArgs e)
		{
			lblDateTime.Text = DateTime.Now.ToString();
		}

		private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if (e.KeyCode != Keys.Return)
				{
					return;
				}
				if (txtBarcode.Text == string.Empty)
				{
					return;
				}
				ProductBarcodeEntity productBarcode = db.ProductBarcodes().FirstOrDefault((ProductBarcodeEntity x) => x.Barcode == txtBarcode.Text);
				if (productBarcode == null)
				{
					return;
				}
				ProductEntity productEntity = db.Products().FirstOrDefault((ProductEntity x) => x.ProductId == productBarcode.FK_ProductId && x.BrandId == Settings.Default.iBranchId);
				if (productEntity != null)
				{
					POSHelper.GetPOSDetails(productEntity, entPOSDetails, lblPriceCategory.Tag.ToInt32());
					entPosDetail = entPOSDetails.Where((POSDetails x) => x.ProductCode == txtBarcode.Text).FirstOrDefault();
					BindDgv();
					TotalFindMethod();
					txtBarcode.Focus();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnPlus_Click(object sender, EventArgs e)
		{
			if (entPOSDetails.Count != 0)
			{
				if (entPosDetail == null && entPOSDetails.Count > 0)
				{
					entPosDetail = (POSDetails)dgItems.CurrentRow.DataBoundItem;
				}
				entPosDetail.Qty += 1m;
				entPosDetail.Gross = entPosDetail.Rate * entPosDetail.Qty;
				entPosDetail.DisAmount = entPosDetail.Rate * (entPosDetail.Discount / 100m);
				entPosDetail.Taxable = entPosDetail.Gross - entPosDetail.DisAmount;
				entPosDetail.TaxAmount = entPosDetail.Taxable * (entPosDetail.Tax / 100m);
				entPosDetail.NetAmount = entPosDetail.Taxable + entPosDetail.TaxAmount;
				TotalFindMethod();
				dgItems.Refresh();
			}
		}

		private void btnMinus_Click(object sender, EventArgs e)
		{
			if (entPOSDetails.Count == 0)
			{
				return;
			}
			if (entPosDetail == null && entPOSDetails.Count > 0)
			{
				entPosDetail = entPOSDetails.Where((POSDetails x) => x.SL == 1).FirstOrDefault();
			}
			entPosDetail.Qty -= 1m;
			entPosDetail.Gross = entPosDetail.Rate * entPosDetail.Qty;
			entPosDetail.DisAmount = entPosDetail.Rate * (entPosDetail.Discount / 100m);
			entPosDetail.Taxable = entPosDetail.Gross - entPosDetail.DisAmount;
			entPosDetail.TaxAmount = entPosDetail.Taxable * (entPosDetail.Tax / 100m);
			entPosDetail.NetAmount = entPosDetail.Taxable + entPosDetail.TaxAmount;
			TotalFindMethod();
			dgItems.Refresh();
		}

		private void btnRemoveRow_Click(object sender, EventArgs e)
		{
			if (entPOSDetails.Count != 0)
			{
				RowDeleteMethod();
			}
		}

		private void btnSalesAccount_Click(object sender, EventArgs e)
		{
			SelectCustomer selectCustomer = new SelectCustomer("SAccount");
			if (selectCustomer.ShowDialog() == DialogResult.OK)
			{
				lblSalesAccount.Text = selectCustomer.sName.Trim();
				lblSalesAccount.Tag = selectCustomer.iId;
				lblSalesAccount.Refresh();
			}
		}

		private void btnSalesman_Click(object sender, EventArgs e)
		{
			SelectCustomer selectCustomer = new SelectCustomer("Salesman");
			if (selectCustomer.ShowDialog() == DialogResult.OK)
			{
				lblSalesman.Text = selectCustomer.sName.Trim();
				lblSalesman.Tag = selectCustomer.iId;
				lblSalesman.Refresh();
			}
		}

		private void btnDepot_Click(object sender, EventArgs e)
		{
			SelectCustomer selectCustomer = new SelectCustomer("Depot");
			if (selectCustomer.ShowDialog() == DialogResult.OK)
			{
				lblDepot.Text = selectCustomer.sName.Trim();
				lblDepot.Tag = selectCustomer.iId;
				lblDepot.Refresh();
			}
		}

		private void btnPCategory_Click(object sender, EventArgs e)
		{
			SelectCustomer selectCustomer = new SelectCustomer("PCategory");
			if (selectCustomer.ShowDialog() == DialogResult.OK)
			{
				lblPriceCategory.Text = selectCustomer.sName.Trim();
				lblPriceCategory.Tag = selectCustomer.iId;
				lblPriceCategory.Refresh();
			}
		}

		private void btnSalesType_Click(object sender, EventArgs e)
		{
			SelectCustomer selectCustomer = new SelectCustomer("SType");
			if (selectCustomer.ShowDialog() == DialogResult.OK)
			{
				lblSalesType.Text = selectCustomer.sName.Trim();
				lblSalesType.Tag = selectCustomer.iId;
				lblSalesType.Refresh();
			}
		}

		private void btnPayment_Click(object sender, EventArgs e)
		{
			int num = AshproCommon.IdFindMethod("Select AGroupId From Account Where AccountId = '" + lblParty.Tag?.ToString() + "'", isVoucherNo: false);
			int num2 = AshproCommon.IdFindMethod("Select AGroupId From AccountGroup Where GroupName ='CASH IN HAND'", isVoucherNo: false);
			if (num == num2)
			{
				MessageBox.Show("You don't want to make payment in the case of cash sales", Utility.uManufacturer, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			POSCashPayment pOSCashPayment = new POSCashPayment();
			decimal num5 = (pOSCashPayment.lblInvoiceAmount.Value = (pOSCashPayment.lblBalanceAmount.Value = entPOSDetails.Sum((POSDetails x) => x.NetAmount)));
			if (pOSCashPayment.ShowDialog() == DialogResult.OK)
			{
				lblPaidAmount.Text = (pOSCashPayment.txtCardPaid.Value + pOSCashPayment.txtCashPaid.Value).ToString(CommonSettings.strCurencyFormat);
				lblBalance.Text = pOSCashPayment.lblBalanceAmount.Value.ToString(CommonSettings.strCurencyFormat);
				iCashAcctId = pOSCashPayment.cmbCashAccount.SelectedValue.ToInt32();
			}
		}

		private void tmrGrandTotal_Tick(object sender, EventArgs e)
		{
			if (lblGrandTotal.ForeColor == Color.Green)
			{
				lblGrandTotal.ForeColor = Color.Red;
			}
			else
			{
				lblGrandTotal.ForeColor = Color.Green;
			}
			iTmr++;
			if (iTmr > 10)
			{
				iTmr = 0;
				tmrGrandTotal.Enabled = false;
			}
		}

		private void btnKeyBoard_Click(object sender, EventArgs e)
		{
			AniHelper.OSKMethod();
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			entDMaster = db.DotmatrixMasters().FirstOrDefault((DotmatrixMaster x) => x.VochrType == "Sales Invoice" && x.FormType == lblSalesType.Text && x.isDefault);
			if (entDMaster != null)
			{
				entDSettings = new List<DotmatrixSettings>();
				entDSettings = (from x in db.DotmatrixSettings()
					where x.printMasterId == entDMaster.printMasterId
					select x).ToList();
				Company company = new Company();
				company = db.Companies().FirstOrDefault();
				entcompany = company.CopyTo<ExCompany>();
				entcompany.CompanyName = company.InstituteName;
				entPartyDtl = new PartyDetails();
				account = db.Accounts().FirstOrDefault((AccountEntity x) => x.AccountId == lblParty.Tag.ToInt32());
				entPartyDtl = POSHelper.GetParty(account);
				int iSalesInvId = AshproCommon.IdFindMethod("Select SalesInvId From SalesEstimateMaster Where InvType='" + lblSalesType.Text + "'and SalesInvNo='" + nudInvoiceNo.Value + "' and FYearID='" + iFYearId + "'" + sBranchDetails, isVoucherNo: false);
				entsaleshdr = new TransactionMaster();
				salesMaster = new SalesMaster();
				salesMaster = db.SalesMasters().FirstOrDefault((SalesMaster x) => x.SalesInvId == iSalesInvId);
				entsaleshdr = POSHelper.GetTMaster(salesMaster);
				entsalesdtls = new List<Transaction>();
				salesDetails = new List<SalesDetails>();
				salesDetails = (from x in db.SalesDetails()
					where x.SalesInvId == iSalesInvId
					select x).ToList();
				entsalesdtls = POSHelper.GetTransactionsFromSDetails(salesDetails, db);
				PrintHelper printHelper = new PrintHelper();
				printHelper.PrintMethod(entDMaster, entDSettings, entcompany, entsaleshdr, entPartyDtl, entsalesdtls, entsaleshdr, null, entsaleshdr);
				printHelper = null;
			}
			else
			{
				Messages.InformationMessage("There is no default print design. Please make your own design");
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if (Messages.DeleteConfirmationMessage())
				{
					DeleteSubMethod();
					AshproCommon.DeleteFromDatabase("SalesEstimateMaster", "SalesInvId", iMasterId.ToInt32());
				}
				btnNew_Click(null, null);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (entPOSDetails.Count == 0)
			{
				MessageBox.Show("No records to save!", Utility.uManufacturer);
				return;
			}
			int InvoiceNo = 0;
			salesDetails = POSHelper.SalesDetailsMethod(entPOSDetails, lblParty.Tag.ToInt32(), lblSalesType.Text);
			int? num = iMasterId;
			if ((num.GetValueOrDefault() == 0) & num.HasValue)
			{
				if (CommonSettings.blnEnableMultiBranch)
				{
					InvoiceNo = ((from x in db.SalesMasters()
						where x.InvType == lblSalesType.Text && x.FYearID == iFYearId && x.FK_BranchId == Settings.Default.iBranchId
						select x).Max((SalesMaster x) => x.SalesInvNo) + 1) ?? 1;
				}
				else
				{
					InvoiceNo = ((from x in db.SalesMasters()
						where x.InvType == lblSalesType.Text && x.FYearID == iFYearId
						select x).Max((SalesMaster x) => x.SalesInvNo) + 1) ?? 1;
				}
				SalesMasterMethod(InvoiceNo);
				salesMaster.SalesInvId = null;
				if (AshproCommon.InsertToDatabaseObj(salesMaster, "SalesEstimateMaster"))
				{
					if (CommonSettings.blnEnableMultiBranch)
					{
						salesMaster = db.SalesMasters().FirstOrDefault((SalesMaster x) => x.InvType == lblSalesType.Text && x.SalesInvNo == InvoiceNo && x.FYearID == iFYearId && x.FK_BranchId == Settings.Default.iBranchId);
					}
					else
					{
						salesMaster = db.SalesMasters().FirstOrDefault((SalesMaster x) => x.InvType == lblSalesType.Text && x.SalesInvNo == InvoiceNo && x.FYearID == iFYearId);
					}
					if (salesMaster != null)
					{
						iMasterId = salesMaster.SalesInvId;
						foreach (SalesDetails salesDetail in salesDetails)
						{
							salesDetail.SalesSettingsId = null;
							salesDetail.SalesInvId = iMasterId.toInt32();
						}
						SaveMethodSub(InvoiceNo);
					}
				}
			}
			else
			{
				foreach (SalesDetails salesDetail2 in salesDetails)
				{
					salesDetail2.SalesSettingsId = null;
					salesDetail2.SalesInvId = iMasterId.toInt32();
				}
				DeleteSubMethod();
				num = iMasterId;
				InvoiceNo = AshproCommon.IdFindMethod("Select SalesInvNo From SalesEstimateMaster Where SalesInvId='" + num + "'", isVoucherNo: false);
				SalesMasterMethod(InvoiceNo);
				if (AshproCommon.UpdateToDatabaseObj(salesMaster, "SalesEstimateMaster", "SalesInvId", iMasterId.ToInt32()))
				{
					SaveMethodSub(InvoiceNo);
				}
			}
			btnNew_Click(null, null);
		}

		private void nudInvoiceNo_ValueChanged(object sender, EventArgs e)
		{
			if (CommonSettings.blnEnableMultiBranch)
			{
				salesMaster = (from x in db.SalesMasters()
					where x.InvType == lblSalesType.Text && x.SalesInvNo == nudInvoiceNo.Value.ToInt32() && x.FYearID == iFYearId && x.FK_BranchId == Settings.Default.iBranchId
					select x).FirstOrDefault();
			}
			else
			{
				salesMaster = (from x in db.SalesMasters()
					where x.InvType == lblSalesType.Text && x.SalesInvNo == nudInvoiceNo.Value.ToInt32() && x.FYearID == iFYearId
					select x).FirstOrDefault();
			}
			if (salesMaster != null)
			{
				iMasterId = salesMaster.SalesInvId.ToInt32();
				int? num = iMasterId;
				salesDetails = AshproCommon.GetListMethod<SalesDetails>("Select * From SalesEstimateSettings Where SalesInvId='" + num + "'");
				LoadMethod(salesMaster, salesDetails);
			}
			else
			{
				btnClearMethod();
			}
		}

		private void btnTouch_Click(object sender, EventArgs e)
		{
			POSProductView pOSProductView = new POSProductView(this);
			pOSProductView.entPOSDetails = entPOSDetails;
			pOSProductView.iPCategoryID = lblPriceCategory.Tag.ToInt32();
			if (pOSProductView.ShowDialog() == DialogResult.OK)
			{
				txtBarcode.Text = string.Empty;
				txtBarcode.Focus();
			}
		}

		private void txtAddDiscountAmnt_Validated(object sender, EventArgs e)
		{
			if (txtAddDiscountAmnt.Text == string.Empty)
			{
				txtAddDiscount.Text = "0";
				return;
			}
			decimal value = txtAddDiscountAmnt.Value;
			decimal num = entPOSDetails.Sum((POSDetails x) => x.NetAmount);
			decimal num2 = default(decimal);
			if (value > 0m && num > 0m)
			{
				num2 = value / num * 100m;
			}
			txtAddDiscount.Text = num2.ToString(CommonSettings.strCurencyFormat);
			TotalFindMethod();
		}

		private void txtAddDiscount_Validated(object sender, EventArgs e)
		{
			if (txtAddDiscount.Text == string.Empty)
			{
				txtAddDiscountAmnt.Text = "0";
				return;
			}
			decimal value = txtAddDiscount.Value;
			decimal num = entPOSDetails.Sum((POSDetails x) => x.NetAmount);
			decimal num2 = default(decimal);
			if (value > 0m)
			{
				num2 = value / 100m * num;
			}
			txtAddDiscountAmnt.Text = num2.ToString(CommonSettings.strCurencyFormat);
			TotalFindMethod();
		}

		private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
			{
				e.Handled = true;
			}
		}

		private void btnSearchBill_Click(object sender, EventArgs e)
		{
			POSFormSearch pOSFormSearch = new POSFormSearch();
			pOSFormSearch.searchType = "BILL";
			pOSFormSearch.FormType = lblSalesType.Text;
			if (pOSFormSearch.ShowDialog() == DialogResult.OK)
			{
				salesMaster = pOSFormSearch.SalesMaster;
				ReloadMethod(salesMaster, isSales: true);
			}
		}

		private void btnRetrieveBill_Click(object sender, EventArgs e)
		{
			POSFormSearch pOSFormSearch = new POSFormSearch();
			pOSFormSearch.searchType = "Retrieve";
			pOSFormSearch.FormType = lblSalesType.Text;
			if (pOSFormSearch.ShowDialog() == DialogResult.OK)
			{
				ReloadMethod(pOSFormSearch.SalesMaster);
				DeleteHoldedBill();
			}
		}

		private void btnHoldBill_Click(object sender, EventArgs e)
		{
			if (db.POSHoldMasters().Any((SalesMaster x) => x.SalesInvNo == nudInvoiceNo.Value.ToInt32()))
			{
				SalesMasterMethod(db.POSHoldMasters().Max((SalesMaster x) => x.SalesInvNo + 1).toInt32(), isPOSHold: true);
			}
			else
			{
				SalesMasterMethod(nudInvoiceNo.Value.ToInt32(), isPOSHold: true);
			}
			SalesMasterMethod(Convert.ToInt32(nudInvoiceNo.Value.ToString()));
			POSHelper.InvoiceHoldMethod(salesMaster, entPOSDetails);
			btnNew_Click(null, null);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ashpro.POSView));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
			pnlTitle = new System.Windows.Forms.Panel();
			btnMinimize = new System.Windows.Forms.Button();
			btnClose = new System.Windows.Forms.Button();
			lblsystem = new System.Windows.Forms.Label();
			lblSalesType = new System.Windows.Forms.Label();
			lblUser = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			picLogo = new System.Windows.Forms.PictureBox();
			label3 = new System.Windows.Forms.Label();
			panel2 = new System.Windows.Forms.Panel();
			lblDateTime = new System.Windows.Forms.Label();
			btnLookup = new System.Windows.Forms.Button();
			txtBarcode = new AshUsercontrol.AshTextBoxNormal();
			nudInvoiceNo = new AshUsercontrol.AshUpDown();
			btnCustomer = new System.Windows.Forms.Button();
			panel5 = new System.Windows.Forms.Panel();
			cbPrint = new System.Windows.Forms.CheckBox();
			txtAddDiscount = new AshUsercontrol.AshTextBoxNormal();
			txtAddDiscountAmnt = new AshUsercontrol.AshTextBoxNormal();
			lblBalance = new AshUsercontrol.AshNumericLabel();
			lblRoundOff = new AshUsercontrol.AshNumericLabel();
			lblPaidAmount = new AshUsercontrol.AshNumericLabel();
			lblTaxTotal = new AshUsercontrol.AshNumericLabel();
			lblDiscount = new AshUsercontrol.AshNumericLabel();
			lblGross = new AshUsercontrol.AshNumericLabel();
			label15 = new System.Windows.Forms.Label();
			label12 = new System.Windows.Forms.Label();
			label14 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label11 = new System.Windows.Forms.Label();
			label10 = new System.Windows.Forms.Label();
			label9 = new System.Windows.Forms.Label();
			label13 = new System.Windows.Forms.Label();
			lblTotalQty = new AshUsercontrol.AshNumericLabel();
			label8 = new System.Windows.Forms.Label();
			lblGrandTotal = new AshUsercontrol.AshNumericLabel();
			btnSeperator2 = new System.Windows.Forms.Button();
			pnlSide = new System.Windows.Forms.Panel();
			button3 = new System.Windows.Forms.Button();
			button2 = new System.Windows.Forms.Button();
			button1 = new System.Windows.Forms.Button();
			button64 = new System.Windows.Forms.Button();
			btnRemoveRow = new System.Windows.Forms.Button();
			btnMinus = new System.Windows.Forms.Button();
			btnPlus = new System.Windows.Forms.Button();
			dgItems = new System.Windows.Forms.DataGridView();
			col_SL = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_ProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_FreeQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Gross = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_DisAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Taxable = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Tax = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_TaxAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_NetAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			panel6 = new System.Windows.Forms.Panel();
			panel8 = new System.Windows.Forms.Panel();
			btnTouch = new System.Windows.Forms.Button();
			btnKeyBoard = new System.Windows.Forms.Button();
			btnNew = new System.Windows.Forms.Button();
			numKeyBoardPOS1 = new AshUsercontrol.NumKeyBoardPOS();
			panel7 = new System.Windows.Forms.Panel();
			btnSalesAccount = new System.Windows.Forms.Button();
			btnSalesType = new System.Windows.Forms.Button();
			btnSalesman = new System.Windows.Forms.Button();
			btnDepot = new System.Windows.Forms.Button();
			btnHoldBill = new System.Windows.Forms.Button();
			btnRetrieveBill = new System.Windows.Forms.Button();
			btnPCategory = new System.Windows.Forms.Button();
			btnSearchBill = new System.Windows.Forms.Button();
			btnPayment = new System.Windows.Forms.Button();
			panel4 = new System.Windows.Forms.Panel();
			btnSave = new System.Windows.Forms.Button();
			btnDelete = new System.Windows.Forms.Button();
			btnPrint = new System.Windows.Forms.Button();
			panel3 = new System.Windows.Forms.Panel();
			lblSalesAccount = new System.Windows.Forms.Label();
			lblPriceCategory = new System.Windows.Forms.Label();
			lblParty = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			lblSalesman = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			lblDepot = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			tmrGrandTotal = new System.Windows.Forms.Timer(components);
			tmrDateTime = new System.Windows.Forms.Timer(components);
			bdsItems = new System.Windows.Forms.BindingSource(components);
			pnlMain = new System.Windows.Forms.Panel();
			pnlTitle.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
			panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)nudInvoiceNo).BeginInit();
			panel5.SuspendLayout();
			pnlSide.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgItems).BeginInit();
			panel6.SuspendLayout();
			panel8.SuspendLayout();
			panel7.SuspendLayout();
			panel4.SuspendLayout();
			panel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)bdsItems).BeginInit();
			pnlMain.SuspendLayout();
			SuspendLayout();
			pnlTitle.BackColor = System.Drawing.Color.FromArgb(29, 48, 65);
			pnlTitle.Controls.Add(btnMinimize);
			pnlTitle.Controls.Add(btnClose);
			pnlTitle.Controls.Add(lblsystem);
			pnlTitle.Controls.Add(lblSalesType);
			pnlTitle.Controls.Add(lblUser);
			pnlTitle.Controls.Add(label2);
			pnlTitle.Controls.Add(picLogo);
			pnlTitle.Controls.Add(label3);
			pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
			pnlTitle.Location = new System.Drawing.Point(0, 0);
			pnlTitle.Name = "pnlTitle";
			pnlTitle.Size = new System.Drawing.Size(1386, 44);
			pnlTitle.TabIndex = 1;
			pnlTitle.Visible = false;
			btnMinimize.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			btnMinimize.FlatAppearance.BorderColor = System.Drawing.Color.White;
			btnMinimize.FlatAppearance.BorderSize = 0;
			btnMinimize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnMinimize.Font = new System.Drawing.Font("Verdana", 21.75f);
			btnMinimize.ForeColor = System.Drawing.Color.DarkGray;
			btnMinimize.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnMinimize.Location = new System.Drawing.Point(1270, 1);
			btnMinimize.Name = "btnMinimize";
			btnMinimize.Size = new System.Drawing.Size(55, 47);
			btnMinimize.TabIndex = 118;
			btnMinimize.TabStop = false;
			btnMinimize.Text = "-";
			btnMinimize.UseVisualStyleBackColor = true;
			btnMinimize.Click += new System.EventHandler(btnMinimize_Click);
			btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			btnClose.BackColor = System.Drawing.Color.Transparent;
			btnClose.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnClose.FlatAppearance.BorderSize = 0;
			btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 15f, System.Drawing.FontStyle.Bold);
			btnClose.ForeColor = System.Drawing.Color.White;
			btnClose.Image = (System.Drawing.Image)resources.GetObject("btnClose.Image");
			btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnClose.Location = new System.Drawing.Point(1330, 1);
			btnClose.Name = "btnClose";
			btnClose.Size = new System.Drawing.Size(55, 47);
			btnClose.TabIndex = 114;
			btnClose.UseVisualStyleBackColor = false;
			btnClose.Click += new System.EventHandler(btnClose_Click);
			lblsystem.AutoSize = true;
			lblsystem.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblsystem.ForeColor = System.Drawing.Color.White;
			lblsystem.Location = new System.Drawing.Point(836, 19);
			lblsystem.Name = "lblsystem";
			lblsystem.Size = new System.Drawing.Size(22, 16);
			lblsystem.TabIndex = 0;
			lblsystem.Text = "--";
			lblSalesType.AutoSize = true;
			lblSalesType.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblSalesType.ForeColor = System.Drawing.Color.White;
			lblSalesType.Location = new System.Drawing.Point(616, 19);
			lblSalesType.Name = "lblSalesType";
			lblSalesType.Size = new System.Drawing.Size(46, 16);
			lblSalesType.TabIndex = 0;
			lblSalesType.Text = "Local";
			lblUser.AutoSize = true;
			lblUser.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblUser.ForeColor = System.Drawing.Color.White;
			lblUser.Location = new System.Drawing.Point(352, 19);
			lblUser.Name = "lblUser";
			lblUser.Size = new System.Drawing.Size(54, 16);
			lblUser.TabIndex = 0;
			lblUser.Text = "Admin";
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label2.ForeColor = System.Drawing.Color.White;
			label2.Location = new System.Drawing.Point(506, 19);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(95, 16);
			label2.TabIndex = 0;
			label2.Text = "Sales Type :";
			picLogo.BackColor = System.Drawing.Color.Transparent;
			picLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			picLogo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			picLogo.Location = new System.Drawing.Point(2, -26);
			picLogo.Name = "picLogo";
			picLogo.Size = new System.Drawing.Size(216, 71);
			picLogo.TabIndex = 113;
			picLogo.TabStop = false;
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label3.ForeColor = System.Drawing.Color.White;
			label3.Location = new System.Drawing.Point(274, 19);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(50, 16);
			label3.TabIndex = 0;
			label3.Text = "User :";
			panel2.Controls.Add(lblDateTime);
			panel2.Controls.Add(btnLookup);
			panel2.Controls.Add(txtBarcode);
			panel2.Controls.Add(nudInvoiceNo);
			panel2.Dock = System.Windows.Forms.DockStyle.Top;
			panel2.Location = new System.Drawing.Point(0, 44);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(1386, 43);
			panel2.TabIndex = 0;
			lblDateTime.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			lblDateTime.AutoSize = true;
			lblDateTime.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblDateTime.Location = new System.Drawing.Point(1128, 15);
			lblDateTime.Name = "lblDateTime";
			lblDateTime.Size = new System.Drawing.Size(105, 16);
			lblDateTime.TabIndex = 3;
			lblDateTime.Text = "0 : 0 : 0 : 0 : 0";
			btnLookup.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			btnLookup.BackColor = System.Drawing.Color.White;
			btnLookup.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
			btnLookup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnLookup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold);
			btnLookup.ForeColor = System.Drawing.Color.DimGray;
			btnLookup.Image = (System.Drawing.Image)resources.GetObject("btnLookup.Image");
			btnLookup.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnLookup.Location = new System.Drawing.Point(989, 4);
			btnLookup.Name = "btnLookup";
			btnLookup.Size = new System.Drawing.Size(100, 35);
			btnLookup.TabIndex = 2;
			btnLookup.Text = "Look up";
			btnLookup.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			btnLookup.UseVisualStyleBackColor = false;
			btnLookup.Click += new System.EventHandler(btnLookup_Click);
			txtBarcode.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtBarcode.Data = 0;
			txtBarcode.EnterColor = System.Drawing.Color.LightPink;
			txtBarcode.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtBarcode.Format = null;
			txtBarcode.isAllowNegative = false;
			txtBarcode.isAllowSpecialChar = false;
			txtBarcode.isNumberOnly = true;
			txtBarcode.isNumeric = false;
			txtBarcode.LeaveColor = System.Drawing.Color.White;
			txtBarcode.Location = new System.Drawing.Point(199, 20);
			txtBarcode.Margin = new System.Windows.Forms.Padding(4);
			txtBarcode.Name = "txtBarcode";
			txtBarcode.Size = new System.Drawing.Size(194, 16);
			txtBarcode.TabIndex = 1;
			txtBarcode.Value = new decimal(new int[4]);
			txtBarcode.WaterMark = " Barcode";
			txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(txtBarcode_KeyDown);
			txtBarcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtBarcode_KeyPress);
			nudInvoiceNo.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			nudInvoiceNo.Location = new System.Drawing.Point(12, 16);
			nudInvoiceNo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			nudInvoiceNo.Maximum = new decimal(new int[4] { 1000000000, 0, 0, 0 });
			nudInvoiceNo.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
			nudInvoiceNo.Name = "nudInvoiceNo";
			nudInvoiceNo.Size = new System.Drawing.Size(178, 23);
			nudInvoiceNo.TabIndex = 0;
			nudInvoiceNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			nudInvoiceNo.Value = new decimal(new int[4] { 1, 0, 0, 0 });
			nudInvoiceNo.ValueChanged += new System.EventHandler(nudInvoiceNo_ValueChanged);
			btnCustomer.BackColor = System.Drawing.Color.FromArgb(17, 55, 81);
			btnCustomer.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnCustomer.FlatAppearance.BorderSize = 0;
			btnCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold);
			btnCustomer.ForeColor = System.Drawing.Color.White;
			btnCustomer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnCustomer.Location = new System.Drawing.Point(11, 3);
			btnCustomer.Name = "btnCustomer";
			btnCustomer.Size = new System.Drawing.Size(193, 41);
			btnCustomer.TabIndex = 137;
			btnCustomer.Text = "Cash/Party";
			btnCustomer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			btnCustomer.UseVisualStyleBackColor = false;
			btnCustomer.Click += new System.EventHandler(btnCustomer_Click);
			panel5.Controls.Add(cbPrint);
			panel5.Controls.Add(txtAddDiscount);
			panel5.Controls.Add(txtAddDiscountAmnt);
			panel5.Controls.Add(lblBalance);
			panel5.Controls.Add(lblRoundOff);
			panel5.Controls.Add(lblPaidAmount);
			panel5.Controls.Add(lblTaxTotal);
			panel5.Controls.Add(lblDiscount);
			panel5.Controls.Add(lblGross);
			panel5.Controls.Add(label15);
			panel5.Controls.Add(label12);
			panel5.Controls.Add(label14);
			panel5.Controls.Add(label5);
			panel5.Controls.Add(label11);
			panel5.Controls.Add(label10);
			panel5.Controls.Add(label9);
			panel5.Controls.Add(label13);
			panel5.Controls.Add(lblTotalQty);
			panel5.Controls.Add(label8);
			panel5.Controls.Add(lblGrandTotal);
			panel5.Controls.Add(btnSeperator2);
			panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
			panel5.Location = new System.Drawing.Point(0, 514);
			panel5.Name = "panel5";
			panel5.Size = new System.Drawing.Size(974, 154);
			panel5.TabIndex = 3;
			cbPrint.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			cbPrint.AutoSize = true;
			cbPrint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			cbPrint.Location = new System.Drawing.Point(712, 129);
			cbPrint.Name = "cbPrint";
			cbPrint.Size = new System.Drawing.Size(47, 17);
			cbPrint.TabIndex = 118;
			cbPrint.Text = "Print";
			cbPrint.UseVisualStyleBackColor = true;
			txtAddDiscount.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			txtAddDiscount.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtAddDiscount.Data = 0;
			txtAddDiscount.EnterColor = System.Drawing.Color.LightPink;
			txtAddDiscount.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtAddDiscount.Format = null;
			txtAddDiscount.isAllowNegative = false;
			txtAddDiscount.isAllowSpecialChar = false;
			txtAddDiscount.isNumberOnly = false;
			txtAddDiscount.isNumeric = true;
			txtAddDiscount.LeaveColor = System.Drawing.Color.White;
			txtAddDiscount.Location = new System.Drawing.Point(926, 15);
			txtAddDiscount.Margin = new System.Windows.Forms.Padding(4);
			txtAddDiscount.Name = "txtAddDiscount";
			txtAddDiscount.Size = new System.Drawing.Size(36, 16);
			txtAddDiscount.TabIndex = 117;
			txtAddDiscount.Text = "0";
			txtAddDiscount.Value = new decimal(new int[4]);
			txtAddDiscount.WaterMark = null;
			txtAddDiscount.Validated += new System.EventHandler(txtAddDiscount_Validated);
			txtAddDiscountAmnt.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			txtAddDiscountAmnt.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtAddDiscountAmnt.Data = 0;
			txtAddDiscountAmnt.EnterColor = System.Drawing.Color.LightPink;
			txtAddDiscountAmnt.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtAddDiscountAmnt.Format = null;
			txtAddDiscountAmnt.isAllowNegative = false;
			txtAddDiscountAmnt.isAllowSpecialChar = false;
			txtAddDiscountAmnt.isNumberOnly = false;
			txtAddDiscountAmnt.isNumeric = true;
			txtAddDiscountAmnt.LeaveColor = System.Drawing.Color.White;
			txtAddDiscountAmnt.Location = new System.Drawing.Point(805, 15);
			txtAddDiscountAmnt.Margin = new System.Windows.Forms.Padding(4);
			txtAddDiscountAmnt.Name = "txtAddDiscountAmnt";
			txtAddDiscountAmnt.Size = new System.Drawing.Size(82, 16);
			txtAddDiscountAmnt.TabIndex = 117;
			txtAddDiscountAmnt.Text = "0";
			txtAddDiscountAmnt.Value = new decimal(new int[4]);
			txtAddDiscountAmnt.WaterMark = null;
			txtAddDiscountAmnt.Validated += new System.EventHandler(txtAddDiscountAmnt_Validated);
			lblBalance.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			lblBalance.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblBalance.Format = null;
			lblBalance.Location = new System.Drawing.Point(850, 72);
			lblBalance.Name = "lblBalance";
			lblBalance.Size = new System.Drawing.Size(112, 16);
			lblBalance.TabIndex = 116;
			lblBalance.Text = "0";
			lblBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			lblBalance.Value = new decimal(new int[4]);
			lblRoundOff.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			lblRoundOff.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblRoundOff.Format = null;
			lblRoundOff.Location = new System.Drawing.Point(614, 128);
			lblRoundOff.Name = "lblRoundOff";
			lblRoundOff.Size = new System.Drawing.Size(87, 16);
			lblRoundOff.TabIndex = 116;
			lblRoundOff.Text = "0";
			lblRoundOff.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			lblRoundOff.Value = new decimal(new int[4]);
			lblPaidAmount.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			lblPaidAmount.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblPaidAmount.Format = null;
			lblPaidAmount.Location = new System.Drawing.Point(850, 44);
			lblPaidAmount.Name = "lblPaidAmount";
			lblPaidAmount.Size = new System.Drawing.Size(112, 16);
			lblPaidAmount.TabIndex = 116;
			lblPaidAmount.Text = "0";
			lblPaidAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			lblPaidAmount.Value = new decimal(new int[4]);
			lblTaxTotal.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			lblTaxTotal.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblTaxTotal.Format = null;
			lblTaxTotal.Location = new System.Drawing.Point(614, 100);
			lblTaxTotal.Name = "lblTaxTotal";
			lblTaxTotal.Size = new System.Drawing.Size(87, 16);
			lblTaxTotal.TabIndex = 116;
			lblTaxTotal.Text = "0";
			lblTaxTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			lblTaxTotal.Value = new decimal(new int[4]);
			lblDiscount.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			lblDiscount.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblDiscount.Format = null;
			lblDiscount.Location = new System.Drawing.Point(614, 72);
			lblDiscount.Name = "lblDiscount";
			lblDiscount.Size = new System.Drawing.Size(87, 16);
			lblDiscount.TabIndex = 116;
			lblDiscount.Text = "0";
			lblDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			lblDiscount.Value = new decimal(new int[4]);
			lblGross.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			lblGross.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblGross.Format = null;
			lblGross.Location = new System.Drawing.Point(614, 44);
			lblGross.Name = "lblGross";
			lblGross.Size = new System.Drawing.Size(87, 16);
			lblGross.TabIndex = 116;
			lblGross.Text = "0";
			lblGross.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			lblGross.Value = new decimal(new int[4]);
			label15.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			label15.AutoSize = true;
			label15.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label15.Location = new System.Drawing.Point(723, 72);
			label15.Name = "label15";
			label15.Size = new System.Drawing.Size(70, 16);
			label15.TabIndex = 115;
			label15.Text = "Balance :";
			label12.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			label12.AutoSize = true;
			label12.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label12.Location = new System.Drawing.Point(561, 128);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(59, 16);
			label12.TabIndex = 115;
			label12.Text = "Round :";
			label14.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			label14.AutoSize = true;
			label14.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label14.Location = new System.Drawing.Point(895, 13);
			label14.Name = "label14";
			label14.Size = new System.Drawing.Size(32, 16);
			label14.TabIndex = 115;
			label14.Text = "% :";
			label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label5.Location = new System.Drawing.Point(747, 44);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(46, 16);
			label5.TabIndex = 115;
			label5.Text = "Paid :";
			label11.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			label11.AutoSize = true;
			label11.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label11.Location = new System.Drawing.Point(577, 100);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(43, 16);
			label11.TabIndex = 115;
			label11.Text = "Tax :";
			label10.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			label10.AutoSize = true;
			label10.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label10.Location = new System.Drawing.Point(544, 72);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(76, 16);
			label10.TabIndex = 115;
			label10.Text = "Discount :";
			label9.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			label9.AutoSize = true;
			label9.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label9.Location = new System.Drawing.Point(565, 44);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(55, 16);
			label9.TabIndex = 115;
			label9.Text = "Gross :";
			label13.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			label13.AutoSize = true;
			label13.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label13.Location = new System.Drawing.Point(717, 13);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(76, 16);
			label13.TabIndex = 113;
			label13.Text = "Add Disc :";
			lblTotalQty.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			lblTotalQty.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblTotalQty.Format = null;
			lblTotalQty.Location = new System.Drawing.Point(614, 16);
			lblTotalQty.Name = "lblTotalQty";
			lblTotalQty.Size = new System.Drawing.Size(87, 16);
			lblTotalQty.TabIndex = 114;
			lblTotalQty.Text = "0";
			lblTotalQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			lblTotalQty.Value = new decimal(new int[4]);
			label8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label8.Location = new System.Drawing.Point(577, 16);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(43, 16);
			label8.TabIndex = 113;
			label8.Text = "Qty :";
			lblGrandTotal.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			lblGrandTotal.BackColor = System.Drawing.Color.DarkGray;
			lblGrandTotal.Font = new System.Drawing.Font("MS Reference Sans Serif", 21.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblGrandTotal.Format = "N2";
			lblGrandTotal.Location = new System.Drawing.Point(761, 111);
			lblGrandTotal.Name = "lblGrandTotal";
			lblGrandTotal.Size = new System.Drawing.Size(210, 40);
			lblGrandTotal.TabIndex = 112;
			lblGrandTotal.Text = "0.00";
			lblGrandTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			lblGrandTotal.Value = new decimal(new int[4] { 0, 0, 0, 131072 });
			btnSeperator2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnSeperator2.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnSeperator2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSeperator2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnSeperator2.Location = new System.Drawing.Point(704, 9);
			btnSeperator2.Name = "btnSeperator2";
			btnSeperator2.Size = new System.Drawing.Size(2, 137);
			btnSeperator2.TabIndex = 111;
			btnSeperator2.UseVisualStyleBackColor = true;
			pnlSide.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			pnlSide.BackColor = System.Drawing.SystemColors.Control;
			pnlSide.Controls.Add(button3);
			pnlSide.Controls.Add(button2);
			pnlSide.Controls.Add(button1);
			pnlSide.Controls.Add(button64);
			pnlSide.Controls.Add(btnRemoveRow);
			pnlSide.Controls.Add(btnMinus);
			pnlSide.Controls.Add(btnPlus);
			pnlSide.Location = new System.Drawing.Point(921, 3);
			pnlSide.Name = "pnlSide";
			pnlSide.Size = new System.Drawing.Size(50, 511);
			pnlSide.TabIndex = 2;
			button3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			button3.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			button3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			button3.Location = new System.Drawing.Point(3, 257);
			button3.Name = "button3";
			button3.Size = new System.Drawing.Size(45, 2);
			button3.TabIndex = 131;
			button3.UseVisualStyleBackColor = true;
			button2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			button2.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			button2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			button2.Location = new System.Drawing.Point(3, 195);
			button2.Name = "button2";
			button2.Size = new System.Drawing.Size(45, 2);
			button2.TabIndex = 130;
			button2.UseVisualStyleBackColor = true;
			button1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			button1.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			button1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			button1.Location = new System.Drawing.Point(3, 128);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(45, 2);
			button1.TabIndex = 129;
			button1.UseVisualStyleBackColor = true;
			button64.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			button64.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			button64.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			button64.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			button64.Location = new System.Drawing.Point(3, 65);
			button64.Name = "button64";
			button64.Size = new System.Drawing.Size(45, 2);
			button64.TabIndex = 128;
			button64.UseVisualStyleBackColor = true;
			btnRemoveRow.BackColor = System.Drawing.Color.White;
			btnRemoveRow.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnRemoveRow.FlatAppearance.BorderSize = 0;
			btnRemoveRow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnRemoveRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 24f, System.Drawing.FontStyle.Bold);
			btnRemoveRow.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnRemoveRow.Image = (System.Drawing.Image)resources.GetObject("btnRemoveRow.Image");
			btnRemoveRow.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnRemoveRow.Location = new System.Drawing.Point(3, 199);
			btnRemoveRow.Name = "btnRemoveRow";
			btnRemoveRow.Size = new System.Drawing.Size(45, 60);
			btnRemoveRow.TabIndex = 25;
			btnRemoveRow.UseVisualStyleBackColor = false;
			btnRemoveRow.Click += new System.EventHandler(btnRemoveRow_Click);
			btnMinus.BackColor = System.Drawing.Color.White;
			btnMinus.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnMinus.FlatAppearance.BorderSize = 0;
			btnMinus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 24f, System.Drawing.FontStyle.Bold);
			btnMinus.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnMinus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnMinus.Location = new System.Drawing.Point(3, 131);
			btnMinus.Name = "btnMinus";
			btnMinus.Size = new System.Drawing.Size(45, 60);
			btnMinus.TabIndex = 24;
			btnMinus.Text = "-";
			btnMinus.UseVisualStyleBackColor = false;
			btnMinus.Click += new System.EventHandler(btnMinus_Click);
			btnPlus.BackColor = System.Drawing.Color.White;
			btnPlus.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnPlus.FlatAppearance.BorderSize = 0;
			btnPlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 24f, System.Drawing.FontStyle.Bold);
			btnPlus.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnPlus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnPlus.Location = new System.Drawing.Point(3, 65);
			btnPlus.Name = "btnPlus";
			btnPlus.Size = new System.Drawing.Size(45, 60);
			btnPlus.TabIndex = 23;
			btnPlus.Text = "+";
			btnPlus.UseVisualStyleBackColor = false;
			btnPlus.Click += new System.EventHandler(btnPlus_Click);
			dgItems.AllowUserToAddRows = false;
			dgItems.AllowUserToDeleteRows = false;
			dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
			dataGridViewCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dgItems.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle;
			dgItems.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			dgItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			dgItems.BackgroundColor = System.Drawing.Color.WhiteSmoke;
			dgItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
			dgItems.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			dgItems.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dgItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			dgItems.ColumnHeadersHeight = 40;
			dgItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			dgItems.Columns.AddRange(col_SL, col_ProductCode, col_ProductName, col_Qty, col_Unit, col_FreeQty, col_Rate, col_Gross, col_Discount, col_DisAmount, col_Taxable, col_Tax, col_TaxAmount, col_NetAmount);
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(61, 77, 125);
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dgItems.DefaultCellStyle = dataGridViewCellStyle3;
			dgItems.GridColor = System.Drawing.SystemColors.Control;
			dgItems.Location = new System.Drawing.Point(0, 0);
			dgItems.Name = "dgItems";
			dgItems.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dgItems.RowHeadersVisible = false;
			dgItems.RowHeadersWidth = 40;
			dgItems.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dgItems.RowTemplate.Height = 35;
			dgItems.Size = new System.Drawing.Size(918, 514);
			dgItems.TabIndex = 1;
			dgItems.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgItems_CellContentClick);
			dgItems.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgItems_CellEndEdit);
			dgItems.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(dgItems_DataError);
			dgItems.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dgItems_EditingControlShowing);
			dgItems.KeyDown += new System.Windows.Forms.KeyEventHandler(dgItems_KeyDown);
			dgItems.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(dgItems_CellContentClick);
			col_SL.DataPropertyName = "SL";
			col_SL.FillWeight = 30.18542f;
			col_SL.HeaderText = "#";
			col_SL.MinimumWidth = 8;
			col_SL.Name = "col_SL";
			col_SL.ReadOnly = true;
			col_ProductCode.DataPropertyName = "ProductCode";
			col_ProductCode.HeaderText = "P.Code";
			col_ProductCode.MinimumWidth = 8;
			col_ProductCode.Name = "col_ProductCode";
			col_ProductCode.ReadOnly = true;
			col_ProductCode.Visible = false;
			col_ProductName.DataPropertyName = "ProductName";
			col_ProductName.FillWeight = 174.3004f;
			col_ProductName.HeaderText = "Name";
			col_ProductName.MinimumWidth = 8;
			col_ProductName.Name = "col_ProductName";
			col_ProductName.ReadOnly = true;
			col_Qty.DataPropertyName = "Qty";
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			col_Qty.DefaultCellStyle = dataGridViewCellStyle4;
			col_Qty.FillWeight = 71.59125f;
			col_Qty.HeaderText = "Qty";
			col_Qty.MinimumWidth = 8;
			col_Qty.Name = "col_Qty";
			col_Unit.DataPropertyName = "UnitName";
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			col_Unit.DefaultCellStyle = dataGridViewCellStyle5;
			col_Unit.HeaderText = "Unit";
			col_Unit.MinimumWidth = 8;
			col_Unit.Name = "col_Unit";
			col_Unit.ReadOnly = true;
			col_FreeQty.DataPropertyName = "FreeQty";
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			col_FreeQty.DefaultCellStyle = dataGridViewCellStyle6;
			col_FreeQty.HeaderText = "F.Qty";
			col_FreeQty.MinimumWidth = 8;
			col_FreeQty.Name = "col_FreeQty";
			col_FreeQty.Visible = false;
			col_Rate.DataPropertyName = "Rate";
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_Rate.DefaultCellStyle = dataGridViewCellStyle7;
			col_Rate.FillWeight = 97.0194f;
			col_Rate.HeaderText = "Rate";
			col_Rate.MinimumWidth = 8;
			col_Rate.Name = "col_Rate";
			col_Gross.DataPropertyName = "Gross";
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_Gross.DefaultCellStyle = dataGridViewCellStyle8;
			col_Gross.HeaderText = "Gross";
			col_Gross.MinimumWidth = 8;
			col_Gross.Name = "col_Gross";
			col_Gross.ReadOnly = true;
			col_Gross.Visible = false;
			col_Discount.DataPropertyName = "Discount";
			dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			col_Discount.DefaultCellStyle = dataGridViewCellStyle9;
			col_Discount.HeaderText = "Discount";
			col_Discount.MinimumWidth = 8;
			col_Discount.Name = "col_Discount";
			col_Discount.ReadOnly = true;
			col_DisAmount.DataPropertyName = "DisAmount";
			dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_DisAmount.DefaultCellStyle = dataGridViewCellStyle10;
			col_DisAmount.HeaderText = "Dis Amount";
			col_DisAmount.MinimumWidth = 8;
			col_DisAmount.Name = "col_DisAmount";
			col_DisAmount.Visible = false;
			col_Taxable.DataPropertyName = "Taxable";
			dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_Taxable.DefaultCellStyle = dataGridViewCellStyle11;
			col_Taxable.HeaderText = "Taxable Amount";
			col_Taxable.MinimumWidth = 8;
			col_Taxable.Name = "col_Taxable";
			col_Taxable.Visible = false;
			col_Tax.DataPropertyName = "Tax";
			dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			col_Tax.DefaultCellStyle = dataGridViewCellStyle12;
			col_Tax.HeaderText = "Tax";
			col_Tax.MinimumWidth = 8;
			col_Tax.Name = "col_Tax";
			col_TaxAmount.DataPropertyName = "TaxAmount";
			dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_TaxAmount.DefaultCellStyle = dataGridViewCellStyle13;
			col_TaxAmount.HeaderText = "Tax Amount";
			col_TaxAmount.MinimumWidth = 8;
			col_TaxAmount.Name = "col_TaxAmount";
			col_TaxAmount.Visible = false;
			col_NetAmount.DataPropertyName = "NetAmount";
			dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_NetAmount.DefaultCellStyle = dataGridViewCellStyle14;
			col_NetAmount.FillWeight = 126.9036f;
			col_NetAmount.HeaderText = "Amount";
			col_NetAmount.MinimumWidth = 8;
			col_NetAmount.Name = "col_NetAmount";
			col_NetAmount.ReadOnly = true;
			panel6.Controls.Add(panel8);
			panel6.Controls.Add(panel7);
			panel6.Controls.Add(panel4);
			panel6.Dock = System.Windows.Forms.DockStyle.Right;
			panel6.Location = new System.Drawing.Point(974, 87);
			panel6.Name = "panel6";
			panel6.Size = new System.Drawing.Size(412, 668);
			panel6.TabIndex = 1;
			panel8.Controls.Add(btnTouch);
			panel8.Controls.Add(btnKeyBoard);
			panel8.Controls.Add(btnNew);
			panel8.Controls.Add(numKeyBoardPOS1);
			panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
			panel8.Location = new System.Drawing.Point(0, 319);
			panel8.Name = "panel8";
			panel8.Size = new System.Drawing.Size(412, 259);
			panel8.TabIndex = 146;
			btnTouch.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			btnTouch.BackColor = System.Drawing.Color.LightGray;
			btnTouch.BackgroundImage = (System.Drawing.Image)resources.GetObject("btnTouch.BackgroundImage");
			btnTouch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			btnTouch.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnTouch.FlatAppearance.BorderSize = 0;
			btnTouch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnTouch.Font = new System.Drawing.Font("Microsoft Sans Serif", 13f, System.Drawing.FontStyle.Bold);
			btnTouch.ForeColor = System.Drawing.Color.FromArgb(254, 245, 188);
			btnTouch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnTouch.Location = new System.Drawing.Point(4, 176);
			btnTouch.Name = "btnTouch";
			btnTouch.Size = new System.Drawing.Size(93, 80);
			btnTouch.TabIndex = 140;
			btnTouch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			btnTouch.UseVisualStyleBackColor = false;
			btnTouch.Click += new System.EventHandler(btnTouch_Click);
			btnKeyBoard.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			btnKeyBoard.BackColor = System.Drawing.Color.LightGray;
			btnKeyBoard.BackgroundImage = (System.Drawing.Image)resources.GetObject("btnKeyBoard.BackgroundImage");
			btnKeyBoard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			btnKeyBoard.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnKeyBoard.FlatAppearance.BorderSize = 0;
			btnKeyBoard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnKeyBoard.Font = new System.Drawing.Font("Microsoft Sans Serif", 13f, System.Drawing.FontStyle.Bold);
			btnKeyBoard.ForeColor = System.Drawing.Color.FromArgb(254, 245, 188);
			btnKeyBoard.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			btnKeyBoard.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnKeyBoard.Location = new System.Drawing.Point(4, 90);
			btnKeyBoard.Name = "btnKeyBoard";
			btnKeyBoard.Size = new System.Drawing.Size(93, 80);
			btnKeyBoard.TabIndex = 139;
			btnKeyBoard.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			btnKeyBoard.UseVisualStyleBackColor = false;
			btnKeyBoard.Click += new System.EventHandler(btnKeyBoard_Click);
			btnNew.BackColor = System.Drawing.Color.DimGray;
			btnNew.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnNew.FlatAppearance.BorderSize = 0;
			btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 13f, System.Drawing.FontStyle.Bold);
			btnNew.ForeColor = System.Drawing.Color.FromArgb(254, 245, 188);
			btnNew.Image = (System.Drawing.Image)resources.GetObject("btnNew.Image");
			btnNew.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			btnNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnNew.Location = new System.Drawing.Point(4, 4);
			btnNew.Name = "btnNew";
			btnNew.Size = new System.Drawing.Size(93, 80);
			btnNew.TabIndex = 141;
			btnNew.Text = "New";
			btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			btnNew.UseVisualStyleBackColor = false;
			btnNew.Click += new System.EventHandler(btnNew_Click);
			numKeyBoardPOS1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			numKeyBoardPOS1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f);
			numKeyBoardPOS1.Location = new System.Drawing.Point(101, 7);
			numKeyBoardPOS1.Margin = new System.Windows.Forms.Padding(4);
			numKeyBoardPOS1.Name = "numKeyBoardPOS1";
			numKeyBoardPOS1.Size = new System.Drawing.Size(308, 248);
			numKeyBoardPOS1.TabIndex = 0;
			numKeyBoardPOS1.atQtyClick += new AshUsercontrol.QtyClickEventHandler(numKeyBoardPOS1_atQtyClick);
			numKeyBoardPOS1.atRateClick += new AshUsercontrol.RateClickEventHandler(numKeyBoardPOS1_atRateClick);
			numKeyBoardPOS1.atEnterClick += new AshUsercontrol.NumberClickEventHandler(numKeyBoardPOS1_atEnterClick);
			panel7.Controls.Add(btnCustomer);
			panel7.Controls.Add(btnSalesAccount);
			panel7.Controls.Add(btnSalesType);
			panel7.Controls.Add(btnSalesman);
			panel7.Controls.Add(btnDepot);
			panel7.Controls.Add(btnHoldBill);
			panel7.Controls.Add(btnRetrieveBill);
			panel7.Controls.Add(btnPCategory);
			panel7.Controls.Add(btnSearchBill);
			panel7.Controls.Add(btnPayment);
			panel7.Dock = System.Windows.Forms.DockStyle.Top;
			panel7.Location = new System.Drawing.Point(0, 0);
			panel7.Name = "panel7";
			panel7.Size = new System.Drawing.Size(412, 230);
			panel7.TabIndex = 145;
			btnSalesAccount.BackColor = System.Drawing.Color.FromArgb(17, 55, 81);
			btnSalesAccount.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			btnSalesAccount.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnSalesAccount.FlatAppearance.BorderSize = 0;
			btnSalesAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSalesAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold);
			btnSalesAccount.ForeColor = System.Drawing.Color.White;
			btnSalesAccount.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			btnSalesAccount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnSalesAccount.Location = new System.Drawing.Point(215, 3);
			btnSalesAccount.Name = "btnSalesAccount";
			btnSalesAccount.Size = new System.Drawing.Size(193, 41);
			btnSalesAccount.TabIndex = 137;
			btnSalesAccount.Text = "Sales Account";
			btnSalesAccount.UseVisualStyleBackColor = false;
			btnSalesAccount.Click += new System.EventHandler(btnSalesAccount_Click);
			btnSalesType.BackColor = System.Drawing.Color.FromArgb(17, 55, 81);
			btnSalesType.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnSalesType.FlatAppearance.BorderSize = 0;
			btnSalesType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSalesType.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold);
			btnSalesType.ForeColor = System.Drawing.Color.White;
			btnSalesType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnSalesType.Location = new System.Drawing.Point(11, 47);
			btnSalesType.Name = "btnSalesType";
			btnSalesType.Size = new System.Drawing.Size(193, 41);
			btnSalesType.TabIndex = 137;
			btnSalesType.Text = "Sales Type";
			btnSalesType.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			btnSalesType.UseVisualStyleBackColor = false;
			btnSalesType.Click += new System.EventHandler(btnSalesType_Click);
			btnSalesman.BackColor = System.Drawing.Color.FromArgb(17, 55, 81);
			btnSalesman.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnSalesman.FlatAppearance.BorderSize = 0;
			btnSalesman.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSalesman.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold);
			btnSalesman.ForeColor = System.Drawing.Color.White;
			btnSalesman.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnSalesman.Location = new System.Drawing.Point(215, 48);
			btnSalesman.Name = "btnSalesman";
			btnSalesman.Size = new System.Drawing.Size(193, 41);
			btnSalesman.TabIndex = 137;
			btnSalesman.Text = "Salesman";
			btnSalesman.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			btnSalesman.UseVisualStyleBackColor = false;
			btnSalesman.Click += new System.EventHandler(btnSalesman_Click);
			btnDepot.BackColor = System.Drawing.Color.FromArgb(17, 55, 81);
			btnDepot.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnDepot.FlatAppearance.BorderSize = 0;
			btnDepot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnDepot.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold);
			btnDepot.ForeColor = System.Drawing.Color.White;
			btnDepot.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnDepot.Location = new System.Drawing.Point(11, 94);
			btnDepot.Name = "btnDepot";
			btnDepot.Size = new System.Drawing.Size(193, 41);
			btnDepot.TabIndex = 137;
			btnDepot.Text = "Depot/Godown";
			btnDepot.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			btnDepot.UseVisualStyleBackColor = false;
			btnDepot.Click += new System.EventHandler(btnDepot_Click);
			btnHoldBill.BackColor = System.Drawing.Color.FromArgb(17, 55, 81);
			btnHoldBill.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnHoldBill.FlatAppearance.BorderSize = 0;
			btnHoldBill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnHoldBill.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold);
			btnHoldBill.ForeColor = System.Drawing.Color.White;
			btnHoldBill.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnHoldBill.Location = new System.Drawing.Point(11, 183);
			btnHoldBill.Name = "btnHoldBill";
			btnHoldBill.Size = new System.Drawing.Size(193, 41);
			btnHoldBill.TabIndex = 137;
			btnHoldBill.Text = "Hold Bill";
			btnHoldBill.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			btnHoldBill.UseVisualStyleBackColor = false;
			btnHoldBill.Click += new System.EventHandler(btnHoldBill_Click);
			btnRetrieveBill.BackColor = System.Drawing.Color.FromArgb(17, 55, 81);
			btnRetrieveBill.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnRetrieveBill.FlatAppearance.BorderSize = 0;
			btnRetrieveBill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnRetrieveBill.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold);
			btnRetrieveBill.ForeColor = System.Drawing.Color.White;
			btnRetrieveBill.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnRetrieveBill.Location = new System.Drawing.Point(215, 183);
			btnRetrieveBill.Name = "btnRetrieveBill";
			btnRetrieveBill.Size = new System.Drawing.Size(193, 41);
			btnRetrieveBill.TabIndex = 137;
			btnRetrieveBill.Text = "Retrieve";
			btnRetrieveBill.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			btnRetrieveBill.UseVisualStyleBackColor = false;
			btnRetrieveBill.Click += new System.EventHandler(btnRetrieveBill_Click);
			btnPCategory.BackColor = System.Drawing.Color.FromArgb(17, 55, 81);
			btnPCategory.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnPCategory.FlatAppearance.BorderSize = 0;
			btnPCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnPCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold);
			btnPCategory.ForeColor = System.Drawing.Color.White;
			btnPCategory.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnPCategory.Location = new System.Drawing.Point(215, 93);
			btnPCategory.Name = "btnPCategory";
			btnPCategory.Size = new System.Drawing.Size(193, 41);
			btnPCategory.TabIndex = 137;
			btnPCategory.Text = "Price Category";
			btnPCategory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			btnPCategory.UseVisualStyleBackColor = false;
			btnPCategory.Click += new System.EventHandler(btnPCategory_Click);
			btnSearchBill.BackColor = System.Drawing.Color.FromArgb(17, 55, 81);
			btnSearchBill.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnSearchBill.FlatAppearance.BorderSize = 0;
			btnSearchBill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSearchBill.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold);
			btnSearchBill.ForeColor = System.Drawing.Color.White;
			btnSearchBill.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnSearchBill.Location = new System.Drawing.Point(215, 138);
			btnSearchBill.Name = "btnSearchBill";
			btnSearchBill.Size = new System.Drawing.Size(193, 41);
			btnSearchBill.TabIndex = 137;
			btnSearchBill.Text = "Search Bill";
			btnSearchBill.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			btnSearchBill.UseVisualStyleBackColor = false;
			btnSearchBill.Click += new System.EventHandler(btnSearchBill_Click);
			btnPayment.BackColor = System.Drawing.Color.FromArgb(17, 55, 81);
			btnPayment.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnPayment.FlatAppearance.BorderSize = 0;
			btnPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnPayment.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold);
			btnPayment.ForeColor = System.Drawing.Color.White;
			btnPayment.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnPayment.Location = new System.Drawing.Point(11, 139);
			btnPayment.Name = "btnPayment";
			btnPayment.Size = new System.Drawing.Size(193, 41);
			btnPayment.TabIndex = 137;
			btnPayment.Text = "Payment";
			btnPayment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			btnPayment.UseVisualStyleBackColor = false;
			btnPayment.Click += new System.EventHandler(btnPayment_Click);
			panel4.Controls.Add(btnSave);
			panel4.Controls.Add(btnDelete);
			panel4.Controls.Add(btnPrint);
			panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
			panel4.Location = new System.Drawing.Point(0, 578);
			panel4.Name = "panel4";
			panel4.Size = new System.Drawing.Size(412, 90);
			panel4.TabIndex = 144;
			btnSave.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			btnSave.BackColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnSave.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnSave.FlatAppearance.BorderSize = 0;
			btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 13f);
			btnSave.ForeColor = System.Drawing.Color.White;
			btnSave.Image = (System.Drawing.Image)resources.GetObject("btnSave.Image");
			btnSave.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnSave.Location = new System.Drawing.Point(0, 7);
			btnSave.Name = "btnSave";
			btnSave.Size = new System.Drawing.Size(175, 80);
			btnSave.TabIndex = 138;
			btnSave.Text = "Save";
			btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			btnSave.UseVisualStyleBackColor = false;
			btnSave.Click += new System.EventHandler(btnSave_Click);
			btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnDelete.BackColor = System.Drawing.Color.DimGray;
			btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnDelete.FlatAppearance.BorderSize = 0;
			btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 13f);
			btnDelete.ForeColor = System.Drawing.Color.FromArgb(254, 245, 188);
			btnDelete.Image = (System.Drawing.Image)resources.GetObject("btnDelete.Image");
			btnDelete.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			btnDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnDelete.Location = new System.Drawing.Point(306, 7);
			btnDelete.Name = "btnDelete";
			btnDelete.Size = new System.Drawing.Size(101, 80);
			btnDelete.TabIndex = 143;
			btnDelete.Text = "Delete Bill";
			btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			btnDelete.UseVisualStyleBackColor = false;
			btnDelete.Click += new System.EventHandler(btnDelete_Click);
			btnPrint.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnPrint.BackColor = System.Drawing.Color.DimGray;
			btnPrint.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnPrint.FlatAppearance.BorderSize = 0;
			btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 13f);
			btnPrint.ForeColor = System.Drawing.Color.FromArgb(254, 245, 188);
			btnPrint.Image = (System.Drawing.Image)resources.GetObject("btnPrint.Image");
			btnPrint.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			btnPrint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnPrint.Location = new System.Drawing.Point(181, 7);
			btnPrint.Name = "btnPrint";
			btnPrint.Size = new System.Drawing.Size(122, 80);
			btnPrint.TabIndex = 142;
			btnPrint.Text = "Print";
			btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			btnPrint.UseVisualStyleBackColor = false;
			btnPrint.Click += new System.EventHandler(btnPrint_Click);
			panel3.BackColor = System.Drawing.Color.Maroon;
			panel3.Controls.Add(lblSalesAccount);
			panel3.Controls.Add(lblPriceCategory);
			panel3.Controls.Add(lblParty);
			panel3.Controls.Add(label7);
			panel3.Controls.Add(lblSalesman);
			panel3.Controls.Add(label6);
			panel3.Controls.Add(label4);
			panel3.Controls.Add(lblDepot);
			panel3.Controls.Add(label1);
			panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
			panel3.Location = new System.Drawing.Point(0, 755);
			panel3.Name = "panel3";
			panel3.Size = new System.Drawing.Size(1386, 33);
			panel3.TabIndex = 0;
			lblSalesAccount.AutoSize = true;
			lblSalesAccount.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblSalesAccount.Location = new System.Drawing.Point(225, 9);
			lblSalesAccount.Name = "lblSalesAccount";
			lblSalesAccount.Size = new System.Drawing.Size(110, 16);
			lblSalesAccount.TabIndex = 0;
			lblSalesAccount.Text = "Sales Account";
			lblPriceCategory.AutoSize = true;
			lblPriceCategory.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblPriceCategory.Location = new System.Drawing.Point(1170, 8);
			lblPriceCategory.Name = "lblPriceCategory";
			lblPriceCategory.Size = new System.Drawing.Size(60, 16);
			lblPriceCategory.TabIndex = 0;
			lblPriceCategory.Text = "Normal";
			lblParty.AutoSize = true;
			lblParty.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblParty.Location = new System.Drawing.Point(775, 9);
			lblParty.Name = "lblParty";
			lblParty.Size = new System.Drawing.Size(107, 16);
			lblParty.TabIndex = 0;
			lblParty.Text = "Cash Account";
			label7.AutoSize = true;
			label7.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label7.Location = new System.Drawing.Point(1027, 9);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(124, 16);
			label7.TabIndex = 0;
			label7.Text = "Price Category :";
			lblSalesman.AutoSize = true;
			lblSalesman.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblSalesman.Location = new System.Drawing.Point(485, 10);
			lblSalesman.Name = "lblSalesman";
			lblSalesman.Size = new System.Drawing.Size(54, 16);
			lblSalesman.TabIndex = 0;
			lblSalesman.Text = "Admin";
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label6.Location = new System.Drawing.Point(668, 10);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(101, 16);
			label6.TabIndex = 0;
			label6.Text = "Cash/Party :";
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label4.Location = new System.Drawing.Point(391, 9);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(88, 16);
			label4.TabIndex = 0;
			label4.Text = "Salesman :";
			lblDepot.AutoSize = true;
			lblDepot.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblDepot.Location = new System.Drawing.Point(77, 9);
			lblDepot.Name = "lblDepot";
			lblDepot.Size = new System.Drawing.Size(65, 16);
			lblDepot.TabIndex = 0;
			lblDepot.Text = "Primary";
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(10, 8);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(60, 16);
			label1.TabIndex = 0;
			label1.Text = "Depot :";
			tmrGrandTotal.Interval = 500;
			tmrGrandTotal.Tick += new System.EventHandler(tmrGrandTotal_Tick);
			tmrDateTime.Tick += new System.EventHandler(tmrDateTime_Tick);
			pnlMain.Controls.Add(dgItems);
			pnlMain.Controls.Add(panel5);
			pnlMain.Controls.Add(pnlSide);
			pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			pnlMain.Location = new System.Drawing.Point(0, 87);
			pnlMain.Name = "pnlMain";
			pnlMain.Size = new System.Drawing.Size(974, 668);
			pnlMain.TabIndex = 4;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.ClientSize = new System.Drawing.Size(1386, 788);
			base.Controls.Add(pnlMain);
			base.Controls.Add(panel6);
			base.Controls.Add(panel3);
			base.Controls.Add(panel2);
			base.Controls.Add(pnlTitle);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "POSView";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "POSView";
			base.Load += new System.EventHandler(POSView_Load);
			pnlTitle.ResumeLayout(false);
			pnlTitle.PerformLayout();
			((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)nudInvoiceNo).EndInit();
			panel5.ResumeLayout(false);
			panel5.PerformLayout();
			pnlSide.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dgItems).EndInit();
			panel6.ResumeLayout(false);
			panel8.ResumeLayout(false);
			panel7.ResumeLayout(false);
			panel4.ResumeLayout(false);
			panel3.ResumeLayout(false);
			panel3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)bdsItems).EndInit();
			pnlMain.ResumeLayout(false);
			ResumeLayout(false);
		}
	}
}
