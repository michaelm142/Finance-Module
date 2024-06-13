// Decompiled with JetBrains decompiler
// Type: Yahoo.Finance.EarningsCalendarProvider
// Assembly: Yahoo.Finance, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8859EB3C-3A3D-4D2A-8276-F613AD1DF364
// Assembly location: C:\Users\dagma\.nuget\packages\yahoo.finance\4.3.3\lib\netstandard2.0\Yahoo.Finance.dll

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Yahoo.Finance
{
  public class EarningsCalendarProvider
  {
    public async Task<string[]> GetCompaniesReportingEarningsAsync(DateTime date)
    {
      string str1 = await (await new HttpClient().GetAsync("https://finance.yahoo.com/calendar/earnings?day=" + date.Year.ToString("0000") + "-" + date.Month.ToString("00") + "-" + date.Day.ToString("00"))).Content.ReadAsStringAsync();
      List<string> stringList1 = new List<string>();
      int startIndex = str1.IndexOf("table class");
      if (startIndex == -1)
        return new List<string>().ToArray();
      int num1 = str1.IndexOf("</table");
      string str2 = str1.Substring(startIndex, num1 - startIndex);
      stringList1.Add("<tr");
      string[] array = stringList1.ToArray();
      string[] strArray1 = str2.Split(array, StringSplitOptions.None);
      List<string> stringList2 = new List<string>();
      for (int index = 2; index < strArray1.Length; ++index)
      {
        stringList1.Clear();
        stringList1.Add("<td");
        string[] strArray2 = strArray1[index].Split(stringList1.ToArray(), StringSplitOptions.None);
        int num2 = strArray2[1].IndexOf("href");
        int num3 = strArray2[1].IndexOf(">", num2 + 1);
        int num4 = strArray2[1].IndexOf("<", num3 + 1);
        stringList2.Add(strArray2[1].Substring(num3 + 1, num4 - num3 - 1));
      }
      return stringList2.ToArray();
    }
  }
}
