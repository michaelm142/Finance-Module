// Decompiled with JetBrains decompiler
// Type: Yahoo.Finance.HistoricalDataDownloadResult
// Assembly: Yahoo.Finance, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8859EB3C-3A3D-4D2A-8276-F613AD1DF364
// Assembly location: C:\Users\dagma\.nuget\packages\yahoo.finance\4.3.3\lib\netstandard2.0\Yahoo.Finance.dll

namespace Yahoo.Finance
{
  public enum HistoricalDataDownloadResult
  {
    Successful,
    DataDoesNotExistForSpecifiedTimePeriod,
    Unauthorized,
    OtherFailure,
    Downloading,
    NoDataFound,
  }
}
