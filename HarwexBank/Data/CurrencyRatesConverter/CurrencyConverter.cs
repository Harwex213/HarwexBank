
using System;
using System.Collections.Generic;
using HarwexBank.Parser;
using HarwexBank.Parser.MyFin;

namespace HarwexBank
{
    public class CurrencyConverter : ICurrencyRatesSubject
    {
        public const string UsdRateName = "Доллар США";
        public const string RubRateName = "Российский рубль";

        private readonly List<ICurrencyRatesObserver> _observers;
        private ParserWorker<Dictionary<string, string>> _parser;

        public CurrencyConverter()
        {
            _observers = new List<ICurrencyRatesObserver>();
            
            _parser = new ParserWorker<Dictionary<string, string>>(new MyFinCurrencyRatesParser());

            _parser.OnCompleted += _ => { };
            _parser.OnNewData += (_, data) =>
            {
                UsdToBynRate = Convert.ToDecimal(data[UsdRateName]);
                RubToBynRate = Convert.ToDecimal(data[RubRateName]);

                Notify();
            };

            _parser.Settings = new MyFinCurrencyRatesSettings();
            _parser.Start();
        }
        
        public decimal UsdToBynRate { get; private set; }
        public decimal RubToBynRate { get; private set; }

        public void Attach(ICurrencyRatesObserver observer)
        {
            _observers.Add(observer);
            _parser.Start();
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

        public bool CheckRatesExistence()
        {
            return UsdToBynRate != 0 && RubToBynRate != 0;
        }

        public decimal ConvertCurrencies(CurrencyTypeModel.CurrencyTypes currencyTarget,
            CurrencyTypeModel.CurrencyTypes currencyInitial, decimal amount)
        {
            if (!CheckRatesExistence())
            {
                return -1;
            }
            
            switch (currencyTarget)
            {
                case CurrencyTypeModel.CurrencyTypes.BYN:
                    return currencyInitial switch
                    {
                        CurrencyTypeModel.CurrencyTypes.BYN => amount,
                        CurrencyTypeModel.CurrencyTypes.RUB => ConvertRubToByn(amount),
                        CurrencyTypeModel.CurrencyTypes.USD => ConvertUsdToByn(amount),
                        _ => throw new ArgumentOutOfRangeException(nameof(currencyInitial), currencyInitial, null)
                    };
                case CurrencyTypeModel.CurrencyTypes.RUB:
                    return currencyInitial switch
                    {
                        CurrencyTypeModel.CurrencyTypes.BYN => ConvertBynToRub(amount),
                        CurrencyTypeModel.CurrencyTypes.RUB => amount,
                        CurrencyTypeModel.CurrencyTypes.USD => ConvertBynToRub(ConvertUsdToByn(amount)),
                        _ => throw new ArgumentOutOfRangeException(nameof(currencyInitial), currencyInitial, null)
                    };
                case CurrencyTypeModel.CurrencyTypes.USD:
                    return currencyInitial switch
                    {
                        CurrencyTypeModel.CurrencyTypes.BYN => ConvertBynToUsd(amount),
                        CurrencyTypeModel.CurrencyTypes.RUB => ConvertBynToUsd(ConvertRubToByn(amount)),
                        CurrencyTypeModel.CurrencyTypes.USD => amount,
                        _ => throw new ArgumentOutOfRangeException(nameof(currencyInitial), currencyInitial, null)
                    };
                default:
                    throw new ArgumentOutOfRangeException(nameof(currencyTarget), currencyTarget, null);
            }
        }

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