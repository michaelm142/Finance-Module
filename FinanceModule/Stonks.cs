using System;
using System.Collections.Generic;
using Fynance;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using System.Linq;
using Fynance.Result;
using System.Collections;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.ComTypes;

namespace FinanceModule
{
    public class Stonks
    {
        private static bool _loaded;

        public static List<Stonk> stonks = new List<Stonk>();

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

        private static string[] spin = new string[] { "   ", ".  ", ".. ", "..." };

        public static Stonk Get(string symbol)
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

        public static Stonket[] GetTwoDayStockData(Stonk s, DateTime today, DateTime yesterday)
        {
            // determine the current buisness day
            if (today.DayOfWeek == DayOfWeek.Saturday)
            {
                today = DateTime.Now.AddDays(-1);
                yesterday = DateTime.Now.AddDays(-2);
            }
            else if (today.DayOfWeek == DayOfWeek.Sunday)
            {
                today = DateTime.Now.AddDays(-2);
                yesterday = DateTime.Now.AddDays(-3);
            }
            else if (today.DayOfWeek == DayOfWeek.Monday)
                yesterday = today.AddDays(-4);

            // set time to noon for each day
            yesterday = new DateTime(yesterday.Year, yesterday.Month, yesterday.Day, 12, 0, 0, 0);
            today = new DateTime(today.Year, today.Month, today.Day, 12, 0, 0, 0);

            // determine if it is a federal holiday
            while (IsFederalHoliday(today))
            {
                today = today.AddDays(-1);
                yesterday = yesterday.AddDays(-1);
            }
            if (IsFederalHoliday(yesterday))
                yesterday = yesterday.AddDays(-1);

            try
            {
                // create two tickers for today and yesterday
                YahooTicker todayTicker = new YahooTicker(s.Symbol);
                todayTicker.SetStartDate(today.AddHours(-7));
                todayTicker.SetFinishDate(today.AddHours(7));

                YahooTicker yesterdayTicker = new YahooTicker(s.Symbol);
                yesterdayTicker.SetStartDate(yesterday.AddHours(-7));
                yesterdayTicker.SetFinishDate(yesterday.AddHours(7));

                // download stock data
                Task<FyResult> todayTask = todayTicker.GetAsync();
                Task<FyResult> yesterdayTask = yesterdayTicker.GetAsync();
                Console.Write("Downloading Data    ");
                for (int i = 0; !todayTask.IsCompleted && !yesterdayTask.IsCompleted; i++)
                {
                    Console.SetCursorPosition(Console.CursorLeft - 3, Console.CursorTop);
                    Console.Write(spin[i % spin.Length]);
                    Thread.Sleep(100);
                }

                // convert data to local format
                Stonket todayStonket = new Stonket((long)todayTask.Result.Quotes[0].Volume, todayTask.Result.Quotes[0].High, todayTask.Result.Quotes[0].Low,
                    todayTask.Result.Quotes[0].Close, todayTask.Result.Quotes[0].Open, todayTask.Result.Quotes[0].AdjClose, todayTask.Result.Quotes[0].Period);
                Stonket yesterdayStonket = new Stonket((long)yesterdayTask.Result.Quotes[0].Volume, yesterdayTask.Result.Quotes[0].High, yesterdayTask.Result.Quotes[0].Low,
                    yesterdayTask.Result.Quotes[0].Close, yesterdayTask.Result.Quotes[0].Open, yesterdayTask.Result.Quotes[0].AdjClose, yesterdayTask.Result.Quotes[0].Period);

                return new Stonket[] { todayStonket, yesterdayStonket };
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception of type {0} was thrown. {1}", e.GetType(), e.Message);
            }

            // failed to get data, return null
            return null;
        }

        // determine if it is a federal holiday
        public static bool IsFederalHoliday(DateTime date)
        {
            // List of federal holidays in the United States
            // New Year's Day
            if (date.Month == 1 && date.Day == 1)
                return true;

            // Martin Luther King Jr. Day (Third Monday in January)
            if (date.Month == 1 && date.DayOfWeek == DayOfWeek.Monday && WeekOfMonth(date) == 3)
                return true;

            // Washington's Birthday (Third Monday in February)
            if (date.Month == 2 && date.DayOfWeek == DayOfWeek.Monday && WeekOfMonth(date) == 3)
                return true;

            // Memorial Day (Last Monday in May)
            if (date.Month == 5 && date.DayOfWeek == DayOfWeek.Monday && date.AddDays(7).Month != 5)
                return true;

            // Independence Day
            if (date.Month == 7 && date.Day == 4)
                return true;

            // Labor Day (First Monday in September)
            if (date.Month == 9 && date.DayOfWeek == DayOfWeek.Monday && WeekOfMonth(date) == 1)
                return true;

            // Columbus Day (Second Monday in October)
            if (date.Month == 10 && date.DayOfWeek == DayOfWeek.Monday && WeekOfMonth(date) == 2)
                return true;

            // Veterans Day
            if (date.Month == 11 && date.Day == 11)
                return true;

            // Thanksgiving Day (Fourth Thursday in November)
            if (date.Month == 11 && date.DayOfWeek == DayOfWeek.Thursday && WeekOfMonth(date) == 4)
                return true;

            // Christmas Day
            if (date.Month == 12 && date.Day == 25)
                return true;

            // If none of the above, it's not a federal holiday
            return false;
        }

        // Helper method to determine the week of the month
        private static int WeekOfMonth(DateTime date)
        {
            DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            int offset = (int)firstDayOfMonth.DayOfWeek;
            int weekOfMonth = (date.Day + offset - 1) / 7 + 1;
            return weekOfMonth;
        }
    }
}
