using System.Text;
using console.Entities;
using console.UI;

namespace console;
class Program
{
    private List<UserAccount> userAccounts;

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


    static void Main(string[] args)
    {
        long cardNumber = 0;
        int cardPin = 0;

        AppScreen.Welcome();

        while (cardNumber == 0) {
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

        while (cardPin == 0) {
            var inputKey = Console.ReadKey(true);

            if (inputKey.Key == ConsoleKey.Enter)
            {
                if (inputPin.Length == 6) {
                    try {
                        var temp = Int32.Parse(inputPin.ToString());
                        if (temp > 0) {
                            cardPin = temp;
                            break;
                        }
                    } catch (FormatException e) {
                        Utility.Alertify(e.Message, false);
                        inputPin.Clear(); // reset inputPin = "";
                        continue;
                    }
                } else {
                    Utility.Alertify("\nMust be 6 digits. Please try again", false);
                    inputPin.Clear(); // reset inputPin = "";
                    continue;
                }
            }

            if (inputKey.Key == ConsoleKey.Backspace && inputPin.Length > 0) {
                inputPin.Remove(inputPin.Length - 1, 1);
                Console.Write("\b \b");
            } else {
                //inputPin = inputPin + inputKey.KeyChar;
                inputPin.Append(inputKey.KeyChar);
                Console.Write("*");
            }
        }
        

        Utility.Alertify($"Your card number is {cardNumber}", true);
        Utility.Alertify($"Your card pin is {inputPin}", true);

        Utility.PressEnterToContinue();
    }
}

