using System.Text;

namespace HarwexBank
{
    public class IssuedCreditModel : ObservableObject
    {
        private int _id;
        private string _takingDate;
        private int _term;
        private decimal _amount;
        private CreditTypeModel _creditType;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        public string TakingDate
        {
            get => _takingDate;
            set
            {
                _takingDate = value;
                OnPropertyChanged("TakingDate");
            }
        }

        public int Term
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

        public CreditTypeModel CreditType
        {
            get => _creditType;
            set
            {
                _creditType = value;
                OnPropertyChanged("CreditType");
            }
        }
    }
}