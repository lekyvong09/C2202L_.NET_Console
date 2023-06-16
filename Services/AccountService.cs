using System;
namespace console.Services
{
	public class AccountService : IAccountService
    {
        private readonly IDataService _dataService;

        public AccountService(IDataService data)
		{
            _dataService = data;
		}

        public decimal CheckBalance()
        {
            /// return balance
            var account = _dataService.CurrentActiveUser;
            return account.AccountBalance;
        }
    }
}

