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