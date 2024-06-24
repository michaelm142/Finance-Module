using Fynance.Result;
using Fynance;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceModule
{
    public class Stonk
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
        public IEnumerable<int> DownloadStockData(DateTime startDate, DateTime endDate)
        {
            HistoricalData = new List<Stonket>();
            YahooTicker ticker = new YahooTicker(Symbol);
            ticker.SetStartDate(startDate);
            ticker.SetFinishDate(endDate);
            Task<FyResult> t = ticker.GetAsync();
            for (int i = 1; !t.IsCompleted; i++)
                yield return i;
            Console.WriteLine();
            Console.WriteLine(ticker.Result.Currency);

            foreach (var data in t.Result.Quotes)
            {
                // Console.WriteLine("(" + i.ToString() + ") " + companyName + " Closing price on: " + data.ElementAt(i).DateTime.Month + "/" + data.ElementAt(i).DateTime.Day + "/" + data.ElementAt(i).DateTime.Year + "$" + Math.Round(data.ElementAt(i).Close, 2));
                HistoricalData.Add(new Stonket((long)data.Volume, data.High, data.Low, data.Close, data.Open, data.AdjClose, data.Period));
            }
        }

        public override string ToString()
        {
            return string.Format("{0} | {1}", Symbol, CompanyName);
        }
    }

    public class Stonket
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
