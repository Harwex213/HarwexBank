namespace HarwexBank
{
    public class CardModel : ObservableObject, ModelResourcesManager.IModel
    {
        private int _id;
        private int _accountId;
        private string _cardType;
        private string _number;
        private string _ownerName;
        private string _timeFrame;
        private string _cvv;
        
        public virtual AccountModel BankAccountModel { get; set; }
        public virtual CardTypeModel CardTypeModelNavigation { get; set; }
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        public int AccountId
        {
            get => _accountId;
            set
            {
                _accountId = value;
                OnPropertyChanged("AccountId");
            }
        }

        public string CardType
        {
            get => _cardType;
            set
            {
                _cardType = value;
                OnPropertyChanged("CardType");
            }
        }

        public string Number
        {
            get => _number;
            set
            {
                _number = value;
                OnPropertyChanged("Number");
            }
        }

        public string OwnerName
        {
            get => _ownerName;
            set
            {
                _ownerName = value;
                OnPropertyChanged("OwnerName");
            }
        }

        public string TimeFrame
        {
            get => _timeFrame;
            set
            {
                _timeFrame = value;
                OnPropertyChanged("TimeFrame");
            }
        }

        public string Cvv
        {
            get => _cvv;
            set
            {
                _cvv = value;
                OnPropertyChanged("Cvv");
            }
        }
    }
}