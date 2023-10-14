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
        [FindsBy(How = How.Id, Using = "accountSelect")]
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

        public AccountPage()
        {
            PageFactory.InitElements(BasePage.driver, this);
        }

        public void SelectDropDownAccount(string account)
        {
            Thread.Sleep(1000);
            SelectElement select = new SelectElement(_dropdownAccount);
            select.SelectByText(account);
        }
        public void ClickTransactions()
        {
            _btnTransactions.Click();
        }
        public void ClickDeposit()
        {
            Thread.Sleep(1000);
            _btnDeposit.Click();
        }
        public void ClickWithdrawl()
        {
            Thread.Sleep(1000);
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
            _btnSubmit.Click();
        }
    }
}
