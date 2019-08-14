using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ConsoleOOPselenium
{
    class Program
    {
        static void Main(string[] args)
        {
            var scraper = new Scrape("gregsmith446@intracitygeeks.org", "SILICONrhode1!");

            scraper.LogIn();
            scraper.NavigateToPortfolio();
            scraper.ScrapeStockData();
        }
    }

}
