// Decompiled with JetBrains decompiler
// Type: Yahoo.Finance.StreamQuoteStore
// Assembly: Yahoo.Finance, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8859EB3C-3A3D-4D2A-8276-F613AD1DF364
// Assembly location: C:\Users\dagma\.nuget\packages\yahoo.finance\4.3.3\lib\netstandard2.0\Yahoo.Finance.dll

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Yahoo.Finance
{
  public class StreamQuoteStore
  {
    private string _Symbol;
    private JObject QuoteObj;

    public StreamQuoteStore(string symbol, JObject obj)
    {
      this._Symbol = symbol;
      this.QuoteObj = obj;
    }

    public string Symbol => this._Symbol;

    public float? Open
    {
      get
      {
        try
        {
          return new float?(Convert.ToSingle(JObject.Parse(this.QuoteObj.Property("regularMarketOpen").Value.ToString()).Property("raw").Value.ToString()));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? YearRangeLow
    {
      get
      {
        try
        {
          string str = JObject.Parse(this.QuoteObj.Property("fiftyTwoWeekRange").Value.ToString()).Property("raw").Value.ToString();
          int num1 = str.IndexOf(" ");
          int num2 = str.IndexOf(" ", num1 + 1);
          float single1 = Convert.ToSingle(str.Substring(0, num1 - 1));
          double single2 = (double) Convert.ToSingle(str.Substring(num2 + 1));
          return new float?(single1);
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? YearRangeHigh
    {
      get
      {
        try
        {
          string str = JObject.Parse(this.QuoteObj.Property("fiftyTwoWeekRange").Value.ToString()).Property("raw").Value.ToString();
          int num1 = str.IndexOf(" ");
          int num2 = str.IndexOf(" ", num1 + 1);
          double single = (double) Convert.ToSingle(str.Substring(0, num1 - 1));
          return new float?(Convert.ToSingle(str.Substring(num2 + 1)));
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
          return new float?(Convert.ToSingle(JObject.Parse(this.QuoteObj.Property("sharesOutstanding").Value.ToString()).Property("raw").Value.ToString()));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? DayHigh
    {
      get
      {
        try
        {
          return new float?(Convert.ToSingle(this.GetRawValueFromChildObject("regularMarketDayHigh")));
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
          return this.QuoteObj.Property("shortName").Value.ToString();
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
          return this.QuoteObj.Property("longName").Value.ToString();
        }
        catch
        {
          return (string) null;
        }
      }
    }

    public float? DayChange
    {
      get
      {
        try
        {
          return new float?(Convert.ToSingle(this.GetRawValueFromChildObject("regularMarketChange")));
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
          return new float?(Convert.ToSingle(this.GetRawValueFromChildObject("regularMarketPreviousClose")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public string ExchangeTimezone
    {
      get
      {
        try
        {
          return this.QuoteObj.Property("exchangeTimezoneShortName").Value.ToString();
        }
        catch
        {
          return (string) null;
        }
      }
    }

    public float? DayLow
    {
      get
      {
        try
        {
          return new float?(Convert.ToSingle(this.GetRawValueFromChildObject("regularMarketDayLow")));
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
          return this.QuoteObj.Property("currency").Value.ToString();
        }
        catch
        {
          return (string) null;
        }
      }
    }

    public float? Price
    {
      get
      {
        try
        {
          return new float?(Convert.ToSingle(this.GetRawValueFromChildObject("regularMarketPrice")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? Volume
    {
      get
      {
        try
        {
          return new float?(Convert.ToSingle(this.GetRawValueFromChildObject("regularMarketVolume")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public float? MarketCap
    {
      get
      {
        try
        {
          return new float?(Convert.ToSingle(this.GetRawValueFromChildObject("marketCap")));
        }
        catch
        {
          return new float?();
        }
      }
    }

    public string ExchangeName
    {
      get
      {
        try
        {
          return this.QuoteObj.Property("fullExchangeName").Value.ToString();
        }
        catch
        {
          return (string) null;
        }
      }
    }

    public string QuoteType
    {
      get
      {
        try
        {
          return this.QuoteObj.Property("quoteType").Value.ToString();
        }
        catch
        {
          return (string) null;
        }
      }
    }

    private string GetRawValueFromChildObject(string PropertyName)
    {
      JProperty jproperty = this.QuoteObj.Property(PropertyName);
      if (jproperty == null)
        throw new Exception("Unable to find property '" + PropertyName + "' in quote data.");
      return ((((JToken) jproperty).Type == 4 ? JObject.Parse(jproperty.Value.ToString()).Property("raw") : throw new Exception("Property '" + PropertyName + "' was not a child object.")) ?? throw new Exception("Property labeled 'raw' does not exist in the specified child object.")).Value.ToString();
    }

    public static StreamQuoteStore[] ExtractStreamQuoteStoresFromWebPage(string web_page_html) => StreamQuoteStore.ExtractStreamQuoteStoresFromRootJson(StoreExtractionToolkit.ExtractRootJsonFromWebpage(web_page_html));

    public static StreamQuoteStore[] ExtractStreamQuoteStoresFromRootJson(string root_json)
    {
      IEnumerable<JProperty> jproperties = JObject.Parse(JObject.Parse(JObject.Parse(JObject.Parse(JObject.Parse(JObject.Parse(root_json).Property("context").Value.ToString()).Property("dispatcher").Value.ToString()).Property("stores").Value.ToString()).Property("StreamDataStore").Value.ToString()).Property("quoteData").Value.ToString()).Properties();
      List<StreamQuoteStore> streamQuoteStoreList = new List<StreamQuoteStore>();
      foreach (JProperty jproperty in jproperties)
      {
        JObject jobject = JObject.Parse(jproperty.Value.ToString());
        streamQuoteStoreList.Add(new StreamQuoteStore(jproperty.Name, jobject));
      }
      return streamQuoteStoreList.ToArray();
    }
  }
}
