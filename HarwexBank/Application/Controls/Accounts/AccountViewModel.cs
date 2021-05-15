using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace HarwexBank
{
    public class AccountsListViewModel : Control, IControlViewModel { }
    public class CreateNewAccountViewModel : Control, IControlViewModel { }
    public class AccountViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "Счета";
        public AccountViewModel()
        {
            AccountToOpen = new AccountModel();
            CardToCreate = new CardModel();
            
            switch (MainViewModel.WindowFactory)
            {
                case ClientMainWindow:
                    // Using data.
                    AccountModels = ModelResourcesManager.GetInstance().UserAccounts;
                    CurrencyTypeModels = ModelResourcesManager.GetInstance().ExistedCurrencyTypes;
                    CardTypeModels = ModelResourcesManager.GetInstance().ExistedCardTypes;
                    
                    // Using VM
                    ControlViewModels.Add(new AccountsListViewModel());
                    ControlViewModels.Add(new CreateNewAccountViewModel());
                    break;
                
                case WorkerMainWindow:
                    
                    // Using VM
                    ControlViewModels.Add(new AccountsListViewModel());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            SelectedControlViewModel = ControlViewModels.FirstOrDefault();
        }
        
        // Used data in Views
        public ObservableCollection<AccountModel> AccountModels { get; set; }
        public ObservableCollection<CurrencyTypeModel> CurrencyTypeModels { get; }
        public ObservableCollection<CardTypeModel> CardTypeModels { get; }
        
        // Shells for new models
        public AccountModel AccountToOpen { get; set; }
        public CardModel CardToCreate { get; set; }

        #region Commands

        #region Files & Properties

        private ICommand _goBackCommand;
        private ICommand _openAccountCommand;
        private ICommand _createAccountCommand;
        private ICommand _closeAccountCommand;
        private ICommand _freezeAccountCommand;

        public ICommand GoBackCommand
        {
            get
            {
                _goBackCommand ??= new RelayCommand(
                    _ => GoBack());
        
                return _goBackCommand;
            }
        }

        public ICommand OpenAccountCommand
        {
            get
            {
                _openAccountCommand ??= new RelayCommand(
                    _ => OpenAccountModel());
        
                return _openAccountCommand;
            }
        }
        
        public ICommand CreateAccountCommand
        {
            get
            {
                _createAccountCommand ??= new RelayCommand(
                    _ => CreateAccountModel());
        
                return _createAccountCommand;
            }
        }
        
        public ICommand CloseAccountCommand
        {
            get
            {
                _closeAccountCommand ??= new RelayCommand(
                    a => CloseAccountModel((AccountModel)a),
                    a => a is AccountModel);
        
                return _closeAccountCommand;
            }
        }
        
        public ICommand FreezeAccountCommand
        {
            get
            {
                _freezeAccountCommand ??= new RelayCommand(
                    a => FreezeAccountModel((AccountModel)a),
                    a => a is AccountModel);
        
                return _freezeAccountCommand;
            }
        }
        
        #endregion

        #region Methods
        
        private void GoBack()
        {
            SelectedControlViewModel = ControlViewModels[0];
        }
        
        private void OpenAccountModel()
        {
            SelectedControlViewModel = ControlViewModels[1];
        }
        
        private void CreateAccountModel()
        {
            // Adding Account Model to DB.
            AccountToOpen.UserId = ModelResourcesManager.GetInstance().LoggedInUser.Id;
            AccountToOpen.RegistrationDate = DateTime.Now;
            AccountToOpen.Amount = 0;
            AccountToOpen.IsFrozen = false;
            ModelResourcesManager.GetInstance().AddModel(AccountToOpen);

            // Adding Card Model to DB.
            var number = string.Empty;
            for (var i = 0; i < 16; i++)
            {
                number += new Random().Next(0, 9).ToString();
            }
            CardToCreate.AccountId = AccountToOpen.Id;
            CardToCreate.Number = number;
            CardToCreate.OwnerName = ModelResourcesManager.GetInstance().LoggedInUser.FirstName.ToUpper() + " " +
                                     ModelResourcesManager.GetInstance().LoggedInUser.LastName.ToUpper();
            CardToCreate.TimeFrame = new Random().Next(0, 12) + "/" + (DateTime.Now.Year + 3).ToString()[2..];
            CardToCreate.Cvv = new Random().Next(0, 9).ToString() + new Random().Next(0, 9) + new Random().Next(0, 9);
            ModelResourcesManager.GetInstance().AddModel(CardToCreate);
            
            AccountToOpen.Cards.Add(CardToCreate);
            AccountModels.Add(AccountToOpen);
            
            AccountToOpen = new AccountModel();
            CardToCreate = new CardModel();
            
            SelectedControlViewModel = ControlViewModels[0];
        }
        
        private void CloseAccountModel(AccountModel account)
        {
            AccountModels.Remove(account);
            ModelResourcesManager.GetInstance().RemoveModel(account);
        }
        
        private void FreezeAccountModel(AccountModel account)
        {
            account.IsFrozen = !account.IsFrozen;
            ModelResourcesManager.GetInstance().UpdateModel(account);

            switch (account.FreezeButtonText)
            {
                case "Разморозить счёт":
                    account.FreezeButtonText = "Заморозить счёт";
                    break;
                
                case "Заморозить счёт":
                    account.FreezeButtonText = "Разморозить счёт";
                    break;
            }
        }

        #endregion

        #endregion
        
    }
}