using System.Text;
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
        int selectedNumber = AppScreen.ProcessMenuOption();

        Utility.Alertify($"\nYour selected option is {selectedNumber}", true);
    }



}

