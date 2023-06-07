using System;
namespace console.UI
{
	public class Utility
	{
        public static void PressEnterToContinue()
        {
            Console.WriteLine("\n\nPress Enter to continue...");
            Console.ReadLine();
        }

        public static string GetUserInput(string prompt)
        {
            Console.WriteLine($"Enter {prompt}");
            return Console.ReadLine();
        }

        public static void Alertify(string msg, bool success) {
            if (success) {
                Console.ForegroundColor = ConsoleColor.Blue;
            } else {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine(msg);
            Console.ResetColor();
        }
    }

}

