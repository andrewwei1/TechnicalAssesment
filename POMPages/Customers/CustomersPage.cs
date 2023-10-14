using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalAssesment.POMPages.Customers
{
    class CustomersPage
    {
        [FindsBy(How = How.Id, Using = "userSelect")]
        IWebElement _dropdownUser;

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Login')]")]
        IWebElement _btnLogin;

        public CustomersPage()
        {
            PageFactory.InitElements(BasePage.driver, this);
        }
        public void SelectDropDownUser(string user)
        {
            Thread.Sleep(1000);
            SelectElement select = new SelectElement(_dropdownUser);
            select.SelectByText(user);
        }
        public void clickLogin()
        {
            Thread.Sleep(1000);
            _btnLogin.Click();
        }
    }
}
