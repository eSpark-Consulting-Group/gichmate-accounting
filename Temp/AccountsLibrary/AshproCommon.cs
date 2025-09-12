using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccountsLibrary.Properties;
using AccountsPro.SubProjects.AccountsLibraryFramework;

namespace AccountsLibrary
{
	public class AshproCommon
	{
		public static T GetObjectDetails<T>(string sTable, string sCon = null)
		{
			sCon = sCon ?? Settings.Default.strConnetionString;
			if (sTable.Contains("Select") && sTable.Count() > 15)
			{
				return AshproORM.GetObjectDetails<T>(sTable, sCon);
			}
			string empty = string.Empty;
			empty = "spGet" + sTable;
			return AshproORM.GetObject_SP<T>(empty, sCon);
		}

		public static DataTable GetDataTable(string Query, string sConnection = null)
		{
			if (Query == string.Empty)
			{
				return null;
			}
			if (sConnection == null)
			{
				sConnection = Settings.Default.strConnetionString;
			}
			return AshproORM.GetDataTable(Query, sConnection);
		}

		public static DataTable GetDataTable(string sTable, object entity, string sConnection = null)
		{
			if (sTable == string.Empty)
			{
				return null;
			}
			if (sConnection == null)
			{
				sConnection = Settings.Default.strConnetionString;
			}
			string empty = string.Empty;
			empty = "spGet" + sTable;
			if (entity == null)
			{
				return AshproORM.GetDataTable_SP(empty, Settings.Default.strConnetionString);
			}
			return AshproORM.GetDataTableWithIdParameter_SP(empty, entity, Settings.Default.strConnetionString);
		}

		public static async Task<DataTable> GetDataTableAsync(string Query, string sConnection = null)
		{
			if (Query == string.Empty)
			{
				return null;
			}
			if (sConnection == null)
			{
				sConnection = Settings.Default.strConnetionString;
			}
			return await AshproORM.GetDataTableAsync(Query, sConnection);
		}

		public static void IdFindMethod(string strTab, string strColm, string strWCm, string strComboText, out string strID)
		{
			strID = Convert.ToString(IdFindMethod("SELECT " + strColm + " FROM " + strTab + " where " + strWCm + " = '" + strComboText + "'"));
		}

		public static dynamic IdFindMethod(string Query)
		{
			return AshproORM.ValueFindMethod(Query, Settings.Default.strConnetionString);
		}

		public static int IdFindMethod(string Query, bool isVoucherNo = false)
		{
			int? num = 0;
			if (IdFindMethod(Query) != null)
			{
				num = IdFindMethod(Query);
				if (isVoucherNo)
				{
					num = ((!num.HasValue) ? new int?(1) : (num + 1));
				}
			}
			else if (isVoucherNo)
			{
				num = 1;
			}
			return num.toInt32();
		}

		public static decimal ValueFindMethod(string Query)
		{
			decimal? obj = Convert.ToDecimal(IdFindMethod(Query));
			return obj.ToDecimal();
		}

		public static List<T> GetListMethod<T>(string sTable, object entity = null)
		{
			if (sTable.Contains("Select") && sTable.Count() > 15)
			{
				return AshproORM.GetListMethod<T>(sTable, Settings.Default.strConnetionString);
			}
			string empty = string.Empty;
			empty = "spGet" + sTable;
			if (entity == null)
			{
				return AshproORM.GetList_SP<T>(empty, Settings.Default.strConnetionString);
			}
			return AshproORM.GetList_SP<T>(empty, entity, Settings.Default.strConnetionString);
		}

		public static List<string> GetStringListMethod(string Query)
		{
			return AshproORM.GetStringListMethod(Query, Settings.Default.strConnetionString);
		}

		public static bool DatabaseMethod(string Query, string Connection = null)
		{
			if (Query == string.Empty)
			{
				return false;
			}
			if (Connection == null)
			{
				Connection = Settings.Default.strConnetionString;
			}
			return AshproORM.DatabaseMethod(Query, Connection);
		}

		public static bool InsertToDatabase(List<object> datas, string table = null, string sCon = null)
		{
			try
			{
				foreach (object data in datas)
				{
					InsertToDatabaseObj(data, table, sCon);
				}
			}
			catch (Exception ex)
			{
				_ = ex.Message;
				return false;
			}
			return true;
		}

		public static bool InsertToDatabaseObj(object data, string table = null, string sCon = null)
		{
			if (sCon == null)
			{
				sCon = Settings.Default.strConnetionString;
			}
			if (table == null)
			{
				table = data.GetType().Name;
			}
			if (table == "[User]")
			{
				table = "User";
			}
			table = "spInsert" + table;
			return AshproORM.InsertMethod_SP(data, table, sCon);
		}

		public static bool UpdateToDatabase(List<object> datas, string table, string column)
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
					UpdateToDatabaseObj(data, table, column, iValue);
				}
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		public static bool UpdateToDatabaseObj(object data, string table, string column, int iValue)
		{
			if (table == "[User]")
			{
				table = "User";
			}
			table = "spUpdate" + table;
			return AshproORM.UpdateMethod_SP(data, table, Settings.Default.strConnetionString);
		}

		public static bool DeleteFromDatabase(string table, string column, int iValue)
		{
			if (table == "[User]")
			{
				table = "User";
			}
			return AshproORM.DeleteMethod_SP(iValue, "spDelete" + table, Settings.Default.strConnetionString);
		}

		public static bool DeleteMethod(string Query)
		{
			return AshproORM.DeleteMethod(Query, Settings.Default.strConnetionString);
		}

		public static void FillCombo(Control ctrl, string Query)
		{
			if (ctrl is ComboBox)
			{
				ComboBox comboBox = (ComboBox)ctrl;
				comboBox.ValueMember = "Id";
				comboBox.DisplayMember = "Name";
				DataTable dataTable = new DataTable();
				dataTable = (DataTable)(comboBox.DataSource = GetDataTable(Query));
				return;
			}
			if (ctrl is CheckedListBox)
			{
				CheckedListBox checkedListBox = (CheckedListBox)ctrl;
				checkedListBox.ValueMember = "Id";
				checkedListBox.DisplayMember = "Name";
				checkedListBox.DataSource = GetDataTable(Query);
				return;
			}
			DataTable dataTable3 = new DataTable();
			dataTable3 = GetDataTable(Query);
			foreach (DataRow row in dataTable3.Rows)
			{
				ctrl.Tag = row[0].ToString();
				ctrl.Text = row[1].ToString();
			}
		}

		public static bool UpdateDatabase(DataTable datas, DataTable oldDatas, string table, string sConnection = null)
		{
			if (sConnection == null)
			{
				sConnection = Settings.Default.strConnetionString;
			}
			return AshproORM.UpdateDatabase(datas, oldDatas, table, sConnection);
		}

		public static bool UpdateDatabase(List<object> datas, List<object> oldDatas, string table, string sColumn, string sConnection = null)
		{
			if (sConnection == null)
			{
				sConnection = Settings.Default.strConnetionString;
			}
			return AshproORM.UpdateMethod_SP(datas, oldDatas, table, sColumn, sConnection);
		}

		public static bool InsertMethod_SP(List<object> entities, string sStoredProceedure)
		{
			return AshproORM.InsertMethod_SP(entities, sStoredProceedure, Settings.Default.strConnetionString);
		}

		public static int InsertMethod_SP(object entity, DataSet ds, string sStoredProceedure, object Secondentity = null, string sConnection = null)
		{
			if (sConnection == null)
			{
				sConnection = Settings.Default.strConnetionString;
			}
			return AshproORM.InsertMethod_SP(entity, ds, sStoredProceedure, sConnection, Secondentity);
		}

		public static bool InsertMethod_SP(object entity, string sStoredProceedure)
		{
			return AshproORM.InsertMethod_SP(entity, sStoredProceedure, Settings.Default.strConnetionString);
		}

		public static bool UpdateMethod_SP(object entity, string sStoredProceedure)
		{
			return AshproORM.UpdateMethod_SP(entity, sStoredProceedure, Settings.Default.strConnetionString);
		}

		public static bool UpdateMethod_SP(List<object> entities, string sStoredProceedure)
		{
			return AshproORM.UpdateMethod_SP(entities, sStoredProceedure, Settings.Default.strConnetionString);
		}

		public static bool DeleteMethod_SP(object entity, string sStoredProceedure)
		{
			return AshproORM.DeleteMethod_SP(entity, sStoredProceedure, Settings.Default.strConnetionString);
		}

		public static DataTable GetDataTable_SP(string sStoredProceedure)
		{
			return AshproORM.GetDataTable_SP(sStoredProceedure, Settings.Default.strConnetionString);
		}

		public static DataTable GetDataTableWithIdParameter_SP(string sStoredProceedure, string Value)
		{
			return AshproORM.GetDataTableWithIdParameter_SP(sStoredProceedure, Value, Settings.Default.strConnetionString);
		}

		public static DataTable GetDataTableWithIdParameter_SP(string sStoredProceedure, object entity)
		{
			return AshproORM.GetDataTableWithIdParameter_SP(sStoredProceedure, entity, Settings.Default.strConnetionString);
		}

		public static DataSet GetDataSet_SP(string sStoredProceedure, object entity)
		{
			return AshproORM.GetDataSet_SP(sStoredProceedure, entity, Settings.Default.strConnetionString);
		}

		public static List<T> GetList_SP<T>(string sStoredProceedure)
		{
			return AshproORM.GetList_SP<T>(sStoredProceedure, Settings.Default.strConnetionString);
		}

		public static List<T> GetList_SP<T>(string sStoredProceedure, string Value)
		{
			return AshproORM.GetList_SP<T>(sStoredProceedure, Value, Settings.Default.strConnetionString);
		}

		public static List<T> GetList_SP<T>(string sStoredProceedure, object entity)
		{
			return AshproORM.GetList_SP<T>(sStoredProceedure, entity, Settings.Default.strConnetionString);
		}

		public static T GetObject_SP<T>(string sStoredProceedure, string Value)
		{
			return AshproORM.GetObject_SP<T>(sStoredProceedure, Value, Settings.Default.strConnetionString);
		}

		public static T GetObjectWithParameter_SP<T>(string sStoredProceedure, object entity)
		{
			return AshproORM.GetObjectWithparameter_SP<T>(sStoredProceedure, entity, Settings.Default.strConnetionString);
		}

		public static T GetObject_SP<T>(string sStoredProceedure)
		{
			return AshproORM.GetObject_SP<T>(sStoredProceedure, Settings.Default.strConnetionString);
		}

		public static int GetIntData_SP(string sStoredProceedure)
		{
			return AshproORM.GetData_SP(sStoredProceedure, Settings.Default.strConnetionString).ToInt32();
		}

		public static decimal GetDecimalData_SP(string sStoredProceedure)
		{
			return AshproORM.GetData_SP(sStoredProceedure, Settings.Default.strConnetionString).ToDecimal();
		}

		public static decimal GetDecimalDataWithParameter_SP(string sStoredProceedure, object entity)
		{
			return AshproORM.GetDataWithParameter_SP(sStoredProceedure, entity, Settings.Default.strConnetionString).ToDecimal();
		}

		public static string GetstringData_SP(string sStoredProceedure)
		{
			return AshproORM.GetData_SP(sStoredProceedure, Settings.Default.strConnetionString);
		}

		public static int GetIntDataWithParameter_SP(string sStoredProceedure, object entity)
		{
			return AshproORM.GetDataWithParameter_SP(sStoredProceedure, entity, Settings.Default.strConnetionString).ToInt32();
		}

		public static bool DBExicutionMethod_SP(string sStoredProceedure, object entity = null)
		{
			return AshproORM.DatabaseExecution_SP(sStoredProceedure, Settings.Default.strConnetionString, entity);
		}
	}
}
