using System;
using AccountManager_Library;
using Models;

// must add reference to the class library

namespace AccountManagerApp
{
    class Program
    {
        static void Main()
        {
            AccountActions system = new AccountActions();

            Console.WriteLine("Hello there! This is better than that other example...\n");

            Console.WriteLine(system.DisplayRequestedAccount(100));

            Console.ReadLine();

        }
    }
}