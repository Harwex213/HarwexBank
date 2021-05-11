using System;

namespace HarwexBank
{
    public abstract class JournalModel : ObservableObject
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public virtual UserModel UserIdNavigation { get; set; }
    }
    
    public class NotificationModel : JournalModel
    {
        public string Message { get; set; }
    }
    
    public class OperationModel : JournalModel
    {
        public int BankAccountInitiator { get; set; }
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