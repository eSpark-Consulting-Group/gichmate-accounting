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
	[XmlSchemaProvider("GetTypedDataSetSchema")]
	[DesignerCategory("code")]
	[XmlRoot("dsProwisePurchasePO")]
	[ToolboxItem(true)]
	public class dsProwisePurchasePO : System.Data.DataSet
	{
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public delegate void dtProwisePurchasePORowChangeEventHandler(object sender, dtProwisePurchasePORowChangeEvent e);

		[Serializable]
		[XmlSchemaProvider("GetTypedTableSchema")]
		public class dtProwisePurchasePODataTable : TypedTableBase<dtProwisePurchasePORow>
		{
			private DataColumn columnPurDate;

			private DataColumn columnPurInvNo;

			private DataColumn columnRefInvNo;

			private DataColumn columnSupplier;

			private DataColumn columnProductName;

			private DataColumn columnProductCode;

			private DataColumn columnBarcode;

			private DataColumn columnProQty;

			private DataColumn columnUnit;

			private DataColumn columnProRate;

			private DataColumn columnGross;

			private DataColumn columnDiscAmnt;

			private DataColumn columnTaxable;

			private DataColumn columnGSTAMOUNT;

			private DataColumn columnNetAmnt;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn PurDateColumn => columnPurDate;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn PurInvNoColumn => columnPurInvNo;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn RefInvNoColumn => columnRefInvNo;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn SupplierColumn => columnSupplier;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn ProductNameColumn => columnProductName;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn ProductCodeColumn => columnProductCode;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn BarcodeColumn => columnBarcode;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn ProQtyColumn => columnProQty;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn UnitColumn => columnUnit;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn ProRateColumn => columnProRate;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
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

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public DataColumn NetAmntColumn => columnNetAmnt;

			[DebuggerNonUserCode]
			[Browsable(false)]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public int Count => base.Rows.Count;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public dtProwisePurchasePORow this[int index] => (dtProwisePurchasePORow)base.Rows[index];

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtProwisePurchasePORowChangeEventHandler dtProwisePurchasePORowChanging;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtProwisePurchasePORowChangeEventHandler dtProwisePurchasePORowChanged;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtProwisePurchasePORowChangeEventHandler dtProwisePurchasePORowDeleting;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public event dtProwisePurchasePORowChangeEventHandler dtProwisePurchasePORowDeleted;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtProwisePurchasePODataTable()
			{
				base.TableName = "dtProwisePurchasePO";
				BeginInit();
				InitClass();
				EndInit();
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			internal dtProwisePurchasePODataTable(DataTable table)
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
			protected dtProwisePurchasePODataTable(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
				InitVars();
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void AdddtProwisePurchasePORow(dtProwisePurchasePORow row)
			{
				base.Rows.Add(row);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtProwisePurchasePORow AdddtProwisePurchasePORow(string PurDate, string PurInvNo, string RefInvNo, string Supplier, string ProductName, string ProductCode, string Barcode, string ProQty, string Unit, string ProRate, string Gross, string DiscAmnt, string Taxable, string GSTAMOUNT, string NetAmnt)
			{
				dtProwisePurchasePORow dtProwisePurchasePORow = (dtProwisePurchasePORow)NewRow();
				object[] array2 = (dtProwisePurchasePORow.ItemArray = new object[15]
				{
					PurDate, PurInvNo, RefInvNo, Supplier, ProductName, ProductCode, Barcode, ProQty, Unit, ProRate,
					Gross, DiscAmnt, Taxable, GSTAMOUNT, NetAmnt
				});
				base.Rows.Add(dtProwisePurchasePORow);
				return dtProwisePurchasePORow;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public override DataTable Clone()
			{
				dtProwisePurchasePODataTable dtProwisePurchasePODataTable = (dtProwisePurchasePODataTable)base.Clone();
				dtProwisePurchasePODataTable.InitVars();
				return dtProwisePurchasePODataTable;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataTable CreateInstance()
			{
				return new dtProwisePurchasePODataTable();
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			internal void InitVars()
			{
				columnPurDate = base.Columns["PurDate"];
				columnPurInvNo = base.Columns["PurInvNo"];
				columnRefInvNo = base.Columns["RefInvNo"];
				columnSupplier = base.Columns["Supplier"];
				columnProductName = base.Columns["ProductName"];
				columnProductCode = base.Columns["ProductCode"];
				columnBarcode = base.Columns["Barcode"];
				columnProQty = base.Columns["ProQty"];
				columnUnit = base.Columns["Unit"];
				columnProRate = base.Columns["ProRate"];
				columnGross = base.Columns["Gross"];
				columnDiscAmnt = base.Columns["DiscAmnt"];
				columnTaxable = base.Columns["Taxable"];
				columnGSTAMOUNT = base.Columns["GSTAMOUNT"];
				columnNetAmnt = base.Columns["NetAmnt"];
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			private void InitClass()
			{
				columnPurDate = new DataColumn("PurDate", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnPurDate);
				columnPurInvNo = new DataColumn("PurInvNo", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnPurInvNo);
				columnRefInvNo = new DataColumn("RefInvNo", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnRefInvNo);
				columnSupplier = new DataColumn("Supplier", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnSupplier);
				columnProductName = new DataColumn("ProductName", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnProductName);
				columnProductCode = new DataColumn("ProductCode", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnProductCode);
				columnBarcode = new DataColumn("Barcode", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnBarcode);
				columnProQty = new DataColumn("ProQty", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnProQty);
				columnUnit = new DataColumn("Unit", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnUnit);
				columnProRate = new DataColumn("ProRate", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnProRate);
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
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtProwisePurchasePORow NewdtProwisePurchasePORow()
			{
				return (dtProwisePurchasePORow)NewRow();
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new dtProwisePurchasePORow(builder);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override Type GetRowType()
			{
				return typeof(dtProwisePurchasePORow);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this.dtProwisePurchasePORowChanged != null)
				{
					this.dtProwisePurchasePORowChanged(this, new dtProwisePurchasePORowChangeEvent((dtProwisePurchasePORow)e.Row, e.Action));
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this.dtProwisePurchasePORowChanging != null)
				{
					this.dtProwisePurchasePORowChanging(this, new dtProwisePurchasePORowChangeEvent((dtProwisePurchasePORow)e.Row, e.Action));
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.dtProwisePurchasePORowDeleted != null)
				{
					this.dtProwisePurchasePORowDeleted(this, new dtProwisePurchasePORowChangeEvent((dtProwisePurchasePORow)e.Row, e.Action));
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this.dtProwisePurchasePORowDeleting != null)
				{
					this.dtProwisePurchasePORowDeleting(this, new dtProwisePurchasePORowChangeEvent((dtProwisePurchasePORow)e.Row, e.Action));
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void RemovedtProwisePurchasePORow(dtProwisePurchasePORow row)
			{
				base.Rows.Remove(row);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				dsProwisePurchasePO dsProwisePurchasePO2 = new dsProwisePurchasePO();
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
				xmlSchemaAttribute.FixedValue = dsProwisePurchasePO2.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "dtProwisePurchasePODataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = dsProwisePurchasePO2.GetSchemaSerializable();
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

		public class dtProwisePurchasePORow : DataRow
		{
			private dtProwisePurchasePODataTable tabledtProwisePurchasePO;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string PurDate
			{
				get
				{
					try
					{
						return (string)base[tabledtProwisePurchasePO.PurDateColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'PurDate' in table 'dtProwisePurchasePO' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtProwisePurchasePO.PurDateColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string PurInvNo
			{
				get
				{
					try
					{
						return (string)base[tabledtProwisePurchasePO.PurInvNoColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'PurInvNo' in table 'dtProwisePurchasePO' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtProwisePurchasePO.PurInvNoColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string RefInvNo
			{
				get
				{
					try
					{
						return (string)base[tabledtProwisePurchasePO.RefInvNoColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'RefInvNo' in table 'dtProwisePurchasePO' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtProwisePurchasePO.RefInvNoColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string Supplier
			{
				get
				{
					try
					{
						return (string)base[tabledtProwisePurchasePO.SupplierColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Supplier' in table 'dtProwisePurchasePO' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtProwisePurchasePO.SupplierColumn] = value;
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
						return (string)base[tabledtProwisePurchasePO.ProductNameColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'ProductName' in table 'dtProwisePurchasePO' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtProwisePurchasePO.ProductNameColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string ProductCode
			{
				get
				{
					try
					{
						return (string)base[tabledtProwisePurchasePO.ProductCodeColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'ProductCode' in table 'dtProwisePurchasePO' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtProwisePurchasePO.ProductCodeColumn] = value;
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
						return (string)base[tabledtProwisePurchasePO.BarcodeColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Barcode' in table 'dtProwisePurchasePO' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtProwisePurchasePO.BarcodeColumn] = value;
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
						return (string)base[tabledtProwisePurchasePO.ProQtyColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'ProQty' in table 'dtProwisePurchasePO' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtProwisePurchasePO.ProQtyColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string Unit
			{
				get
				{
					try
					{
						return (string)base[tabledtProwisePurchasePO.UnitColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Unit' in table 'dtProwisePurchasePO' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtProwisePurchasePO.UnitColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string ProRate
			{
				get
				{
					try
					{
						return (string)base[tabledtProwisePurchasePO.ProRateColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'ProRate' in table 'dtProwisePurchasePO' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtProwisePurchasePO.ProRateColumn] = value;
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
						return (string)base[tabledtProwisePurchasePO.GrossColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Gross' in table 'dtProwisePurchasePO' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtProwisePurchasePO.GrossColumn] = value;
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
						return (string)base[tabledtProwisePurchasePO.DiscAmntColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'DiscAmnt' in table 'dtProwisePurchasePO' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtProwisePurchasePO.DiscAmntColumn] = value;
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
						return (string)base[tabledtProwisePurchasePO.TaxableColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'Taxable' in table 'dtProwisePurchasePO' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtProwisePurchasePO.TaxableColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public string GSTAMOUNT
			{
				get
				{
					try
					{
						return (string)base[tabledtProwisePurchasePO.GSTAMOUNTColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'GSTAMOUNT' in table 'dtProwisePurchasePO' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtProwisePurchasePO.GSTAMOUNTColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public string NetAmnt
			{
				get
				{
					try
					{
						return (string)base[tabledtProwisePurchasePO.NetAmntColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("The value for column 'NetAmnt' in table 'dtProwisePurchasePO' is DBNull.", innerException);
					}
				}
				set
				{
					base[tabledtProwisePurchasePO.NetAmntColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			internal dtProwisePurchasePORow(DataRowBuilder rb)
				: base(rb)
			{
				tabledtProwisePurchasePO = (dtProwisePurchasePODataTable)base.Table;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public bool IsPurDateNull()
			{
				return IsNull(tabledtProwisePurchasePO.PurDateColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetPurDateNull()
			{
				base[tabledtProwisePurchasePO.PurDateColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsPurInvNoNull()
			{
				return IsNull(tabledtProwisePurchasePO.PurInvNoColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetPurInvNoNull()
			{
				base[tabledtProwisePurchasePO.PurInvNoColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsRefInvNoNull()
			{
				return IsNull(tabledtProwisePurchasePO.RefInvNoColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetRefInvNoNull()
			{
				base[tabledtProwisePurchasePO.RefInvNoColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsSupplierNull()
			{
				return IsNull(tabledtProwisePurchasePO.SupplierColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetSupplierNull()
			{
				base[tabledtProwisePurchasePO.SupplierColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsProductNameNull()
			{
				return IsNull(tabledtProwisePurchasePO.ProductNameColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetProductNameNull()
			{
				base[tabledtProwisePurchasePO.ProductNameColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsProductCodeNull()
			{
				return IsNull(tabledtProwisePurchasePO.ProductCodeColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetProductCodeNull()
			{
				base[tabledtProwisePurchasePO.ProductCodeColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsBarcodeNull()
			{
				return IsNull(tabledtProwisePurchasePO.BarcodeColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetBarcodeNull()
			{
				base[tabledtProwisePurchasePO.BarcodeColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsProQtyNull()
			{
				return IsNull(tabledtProwisePurchasePO.ProQtyColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetProQtyNull()
			{
				base[tabledtProwisePurchasePO.ProQtyColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsUnitNull()
			{
				return IsNull(tabledtProwisePurchasePO.UnitColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetUnitNull()
			{
				base[tabledtProwisePurchasePO.UnitColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsProRateNull()
			{
				return IsNull(tabledtProwisePurchasePO.ProRateColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetProRateNull()
			{
				base[tabledtProwisePurchasePO.ProRateColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsGrossNull()
			{
				return IsNull(tabledtProwisePurchasePO.GrossColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetGrossNull()
			{
				base[tabledtProwisePurchasePO.GrossColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsDiscAmntNull()
			{
				return IsNull(tabledtProwisePurchasePO.DiscAmntColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetDiscAmntNull()
			{
				base[tabledtProwisePurchasePO.DiscAmntColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsTaxableNull()
			{
				return IsNull(tabledtProwisePurchasePO.TaxableColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetTaxableNull()
			{
				base[tabledtProwisePurchasePO.TaxableColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsGSTAMOUNTNull()
			{
				return IsNull(tabledtProwisePurchasePO.GSTAMOUNTColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public void SetGSTAMOUNTNull()
			{
				base[tabledtProwisePurchasePO.GSTAMOUNTColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsNetAmntNull()
			{
				return IsNull(tabledtProwisePurchasePO.NetAmntColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public void SetNetAmntNull()
			{
				base[tabledtProwisePurchasePO.NetAmntColumn] = Convert.DBNull;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		public class dtProwisePurchasePORowChangeEvent : EventArgs
		{
			private dtProwisePurchasePORow eventRow;

			private DataRowAction eventAction;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			public dtProwisePurchasePORow Row => eventRow;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public DataRowAction Action => eventAction;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
			[DebuggerNonUserCode]
			public dtProwisePurchasePORowChangeEvent(dtProwisePurchasePORow row, DataRowAction action)
			{
				eventRow = row;
				eventAction = action;
			}
		}

		private dtProwisePurchasePODataTable tabledtProwisePurchasePO;

		private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		public dtProwisePurchasePODataTable dtProwisePurchasePO => tabledtProwisePurchasePO;

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

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DebuggerNonUserCode]
		public new DataRelationCollection Relations => base.Relations;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		public dsProwisePurchasePO()
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
		protected dsProwisePurchasePO(SerializationInfo info, StreamingContext context)
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
				if (dataSet.Tables["dtProwisePurchasePO"] != null)
				{
					base.Tables.Add(new dtProwisePurchasePODataTable(dataSet.Tables["dtProwisePurchasePO"]));
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

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		public override System.Data.DataSet Clone()
		{
			dsProwisePurchasePO dsProwisePurchasePO2 = (dsProwisePurchasePO)base.Clone();
			dsProwisePurchasePO2.InitVars();
			dsProwisePurchasePO2.SchemaSerializationMode = SchemaSerializationMode;
			return dsProwisePurchasePO2;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
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
				if (dataSet.Tables["dtProwisePurchasePO"] != null)
				{
					base.Tables.Add(new dtProwisePurchasePODataTable(dataSet.Tables["dtProwisePurchasePO"]));
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
			tabledtProwisePurchasePO = (dtProwisePurchasePODataTable)base.Tables["dtProwisePurchasePO"];
			if (initTable && tabledtProwisePurchasePO != null)
			{
				tabledtProwisePurchasePO.InitVars();
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		private void InitClass()
		{
			base.DataSetName = "dsProwisePurchasePO";
			base.Prefix = "";
			base.Namespace = "http://tempuri.org/dsProwisePurchasePO.xsd";
			base.EnforceConstraints = true;
			SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
			tabledtProwisePurchasePO = new dtProwisePurchasePODataTable();
			base.Tables.Add(tabledtProwisePurchasePO);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
		[DebuggerNonUserCode]
		private bool ShouldSerializedtProwisePurchasePO()
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
			dsProwisePurchasePO dsProwisePurchasePO2 = new dsProwisePurchasePO();
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
			xmlSchemaAny.Namespace = dsProwisePurchasePO2.Namespace;
			xmlSchemaSequence.Items.Add(xmlSchemaAny);
			xmlSchemaComplexType.Particle = xmlSchemaSequence;
			XmlSchema schemaSerializable = dsProwisePurchasePO2.GetSchemaSerializable();
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
