using System.Collections.Generic;

namespace HarwexBank
{
    public abstract class PagesFactory
    {
        public static PagesFactory GetFactoryByUserType(UserTypeModel userTypeModel)
        {
            return userTypeModel.Name switch
            {
                "admin" => new AdminPages(),
                "worker" => new WorkerPages(),
                "client" => new ClientPages(),
                _ => null
            };
        }

        public abstract List<IControlViewModel> GetPages();
    }
    
    public class AdminPages : PagesFactory
    {
        public override List<IControlViewModel> GetPages()
        {
            return new();
        }
    }
    
    public class WorkerPages : PagesFactory
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
    
    public class ClientPages : PagesFactory
    {
        public override List<IControlViewModel> GetPages()
        {
            return new()
            {
                new CardsViewModel(),
                new FinanceViewModel(),
                new OperationsViewModel(),
                new JournalViewModel()
            };
        }
    }
}