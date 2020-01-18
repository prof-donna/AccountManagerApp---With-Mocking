using System;
using NUnit.Framework;
using AccountManager_DAL;
using AccountManager_Library;
using System.Collections.Generic;
using Models;
using Moq;


namespace TestExamplesUsingMoq
{
    public class IntegrationTestsWithRealMocks
    {
        
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DisplayRequestedAccount_AccountFound()
        {
            //Arrange
            // (mock data)
            List<Account> expectedList = new List<Account>();
            Account expectedAccount = new Account(1126){Balance=1111.11,Type = "Checking"};
            expectedList.Add(expectedAccount);

            // (mock behaviour)
            var mockDataAccessLayer = new Mock<IDataAccessLayer>();
            mockDataAccessLayer.Setup(dal => dal.GetAccountByNumber(It.IsAny<int>())).Returns(expectedList);

            // (piece under test)
            AccountActions actions = new AccountActions(mockDataAccessLayer.Object);


            //Act
            String actualDisplay = actions.DisplayRequestedAccount(expectedAccount.Number);

            //Assert
            Assert.AreEqual(expectedAccount.ToString(),actualDisplay);

        }


        [Test]
        public void DisplayRequestedAccount_AccountNotFound()
        {
            //Arrange
            // (mock data)
            List<Account> expectedList = null;
            int requestedAccountNumber = 1126;
            String expectedDisplay = "No accounts with number " + requestedAccountNumber + " were found.";

            // (mock behaviour)
            var mockDataAccessLayer = new Mock<IDataAccessLayer>();
            mockDataAccessLayer.Setup(dal => dal.GetAccountByNumber(It.IsAny<int>())).Returns(expectedList);

            // (piece under test)
            AccountActions actions = new AccountActions(mockDataAccessLayer.Object);


            //Act
            String actualDisplay = actions.DisplayRequestedAccount(requestedAccountNumber);

            //Assert
            Assert.AreEqual(expectedDisplay,actualDisplay);

        }


        [Test]
        public void DisplayRequestedAccount_TooManyAccountsReturned_ThrowsException()
        {
            //Arrange

            int requestedAccountNumber = 1126;

            // (mock data)
            List<Account> expectedList = new List<Account>();
            Account expectedAccount = new Account(1126) { Balance = 1111.11, Type = "Checking" };
            expectedList.Add(expectedAccount);
            expectedList.Add(expectedAccount); // important to add the same thing twice

            // (mock behaviour)
            var mockDataAccessLayer = new Mock<IDataAccessLayer>();
            mockDataAccessLayer.Setup(dal => dal.GetAccountByNumber(It.IsAny<int>())).Returns(expectedList);


            // (piece under test)
            AccountActions actions = new AccountActions(mockDataAccessLayer.Object);


            //Act & Assert
            Assert.Throws<DuplicateAccountNumberException>(() => actions.DisplayRequestedAccount(requestedAccountNumber), "Multiple records with same account number were returned.");

        }
    }
}