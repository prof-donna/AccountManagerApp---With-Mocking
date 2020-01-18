using System.Collections.Generic;
using Models;

namespace AccountManager_DAL
{
    public interface IDataAccessLayer
    {
        Account AddAccount(Account account);
        List<Account> GetAccountByNumber(int number);
        List<AccountNumber> GetAllAccountNumbers();
        List<AccountNumber> GetListOfAccountNumbers(int number);
        bool UpdateBalanceForAccount(int number, double newAmount);
    }
}