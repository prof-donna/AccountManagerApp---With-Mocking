using NUnit.Framework;
using AccountManager_DAL;
using AccountManager_Library;
using System.Collections.Generic;
using Models;
using Moq;      // NOTE: Must add the NuGet "Moq" package, and add using statement to any test file that uses Mocks


namespace TestExamplesUsingMoq
{
    public class UnitTestsWithRealMocks
    {
        
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetRequestedAccount_AccountFound()
        {
            //Arrange
            // (set up mock data)
            List<Account> expectedList = new List<Account>();
            Account expectedAccount = new Account(1126){Balance=1111.11,Type = "Checking"};
            expectedList.Add(expectedAccount);

            // (create mock object and define its behaviour)
            var mockDataAccessLayer = new Mock<IDataAccessLayer>();
            mockDataAccessLayer.Setup(dal => dal.GetAccountByNumber(It.IsAny<int>())).Returns(expectedList);

            // (create the REAL object that is under test)
            var processorUnderTest = new AccountProcessor(mockDataAccessLayer.Object); // NOTE: This is where we pass in the MOCKED data access layer object

            //Act
            Account actualAccount = processorUnderTest.GetRequestedAccount(expectedAccount.Number);     // NOTE: The method we're testing

            //mockDataAccessLayer.Verify(x => x.GetAccountByNumber(expectedAccount.Number));  // TODO: Figure out the purpose of this code

            //Assert
            Assert.AreEqual(expectedAccount.Number,actualAccount.Number);
            Assert.AreEqual(expectedAccount.Balance, actualAccount.Balance);
            Assert.AreEqual(expectedAccount.Type, actualAccount.Type);


        }


        [Test]
        public void GetRequestedAccount_AccountNotFound()
        {
            //Arrange
            // (mock data)
            List<Account> expectedList = null;
            int requestedAccountNumber = 1126;

            // (mock behaviour)
            var mockDataAccessLayer = new Mock<IDataAccessLayer>();
            mockDataAccessLayer.Setup(dal => dal.GetAccountByNumber(It.IsAny<int>())).Returns(expectedList);

            // (piece under test)
            var processorUnderTest = new AccountProcessor(mockDataAccessLayer.Object);

            //Act
            Account actualAccount = processorUnderTest.GetRequestedAccount(requestedAccountNumber);

            //Assert
            Assert.IsNull(actualAccount);


        }


        [Test]
        public void GetRequestedAccount_TooManyAccountsReturned_ThrowsException()
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
            var processorUnderTest = new AccountProcessor(mockDataAccessLayer.Object);


            //Act & Assert
            Assert.Throws<DuplicateAccountNumberException>(() => processorUnderTest.GetRequestedAccount(requestedAccountNumber), "Multiple records with same account number were returned.");

        }
    }
}