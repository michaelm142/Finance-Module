// Decompiled with JetBrains decompiler
// Type: Yahoo.Finance.EquityScreener
// Assembly: Yahoo.Finance, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8859EB3C-3A3D-4D2A-8276-F613AD1DF364
// Assembly location: C:\Users\dagma\.nuget\packages\yahoo.finance\4.3.3\lib\netstandard2.0\Yahoo.Finance.dll

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Yahoo.Finance
{
  public class EquityScreener
  {
    public async Task<EquitySummaryData[]> GetEquitiesAsync(EquityScreen type)
    {
      string requestUri = "";
      switch (type)
      {
        case EquityScreen.Gainers:
          requestUri = "https://finance.yahoo.com/gainers";
          break;
        case EquityScreen.Losers:
          requestUri = "https://finance.yahoo.com/losers";
          break;
        case EquityScreen.MostActives:
          requestUri = "https://finance.yahoo.com/most-active";
          break;
        case EquityScreen.Trending:
          requestUri = "https://finance.yahoo.com/trending-tickers";
          break;
        case EquityScreen.TopEtfs:
          requestUri = "https://finance.yahoo.com/etfs";
          break;
      }
      return this.GetEquitySummariesFromWebData(await (await new HttpClient().GetAsync(requestUri)).Content.ReadAsStringAsync());
    }

    private EquitySummaryData[] GetEquitySummariesFromWebData(string web_data)
    {
      List<string> stringList = new List<string>();
      int num1 = web_data.IndexOf("<tbody");
      int num2 = web_data.IndexOf("</tbody>", num1 + 1);
      string str = web_data.Substring(num1 + 1, num2 - num1 - 1);
      stringList.Clear();
      stringList.Add("<tr class");
      string[] array = stringList.ToArray();
      string[] strArray1 = str.Split(array, StringSplitOptions.None);
      List<EquitySummaryData> equitySummaryDataList = new List<EquitySummaryData>();
      for (int index = 1; index < strArray1.Length; ++index)
      {
        EquitySummaryData equitySummaryData = new EquitySummaryData();
        stringList.Clear();
        stringList.Add("<td ");
        string[] strArray2 = strArray1[index].Split(stringList.ToArray(), StringSplitOptions.None);
        try
        {
          int num3 = strArray2[1].IndexOf("a href");
          int num4 = strArray2[1].IndexOf(">", num3 + 1);
          int num5 = strArray2[1].IndexOf("<", num4 + 1);
          equitySummaryData.StockSymbol = strArray2[1].Substring(num4 + 1, num5 - num4 - 1);
        }
        catch
        {
          equitySummaryData.StockSymbol = "?";
        }
        try
        {
          int num6 = strArray2[2].IndexOf("react-text");
          int num7 = strArray2[2].IndexOf(">", num6 + 1);
          int num8 = strArray2[2].IndexOf("<", num7 + 1);
          equitySummaryData.Name = strArray2[2].Substring(num7 + 1, num8 - num7 - 1);
          equitySummaryData.Name = equitySummaryData.Name.Replace("&#x27;", "'");
          equitySummaryData.Name = equitySummaryData.Name.Replace("&amp;", "&");
        }
        catch
        {
          equitySummaryData.Name = "";
        }
        try
        {
          int num9 = strArray2[3].IndexOf("span class");
          int num10 = strArray2[3].IndexOf(">", num9 + 1);
          int num11 = strArray2[3].IndexOf("<", num10 + 1);
          equitySummaryData.Price = Convert.ToSingle(strArray2[3].Substring(num10 + 1, num11 - num10 - 1));
        }
        catch
        {
          equitySummaryData.Price = 0.0f;
        }
        try
        {
          int num12 = strArray2[4].IndexOf("span class");
          int num13 = strArray2[4].IndexOf(">", num12 + 1);
          int num14 = strArray2[4].IndexOf("<", num13 + 1);
          equitySummaryData.DollarChange = Convert.ToSingle(strArray2[4].Substring(num13 + 1, num14 - num13 - 1).Replace("+", ""));
        }
        catch
        {
          equitySummaryData.DollarChange = 0.0f;
        }
        try
        {
          int num15 = strArray2[5].IndexOf("span class");
          int num16 = strArray2[5].IndexOf(">", num15 + 1);
          int num17 = strArray2[5].IndexOf("<", num16 + 1);
          float num18 = Convert.ToSingle(strArray2[5].Substring(num16 + 1, num17 - num16 - 1).Replace("+", "").Replace("%", "")) / 100f;
          equitySummaryData.PercentChange = num18;
        }
        catch
        {
          equitySummaryData.PercentChange = 0.0f;
        }
        try
        {
          int num19 = strArray2[6].IndexOf("span class");
          int num20 = strArray2[6].IndexOf(">", num19 + 1);
          int num21 = strArray2[6].IndexOf("<", num20 + 1);
          string representation = strArray2[6].Substring(num20 + 1, num21 - num20 - 1);
          equitySummaryData.Volume = (long) Convert.ToInt32(YahooFinanceToolkit.GetMarketCapFromString(representation));
        }
        catch
        {
          equitySummaryData.Volume = 0L;
        }
        try
        {
          int num22 = strArray2[7].IndexOf("react-text");
          int num23 = strArray2[7].IndexOf(">", num22 + 1);
          int num24 = strArray2[7].IndexOf("<", num23 + 1);
          string representation = strArray2[7].Substring(num23 + 1, num24 - num23 - 1);
          equitySummaryData.AverageVolume = (long) Convert.ToInt32(YahooFinanceToolkit.GetMarketCapFromString(representation));
        }
        catch
        {
          equitySummaryData.AverageVolume = 0L;
        }
        try
        {
          int num25 = strArray2[8].IndexOf("span class");
          int num26 = strArray2[8].IndexOf(">", num25 + 1);
          int num27 = strArray2[8].IndexOf("<", num26 + 1);
          string representation = strArray2[8].Substring(num26 + 1, num27 - num26 - 1);
          equitySummaryData.MarketCap = YahooFinanceToolkit.GetMarketCapFromString(representation);
        }
        catch
        {
          equitySummaryData.MarketCap = 0.0;
        }
        try
        {
          int num28 = strArray2[9].IndexOf("react-text");
          int num29 = strArray2[9].IndexOf(">", num28 + 1);
          int num30 = strArray2[9].IndexOf("<", num29 + 1);
          strArray2[9].Substring(num29 + 1, num30 - num29 - 1);
          equitySummaryData.PriceEarningsRatio = new float?(Convert.ToSingle(strArray2[9].Substring(num29 + 1, num30 - num29 - 1)));
        }
        catch
        {
          equitySummaryData.PriceEarningsRatio = new float?();
        }
        equitySummaryDataList.Add(equitySummaryData);
      }
      return equitySummaryDataList.ToArray();
    }
  }
}
