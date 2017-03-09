using Nito.AsyncEx;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace selenium_webdriver_example
{
    class Program
    {
        static void Main(string[] args)
        {
            chromedriver_appraisalitem.CreateNewAsync("http://localhost:8109/Admin/ElevatoSoftware/Appraisals/AppraisalItemSelfRating.aspx?PerformanceAppraisalsItemId=97", "wl+hra@44group.com", "test");
            chromedriver_appraisalitem.CreateNewAsync("http://localhost:8109/Admin/ElevatoSoftware/Appraisals/AppraisalItemSelfRating.aspx?PerformanceAppraisalsItemId=98", "wl+hra@44group.com", "test");

            //elevato_appraisalRatings();

            Console.ReadKey();
        }

        private static async Task elevato_appraisalRatings()
        {
            //IWebDriver driver = new FirefoxDriver(FirefoxDriverService.CreateDefaultService(), new FirefoxOptions() { UseLegacyImplementation = true }, TimeSpan.FromSeconds(10));

            // todo: implement firefox, ie... etc.
            IWebDriver driver = new ChromeDriver();

            // todo: implement url attribute
            driver.Url = "http://localhost:8109/login.aspx?ReturnUrl=/admin";

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver
                .FindElement(By.XPath("//*[@id='ctl00_ContentPlaceHolder1_lvLogin_EcommLogin1_Login1_UserName']"))
                .SendKeys("wl+hra@44group.com");

            driver
                .FindElement(By.XPath("//*[@id='ctl00_ContentPlaceHolder1_lvLogin_EcommLogin1_Login1_Password']"))
                .SendKeys("test" + Keys.Enter);

            driver.Navigate().GoToUrl("http://localhost:8109/Admin/ElevatoSoftware/Appraisals/default.aspx");

            driver.FindElement(By.XPath("//*[@id='ctl00_ContentPlaceHolder1_ShortCuts1_DivAppraisalManage']/a[1]")).Click();

            driver.FindElement(By.XPath("//*[@id='ctl00_ContentPlaceHolder1_AppraisalsManage1_GvAppraisalsList']/tbody/tr[2]/td[1]/span/a")).Click();

            var ratingsLinks = driver.FindElements(By.XPath("//td[@class='CommandColumn device_sm toSideBar']/div[@class='appraisalActionButtons']/a"));

            foreach (var ratingLink in ratingsLinks)
            {
                string link = ratingLink.GetAttribute("href");
                chromedriver_appraisalitem.CreateNewAsync(link, "wl+hra@44group.com", "test");

                //driver.SwitchTo().Window(selfRatingLink.GetAttribute("href")).Url = selfRatingLink.GetAttribute("href");
            }
        }

    }
}
