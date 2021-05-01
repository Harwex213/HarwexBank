using System.Collections.Generic;

namespace HarwexBank
{
    public class CreditViewModel : ObservableObject, IControlViewModel
    {
        public string Name => "Кредиты";
        public List<CreditModel> CreditModels { get; }
        public CreditViewModel(UserModel userModel)
        {
            CreditModels = userModel.CreditList;
        }
    }
}