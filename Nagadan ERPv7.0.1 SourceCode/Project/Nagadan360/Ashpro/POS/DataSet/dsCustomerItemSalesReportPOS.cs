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
	[XmlRoot("dsCustomerItemSalesReportPOS")]
	[HelpKeyword("vs.data.DataSet")]
	[DesignerCategory("code")]
	[ToolboxItem(true)]
	[XmlSchemaProvider("GetTypedDataSetSchema")]
	public class dsCustomerItemSalesReportPOS : System.Data.DataSet
	{
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public delegate void dtCustomerItemSalesReportPOSRowChangeEventHandler(object sender, dtCustomerItemSalesReportPOSRowChangeEvent e);

		[Serializable]
		[XmlSchemaProvider("GetTypedTableSchema")]
		public class dtCustomerItemSalesReportPOSDataTable : TypedTableBase<dtCustomerItemSalesReportPOSRow>
		{
			private DataColumn columnInvoiceDate;

			private DataColumn columnInvoiceNo;

			private DataColumn columnProductName;

			private DataColumn columnQuantity;

			private DataColumn columnTaxAmount;

			private DataColumn columnNetAmount;

			private DataColumn columnBarcode;

			private DataColumn columnUnitname;

			private DataColumn columnDiscountAmount;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn InvoiceDateColumn => columnInvoiceDate;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn InvoiceNoColumn => columnInvoiceNo;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn ProductNameColumn => columnProductName;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn QuantityColumn => columnQuantity;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn TaxAmountColumn => columnTaxAmount;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn NetAmountColumn => columnNetAmount;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn BarcodeColumn => columnBarcode;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn UnitnameColumn => columnUnitname;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn DiscountAmountColumn => columnDiscountAmount;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[Browsable(false)]
			public int Count => base.Rows.Count;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtCustomerItemSalesReportPOSRow this[int index] => (dtCustomerItemSalesReportPOSRow)base.Rows[index];

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtCustomerItemSalesReportPOSRowChangeEventHandler dtCustomerItemSalesReportPOSRowChanging;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtCustomerItemSalesReportPOSRowChangeEventHandler dtCustomerItemSalesReportPOSRowChanged;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtCustomerItemSalesReportPOSRowChangeEventHandler dtCustomerItemSalesReportPOSRowDeleting;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtCustomerItemSalesReportPOSRowChangeEventHandler dtCustomerItemSalesReportPOSRowDeleted;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public dtCustomerItemSalesReportPOSDataTable()
			{
				base.TableName = "dtCustomerItemSalesReportPOS";
				BeginInit();
				InitClass();
				EndInit();
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			internal dtCustomerItemSalesReportPOSDataTable(DataTable table)
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
			protected dtCustomerItemSalesReportPOSDataTable(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
				InitVars();
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void AdddtCustomerItemSalesReportPOSRow(dtCustomerItemSalesReportPOSRow row)
			{
				base.Rows.Add(row);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public dtCustomerItemSalesReportPOSRow AdddtCustomerItemSalesReportPOSRow(string InvoiceDate, string InvoiceNo, string ProductName, string Quantity, string TaxAmount, string NetAmount, string Barcode, string Unitname, string DiscountAmount)
			{
				dtCustomerItemSalesReportPOSRow dtCustomerItemSalesReportPOSRow = (dtCustomerItemSalesReportPOSRow)NewRow();
				object[] array2 = (dtCustomerItemSalesReportPOSRow.ItemArray = new object[9] { InvoiceDate, InvoiceNo, ProductName, Quantity, TaxAmount, NetAmount, Barcode, Unitname, DiscountAmount });
				base.Rows.Add(dtCustomerItemSalesReportPOSRow);
				return dtCustomerItemSalesReportPOSRow;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public override DataTable Clone()
			{
				dtCustomerItemSalesReportPOSDataTable dtCustomerItemSalesReportPOSDataTable = (dtCustomerItemSalesReportPOSDataTable)base.Clone();
				dtCustomerItemSalesReportPOSDataTable.InitVars();
				return dtCustomerItemSalesReportPOSDataTable;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override DataTable CreateInstance()
			{
				return new dtCustomerItemSalesReportPOSDataTable();
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			internal void InitVars()
			{
				columnInvoiceDate = base.Columns["InvoiceDate"];
				columnInvoiceNo = base.Columns["InvoiceNo"];
				columnProductName = base.Columns["ProductName"];
				columnQuantity = base.Columns["Quantity"];
				columnTaxAmount = base.Columns["TaxAmount"];
				columnNetAmount = base.Columns["NetAmount"];
				columnBarcode = base.Columns["Barcode"];
				columnUnitname = base.Columns["Unitname"];
				columnDiscountAmount = base.Columns["DiscountAmount"];
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			private void InitClass()
			{
				columnInvoiceDate = new DataColumn("InvoiceDate", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnInvoiceDate);
				columnInvoiceNo = new DataColumn("InvoiceNo", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnInvoiceNo);
				columnProductName = new DataColumn("ProductName", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnProductName);
				columnQuantity = new DataColumn("Quantity", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnQuantity);
				columnTaxAmount = new DataColumn("TaxAmount", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnTaxAmount);
				columnNetAmount = new DataColumn("NetAmount", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnNetAmount);
				columnBarcode = new DataColumn("Barcode", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnBarcode);
				columnUnitname = new DataColumn("Unitname", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnUnitname);
				columnDiscountAmount = new DataColumn("DiscountAmount", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnDiscountAmount);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public dtCustomerItemSalesReportPOSRow NewdtCustomerItemSalesReportPOSRow()
			{
				return (dtCustomerItemSalesReportPOSRow)NewRow();
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new dtCustomerItemSalesReportPOSRow(builder);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override Type GetRowType()
			{
				return typeof(dtCustomerItemSalesReportPOSRow);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this.dtCustomerItemSalesReportPOSRowChanged != null)
				{
					this.dtCustomerItemSalesReportPOSRowChanged(this, new dtCustomerItemSalesReportPOSRowChangeEvent((dtCustomerItemSalesReportPOSRow)e.Row, e.Action));
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this.dtCustomerItemSalesReportPOSRowChanging != null)
				{
					this.dtCustomerItemSalesReportPOSRowChanging(this, new dtCustomerItemSalesReportPOSRowChangeEvent((dtCustomerItemSalesReportPOSRow)e.Row, e.Action));
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.dtCustomerItemSalesReportPOSRowDeleted != null)
				{
					this.dtCustomerItemSalesReportPOSRowDeleted(this, new dtCustomerItemSalesReportPOSRowChangeEvent((dtCustomerItemSalesReportPOSRow)e.Row, e.Action));
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this.dtCustomerItemSalesReportPOSRowDeleting != null)
				{
					this.dtCustomerItemSalesReportPOSRowDeleting(this, new dtCustomerItemSalesReportPOSRowChangeEvent((dtCustomerItemSalesReportPOSRow)e.Row, e.Action));
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void RemovedtCustomerItemSalesReportPOSRow(dtCustomerItemSalesReportPOSRow row)
			{
				base.Rows.Remove(row);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				dsCustomerItemSalesReportPOS dsCustomerItemSalesReportPOS2 = new dsCustomerItemSalesReportPOS();
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
				xmlSchemaAttribute.FixedValue = dsCustomerItemSalesReportPOS2.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "dtCustomerItemSalesReportPOSDataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = dsCustomerItemSalesReportPOS2.GetSchemaSerializable();
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

		public class dtCustomerItemSalesReportPOSRow : DataRow
		{
			private dtCustomerItemSalesReportPOSDataTable tabledtCustomerItemSalesReportPOS;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string InvoiceDate
			{
				get
				{
					try
					{
						return (string)base[tabledtCustomerItemSalesReportPOS.InvoiceDateColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'InvoiceDate' in table 'dtCustomerItemSalesReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtCustomerItemSalesReportPOS.InvoiceDateColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string InvoiceNo
			{
				get
				{
					try
					{
						return (string)base[tabledtCustomerItemSalesReportPOS.InvoiceNoColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'InvoiceNo' in table 'dtCustomerItemSalesReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtCustomerItemSalesReportPOS.InvoiceNoColumn] = value;
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
						return (string)base[tabledtCustomerItemSalesReportPOS.ProductNameColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'ProductName' in table 'dtCustomerItemSalesReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtCustomerItemSalesReportPOS.ProductNameColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string Quantity
			{
				get
				{
					try
					{
						return (string)base[tabledtCustomerItemSalesReportPOS.QuantityColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Quantity' in table 'dtCustomerItemSalesReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtCustomerItemSalesReportPOS.QuantityColumn] = value;
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
						return (string)base[tabledtCustomerItemSalesReportPOS.TaxAmountColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'TaxAmount' in table 'dtCustomerItemSalesReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtCustomerItemSalesReportPOS.TaxAmountColumn] = value;
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
						return (string)base[tabledtCustomerItemSalesReportPOS.NetAmountColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'NetAmount' in table 'dtCustomerItemSalesReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtCustomerItemSalesReportPOS.NetAmountColumn] = value;
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
						return (string)base[tabledtCustomerItemSalesReportPOS.BarcodeColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Barcode' in table 'dtCustomerItemSalesReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtCustomerItemSalesReportPOS.BarcodeColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string Unitname
			{
				get
				{
					try
					{
						return (string)base[tabledtCustomerItemSalesReportPOS.UnitnameColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Unitname' in table 'dtCustomerItemSalesReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtCustomerItemSalesReportPOS.UnitnameColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string DiscountAmount
			{
				get
				{
					try
					{
						return (string)base[tabledtCustomerItemSalesReportPOS.DiscountAmountColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'DiscountAmount' in table 'dtCustomerItemSalesReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtCustomerItemSalesReportPOS.DiscountAmountColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			internal dtCustomerItemSalesReportPOSRow(DataRowBuilder rb)
				: base(rb)
			{
				tabledtCustomerItemSalesReportPOS = (dtCustomerItemSalesReportPOSDataTable)base.Table;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsInvoiceDateNull()
			{
				return IsNull(tabledtCustomerItemSalesReportPOS.InvoiceDateColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetInvoiceDateNull()
			{
				base[tabledtCustomerItemSalesReportPOS.InvoiceDateColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsInvoiceNoNull()
			{
				return IsNull(tabledtCustomerItemSalesReportPOS.InvoiceNoColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetInvoiceNoNull()
			{
				base[tabledtCustomerItemSalesReportPOS.InvoiceNoColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsProductNameNull()
			{
				return IsNull(tabledtCustomerItemSalesReportPOS.ProductNameColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetProductNameNull()
			{
				base[tabledtCustomerItemSalesReportPOS.ProductNameColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsQuantityNull()
			{
				return IsNull(tabledtCustomerItemSalesReportPOS.QuantityColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetQuantityNull()
			{
				base[tabledtCustomerItemSalesReportPOS.QuantityColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsTaxAmountNull()
			{
				return IsNull(tabledtCustomerItemSalesReportPOS.TaxAmountColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetTaxAmountNull()
			{
				base[tabledtCustomerItemSalesReportPOS.TaxAmountColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsNetAmountNull()
			{
				return IsNull(tabledtCustomerItemSalesReportPOS.NetAmountColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetNetAmountNull()
			{
				base[tabledtCustomerItemSalesReportPOS.NetAmountColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsBarcodeNull()
			{
				return IsNull(tabledtCustomerItemSalesReportPOS.BarcodeColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetBarcodeNull()
			{
				base[tabledtCustomerItemSalesReportPOS.BarcodeColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsUnitnameNull()
			{
				return IsNull(tabledtCustomerItemSalesReportPOS.UnitnameColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetUnitnameNull()
			{
				base[tabledtCustomerItemSalesReportPOS.UnitnameColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsDiscountAmountNull()
			{
				return IsNull(tabledtCustomerItemSalesReportPOS.DiscountAmountColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetDiscountAmountNull()
			{
				base[tabledtCustomerItemSalesReportPOS.DiscountAmountColumn] = Convert.DBNull;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public class dtCustomerItemSalesReportPOSRowChangeEvent : EventArgs
		{
			private dtCustomerItemSalesReportPOSRow eventRow;

			private DataRowAction eventAction;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public dtCustomerItemSalesReportPOSRow Row => eventRow;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataRowAction Action => eventAction;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public dtCustomerItemSalesReportPOSRowChangeEvent(dtCustomerItemSalesReportPOSRow row, DataRowAction action)
			{
				eventRow = row;
				eventAction = action;
			}
		}

		private dtCustomerItemSalesReportPOSDataTable tabledtCustomerItemSalesReportPOS;

		private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public dtCustomerItemSalesReportPOSDataTable dtCustomerItemSalesReportPOS => tabledtCustomerItemSalesReportPOS;

		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DebuggerNonUserCode]
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

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		public new DataTableCollection Tables => base.Tables;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new DataRelationCollection Relations => base.Relations;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		public dsCustomerItemSalesReportPOS()
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
		protected dsCustomerItemSalesReportPOS(SerializationInfo info, StreamingContext context)
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
				if (dataSet.Tables["dtCustomerItemSalesReportPOS"] != null)
				{
					base.Tables.Add(new dtCustomerItemSalesReportPOSDataTable(dataSet.Tables["dtCustomerItemSalesReportPOS"]));
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

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		public override System.Data.DataSet Clone()
		{
			dsCustomerItemSalesReportPOS dsCustomerItemSalesReportPOS2 = (dsCustomerItemSalesReportPOS)base.Clone();
			dsCustomerItemSalesReportPOS2.InitVars();
			dsCustomerItemSalesReportPOS2.SchemaSerializationMode = SchemaSerializationMode;
			return dsCustomerItemSalesReportPOS2;
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
				if (dataSet.Tables["dtCustomerItemSalesReportPOS"] != null)
				{
					base.Tables.Add(new dtCustomerItemSalesReportPOSDataTable(dataSet.Tables["dtCustomerItemSalesReportPOS"]));
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

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
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
			tabledtCustomerItemSalesReportPOS = (dtCustomerItemSalesReportPOSDataTable)base.Tables["dtCustomerItemSalesReportPOS"];
			if (initTable && tabledtCustomerItemSalesReportPOS != null)
			{
				tabledtCustomerItemSalesReportPOS.InitVars();
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		private void InitClass()
		{
			base.DataSetName = "dsCustomerItemSalesReportPOS";
			base.Prefix = "";
			base.Namespace = "http://tempuri.org/dsCustomerItemSalesReportPOS.xsd";
			base.EnforceConstraints = true;
			SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
			tabledtCustomerItemSalesReportPOS = new dtCustomerItemSalesReportPOSDataTable();
			base.Tables.Add(tabledtCustomerItemSalesReportPOS);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		private bool ShouldSerializedtCustomerItemSalesReportPOS()
		{
			return false;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
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
			dsCustomerItemSalesReportPOS dsCustomerItemSalesReportPOS2 = new dsCustomerItemSalesReportPOS();
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
			xmlSchemaAny.Namespace = dsCustomerItemSalesReportPOS2.Namespace;
			xmlSchemaSequence.Items.Add(xmlSchemaAny);
			xmlSchemaComplexType.Particle = xmlSchemaSequence;
			XmlSchema schemaSerializable = dsCustomerItemSalesReportPOS2.GetSchemaSerializable();
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
