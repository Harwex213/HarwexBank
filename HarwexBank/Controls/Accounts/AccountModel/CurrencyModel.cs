namespace HarwexBank
{
    public class CurrencyModel : ObservableObject
    {
        private int _id;
        private string _name;

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