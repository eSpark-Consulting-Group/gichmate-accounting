using System;
using System.Reflection;

namespace AshFramework
{
	public class GlobalFunctions
	{
		public static string getFormattedValue(string Format, decimal _value)
		{
			if (Format != null && Format != "" && Format.Length > 1)
			{
				return _value.ToString(Format);
			}
			return _value.ToString();
		}

		public static string GetDate(DateTime dateTime)
		{
			try
			{
				return dateTime.ToString("yyyy-MM-ddTHH:mm:ss");
			}
			catch (Exception)
			{
				throw;
			}
		}

		public static void CopyData(object source, object target, bool CopyID = false)
		{
			Type type = source.GetType();
			Type type2 = target.GetType();
			PropertyInfo[] properties = type.GetProperties();
			PropertyInfo[] array = properties;
			foreach (PropertyInfo propertyInfo in array)
			{
				PropertyInfo property = type2.GetProperty(propertyInfo.Name);
				if (!(property != null))
				{
					continue;
				}
				if (propertyInfo.PropertyType.Name == "DateTime")
				{
					property.SetValue(target, GetDate((DateTime)propertyInfo.GetValue(source, null)).toDateTime());
				}
				else if (propertyInfo.PropertyType.Name == "Boolean")
				{
					property.SetValue(target, propertyInfo.GetValue(source, null).ToString().ToBool());
				}
				else if (propertyInfo.PropertyType.Name == "Byte[]")
				{
					property.SetValue(target, (byte[])propertyInfo.GetValue(source, null));
				}
				else if (propertyInfo.PropertyType.Name == "Nullable`1")
				{
					if (propertyInfo.PropertyType.FullName.Contains("System.Boolean"))
					{
						try
						{
							property.SetValue(target, propertyInfo.GetValue(source, null).ToString().ToBool());
						}
						catch (Exception)
						{
						}
					}
				}
				else if (!isValidDataType(propertyInfo.PropertyType.ToString()))
				{
					continue;
				}
				if ((!(propertyInfo.Name == "id") || CopyID) && propertyInfo.CanRead && !(property == null) && property.CanWrite && (!(property.GetSetMethod(nonPublic: true) != null) || !property.GetSetMethod(nonPublic: true).IsPrivate) && (property.GetSetMethod().Attributes & MethodAttributes.Static) == 0 && property.PropertyType.IsAssignableFrom(propertyInfo.PropertyType))
				{
					try
					{
						property.SetValue(target, propertyInfo.GetValue(source, null), null);
					}
					catch (Exception)
					{
					}
				}
			}
		}

		private static bool isValidDataType(string dataType)
		{
			bool result = false;
			switch (dataType)
			{
			case "System.Nullable`1[System.Double]":
				result = true;
				break;
			case "System.Nullable`1[System.Decimal]":
				result = true;
				break;
			case "System.Nullable`1[System.Int16]":
				result = true;
				break;
			case "System.Nullable`1[System.Int32]":
				result = true;
				break;
			case "System.Nullable`1[System.Int64]":
				result = true;
				break;
			case "System.Nullable`1[System.Boolean]":
				result = true;
				break;
			case "System.Nullable`1[System.DateTime]":
				result = true;
				break;
			case "System.Boolean":
				result = true;
				break;
			case "System.Int16":
				result = true;
				break;
			case "System.Int32":
				result = true;
				break;
			case "System.Int64":
				result = true;
				break;
			case "System.String":
				result = true;
				break;
			case "System.Decimal":
				result = true;
				break;
			case "System.Double":
				result = true;
				break;
			}
			return result;
		}
	}
}
