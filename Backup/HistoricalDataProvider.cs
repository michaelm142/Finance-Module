// Decompiled with JetBrains decompiler
// Type: Yahoo.Finance.HistoricalDataProvider
// Assembly: Yahoo.Finance, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8859EB3C-3A3D-4D2A-8276-F613AD1DF364
// Assembly location: C:\Users\dagma\.nuget\packages\yahoo.finance\4.3.3\lib\netstandard2.0\Yahoo.Finance.dll

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TimHanewichToolkit;

namespace Yahoo.Finance
{
  public class HistoricalDataProvider
  {
    public HistoricalDataRecord[] HistoricalData { get; set; }

    public HistoricalDataDownloadResult DownloadResult { get; set; }

    public async Task DownloadHistoricalDataAsync(
      string StockSymbol,
      DateTime PeriodStart,
      DateTime PeriodEnd,
      int try_count = 10)
    {
      this.HistoricalData = (HistoricalDataRecord[]) null;
      this.DownloadResult = HistoricalDataDownloadResult.Downloading;
      for (int HaveTriedCount = 0; this.DownloadResult != HistoricalDataDownloadResult.Successful && HaveTriedCount < try_count && this.DownloadResult != HistoricalDataDownloadResult.DataDoesNotExistForSpecifiedTimePeriod; ++HaveTriedCount)
        await this.TryGetHistoricalDatAsync(StockSymbol, PeriodStart, PeriodEnd);
    }

    private async Task TryGetHistoricalDatAsync(string symbol, DateTime start, DateTime end)
    {
      DateTime now = DateTime.Now;
      HttpClient hc = new HttpClient();
      string str1 = await (await hc.GetAsync("https://finance.yahoo.com/quote/" + symbol)).Content.ReadAsStringAsync();
      int unixTime = UnixToolkit.GetUnixTime(start);
      string str2 = unixTime.ToString();
      unixTime = UnixToolkit.GetUnixTime(end);
      string str3 = unixTime.ToString();
      string[] strArray1 = new string[7]
      {
        "https://query1.finance.yahoo.com/v7/finance/download/",
        symbol,
        "?period1=",
        str2,
        "&period2=",
        str3,
        "&interval=1d&events=history"
      };
      HttpResponseMessage fr = await hc.GetAsync(string.Concat(strArray1));
      string str4 = await fr.Content.ReadAsStringAsync();
      if (fr.StatusCode != HttpStatusCode.OK)
      {
        this.HistoricalData = (HistoricalDataRecord[]) null;
        this.DownloadResult = fr.StatusCode != HttpStatusCode.BadRequest ? (fr.StatusCode != HttpStatusCode.Unauthorized ? (fr.StatusCode != HttpStatusCode.NotFound ? HistoricalDataDownloadResult.OtherFailure : HistoricalDataDownloadResult.NoDataFound) : HistoricalDataDownloadResult.Unauthorized) : HistoricalDataDownloadResult.DataDoesNotExistForSpecifiedTimePeriod;
        hc = (HttpClient) null;
        fr = (HttpResponseMessage) null;
      }
      else
      {
        List<HistoricalDataRecord> historicalDataRecordList = new List<HistoricalDataRecord>();
        List<string> stringList = new List<string>();
        stringList.Add("\n");
        string[] strArray2 = str4.Split(stringList.ToArray(), StringSplitOptions.None);
        for (int index = 1; index <= strArray2.Length - 1; ++index)
        {
          string str5 = strArray2[index];
          if (str5 != "")
          {
            try
            {
              HistoricalDataRecord historicalDataRecord = new HistoricalDataRecord();
              stringList.Clear();
              stringList.Add(",");
              string[] strArray3 = str5.Split(stringList.ToArray(), StringSplitOptions.None);
              historicalDataRecord.Date = DateTime.Parse(strArray3[0]);
              historicalDataRecord.Open = Convert.ToSingle(strArray3[1]);
              historicalDataRecord.High = Convert.ToSingle(strArray3[2]);
              historicalDataRecord.Low = Convert.ToSingle(strArray3[3]);
              historicalDataRecord.Close = Convert.ToSingle(strArray3[4]);
              historicalDataRecord.AdjustedClose = Convert.ToSingle(strArray3[5]);
              historicalDataRecord.Volume = Convert.ToInt32(strArray3[6]);
              historicalDataRecordList.Add(historicalDataRecord);
            }
            catch
            {
              throw new Exception("Unable to conver this row: " + str5);
            }
          }
        }
        this.HistoricalData = historicalDataRecordList.ToArray();
        this.DownloadResult = HistoricalDataDownloadResult.Successful;
        hc = (HttpClient) null;
        fr = (HttpResponseMessage) null;
      }
    }
  }
}
