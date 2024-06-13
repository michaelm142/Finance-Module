// Decompiled with JetBrains decompiler
// Type: Yahoo.Finance.StoreExtractionToolkit
// Assembly: Yahoo.Finance, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8859EB3C-3A3D-4D2A-8276-F613AD1DF364
// Assembly location: C:\Users\dagma\.nuget\packages\yahoo.finance\4.3.3\lib\netstandard2.0\Yahoo.Finance.dll

using System;

namespace Yahoo.Finance
{
  public class StoreExtractionToolkit
  {
    public static string ExtractRootJsonFromWebpage(string web_page_html)
    {
      int num1 = web_page_html.IndexOf("root.App.main =");
      if (num1 == -1)
        throw new Exception("Unable to location data store in web page content.");
      int startIndex = web_page_html.IndexOf("{", num1 + 1) - 1;
      int num2 = web_page_html.IndexOf("</script><script>", startIndex);
      int num3 = web_page_html.LastIndexOf(";", num2 - 1);
      int num4 = web_page_html.LastIndexOf(";", num3 - 1);
      return web_page_html.Substring(startIndex + 1, num4 - startIndex - 1);
    }
  }
}
