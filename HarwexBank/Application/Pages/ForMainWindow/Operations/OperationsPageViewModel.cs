

namespace HarwexBank
{
    public class OperationsPageViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "Операции";

        public OperationsPageViewModel()
        {
            ControlViewModels.Add(new TransferToAccountViewModel());
            ControlViewModels.Add(new CreditRepaymentViewModel());

            SelectedControlViewModel = ControlViewModels[0];
        }
    }
}