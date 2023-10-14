using NuGet.Frameworks;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalAssesment.POMPages.Managers
{
    class ManagerPage
    {
        [FindsBy(How = How.XPath, Using = "//body/div[1]/div[1]/div[2]/div[1]/div[1]/button[1]")]
        IWebElement _btnAddCust;

        [FindsBy(How = How.XPath, Using = "//body/div[1]/div[1]/div[2]/div[1]/div[1]/button[3]")]
        IWebElement _btnCustomersList;

        [FindsBy(How = How.XPath, Using = "//input[@placeholder = 'First Name']")]
        IWebElement _inputFName;

        [FindsBy(How = How.XPath, Using = "//input[@placeholder = 'Last Name']")]
        IWebElement _inputLName;

        [FindsBy(How = How.XPath, Using = "//input[@placeholder = 'Post Code']")]
        IWebElement _inputPCode;

        [FindsBy(How = How.XPath, Using = "//button[@type= 'submit']")]
        IWebElement _btnSubmit;

        [FindsBy(How = How.XPath, Using = "//input[@placeholder= 'Search Customer']")]
        IWebElement _inputSearch;

        [FindsBy(How = How.XPath, Using = "//body/div[1]/div[1]/div[2]/div[1]/div[2]/div[1]/div[1]/table[1]")]
        IList<IWebElement> _tableRows { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Delete')]")]
        IWebElement _btnDelete;

        public ManagerPage()
        {
            PageFactory.InitElements(BasePage.driver, this);
        }

        public void ClickAddCustomer()
        {
            BasePage.setImplicitWait(1);
            _btnAddCust.Click();
        }
        public void ClickCustomers()
        {
            BasePage.setImplicitWait(1);
            _btnCustomersList.Click();
        }
        public void InputFName(string fName)
        {
            _inputFName.SendKeys(fName);
        }
        public void InputLName(string lName)
        {
            _inputLName.SendKeys(lName);
        }
        public void InputPCode(string pCode)
        {
            _inputPCode.SendKeys(pCode);
        }
        public void ClickSubmit()
        {
            _btnSubmit.Click();
        }
        public void SearchCustomer(string name)
        {
            _inputSearch.SendKeys(name);
        }
        public void CheckCustomerExists(string fName, string lName)
        {
            string fullName = fName + " " + lName;
            foreach (var row in _tableRows)
            {
                if (row.Text.Contains(fName) && row.Text.Contains(lName))
                {
                    Console.WriteLine("Customer '" + fullName + "' exists");
                }
                else
                {
                    Console.WriteLine("Customer '" + fullName + "' does not exist");
                }
            }
        }
        public void SearchAndDeleteCustomer(string fName, string lName)
        {
            _inputSearch.Clear();
            SearchCustomer(fName);
            string fullName = fName + " " + lName;
            foreach (var row in _tableRows)
            {
                if (row.Text.Contains(fName) && row.Text.Contains(lName))
                {
                    row.FindElement(By.XPath("//button[contains(text(),'Delete')]")).Click();
                    CheckCustomerExists(fName, lName);
                }
            }
        }
    }
}
