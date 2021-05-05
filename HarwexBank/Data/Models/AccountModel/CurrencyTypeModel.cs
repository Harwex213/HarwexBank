using System.Collections.Generic;

namespace HarwexBank
{
    public class CurrencyTypeModel : ObservableObject
    {
        public CurrencyTypeModel()
        {
            Accounts = new HashSet<AccountModel>();
        }

        private int _id;
        private string _name;

        public virtual ICollection<AccountModel> Accounts { get; set; }
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