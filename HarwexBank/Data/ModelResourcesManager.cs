using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HarwexBank
{
    public class ModelResourcesManager
    {
        private static ModelResourcesManager _manager;
        private HarwexBankContext _context;

        private ModelResourcesManager()
        {
            _context = new HarwexBankContext();
        }

        public static ModelResourcesManager GetInstance()
        {
            return _manager ??= new ModelResourcesManager();
        }
        
        
        
        public UserModel GetUserByLogin(string login)
        {
            return _context.Users.FirstOrDefault(u => u.Login == login);
        }
        
        public IEnumerable<UserModel> GetAllClients()
        {
            return _context.Users.Where(u => u.UserType == "client").ToList();
        }
    }
}