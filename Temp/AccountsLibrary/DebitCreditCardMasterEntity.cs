namespace AccountsLibrary
{
	public class DebitCreditCardMasterEntity
	{
		public int? CardId { get; set; }

		public string CardName { get; set; }

		public decimal BankComm { get; set; }

		public decimal CardComm { get; set; }

		public string Remarks { get; set; }
	}
}
