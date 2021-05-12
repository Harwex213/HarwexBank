using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace HarwexBank
{
    public class CreditRepaymentViewModel : BaseControlViewModel, IControlViewModel, IDataErrorInfo
    {
        public string Name => "Оплата по кредиту";

        public CreditRepaymentViewModel()
        {
            UserAccounts = MainViewModel.Data.UserAccounts;
            UserCredits = MainViewModel.Data.UserCredits;
            UserJournal = MainViewModel.Data.UserJournal;
        }

        // Using Data.
        public ObservableCollection<JournalModel> UserJournal { get; set; }
        public ObservableCollection<AccountModel> UserAccounts { get; set; }
        public ObservableCollection<IssuedCreditModel> UserCredits { get; set; }
        public AccountModel AccountInitiator { get; set; }
        public IssuedCreditModel SelectedCredit { get; set; }
        public decimal AmountToTransfer { get; set; }
        
        // Data Validation.
        public string Error => null;

        public string this[string name]
        {
            get
            {
                string result = null;
                var amountToPaid = SelectedCredit.Amount - SelectedCredit.RepaidAmount;

                switch (name)
                {
                    case nameof(AmountToTransfer):
                        if (AmountToTransfer < 0)
                        {
                            result = "Сумма не должна быть отрицательной";
                        }
                        if (AmountToTransfer > AccountInitiator.Amount)
                        {
                            result = "Сумма не должна превышать сумму счёта: " + AccountInitiator.Amount;
                        }
                        if (AmountToTransfer > amountToPaid)
                        {
                            result = "Сумма не должна превышать сумму погашения: " + amountToPaid;
                        }
                        break;
                }

                return result;
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
            AccountInitiator.Amount -= AmountToTransfer;
            SelectedCredit.RepaidAmount += AmountToTransfer;
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
                Amount = AmountToTransfer
            };
            UserJournal.Add(journalNote);
            ModelResourcesManager.GetInstance().GenerateCreditRepayment(journalNote, AccountInitiator, SelectedCredit);
        }
        
        #endregion
    }
}