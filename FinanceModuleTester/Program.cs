using System;
using System.Threading;
using FinanceModule;

namespace FinanceModuleTester
{
    internal class Program
    {
        private static string[] spin = new string[] { "   ", ".  ", ".. ", "..." };
        static void Main(string[] args)
        {
            Stonks.Load();
            Stonk s = Stonks.Get("AMC");

            //Stonket[] twoDayData = Stonks.GetTwoDayStockData(s, DateTime.Now, DateTime.Now.AddDays(-1));
            //Stonket today = twoDayData[0];
            //Stonket yesterday = twoDayData[1];

            //decimal priceChange = today.ClosingPrice - yesterday.ClosingPrice;

            //decimal percentChange = priceChange / yesterday.AdjustedClosingPrice * 100;

            //Console.WriteLine(s.CompanyName + " " + s.Symbol);
            //Console.WriteLine("Last Price\t Price Change\t% Change");
            //Console.WriteLine("{0}\t{1}\t{2}", Math.Round(today.AdjustedClosingPrice, 2), Math.Round(priceChange, 2), Math.Round(percentChange, 2));

            Console.Write("Downloading Data    ");
            foreach (int i in s.DownloadStockData(DateTime.Now.AddDays(-10), DateTime.Now))
            {
                Console.SetCursorPosition(Console.CursorLeft - 3, Console.CursorTop);
                Console.Write(spin[i % spin.Length]);
                Thread.Sleep(100);
            }

            foreach (Stonket stonket in s.HistoricalData)
            {
                Console.WriteLine("\t<{0}>Opening:${1} | High:${2} | Low:${3} | Close:${4} | Volume:{5}", stonket.TimeStamp, Math.Round(stonket.OpeningPrice, 2),
                    Math.Round(stonket.HighPrice, 2), Math.Round(stonket.LowPrice, 2), Math.Round(stonket.ClosingPrice, 2), stonket.Volume);
            }
        }
    }
}
