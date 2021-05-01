

namespace HarwexBank.Models
{
    public class User : ObservableObject
    {
        private int _id;
        private string _firstName;
        private string _lastName;
        private string _patronymic;
        private string _address;
        private string _passport;
        private UserType _userType;
        private string _login;
        private string _password;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string FirstName
        {
            get => _firstName;
            set => _firstName = value;
        }

        public string LastName
        {
            get => _lastName;
            set => _lastName = value;
        }

        public string Patronymic
        {
            get => _patronymic;
            set => _patronymic = value;
        }

        public string Address
        {
            get => _address;
            set => _address = value;
        }

        public string Passport
        {
            get => _passport;
            set => _passport = value;
        }

        public UserType UserType
        {
            get => _userType;
            set => _userType = value;
        }

        public string Login
        {
            get => _login;
            set => _login = value;
        }

        public string Password
        {
            get => _password;
            set => _password = value;
        }
    }
}