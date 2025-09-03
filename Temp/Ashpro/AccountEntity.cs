namespace Ashpro
{
	public class AccountEntity
	{
		public int? AccountId { get; set; }

		public int? AGroupId { get; set; }

		public int? RootId { get; set; }

		public int? PCategoryId { get; set; }

		public string AccountName { get; set; }

		public decimal OpeningBalance { get; set; }

		public string DebitOrCridt { get; set; }

		public string AddressOne { get; set; }

		public string AddressTwo { get; set; }

		public string AddressThree { get; set; }

		public string PhoneOne { get; set; }

		public string MobileNo { get; set; }

		public string FaxNo { get; set; }

		public string TinNo { get; set; }

		public string CstNo { get; set; }

		public string EMailOne { get; set; }

		public string Remarks { get; set; }

		public string BankName { get; set; }

		public string BankAddress { get; set; }

		public string AccountNo { get; set; }

		public bool isActive { get; set; }

		public bool IsBuiltIn { get; set; }

		public decimal DebitLimit { get; set; }

		public bool IsLimitApplicable { get; set; }

		public string AddressFour { get; set; }

		public string FieldOne { get; set; }

		public string FieldTwo { get; set; }

		public string FieldThree { get; set; }

		public string FieldFour { get; set; }
	}
}
