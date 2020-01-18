using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace AccountManager_Library
{
    public class FinancialDecisions
    {
        public FinancialDecisions()
        {
        }

        public enum Decision
        {
            Approved,
            Denied,
            Inconclusive
        }

        public Decision AssessLoanQualifications(Account account, double loanAmount)
        {
            // approval rule: savings (collateral) balance of at least 1000
            if (account.Balance > 999 && account.Type.Equals("Savings"))
                return Decision.Approved;

            // approval rule: checking balance > 0.5 * loanAmount
            if (account.Balance > (0.5 * loanAmount) && account.Type.Equals("Checking"))
                return Decision.Approved;

            // denial rule: savings balance < 500
            if (account.Balance < 500 && account.Type.Equals("Savings"))
                return Decision.Denied;

            // denial rule: checking balance < 100
            if (account.Balance < 100 && account.Type.Equals("Checking"))
                return Decision.Denied;


            return Decision.Inconclusive;
        }


        public Boolean TransactionExceedsOverdraftLimit(Account account, double withdrawalAmount, double overdraftLimit)
        {
            if (account.Balance - withdrawalAmount < -overdraftLimit)
                return true;

            return false;

        }

    }
}
