using System;
using System.Collections;
using System.Collections.Generic;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Support.UI;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Options are used for setting "browser capabilities", such as setting a User-Agent
            // property as shown below:
            var options = new PhantomJSOptions();
            //options.AddAdditionalCapability("phantomjs.page.settings.userAgent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:25.0) Gecko/20100101 Firefox/25.0");
            //options.AddAdditionalCapability("applicationCacheEnabled", false);

            // Services are used for setting up the WebDriver to your likings, such as
            // hiding the console window and restricting image loading as shown below:
            var service = PhantomJSDriverService.CreateDefaultService(Environment.CurrentDirectory);
            service.HideCommandPromptWindow = true;
            service.LoadImages = false;

            using (IWebDriver driver = new PhantomJSDriver(service, options))
            {
                //Notice navigation is slightly different than the Java version
                //This is because 'get' is a keyword in C#
                //driver.Navigate().GoToUrl("https://www.walmart.com/browse/shoes/mens-shoes/5438_1045804_1045807?page=2&ps=48#searchProductResult");

                driver.Url =
                    @"https://www.walmart.com/browse/shoes/mens-shoes/5438_1045804_1045807?page=2&ps=48#searchProductResult";

                // Find the text input element by its name
                //IWebElement query = driver.FindElement(By.Name("q"));

                //IJavaScriptExecutor js = ((IJavaScriptExecutor)driver);
                //js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");

                IWebElement query = driver.FindElement(By.TagName("body"));

                //IList<IWebElement> cheeses = driver.FindElements(By.ClassName("cheese"));

                Console.ReadLine();
            }

        
        }
    }
}
