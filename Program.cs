using System.Globalization;
using System.Text;
using console.Constant;
using console.Entities;
using console.Services;
using console.UI;
using Microsoft.Extensions.DependencyInjection;

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
                        Console.WriteLine("\nOnly multiples of 20 and 50 are allowed");

                        int amount = 0;
                        while(amount == 0)
                        {
                            string amountInput = Utility.GetUserInput("Input deposit amount:");
                            try
                            {
                                var t = Int32.Parse(amountInput);

                                if (!((t % 50) % 20 == 0 ||
                                    ((t % 50) % 20 == 10 && (t / 50) > 0) ||
                                    (t % 20) == 0
                                    ))
                                {
                                    Utility.Alertify("\nInvalid Input", false);
                                    continue;
                                }
                                amount = t;
                            }
                            catch (FormatException)
                            {
                                Utility.Alertify("\nInvalid Input", false);
                                continue;
                            }
                        }

                        bool depositSuccess = accountService.PlaceDeposit(amount);
                        if (depositSuccess) {
                            Utility.Alertify("\nDeposit successful.", true);
                        } else {
                            Utility.Alertify("\nInvalid input. Please try again", false);
                        }
                        Utility.PressEnterToContinue();
                        break;
                    case (int)AppMenu.Withdrawal:
                        Console.WriteLine("Withdrawal");
                        break;
                    case (int)AppMenu.Transfer:
                        Console.WriteLine("Transfer");
                        break;
                    case (int)AppMenu.Transactions:
                        Console.WriteLine("Transactions");
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

