using System.Globalization;
using System.Text;
using console.Constant;
using console.Entities;
using console.Services;
using console.UI;
using Microsoft.Extensions.DependencyInjection;
using ConsoleTables;

namespace console;
class Program
{
    
    static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddScoped<IDataService, DataService>()
            .AddScoped<IUserLogin, UserLogin>()
            .AddScoped<IAccountService, AccountService>()
            .BuildServiceProvider();

        long cardNumber;
        int cardPin;
        bool loginSuccess = false;
        IUserLogin userLogin = serviceProvider.GetRequiredService<IUserLogin>();
        IDataService dataService = serviceProvider.GetRequiredService<IDataService>();
        IAccountService accountService = serviceProvider.GetRequiredService<IAccountService>();

        int selectedAppMenu = (int)AppMenu.Logout;

        while(selectedAppMenu == (int)AppMenu.Logout)
        {
            AppScreen.Welcome();
            while (loginSuccess == false)
            {
                cardNumber = AppScreen.InputCardNumberScreen();
                cardPin = AppScreen.InputCardPinScreen();

                Utility.Alertify($"Your card number is {cardNumber}", true);
                Utility.Alertify($"Your card pin is {cardPin}", true);

                loginSuccess = userLogin.CheckUserCardNumAndPassword(cardNumber, cardPin);
            }

            Console.WriteLine($"Welcome back, {dataService.CurrentActiveUser.FullName}");
            Utility.PressEnterToContinue();

            selectedAppMenu = (int)AppMenu.AccountBalance;
            while (selectedAppMenu > 0 && selectedAppMenu < 6)
            {
                AppScreen.DisplayAppMenu();
                selectedAppMenu = AppScreen.ProcessMenuOption();

                switch (selectedAppMenu)
                {
                    case (int)AppMenu.AccountBalance:
                        Console.WriteLine($"Your account balance {accountService.CheckBalance().ToString("C", new CultureInfo("en-US"))}");
                        Utility.PressEnterToContinue();
                        break;
                    case (int)AppMenu.CashDeposit:
                        int amount = AppScreen.GetInputAmountForDeposit();
                        bool depositSuccess = accountService.PlaceDeposit(amount);
                        if (depositSuccess) {
                            Utility.Alertify("\nDeposit successful.", true);
                        } else {
                            Utility.Alertify("\nInvalid input. Please try again", false);
                        }
                        Utility.PressEnterToContinue();
                        break;
                    case (int)AppMenu.Withdrawal:
                        int withdrawalAmount = AppScreen.GetWithdrawAmount();
                        if (withdrawalAmount > dataService.CurrentActiveUser.AccountBalance) {
                            Utility.Alertify("\nYour balance is not enough", false);
                        } else {
                            dataService.Withdrawal(withdrawalAmount);
                            Utility.Alertify("\nPlease get your cash at the tray", true);
                        }
                        Utility.PressEnterToContinue();
                        break;
                    case (int)AppMenu.Transfer:
                        Console.WriteLine("Internal Transfer");
                        long targetAccountNumber = 0;
                        while (targetAccountNumber == 0) {
                            string numberInput = Utility.GetUserInput("Input target account:");
                            try {
                                var tempAccountNumber = Int64.Parse(numberInput);
                                bool existTargetAccount =
                                    dataService.UserAccounts.Any(cus => cus.AccountNumber == tempAccountNumber
                                                 && cus.AccountNumber != dataService.CurrentActiveUser.AccountNumber);
                                if (existTargetAccount) {
                                    targetAccountNumber = tempAccountNumber;
                                } else {
                                    Utility.Alertify("\nWrong target account", false);
                                    continue;
                                }
                            } catch (FormatException) {
                                Utility.Alertify("\nWrong target account", false);
                                continue;
                            }
                        }

                        /// transfer
                        int transferAmount = AppScreen.GetTransferAmount();
                        dataService.InternalTransferSourceAccount(transferAmount, targetAccountNumber);
                        dataService.InternalTransferTargetAccount(transferAmount, targetAccountNumber);
                        break;
                    case (int)AppMenu.Transactions:
                        Console.WriteLine("Viewing Transactions...");
                        List<Transaction> transactions =
                            dataService.TransactionList
                            .Where(t => t.UserBankAccountNumber == dataService.CurrentActiveUser.AccountNumber)
                            .ToList();
                        if (transactions.Count < 1) {
                            Utility.Alertify("\nThere is no transaction yet", false);
                        } else {
                            var table = new ConsoleTable("Id", "Transaction Date", "Type"
                                , "Description", "Amount");
                            foreach (var tran in transactions) {
                                table.AddRow(
                                    tran.TransactionId,
                                    tran.TransactionDate,
                                    tran.TransactionType,
                                    tran.Description,
                                    tran.TransactionAmount.ToString("C", new CultureInfo("en-US")));
                            }
                            table.Options.EnableCount = false;
                            table.Write();
                            Console.WriteLine($"You have {transactions.Count} transactions");
                        }
                        Utility.PressEnterToContinue();
                        break;
                    case (int)AppMenu.Logout:
                        Utility.PrintingDotAnimation("Logging out");
                        loginSuccess = false;
                        Console.Clear();
                        break;
                    default:
                        Utility.Alertify($"\n Invalid option", false);
                        break;
                }
            }

            // Utility.Alertify($"\nYour selected option is {selectedAppMenu}", true);
        }
        
    }

}

