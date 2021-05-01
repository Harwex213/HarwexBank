

using System.Collections.Generic;
using System.Windows;

namespace HarwexBank
{
    public class AccountViewModel : ObservableObject, IControlViewModel
    {
        public string Name => "Счета";
        public List<AccountModel> AccountModels { get; }

        public AccountViewModel()
        {
            // TODO Make loading Accounts of User from Database
            
            AccountModels = new List<AccountModel>()
            {
                new()
                {
                    Cash = 700,
                    CreationDate = "12.04.2020",
                    Currency = new CurrencyModel() {Id = 0, Name = "RUB"},
                    Id = 129489129
                },
                new()
                {
                    Cash = 6348,
                    CreationDate = "20.08.2040",
                    Currency = new CurrencyModel() {Id = 0, Name = "BYN"},
                    Id = 834284910
                },                new()
                {
                    Cash = 2131,
                    CreationDate = "12.04.2010",
                    Currency = new CurrencyModel() {Id = 0, Name = "RUB"},
                    Id = 0249128
                }
            };
        }
    }
}