using System;

namespace Ashpro
{
	public class SerchEntity
	{
		public int Id { get; set; }

		public int InvoiceNo { get; set; }

		public DateTime InvDate { get; set; }

		public int PartyAccntId { get; set; }

		public string PartyName { get; set; }

		public decimal InvAmount { get; set; }
	}
}
