using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace selenium_webdriver_example
{
    public static class chromedriver_appraisalitem
    {
        public static async Task CreateNewAsync(string ratingLink, string login, string pass)
        {
            await Task.Factory.StartNew(() => CreateNew(ratingLink, login, pass));
            
        }

        private static void CreateNew(string ratingLink, string login, string pass)
        {
            var ratingDriver = new ChromeDriver();
            ratingDriver.Url = ratingLink;

            var wait = new WebDriverWait(ratingDriver, TimeSpan.FromSeconds(2));

            ratingDriver
            .FindElement(By.XPath("//*[@id='ctl00_ContentPlaceHolder1_lvLogin_EcommLogin1_Login1_UserName']"))
            .SendKeys(login);

            ratingDriver
                .FindElement(By.XPath("//*[@id='ctl00_ContentPlaceHolder1_lvLogin_EcommLogin1_Login1_Password']"))
                .SendKeys(pass + Keys.Enter);

            Task.Delay(TimeSpan.FromSeconds(1));
            var cookieModal = ratingDriver.FindElementById("ctl00_ContentPlaceHolder1_AppraisalItemRating1_ReminderInformationCookieModalPopup");
            if (cookieModal != null && cookieModal.Displayed)
            {
                cookieModal.FindElement(By.Id("ctl00_ContentPlaceHolder1_AppraisalItemRating1_LbReminderInformationSetCookie")).Click();
            }

            Task.Delay(TimeSpan.FromSeconds(1));

            var content_section = ratingDriver
            .FindElementByXPath("//div[@id='BusinessGoalPlansDiv']/div[@class='List-block']")
            .FindElement(By.XPath("//div[@class='Content-section']"));


            var selects = content_section
                .FindElements(By.XPath("//div[@class='AppraisalItemSelectedGoalRating']/select"));
            foreach (var select in selects)
            {
                var drpGoalRating = new SelectElement(select);

                // todo: implement random
                drpGoalRating.SelectByIndex(3);
            }


            //Thread.Sleep(TimeSpan.FromSeconds(2));
            //var arrows = ratingDriver.FindElements(By.XPath("//div[@class='col-xs-2 col-sm-2 col-lg-1 arrowFixedPos']"));

            //for (int i = arrows.Count - 1; i >= 0; i--)
            //{
            //    wait.Until<IWebElement>((d) => d.FindElement(By.CssSelector("btn-collapse"))).Click();
            //}

            Task.Delay(TimeSpan.FromSeconds(3));

            var comments = ratingDriver.FindElementsByCssSelector("textarea[placeholder='Komentarz']");
            foreach (var comment in comments)
            {
                if (comment.Displayed)
                {
                    comment.SendKeys("komentarz testowy"); 
                }
            }

            Task.Delay(TimeSpan.FromSeconds(1));
            ratingDriver.FindElementByXPath("//input[@type='submit']").Click();

        }

    }
}
