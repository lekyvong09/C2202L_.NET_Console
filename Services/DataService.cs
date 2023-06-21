using System;
using console.Constant;
using console.Entities;

namespace console.Services
{
	public class DataService : IDataService /// implement
	{

        public List<UserAccount> UserAccounts { get; set; }
        public UserAccount CurrentActiveUser { get; set; }
        public List<Transaction> TransactionList { get; set; }


        public DataService()
		{
            InitializeData();
        }

        void InitializeData()
        {
            UserAccounts = new List<UserAccount> {
                new UserAccount
                { Id = 1, FullName = "Ray", AccountNumber = 123456, CardNumber = 654321, CardPin = 111111, AccountBalance = 10000, IsLocked = false },
                new UserAccount
                { Id = 2, FullName = "User 2", AccountNumber = 234567, CardNumber = 765432, CardPin = 222222, AccountBalance = 20000, IsLocked = false },
                new UserAccount
                { Id = 3, FullName = "User 3", AccountNumber = 345678, CardNumber = 876543, CardPin = 333333, AccountBalance = 30000, IsLocked = true }
            };
            TransactionList = new List<Transaction>
            {
                new Transaction {TransactionId = 6, UserBankAccountNumber = 123456, TransactionDate = DateTime.Now.AddDays(-10).AddHours(-6), TransactionType = TransactionType.Deposit, Description = "deposit from ATM number 123", TransactionAmount = 100},
                new Transaction {TransactionId = 4, UserBankAccountNumber = 123456, TransactionDate = DateTime.Now.AddDays(-1).AddHours(-3), TransactionType = TransactionType.Withdrawal, Description = "withdraw from ATM number 123", TransactionAmount = 100},
                new Transaction {TransactionId = 5, UserBankAccountNumber = 234567, TransactionDate = DateTime.Now.AddDays(-5).AddHours(-2), TransactionType = TransactionType.Transfer, Description = "transfer from ATM number 123", TransactionAmount = 100, TargetBankAccountNumber = 345678}
            };
        }

        public void Deposit(int amount)
        {
            CurrentActiveUser.AccountBalance += amount;

            /// add transaction record
            var transaction = new Transaction();
            transaction.TransactionId = GetSeqTransactionId();
            transaction.UserBankAccountNumber = CurrentActiveUser.AccountNumber;
            transaction.TransactionDate = DateTime.Now;
            transaction.TransactionType = TransactionType.Deposit;
            transaction.Description = $"deposit at ATM1 on {DateTime.Now}";
            transaction.TransactionAmount = amount;

            TransactionList.Add(transaction);
        }

        public void Withdrawal(int amount)
        {
            CurrentActiveUser.AccountBalance -= amount;

            /// add transaction record
            var transaction = new Transaction();
            transaction.TransactionId = GetSeqTransactionId();
            transaction.UserBankAccountNumber = CurrentActiveUser.AccountNumber;
            transaction.TransactionDate = DateTime.Now;
            transaction.TransactionType = TransactionType.Withdrawal;
            transaction.Description = $"withdrawal at ATM1 on {DateTime.Now}";
            transaction.TransactionAmount = amount;

            TransactionList.Add(transaction);
        }

        public void InternalTransferSourceAccount(int amount, long targetBankAccountNumber)
        {
            var transaction = new Transaction();
            transaction.TransactionId = GetSeqTransactionId();
            transaction.UserBankAccountNumber = CurrentActiveUser.AccountNumber;
            transaction.TransactionDate = DateTime.Now;
            transaction.TransactionType = TransactionType.Transfer;
            transaction.Description = $"transfer to account {targetBankAccountNumber} on {DateTime.Now}";
            transaction.TransactionAmount = amount;
            transaction.TargetBankAccountNumber = targetBankAccountNumber;

            TransactionList.Add(transaction);
            CurrentActiveUser.AccountBalance -= amount;
        }

        public void InternalTransferTargetAccount(int amount, long targetBankAccountNumber)
        {
            var transaction = new Transaction();
            transaction.TransactionId = GetSeqTransactionId();
            transaction.UserBankAccountNumber = targetBankAccountNumber;
            transaction.TransactionDate = DateTime.Now;
            transaction.TransactionType = TransactionType.Transfer;
            transaction.Description = $"receive from account {targetBankAccountNumber} on {DateTime.Now}";
            transaction.TransactionAmount = amount;
            transaction.TargetBankAccountNumber = CurrentActiveUser.AccountNumber;

            TransactionList.Add(transaction);
            var targetAccount =
                UserAccounts.Where(i => i.AccountNumber == targetBankAccountNumber).FirstOrDefault();

            targetAccount.AccountBalance += amount;
        }

        private long GetSeqTransactionId()
        {
            long maxTransactionId = TransactionList.OrderByDescending(i => i.TransactionId)
                .Select(i => i.TransactionId)
                .First();
            return maxTransactionId + 1;
        }
    }
}

