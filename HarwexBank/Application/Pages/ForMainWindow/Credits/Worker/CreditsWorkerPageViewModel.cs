using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;

namespace HarwexBank
{
    public class CreditsWorkerPageViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "Кредиты";

        public CreditsWorkerPageViewModel()
        {
            AwaitToApprovedCreditModels = ModelResourcesManager.GetInstance().AllNonApprovedCredits;
        }

        public ObservableCollection<IssuedCreditModel> AwaitToApprovedCreditModels { get; }

        #region Commands

        // Fields.
        private ICommand _acceptCreditCommand;
        private ICommand _declineCreditCommand;
        
        // Props.
        public ICommand AcceptCreditCommand
        {
            get
            {
                _acceptCreditCommand ??= new RelayCommand(
                    c => AcceptCredit((IssuedCreditModel) c),
                    c => c is IssuedCreditModel);
        
                return _acceptCreditCommand;
            }
        }
        
        public ICommand DeclineCreditCommand
        {
            get
            {
                _declineCreditCommand ??= new RelayCommand(
                    c => DeclineCredit((IssuedCreditModel) c),
                    c => c is IssuedCreditModel);
        
                return _declineCreditCommand;
            }
        }
        
        // Methods.
        private void AcceptCredit(IssuedCreditModel issuedCreditModel)
        {
            issuedCreditModel.IsApproved = true;
            
            var currencyConverter = ModelResourcesManager.GetInstance().CurrencyConverter;
            var accountToPay = ModelResourcesManager.GetInstance().GetAccountById(issuedCreditModel.AccountId);
            accountToPay.Amount += currencyConverter.ConvertCurrencies(
                accountToPay.CurrencyTypeModelNavigation.CurrencyTypeEnum,
                issuedCreditModel.CreditTypeModelNavigation.CurrencyTypeModelNavigation.CurrencyTypeEnum,
                issuedCreditModel.Amount);

            ModelResourcesManager.GetInstance().UpdateModel(issuedCreditModel);
            AwaitToApprovedCreditModels.Remove(issuedCreditModel);
            
            MessageBox.Show("Кредит принят");
        }
        
        private void DeclineCredit(IssuedCreditModel issuedCreditModel)
        {
            ModelResourcesManager.GetInstance().RemoveModel(issuedCreditModel);
            AwaitToApprovedCreditModels.Remove(issuedCreditModel);
            
            MessageBox.Show("Кредит отклонен");
        }

        #endregion
    }
}