using System.Windows.Controls;

namespace HarwexBank
{
    public class CreditRepayment : Control, IControlViewModel
    {
        public new string Name => "Операции";
    }

    public class TransferToAccount : Control, IControlViewModel
    {
        public new string Name => "Операции";
    }
    
    public class OperationsPageViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "Операции";

        public OperationsPageViewModel()
        {
            
        }
        
        
    }
}