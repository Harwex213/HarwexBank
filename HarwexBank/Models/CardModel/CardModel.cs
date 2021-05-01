namespace HarwexBank
{
    public class CardModel : ObservableObject
    {
        private int _id;
        private string _cardNumber;
        private string _cardName;
        private string _cardPeriod;
        private string _cardCVV;
        private string _cardCreationDate;
        private CardTypeModel _cardType;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        public string CardNumber
        {
            get => _cardNumber;
            set
            {
                _cardNumber = value;
                OnPropertyChanged("CardNumber");
            }
        }

        public string CardName
        {
            get => _cardName;
            set
            {
                _cardName = value;
                OnPropertyChanged("CardName");
            }
        }

        public string CardPeriod
        {
            get => _cardPeriod;
            set
            {
                _cardPeriod = value;
                OnPropertyChanged("CardPeriod");
            }
        }

        public string CardCvv
        {
            get => _cardCVV;
            set
            {
                _cardCVV = value;
                OnPropertyChanged("CardCvv");
            }
        }

        public string CardCreationDate
        {
            get => _cardCreationDate;
            set
            {
                _cardCreationDate = value;
                OnPropertyChanged("CardCreationDate");
            }
        }

        public CardTypeModel CardType
        {
            get => _cardType;
            set
            {
                _cardType = value;
                OnPropertyChanged("CardType");
            }
        }
    }
}