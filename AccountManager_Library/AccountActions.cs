using AccountManager_DAL;
using Models;

namespace AccountManager_Library
{
    public class AccountActions
    {
        private AccountProcessor _processor;

        public AccountActions()
        {
            _processor = new AccountProcessor();
        }

        public AccountActions(IDataAccessLayer dal)     // NOTE:    In order to create a REAL AccountActions object will use a REAL AccountProcessor
                                                        //          with a MOCKED data access layer, we need a constructor that accepts an INTERFACE 
                                                        //          (which allows us to pass in an object that MOCKS the data access layer)
        {
            _processor = new AccountProcessor(dal);
        }

        public string DisplayRequestedAccount(int accountNumber)
        {

            Account account = _processor.GetRequestedAccount(accountNumber);

            if (account != null)
                return account.ToString();

            return "No accounts with number " + accountNumber + " were found.";
        }

        public Account DepositAmountToAccount(int accountNumber, double deposit)
        {
            if (deposit > 0.0)
            {
                Account account = _processor.GetRequestedAccount(accountNumber);

                if (account != null)
                {
                    double newBalance = account.Balance + deposit;
                    Account updatedAccount = _processor.DepositAmount(account, newBalance);
                    //TODO: any other checks to do here?
                    return updatedAccount;
                }
            }

            // TODO: throw invalid deposit exception?
            return null;
        }
    }
}
