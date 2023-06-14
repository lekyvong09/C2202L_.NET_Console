using System.Text;
using console.Constant;
using console.Entities;
using console.Services;
using console.UI;

namespace console;
class Program
{
    
    static void Main(string[] args)
    {
        long cardNumber;
        int cardPin;
        bool loginSuccess = false;
        UserLogin userLogin = new UserLogin();
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

            Console.WriteLine($"Welcome back, {userLogin.CurrentActiveUser.FullName}");
            Utility.PressEnterToContinue();


            AppScreen.DisplayAppMenu();
            selectedAppMenu = AppScreen.ProcessMenuOption();

            switch (selectedAppMenu)
            {
                case (int)AppMenu.AccountBalance:
                    Console.WriteLine("Checking account number");
                    break;
                case (int)AppMenu.CashDeposit:
                    Console.WriteLine("Deposit");
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

            Utility.Alertify($"\nYour selected option is {selectedAppMenu}", true);
        }
        
    }

}

