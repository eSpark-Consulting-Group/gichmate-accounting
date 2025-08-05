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
	[XmlRoot("dsVoucherInvoice")]
	[HelpKeyword("vs.data.DataSet")]
	[XmlSchemaProvider("GetTypedDataSetSchema")]
	[DesignerCategory("code")]
	[ToolboxItem(true)]
	public class dsVoucherInvoice : System.Data.DataSet
	{
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public delegate void dtVoucherInvoiceRowChangeEventHandler(object sender, dtVoucherInvoiceRowChangeEvent e);

		[Serializable]
		[XmlSchemaProvider("GetTypedTableSchema")]
		public class dtVoucherInvoiceDataTable : TypedTableBase<dtVoucherInvoiceRow>
		{
			private DataColumn columnInvoiceNo;

			private DataColumn columnTotalAmount;

			private DataColumn columnPaidAmount;

			private DataColumn columnBalance;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn InvoiceNoColumn => columnInvoiceNo;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn TotalAmountColumn => columnTotalAmount;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn PaidAmountColumn => columnPaidAmount;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn BalanceColumn => columnBalance;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[Browsable(false)]
			public int Count => base.Rows.Count;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public dtVoucherInvoiceRow this[int index] => (dtVoucherInvoiceRow)base.Rows[index];

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtVoucherInvoiceRowChangeEventHandler dtVoucherInvoiceRowChanging;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtVoucherInvoiceRowChangeEventHandler dtVoucherInvoiceRowChanged;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtVoucherInvoiceRowChangeEventHandler dtVoucherInvoiceRowDeleting;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtVoucherInvoiceRowChangeEventHandler dtVoucherInvoiceRowDeleted;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtVoucherInvoiceDataTable()
			{
				base.TableName = "dtVoucherInvoice";
				BeginInit();
				InitClass();
				EndInit();
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			internal dtVoucherInvoiceDataTable(DataTable table)
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
			protected dtVoucherInvoiceDataTable(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
				InitVars();
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void AdddtVoucherInvoiceRow(dtVoucherInvoiceRow row)
			{
				base.Rows.Add(row);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtVoucherInvoiceRow AdddtVoucherInvoiceRow(string InvoiceNo, string TotalAmount, string PaidAmount, string Balance)
			{
				dtVoucherInvoiceRow dtVoucherInvoiceRow = (dtVoucherInvoiceRow)NewRow();
				object[] array2 = (dtVoucherInvoiceRow.ItemArray = new object[4] { InvoiceNo, TotalAmount, PaidAmount, Balance });
				base.Rows.Add(dtVoucherInvoiceRow);
				return dtVoucherInvoiceRow;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public override DataTable Clone()
			{
				dtVoucherInvoiceDataTable dtVoucherInvoiceDataTable = (dtVoucherInvoiceDataTable)base.Clone();
				dtVoucherInvoiceDataTable.InitVars();
				return dtVoucherInvoiceDataTable;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataTable CreateInstance()
			{
				return new dtVoucherInvoiceDataTable();
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			internal void InitVars()
			{
				columnInvoiceNo = base.Columns["InvoiceNo"];
				columnTotalAmount = base.Columns["TotalAmount"];
				columnPaidAmount = base.Columns["PaidAmount"];
				columnBalance = base.Columns["Balance"];
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			private void InitClass()
			{
				columnInvoiceNo = new DataColumn("InvoiceNo", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnInvoiceNo);
				columnTotalAmount = new DataColumn("TotalAmount", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnTotalAmount);
				columnPaidAmount = new DataColumn("PaidAmount", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnPaidAmount);
				columnBalance = new DataColumn("Balance", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnBalance);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtVoucherInvoiceRow NewdtVoucherInvoiceRow()
			{
				return (dtVoucherInvoiceRow)NewRow();
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new dtVoucherInvoiceRow(builder);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override Type GetRowType()
			{
				return typeof(dtVoucherInvoiceRow);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this.dtVoucherInvoiceRowChanged != null)
				{
					this.dtVoucherInvoiceRowChanged(this, new dtVoucherInvoiceRowChangeEvent((dtVoucherInvoiceRow)e.Row, e.Action));
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this.dtVoucherInvoiceRowChanging != null)
				{
					this.dtVoucherInvoiceRowChanging(this, new dtVoucherInvoiceRowChangeEvent((dtVoucherInvoiceRow)e.Row, e.Action));
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.dtVoucherInvoiceRowDeleted != null)
				{
					this.dtVoucherInvoiceRowDeleted(this, new dtVoucherInvoiceRowChangeEvent((dtVoucherInvoiceRow)e.Row, e.Action));
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this.dtVoucherInvoiceRowDeleting != null)
				{
					this.dtVoucherInvoiceRowDeleting(this, new dtVoucherInvoiceRowChangeEvent((dtVoucherInvoiceRow)e.Row, e.Action));
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void RemovedtVoucherInvoiceRow(dtVoucherInvoiceRow row)
			{
				base.Rows.Remove(row);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				dsVoucherInvoice dsVoucherInvoice2 = new dsVoucherInvoice();
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
				xmlSchemaAttribute.FixedValue = dsVoucherInvoice2.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "dtVoucherInvoiceDataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = dsVoucherInvoice2.GetSchemaSerializable();
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

		public class dtVoucherInvoiceRow : DataRow
		{
			private dtVoucherInvoiceDataTable tabledtVoucherInvoice;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string InvoiceNo
			{
				get
				{
					try
					{
						return (string)base[tabledtVoucherInvoice.InvoiceNoColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'InvoiceNo' in table 'dtVoucherInvoice' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtVoucherInvoice.InvoiceNoColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string TotalAmount
			{
				get
				{
					try
					{
						return (string)base[tabledtVoucherInvoice.TotalAmountColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'TotalAmount' in table 'dtVoucherInvoice' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtVoucherInvoice.TotalAmountColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string PaidAmount
			{
				get
				{
					try
					{
						return (string)base[tabledtVoucherInvoice.PaidAmountColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'PaidAmount' in table 'dtVoucherInvoice' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtVoucherInvoice.PaidAmountColumn] = value;
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
						return (string)base[tabledtVoucherInvoice.BalanceColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Balance' in table 'dtVoucherInvoice' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtVoucherInvoice.BalanceColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			internal dtVoucherInvoiceRow(DataRowBuilder rb)
				: base(rb)
			{
				tabledtVoucherInvoice = (dtVoucherInvoiceDataTable)base.Table;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsInvoiceNoNull()
			{
				return IsNull(tabledtVoucherInvoice.InvoiceNoColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetInvoiceNoNull()
			{
				base[tabledtVoucherInvoice.InvoiceNoColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsTotalAmountNull()
			{
				return IsNull(tabledtVoucherInvoice.TotalAmountColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetTotalAmountNull()
			{
				base[tabledtVoucherInvoice.TotalAmountColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsPaidAmountNull()
			{
				return IsNull(tabledtVoucherInvoice.PaidAmountColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetPaidAmountNull()
			{
				base[tabledtVoucherInvoice.PaidAmountColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsBalanceNull()
			{
				return IsNull(tabledtVoucherInvoice.BalanceColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetBalanceNull()
			{
				base[tabledtVoucherInvoice.BalanceColumn] = Convert.DBNull;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public class dtVoucherInvoiceRowChangeEvent : EventArgs
		{
			private dtVoucherInvoiceRow eventRow;

			private DataRowAction eventAction;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtVoucherInvoiceRow Row => eventRow;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataRowAction Action => eventAction;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtVoucherInvoiceRowChangeEvent(dtVoucherInvoiceRow row, DataRowAction action)
			{
				eventRow = row;
				eventAction = action;
			}
		}

		private dtVoucherInvoiceDataTable tabledtVoucherInvoice;

		private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public dtVoucherInvoiceDataTable dtVoucherInvoice => tabledtVoucherInvoice;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[Browsable(true)]
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

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new DataTableCollection Tables => base.Tables;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		public new DataRelationCollection Relations => base.Relations;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public dsVoucherInvoice()
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
		protected dsVoucherInvoice(SerializationInfo info, StreamingContext context)
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
				if (dataSet.Tables["dtVoucherInvoice"] != null)
				{
					base.Tables.Add(new dtVoucherInvoiceDataTable(dataSet.Tables["dtVoucherInvoice"]));
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
			dsVoucherInvoice dsVoucherInvoice2 = (dsVoucherInvoice)base.Clone();
			dsVoucherInvoice2.InitVars();
			dsVoucherInvoice2.SchemaSerializationMode = SchemaSerializationMode;
			return dsVoucherInvoice2;
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
				if (dataSet.Tables["dtVoucherInvoice"] != null)
				{
					base.Tables.Add(new dtVoucherInvoiceDataTable(dataSet.Tables["dtVoucherInvoice"]));
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
			tabledtVoucherInvoice = (dtVoucherInvoiceDataTable)base.Tables["dtVoucherInvoice"];
			if (initTable && tabledtVoucherInvoice != null)
			{
				tabledtVoucherInvoice.InitVars();
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		private void InitClass()
		{
			base.DataSetName = "dsVoucherInvoice";
			base.Prefix = "";
			base.Namespace = "http://tempuri.org/dsVoucherInvoice.xsd";
			base.EnforceConstraints = true;
			SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
			tabledtVoucherInvoice = new dtVoucherInvoiceDataTable();
			base.Tables.Add(tabledtVoucherInvoice);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		private bool ShouldSerializedtVoucherInvoice()
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

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public static XmlSchemaComplexType GetTypedDataSetSchema(XmlSchemaSet xs)
		{
			dsVoucherInvoice dsVoucherInvoice2 = new dsVoucherInvoice();
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
			xmlSchemaAny.Namespace = dsVoucherInvoice2.Namespace;
			xmlSchemaSequence.Items.Add(xmlSchemaAny);
			xmlSchemaComplexType.Particle = xmlSchemaSequence;
			XmlSchema schemaSerializable = dsVoucherInvoice2.GetSchemaSerializable();
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
