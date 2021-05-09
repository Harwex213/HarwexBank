using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace HarwexBank
{
    public class AccountsListPage : Control, IControlViewModel { }
    public class CreateNewAccountPage : Control, IControlViewModel { }
    public class AccountViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "Счета";
        public AccountViewModel()
        {
            AccountModels = new ObservableCollection<AccountModel>(MainViewModel.Data.LoggedInUser.Accounts);
            
            ControlViewModels.Add(new AccountsListPage());
            ControlViewModels.Add(new CreateNewAccountPage());

            SelectedControlViewModel = ControlViewModels[0];
        }
        public ObservableCollection<AccountModel> AccountModels { get; }
        
        public AccountModel AccountToOpen { get; set; }

        #region Commands

        #region Files & Properties

        private ICommand _openAccountCommand;
        private ICommand _createAccountCommand;
        private ICommand _closeAccountCommand;
        private ICommand _freezeAccountCommand;

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
                _openAccountCommand ??= new RelayCommand(
                    _ => CreateAccountModel());
        
                return _openAccountCommand;
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
        
        private void OpenAccountModel()
        {
            SelectedControlViewModel = ControlViewModels[1];
        }
        
        private void CreateAccountModel()
        {
            AccountToOpen.UserId = MainViewModel.Data.LoggedInUser.Id;
            AccountToOpen.RegistrationDate = DateTime.Now;
            AccountToOpen.Amount = 0;
            AccountToOpen.IsFrozen = false;

            AccountModels.Add(AccountToOpen);
            ModelResourcesManager.GetInstance().AddModel(AccountToOpen);
            
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
        }

        #endregion

        #endregion
        
    }
}