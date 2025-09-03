using System;
using System.Windows.Forms;

namespace AshUsercontrol
{
	public class DataGridViewDateTimeCell : DataGridViewTextBoxCell
	{
		public override Type EditType => typeof(CalendarEditingControl);

		public override Type ValueType => typeof(DateTime);

		public override object DefaultNewRowValue => DateTime.Now;

		public DataGridViewDateTimeCell()
		{
			base.Style.Format = "d";
		}

		public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
		{
			base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
			CalendarEditingControl calendarEditingControl = base.DataGridView.EditingControl as CalendarEditingControl;
			if (base.Value == null)
			{
				calendarEditingControl.Value = (DateTime)DefaultNewRowValue;
			}
			else
			{
				calendarEditingControl.Value = (DateTime)base.Value;
			}
		}
	}
}
