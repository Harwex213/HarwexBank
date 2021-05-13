﻿using System;
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
                    // Using data.
                    UserApprovedCreditModels = MainViewModel.Data.UserCredits;
                    CreditTypeModels = MainViewModel.Data.ExistedCreditTypes;
                    
                    // Using VM
                    ControlViewModels.Add(new CreditListViewModel());
                    ControlViewModels.Add(new TakeNewCreditViewModel());
                    break;
                
                case "worker":
                    
                    // Using VM
                    ControlViewModels.Add(new CreditListViewModel());
                    ControlViewModels.Add(new TakeNewCreditViewModel());
                    break;
                
                case "admin":
                    CreditTypeModels = MainViewModel.Data.ExistedCreditTypes;
                    
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
            CreditToTaking.UserId = MainViewModel.Data.LoggedInUser.Id;
            CreditToTaking.DateIn = DateTime.Now;
            CreditToTaking.IsApproved = false;
            CreditToTaking.IsRepaid = false;

            ModelResourcesManager.GetInstance().AddModel(CreditToTaking);
            MessageBox.Show("Заявка оставлена");
            CreditToTaking = new IssuedCreditModel();
            
            SelectedControlViewModel = ControlViewModels[0];
        }

        #endregion
        
    }
}