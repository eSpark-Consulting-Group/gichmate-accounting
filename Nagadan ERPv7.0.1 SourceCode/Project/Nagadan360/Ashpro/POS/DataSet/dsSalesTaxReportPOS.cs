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
	[DesignerCategory("code")]
	[ToolboxItem(true)]
	[XmlSchemaProvider("GetTypedDataSetSchema")]
	[XmlRoot("dsSalesTaxReportPOS")]
	[HelpKeyword("vs.data.DataSet")]
	public class dsSalesTaxReportPOS : System.Data.DataSet
	{
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public delegate void dtSalesTaxReportPOSRowChangeEventHandler(object sender, dtSalesTaxReportPOSRowChangeEvent e);

		[Serializable]
		[XmlSchemaProvider("GetTypedTableSchema")]
		public class dtSalesTaxReportPOSDataTable : TypedTableBase<dtSalesTaxReportPOSRow>
		{
			private DataColumn columnDate;

			private DataColumn columnReceiptNo;

			private DataColumn columnAccount;

			private DataColumn columnTaxable;

			private DataColumn columnTax;

			private DataColumn columnReturn;

			private DataColumn columnTaxOnReturn;

			private DataColumn columnNet;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn DateColumn => columnDate;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn ReceiptNoColumn => columnReceiptNo;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn AccountColumn => columnAccount;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn TaxableColumn => columnTaxable;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn TaxColumn => columnTax;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn ReturnColumn => columnReturn;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn TaxOnReturnColumn => columnTaxOnReturn;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn NetColumn => columnNet;

			[Browsable(false)]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public int Count => base.Rows.Count;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public dtSalesTaxReportPOSRow this[int index] => (dtSalesTaxReportPOSRow)base.Rows[index];

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtSalesTaxReportPOSRowChangeEventHandler dtSalesTaxReportPOSRowChanging;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtSalesTaxReportPOSRowChangeEventHandler dtSalesTaxReportPOSRowChanged;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtSalesTaxReportPOSRowChangeEventHandler dtSalesTaxReportPOSRowDeleting;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtSalesTaxReportPOSRowChangeEventHandler dtSalesTaxReportPOSRowDeleted;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtSalesTaxReportPOSDataTable()
			{
				base.TableName = "dtSalesTaxReportPOS";
				BeginInit();
				InitClass();
				EndInit();
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			internal dtSalesTaxReportPOSDataTable(DataTable table)
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
			protected dtSalesTaxReportPOSDataTable(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
				InitVars();
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void AdddtSalesTaxReportPOSRow(dtSalesTaxReportPOSRow row)
			{
				base.Rows.Add(row);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtSalesTaxReportPOSRow AdddtSalesTaxReportPOSRow(string Date, string ReceiptNo, string Account, string Taxable, string Tax, string Return, string TaxOnReturn, string Net)
			{
				dtSalesTaxReportPOSRow dtSalesTaxReportPOSRow = (dtSalesTaxReportPOSRow)NewRow();
				object[] array2 = (dtSalesTaxReportPOSRow.ItemArray = new object[8] { Date, ReceiptNo, Account, Taxable, Tax, Return, TaxOnReturn, Net });
				base.Rows.Add(dtSalesTaxReportPOSRow);
				return dtSalesTaxReportPOSRow;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public override DataTable Clone()
			{
				dtSalesTaxReportPOSDataTable dtSalesTaxReportPOSDataTable = (dtSalesTaxReportPOSDataTable)base.Clone();
				dtSalesTaxReportPOSDataTable.InitVars();
				return dtSalesTaxReportPOSDataTable;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataTable CreateInstance()
			{
				return new dtSalesTaxReportPOSDataTable();
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			internal void InitVars()
			{
				columnDate = base.Columns["Date"];
				columnReceiptNo = base.Columns["ReceiptNo"];
				columnAccount = base.Columns["Account"];
				columnTaxable = base.Columns["Taxable"];
				columnTax = base.Columns["Tax"];
				columnReturn = base.Columns["Return"];
				columnTaxOnReturn = base.Columns["TaxOnReturn"];
				columnNet = base.Columns["Net"];
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			private void InitClass()
			{
				columnDate = new DataColumn("Date", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnDate);
				columnReceiptNo = new DataColumn("ReceiptNo", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnReceiptNo);
				columnAccount = new DataColumn("Account", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnAccount);
				columnTaxable = new DataColumn("Taxable", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnTaxable);
				columnTax = new DataColumn("Tax", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnTax);
				columnReturn = new DataColumn("Return", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnReturn);
				columnTaxOnReturn = new DataColumn("TaxOnReturn", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnTaxOnReturn);
				columnNet = new DataColumn("Net", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnNet);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtSalesTaxReportPOSRow NewdtSalesTaxReportPOSRow()
			{
				return (dtSalesTaxReportPOSRow)NewRow();
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new dtSalesTaxReportPOSRow(builder);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override Type GetRowType()
			{
				return typeof(dtSalesTaxReportPOSRow);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this.dtSalesTaxReportPOSRowChanged != null)
				{
					this.dtSalesTaxReportPOSRowChanged(this, new dtSalesTaxReportPOSRowChangeEvent((dtSalesTaxReportPOSRow)e.Row, e.Action));
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this.dtSalesTaxReportPOSRowChanging != null)
				{
					this.dtSalesTaxReportPOSRowChanging(this, new dtSalesTaxReportPOSRowChangeEvent((dtSalesTaxReportPOSRow)e.Row, e.Action));
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.dtSalesTaxReportPOSRowDeleted != null)
				{
					this.dtSalesTaxReportPOSRowDeleted(this, new dtSalesTaxReportPOSRowChangeEvent((dtSalesTaxReportPOSRow)e.Row, e.Action));
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this.dtSalesTaxReportPOSRowDeleting != null)
				{
					this.dtSalesTaxReportPOSRowDeleting(this, new dtSalesTaxReportPOSRowChangeEvent((dtSalesTaxReportPOSRow)e.Row, e.Action));
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void RemovedtSalesTaxReportPOSRow(dtSalesTaxReportPOSRow row)
			{
				base.Rows.Remove(row);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				dsSalesTaxReportPOS dsSalesTaxReportPOS2 = new dsSalesTaxReportPOS();
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
				xmlSchemaAttribute.FixedValue = dsSalesTaxReportPOS2.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "dtSalesTaxReportPOSDataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = dsSalesTaxReportPOS2.GetSchemaSerializable();
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

		public class dtSalesTaxReportPOSRow : DataRow
		{
			private dtSalesTaxReportPOSDataTable tabledtSalesTaxReportPOS;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string Date
			{
				get
				{
					try
					{
						return (string)base[tabledtSalesTaxReportPOS.DateColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Date' in table 'dtSalesTaxReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSalesTaxReportPOS.DateColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string ReceiptNo
			{
				get
				{
					try
					{
						return (string)base[tabledtSalesTaxReportPOS.ReceiptNoColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'ReceiptNo' in table 'dtSalesTaxReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSalesTaxReportPOS.ReceiptNoColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string Account
			{
				get
				{
					try
					{
						return (string)base[tabledtSalesTaxReportPOS.AccountColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Account' in table 'dtSalesTaxReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSalesTaxReportPOS.AccountColumn] = value;
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
						return (string)base[tabledtSalesTaxReportPOS.TaxableColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Taxable' in table 'dtSalesTaxReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSalesTaxReportPOS.TaxableColumn] = value;
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
						return (string)base[tabledtSalesTaxReportPOS.TaxColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Tax' in table 'dtSalesTaxReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSalesTaxReportPOS.TaxColumn] = value;
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
						return (string)base[tabledtSalesTaxReportPOS.ReturnColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Return' in table 'dtSalesTaxReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSalesTaxReportPOS.ReturnColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string TaxOnReturn
			{
				get
				{
					try
					{
						return (string)base[tabledtSalesTaxReportPOS.TaxOnReturnColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'TaxOnReturn' in table 'dtSalesTaxReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSalesTaxReportPOS.TaxOnReturnColumn] = value;
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
						return (string)base[tabledtSalesTaxReportPOS.NetColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Net' in table 'dtSalesTaxReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSalesTaxReportPOS.NetColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			internal dtSalesTaxReportPOSRow(DataRowBuilder rb)
				: base(rb)
			{
				tabledtSalesTaxReportPOS = (dtSalesTaxReportPOSDataTable)base.Table;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsDateNull()
			{
				return IsNull(tabledtSalesTaxReportPOS.DateColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetDateNull()
			{
				base[tabledtSalesTaxReportPOS.DateColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsReceiptNoNull()
			{
				return IsNull(tabledtSalesTaxReportPOS.ReceiptNoColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetReceiptNoNull()
			{
				base[tabledtSalesTaxReportPOS.ReceiptNoColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsAccountNull()
			{
				return IsNull(tabledtSalesTaxReportPOS.AccountColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetAccountNull()
			{
				base[tabledtSalesTaxReportPOS.AccountColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsTaxableNull()
			{
				return IsNull(tabledtSalesTaxReportPOS.TaxableColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetTaxableNull()
			{
				base[tabledtSalesTaxReportPOS.TaxableColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsTaxNull()
			{
				return IsNull(tabledtSalesTaxReportPOS.TaxColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetTaxNull()
			{
				base[tabledtSalesTaxReportPOS.TaxColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsReturnNull()
			{
				return IsNull(tabledtSalesTaxReportPOS.ReturnColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetReturnNull()
			{
				base[tabledtSalesTaxReportPOS.ReturnColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsTaxOnReturnNull()
			{
				return IsNull(tabledtSalesTaxReportPOS.TaxOnReturnColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetTaxOnReturnNull()
			{
				base[tabledtSalesTaxReportPOS.TaxOnReturnColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsNetNull()
			{
				return IsNull(tabledtSalesTaxReportPOS.NetColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetNetNull()
			{
				base[tabledtSalesTaxReportPOS.NetColumn] = Convert.DBNull;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public class dtSalesTaxReportPOSRowChangeEvent : EventArgs
		{
			private dtSalesTaxReportPOSRow eventRow;

			private DataRowAction eventAction;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public dtSalesTaxReportPOSRow Row => eventRow;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataRowAction Action => eventAction;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public dtSalesTaxReportPOSRowChangeEvent(dtSalesTaxReportPOSRow row, DataRowAction action)
			{
				eventRow = row;
				eventAction = action;
			}
		}

		private dtSalesTaxReportPOSDataTable tabledtSalesTaxReportPOS;

		private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[Browsable(false)]
		public dtSalesTaxReportPOSDataTable dtSalesTaxReportPOS => tabledtSalesTaxReportPOS;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[Browsable(true)]
		[DebuggerNonUserCode]
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
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public new DataTableCollection Tables => base.Tables;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new DataRelationCollection Relations => base.Relations;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		public dsSalesTaxReportPOS()
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
		protected dsSalesTaxReportPOS(SerializationInfo info, StreamingContext context)
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
				if (dataSet.Tables["dtSalesTaxReportPOS"] != null)
				{
					base.Tables.Add(new dtSalesTaxReportPOSDataTable(dataSet.Tables["dtSalesTaxReportPOS"]));
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
			dsSalesTaxReportPOS dsSalesTaxReportPOS2 = (dsSalesTaxReportPOS)base.Clone();
			dsSalesTaxReportPOS2.InitVars();
			dsSalesTaxReportPOS2.SchemaSerializationMode = SchemaSerializationMode;
			return dsSalesTaxReportPOS2;
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
				if (dataSet.Tables["dtSalesTaxReportPOS"] != null)
				{
					base.Tables.Add(new dtSalesTaxReportPOSDataTable(dataSet.Tables["dtSalesTaxReportPOS"]));
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
			tabledtSalesTaxReportPOS = (dtSalesTaxReportPOSDataTable)base.Tables["dtSalesTaxReportPOS"];
			if (initTable && tabledtSalesTaxReportPOS != null)
			{
				tabledtSalesTaxReportPOS.InitVars();
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		private void InitClass()
		{
			base.DataSetName = "dsSalesTaxReportPOS";
			base.Prefix = "";
			base.Namespace = "http://tempuri.org/dsSalesTaxReportPOS.xsd";
			base.EnforceConstraints = true;
			SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
			tabledtSalesTaxReportPOS = new dtSalesTaxReportPOSDataTable();
			base.Tables.Add(tabledtSalesTaxReportPOS);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		private bool ShouldSerializedtSalesTaxReportPOS()
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
			dsSalesTaxReportPOS dsSalesTaxReportPOS2 = new dsSalesTaxReportPOS();
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
			xmlSchemaAny.Namespace = dsSalesTaxReportPOS2.Namespace;
			xmlSchemaSequence.Items.Add(xmlSchemaAny);
			xmlSchemaComplexType.Particle = xmlSchemaSequence;
			XmlSchema schemaSerializable = dsSalesTaxReportPOS2.GetSchemaSerializable();
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
