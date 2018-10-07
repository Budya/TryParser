using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;


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

            List<string> sourceUrls = new List<string>();
            List<Shoes> resultListShoes = new List<Shoes>();

            IWebDriver driver = new ChromeDriver(service, options);
            driver.Url = @"https://www.walmart.com/browse/clothing/mens-shoes/5438_1045804_1045807?page=1&ps=48#searchProductResult";

            WebDriverWait waitForElement = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            waitForElement.Until(ExpectedConditions.ElementIsVisible(By.ClassName("GlobalFooterCopyright")));

            IWebElement elem = driver.FindElement(By.ClassName("GlobalFooterCopyright"));

            driver.ExecuteJavaScript("arguments[0].scrollIntoView(true);", elem);

            // get count of pages for parsing
            var pagesWebElements = driver.FindElement(By.ClassName("paginator-list")).FindElements(By.TagName("li"));
            var lastPage = pagesWebElements[pagesWebElements.Count-1].FindElement(By.TagName("a")).Text;
            

            // !!!!!!! ONLY FOR TEST
            //int lastPageNum = Int32.Parse(lastPage);
            int lastPageNum = 5;


            // creating a list of urls
            string urlFirstPart = @"https://www.walmart.com/browse/clothing/mens-shoes/5438_1045804_1045807?page=";
            string urlEndPart = @"&ps=48#searchProductResult";
            for (int pageIterator = 1; pageIterator <= lastPageNum; pageIterator++)
            {
                string walmartUrl = urlFirstPart + pageIterator + urlEndPart;
                sourceUrls.Add(walmartUrl);
            }

            foreach (var sourceUrl in sourceUrls)
            {
                driver.Url = sourceUrl;

                WebDriverWait waitForElement1 = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                waitForElement1.Until(ExpectedConditions.ElementIsVisible(By.ClassName("GlobalFooterCopyright")));
                IWebElement elem1 = driver.FindElement(By.ClassName("GlobalFooterCopyright"));

                driver.ExecuteJavaScript("arguments[0].scrollIntoView(true);", elem1);

                var elements = driver.FindElement(By.ClassName("search-result-gridview-items"))
                    .FindElements(By.ClassName("Grid-col"));



                foreach (var webElement in elements)
                {
                    Shoes shoes =  new Shoes();

                    Console.WriteLine(webElement.FindElement(By.TagName("img"))
                        .GetAttribute("data-image-src")); //ссылка на изображение
                    shoes.ShoesImageUrl = webElement.FindElement(By.TagName("img"))
                        .GetAttribute("data-image-src");
                    try
                    {
                        Console.WriteLine(webElement.FindElement(By.ClassName("product-brand"))
                            .FindElement(By.TagName("strong")).Text);

                        shoes.ShoesBrand = webElement.FindElement(By.ClassName("product-brand"))
                            .FindElement(By.TagName("strong")).Text;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("No Brand Name");
                        shoes.ShoesBrand = "No Brand Name";
                        //throw;
                    }
                    
                    Console.WriteLine(webElement.FindElement(By.TagName("img"))
                        .GetAttribute("alt")); // название ботинок

                    shoes.ShoesTitle = webElement.FindElement(By.TagName("img"))
                        .GetAttribute("alt");

                    try
                    {
                        var selectors = webElement.FindElement(By.ClassName("swatch-selector")).FindElements(By.TagName("button"));
                        foreach (var selector in selectors)
                        {
                            Console.WriteLine(selector.FindElement(By.TagName("img")).GetAttribute("alt"));
                            string str = selector.FindElement(By.TagName("img")).GetAttribute("alt");
                            shoes.ShoesVariants.Add(str);
                        }
                    }
                    catch (Exception e)
                    {
                        //Console.WriteLine(e.Message);
                        
                        shoes.ShoesVariants.Add("No Variants");
                    }

                    try
                    {
                        Console.WriteLine(webElement.FindElement(By.ClassName("price-group")).GetAttribute("aria-label"));
                        shoes.ShoesPrice = webElement.FindElement(By.ClassName("price-group"))
                            .GetAttribute("aria-label");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Out of Stock");
                        shoes.ShoesPrice = "Out of Stock";
                        //throw;
                    }
                    
                    resultListShoes.Add(shoes);
                }

            }

            Console.ReadKey();
        }
    }
}
