using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HarwexBank
{
    public class CreditViewModel : ObservableObject, IControlViewModel
    {
        public string Name => "Кредиты";
        public ObservableCollection<IssuedCreditModel> CreditModels { get; }
        public CreditViewModel()
        {
            CreditModels = new ObservableCollection<IssuedCreditModel>(ApplicationViewModel.LoggedInUser.IssuedCredits);
        }
    }
}