using System;
using System.Collections.Generic;
using YahooFinanceApi;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using System.Linq;

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

        public async Task<List<Stonket>> GetStockData(Stonk stonk, DateTime startDate, DateTime endDate)
        {
            List<Stonket> outval = new List<Stonket>();
            try
            {
                var data = await Yahoo.GetHistoricalAsync(stonk.Symbol, startDate, endDate);
                var security = await Yahoo.Symbols(stonk.Symbol).Fields(Field.LongName).QueryAsync();
                var ticker = security[stonk.Symbol];
                var companyName = ticker[Field.LongName];

                for (int i = 0; i < data.Count; i++)
                {
                    // Console.WriteLine("(" + i.ToString() + ") " + companyName + " Closing price on: " + data.ElementAt(i).DateTime.Month + "/" + data.ElementAt(i).DateTime.Day + "/" + data.ElementAt(i).DateTime.Year + "$" + Math.Round(data.ElementAt(i).Close, 2));
                    outval.Add(new Stonket(data.ElementAt(i).Volume, data.ElementAt(i).High, data.ElementAt(i).Low, data.ElementAt(i).Close, data.ElementAt(i).Open, data.ElementAt(i).DateTime));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception of type {0} occoured. {1}", e.GetType(), e.Message);
            }

            return outval;
        }

        public static Stonk? Get(string nameOrSymbol)
        {
            Stonk? stonk = stonks.Find(s => s.Symbol == nameOrSymbol);
            if (stonk != null)
                return stonk;

            return stonks.Find(s => s.CompanyName == nameOrSymbol);
        }

        public static List<Stonket> Get(Stonk stonk, DateTime startTime, DateTime endTime)
        {
            if (!_loaded)
                throw new InvalidOperationException("Stonk symbols are not loaded!");

            return s.GetStockData(stonk, startTime, endTime).Result;
        }
    }
}
