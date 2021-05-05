using System.Collections.Generic;

namespace HarwexBank
{
    public class AccountModel : ObservableObject
    {
        private int _id;
        private string _creationDate;
        private decimal _cash;
        private CurrencyModel _currency;
        private List<CardModel> _cardsList;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        public string CreationDate
        {
            get => _creationDate;
            set
            {
                _creationDate = value;
                OnPropertyChanged("CreationDate");
            }
        }

        public decimal Cash
        {
            get => _cash;
            set
            {
                _cash = value;
                OnPropertyChanged("Cash");
            }
        }

        public CurrencyModel Currency
        {
            get => _currency;
            set
            {
                _currency = value;
                OnPropertyChanged("Currency");
            }
        }

        public List<CardModel> CardsList
        {
            get => _cardsList;
            set
            {
                _cardsList = value;
                OnPropertyChanged("CardsList");
            }
        }
    }
}