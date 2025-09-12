namespace AccountsLibrary
{
	public class UserPrevilegeDtl
	{
		public int? Id { get; set; }

		public int? FK_UserPrevilegeHdrId { get; set; }

		public int? FK_MenuDetailsId { get; set; }

		public bool Visible { get; set; }

		public bool Enable { get; set; }

		public bool SaveData { get; set; }

		public bool EditData { get; set; }

		public bool DeleteData { get; set; }
	}
}
