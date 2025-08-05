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
	[XmlSchemaProvider("GetTypedDataSetSchema")]
	[XmlRoot("dsCustomerSalesReportPOS")]
	[HelpKeyword("vs.data.DataSet")]
	[DesignerCategory("code")]
	[ToolboxItem(true)]
	public class dsCustomerSalesReportPOS : System.Data.DataSet
	{
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public delegate void dtCustomerSalesReportPOSRowChangeEventHandler(object sender, dtCustomerSalesReportPOSRowChangeEvent e);

		[Serializable]
		[XmlSchemaProvider("GetTypedTableSchema")]
		public class dtCustomerSalesReportPOSDataTable : TypedTableBase<dtCustomerSalesReportPOSRow>
		{
			private DataColumn columnInvoiceDate;

			private DataColumn columnInvoiceNo;

			private DataColumn columnTaxable;

			private DataColumn columnTax;

			private DataColumn columnNet;

			private DataColumn columnPayment;

			private DataColumn columnReturn;

			private DataColumn columnBalance;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn InvoiceDateColumn => columnInvoiceDate;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn InvoiceNoColumn => columnInvoiceNo;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn TaxableColumn => columnTaxable;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn TaxColumn => columnTax;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn NetColumn => columnNet;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn PaymentColumn => columnPayment;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn ReturnColumn => columnReturn;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn BalanceColumn => columnBalance;

			[Browsable(false)]
			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public int Count => base.Rows.Count;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public dtCustomerSalesReportPOSRow this[int index] => (dtCustomerSalesReportPOSRow)base.Rows[index];

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtCustomerSalesReportPOSRowChangeEventHandler dtCustomerSalesReportPOSRowChanging;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtCustomerSalesReportPOSRowChangeEventHandler dtCustomerSalesReportPOSRowChanged;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtCustomerSalesReportPOSRowChangeEventHandler dtCustomerSalesReportPOSRowDeleting;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtCustomerSalesReportPOSRowChangeEventHandler dtCustomerSalesReportPOSRowDeleted;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtCustomerSalesReportPOSDataTable()
			{
				base.TableName = "dtCustomerSalesReportPOS";
				BeginInit();
				InitClass();
				EndInit();
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			internal dtCustomerSalesReportPOSDataTable(DataTable table)
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
			protected dtCustomerSalesReportPOSDataTable(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
				InitVars();
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void AdddtCustomerSalesReportPOSRow(dtCustomerSalesReportPOSRow row)
			{
				base.Rows.Add(row);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtCustomerSalesReportPOSRow AdddtCustomerSalesReportPOSRow(string InvoiceDate, string InvoiceNo, string Taxable, string Tax, string Net, string Payment, string Return, string Balance)
			{
				dtCustomerSalesReportPOSRow dtCustomerSalesReportPOSRow = (dtCustomerSalesReportPOSRow)NewRow();
				object[] array2 = (dtCustomerSalesReportPOSRow.ItemArray = new object[8] { InvoiceDate, InvoiceNo, Taxable, Tax, Net, Payment, Return, Balance });
				base.Rows.Add(dtCustomerSalesReportPOSRow);
				return dtCustomerSalesReportPOSRow;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public override DataTable Clone()
			{
				dtCustomerSalesReportPOSDataTable dtCustomerSalesReportPOSDataTable = (dtCustomerSalesReportPOSDataTable)base.Clone();
				dtCustomerSalesReportPOSDataTable.InitVars();
				return dtCustomerSalesReportPOSDataTable;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataTable CreateInstance()
			{
				return new dtCustomerSalesReportPOSDataTable();
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
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
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtCustomerSalesReportPOSRow NewdtCustomerSalesReportPOSRow()
			{
				return (dtCustomerSalesReportPOSRow)NewRow();
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new dtCustomerSalesReportPOSRow(builder);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override Type GetRowType()
			{
				return typeof(dtCustomerSalesReportPOSRow);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this.dtCustomerSalesReportPOSRowChanged != null)
				{
					this.dtCustomerSalesReportPOSRowChanged(this, new dtCustomerSalesReportPOSRowChangeEvent((dtCustomerSalesReportPOSRow)e.Row, e.Action));
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this.dtCustomerSalesReportPOSRowChanging != null)
				{
					this.dtCustomerSalesReportPOSRowChanging(this, new dtCustomerSalesReportPOSRowChangeEvent((dtCustomerSalesReportPOSRow)e.Row, e.Action));
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.dtCustomerSalesReportPOSRowDeleted != null)
				{
					this.dtCustomerSalesReportPOSRowDeleted(this, new dtCustomerSalesReportPOSRowChangeEvent((dtCustomerSalesReportPOSRow)e.Row, e.Action));
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this.dtCustomerSalesReportPOSRowDeleting != null)
				{
					this.dtCustomerSalesReportPOSRowDeleting(this, new dtCustomerSalesReportPOSRowChangeEvent((dtCustomerSalesReportPOSRow)e.Row, e.Action));
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void RemovedtCustomerSalesReportPOSRow(dtCustomerSalesReportPOSRow row)
			{
				base.Rows.Remove(row);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				dsCustomerSalesReportPOS dsCustomerSalesReportPOS2 = new dsCustomerSalesReportPOS();
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
				xmlSchemaAttribute.FixedValue = dsCustomerSalesReportPOS2.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "dtCustomerSalesReportPOSDataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = dsCustomerSalesReportPOS2.GetSchemaSerializable();
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

		public class dtCustomerSalesReportPOSRow : DataRow
		{
			private dtCustomerSalesReportPOSDataTable tabledtCustomerSalesReportPOS;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string InvoiceDate
			{
				get
				{
					try
					{
						return (string)base[tabledtCustomerSalesReportPOS.InvoiceDateColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'InvoiceDate' in table 'dtCustomerSalesReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtCustomerSalesReportPOS.InvoiceDateColumn] = value;
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
						return (string)base[tabledtCustomerSalesReportPOS.InvoiceNoColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'InvoiceNo' in table 'dtCustomerSalesReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtCustomerSalesReportPOS.InvoiceNoColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string Taxable
			{
				get
				{
					try
					{
						return (string)base[tabledtCustomerSalesReportPOS.TaxableColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Taxable' in table 'dtCustomerSalesReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtCustomerSalesReportPOS.TaxableColumn] = value;
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
						return (string)base[tabledtCustomerSalesReportPOS.TaxColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Tax' in table 'dtCustomerSalesReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtCustomerSalesReportPOS.TaxColumn] = value;
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
						return (string)base[tabledtCustomerSalesReportPOS.NetColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Net' in table 'dtCustomerSalesReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtCustomerSalesReportPOS.NetColumn] = value;
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
						return (string)base[tabledtCustomerSalesReportPOS.PaymentColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Payment' in table 'dtCustomerSalesReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtCustomerSalesReportPOS.PaymentColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string Return
			{
				get
				{
					try
					{
						return (string)base[tabledtCustomerSalesReportPOS.ReturnColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Return' in table 'dtCustomerSalesReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtCustomerSalesReportPOS.ReturnColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string Balance
			{
				get
				{
					try
					{
						return (string)base[tabledtCustomerSalesReportPOS.BalanceColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Balance' in table 'dtCustomerSalesReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtCustomerSalesReportPOS.BalanceColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			internal dtCustomerSalesReportPOSRow(DataRowBuilder rb)
				: base(rb)
			{
				tabledtCustomerSalesReportPOS = (dtCustomerSalesReportPOSDataTable)base.Table;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsInvoiceDateNull()
			{
				return IsNull(tabledtCustomerSalesReportPOS.InvoiceDateColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetInvoiceDateNull()
			{
				base[tabledtCustomerSalesReportPOS.InvoiceDateColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsInvoiceNoNull()
			{
				return IsNull(tabledtCustomerSalesReportPOS.InvoiceNoColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetInvoiceNoNull()
			{
				base[tabledtCustomerSalesReportPOS.InvoiceNoColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsTaxableNull()
			{
				return IsNull(tabledtCustomerSalesReportPOS.TaxableColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetTaxableNull()
			{
				base[tabledtCustomerSalesReportPOS.TaxableColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsTaxNull()
			{
				return IsNull(tabledtCustomerSalesReportPOS.TaxColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetTaxNull()
			{
				base[tabledtCustomerSalesReportPOS.TaxColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsNetNull()
			{
				return IsNull(tabledtCustomerSalesReportPOS.NetColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetNetNull()
			{
				base[tabledtCustomerSalesReportPOS.NetColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsPaymentNull()
			{
				return IsNull(tabledtCustomerSalesReportPOS.PaymentColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetPaymentNull()
			{
				base[tabledtCustomerSalesReportPOS.PaymentColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsReturnNull()
			{
				return IsNull(tabledtCustomerSalesReportPOS.ReturnColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetReturnNull()
			{
				base[tabledtCustomerSalesReportPOS.ReturnColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsBalanceNull()
			{
				return IsNull(tabledtCustomerSalesReportPOS.BalanceColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetBalanceNull()
			{
				base[tabledtCustomerSalesReportPOS.BalanceColumn] = Convert.DBNull;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public class dtCustomerSalesReportPOSRowChangeEvent : EventArgs
		{
			private dtCustomerSalesReportPOSRow eventRow;

			private DataRowAction eventAction;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public dtCustomerSalesReportPOSRow Row => eventRow;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataRowAction Action => eventAction;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public dtCustomerSalesReportPOSRowChangeEvent(dtCustomerSalesReportPOSRow row, DataRowAction action)
			{
				eventRow = row;
				eventAction = action;
			}
		}

		private dtCustomerSalesReportPOSDataTable tabledtCustomerSalesReportPOS;

		private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public dtCustomerSalesReportPOSDataTable dtCustomerSalesReportPOS => tabledtCustomerSalesReportPOS;

		[Browsable(true)]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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
		public dsCustomerSalesReportPOS()
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
		protected dsCustomerSalesReportPOS(SerializationInfo info, StreamingContext context)
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
				if (dataSet.Tables["dtCustomerSalesReportPOS"] != null)
				{
					base.Tables.Add(new dtCustomerSalesReportPOSDataTable(dataSet.Tables["dtCustomerSalesReportPOS"]));
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
			dsCustomerSalesReportPOS dsCustomerSalesReportPOS2 = (dsCustomerSalesReportPOS)base.Clone();
			dsCustomerSalesReportPOS2.InitVars();
			dsCustomerSalesReportPOS2.SchemaSerializationMode = SchemaSerializationMode;
			return dsCustomerSalesReportPOS2;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		protected override bool ShouldSerializeTables()
		{
			return false;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
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
				if (dataSet.Tables["dtCustomerSalesReportPOS"] != null)
				{
					base.Tables.Add(new dtCustomerSalesReportPOSDataTable(dataSet.Tables["dtCustomerSalesReportPOS"]));
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

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		internal void InitVars(bool initTable)
		{
			tabledtCustomerSalesReportPOS = (dtCustomerSalesReportPOSDataTable)base.Tables["dtCustomerSalesReportPOS"];
			if (initTable && tabledtCustomerSalesReportPOS != null)
			{
				tabledtCustomerSalesReportPOS.InitVars();
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		private void InitClass()
		{
			base.DataSetName = "dsCustomerSalesReportPOS";
			base.Prefix = "";
			base.Namespace = "http://tempuri.org/dsCustomerSalesReportPOS.xsd";
			base.EnforceConstraints = true;
			SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
			tabledtCustomerSalesReportPOS = new dtCustomerSalesReportPOSDataTable();
			base.Tables.Add(tabledtCustomerSalesReportPOS);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		private bool ShouldSerializedtCustomerSalesReportPOS()
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
			dsCustomerSalesReportPOS dsCustomerSalesReportPOS2 = new dsCustomerSalesReportPOS();
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
			xmlSchemaAny.Namespace = dsCustomerSalesReportPOS2.Namespace;
			xmlSchemaSequence.Items.Add(xmlSchemaAny);
			xmlSchemaComplexType.Particle = xmlSchemaSequence;
			XmlSchema schemaSerializable = dsCustomerSalesReportPOS2.GetSchemaSerializable();
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
