using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Models;

namespace AccountManager_DAL
{
    // NOTE: To be able to MOCK this object, it must be implementing an INTERFACE
    public class DataAccessLayer : IDataAccessLayer
    {
        // private DB operations

        private const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Donna\source\repos\AccountManagerApp\AccountManager_DAL\Database1.mdf;Integrated Security=True";

        private SqlConnection ConnectToDatabase()
        {
            var conn = new SqlConnection(ConnectionString);
            conn.Open();
            return conn;

        }

        private Boolean DisconnectFromDatabase(SqlConnection conn)
        {
            conn.Close();
            return true;
        }


        // domain-related calls to the database

        public Account AddAccount(Account account)
        {
            throw new NotImplementedException();
        }


        public List<Account> GetAccountByNumber(int number)
        {
            string query = "select * from Accounts where Number='" + number + "' order by Number asc";

            SqlConnection conn = ConnectToDatabase();
            List<Account> resultList = GetAccountFromDatabase(conn, query);
            DisconnectFromDatabase(conn);

            return resultList;
        }

        public List<AccountNumber> GetListOfAccountNumbers(int number)
        {
            string query = "select Number from Accounts order by Number asc";

            SqlConnection conn = ConnectToDatabase();
            List<AccountNumber> resultList = GetAllAccountNumbersFromDatabase(conn, query);
            DisconnectFromDatabase(conn);

            return resultList;
        }


        public List<AccountNumber> GetAllAccountNumbers()
        {
            throw new NotImplementedException();
        }


        public bool UpdateBalanceForAccount(int number, double newAmount)
        {
            throw new NotImplementedException();
        }




        private List<Account> GetAccountFromDatabase(SqlConnection conn, string query)
        {
            List<Account> resultList = new List<Account>();

            SqlCommand command = new SqlCommand
            {
                Connection = conn,
                CommandText = query
            };

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader.FieldCount == 3)
                        {
                            var accountNumber = int.Parse(reader.GetValue(0).ToString());
                            var balance = double.Parse(reader.GetValue(1).ToString());
                            var type = reader.GetValue(2).ToString();

                            var account = new Account(accountNumber)
                            {
                                Balance = balance,
                                Type = type
                            };

                            resultList.Add(account);
                        }
                        else
                        {
                            throw new AccountDataIntegrityException();
                        }
                    }
                }
                else
                {
                    //Console.WriteLine("No rows found.");
                    reader.Close();
                    return null;
                }
                reader.Close();
            }

            return resultList;

        }





        private List<AccountNumber> GetAllAccountNumbersFromDatabase(SqlConnection conn, string query)
        {
            List<AccountNumber> listOfAccountNumbers = new List<AccountNumber>();

            SqlCommand command = new SqlCommand
            {
                Connection = conn,
                CommandText = query
            };

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var accountNumber = int.Parse(reader[0].ToString());
                        var number = new AccountNumber(accountNumber);
                        //temp.Balance = reader[1];
                        //temp.Type = reader[2];

                        listOfAccountNumbers.Add(number);

                    }
                }
                else
                {
                    //Console.WriteLine("No rows found.");
                    return null;
                }
                reader.Close();
            }

            return listOfAccountNumbers;

        }



    }
}

