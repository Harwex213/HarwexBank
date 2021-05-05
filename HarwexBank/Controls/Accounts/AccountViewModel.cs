

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace HarwexBank
{
    public class AccountViewModel : ObservableObject, IControlViewModel
    {
        public string Name => "Счета";
        public ObservableCollection<AccountModel> AccountModels { get; }
        public AccountViewModel()
        {
            AccountModels = new ObservableCollection<AccountModel>(ApplicationViewModel.LoggedInUser.Accounts);
        }
    }
}