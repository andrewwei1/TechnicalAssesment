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

        public LoginPage()
        {
            PageFactory.InitElements(BasePage.driver, this);
        }
        public void loginCust()
        {
            BasePage.setImplicitWait(1);
            _btnCustLogin.Click();
        }

        public void bankMgrLogin()
        {
            BasePage.setImplicitWait(1);
            _btnManagerLogin.Click();
        }
    }
}
