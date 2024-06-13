using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceModule
{
    public struct Stonk
    {
        public string Symbol;
        public string CompanyName;

        public List<Stonket> HistoricalData;

        public Stonk(string symbol, string companyName, List<Stonket> stonkets)
        {
            Symbol = symbol;
            CompanyName = companyName;
            HistoricalData = stonkets;
        }

        public override string ToString()
        {
            return string.Format("{0} | {1}", Symbol, CompanyName);
        }
    }

    public struct Stonket
    {
        public long Volume;
        public decimal HighPrice;
        public decimal LowPrice;
        public decimal ClosingPrice;
        public decimal OpeningPrice;
        public decimal AdjustedClosingPrice;
        public DateTime TimeStamp;

        public Stonket(long volume, decimal highPrice, decimal lowPrice, decimal closingPrice, decimal openingPrice, decimal adjustedClosingPrice, DateTime timeStamp)
        {
            Volume = volume;
            HighPrice = highPrice;
            LowPrice = lowPrice;
            ClosingPrice = closingPrice;
            OpeningPrice = openingPrice;
            TimeStamp = timeStamp;
            AdjustedClosingPrice = adjustedClosingPrice;
        }
    }
}
