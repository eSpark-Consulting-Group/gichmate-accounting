namespace Ashpro
{
	public class ReminderTB
	{
		public int? ReminderId { get; set; }

		public string RemainderHead { get; set; }

		public string Remainder { get; set; }

		public string CurrentDate { get; set; }

		public string ReminderDate { get; set; }

		public int? UserId { get; set; }

		public int? TargetUserId { get; set; }

		public int? FK_BranchId { get; set; }
	}
}
