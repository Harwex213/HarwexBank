using System.Collections.Generic;
using System.Linq;
using AngleSharp.Html.Dom;

namespace HarwexBank.Parser.MyFin
{
    public class MyFinCurrencyRatesParser : IParser<string[]>
    {
        public string[] Parse(IHtmlDocument document)
        {
            var items = document.QuerySelectorAll("tr").
                Where(item => item.ClassName != null && item.ClassName.Contains("row body odd"));

            var array = items.Select(item => item.Children[1].TextContent).ToArray();
            return array;
        }
    }
}