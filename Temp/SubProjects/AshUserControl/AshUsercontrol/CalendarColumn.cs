using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace AshUsercontrol
{
	public class CalendarColumn : DataGridViewColumn
	{
		private IContainer components = null;

		public override DataGridViewCell CellTemplate
		{
			get
			{
				return base.CellTemplate;
			}
			set
			{
				if (value != null && !value.GetType().IsAssignableFrom(typeof(CalendarCell)))
				{
					throw new InvalidCastException("Must be a CalendarCell");
				}
				base.CellTemplate = value;
			}
		}

		public CalendarColumn()
			: base(new CalendarCell())
		{
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			components = new Container();
		}
	}
}
