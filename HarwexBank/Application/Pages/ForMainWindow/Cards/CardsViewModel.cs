using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HarwexBank
{
    public class CardsViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "Карты";

        public CardsViewModel()
        {
            CardToCreate = new CardModel();
            
            LoggedInUser = ModelResourcesManager.GetInstance().LoggedInUser;
            UserCards = ModelResourcesManager.GetInstance().UserCards;
            UserAccounts = ModelResourcesManager.GetInstance().UserAccounts;
            CardTypeModels = ModelResourcesManager.GetInstance().ExistedCardTypes;

            if (UserCards.Count > 0)
            {
                SelectedCard = UserCards[0];
            }
            
            ControlViewModels.Add(new CardsListViewModel());
            ControlViewModels.Add(new CreateNewCardViewModel());

            SelectedControlViewModel = ControlViewModels[0];
        }

        // Using Data
        public UserModel LoggedInUser { get; set; }
        
        // Using in Card List

        #region Fields

        private CardModel _selectedCard;
        private AccountModel _selectedAccount;
        private JournalModel _lastOperation;
        private string _lastOperationName;

        #endregion
        
        public ObservableCollection<CardModel> UserCards { get; set; }

        public CardModel SelectedCard
        {
            get => _selectedCard;
            set
            {
                _selectedCard = value;
                SelectedAccount = ModelResourcesManager.GetInstance().GetAccountById(SelectedCard.AccountId);
                LastOperation = SelectedAccount.Operations.LastOrDefault();
                LastOperationName = LastOperation switch
                {
                    TransferToAccountModel => "Переводы",
                    CreditRepaymentModel => "Погашение кредита",
                    _ => ""
                };
                OnPropertyChanged("SelectedCard");
            }
        }

        public AccountModel SelectedAccount
        {
            get => _selectedAccount;
            set
            {
                _selectedAccount = value;
                OnPropertyChanged("SelectedAccount");
            }
        }
        public JournalModel LastOperation
        {
            get => _lastOperation;
            set
            {
                _lastOperation = value;
                OnPropertyChanged("LastOperation");
            }
        }
        public string LastOperationName
        {
            get => _lastOperationName;
            set
            {
                _lastOperationName = value;
                OnPropertyChanged("LastOperationName");
            }
        }

        // Using in Creating New Card
        public CardModel CardToCreate { get; set; }
        public ObservableCollection<AccountModel> UserAccounts { get; set; }
        public AccountModel SelectedAccountToAdd { get; set; }
        public ObservableCollection<CardTypeModel> CardTypeModels { get; }

        #region Commands

        // Fields.
        private ICommand _goToCardCreationCommand;
        private ICommand _createNewCardCommand;
        private ICommand _goToCardListCommand;
        private ICommand _selectCardCommand;
        private ICommand _deleteCardCommand;

        // Props.
        public ICommand GoToCardCreationCommand
        {
            get
            {
                _goToCardCreationCommand ??= new RelayCommand(
                    _ => GoToCardCreation());
        
                return _goToCardCreationCommand;
            }
        }
        
        public ICommand CreateNewCardCommand
        {
            get
            {
                _createNewCardCommand ??= new RelayCommand(
                    _ => CreateNewCard());
        
                return _createNewCardCommand;
            }
        }

        public ICommand DeleteCardCommand
        {
            get
            {
                _deleteCardCommand ??= new RelayCommand(
                    c => DeleteCard((CardModel) c),
                    c => c is CardModel);
        
                return _deleteCardCommand;
            }
        }

        public ICommand SelectCardCommand
        {
            get
            {
                _selectCardCommand ??= new RelayCommand(
                    c => SelectCard((CardModel) c),
                    c => c is CardModel);
        
                return _selectCardCommand;
            }
        }

        public ICommand GoToCardListCommand
        {
            get
            {
                _goToCardListCommand ??= new RelayCommand(
                    _ => GoToCardList());
        
                return _goToCardListCommand;
            }
        }
        
        // Methods.
        private void GoToCardCreation()
        {
            SelectedControlViewModel = ControlViewModels[1];
        }

        private void GoToCardList()
        {
            SelectedControlViewModel = ControlViewModels[0];
        }
        
        private void SelectCard(CardModel cardModel)
        {
            SelectedCard = cardModel;
        }

        private void DeleteCard(CardModel cardModel)
        {
            switch (MessageBox.Show("Вы уверены?", "Удаление карты", MessageBoxButton.YesNo))
            {
                case MessageBoxResult.None:
                    break;
                case MessageBoxResult.OK:
                    break;
                case MessageBoxResult.Cancel:
                    break;
                case MessageBoxResult.Yes:
                    UserCards.Remove(cardModel);
                    ModelResourcesManager.GetInstance().RemoveModel(cardModel);
                    break;
                case MessageBoxResult.No:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void CreateNewCard()
        {
            // Creating new Card.
            var number = string.Empty;
            for (var i = 0; i < 16; i++)
            {
                number += new Random().Next(0, 9).ToString();
            }
            CardToCreate.AccountId = SelectedAccountToAdd.Id;
            CardToCreate.Number = number;
            CardToCreate.OwnerName = ModelResourcesManager.GetInstance().LoggedInUser.FirstName.ToUpper() + " " +
                                     ModelResourcesManager.GetInstance().LoggedInUser.LastName.ToUpper();
            CardToCreate.TimeFrame = new Random().Next(0, 12) + "/" + (DateTime.Now.Year + 3).ToString()[2..];
            CardToCreate.Cvv = new Random().Next(0, 9).ToString() + new Random().Next(0, 9) + new Random().Next(0, 9);

            // Add it's to DataBase + to User Cards
            UserCards.Add(CardToCreate);
            ModelResourcesManager.GetInstance().AddModel(CardToCreate);

            // Add it to User Accounts.
            ModelResourcesManager.GetInstance().UserAccounts.First(a => a.Id == SelectedAccountToAdd.Id)
                .Cards.Add(CardToCreate);
            CardToCreate = new CardModel();
            
            GoToCardList();
        }

        #endregion
    }
}