// Decompiled with JetBrains decompiler
// Type: Yahoo.Finance.QuoteSummaryStore
// Assembly: Yahoo.Finance, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8859EB3C-3A3D-4D2A-8276-F613AD1DF364
// Assembly location: C:\Users\dagma\.nuget\packages\yahoo.finance\4.3.3\lib\netstandard2.0\Yahoo.Finance.dll

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Yahoo.Finance
{
  public class QuoteSummaryStore
  {
    private JObject QuoteSummaryStoreObj;

    public QuoteSummaryStore(JObject quote_summary_store_obj) => this.QuoteSummaryStoreObj = quote_summary_store_obj;

    public string Symbol => this.QuoteSummaryStoreObj.Property("symbol").Value.ToString();

    public string BusinessSummary
    {
      get
      {
        try
        {
          return Extensions.Value<string>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("summaryProfile.longBusinessSummary"));
        }
        catch
        {
          return (string) null;
        }
      }
    }

    public int? FullTimeEmployees
    {
      get
      {
        try
        {
          return new int?(Extensions.Value<int>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("summaryProfile.fullTimeEmployees")));
        }
        catch
        {
          return new int?();
        }
      }
    }

    public string Sector
    {
      get
      {
        try
        {
          return Extensions.Value<string>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("summaryProfile.sector"));
        }
        catch
        {
          return (string) null;
        }
      }
    }

    public string Industry
    {
      get
      {
        try
        {
          return Extensions.Value<string>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("summaryProfile.industry"));
        }
        catch
        {
          return (string) null;
        }
      }
    }

    public float? Open
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("price.regularMarketOpen.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public int? AverageVolume90Day
    {
      get
      {
        try
        {
          return new int?(Extensions.Value<int>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("price.averageDailyVolume3Month.raw")));
        }
        catch
        {
          return new int?();
        }
      }
    }

    public string Exchange
    {
      get
      {
        try
        {
          return Extensions.Value<string>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("price.exchange"));
        }
        catch
        {
          return (string) null;
        }
      }
    }

    public float? DayHigh
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("price.regularMarketDayHigh.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public string ShortName
    {
      get
      {
        try
        {
          return Extensions.Value<string>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("price.shortName"));
        }
        catch
        {
          return (string) null;
        }
      }
    }

    public string LongName
    {
      get
      {
        try
        {
          return Extensions.Value<string>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("price.longName"));
        }
        catch
        {
          return (string) null;
        }
      }
    }

    public float? Change
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("price.regularMarketChange.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? PreviousClose
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("price.regularMarketPreviousClose.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? Price
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("price.regularMarketPrice.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public string Currency
    {
      get
      {
        try
        {
          return Extensions.Value<string>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("price.currency"));
        }
        catch
        {
          return (string) null;
        }
      }
    }

    public int? Volume
    {
      get
      {
        try
        {
          return new int?(Extensions.Value<int>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("price.regularMarketVolume.raw")));
        }
        catch
        {
          return new int?();
        }
      }
    }

    public float? MarketCap
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("price.marketCap.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? ChangePercent
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("price.regularMarketChangePercent.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? DayLow
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("price.regularMarketDayLow.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? BidPrice
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("summaryDetail.bid.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public int? BidQuantity
    {
      get
      {
        try
        {
          return new int?(Extensions.Value<int>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("summaryDetail.bidSize.raw")));
        }
        catch
        {
          return new int?();
        }
      }
    }

    public float? AskPrice
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("summaryDetail.ask.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public int? AskQuantity
    {
      get
      {
        try
        {
          return new int?(Extensions.Value<int>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("summaryDetail.askSize.raw")));
        }
        catch
        {
          return new int?();
        }
      }
    }

    public int? AverageVolume10Day
    {
      get
      {
        try
        {
          return new int?(Extensions.Value<int>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("summaryDetail.averageDailyVolume10Day.raw")));
        }
        catch
        {
          return new int?();
        }
      }
    }

    public float? YearLow
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("summaryDetail.fiftyTwoWeekLow.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? YearHigh
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("summaryDetail.fiftyTwoWeekHigh.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? Beta
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("summaryDetail.beta.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? PriceEarningsRatio
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("summaryDetail.trailingPE.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? EarningsPerShare
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("defaultKeyStatistics.trailingEps.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public DateTime? EarningsDate
    {
      get
      {
        try
        {
          return new DateTime?(DateTime.Parse(Extensions.Value<string>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("earnings.earningsChart.earningsDate[0].fmt"))));
        }
        catch
        {
          return new DateTime?();
        }
      }
    }

    public float? ForwardDividend
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("summaryDetail.dividendRate.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? ForwardDividendYield
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("summaryDetail.dividendYield.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public DateTime? ExDividendDate
    {
      get
      {
        try
        {
          return new DateTime?(DateTime.Parse(Extensions.Value<string>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("summaryDetail.exDividendDate.fmt"))));
        }
        catch
        {
          return new DateTime?();
        }
      }
    }

    public float? YearTargetEstimate
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("financialData.targetMeanPrice.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public DateTime? LastFiscalYearEnd
    {
      get
      {
        try
        {
          return new DateTime?(DateTime.Parse(Extensions.Value<string>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("defaultKeyStatistics.lastFiscalYearEnd.fmt"))));
        }
        catch
        {
          return new DateTime?();
        }
      }
    }

    public DateTime? LastFiscalQuarterEnd
    {
      get
      {
        try
        {
          return new DateTime?(DateTime.Parse(Extensions.Value<string>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("defaultKeyStatistics.mostRecentQuarter.fmt"))));
        }
        catch
        {
          return new DateTime?();
        }
      }
    }

    public float? ProfitMargin
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("defaultKeyStatistics.profitMargins.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? OperatingMargin
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("financialData.operatingMargins.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? ReturnOnAssets
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("financialData.returnOnAssets.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? ReturnOnEquity
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("financialData.returnOnEquity.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? Revenue
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("financialData.totalRevenue.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? RevenuePerShare
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("financialData.revenuePerShare.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? QuarterlyRevenueGrowth
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("financialData.revenueGrowth.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? GrossProfit
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("financialData.grossProfits.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? EDBITDA
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("financialData.ebitda.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? NetIncomeAvailableToCommon
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("defaultKeyStatistics.netIncomeToCommon.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? QuarterlyEarningsGrowth
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("defaultKeyStatistics.earningsQuarterlyGrowth.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? Cash
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("financialData.totalCash.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? CashPerShare
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("financialData.totalCashPerShare.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? Debt
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("financialData.totalDebt.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? DebtToEquityRatio
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("financialData.debtToEquity.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? CurrentRatio
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("financialData.currentRatio.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? BookValuePerShare
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("defaultKeyStatistics.bookValue.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? OperatingCashFlow
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("financialData.operatingCashflow.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? LeveredFreeCashFlow
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("financialData.freeCashflow.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? YearChangePercent
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("defaultKeyStatistics.52WeekChange.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? SP500YearChangePercent
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("defaultKeyStatistics.SandP52WeekChange.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? MovingAverage50Day
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("summaryDetail.fiftyDayAverage.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? MovingAverage200Day
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("summaryDetail.twoHundredDayAverage.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? SharesOutstanding
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("defaultKeyStatistics.sharesOutstanding.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? Float
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("defaultKeyStatistics.floatShares.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? PercentHeldByInsiders
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("defaultKeyStatistics.heldPercentInsiders.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? PercentHeldByInstitutions
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("defaultKeyStatistics.heldPercentInstitutions.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? SharesShort
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("defaultKeyStatistics.sharesShort.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? ShortRatio
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("defaultKeyStatistics.shortRatio.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? ShortPercentOfFloat
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("defaultKeyStatistics.shortPercentOfFloat.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? ShortPercentOfSharesOutstanding
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("defaultKeyStatistics.sharesPercentSharesOut.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? ForwardAnnualDividend
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("summaryDetail.dividendRate.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? ForwardAnnualDividendYield
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("summaryDetail.dividendYield.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? TrailingAnnualDividend
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("summaryDetail.trailingAnnualDividendRate.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? TrailingAnnualDividendYield
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("summaryDetail.trailingAnnualDividendYield.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? FiveYearAverageDividendYield
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("summaryDetail.fiveYearAvgDividendYield.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? DividendPayoutRatio
    {
      get
      {
        try
        {
          return new float?(Extensions.Value<float>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("summaryDetail.payoutRatio.raw")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public DateTime? DividendDate
    {
      get
      {
        try
        {
          return new DateTime?(DateTime.Parse(Extensions.Value<string>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("calendarEvents.dividendDate.fmt"))));
        }
        catch
        {
          return new DateTime?();
        }
      }
    }

    public string LastSplitFactor
    {
      get
      {
        try
        {
          return Extensions.Value<string>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("defaultKeyStatistics.lastSplitFactor"));
        }
        catch
        {
          return (string) null;
        }
      }
    }

    public DateTime? LastSplitDate
    {
      get
      {
        try
        {
          return new DateTime?(DateTime.Parse(Extensions.Value<string>((IEnumerable<JToken>) ((JToken) this.QuoteSummaryStoreObj).SelectToken("defaultKeyStatistics.lastSplitDate.fmt"))));
        }
        catch
        {
          return new DateTime?();
        }
      }
    }

    public static QuoteSummaryStore CreateFromRootJson(string json) => new QuoteSummaryStore(JObject.Parse(JObject.Parse(JObject.Parse(JObject.Parse(JObject.Parse(json).Property("context").Value.ToString()).Property("dispatcher").Value.ToString()).Property("stores").Value.ToString()).Property(nameof (QuoteSummaryStore)).Value.ToString()));

    public static QuoteSummaryStore CreateFromWebpage(string webpage_html) => QuoteSummaryStore.CreateFromRootJson(StoreExtractionToolkit.ExtractRootJsonFromWebpage(webpage_html));

    public static async Task<QuoteSummaryStore> DownloadAsync(string symbol) => QuoteSummaryStore.CreateFromWebpage(await (await new HttpClient().GetAsync("https://finance.yahoo.com/quote/" + symbol)).Content.ReadAsStringAsync());
  }
}
