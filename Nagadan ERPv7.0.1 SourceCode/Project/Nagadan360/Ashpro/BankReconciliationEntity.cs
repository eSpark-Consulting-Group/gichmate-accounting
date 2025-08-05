using System;

namespace Ashpro
{
	public class BankReconciliationEntity : TransactionLog
	{
		public int SL { get; set; }

		public DateTime BankDate { get; set; }

		public string Particulars { get; set; }
	}
}
