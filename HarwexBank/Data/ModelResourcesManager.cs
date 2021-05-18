using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HarwexBank
{
    public class ModelResourcesManager
    {
        public interface IModel { }
        private static ModelResourcesManager _manager;
        private HarwexBankContext _context;
        private ModelResourcesManager()
        {
            _context = new HarwexBankContext();
            
            _context.Users.Load();
            _context.UserTypes.Load();
             _context.Accounts.Load();
            _context.CurrencyTypes.Load();
            _context.IssuedCredits.Load();
            _context.CreditTypes.Load();
            _context.Cards.Load();
            _context.CardTypes.Load();
            _context.Journal.Load();
            _context.Notifications.Load();
            _context.Operations.Load();
            _context.TransferToAccounts.Load();
            _context.CreditRepayments.Load();
        }

        public static ModelResourcesManager GetInstance()
        {
            return _manager ??= new ModelResourcesManager();
        }

        #region ApplicationData
        
        // User Data.
        private UserModel _loggedInUser;
        
        public UserModel LoggedInUser
        {
            get => _loggedInUser;
            set
            {
                _loggedInUser = value;

                if (_loggedInUser == null) 
                    return;
                
                // Adding Accounts.
                UserAccounts = new ObservableCollection<AccountModel>(_loggedInUser?.Accounts);
                // Adding Cards.
                UserCards = new ObservableCollection<CardModel>();
                foreach (var account in _loggedInUser.Accounts)
                {
                    foreach (var card in account.Cards)
                    {
                        UserCards.Add(card);
                    }
                }
                // Adding Credits.
                UserCredits = new ObservableCollection<IssuedCreditModel>(_loggedInUser?.IssuedCredits
                    .Where(c => c.IsApproved).Where(c => !c.IsRepaid));
                // Adding Journal.
                var journal = _loggedInUser.Journal?.ToList();
                journal?.Sort(new SortJournalByDate());
                UserJournal = new ObservableCollection<JournalModel>(journal ?? new List<JournalModel>());
            }
        }
        
        public ObservableCollection<AccountModel> UserAccounts { get; private set; }
        public ObservableCollection<CardModel> UserCards { get; private set; }
        public ObservableCollection<IssuedCreditModel> UserCredits { get; private set; }
        public ObservableCollection<JournalModel> UserJournal { get; private set; }
        
        // Global Data.
        private ObservableCollection<UserModel> _existedClients;
        private ObservableCollection<IssuedCreditModel> _allNonApprovedCredits;
        private ObservableCollection<JournalModel> _allJournal;
        private ObservableCollection<CurrencyTypeModel> _existedCurrencyTypes;
        private ObservableCollection<CardTypeModel> _existedCardTypes;
        private ObservableCollection<CreditTypeModel> _existedCreditTypes;

        public ObservableCollection<UserModel> ExistedClients =>
            _existedClients ??= new ObservableCollection<UserModel>(GetAllClients());
        public ObservableCollection<IssuedCreditModel> AllNonApprovedCredits => 
            _allNonApprovedCredits ??= new ObservableCollection<IssuedCreditModel>(GetAllNonApprovedCredits());
        public ObservableCollection<JournalModel> AllJournal =>
            _allJournal ??= new ObservableCollection<JournalModel>(GetAllJournalNotes());
        public ObservableCollection<CurrencyTypeModel> ExistedCurrencyTypes =>
            _existedCurrencyTypes ??= new ObservableCollection<CurrencyTypeModel>(GetExistedCurrencyTypeModels());
        public ObservableCollection<CardTypeModel> ExistedCardTypes =>
            _existedCardTypes ??= new ObservableCollection<CardTypeModel>(GetExistedCardTypeModels());
        public ObservableCollection<CreditTypeModel> ExistedCreditTypes =>
            _existedCreditTypes ??= new ObservableCollection<CreditTypeModel>(GetExistedCreditTypeModels());
        
        private IEnumerable<UserModel> GetAllClients()
        {
            return _context.Users?.Where(u => u.UserType == "client").ToList();
        }
        
        private IEnumerable<IssuedCreditModel> GetAllNonApprovedCredits()
        {
            return _context.IssuedCredits?.Where(c => !c.IsRepaid).ToList();
        }

        private IEnumerable<JournalModel> GetAllJournalNotes()
        {
            var journal = _context.Journal?.ToList();
            journal?.Sort(new SortJournalByDate());
            return journal;
        }
        
        private IEnumerable<CurrencyTypeModel> GetExistedCurrencyTypeModels()
        {
            return _context.CurrencyTypes?.ToList();
        }
        
        private IEnumerable<CardTypeModel> GetExistedCardTypeModels()
        {
            return _context.CardTypes?.ToList();
        }
        
        private IEnumerable<CreditTypeModel> GetExistedCreditTypeModels()
        {
            return _context.CreditTypes?.ToList();
        }

        #endregion

        public UserModel GetUserByLogin(string login)
        {
            return _context.Users.FirstOrDefault(u => u.Login == login);
        }
        
        public AccountModel GetAccountById(int id)
        {
            return _context.Accounts.FirstOrDefault(a => a.Id == id);
        }

        #region CRUD operations

        public void AddModel(IModel model)
        {
            switch (model)
            {
                case UserModel userModel:
                    _context.Users.Add(userModel);
                    break;
                case UserTypeModel userTypeModel:
                    _context.UserTypes.Add(userTypeModel);
                    break;
                case IssuedCreditModel issuedCreditModel:
                    _context.IssuedCredits.Add(issuedCreditModel);
                    break;
                case CreditTypeModel creditTypeModel:
                    _context.CreditTypes.Add(creditTypeModel);
                    break;
                case AccountModel accountModel:
                    _context.Accounts.Add(accountModel);
                    break;
                case CurrencyTypeModel currencyTypeModel:
                    _context.CurrencyTypes.Add(currencyTypeModel);
                    break;
                case CardModel cardModel:
                    _context.Cards.Add(cardModel);
                    break;
                case CardTypeModel cardTypeModel:
                    _context.CardTypes.Add(cardTypeModel);
                    break;
            }
            
            _context.SaveChanges();
        }
        
        public void UpdateModel(IModel model)
        {
            switch (model)
            {
                case UserModel userModel:
                    _context.Users.Update(userModel);
                    break;
                case UserTypeModel userTypeModel:
                    _context.UserTypes.Update(userTypeModel);
                    break;
                case IssuedCreditModel issuedCreditModel:
                    _context.IssuedCredits.Update(issuedCreditModel);
                    break;
                case CreditTypeModel creditTypeModel:
                    _context.CreditTypes.Update(creditTypeModel);
                    break;
                case AccountModel accountModel:
                    _context.Accounts.Update(accountModel);
                    break;
                case CurrencyTypeModel currencyTypeModel:
                    _context.CurrencyTypes.Update(currencyTypeModel);
                    break;
                case CardModel cardModel:
                    _context.Cards.Update(cardModel);
                    break;
                case CardTypeModel cardTypeModel:
                    _context.CardTypes.Update(cardTypeModel);
                    break;
            }
            
            _context.SaveChanges();
        }

        public void RemoveModel(IModel model)
        {
            switch (model)
            {
                case UserModel userModel:
                    _context.Users.Remove(userModel);
                    break;
                case UserTypeModel userTypeModel:
                    _context.UserTypes.Remove(userTypeModel);
                    break;
                case IssuedCreditModel issuedCreditModel:
                    _context.IssuedCredits.Remove(issuedCreditModel);
                    break;
                case CreditTypeModel creditTypeModel:
                    _context.CreditTypes.Remove(creditTypeModel);
                    break;
                case AccountModel accountModel:
                    _context.Accounts.Remove(accountModel);
                    break;
                case CurrencyTypeModel currencyTypeModel:
                    _context.CurrencyTypes.Remove(currencyTypeModel);
                    break;
                case CardModel cardModel:
                    _context.Cards.Remove(cardModel);
                    break;
                case CardTypeModel cardTypeModel:
                    _context.CardTypes.Remove(cardTypeModel);
                    break;
            }
            
            _context.SaveChanges();
        }

        public void GenerateCreditRepayment(JournalModel note, AccountModel accountInitiator,
            IssuedCreditModel selectedCredit)
        {
            _context.Accounts.Update(accountInitiator);
            _context.IssuedCredits.Update(selectedCredit);
            GenerateJournalNote(note);
        }
        
        public void GenerateTransfer(JournalModel note, AccountModel accountInitiator,
            AccountModel accountReceiver)
        {
            _context.Accounts.Update(accountInitiator);
            _context.Accounts.Update(accountReceiver);
            GenerateJournalNote(note);
        }

        public void GenerateJournalNote(JournalModel operation)
        {
            _context.Journal.Add(operation);
            _context.SaveChanges();
        }
        
        #endregion
    }
}