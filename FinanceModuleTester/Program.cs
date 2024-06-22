using System;
using FinanceModule;

namespace FinanceModuleTester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stonks.Load();
            Stonk s = Stonks.Get("AMC");

            Stonket[] twoDayData = Stonks.GetTwoDayStockData(s, DateTime.Now, DateTime.Now.AddDays(-1));
            Stonket today = twoDayData[0];
            Stonket yesterday = twoDayData[1];

            decimal priceChange = today.ClosingPrice - yesterday.ClosingPrice;

            decimal percentChange = priceChange / yesterday.AdjustedClosingPrice * 100;

            Console.WriteLine(s.CompanyName + " " + s.Symbol);
            Console.WriteLine("Last Price\t Price Change\t% Change");
            Console.WriteLine("{0}\t{1}\t{2}", Math.Round(today.AdjustedClosingPrice, 2), Math.Round(priceChange, 2), Math.Round(percentChange, 2));


            //s.HistoricalData = Stonks.Get(s, DateTime.Now.AddDays(-100), DateTime.Now);

            //foreach (Stonket stonket in s.HistoricalData)
            //{
            //    Console.WriteLine("\t<{0}>Opening:${1} | High:${2} | Low:${3} | Close:${4} | Volume:{5}", stonket.TimeStamp, Math.Round(stonket.OpeningPrice, 2),
            //        Math.Round(stonket.HighPrice, 2), Math.Round(stonket.LowPrice, 2), Math.Round(stonket.ClosingPrice, 2), stonket.Volume);
            //}
        }
    }
}
