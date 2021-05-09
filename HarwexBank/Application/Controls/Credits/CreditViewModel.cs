using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HarwexBank
{
    public class CreditViewModel : BaseViewModel, IControlViewModel
    {
        public string Name => "Кредиты";
        public ObservableCollection<IssuedCreditModel> CreditModels { get; }
        public CreditViewModel()
        {
            CreditModels = new ObservableCollection<IssuedCreditModel>(MainViewModel.Data.LoggedInUser.IssuedCredits);
        }
    }
}