using System.Diagnostics.CodeAnalysis;

namespace HarwexBank
{
    public class ModelResourcesManager
    {
        private static ModelResourcesManager _manager;
        private HarwexBankContext _dbContext;

        private ModelResourcesManager()
        {
            _dbContext = new HarwexBankContext();
        }

        public static ModelResourcesManager GetInstance()
        {
            return _manager ??= new ModelResourcesManager();
        }
    }
}