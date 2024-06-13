using System;
using FinanceModule;

namespace FinanceModuleTester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stonks.Load();
            Stonk s = Stonks.Get("AMC").Value;
            s.HistoricalData = Stonks.Get(s, DateTime.Now.AddDays(-100), DateTime.Now);

            Console.WriteLine(s.CompanyName + " " + s.Symbol); 
            foreach (Stonket stonket in s.HistoricalData)
            {
                Console.WriteLine("\t<{0}>Opening:${1} | High:${2} | Low:${3} | Close:${4} | Volume:{5}", stonket.TimeStamp, Math.Round(stonket.OpeningPrice, 2),
                    Math.Round(stonket.HighPrice, 2), Math.Round(stonket.LowPrice, 2), Math.Round(stonket.ClosingPrice, 2), stonket.Volume);
            }
        }
    }
}
