using System.Windows.Forms;

namespace AshUsercontrol
{
	public class DataGridViewDateTimeColumn : DataGridViewColumn
	{
		public DataGridViewDateTimeColumn()
		{
			CellTemplate = new DataGridViewDateTimeCell();
			ReadOnly = false;
		}
	}
}
