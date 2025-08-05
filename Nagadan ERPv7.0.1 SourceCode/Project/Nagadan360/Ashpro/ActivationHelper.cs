using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ashpro
{
	public static class ActivationHelper
	{
		private static TimeZoneInfo Bangladesh_ZONE = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");

		public static void UpdateChecking()
		{
			if (AniHelper.CheckInternet())
			{
				try
				{
					string fileName = Application.StartupPath + "\\UpdationTool.exe";
					Process.Start(fileName);
				}
				catch (Exception ex)
				{
					Messages.ErrorMessage(ex.Message);
				}
			}
		}

		public static async Task UpdationTask()
		{
			if (Utility.uManufacturer != "DeskCloud ERP")
			{
				try
				{
					Task Updationtask = new Task(UpdateChecking);
					Updationtask.Start();
					await Updationtask;
				}
				catch (Exception ex)
				{
					Messages.ErrorMessage(ex.Message);
				}
			}
		}
	}
}
