

using System.Collections.Generic;
using System.Windows;

namespace HarwexBank
{
    public class AccountViewModel : ObservableObject, IControlViewModel
    {
        public string Name => "Счета";
        public List<AccountModel> AccountModels { get; }

        public AccountViewModel(UserModel userModel)
        {
            // TODO Make loading Accounts of User from Database

            AccountModels = userModel.AccountList;
        }
    }
}