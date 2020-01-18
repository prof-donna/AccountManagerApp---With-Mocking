using Models;

namespace AccountManager_Library
{
    public interface IAccountProcessor
    {
        Account DepositAmount(Account account, double newBalance);
        Account GetRequestedAccount(int accountNumber);
    }
}