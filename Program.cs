using console.UI;

namespace console;
class Program
{
    static void Main(string[] args)
    {
        //AppScreen appScreen = new AppScreen();
        AppScreen.Welcome();

        string input = Utility.GetUserInput("your card number");

        // "Your card number is " + input
        Console.WriteLine($"Your card number is {input}");

        Utility.PressEnterToContinue();
    }
}

