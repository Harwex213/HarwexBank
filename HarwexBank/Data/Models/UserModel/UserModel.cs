using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace HarwexBank
{
    public class UserModel : ObservableObject, ModelResourcesManager.IModel, IDataErrorInfo
    {
        public UserModel()
        {
            Accounts = new HashSet<AccountModel>();
            IssuedCredits = new HashSet<IssuedCreditModel>();
        }
        public string FullName => FirstName + " " + LastName + " " + Patronymic;
        
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
        
        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string name]
        {
            get
            {
                string result = null;
                var stringNameToCheck = string.Empty;

                switch (name)
                {
                    case nameof(FirstName):
                        stringNameToCheck = FirstName;
                        goto case "CheckFullName";
                        
                    case nameof(LastName):
                        stringNameToCheck = LastName;
                        goto case "CheckFullName";
                        
                    case nameof(Patronymic):
                        if (!string.IsNullOrEmpty(Patronymic))
                        {
                            stringNameToCheck = Patronymic;
                            goto case "CheckFullName";
                        }
                        break;

                    case "CheckFullName":
                        if (stringNameToCheck.Length is < 2 or > 50)
                        {
                            result = "Должно быть длиннее 2 символов и меньше 50";
                        }
                
                        if (Regex.Match(stringNameToCheck, @"^[A-Z][a-zA-Z]*$").Success)
                        {
                            result = "Должно содержать только буквы";
                        }
                        break;
                    
                    case nameof(Address):
                        if (stringNameToCheck.Length > 6)
                        {
                            result = "Должен быть длиннее 6 символов.";
                        }
                        break;
                    
                    case nameof(Passport):
                        if (Regex.Match(Address, @"^[A-Z]{2}[0-9]{7}$").Success)
                        {
                            result = "Первые два символа должны быть латинские буквы. Следующие семь цифры.";
                        }
                        break;
                    
                    case nameof(Login):
                        
                        break;
                }

                return result;
            }
        }
    }
}