using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HarwexBank
{
    public class AccountViewModel : ObservableObject, IControlViewModel
    {
        public string Name => "Счета";
        public AccountViewModel()
        {
            AccountModels = new ObservableCollection<AccountModel>(MainViewModel.LoggedInUser.Accounts);
        }
        public ObservableCollection<AccountModel> AccountModels { get; }

        #region Commands

        #region Files & Properties

        private ICommand _openAccountCommand;
        private ICommand _closeAccountCommand;
        private ICommand _freezeAccountCommand;

        public ICommand OpenAccountCommand
        {
            get
            {
                _openAccountCommand ??= new RelayCommand(
                    a => OpenAccountModel((AccountModel)a),
                    a => a is AccountModel);
        
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
        
        private void OpenAccountModel(AccountModel account)
        {
            // TODO: Make opening new Account
        }
        private void CloseAccountModel(AccountModel account)
        {
            ModelResourcesManager.GetInstance().RemoveModel(account);
            AccountModels.Remove(account);
        }
        
        private void FreezeAccountModel(AccountModel account)
        {
            // TODO: Update the account
            
            account.IsFrozen = !account.IsFrozen;
        }

        #endregion

        #endregion
        
    }
}