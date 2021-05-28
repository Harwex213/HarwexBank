using System;
using System.Collections.Generic;
using HarwexBank.Parser;
using HarwexBank.Parser.MyFin;

namespace HarwexBank
{
    public class CurrencyConverter : ICurrencyRatesSubject
    {
        public const int UsdToBynIndex = 0;
        public const int RubToBynIndex = 2;

        private readonly List<ICurrencyRatesObserver> _observers;

        public CurrencyConverter()
        {
            _observers = new List<ICurrencyRatesObserver>();
            
            var parser = new ParserWorker<string[]>(new MyFinCurrencyRatesParser());

            parser.OnCompleted += _ => { };
            parser.OnNewData += (_, data) =>
            {
                UsdToBynRate = Convert.ToDecimal(data[UsdToBynIndex]);
                RubToBynRate = Convert.ToDecimal(data[RubToBynIndex]);

                Notify();
            };

            parser.Settings = new MyFinCurrencyRatesSettings();
            parser.Start();
        }
        
        public decimal UsdToBynRate { get; private set; }
        public decimal RubToBynRate { get; private set; }

        public void Attach(ICurrencyRatesObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(ICurrencyRatesObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.NewRates(this);
            }
        }

        #region CurrencyConvertions

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

        #endregion
    }
}