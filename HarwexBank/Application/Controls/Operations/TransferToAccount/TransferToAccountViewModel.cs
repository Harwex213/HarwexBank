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
            UserAccounts = MainViewModel.Data.UserAccounts;
            UserJournal = MainViewModel.Data.UserJournal;
        }

        // Using Data.
        public ObservableCollection<JournalModel> UserJournal { get; set; }
        public ObservableCollection<AccountModel> UserAccounts { get; set; }
        public AccountModel AccountInitiator { get; set; }
        public AccountModel AccountReceiver { get; set; }
        public int AccountReceiverId { get; set; }
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
                        break;
                    case nameof(AmountToTransfer):
                        if (AmountToTransfer < 0)
                        {
                            return "Сумма не должна быть отрицательной";
                        }
                        if (AmountToTransfer > AccountInitiator.Amount)
                        {
                            return "Сумма не должна превышать сумму счёта: " + AccountInitiator.Amount;
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
            AccountInitiator.Amount -= AmountToTransfer;
            AccountReceiver.Amount += AmountToTransfer;
            JournalModel journalNote = new TransferToAccountModel()
            {
                UserId = AccountInitiator.UserId,
                Date = DateTime.Now,
                BankAccountInitiator = AccountInitiator.Id,
                BankAccountReceiver = AccountReceiver.Id,
                Amount = AmountToTransfer
            };
            UserJournal.Add(journalNote);
            ModelResourcesManager.GetInstance().GenerateTransfer(journalNote, AccountInitiator, AccountReceiver);
            
            MessageBox.Show("Успешно переведено");
        }
        
        #endregion
    }
}