using System.Collections.Generic;
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
            _context.Operations.Load();
            _context.TransferToAccounts.Load();
            _context.CreditRepayments.Load();
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
            
            _context.SaveChangesAsync();
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
            
            _context.SaveChangesAsync();
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
            
            _context.SaveChangesAsync();
        }

        public void GenerateOperation(OperationModel operation)
        {
            _context.Operations.Add(operation);
            
            _context.SaveChangesAsync();
        }
    }
}