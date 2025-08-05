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
	[DesignerCategory("code")]
	[XmlRoot("dsPendingKOT")]
	[ToolboxItem(true)]
	[XmlSchemaProvider("GetTypedDataSetSchema")]
	public class dsPendingKOT : System.Data.DataSet
	{
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public delegate void dtPendingKOTRowChangeEventHandler(object sender, dtPendingKOTRowChangeEvent e);

		[Serializable]
		[XmlSchemaProvider("GetTypedTableSchema")]
		public class dtPendingKOTDataTable : TypedTableBase<dtPendingKOTRow>
		{
			private DataColumn columnAccountName;

			private DataColumn columnDateAndTime;

			private DataColumn columnTerminal;

			private DataColumn columnSerialNo;

			private DataColumn columnTotalAmount;

			private DataColumn columnTockenNo;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn AccountNameColumn => columnAccountName;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn DateAndTimeColumn => columnDateAndTime;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn TerminalColumn => columnTerminal;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn SerialNoColumn => columnSerialNo;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn TotalAmountColumn => columnTotalAmount;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn TockenNoColumn => columnTockenNo;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[Browsable(false)]
			public int Count => base.Rows.Count;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtPendingKOTRow this[int index] => (dtPendingKOTRow)base.Rows[index];

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtPendingKOTRowChangeEventHandler dtPendingKOTRowChanging;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtPendingKOTRowChangeEventHandler dtPendingKOTRowChanged;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtPendingKOTRowChangeEventHandler dtPendingKOTRowDeleting;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtPendingKOTRowChangeEventHandler dtPendingKOTRowDeleted;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtPendingKOTDataTable()
			{
				base.TableName = "dtPendingKOT";
				BeginInit();
				InitClass();
				EndInit();
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			internal dtPendingKOTDataTable(DataTable table)
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
			protected dtPendingKOTDataTable(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
				InitVars();
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void AdddtPendingKOTRow(dtPendingKOTRow row)
			{
				base.Rows.Add(row);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtPendingKOTRow AdddtPendingKOTRow(string AccountName, string DateAndTime, string Terminal, string SerialNo, string TotalAmount, string TockenNo)
			{
				dtPendingKOTRow dtPendingKOTRow = (dtPendingKOTRow)NewRow();
				object[] array2 = (dtPendingKOTRow.ItemArray = new object[6] { AccountName, DateAndTime, Terminal, SerialNo, TotalAmount, TockenNo });
				base.Rows.Add(dtPendingKOTRow);
				return dtPendingKOTRow;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public override DataTable Clone()
			{
				dtPendingKOTDataTable dtPendingKOTDataTable = (dtPendingKOTDataTable)base.Clone();
				dtPendingKOTDataTable.InitVars();
				return dtPendingKOTDataTable;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override DataTable CreateInstance()
			{
				return new dtPendingKOTDataTable();
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			internal void InitVars()
			{
				columnAccountName = base.Columns["AccountName"];
				columnDateAndTime = base.Columns["DateAndTime"];
				columnTerminal = base.Columns["Terminal"];
				columnSerialNo = base.Columns["SerialNo"];
				columnTotalAmount = base.Columns["TotalAmount"];
				columnTockenNo = base.Columns["TockenNo"];
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			private void InitClass()
			{
				columnAccountName = new DataColumn("AccountName", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnAccountName);
				columnDateAndTime = new DataColumn("DateAndTime", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnDateAndTime);
				columnTerminal = new DataColumn("Terminal", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnTerminal);
				columnSerialNo = new DataColumn("SerialNo", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnSerialNo);
				columnTotalAmount = new DataColumn("TotalAmount", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnTotalAmount);
				columnTockenNo = new DataColumn("TockenNo", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnTockenNo);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtPendingKOTRow NewdtPendingKOTRow()
			{
				return (dtPendingKOTRow)NewRow();
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new dtPendingKOTRow(builder);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override Type GetRowType()
			{
				return typeof(dtPendingKOTRow);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this.dtPendingKOTRowChanged != null)
				{
					this.dtPendingKOTRowChanged(this, new dtPendingKOTRowChangeEvent((dtPendingKOTRow)e.Row, e.Action));
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this.dtPendingKOTRowChanging != null)
				{
					this.dtPendingKOTRowChanging(this, new dtPendingKOTRowChangeEvent((dtPendingKOTRow)e.Row, e.Action));
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.dtPendingKOTRowDeleted != null)
				{
					this.dtPendingKOTRowDeleted(this, new dtPendingKOTRowChangeEvent((dtPendingKOTRow)e.Row, e.Action));
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this.dtPendingKOTRowDeleting != null)
				{
					this.dtPendingKOTRowDeleting(this, new dtPendingKOTRowChangeEvent((dtPendingKOTRow)e.Row, e.Action));
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void RemovedtPendingKOTRow(dtPendingKOTRow row)
			{
				base.Rows.Remove(row);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				dsPendingKOT dsPendingKOT2 = new dsPendingKOT();
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
				xmlSchemaAttribute.FixedValue = dsPendingKOT2.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "dtPendingKOTDataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = dsPendingKOT2.GetSchemaSerializable();
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

		public class dtPendingKOTRow : DataRow
		{
			private dtPendingKOTDataTable tabledtPendingKOT;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string AccountName
			{
				get
				{
					try
					{
						return (string)base[tabledtPendingKOT.AccountNameColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'AccountName' in table 'dtPendingKOT' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPendingKOT.AccountNameColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string DateAndTime
			{
				get
				{
					try
					{
						return (string)base[tabledtPendingKOT.DateAndTimeColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'DateAndTime' in table 'dtPendingKOT' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPendingKOT.DateAndTimeColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string Terminal
			{
				get
				{
					try
					{
						return (string)base[tabledtPendingKOT.TerminalColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Terminal' in table 'dtPendingKOT' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPendingKOT.TerminalColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string SerialNo
			{
				get
				{
					try
					{
						return (string)base[tabledtPendingKOT.SerialNoColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'SerialNo' in table 'dtPendingKOT' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPendingKOT.SerialNoColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string TotalAmount
			{
				get
				{
					try
					{
						return (string)base[tabledtPendingKOT.TotalAmountColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'TotalAmount' in table 'dtPendingKOT' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPendingKOT.TotalAmountColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string TockenNo
			{
				get
				{
					try
					{
						return (string)base[tabledtPendingKOT.TockenNoColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'TockenNo' in table 'dtPendingKOT' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtPendingKOT.TockenNoColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			internal dtPendingKOTRow(DataRowBuilder rb)
				: base(rb)
			{
				tabledtPendingKOT = (dtPendingKOTDataTable)base.Table;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsAccountNameNull()
			{
				return IsNull(tabledtPendingKOT.AccountNameColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetAccountNameNull()
			{
				base[tabledtPendingKOT.AccountNameColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsDateAndTimeNull()
			{
				return IsNull(tabledtPendingKOT.DateAndTimeColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetDateAndTimeNull()
			{
				base[tabledtPendingKOT.DateAndTimeColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsTerminalNull()
			{
				return IsNull(tabledtPendingKOT.TerminalColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetTerminalNull()
			{
				base[tabledtPendingKOT.TerminalColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsSerialNoNull()
			{
				return IsNull(tabledtPendingKOT.SerialNoColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetSerialNoNull()
			{
				base[tabledtPendingKOT.SerialNoColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsTotalAmountNull()
			{
				return IsNull(tabledtPendingKOT.TotalAmountColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetTotalAmountNull()
			{
				base[tabledtPendingKOT.TotalAmountColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsTockenNoNull()
			{
				return IsNull(tabledtPendingKOT.TockenNoColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetTockenNoNull()
			{
				base[tabledtPendingKOT.TockenNoColumn] = Convert.DBNull;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public class dtPendingKOTRowChangeEvent : EventArgs
		{
			private dtPendingKOTRow eventRow;

			private DataRowAction eventAction;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtPendingKOTRow Row => eventRow;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataRowAction Action => eventAction;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtPendingKOTRowChangeEvent(dtPendingKOTRow row, DataRowAction action)
			{
				eventRow = row;
				eventAction = action;
			}
		}

		private dtPendingKOTDataTable tabledtPendingKOT;

		private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;

		[DebuggerNonUserCode]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[Browsable(false)]
		public dtPendingKOTDataTable dtPendingKOT => tabledtPendingKOT;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DebuggerNonUserCode]
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
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public new DataTableCollection Tables => base.Tables;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DebuggerNonUserCode]
		public new DataRelationCollection Relations => base.Relations;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public dsPendingKOT()
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
		protected dsPendingKOT(SerializationInfo info, StreamingContext context)
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
				if (dataSet.Tables["dtPendingKOT"] != null)
				{
					base.Tables.Add(new dtPendingKOTDataTable(dataSet.Tables["dtPendingKOT"]));
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
			dsPendingKOT dsPendingKOT2 = (dsPendingKOT)base.Clone();
			dsPendingKOT2.InitVars();
			dsPendingKOT2.SchemaSerializationMode = SchemaSerializationMode;
			return dsPendingKOT2;
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
				if (dataSet.Tables["dtPendingKOT"] != null)
				{
					base.Tables.Add(new dtPendingKOTDataTable(dataSet.Tables["dtPendingKOT"]));
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
			tabledtPendingKOT = (dtPendingKOTDataTable)base.Tables["dtPendingKOT"];
			if (initTable && tabledtPendingKOT != null)
			{
				tabledtPendingKOT.InitVars();
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		private void InitClass()
		{
			base.DataSetName = "dsPendingKOT";
			base.Prefix = "";
			base.Namespace = "http://tempuri.org/dsPendingKOT.xsd";
			base.EnforceConstraints = true;
			SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
			tabledtPendingKOT = new dtPendingKOTDataTable();
			base.Tables.Add(tabledtPendingKOT);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		private bool ShouldSerializedtPendingKOT()
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
			dsPendingKOT dsPendingKOT2 = new dsPendingKOT();
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
			xmlSchemaAny.Namespace = dsPendingKOT2.Namespace;
			xmlSchemaSequence.Items.Add(xmlSchemaAny);
			xmlSchemaComplexType.Particle = xmlSchemaSequence;
			XmlSchema schemaSerializable = dsPendingKOT2.GetSchemaSerializable();
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
