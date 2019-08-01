﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ConsoleOOPselenium
{
    class Scrape
    {
        private readonly string UserId;
        private readonly string Password;
        public ChromeDriver driver;

        public Scrape(string id, string password)
        {
            UserId = id;
            Password = password;

            driver = new ChromeDriver(@"\Users\gregs\Desktop\CD\ConsoleOOPselenium\ConsoleOOPselenium\bin\Debug\netcoreapp2.1");
            
        }

        public void LogIn()
        {
            driver.Navigate().GoToUrl("https://login.yahoo.com/config/login?.src=finance&amp;.intl=us&amp;.done=https%3A%2F%2Ffinance.yahoo.com%2Fportfolios");
            driver.FindElement(By.Id("login-username")).SendKeys(UserId + Keys.Enter);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.FindElement(By.Id("login-passwd")).SendKeys(Password + Keys.Enter);
        }

        public void NavigateToPortfolio()
        {
            driver.Navigate().GoToUrl("https://finance.yahoo.com/portfolio/p_0/view/v1");
        }

        public List<StockModel> ScrapeStockData()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(100);

            List<StockModel> stockList = new List<StockModel>();

            IWebElement list = driver.FindElement(By.TagName("tbody"));
            ReadOnlyCollection<IWebElement> items = list.FindElements(By.TagName("tr"));
            int count = items.Count;

            Console.WriteLine("There are " + count + " stocks in the list.");

            IList<IWebElement> stockData = driver.FindElements(By.ClassName("simpTblRow"));

            for (int i = 1; i <= count; i++)
            {
                string symbol = driver.FindElement(By.XPath("//*[@id=\"pf-detail-table\"]/div[1]/table/tbody/tr[" + i + "]/td[1]/a")).GetAttribute("innerText");
                string price = driver.FindElement(By.XPath("//*[@id=\"pf-detail-table\"]/div[1]/table/tbody/tr[" + i + "]/td[2]/span")).GetAttribute("innerText");
                string pchange = driver.FindElement(By.XPath("//*[@id=\"pf-detail-table\"]/div[1]/table/tbody/tr[" + i + "]/td[4]/span")).GetAttribute("innerText");
                string volume = driver.FindElement(By.XPath("//*[@id=\"pf-detail-table\"]/div[1]/table/tbody/tr[" + i + "]/td[7]/span")).GetAttribute("innerText");
                string marketcap = driver.FindElement(By.XPath("//*[@id=\"pf-detail-table\"]/div[1]/table/tbody/tr[" + i + "]/td[13]/span")).GetAttribute("innerText");

                Console.WriteLine("Stock Data: " + symbol + "\n" + price + "\n" + pchange + "\n" + volume + "\n" + marketcap + "\n");

                StockModel eachStock = new StockModel
                {
                    Symbol = symbol,
                    Price = price,
                    PChange = pchange,
                    Volume = volume,
                    MarketCap = marketcap
                };

                stockList.Add(eachStock);

                driver.Close();
            }
            return stockList;
        }

        class Program
        {
            static void Main(string[] args)
            {

                Console.WriteLine("Let's Get This Bread!");

                var scraper = new Scrape("gregsmith446@intracitygeeks.org", "SILICONrhode1!");

                scraper.LogIn();
                scraper.NavigateToPortfolio();
                scraper.ScrapeStockData();
            }
        }

    }
}
