using System.Collections.Generic;

namespace HarwexBank
{
    public class FinanceViewModel : BaseControlViewModel, IControlViewModel 
    {
        public string Name => "Финансы";

        public FinanceViewModel()
        {
            ControlViewModels.Add(new AccountViewModel());
            ((AccountViewModel) ControlViewModels[0]).ControlViewModelOwner = this;
            
            ControlViewModels.Add(new CreditViewModel());
            ((CreditViewModel) ControlViewModels[1]).ControlViewModelOwner = this;

            SelectedControlViewModel = ControlViewModels[0];
        }
    }
}