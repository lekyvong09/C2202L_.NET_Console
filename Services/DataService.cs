﻿using System;
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
                new Transaction {TransactionId = 1, UserBankAccountNumber = 123456, TransactionDate = DateTime.Now.AddDays(-10).AddHours(-6), TransactionType = TransactionType.Deposit, Description = "deposit from ATM number 123", TransactionAmount = 100},
                new Transaction {TransactionId = 2, UserBankAccountNumber = 123456, TransactionDate = DateTime.Now.AddDays(-1).AddHours(-3), TransactionType = TransactionType.Withdrawal, Description = "withdraw from ATM number 123", TransactionAmount = 100},
                new Transaction {TransactionId = 3, UserBankAccountNumber = 234567, TransactionDate = DateTime.Now.AddDays(-5).AddHours(-2), TransactionType = TransactionType.Transfer, Description = "transfer from ATM number 123", TransactionAmount = 100, TargetBankAccountNumber = 345678}
            };
        }

        public void Deposit(int amount)
        {
            CurrentActiveUser.AccountBalance += amount;
        }
    }
}

