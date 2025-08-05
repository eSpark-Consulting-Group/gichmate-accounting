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
	[XmlRoot("dsSalesSummaryReportPOS")]
	[HelpKeyword("vs.data.DataSet")]
	[ToolboxItem(true)]
	[DesignerCategory("code")]
	public class dsSalesSummaryReportPOS : System.Data.DataSet
	{
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
		public delegate void dtSalesSummaryReportPOSRowChangeEventHandler(object sender, dtSalesSummaryReportPOSRowChangeEvent e);

		[Serializable]
		[XmlSchemaProvider("GetTypedTableSchema")]
		public class dtSalesSummaryReportPOSDataTable : TypedTableBase<dtSalesSummaryReportPOSRow>
		{
			private DataColumn columnDate;

			private DataColumn columnTaxable;

			private DataColumn columnTax;

			private DataColumn columnReturn;

			private DataColumn columnNet;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			public DataColumn DateColumn => columnDate;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn TaxableColumn => columnTaxable;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			public DataColumn TaxColumn => columnTax;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn ReturnColumn => columnReturn;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn NetColumn => columnNet;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[Browsable(false)]
			[DebuggerNonUserCode]
			public int Count => base.Rows.Count;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			public dtSalesSummaryReportPOSRow this[int index] => (dtSalesSummaryReportPOSRow)base.Rows[index];

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			public event dtSalesSummaryReportPOSRowChangeEventHandler dtSalesSummaryReportPOSRowChanging;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			public event dtSalesSummaryReportPOSRowChangeEventHandler dtSalesSummaryReportPOSRowChanged;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			public event dtSalesSummaryReportPOSRowChangeEventHandler dtSalesSummaryReportPOSRowDeleting;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			public event dtSalesSummaryReportPOSRowChangeEventHandler dtSalesSummaryReportPOSRowDeleted;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			public dtSalesSummaryReportPOSDataTable()
			{
				base.TableName = "dtSalesSummaryReportPOS";
				BeginInit();
				InitClass();
				EndInit();
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			internal dtSalesSummaryReportPOSDataTable(DataTable table)
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

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			protected dtSalesSummaryReportPOSDataTable(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
				InitVars();
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			public void AdddtSalesSummaryReportPOSRow(dtSalesSummaryReportPOSRow row)
			{
				base.Rows.Add(row);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			public dtSalesSummaryReportPOSRow AdddtSalesSummaryReportPOSRow(string Date, string Taxable, string Tax, string Return, string Net)
			{
				dtSalesSummaryReportPOSRow dtSalesSummaryReportPOSRow = (dtSalesSummaryReportPOSRow)NewRow();
				object[] array2 = (dtSalesSummaryReportPOSRow.ItemArray = new object[5] { Date, Taxable, Tax, Return, Net });
				base.Rows.Add(dtSalesSummaryReportPOSRow);
				return dtSalesSummaryReportPOSRow;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			public override DataTable Clone()
			{
				dtSalesSummaryReportPOSDataTable dtSalesSummaryReportPOSDataTable = (dtSalesSummaryReportPOSDataTable)base.Clone();
				dtSalesSummaryReportPOSDataTable.InitVars();
				return dtSalesSummaryReportPOSDataTable;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataTable CreateInstance()
			{
				return new dtSalesSummaryReportPOSDataTable();
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			internal void InitVars()
			{
				columnDate = base.Columns["Date"];
				columnTaxable = base.Columns["Taxable"];
				columnTax = base.Columns["Tax"];
				columnReturn = base.Columns["Return"];
				columnNet = base.Columns["Net"];
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			private void InitClass()
			{
				columnDate = new DataColumn("Date", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnDate);
				columnTaxable = new DataColumn("Taxable", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnTaxable);
				columnTax = new DataColumn("Tax", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnTax);
				columnReturn = new DataColumn("Return", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnReturn);
				columnNet = new DataColumn("Net", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnNet);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			public dtSalesSummaryReportPOSRow NewdtSalesSummaryReportPOSRow()
			{
				return (dtSalesSummaryReportPOSRow)NewRow();
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new dtSalesSummaryReportPOSRow(builder);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			protected override Type GetRowType()
			{
				return typeof(dtSalesSummaryReportPOSRow);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this.dtSalesSummaryReportPOSRowChanged != null)
				{
					this.dtSalesSummaryReportPOSRowChanged(this, new dtSalesSummaryReportPOSRowChangeEvent((dtSalesSummaryReportPOSRow)e.Row, e.Action));
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this.dtSalesSummaryReportPOSRowChanging != null)
				{
					this.dtSalesSummaryReportPOSRowChanging(this, new dtSalesSummaryReportPOSRowChangeEvent((dtSalesSummaryReportPOSRow)e.Row, e.Action));
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.dtSalesSummaryReportPOSRowDeleted != null)
				{
					this.dtSalesSummaryReportPOSRowDeleted(this, new dtSalesSummaryReportPOSRowChangeEvent((dtSalesSummaryReportPOSRow)e.Row, e.Action));
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this.dtSalesSummaryReportPOSRowDeleting != null)
				{
					this.dtSalesSummaryReportPOSRowDeleting(this, new dtSalesSummaryReportPOSRowChangeEvent((dtSalesSummaryReportPOSRow)e.Row, e.Action));
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			public void RemovedtSalesSummaryReportPOSRow(dtSalesSummaryReportPOSRow row)
			{
				base.Rows.Remove(row);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				dsSalesSummaryReportPOS dsSalesSummaryReportPOS2 = new dsSalesSummaryReportPOS();
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
				xmlSchemaAttribute.FixedValue = dsSalesSummaryReportPOS2.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "dtSalesSummaryReportPOSDataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = dsSalesSummaryReportPOS2.GetSchemaSerializable();
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

		public class dtSalesSummaryReportPOSRow : DataRow
		{
			private dtSalesSummaryReportPOSDataTable tabledtSalesSummaryReportPOS;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			public string Date
			{
				get
				{
					try
					{
						return (string)base[tabledtSalesSummaryReportPOS.DateColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Date' in table 'dtSalesSummaryReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSalesSummaryReportPOS.DateColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			public string Taxable
			{
				get
				{
					try
					{
						return (string)base[tabledtSalesSummaryReportPOS.TaxableColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Taxable' in table 'dtSalesSummaryReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSalesSummaryReportPOS.TaxableColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			public string Tax
			{
				get
				{
					try
					{
						return (string)base[tabledtSalesSummaryReportPOS.TaxColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Tax' in table 'dtSalesSummaryReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSalesSummaryReportPOS.TaxColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			public string Return
			{
				get
				{
					try
					{
						return (string)base[tabledtSalesSummaryReportPOS.ReturnColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Return' in table 'dtSalesSummaryReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSalesSummaryReportPOS.ReturnColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			public string Net
			{
				get
				{
					try
					{
						return (string)base[tabledtSalesSummaryReportPOS.NetColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Net' in table 'dtSalesSummaryReportPOS' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtSalesSummaryReportPOS.NetColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			internal dtSalesSummaryReportPOSRow(DataRowBuilder rb)
				: base(rb)
			{
				tabledtSalesSummaryReportPOS = (dtSalesSummaryReportPOSDataTable)base.Table;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsDateNull()
			{
				return IsNull(tabledtSalesSummaryReportPOS.DateColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			public void SetDateNull()
			{
				base[tabledtSalesSummaryReportPOS.DateColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsTaxableNull()
			{
				return IsNull(tabledtSalesSummaryReportPOS.TaxableColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			public void SetTaxableNull()
			{
				base[tabledtSalesSummaryReportPOS.TaxableColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsTaxNull()
			{
				return IsNull(tabledtSalesSummaryReportPOS.TaxColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			public void SetTaxNull()
			{
				base[tabledtSalesSummaryReportPOS.TaxColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsReturnNull()
			{
				return IsNull(tabledtSalesSummaryReportPOS.ReturnColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			public void SetReturnNull()
			{
				base[tabledtSalesSummaryReportPOS.ReturnColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsNetNull()
			{
				return IsNull(tabledtSalesSummaryReportPOS.NetColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			public void SetNetNull()
			{
				base[tabledtSalesSummaryReportPOS.NetColumn] = Convert.DBNull;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
		public class dtSalesSummaryReportPOSRowChangeEvent : EventArgs
		{
			private dtSalesSummaryReportPOSRow eventRow;

			private DataRowAction eventAction;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			public dtSalesSummaryReportPOSRow Row => eventRow;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			public DataRowAction Action => eventAction;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
			[DebuggerNonUserCode]
			public dtSalesSummaryReportPOSRowChangeEvent(dtSalesSummaryReportPOSRow row, DataRowAction action)
			{
				eventRow = row;
				eventAction = action;
			}
		}

		private dtSalesSummaryReportPOSDataTable tabledtSalesSummaryReportPOS;

		private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
		public dtSalesSummaryReportPOSDataTable dtSalesSummaryReportPOS => tabledtSalesSummaryReportPOS;

		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
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
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
		public new DataTableCollection Tables => base.Tables;

		[DebuggerNonUserCode]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
		public new DataRelationCollection Relations => base.Relations;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
		[DebuggerNonUserCode]
		public dsSalesSummaryReportPOS()
		{
			BeginInit();
			InitClass();
			CollectionChangeEventHandler value = SchemaChanged;
			base.Tables.CollectionChanged += value;
			base.Relations.CollectionChanged += value;
			EndInit();
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
		[DebuggerNonUserCode]
		protected dsSalesSummaryReportPOS(SerializationInfo info, StreamingContext context)
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
				if (dataSet.Tables["dtSalesSummaryReportPOS"] != null)
				{
					base.Tables.Add(new dtSalesSummaryReportPOSDataTable(dataSet.Tables["dtSalesSummaryReportPOS"]));
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

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
		[DebuggerNonUserCode]
		protected override void InitializeDerivedDataSet()
		{
			BeginInit();
			InitClass();
			EndInit();
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
		[DebuggerNonUserCode]
		public override System.Data.DataSet Clone()
		{
			dsSalesSummaryReportPOS dsSalesSummaryReportPOS2 = (dsSalesSummaryReportPOS)base.Clone();
			dsSalesSummaryReportPOS2.InitVars();
			dsSalesSummaryReportPOS2.SchemaSerializationMode = SchemaSerializationMode;
			return dsSalesSummaryReportPOS2;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
		[DebuggerNonUserCode]
		protected override bool ShouldSerializeTables()
		{
			return false;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
		[DebuggerNonUserCode]
		protected override bool ShouldSerializeRelations()
		{
			return false;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
		[DebuggerNonUserCode]
		protected override void ReadXmlSerializable(XmlReader reader)
		{
			if (DetermineSchemaSerializationMode(reader) == SchemaSerializationMode.IncludeSchema)
			{
				Reset();
				System.Data.DataSet dataSet = new System.Data.DataSet();
				dataSet.ReadXml(reader);
				if (dataSet.Tables["dtSalesSummaryReportPOS"] != null)
				{
					base.Tables.Add(new dtSalesSummaryReportPOSDataTable(dataSet.Tables["dtSalesSummaryReportPOS"]));
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

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
		[DebuggerNonUserCode]
		protected override XmlSchema GetSchemaSerializable()
		{
			MemoryStream memoryStream = new MemoryStream();
			WriteXmlSchema(new XmlTextWriter(memoryStream, null));
			memoryStream.Position = 0L;
			return XmlSchema.Read(new XmlTextReader(memoryStream), null);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
		[DebuggerNonUserCode]
		internal void InitVars()
		{
			InitVars(initTable: true);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
		[DebuggerNonUserCode]
		internal void InitVars(bool initTable)
		{
			tabledtSalesSummaryReportPOS = (dtSalesSummaryReportPOSDataTable)base.Tables["dtSalesSummaryReportPOS"];
			if (initTable && tabledtSalesSummaryReportPOS != null)
			{
				tabledtSalesSummaryReportPOS.InitVars();
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
		[DebuggerNonUserCode]
		private void InitClass()
		{
			base.DataSetName = "dsSalesSummaryReportPOS";
			base.Prefix = "";
			base.Namespace = "http://tempuri.org/dsSalesSummaryReportPOS.xsd";
			base.EnforceConstraints = true;
			SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
			tabledtSalesSummaryReportPOS = new dtSalesSummaryReportPOSDataTable();
			base.Tables.Add(tabledtSalesSummaryReportPOS);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
		[DebuggerNonUserCode]
		private bool ShouldSerializedtSalesSummaryReportPOS()
		{
			return false;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
		[DebuggerNonUserCode]
		private void SchemaChanged(object sender, CollectionChangeEventArgs e)
		{
			if (e.Action == CollectionChangeAction.Remove)
			{
				InitVars();
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
		[DebuggerNonUserCode]
		public static XmlSchemaComplexType GetTypedDataSetSchema(XmlSchemaSet xs)
		{
			dsSalesSummaryReportPOS dsSalesSummaryReportPOS2 = new dsSalesSummaryReportPOS();
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
			xmlSchemaAny.Namespace = dsSalesSummaryReportPOS2.Namespace;
			xmlSchemaSequence.Items.Add(xmlSchemaAny);
			xmlSchemaComplexType.Particle = xmlSchemaSequence;
			XmlSchema schemaSerializable = dsSalesSummaryReportPOS2.GetSchemaSerializable();
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
