using System.Collections.Generic;
using System.Linq;
using AngleSharp.Html.Dom;

namespace HarwexBank.Parser.MyFin
{
    public class MyFinCurrencyRatesSettings : IParserSettings
    {
        public string BaseUrl { get; set; } = "https://myfin.by/bank/kursy_valjut_nbrb";
        public string Prefix { get; set; }
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
    }
}