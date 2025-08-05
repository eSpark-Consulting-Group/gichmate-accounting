using System.Windows.Forms;
using AshUsercontrol;

namespace Ashpro
{
	public static class FormatHelperClass
	{
		public static void Format(Form form)
		{
			foreach (Control control5 in form.Controls)
			{
				if (control5 is AshTextBoxNormal)
				{
					AshTextBoxNormal ashTextBoxNormal = (AshTextBoxNormal)control5;
					if (ashTextBoxNormal.isNumeric)
					{
						ashTextBoxNormal.Format = CommonSettings.strCurencyFormat;
					}
				}
				else if (control5 is Panel)
				{
					Panel panel = (Panel)control5;
					foreach (Control control6 in panel.Controls)
					{
						if (control6 is AshTextBoxNormal)
						{
							AshTextBoxNormal ashTextBoxNormal2 = (AshTextBoxNormal)control6;
							if (ashTextBoxNormal2.isNumeric)
							{
								ashTextBoxNormal2.Format = CommonSettings.strCurencyFormat;
							}
						}
					}
				}
				else
				{
					if (!(control5 is TabControl))
					{
						continue;
					}
					TabControl tabControl = (TabControl)control5;
					foreach (TabPage tabPage in tabControl.TabPages)
					{
						foreach (Control control7 in tabPage.Controls)
						{
							if (control7 is AshTextBoxNormal)
							{
								AshTextBoxNormal ashTextBoxNormal3 = (AshTextBoxNormal)control7;
								if (ashTextBoxNormal3.isNumeric)
								{
									ashTextBoxNormal3.Format = CommonSettings.strCurencyFormat;
								}
							}
							else
							{
								if (!(control7 is Panel))
								{
									continue;
								}
								Panel panel2 = (Panel)control7;
								foreach (Control control8 in panel2.Controls)
								{
									if (control8 is AshTextBoxNormal)
									{
										AshTextBoxNormal ashTextBoxNormal4 = (AshTextBoxNormal)control8;
										if (ashTextBoxNormal4.isNumeric)
										{
											ashTextBoxNormal4.Format = CommonSettings.strCurencyFormat;
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}
}
