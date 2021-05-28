using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace HarwexBank
{
    public class TransferToAccountViewModel : BaseControlViewModel, IControlViewModel, IDataErrorInfo
    {
        public string Name => "Перевод средств другому счёту";

        public TransferToAccountViewModel()
        {
            UserAccounts = ModelResourcesManager.GetInstance().UserAccounts;
            UserJournal = ModelResourcesManager.GetInstance().UserJournal;
            CurrencyTypeModels = ModelResourcesManager.GetInstance().ExistedCurrencyTypes;

            OperationCurrencyType = CurrencyTypeModels[0];
        }

        // Using Data.
        public ObservableCollection<JournalModel> UserJournal { get; set; }
        public ObservableCollection<AccountModel> UserAccounts { get; set; }
        public ObservableCollection<CurrencyTypeModel> CurrencyTypeModels { get; set; }
        public AccountModel AccountInitiator { get; set; }
        public AccountModel AccountReceiver { get; set; }
        public int AccountReceiverId { get; set; }
        public CurrencyTypeModel OperationCurrencyType { get; set; }
        public decimal AmountToTransfer { get; set; }

        // Data Validation.
        public string Error => null;

        public string this[string name]
        {
            get
            {
                if (AccountInitiator is null)
                {
                    return "Выберите сначала счёт";
                }

                switch (name)
                {
                    case nameof(AccountReceiverId):
                        if ((AccountReceiver = ModelResourcesManager.GetInstance().GetAccountById(AccountReceiverId)) == null)
                        {
                            return "Введённый счёт не найден";
                        }
                        if (AccountReceiver == AccountInitiator)
                        {
                            return "Введённые счёта не должны совпадать";
                        }
                        break;
                    case nameof(AmountToTransfer):
                        if (AmountToTransfer <= 0)
                        {
                            return "Сумма не должна быть меньше, либо равна нулю";
                        }
                        if (!AccountModel.CheckAccountAmountToPossibilityOfTransfer(
                            AccountInitiator.CurrencyTypeModelNavigation.CurrencyTypeEnum,
                            OperationCurrencyType.CurrencyTypeEnum,
                            AccountInitiator.Amount,
                            AmountToTransfer))
                        {
                            return $"Сумма не должна превышать сумму счёта: {AccountInitiator.Amount} {AccountInitiator.CurrencyType}";
                        }
                        break;
                }

                return null;
            }
        }

        #region Commands

        // Fields.
        private ICommand _transferToAccountCommand;
        
        // Props.
        public ICommand TransferToAccountCommand
        {
            get
            {
                _transferToAccountCommand ??= new RelayCommand(
                    _ => TransferToAccount());
        
                return _transferToAccountCommand;
            }
        }
        
        // Methods.
        private void TransferToAccount()
        {
            if (!AccountModel.CheckAccountAmountToPossibilityOfTransfer(
                AccountInitiator.CurrencyTypeModelNavigation.CurrencyTypeEnum,
                OperationCurrencyType.CurrencyTypeEnum,
                AccountInitiator.Amount,
                AmountToTransfer))
            {
                MessageBox.Show("Сумма не должна превышать сумму" +
                                $" счёта: {AccountInitiator.Amount} {AccountInitiator.CurrencyType}");
                return;
            }
            
            var currencyConverter = ModelResourcesManager.GetInstance().CurrencyConverter;
            
            AccountInitiator.Amount -= currencyConverter.ConvertCurrencies(
                AccountInitiator.CurrencyTypeModelNavigation.CurrencyTypeEnum,
                OperationCurrencyType.CurrencyTypeEnum, AmountToTransfer);
            AccountReceiver.Amount += currencyConverter.ConvertCurrencies(
                AccountReceiver.CurrencyTypeModelNavigation.CurrencyTypeEnum,
                OperationCurrencyType.CurrencyTypeEnum, AmountToTransfer);
            JournalModel journalNote = new TransferToAccountModel()
            {
                UserId = AccountInitiator.UserId,
                Date = DateTime.Now,
                BankAccountInitiator = AccountInitiator.Id,
                BankAccountReceiver = AccountReceiver.Id,
                CurrencyTypeModelNavigation = OperationCurrencyType,
                Amount = AmountToTransfer
            };
            UserJournal.Add(journalNote);
            ModelResourcesManager.GetInstance().GenerateTransfer(journalNote, AccountInitiator, AccountReceiver);
            
            MessageBox.Show("Успешно переведено");
        }
        
        #endregion
    }
}