using System.Collections.Generic;

namespace HarwexBank
{
    public class UserModel : ObservableObject
    {
        public UserModel()
        {
            Accounts = new HashSet<AccountModel>();
            IssuedCredits = new HashSet<IssuedCreditModel>();
        }
        
        private int _id;
        private string _firstName;
        private string _lastName;
        private string _patronymic;
        private string _address;
        private string _passport;
        private string _login;
        private string _password;
        private bool _isBlocked;
        public string UserType { get; set; }

        public virtual UserTypeModel UserTypeModelNavigation { get; set; }
        public virtual ICollection<AccountModel> Accounts { get; set; }
        public virtual ICollection<IssuedCreditModel> IssuedCredits { get; set; }
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        public string Patronymic
        {
            get => _patronymic;
            set
            {
                _patronymic = value;
                OnPropertyChanged("Patronymic");
            }
        }

        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged("Address");
            }
        }

        public string Passport
        {
            get => _passport;
            set
            {
                _passport = value;
                OnPropertyChanged("Passport");
            }
        }

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged("Login");
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }
        public bool IsBlocked
        {
            get => _isBlocked;
            set
            {
                _isBlocked = value;
                OnPropertyChanged("IsBlocked");
            }
        }
    }
}