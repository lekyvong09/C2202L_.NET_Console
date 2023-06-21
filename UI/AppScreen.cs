using System;
using System.Globalization;
using System.Text;
using console.Constant;

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

        public static long InputCardNumberScreen()
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


        public static int InputCardPinScreen()
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


        public static void DisplayAppMenu() {
            Console.Clear();
            Console.WriteLine("---------App Menu---------");
            Console.WriteLine("1. Account Balance       :");
            Console.WriteLine("2. Cash Deposit          :");
            Console.WriteLine("3. Withdrawal            :");
            Console.WriteLine("4. Transfer              :");
            Console.WriteLine("5. Transtraction         :");
            Console.WriteLine("6. Logout                :");
        }


        public static int ProcessMenuOption()
        {
            int selectedNumber = 0;
            while (selectedNumber < 1 || selectedNumber > 6)
            {
                string numberInput = Utility.GetUserInput("Input option");
                try {
                    selectedNumber = Int32.Parse(numberInput);
                    continue;
                } catch (FormatException e)
                {
                    Utility.Alertify("Invalid input. " + e.Message, false);
                }
                DisplayAppMenu();
            }

            

            return selectedNumber;
        }

        public static int GetInputAmountForDeposit()
        {
            Console.WriteLine("\nOnly multiples of 20 and 50 are allowed");

            int amount = 0;
            while (amount == 0)
            {
                string amountInput = Utility.GetUserInput("Input deposit amount:");
                try
                {
                    var t = Int32.Parse(amountInput);

                    if (!((t % 50) % 20 == 0 ||
                        ((t % 50) % 20 == 10 && (t / 50) > 0) ||
                        (t % 20) == 0
                        ))
                    {
                        Utility.Alertify("\nInvalid Input", false);
                        continue;
                    }
                    amount = t;
                }
                catch (FormatException)
                {
                    Utility.Alertify("\nInvalid Input", false);
                    continue;
                }
            }

            return amount;
        }


        public static int GetWithdrawAmount()
        {
            /// display menu
            Console.WriteLine("\nSelect to Withdraw");
            Console.WriteLine($":1. {20.ToString("C", new CultureInfo("en-US"))}");
            Console.WriteLine($":2. {50.ToString("C", new CultureInfo("en-US"))}");
            Console.WriteLine($":3. {100.ToString("C", new CultureInfo("en-US"))}");
            Console.WriteLine($":4. {200.ToString("C", new CultureInfo("en-US"))}");
            Console.WriteLine($":5. {500.ToString("C", new CultureInfo("en-US"))}");
            Console.WriteLine($":6. {1000.ToString("C", new CultureInfo("en-US"))}");
            Console.WriteLine($":7. {2000.ToString("C", new CultureInfo("en-US"))}");
            Console.WriteLine($":8. {5000.ToString("C", new CultureInfo("en-US"))}");
            Console.WriteLine(":9. Input your own amount\n");

            /// user input option
            int option = 0;
            while (option < 1 || option > 9)
            {
                string numberInput = Utility.GetUserInput("Input option:");
                try {
                    option = Int32.Parse(numberInput);
                } catch (FormatException) {
                    Utility.Alertify("Invalid input", false);
                }
            }

            switch (option)
            {
                case 1:
                    return 20;
                case 2:
                    return 50;
                case 3:
                    return 100;
                case 4:
                    return 200;
                case 5:
                    return 500;
                case 6:
                    return 1000;
                case 7:
                    return 2000;
                case 8:
                    return 5000;
                default:
                    int inputAmount = 0;
                    while (inputAmount == 0)
                    {
                        string numberInput = Utility.GetUserInput("Input amount");
                        try {
                            inputAmount = Int32.Parse(numberInput);
                            if (!(inputAmount % 20 == 0 || inputAmount % 50 == 0)) {
                                Utility.Alertify("Invalid input. Must be divisible to $20 or $50", false);
                                inputAmount = 0;
                                continue;
                            }
                            if (inputAmount < 1 || inputAmount > 5000) {
                                Utility.Alertify("Invalid input. Must be positive and less than $5000", false);
                                inputAmount = 0;
                                continue;
                            }
                        } catch (FormatException) {
                            Utility.Alertify("Invalid input. Please try again", false);
                        }
                    }
                    return inputAmount;
            }

            /// check user input amount
            ///

        }

        public static int GetTransferAmount()
        {
            int amount = 0;
            while (amount == 0)
            {
                string amountInput = Utility.GetUserInput("Input amount:");
                try
                {
                    var t = Int32.Parse(amountInput);

                    if (t > 0) {
                        amount = t;
                    } else {
                        continue;
                    }
                }
                catch (FormatException)
                {
                    Utility.Alertify("\nInvalid Input. Please try again", false);
                    continue;
                }
            }

            return amount;
        }
    }
        
}

