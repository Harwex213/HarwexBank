

using System.Collections.Generic;

namespace HarwexBank
{
    public class AccountViewModel : ObservableObject, IControlViewModel
    {
        public string Name => "Счета";

        public List<AccountModel> AccountModels { get; }

        public AccountViewModel()
        {
            
        }
    }
}