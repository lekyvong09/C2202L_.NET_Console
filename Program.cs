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
            cardNumber = InputCardNumberScreen();
            cardPin = InputCardPinScreen();

            Utility.Alertify($"Your card number is {cardNumber}", true);
            Utility.Alertify($"Your card pin is {cardPin}", true);

            loginSuccess = userLogin.CheckUserCardNumAndPassword(cardNumber, cardPin);

        }


        Console.WriteLine($"Welcome back, {userLogin.CurrentActiveUser.FullName}");
        Utility.PressEnterToContinue();

        /// welcome user message....
        /// deposit -> balance + amount
    }

    private static long InputCardNumberScreen()
    {
        long cardNumber = 0;
        while (cardNumber == 0)
        {
            string input = Utility.GetUserInput("your card number");
            try
            {
                cardNumber = long.Parse(input);
            }
            catch (FormatException e)
            {
                /// handle exception -- stop || display message
                Utility.Alertify(e.Message, false);
            }
        }

        return cardNumber;
    }


    private static int InputCardPinScreen()
    {
        int cardPin = 0;
        Console.WriteLine("\n\nPress Enter your PIN to continue..");
        StringBuilder inputPin = new StringBuilder();

        while (cardPin == 0)
        {
            var inputKey = Console.ReadKey(true);

            if (inputKey.Key == ConsoleKey.Enter)
            {
                if (inputPin.Length == 6)
                {
                    try
                    {
                        var temp = Int32.Parse(inputPin.ToString());
                        if (temp > 0)
                        {
                            cardPin = temp;
                            break;
                        }
                        else
                        {
                            Utility.Alertify("\nMust be 6 digits. Please try again", false);
                            inputPin.Clear(); // reset inputPin = "";
                            continue;
                        }
                    }
                    catch (FormatException e)
                    {
                        Utility.Alertify(e.Message, false);
                        inputPin.Clear(); // reset inputPin = "";
                        continue;
                    }
                }
                else
                {
                    Utility.Alertify("\nMust be 6 digits. Please try again", false);
                    inputPin.Clear(); // reset inputPin = "";
                    continue;
                }
            }

            if (inputKey.Key == ConsoleKey.Backspace && inputPin.Length > 0)
            {
                inputPin.Remove(inputPin.Length - 1, 1);
                Console.Write("\b \b");
            }
            else
            {
                //inputPin = inputPin + inputKey.KeyChar;
                inputPin.Append(inputKey.KeyChar);
                Console.Write("*");
            }
        }

        return cardPin;
    }
}

