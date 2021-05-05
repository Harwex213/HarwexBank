using System.Collections.Generic;

namespace HarwexBank
{
    public class CardTypeModel : ObservableObject
    {
        public CardTypeModel()
        {
            Cards = new HashSet<CardModel>();
        }
        
        private int _id;
        private string _name;

        public virtual ICollection<CardModel> Cards { get; set; }
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
    }
}