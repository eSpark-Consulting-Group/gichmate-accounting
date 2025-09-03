#define TRACE
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace AshUsercontrol
{
	public class AshDataGridview : DataGridView
	{
		private DataGridViewComboBoxEditingControl cb;

		private int WM_LBUTTONDOWN = 513;

		private int WM_LBUTTONDBLCLK = 515;

		private int MK_LBUTTON = 1;

		private IContainer components = null;

		public Keys LastKey { get; set; }

		public bool EnterKeyNavigation { get; set; }

		public AshDataGridview()
		{
			DoubleBuffered = true;
			InitializeComponent();
			base.DefaultCellStyle.SelectionBackColor = Color.LightPink;
		}

		protected override void InitLayout()
		{
			base.InitLayout();
			base.EnableHeadersVisualStyles = false;
			base.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(224, 224, 224);
		}

		protected override void OnDataError(bool displayErrorDialogIfNoHandler, DataGridViewDataErrorEventArgs e)
		{
			if (e.Exception is FormatException)
			{
				base.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
				base.Rows[e.RowIndex].ErrorText = "Invalid input for column " + base.Columns[e.ColumnIndex].HeaderText;
				e.ThrowException = false;
			}
			else
			{
				base.OnDataError(displayErrorDialogIfNoHandler, e);
			}
		}

		protected override void OnCellValidating(DataGridViewCellValidatingEventArgs e)
		{
			base.Rows[e.RowIndex].ErrorText = "";
			base.OnCellValidating(e);
		}

		protected override void OnEditingControlShowing(DataGridViewEditingControlShowingEventArgs e)
		{
			base.OnEditingControlShowing(e);
			if (e.Control is DataGridViewComboBoxEditingControl)
			{
				cb = (DataGridViewComboBoxEditingControl)e.Control;
				if (cb == null)
				{
				}
			}
		}

		protected override void OnCellEnter(DataGridViewCellEventArgs e)
		{
			base.OnCellEnter(e);
			base.CurrentCell.Style.BackColor = Color.LightPink;
			if (EnterKeyNavigation && base[e.ColumnIndex, e.RowIndex].ReadOnly)
			{
				SendKeys.Send("{TAB}");
			}
		}

		protected override void OnCellLeave(DataGridViewCellEventArgs e)
		{
			base.CurrentCell.Style.BackColor = Color.White;
		}

		protected override bool ProcessDialogKey(Keys keyData)
		{
			if (base.Rows.Count > 0)
			{
				Keys keys = keyData & Keys.KeyCode;
				if (keys == Keys.Up || keys == Keys.Down)
				{
					return base.ProcessDialogKey(keyData);
				}
				switch (keys)
				{
				case Keys.Left:
				{
					int num = base.CurrentCell.ColumnIndex - 1;
					while (num >= 0 && (base.Columns[num].ReadOnly || !base.Columns[num].Visible))
					{
						num--;
					}
					if (num < 0)
					{
						return true;
					}
					try
					{
						base.CurrentCell = base.Rows[base.CurrentCell.RowIndex].Cells[num];
					}
					catch
					{
					}
					return true;
				}
				default:
					if (keys != Keys.Return)
					{
						return base.ProcessDialogKey(keyData);
					}
					goto case Keys.Tab;
				case Keys.Tab:
				case Keys.Right:
				{
					Trace.WriteLine("on dilog key");
					if (keys == Keys.Return)
					{
						string name = base.CurrentCell.OwningColumn.Name;
						if (name == "col_Code")
						{
							return false;
						}
					}
					int i;
					for (i = base.CurrentCell.ColumnIndex + 1; i < base.Columns.Count && (base.Columns[i].ReadOnly || !base.Columns[i].Visible); i++)
					{
					}
					if (i < base.Columns.Count)
					{
						if (i < 0)
						{
							return true;
						}
						try
						{
							base.CurrentCell = base.Rows[base.CurrentCell.RowIndex].Cells[i];
						}
						catch
						{
						}
					}
					else
					{
						if (base.CurrentCell.RowIndex == base.Rows.Count - 1)
						{
							return base.ProcessDialogKey(keyData);
						}
						for (i = 0; i <= base.CurrentCell.ColumnIndex && (base.Columns[i].ReadOnly || !base.Columns[i].Visible); i++)
						{
						}
						if (i <= base.CurrentCell.ColumnIndex)
						{
							if (i < 0)
							{
								return true;
							}
							try
							{
								base.CurrentCell = base.Rows[base.CurrentCell.RowIndex + 1].Cells[i];
							}
							catch
							{
							}
						}
					}
					return true;
				}
				}
			}
			return false;
		}

		protected override bool ProcessDataGridViewKey(KeyEventArgs e)
		{
			try
			{
				if (base.Rows.Count > 0)
				{
					if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
					{
						return base.ProcessDataGridViewKey(e);
					}
					if (e.KeyCode == Keys.Left)
					{
						int num = base.CurrentCell.ColumnIndex - 1;
						while (num >= 0 && (base.Columns[num].ReadOnly || !base.Columns[num].Visible))
						{
							num--;
						}
						if (num < 0)
						{
							return true;
						}
						try
						{
							base.CurrentCell = base.Rows[base.CurrentCell.RowIndex].Cells[num];
						}
						catch
						{
						}
						return true;
					}
					if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Return || e.KeyData == Keys.Tab)
					{
						Trace.WriteLine("on datagridview key");
						if (e.KeyCode == Keys.Return)
						{
							string name = base.CurrentCell.OwningColumn.Name;
							if (name == "col_Code")
							{
								return false;
							}
						}
						int i;
						for (i = base.CurrentCell.ColumnIndex + 1; i < base.Columns.Count && (base.Columns[i].ReadOnly || !base.Columns[i].Visible); i++)
						{
						}
						if (i < base.Columns.Count)
						{
							if (i < 0)
							{
								return true;
							}
							try
							{
								base.CurrentCell = base.Rows[base.CurrentCell.RowIndex].Cells[i];
							}
							catch
							{
							}
						}
						else
						{
							if (base.CurrentCell.RowIndex == base.Rows.Count - 1)
							{
								return base.ProcessDataGridViewKey(e);
							}
							for (i = 0; i <= base.CurrentCell.ColumnIndex && (base.Columns[i].ReadOnly || !base.Columns[i].Visible); i++)
							{
							}
							if (i <= base.CurrentCell.ColumnIndex)
							{
								if (i < 0)
								{
									return true;
								}
								try
								{
									base.CurrentCell = base.Rows[base.CurrentCell.RowIndex + 1].Cells[i];
								}
								catch
								{
								}
							}
						}
						return true;
					}
				}
			}
			catch (Exception)
			{
			}
			return base.ProcessDataGridViewKey(e);
		}

		protected override void WndProc(ref Message m)
		{
			try
			{
				if (m.Msg == WM_LBUTTONDOWN || m.Msg == WM_LBUTTONDBLCLK)
				{
					if (m.WParam.ToInt32() == MK_LBUTTON)
					{
						int num = m.LParam.ToInt32();
						int num2 = num & 0xFFFF;
						int num3 = num >> 16;
						base.WndProc(ref m);
					}
				}
				else
				{
					base.WndProc(ref m);
				}
			}
			catch (Exception)
			{
				base.WndProc(ref m);
			}
		}

		private bool IsReadonlyCell(int xpos, int ypos)
		{
			try
			{
				int i;
				for (i = 0; i < base.ColumnCount && !GetColumnDisplayRectangle(i, cutOverflow: true).Contains(xpos, ypos); i++)
				{
				}
				if (i < base.ColumnCount)
				{
					if (base.Columns[i].ReadOnly)
					{
						return true;
					}
					return false;
				}
				return false;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public void MoveForward()
		{
			SendKeys.Send("{Tab}");
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
			components = new System.ComponentModel.Container();
		}
	}
}
