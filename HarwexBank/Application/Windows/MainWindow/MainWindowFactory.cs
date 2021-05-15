using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HarwexBank
{
    public abstract class MainWindowFactory
    {
        public static MainWindowFactory GetFactory()
        {
            return ModelResourcesManager.GetInstance().LoggedInUser.UserType switch
            {
                "admin" => new AdminMainWindow(),
                "worker" => new WorkerMainWindow(),
                "client" => new ClientMainWindow(),
                _ => null
            };
        }

        public abstract List<IControlViewModel> GetPages();
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
    }
}