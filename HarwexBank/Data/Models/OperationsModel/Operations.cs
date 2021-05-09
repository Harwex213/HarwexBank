using System;

namespace HarwexBank
{
    public class OperationModel : ObservableObject
    {
        public int Id { get; set; }
        public int BankAccountInitiator { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }

        public virtual AccountModel BankAccountModelInitiatorNavigation { get; set; }
    }
    
    public partial class TransferToAccount : OperationModel
    {
        public int BankAccountReceiver { get; set; }
        public virtual AccountModel BankAccountModelReceiverNavigation { get; set; }
    }

    public partial class CreditRepayment : OperationModel
    {
        public int SelectedCredit { get; set; }
        public virtual IssuedCreditModel SelectedCreditModelNavigation { get; set; }
    }
}