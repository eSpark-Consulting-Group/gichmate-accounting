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
	[XmlRoot("dsSupplierPurchaseReportPOS")]
	[ToolboxItem(true)]
	[DesignerCategory("code")]
	[XmlSchemaProvider("GetTypedDataSetSchema")]
	[HelpKeyword("vs.data.DataSet")]
	public class dsSupplierPurchaseReportPOS : System.Data.DataSet
	{
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public delegate void dtSupplierPurchaseReportPOSRowChangeEventHandler(object sender, dtSupplierPurchaseReportPOSRowChangeEvent e);

		[Serializable]
		[XmlSchemaProvider("GetTypedTableSchema")]
		public class dtSupplierPurchaseReportPOSDataTable : TypedTableBase<dtSupplierPurchaseReportPOSRow>
		{
			private DataColumn columnInvoiceDate;

			private DataColumn columnInvoiceNo;

			private DataColumn columnTaxable;

			private DataColumn columnTax;

			private DataColumn columnNet;

			private DataColumn columnPayment;

			private DataColumn columnReturn;

			private DataColumn columnBalance;

			private DataColumn columnSupplierName;

			private DataColumn columnTaxId;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn InvoiceDateColumn => columnInvoiceDate;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn InvoiceNoColumn => columnInvoiceNo;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn TaxableColumn => columnTaxable;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn TaxColumn => columnTax;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn NetColumn => columnNet;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn PaymentColumn => columnPayment;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn ReturnColumn => columnReturn;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn BalanceColumn => columnBalance;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn SupplierNameColumn => columnSupplierName;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn TaxIdColumn => columnTaxId;

			[Browsable(false)]
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public int Count => base.Rows.Count;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public dtSupplierPurchaseReportPOSRow this[int index] => (dtSupplierPurchaseReportPOSRow)base.Rows[index];

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtSupplierPurchaseReportPOSRowChangeEventHandler dtSupplierPurchaseReportPOSRowChanging;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtSupplierPurchaseReportPOSRowChangeEventHandler dtSupplierPurchaseReportPOSRowChanged;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtSupplierPurchaseReportPOSRowChangeEventHandler dtSupplierPurchaseReportPOSRowDeleting;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtSupplierPurchaseReportPOSRowChangeEventHandler dtSupplierPurchaseReportPOSRowDeleted;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtSupplierPurchaseReportPOSDataTable()
			{
				base.TableName = "dtSupplierPurchaseReportPOS";
				BeginInit();
				InitClass();
				EndInit();
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			internal dtSupplierPurchaseReportPOSDataTable(DataTable table)
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
			protected dtSupplierPurchaseReportPOSDataTable(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
				InitVars();
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void AdddtSupplierPurchaseReportPOSRow(dtSupplierPurchaseReportPOSRow row)
			{
				base.Rows.Add(row);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtSupplierPurchaseReportPOSRow AdddtSupplierPurchaseReportPOSRow(string InvoiceDate, string InvoiceNo, string Taxable, string Tax, string Net, string Payment, string Return, string Balance, string SupplierName, string TaxId)
			{
				dtSupplierPurchaseReportPOSRow dtSupplierPurchaseReportPOSRow = (dtSupplierPurchaseReportPOSRow)NewRow();
				object[] array2 = (dtSupplierPurchaseReportPOSRow.ItemArray = new object[10] { InvoiceDate, InvoiceNo, Taxable, Tax, Net, Payment, Return, Balance, SupplierName, TaxId });
				base.Rows.Add(dtSupplierPurchaseReportPOSRow);
				return dtSupplierPurchaseReportPOSRow;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public override DataTable Clone()
			{
				dtSupplierPurchaseReportPOSDataTable dtSupplierPurchaseReportPOSDataTable = (dtSupplierPurchaseReportPOSDataTable)base.Clone();
				dtSupplierPurchaseReportPOSDataTable.InitVars();
				return dtSupplierPurchaseReportPOSDataTable;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataTable CreateInstance()
			{
				return new dtSupplierPurchaseReportPOSDataTable();
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			internal void InitVars()
			{
				columnInvoiceDate = base.Columns["InvoiceDate"];
				columnInvoiceNo = base.Columns["InvoiceNo"];
				columnTaxable = base.Columns["Taxable"];
				columnTax = base.Columns["Tax"];
				columnNet = base.Columns["Net"];
				columnPayment = base.Columns["Payment"];
				columnReturn = base.Columns["Return"];
				columnBalance = base.Columns["Balance"];
				columnSupplierName = base.Columns["SupplierName"];
				columnTaxId = base.Columns["TaxId"];
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			private void InitClass()
			{
				columnInvoiceDate = new DataColumn("InvoiceDate", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnInvoiceDate);
				columnInvoiceNo = new DataColumn("InvoiceNo", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnInvoiceNo);
				columnTaxable = new DataColumn("Taxable", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnTaxable);
				columnTax = new DataColumn("Tax", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnTax);
				columnNet = new DataColumn("Net", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnNet);
				columnPayment = new DataColumn("Payment", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnPayment);
				columnReturn = new DataColumn("Return", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnReturn);
				columnBalance = new DataColumn("Balance", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnBalance);
				columnSupplierName = new DataColumn("SupplierName", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnSupplierName);
				columnTaxId = new DataColumn("TaxId", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnTaxId);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public dtSupplierPurchaseReportPOSRow NewdtSupplierPurchaseReportPOSRow()
			{
				return (dtSupplierPurchaseReportPOSRow)NewRow();
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new dtSupplierPurchaseReportPOSRow(builder);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override Type GetRowType()
			{
				return typeof(dtSupplierPurchaseReportPOSRow);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this.dtSupplierPurchaseReportPOSRowChanged != null)
				{
					this.dtSupplierPurchaseReportPOSRowChanged(this, new dtSupplierPurchaseReportPOSRowChangeEvent((dtSupplierPurchaseReportPOSRow)e.Row, e.Action));
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this.dtSupplierPurchaseReportPOSRowChanging != null)
				{
					this.dtSupplierPurchaseReportPOSRowChanging(this, new dtSupplierPurchaseReportPOSRowChangeEvent((dtSupplierPurchaseReportPOSRow)e.Row, e.Action));
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.dtSupplierPurchaseReportPOSRowDeleted != null)
				{
					this.dtSupplierPurchaseReportPOSRowDeleted(this, new dtSupplierPurchaseReportPOSRowChangeEvent((dtSupplierPurchaseReportPOSRow)e.Row, e.Action));
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this.dtSupplierPurchaseReportPOSRowDeleting != null)
				{
					this.dtSupplierPurchaseReportPOSRowDeleting(this, new dtSupplierPurchaseReportPOSRowChangeEvent((dtSupplierPurchaseReportPOSRow)e.Row, e.Action));
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void RemovedtSupplierPurchaseReportPOSRow(dtSupplierPurchaseReportPOSRow row)
			{
				base.Rows.Remove(row);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				dsSupplierPurchaseReportPOS dsSupplierPurchaseReportPOS2 = new dsSupplierPurchaseReportPOS();
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
				xmlSchemaAttribute.FixedValue = dsSupplierPurchaseReportPOS2.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "dtSupplierPurchaseReportPOSDataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = dsSupplierPurchaseReportPOS2.GetSchemaSerializable();
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

		public class dtSupplierPurchaseReportPOSRow : DataRow
		{
			private dtSupplierPurchaseReportPOSDataTable tabledtSupplierPurchaseReportPOS;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string InvoiceDate
			{
				get
				{
					try
					{
						return (string)base[tabledtSupplierPurchaseReportPOS.InvoiceDateColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'InvoiceDate' in table 'dtSupplierPurchaseReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSupplierPurchaseReportPOS.InvoiceDateColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string InvoiceNo
			{
				get
				{
					try
					{
						return (string)base[tabledtSupplierPurchaseReportPOS.InvoiceNoColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'InvoiceNo' in table 'dtSupplierPurchaseReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSupplierPurchaseReportPOS.InvoiceNoColumn] = value;
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
						return (string)base[tabledtSupplierPurchaseReportPOS.TaxableColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Taxable' in table 'dtSupplierPurchaseReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSupplierPurchaseReportPOS.TaxableColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string Tax
			{
				get
				{
					try
					{
						return (string)base[tabledtSupplierPurchaseReportPOS.TaxColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Tax' in table 'dtSupplierPurchaseReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSupplierPurchaseReportPOS.TaxColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string Net
			{
				get
				{
					try
					{
						return (string)base[tabledtSupplierPurchaseReportPOS.NetColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Net' in table 'dtSupplierPurchaseReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSupplierPurchaseReportPOS.NetColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string Payment
			{
				get
				{
					try
					{
						return (string)base[tabledtSupplierPurchaseReportPOS.PaymentColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Payment' in table 'dtSupplierPurchaseReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSupplierPurchaseReportPOS.PaymentColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string Return
			{
				get
				{
					try
					{
						return (string)base[tabledtSupplierPurchaseReportPOS.ReturnColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Return' in table 'dtSupplierPurchaseReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSupplierPurchaseReportPOS.ReturnColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string Balance
			{
				get
				{
					try
					{
						return (string)base[tabledtSupplierPurchaseReportPOS.BalanceColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Balance' in table 'dtSupplierPurchaseReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSupplierPurchaseReportPOS.BalanceColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string SupplierName
			{
				get
				{
					try
					{
						return (string)base[tabledtSupplierPurchaseReportPOS.SupplierNameColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'SupplierName' in table 'dtSupplierPurchaseReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSupplierPurchaseReportPOS.SupplierNameColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string TaxId
			{
				get
				{
					try
					{
						return (string)base[tabledtSupplierPurchaseReportPOS.TaxIdColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'TaxId' in table 'dtSupplierPurchaseReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSupplierPurchaseReportPOS.TaxIdColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			internal dtSupplierPurchaseReportPOSRow(DataRowBuilder rb)
				: base(rb)
			{
				tabledtSupplierPurchaseReportPOS = (dtSupplierPurchaseReportPOSDataTable)base.Table;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsInvoiceDateNull()
			{
				return IsNull(tabledtSupplierPurchaseReportPOS.InvoiceDateColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetInvoiceDateNull()
			{
				base[tabledtSupplierPurchaseReportPOS.InvoiceDateColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsInvoiceNoNull()
			{
				return IsNull(tabledtSupplierPurchaseReportPOS.InvoiceNoColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetInvoiceNoNull()
			{
				base[tabledtSupplierPurchaseReportPOS.InvoiceNoColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsTaxableNull()
			{
				return IsNull(tabledtSupplierPurchaseReportPOS.TaxableColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetTaxableNull()
			{
				base[tabledtSupplierPurchaseReportPOS.TaxableColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsTaxNull()
			{
				return IsNull(tabledtSupplierPurchaseReportPOS.TaxColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetTaxNull()
			{
				base[tabledtSupplierPurchaseReportPOS.TaxColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsNetNull()
			{
				return IsNull(tabledtSupplierPurchaseReportPOS.NetColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetNetNull()
			{
				base[tabledtSupplierPurchaseReportPOS.NetColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsPaymentNull()
			{
				return IsNull(tabledtSupplierPurchaseReportPOS.PaymentColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetPaymentNull()
			{
				base[tabledtSupplierPurchaseReportPOS.PaymentColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsReturnNull()
			{
				return IsNull(tabledtSupplierPurchaseReportPOS.ReturnColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetReturnNull()
			{
				base[tabledtSupplierPurchaseReportPOS.ReturnColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsBalanceNull()
			{
				return IsNull(tabledtSupplierPurchaseReportPOS.BalanceColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetBalanceNull()
			{
				base[tabledtSupplierPurchaseReportPOS.BalanceColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsSupplierNameNull()
			{
				return IsNull(tabledtSupplierPurchaseReportPOS.SupplierNameColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetSupplierNameNull()
			{
				base[tabledtSupplierPurchaseReportPOS.SupplierNameColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsTaxIdNull()
			{
				return IsNull(tabledtSupplierPurchaseReportPOS.TaxIdColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetTaxIdNull()
			{
				base[tabledtSupplierPurchaseReportPOS.TaxIdColumn] = Convert.DBNull;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public class dtSupplierPurchaseReportPOSRowChangeEvent : EventArgs
		{
			private dtSupplierPurchaseReportPOSRow eventRow;

			private DataRowAction eventAction;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtSupplierPurchaseReportPOSRow Row => eventRow;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataRowAction Action => eventAction;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public dtSupplierPurchaseReportPOSRowChangeEvent(dtSupplierPurchaseReportPOSRow row, DataRowAction action)
			{
				eventRow = row;
				eventAction = action;
			}
		}

		private dtSupplierPurchaseReportPOSDataTable tabledtSupplierPurchaseReportPOS;

		private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;

		[DebuggerNonUserCode]
		[Browsable(false)]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public dtSupplierPurchaseReportPOSDataTable dtSupplierPurchaseReportPOS => tabledtSupplierPurchaseReportPOS;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public dsSupplierPurchaseReportPOS()
		{
			BeginInit();
			InitClass();
			CollectionChangeEventHandler value = SchemaChanged;
			base.Tables.CollectionChanged += value;
			base.Relations.CollectionChanged += value;
			EndInit();
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		protected dsSupplierPurchaseReportPOS(SerializationInfo info, StreamingContext context)
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
				if (dataSet.Tables["dtSupplierPurchaseReportPOS"] != null)
				{
					base.Tables.Add(new dtSupplierPurchaseReportPOSDataTable(dataSet.Tables["dtSupplierPurchaseReportPOS"]));
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
			dsSupplierPurchaseReportPOS dsSupplierPurchaseReportPOS2 = (dsSupplierPurchaseReportPOS)base.Clone();
			dsSupplierPurchaseReportPOS2.InitVars();
			dsSupplierPurchaseReportPOS2.SchemaSerializationMode = SchemaSerializationMode;
			return dsSupplierPurchaseReportPOS2;
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
				if (dataSet.Tables["dtSupplierPurchaseReportPOS"] != null)
				{
					base.Tables.Add(new dtSupplierPurchaseReportPOSDataTable(dataSet.Tables["dtSupplierPurchaseReportPOS"]));
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
			tabledtSupplierPurchaseReportPOS = (dtSupplierPurchaseReportPOSDataTable)base.Tables["dtSupplierPurchaseReportPOS"];
			if (initTable && tabledtSupplierPurchaseReportPOS != null)
			{
				tabledtSupplierPurchaseReportPOS.InitVars();
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		private void InitClass()
		{
			base.DataSetName = "dsSupplierPurchaseReportPOS";
			base.Prefix = "";
			base.Namespace = "http://tempuri.org/dsSupplierPurchaseReportPOS.xsd";
			base.EnforceConstraints = true;
			SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
			tabledtSupplierPurchaseReportPOS = new dtSupplierPurchaseReportPOSDataTable();
			base.Tables.Add(tabledtSupplierPurchaseReportPOS);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		private bool ShouldSerializedtSupplierPurchaseReportPOS()
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
			dsSupplierPurchaseReportPOS dsSupplierPurchaseReportPOS2 = new dsSupplierPurchaseReportPOS();
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
			xmlSchemaAny.Namespace = dsSupplierPurchaseReportPOS2.Namespace;
			xmlSchemaSequence.Items.Add(xmlSchemaAny);
			xmlSchemaComplexType.Particle = xmlSchemaSequence;
			XmlSchema schemaSerializable = dsSupplierPurchaseReportPOS2.GetSchemaSerializable();
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
