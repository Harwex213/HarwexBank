using System.Collections.Generic;

namespace HarwexBank
{
    public class CreditViewModel : ObservableObject, IControlViewModel
    {
        public string Name => "Кредиты";
        public List<IssuedCreditModel> CreditModels { get; }
        public CreditViewModel(UserModel userModel)
        {
            CreditModels = userModel.CreditList;
        }
    }
}