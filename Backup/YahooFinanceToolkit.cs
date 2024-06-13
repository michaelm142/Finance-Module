// Decompiled with JetBrains decompiler
// Type: Yahoo.Finance.YahooFinanceToolkit
// Assembly: Yahoo.Finance, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8859EB3C-3A3D-4D2A-8276-F613AD1DF364
// Assembly location: C:\Users\dagma\.nuget\packages\yahoo.finance\4.3.3\lib\netstandard2.0\Yahoo.Finance.dll

using System;

namespace Yahoo.Finance
{
  public class YahooFinanceToolkit
  {
    public static double GetMarketCapFromString(string representation)
    {
      string lower = representation.Substring(representation.Length - 1, 1).ToLower();
      float single = Convert.ToSingle(representation.Substring(0, representation.Length - 1));
      switch (lower)
      {
        case "th":
          return Convert.ToDouble(single * 1000f);
        case "m":
          return Convert.ToDouble(single * 1000000f);
        case "b":
          return Convert.ToDouble(single * 1E+09f);
        case "t":
          return Convert.ToDouble(single * 1E+12f);
        default:
          return Convert.ToDouble(representation);
      }
    }

    public static int StepIndexOf(string body, params string[] parts) => YahooFinanceToolkit.StepIndexOf(body, 0, parts);

    public static int StepIndexOf(string body, int start_at, params string[] parts)
    {
      int startIndex = Math.Max(0, start_at);
      foreach (string part in parts)
      {
        startIndex = body.IndexOf(part, startIndex);
        if (startIndex == -1)
          return -1;
      }
      return startIndex;
    }
  }
}
