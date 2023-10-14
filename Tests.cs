using TechnicalAssesment.POMPages;
using IronXL;
using System.Data;
using System.Formats.Asn1;
using System.Globalization;
using System;
using CsvHelper;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using TechnicalAssesment.POMPages.Managers;
using TechnicalAssesment.POMPages.Customers;

namespace TechnicalAssesment
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Question1()
        {
            string[] _fName = {"Jackson", "Christopher" };
            string[] _lName = { "Frank", "Connely" };

            BasePage.driver.Navigate().GoToUrl("https://www.globalsqa.com/angularJs-protractor/BankingProject/#/login");
            
            LoginPage loginPage= new LoginPage();
            ManagerPage managerPage = new ManagerPage();

            loginPage.bankMgrLogin();

            // Change fileLocation to your local path
            string fileLocation = "C:\\Users\\andre\\source\\repos\\TechnicalAssesment\\CsvFiles\\Customers.csv";
            using (var reader = new StreamReader(fileLocation))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Customer>();
                foreach (var customer in records)
                {
                    managerPage.ClickAddCustomer();
                    managerPage.InputFName(customer.FirstName);
                    managerPage.InputLName(customer.LastName);
                    managerPage.InputPCode(customer.PostCode);
                    managerPage.ClickSubmit();
                    BasePage.AcceptAlert();
                    managerPage.ClickCustomers();
                    managerPage.SearchCustomer(customer.FirstName);
                    managerPage.CheckCustomerExists(customer.FirstName, customer.LastName);
                }
            }
            managerPage.ClickCustomers();
            for (int i = 0; i< _fName.Length; i++)
            {
                managerPage.SearchAndDeleteCustomer(_fName[i], _lName[i]);
            }
        }

        [Test]
        public void Question2()
        {
            BasePage.driver.Navigate().GoToUrl("https://www.globalsqa.com/angularJs-protractor/BankingProject/#/login");

            LoginPage loginPage = new LoginPage();
            CustomersPage customersPage = new CustomersPage();
            AccountPage accountPage = new AccountPage();

            loginPage.loginCust();
            customersPage.SelectDropDownUser("Hermoine Granger");
            customersPage.clickLogin();
            accountPage.SelectDropDownAccount("1003");
            
            int balanceFromExcel = 0;
            int balanceFromAccount = 0;

            // Change fileLocation to your local path
            string fileLocation = "C:\\Users\\andre\\source\\repos\\TechnicalAssesment\\CsvFiles\\Transactions.csv";
            using (var reader = new StreamReader(fileLocation))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Transactions>();
                foreach (var transaction in records)
                {
                    if (transaction.TranscationType == "Credit")
                    {
                        accountPage.ClickDeposit();
                        accountPage.InputAmount(transaction.Amount);
                        balanceFromExcel = balanceFromExcel + int.Parse(transaction.Amount);
                        accountPage.ClickSubmit();
                        balanceFromAccount = accountPage.GetBalance();
                        Console.WriteLine("Total Excel Balance:" + balanceFromExcel + "|| Total Account Balance:" + balanceFromAccount);
                    }
                    else if (transaction.TranscationType == "Debit")
                    {
                        accountPage.ClickWithdrawl();
                        accountPage.InputAmount(transaction.Amount);
                        balanceFromExcel = balanceFromExcel - int.Parse(transaction.Amount);
                        accountPage.ClickSubmit();
                        balanceFromAccount = accountPage.GetBalance();
                        Console.WriteLine("Total Excel Balance:" + balanceFromExcel + "|| Total Account Balance:" + balanceFromAccount);
                    }
                }
                Assert.AreEqual(balanceFromExcel, balanceFromAccount);
            }
        }
        [TearDown]
        public void tearDown()
        {
            BasePage.driver.Close();
            BasePage.driver.Quit();
        }
    }
}