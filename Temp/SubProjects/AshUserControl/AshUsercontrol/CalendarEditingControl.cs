using System;
using System.Windows.Forms;

namespace AshUsercontrol
{
	internal class CalendarEditingControl : DateTimePicker, IDataGridViewEditingControl
	{
		private DataGridView dataGridView;

		private bool valueChanged = false;

		private int rowIndex;

		public object EditingControlFormattedValue
		{
			get
			{
				return base.Value.ToShortDateString();
			}
			set
			{
				if (value is string)
				{
					try
					{
						base.Value = DateTime.Parse((string)value);
					}
					catch
					{
						base.Value = DateTime.Now;
					}
				}
			}
		}

		public int EditingControlRowIndex
		{
			get
			{
				return rowIndex;
			}
			set
			{
				rowIndex = value;
			}
		}

		public bool RepositionEditingControlOnValueChange => false;

		public DataGridView EditingControlDataGridView
		{
			get
			{
				return dataGridView;
			}
			set
			{
				dataGridView = value;
			}
		}

		public bool EditingControlValueChanged
		{
			get
			{
				return valueChanged;
			}
			set
			{
				valueChanged = value;
			}
		}

		public Cursor EditingPanelCursor => base.Cursor;

		public CalendarEditingControl()
		{
			base.Format = DateTimePickerFormat.Short;
		}

		public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
		{
			return EditingControlFormattedValue;
		}

		public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
		{
			Font = dataGridViewCellStyle.Font;
			base.CalendarForeColor = dataGridViewCellStyle.ForeColor;
			base.CalendarMonthBackground = dataGridViewCellStyle.BackColor;
		}

		public bool EditingControlWantsInputKey(Keys key, bool dataGridViewWantsInputKey)
		{
			Keys keys = key & Keys.KeyCode;
			Keys keys2 = keys;
			if ((uint)(keys2 - 33) <= 7u)
			{
				return true;
			}
			return !dataGridViewWantsInputKey;
		}

		public void PrepareEditingControlForEdit(bool selectAll)
		{
		}

		protected override void OnValueChanged(EventArgs eventargs)
		{
			valueChanged = true;
			EditingControlDataGridView.NotifyCurrentCellDirty(dirty: true);
			base.OnValueChanged(eventargs);
		}
	}
}
