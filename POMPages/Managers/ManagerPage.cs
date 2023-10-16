using NuGet.Frameworks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        private IWebDriver driver;
        private WebDriverWait wait;

        public ManagerPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void ClickAddCustomer()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//body/div[1]/div[1]/div[2]/div[1]/div[1]/button[1]")));
            _btnAddCust.Click();
        }
        public void ClickCustomers()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//body/div[1]/div[1]/div[2]/div[1]/div[1]/button[3]")));
            _btnCustomersList.Click();
        }
        public void InputFName(string fName)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//input[@placeholder = 'First Name']")));
            _inputFName.SendKeys(fName);
        }
        public void InputLName(string lName)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//input[@placeholder = 'Last Name']")));
            _inputLName.SendKeys(lName);
        }
        public void InputPCode(string pCode)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//input[@placeholder = 'Post Code']")));
            _inputPCode.SendKeys(pCode);
        }
        public void ClickSubmit()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//button[@type= 'submit']")));
            _btnSubmit.Click();
        }
        public void SearchCustomer(string name)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//input[@placeholder= 'Search Customer']")));
            _inputSearch.SendKeys(name);
        }
        public bool CheckCustomerExists(string fName, string lName)
        {
            string fullName = fName + " " + lName;
            bool result = false;
            foreach (var row in _tableRows)
            {
                if (row.Text.Contains(fName) && row.Text.Contains(lName))
                {
                    Console.WriteLine("Customer " + fullName + " exists");
                    result = true;
                }
                else
                {
                    Console.WriteLine("Customer " + fullName + " does not exist");
                    result = false;
                }
            }
            return result;
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
