using System;
namespace console.UI
{
    public static class AppScreen
    {
        public static void Welcome()
        {
            Console.Title = "My Console App";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Clear();

            Console.WriteLine("\n\n------Welcome to my console app!-----");
            Console.WriteLine("\nPlease insert your card");
            Utility.PressEnterToContinue();
        }

    }
        
}

