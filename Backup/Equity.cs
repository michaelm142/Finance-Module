// Decompiled with JetBrains decompiler
// Type: Yahoo.Finance.Equity
// Assembly: Yahoo.Finance, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8859EB3C-3A3D-4D2A-8276-F613AD1DF364
// Assembly location: C:\Users\dagma\.nuget\packages\yahoo.finance\4.3.3\lib\netstandard2.0\Yahoo.Finance.dll

using System;
using System.Threading.Tasks;

namespace Yahoo.Finance
{
  public class Equity
  {
    public string Symbol { get; set; }

    public EquitySummaryData Summary { get; set; }

    public EquityStatisticalData Statistics { get; set; }

    public static Equity Create(string stock_symbol) => new Equity()
    {
      Symbol = stock_symbol
    };

    public async Task DownloadSummaryAsync()
    {
      int num;
      if (num != 0 && this.Symbol == "")
        throw new Exception("Stock symbol not provided.");
      try
      {
        this.Summary = await EquitySummaryData.CreateAsync(this.Symbol);
      }
      catch
      {
        throw new Exception("Fatal error while downloading summary data.");
      }
    }

    public async Task DownloadStatisticsAsync()
    {
      int num;
      if (num != 0 && this.Symbol == "")
        throw new Exception("Stock symbol not provided.");
      try
      {
        this.Statistics = await EquityStatisticalData.CreateAsync(this.Symbol);
      }
      catch
      {
        throw new Exception("Fatal error while downloading statistic data.");
      }
    }

    private string GetDataByClassName(string web_data, string class_name)
    {
      int num1 = web_data.IndexOf("class=\"" + class_name + "\"");
      int num2 = num1 != -1 ? web_data.IndexOf(">", num1 + 1) : throw new Exception("Unable to find class with name '" + class_name + "' in the web data.");
      int num3 = web_data.IndexOf("<", num2 + 1);
      return web_data.Substring(num2 + 1, num3 - num2 - 1);
    }

    private string GetDataByDataTestName(string web_data, string data_test_name)
    {
      int num1 = web_data.IndexOf("data-test=\"" + data_test_name + "\"");
      int num2 = num1 != -1 ? web_data.IndexOf("<span", num1 + 1) : throw new Exception("Unable to find data with data test name '" + web_data + "' inside web data.");
      int num3 = web_data.IndexOf(">", num2 + 1);
      int num4 = web_data.IndexOf("<", num3 + 1);
      return web_data.Substring(num3 + 1, num4 - num3 - 1);
    }
  }
}
