using System.Text;
using console.Entities;
using console.Services;
using console.UI;

namespace console;
class Program
{
    
    static void Main(string[] args)
    {
        long cardNumber = 0;
        int cardPin = 0;
        bool loginSuccess = false;
        UserLogin userLogin = new UserLogin();

        AppScreen.Welcome();
        while (loginSuccess == false) {

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


            Utility.Alertify($"Your card number is {cardNumber}", true);
            Utility.Alertify($"Your card pin is {inputPin}", true);

            loginSuccess = userLogin.CheckUserCardNumAndPassword(cardNumber, cardPin);

            if (!loginSuccess) {
                cardNumber = 0;
                cardPin = 0;
            }
        }


        Console.WriteLine($"Welcome back, {userLogin.CurrentActiveUser.FullName}");
        Utility.PressEnterToContinue();

        /// welcome user message....
        /// deposit -> balance + amount
    }
}

