using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccountsLibrary.Properties;
using Microsoft.Win32;
using AccountsPro.SubProjects.AccountsLibraryFramework;

namespace AccountsLibrary
{
	public class DBHelper
	{
		public static List<string> GetDataSources()
		{
			List<string> list = new List<string>();
			string machineName = Environment.MachineName;
			RegistryView view = (Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32);
			using (RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, view))
			{
				RegistryKey registryKey2 = registryKey.OpenSubKey("SOFTWARE\\Microsoft\\Microsoft SQL Server\\Instance Names\\SQL", writable: false);
				if (registryKey2 != null)
				{
					string[] valueNames = registryKey2.GetValueNames();
					foreach (string text in valueNames)
					{
						list.Add(machineName + "\\" + text);
					}
				}
			}
			return list;
		}

		public static string NewDatabaseMethodSQL(string Company, string sServer, bool isShowMessage = true, bool isServer = true)
		{
			try
			{
				string empty = string.Empty;
				string empty2 = string.Empty;
				string empty3 = string.Empty;
				if (Company.Contains("\\"))
				{
					empty2 = Company;
					empty = empty2.Substring(empty2.LastIndexOf("\\") + 1);
					empty3 = empty2.Replace("\\" + empty, "");
					empty = empty.Replace(".mdf", "");
					empty3 = empty3 + "\\" + empty + "_log.ldf";
				}
				else
				{
					if (isServer)
					{
						empty3 = Path.GetDirectoryName(Application.ExecutablePath) + "\\Data";
						if (!Directory.Exists(empty3))
						{
							Directory.CreateDirectory(empty3);
						}
						empty2 = empty3 + "\\" + Company + ".mdf";
						empty3 = empty3 + "\\" + empty + "_log.ldf";
					}
					else
					{
						empty2 = "C:\\Program Files\\Microsoft SQL Server\\MSSQL13.MSSQLSERVER\\MSSQL\\DATA\\" + Company + ".mdf";
						empty3 = "C:\\Program Files\\Microsoft SQL Server\\MSSQL13.MSSQLSERVER\\MSSQL\\DATA\\" + empty + "_log.ldf";
					}
					empty = Company;
				}
				string query = "CREATE DATABASE [" + empty + "] ON PRIMARY (NAME = N'" + empty + "', FILENAME = '" + empty2 + "', SIZE = 5MB,  FILEGROWTH = 10%) LOG ON (NAME =N'" + empty + "_log', FILENAME = '" + empty3 + "', SIZE = 5MB, FILEGROWTH = 10%)";
				string text = "Server=" + sServer + ";Integrated security=SSPI;database=master";
				text = ((!isServer) ? ("Server = " + sServer + "; Database =master;User ID=" + Settings.Default.sUserName + ";Password=" + Settings.Default.sPassword) : ("Server=" + sServer + ";Integrated security=SSPI;database=master"));
				try
				{
					AshproCommon.DatabaseMethod(query, text);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message + " 99");
				}
				text = ((!isServer) ? ("Server = " + sServer + "; Database = " + empty + ";User ID=" + Settings.Default.sUserName + ";Password=" + Settings.Default.sPassword) : ("Server = " + sServer + "; Database = " + empty + ";Integrated Security = SSPI;"));
				if (CreatTables(text))
				{
					Settings.Default.strConnetionString = text;
					Settings.Default.Save();
					if (isShowMessage)
					{
						MessageBox.Show("New Company is Created Successfully", Utility.uManufacturer, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					return empty;
				}
				return null;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public static bool CreatTables(string Connection)
		{
			try
			{
				FileInfo fileInfo = new FileInfo(Path.GetDirectoryName(Application.ExecutablePath) + "\\MasterScript.sql");
				if (!fileInfo.Exists)
				{
                    System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
					if (openFileDialog.ShowDialog() == DialogResult.OK)
					{
						fileInfo = new FileInfo(openFileDialog.FileName);
					}
				}
				string text = fileInfo.OpenText().ReadToEnd();
				string[] array = text.Split(new string[1] { "GO-" }, StringSplitOptions.None);
				string[] array2 = array;
				foreach (string text2 in array2)
				{
					try
					{
						AshproCommon.DatabaseMethod(text2, Connection);
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message + " 134 " + text2);
					}
				}
			}
			catch (Exception ex2)
			{
				MessageBox.Show(ex2.Message + " 145");
				return false;
			}
			return true;
		}

		public static string DatabaseOnlyMethodSQL(string Company, string sServer)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			string empty3 = string.Empty;
			if (Company.Contains("\\"))
			{
				empty2 = Company;
				empty = empty2.Substring(empty2.LastIndexOf("\\") + 1);
				empty3 = empty2.Replace("\\" + empty, "");
				empty = empty.Replace(".mdf", "");
			}
			else
			{
				empty3 = Path.GetDirectoryName(Application.ExecutablePath) + "\\Data";
				if (!Directory.Exists(empty2))
				{
					Directory.CreateDirectory(empty2);
				}
				empty2 = empty3 + "\\" + Company + ".mdf";
				empty = Company;
			}
			string query = "CREATE DATABASE [" + empty + "] ON PRIMARY (NAME = N'" + empty + "', FILENAME = '" + empty2 + "', SIZE = 5MB,  FILEGROWTH = 10%) LOG ON (NAME =N'" + empty + "_log', FILENAME = '" + empty3 + "\\" + empty + "_log.ldf', SIZE = 5MB, FILEGROWTH = 10%)";
			string connection = "Server=" + sServer + ";Integrated security=SSPI;database=master";
			try
			{
				AshproCommon.DatabaseMethod(query, connection);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " 99");
			}
			connection = "Server = " + sServer + "; Database = " + empty + ";Integrated Security = SSPI;";
			Settings.Default.strConnetionString = connection;
			Settings.Default.Save();
			MessageBox.Show("New Company is Created Successfully", Utility.uManufacturer, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return empty;
		}

		public static async Task<bool> isValidCompany(string sCompanyName)
		{
			return await Task.Run(() => sCompanyName switch
			{
				"msdb" => false, 
				"model" => false, 
				"tempdb" => false, 
				"master" => false, 
				_ => true, 
			});
		}

		public static async Task<bool> isApplicationCompany(string sCompanyName, string sServer, bool isWAuthentication)
		{
			return await Task.Run(delegate
			{
				using (SqlConnection sqlConnection = new SqlConnection(DBConnectionStringSQLMethod(sCompanyName, sServer, isWAuthentication)))
				{
					try
					{
						sqlConnection.Open();
						using SqlCommand sqlCommand = new SqlCommand("select name from sys.tables where  type='U' and name='Account'", sqlConnection);
						using SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
						if (sqlDataReader.Read())
						{
							return true;
						}
					}
					catch (Exception)
					{
						return false;
					}
					finally
					{
						sqlConnection.Close();
					}
				}
				return false;
			});
		}

		public static string DBConnectionStringSQLMethod(string sCompany, string sServer, bool isWAuthentication)
		{
			string text = null;
			if (!isWAuthentication)
			{
				return "Server = " + sServer + "; Database = " + sCompany + ";User ID=" + Settings.Default.sUserName + ";Password=" + Settings.Default.sPassword;
			}
			return "Server = " + sServer + "; Database = " + sCompany + ";Integrated Security = True";
		}

		public static bool isUpgradeRequired()
		{
			string text = null;
			string text2 = null;
			int num = 0;
			new DBContext();
			try
			{
				int num2 = (Utility.uVersion = AniHelper.GetApplicationVersion());
				text = AshproCommon.IdFindMethod("Select Productserial From Institute");
				if (text.Contains("."))
				{
					string text3 = text.Substring(0, 2);
					string text4 = text.Substring(3, 1);
					string text5 = text.Substring(5, 4);
					num = (text3 + text4 + text5).ToInt32();
					if (num2 < num)
					{
						throw new Exception("Application Version: " + text2 + " Less Than Database Version:" + text);
					}
					if (num2 > num)
					{
						return true;
					}
					return false;
				}
				return false;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " 320");
				return true;
			}
		}

		//public static void DBUpgradInMethod()
		//{
		//	try
		//	{
		//		if (isUpgradeRequired())
		//		{
		//			UpgradeHelper upgradeHelper = new UpgradeHelper();
		//			upgradeHelper.ShowDialog();
		//		}
		//	}
		//	catch (Exception)
		//	{
		//	}
		//}

		public static void DatabaseAddMethod(string strDatabase, string sServer = "", bool isWAutentication = true)
		{
			string text = null;
			Settings.Default.strSQLCon = strDatabase;
			Settings.Default.strServer = sServer;
			Settings.Default.isServer = isWAutentication;
			text = DBConnectionStringSQLMethod(strDatabase, sServer, isWAutentication);
			Settings.Default.strConnetionString = text;
			Settings.Default.Save();
		}
	}
}
