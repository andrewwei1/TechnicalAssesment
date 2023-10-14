using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalAssesment.POMPages
{
    class LoginPage
    {
        IWebElement btnCustLogin = BasePage.driver.FindElement(By.XPath("//button[text()= 'Customer Login']"));
        IWebElement btnBankMgrLogin = BasePage.driver.FindElement(By.XPath("//button[text()= 'Manager Login']"));

        public void loginCust()
        {
            btnCustLogin.Click();
        }

        public void bankMgrLogin()
        {
            btnBankMgrLogin.Click();
        }
    }
}
