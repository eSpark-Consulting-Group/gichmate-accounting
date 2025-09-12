using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AccountsPro.SubProjects.AccountsLibraryFramework
{
	public static class StringExtension
	{
		public static T Cast<T>(this object obj, T typeHolder)
		{
			return (T)obj;
		}

		public static bool IsArabic(this string input)
		{
			return Regex.IsMatch(input, "\\p{IsArabic}");
		}

		public static bool toBool(this bool? obj)
		{
			try
			{
				return Convert.ToBoolean(obj);
			}
			catch (Exception)
			{
				return false;
			}
		}

		public static bool ToBool(this string obj)
		{
			try
			{
				if (obj != "")
				{
					return Convert.ToBoolean(obj);
				}
				return false;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public static int ToInt32(this string obj)
		{
			try
			{
				if (obj == null || obj.Trim() == "")
				{
					return 0;
				}
				return Convert.ToInt32(obj);
			}
			catch (Exception)
			{
				return 0;
			}
		}

		public static short ToInt16(this string obj)
		{
			try
			{
				if (obj == null || obj.Trim() == "")
				{
					return 0;
				}
				return Convert.ToInt16(obj);
			}
			catch (Exception)
			{
				return 0;
			}
		}

		public static decimal ToDecimal(this string obj)
		{
			try
			{
				decimal result = default;
				if (decimal.TryParse(obj, out result))
				{
					return result;
				}
				return 0m;
			}
			catch (Exception)
			{
				return 0m;
			}
		}

		public static double ToDouble(this decimal? obj)
		{
			return Convert.ToDouble(obj);
		}

		public static string RoundFormatted(this decimal? obj, string Format)
		{
			if (!obj.HasValue)
			{
				return 0.ToString(Format);
			}
			return GlobalFunctions.getFormattedValue(Format, obj.ToDecimal());
		}

		public static double ToDouble(this string obj)
		{
			try
			{
				if (obj == null || obj.Trim() == "")
				{
					return 0.0;
				}
				return Convert.ToDouble(obj);
			}
			catch (Exception)
			{
				return 0.0;
			}
		}

		public static DateTime toDateTime(this string Obj)
		{
			try
			{
				return Convert.ToDateTime(Obj);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DateTime ToDateTime(this DateTime? Obj)
		{
			if (!Obj.HasValue)
			{
				return DateTime.Now;
			}
			return Obj.Value;
		}

		public static bool isNumeric(this object Expression)
		{
			double result;
			return double.TryParse(Convert.ToString(Expression), NumberStyles.Any, NumberFormatInfo.InvariantInfo, out result);
		}

		public static string ToString2(this object _obj)
		{
			if (_obj != null)
			{
				return _obj.ToString();
			}
			return "";
		}

		public static int ToInt32(this object _obj)
		{
			return _obj?.ToString().ToInt32() ?? 0;
		}

		public static int toInt32(this int? _obj)
		{
			if (_obj.HasValue)
			{
				return Convert.ToInt32(_obj);
			}
			return 0;
		}

		public static decimal ToDecimal(this decimal? obj)
		{
			return Convert.ToDecimal(obj);
		}

		public static decimal ToDecimal(this double obj)
		{
			return Convert.ToDecimal(obj);
		}

		public static decimal Round(this decimal? obj, int NoofDecimals)
		{
			return Math.Round(obj.ToDecimal(), NoofDecimals);
		}

		public static string ToString(this decimal? obj, string sFormat)
		{
			return obj.ToDecimal().ToString(sFormat);
		}

		public static bool IsNumericKey(this KeyEventArgs key)
		{
			switch (key.KeyCode)
			{
			case Keys.Back:
			case Keys.Delete:
			case Keys.D0:
			case Keys.D1:
			case Keys.D2:
			case Keys.D3:
			case Keys.D4:
			case Keys.D5:
			case Keys.D6:
			case Keys.D7:
			case Keys.D8:
			case Keys.D9:
			case Keys.NumPad0:
			case Keys.NumPad1:
			case Keys.NumPad2:
			case Keys.NumPad3:
			case Keys.NumPad4:
			case Keys.NumPad5:
			case Keys.NumPad6:
			case Keys.NumPad7:
			case Keys.NumPad8:
			case Keys.NumPad9:
			case Keys.OemMinus:
				return true;
			default:
				return false;
			}
		}

		public static T CopyTo<T>(this object source)
		{
			T val = Activator.CreateInstance<T>();
			if (source != null)
			{
				GlobalFunctions.CopyData(source, val);
			}
			return val;
		}

		public static string ToBase64(this string plainText)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(plainText);
			return Convert.ToBase64String(bytes);
		}

		public static string FromBase64(this string base64EncodedData)
		{
			byte[] bytes = Convert.FromBase64String(base64EncodedData);
			return Encoding.UTF8.GetString(bytes);
		}

		public static string ToArabic(this string input)
		{
			return input.Replace('0', '۰').Replace('1', '۱').Replace('2', '۲')
				.Replace('3', '۳')
				.Replace('4', '۴')
				.Replace('5', '۵')
				.Replace('6', '۶')
				.Replace('7', '۷')
				.Replace('8', '۸')
				.Replace('9', '۹');
		}

		public static void ToCSV(this DataTable dtDataTable, string strFilePath)
		{
			StreamWriter streamWriter = new StreamWriter(strFilePath, append: false, Encoding.BigEndianUnicode);
			for (int i = 0; i < dtDataTable.Columns.Count; i++)
			{
				streamWriter.Write(dtDataTable.Columns[i]);
				if (i < dtDataTable.Columns.Count - 1)
				{
					streamWriter.Write(",");
				}
			}
			streamWriter.Write(streamWriter.NewLine);
			foreach (DataRow row in dtDataTable.Rows)
			{
				for (int j = 0; j < dtDataTable.Columns.Count; j++)
				{
					if (!Convert.IsDBNull(row[j]))
					{
						string text = row[j].ToString();
						if (text.Contains(','))
						{
							text = $"\"{text}\"";
							streamWriter.Write(text);
						}
						else
						{
							streamWriter.Write(row[j].ToString());
						}
					}
					if (j < dtDataTable.Columns.Count - 1)
					{
						streamWriter.Write(",");
					}
				}
				streamWriter.Write(streamWriter.NewLine);
			}
			streamWriter.Close();
		}
	}
}
