using System.Windows.Forms;
using AshproMessage;

namespace Ashpro
{
	public static class Messages
	{
		public static void InformationMessage(string strMsg)
		{
			MessageBox.Show(strMsg, Utility.uManufacturer, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		public static void WarningMessage(string strMsg)
		{
			MessageBox.Show(strMsg, Utility.uManufacturer, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		public static void WarningMessage()
		{
			MessageBox.Show(MessageKeys.WarningMessage, Utility.uManufacturer, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		public static void BuiltinMessage()
		{
			MessageBox.Show(MessageKeys.BuiltinMessage, Utility.uManufacturer, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		public static void ErrorMessage(string strMsg)
		{
			MessageBox.Show(strMsg, Utility.uManufacturer, MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}

		public static void SavedMessage()
		{
			MessageBox.Show(MessageKeys.SavedMessage, Utility.uManufacturer, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		public static void UpdateMessage()
		{
			MessageBox.Show(MessageKeys.UpdateMessage, Utility.uManufacturer, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		public static void DeleteMessage()
		{
			MessageBox.Show(MessageKeys.DeleteMessage, Utility.uManufacturer, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		public static void ReferenceExistsMessage()
		{
			MessageBox.Show(MessageKeys.ReferenceExistsMessage, Utility.uManufacturer, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		public static void ReferenceExistsMessageForUpdate()
		{
			MessageBox.Show(MessageKeys.ReferenceExistsMessageForUpdate, Utility.uManufacturer, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		public static bool DeleteConfirmationMessage()
		{
			if (MessageBox.Show(MessageKeys.DeleteConfirmationMessage, Utility.uManufacturer, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				return true;
			}
			return false;
		}

		public static bool DialogMessage(string Msg, bool isYesisDefault = true)
		{
			if (isYesisDefault)
			{
				if (MessageBox.Show(Msg, Utility.uManufacturer, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
				{
					return true;
				}
				return false;
			}
			if (MessageBox.Show(Msg, Utility.uManufacturer, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
			{
				return true;
			}
			return false;
		}

		public static void CloseConfirmationMessage(Form frm)
		{
			if (MessageBox.Show(MessageKeys.CloseConfirmationMessage, Utility.uManufacturer, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				frm.Close();
			}
		}
	}
}
