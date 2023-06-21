using System;
using console.Entities;

namespace console.Services
{
	public interface IDataService
	{
        List<Transaction> TransactionList { get; set; }
        List<UserAccount> UserAccounts { get; set; }
        UserAccount CurrentActiveUser { get; set; }
        void Deposit(int amount);
        void Withdrawal(int amount);
        void InternalTransferSourceAccount(int amount, long targetBankAccountNumber);
        void InternalTransferTargetAccount(int amount, long targetBankAccountNumber);
    }
}

