using System;

namespace Ashpro
{
	public class AttenSettings
	{
		public int? AttenSettingsId { get; set; }

		public int? AttendanceMasterId { get; set; }

		public int? EmpID { get; set; }

		public bool Morning { get; set; }

		public bool Evening { get; set; }

		public decimal AttenType { get; set; }

		public string EmpName { get; set; }

		public DateTime AtteDate { get; set; }
	}
}
