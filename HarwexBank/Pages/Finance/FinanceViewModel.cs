using System.Collections.Generic;

namespace HarwexBank
{
    public class FinanceViewModel : BasePageViewModel, IPageViewModel
    {
        public string Name => "Финансы";

        public FinanceViewModel()
        {
            ControlViewModels.Add(new AccountViewModel());
            ControlViewModels.Add(new CreditViewModel());

            SelectedControlViewModel = ControlViewModels[0];
        }
    }
}