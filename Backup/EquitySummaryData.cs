// Decompiled with JetBrains decompiler
// Type: Yahoo.Finance.EquitySummaryData
// Assembly: Yahoo.Finance, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8859EB3C-3A3D-4D2A-8276-F613AD1DF364
// Assembly location: C:\Users\dagma\.nuget\packages\yahoo.finance\4.3.3\lib\netstandard2.0\Yahoo.Finance.dll

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Yahoo.Finance
{
  public class EquitySummaryData : EquityData
  {
    public string Name { get; set; }

    public string StockSymbol { get; set; }

    public float Price { get; set; }

    public float DollarChange { get; set; }

    public float PercentChange { get; set; }

    public float PreviousClose { get; set; }

    public float Open { get; set; }

    public float BidPrice { get; set; }

    public int BidQuantity { get; set; }

    public float AskPrice { get; set; }

    public int AskQuantity { get; set; }

    public float DayRangeLow { get; set; }

    public float DayRangeHigh { get; set; }

    public float YearRangeLow { get; set; }

    public float YearRangeHigh { get; set; }

    public long Volume { get; set; }

    public long AverageVolume { get; set; }

    public double MarketCap { get; set; }

    public float? Beta { get; set; }

    public float? PriceEarningsRatio { get; set; }

    public float EarningsPerShare { get; set; }

    public DateTime? EarningsDate { get; set; }

    public float? ForwardDividend { get; set; }

    public float? ForwardDividendYield { get; set; }

    public DateTime? ExDividendDate { get; set; }

    public float YearTargetEstimate { get; set; }

    public static async Task<EquitySummaryData> CreateAsync(string symbol)
    {
      HttpClient httpClient = new HttpClient();
      HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
      httpRequestMessage.Method = HttpMethod.Get;
      httpRequestMessage.RequestUri = new Uri("https://finance.yahoo.com/quote/" + symbol);
      httpRequestMessage.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36 Edg/122.0.0.0");
      httpRequestMessage.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
      HttpRequestMessage request = httpRequestMessage;
      string web = await (await httpClient.SendAsync(request)).Content.ReadAsStringAsync();
      EquitySummaryData async = !web.ToLower().Contains(">Please check your spelling.".ToLower()) ? EquitySummaryData.ParseFromWebContent(web) : throw new Exception("Stock '" + symbol + "' is not a valid stock symbol.");
      if (async.Name == null)
        async.Name = symbol.Trim().ToUpper();
      if (async.StockSymbol == null)
        async.StockSymbol = symbol.Trim().ToUpper();
      async.QueriedSymbol = symbol.ToUpper().Trim();
      return async;
    }

    public static EquitySummaryData ParseFromWebContent(string web)
    {
      EquitySummaryData fromWebContent = new EquitySummaryData();
      List<string> stringList = new List<string>();
      int num1;
      int num2;
      if (web.Split(new string[1]{ "svelte" }, StringSplitOptions.None).Length > 10)
      {
        try
        {
          int start_at = YahooFinanceToolkit.StepIndexOf(web, "<section class=\"main svelte", "<h1", ">");
          int num3 = YahooFinanceToolkit.StepIndexOf(web, start_at, "<");
          string str = web.Substring(start_at + 1, num3 - start_at - 1);
          int num4 = str.IndexOf("(");
          fromWebContent.Name = str.Substring(0, num4 - 1).Trim();
          fromWebContent.Name = fromWebContent.Name.Replace("&amp;", "&");
          fromWebContent.Name = fromWebContent.Name.Replace("&#x27;", "'");
          int num5 = str.IndexOf(")", num4 + 1);
          fromWebContent.StockSymbol = str.Substring(num4 + 1, num5 - num4 - 1);
        }
        catch
        {
          fromWebContent.Name = (string) null;
          fromWebContent.StockSymbol = (string) null;
        }
        string byDataFieldName2_1 = fromWebContent.GetDataByDataFieldName2(web, "regularMarketPrice");
        string str1 = fromWebContent.HtmlCleanse(byDataFieldName2_1);
        fromWebContent.Price = Convert.ToSingle(str1);
        try
        {
          fromWebContent.DollarChange = Convert.ToSingle(fromWebContent.HtmlCleanse(fromWebContent.GetDataByDataFieldName2(web, "regularMarketChange")));
        }
        catch
        {
        }
        try
        {
          string byDataFieldName2_2 = fromWebContent.GetDataByDataFieldName2(web, "regularMarketChangePercent");
          float single = Convert.ToSingle(fromWebContent.HtmlCleanse(byDataFieldName2_2).Replace("+", "").Replace("(", "").Replace(")", "").Replace("%", ""));
          fromWebContent.PercentChange = single / 100f;
        }
        catch
        {
        }
        try
        {
          fromWebContent.PreviousClose = Convert.ToSingle(fromWebContent.HtmlCleanse(fromWebContent.GetDataByDataFieldName2(web, "regularMarketPreviousClose")));
        }
        catch
        {
        }
        try
        {
          fromWebContent.Open = Convert.ToSingle(fromWebContent.HtmlCleanse(fromWebContent.GetDataByDataFieldName2(web, "regularMarketOpen")));
        }
        catch
        {
        }
        try
        {
          num1 = 0;
          num2 = 0;
          int num6 = web.IndexOf(">Bid<");
          int num7 = web.IndexOf("<span class=", num6 + 1);
          int num8 = web.IndexOf(">", num7 + 1);
          int num9 = web.IndexOf("<", num8 + 1);
          string str2 = web.Substring(num8 + 1, num9 - num8 - 1);
          if (str2 == "")
          {
            int num10 = web.IndexOf(">", num9 + 1);
            int num11 = web.IndexOf("<", num10 + 1);
            str2 = web.Substring(num10 + 1, num11 - num10 - 1);
          }
          stringList.Clear();
          stringList.Add(" x ");
          string[] strArray = str2.Split(stringList.ToArray(), StringSplitOptions.None);
          fromWebContent.BidPrice = Convert.ToSingle(strArray[0]);
          fromWebContent.BidQuantity = Convert.ToInt32(strArray[1]);
        }
        catch
        {
          fromWebContent.BidPrice = 0.0f;
          fromWebContent.BidQuantity = 0;
        }
        try
        {
          num1 = 0;
          num2 = 0;
          int num12 = web.IndexOf(">Ask<");
          int num13 = web.IndexOf("<span class=", num12 + 1);
          int num14 = web.IndexOf(">", num13 + 1);
          int num15 = web.IndexOf("<", num14 + 1);
          string str3 = web.Substring(num14 + 1, num15 - num14 - 1);
          if (str3 == "")
          {
            int num16 = web.IndexOf(">", num15 + 1);
            int num17 = web.IndexOf("<", num16 + 1);
            str3 = web.Substring(num16 + 1, num17 - num16 - 1);
          }
          stringList.Clear();
          stringList.Add(" x ");
          string[] strArray = str3.Split(stringList.ToArray(), StringSplitOptions.None);
          fromWebContent.AskPrice = Convert.ToSingle(strArray[0]);
          fromWebContent.AskQuantity = Convert.ToInt32(strArray[1]);
        }
        catch
        {
          fromWebContent.AskPrice = 0.0f;
          fromWebContent.AskQuantity = 0;
        }
        try
        {
          string str4 = fromWebContent.GetDataByDataFieldName2(web, "regularMarketDayRange").Replace(" ", "");
          stringList.Clear();
          stringList.Add("-");
          string[] array = stringList.ToArray();
          string[] strArray = str4.Split(array, StringSplitOptions.None);
          fromWebContent.DayRangeLow = Convert.ToSingle(strArray[0].Trim());
          fromWebContent.DayRangeHigh = Convert.ToSingle(strArray[1].Trim());
        }
        catch
        {
          fromWebContent.DayRangeLow = 0.0f;
          fromWebContent.DayRangeHigh = 0.0f;
        }
        try
        {
          string str5 = fromWebContent.GetDataByDataFieldName2(web, "fiftyTwoWeekRange").Replace(" ", "");
          stringList.Clear();
          stringList.Add("-");
          string[] array = stringList.ToArray();
          string[] strArray = str5.Split(array, StringSplitOptions.None);
          fromWebContent.YearRangeLow = Convert.ToSingle(strArray[0].Trim());
          fromWebContent.YearRangeHigh = Convert.ToSingle(strArray[1].Trim());
        }
        catch
        {
          fromWebContent.YearRangeLow = 0.0f;
          fromWebContent.YearRangeHigh = 0.0f;
        }
        try
        {
          fromWebContent.Volume = Convert.ToInt64(fromWebContent.GetDataByDataFieldName2(web, "regularMarketVolume").Replace(",", ""));
        }
        catch
        {
        }
        try
        {
          fromWebContent.AverageVolume = Convert.ToInt64(fromWebContent.GetDataByDataFieldName2(web, "averageVolume").Replace(",", ""));
        }
        catch
        {
        }
        try
        {
          string str6 = fromWebContent.GetDataByDataFieldName2(web, "marketCap").Trim();
          string lower = str6.Substring(str6.Length - 1, 1).ToLower();
          float single = Convert.ToSingle(str6.Substring(0, str6.Length - 1));
          switch (lower)
          {
            case "th":
              fromWebContent.MarketCap = Convert.ToDouble(single * 1000f);
              break;
            case "m":
              fromWebContent.MarketCap = Convert.ToDouble(single * 1000000f);
              break;
            case "b":
              fromWebContent.MarketCap = Convert.ToDouble(single * 1E+09f);
              break;
            case "t":
              fromWebContent.MarketCap = Convert.ToDouble(single * 1E+12f);
              break;
          }
        }
        catch
        {
          fromWebContent.MarketCap = 0.0;
        }
        try
        {
          int num18 = web.IndexOf(">Beta (5Y Monthly)<");
          int num19 = web.IndexOf("<span class", num18 + 1);
          int num20 = web.IndexOf(">", num19 + 1);
          int num21 = web.IndexOf("<", num20 + 1);
          fromWebContent.Beta = new float?(Convert.ToSingle(web.Substring(num20 + 1, num21 - num20 - 1)));
        }
        catch
        {
          fromWebContent.Beta = new float?();
        }
        try
        {
          fromWebContent.PriceEarningsRatio = new float?(Convert.ToSingle(fromWebContent.GetDataByDataFieldName2(web, "trailingPE").Trim()));
        }
        catch
        {
        }
        try
        {
          int num22 = web.IndexOf(">EPS (TTM)<");
          int num23 = web.IndexOf("<span class", num22 + 1);
          int num24 = web.IndexOf("<fin-streamer", num23 + 1);
          int num25 = web.IndexOf(">", num24 + 1);
          int num26 = web.IndexOf("<", num25 + 1);
          fromWebContent.EarningsPerShare = Convert.ToSingle(web.Substring(num25 + 1, num26 - num25 - 1));
        }
        catch
        {
        }
        try
        {
          int num27 = web.IndexOf(">Earnings Date<");
          int num28 = web.IndexOf("<span", num27 + 1);
          int num29 = web.IndexOf(">", num28 + 1);
          int num30 = web.IndexOf("<", num29 + 1);
          string s = web.Substring(num29 + 1, num30 - num29 - 1);
          if (s.Contains("-"))
            s = s.Substring(0, s.IndexOf("-") - 1).Trim();
          fromWebContent.EarningsDate = new DateTime?(DateTime.Parse(s));
        }
        catch
        {
          fromWebContent.EarningsDate = new DateTime?();
        }
        try
        {
          int num31 = web.IndexOf(">Forward Dividend &amp; Yield<");
          int num32 = web.IndexOf("<span class", num31 + 1);
          int num33 = web.IndexOf(">", num32 + 1);
          int num34 = web.IndexOf("<", num33 + 1);
          string str7 = web.Substring(num33 + 1, num34 - num33 - 1);
          if (str7 == "N/A (N/A)")
          {
            fromWebContent.ForwardDividend = new float?();
            fromWebContent.ForwardDividendYield = new float?();
          }
          else
          {
            stringList.Clear();
            stringList.Add(" ");
            string[] strArray = str7.Split(stringList.ToArray(), StringSplitOptions.None);
            fromWebContent.ForwardDividend = new float?(Convert.ToSingle(strArray[0]));
            fromWebContent.ForwardDividendYield = new float?(Convert.ToSingle(strArray[1].Replace("%", "").Replace("(", "").Replace(")", "")) / 100f);
          }
        }
        catch
        {
          fromWebContent.ForwardDividend = new float?(0.0f);
          fromWebContent.ForwardDividendYield = new float?(0.0f);
        }
        try
        {
          int num35 = web.IndexOf(">Ex-Dividend Date<");
          int num36 = web.IndexOf("<span class", num35 + 1);
          int num37 = web.IndexOf(">", num36 + 1);
          int num38 = web.IndexOf("<", num37 + 1);
          string s = web.Substring(num37 + 1, num38 - num37 - 1);
          fromWebContent.ExDividendDate = new DateTime?(DateTime.Parse(s));
        }
        catch
        {
          fromWebContent.ExDividendDate = new DateTime?();
        }
        try
        {
          fromWebContent.YearTargetEstimate = Convert.ToSingle(fromWebContent.GetDataByDataFieldName2(web, "targetMeanPrice"));
        }
        catch
        {
        }
      }
      else
      {
        string dataByClassName = fromWebContent.GetDataByClassName(web, "D(ib) Fz(18px)");
        int num39 = dataByClassName.IndexOf("(");
        fromWebContent.Name = dataByClassName.Substring(0, num39 - 1).Trim();
        fromWebContent.Name = fromWebContent.Name.Replace("&amp;", "&");
        fromWebContent.Name = fromWebContent.Name.Replace("&#x27;", "'");
        int num40 = dataByClassName.IndexOf(")", num39 + 1);
        fromWebContent.StockSymbol = dataByClassName.Substring(num39 + 1, num40 - num39 - 1);
        fromWebContent.Price = Convert.ToSingle(fromWebContent.GetDataByClassName(web, "Fw(b) Fz(36px) Mb(-4px) D(ib)"));
        try
        {
          int num41 = web.IndexOf("qsp-price-change");
          int num42 = web.IndexOf("<span class", num41 + 1);
          int num43 = web.IndexOf(">", num42 + 1);
          int num44 = web.IndexOf("<", num43 + 1);
          string str = web.Substring(num43 + 1, num44 - num43 - 1);
          fromWebContent.DollarChange = Convert.ToSingle(str.Replace("+", ""));
        }
        catch
        {
          fromWebContent.DollarChange = 0.0f;
        }
        try
        {
          int num45 = web.LastIndexOf("data-field=\"regularMarketChangePercent\" data-trend=\"txt\"");
          int num46 = web.IndexOf("<span class", num45 + 1);
          int num47 = web.IndexOf(">", num46 + 1);
          int num48 = web.IndexOf("<", num47 + 1);
          float single = Convert.ToSingle(web.Substring(num47 + 1, num48 - num47 - 1).Replace("+", "").Replace("(", "").Replace(")", "").Replace("%", ""));
          fromWebContent.PercentChange = single / 100f;
        }
        catch
        {
          fromWebContent.PercentChange = 0.0f;
        }
        try
        {
          fromWebContent.PreviousClose = Convert.ToSingle(fromWebContent.GetDataByDataTestName(web, "PREV_CLOSE-value"));
        }
        catch
        {
          fromWebContent.PreviousClose = 0.0f;
        }
        try
        {
          fromWebContent.Open = Convert.ToSingle(fromWebContent.GetDataByDataTestName(web, "OPEN-value"));
        }
        catch
        {
          fromWebContent.Open = 0.0f;
        }
        try
        {
          num1 = 0;
          num2 = 0;
          int num49 = web.IndexOf("BID-value");
          int num50 = web.IndexOf(">", num49 + 1);
          int num51 = web.IndexOf("<", num50 + 1);
          string str = web.Substring(num50 + 1, num51 - num50 - 1);
          if (str == "")
          {
            int num52 = web.IndexOf(">", num51 + 1);
            int num53 = web.IndexOf("<", num52 + 1);
            str = web.Substring(num52 + 1, num53 - num52 - 1);
          }
          stringList.Clear();
          stringList.Add(" x ");
          string[] strArray = str.Split(stringList.ToArray(), StringSplitOptions.None);
          fromWebContent.BidPrice = Convert.ToSingle(strArray[0]);
          fromWebContent.BidQuantity = Convert.ToInt32(strArray[1]);
        }
        catch
        {
          fromWebContent.BidPrice = 0.0f;
          fromWebContent.BidQuantity = 0;
        }
        try
        {
          num1 = 0;
          num2 = 0;
          int num54 = web.IndexOf("ASK-value");
          int num55 = web.IndexOf(">", num54 + 1);
          int num56 = web.IndexOf("<", num55 + 1);
          string str = web.Substring(num55 + 1, num56 - num55 - 1);
          if (str == "")
          {
            int num57 = web.IndexOf(">", num56 + 1);
            int num58 = web.IndexOf("<", num57 + 1);
            str = web.Substring(num57 + 1, num58 - num57 - 1);
          }
          stringList.Clear();
          stringList.Add(" x ");
          string[] strArray = str.Split(stringList.ToArray(), StringSplitOptions.None);
          fromWebContent.AskPrice = Convert.ToSingle(strArray[0]);
          fromWebContent.AskQuantity = Convert.ToInt32(strArray[1]);
        }
        catch
        {
          fromWebContent.AskPrice = 0.0f;
          fromWebContent.AskQuantity = 0;
        }
        try
        {
          int num59 = web.IndexOf("DAYS_RANGE-value");
          int num60 = web.IndexOf(">", num59 + 1);
          int num61 = web.IndexOf("<", num60 + 1);
          string str = web.Substring(num60 + 1, num61 - num60 - 1).Replace(" ", "");
          stringList.Clear();
          stringList.Add("-");
          string[] array = stringList.ToArray();
          string[] strArray = str.Split(array, StringSplitOptions.None);
          fromWebContent.DayRangeLow = Convert.ToSingle(strArray[0].Trim());
          fromWebContent.DayRangeHigh = Convert.ToSingle(strArray[1].Trim());
        }
        catch
        {
          fromWebContent.DayRangeLow = 0.0f;
          fromWebContent.DayRangeHigh = 0.0f;
        }
        try
        {
          int num62 = web.IndexOf("FIFTY_TWO_WK_RANGE-value");
          int num63 = web.IndexOf(">", num62 + 1);
          int num64 = web.IndexOf("<", num63 + 1);
          string str = web.Substring(num63 + 1, num64 - num63 - 1).Replace(" ", "");
          stringList.Clear();
          stringList.Add("-");
          string[] array = stringList.ToArray();
          string[] strArray = str.Split(array, StringSplitOptions.None);
          fromWebContent.YearRangeLow = Convert.ToSingle(strArray[0].Trim());
          fromWebContent.YearRangeHigh = Convert.ToSingle(strArray[1].Trim());
        }
        catch
        {
          fromWebContent.YearRangeLow = 0.0f;
          fromWebContent.YearRangeHigh = 0.0f;
        }
        try
        {
          fromWebContent.Volume = Convert.ToInt64(fromWebContent.GetDataByDataFieldName(web, "regularMarketVolume").Replace(",", ""));
        }
        catch
        {
          fromWebContent.Volume = 0L;
        }
        try
        {
          fromWebContent.AverageVolume = Convert.ToInt64(fromWebContent.GetDataByDataTestName(web, "AVERAGE_VOLUME_3MONTH-value").Replace(",", ""));
        }
        catch
        {
          fromWebContent.AverageVolume = 0L;
        }
        try
        {
          string dataByDataTestName = fromWebContent.GetDataByDataTestName(web, "MARKET_CAP-value");
          string lower = dataByDataTestName.Substring(dataByDataTestName.Length - 1, 1).ToLower();
          float single = Convert.ToSingle(dataByDataTestName.Substring(0, dataByDataTestName.Length - 1));
          switch (lower)
          {
            case "th":
              fromWebContent.MarketCap = Convert.ToDouble(single * 1000f);
              break;
            case "m":
              fromWebContent.MarketCap = Convert.ToDouble(single * 1000000f);
              break;
            case "b":
              fromWebContent.MarketCap = Convert.ToDouble(single * 1E+09f);
              break;
            case "t":
              fromWebContent.MarketCap = Convert.ToDouble(single * 1E+12f);
              break;
          }
        }
        catch
        {
          fromWebContent.MarketCap = 0.0;
        }
        try
        {
          fromWebContent.Beta = new float?(Convert.ToSingle(fromWebContent.GetDataByDataTestName(web, "BETA_5Y-value")));
        }
        catch
        {
          fromWebContent.Beta = new float?();
        }
        try
        {
          fromWebContent.PriceEarningsRatio = new float?(Convert.ToSingle(fromWebContent.GetDataByDataTestName(web, "PE_RATIO-value")));
        }
        catch
        {
          fromWebContent.PriceEarningsRatio = new float?();
        }
        try
        {
          fromWebContent.EarningsPerShare = Convert.ToSingle(fromWebContent.GetDataByDataTestName(web, "EPS_RATIO-value"));
        }
        catch
        {
          fromWebContent.EarningsPerShare = 0.0f;
        }
        try
        {
          int num65 = web.IndexOf("EARNINGS_DATE-value");
          int num66 = web.IndexOf("<span", num65 + 1);
          int num67 = web.IndexOf(">", num66 + 1);
          int num68 = web.IndexOf("<", num67 + 1);
          string s = web.Substring(num67 + 1, num68 - num67 - 1);
          fromWebContent.EarningsDate = new DateTime?(DateTime.Parse(s));
        }
        catch
        {
          fromWebContent.EarningsDate = new DateTime?();
        }
        try
        {
          int num69 = web.IndexOf("DIVIDEND_AND_YIELD-value");
          int num70 = web.IndexOf(">", num69 + 1);
          int num71 = web.IndexOf("<", num70 + 1);
          string str = web.Substring(num70 + 1, num71 - num70 - 1);
          if (str == "N/A (N/A)")
          {
            fromWebContent.ForwardDividend = new float?();
            fromWebContent.ForwardDividendYield = new float?();
          }
          else
          {
            stringList.Clear();
            stringList.Add(" ");
            string[] strArray = str.Split(stringList.ToArray(), StringSplitOptions.None);
            fromWebContent.ForwardDividend = new float?(Convert.ToSingle(strArray[0]));
            fromWebContent.ForwardDividendYield = new float?(Convert.ToSingle(strArray[1].Replace("%", "").Replace("(", "").Replace(")", "")) / 100f);
          }
        }
        catch
        {
          fromWebContent.ForwardDividend = new float?(0.0f);
          fromWebContent.ForwardDividendYield = new float?(0.0f);
        }
        try
        {
          int num72 = web.IndexOf("EX_DIVIDEND_DATE-value");
          int num73 = web.IndexOf("<span", num72 + 1);
          int num74 = web.IndexOf(">", num73 + 1);
          int num75 = web.IndexOf("<", num74 + 1);
          string s = web.Substring(num74 + 1, num75 - num74 - 1);
          fromWebContent.ExDividendDate = new DateTime?(DateTime.Parse(s));
        }
        catch
        {
          fromWebContent.ExDividendDate = new DateTime?();
        }
        try
        {
          fromWebContent.YearTargetEstimate = Convert.ToSingle(fromWebContent.GetDataByDataTestName(web, "ONE_YEAR_TARGET_PRICE-value"));
        }
        catch
        {
          fromWebContent.YearTargetEstimate = 0.0f;
        }
      }
      return fromWebContent;
    }

    private string GetDataByClassName(string web_data, string class_name)
    {
      int num1 = web_data.IndexOf("class=\"" + class_name + "\"");
      int num2 = num1 != -1 ? web_data.IndexOf(">", num1 + 1) : throw new Exception("Unable to find class with name '" + class_name + "' in the web data.");
      int num3 = web_data.IndexOf("<", num2 + 1);
      return web_data.Substring(num2 + 1, num3 - num2 - 1);
    }

    private string GetDataByClassName2(string web_data, string class_name)
    {
      int startIndex1 = web_data.IndexOf("class=\"" + class_name + "\"");
      int num1 = startIndex1 != -1 ? web_data.IndexOf(">", startIndex1 + 1) : throw new Exception("Unable to find class with name '" + class_name + "' in the web data.");
      int startIndex2 = web_data.LastIndexOf("<", startIndex1);
      int num2 = web_data.IndexOf(" ", startIndex2);
      string str = web_data.Substring(startIndex2 + 1, num2 - startIndex2 - 1);
      int num3 = web_data.IndexOf("</" + str + ">", startIndex1 + 1);
      return web_data.Substring(num1 + 1, num3 - num1 - 1);
    }

    private string GetDataByClassNames(string web_data, params string[] class_names)
    {
      if (class_names.Length == 0)
        throw new Exception("You must provide at least one class name to the GetDataByClassNames method.");
      string dataByClassNames = "7b19ee342f3e4c8dade3fd1eec3f8b7a";
      foreach (string className in class_names)
      {
        if (dataByClassNames == "7b19ee342f3e4c8dade3fd1eec3f8b7a")
        {
          try
          {
            dataByClassNames = this.GetDataByClassName2(web_data, className);
          }
          catch
          {
            dataByClassNames = "7b19ee342f3e4c8dade3fd1eec3f8b7a";
          }
        }
      }
      if (dataByClassNames == "7b19ee342f3e4c8dade3fd1eec3f8b7a")
      {
        string str = "";
        foreach (string className in class_names)
          str = str + className + ", ";
        throw new Exception("Unable to find data with any of the following class names: " + str.Substring(0, str.Length - 2));
      }
      return dataByClassNames;
    }

    private string GetDataByDataTestName(string web_data, string data_test_name)
    {
      int num1 = web_data.IndexOf("data-test=\"" + data_test_name + "\"");
      int num2 = num1 != -1 ? web_data.IndexOf(">", num1 + 1) : throw new Exception("Unable to find data with data test name '" + web_data + "' inside web data.");
      int num3 = web_data.IndexOf("<", num2 + 1);
      return web_data.Substring(num2 + 1, num3 - num2 - 1);
    }

    private string GetDataByDataFieldName(string web_data, string data_field)
    {
      int num1 = web_data.IndexOf("data-field=\"" + data_field + "\"");
      int num2 = num1 != -1 ? web_data.IndexOf(">", num1 + 1) : throw new Exception("Unable to find data field '" + data_field + "' in web content");
      int num3 = web_data.IndexOf("<", num2 + 1);
      return web_data.Substring(num2 + 1, num3 - num2 - 1);
    }

    private string GetDataByDataFieldName2(string web_data, string data_field)
    {
      int startIndex1 = web_data.IndexOf("data-field=\"" + data_field + "\"");
      int num1 = startIndex1 != -1 ? web_data.IndexOf(">", startIndex1 + 1) : throw new Exception("Unable to find data field '" + data_field + "' in web content");
      int startIndex2 = web_data.LastIndexOf("<", startIndex1);
      int num2 = web_data.IndexOf(" ", startIndex2);
      string str = web_data.Substring(startIndex2 + 1, num2 - startIndex2 - 1);
      int num3 = web_data.IndexOf("</" + str + ">", startIndex1 + 1);
      return web_data.Substring(num1 + 1, num3 - num1 - 1);
    }

    private string HtmlCleanse(string input) => new Regex("<.*?>").Replace(input, "");
  }
}
