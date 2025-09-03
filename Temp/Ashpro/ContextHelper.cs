using System;
using System.Collections.Generic;
using System.Data;
using AshFramework;

namespace Ashpro
{
	public class ContextHelper
	{
		public static List<AccountEntity> GetAccounts(string Query, object entity, string sCon = null)
		{
			List<AccountEntity> list = new List<AccountEntity>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				AccountEntity item = new AccountEntity
				{
					AccountId = row["AccountId"].ToString().ToInt32(),
					AGroupId = row["AGroupId"].ToString().ToInt32(),
					RootId = row["RootId"].ToString().ToInt32(),
					PCategoryId = row["PCategoryId"].ToString().ToInt32(),
					AccountName = row["AccountName"].ToString(),
					OpeningBalance = row["OpeningBalance"].ToString().ToDecimal(),
					DebitOrCridt = row["DebitOrCridt"].ToString(),
					AddressOne = row["AddressOne"].ToString(),
					AddressTwo = row["AddressTwo"].ToString(),
					AddressThree = row["AddressThree"].ToString(),
					PhoneOne = row["PhoneOne"].ToString(),
					MobileNo = row["MobileNo"].ToString(),
					FaxNo = row["FaxNo"].ToString(),
					TinNo = row["TinNo"].ToString(),
					CstNo = row["CstNo"].ToString(),
					EMailOne = row["EMailOne"].ToString(),
					Remarks = row["Remarks"].ToString(),
					BankName = row["BankName"].ToString(),
					BankAddress = row["BankAddress"].ToString(),
					AccountNo = row["AccountNo"].ToString(),
					isActive = row["isActive"].ToString().ToBool(),
					IsBuiltIn = row["IsBuiltIn"].ToString().ToBool(),
					DebitLimit = row["DebitLimit"].ToString().ToDecimal(),
					IsLimitApplicable = row["IsLimitApplicable"].ToString().ToBool(),
					AddressFour = row["AddressFour"].ToString(),
					FieldOne = row["FieldOne"].ToString(),
					FieldTwo = row["FieldTwo"].ToString(),
					FieldThree = row["FieldThree"].ToString(),
					FieldFour = row["FieldFour"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<AccountGroupEntity> GetAccountGroups(string Query, object entity, string sCon = null)
		{
			List<AccountGroupEntity> list = new List<AccountGroupEntity>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				AccountGroupEntity item = new AccountGroupEntity
				{
					AGroupId = row["AGroupId"].ToString().ToInt32(),
					GroupName = row["GroupName"].ToString(),
					Under = row["Under"].ToString(),
					Remarks = row["Remarks"].ToString(),
					isBuilIn = row["isBuilIn"].ToString().ToBool()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<AttenMaster> GetAttenMasters(string Query, object entity, string sCon = null)
		{
			List<AttenMaster> list = new List<AttenMaster>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				AttenMaster item = new AttenMaster
				{
					AttendanceMasterId = row["AttendanceMasterId"].ToString().ToInt32(),
					AtteInvNo = row["AtteInvNo"].ToString().ToInt32(),
					AtteDate = row["AtteDate"].ToString().toDateTime(),
					Remarks = row["Remarks"].ToString(),
					FK_BranchId = row["FK_BranchId"].ToString().ToInt32()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<AttenSettings> GetAttenSettings(string Query, object entity, string sCon = null)
		{
			List<AttenSettings> list = new List<AttenSettings>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				AttenSettings item = new AttenSettings
				{
					AttenSettingsId = row["AttenSettingsId"].ToString().ToInt32(),
					AttendanceMasterId = row["AttendanceMasterId"].ToString().ToInt32(),
					EmpID = row["EmpID"].ToString().ToInt32(),
					Morning = row["Morning"].ToString().ToBool(),
					Evening = row["Evening"].ToString().ToBool(),
					AttenType = row["AttenType"].ToString().ToDecimal(),
					EmpName = row["EmpName"].ToString(),
					AtteDate = row["AtteDate"].ToString().toDateTime()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<BarcodeDTL> GetBarcodeDTLs(string Query, object entity, string sCon = null)
		{
			List<BarcodeDTL> list = new List<BarcodeDTL>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				BarcodeDTL item = new BarcodeDTL
				{
					id = row["id"].ToString().ToInt32(),
					Name = row["Name"].ToString(),
					Text = row["Text"].ToString(),
					Value = row["Value"].ToString(),
					Position_x = row["Position_x"].ToString().ToInt32(),
					Position_y = row["Position_y"].ToString().ToInt32(),
					Width = row["Width"].ToString().ToInt32(),
					Height = row["Height"].ToString().ToInt32(),
					Alignment = row["Alignment"].ToString(),
					RotationPoint = row["RotationPoint"].ToString(),
					fontFamily = row["fontFamily"].ToString(),
					fontSize = row["fontSize"].ToString().ToInt32(),
					fStyle = row["fStyle"].ToString(),
					Format = row["Format"].ToString(),
					bAlignment = row["bAlignment"].ToString(),
					FK_BarcodeHDR = row["FK_BarcodeHDR"].ToString().ToInt32(),
					Type = row["Type"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<BarcodeHDR> GetBarcodeHDRs(string Query, object entity, string sCon = null)
		{
			List<BarcodeHDR> list = new List<BarcodeHDR>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				BarcodeHDR item = new BarcodeHDR
				{
					Id = row["Id"].ToString().ToInt32(),
					name = row["name"].ToString(),
					FirstMargin = row["FirstMargin"].ToString().ToInt32(),
					TopMargin = row["TopMargin"].ToString().ToInt32(),
					RightMargin = row["RightMargin"].ToString().ToInt32(),
					BottomMargin = row["BottomMargin"].ToString().ToInt32(),
					Width = row["Width"].ToString().ToInt32(),
					Height = row["Height"].ToString().ToInt32(),
					Accross = row["Accross"].ToString().ToInt32(),
					Down = row["Down"].ToString().ToInt32(),
					TotalPerPage = row["TotalPerPage"].ToString().ToInt32(),
					PrinterName = row["PrinterName"].ToString(),
					PaperName = row["PaperName"].ToString(),
					PSWidth = row["PSWidth"].ToString().ToInt32(),
					PSHieght = row["PSHieght"].ToString().ToInt32(),
					isBorderIncluded = row["isBorderIncluded"].ToString().ToBool(),
					isCuttingEdge = row["isCuttingEdge"].ToString().ToBool(),
					BarcodeField = row["BarcodeField"].ToString().ToInt32(),
					isBarcode = row["isBarcode"].ToString().ToBool()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<BarcodeScale> GetBarcodeScales(string Query, object entity, string sCon = null)
		{
			List<BarcodeScale> list = new List<BarcodeScale>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				BarcodeScale item = new BarcodeScale
				{
					Id = row["Id"].ToString().ToInt32(),
					Name = row["Name"].ToString(),
					IPAddress = row["IPAddress"].ToString(),
					Remarks = row["Remarks"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<Batch> GetBatchs(string Query, object entity, string sCon = null)
		{
			List<Batch> list = new List<Batch>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				Batch item = new Batch
				{
					Id = row["Id"].ToString().ToInt32(),
					Name = row["Name"].ToString(),
					FK_PurInvId = row["FK_PurInvId"].ToString().ToInt32(),
					Remarks = row["Remarks"].ToString(),
					FK_BranchId = row["FK_BranchId"].ToString().ToInt32(),
					RefNo = row["RefNo"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<Branch> GetBranchs(string Query, object entity, string sCon = null)
		{
			List<Branch> list = new List<Branch>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				Branch item = new Branch
				{
					Id = row["Id"].ToString().ToInt32(),
					Name = row["Name"].ToString(),
					Description = row["Description"].ToString(),
					Address = row["Address"].ToString(),
					City = row["City"].ToString(),
					State = row["State"].ToString(),
					Country = row["Country"].ToString(),
					PostalCode = row["PostalCode"].ToString(),
					PhoneNumber = row["PhoneNumber"].ToString(),
					MobileNumber = row["MobileNumber"].ToString(),
					ContactPerson = row["ContactPerson"].ToString(),
					Status = row["Status"].ToString(),
					Active = row["Active"].ToString().ToBool()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<BrandEntity> GetBrands(string Query, object entity, string sCon = null)
		{
			List<BrandEntity> list = new List<BrandEntity>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				BrandEntity item = new BrandEntity
				{
					BrandId = row["BrandId"].ToString().ToInt32(),
					BrandName = row["BrandName"].ToString(),
					Description = row["Description"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<Company> GetInstitutes(string Query, object entity, string sCon = null)
		{
			List<Company> list = new List<Company>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				Company item = new Company
				{
					InstituteId = row["InstituteId"].ToString().ToInt32(),
					InstituteName = row["InstituteName"].ToString(),
					Place = row["Place"].ToString(),
					Address = row["Address"].ToString(),
					Country = row["Country"].ToString(),
					Pincod = row["Pincod"].ToString(),
					PhoneNo = row["PhoneNo"].ToString(),
					MobNo = row["MobNo"].ToString(),
					FaxNo = row["FaxNo"].ToString(),
					website = row["website"].ToString(),
					EmailId = row["EmailId"].ToString(),
					Sector = row["Sector"].ToString(),
					Logo = ((row["Logo"] == DBNull.Value) ? null : ((byte[])row["Logo"])),
					LogoPath = row["LogoPath"].ToString(),
					Productserial = row["Productserial"].ToString(),
					GSTNo = row["GSTNo"].ToString(),
					State = row["State"].ToString(),
					StateCode = row["StateCode"].ToString().ToInt32(),
					BankName = row["BankName"].ToString(),
					BankBranch = row["BankBranch"].ToString(),
					IFSC = row["IFSC"].ToString(),
					AccNo = row["AccNo"].ToString(),
					LocalName = row["LocalName"].ToString(),
					CompanyId = row["CompanyId"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<ChequeDetails> GetChequeDetailss(string Query, object entity, string sCon = null)
		{
			List<ChequeDetails> list = new List<ChequeDetails>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				ChequeDetails item = new ChequeDetails
				{
					CheqDtlsId = row["CheqDtlsId"].ToString().ToInt32(),
					VchrId = row["VchrId"].ToString().ToInt32(),
					VchrType = row["VchrType"].ToString(),
					AccountId = row["AccountId"].ToString().ToInt32(),
					BankAcctId = row["BankAcctId"].ToString().ToInt32(),
					Amount = row["Amount"].ToString().ToDecimal(),
					Bank = row["Bank"].ToString(),
					Branch = row["Branch"].ToString(),
					CheqNo = row["CheqNo"].ToString(),
					CheqDate = row["CheqDate"].ToString(),
					Status = row["Status"].ToString(),
					RefNo = row["RefNo"].ToString(),
					Remarks = row["Remarks"].ToString(),
					TranDate = row["TranDate"].ToString().toDateTime(),
					JeId = row["JeId"].ToString().ToInt32(),
					Type = row["Type"].ToString(),
					CollectedDate = row["CollectedDate"].ToString().toDateTime(),
					Date = row["Date"].ToString().toDateTime(),
					ChqRtnAmount = row["ChqRtnAmount"].ToString().ToDecimal(),
					FK_BranchId = row["FK_BranchId"].ToString().ToInt32()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<CostCenter> GetCostCenters(string Query, object entity, string sCon = null)
		{
			List<CostCenter> list = new List<CostCenter>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				CostCenter item = new CostCenter
				{
					CostCenterID = row["CostCenterID"].ToString().ToInt32(),
					CostCenterName = row["CostCenterName"].ToString(),
					Description = row["Description"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<CulenderPlanning> GetCulenderPlannings(string Query, object entity, string sCon = null)
		{
			List<CulenderPlanning> list = new List<CulenderPlanning>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				CulenderPlanning item = new CulenderPlanning
				{
					CPlaningId = row["CPlaningId"].ToString().ToInt32(),
					CplanninDate = row["CplanninDate"].ToString().toDateTime(),
					Description = ((row["Description"] == DBNull.Value) ? null : ((byte[])row["Description"])),
					FK_BranchId = row["FK_BranchId"].ToString().ToInt32()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<CurrencyInfo> GetCurrencyInfos(string Query, object entity, string sCon = null)
		{
			List<CurrencyInfo> list = new List<CurrencyInfo>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				CurrencyInfo item = new CurrencyInfo
				{
					CurrencyID = row["CurrencyID"].ToString().ToInt32(),
					CurrencyCode = row["CurrencyCode"].ToString(),
					IsCurrencyNameFeminine = row["IsCurrencyNameFeminine"].ToString().ToBool(),
					EnglishCurrencyName = row["EnglishCurrencyName"].ToString(),
					EnglishPluralCurrencyName = row["EnglishPluralCurrencyName"].ToString(),
					EnglishCurrencyPartName = row["EnglishCurrencyPartName"].ToString(),
					EnglishPluralCurrencyPartName = row["EnglishPluralCurrencyPartName"].ToString(),
					Arabic1CurrencyName = row["Arabic1CurrencyName"].ToString(),
					Arabic2CurrencyName = row["Arabic2CurrencyName"].ToString(),
					Arabic310CurrencyName = row["Arabic310CurrencyName"].ToString(),
					Arabic1199CurrencyName = row["Arabic1199CurrencyName"].ToString(),
					Arabic1CurrencyPartName = row["Arabic1CurrencyPartName"].ToString(),
					Arabic2CurrencyPartName = row["Arabic2CurrencyPartName"].ToString(),
					Arabic310CurrencyPartName = row["Arabic310CurrencyPartName"].ToString(),
					Arabic1199CurrencyPartName = row["Arabic1199CurrencyPartName"].ToString(),
					PartPrecision = row["PartPrecision"].ToString().ToInt32(),
					IsCurrencyPartNameFeminine = row["IsCurrencyPartNameFeminine"].ToString().ToBool()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<CustomQuery> GetCustomQuerys(string Query, object entity, string sCon = null)
		{
			List<CustomQuery> list = new List<CustomQuery>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				CustomQuery item = new CustomQuery
				{
					id = row["id"].ToString().ToInt32(),
					CustmQryName = row["CustmQryName"].ToString(),
					Query = row["Query"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<DebitCreditCardMasterEntity> GetDebitCreditCardMasters(string Query, object entity, string sCon = null)
		{
			List<DebitCreditCardMasterEntity> list = new List<DebitCreditCardMasterEntity>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				DebitCreditCardMasterEntity item = new DebitCreditCardMasterEntity
				{
					CardId = row["CardId"].ToString().ToInt32(),
					CardName = row["CardName"].ToString(),
					BankComm = row["BankComm"].ToString().ToDecimal(),
					CardComm = row["CardComm"].ToString().ToDecimal(),
					Remarks = row["Remarks"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<EmployeeDetailsEntity> GetEmployeeDetailss(string Query, object entity, string sCon = null)
		{
			List<EmployeeDetailsEntity> list = new List<EmployeeDetailsEntity>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				EmployeeDetailsEntity item = new EmployeeDetailsEntity
				{
					EmpID = row["EmpID"].ToString().ToInt32(),
					EmployeeID = row["EmployeeID"].ToString().ToInt32(),
					EmpName = row["EmpName"].ToString(),
					AddressOne = row["AddressOne"].ToString(),
					AddressTwo = row["AddressTwo"].ToString(),
					AddressThree = row["AddressThree"].ToString(),
					PhNumber = row["PhNumber"].ToString(),
					EmailId = row["EmailId"].ToString(),
					Designation = row["Designation"].ToString(),
					Salary = row["Salary"].ToString().ToDecimal(),
					CasualLeave = row["CasualLeave"].ToString().ToDecimal(),
					Gender = row["Gender"].ToString(),
					Qualification = row["Qualification"].ToString(),
					ParentName = row["ParentName"].ToString(),
					Relation = row["Relation"].ToString(),
					ParentAddress = row["ParentAddress"].ToString(),
					ParentPhNo = row["ParentPhNo"].ToString(),
					Remarks = row["Remarks"].ToString(),
					isActive = row["isActive"].ToString().ToBool(),
					AdharNo = row["AdharNo"].ToString(),
					SalaryAccntId = row["SalaryAccntId"].ToString().ToInt32(),
					SalaryAdvAccntId = row["SalaryAdvAccntId"].ToString().ToInt32(),
					SalaryPayableId = row["SalaryPayableId"].ToString().ToInt32(),
					FK_BranchId = row["FK_BranchId"].ToString().ToInt32()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<FinancialYear> GetFinancialYears(string Query, object entity, string sCon = null)
		{
			List<FinancialYear> list = new List<FinancialYear>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				FinancialYear item = new FinancialYear
				{
					FyearId = row["FyearId"].ToString().ToInt32(),
					FyearName = row["FyearName"].ToString(),
					DateFrom = row["DateFrom"].ToString(),
					DateTo = row["DateTo"].ToString(),
					Old = row["Old"].ToString().ToBool(),
					Closed = row["Closed"].ToString().ToBool()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<GdwnStckTranLog> GetGdwnStckTranLogs(string Query, object entity, string sCon = null)
		{
			List<GdwnStckTranLog> list = new List<GdwnStckTranLog>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				GdwnStckTranLog item = new GdwnStckTranLog
				{
					GSTranId = row["GSTranId"].ToString().ToInt32(),
					GodwnId = row["GodwnId"].ToString().ToInt32(),
					GodownName = row["GodownName"].ToString(),
					ProductId = row["ProductId"].ToString().ToInt32(),
					ProductName = row["ProductName"].ToString(),
					VchrId = row["VchrId"].ToString().ToInt32(),
					VchrNo = row["VchrNo"].ToString().ToInt32(),
					VchrDate = row["VchrDate"].ToString().toDateTime(),
					VchrType = row["VchrType"].ToString(),
					InQuantity = row["InQuantity"].ToString().ToDecimal(),
					OutQuantity = row["OutQuantity"].ToString().ToDecimal(),
					UnitId = row["UnitId"].ToString().ToInt32(),
					Narration = row["Narration"].ToString(),
					UserId = row["UserId"].ToString().ToInt32(),
					TranDate = row["TranDate"].ToString().toDateTime(),
					FK_BatchId = row["FK_BatchId"].ToString().ToInt32(),
					FK_BranchId = row["FK_BranchId"].ToString().ToInt32(),
					Barcode = row["Barcode"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<GodownEntity> GetGodowns(string Query, object entity, string sCon = null)
		{
			List<GodownEntity> list = new List<GodownEntity>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				GodownEntity item = new GodownEntity
				{
					GodwnId = row["GodwnId"].ToString().ToInt32(),
					GodownName = row["GodownName"].ToString(),
					AddressOne = row["AddressOne"].ToString(),
					AddressTwo = row["AddressTwo"].ToString(),
					AddressThree = row["AddressThree"].ToString(),
					Phone = row["Phone"].ToString(),
					Description = row["Description"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<ItemNote> GetItemNotes(string Query, object entity, string sCon = null)
		{
			List<ItemNote> list = new List<ItemNote>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				ItemNote item = new ItemNote
				{
					Id = row["Id"].ToString().ToInt32(),
					Note = row["Note"].ToString(),
					IsMain = row["IsMain"].ToString().ToBool(),
					FieldOne = row["FieldOne"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<KOTOrderMaster> GetKOTOrderMasters(string Query, object entity, string sCon = null)
		{
			List<KOTOrderMaster> list = new List<KOTOrderMaster>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				KOTOrderMaster item = new KOTOrderMaster
				{
					KOTMasterId = row["KOTMasterId"].ToString().ToInt32(),
					TockenNo = row["TockenNo"].ToString().ToInt32(),
					DateAndTime = row["DateAndTime"].ToString().toDateTime(),
					CookingTime = row["CookingTime"].ToString().ToDecimal(),
					Terminal = row["Terminal"].ToString(),
					SerialNo = row["SerialNo"].ToString().ToInt32(),
					FK_FYearId = row["FK_FYearId"].ToString().ToInt32(),
					FK_CustomerId = row["FK_CustomerId"].ToString().ToInt32(),
					FK_UserId = row["FK_UserId"].ToString().ToInt32(),
					FK_StaffId = row["FK_StaffId"].ToString().ToInt32(),
					TableId = row["TableId"].ToString().ToInt32(),
					Remarks = row["Remarks"].ToString(),
					Notes = row["Notes"].ToString(),
					FK_KOTPrinterId = row["FK_KOTPrinterId"].ToString().ToInt32(),
					OrderType = row["OrderType"].ToString(),
					TotalQuantity = row["TotalQuantity"].ToString().ToDecimal(),
					TotalAmount = row["TotalAmount"].ToString().ToDecimal(),
					FK_BranchId = row["FK_BranchId"].ToString().ToInt32()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<KOTOrderSettings> GetKOTOrderSettingss(string Query, object entity, string sCon = null)
		{
			List<KOTOrderSettings> list = new List<KOTOrderSettings>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				KOTOrderSettings item = new KOTOrderSettings
				{
					KOTSettingsId = row["KOTSettingsId"].ToString().ToInt32(),
					FK_KOTMasterId = row["FK_KOTMasterId"].ToString().ToInt32(),
					ProductId = row["ProductId"].ToString().ToInt32(),
					Quantity = row["Quantity"].ToString().ToDecimal(),
					UnitId = row["UnitId"].ToString().ToInt32(),
					Rate = row["Rate"].ToString().ToDecimal(),
					NETAmount = row["NETAmount"].ToString().ToDecimal(),
					Barcode = row["Barcode"].ToString(),
					ItemNote = row["ItemNote"].ToString(),
					ProPriority = row["ProPriority"].ToString().ToInt32()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<KOTPrinter> GetKOTPrinters(string Query, object entity, string sCon = null)
		{
			List<KOTPrinter> list = new List<KOTPrinter>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				KOTPrinter item = new KOTPrinter
				{
					Id = row["Id"].ToString().ToInt32(),
					KOTName = row["KOTName"].ToString(),
					PrinterName = row["PrinterName"].ToString(),
					ConnectionType = row["ConnectionType"].ToString(),
					IPaddress = row["IPaddress"].ToString(),
					Mode = row["Mode"].ToString(),
					FK_BranchId = row["FK_BranchId"].ToString().ToInt32()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<ManufactureDtls> GetManufactureDtls(string Query, object entity, string sCon = null)
		{
			List<ManufactureDtls> list = new List<ManufactureDtls>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				ManufactureDtls item = new ManufactureDtls
				{
					Id = row["Id"].ToString().ToInt32(),
					FK_ManufactureHdrId = row["FK_ManufactureHdrId"].ToString().ToInt32(),
					FK_ProductId = row["FK_ProductId"].ToString().ToInt32(),
					Quantity = row["Quantity"].ToString().ToDecimal(),
					FK_UnitId = row["FK_UnitId"].ToString().ToInt32(),
					PurchasePrice = row["PurchasePrice"].ToString().ToDecimal(),
					SalesePrice = row["SalesePrice"].ToString().ToDecimal(),
					RawCost = row["RawCost"].ToString().ToDecimal(),
					Expenses = row["Expenses"].ToString().ToDecimal(),
					Profit = row["Profit"].ToString().ToDecimal(),
					FinalAmount = row["FinalAmount"].ToString().ToDecimal(),
					NewPurchaseRate = row["NewPurchaseRate"].ToString().ToDecimal(),
					NewSalesRate = row["NewSalesRate"].ToString().ToDecimal(),
					FK_BatchId = row["FK_BatchId"].ToString().ToInt32()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<ManufactureHdr> GetManufactureHdrs(string Query, object entity, string sCon = null)
		{
			List<ManufactureHdr> list = new List<ManufactureHdr>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				ManufactureHdr item = new ManufactureHdr
				{
					Id = row["Id"].ToString().ToInt32(),
					InvNo = row["InvNo"].ToString().ToInt32(),
					Date = row["Date"].ToString().toDateTime(),
					FK_GodownId = row["FK_GodownId"].ToString().ToInt32(),
					Remarks = row["Remarks"].ToString(),
					FK_UserId = row["FK_UserId"].ToString().ToInt32(),
					EntryDate = row["EntryDate"].ToString().toDateTime(),
					FK_BranchId = row["FK_BranchId"].ToString().ToInt32()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<ManufacturingExp> GetManufacturingExps(string Query, object entity, string sCon = null)
		{
			List<ManufacturingExp> list = new List<ManufacturingExp>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				ManufacturingExp item = new ManufacturingExp
				{
					Id = row["Id"].ToString().ToInt32(),
					FK_ExpenseAccountId = row["FK_ExpenseAccountId"].ToString().ToInt32(),
					FK_PaymentAccountId = row["FK_PaymentAccountId"].ToString().ToInt32(),
					Date = row["Date"].ToString().toDateTime(),
					Amount = row["Amount"].ToString().ToDecimal(),
					FK_ManufactureHdrId = row["FK_ManufactureHdrId"].ToString().ToInt32()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<Masters> GetMasters(DataTable dt)
		{
			List<Masters> list = new List<Masters>();
			foreach (DataRow row in dt.Rows)
			{
				Masters item = new Masters
				{
					id = row["id"].ToString().ToInt32(),
					name = row["name"].ToString(),
					description = row["description"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<MasterValue> GetMasterValues(string Query, object entity, string sCon = null)
		{
			List<MasterValue> list = new List<MasterValue>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				MasterValue item = new MasterValue
				{
					Id = row["Id"].ToString().ToInt32(),
					Name = row["Name"].ToString(),
					Description = row["Description"].ToString(),
					MasterType = row["MasterType"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<MenuDetails> GetMenuDetails(string Query, object entity, string sCon = null)
		{
			List<MenuDetails> list = new List<MenuDetails>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				MenuDetails item = new MenuDetails
				{
					Id = row["Id"].ToString().ToInt32(),
					Name = row["Name"].ToString(),
					MenuCaption = row["MenuCaption"].ToString(),
					ParentId = row["ParentId"].ToString().ToInt32(),
					AssemblyName = row["AssemblyName"].ToString(),
					ClassName = row["ClassName"].ToString(),
					IsForm = row["IsForm"].ToString().ToBool(),
					Icon = ((row["Icon"] == DBNull.Value) ? null : ((byte[])row["Icon"])),
					FK_ModuleId = row["FK_ModuleId"].ToString().ToInt32(),
					IsDialog = row["IsDialog"].ToString().ToBool(),
					Status = row["Status"].ToString().ToBool(),
					Position = row["Position"].ToString().ToInt32(),
					IsCrudMenu = row["IsCrudMenu"].ToString().ToBool()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<SalesMaster> GetPOSHoldMasters(string Query, object entity, string sCon = null)
		{
			List<SalesMaster> list = new List<SalesMaster>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				SalesMaster item = new SalesMaster
				{
					SalesInvId = row["Id"].ToString().ToInt32(),
					SalesInvNo = row["SalesInvNo"].ToString().ToInt32(),
					InvType = row["InvType"].ToString(),
					SalesDate = row["SalesDate"].ToString().toDateTime(),
					SalesAccountId = row["SalesAccountId"].ToString().ToInt32(),
					PartyAccountId = row["PartyAccountId"].ToString().ToInt32(),
					RouteId = row["RouteId"].ToString().ToInt32(),
					VehId = row["VehId"].ToString().ToInt32(),
					StafId = row["StafId"].ToString().ToInt32(),
					PriceCategoryId = row["PriceCategoryId"].ToString().ToInt32(),
					AditionalAmount = row["AditionalAmount"].ToString().ToDecimal(),
					Frieght = row["Frieght"].ToString().ToDecimal(),
					TotalDiscount = row["TotalDiscount"].ToString().ToDecimal(),
					BillDiscount = row["BillDiscount"].ToString().ToDecimal(),
					GrandDiscount = row["GrandDiscount"].ToString().ToDecimal(),
					JEId = row["JEId"].ToString().ToInt32(),
					EntryDate = row["EntryDate"].ToString().toDateTime(),
					Remarks = row["Remarks"].ToString(),
					IGSTAmount = row["IGSTAmount"].ToString().ToDecimal(),
					SGSTAmount = row["SGSTAmount"].ToString().ToDecimal(),
					CGSTAmount = row["CGSTAmount"].ToString().ToDecimal(),
					TaxableAmount = row["TaxableAmount"].ToString().ToDecimal(),
					FYearID = row["FYearID"].ToString().ToInt32(),
					FloodCess = row["FloodCess"].ToString().ToDecimal(),
					TotalTaxAmount = row["TotalTaxAmount"].ToString().ToDecimal(),
					FK_BranchId = row["FK_BranchId"].ToString().ToInt32(),
					CntrInvNo = row["CntrInvNo"].ToString(),
					SequenceNo = row["SequenceNo"].ToString().ToInt32(),
					CashPayment = row["CashPayment"].ToString().ToDecimal(),
					CardPayment = row["CardPayment"].ToString().ToDecimal(),
					Credit = row["Credit"].ToString().ToDecimal(),
					FielOne = row["FielOne"].ToString(),
					FieldTwo = row["FieldTwo"].ToString(),
					FieldThree = row["FieldThree"].ToString(),
					FieldFour = row["FieldFour"].ToString(),
					Terminal = row["Terminal"].ToString(),
					IsClosed = row["IsClosed"].ToString().ToBool(),
					FreeAmount = row["FreeAmount"].ToString().ToDecimal(),
					ReturnAmount = row["ReturnAmount"].ToString().ToDecimal(),
					OtherPayment = row["OtherPayment"].ToString().ToDecimal(),
					CashAccountId = row["CashAccountId"].ToString().ToInt32(),
					CardAccountId = row["CardAccountId"].ToString().ToInt32(),
					OnlineAccountId = row["OnlineAccountId"].ToString().ToInt32(),
					IsSettiled = row["IsSettiled"].ToString().ToBool(),
					TotalPayment = row["TotalPayment"].ToString().ToDecimal(),
					VoucherNumber = row["VoucherNumber"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<SalesDetails> GetPOSHoldSettings(string Query, object entity, string sCon = null)
		{
			List<SalesDetails> list = new List<SalesDetails>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				SalesDetails item = new SalesDetails
				{
					SalesSettingsId = row["Id"].ToString().ToInt32(),
					SalesInvId = row["SalesInvId"].ToString().ToInt32(),
					PartyAccountId = row["PartyAccountId"].ToString().ToInt32(),
					ProductId = row["ProductId"].ToString().ToInt32(),
					ProQty = row["ProQty"].ToString().ToDecimal(),
					UnitId = row["UnitId"].ToString().ToInt32(),
					ProRate = row["ProRate"].ToString().ToDecimal(),
					ProDiscont = row["ProDiscont"].ToString().ToDecimal(),
					DiscAmnt = row["DiscAmnt"].ToString().ToDecimal(),
					GrossAmnt = row["GrossAmnt"].ToString().ToDecimal(),
					NetAmnt = row["NetAmnt"].ToString().ToDecimal(),
					Date = row["Date"].ToString().toDateTime(),
					HSN = row["HSN"].ToString(),
					GST = row["GST"].ToString().ToDecimal(),
					GSTAMOUNT = row["GSTAMOUNT"].ToString().ToDecimal(),
					SGST = row["SGST"].ToString().ToDecimal(),
					SGSTAmount = row["SGSTAmount"].ToString().ToDecimal(),
					CGST = row["CGST"].ToString().ToDecimal(),
					CGSTAmount = row["CGSTAmount"].ToString().ToDecimal(),
					IGST = row["IGST"].ToString().ToDecimal(),
					IGSTAMOUNT = row["IGSTAMOUNT"].ToString().ToDecimal(),
					FK_BatchId = row["FK_BatchId"].ToString().ToInt32(),
					Barcode = row["Barcode"].ToString(),
					ItemCalory = row["ItemCalory"].ToString().ToDecimal(),
					EmployeeID = row["EmployeeID"].ToString().ToInt32(),
					EmployeeCommision = row["EmployeeCommision"].ToString().ToDecimal(),
					AgentId = row["AgentId"].ToString().ToInt32(),
					AgentCommission = row["AgentCommission"].ToString().ToDecimal(),
					ProductName = row["ProductName"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<PrevilegeCard> GetPrevilegeCards(string Query, object entity, string sCon = null)
		{
			List<PrevilegeCard> list = new List<PrevilegeCard>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				PrevilegeCard item = new PrevilegeCard
				{
					PrevilageCardId = row["PrevilageCardId"].ToString().ToInt32(),
					ConsumerCode = row["ConsumerCode"].ToString(),
					CustomerId = row["CustomerId"].ToString().ToInt32(),
					PriceCategoryId = row["PriceCategoryId"].ToString().ToInt32(),
					NomineeAccountId = row["NomineeAccountId"].ToString().ToInt32(),
					ExpairyDate = row["ExpairyDate"].ToString().toDateTime(),
					RenewalDate = row["RenewalDate"].ToString().toDateTime(),
					DOBofCustomer = row["DOBofCustomer"].ToString().toDateTime(),
					Remarks = row["Remarks"].ToString(),
					FK_BranchId = row["FK_BranchId"].ToString().ToInt32()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<PriceCategoryEntity> GetPriceCategories(string Query, object entity, string sCon = null)
		{
			List<PriceCategoryEntity> list = new List<PriceCategoryEntity>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				PriceCategoryEntity item = new PriceCategoryEntity
				{
					PCategoryId = row["PCategoryId"].ToString().ToInt32(),
					PCategoryName = row["PCategoryName"].ToString(),
					Percentage = row["Percentage"].ToString().ToDecimal(),
					SDiscountPercentage = row["SDiscountPercentage"].ToString().ToDecimal()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<DotmatrixMaster> GetPrintDesignMasters(string Query, object entity, string sCon = null)
		{
			List<DotmatrixMaster> list = new List<DotmatrixMaster>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				DotmatrixMaster item = new DotmatrixMaster
				{
					printMasterId = row["printMasterId"].ToString().ToInt32(),
					DesignName = row["DesignName"].ToString(),
					VochrType = row["VochrType"].ToString(),
					isDefault = row["isDefault"].ToString().ToBool(),
					isLandScape = row["isLandScape"].ToString().ToBool(),
					PageHieght = row["PageHieght"].ToString().ToInt32(),
					PageWidth = row["PageWidth"].ToString().ToInt32(),
					LinesPerPage = row["LinesPerPage"].ToString().ToInt32(),
					SpaceBtwnLines = row["SpaceBtwnLines"].ToString().ToInt32(),
					BackgroundImgPath = row["BackgroundImgPath"].ToString(),
					PrinterName = row["PrinterName"].ToString(),
					PrintType = row["PrintType"].ToString(),
					upScroll = row["upScroll"].ToString().ToInt32(),
					downScroll = row["downScroll"].ToString().ToInt32(),
					Riverse = row["Riverse"].ToString(),
					Bold_On = row["Bold_On"].ToString(),
					Bold_Off = row["Bold_Off"].ToString(),
					Pitch = row["Pitch"].ToString(),
					FormType = row["FormType"].ToString(),
					SpaceInLines = row["SpaceInLines"].ToString().ToInt32()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<DotmatrixSettings> GetPrintDesignSettingss(string Query, object entity, string sCon = null)
		{
			List<DotmatrixSettings> list = new List<DotmatrixSettings>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				DotmatrixSettings item = new DotmatrixSettings
				{
					PrintDesignSettingsID = row["PrintDesignSettingsID"].ToString().ToInt32(),
					printMasterId = row["printMasterId"].ToString().ToInt32(),
					ControleName = row["ControleName"].ToString(),
					Type = row["Type"].ToString(),
					FeildText = row["FeildText"].ToString(),
					X = row["X"].ToString().ToInt32(),
					Y = row["Y"].ToString().ToInt32(),
					FontSize = row["FontSize"].ToString().ToInt32(),
					Font = row["Font"].ToString(),
					isBold = row["isBold"].ToString().ToBool(),
					width = row["width"].ToString().ToInt32(),
					Alaingmnet = row["Alaingmnet"].ToString(),
					Position = row["Position"].ToString(),
					TabIndex = row["TabIndex"].ToString().ToInt32(),
					iIndex = row["iIndex"].ToString().ToInt32(),
					Height = row["Height"].ToString().ToInt32(),
					Spec = row["Spec"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<ProCombMaster> GetProCombMasters(string Query, object entity, string sCon = null)
		{
			List<ProCombMaster> list = new List<ProCombMaster>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				ProCombMaster item = new ProCombMaster
				{
					ProCombMasterId = row["ProCombMasterId"].ToString().ToInt32(),
					CombProName = row["CombProName"].ToString(),
					ProCombVchrNo = row["ProCombVchrNo"].ToString().ToInt32(),
					Date = row["Date"].ToString().toDateTime(),
					ProQty = row["ProQty"].ToString().ToDecimal(),
					UnitId = row["UnitId"].ToString().ToInt32(),
					ProSaleRate = row["ProSaleRate"].ToString().ToDecimal(),
					ProPurRate = row["ProPurRate"].ToString().ToDecimal(),
					MRP = row["MRP"].ToString().ToDecimal(),
					CombType = row["CombType"].ToString(),
					Barcode = row["Barcode"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<ProCombSettings> GetProCombSettings(string Query, object entity, string sCon = null)
		{
			List<ProCombSettings> list = new List<ProCombSettings>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				ProCombSettings item = new ProCombSettings
				{
					ProCombSettingsId = row["ProCombSettingsId"].ToString().ToInt32(),
					ProCombMasterId = row["ProCombMasterId"].ToString().ToInt32(),
					ProductId = row["ProductId"].ToString().ToInt32(),
					ProQty = row["ProQty"].ToString().ToDecimal(),
					UnitId = row["UnitId"].ToString().ToInt32(),
					SaleRate = row["SaleRate"].ToString().ToDecimal(),
					PurRate = row["PurRate"].ToString().ToDecimal(),
					MRP = row["MRP"].ToString().ToDecimal(),
					Barcode = row["Barcode"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<ExProductPOS> GetPOSProducts(string Query, object entity = null)
		{
			List<ExProductPOS> list = new List<ExProductPOS>();
			DataTable dataTableWithIdParameter_SP = AshproCommon.GetDataTableWithIdParameter_SP(Query, entity);
			foreach (DataRow row in dataTableWithIdParameter_SP.Rows)
			{
				ExProductPOS item = new ExProductPOS
				{
					ProductId = row["ProductId"].ToString().ToInt32(),
					ProductName = row["ProductName"].ToString(),
					image = ((row["image"] == DBNull.Value) ? null : ((byte[])row["image"])),
					Barcode = row["Barcode"].ToString(),
					Rate = row["Rate"].ToString().ToDecimal()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<ProductEntity> GetProducts(string Query = null, object entity = null, DataTable data = null, string sCon = null)
		{
			List<ProductEntity> list = new List<ProductEntity>();
			DataTable dataTable = new DataTable();
			dataTable = ((data == null) ? AshproCommon.GetDataTable(Query, entity, sCon) : data);
			foreach (DataRow row in dataTable.Rows)
			{
				ProductEntity item = new ProductEntity
				{
					ProductId = row["ProductId"].ToString().ToInt32(),
					ProductName = row["ProductName"].ToString(),
					PGroupId = row["PGroupId"].ToString().ToInt32(),
					ProductCode = row["ProductCode"].ToString().ToInt32(),
					ProductType = row["ProductType"].ToString(),
					GodwnId = row["GodwnId"].ToString().ToInt32(),
					SelfId = row["SelfId"].ToString().ToInt32(),
					SaleDis = row["SaleDis"].ToString().ToDecimal(),
					SalesmanCommision = row["SalesmanCommision"].ToString().ToDecimal(),
					SalesUnit = row["SalesUnit"].ToString().ToInt32(),
					SellingPrice = row["SellingPrice"].ToString().ToDecimal(),
					Specification = row["Specification"].ToString(),
					HSNNo = row["HSNNo"].ToString(),
					IsScale = row["IsScale"].ToString().ToBool(),
					OpeningStock = row["OpeningStock"].ToString().ToDecimal(),
					OppStockDate = row["OppStockDate"].ToString().toDateTime(),
					ReOrderStock = row["ReOrderStock"].ToString().ToDecimal(),
					AccountId = row["AccountId"].ToString().ToInt32(),
					AgentCommision = row["AgentCommision"].ToString().ToDecimal(),
					AliasCode = row["AliasCode"].ToString(),
					AliasName = row["AliasName"].ToString(),
					Barcode = row["Barcode"].ToString(),
					BrandId = row["BrandId"].ToString().ToInt32(),
					description = row["description"].ToString(),
					ExpairingDays = row["ExpairingDays"].ToString().ToInt32(),
					FieldFive = row["FieldFive"].ToString(),
					FieldFour = row["FieldFour"].ToString(),
					FieldOne = row["FieldOne"].ToString(),
					FieldThree = row["FieldThree"].ToString(),
					FieldTwo = row["FieldTwo"].ToString(),
					filePath = row["filePath"].ToString(),
					image = ((row["image"] == DBNull.Value) ? null : ((byte[])row["image"])),
					isActive = row["isActive"].ToString().ToBool(),
					IsAllowMultiple = row["IsAllowMultiple"].ToString().ToBool(),
					IsCommisionBasedPercentage = row["IsCommisionBasedPercentage"].ToString().ToBool(),
					IsExpairing = row["IsExpairing"].ToString().ToBool(),
					isInclusive = row["isInclusive"].ToString().ToBool(),
					IsMarginBasedPercentage = row["IsMarginBasedPercentage"].ToString().ToBool(),
					ListOrder = row["ListOrder"].ToString().ToInt32(),
					LocalName = row["LocalName"].ToString(),
					LoyalityPont = row["LoyalityPont"].ToString().ToInt32(),
					MinimumMargin = row["MinimumMargin"].ToString().ToDecimal(),
					MRP = row["MRP"].ToString().ToDecimal(),
					MRPMargin = row["MRPMargin"].ToString().ToDecimal(),
					NormalMargin = row["NormalMargin"].ToString().ToDecimal(),
					PurchaseDis = row["PurchaseDis"].ToString().ToDecimal(),
					PurchasePrice = row["PurchasePrice"].ToString().ToDecimal(),
					PurchaseUnit = row["PurchaseUnit"].ToString().ToInt32(),
					Remarks = row["Remarks"].ToString(),
					ReportUnit = row["ReportUnit"].ToString().ToInt32(),
					RetailMargin = row["RetailMargin"].ToString().ToDecimal(),
					TCategoryId = row["TCategoryId"].ToString().ToInt32(),
					UnitId = row["UnitId"].ToString().ToInt32(),
					WholesaleMargin = row["WholesaleMargin"].ToString().ToDecimal(),
					isRateBasedWeight = row["isRateBasedWeight"].ToString().ToBool(),
					PriorityStatus = row["PriorityStatus"].ToString(),
					CookingTime = row["CookingTime"].ToString().ToInt32(),
					IsFavorite = row["IsFavorite"].ToString().ToBool(),
					RestaurantItemType = row["RestaurantItemType"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		//public static List<ProductBarcodeEntity> GetProductBarcodes(string Query, object entity, string sCon = null)
		//{
		//	List<ProductBarcodeEntity> list = new List<ProductBarcodeEntity>();
		//	DataTable dataTable = new DataTable();
		//	dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
		//	foreach (DataRow row in dataTable.Rows)
		//	{
		//		ProductBarcodeEntity productBarcodeEntity = new ProductBarcodeEntity();
		//		productBarcodeEntity.Id = row["Id"].ToString().ToInt32();
		//		productBarcodeEntity.Barcode = row["Barcode"].ToString();
		//		productBarcodeEntity.BarcodeType = row["BarcodeType"].ToString();
		//		if (row["ExpairyDate"] != DBNull.Value)
		//		{
		//			productBarcodeEntity.ExpairyDate = row["ExpairyDate"].ToString().toDateTime();
		//		}
		//		if (row["ManufDate"] != DBNull.Value)
		//		{
		//			productBarcodeEntity.ManufDate = row["ManufDate"].ToString().toDateTime();
		//		}
		//		productBarcodeEntity.FK_ProductId = row["FK_ProductId"].ToString().ToInt32();
		//		productBarcodeEntity.FK_UnitId = row["FK_UnitId"].ToString().ToInt32();
		//		productBarcodeEntity.FK_BatchId = row["FK_BatchId"].ToString().ToInt32();
		//		productBarcodeEntity.FK_BranchId = row["FK_BranchId"].ToString().ToInt32();
		//		productBarcodeEntity.Remarks = row["Remarks"].ToString();
		//		productBarcodeEntity.PurchasePrice = row["PurchasePrice"].ToString().ToDecimal();
		//		productBarcodeEntity.SellingPrice = row["SellingPrice"].ToString().ToDecimal();
		//		productBarcodeEntity.MRP = row["MRP"].ToString().ToDecimal();
		//		productBarcodeEntity.ProductCost = row["ProductCost"].ToString().ToDecimal();
		//		productBarcodeEntity.FieldOne = row["FieldOne"].ToString();
		//		productBarcodeEntity.FieldTwo = row["FieldTwo"].ToString();
		//		productBarcodeEntity.FieldThree = row["FieldThree"].ToString();
		//		productBarcodeEntity.FieldFour = row["FieldFour"].ToString();
		//		productBarcodeEntity.MinimuPrice = row["MinimuPrice"].ToString().ToDecimal();
		//		productBarcodeEntity.RetailPrice = row["RetailPrice"].ToString().ToDecimal();
		//		productBarcodeEntity.WholesalePrice = row["WholesalePrice"].ToString().ToDecimal();
		//		productBarcodeEntity.ProductBarcodeName = row["ProductBarcodeName"].ToString();
		//		productBarcodeEntity.LocalName = row["LocalName"].ToString();
		//		productBarcodeEntity.image = ((row["image"] == DBNull.Value) ? null : ((byte[])row["image"]));
		//		list.Add(productBarcodeEntity);
		//	}
		//	return list;
		//}

		//public static List<ProductBarcodeScale> GetProductBarcodeScales(string Query, object entity, string sCon = null)
		//{
		//	List<ProductBarcodeScale> list = new List<ProductBarcodeScale>();
		//	DataTable dataTable = new DataTable();
		//	dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
		//	foreach (DataRow row in dataTable.Rows)
		//	{
		//		ProductBarcodeScale item = new ProductBarcodeScale
		//		{
		//			Id = row["Id"].ToString().ToInt32(),
		//			FK_ProductId = row["FK_ProductId"].ToString().ToInt32(),
		//			FK_BarcodeScale = row["FK_BarcodeScale"].ToString().ToInt32()
		//		};
		//		list.Add(item);
		//	}
		//	return list;
		//}

		public static List<ProductGroupEntity> GetProductGroups(string Query, object entity, string sCon = null)
		{
			List<ProductGroupEntity> list = new List<ProductGroupEntity>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				ProductGroupEntity item = new ProductGroupEntity
				{
					PGroupId = row["PGroupId"].ToString().ToInt32(),
					PGroupName = row["PGroupName"].ToString(),
					Under = row["Under"].ToString(),
					SalesInsentive = row["SalesInsentive"].ToString().ToDecimal(),
					SalesDis = row["SalesDis"].ToString().ToDecimal(),
					PurchDis = row["PurchDis"].ToString().ToDecimal(),
					Remarks = row["Remarks"].ToString(),
					ListOrder = row["ListOrder"].ToString().ToInt32(),
					isShowInGrid = row["isShowInGrid"].ToString().ToBool()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<ProductPriceCategory> GetProductPriceCategorys(string Query, object entity, string sCon = null)
		{
			List<ProductPriceCategory> list = new List<ProductPriceCategory>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				ProductPriceCategory item = new ProductPriceCategory
				{
					ProPrceCatId = row["ProPrceCatId"].ToString().ToInt32(),
					ProductId = row["ProductId"].ToString().ToInt32(),
					PCategoryId = row["PCategoryId"].ToString().ToInt32(),
					UnitId = row["UnitId"].ToString().ToInt32(),
					PGroupId = row["PGroupId"].ToString().ToInt32(),
					SPrice = row["SPrice"].ToString().ToDecimal(),
					SPDisc = row["SPDisc"].ToString().ToDecimal(),
					PPrice = row["PPrice"].ToString().ToDecimal(),
					PDisc = row["PDisc"].ToString().ToDecimal(),
					Barcode = row["Barcode"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<ProductPrinter> GetProductPrinters(string Query, object entity, string sCon = null)
		{
			List<ProductPrinter> list = new List<ProductPrinter>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				ProductPrinter item = new ProductPrinter
				{
					Id = row["Id"].ToString().ToInt32(),
					FK_KOTPrinterId = row["FK_KOTPrinterId"].ToString().ToInt32(),
					FK_ProductId = row["FK_ProductId"].ToString().ToInt32(),
					FK_BranchId = row["FK_BranchId"].ToString().ToInt32()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<ProductUnit> GetProductUnits(string Query, object entity, string sCon = null)
		{
			List<ProductUnit> list = new List<ProductUnit>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				ProductUnit item = new ProductUnit
				{
					ProUnitId = row["ProUnitId"].ToString().ToInt32(),
					ProductId = row["ProductId"].ToString().ToInt32(),
					UnitId = row["UnitId"].ToString().ToInt32(),
					UnitType = row["UnitType"].ToString(),
					Barcode = row["Barcode"].ToString(),
					UnitQty = row["UnitQty"].ToString().ToDecimal(),
					ProductUnitName = row["ProductUnitName"].ToString(),
					IsInclusive = row["IsInclusive"].ToString().ToBool(),
					OpeningStock = row["OpeningStock"].ToString().ToDecimal(),
					SalesPrice = row["SalesPrice"].ToString().ToDecimal()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<PurchaseInvoiceHdr> GetPurInvMasters(string Query, object entity, string sCon = null)
		{
			List<PurchaseInvoiceHdr> list = new List<PurchaseInvoiceHdr>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				PurchaseInvoiceHdr item = new PurchaseInvoiceHdr
				{
					PurInvId = row["PurInvId"].ToString().ToInt32(),
					PurInvNo = row["PurInvNo"].ToString().ToInt32(),
					InvType = row["InvType"].ToString(),
					PurDate = row["PurDate"].ToString().toDateTime(),
					PurActId = row["PurActId"].ToString().ToInt32(),
					PartyAccId = row["PartyAccId"].ToString().ToInt32(),
					PriceCategoryId = row["PriceCategoryId"].ToString().ToInt32(),
					AdditionalAmnt = row["AdditionalAmnt"].ToString().ToDecimal(),
					Frieght = row["Frieght"].ToString().ToDecimal(),
					TotalDiscount = row["TotalDiscount"].ToString().ToDecimal(),
					BillDisc = row["BillDisc"].ToString().ToDecimal(),
					TotalTax = row["TotalTax"].ToString().ToDecimal(),
					TotalAmnt = row["TotalAmnt"].ToString().ToDecimal(),
					JEId = row["JEId"].ToString().ToInt32(),
					EntryDate = row["EntryDate"].ToString().toDateTime(),
					Remarks = row["Remarks"].ToString(),
					IGSTAmount = row["IGSTAmount"].ToString().ToDecimal(),
					SGSTAmount = row["SGSTAmount"].ToString().ToDecimal(),
					CGSTAmount = row["CGSTAmount"].ToString().ToDecimal(),
					RefInvNo = row["RefInvNo"].ToString(),
					Taxable = row["Taxable"].ToString().ToDecimal(),
					FK_BranchId = row["FK_BranchId"].ToString().ToInt32(),
					CashPayment = row["CashPayment"].ToString().ToDecimal(),
					CardPayment = row["CardPayment"].ToString().ToDecimal(),
					Credit = row["Credit"].ToString().ToDecimal(),
					FreeAmount = row["FreeAmount"].ToString().ToDecimal(),
					ReturnAmount = row["ReturnAmount"].ToString().ToDecimal(),
					IsClosed = row["IsClosed"].ToString().ToBool(),
					FielOne = row["FielOne"].ToString(),
					FieldTwo = row["FieldTwo"].ToString(),
					FieldThree = row["FieldThree"].ToString(),
					FieldFour = row["FieldFour"].ToString(),
					CashAccountId = row["CashAccountId"].ToString().ToInt32(),
					CardAccountId = row["CardAccountId"].ToString().ToInt32(),
					OnlineAccountId = row["OnlineAccountId"].ToString().ToInt32(),
					IsSettiled = row["IsSettiled"].ToString().ToBool(),
					TotalPayment = row["TotalPayment"].ToString().ToDecimal(),
					VoucherNumber = row["VoucherNumber"].ToString(),
					SequenceNo = row["SequenceNo"].ToString().ToInt32(),
					Terminal = row["Terminal"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<PurchaseInvoiceDtl> GetPurInvSettings(string Query, object entity, string sCon = null)
		{
			List<PurchaseInvoiceDtl> list = new List<PurchaseInvoiceDtl>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				PurchaseInvoiceDtl item = new PurchaseInvoiceDtl
				{
					PurSettingsId = row["PurSettingsId"].ToString().ToInt32(),
					PurInvId = row["PurInvId"].ToString().ToInt32(),
					PartyAccId = row["PartyAccId"].ToString().ToInt32(),
					ProId = row["ProId"].ToString().ToInt32(),
					ProQty = row["ProQty"].ToString().ToDecimal(),
					UnitId = row["UnitId"].ToString().ToInt32(),
					ProRate = row["ProRate"].ToString().ToDecimal(),
					Disc = row["Disc"].ToString().ToDecimal(),
					DiscAmnt = row["DiscAmnt"].ToString().ToDecimal(),
					Gross = row["Gross"].ToString().ToDecimal(),
					NetAmnt = row["NetAmnt"].ToString().ToDecimal(),
					Date = row["Date"].ToString().toDateTime(),
					HSN = row["HSN"].ToString(),
					GST = row["GST"].ToString().ToDecimal(),
					GSTAMOUNT = row["GSTAMOUNT"].ToString().ToDecimal(),
					SGST = row["SGST"].ToString().ToDecimal(),
					SGSTAmount = row["SGSTAmount"].ToString().ToDecimal(),
					CGST = row["CGST"].ToString().ToDecimal(),
					CGSTAmount = row["CGSTAmount"].ToString().ToDecimal(),
					IGST = row["IGST"].ToString().ToDecimal(),
					IGSTAMOUNT = row["IGSTAMOUNT"].ToString().ToDecimal(),
					FK_BatchId = row["FK_BatchId"].ToString().ToInt32(),
					Barcode = row["Barcode"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<PurRtnInvMaster> GetPurRtnInvMasters(string Query, object entity, string sCon = null)
		{
			List<PurRtnInvMaster> list = new List<PurRtnInvMaster>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				PurRtnInvMaster item = new PurRtnInvMaster
				{
					PurRtnInvId = row["PurRtnInvId"].ToString().ToInt32(),
					PurRtnInvNo = row["PurRtnInvNo"].ToString().ToInt32(),
					PurRtnDate = row["PurRtnDate"].ToString().toDateTime(),
					PurRtnAcId = row["PurRtnAcId"].ToString().ToInt32(),
					PartyAcId = row["PartyAcId"].ToString().ToInt32(),
					PriceCategoryId = row["PriceCategoryId"].ToString().ToInt32(),
					AdditionalAmnt = row["AdditionalAmnt"].ToString().ToDecimal(),
					Frieght = row["Frieght"].ToString().ToDecimal(),
					TotalDiscount = row["TotalDiscount"].ToString().ToDecimal(),
					BillDisc = row["BillDisc"].ToString().ToDecimal(),
					TotalTax = row["TotalTax"].ToString().ToDecimal(),
					TotalAmnt = row["TotalAmnt"].ToString().ToDecimal(),
					JEId = row["JEId"].ToString().ToInt32(),
					EntryDate = row["EntryDate"].ToString().toDateTime(),
					Remarks = row["Remarks"].ToString(),
					IGSTAmount = row["IGSTAmount"].ToString().ToDecimal(),
					SGSTAmount = row["SGSTAmount"].ToString().ToDecimal(),
					CGSTAmount = row["CGSTAmount"].ToString().ToDecimal(),
					Taxable = row["Taxable"].ToString().ToDecimal(),
					FK_BranchId = row["FK_BranchId"].ToString().ToInt32(),
					SequenceNo = row["SequenceNo"].ToString().ToInt32(),
					Terminal = row["Terminal"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<PurRtnInvSttings> GetPurRtnInvSttings(string Query, object entity, string sCon = null)
		{
			List<PurRtnInvSttings> list = new List<PurRtnInvSttings>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				PurRtnInvSttings item = new PurRtnInvSttings
				{
					PurRtnSettId = row["PurRtnSettId"].ToString().ToInt32(),
					PurRtnInvId = row["PurRtnInvId"].ToString().ToInt32(),
					ProId = row["ProId"].ToString().ToInt32(),
					ProQty = row["ProQty"].ToString().ToDecimal(),
					UnitId = row["UnitId"].ToString().ToInt32(),
					ProRate = row["ProRate"].ToString().ToDecimal(),
					Disc = row["Disc"].ToString().ToDecimal(),
					DiscAmnt = row["DiscAmnt"].ToString().ToDecimal(),
					Gross = row["Gross"].ToString().ToDecimal(),
					Tax = row["Tax"].ToString().ToDecimal(),
					TaxAmnt = row["TaxAmnt"].ToString().ToDecimal(),
					NetAmnt = row["NetAmnt"].ToString().ToDecimal(),
					PartyAccId = row["PartyAccId"].ToString().ToInt32(),
					Date = row["Date"].ToString().toDateTime(),
					HSN = row["HSN"].ToString(),
					GST = row["GST"].ToString().ToDecimal(),
					GSTAMOUNT = row["GSTAMOUNT"].ToString().ToDecimal(),
					SGST = row["SGST"].ToString().ToDecimal(),
					SGSTAmount = row["SGSTAmount"].ToString().ToDecimal(),
					CGST = row["CGST"].ToString().ToDecimal(),
					CGSTAmount = row["CGSTAmount"].ToString().ToDecimal(),
					IGST = row["IGST"].ToString().ToDecimal(),
					IGSTAMOUNT = row["IGSTAMOUNT"].ToString().ToDecimal(),
					FK_BatchId = row["FK_BatchId"].ToString().ToInt32(),
					Barcode = row["Barcode"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<RateFormat> GetRateFormats(string Query, object entity, string sCon = null)
		{
			List<RateFormat> list = new List<RateFormat>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				RateFormat item = new RateFormat
				{
					Id = row["Id"].ToString().ToInt32(),
					Name = row["Name"].ToString().ToInt32(),
					Value = row["Value"].ToString(),
					Alternative = row["Alternative"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<RawmaterialMappingEntity> GetRawmaterialMappings(string Query, object entity, string sCon = null)
		{
			List<RawmaterialMappingEntity> list = new List<RawmaterialMappingEntity>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				RawmaterialMappingEntity item = new RawmaterialMappingEntity
				{
					Id = row["Id"].ToString().ToInt32(),
					FK_ProductId = row["FK_ProductId"].ToString().ToInt32(),
					Quantity = row["Quantity"].ToString().ToDecimal(),
					FK_UnitId = row["FK_UnitId"].ToString().ToInt32(),
					Rate = row["Rate"].ToString().ToDecimal(),
					NetAmount = row["NetAmount"].ToString().ToDecimal(),
					FK_ManufactureHdrId = row["FK_ManufactureHdrId"].ToString().ToInt32(),
					FK_BatchId = row["FK_BatchId"].ToString().ToInt32()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<ReceiptAndPayment> GetReceiptAndPayments(string Query, object entity, string sCon = null)
		{
			List<ReceiptAndPayment> list = new List<ReceiptAndPayment>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				ReceiptAndPayment item = new ReceiptAndPayment
				{
					Id = row["Id"].ToString().ToInt32(),
					IsSettiled = row["IsSettiled"].ToString().ToBool(),
					CntrInvNo = row["CntrInvNo"].ToString(),
					SalesDate = row["SalesDate"].ToString().toDateTime(),
					GrandDiscount = row["GrandDiscount"].ToString().ToDecimal(),
					TotalPayment = row["TotalPayment"].ToString().ToDecimal(),
					Credit = row["Credit"].ToString().ToDecimal(),
					Payment = row["Payment"].ToString().ToDecimal(),
					Balance = row["Balance"].ToString().ToDecimal(),
					VchrNo = row["VchrNo"].ToString(),
					InvoiceId = row["InvoiceId"].ToString().ToInt32(),
					VchrId = row["VchrId"].ToString().ToInt32(),
					InvoiceType = row["InvoiceType"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<ReminderTB> GetReminderTBs(string Query, object entity, string sCon = null)
		{
			List<ReminderTB> list = new List<ReminderTB>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				ReminderTB item = new ReminderTB
				{
					ReminderId = row["ReminderId"].ToString().ToInt32(),
					RemainderHead = row["RemainderHead"].ToString(),
					Remainder = row["Remainder"].ToString(),
					CurrentDate = row["CurrentDate"].ToString(),
					ReminderDate = row["ReminderDate"].ToString(),
					UserId = row["UserId"].ToString().ToInt32(),
					TargetUserId = row["TargetUserId"].ToString().ToInt32(),
					FK_BranchId = row["FK_BranchId"].ToString().ToInt32()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<RestaurantTable> GetRestaurantTables(string Query, object entity, string sCon = null)
		{
			List<RestaurantTable> list = new List<RestaurantTable>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				RestaurantTable item = new RestaurantTable
				{
					TableId = row["TableId"].ToString().ToInt32(),
					TableName = row["TableName"].ToString(),
					ChairCount = row["ChairCount"].ToString().ToInt32(),
					FloorName = row["FloorName"].ToString(),
					XPosition = row["XPosition"].ToString().ToInt32(),
					YPostion = row["YPostion"].ToString().ToInt32(),
					Image = ((row["Image"] == DBNull.Value) ? null : ((byte[])row["Image"])),
					FK_EmplyeeId = row["FK_EmplyeeId"].ToString().ToInt32()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<RouteEntity> GetRoutes(string Query, object entity, string sCon = null)
		{
			List<RouteEntity> list = new List<RouteEntity>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				RouteEntity item = new RouteEntity
				{
					RootId = row["RootId"].ToString().ToInt32(),
					RootName = row["RootName"].ToString(),
					WeekDay = row["WeekDay"].ToString(),
					SalesManId = row["SalesManId"].ToString().ToInt32(),
					DriverId = row["DriverId"].ToString().ToInt32(),
					VehicleId = row["VehicleId"].ToString().ToInt32(),
					FK_BranchId = row["FK_BranchId"].ToString().ToInt32()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<SalaryProcessMaster> GetSalaryProcessMasters(string Query, object entity, string sCon = null)
		{
			List<SalaryProcessMaster> list = new List<SalaryProcessMaster>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				SalaryProcessMaster item = new SalaryProcessMaster
				{
					SPrcsMasterID = row["SPrcsMasterID"].ToString().ToInt32(),
					SPrcsVchrNo = row["SPrcsVchrNo"].ToString().ToInt32(),
					SPrcsDate = row["SPrcsDate"].ToString().toDateTime(),
					SlryPrd = row["SlryPrd"].ToString(),
					DateFrom = row["DateFrom"].ToString().toDateTime(),
					DateTo = row["DateTo"].ToString().toDateTime(),
					StaffType = row["StaffType"].ToString(),
					Description = row["Description"].ToString(),
					CashOrBankAcctId = row["CashOrBankAcctId"].ToString().ToInt32(),
					CashOrBankAcctName = row["CashOrBankAcctName"].ToString(),
					FK_BranchId = row["FK_BranchId"].ToString().ToInt32()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<SalesCounterEntity> GetSalesCounters(string Query, object entity, string sCon = null)
		{
			List<SalesCounterEntity> list = new List<SalesCounterEntity>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				SalesCounterEntity item = new SalesCounterEntity
				{
					SalesCounterId = row["SalesCounterId"].ToString().ToInt32(),
					SalesCounterName = row["SalesCounterName"].ToString(),
					CashAccountId = row["CashAccountId"].ToString().ToInt32(),
					Remarks = row["Remarks"].ToString(),
					FK_BranchId = row["FK_BranchId"].ToString().ToInt32()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<SalesMaster> GetSalesEstimateMasters(string Query, object entity, string sCon = null)
		{
			List<SalesMaster> list = new List<SalesMaster>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				SalesMaster item = new SalesMaster
				{
					SalesInvId = row["SalesInvId"].ToString().ToInt32(),
					SalesInvNo = row["SalesInvNo"].ToString().ToInt32(),
					InvType = row["InvType"].ToString(),
					SalesDate = row["SalesDate"].ToString().toDateTime(),
					SalesAccountId = row["SalesAccountId"].ToString().ToInt32(),
					PartyAccountId = row["PartyAccountId"].ToString().ToInt32(),
					RouteId = row["RouteId"].ToString().ToInt32(),
					VehId = row["VehId"].ToString().ToInt32(),
					StafId = row["StafId"].ToString().ToInt32(),
					PriceCategoryId = row["PriceCategoryId"].ToString().ToInt32(),
					AditionalAmount = row["AditionalAmount"].ToString().ToDecimal(),
					Frieght = row["Frieght"].ToString().ToDecimal(),
					TotalDiscount = row["TotalDiscount"].ToString().ToDecimal(),
					BillDiscount = row["BillDiscount"].ToString().ToDecimal(),
					GrandDiscount = row["GrandDiscount"].ToString().ToDecimal(),
					JEId = row["JEId"].ToString().ToInt32(),
					EntryDate = row["EntryDate"].ToString().toDateTime(),
					Remarks = row["Remarks"].ToString(),
					IGSTAmount = row["IGSTAmount"].ToString().ToDecimal(),
					SGSTAmount = row["SGSTAmount"].ToString().ToDecimal(),
					CGSTAmount = row["CGSTAmount"].ToString().ToDecimal(),
					TaxableAmount = row["TaxableAmount"].ToString().ToDecimal(),
					FYearID = row["FYearID"].ToString().ToInt32(),
					FloodCess = row["FloodCess"].ToString().ToDecimal(),
					TotalTaxAmount = row["TotalTaxAmount"].ToString().ToDecimal(),
					FK_BranchId = row["FK_BranchId"].ToString().ToInt32(),
					CntrInvNo = row["CntrInvNo"].ToString(),
					SequenceNo = row["SequenceNo"].ToString().ToInt32(),
					CashPayment = row["CashPayment"].ToString().ToDecimal(),
					CardPayment = row["CardPayment"].ToString().ToDecimal(),
					Credit = row["Credit"].ToString().ToDecimal(),
					FielOne = row["FielOne"].ToString(),
					FieldTwo = row["FieldTwo"].ToString(),
					FieldThree = row["FieldThree"].ToString(),
					FieldFour = row["FieldFour"].ToString(),
					Terminal = row["Terminal"].ToString(),
					IsClosed = row["IsClosed"].ToString().ToBool(),
					FreeAmount = row["FreeAmount"].ToString().ToDecimal(),
					ReturnAmount = row["ReturnAmount"].ToString().ToDecimal(),
					OtherPayment = row["OtherPayment"].ToString().ToDecimal(),
					CashAccountId = row["CashAccountId"].ToString().ToInt32(),
					CardAccountId = row["CardAccountId"].ToString().ToInt32(),
					OnlineAccountId = row["OnlineAccountId"].ToString().ToInt32(),
					IsSettiled = row["IsSettiled"].ToString().ToBool(),
					TotalPayment = row["TotalPayment"].ToString().ToDecimal(),
					VoucherNumber = row["VoucherNumber"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<SalesDetails> GetSalesEstimateSettings(string Query, object entity, string sCon = null)
		{
			List<SalesDetails> list = new List<SalesDetails>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				SalesDetails salesDetails = new SalesDetails();
				salesDetails.SalesSettingsId = row["SalesSettingsId"].ToString().ToInt32();
				salesDetails.SalesInvId = row["SalesInvId"].ToString().ToInt32();
				salesDetails.PartyAccountId = row["PartyAccountId"].ToString().ToInt32();
				salesDetails.ProductId = row["ProductId"].ToString().ToInt32();
				salesDetails.ProQty = row["ProQty"].ToString().ToDecimal();
				salesDetails.UnitId = row["UnitId"].ToString().ToInt32();
				salesDetails.ProRate = row["ProRate"].ToString().ToDecimal();
				salesDetails.ProDiscont = row["ProDiscont"].ToString().ToDecimal();
				salesDetails.DiscAmnt = row["DiscAmnt"].ToString().ToDecimal();
				salesDetails.GrossAmnt = row["GrossAmnt"].ToString().ToDecimal();
				salesDetails.NetAmnt = row["NetAmnt"].ToString().ToDecimal();
				salesDetails.Date = row["Date"].ToString().toDateTime();
				salesDetails.HSN = row["HSN"].ToString();
				salesDetails.GST = row["GST"].ToString().ToDecimal();
				salesDetails.GSTAMOUNT = row["GSTAMOUNT"].ToString().ToDecimal();
				salesDetails.SGST = row["SGST"].ToString().ToDecimal();
				salesDetails.SGSTAmount = row["SGSTAmount"].ToString().ToDecimal();
				salesDetails.CGST = row["CGST"].ToString().ToDecimal();
				salesDetails.CGSTAmount = row["CGSTAmount"].ToString().ToDecimal();
				salesDetails.IGST = row["IGST"].ToString().ToDecimal();
				salesDetails.IGSTAMOUNT = row["IGSTAMOUNT"].ToString().ToDecimal();
				salesDetails.FK_BatchId = row["FK_BatchId"].ToString().ToInt32();
				salesDetails.Barcode = row["Barcode"].ToString();
				salesDetails.ItemCalory = row["ItemCalory"].ToString().ToDecimal();
				salesDetails.EmployeeID = row["EmployeeID"].ToString().ToInt32();
				salesDetails.EmployeeCommision = row["EmployeeCommision"].ToString().ToDecimal();
				salesDetails.AgentId = row["AgentId"].ToString().ToInt32();
				salesDetails.AgentCommission = row["AgentCommission"].ToString().ToDecimal();
				salesDetails.ProductName = row["ProductName"].ToString();
				list.Add(salesDetails);
			}
			return list;
		}

		public static List<SalesOrderMaster> GetSalesOrderMasters(string Query, object entity, string sCon = null)
		{
			List<SalesOrderMaster> list = new List<SalesOrderMaster>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				SalesOrderMaster item = new SalesOrderMaster
				{
					SalesOrderMasterId = row["SalesOrderMasterId"].ToString().ToInt32(),
					VchrNo = row["VchrNo"].ToString().ToInt32(),
					CustomerId = row["CustomerId"].ToString().ToInt32(),
					SalesAccountId = row["SalesAccountId"].ToString().ToInt32(),
					SalesManId = row["SalesManId"].ToString().ToInt32(),
					RouteId = row["RouteId"].ToString().ToInt32(),
					PriceCategoryId = row["PriceCategoryId"].ToString().ToInt32(),
					Remarks = row["Remarks"].ToString(),
					NetAmount = row["NetAmount"].ToString().ToDecimal(),
					AdditionalAmnt = row["AdditionalAmnt"].ToString().ToDecimal(),
					Freight = row["Freight"].ToString().ToDecimal(),
					BillDiscount = row["BillDiscount"].ToString().ToDecimal(),
					AddDiscount = row["AddDiscount"].ToString().ToDecimal(),
					GrandTotal = row["GrandTotal"].ToString().ToDecimal(),
					InvType = row["InvType"].ToString(),
					JEid = row["JEid"].ToString().ToInt32(),
					EntryDate = row["EntryDate"].ToString().toDateTime(),
					InvDate = row["InvDate"].ToString().toDateTime(),
					Status = row["Status"].ToString().ToBool(),
					Taxable = row["Taxable"].ToString().ToDecimal(),
					TotalTax = row["TotalTax"].ToString().ToDecimal(),
					FK_BranchId = row["FK_BranchId"].ToString().ToInt32()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<SalesOrderSettings> GetSalesOrderSettings(string Query, object entity, string sCon = null)
		{
			List<SalesOrderSettings> list = new List<SalesOrderSettings>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				SalesOrderSettings item = new SalesOrderSettings
				{
					SalesOrderMasterId = row["SalesOrderMasterId"].ToString().ToInt32(),
					PartyAccountId = row["PartyAccountId"].ToString().ToInt32(),
					ProductId = row["ProductId"].ToString().ToInt32(),
					ProQty = row["ProQty"].ToString().ToDecimal(),
					UnitId = row["UnitId"].ToString().ToInt32(),
					ProRate = row["ProRate"].ToString().ToDecimal(),
					ProDiscont = row["ProDiscont"].ToString().ToDecimal(),
					DiscAmnt = row["DiscAmnt"].ToString().ToDecimal(),
					GrossAmnt = row["GrossAmnt"].ToString().ToDecimal(),
					NetAmnt = row["NetAmnt"].ToString().ToDecimal(),
					Taxable = row["Taxable"].ToString().ToDecimal(),
					Tax = row["Tax"].ToString().ToDecimal(),
					TaxAmount = row["TaxAmount"].ToString().ToDecimal(),
					FK_BatchId = row["FK_BatchId"].ToString().ToInt32(),
					Barcode = row["Barcode"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<SalesRtnMaster> GetSalesRtnMasters(string Query, object entity, string sCon = null)
		{
			List<SalesRtnMaster> list = new List<SalesRtnMaster>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				SalesRtnMaster item = new SalesRtnMaster
				{
					SlsRtrnId = row["SlsRtrnId"].ToString().ToInt32(),
					SlRtnInvNo = row["SlRtnInvNo"].ToString().ToInt32(),
					SlsRtnDate = row["SlsRtnDate"].ToString().toDateTime(),
					SlsRtnAccountId = row["SlsRtnAccountId"].ToString().ToInt32(),
					PartyAccountId = row["PartyAccountId"].ToString().ToInt32(),
					RouteId = row["RouteId"].ToString().ToInt32(),
					VehId = row["VehId"].ToString().ToInt32(),
					StaffId = row["StaffId"].ToString().ToInt32(),
					PriceCategoryId = row["PriceCategoryId"].ToString().ToInt32(),
					AditionalAmnt = row["AditionalAmnt"].ToString().ToDecimal(),
					Frieght = row["Frieght"].ToString().ToDecimal(),
					TotalDiscount = row["TotalDiscount"].ToString().ToDecimal(),
					BillDiscount = row["BillDiscount"].ToString().ToDecimal(),
					TotalAmount = row["TotalAmount"].ToString().ToDecimal(),
					JeId = row["JeId"].ToString().ToInt32(),
					EntryDate = row["EntryDate"].ToString().toDateTime(),
					Remarks = row["Remarks"].ToString(),
					IGSTAmount = row["IGSTAmount"].ToString().ToDecimal(),
					SGSTAmount = row["SGSTAmount"].ToString().ToDecimal(),
					CGSTAmount = row["CGSTAmount"].ToString().ToDecimal(),
					FYearID = row["FYearID"].ToString().ToInt32(),
					TotalTax = row["TotalTax"].ToString().ToDecimal(),
					Taxable = row["Taxable"].ToString().ToDecimal(),
					FK_BranchId = row["FK_BranchId"].ToString().ToInt32(),
					SequenceNo = row["SequenceNo"].ToString().ToInt32(),
					Terminal = row["Terminal"].ToString(),
					RefInvNo = row["RefInvNo"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<SalesRtnDetails> GetSlsRtnSettings(string Query, object entity, string sCon = null)
		{
			List<SalesRtnDetails> list = new List<SalesRtnDetails>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				SalesRtnDetails item = new SalesRtnDetails
				{
					SlsrtnSettingsID = row["SlsrtnSettingsID"].ToString().ToInt32(),
					SlsRtnId = row["SlsRtnId"].ToString().ToInt32(),
					ProductId = row["ProductId"].ToString().ToInt32(),
					ProQty = row["ProQty"].ToString().ToDecimal(),
					UnitId = row["UnitId"].ToString().ToInt32(),
					ProRate = row["ProRate"].ToString().ToDecimal(),
					ProDis = row["ProDis"].ToString().ToDecimal(),
					DiscAmnt = row["DiscAmnt"].ToString().ToDecimal(),
					GrossAmnt = row["GrossAmnt"].ToString().ToDecimal(),
					NetAmnt = row["NetAmnt"].ToString().ToDecimal(),
					PartyAccountId = row["PartyAccountId"].ToString().ToInt32(),
					Date = row["Date"].ToString().toDateTime(),
					HSN = row["HSN"].ToString().ToString(),
					GST = row["GST"].ToString().ToDecimal(),
					GSTAMOUNT = row["GSTAMOUNT"].ToString().ToDecimal(),
					SGST = row["SGST"].ToString().ToDecimal(),
					SGSTAmount = row["SGSTAmount"].ToString().ToDecimal(),
					CGST = row["CGST"].ToString().ToDecimal(),
					CGSTAmount = row["CGSTAmount"].ToString().ToDecimal(),
					IGST = row["IGST"].ToString().ToDecimal(),
					IGSTAMOUNT = row["IGSTAMOUNT"].ToString().ToDecimal(),
					FK_BatchId = row["FK_BatchId"].ToString().ToInt32(),
					Barcode = row["Barcode"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<SlryPrcsSettings> GetSlryPrcsSettings(string Query, object entity, string sCon = null)
		{
			List<SlryPrcsSettings> list = new List<SlryPrcsSettings>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				SlryPrcsSettings item = new SlryPrcsSettings
				{
					SPrcsStngsID = row["SPrcsStngsID"].ToString().ToInt32(),
					SprcsMasterID = row["SprcsMasterID"].ToString().ToInt32(),
					EmpID = row["EmpID"].ToString().ToInt32(),
					EmpName = row["EmpName"].ToString(),
					LeavsTaken = row["LeavsTaken"].ToString().ToDecimal(),
					CasualLeave = row["CasualLeave"].ToString().ToDecimal(),
					BasicSalary = row["BasicSalary"].ToString().ToDecimal(),
					LeaveDiduction = row["LeaveDiduction"].ToString().ToDecimal(),
					Incentive = row["Incentive"].ToString().ToDecimal(),
					OtherAmnt = row["OtherAmnt"].ToString().ToDecimal(),
					SalaryCut = row["SalaryCut"].ToString().ToDecimal(),
					OtherDiduction = row["OtherDiduction"].ToString().ToDecimal(),
					NetSalary = row["NetSalary"].ToString().ToDecimal(),
					SlryAdvance = row["SlryAdvance"].ToString().ToDecimal(),
					PaidAmnt = row["PaidAmnt"].ToString().ToDecimal(),
					Balance = row["Balance"].ToString().ToDecimal(),
					Remarks = row["Remarks"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<SelectedFYear> GetSelectedFYears(string Query, object entity, string sCon = null)
		{
			List<SelectedFYear> list = new List<SelectedFYear>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				SelectedFYear item = new SelectedFYear
				{
					SlectedFY = row["SlectedFY"].ToString().ToInt32(),
					FyearId = row["FyearId"].ToString().ToInt32()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<SelfEntity> GetSelfs(string Query, object entity, string sCon = null)
		{
			List<SelfEntity> list = new List<SelfEntity>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				SelfEntity item = new SelfEntity
				{
					SelfId = row["SelfId"].ToString().ToInt32(),
					GodownId = row["GodownId"].ToString().ToInt32(),
					SelfName = row["SelfName"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<SettingsEntity> GetSettings(string Query, object entity, string sCon = null)
		{
			List<SettingsEntity> list = new List<SettingsEntity>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				SettingsEntity item = new SettingsEntity
				{
					Id = row["Id"].ToString().ToInt32(),
					Name = row["Name"].ToString(),
					Caption = row["Caption"].ToString(),
					Remarks = row["Remarks"].ToString(),
					BitValue = row["BitValue"].ToString().ToBool(),
					StringValue = row["StringValue"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<StockAdjusterMaster> GetStockAdjusterMasters(string Query, object entity, string sCon = null)
		{
			List<StockAdjusterMaster> list = new List<StockAdjusterMaster>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				StockAdjusterMaster item = new StockAdjusterMaster
				{
					stockAdjusterMasterId = row["stockAdjusterMasterId"].ToString().ToInt32(),
					StockAdjusterMasterCode = row["StockAdjusterMasterCode"].ToString().ToInt32(),
					Date = row["Date"].ToString().toDateTime(),
					Remarks = row["Remarks"].ToString(),
					ProtGroup = row["ProtGroup"].ToString().ToInt32(),
					ProBrand = row["ProBrand"].ToString().ToInt32(),
					FK_BranchId = row["FK_BranchId"].ToString().ToInt32()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<StockAdjustSettings> GetStockAdjustSettings(string Query, object entity, string sCon = null)
		{
			List<StockAdjustSettings> list = new List<StockAdjustSettings>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				StockAdjustSettings item = new StockAdjustSettings
				{
					stockAdjustSettingsId = row["stockAdjustSettingsId"].ToString().ToInt32(),
					stockAdjusterMasterId = row["stockAdjusterMasterId"].ToString().ToInt32(),
					productId = row["productId"].ToString().ToInt32(),
					currSystemStock = row["currSystemStock"].ToString().ToDecimal(),
					curActualStock = row["curActualStock"].ToString().ToDecimal(),
					unitId = row["unitId"].ToString().ToInt32(),
					FK_BatchId = row["FK_BatchId"].ToString().ToInt32(),
					Barcode = row["Barcode"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<StockTranLog> GetStockTranLogs(string Query, object entity, string sCon = null)
		{
			List<StockTranLog> list = new List<StockTranLog>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				StockTranLog item = new StockTranLog
				{
					STranId = row["STranId"].ToString().ToInt32(),
					ProductId = row["ProductId"].ToString().ToInt32(),
					VchrId = row["VchrId"].ToString().ToInt32(),
					VchrNo = row["VchrNo"].ToString().ToInt32(),
					VchrDate = row["VchrDate"].ToString().toDateTime(),
					VchrType = row["VchrType"].ToString(),
					VchrFormType = row["VchrFormType"].ToString(),
					AccountId = row["AccountId"].ToString().ToInt32(),
					InQuantity = row["InQuantity"].ToString().ToDecimal(),
					OutQuantity = row["OutQuantity"].ToString().ToDecimal(),
					UnitId = row["UnitId"].ToString().ToInt32(),
					Narration = row["Narration"].ToString(),
					UserId = row["UserId"].ToString().ToInt32(),
					TranDate = row["TranDate"].ToString().toDateTime(),
					FK_BatchId = row["FK_BatchId"].ToString().ToInt32(),
					FK_BranchId = row["FK_BranchId"].ToString().ToInt32(),
					Barcode = row["Barcode"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<StockTransferMaster> GetStockTransferMasters(string Query, object entity, string sCon = null)
		{
			List<StockTransferMaster> list = new List<StockTransferMaster>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				StockTransferMaster item = new StockTransferMaster
				{
					StockTransferId = row["StockTransferId"].ToString().ToInt32(),
					StockTransferCode = row["StockTransferCode"].ToString().ToInt32(),
					TransferDate = row["TransferDate"].ToString().toDateTime(),
					FromGodownId = row["FromGodownId"].ToString().ToInt32(),
					FromGodownName = row["FromGodownName"].ToString(),
					ToGodownId = row["ToGodownId"].ToString().ToInt32(),
					ToGodownName = row["ToGodownName"].ToString(),
					Remarks = row["Remarks"].ToString(),
					UserId = row["UserId"].ToString().ToInt32(),
					EntryDate = row["EntryDate"].ToString().toDateTime(),
					CompanyName = row["CompanyName"].ToString(),
					CompanyPlace = row["CompanyPlace"].ToString(),
					TotalAmnt = row["TotalAmnt"].ToString().ToDecimal(),
					TotalQty = row["TotalQty"].ToString().ToDecimal(),
					TranType = row["TranType"].ToString(),
					FK_BranchId = row["FK_BranchId"].ToString().ToInt32()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<StockTranSettings> GetStockTranSettings(string Query, object entity, string sCon = null)
		{
			List<StockTranSettings> list = new List<StockTranSettings>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				StockTranSettings item = new StockTranSettings
				{
					StockTranSettingsId = row["StockTranSettingsId"].ToString().ToInt32(),
					StockTransferId = row["StockTransferId"].ToString().ToInt32(),
					ProductId = row["ProductId"].ToString().ToInt32(),
					ProductName = row["ProductName"].ToString(),
					ProQuantity = row["ProQuantity"].ToString().ToDecimal(),
					UnitId = row["UnitId"].ToString().ToInt32(),
					CompanyName = row["CompanyName"].ToString(),
					CompanyPlace = row["CompanyPlace"].ToString(),
					ProRate = row["ProRate"].ToString().ToDecimal(),
					Total = row["Total"].ToString().ToDecimal(),
					FK_BatchId = row["FK_BatchId"].ToString().ToInt32()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<TaxCategoryEntity> GetTaxCategorys(string Query, object entity, string sCon = null)
		{
			List<TaxCategoryEntity> list = new List<TaxCategoryEntity>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				TaxCategoryEntity item = new TaxCategoryEntity
				{
					TCategoryId = row["TCategoryId"].ToString().ToInt32(),
					TCategoryName = row["TCategoryName"].ToString(),
					SalesVat = row["SalesVat"].ToString().ToDecimal(),
					PurVat = row["PurVat"].ToString().ToDecimal(),
					SaleCst = row["SaleCst"].ToString().ToDecimal(),
					PurCst = row["PurCst"].ToString().ToDecimal(),
					SaleCess = row["SaleCess"].ToString().ToDecimal(),
					PurCess = row["PurCess"].ToString().ToDecimal(),
					SalesXDuty = row["SalesXDuty"].ToString().ToDecimal(),
					PurXDuty = row["PurXDuty"].ToString().ToDecimal()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<TransactionLog> GetTransactionLogs(string Query, object entity, string sCon = null)
		{
			List<TransactionLog> list = new List<TransactionLog>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				TransactionLog item = new TransactionLog
				{
					TranId = row["TranId"].ToString().ToInt32(),
					VchrId = row["VchrId"].ToString().ToInt32(),
					VchrNo = row["VchrNo"].ToString().ToInt32(),
					VchrType = row["VchrType"].ToString(),
					VchrDate = row["VchrDate"].ToString().toDateTime(),
					AccountId = row["AccountId"].ToString().ToInt32(),
					OppAccountId = row["OppAccountId"].ToString().ToInt32(),
					DebitAmount = row["DebitAmount"].ToString().ToDecimal(),
					CreditAmount = row["CreditAmount"].ToString().ToDecimal(),
					Narration = row["Narration"].ToString(),
					UserID = row["UserID"].ToString().ToInt32(),
					TranDate = row["TranDate"].ToString().toDateTime(),
					Status = row["Status"].ToString(),
					RefNo = row["RefNo"].ToString(),
					VchrName = row["VchrName"].ToString(),
					FK_BranchId = row["FK_BranchId"].ToString().ToInt32(),
					SequenceNo = row["SequenceNo"].ToString().ToInt32(),
					Terminal = row["Terminal"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<Translator> GetTranslators(string Query, object entity, string sCon = null)
		{
			List<Translator> list = new List<Translator>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				Translator item = new Translator
				{
					Id = row["Id"].ToString().ToInt32(),
					Name = row["Name"].ToString(),
					Value = row["Value"].ToString(),
					Form = row["Form"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<UnitEntity> GetUnits(string Query, object entity, string sCon = null)
		{
			List<UnitEntity> list = new List<UnitEntity>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				UnitEntity item = new UnitEntity
				{
					UnitId = row["UnitId"].ToString().ToInt32(),
					UnitName = row["UnitName"].ToString(),
					UnitNo = row["UnitNo"].ToString().ToInt32()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<User> GetUsers(string Query, object entity, string sCon = null)
		{
			List<User> list = new List<User>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				User item = new User
				{
					UserId = row["UserId"].ToString().ToInt32(),
					PrevilageId = row["PrevilageId"].ToString().ToInt32(),
					UserName = row["UserName"].ToString(),
					PassWord = row["PassWord"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<UserPrevilegeHdr> GetUserPrevilegeHdrs(string Query, object entity, string sCon = null)
		{
			List<UserPrevilegeHdr> list = new List<UserPrevilegeHdr>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				UserPrevilegeHdr item = new UserPrevilegeHdr
				{
					Id = row["Id"].ToString().ToInt32(),
					Name = row["Name"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<UserPrevilegeDtl> GetUserPrevilegeDtls(string Query, object entity, string sCon = null)
		{
			List<UserPrevilegeDtl> list = new List<UserPrevilegeDtl>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				UserPrevilegeDtl item = new UserPrevilegeDtl
				{
					Id = row["Id"].ToString().ToInt32(),
					FK_UserPrevilegeHdrId = row["FK_UserPrevilegeHdrId"].ToString().ToInt32(),
					FK_MenuDetailsId = row["FK_MenuDetailsId"].ToString().ToInt32(),
					Visible = row["Visible"].ToString().ToBool(),
					Enable = row["Enable"].ToString().ToBool(),
					SaveData = row["SaveData"].ToString().ToBool(),
					EditData = row["EditData"].ToString().ToBool(),
					DeleteData = row["DeleteData"].ToString().ToBool()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<VchrTranLog> GetVchrTranLogs(string Query, object entity, string sCon = null)
		{
			List<VchrTranLog> list = new List<VchrTranLog>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				VchrTranLog item = new VchrTranLog
				{
					VchrTranId = row["VchrTranId"].ToString().ToInt32(),
					VchrNo = row["VchrNo"].ToString().ToInt32(),
					VchrDate = row["VchrDate"].ToString().toDateTime(),
					VchrAmount = row["VchrAmount"].ToString().ToDecimal(),
					VchrType = row["VchrType"].ToString(),
					FromAccountId = row["FromAccountId"].ToString().ToInt32(),
					ToAccountId = row["ToAccountId"].ToString().ToInt32(),
					Narration = row["Narration"].ToString(),
					SalesManId = row["SalesManId"].ToString().ToInt32(),
					UserId = row["UserId"].ToString().ToInt32(),
					TranDate = row["TranDate"].ToString().toDateTime(),
					VchrName = row["VchrName"].ToString(),
					FK_BranchId = row["FK_BranchId"].ToString().ToInt32(),
					SequenceNo = row["SequenceNo"].ToString().ToInt32(),
					Terminal = row["Terminal"].ToString()
				};
				list.Add(item);
			}
			return list;
		}

		public static List<VehicleEntity> GetVehicles(string Query, object entity, string sCon = null)
		{
			List<VehicleEntity> list = new List<VehicleEntity>();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query, entity, sCon);
			foreach (DataRow row in dataTable.Rows)
			{
				VehicleEntity vehicleEntity = new VehicleEntity();
				vehicleEntity.VanId = row["VanId"].ToString().ToInt32();
				vehicleEntity.VehName = row["VehName"].ToString();
				vehicleEntity.Manufacturer = row["Manufacturer"].ToString();
				vehicleEntity.Model = row["Model"].ToString();
				vehicleEntity.YearOfModel = row["YearOfModel"].ToString();
				vehicleEntity.RegNo = row["RegNo"].ToString();
				vehicleEntity.Ownership = row["Ownership"].ToString();
				vehicleEntity.RegisterdOwner = row["RegisterdOwner"].ToString();
				vehicleEntity.EngineNo = row["EngineNo"].ToString();
				vehicleEntity.ChasisNo = row["ChasisNo"].ToString();
				string text = row["InsurenceDate"].ToString();
				if (text != string.Empty)
				{
					vehicleEntity.InsurenceDate = text.toDateTime();
				}
				else
				{
					vehicleEntity.InsurenceDate = null;
				}
				string text2 = row["TaxDate"].ToString();
				if (text2 != string.Empty)
				{
					vehicleEntity.TaxDate = text2.toDateTime();
				}
				else
				{
					vehicleEntity.TaxDate = null;
				}
				string text3 = row["PolusionDate"].ToString();
				if (text3 != string.Empty)
				{
					vehicleEntity.PolusionDate = text3.toDateTime();
				}
				else
				{
					vehicleEntity.PolusionDate = null;
				}
				vehicleEntity.BColor = row["BColor"].ToString();
				string text4 = row["LoanEMIDate"].ToString();
				if (text4 != string.Empty)
				{
					vehicleEntity.LoanEMIDate = text4.toDateTime();
				}
				else
				{
					vehicleEntity.LoanEMIDate = null;
				}
				vehicleEntity.LoanEMIAmount = row["LoanEMIAmount"].ToString();
				vehicleEntity.Remarks = row["Remarks"].ToString();
				vehicleEntity.FK_BranchId = row["FK_BranchId"].ToString().ToInt32();
				list.Add(vehicleEntity);
			}
			return list;
		}
	}
}
