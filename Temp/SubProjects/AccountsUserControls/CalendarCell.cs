using System;
using System.Windows.Forms;

namespace AccountsPro.SubProjects.AccountsUserControls
{
	public class CalendarCell : DataGridViewTextBoxCell
	{
		public override Type EditType => typeof(CalendarEditingControl);

		public override Type ValueType => typeof(DateTime);

		public override object DefaultNewRowValue => DateTime.Now;

		public CalendarCell()
		{
            Style.Format = "d";
		}

		public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
		{
			base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
			CalendarEditingControl calendarEditingControl = DataGridView.EditingControl as CalendarEditingControl;
			if (Value == null)
			{
				calendarEditingControl.Value = (DateTime)DefaultNewRowValue;
			}
			else
			{
				calendarEditingControl.Value = (DateTime)Value;
			}
		}
	}
}
