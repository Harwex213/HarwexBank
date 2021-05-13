using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HarwexBank
{
    public abstract class MainWindowFactory
    {
        public static MainWindowFactory GetFactoryByUserType(UserTypeModel userTypeModel)
        {
            return userTypeModel.Name switch
            {
                "admin" => new AdminMainWindow(),
                "worker" => new WorkerMainWindow(),
                "client" => new ClientMainWindow(),
                _ => null
            };
        }

        public abstract List<IControlViewModel> GetPages();
        public abstract void GetNecessaryInfo(MainWindowInfo mainWindowInfo);
    }
    
    public class AdminMainWindow : MainWindowFactory
    {
        public override List<IControlViewModel> GetPages()
        {
            return new()
            {
                new CreditsAdminPageViewModel()
            };
        }

        public override void GetNecessaryInfo(MainWindowInfo mainWindowInfo)
        {
            mainWindowInfo.ExistedCreditTypes = new ObservableCollection<CreditTypeModel>(
                ModelResourcesManager.GetInstance().GetExistedCreditTypeModels());
        }
    }
    
    public class WorkerMainWindow : MainWindowFactory
    {
        public override List<IControlViewModel> GetPages()
        {
            return new()
            {
                new ClientsViewModel(),
                new CreditsWorkerPageViewModel(),
                new JournalWorkerViewModel()
            };
        }
        
        public override void GetNecessaryInfo(MainWindowInfo mainWindowInfo)
        {
            mainWindowInfo.ExistedClients = new ObservableCollection<UserModel>(
                ModelResourcesManager.GetInstance().GetAllClients());

            var journal = ModelResourcesManager.GetInstance().GetJournalNotes()?.ToList();
            journal?.Sort(new SortJournalByDate());
            mainWindowInfo.GlobalJournal = new ObservableCollection<JournalModel>(journal ?? new List<JournalModel>());
            
            mainWindowInfo.AllNonApprovedCredits = new ObservableCollection<IssuedCreditModel>(
                ModelResourcesManager.GetInstance().GetAllTakingCredits().Where(c => !c.IsApproved));
        }
    }
    
    public class ClientMainWindow : MainWindowFactory
    {
        public override List<IControlViewModel> GetPages()
        {
            return new()
            {
                new CardsViewModel(),
                new FinanceViewModel(),
                new OperationsPageViewModel(),
                new JournalClientViewModel()
            };
        }
        
        public override void GetNecessaryInfo(MainWindowInfo mainWindowInfo)
        {
            mainWindowInfo.UserAccounts = new ObservableCollection<AccountModel>(
                mainWindowInfo.LoggedInUser.Accounts);

            var userCards = new ObservableCollection<CardModel>();
            foreach (var account in mainWindowInfo.UserAccounts)
            {
                foreach (var card in account.Cards)
                {
                    userCards.Add(card);
                }
            }
            mainWindowInfo.UserCards = userCards;
            
            mainWindowInfo.UserCredits = new ObservableCollection<IssuedCreditModel>(
                mainWindowInfo.LoggedInUser.IssuedCredits.Where(c => c.IsApproved));
            
            var journal = mainWindowInfo.LoggedInUser.Journal?.ToList();
            journal?.Sort(new SortJournalByDate());
            mainWindowInfo.UserJournal = new ObservableCollection<JournalModel>(journal ?? new List<JournalModel>());
            
            mainWindowInfo.ExistedCardTypes = new ObservableCollection<CardTypeModel>(
                ModelResourcesManager.GetInstance().GetExistedCardTypeModels());
            
            mainWindowInfo.ExistedCreditTypes = new ObservableCollection<CreditTypeModel>(
                ModelResourcesManager.GetInstance().GetExistedCreditTypeModels());
            
            mainWindowInfo.ExistedCurrencyTypes = new ObservableCollection<CurrencyTypeModel>(
                ModelResourcesManager.GetInstance().GetExistedCurrencyTypeModels());
        }
    }
}