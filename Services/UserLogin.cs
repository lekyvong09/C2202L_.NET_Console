using System;
using console.Entities;
using console.UI;

namespace console.Services
{
	public class UserLogin : IUserLogin
	{
        private IDataService _dataService;

        public UserLogin(IDataService dataService) {
            _dataService = dataService;
        }

        public bool CheckUserCardNumAndPassword(long cardNumber, int cardPin) {
            bool isLoginSuccess = false;

            Utility.PrintingDotAnimation("Verifying");
            Console.Clear();

            //Console.WriteLine($"Check number of account {userAccounts.Count()}");

            /// LINQ method
            //UserAccount account = userAccounts
            //    .Where(element => element.CardNumber == cardNumber)
            //    .FirstOrDefault();

            /// LINQ query
            UserAccount account = (from a in _dataService.UserAccounts
                                   where a.CardNumber == cardNumber
                                   select a)
                                   .FirstOrDefault();
            // UserAccount account = userAccounts.Find(element => element.CardNumber == cardNumber);

            if (account != null) {
                account.TotalLogin++;

                if (account.IsLocked) {
                    Utility.Alertify("\nYour account is locked. Please contact our nearest office.\n", false);
                    Environment.Exit(1); /// force to exit Application
                }

                if (account.CardPin == cardPin) {
                    isLoginSuccess = true;
                    _dataService.CurrentActiveUser = account;
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

