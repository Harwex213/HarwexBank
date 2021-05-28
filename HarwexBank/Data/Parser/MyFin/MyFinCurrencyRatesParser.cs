using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Html.Dom;

namespace HarwexBank.Parser.MyFin
{
    public class MyFinCurrencyRatesParser : IParser<Dictionary<string, string>>
    {
        public Dictionary<string, string> Parse(IHtmlDocument document)
        {
            var items = document.QuerySelectorAll("tr").
                Where(item => item.ClassName != null && item.ClassName.Contains("row body odd"));
            
            return items.ToDictionary(item => item.Children[0].TextContent,
                item => item.Children[1].TextContent);
        }
    }
}