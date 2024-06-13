// Decompiled with JetBrains decompiler
// Type: Yahoo.Finance.EquityData
// Assembly: Yahoo.Finance, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8859EB3C-3A3D-4D2A-8276-F613AD1DF364
// Assembly location: C:\Users\dagma\.nuget\packages\yahoo.finance\4.3.3\lib\netstandard2.0\Yahoo.Finance.dll

using System;

namespace Yahoo.Finance
{
  public class EquityData
  {
    public string QueriedSymbol { get; set; }

    public DateTimeOffset DataCollectedOn { get; set; }

    public EquityData() => this.DataCollectedOn = DateTimeOffset.Now;
  }
}
