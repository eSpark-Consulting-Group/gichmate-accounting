using System.Collections.Generic;
using System.Data;

namespace Ashpro
{
	public abstract class ReaderHelper<T>
	{
		public virtual List<T> GetAll(DataTable dt)
		{
			return LoadLists(dt.CreateDataReader());
		}

		public abstract T LoadFromReader(DataTableReader reader);

		public virtual List<T> LoadLists(DataTableReader reader)
		{
			List<T> list = new List<T>();
			try
			{
				while (reader.Read())
				{
					list.Add(LoadFromReader(reader));
				}
				return list;
			}
			finally
			{
				if (!reader.IsClosed)
				{
					reader.Close();
				}
			}
		}
	}
}
