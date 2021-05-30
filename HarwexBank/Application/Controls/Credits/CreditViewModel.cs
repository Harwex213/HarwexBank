﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HarwexBank
{
    public class CreditViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "Кредиты";
        public CreditViewModel()
        {
            switch (MainViewModel.WindowFactory)
            {
                case ClientMainWindow:
                    // Using data.
                    UserApprovedCreditModels = ModelResourcesManager.GetInstance().UserCredits;
                    UserAccounts = ModelResourcesManager.GetInstance().UserAccounts;
                    CreditTypeModels = ModelResourcesManager.GetInstance().ExistedCreditTypes;

                    ResetCreditToTaking();
                    
                    // Using VM
                    ControlViewModels.Add(new CreditListViewModel());
                    ControlViewModels.Add(new TakeNewCreditViewModel());
                    break;
                
                case WorkerMainWindow:
                    
                    // Using VM
                    ControlViewModels.Add(new CreditListViewModel());
                    break;
                
                case AdminMainWindow:
                    // Using VM
                    ControlViewModels.Add(new CreditListViewModel());
                    
                    CreditTypeModels = ModelResourcesManager.GetInstance().ExistedCreditTypes;
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            SelectedControlViewModel = ControlViewModels.FirstOrDefault();
        }
        
        // Used data in Views
        public ObservableCollection<IssuedCreditModel> UserApprovedCreditModels { get; set; }
        public ObservableCollection<CreditTypeModel> CreditTypeModels { get; }
        
        // Shells for new models
        public IssuedCreditModel CreditToTaking { get; set;  }
        public ObservableCollection<AccountModel> UserAccounts { get; set; }
        public AccountModel AccountIdToPay { get; set; }

        private void ResetCreditToTaking()
        {
            CreditToTaking = new IssuedCreditModel{ CreditTypeModelNavigation = CreditTypeModels[0] };
            CreditToTaking.PropertyChanged += (sender, args) =>
            {
                switch (args.PropertyName)
                {
                    case nameof(IssuedCreditModel.Amount):
                    case nameof(IssuedCreditModel.Term):
                    {
                        var creditCalculator = new CreditCalculator.CreditCalculator(CreditToTaking.Amount,
                            CreditToTaking.CreditTypeModelNavigation.Rate, CreditToTaking.Term);
                        if (sender is not null)
                        {
                            ((IssuedCreditModel) sender).AmountToPay = creditCalculator.GetAnnuityPlan() *
                                                                       ((IssuedCreditModel) sender).Term;
                            if (((IssuedCreditModel) sender).AmountToPay != 0)
                            {
                                ((IssuedCreditModel) sender).OverPaymentAmount = ((IssuedCreditModel) sender).AmountToPay - 
                                                                                 ((IssuedCreditModel) sender).Amount;
                                ((IssuedCreditModel) sender).AmountRemained = ((IssuedCreditModel) sender).AmountToPay -
                                                                              ((IssuedCreditModel) sender).RepaidAmount;
                            }
                            else
                            {
                                ((IssuedCreditModel) sender).OverPaymentAmount = 0;
                            }
                        }
                        break;
                    }
                }
            };
        }

        #region Commands

        // Fields.
        private ICommand _goBackCommand;
        private ICommand _takeNewCreditCommand;
        private ICommand _createNewCreditCommand;
        
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
            CreditToTaking.UserId = ModelResourcesManager.GetInstance().LoggedInUser.Id;
            CreditToTaking.DateIn = DateTime.Now;
            CreditToTaking.IsApproved = false;
            CreditToTaking.IsRepaid = false;
            CreditToTaking.AccountId = AccountIdToPay.Id;

            ModelResourcesManager.GetInstance().AddModel(CreditToTaking);
            MessageBox.Show("Заявка оставлена");
            ResetCreditToTaking();
            
            SelectedControlViewModel = ControlViewModels[0];
        }

        #endregion
        
    }
}