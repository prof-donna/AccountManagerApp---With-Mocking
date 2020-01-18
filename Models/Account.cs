using System;

namespace Models
{
    public class Account
    {
        public int Number { get; set; }
        public string Type { get; set; }
        public double Balance { get; set; }


        public Account()
        {
            var accountNumber = new AccountNumber();

            // TODO: generate number

            Number = accountNumber.Value;
            Balance = 0.0;
            Type = "Checking";
        }

        public Account(int number)
        {
            Number = number;
            Balance = 0.0;
            Type = "Checking";

            // TODO: checking here for an exception?

        }


        public override string ToString()
        {
            return "Account Number: " + Number + "\nAccount Balance: " + Balance + "\nAccount Type: " + Type + "\n";
        }
    }


    public class AccountDataIntegrityException : Exception
    {
    }
}