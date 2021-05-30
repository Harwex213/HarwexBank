using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
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
            
            _context.Database.SetCommandTimeout(60);
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
        private CurrencyConverter _currencyConverter;
        public CurrencyConverter CurrencyConverter => _currencyConverter ??= new CurrencyConverter();

        public ObservableCollection<UserModel> ExistedClients => new ObservableCollection<UserModel>(GetAllClients());
        public ObservableCollection<IssuedCreditModel> AllNonApprovedCredits =>  new ObservableCollection<IssuedCreditModel>(GetAllNonApprovedCredits());
        public ObservableCollection<JournalModel> AllJournal => new ObservableCollection<JournalModel>(GetAllJournalNotes());
        public ObservableCollection<CurrencyTypeModel> ExistedCurrencyTypes => new ObservableCollection<CurrencyTypeModel>(GetExistedCurrencyTypeModels());
        public ObservableCollection<CardTypeModel> ExistedCardTypes => new ObservableCollection<CardTypeModel>(GetExistedCardTypeModels());
        public ObservableCollection<CreditTypeModel> ExistedCreditTypes => new ObservableCollection<CreditTypeModel>(GetExistedCreditTypeModels());
        
        private IEnumerable<UserModel> GetAllClients()
        {
            return _context.Users?.Where(u => u.UserType == "client").ToList();
        }
        
        private IEnumerable<IssuedCreditModel> GetAllNonApprovedCredits()
        {
            return _context.IssuedCredits?.Where(c => !c.IsApproved).ToList();
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
            _context.AddAsync(model);
            _context.SaveChanges();
        }
        
        public void UpdateModel(IModel model)
        {
            _context.Update(model);
            _context.SaveChanges();
        }

        public void RemoveModel(IModel model)
        {
            _context.Remove(model);
            _context.SaveChanges();
        }

        public void GenerateCreditRepayment(JournalModel note, AccountModel accountInitiator,
            IssuedCreditModel selectedCredit)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Accounts.Update(accountInitiator);
                _context.IssuedCredits.Update(selectedCredit);
                GenerateJournalNote(note);
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                MessageBox.Show(e.Message);
            }
        }
        
        public void GenerateTransfer(JournalModel note, AccountModel accountInitiator,
            AccountModel accountReceiver)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Accounts.Update(accountInitiator);
                _context.Accounts.Update(accountReceiver);
                GenerateJournalNote(note);
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                MessageBox.Show(e.Message);
            }
        }

        public void GenerateJournalNote(JournalModel operation)
        {
            _context.Journal.Add(operation);
            _context.SaveChanges();
        }
        
        #endregion
    }
}