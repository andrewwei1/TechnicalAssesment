using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalAssesment.POMPages
{
    class LoginPage
    {
        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Customer Login')]")]
        IWebElement _btnCustLogin;

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Bank Manager Login')]")]
        IWebElement _btnManagerLogin;
        
        private IWebDriver driver;
        private WebDriverWait wait;

        public LoginPage(IWebDriver driver)
        {   
            this.driver = driver;
            PageFactory.InitElements(driver, this);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
        public void loginCust()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(text(),'Customer Login')]")));
            _btnCustLogin.Click();
        }

        public void bankMgrLogin()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(text(),'Bank Manager Login')]")));
            _btnManagerLogin.Click();
        }
    }
}
