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
    static class BasePage
    {
        public static IWebDriver driver = new ChromeDriver();
        public static WebDriverWait wait = new WebDriverWait(BasePage.driver, TimeSpan.FromSeconds(10));
        public static void AcceptAlert()
        {
            driver.SwitchTo().Alert().Accept();
        }

        public static void CloseAlert()
        {
            driver.SwitchTo().Alert().Dismiss();
        }

        public static void setImplicitWait(int seconds)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(seconds);
        }

        //public static void setExplicitWait(By locator, int seconds)
        //{
        //    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
        //    wait.Until(ExpectedConditions.ElementIsVisible(locator));
        //}
    }
   
}
