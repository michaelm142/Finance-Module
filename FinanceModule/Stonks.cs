using System;
using System.Collections.Generic;
using Fynance;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using System.Linq;
using Fynance.Result;
using System.Collections;

namespace FinanceModule
{
    public class Stonks
    {
        private static bool _loaded;

        public static List<Stonk> stonks = new List<Stonk>();

        private static Stonks s = new Stonks();

        public static void Load()
        {
            LoadFrom("dow.csv");
            LoadFrom("nasdaq.csv");
            LoadFrom("nyse.csv");

            _loaded = true;
        }

        private static void LoadFrom(string filename)
        {
            using (StreamReader file = new StreamReader(new FileStream(filename, FileMode.Open)))
            {
                while (!file.EndOfStream)
                {
                    string line = file.ReadLine();
                    string[] pragma = line.Split(new char[] { ',' });
                    Stonk s = new Stonk(pragma[0], pragma[1], null);
                    stonks.Add(s);
                }
            }
        }

        string[] spin = new string[] { "   ", ".  ", ".. ", "..." };

        public List<Stonket> GetStockData(Stonk stonk, DateTime startDate, DateTime endDate)
        {
            List<Stonket> outval = new List<Stonket>();
            try
            {
                YahooTicker ticker = new YahooTicker(stonk.Symbol);
                ticker.SetStartDate(startDate);
                ticker.SetFinishDate(endDate);
                Task<FyResult> t = ticker.GetAsync();
                Console.Write("Downloading Data    ");
                for (int i = 0; !t.IsCompleted; i++)
                {
                    Console.SetCursorPosition(Console.CursorLeft - 3, Console.CursorTop);
                    Console.Write(spin[i % spin.Length]);
                    Thread.Sleep(100);
                }
                Console.WriteLine();
                Console.WriteLine(ticker.Result.Currency);

                foreach (var data in t.Result.Quotes)
                {
                    // Console.WriteLine("(" + i.ToString() + ") " + companyName + " Closing price on: " + data.ElementAt(i).DateTime.Month + "/" + data.ElementAt(i).DateTime.Day + "/" + data.ElementAt(i).DateTime.Year + "$" + Math.Round(data.ElementAt(i).Close, 2));
                    outval.Add(new Stonket((long)data.Volume, data.High, data.Low, data.Close, data.Open, data.AdjClose, data.Period));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception of type {0} occoured. {1}", e.GetType(), e.Message);
            }

            return outval;
        }

        public static Stonk? Get(string symbol)
        {
            //Stonk s = new Stonk();
            //try
            //{
            //    YahooTicker ticker = new YahooTicker(symbol);
            //    ticker.Result.n
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("Exception of type {0} occoured. {1}", e.GetType(), e.Message);
            //}
            //if (stonk != null)
            //    return stonk;

            return stonks.Find(s => s.Symbol == symbol);
        }

        public static List<Stonket> Get(Stonk stonk, DateTime startTime, DateTime endTime)
        {
            if (!_loaded)
                throw new InvalidOperationException("Stonk symbols are not loaded!");

            return s.GetStockData(stonk, startTime, endTime);
        }
    }
}
