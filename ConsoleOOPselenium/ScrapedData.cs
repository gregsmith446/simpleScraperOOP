using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace ConsoleOOPselenium
{
    class ScrapedData
    {
        public IList<IWebElement> StockSymbols { get; set; }
        public IList<IWebElement> StockLastPrices { get; set; }
        public IList<IWebElement> StockChangePercents { get; set; }
        public IList<IWebElement> StockVolumes { get; set; }
        public IList<IWebElement> StockMarketCaps { get; set; }

        public ScrapedData(IList<IWebElement> symbols, IList<IWebElement> lastPrices,
                    IList<IWebElement> changePercents,
                    IList<IWebElement> volumes,
                    IList<IWebElement> marketCaps)
        {
            StockSymbols = symbols;
            StockLastPrices = lastPrices;
            StockChangePercents = changePercents;
            StockVolumes = volumes;
            StockMarketCaps = marketCaps;
        }
    }
}
