using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AccountsLibrary.Properties;
using AccountsPro.SubProjects.AccountsLibraryFramework;

namespace AccountsLibrary
{
	public static class AshproORM
	{
		public static T GetObjectDetails<T>(string Query, string Connection)
		{
			try
			{
				_ = typeof(T);
				T result = Activator.CreateInstance<T>();
				DataTable dataTable = new DataTable();
				dataTable = GetDataTable(Query, Connection);
				if (dataTable.Rows.Count > 0)
				{
					result = GetItem<T>(dataTable.Rows[0]);
				}
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable GetDataTable(string Query, string sConnection)
		{
			CultureInfo cultureInfo3 = (Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo("en")));
			try
			{
				DataTable dataTable = new DataTable();
				using (SqlConnection selectConnection = new SqlConnection(sConnection))
				{
					using SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(Query, selectConnection);
					sqlDataAdapter.Fill(dataTable);
				}
				return dataTable;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static async Task<DataTable> GetDataTableAsync(string Query, string sConnection)
		{
			return await Task.Run(delegate
			{
				try
				{
					DataTable dataTable = new DataTable();
					using (SqlConnection selectConnection = new SqlConnection(sConnection))
					{
						using SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(Query, selectConnection);
						sqlDataAdapter.Fill(dataTable);
					}
					return dataTable;
				}
				catch (Exception ex)
				{
					throw ex;
				}
			});
		}

		public static dynamic ValueFindMethod(string Query, string Connection)
		{
			try
			{
				DataTable dataTable = new DataTable();
				dataTable = GetDataTable(Query, Connection);
				if (dataTable.Rows.Count > 0)
				{
					return (dataTable.Rows[0][0] == DBNull.Value) ? null : dataTable.Rows[0][0];
				}
				return null;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static List<T> GetListMethod<T>(string Query, string Connection)
		{
			try
			{
				List<T> list = new List<T>();
				DataTable dataTable = new DataTable();
				dataTable = GetDataTable(Query, Connection);
				return ConvertDataTable<T>(dataTable);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static List<string> GetStringListMethod(string Query, string Connection)
		{
			try
			{
				List<string> list = new List<string>();
				DataTable dataTable = new DataTable();
				dataTable = GetDataTable(Query, Connection);
				foreach (DataRow row in dataTable.Rows)
				{
					list.Add(row[0].ToString());
				}
				return list;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool DatabaseMethod(string Query, string Connection)
		{
			if (Query == string.Empty)
			{
				return false;
			}
			try
			{
				using SqlConnection sqlConnection = new SqlConnection(Connection);
				using SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);
				sqlConnection.Open();
				sqlCommand.ExecuteNonQuery();
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		public static bool InsertToDatabase(List<object> datas, string table, string Connection)
		{
			try
			{
				foreach (object data in datas)
				{
					InsertToDatabaseObj(data, table, Connection);
				}
				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool InsertToDatabaseObj(object data, string table, string Connection)
		{
			try
			{
				List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
				using (SqlConnection sqlConnection = new SqlConnection(Connection))
				{
					PropertyInfo[] properties = data.GetType().GetProperties();
					foreach (PropertyInfo propertyInfo in properties)
					{
						if (propertyInfo.GetValue(data, null) != null && (!(propertyInfo.PropertyType.Name == "Nullable`1") || !(propertyInfo.GetValue(data, null).ToString() == "0")))
						{
							list.Add(new KeyValuePair<string, string>(propertyInfo.Name, "@" + propertyInfo.Name));
						}
					}
					string insertCommand = getInsertCommand(table, list);
					using SqlCommand sqlCommand = new SqlCommand(insertCommand, sqlConnection);
					sqlCommand.Parameters.Clear();
					PropertyInfo[] properties2 = data.GetType().GetProperties();
					foreach (PropertyInfo propertyInfo2 in properties2)
					{
						if (propertyInfo2.GetValue(data, null) != null && (!(propertyInfo2.PropertyType.Name == "Nullable`1") || !(propertyInfo2.GetValue(data, null).ToString() == "0")))
						{
							if (propertyInfo2.PropertyType.Name == "Byte[]")
							{
								sqlCommand.Parameters.AddWithValue("@" + propertyInfo2.Name, (byte[])propertyInfo2.GetValue(data, null));
							}
							else if (propertyInfo2.PropertyType.Name == "DateTime")
							{
								sqlCommand.Parameters.AddWithValue("@" + propertyInfo2.Name, AniHelper.GetDate((DateTime)propertyInfo2.GetValue(data, null)));
							}
							else
							{
								sqlCommand.Parameters.AddWithValue("@" + propertyInfo2.Name, propertyInfo2.GetValue(data, null).ToString());
							}
						}
					}
					sqlConnection.Open();
					sqlCommand.ExecuteNonQuery();
				}
				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool UpdateToDatabase(List<object> datas, string table, string column, string Connection)
		{
			try
			{
				int iValue = -1;
				foreach (object data in datas)
				{
					PropertyInfo[] properties = data.GetType().GetProperties();
					foreach (PropertyInfo propertyInfo in properties)
					{
						if (propertyInfo.Name == column)
						{
							iValue = propertyInfo.GetValue(data, null).ToInt32();
							break;
						}
					}
					UpdateToDatabaseObj(data, table, column, iValue, Connection);
				}
				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool UpdateToDatabaseObjDirect(string table, string column, int iValue,string whereClause)
		{
			try
			{
				//string Connection = Settings.Default.strConnetionString;

				//string updateCommand = getUpdateCommand(table, list, column, "@" + column);
				
				//sqlCommand.Parameters.Clear();
				//sqlCommand.Parameters.AddWithValue("@" + column, iValue);


				using (SqlConnection sqlConnection = new SqlConnection(Settings.Default.strConnetionString))
				{
					sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Update " + table + " set " + column + "=" + iValue+" "+whereClause,sqlConnection);
                    sqlCommand.ExecuteNonQuery();
				}
			}
			catch { }
            return true;
		}


            public static bool UpdateToDatabaseObj(object data, string table, string column, int iValue, string Connection)
		{
			try
			{
				List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
                if (Connection == null)
                {
                    Connection = Settings.Default.strConnetionString;
                }
                using (SqlConnection sqlConnection = new SqlConnection(Connection))
				{
					PropertyInfo[] properties = data.GetType().GetProperties();
					foreach (PropertyInfo propertyInfo in properties)
					{
						if (propertyInfo.GetValue(data, null) != null && propertyInfo.Name != column && (!(propertyInfo.PropertyType.Name == "Nullable`1") || !(propertyInfo.GetValue(data, null).ToString() == "0")))
						{
							list.Add(new KeyValuePair<string, string>(propertyInfo.Name, "@" + propertyInfo.Name));
						}
					}
					string updateCommand = getUpdateCommand(table, list, column, "@" + column);
					using SqlCommand sqlCommand = new SqlCommand(updateCommand, sqlConnection);
					sqlCommand.Parameters.Clear();
					sqlCommand.Parameters.AddWithValue("@" + column, iValue);
					PropertyInfo[] properties2 = data.GetType().GetProperties();
					foreach (PropertyInfo propertyInfo2 in properties2)
					{
						if (propertyInfo2.GetValue(data, null) != null && propertyInfo2.Name != column && (!(propertyInfo2.PropertyType.Name == "Nullable`1") || !(propertyInfo2.GetValue(data, null).ToString() == "0")))
						{
							if (propertyInfo2.PropertyType.Name == "Byte[]")
							{
								sqlCommand.Parameters.AddWithValue("@" + propertyInfo2.Name, (byte[])propertyInfo2.GetValue(data, null));
							}
							else if (propertyInfo2.PropertyType.Name == "DateTime")
							{
								sqlCommand.Parameters.AddWithValue("@" + propertyInfo2.Name, AniHelper.GetDate((DateTime)propertyInfo2.GetValue(data, null)));
							}
							else
							{
								sqlCommand.Parameters.AddWithValue("@" + propertyInfo2.Name, propertyInfo2.GetValue(data, null).ToString());
							}
						}
					}
					sqlConnection.Open();
					sqlCommand.ExecuteNonQuery();
				}
				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool DeleteFromDatabase(string table, string column, int iValue, string Connection)
		{
			string cmdText = "Delete From  " + table + " Where " + column + " = @" + column;
			try
			{
				using (SqlConnection sqlConnection = new SqlConnection(Connection))
				{
					using SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection);
					sqlCommand.Parameters.Clear();
					sqlCommand.Parameters.AddWithValue("@" + column, iValue);
					sqlConnection.Open();
					sqlCommand.ExecuteNonQuery();
				}
				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool DeleteMethod(string Query, string Connection)
		{
			try
			{
				using (SqlConnection sqlConnection = new SqlConnection(Connection))
				{
					using SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);
					sqlConnection.Open();
					sqlCommand.ExecuteNonQuery();
				}
				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool UpdateDatabase(DataTable datas, DataTable oldDatas, string table, string sConnection)
		{
			try
			{
				DeleteOldItem(datas, oldDatas, table, sConnection);
				bool result = false;
				string sValue = string.Empty;
				List<KeyValuePair<object, object>> list = new List<KeyValuePair<object, object>>();
				SqlConnection sqlConnection = new SqlConnection(sConnection);
				sqlConnection.Open();
				try
				{
					foreach (DataRow row in datas.Rows)
					{
						string text = string.Empty;
						string empty = string.Empty;
						List<Common> source = new List<Common>();
						list.Clear();
						foreach (DataColumn column in row.Table.Columns)
						{
							if (column.ColumnName == row.Table.Columns[0].ColumnName)
							{
								text = column.ColumnName;
								sValue = row[column.ColumnName].ToString();
							}
							else
							{
								list.Add(new KeyValuePair<object, object>(column.ColumnName, row[column.ColumnName].ToString()));
							}
							source = GetCommon(table, text, sConnection);
						}
						if (sValue != null && sValue != string.Empty)
						{
							empty = ((!source.Any((Common x) => x.id == Convert.ToInt32(sValue))) ? getInsertCommand(table, list) : getUpdateCommand(table, list, text, sValue));
							using SqlCommand sqlCommand = new SqlCommand(empty, sqlConnection);
							sqlCommand.ExecuteNonQuery();
						}
					}
					result = true;
				}
				catch (Exception ex)
				{
					throw ex;
				}
				finally
				{
					sqlConnection.Close();
				}
				return result;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static bool UpdateDatabase(List<object> newDatas, List<object> oldDatas, string sTable, string sColumn, string sConnection)
		{
			List<Common> list = new List<Common>();
			List<Common> list2 = new List<Common>();
			list = GetIdList(newDatas, sColumn);
			list2 = GetIdList(oldDatas, sColumn);
			try
			{
				foreach (Common item2 in list2)
				{
					if (!list.Any((Common x) => x.id == item2.id))
					{
						DeleteFromDatabase(sTable, sColumn, item2.id, sConnection);
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			foreach (Common item in list)
			{
				if (list2.Any((Common x) => x.id == item.id))
				{
					int? num = item.id;
					try
					{
						foreach (object newData in newDatas)
						{
							PropertyInfo[] properties = newData.GetType().GetProperties();
							foreach (PropertyInfo propertyInfo in properties)
							{
								if (propertyInfo.Name == sColumn && new int?(propertyInfo.GetValue(newData, null).ToInt32()) == num)
								{
									UpdateToDatabaseObj(newData, sTable, sColumn, num.ToInt32(), sConnection);
									break;
								}
							}
						}
					}
					catch (Exception ex2)
					{
						throw ex2;
					}
					continue;
				}
				int? num2 = item.id;
				try
				{
					foreach (object newData2 in newDatas)
					{
						PropertyInfo[] properties2 = newData2.GetType().GetProperties();
						foreach (PropertyInfo propertyInfo2 in properties2)
						{
							if (propertyInfo2.Name == sColumn && new int?(propertyInfo2.GetValue(newData2, null).ToInt32()) == num2)
							{
								InsertToDatabaseObj(newData2, sTable, sConnection);
								break;
							}
						}
					}
				}
				catch (Exception)
				{
					throw;
				}
				break;
			}
			return true;
		}

		private static void DeleteOldItem(DataTable newDt, DataTable oldDt, string sTable, string sConnection)
		{
			try
			{
				string column = string.Empty;
				foreach (DataRow row in newDt.Rows)
				{
					column = row.Table.Columns[0].ColumnName;
				}
				List<Common> list = new List<Common>();
				List<Common> list2 = new List<Common>();
				list = GetIdList(newDt);
				list2 = GetIdList(oldDt);
				foreach (Common item in list2)
				{
					if (!list.Any((Common x) => x.id == item.id))
					{
						DeleteFromDatabase(sTable, column, item.id, sConnection);
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		private static List<Common> GetCommon(string sTable, string sColumn, string sConnection)
		{
			try
			{
				List<Common> list = new List<Common>();
				DataTable dataTable = new DataTable();
				dataTable = GetDataTable("Select " + sColumn + " From " + sTable, sConnection);
				foreach (DataRow row in dataTable.Rows)
				{
					Common common = new Common();
					common.id = Convert.ToInt32(row[0].ToString());
					list.Add(common);
				}
				return list;
			}
			catch (Exception)
			{
				throw;
			}
		}

		private static List<Common> GetIdList(List<object> data, string sColumn)
		{
			try
			{
				List<Common> list = new List<Common>();
				foreach (object datum in data)
				{
					PropertyInfo[] properties = datum.GetType().GetProperties();
					foreach (PropertyInfo propertyInfo in properties)
					{
						Common common = new Common();
						if (propertyInfo.Name == sColumn)
						{
							common.id = propertyInfo.GetValue(datum, null).ToInt32();
							list.Add(common);
							break;
						}
					}
				}
				return list;
			}
			catch (Exception)
			{
				throw;
			}
		}

		private static List<Common> GetIdList(DataTable sTable)
		{
			try
			{
				List<Common> list = new List<Common>();
				foreach (DataRow row in sTable.Rows)
				{
					Common common = new Common();
					common.id = Convert.ToInt32(row[0].ToString());
					list.Add(common);
				}
				return list;
			}
			catch (Exception)
			{
				throw;
			}
		}

		private static List<T> ConvertDataTable<T>(DataTable dt)
		{
			try
			{
				List<T> list = new List<T>();
				foreach (DataRow row in dt.Rows)
				{
					T item = GetItem<T>(row);
					list.Add(item);
				}
				return list;
			}
			catch (Exception)
			{
				throw;
			}
		}

		private static T GetItem<T>(DataRow dr)
		{
			try
			{
				Type typeFromHandle = typeof(T);
				T val = Activator.CreateInstance<T>();
				foreach (DataColumn column in dr.Table.Columns)
				{
					PropertyInfo[] properties = typeFromHandle.GetProperties();
					foreach (PropertyInfo propertyInfo in properties)
					{
						if (!(propertyInfo.Name == column.ColumnName))
						{
							continue;
						}
						if (dr[column.ColumnName] != DBNull.Value)
						{
							if (propertyInfo.PropertyType.Name == "Boolean")
							{
								if (dr[column.ColumnName].ToString() != string.Empty)
								{
									propertyInfo.SetValue(val, dr[column.ColumnName].ToString().ToBool(), null);
								}
							}
							else if (propertyInfo.PropertyType.Name == "Int32")
							{
								if (dr[column.ColumnName].ToString() != string.Empty)
								{
									propertyInfo.SetValue(val, dr[column.ColumnName].ToInt32(), null);
								}
							}
							else if (propertyInfo.PropertyType.Name == "Decimal")
							{
								if (dr[column.ColumnName].ToString() != string.Empty)
								{
									propertyInfo.SetValue(val, dr[column.ColumnName].ToString().ToDecimal(), null);
								}
							}
							else if (propertyInfo.PropertyType.Name == "Nullable`1")
							{
								if (propertyInfo.PropertyType.FullName.Contains("System.Int32"))
								{
									if (dr[column.ColumnName].ToString() != string.Empty)
									{
										propertyInfo.SetValue(val, dr[column.ColumnName].ToInt32(), null);
									}
								}
								else if (propertyInfo.PropertyType.FullName.Contains("System.Boolean"))
								{
									if (dr[column.ColumnName].ToString() != string.Empty)
									{
										propertyInfo.SetValue(val, dr[column.ColumnName].ToString().ToBool(), null);
									}
								}
								else if (propertyInfo.PropertyType.FullName.Contains("System.DateTime"))
								{
									if (dr[column.ColumnName].ToString() != string.Empty)
									{
										propertyInfo.SetValue(val, Convert.ToDateTime(dr[column.ColumnName].ToString()), null);
									}
								}
								else if (propertyInfo.PropertyType.FullName.Contains("System.Decimal") && dr[column.ColumnName].ToString() != string.Empty)
								{
									propertyInfo.SetValue(val, dr[column.ColumnName].ToString().ToDecimal(), null);
								}
							}
							else if (propertyInfo.PropertyType.Name == "DateTime")
							{
								if (dr[column.ColumnName].ToString() != string.Empty)
								{
									propertyInfo.SetValue(val, Convert.ToDateTime(dr[column.ColumnName].ToString()), null);
								}
							}
							else if (propertyInfo.PropertyType.Name == "Byte[]")
							{
								propertyInfo.SetValue(val, (byte[])dr[column.ColumnName], null);
							}
							else
							{
								propertyInfo.SetValue(val, dr[column.ColumnName].ToString(), null);
							}
						}
						else
						{
							propertyInfo.SetValue(val, null, null);
						}
					}
				}
				return val;
			}
			catch (Exception ex)
			{
				_ = ex.Message;
				throw;
			}
		}

		private static string getInsertCommand(string table, List<KeyValuePair<dynamic, dynamic>> values)
		{
			string text = null;
			text = text + "INSERT INTO " + table + " ( ";
			foreach (KeyValuePair<object, object> value in values)
			{
				text = (string)(text + (dynamic)value.Key);
				text += ", ";
			}
			text = text.Remove(text.Length - 2, 2);
			text += ") VALUES ( ";
			foreach (KeyValuePair<object, object> value2 in values)
			{
				if (((dynamic)value2.Key).GetType().Name == "System.Int")
				{
					text = (string)(text + (dynamic)value2.Value);
				}
				else
				{
					text += "'";
					text = (string)(text + (dynamic)value2.Value);
					text += "'";
				}
				text += ", ";
			}
			text = text.Remove(text.Length - 2, 2);
			return text + ")";
		}

		public static string getUpdateCommand(string table, List<KeyValuePair<dynamic, dynamic>> values, string column, string sValue)
		{
			string text = null;
			text = text + "Update  " + table + " Set ";
			foreach (KeyValuePair<object, object> value in values)
			{
				text = (string)(text + (dynamic)value.Key);
				text += "=";
				if (((dynamic)value.Key).GetType().Name == "System.Int")
				{
					text = (string)(text + (dynamic)value.Value);
				}
				else
				{
					text += "'";
					text = (string)(text + (dynamic)value.Value);
					text += "'";
				}
				text += ", ";
			}
			text = text.Remove(text.Length - 2, 2);
			return text + " Where " + column + " = '" + sValue + "'";
		}

		private static bool isValidDataType(string dataType)
		{
			bool result = false;
			switch (dataType)
			{
			case "System.Nullable`1[System.Int64]":
				result = true;
				break;
			case "System.Double":
				result = true;
				break;
			case "System.Boolean":
				result = true;
				break;
			case "System.Int16":
				result = true;
				break;
			case "System.Nullable`1[System.Boolean]":
				result = true;
				break;
			case "System.Int64":
				result = true;
				break;
			case "System.Decimal":
				result = true;
				break;
			case "System.Nullable`1[System.DateTime]":
				result = true;
				break;
			case "System.Nullable`1[System.Int16]":
				result = true;
				break;
			case "System.Nullable`1[System.Decimal]":
				result = true;
				break;
			case "System.Int32":
				result = true;
				break;
			case "System.Nullable`1[System.Int32]":
				result = true;
				break;
			case "System.String":
				result = true;
				break;
			case "System.Nullable`1[System.Double]":
				result = true;
				break;
			}
			return result;
		}

		private static string getUpdateCommand(string table, List<KeyValuePair<string, string>> values, string column, dynamic sValue)
		{
			string text = null;
			text = text + "Update  " + table + " Set ";
			foreach (KeyValuePair<string, string> value in values)
			{
				text += value.Key;
				text += "=";
				text += value.Value;
				text += ", ";
			}
			text = text.Remove(text.Length - 2, 2);
			return (string)(text + (" Where " + column + " = " + sValue));
		}

		private static string getInsertCommand(string table, List<KeyValuePair<string, string>> values)
		{
			string text = null;
			text = text + "INSERT INTO " + table + " ( ";
			foreach (KeyValuePair<string, string> value in values)
			{
				text += value.Key;
				text += ", ";
			}
			text = text.Remove(text.Length - 2, 2);
			text += ") VALUES ( ";
			foreach (KeyValuePair<string, string> value2 in values)
			{
				text += value2.Value;
				text += ", ";
			}
			text = text.Remove(text.Length - 2, 2);
			return text + ")";
		}

		public static bool InsertMethod_SP(List<object> entities, string sStoredProceedure, string Connection)
		{
			bool flag = false;
			try
			{
				foreach (object entity in entities)
				{
					InsertMethod_SP(entity, sStoredProceedure, Connection);
				}
				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool InsertMethod_SP(object entity, string sStoredProceedure, string Connection)
		{
			bool flag = false;
			using SqlConnection sqlConnection = new SqlConnection(Connection);
			using SqlCommand sqlCommand = new SqlCommand(sStoredProceedure, sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			try
			{
				PropertyInfo[] properties = entity.GetType().GetProperties();
				foreach (PropertyInfo propertyInfo in properties)
				{
					if (propertyInfo.GetValue(entity, null) != null && (!(propertyInfo.PropertyType.Name == "Nullable`1") || !(propertyInfo.GetValue(entity, null).ToString() == "0")))
					{
						if (propertyInfo.PropertyType.Name == "Byte[]")
						{
							sqlCommand.Parameters.AddWithValue(propertyInfo.Name, (byte[])propertyInfo.GetValue(entity, null));
						}
						else if (propertyInfo.PropertyType.Name == "DateTime")
						{
							sqlCommand.Parameters.AddWithValue("@" + propertyInfo.Name, AniHelper.GetDate((DateTime)propertyInfo.GetValue(entity, null)));
						}
						else
						{
							sqlCommand.Parameters.AddWithValue(propertyInfo.Name, propertyInfo.GetValue(entity, null).ToString());
						}
					}
				}
				sqlConnection.Open();
				int num = sqlCommand.ExecuteNonQuery();
				if (num > 0)
				{
					return true;
				}
				return false;
			}
			catch (Exception ex)
			{
				throw ex;
			}
            return false;
        }

		public static int InsertMethod_SP(object entity, DataSet ds, string sStoredProceedure, string Connection, object Secondentity = null)
		{
			int? obj = null;
			using (SqlConnection sqlConnection = new SqlConnection(Connection))
			{
				using SqlCommand sqlCommand = new SqlCommand(sStoredProceedure, sqlConnection);
				sqlCommand.CommandType = CommandType.StoredProcedure;
				try
				{
					if (ds != null)
					{
						foreach (DataTable table in ds.Tables)
						{
							sqlCommand.Parameters.AddWithValue("@" + table.TableName, table);
						}
					}
					if (entity != null)
					{
						PropertyInfo[] properties = entity.GetType().GetProperties();
						foreach (PropertyInfo propertyInfo in properties)
						{
							if (propertyInfo.GetValue(entity, null) != null && (!(propertyInfo.PropertyType.Name == "Nullable`1") || !(propertyInfo.GetValue(entity, null).ToString() == "0")))
							{
								if (propertyInfo.PropertyType.Name == "Byte[]")
								{
									sqlCommand.Parameters.AddWithValue(propertyInfo.Name, (byte[])propertyInfo.GetValue(entity, null));
								}
								else if (propertyInfo.PropertyType.Name == "DateTime")
								{
									sqlCommand.Parameters.AddWithValue("@" + propertyInfo.Name, AniHelper.GetDate((DateTime)propertyInfo.GetValue(entity, null)));
								}
								else
								{
									sqlCommand.Parameters.AddWithValue(propertyInfo.Name, propertyInfo.GetValue(entity, null).ToString());
								}
							}
						}
					}
					if (Secondentity != null)
					{
						PropertyInfo[] properties2 = Secondentity.GetType().GetProperties();
						foreach (PropertyInfo propertyInfo2 in properties2)
						{
							if (propertyInfo2.GetValue(Secondentity, null) != null && (!(propertyInfo2.PropertyType.Name == "Nullable`1") || !(propertyInfo2.GetValue(Secondentity, null).ToString() == "0")))
							{
								if (propertyInfo2.PropertyType.Name == "Byte[]")
								{
									sqlCommand.Parameters.AddWithValue(propertyInfo2.Name, (byte[])propertyInfo2.GetValue(Secondentity, null));
								}
								else if (propertyInfo2.PropertyType.Name == "DateTime")
								{
									sqlCommand.Parameters.AddWithValue("@" + propertyInfo2.Name, AniHelper.GetDate((DateTime)propertyInfo2.GetValue(Secondentity, null)));
								}
								else
								{
									sqlCommand.Parameters.AddWithValue(propertyInfo2.Name, propertyInfo2.GetValue(Secondentity, null).ToString());
								}
							}
						}
					}
					sqlCommand.Parameters.Add("@return", SqlDbType.Int);
					sqlCommand.Parameters.Add("@errMessage", SqlDbType.Char, 500);
					sqlCommand.Parameters["@return"].Direction = ParameterDirection.Output;
					sqlCommand.Parameters["@errMessage"].Direction = ParameterDirection.Output;
					sqlConnection.Open();
					sqlCommand.CommandTimeout = 0;
					sqlCommand.ExecuteNonQuery();
					sqlCommand.Parameters["@errMessage"].Value.ToString2();
					obj = sqlCommand.Parameters["@return"].Value.ToString2().ToInt32();
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			return obj.toInt32();
		}

		public static bool UpdateMethod_SP(object entity, string sStoredProceedure, string Connection)
		{
			bool flag = false;
			int num = 0;
			using (SqlConnection sqlConnection = new SqlConnection(Connection))
			{
				using SqlCommand sqlCommand = new SqlCommand(sStoredProceedure, sqlConnection);
				sqlCommand.CommandType = CommandType.StoredProcedure;
				try
				{
					PropertyInfo[] properties = entity.GetType().GetProperties();
					foreach (PropertyInfo propertyInfo in properties)
					{
						if (propertyInfo.GetValue(entity, null) != null && (!(propertyInfo.PropertyType.Name == "Nullable`1") || !(propertyInfo.GetValue(entity, null).ToString() == "0")))
						{
							if (propertyInfo.PropertyType.Name == "Byte[]")
							{
								sqlCommand.Parameters.AddWithValue(propertyInfo.Name, (byte[])propertyInfo.GetValue(entity, null));
							}
							else if (propertyInfo.PropertyType.Name == "DateTime")
							{
								sqlCommand.Parameters.AddWithValue("@" + propertyInfo.Name, AniHelper.GetDate((DateTime)propertyInfo.GetValue(entity, null)));
							}
							else
							{
								sqlCommand.Parameters.AddWithValue(propertyInfo.Name, propertyInfo.GetValue(entity, null).ToString());
							}
						}
					}
					sqlConnection.Open();
					num = sqlCommand.ExecuteNonQuery();
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			if (num > 0)
			{
				return true;
			}
			return false;
		}

		public static bool UpdateMethod_SP(List<object> entities, string sStoredProceedure, string Connection)
		{
			try
			{
				foreach (object entity in entities)
				{
					UpdateMethod_SP(entity, sStoredProceedure, Connection);
				}
				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool UpdateMethod_SP(List<object> newDatas, List<object> oldDatas, string sTable, string sColumn, string sConnection)
		{
			List<Common> list = new List<Common>();
			List<Common> list2 = new List<Common>();
			list = GetIdList(newDatas, sColumn);
			list2 = GetIdList(oldDatas, sColumn);
			try
			{
				if (list2.Count > 0)
				{
					foreach (Common item2 in list2)
					{
						if (!list.Any((Common x) => x.id == item2.id))
						{
							DeleteMethod_SP(item2.id, "spDelete" + sTable, sConnection);
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			foreach (Common item in list)
			{
				if (list2.Any((Common x) => x.id == item.id))
				{
					int? num = item.id;
					try
					{
						foreach (object newData in newDatas)
						{
							PropertyInfo[] properties = newData.GetType().GetProperties();
							int num2 = 0;
							if (0 < properties.Length)
							{
								PropertyInfo propertyInfo = properties[num2];
								if (propertyInfo.Name == sColumn && new int?(propertyInfo.GetValue(newData, null).ToInt32()) == num)
								{
									UpdateMethod_SP(newData, "spUpdate" + sTable, sConnection);
								}
							}
						}
					}
					catch (Exception ex2)
					{
						throw ex2;
					}
					continue;
				}
				int? num3 = item.id;
				try
				{
					foreach (object newData2 in newDatas)
					{
						PropertyInfo[] properties2 = newData2.GetType().GetProperties();
						foreach (PropertyInfo propertyInfo2 in properties2)
						{
							if (propertyInfo2.Name == sColumn && new int?(propertyInfo2.GetValue(newData2, null).ToInt32()) == num3)
							{
								InsertMethod_SP(newData2, "spInsert" + sTable, sConnection);
								break;
							}
						}
					}
				}
				catch (Exception ex3)
				{
					throw ex3;
				}
				break;
			}
			return true;
		}

		public static bool DeleteMethod_SP(object entity, string sStoredProceedure, string Connection)
		{
			bool flag = false;
			int num = 0;
			using (SqlConnection sqlConnection = new SqlConnection(Connection))
			{
				using SqlCommand sqlCommand = new SqlCommand(sStoredProceedure, sqlConnection);
				sqlCommand.CommandType = CommandType.StoredProcedure;
				try
				{
					sqlCommand.Parameters.AddWithValue("Id", entity.ToInt32());
					sqlConnection.Open();
					num = sqlCommand.ExecuteNonQuery();
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			if (num > 0)
			{
				return true;
			}
			return false;
		}

		public static DataTable GetDataTable_SP(string sStoredProceedure, string Connection)
		{
			CultureInfo cultureInfo3 = (Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo("en")));
			try
			{
				DataTable dataTable = new DataTable();
				using (SqlConnection connection = new SqlConnection(Connection))
				{
					using SqlCommand sqlCommand = new SqlCommand(sStoredProceedure, connection);
					sqlCommand.CommandType = CommandType.StoredProcedure;
					using SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
					sqlDataAdapter.Fill(dataTable);
				}
				return dataTable;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable GetDataTableWithIdParameter_SP(string sStoredProceedure, string Value, string Connection)
		{
			CultureInfo cultureInfo3 = (Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo("en")));
			try
			{
				DataTable dataTable = new DataTable();
				using (SqlConnection connection = new SqlConnection(Connection))
				{
					using SqlCommand sqlCommand = new SqlCommand(sStoredProceedure, connection);
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.Parameters.AddWithValue("@Id", Value);
					using SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
					sqlDataAdapter.Fill(dataTable);
				}
				return dataTable;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable GetDataTableWithIdParameter_SP(string sStoredProceedure, object entity, string Connection)
		{
			CultureInfo cultureInfo3 = (Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo("en")));
			try
			{
				DataTable dataTable = new DataTable();
				using (SqlConnection connection = new SqlConnection(Connection))
				{
					using SqlCommand sqlCommand = new SqlCommand(sStoredProceedure, connection);
					sqlCommand.CommandType = CommandType.StoredProcedure;
					if (entity != null)
					{
						PropertyInfo[] properties = entity.GetType().GetProperties();
						foreach (PropertyInfo propertyInfo in properties)
						{
							if (propertyInfo.GetValue(entity, null) != null)
							{
								sqlCommand.Parameters.AddWithValue(propertyInfo.Name, propertyInfo.GetValue(entity, null).ToString());
							}
						}
					}
					sqlCommand.CommandTimeout = 0;
					using SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
					sqlDataAdapter.Fill(dataTable);
				}
				return dataTable;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static List<T> GetList_SP<T>(string sStoredProceedure, string Connection)
		{
			try
			{
				List<T> list = new List<T>();
				DataTable dataTable = new DataTable();
				dataTable = GetDataTable_SP(sStoredProceedure, Connection);
				return ConvertDataTable<T>(dataTable);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static List<T> GetList_SP<T>(string sStoredProceedure, string Value, string Connection)
		{
			try
			{
				List<T> list = new List<T>();
				DataTable dataTable = new DataTable();
				dataTable = GetDataTableWithIdParameter_SP(sStoredProceedure, Value, Connection);
				return ConvertDataTable<T>(dataTable);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static List<T> GetList_SP<T>(string sStoredProceedure, object entity, string Connection)
		{
			try
			{
				List<T> list = new List<T>();
				DataTable dataTable = new DataTable();
				dataTable = GetDataTableWithIdParameter_SP(sStoredProceedure, entity, Connection);
				return ConvertDataTable<T>(dataTable);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static T GetObject_SP<T>(string sStoredProceedure, string Value, string Connection)
		{
			try
			{
				_ = typeof(T);
				T result = Activator.CreateInstance<T>();
				DataTable dataTable = new DataTable();
				dataTable = GetDataTableWithIdParameter_SP(sStoredProceedure, Value, Connection);
				if (dataTable.Rows.Count > 0)
				{
					result = GetItem<T>(dataTable.Rows[0]);
				}
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static T GetObjectWithparameter_SP<T>(string sStoredProceedure, object entity, string Connection)
		{
			try
			{
				_ = typeof(T);
				T result = Activator.CreateInstance<T>();
				DataTable dataTable = new DataTable();
				dataTable = GetDataTableWithIdParameter_SP(sStoredProceedure, entity, Connection);
				if (dataTable.Rows.Count > 0)
				{
					result = GetItem<T>(dataTable.Rows[0]);
				}
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static T GetObject_SP<T>(string sStoredProceedure, string Connection)
		{
			try
			{
				T result = Activator.CreateInstance<T>();
				DataTable dataTable = new DataTable();
				dataTable = GetDataTable_SP(sStoredProceedure, Connection);
				if (dataTable.Rows.Count > 0)
				{
					result = GetItem<T>(dataTable.Rows[0]);
				}
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static string GetData_SP(string sStoredProceedure, string Connection)
		{
			DataTable dataTable = new DataTable();
			dataTable = GetDataTable_SP(sStoredProceedure, Connection);
			if (dataTable.Rows.Count > 0)
			{
				return dataTable.Rows[0][0].ToString();
			}
			return null;
		}

		public static string GetDataWithParameter_SP(string sStoredProceedure, object entity, string Connection)
		{
			DataTable dataTable = new DataTable();
			dataTable = GetDataTableWithIdParameter_SP(sStoredProceedure, entity, Connection);
			if (dataTable.Rows.Count > 0)
			{
				return dataTable.Rows[0][0].ToString();
			}
			return null;
		}

		public static bool DatabaseExecution_SP(string sStoredProceedure, string Connection, object entity = null)
		{
			bool flag = false;
			using SqlConnection sqlConnection = new SqlConnection(Connection);
			using SqlCommand sqlCommand = new SqlCommand(sStoredProceedure, sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			if (entity != null)
			{
				PropertyInfo[] properties = entity.GetType().GetProperties();
				foreach (PropertyInfo propertyInfo in properties)
				{
					if (propertyInfo.GetValue(entity, null) != null)
					{
						sqlCommand.Parameters.AddWithValue(propertyInfo.Name, propertyInfo.GetValue(entity, null).ToString());
					}
				}
			}
			try
			{
				sqlConnection.Open();
				int num = sqlCommand.ExecuteNonQuery();
				if (num > 0)
				{
					return true;
				}
				return false;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataSet GetDataSet_SP(string sStoredProceedure, object entity, string Connection)
		{
			CultureInfo cultureInfo3 = (Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo("en")));
			DataSet dataSet = new DataSet();
			try
			{
				using (SqlConnection connection = new SqlConnection(Connection))
				{
					using SqlCommand sqlCommand = new SqlCommand(sStoredProceedure, connection);
					sqlCommand.CommandType = CommandType.StoredProcedure;
					if (entity != null)
					{
						PropertyInfo[] properties = entity.GetType().GetProperties();
						foreach (PropertyInfo propertyInfo in properties)
						{
							if (propertyInfo.GetValue(entity, null) != null)
							{
								sqlCommand.Parameters.AddWithValue(propertyInfo.Name, propertyInfo.GetValue(entity, null).ToString());
							}
						}
					}
					sqlCommand.CommandTimeout = 0;
					using SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
					sqlDataAdapter.Fill(dataSet);
				}
				return dataSet;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
