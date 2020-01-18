using System.Collections.Generic;
using AccountManager_DAL;
using Models;

namespace AccountManager_Library
{
    // NOTE: To be able to MOCK this object, it must be implementing an INTERFACE

    public class AccountProcessor : IAccountProcessor
    {
        private readonly IDataAccessLayer _dataAccessLayer;     // NOTE:    In order to have the ability to choose whether or not to mock the data access layer
                                                                //          it must be defined by the INTERFACE

        public AccountProcessor()
        {
            _dataAccessLayer = new DataAccessLayer();
        }

        public AccountProcessor( IDataAccessLayer dal )     // NOTE:    In order to create a REAL AccountProcessor with a MOCKED data access layer
                                                            //          we need a constructor that accepts an INTERFACE (which allows us to pass in an
                                                            //          object that MOCKS the data access layer
        {
            _dataAccessLayer = dal;
        }

        public Account GetRequestedAccount(int accountNumber)
        {
            Account account = null;

            List<Account> resultsList = _dataAccessLayer.GetAccountByNumber(accountNumber);

            if (resultsList == null)
                return null;

            switch (resultsList.Count)
            {
                case 0:
                    break;
                case 1:
                {
                    foreach (var anAccount in resultsList)
                    {
                        account = anAccount;
                    }
                    break;
                }
                default:
                {
                    throw new DuplicateAccountNumberException();
                }
            }

            return account;

        }


        public Account DepositAmount(Account account, double newBalance)
        {

            if (_dataAccessLayer.UpdateBalanceForAccount(account.Number, newBalance))
            {
                return GetRequestedAccount(account.Number);
            }

            // TODO: what would be the reason for this query to fail? what do we do about it?
            return null;
        }



    }
}