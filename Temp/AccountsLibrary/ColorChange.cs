using System;
using System.Windows.Forms;
using AccountsLibrary.Properties;
using AccountsPro.SubProjects.AccountsLibraryFramework;
using AccountsPro.SubProjects.AccountsUserControls;

namespace AccountsLibrary
{
	public class ColorChange
	{
		public static void Color(Form form)
		{
			try
			{
				foreach (Control control3 in form.Controls)
				{
					if (control3 is Button)
					{
						Button button = (Button)control3;
						if (button.Name != "btnEnd")
						{
							button.BackColor = Settings.Default.BackColor;
							button.ForeColor = Settings.Default.ForeColor;
						}
					}
					else if (control3 is DataGridView)
					{
						DataGridView dataGridView = (DataGridView)control3;
						dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Settings.Default.BackColor;
						dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Settings.Default.ForeColor;
					}
					else if (control3 is AshDataGridview)
					{
						AshDataGridview ashDataGridview = (AshDataGridview)control3;
						ashDataGridview.ColumnHeadersDefaultCellStyle.BackColor = Settings.Default.BackColor;
						ashDataGridview.ColumnHeadersDefaultCellStyle.ForeColor = Settings.Default.ForeColor;
					}
					else if (control3 is Panel)
					{
						Panel panel = (Panel)control3;
						if (panel.Name != "ashGradientPanel1" && panel.Name != "pnlMaster")
						{
							Panel(panel);
						}
						else if (panel.Name == "ashGradientPanel1")
						{
							AshGradientPanel ashGradientPanel = (AshGradientPanel)control3;
							ashGradientPanel.BottomColor = Settings.Default.BackColor;
							ashGradientPanel.TopColor = Settings.Default.ForeColor;
							ashGradientPanel.BackColor = Settings.Default.BackColor;
						}
						else
						{
							MasterGradientPanel masterGradientPanel = (MasterGradientPanel)control3;
							masterGradientPanel.BottomColor = Settings.Default.BackColor;
							masterGradientPanel.TopColor = Settings.Default.ForeColor;
							masterGradientPanel.BackColor = Settings.Default.BackColor;
						}
					}
					else if (control3 is TabControl)
					{
						TabControl tabControl = (TabControl)control3;
						foreach (TabPage tabPage in tabControl.TabPages)
						{
							foreach (Control control4 in tabPage.Controls)
							{
								if (control4 is Button)
								{
									Button button2 = (Button)control4;
									if (button2.Name != "btnEnd")
									{
										button2.BackColor = Settings.Default.BackColor;
										button2.ForeColor = Settings.Default.ForeColor;
									}
								}
								else if (control4 is Panel)
								{
									Panel panel2 = (Panel)control4;
									if (panel2.Name != "ashGradientPanel1")
									{
										Panel(panel2);
									}
								}
								else if (control4 is GroupBox)
								{
									GroupBox pnl = (GroupBox)control4;
									GroupBox(pnl);
								}
							}
						}
					}
					else if (control3 is GroupBox)
					{
						GroupBox pnl2 = (GroupBox)control3;
						GroupBox(pnl2);
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static void Panel(Panel pnl)
		{
			try
			{
				foreach (Control control in pnl.Controls)
				{
					if (control is Button)
					{
						Button button = (Button)control;
						if (button.Name != "btnEnd")
						{
							button.BackColor = Settings.Default.BackColor;
							button.ForeColor = Settings.Default.ForeColor;
						}
					}
					else if (control is DataGridView)
					{
						DataGridView dataGridView = (DataGridView)control;
						dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Settings.Default.BackColor;
						dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Settings.Default.ForeColor;
					}
					else if (control is AshDataGridview)
					{
						AshDataGridview ashDataGridview = (AshDataGridview)control;
						ashDataGridview.ColumnHeadersDefaultCellStyle.BackColor = Settings.Default.BackColor;
						ashDataGridview.ColumnHeadersDefaultCellStyle.ForeColor = Settings.Default.ForeColor;
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static void GroupBox(GroupBox pnl)
		{
			try
			{
				foreach (Control control in pnl.Controls)
				{
					if (control is Button)
					{
						Button button = (Button)control;
						if (button.Name != "btnEnd")
						{
							button.BackColor = Settings.Default.BackColor;
							button.ForeColor = Settings.Default.ForeColor;
						}
					}
					else if (control is DataGridView)
					{
						DataGridView dataGridView = (DataGridView)control;
						dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Settings.Default.BackColor;
						dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Settings.Default.ForeColor;
					}
					else if (control is AshDataGridview)
					{
						AshDataGridview ashDataGridview = (AshDataGridview)control;
						ashDataGridview.ColumnHeadersDefaultCellStyle.BackColor = Settings.Default.BackColor;
						ashDataGridview.ColumnHeadersDefaultCellStyle.ForeColor = Settings.Default.ForeColor;
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
