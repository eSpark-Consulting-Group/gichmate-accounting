using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Ashpro.POS.DataSet
{
	[Serializable]
	[HelpKeyword("vs.data.DataSet")]
	[XmlRoot("dsPOSPrintWithInvoiceNo")]
	[XmlSchemaProvider("GetTypedDataSetSchema")]
	[DesignerCategory("code")]
	[ToolboxItem(true)]
	public class dsPOSPrintWithInvoiceNo : System.Data.DataSet
	{
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public delegate void dtCompanyRowChangeEventHandler(object sender, dtCompanyRowChangeEvent e);

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public delegate void dtPOSMasterRowChangeEventHandler(object sender, dtPOSMasterRowChangeEvent e);

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public delegate void dtPOSDetailsRowChangeEventHandler(object sender, dtPOSDetailsRowChangeEvent e);

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public delegate void dtPaymentRowChangeEventHandler(object sender, dtPaymentRowChangeEvent e);

		[Serializable]
		[XmlSchemaProvider("GetTypedTableSchema")]
		public class dtCompanyDataTable : TypedTableBase<dtCompanyRow>
		{
			private DataColumn columnLogo;

			private DataColumn columnShopName;

			private DataColumn columnPlace;

			private DataColumn columnAddress;

			private DataColumn columnCountry;

			private DataColumn columnPincod;

			private DataColumn columnPhoneNo;

			private DataColumn columnMobNo;

			private DataColumn columnwebsite;

			private DataColumn columnEmailId;

			private DataColumn columnTaxNumber;

			private DataColumn columnState;

			private DataColumn columnStateCode;

			private DataColumn columnLocalName;
			

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn ShopNameColumn => columnShopName;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn LogoColumn => columnLogo;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn PlaceColumn => columnPlace;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn AddressColumn => columnAddress;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn CountryColumn => columnCountry;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn PincodColumn => columnPincod;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn PhoneNoColumn => columnPhoneNo;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn MobNoColumn => columnMobNo;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn websiteColumn => columnwebsite;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn EmailIdColumn => columnEmailId;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn TaxNumberColumn => columnTaxNumber;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn StateColumn => columnState;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn StateCodeColumn => columnStateCode;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn LocalNameColumn => columnLocalName;

			[Browsable(false)]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public int Count => base.Rows.Count;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public dtCompanyRow this[int index] => (dtCompanyRow)base.Rows[index];

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtCompanyRowChangeEventHandler dtCompanyRowChanging;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtCompanyRowChangeEventHandler dtCompanyRowChanged;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtCompanyRowChangeEventHandler dtCompanyRowDeleting;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtCompanyRowChangeEventHandler dtCompanyRowDeleted;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public dtCompanyDataTable()
			{
				base.TableName = "dtCompany";
				BeginInit();
				InitClass();
				EndInit();
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			internal dtCompanyDataTable(DataTable table)
			{
				base.TableName = table.TableName;
				if (table.CaseSensitive != table.DataSet.CaseSensitive)
				{
					base.CaseSensitive = table.CaseSensitive;
				}
				if (table.Locale.ToString() != table.DataSet.Locale.ToString())
				{
					base.Locale = table.Locale;
				}
				if (table.Namespace != table.DataSet.Namespace)
				{
					base.Namespace = table.Namespace;
				}
				base.Prefix = table.Prefix;
				base.MinimumCapacity = table.MinimumCapacity;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected dtCompanyDataTable(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
				InitVars();
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void AdddtCompanyRow(dtCompanyRow row)
			{
				base.Rows.Add(row);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public dtCompanyRow AdddtCompanyRow(string ShopName, string Place, string Address, string Country, string Pincod, string PhoneNo, string MobNo, string website, string EmailId, string TaxNumber, string State, string StateCode, string LocalName)
			{
				dtCompanyRow dtCompanyRow = (dtCompanyRow)NewRow();
				object[] array2 = (dtCompanyRow.ItemArray = new object[13]
				{
					ShopName, Place, Address, Country, Pincod, PhoneNo, MobNo, website, EmailId, TaxNumber,
					State, StateCode, LocalName
				});
				base.Rows.Add(dtCompanyRow);
				return dtCompanyRow;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public override DataTable Clone()
			{
				dtCompanyDataTable dtCompanyDataTable = (dtCompanyDataTable)base.Clone();
				dtCompanyDataTable.InitVars();
				return dtCompanyDataTable;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override DataTable CreateInstance()
			{
				return new dtCompanyDataTable();
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			internal void InitVars()
			{
				columnLogo = base.Columns["Logo"];
				columnShopName = base.Columns["ShopName"];
				columnPlace = base.Columns["Place"];
				columnAddress = base.Columns["Address"];
				columnCountry = base.Columns["Country"];
				columnPincod = base.Columns["Pincod"];
				columnPhoneNo = base.Columns["PhoneNo"];
				columnMobNo = base.Columns["MobNo"];
				columnwebsite = base.Columns["website"];
				columnEmailId = base.Columns["EmailId"];
				columnTaxNumber = base.Columns["TaxNumber"];
				columnState = base.Columns["State"];
				columnStateCode = base.Columns["StateCode"];
				columnLocalName = base.Columns["LocalName"];
				
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			private void InitClass()
			{
				columnLogo = new DataColumn("Logo", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnLogo);
				columnShopName = new DataColumn("ShopName", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnShopName);
				columnPlace = new DataColumn("Place", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnPlace);
				columnAddress = new DataColumn("Address", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnAddress);
				columnCountry = new DataColumn("Country", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnCountry);
				columnPincod = new DataColumn("Pincod", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnPincod);
				columnPhoneNo = new DataColumn("PhoneNo", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnPhoneNo);
				columnMobNo = new DataColumn("MobNo", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnMobNo);
				columnwebsite = new DataColumn("website", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnwebsite);
				columnEmailId = new DataColumn("EmailId", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnEmailId);
				columnTaxNumber = new DataColumn("TaxNumber", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnTaxNumber);
				columnState = new DataColumn("State", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnState);
				columnStateCode = new DataColumn("StateCode", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnStateCode);
				columnLocalName = new DataColumn("LocalName", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnLocalName);
				
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtCompanyRow NewdtCompanyRow()
			{
				return (dtCompanyRow)NewRow();
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new dtCompanyRow(builder);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override Type GetRowType()
			{
				return typeof(dtCompanyRow);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this.dtCompanyRowChanged != null)
				{
					this.dtCompanyRowChanged(this, new dtCompanyRowChangeEvent((dtCompanyRow)e.Row, e.Action));
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this.dtCompanyRowChanging != null)
				{
					this.dtCompanyRowChanging(this, new dtCompanyRowChangeEvent((dtCompanyRow)e.Row, e.Action));
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.dtCompanyRowDeleted != null)
				{
					this.dtCompanyRowDeleted(this, new dtCompanyRowChangeEvent((dtCompanyRow)e.Row, e.Action));
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this.dtCompanyRowDeleting != null)
				{
					this.dtCompanyRowDeleting(this, new dtCompanyRowChangeEvent((dtCompanyRow)e.Row, e.Action));
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void RemovedtCompanyRow(dtCompanyRow row)
			{
				base.Rows.Remove(row);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				dsPOSPrintWithInvoiceNo dsPOSPrintWithInvoiceNo2 = new dsPOSPrintWithInvoiceNo();
				XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
				xmlSchemaAny.Namespace = "http://www.w3.org/2001/XMLSchema";
				xmlSchemaAny.MinOccurs = 0m;
				xmlSchemaAny.MaxOccurs = decimal.MaxValue;
				xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny);
				XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
				xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
				xmlSchemaAny2.MinOccurs = 1m;
				xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny2);
				XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
				xmlSchemaAttribute.Name = "namespace";
				xmlSchemaAttribute.FixedValue = dsPOSPrintWithInvoiceNo2.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "dtCompanyDataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = dsPOSPrintWithInvoiceNo2.GetSchemaSerializable();
				if (xs.Contains(schemaSerializable.TargetNamespace))
				{
					MemoryStream memoryStream = new MemoryStream();
					MemoryStream memoryStream2 = new MemoryStream();
					try
					{
						XmlSchema xmlSchema = null;
						schemaSerializable.Write(memoryStream);
						IEnumerator enumerator = xs.Schemas(schemaSerializable.TargetNamespace).GetEnumerator();
						while (enumerator.MoveNext())
						{
							xmlSchema = (XmlSchema)enumerator.Current;
							memoryStream2.SetLength(0L);
							xmlSchema.Write(memoryStream2);
							if (memoryStream.Length == memoryStream2.Length)
							{
								memoryStream.Position = 0L;
								memoryStream2.Position = 0L;
								while (memoryStream.Position != memoryStream.Length && memoryStream.ReadByte() == memoryStream2.ReadByte())
								{
								}
								if (memoryStream.Position == memoryStream.Length)
								{
									return xmlSchemaComplexType;
								}
							}
						}
					}
					finally
					{
						memoryStream?.Close();
						memoryStream2?.Close();
					}
				}
				xs.Add(schemaSerializable);
				return xmlSchemaComplexType;
			}
		}

		[Serializable]
		[XmlSchemaProvider("GetTypedTableSchema")]
		public class dtPOSMasterDataTable : TypedTableBase<dtPOSMasterRow>
		{
			private DataColumn columnSalesInvNo;

			private DataColumn columnInvType;

			private DataColumn columnSalesDate;

			private DataColumn columnSalesAccount;

			private DataColumn columnPartyAccount;

			private DataColumn columnSalesMan;

			private DataColumn columnAditionalAmount;

			private DataColumn columnFrieght;

			private DataColumn columnTotalDiscount;

			private DataColumn columnBillDiscount;

			private DataColumn columnGrandTotal;

			private DataColumn columnJEName;

			private DataColumn columnEntryDate;

			private DataColumn columnRemarks;

			private DataColumn columnIGSTAmount;

			private DataColumn columnSGSTAmount;

			private DataColumn columnCGSTAmount;

			private DataColumn columnTaxableAmount;

			private DataColumn columnFinancialyear;

			private DataColumn columnFloodCess;

			private DataColumn columnTotalTaxAmount;

			private DataColumn columnBranchName;

			private DataColumn columnCntrInvNo;

			private DataColumn columnSequenceNo;

			private DataColumn columnCashPayment;

			private DataColumn columnCardPayment;

			private DataColumn columnCredit;

			private DataColumn columnTerminal;

			private DataColumn columnFreeAmount;

			private DataColumn columnReturnAmount;

			private DataColumn columnOtherPayment;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn SalesInvNoColumn => columnSalesInvNo;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn InvTypeColumn => columnInvType;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn SalesDateColumn => columnSalesDate;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn SalesAccountColumn => columnSalesAccount;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn PartyAccountColumn => columnPartyAccount;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn SalesManColumn => columnSalesMan;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn AditionalAmountColumn => columnAditionalAmount;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn FrieghtColumn => columnFrieght;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn TotalDiscountColumn => columnTotalDiscount;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn BillDiscountColumn => columnBillDiscount;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn GrandTotalColumn => columnGrandTotal;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn JENameColumn => columnJEName;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn EntryDateColumn => columnEntryDate;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn RemarksColumn => columnRemarks;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn IGSTAmountColumn => columnIGSTAmount;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn SGSTAmountColumn => columnSGSTAmount;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn CGSTAmountColumn => columnCGSTAmount;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn TaxableAmountColumn => columnTaxableAmount;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn FinancialyearColumn => columnFinancialyear;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn FloodCessColumn => columnFloodCess;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn TotalTaxAmountColumn => columnTotalTaxAmount;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn BranchNameColumn => columnBranchName;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn CntrInvNoColumn => columnCntrInvNo;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn SequenceNoColumn => columnSequenceNo;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn CashPaymentColumn => columnCashPayment;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn CardPaymentColumn => columnCardPayment;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn CreditColumn => columnCredit;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn TerminalColumn => columnTerminal;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn FreeAmountColumn => columnFreeAmount;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn ReturnAmountColumn => columnReturnAmount;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn OtherPaymentColumn => columnOtherPayment;

			[DebuggerNonUserCode]
			[Browsable(false)]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public int Count => base.Rows.Count;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtPOSMasterRow this[int index] => (dtPOSMasterRow)base.Rows[index];

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtPOSMasterRowChangeEventHandler dtPOSMasterRowChanging;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtPOSMasterRowChangeEventHandler dtPOSMasterRowChanged;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtPOSMasterRowChangeEventHandler dtPOSMasterRowDeleting;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtPOSMasterRowChangeEventHandler dtPOSMasterRowDeleted;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtPOSMasterDataTable()
			{
				base.TableName = "dtPOSMaster";
				BeginInit();
				InitClass();
				EndInit();
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			internal dtPOSMasterDataTable(DataTable table)
			{
				base.TableName = table.TableName;
				if (table.CaseSensitive != table.DataSet.CaseSensitive)
				{
					base.CaseSensitive = table.CaseSensitive;
				}
				if (table.Locale.ToString() != table.DataSet.Locale.ToString())
				{
					base.Locale = table.Locale;
				}
				if (table.Namespace != table.DataSet.Namespace)
				{
					base.Namespace = table.Namespace;
				}
				base.Prefix = table.Prefix;
				base.MinimumCapacity = table.MinimumCapacity;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected dtPOSMasterDataTable(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
				InitVars();
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void AdddtPOSMasterRow(dtPOSMasterRow row)
			{
				base.Rows.Add(row);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtPOSMasterRow AdddtPOSMasterRow(string SalesInvNo, string InvType, DateTime SalesDate, string SalesAccount, string PartyAccount, string SalesMan, string AditionalAmount, string Frieght, string TotalDiscount, string BillDiscount, string GrandTotal, string JEName, string EntryDate, string Remarks, string IGSTAmount, string SGSTAmount, string CGSTAmount, string TaxableAmount, string Financialyear, string FloodCess, string TotalTaxAmount, string BranchName, string CntrInvNo, string SequenceNo, decimal CashPayment, decimal CardPayment, decimal Credit, string Terminal, decimal FreeAmount, decimal ReturnAmount, string OtherPayment)
			{
				dtPOSMasterRow dtPOSMasterRow = (dtPOSMasterRow)NewRow();
				object[] array2 = (dtPOSMasterRow.ItemArray = new object[31]
				{
					SalesInvNo, InvType, SalesDate, SalesAccount, PartyAccount, SalesMan, AditionalAmount, Frieght, TotalDiscount, BillDiscount,
					GrandTotal, JEName, EntryDate, Remarks, IGSTAmount, SGSTAmount, CGSTAmount, TaxableAmount, Financialyear, FloodCess,
					TotalTaxAmount, BranchName, CntrInvNo, SequenceNo, CashPayment, CardPayment, Credit, Terminal, FreeAmount, ReturnAmount,
					OtherPayment
				});
				base.Rows.Add(dtPOSMasterRow);
				return dtPOSMasterRow;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public override DataTable Clone()
			{
				dtPOSMasterDataTable dtPOSMasterDataTable = (dtPOSMasterDataTable)base.Clone();
				dtPOSMasterDataTable.InitVars();
				return dtPOSMasterDataTable;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override DataTable CreateInstance()
			{
				return new dtPOSMasterDataTable();
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			internal void InitVars()
			{
				columnSalesInvNo = base.Columns["SalesInvNo"];
				columnInvType = base.Columns["InvType"];
				columnSalesDate = base.Columns["SalesDate"];
				columnSalesAccount = base.Columns["SalesAccount"];
				columnPartyAccount = base.Columns["PartyAccount"];
				columnSalesMan = base.Columns["SalesMan"];
				columnAditionalAmount = base.Columns["AditionalAmount"];
				columnFrieght = base.Columns["Frieght"];
				columnTotalDiscount = base.Columns["TotalDiscount"];
				columnBillDiscount = base.Columns["BillDiscount"];
				columnGrandTotal = base.Columns["GrandTotal"];
				columnJEName = base.Columns["JEName"];
				columnEntryDate = base.Columns["EntryDate"];
				columnRemarks = base.Columns["Remarks"];
				columnIGSTAmount = base.Columns["IGSTAmount"];
				columnSGSTAmount = base.Columns["SGSTAmount"];
				columnCGSTAmount = base.Columns["CGSTAmount"];
				columnTaxableAmount = base.Columns["TaxableAmount"];
				columnFinancialyear = base.Columns["Financialyear"];
				columnFloodCess = base.Columns["FloodCess"];
				columnTotalTaxAmount = base.Columns["TotalTaxAmount"];
				columnBranchName = base.Columns["BranchName"];
				columnCntrInvNo = base.Columns["CntrInvNo"];
				columnSequenceNo = base.Columns["SequenceNo"];
				columnCashPayment = base.Columns["CashPayment"];
				columnCardPayment = base.Columns["CardPayment"];
				columnCredit = base.Columns["Credit"];
				columnTerminal = base.Columns["Terminal"];
				columnFreeAmount = base.Columns["FreeAmount"];
				columnReturnAmount = base.Columns["ReturnAmount"];
				columnOtherPayment = base.Columns["OtherPayment"];
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			private void InitClass()
			{
				columnSalesInvNo = new DataColumn("SalesInvNo", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnSalesInvNo);
				columnInvType = new DataColumn("InvType", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnInvType);
				columnSalesDate = new DataColumn("SalesDate", typeof(DateTime), null, MappingType.Element);
				base.Columns.Add(columnSalesDate);
				columnSalesAccount = new DataColumn("SalesAccount", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnSalesAccount);
				columnPartyAccount = new DataColumn("PartyAccount", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnPartyAccount);
				columnSalesMan = new DataColumn("SalesMan", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnSalesMan);
				columnAditionalAmount = new DataColumn("AditionalAmount", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnAditionalAmount);
				columnFrieght = new DataColumn("Frieght", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnFrieght);
				columnTotalDiscount = new DataColumn("TotalDiscount", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnTotalDiscount);
				columnBillDiscount = new DataColumn("BillDiscount", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnBillDiscount);
				columnGrandTotal = new DataColumn("GrandTotal", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnGrandTotal);
				columnJEName = new DataColumn("JEName", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnJEName);
				columnEntryDate = new DataColumn("EntryDate", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnEntryDate);
				columnRemarks = new DataColumn("Remarks", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnRemarks);
				columnIGSTAmount = new DataColumn("IGSTAmount", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnIGSTAmount);
				columnSGSTAmount = new DataColumn("SGSTAmount", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnSGSTAmount);
				columnCGSTAmount = new DataColumn("CGSTAmount", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnCGSTAmount);
				columnTaxableAmount = new DataColumn("TaxableAmount", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnTaxableAmount);
				columnFinancialyear = new DataColumn("Financialyear", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnFinancialyear);
				columnFloodCess = new DataColumn("FloodCess", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnFloodCess);
				columnTotalTaxAmount = new DataColumn("TotalTaxAmount", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnTotalTaxAmount);
				columnBranchName = new DataColumn("BranchName", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnBranchName);
				columnCntrInvNo = new DataColumn("CntrInvNo", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnCntrInvNo);
				columnSequenceNo = new DataColumn("SequenceNo", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnSequenceNo);
				columnCashPayment = new DataColumn("CashPayment", typeof(decimal), null, MappingType.Element);
				base.Columns.Add(columnCashPayment);
				columnCardPayment = new DataColumn("CardPayment", typeof(decimal), null, MappingType.Element);
				base.Columns.Add(columnCardPayment);
				columnCredit = new DataColumn("Credit", typeof(decimal), null, MappingType.Element);
				base.Columns.Add(columnCredit);
				columnTerminal = new DataColumn("Terminal", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnTerminal);
				columnFreeAmount = new DataColumn("FreeAmount", typeof(decimal), null, MappingType.Element);
				base.Columns.Add(columnFreeAmount);
				columnReturnAmount = new DataColumn("ReturnAmount", typeof(decimal), null, MappingType.Element);
				base.Columns.Add(columnReturnAmount);
				columnOtherPayment = new DataColumn("OtherPayment", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnOtherPayment);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public dtPOSMasterRow NewdtPOSMasterRow()
			{
				return (dtPOSMasterRow)NewRow();
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new dtPOSMasterRow(builder);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override Type GetRowType()
			{
				return typeof(dtPOSMasterRow);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this.dtPOSMasterRowChanged != null)
				{
					this.dtPOSMasterRowChanged(this, new dtPOSMasterRowChangeEvent((dtPOSMasterRow)e.Row, e.Action));
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this.dtPOSMasterRowChanging != null)
				{
					this.dtPOSMasterRowChanging(this, new dtPOSMasterRowChangeEvent((dtPOSMasterRow)e.Row, e.Action));
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.dtPOSMasterRowDeleted != null)
				{
					this.dtPOSMasterRowDeleted(this, new dtPOSMasterRowChangeEvent((dtPOSMasterRow)e.Row, e.Action));
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this.dtPOSMasterRowDeleting != null)
				{
					this.dtPOSMasterRowDeleting(this, new dtPOSMasterRowChangeEvent((dtPOSMasterRow)e.Row, e.Action));
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void RemovedtPOSMasterRow(dtPOSMasterRow row)
			{
				base.Rows.Remove(row);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				dsPOSPrintWithInvoiceNo dsPOSPrintWithInvoiceNo2 = new dsPOSPrintWithInvoiceNo();
				XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
				xmlSchemaAny.Namespace = "http://www.w3.org/2001/XMLSchema";
				xmlSchemaAny.MinOccurs = 0m;
				xmlSchemaAny.MaxOccurs = decimal.MaxValue;
				xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny);
				XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
				xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
				xmlSchemaAny2.MinOccurs = 1m;
				xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny2);
				XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
				xmlSchemaAttribute.Name = "namespace";
				xmlSchemaAttribute.FixedValue = dsPOSPrintWithInvoiceNo2.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "dtPOSMasterDataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = dsPOSPrintWithInvoiceNo2.GetSchemaSerializable();
				if (xs.Contains(schemaSerializable.TargetNamespace))
				{
					MemoryStream memoryStream = new MemoryStream();
					MemoryStream memoryStream2 = new MemoryStream();
					try
					{
						XmlSchema xmlSchema = null;
						schemaSerializable.Write(memoryStream);
						IEnumerator enumerator = xs.Schemas(schemaSerializable.TargetNamespace).GetEnumerator();
						while (enumerator.MoveNext())
						{
							xmlSchema = (XmlSchema)enumerator.Current;
							memoryStream2.SetLength(0L);
							xmlSchema.Write(memoryStream2);
							if (memoryStream.Length == memoryStream2.Length)
							{
								memoryStream.Position = 0L;
								memoryStream2.Position = 0L;
								while (memoryStream.Position != memoryStream.Length && memoryStream.ReadByte() == memoryStream2.ReadByte())
								{
								}
								if (memoryStream.Position == memoryStream.Length)
								{
									return xmlSchemaComplexType;
								}
							}
						}
					}
					finally
					{
						memoryStream?.Close();
						memoryStream2?.Close();
					}
				}
				xs.Add(schemaSerializable);
				return xmlSchemaComplexType;
			}
		}

		[Serializable]
		[XmlSchemaProvider("GetTypedTableSchema")]
		public class dtPOSDetailsDataTable : TypedTableBase<dtPOSDetailsRow>
		{
			private DataColumn columnSL;

			private DataColumn columnProductCode;

			private DataColumn columnproductId;

			private DataColumn columnProductName;

			private DataColumn columnBrandName; // newly added

			private DataColumn columnBarcode;

			private DataColumn columnQty;

			private DataColumn columnUnitName;

			private DataColumn columnUnitId;

			private DataColumn columnFreeQty;

			private DataColumn columnRate;

			private DataColumn columnGross;

			private DataColumn columnDiscount;

			private DataColumn columnDisAmount;

			private DataColumn columnTaxable;

			private DataColumn columnTax;

			private DataColumn columnTaxAmount;

			private DataColumn columnNetAmount;

			private DataColumn columnLocalName;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn SLColumn => columnSL;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn ProductCodeColumn => columnProductCode;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn productIdColumn => columnproductId;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn ProductNameColumn => columnProductName;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn BrandNameColumn => columnBrandName;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn BarcodeColumn => columnBarcode;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn QtyColumn => columnQty;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn UnitNameColumn => columnUnitName;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn UnitIdColumn => columnUnitId;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn FreeQtyColumn => columnFreeQty;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn RateColumn => columnRate;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn GrossColumn => columnGross;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn DiscountColumn => columnDiscount;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn DisAmountColumn => columnDisAmount;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn TaxableColumn => columnTaxable;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn TaxColumn => columnTax;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn TaxAmountColumn => columnTaxAmount;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn NetAmountColumn => columnNetAmount;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn LocalNameColumn => columnLocalName;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[Browsable(false)]
			[DebuggerNonUserCode]
			public int Count => base.Rows.Count;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtPOSDetailsRow this[int index] => (dtPOSDetailsRow)base.Rows[index];

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtPOSDetailsRowChangeEventHandler dtPOSDetailsRowChanging;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtPOSDetailsRowChangeEventHandler dtPOSDetailsRowChanged;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtPOSDetailsRowChangeEventHandler dtPOSDetailsRowDeleting;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtPOSDetailsRowChangeEventHandler dtPOSDetailsRowDeleted;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public dtPOSDetailsDataTable()
			{
				base.TableName = "dtPOSDetails";
				BeginInit();
				InitClass();
				EndInit();
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			internal dtPOSDetailsDataTable(DataTable table)
			{
				base.TableName = table.TableName;
				if (table.CaseSensitive != table.DataSet.CaseSensitive)
				{
					base.CaseSensitive = table.CaseSensitive;
				}
				if (table.Locale.ToString() != table.DataSet.Locale.ToString())
				{
					base.Locale = table.Locale;
				}
				if (table.Namespace != table.DataSet.Namespace)
				{
					base.Namespace = table.Namespace;
				}
				base.Prefix = table.Prefix;
				base.MinimumCapacity = table.MinimumCapacity;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected dtPOSDetailsDataTable(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
				InitVars();
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void AdddtPOSDetailsRow(dtPOSDetailsRow row)
			{
				base.Rows.Add(row);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtPOSDetailsRow AdddtPOSDetailsRow(string SL, string ProductCode, string productId, string ProductName, string Barcode, string Qty, string UnitName, string UnitId, string FreeQty, string Rate, string Gross, string Discount, string DisAmount, string Taxable, string Tax, string TaxAmount, string NetAmount, string LocalName)
			{
				dtPOSDetailsRow dtPOSDetailsRow = (dtPOSDetailsRow)NewRow();
				object[] array2 = (dtPOSDetailsRow.ItemArray = new object[18]
				{
					SL, ProductCode, productId, ProductName, Barcode, Qty, UnitName, UnitId, FreeQty, Rate,
					Gross, Discount, DisAmount, Taxable, Tax, TaxAmount, NetAmount, LocalName
				});
				base.Rows.Add(dtPOSDetailsRow);
				return dtPOSDetailsRow;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public override DataTable Clone()
			{
				dtPOSDetailsDataTable dtPOSDetailsDataTable = (dtPOSDetailsDataTable)base.Clone();
				dtPOSDetailsDataTable.InitVars();
				return dtPOSDetailsDataTable;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataTable CreateInstance()
			{
				return new dtPOSDetailsDataTable();
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			internal void InitVars()
			{
				columnSL = base.Columns["SL"];
				columnProductCode = base.Columns["ProductCode"];
				columnproductId = base.Columns["productId"];
				columnProductName = base.Columns["ProductName"];
				columnBrandName = base.Columns["BrandName"];
				columnBarcode = base.Columns["Barcode"];
				columnQty = base.Columns["Qty"];
				columnUnitName = base.Columns["UnitName"];
				columnUnitId = base.Columns["UnitId"];
				columnFreeQty = base.Columns["FreeQty"];
				columnRate = base.Columns["Rate"];
				columnGross = base.Columns["Gross"];
				columnDiscount = base.Columns["Discount"];
				columnDisAmount = base.Columns["DisAmount"];
				columnTaxable = base.Columns["Taxable"];
				columnTax = base.Columns["Tax"];
				columnTaxAmount = base.Columns["TaxAmount"];
				columnNetAmount = base.Columns["NetAmount"];
				columnLocalName = base.Columns["LocalName"];
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			private void InitClass()
			{
				columnSL = new DataColumn("SL", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnSL);
				columnProductCode = new DataColumn("ProductCode", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnProductCode);
				columnproductId = new DataColumn("productId", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnproductId);
				columnProductName = new DataColumn("ProductName", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnProductName);
				columnBrandName = new DataColumn("BrandName", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnBrandName);
				columnBarcode = new DataColumn("Barcode", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnBarcode);
				columnQty = new DataColumn("Qty", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnQty);
				columnUnitName = new DataColumn("UnitName", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnUnitName);
				columnUnitId = new DataColumn("UnitId", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnUnitId);
				columnFreeQty = new DataColumn("FreeQty", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnFreeQty);
				columnRate = new DataColumn("Rate", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnRate);
				columnGross = new DataColumn("Gross", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnGross);
				columnDiscount = new DataColumn("Discount", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnDiscount);
				columnDisAmount = new DataColumn("DisAmount", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnDisAmount);
				columnTaxable = new DataColumn("Taxable", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnTaxable);
				columnTax = new DataColumn("Tax", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnTax);
				columnTaxAmount = new DataColumn("TaxAmount", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnTaxAmount);
				columnNetAmount = new DataColumn("NetAmount", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnNetAmount);
				columnLocalName = new DataColumn("LocalName", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnLocalName);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public dtPOSDetailsRow NewdtPOSDetailsRow()
			{
				return (dtPOSDetailsRow)NewRow();
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new dtPOSDetailsRow(builder);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override Type GetRowType()
			{
				return typeof(dtPOSDetailsRow);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this.dtPOSDetailsRowChanged != null)
				{
					this.dtPOSDetailsRowChanged(this, new dtPOSDetailsRowChangeEvent((dtPOSDetailsRow)e.Row, e.Action));
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this.dtPOSDetailsRowChanging != null)
				{
					this.dtPOSDetailsRowChanging(this, new dtPOSDetailsRowChangeEvent((dtPOSDetailsRow)e.Row, e.Action));
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.dtPOSDetailsRowDeleted != null)
				{
					this.dtPOSDetailsRowDeleted(this, new dtPOSDetailsRowChangeEvent((dtPOSDetailsRow)e.Row, e.Action));
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this.dtPOSDetailsRowDeleting != null)
				{
					this.dtPOSDetailsRowDeleting(this, new dtPOSDetailsRowChangeEvent((dtPOSDetailsRow)e.Row, e.Action));
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void RemovedtPOSDetailsRow(dtPOSDetailsRow row)
			{
				base.Rows.Remove(row);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				dsPOSPrintWithInvoiceNo dsPOSPrintWithInvoiceNo2 = new dsPOSPrintWithInvoiceNo();
				XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
				xmlSchemaAny.Namespace = "http://www.w3.org/2001/XMLSchema";
				xmlSchemaAny.MinOccurs = 0m;
				xmlSchemaAny.MaxOccurs = decimal.MaxValue;
				xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny);
				XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
				xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
				xmlSchemaAny2.MinOccurs = 1m;
				xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny2);
				XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
				xmlSchemaAttribute.Name = "namespace";
				xmlSchemaAttribute.FixedValue = dsPOSPrintWithInvoiceNo2.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "dtPOSDetailsDataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = dsPOSPrintWithInvoiceNo2.GetSchemaSerializable();
				if (xs.Contains(schemaSerializable.TargetNamespace))
				{
					MemoryStream memoryStream = new MemoryStream();
					MemoryStream memoryStream2 = new MemoryStream();
					try
					{
						XmlSchema xmlSchema = null;
						schemaSerializable.Write(memoryStream);
						IEnumerator enumerator = xs.Schemas(schemaSerializable.TargetNamespace).GetEnumerator();
						while (enumerator.MoveNext())
						{
							xmlSchema = (XmlSchema)enumerator.Current;
							memoryStream2.SetLength(0L);
							xmlSchema.Write(memoryStream2);
							if (memoryStream.Length == memoryStream2.Length)
							{
								memoryStream.Position = 0L;
								memoryStream2.Position = 0L;
								while (memoryStream.Position != memoryStream.Length && memoryStream.ReadByte() == memoryStream2.ReadByte())
								{
								}
								if (memoryStream.Position == memoryStream.Length)
								{
									return xmlSchemaComplexType;
								}
							}
						}
					}
					finally
					{
						memoryStream?.Close();
						memoryStream2?.Close();
					}
				}
				xs.Add(schemaSerializable);
				return xmlSchemaComplexType;
			}
		}

		[Serializable]
		[XmlSchemaProvider("GetTypedTableSchema")]
		public class dtPaymentDataTable : TypedTableBase<dtPaymentRow>
		{
			private DataColumn columnName;

			private DataColumn columnValue;

			private DataColumn columnRightLabel;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn NameColumn => columnName;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn ValueColumn => columnValue;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn RightLabelColumn => columnRightLabel;

			[Browsable(false)]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public int Count => base.Rows.Count;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public dtPaymentRow this[int index] => (dtPaymentRow)base.Rows[index];

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtPaymentRowChangeEventHandler dtPaymentRowChanging;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtPaymentRowChangeEventHandler dtPaymentRowChanged;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtPaymentRowChangeEventHandler dtPaymentRowDeleting;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtPaymentRowChangeEventHandler dtPaymentRowDeleted;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtPaymentDataTable()
			{
				base.TableName = "dtPayment";
				BeginInit();
				InitClass();
				EndInit();
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			internal dtPaymentDataTable(DataTable table)
			{
				base.TableName = table.TableName;
				if (table.CaseSensitive != table.DataSet.CaseSensitive)
				{
					base.CaseSensitive = table.CaseSensitive;
				}
				if (table.Locale.ToString() != table.DataSet.Locale.ToString())
				{
					base.Locale = table.Locale;
				}
				if (table.Namespace != table.DataSet.Namespace)
				{
					base.Namespace = table.Namespace;
				}
				base.Prefix = table.Prefix;
				base.MinimumCapacity = table.MinimumCapacity;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected dtPaymentDataTable(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
				InitVars();
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void AdddtPaymentRow(dtPaymentRow row)
			{
				base.Rows.Add(row);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtPaymentRow AdddtPaymentRow(string Name, string Value, string RightLabel)
			{
				dtPaymentRow dtPaymentRow = (dtPaymentRow)NewRow();
				object[] array2 = (dtPaymentRow.ItemArray = new object[3] { Name, Value, RightLabel });
				base.Rows.Add(dtPaymentRow);
				return dtPaymentRow;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public override DataTable Clone()
			{
				dtPaymentDataTable dtPaymentDataTable = (dtPaymentDataTable)base.Clone();
				dtPaymentDataTable.InitVars();
				return dtPaymentDataTable;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataTable CreateInstance()
			{
				return new dtPaymentDataTable();
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			internal void InitVars()
			{
				columnName = base.Columns["Name"];
				columnValue = base.Columns["Value"];
				columnRightLabel = base.Columns["RightLabel"];
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			private void InitClass()
			{
				columnName = new DataColumn("Name", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnName);
				columnValue = new DataColumn("Value", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnValue);
				columnRightLabel = new DataColumn("RightLabel", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnRightLabel);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public dtPaymentRow NewdtPaymentRow()
			{
				return (dtPaymentRow)NewRow();
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new dtPaymentRow(builder);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override Type GetRowType()
			{
				return typeof(dtPaymentRow);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this.dtPaymentRowChanged != null)
				{
					this.dtPaymentRowChanged(this, new dtPaymentRowChangeEvent((dtPaymentRow)e.Row, e.Action));
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this.dtPaymentRowChanging != null)
				{
					this.dtPaymentRowChanging(this, new dtPaymentRowChangeEvent((dtPaymentRow)e.Row, e.Action));
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.dtPaymentRowDeleted != null)
				{
					this.dtPaymentRowDeleted(this, new dtPaymentRowChangeEvent((dtPaymentRow)e.Row, e.Action));
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this.dtPaymentRowDeleting != null)
				{
					this.dtPaymentRowDeleting(this, new dtPaymentRowChangeEvent((dtPaymentRow)e.Row, e.Action));
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void RemovedtPaymentRow(dtPaymentRow row)
			{
				base.Rows.Remove(row);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				dsPOSPrintWithInvoiceNo dsPOSPrintWithInvoiceNo2 = new dsPOSPrintWithInvoiceNo();
				XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
				xmlSchemaAny.Namespace = "http://www.w3.org/2001/XMLSchema";
				xmlSchemaAny.MinOccurs = 0m;
				xmlSchemaAny.MaxOccurs = decimal.MaxValue;
				xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny);
				XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
				xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
				xmlSchemaAny2.MinOccurs = 1m;
				xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny2);
				XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
				xmlSchemaAttribute.Name = "namespace";
				xmlSchemaAttribute.FixedValue = dsPOSPrintWithInvoiceNo2.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "dtPaymentDataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = dsPOSPrintWithInvoiceNo2.GetSchemaSerializable();
				if (xs.Contains(schemaSerializable.TargetNamespace))
				{
					MemoryStream memoryStream = new MemoryStream();
					MemoryStream memoryStream2 = new MemoryStream();
					try
					{
						XmlSchema xmlSchema = null;
						schemaSerializable.Write(memoryStream);
						IEnumerator enumerator = xs.Schemas(schemaSerializable.TargetNamespace).GetEnumerator();
						while (enumerator.MoveNext())
						{
							xmlSchema = (XmlSchema)enumerator.Current;
							memoryStream2.SetLength(0L);
							xmlSchema.Write(memoryStream2);
							if (memoryStream.Length == memoryStream2.Length)
							{
								memoryStream.Position = 0L;
								memoryStream2.Position = 0L;
								while (memoryStream.Position != memoryStream.Length && memoryStream.ReadByte() == memoryStream2.ReadByte())
								{
								}
								if (memoryStream.Position == memoryStream.Length)
								{
									return xmlSchemaComplexType;
								}
							}
						}
					}
					finally
					{
						memoryStream?.Close();
						memoryStream2?.Close();
					}
				}
				xs.Add(schemaSerializable);
				return xmlSchemaComplexType;
			}
		}

		public class dtCompanyRow : DataRow
		{
			private dtCompanyDataTable tabledtCompany;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string ShopName
			{
				get
				{
					try
					{
						return (string)base[tabledtCompany.ShopNameColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'ShopName' in table 'dtCompany' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtCompany.ShopNameColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string Place
			{
				get
				{
					try
					{
						return (string)base[tabledtCompany.PlaceColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Place' in table 'dtCompany' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtCompany.PlaceColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string Address
			{
				get
				{
					try
					{
						return (string)base[tabledtCompany.AddressColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Address' in table 'dtCompany' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtCompany.AddressColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string Country
			{
				get
				{
					try
					{
						return (string)base[tabledtCompany.CountryColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Country' in table 'dtCompany' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtCompany.CountryColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string Pincod
			{
				get
				{
					try
					{
						return (string)base[tabledtCompany.PincodColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Pincod' in table 'dtCompany' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtCompany.PincodColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string PhoneNo
			{
				get
				{
					try
					{
						return (string)base[tabledtCompany.PhoneNoColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'PhoneNo' in table 'dtCompany' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtCompany.PhoneNoColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string MobNo
			{
				get
				{
					try
					{
						return (string)base[tabledtCompany.MobNoColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'MobNo' in table 'dtCompany' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtCompany.MobNoColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string website
			{
				get
				{
					try
					{
						return (string)base[tabledtCompany.websiteColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'website' in table 'dtCompany' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtCompany.websiteColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string EmailId
			{
				get
				{
					try
					{
						return (string)base[tabledtCompany.EmailIdColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'EmailId' in table 'dtCompany' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtCompany.EmailIdColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string TaxNumber
			{
				get
				{
					try
					{
						return (string)base[tabledtCompany.TaxNumberColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'TaxNumber' in table 'dtCompany' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtCompany.TaxNumberColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string State
			{
				get
				{
					try
					{
						return (string)base[tabledtCompany.StateColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'State' in table 'dtCompany' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtCompany.StateColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string StateCode
			{
				get
				{
					try
					{
						return (string)base[tabledtCompany.StateCodeColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'StateCode' in table 'dtCompany' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtCompany.StateCodeColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string LocalName
			{
				get
				{
					try
					{
						return (string)base[tabledtCompany.LocalNameColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'LocalName' in table 'dtCompany' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtCompany.LocalNameColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			internal dtCompanyRow(DataRowBuilder rb)
				: base(rb)
			{
				tabledtCompany = (dtCompanyDataTable)base.Table;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsShopNameNull()
			{
				return IsNull(tabledtCompany.ShopNameColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetShopNameNull()
			{
				base[tabledtCompany.ShopNameColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsPlaceNull()
			{
				return IsNull(tabledtCompany.PlaceColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetPlaceNull()
			{
				base[tabledtCompany.PlaceColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsAddressNull()
			{
				return IsNull(tabledtCompany.AddressColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetAddressNull()
			{
				base[tabledtCompany.AddressColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsCountryNull()
			{
				return IsNull(tabledtCompany.CountryColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetCountryNull()
			{
				base[tabledtCompany.CountryColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsPincodNull()
			{
				return IsNull(tabledtCompany.PincodColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetPincodNull()
			{
				base[tabledtCompany.PincodColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsPhoneNoNull()
			{
				return IsNull(tabledtCompany.PhoneNoColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetPhoneNoNull()
			{
				base[tabledtCompany.PhoneNoColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsMobNoNull()
			{
				return IsNull(tabledtCompany.MobNoColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetMobNoNull()
			{
				base[tabledtCompany.MobNoColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IswebsiteNull()
			{
				return IsNull(tabledtCompany.websiteColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetwebsiteNull()
			{
				base[tabledtCompany.websiteColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsEmailIdNull()
			{
				return IsNull(tabledtCompany.EmailIdColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetEmailIdNull()
			{
				base[tabledtCompany.EmailIdColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsTaxNumberNull()
			{
				return IsNull(tabledtCompany.TaxNumberColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetTaxNumberNull()
			{
				base[tabledtCompany.TaxNumberColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsStateNull()
			{
				return IsNull(tabledtCompany.StateColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetStateNull()
			{
				base[tabledtCompany.StateColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsStateCodeNull()
			{
				return IsNull(tabledtCompany.StateCodeColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetStateCodeNull()
			{
				base[tabledtCompany.StateCodeColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsLocalNameNull()
			{
				return IsNull(tabledtCompany.LocalNameColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetLocalNameNull()
			{
				base[tabledtCompany.LocalNameColumn] = Convert.DBNull;
			}
		}

		public class dtPOSMasterRow : DataRow
		{
			private dtPOSMasterDataTable tabledtPOSMaster;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string SalesInvNo
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSMaster.SalesInvNoColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'SalesInvNo' in table 'dtPOSMaster' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSMaster.SalesInvNoColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string InvType
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSMaster.InvTypeColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'InvType' in table 'dtPOSMaster' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSMaster.InvTypeColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DateTime SalesDate
			{
				get
				{
					try
					{
						return (DateTime)base[tabledtPOSMaster.SalesDateColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'SalesDate' in table 'dtPOSMaster' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSMaster.SalesDateColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string SalesAccount
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSMaster.SalesAccountColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'SalesAccount' in table 'dtPOSMaster' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSMaster.SalesAccountColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string PartyAccount
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSMaster.PartyAccountColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'PartyAccount' in table 'dtPOSMaster' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSMaster.PartyAccountColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string SalesMan
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSMaster.SalesManColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'SalesMan' in table 'dtPOSMaster' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSMaster.SalesManColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string AditionalAmount
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSMaster.AditionalAmountColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'AditionalAmount' in table 'dtPOSMaster' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSMaster.AditionalAmountColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string Frieght
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSMaster.FrieghtColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Frieght' in table 'dtPOSMaster' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSMaster.FrieghtColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string TotalDiscount
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSMaster.TotalDiscountColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'TotalDiscount' in table 'dtPOSMaster' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSMaster.TotalDiscountColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string BillDiscount
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSMaster.BillDiscountColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'BillDiscount' in table 'dtPOSMaster' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSMaster.BillDiscountColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string GrandTotal
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSMaster.GrandTotalColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'GrandTotal' in table 'dtPOSMaster' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSMaster.GrandTotalColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string JEName
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSMaster.JENameColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'JEName' in table 'dtPOSMaster' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSMaster.JENameColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string EntryDate
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSMaster.EntryDateColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'EntryDate' in table 'dtPOSMaster' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSMaster.EntryDateColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string Remarks
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSMaster.RemarksColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Remarks' in table 'dtPOSMaster' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSMaster.RemarksColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string IGSTAmount
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSMaster.IGSTAmountColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'IGSTAmount' in table 'dtPOSMaster' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSMaster.IGSTAmountColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string SGSTAmount
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSMaster.SGSTAmountColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'SGSTAmount' in table 'dtPOSMaster' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSMaster.SGSTAmountColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string CGSTAmount
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSMaster.CGSTAmountColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'CGSTAmount' in table 'dtPOSMaster' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSMaster.CGSTAmountColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string TaxableAmount
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSMaster.TaxableAmountColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'TaxableAmount' in table 'dtPOSMaster' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSMaster.TaxableAmountColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string Financialyear
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSMaster.FinancialyearColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Financialyear' in table 'dtPOSMaster' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSMaster.FinancialyearColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string FloodCess
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSMaster.FloodCessColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'FloodCess' in table 'dtPOSMaster' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSMaster.FloodCessColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string TotalTaxAmount
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSMaster.TotalTaxAmountColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'TotalTaxAmount' in table 'dtPOSMaster' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSMaster.TotalTaxAmountColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string BranchName
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSMaster.BranchNameColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'BranchName' in table 'dtPOSMaster' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSMaster.BranchNameColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string CntrInvNo
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSMaster.CntrInvNoColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'CntrInvNo' in table 'dtPOSMaster' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSMaster.CntrInvNoColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string SequenceNo
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSMaster.SequenceNoColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'SequenceNo' in table 'dtPOSMaster' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSMaster.SequenceNoColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public decimal CashPayment
			{
				get
				{
					try
					{
						return (decimal)base[tabledtPOSMaster.CashPaymentColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'CashPayment' in table 'dtPOSMaster' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSMaster.CashPaymentColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public decimal CardPayment
			{
				get
				{
					try
					{
						return (decimal)base[tabledtPOSMaster.CardPaymentColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'CardPayment' in table 'dtPOSMaster' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSMaster.CardPaymentColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public decimal Credit
			{
				get
				{
					try
					{
						return (decimal)base[tabledtPOSMaster.CreditColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Credit' in table 'dtPOSMaster' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSMaster.CreditColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string Terminal
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSMaster.TerminalColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Terminal' in table 'dtPOSMaster' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSMaster.TerminalColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public decimal FreeAmount
			{
				get
				{
					try
					{
						return (decimal)base[tabledtPOSMaster.FreeAmountColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'FreeAmount' in table 'dtPOSMaster' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSMaster.FreeAmountColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public decimal ReturnAmount
			{
				get
				{
					try
					{
						return (decimal)base[tabledtPOSMaster.ReturnAmountColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'ReturnAmount' in table 'dtPOSMaster' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSMaster.ReturnAmountColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string OtherPayment
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSMaster.OtherPaymentColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'OtherPayment' in table 'dtPOSMaster' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSMaster.OtherPaymentColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			internal dtPOSMasterRow(DataRowBuilder rb)
				: base(rb)
			{
				tabledtPOSMaster = (dtPOSMasterDataTable)base.Table;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsSalesInvNoNull()
			{
				return IsNull(tabledtPOSMaster.SalesInvNoColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetSalesInvNoNull()
			{
				base[tabledtPOSMaster.SalesInvNoColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsInvTypeNull()
			{
				return IsNull(tabledtPOSMaster.InvTypeColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetInvTypeNull()
			{
				base[tabledtPOSMaster.InvTypeColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsSalesDateNull()
			{
				return IsNull(tabledtPOSMaster.SalesDateColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetSalesDateNull()
			{
				base[tabledtPOSMaster.SalesDateColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsSalesAccountNull()
			{
				return IsNull(tabledtPOSMaster.SalesAccountColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetSalesAccountNull()
			{
				base[tabledtPOSMaster.SalesAccountColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsPartyAccountNull()
			{
				return IsNull(tabledtPOSMaster.PartyAccountColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetPartyAccountNull()
			{
				base[tabledtPOSMaster.PartyAccountColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsSalesManNull()
			{
				return IsNull(tabledtPOSMaster.SalesManColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetSalesManNull()
			{
				base[tabledtPOSMaster.SalesManColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsAditionalAmountNull()
			{
				return IsNull(tabledtPOSMaster.AditionalAmountColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetAditionalAmountNull()
			{
				base[tabledtPOSMaster.AditionalAmountColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsFrieghtNull()
			{
				return IsNull(tabledtPOSMaster.FrieghtColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetFrieghtNull()
			{
				base[tabledtPOSMaster.FrieghtColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsTotalDiscountNull()
			{
				return IsNull(tabledtPOSMaster.TotalDiscountColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetTotalDiscountNull()
			{
				base[tabledtPOSMaster.TotalDiscountColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsBillDiscountNull()
			{
				return IsNull(tabledtPOSMaster.BillDiscountColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetBillDiscountNull()
			{
				base[tabledtPOSMaster.BillDiscountColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsGrandTotalNull()
			{
				return IsNull(tabledtPOSMaster.GrandTotalColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetGrandTotalNull()
			{
				base[tabledtPOSMaster.GrandTotalColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsJENameNull()
			{
				return IsNull(tabledtPOSMaster.JENameColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetJENameNull()
			{
				base[tabledtPOSMaster.JENameColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsEntryDateNull()
			{
				return IsNull(tabledtPOSMaster.EntryDateColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetEntryDateNull()
			{
				base[tabledtPOSMaster.EntryDateColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsRemarksNull()
			{
				return IsNull(tabledtPOSMaster.RemarksColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetRemarksNull()
			{
				base[tabledtPOSMaster.RemarksColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsIGSTAmountNull()
			{
				return IsNull(tabledtPOSMaster.IGSTAmountColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetIGSTAmountNull()
			{
				base[tabledtPOSMaster.IGSTAmountColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsSGSTAmountNull()
			{
				return IsNull(tabledtPOSMaster.SGSTAmountColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetSGSTAmountNull()
			{
				base[tabledtPOSMaster.SGSTAmountColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsCGSTAmountNull()
			{
				return IsNull(tabledtPOSMaster.CGSTAmountColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetCGSTAmountNull()
			{
				base[tabledtPOSMaster.CGSTAmountColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsTaxableAmountNull()
			{
				return IsNull(tabledtPOSMaster.TaxableAmountColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetTaxableAmountNull()
			{
				base[tabledtPOSMaster.TaxableAmountColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsFinancialyearNull()
			{
				return IsNull(tabledtPOSMaster.FinancialyearColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetFinancialyearNull()
			{
				base[tabledtPOSMaster.FinancialyearColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsFloodCessNull()
			{
				return IsNull(tabledtPOSMaster.FloodCessColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetFloodCessNull()
			{
				base[tabledtPOSMaster.FloodCessColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsTotalTaxAmountNull()
			{
				return IsNull(tabledtPOSMaster.TotalTaxAmountColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetTotalTaxAmountNull()
			{
				base[tabledtPOSMaster.TotalTaxAmountColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsBranchNameNull()
			{
				return IsNull(tabledtPOSMaster.BranchNameColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetBranchNameNull()
			{
				base[tabledtPOSMaster.BranchNameColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsCntrInvNoNull()
			{
				return IsNull(tabledtPOSMaster.CntrInvNoColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetCntrInvNoNull()
			{
				base[tabledtPOSMaster.CntrInvNoColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsSequenceNoNull()
			{
				return IsNull(tabledtPOSMaster.SequenceNoColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetSequenceNoNull()
			{
				base[tabledtPOSMaster.SequenceNoColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsCashPaymentNull()
			{
				return IsNull(tabledtPOSMaster.CashPaymentColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetCashPaymentNull()
			{
				base[tabledtPOSMaster.CashPaymentColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsCardPaymentNull()
			{
				return IsNull(tabledtPOSMaster.CardPaymentColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetCardPaymentNull()
			{
				base[tabledtPOSMaster.CardPaymentColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsCreditNull()
			{
				return IsNull(tabledtPOSMaster.CreditColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetCreditNull()
			{
				base[tabledtPOSMaster.CreditColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsTerminalNull()
			{
				return IsNull(tabledtPOSMaster.TerminalColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetTerminalNull()
			{
				base[tabledtPOSMaster.TerminalColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsFreeAmountNull()
			{
				return IsNull(tabledtPOSMaster.FreeAmountColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetFreeAmountNull()
			{
				base[tabledtPOSMaster.FreeAmountColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsReturnAmountNull()
			{
				return IsNull(tabledtPOSMaster.ReturnAmountColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetReturnAmountNull()
			{
				base[tabledtPOSMaster.ReturnAmountColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsOtherPaymentNull()
			{
				return IsNull(tabledtPOSMaster.OtherPaymentColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetOtherPaymentNull()
			{
				base[tabledtPOSMaster.OtherPaymentColumn] = Convert.DBNull;
			}
		}

		public class dtPOSDetailsRow : DataRow
		{
			private dtPOSDetailsDataTable tabledtPOSDetails;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string SL
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSDetails.SLColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'SL' in table 'dtPOSDetails' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSDetails.SLColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string ProductCode
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSDetails.ProductCodeColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'ProductCode' in table 'dtPOSDetails' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSDetails.ProductCodeColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string productId
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSDetails.productIdColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'productId' in table 'dtPOSDetails' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSDetails.productIdColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string ProductName
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSDetails.ProductNameColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'ProductName' in table 'dtPOSDetails' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSDetails.ProductNameColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string Barcode
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSDetails.BarcodeColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Barcode' in table 'dtPOSDetails' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSDetails.BarcodeColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string Qty
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSDetails.QtyColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Qty' in table 'dtPOSDetails' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSDetails.QtyColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string UnitName
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSDetails.UnitNameColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'UnitName' in table 'dtPOSDetails' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSDetails.UnitNameColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string UnitId
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSDetails.UnitIdColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'UnitId' in table 'dtPOSDetails' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSDetails.UnitIdColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string FreeQty
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSDetails.FreeQtyColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'FreeQty' in table 'dtPOSDetails' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSDetails.FreeQtyColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string Rate
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSDetails.RateColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Rate' in table 'dtPOSDetails' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSDetails.RateColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string Gross
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSDetails.GrossColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Gross' in table 'dtPOSDetails' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSDetails.GrossColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string Discount
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSDetails.DiscountColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Discount' in table 'dtPOSDetails' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSDetails.DiscountColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string DisAmount
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSDetails.DisAmountColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'DisAmount' in table 'dtPOSDetails' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSDetails.DisAmountColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string Taxable
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSDetails.TaxableColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Taxable' in table 'dtPOSDetails' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSDetails.TaxableColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string Tax
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSDetails.TaxColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Tax' in table 'dtPOSDetails' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSDetails.TaxColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string TaxAmount
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSDetails.TaxAmountColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'TaxAmount' in table 'dtPOSDetails' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSDetails.TaxAmountColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string NetAmount
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSDetails.NetAmountColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'NetAmount' in table 'dtPOSDetails' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSDetails.NetAmountColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string LocalName
			{
				get
				{
					try
					{
						return (string)base[tabledtPOSDetails.LocalNameColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'LocalName' in table 'dtPOSDetails' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPOSDetails.LocalNameColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			internal dtPOSDetailsRow(DataRowBuilder rb)
				: base(rb)
			{
				tabledtPOSDetails = (dtPOSDetailsDataTable)base.Table;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsSLNull()
			{
				return IsNull(tabledtPOSDetails.SLColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetSLNull()
			{
				base[tabledtPOSDetails.SLColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsProductCodeNull()
			{
				return IsNull(tabledtPOSDetails.ProductCodeColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetProductCodeNull()
			{
				base[tabledtPOSDetails.ProductCodeColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsproductIdNull()
			{
				return IsNull(tabledtPOSDetails.productIdColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetproductIdNull()
			{
				base[tabledtPOSDetails.productIdColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsProductNameNull()
			{
				return IsNull(tabledtPOSDetails.ProductNameColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetProductNameNull()
			{
				base[tabledtPOSDetails.ProductNameColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsBarcodeNull()
			{
				return IsNull(tabledtPOSDetails.BarcodeColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetBarcodeNull()
			{
				base[tabledtPOSDetails.BarcodeColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsQtyNull()
			{
				return IsNull(tabledtPOSDetails.QtyColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetQtyNull()
			{
				base[tabledtPOSDetails.QtyColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsUnitNameNull()
			{
				return IsNull(tabledtPOSDetails.UnitNameColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetUnitNameNull()
			{
				base[tabledtPOSDetails.UnitNameColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsUnitIdNull()
			{
				return IsNull(tabledtPOSDetails.UnitIdColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetUnitIdNull()
			{
				base[tabledtPOSDetails.UnitIdColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsFreeQtyNull()
			{
				return IsNull(tabledtPOSDetails.FreeQtyColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetFreeQtyNull()
			{
				base[tabledtPOSDetails.FreeQtyColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsRateNull()
			{
				return IsNull(tabledtPOSDetails.RateColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetRateNull()
			{
				base[tabledtPOSDetails.RateColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsGrossNull()
			{
				return IsNull(tabledtPOSDetails.GrossColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetGrossNull()
			{
				base[tabledtPOSDetails.GrossColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsDiscountNull()
			{
				return IsNull(tabledtPOSDetails.DiscountColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetDiscountNull()
			{
				base[tabledtPOSDetails.DiscountColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsDisAmountNull()
			{
				return IsNull(tabledtPOSDetails.DisAmountColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetDisAmountNull()
			{
				base[tabledtPOSDetails.DisAmountColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsTaxableNull()
			{
				return IsNull(tabledtPOSDetails.TaxableColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetTaxableNull()
			{
				base[tabledtPOSDetails.TaxableColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsTaxNull()
			{
				return IsNull(tabledtPOSDetails.TaxColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetTaxNull()
			{
				base[tabledtPOSDetails.TaxColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsTaxAmountNull()
			{
				return IsNull(tabledtPOSDetails.TaxAmountColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetTaxAmountNull()
			{
				base[tabledtPOSDetails.TaxAmountColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsNetAmountNull()
			{
				return IsNull(tabledtPOSDetails.NetAmountColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetNetAmountNull()
			{
				base[tabledtPOSDetails.NetAmountColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsLocalNameNull()
			{
				return IsNull(tabledtPOSDetails.LocalNameColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetLocalNameNull()
			{
				base[tabledtPOSDetails.LocalNameColumn] = Convert.DBNull;
			}
		}

		public class dtPaymentRow : DataRow
		{
			private dtPaymentDataTable tabledtPayment;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string Name
			{
				get
				{
					try
					{
						return (string)base[tabledtPayment.NameColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Name' in table 'dtPayment' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPayment.NameColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string Value
			{
				get
				{
					try
					{
						return (string)base[tabledtPayment.ValueColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Value' in table 'dtPayment' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPayment.ValueColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string RightLabel
			{
				get
				{
					try
					{
						return (string)base[tabledtPayment.RightLabelColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'RightLabel' in table 'dtPayment' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPayment.RightLabelColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			internal dtPaymentRow(DataRowBuilder rb)
				: base(rb)
			{
				tabledtPayment = (dtPaymentDataTable)base.Table;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsNameNull()
			{
				return IsNull(tabledtPayment.NameColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetNameNull()
			{
				base[tabledtPayment.NameColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsValueNull()
			{
				return IsNull(tabledtPayment.ValueColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetValueNull()
			{
				base[tabledtPayment.ValueColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsRightLabelNull()
			{
				return IsNull(tabledtPayment.RightLabelColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetRightLabelNull()
			{
				base[tabledtPayment.RightLabelColumn] = Convert.DBNull;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public class dtCompanyRowChangeEvent : EventArgs
		{
			private dtCompanyRow eventRow;

			private DataRowAction eventAction;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtCompanyRow Row => eventRow;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataRowAction Action => eventAction;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtCompanyRowChangeEvent(dtCompanyRow row, DataRowAction action)
			{
				eventRow = row;
				eventAction = action;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public class dtPOSMasterRowChangeEvent : EventArgs
		{
			private dtPOSMasterRow eventRow;

			private DataRowAction eventAction;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtPOSMasterRow Row => eventRow;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataRowAction Action => eventAction;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public dtPOSMasterRowChangeEvent(dtPOSMasterRow row, DataRowAction action)
			{
				eventRow = row;
				eventAction = action;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public class dtPOSDetailsRowChangeEvent : EventArgs
		{
			private dtPOSDetailsRow eventRow;

			private DataRowAction eventAction;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtPOSDetailsRow Row => eventRow;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataRowAction Action => eventAction;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtPOSDetailsRowChangeEvent(dtPOSDetailsRow row, DataRowAction action)
			{
				eventRow = row;
				eventAction = action;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public class dtPaymentRowChangeEvent : EventArgs
		{
			private dtPaymentRow eventRow;

			private DataRowAction eventAction;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public dtPaymentRow Row => eventRow;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataRowAction Action => eventAction;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public dtPaymentRowChangeEvent(dtPaymentRow row, DataRowAction action)
			{
				eventRow = row;
				eventAction = action;
			}
		}

		private dtCompanyDataTable tabledtCompany;

		private dtPOSMasterDataTable tabledtPOSMaster;

		private dtPOSDetailsDataTable tabledtPOSDetails;

		private dtPaymentDataTable tabledtPayment;

		private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;

		[DebuggerNonUserCode]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[Browsable(false)]
		public dtCompanyDataTable dtCompany => tabledtCompany;

		[Browsable(false)]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public dtPOSMasterDataTable dtPOSMaster => tabledtPOSMaster;

		[DebuggerNonUserCode]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public dtPOSDetailsDataTable dtPOSDetails => tabledtPOSDetails;

		[DebuggerNonUserCode]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public dtPaymentDataTable dtPayment => tabledtPayment;

		[DebuggerNonUserCode]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(true)]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public override SchemaSerializationMode SchemaSerializationMode
		{
			get
			{
				return _schemaSerializationMode;
			}
			set
			{
				_schemaSerializationMode = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new DataTableCollection Tables => base.Tables;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		public new DataRelationCollection Relations => base.Relations;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		public dsPOSPrintWithInvoiceNo()
		{
			BeginInit();
			InitClass();
			CollectionChangeEventHandler value = SchemaChanged;
			base.Tables.CollectionChanged += value;
			base.Relations.CollectionChanged += value;
			EndInit();
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		protected dsPOSPrintWithInvoiceNo(SerializationInfo info, StreamingContext context)
			: base(info, context, ConstructSchema: false)
		{
			if (IsBinarySerialized(info, context))
			{
				InitVars(initTable: false);
				CollectionChangeEventHandler value = SchemaChanged;
				Tables.CollectionChanged += value;
				Relations.CollectionChanged += value;
				return;
			}
			string s = (string)info.GetValue("XmlSchema", typeof(string));
			if (DetermineSchemaSerializationMode(info, context) == SchemaSerializationMode.IncludeSchema)
			{
				System.Data.DataSet dataSet = new System.Data.DataSet();
				dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
				if (dataSet.Tables["dtCompany"] != null)
				{
					base.Tables.Add(new dtCompanyDataTable(dataSet.Tables["dtCompany"]));
				}
				if (dataSet.Tables["dtPOSMaster"] != null)
				{
					base.Tables.Add(new dtPOSMasterDataTable(dataSet.Tables["dtPOSMaster"]));
				}
				if (dataSet.Tables["dtPOSDetails"] != null)
				{
					base.Tables.Add(new dtPOSDetailsDataTable(dataSet.Tables["dtPOSDetails"]));
				}
				if (dataSet.Tables["dtPayment"] != null)
				{
					base.Tables.Add(new dtPaymentDataTable(dataSet.Tables["dtPayment"]));
				}
				base.DataSetName = dataSet.DataSetName;
				base.Prefix = dataSet.Prefix;
				base.Namespace = dataSet.Namespace;
				base.Locale = dataSet.Locale;
				base.CaseSensitive = dataSet.CaseSensitive;
				base.EnforceConstraints = dataSet.EnforceConstraints;
				Merge(dataSet, preserveChanges: false, MissingSchemaAction.Add);
				InitVars();
			}
			else
			{
				ReadXmlSchema(new XmlTextReader(new StringReader(s)));
			}
			GetSerializationData(info, context);
			CollectionChangeEventHandler value2 = SchemaChanged;
			base.Tables.CollectionChanged += value2;
			Relations.CollectionChanged += value2;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		protected override void InitializeDerivedDataSet()
		{
			BeginInit();
			InitClass();
			EndInit();
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public override System.Data.DataSet Clone()
		{
			dsPOSPrintWithInvoiceNo dsPOSPrintWithInvoiceNo2 = (dsPOSPrintWithInvoiceNo)base.Clone();
			dsPOSPrintWithInvoiceNo2.InitVars();
			dsPOSPrintWithInvoiceNo2.SchemaSerializationMode = SchemaSerializationMode;
			return dsPOSPrintWithInvoiceNo2;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		protected override bool ShouldSerializeTables()
		{
			return false;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		protected override bool ShouldSerializeRelations()
		{
			return false;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		protected override void ReadXmlSerializable(XmlReader reader)
		{
			if (DetermineSchemaSerializationMode(reader) == SchemaSerializationMode.IncludeSchema)
			{
				Reset();
				System.Data.DataSet dataSet = new System.Data.DataSet();
				dataSet.ReadXml(reader);
				if (dataSet.Tables["dtCompany"] != null)
				{
					base.Tables.Add(new dtCompanyDataTable(dataSet.Tables["dtCompany"]));
				}
				if (dataSet.Tables["dtPOSMaster"] != null)
				{
					base.Tables.Add(new dtPOSMasterDataTable(dataSet.Tables["dtPOSMaster"]));
				}
				if (dataSet.Tables["dtPOSDetails"] != null)
				{
					base.Tables.Add(new dtPOSDetailsDataTable(dataSet.Tables["dtPOSDetails"]));
				}
				if (dataSet.Tables["dtPayment"] != null)
				{
					base.Tables.Add(new dtPaymentDataTable(dataSet.Tables["dtPayment"]));
				}
				base.DataSetName = dataSet.DataSetName;
				base.Prefix = dataSet.Prefix;
				base.Namespace = dataSet.Namespace;
				base.Locale = dataSet.Locale;
				base.CaseSensitive = dataSet.CaseSensitive;
				base.EnforceConstraints = dataSet.EnforceConstraints;
				Merge(dataSet, preserveChanges: false, MissingSchemaAction.Add);
				InitVars();
			}
			else
			{
				ReadXml(reader);
				InitVars();
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		protected override XmlSchema GetSchemaSerializable()
		{
			MemoryStream memoryStream = new MemoryStream();
			WriteXmlSchema(new XmlTextWriter(memoryStream, null));
			memoryStream.Position = 0L;
			return XmlSchema.Read(new XmlTextReader(memoryStream), null);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		internal void InitVars()
		{
			InitVars(initTable: true);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		internal void InitVars(bool initTable)
		{
			tabledtCompany = (dtCompanyDataTable)base.Tables["dtCompany"];
			if (initTable && tabledtCompany != null)
			{
				tabledtCompany.InitVars();
			}
			tabledtPOSMaster = (dtPOSMasterDataTable)base.Tables["dtPOSMaster"];
			if (initTable && tabledtPOSMaster != null)
			{
				tabledtPOSMaster.InitVars();
			}
			tabledtPOSDetails = (dtPOSDetailsDataTable)base.Tables["dtPOSDetails"];
			if (initTable && tabledtPOSDetails != null)
			{
				tabledtPOSDetails.InitVars();
			}
			tabledtPayment = (dtPaymentDataTable)base.Tables["dtPayment"];
			if (initTable && tabledtPayment != null)
			{
				tabledtPayment.InitVars();
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		private void InitClass()
		{
			base.DataSetName = "dsPOSPrintWithInvoiceNo";
			base.Prefix = "";
			base.Namespace = "http://tempuri.org/dsPOSPrintWithInvoiceNo.xsd";
			base.EnforceConstraints = true;
			SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
			tabledtCompany = new dtCompanyDataTable();
			base.Tables.Add(tabledtCompany);
			tabledtPOSMaster = new dtPOSMasterDataTable();
			base.Tables.Add(tabledtPOSMaster);
			tabledtPOSDetails = new dtPOSDetailsDataTable();
			base.Tables.Add(tabledtPOSDetails);
			tabledtPayment = new dtPaymentDataTable();
			base.Tables.Add(tabledtPayment);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		private bool ShouldSerializedtCompany()
		{
			return false;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		private bool ShouldSerializedtPOSMaster()
		{
			return false;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		private bool ShouldSerializedtPOSDetails()
		{
			return false;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		private bool ShouldSerializedtPayment()
		{
			return false;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		private void SchemaChanged(object sender, CollectionChangeEventArgs e)
		{
			if (e.Action == CollectionChangeAction.Remove)
			{
				InitVars();
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		public static XmlSchemaComplexType GetTypedDataSetSchema(XmlSchemaSet xs)
		{
			dsPOSPrintWithInvoiceNo dsPOSPrintWithInvoiceNo2 = new dsPOSPrintWithInvoiceNo();
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
			xmlSchemaAny.Namespace = dsPOSPrintWithInvoiceNo2.Namespace;
			xmlSchemaSequence.Items.Add(xmlSchemaAny);
			xmlSchemaComplexType.Particle = xmlSchemaSequence;
			XmlSchema schemaSerializable = dsPOSPrintWithInvoiceNo2.GetSchemaSerializable();
			if (xs.Contains(schemaSerializable.TargetNamespace))
			{
				MemoryStream memoryStream = new MemoryStream();
				MemoryStream memoryStream2 = new MemoryStream();
				try
				{
					XmlSchema xmlSchema = null;
					schemaSerializable.Write(memoryStream);
					IEnumerator enumerator = xs.Schemas(schemaSerializable.TargetNamespace).GetEnumerator();
					while (enumerator.MoveNext())
					{
						xmlSchema = (XmlSchema)enumerator.Current;
						memoryStream2.SetLength(0L);
						xmlSchema.Write(memoryStream2);
						if (memoryStream.Length == memoryStream2.Length)
						{
							memoryStream.Position = 0L;
							memoryStream2.Position = 0L;
							while (memoryStream.Position != memoryStream.Length && memoryStream.ReadByte() == memoryStream2.ReadByte())
							{
							}
							if (memoryStream.Position == memoryStream.Length)
							{
								return xmlSchemaComplexType;
							}
						}
					}
				}
				finally
				{
					memoryStream?.Close();
					memoryStream2?.Close();
				}
			}
			xs.Add(schemaSerializable);
			return xmlSchemaComplexType;
		}
	}
}
