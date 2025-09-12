using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AccountsPro.SubProjects.AccountsUserControls
{
	public class AshNumericLabel : Label
	{
		private decimal _value;

		private IContainer components = null;

		public string Format { get; set; }

		public decimal Value
		{
			get
			{
				decimal result = default;
				decimal.TryParse(Text, out result);
				return result;
			}
			set
			{
				_value = value;
				Text = getFormattedValue(Format, _value);
			}
		}

		protected override void InitLayout()
		{
			base.InitLayout();
			TextAlign = ContentAlignment.MiddleRight;
		}

		public static string getFormattedValue(string Format, decimal _value)
		{
			if (Format != null && Format != "" && Format.Length > 1)
			{
				int num = Convert.ToInt32(Format.Substring(1));
				if (num > 0)
				{
					return Math.Round(_value, num).ToString(Format);
				}
				return _value.ToString(Format);
			}
			return _value.ToString();
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
