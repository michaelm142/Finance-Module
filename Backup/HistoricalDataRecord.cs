// Decompiled with JetBrains decompiler
// Type: Yahoo.Finance.HistoricalDataRecord
// Assembly: Yahoo.Finance, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8859EB3C-3A3D-4D2A-8276-F613AD1DF364
// Assembly location: C:\Users\dagma\.nuget\packages\yahoo.finance\4.3.3\lib\netstandard2.0\Yahoo.Finance.dll

using System;

namespace Yahoo.Finance
{
  public class HistoricalDataRecord
  {
    public DateTime Date { get; set; }

    public float Open { get; set; }

    public float High { get; set; }

    public float Low { get; set; }

    public float Close { get; set; }

    public float AdjustedClose { get; set; }

    public int Volume { get; set; }
  }
}
