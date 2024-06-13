// Decompiled with JetBrains decompiler
// Type: Yahoo.Finance.EquityStatisticalData
// Assembly: Yahoo.Finance, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8859EB3C-3A3D-4D2A-8276-F613AD1DF364
// Assembly location: C:\Users\dagma\.nuget\packages\yahoo.finance\4.3.3\lib\netstandard2.0\Yahoo.Finance.dll

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Yahoo.Finance
{
  public class EquityStatisticalData : EquityData
  {
    public DateTime? FiscalYearEnds { get; set; }

    public DateTime? MostRecentQuarter { get; set; }

    public float? ProfitMargin { get; set; }

    public float? OperatingMargin { get; set; }

    public float? ReturnOnAssets { get; set; }

    public float? ReturnOnEquity { get; set; }

    public long? Revenue { get; set; }

    public float? RevenuePerShare { get; set; }

    public float? QuarterlyRevenueGrowth { get; set; }

    public long? GrossProfit { get; set; }

    public long? EBITDA { get; set; }

    public long? NetIncomeAvailableToCommon { get; set; }

    public float? DilutedEps { get; set; }

    public float? QuarterlyEarningsGrowth { get; set; }

    public long? TotalCash { get; set; }

    public float? TotalCashPerShare { get; set; }

    public long? TotalDebt { get; set; }

    public float? TotalDebtEquityRatio { get; set; }

    public float? CurrentRatio { get; set; }

    public float? BookValuePerShare { get; set; }

    public long? OperatingCashFlow { get; set; }

    public long? LeveredFreeCashFlow { get; set; }

    public float? Beta { get; set; }

    public float? YearChange { get; set; }

    public float? SP500YearChange { get; set; }

    public float? YearHigh { get; set; }

    public float? YearLow { get; set; }

    public float? MovingAverage50Day { get; set; }

    public float? MovingAverage200Day { get; set; }

    public long? AverageVolume3Month { get; set; }

    public long? AverageVolume10Day { get; set; }

    public long? SharesOutstanding { get; set; }

    public long? Float { get; set; }

    public float? PercentHeldByInsiders { get; set; }

    public float? PercentHeldByInstitutions { get; set; }

    public long? SharesShort { get; set; }

    public float? ShortRatio { get; set; }

    public float? ShortPercentOfFloat { get; set; }

    public float? ShortPercentOfSharesOutstanding { get; set; }

    public float? ForwardAnnualDividend { get; set; }

    public float? ForwardAnnualDividendYield { get; set; }

    public float? TrailingAnnualDividend { get; set; }

    public float? TrailingAnnualDividendYield { get; set; }

    public float? FiveYearAverageDividendYield { get; set; }

    public float DividendPayoutRatio { get; set; }

    public DateTime? DividendDate { get; set; }

    public DateTime? ExDividendDate { get; set; }

    public string LastSplitFactor { get; set; }

    public DateTime? LastSplitDate { get; set; }

    public static async Task<EquityStatisticalData> CreateAsync(string symbol)
    {
      EquityStatisticalData ToReturn = new EquityStatisticalData();
      ToReturn.QueriedSymbol = symbol.Trim().ToUpper();
      string web = await (await new HttpClient().GetAsync("https://finance.yahoo.com/quote/" + symbol.Trim().ToLower() + "/key-statistics?p=" + symbol.Trim().ToLower())).Content.ReadAsStringAsync();
      if (web.Split(new List<string>() { "No results for " }.ToArray(), StringSplitOptions.None).Length > 2)
        throw new Exception("Symbol '" + symbol.ToUpper().Trim() + "' is invalid.");
      try
      {
        ToReturn.FiscalYearEnds = new DateTime?(DateTime.Parse(ToReturn.GetValueByDisplayName(web, "Fiscal Year Ends")));
      }
      catch
      {
        ToReturn.FiscalYearEnds = new DateTime?();
      }
      try
      {
        ToReturn.MostRecentQuarter = new DateTime?(DateTime.Parse(ToReturn.GetValueByDisplayName(web, "Most Recent Quarter")));
      }
      catch
      {
        ToReturn.MostRecentQuarter = new DateTime?();
      }
      try
      {
        ToReturn.ProfitMargin = new float?(ToReturn.PercentStringToPercentFloat(ToReturn.GetValueByDisplayName(web, "Profit Margin")));
      }
      catch
      {
        ToReturn.ProfitMargin = new float?();
      }
      try
      {
        ToReturn.OperatingMargin = new float?(ToReturn.PercentStringToPercentFloat(ToReturn.GetValueByDisplayName(web, "Operating Margin")));
      }
      catch
      {
        ToReturn.OperatingMargin = new float?();
      }
      try
      {
        ToReturn.ReturnOnAssets = new float?(ToReturn.PercentStringToPercentFloat(ToReturn.GetValueByDisplayName(web, "Return on Assets")));
      }
      catch
      {
        ToReturn.ReturnOnAssets = new float?();
      }
      try
      {
        ToReturn.ReturnOnEquity = new float?(ToReturn.PercentStringToPercentFloat(ToReturn.GetValueByDisplayName(web, "Return on Equity")));
      }
      catch
      {
        ToReturn.ReturnOnEquity = new float?();
      }
      try
      {
        ToReturn.Revenue = new long?(ToReturn.LongStringToLong(ToReturn.GetValueByDisplayName(web, "Revenue")));
      }
      catch
      {
        ToReturn.Revenue = new long?();
      }
      try
      {
        ToReturn.RevenuePerShare = new float?(Convert.ToSingle(ToReturn.GetValueByDisplayName(web, "Revenue Per Share")));
      }
      catch
      {
        ToReturn.RevenuePerShare = new float?();
      }
      try
      {
        ToReturn.QuarterlyRevenueGrowth = new float?(ToReturn.PercentStringToPercentFloat(ToReturn.GetValueByDisplayName(web, "Quarterly Revenue Growth")));
      }
      catch
      {
        ToReturn.QuarterlyRevenueGrowth = new float?();
      }
      try
      {
        ToReturn.GrossProfit = new long?(ToReturn.LongStringToLong(ToReturn.GetValueByDisplayName(web, "Gross Profit")));
      }
      catch
      {
        ToReturn.GrossProfit = new long?();
      }
      try
      {
        ToReturn.EBITDA = new long?(ToReturn.LongStringToLong(ToReturn.GetValueByDisplayName(web, "EBITDA")));
      }
      catch
      {
        ToReturn.EBITDA = new long?();
      }
      try
      {
        ToReturn.NetIncomeAvailableToCommon = new long?(ToReturn.LongStringToLong(ToReturn.GetValueByDisplayName(web, "Net Income Avi to Common")));
      }
      catch
      {
        ToReturn.NetIncomeAvailableToCommon = new long?();
      }
      try
      {
        ToReturn.DilutedEps = new float?(Convert.ToSingle(ToReturn.GetValueByDisplayName(web, "Diluted EPS")));
      }
      catch
      {
        ToReturn.DilutedEps = new float?();
      }
      try
      {
        ToReturn.QuarterlyEarningsGrowth = new float?(ToReturn.PercentStringToPercentFloat(ToReturn.GetValueByDisplayName(web, "Quarterly Earnings Growth")));
      }
      catch
      {
        ToReturn.QuarterlyEarningsGrowth = new float?();
      }
      try
      {
        ToReturn.TotalCash = new long?(ToReturn.LongStringToLong(ToReturn.GetValueByDisplayName(web, "Total Cash")));
      }
      catch
      {
        ToReturn.TotalCash = new long?();
      }
      try
      {
        ToReturn.TotalCashPerShare = new float?(Convert.ToSingle(ToReturn.GetValueByDisplayName(web, "Total Cash Per Share")));
      }
      catch
      {
        ToReturn.TotalCashPerShare = new float?();
      }
      try
      {
        ToReturn.TotalDebt = new long?(ToReturn.LongStringToLong(ToReturn.GetValueByDisplayName(web, "Total Debt")));
      }
      catch
      {
        ToReturn.TotalDebt = new long?();
      }
      try
      {
        ToReturn.TotalDebtEquityRatio = new float?(Convert.ToSingle(ToReturn.GetValueByDisplayName(web, "Total Debt/Equity")));
      }
      catch
      {
        ToReturn.TotalDebtEquityRatio = new float?();
      }
      try
      {
        ToReturn.CurrentRatio = new float?(Convert.ToSingle(ToReturn.GetValueByDisplayName(web, "Current Ratio")));
      }
      catch
      {
        ToReturn.CurrentRatio = new float?();
      }
      try
      {
        ToReturn.BookValuePerShare = new float?(Convert.ToSingle(ToReturn.GetValueByDisplayName(web, "Book Value Per Share")));
      }
      catch
      {
        ToReturn.BookValuePerShare = new float?();
      }
      try
      {
        ToReturn.OperatingCashFlow = new long?(ToReturn.LongStringToLong(ToReturn.GetValueByDisplayName(web, "Operating Cash Flow")));
      }
      catch
      {
        ToReturn.OperatingCashFlow = new long?();
      }
      try
      {
        ToReturn.LeveredFreeCashFlow = new long?(ToReturn.LongStringToLong(ToReturn.GetValueByDisplayName(web, "Levered Free Cash Flow")));
      }
      catch
      {
        ToReturn.LeveredFreeCashFlow = new long?();
      }
      try
      {
        ToReturn.Beta = new float?(Convert.ToSingle(ToReturn.GetValueByDisplayName(web, "Beta (5Y Monthly)")));
      }
      catch
      {
        ToReturn.Beta = new float?();
      }
      try
      {
        ToReturn.YearChange = new float?(ToReturn.PercentStringToPercentFloat(ToReturn.GetValueByDisplayName(web, "52-Week Change")));
      }
      catch
      {
        ToReturn.YearChange = new float?();
      }
      try
      {
        ToReturn.SP500YearChange = new float?(ToReturn.PercentStringToPercentFloat(ToReturn.GetValueByDisplayName(web, "S&amp;P500 52-Week Change")));
      }
      catch
      {
        ToReturn.SP500YearChange = new float?();
      }
      try
      {
        ToReturn.YearHigh = new float?(Convert.ToSingle(ToReturn.GetValueByDisplayName(web, "52 Week High")));
      }
      catch
      {
        ToReturn.YearHigh = new float?();
      }
      try
      {
        ToReturn.YearLow = new float?(Convert.ToSingle(ToReturn.GetValueByDisplayName(web, "52 Week Low")));
      }
      catch
      {
        ToReturn.YearLow = new float?();
      }
      try
      {
        ToReturn.MovingAverage50Day = new float?(Convert.ToSingle(ToReturn.GetValueByDisplayName(web, "50-Day Moving Average")));
      }
      catch
      {
        ToReturn.MovingAverage50Day = new float?();
      }
      try
      {
        ToReturn.MovingAverage200Day = new float?(Convert.ToSingle(ToReturn.GetValueByDisplayName(web, "200-Day Moving Average")));
      }
      catch
      {
        ToReturn.MovingAverage200Day = new float?();
      }
      try
      {
        ToReturn.AverageVolume3Month = new long?(ToReturn.LongStringToLong(ToReturn.GetValueByDisplayName(web, "Avg Vol (3 month)")));
      }
      catch
      {
        ToReturn.AverageVolume3Month = new long?();
      }
      try
      {
        ToReturn.AverageVolume10Day = new long?(ToReturn.LongStringToLong(ToReturn.GetValueByDisplayName(web, "Avg Vol (10 day)")));
      }
      catch
      {
        ToReturn.AverageVolume10Day = new long?();
      }
      try
      {
        ToReturn.SharesOutstanding = new long?(ToReturn.LongStringToLong(ToReturn.GetValueByDisplayName(web, "Shares Outstanding")));
      }
      catch
      {
        ToReturn.SharesOutstanding = new long?();
      }
      try
      {
        ToReturn.Float = new long?(ToReturn.LongStringToLong(ToReturn.GetValueByDisplayName(web, "Float")));
      }
      catch
      {
        ToReturn.Float = new long?();
      }
      try
      {
        ToReturn.PercentHeldByInsiders = new float?(ToReturn.PercentStringToPercentFloat(ToReturn.GetValueByDisplayName(web, "% Held by Insiders")));
      }
      catch
      {
        ToReturn.PercentHeldByInsiders = new float?();
      }
      try
      {
        ToReturn.PercentHeldByInstitutions = new float?(ToReturn.PercentStringToPercentFloat(ToReturn.GetValueByDisplayName(web, "% Held by Institutions")));
      }
      catch
      {
        ToReturn.PercentHeldByInstitutions = new float?();
      }
      try
      {
        ToReturn.SharesShort = new long?(ToReturn.LongStringToLong(ToReturn.GetValueByDisplayNameLead(web, "Shares Short")));
      }
      catch
      {
        ToReturn.SharesShort = new long?();
      }
      try
      {
        ToReturn.ShortRatio = new float?(Convert.ToSingle(ToReturn.GetValueByDisplayNameLead(web, "Short Ratio")));
      }
      catch
      {
        ToReturn.ShortRatio = new float?();
      }
      try
      {
        ToReturn.ShortPercentOfFloat = new float?(ToReturn.PercentStringToPercentFloat(ToReturn.GetValueByDisplayNameLead(web, "Short % of Float")));
      }
      catch
      {
        ToReturn.ShortPercentOfSharesOutstanding = new float?();
      }
      try
      {
        ToReturn.ShortPercentOfSharesOutstanding = new float?(ToReturn.PercentStringToPercentFloat(ToReturn.GetValueByDisplayNameLead(web, "Short % of Shares Outstanding")));
      }
      catch
      {
        ToReturn.ShortPercentOfSharesOutstanding = new float?();
      }
      try
      {
        ToReturn.ForwardAnnualDividend = new float?(Convert.ToSingle(ToReturn.GetValueByDisplayName(web, "Forward Annual Dividend Rate")));
      }
      catch
      {
        ToReturn.ForwardAnnualDividend = new float?();
      }
      try
      {
        ToReturn.ForwardAnnualDividendYield = new float?(ToReturn.PercentStringToPercentFloat(ToReturn.GetValueByDisplayName(web, "Forward Annual Dividend Yield")));
      }
      catch
      {
        ToReturn.ForwardAnnualDividendYield = new float?();
      }
      try
      {
        ToReturn.TrailingAnnualDividend = new float?(Convert.ToSingle(ToReturn.GetValueByDisplayName(web, "Trailing Annual Dividend Rate")));
      }
      catch
      {
        ToReturn.TrailingAnnualDividend = new float?();
      }
      try
      {
        ToReturn.TrailingAnnualDividendYield = new float?(ToReturn.PercentStringToPercentFloat(ToReturn.GetValueByDisplayName(web, "Trailing Annual Dividend Yield")));
      }
      catch
      {
        ToReturn.TrailingAnnualDividendYield = new float?();
      }
      try
      {
        ToReturn.FiveYearAverageDividendYield = new float?(Convert.ToSingle(ToReturn.GetValueByDisplayName(web, "5 Year Average Dividend Yield")));
      }
      catch
      {
        ToReturn.FiveYearAverageDividendYield = new float?();
      }
      try
      {
        ToReturn.DividendPayoutRatio = ToReturn.PercentStringToPercentFloat(ToReturn.GetValueByDisplayName(web, "Payout Ratio"));
      }
      catch
      {
        ToReturn.DividendPayoutRatio = 0.0f;
      }
      try
      {
        ToReturn.DividendDate = new DateTime?(DateTime.Parse(ToReturn.GetValueByDisplayName(web, "Dividend Date")));
      }
      catch
      {
        ToReturn.DividendDate = new DateTime?();
      }
      try
      {
        ToReturn.ExDividendDate = new DateTime?(DateTime.Parse(ToReturn.GetValueByDisplayName(web, "Ex-Dividend Date")));
      }
      catch
      {
      }
      try
      {
        ToReturn.LastSplitFactor = ToReturn.GetValueByDisplayName(web, "Last Split Factor");
      }
      catch
      {
        ToReturn.LastSplitFactor = (string) null;
      }
      try
      {
        ToReturn.LastSplitDate = new DateTime?(DateTime.Parse(ToReturn.GetValueByDisplayName(web, "Last Split Date")));
      }
      catch
      {
        ToReturn.LastSplitDate = new DateTime?();
      }
      EquityStatisticalData async = ToReturn;
      ToReturn = (EquityStatisticalData) null;
      return async;
    }

    private string GetValueByDisplayName(string web, string display_name)
    {
      int num1 = web.IndexOf(">" + display_name + "<");
      int num2 = num1 != -1 ? web.IndexOf("<td class=", num1 + 1) : throw new Exception("Unable to find '" + display_name + "' in supplied source.");
      int num3 = web.IndexOf(">", num2 + 1);
      int num4 = web.IndexOf("<", num3 + 1);
      return web.Substring(num3 + 1, num4 - num3 - 1);
    }

    private float PercentStringToPercentFloat(string percent_string) => Convert.ToSingle(percent_string.Replace("%", "")) / 100f;

    private long LongStringToLong(string long_string)
    {
      float single;
      try
      {
        single = Convert.ToSingle(long_string.Substring(0, long_string.Length - 1));
      }
      catch
      {
        throw new Exception("Unable to convert value '" + long_string + "' to a long value (using LongStringToLong method).");
      }
      string str = long_string.Substring(long_string.Length - 1, 1).ToLower().Trim();
      long num = 0;
      switch (str)
      {
        case "h":
          num = 1000L;
          break;
        case "m":
          num = 1000000L;
          break;
        case "b":
          num = 1000000000L;
          break;
        case "t":
          num = 1000000000000L;
          break;
      }
      return Convert.ToInt64(single * (float) num);
    }

    private string GetValueByDisplayNameLead(string web, string display_name_lead)
    {
      int num1 = web.IndexOf(">" + display_name_lead);
      int num2 = num1 != -1 ? web.IndexOf("<td class=", num1 + 1) : throw new Exception("Unable to find '" + display_name_lead + "' in supplied source.");
      int num3 = web.IndexOf(">", num2 + 1);
      int num4 = web.IndexOf("<", num3 + 1);
      return web.Substring(num3 + 1, num4 - num3 - 1);
    }
  }
}
