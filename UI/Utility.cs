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
    }

}

