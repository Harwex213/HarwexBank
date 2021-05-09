using System.Collections.Generic;

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
        
        // TODO: public abstract void GetNecessaryInfo();
    }
    
    public class AdminMainWindow : MainWindowFactory
    {
        public override List<IControlViewModel> GetPages()
        {
            return new();
        }
    }
    
    public class WorkerMainWindow : MainWindowFactory
    {
        public override List<IControlViewModel> GetPages()
        {
            return new()
            {
                new ClientsViewModel(),
                new CreditsPageViewModel(),
                new JournalViewModel()
            };
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
                new JournalViewModel()
            };
        }
    }
}