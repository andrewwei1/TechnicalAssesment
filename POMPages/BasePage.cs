using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;

namespace TechnicalAssesment.POMPages
{
     class BasePage
    {
         IWebDriver driver;
        
        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
        }
        public void AcceptAlert()
        {
            driver.SwitchTo().Alert().Accept();
        }
    }
   
}
