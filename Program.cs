using console.UI;

namespace console;
class Program
{
    static void Main(string[] args)
    {
        long cardNumber = 0;

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

        // "Your card number is " + input
        Utility.Alertify($"Your card number is {cardNumber}", true);

        Utility.PressEnterToContinue();
    }
}

