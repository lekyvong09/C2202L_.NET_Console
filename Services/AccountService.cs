using System;
using System.Globalization;
using console.UI;

namespace console.Services
{
	public class AccountService : IAccountService
    {
        private readonly IDataService _dataService;

        public AccountService(IDataService data)
		{
            _dataService = data;
		}

        public decimal CheckBalance()
        {
            /// return balance
            var account = _dataService.CurrentActiveUser;
            return account.AccountBalance;
        }

        public bool PlaceDeposit(int amount)
        {
            int fiftyDollarNoteCount;
            int twentyDollarNoteCount;

            /// Divide: 100 / 50 = 20
            /// Remainder: 100 % 50 = 0
            /// Remainder: 120 % 50 = 20

            /// Case 1: chia cho 50 va so du con lai chia het cho 20 (so tien: $150, $170)
            /// (Amount % 50) % 20 == 0 ==> 120 % 50 = 20 => 20 % 20 = 0
            if ((amount % 50) % 20 == 0)
            {
                fiftyDollarNoteCount = amount / 50;
                twentyDollarNoteCount = (amount % 50) / 20;
            } else
            {
                /// Case 2: (Amount % 50) % 20 = 10
                /// 130 % 50 % 20 = 10 && (130 / 50) > 1
                if ((amount % 50) % 20 == 10 && (amount/50) > 0)
                {
                    fiftyDollarNoteCount = amount / 50 - 1;
                    twentyDollarNoteCount = ((amount % 50) + 50) / 20;
                } else  /// Case 3: (Amount % 20)
                {
                    fiftyDollarNoteCount = 0;
                    twentyDollarNoteCount = amount / 20;
                }
            }

            Console.WriteLine("\nSummary");
            Console.WriteLine("----------");
            Console.WriteLine($"$50  X  {fiftyDollarNoteCount}  =  {fiftyDollarNoteCount * 50}");
            Console.WriteLine($"$20  X  {twentyDollarNoteCount}  =  {twentyDollarNoteCount * 20}");
            Console.WriteLine($"Total US Dollars: {amount.ToString("C", new CultureInfo("en-US"))}");

            /// confirm
            string confirmInput = Utility.GetUserInput("Press y then Enter to confirm");

            if (confirmInput != "y") {
                Utility.Alertify("\nYou have cancelled the request", false);
                return false;
            }

            _dataService.Deposit(amount);
            return true;
        }
    }
}

