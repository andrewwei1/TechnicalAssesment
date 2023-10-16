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
    class AccountPage
    {
        [FindsBy(How = How.XPath, Using = "//select[@id='accountSelect']")]
        IWebElement _dropdownAccount;

        [FindsBy(How = How.XPath, Using = "/html[1]/body[1]/div[1]/div[1]/div[2]/div[1]/div[2]/strong[2]")]
        IWebElement _balanceAmount;

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Transactions')]")]
        IWebElement _btnTransactions;

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Deposit')]")]
        IWebElement _btnDeposit;

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Withdrawl')]")]
        IWebElement _btnWithdrawl;

        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")]
        IWebElement _btnSubmit;
        
        [FindsBy(How = How.XPath, Using = "//input[@type='number']")]
        IWebElement _inputAmount;

        private IWebDriver driver;
        private WebDriverWait wait;
        public AccountPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void SelectDropDownAccount(string account)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//select[@id='accountSelect']")));
            SelectElement select = new SelectElement(_dropdownAccount);
            select.SelectByText(account);
        }
        public void ClickTransactions()
        {
            _btnTransactions.Click();
        }
        public void ClickDeposit()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(text(),'Deposit')]")));
            _btnDeposit.Click();
        }
        public void ClickWithdrawl()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(text(),'Withdrawl')]")));
            _btnWithdrawl.Click();
        }
        public void InputAmount(string amount)
        {
            Thread.Sleep(1000);
            _inputAmount.SendKeys(amount);
        }
        public int GetBalance()
        {
            return int.Parse((_balanceAmount).Text);
        }
        public void ClickSubmit()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//button[@type='submit']")));
            _btnSubmit.Click();
        }
    }
}
