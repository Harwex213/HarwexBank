

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace HarwexBank
{
    public class AccountViewModel : ObservableObject, IControlViewModel
    {
        public string Name => "Счета";
        public AccountViewModel()
        {
            AccountModels = new ObservableCollection<AccountModel>(ApplicationViewModel.LoggedInUser.Accounts);
        }
        public ObservableCollection<AccountModel> AccountModels { get; }

        #region Commands

        #region Files & Properties

        private ICommand _openAccountCommand;
        private ICommand _closeAccountCommand;
        private ICommand _freezeAccountCommand;

        // public ICommand OpenAccountCommand
        // {
        //     get
        //     {
        //         _openAccountCommand ??= new RelayCommand(
        //             p => ChangeViewModel((IPageViewModel)p),
        //             p => p is IPageViewModel);
        //
        //         return _openAccountCommand;
        //     }
        // }
        //
        // public ICommand CloseAccountCommand
        // {
        //     get
        //     {
        //         _closeAccountCommand ??= new RelayCommand(
        //             p => ChangeViewModel((IPageViewModel)p),
        //             p => p is IPageViewModel);
        //
        //         return _closeAccountCommand;
        //     }
        // }
        //
        // public ICommand FreezeAccountCommand
        // {
        //     get
        //     {
        //         _freezeAccountCommand ??= new RelayCommand(
        //             p => ChangeViewModel((IPageViewModel)p),
        //             p => p is IPageViewModel);
        //
        //         return _freezeAccountCommand;
        //     }
        // }
        
        #endregion

        #region Methods
        
        

        #endregion

        #endregion
        
    }
}