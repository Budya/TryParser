using System;
using System.Net;
using HtmlAgilityPack;

namespace TryParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "https://www.walmart.com/browse/shoes/mens-shoes/5438_1045804_1045807/?page=1&cat_id=5438_1045804_1045807&ps=48";
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            

            HtmlNodeCollection htmUl = doc.DocumentNode.SelectNodes(
                "//html/body/div[1]/div/div/div/div[1]/div/section/div[3]/div/div/div[5]/div[2]/div[2]/ul");
            HtmlNodeCollection htmUl_1 = doc.DocumentNode.SelectNodes(
                "//html/body/div[1]/div/div/div/div[1]/div/section/div[3]/div/div/div[5]/div[2]/div[3]/div[2]/ul/li[8]/a");

            

            Console.WriteLine("Hello World!");
        }
    }
}
