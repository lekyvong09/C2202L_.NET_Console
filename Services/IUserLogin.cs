using System;
namespace console.Services
{
	public interface IUserLogin
	{
        bool CheckUserCardNumAndPassword(long cardNumber, int cardPin);

    }
}

