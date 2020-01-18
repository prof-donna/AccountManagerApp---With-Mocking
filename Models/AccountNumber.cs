namespace Models
{
    using System;

    public class AccountNumber
    {
        // current definition of an AccountNumber
        private const int Length = 5;

        public int Value { set; get; }



        public AccountNumber()
        {
            // TODO: auto-generate account number

            Value = -999; //not implemented - TODO

            // TODO: what happens if no more account numbers are available?? (edge case)


        }

        public AccountNumber(int number)
        {
            // TODO: accept an account number, but must validate (and throw exception)
            Value = number;
        }

        protected void GenerateAccountNumber()
        {
            // TODO: auto-generate (and set) a valid account number
        }

        public static Boolean IsValidAccountNumber(int number)
        {
            // TODO: check if account is valid format (assuming 5 digit, as is hard-coded in length variable

            if (number > 9999 && number < 100000)
                return true;

            return false;

        }

        public static Boolean IsValidAccountNumber(int number, int length)
        {
            // TODO: check if account is valid format, based on the provided int length limit <DONE>

            if (length > 0)
            {
                try
                {
                    string minimumNumber = "1"; // "1" + length-1 0s
                    for (int i = 0; i < length - 1; i++)
                        minimumNumber += "0";

                    string maximumNumber = ""; // "9" for length times
                    for (int i = 0; i < length; i++)
                        maximumNumber += "9";

                    if (number > int.Parse(minimumNumber) - 1 && number < int.Parse(maximumNumber) + 1)
                        return true;
                }
                catch (OverflowException)
                {
                    // TODO: what happens if the provided length is bigger than can be supported???  <DONE>

                    throw;

                    //throw new OverflowException();
                }

            }

            return false;
        }

        public static Boolean IsValidAccountNumber(int number, int minimumLength, int maximumLength)
        {
            // TODO: check if account is valid format, based on the provided int length max and min limit



            return false;
        }

        public static Boolean IsUniqueAccountNumber(int number)
        {
            // TODO: check if account is unique/already in the system

            return false;

        }


    }

    public class DuplicateAccountNumberException : Exception
    {

    }

}

