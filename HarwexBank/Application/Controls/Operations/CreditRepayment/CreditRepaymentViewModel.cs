using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace HarwexBank
{
    public class CreditRepaymentViewModel : BaseControlViewModel, IControlViewModel, IDataErrorInfo
    {
        public string Name => "Оплата по кредиту";

        public CreditRepaymentViewModel()
        {
            UserAccounts = ModelResourcesManager.GetInstance().UserAccounts;
            UserCredits = ModelResourcesManager.GetInstance().UserCredits;
            UserJournal = ModelResourcesManager.GetInstance().UserJournal;
            CurrencyTypeModels = ModelResourcesManager.GetInstance().ExistedCurrencyTypes;
            
            OperationCurrencyType = CurrencyTypeModels[0];
        }

        // Using Data.
        // Fields
        private AccountModel _accountInitiator;
        private IssuedCreditModel _selectedCredit;
        private decimal _amountToTransfer;
        
        public ObservableCollection<JournalModel> UserJournal { get; set; }
        public ObservableCollection<AccountModel> UserAccounts { get; set; }
        public ObservableCollection<IssuedCreditModel> UserCredits { get; set; }
        public ObservableCollection<CurrencyTypeModel> CurrencyTypeModels { get; set; }
        public CurrencyTypeModel OperationCurrencyType { get; set; }

        public AccountModel AccountInitiator
        {
            get => _accountInitiator;
            set => Set(ref _accountInitiator, value);
        }

        public IssuedCreditModel SelectedCredit
        {
            get => _selectedCredit;
            set => Set(ref _selectedCredit, value);
        }

        public decimal AmountToTransfer
        {
            get => _amountToTransfer;
            set => Set(ref _amountToTransfer, value);
        }

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
                if (SelectedCredit is null)
                {
                    return "Выберите сначала кредит";
                }

                switch (name)
                {
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
                        var currencyConverter = ModelResourcesManager.GetInstance().CurrencyConverter;
                        if (currencyConverter.ConvertCurrencies(
                                SelectedCredit.CreditTypeModelNavigation.CurrencyTypeModelNavigation.CurrencyTypeEnum,
                                 OperationCurrencyType.CurrencyTypeEnum, AmountToTransfer)
                             > SelectedCredit?.AmountRemained)
                        {
                            return "Сумма не должна превышать остаточную сумму погашения: " + SelectedCredit?.AmountRemained;
                        }
                        break;
                }

                return null;
            }
        }

        #region Commands

        // Fields.
        private ICommand _repayCreditCommand;

        // Props.
        public ICommand RepayCreditCommand
        {
            get
            {
                _repayCreditCommand ??= new RelayCommand(
                    _ => RepayCredit());
        
                return _repayCreditCommand;
            }
        }
        
        // Methods.
        private void RepayCredit()
        {
            if (AccountInitiator.IsFrozen)
            {
                MessageBox.Show("Счёт отправителя заморожен");
                return;
            }
            if (SelectedCredit.IsRepaid)
            {
                MessageBox.Show("Кредит уже выплачен");
                return;
            }
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
            SelectedCredit.RepaidAmount += currencyConverter.ConvertCurrencies(
                SelectedCredit.CreditTypeModelNavigation.CurrencyTypeModelNavigation.CurrencyTypeEnum,
                OperationCurrencyType.CurrencyTypeEnum, AmountToTransfer);
            if (SelectedCredit.RepaidAmount == SelectedCredit.Amount)
            {
                SelectedCredit.IsRepaid = true;
            }
            JournalModel journalNote = new CreditRepaymentModel()
            {
                UserId = AccountInitiator.UserId,
                Date = DateTime.Now,
                BankAccountInitiator = AccountInitiator.Id,
                SelectedCredit = SelectedCredit.Id,
                CurrencyTypeModelNavigation = OperationCurrencyType,
                Amount = AmountToTransfer
            };
            UserJournal.Add(journalNote);
            ModelResourcesManager.GetInstance().GenerateCreditRepayment(journalNote, AccountInitiator, SelectedCredit);

            SelectedCredit.AmountRemained = SelectedCredit.AmountToPay - SelectedCredit.RepaidAmount;
            MessageBox.Show("Успешно оплачено");
        }
        
        #endregion
    }
}