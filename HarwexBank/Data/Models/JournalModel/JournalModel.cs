using System;
using System.Collections.Generic;

namespace HarwexBank
{
    public class SortJournalByDate : IComparer<JournalModel>
    {
        public int Compare(JournalModel x, JournalModel y)
        {
            if (ReferenceEquals(x, y)) 
                return 0;
            
            if (ReferenceEquals(null, y)) 
                return 1;
            
            if (ReferenceEquals(null, x)) 
                return -1;
            
            return y.Date.CompareTo(x.Date);
        }
    }
    
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
    
    public partial class TransferToAccountModel : OperationModel
    {
        public int BankAccountReceiver { get; set; }
        public virtual AccountModel BankAccountModelReceiverNavigation { get; set; }
    }

    public partial class CreditRepaymentModel : OperationModel
    {
        public int SelectedCredit { get; set; }
        public virtual IssuedCreditModel SelectedCreditModelNavigation { get; set; }
    }
}