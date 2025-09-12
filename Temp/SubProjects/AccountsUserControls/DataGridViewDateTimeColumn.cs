using System.Windows.Forms;

namespace AccountsPro.SubProjects.AccountsUserControls
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
