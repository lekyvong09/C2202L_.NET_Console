using System;
namespace console.Services
{
	public interface IAccountService
	{
		decimal CheckBalance();
		bool PlaceDeposit(int amount);
	}
}

