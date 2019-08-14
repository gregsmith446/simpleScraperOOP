using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleOOPselenium
{
    public class StockModel
    {
        private string _symbol;
        private double _lastPrice;
        private double _changePercent;
        private string _volume;
        private string _marketCap;

        public string Symbol { get => _symbol; set => _symbol = value; }
        public double LastPrice { get => _lastPrice; set => _lastPrice = value; }
        public double ChangePercent { get => _changePercent; set => _changePercent = value; }
        public string Volume { get => _volume; set => _volume = value; }
        public string MarketCap { get => _marketCap; set => _marketCap = value; }

        public StockModel()
        {
        }

        public StockModel(string symbol, double lastPrice,
                    double changePercent,
                    string vol, string marketCap)
        {
            Symbol = symbol;
            LastPrice = lastPrice;
            ChangePercent = changePercent;
            Volume = vol;
            MarketCap = marketCap;
        }
    }
}
