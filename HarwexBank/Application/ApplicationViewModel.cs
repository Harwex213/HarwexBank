using System.Collections.Generic;

namespace HarwexBank
{
    public class ApplicationViewModel : ObservableObject
    {
        private IPageViewModel _selectedPageViewModel;
        private List<IPageViewModel> _pageViewModels;
        private UserModel _user;

        public IPageViewModel SelectedPageViewModel
        {
            get => _selectedPageViewModel;
            set
            {
                if (_selectedPageViewModel == value)
                    return;
                _selectedPageViewModel = value;
                OnPropertyChanged("SelectedPageViewModel");
            }
        }

        public List<IPageViewModel> PageViewModels
        {
            get { return _pageViewModels ??= new List<IPageViewModel>(); }
        }

        public UserModel User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged("User");
            }
        }

        public ApplicationViewModel()
        {
            // TODO - Take User from DataBase by Login&Password

            #region USER CREATION - EMULATION

             var user = new UserModel()
            {
                Id = 0,
                FirstName = "Oleg",
                LastName = "Kaportsev",
                Patronymic = "Andreevich",
                Address = "Vitebsk, Pobedu avenue, 10, 61",
                Passport = "BM1234567",
                UserType = new UserTypeModel() {Id = 0, Name = "admin"},
                Login = "123",
                Password = "123",
                AccountList = new List<AccountModel>()
                {
                    new()
                    {
                        Id = 0,
                        Cash = 112412,
                        CreationDate = "12.04.2012",
                        CardsList = new List<CardModel>()
                        {
                            new()
                            {
                                Id = 0,
                                CardNumber = "6711 0077 1050 4715",
                                CardName = "ALEH KAPORTSAU",
                                CardPeriod = "08/22",
                                CardCvv = "000",
                                CardType = new CardTypeModel() {Id = 0, Name = "Visa Classic"},
                                CardCreationDate = "10.02.2012"
                            },
                            new()
                            {
                                Id = 0,
                                CardNumber = "6711 0077 1050 4715",
                                CardName = "ALEH KAPORTSAU",
                                CardPeriod = "08/22",
                                CardCvv = "000",
                                CardType = new CardTypeModel() {Id = 0, Name = "Visa Classic"},
                                CardCreationDate = "10.02.2012"
                            },
                            new()
                            {
                                Id = 0,
                                CardNumber = "6711 0077 1050 4715",
                                CardName = "ALEH KAPORTSAU",
                                CardPeriod = "08/22",
                                CardCvv = "000",
                                CardType = new CardTypeModel() {Id = 0, Name = "Visa Classic"},
                                CardCreationDate = "10.02.2012"
                            }
                        }
                    },
                    new()
                    {
                        Id = 0,
                        Cash = 112412,
                        CreationDate = "12.04.2012",
                        CardsList = new List<CardModel>()
                        {
                            new()
                            {
                                Id = 0,
                                CardNumber = "6711 0077 1050 4715",
                                CardName = "ALEH KAPORTSAU",
                                CardPeriod = "08/22",
                                CardCvv = "000",
                                CardType = new CardTypeModel() {Id = 0, Name = "Visa Classic"},
                                CardCreationDate = "10.02.2012"
                            },
                            new()
                            {
                                Id = 0,
                                CardNumber = "6711 0077 1050 4715",
                                CardName = "ALEH KAPORTSAU",
                                CardPeriod = "08/22",
                                CardCvv = "000",
                                CardType = new CardTypeModel() {Id = 0, Name = "Visa Classic"},
                                CardCreationDate = "10.02.2012"
                            },
                            new()
                            {
                                Id = 0,
                                CardNumber = "6711 0077 1050 4715",
                                CardName = "ALEH KAPORTSAU",
                                CardPeriod = "08/22",
                                CardCvv = "000",
                                CardType = new CardTypeModel() {Id = 0, Name = "Visa Classic"},
                                CardCreationDate = "10.02.2012"
                            }
                        }
                    }
                }
            };

            #endregion

            PageViewModels.Add(new CardsViewModel(user));
            PageViewModels.Add(new FinanceViewModel(user));

            SelectedPageViewModel = PageViewModels[1];
        }
    }
}