using System;
using console.Entities;

namespace console.Services
{
	public interface IDataService
	{
        List<UserAccount> UserAccounts { get; set; }
        UserAccount CurrentActiveUser { get; set; }
    }
}

