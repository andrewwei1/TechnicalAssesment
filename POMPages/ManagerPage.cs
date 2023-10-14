using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalAssesment.POMPages
{
    class ManagerPage
    {
        [FindsBy(How = How.XPath, Using = "//body/div[1]/div[1]/div[2]/div[1]/div[1]/button[1]")]
        IWebElement btnAddCust;

        [FindsBy(How = How.XPath, Using = "//input[@placeholder = 'First Name']")]
        IWebElement _inputFName;

        [FindsBy(How = How.XPath, Using = "//input[@placeholder = 'Last Name']")]
        IWebElement _inputLName;

        [FindsBy(How = How.XPath, Using = "//input[@placeholder = 'Post Code']")]
        IWebElement _inputPCode;

        [FindsBy(How = How.XPath, Using = "//button[@type= 'submit']")]
        IWebElement _btnSubmit;

        public void ClickAddCustomer()
        {
            btnAddCust.Click();
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

        public void clickSubmit()
        {
            _btnSubmit.Click();
        }

    }
}
