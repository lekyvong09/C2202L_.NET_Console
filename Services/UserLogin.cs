using System;
using console.Entities;
using console.UI;

namespace console.Services
{
	public class UserLogin
	{

        private List<UserAccount> userAccounts;

        public UserLogin() {
            InitializeData();
        }

        void InitializeData() {
            userAccounts = new List<UserAccount> {
                new UserAccount
                { Id = 1, FullName = "Ray", AccountNumber = 123456, CardNumber = 654321, CardPin = 111111, AccountBalance = 10000, IsLocked = false },
                new UserAccount
                { Id = 2, FullName = "User 2", AccountNumber = 234567, CardNumber = 765432, CardPin = 222222, AccountBalance = 20000, IsLocked = false },
                new UserAccount
                { Id = 3, FullName = "User 3", AccountNumber = 345678, CardNumber = 876543, CardPin = 333333, AccountBalance = 30000, IsLocked = false }
            };
        }

        public bool CheckUserCardNumAndPassword(long cardNumber, int cardPin) {
            bool isLoginSuccess = false;

            /// verifying....
            Console.WriteLine("\nVerifying...");
            for (int i = 0; i < 10; i++) {
                Console.Write(".");
                Thread.Sleep(200);
            }
            Console.Clear();

            //Console.WriteLine($"Check number of account {userAccounts.Count()}");
            
            userAccounts.Where(element => element.CardNumber == cardNumber).FirstOrDefault();
            UserAccount account = userAccounts.Find(element => element.CardNumber == cardNumber);

            if (account != null) {
                account.TotalLogin++;

                if (account.IsLocked) {
                    Utility.Alertify("\nYour account is locked. Please contact our nearest office.\n", false);
                    Environment.Exit(1); /// force to exit Application
                }

                if (account.CardPin == cardPin) {
                    isLoginSuccess = true;
                    account.TotalLogin = 0;
                } else {
                    if (account.TotalLogin > 3) {
                        account.IsLocked = true;
                        Utility.Alertify("\nYour account is locked. Please contact our nearest office.\n", false);
                        Environment.Exit(1); /// force to exit Application
                    } else {
                        Utility.Alertify("\nInvalid card number or PIN! \n", false);
                    }
                }
            } else {
                Utility.Alertify("\nInvalid card number or PIN! \n", false);
            }

            return isLoginSuccess;
        }


        
	}
}

