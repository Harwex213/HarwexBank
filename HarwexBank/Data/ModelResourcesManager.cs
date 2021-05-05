using System.Collections.Generic;
using System.Linq;

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
        }

        public void StartOperation(OperationModel operation)
        {
            _context.Operations.Add(operation);
        }
    }
}