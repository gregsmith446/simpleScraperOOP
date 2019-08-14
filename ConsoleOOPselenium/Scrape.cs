using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace ConsoleOOPselenium
{
    class Scrape : Database
    {
        private readonly string UserId;
        private readonly string Password;
        public ChromeDriver driver;
        ChromeOptions options = new ChromeOptions();

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

        public void ScrapeStockData()
        {
            Console.WriteLine("Begin ScrapeStockData Method");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(100);

            IWebElement list = driver.FindElement(By.TagName("tbody"));
            ReadOnlyCollection<IWebElement> items = list.FindElements(By.TagName("tr"));
            int count = items.Count;
            Console.WriteLine("There are " + count + " stocks in the list.");

            IList<IWebElement> symbol_elements = driver.FindElements(By.XPath("//*[@aria-label='Symbol']"));
            IList<IWebElement> lastPrice_elements = driver.FindElements(By.XPath("//*[@aria-label='Last Price']"));
            IList<IWebElement> changePercent_elements = driver.FindElements(By.XPath("//*[@aria-label='Chg %']"));
            IList<IWebElement> volume_elements = driver.FindElements(By.XPath("//*[@aria-label='Volume']"));
            IList<IWebElement> marketCap_elements = driver.FindElements(By.XPath("//*[@aria-label='Market Cap']"));

            ScrapedData scrape = new ScrapedData(symbol_elements, lastPrice_elements, changePercent_elements,
                                       volume_elements, marketCap_elements);

            ParseScrapedData(scrape);
            driver.Close();
        }

        private static void ParseScrapedData(ScrapedData extractedData)
        {
            Console.WriteLine("Begin ParseScrapedData Method");
            int stockTotal = extractedData.StockSymbols.Count;
            Console.WriteLine("stocktotal {0}", stockTotal);

            List<string> symbols = new List<string>();
            List<double> lastPrice = new List<double>();
            List<double> changePercent = new List<double>();
            List<string> volume = new List<string>();
            List<string> marketCap = new List<string>();

            StockModel stock = new StockModel();

            for (int i = 0; i < stockTotal; i++)
            {
                symbols.Insert(i, Convert.ToString(extractedData.StockSymbols[i].Text));
                lastPrice.Insert(i, Convert.ToDouble(extractedData.StockLastPrices[i].Text));
                char trim = '%';
                changePercent.Insert(i, Convert.ToDouble(extractedData.StockChangePercents[i].Text.TrimEnd(trim)));
                volume.Insert(i, Convert.ToString(extractedData.StockVolumes[i].Text));
                marketCap.Insert(i, Convert.ToString(extractedData.StockMarketCaps[i].Text));

                stock = new StockModel(symbols[i],
                                  lastPrice[i],
                                  changePercent[i],
                                  volume[i],
                                  marketCap[i]);

                Console.WriteLine("{0} stock created", symbols[i]);

                InsertStockHistory(stock);
                InsertCurrentStock(stock);
            }
        }
    }
}
