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
	[ToolboxItem(true)]
	[XmlSchemaProvider("GetTypedDataSetSchema")]
	[DesignerCategory("code")]
	[XmlRoot("dsSupplierItemPurchaseReportPOS")]
	[HelpKeyword("vs.data.DataSet")]
	public class dsSupplierItemPurchaseReportPOS : System.Data.DataSet
	{
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public delegate void dtSupplierItemPurchaseReportPOSRowChangeEventHandler(object sender, dtSupplierItemPurchaseReportPOSRowChangeEvent e);

		[Serializable]
		[XmlSchemaProvider("GetTypedTableSchema")]
		public class dtSupplierItemPurchaseReportPOSDataTable : TypedTableBase<dtSupplierItemPurchaseReportPOSRow>
		{
			private DataColumn columnPurDate;

			private DataColumn columnPurInvNo;

			private DataColumn columnRefInvNo;

			private DataColumn columnProductName;

			private DataColumn columnBarcode;

			private DataColumn columnProQty;

			private DataColumn columnUnitName;

			private DataColumn columnGross;

			private DataColumn columnDiscAmnt;

			private DataColumn columnTaxable;

			private DataColumn columnGSTAMOUNT;

			private DataColumn columnNetAmnt;

			private DataColumn columnProRate;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn PurDateColumn => columnPurDate;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn PurInvNoColumn => columnPurInvNo;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn RefInvNoColumn => columnRefInvNo;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn ProductNameColumn => columnProductName;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn BarcodeColumn => columnBarcode;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn ProQtyColumn => columnProQty;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn UnitNameColumn => columnUnitName;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn GrossColumn => columnGross;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn DiscAmntColumn => columnDiscAmnt;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn TaxableColumn => columnTaxable;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn GSTAMOUNTColumn => columnGSTAMOUNT;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn NetAmntColumn => columnNetAmnt;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn ProRateColumn => columnProRate;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[Browsable(false)]
			[DebuggerNonUserCode]
			public int Count => base.Rows.Count;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtSupplierItemPurchaseReportPOSRow this[int index] => (dtSupplierItemPurchaseReportPOSRow)base.Rows[index];

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtSupplierItemPurchaseReportPOSRowChangeEventHandler dtSupplierItemPurchaseReportPOSRowChanging;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtSupplierItemPurchaseReportPOSRowChangeEventHandler dtSupplierItemPurchaseReportPOSRowChanged;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtSupplierItemPurchaseReportPOSRowChangeEventHandler dtSupplierItemPurchaseReportPOSRowDeleting;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtSupplierItemPurchaseReportPOSRowChangeEventHandler dtSupplierItemPurchaseReportPOSRowDeleted;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public dtSupplierItemPurchaseReportPOSDataTable()
			{
				base.TableName = "dtSupplierItemPurchaseReportPOS";
				BeginInit();
				InitClass();
				EndInit();
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			internal dtSupplierItemPurchaseReportPOSDataTable(DataTable table)
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
			protected dtSupplierItemPurchaseReportPOSDataTable(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
				InitVars();
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void AdddtSupplierItemPurchaseReportPOSRow(dtSupplierItemPurchaseReportPOSRow row)
			{
				base.Rows.Add(row);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public dtSupplierItemPurchaseReportPOSRow AdddtSupplierItemPurchaseReportPOSRow(string PurDate, string PurInvNo, string RefInvNo, string ProductName, string Barcode, string ProQty, string UnitName, string Gross, string DiscAmnt, string Taxable, string GSTAMOUNT, string NetAmnt, string ProRate)
			{
				dtSupplierItemPurchaseReportPOSRow dtSupplierItemPurchaseReportPOSRow = (dtSupplierItemPurchaseReportPOSRow)NewRow();
				object[] array2 = (dtSupplierItemPurchaseReportPOSRow.ItemArray = new object[13]
				{
					PurDate, PurInvNo, RefInvNo, ProductName, Barcode, ProQty, UnitName, Gross, DiscAmnt, Taxable,
					GSTAMOUNT, NetAmnt, ProRate
				});
				base.Rows.Add(dtSupplierItemPurchaseReportPOSRow);
				return dtSupplierItemPurchaseReportPOSRow;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public override DataTable Clone()
			{
				dtSupplierItemPurchaseReportPOSDataTable dtSupplierItemPurchaseReportPOSDataTable = (dtSupplierItemPurchaseReportPOSDataTable)base.Clone();
				dtSupplierItemPurchaseReportPOSDataTable.InitVars();
				return dtSupplierItemPurchaseReportPOSDataTable;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override DataTable CreateInstance()
			{
				return new dtSupplierItemPurchaseReportPOSDataTable();
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			internal void InitVars()
			{
				columnPurDate = base.Columns["PurDate"];
				columnPurInvNo = base.Columns["PurInvNo"];
				columnRefInvNo = base.Columns["RefInvNo"];
				columnProductName = base.Columns["ProductName"];
				columnBarcode = base.Columns["Barcode"];
				columnProQty = base.Columns["ProQty"];
				columnUnitName = base.Columns["UnitName"];
				columnGross = base.Columns["Gross"];
				columnDiscAmnt = base.Columns["DiscAmnt"];
				columnTaxable = base.Columns["Taxable"];
				columnGSTAMOUNT = base.Columns["GSTAMOUNT"];
				columnNetAmnt = base.Columns["NetAmnt"];
				columnProRate = base.Columns["ProRate"];
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			private void InitClass()
			{
				columnPurDate = new DataColumn("PurDate", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnPurDate);
				columnPurInvNo = new DataColumn("PurInvNo", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnPurInvNo);
				columnRefInvNo = new DataColumn("RefInvNo", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnRefInvNo);
				columnProductName = new DataColumn("ProductName", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnProductName);
				columnBarcode = new DataColumn("Barcode", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnBarcode);
				columnProQty = new DataColumn("ProQty", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnProQty);
				columnUnitName = new DataColumn("UnitName", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnUnitName);
				columnGross = new DataColumn("Gross", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnGross);
				columnDiscAmnt = new DataColumn("DiscAmnt", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnDiscAmnt);
				columnTaxable = new DataColumn("Taxable", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnTaxable);
				columnGSTAMOUNT = new DataColumn("GSTAMOUNT", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnGSTAMOUNT);
				columnNetAmnt = new DataColumn("NetAmnt", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnNetAmnt);
				columnProRate = new DataColumn("ProRate", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnProRate);
				columnPurDate.Caption = "InvoiceDate";
				columnPurInvNo.Caption = "InvoiceNo";
				columnRefInvNo.Caption = "RefNo";
				columnProQty.Caption = "Quantity";
				columnUnitName.Caption = "Unit";
				columnDiscAmnt.Caption = "Discount";
				columnGSTAMOUNT.Caption = "TaxAmount";
				columnNetAmnt.Caption = "NetAmount";
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public dtSupplierItemPurchaseReportPOSRow NewdtSupplierItemPurchaseReportPOSRow()
			{
				return (dtSupplierItemPurchaseReportPOSRow)NewRow();
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new dtSupplierItemPurchaseReportPOSRow(builder);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override Type GetRowType()
			{
				return typeof(dtSupplierItemPurchaseReportPOSRow);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this.dtSupplierItemPurchaseReportPOSRowChanged != null)
				{
					this.dtSupplierItemPurchaseReportPOSRowChanged(this, new dtSupplierItemPurchaseReportPOSRowChangeEvent((dtSupplierItemPurchaseReportPOSRow)e.Row, e.Action));
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this.dtSupplierItemPurchaseReportPOSRowChanging != null)
				{
					this.dtSupplierItemPurchaseReportPOSRowChanging(this, new dtSupplierItemPurchaseReportPOSRowChangeEvent((dtSupplierItemPurchaseReportPOSRow)e.Row, e.Action));
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.dtSupplierItemPurchaseReportPOSRowDeleted != null)
				{
					this.dtSupplierItemPurchaseReportPOSRowDeleted(this, new dtSupplierItemPurchaseReportPOSRowChangeEvent((dtSupplierItemPurchaseReportPOSRow)e.Row, e.Action));
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this.dtSupplierItemPurchaseReportPOSRowDeleting != null)
				{
					this.dtSupplierItemPurchaseReportPOSRowDeleting(this, new dtSupplierItemPurchaseReportPOSRowChangeEvent((dtSupplierItemPurchaseReportPOSRow)e.Row, e.Action));
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void RemovedtSupplierItemPurchaseReportPOSRow(dtSupplierItemPurchaseReportPOSRow row)
			{
				base.Rows.Remove(row);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				dsSupplierItemPurchaseReportPOS dsSupplierItemPurchaseReportPOS2 = new dsSupplierItemPurchaseReportPOS();
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
				xmlSchemaAttribute.FixedValue = dsSupplierItemPurchaseReportPOS2.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "dtSupplierItemPurchaseReportPOSDataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = dsSupplierItemPurchaseReportPOS2.GetSchemaSerializable();
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

		public class dtSupplierItemPurchaseReportPOSRow : DataRow
		{
			private dtSupplierItemPurchaseReportPOSDataTable tabledtSupplierItemPurchaseReportPOS;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string PurDate
			{
				get
				{
					try
					{
						return (string)base[tabledtSupplierItemPurchaseReportPOS.PurDateColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'PurDate' in table 'dtSupplierItemPurchaseReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSupplierItemPurchaseReportPOS.PurDateColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string PurInvNo
			{
				get
				{
					try
					{
						return (string)base[tabledtSupplierItemPurchaseReportPOS.PurInvNoColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'PurInvNo' in table 'dtSupplierItemPurchaseReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSupplierItemPurchaseReportPOS.PurInvNoColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string RefInvNo
			{
				get
				{
					try
					{
						return (string)base[tabledtSupplierItemPurchaseReportPOS.RefInvNoColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'RefInvNo' in table 'dtSupplierItemPurchaseReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSupplierItemPurchaseReportPOS.RefInvNoColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string ProductName
			{
				get
				{
					try
					{
						return (string)base[tabledtSupplierItemPurchaseReportPOS.ProductNameColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'ProductName' in table 'dtSupplierItemPurchaseReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSupplierItemPurchaseReportPOS.ProductNameColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string Barcode
			{
				get
				{
					try
					{
						return (string)base[tabledtSupplierItemPurchaseReportPOS.BarcodeColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Barcode' in table 'dtSupplierItemPurchaseReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSupplierItemPurchaseReportPOS.BarcodeColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string ProQty
			{
				get
				{
					try
					{
						return (string)base[tabledtSupplierItemPurchaseReportPOS.ProQtyColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'ProQty' in table 'dtSupplierItemPurchaseReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSupplierItemPurchaseReportPOS.ProQtyColumn] = value;
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
						return (string)base[tabledtSupplierItemPurchaseReportPOS.UnitNameColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'UnitName' in table 'dtSupplierItemPurchaseReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSupplierItemPurchaseReportPOS.UnitNameColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string Gross
			{
				get
				{
					try
					{
						return (string)base[tabledtSupplierItemPurchaseReportPOS.GrossColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Gross' in table 'dtSupplierItemPurchaseReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSupplierItemPurchaseReportPOS.GrossColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string DiscAmnt
			{
				get
				{
					try
					{
						return (string)base[tabledtSupplierItemPurchaseReportPOS.DiscAmntColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'DiscAmnt' in table 'dtSupplierItemPurchaseReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSupplierItemPurchaseReportPOS.DiscAmntColumn] = value;
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
						return (string)base[tabledtSupplierItemPurchaseReportPOS.TaxableColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Taxable' in table 'dtSupplierItemPurchaseReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSupplierItemPurchaseReportPOS.TaxableColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string GSTAMOUNT
			{
				get
				{
					try
					{
						return (string)base[tabledtSupplierItemPurchaseReportPOS.GSTAMOUNTColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'GSTAMOUNT' in table 'dtSupplierItemPurchaseReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSupplierItemPurchaseReportPOS.GSTAMOUNTColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string NetAmnt
			{
				get
				{
					try
					{
						return (string)base[tabledtSupplierItemPurchaseReportPOS.NetAmntColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'NetAmnt' in table 'dtSupplierItemPurchaseReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSupplierItemPurchaseReportPOS.NetAmntColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string ProRate
			{
				get
				{
					try
					{
						return (string)base[tabledtSupplierItemPurchaseReportPOS.ProRateColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'ProRate' in table 'dtSupplierItemPurchaseReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSupplierItemPurchaseReportPOS.ProRateColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			internal dtSupplierItemPurchaseReportPOSRow(DataRowBuilder rb)
				: base(rb)
			{
				tabledtSupplierItemPurchaseReportPOS = (dtSupplierItemPurchaseReportPOSDataTable)base.Table;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsPurDateNull()
			{
				return IsNull(tabledtSupplierItemPurchaseReportPOS.PurDateColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetPurDateNull()
			{
				base[tabledtSupplierItemPurchaseReportPOS.PurDateColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsPurInvNoNull()
			{
				return IsNull(tabledtSupplierItemPurchaseReportPOS.PurInvNoColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetPurInvNoNull()
			{
				base[tabledtSupplierItemPurchaseReportPOS.PurInvNoColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsRefInvNoNull()
			{
				return IsNull(tabledtSupplierItemPurchaseReportPOS.RefInvNoColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetRefInvNoNull()
			{
				base[tabledtSupplierItemPurchaseReportPOS.RefInvNoColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsProductNameNull()
			{
				return IsNull(tabledtSupplierItemPurchaseReportPOS.ProductNameColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetProductNameNull()
			{
				base[tabledtSupplierItemPurchaseReportPOS.ProductNameColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsBarcodeNull()
			{
				return IsNull(tabledtSupplierItemPurchaseReportPOS.BarcodeColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetBarcodeNull()
			{
				base[tabledtSupplierItemPurchaseReportPOS.BarcodeColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsProQtyNull()
			{
				return IsNull(tabledtSupplierItemPurchaseReportPOS.ProQtyColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetProQtyNull()
			{
				base[tabledtSupplierItemPurchaseReportPOS.ProQtyColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsUnitNameNull()
			{
				return IsNull(tabledtSupplierItemPurchaseReportPOS.UnitNameColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetUnitNameNull()
			{
				base[tabledtSupplierItemPurchaseReportPOS.UnitNameColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsGrossNull()
			{
				return IsNull(tabledtSupplierItemPurchaseReportPOS.GrossColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetGrossNull()
			{
				base[tabledtSupplierItemPurchaseReportPOS.GrossColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsDiscAmntNull()
			{
				return IsNull(tabledtSupplierItemPurchaseReportPOS.DiscAmntColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetDiscAmntNull()
			{
				base[tabledtSupplierItemPurchaseReportPOS.DiscAmntColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsTaxableNull()
			{
				return IsNull(tabledtSupplierItemPurchaseReportPOS.TaxableColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetTaxableNull()
			{
				base[tabledtSupplierItemPurchaseReportPOS.TaxableColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsGSTAMOUNTNull()
			{
				return IsNull(tabledtSupplierItemPurchaseReportPOS.GSTAMOUNTColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetGSTAMOUNTNull()
			{
				base[tabledtSupplierItemPurchaseReportPOS.GSTAMOUNTColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsNetAmntNull()
			{
				return IsNull(tabledtSupplierItemPurchaseReportPOS.NetAmntColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetNetAmntNull()
			{
				base[tabledtSupplierItemPurchaseReportPOS.NetAmntColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsProRateNull()
			{
				return IsNull(tabledtSupplierItemPurchaseReportPOS.ProRateColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetProRateNull()
			{
				base[tabledtSupplierItemPurchaseReportPOS.ProRateColumn] = Convert.DBNull;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public class dtSupplierItemPurchaseReportPOSRowChangeEvent : EventArgs
		{
			private dtSupplierItemPurchaseReportPOSRow eventRow;

			private DataRowAction eventAction;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtSupplierItemPurchaseReportPOSRow Row => eventRow;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataRowAction Action => eventAction;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public dtSupplierItemPurchaseReportPOSRowChangeEvent(dtSupplierItemPurchaseReportPOSRow row, DataRowAction action)
			{
				eventRow = row;
				eventAction = action;
			}
		}

		private dtSupplierItemPurchaseReportPOSDataTable tabledtSupplierItemPurchaseReportPOS;

		private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public dtSupplierItemPurchaseReportPOSDataTable dtSupplierItemPurchaseReportPOS => tabledtSupplierItemPurchaseReportPOS;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[Browsable(true)]
		[DebuggerNonUserCode]
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

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		public new DataTableCollection Tables => base.Tables;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		public new DataRelationCollection Relations => base.Relations;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public dsSupplierItemPurchaseReportPOS()
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
		protected dsSupplierItemPurchaseReportPOS(SerializationInfo info, StreamingContext context)
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
				if (dataSet.Tables["dtSupplierItemPurchaseReportPOS"] != null)
				{
					base.Tables.Add(new dtSupplierItemPurchaseReportPOSDataTable(dataSet.Tables["dtSupplierItemPurchaseReportPOS"]));
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

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
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
			dsSupplierItemPurchaseReportPOS dsSupplierItemPurchaseReportPOS2 = (dsSupplierItemPurchaseReportPOS)base.Clone();
			dsSupplierItemPurchaseReportPOS2.InitVars();
			dsSupplierItemPurchaseReportPOS2.SchemaSerializationMode = SchemaSerializationMode;
			return dsSupplierItemPurchaseReportPOS2;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
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
				if (dataSet.Tables["dtSupplierItemPurchaseReportPOS"] != null)
				{
					base.Tables.Add(new dtSupplierItemPurchaseReportPOSDataTable(dataSet.Tables["dtSupplierItemPurchaseReportPOS"]));
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

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		internal void InitVars(bool initTable)
		{
			tabledtSupplierItemPurchaseReportPOS = (dtSupplierItemPurchaseReportPOSDataTable)base.Tables["dtSupplierItemPurchaseReportPOS"];
			if (initTable && tabledtSupplierItemPurchaseReportPOS != null)
			{
				tabledtSupplierItemPurchaseReportPOS.InitVars();
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		private void InitClass()
		{
			base.DataSetName = "dsSupplierItemPurchaseReportPOS";
			base.Prefix = "";
			base.Namespace = "http://tempuri.org/dsSupplierItemPurchaseReportPOS.xsd";
			base.EnforceConstraints = true;
			SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
			tabledtSupplierItemPurchaseReportPOS = new dtSupplierItemPurchaseReportPOSDataTable();
			base.Tables.Add(tabledtSupplierItemPurchaseReportPOS);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		private bool ShouldSerializedtSupplierItemPurchaseReportPOS()
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

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public static XmlSchemaComplexType GetTypedDataSetSchema(XmlSchemaSet xs)
		{
			dsSupplierItemPurchaseReportPOS dsSupplierItemPurchaseReportPOS2 = new dsSupplierItemPurchaseReportPOS();
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
			xmlSchemaAny.Namespace = dsSupplierItemPurchaseReportPOS2.Namespace;
			xmlSchemaSequence.Items.Add(xmlSchemaAny);
			xmlSchemaComplexType.Particle = xmlSchemaSequence;
			XmlSchema schemaSerializable = dsSupplierItemPurchaseReportPOS2.GetSchemaSerializable();
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
