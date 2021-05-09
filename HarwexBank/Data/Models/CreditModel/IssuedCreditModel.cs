using System;
using System.Collections.Generic;
using System.Text;

namespace HarwexBank
{
    public class IssuedCreditModel : ObservableObject, ModelResourcesManager.IModel
    {
        public IssuedCreditModel()
        {
            CreditRepayments = new HashSet<CreditRepayment>();
        }
        
        private int _id;
        private int _userId;
        private string _creditType;
        private DateTime _dateIn;
        private long _term;
        private decimal _amount;
        private bool _isApproved;
        private bool _isRepaid;

        public virtual CreditTypeModel CreditTypeModelNavigation { get; set; }
        public virtual UserModel UserModelAccount { get; set; }
        public virtual ICollection<CreditRepayment> CreditRepayments { get; set; }
        
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }
        public int UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                OnPropertyChanged("UserId");
            }
        }

        public string CreditType
        {
            get => _creditType;
            set
            {
                _creditType = value;
                OnPropertyChanged("CreditType");
            }
        }

        public DateTime DateIn
        {
            get => _dateIn;
            set
            {
                _dateIn = value;
                OnPropertyChanged("DateIn");
            }
        }
        public long Term
        {
            get => _term;
            set
            {
                _term = value;
                OnPropertyChanged("Term");
            }
        }
        public decimal Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged("Amount");
            }
        }
        public bool IsApproved
        {
            get => _isApproved;
            set
            {
                _isApproved = value;
                OnPropertyChanged("IsApproved");
            }
        }
        public bool IsRepaid
        {
            get => _isRepaid;
            set
            {
                _isRepaid = value;
                OnPropertyChanged("IsRepaid");
            }
        }
    }
}