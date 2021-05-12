using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HarwexBank
{
    public class CreditListViewModel : Control, IControlViewModel { }
    public class TakeNewCreditViewModel : Control, IControlViewModel { }
    public class CreditViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "Кредиты";
        public CreditViewModel()
        {
            CreditToTaking = new IssuedCreditModel();

            switch (MainViewModel.Data.LoggedInUser.UserType)
            {
                case "client":
                    UserApprovedCreditModels = MainViewModel.Data.UserCredits;
                    CreditTypeModels = MainViewModel.Data.ExistedCreditTypes;
                    break;
                
                case "worker":
                    UserApprovedCreditModels = MainViewModel.Data.UserCredits;
                    AwaitToApprovedCreditModels = MainViewModel.Data.AllNonApprovedCredits;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            ControlViewModels.Add(new CreditListViewModel());
            ControlViewModels.Add(new TakeNewCreditViewModel());

            SelectedControlViewModel = ControlViewModels[0];
        }
        
        // Used data in Views
        public ObservableCollection<IssuedCreditModel> UserApprovedCreditModels { get; }
        public ObservableCollection<IssuedCreditModel> AwaitToApprovedCreditModels { get; }
        public ObservableCollection<CreditTypeModel> CreditTypeModels { get; }
        
        // Shells for new models
        public IssuedCreditModel CreditToTaking { get; set;  }

        #region Commands

        // Fields.
        private ICommand _goBackCommand;
        private ICommand _takeNewCreditCommand;
        private ICommand _createNewCreditCommand;
        private ICommand _acceptCreditCommand;
        private ICommand _declineCreditCommand;
        
        // Props.
        
        public ICommand GoBackCommand
        {
            get
            {
                _goBackCommand ??= new RelayCommand(
                    _ => GoBack());
        
                return _goBackCommand;
            }
        }
        public ICommand TakeNewCreditCommand
        {
            get
            {
                _takeNewCreditCommand ??= new RelayCommand(
                    _ => TakeNewCredit());
        
                return _takeNewCreditCommand;
            }
        }
        
        public ICommand CreateNewCreditCommand
        {
            get
            {
                _createNewCreditCommand ??= new RelayCommand(
                    _ => CreateNewCredit());
        
                return _createNewCreditCommand;
            }
        }
        
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
        private void GoBack()
        {
            SelectedControlViewModel = ControlViewModels[0];
        }
        
        private void TakeNewCredit()
        {
            SelectedControlViewModel = ControlViewModels[1];
        }

        private void CreateNewCredit()
        {
            CreditToTaking.UserId = MainViewModel.Data.LoggedInUser.Id;
            CreditToTaking.DateIn = DateTime.Now;
            CreditToTaking.IsApproved = false;
            CreditToTaking.IsRepaid = false;

            ModelResourcesManager.GetInstance().AddModel(CreditToTaking);
            MessageBox.Show("Заявка оставлена");
            CreditToTaking = new IssuedCreditModel();
            
            SelectedControlViewModel = ControlViewModels[0];
        }
        
        private void AcceptCredit(IssuedCreditModel issuedCreditModel)
        {
            issuedCreditModel.IsApproved = true;
            
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