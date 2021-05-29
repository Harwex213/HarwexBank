using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HarwexBank
{
    public class AccountModel : ObservableObject, ModelResourcesManager.IModel
    {
        public AccountModel()
        {
            Cards = new HashSet<CardModel>();
            Operations = new HashSet<OperationModel>();
            TransferToAccounts = new HashSet<TransferToAccountModel>();
        }
        
        private int _id;
        private int _userId;
        private string _currencyType;
        private DateTime _registrationDate;
        private decimal _amount;
        private bool _isFrozen;
        
        private string _isAccountFrozen;
        
        [NotMapped]
        public string IsAccountFrozen
        {
            get => _isFrozen switch
            {
                true => "Счёт заморожен",
                false => null
            };
            set => Set(ref _isAccountFrozen, value);
        }

        public virtual CurrencyTypeModel CurrencyTypeModelNavigation { get; set; }
        public virtual UserModel UserModelAccount { get; set; }
        public virtual ICollection<CardModel> Cards { get; set; }
        public virtual ICollection<OperationModel> Operations { get; set; }
        public virtual ICollection<TransferToAccountModel> TransferToAccounts { get; set; }

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        public int UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                OnPropertyChanged("UserId");
            }
        }

        public string CurrencyType
        {
            get => _currencyType;
            set
            {
                _currencyType = value;
                OnPropertyChanged("CurrencyType");
            }
        }

        public DateTime RegistrationDate
        {
            get => _registrationDate;
            set
            {
                _registrationDate = value;
                OnPropertyChanged("RegistrationDate");
            }
        }

        public decimal Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged("Amount");
            }
        }

        public bool IsFrozen
        {
            get => _isFrozen;
            set
            {
                _isFrozen = value;
                OnPropertyChanged($"IsNotFrozen");
            }
        }
        
        [NotMapped]
        public bool IsNotFrozen => !_isFrozen;
        
        private string _freezeButtonText;
        
        [NotMapped]
        public string FreezeButtonText
        {
            get => _isFrozen switch
            {
                true => "Разморозить счёт",
                false => "Заморозить счёт"
            };
            set => Set(ref _freezeButtonText, value);
        }
        
        public static bool CheckAccountAmountToPossibilityOfTransfer(CurrencyTypeModel.CurrencyTypes currencyTarget,
            CurrencyTypeModel.CurrencyTypes currencyInitial, decimal initialAmount, decimal transferAmount)
        {
            var currencyConverter = ModelResourcesManager.GetInstance().CurrencyConverter;
            var amountToTransfer = currencyConverter.ConvertCurrencies(currencyTarget, currencyInitial, transferAmount);

            return initialAmount > amountToTransfer;
        }
    }
}