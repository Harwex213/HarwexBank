using System;
using System.Collections.Generic;
using HarwexBank.Parser;
using HarwexBank.Parser.MyFin;

namespace HarwexBank
{
    public class CurrencyConverter
    {
        public CurrencyConverter()
        {
            var parser = new ParserWorker<string[]>(new MyFinCurrencyRatesParser());

            parser.OnCompleted += _ => { };
            parser.OnNewData += (_, data) =>
            {
                foreach (var item in data)
                {
                    CurrencyRates.Add(item);
                }
            };

            parser.Settings = new MyFinCurrencyRatesSettings();
            parser.Start();
        }
        
        public List<string> CurrencyRates { get; set; }

        public static int UsdToBynIndex => 0;
        public static int RubToBynIndex => 2;

        public decimal UsdToBynRate => Convert.ToDecimal(CurrencyRates[UsdToBynIndex]);
        public decimal RubToBynRate => Convert.ToDecimal(CurrencyRates[RubToBynIndex]) / 100;

        public decimal ConvertUsdToByn(decimal usdAmount)
        {
            return usdAmount * UsdToBynRate;
        }

        public decimal ConvertBynToUsd(decimal bynAmount)
        {
            return bynAmount / UsdToBynRate;
        }

        public decimal ConvertRubToByn(decimal rubAmount)
        {
            return rubAmount * RubToBynRate;
        }

        public decimal ConvertBynToRub(decimal bynAmount)
        {
            return bynAmount / RubToBynRate;
        }
    }
}