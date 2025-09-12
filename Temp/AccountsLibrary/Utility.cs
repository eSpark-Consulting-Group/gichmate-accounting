using System.Collections.Generic;
using System.Data;

namespace AccountsLibrary
{
	public static class Utility
	{
		public static int iReportFormNumber { get; set; }

		public static string uLicenceNumber { get; set; }

		public static List<string> uMacId { get; set; }

		public static string uManufacturer { get; set; }

		public static int? uPrevilageId { get; set; }

		public static int uVersion { get; set; }

		public static int uResolutionWidth { get; set; }

		public static int uResolutionHeight { get; set; }

		public static string uSpecMacId { get; set; }

		public static string uTerminal { get; set; }

		public static int uSerialNumber { get; set; }

		public static DataTable uDtProducts { get; set; }

		public static MasterValue gbValues { get; set; }

		public static MasterValue pbValues { get; set; }

		public static bool uIsSalesCounter { get; set; }

		public static string uCounter { get; set; }

		public static int uCounterCashAccountId { get; set; }

		public static int iBranchId { get; set; }
	}
}
