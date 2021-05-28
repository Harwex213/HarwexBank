using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace HarwexBank
{
    public class CreditTypeModel : ObservableObject, ModelResourcesManager.IModel, IDataErrorInfo
    {
        public CreditTypeModel()
        {
            IssuedCredits = new HashSet<IssuedCreditModel>();
        }
        
        private int _id;
        private string _name;
        private decimal _ratio;
        private int _minimalTerm;
        private int _maximalTerm;
        private decimal _minimalTakingAmount;
        private decimal _maximalTakingAmount;
        private string _creditCurrencyType;

        public virtual CurrencyTypeModel CurrencyTypeModelNavigation { get; set; }
        public virtual ICollection<IssuedCreditModel> IssuedCredits { get; set; }
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        
        public decimal Rate
        {
            get => _ratio;
            set
            {
                _ratio = value;
                OnPropertyChanged("Rate");
            }
        }
        
        public int MinimalTerm
        {
            get => _minimalTerm;
            set => Set(ref _minimalTerm, value);
        }

        public int MaximalTerm
        {
            get => _maximalTerm;
            set => Set(ref _maximalTerm, value);
        }

        public decimal MinimalTakingAmount
        {
            get => _minimalTakingAmount;
            set => Set(ref _minimalTakingAmount, value);
        }

        public decimal MaximalTakingAmount
        {
            get => _maximalTakingAmount;
            set => Set(ref _maximalTakingAmount, value);
        }

        public string CreditCurrencyType
        {
            get => _creditCurrencyType;
            set => Set(ref _creditCurrencyType, value);
        }

        #region Data Validation

        public string Error => null;

        public string this[string name]
        {
            get
            {
                switch (name)
                {
                    case nameof(Name):
                        if (string.IsNullOrEmpty(Name) || Name.Length is < 6 or > 50)
                        {
                            return "Должно быть длиннее 6 символов";
                        }
                        else if (!Regex.Match(Name ?? string.Empty, @"(^[A-Z][a-zA-Z]*$)|(^[А-я][а-яА-я]*$)").Success)
                        {
                            return "Должно содержать только буквы";
                        }
                        break;
                    case nameof(Rate):
                        if (Rate is < 0 or > 1)
                        {
                            return "Процентная ставка должна быть не больше 1 и не меньше 0";
                        }
                        break;
                }

                return null;
            }
        }

        #endregion
    }
}