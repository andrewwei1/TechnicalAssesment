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

        private IWebDriver driver;
        private WebDriverWait wait;
        public CustomersPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            
        }
        public void SelectDropDownUser(string user)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("userSelect")));
            SelectElement select = new SelectElement(_dropdownUser);
            select.SelectByText(user);
        }
        public void clickLogin()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(text(),'Login')]")));
            _btnLogin.Click();
        }
    }
}
