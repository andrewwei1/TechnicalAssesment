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
using RestSharp;
using Newtonsoft.Json;
using TechnicalAssesment.Model;

namespace TechnicalAssesment
{
    public class Tests
    {
        IWebDriver driver;
        LoginPage loginPage;
        ManagerPage managerPage;
        BasePage basePage;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void Question1()
        {
            string[] _fName = { "Jackson", "Christopher" };
            string[] _lName = { "Frank", "Connely" };

            driver.Navigate().GoToUrl("https://www.globalsqa.com/angularJs-protractor/BankingProject/#/login");

            loginPage = new LoginPage(driver);
            managerPage = new ManagerPage(driver);
            basePage = new BasePage(driver);

            loginPage.bankMgrLogin();

            // Change fileLocation to your local path
            string fileLocation = "C:\\Users\\andre\\source\\repos\\TechnicalAssesment\\CsvFiles\\Customers.csv";
            using (var reader = new StreamReader(fileLocation))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<CustomerModel>();
                foreach (var customer in records)
                {
                    managerPage.ClickAddCustomer();
                    managerPage.InputFName(customer.FirstName);
                    managerPage.InputLName(customer.LastName);
                    managerPage.InputPCode(customer.PostCode);
                    managerPage.ClickSubmit();
                    basePage.AcceptAlert();
                    managerPage.ClickCustomers();
                    managerPage.SearchCustomer(customer.FirstName);
                    Assert.IsTrue(managerPage.CheckCustomerExists(customer.FirstName, customer.LastName));
                }
            }
            managerPage.ClickCustomers();
            for (int i = 0; i < _fName.Length; i++)
            {
                managerPage.SearchAndDeleteCustomer(_fName[i], _lName[i]);
            }
        }

        [TestCase ("Hermoine Granger", "1003")]
        public void Question2(string custName, string accNum)
        {
            driver.Navigate().GoToUrl("https://www.globalsqa.com/angularJs-protractor/BankingProject/#/login");

            LoginPage loginPage = new LoginPage(driver);
            CustomersPage customersPage = new CustomersPage(driver);
            AccountPage accountPage = new AccountPage(driver);

            loginPage.loginCust();
            customersPage.SelectDropDownUser(custName);
            customersPage.clickLogin();
            accountPage.SelectDropDownAccount(accNum);

            int balanceFromExcel = 0;
            int balanceFromAccount = 0;

            // Change fileLocation to your local path
            string fileLocation = "C:\\Users\\andre\\source\\repos\\TechnicalAssesment\\CsvFiles\\Transactions.csv";
            using (var reader = new StreamReader(fileLocation))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<TransactionModel>();
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
                    Assert.That(balanceFromAccount, Is.EqualTo(balanceFromExcel));
                }
                
            }
        }

        [TestCase ("username", "BruceWayne","12345", "ClarkKent")]
        public void Question3(string reqParam, string userName,string password, string newUserName)
        {
            RestClient client = new RestClient("https://petstore3.swagger.io/");
            //Flow 1: CreateUser
            Login(client,userName, "12345");
            CreateUser(client);
            //Flow 2: GetUserByName
            string initialUsername = ReadUser(client, reqParam, userName);
            //Flow 3: UpdateUser
            string updatedUserName = UpdateUsername(client, reqParam, initialUsername, newUserName);
            //Flow 4: ReadUpdatedUser
            Assert.IsTrue(updatedUserName == initialUsername, "The username has been updated from " + initialUsername + " to " + updatedUserName + " succesfully");
            //Flow 5: DeleteUser
            DeleteUser(client, reqParam, updatedUserName);
            //Flow 6: ReadDeletedUser
            ReadUser(client, reqParam, updatedUserName);

        }
        public void Login(RestClient client, string userName, string password)
        {
            var request = new RestRequest("#/user/loginUser", Method.Get);
            request.AddParameter("username", userName);
            request.AddParameter("password", password);
            RestResponse response = client.Execute(request);

            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
        }

        public void CreateUser(RestClient client)
        {
            var newUser = new
            {
                id = 10,
                username = "BruceWayne",
                firstName = "Bruce",
                lastName = "Wayne",
                email = "bruce@email.com",
                password = "12345",
                phone = "12345",
                userStatus = 1
            };
            var request = new RestRequest("#/user/createUser", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddBody(newUser);
            RestResponse response = client.Execute(request);
            Console.WriteLine("Request URL: " + client.BuildUri(request)); // Log the full URL being used
            Console.WriteLine("Response Content: " + response.Content); // Log the response content


            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK, "The error code is: " + (int)response.StatusCode + response.StatusCode);
        }

        public string ReadUser(RestClient client,string reqParam,string username)
        {
            var reqGetUser = new RestRequest("#/user/getUserByName", Method.Get);
            reqGetUser.AddParameter(reqParam, username);
            RestResponse resGetUser = client.Execute(reqGetUser);

            // Deserialize the response content into an object
            UserModel initialResponseObj = JsonConvert.DeserializeObject<UserModel>(resGetUser.Content);

            switch ((int)resGetUser.StatusCode)
            {
                case 200:
                    Console.WriteLine("The user exists.");
                    break;
                case 400:
                    Console.WriteLine("Invalid username provided.");
                    break;
                case 404:
                    Console.WriteLine("User not found.");
                    break;
            }
            return initialResponseObj.username;
        }

        public string UpdateUsername(RestClient client, string reqParam, string userName, string newUserName)
        {
            // Update the username
            var updateUsername = new
            {
                username = newUserName,
            };
            var reqUpdateUser = new RestRequest("#/user/updateUser", Method.Put);
            reqUpdateUser.AddParameter(reqParam, userName);
            reqUpdateUser.AddBody(updateUsername);
            RestResponse resUpdateUser = client.Execute(reqUpdateUser);

            Assert.IsTrue(resUpdateUser.StatusCode == System.Net.HttpStatusCode.OK, "The error code is: " + (int)resUpdateUser.StatusCode + resUpdateUser.StatusCode);
            // Deserialize the response content into an object
            UserModel updateResponseObj = JsonConvert.DeserializeObject<UserModel>(resUpdateUser.Content);
            return updateResponseObj.username;

        }
        public void DeleteUser(RestClient client, string reqParam, string userName)
        {
            var reqDeleteUser = new RestRequest("#/user/deleteUser", Method.Delete);
            reqDeleteUser.AddParameter(reqParam, userName);
            RestResponse resDeleteUser = client.Execute(reqDeleteUser);

            Assert.IsTrue(resDeleteUser.StatusCode == System.Net.HttpStatusCode.OK, "The error code is: " + (int)resDeleteUser.StatusCode + resDeleteUser.StatusCode);
        }

        [TearDown]
        public void tearDown()
        {
            driver.Close();
            driver.Quit();
        }
    }
}