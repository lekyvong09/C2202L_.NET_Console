using System;
using console.Constant;

namespace console.Entities
{
	public class Transaction
	{
		public long TransactionId { get; set; }
		public long UserBankAccountNumber { get; set; }
		public DateTime TransactionDate { get; set; }
		public string Description { get; set; }
		public decimal TransactionAmount { get; set; }
		public long TargetBankAccountNumber { get; set; } = 0;
		public TransactionType TransactionType { get; set; }
	}
}

