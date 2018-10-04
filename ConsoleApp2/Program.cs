using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Options are used for setting "browser capabilities", such as setting a User-Agent
            // property as shown below:
            // var options = new 
            //options.AddAdditionalCapability("phantomjs.page.settings.userAgent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:25.0) Gecko/20100101 Firefox/25.0");
            //options.AddAdditionalCapability("applicationCacheEnabled", false);

            // Services are used for setting up the WebDriver to your likings, such as
            // hiding the console window and restricting image loading as shown below:
            //var service = PhantomJSDriverService.CreateDefaultService(Environment.CurrentDirectory);
            //service.HideCommandPromptWindow = true;
            //service.LoadImages = false;
            var service = ChromeDriverService.CreateDefaultService(Environment.CurrentDirectory);

            var options = new ChromeOptions();
            //options.AddArguments("--headless");

            IWebDriver driver = new ChromeDriver(service, options);
            driver.Url = @"http://tut.by";

            IWebElement element = driver.FindElement(By.TagName("body"));

            Console.ReadKey();
        }
    }
}
