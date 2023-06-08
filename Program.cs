using console.Entities;
using console.UI;

namespace console;
class Program
{
    private List<UserAccount> userAccounts;

    void InitializeData() {
        userAccounts = new List<UserAccount> {
            new UserAccount
            {
                Id = 1,
                FullName = "Ray",
                AccountNumber = 123456,
                CardNumber = 654321,
                CardPin = 111111,
                AccountBalance = 10000,
                IsLocked = false
            },
            new UserAccount
            {
                Id = 2,
                FullName = "User 2",
                AccountNumber = 234567,
                CardNumber = 765432,
                CardPin = 222222,
                AccountBalance = 20000,
                IsLocked = false
            },
            new UserAccount
            {
                Id = 3,
                FullName = "User 3",
                AccountNumber = 345678,
                CardNumber = 876543,
                CardPin = 333333,
                AccountBalance = 30000,
                IsLocked = false
            }
        };
    }


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

