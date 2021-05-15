using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace HarwexBank
{
    public class RegistrationPage01 : Control, IControlViewModel { }
    public class RegistrationPage02 : Control, IControlViewModel { }
    public class RegistrationViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "Регистрация";

        public RegistrationViewModel(AuthorizationViewModel authorizationViewModel)
        {
            AuthorizationViewModel = authorizationViewModel;
            
            ControlViewModels.Add(new RegistrationPage01());
            ControlViewModels.Add(new RegistrationPage02());

            SelectedControlViewModel = ControlViewModels[_iterator];

            RegisteredUser = new UserModel();
        }
        
        // Fields.
        private int _iterator;
        
        // Props.
        public AuthorizationViewModel AuthorizationViewModel { get; }

        public UserModel RegisteredUser { get; set; }

        #region Commands

        // Fields.
        private ICommand _backToLoginCommand;
        private ICommand _finishRegistrationCommand;
        private ICommand _continueRegistrationCommand;
        private ICommand _returnBackCommand;

        // Props.
        public ICommand BackToLoginCommand
        {
            get
            {
                _backToLoginCommand ??= new RelayCommand(
                    _ => BackToLogin());

                return _backToLoginCommand;
            }
        }
        
        public ICommand FinishRegistrationCommand
        {
            get
            {
                _finishRegistrationCommand ??= new RelayCommand(
                    _ => FinishRegistration());

                return _finishRegistrationCommand;
            }
        }
        
        public ICommand ContinueRegistrationCommand
        {
            get
            {
                _continueRegistrationCommand ??= new RelayCommand(
                    _ => ContinueRegistration());

                return _continueRegistrationCommand;
            }
        }

        public ICommand ReturnBackCommand
        {
            get
            {
                _returnBackCommand ??= new RelayCommand(
                    _ => ReturnBack());

                return _returnBackCommand;
            }
        }
        
        // Methods.
        private void BackToLogin()
        {
            AuthorizationViewModel.GoToLoginView();
        }
        
        private void FinishRegistration()
        {
            ModelResourcesManager.GetInstance().AddModel(RegisteredUser);
            
            AuthorizationViewModel.RegistrationFinished();
        }
        
        private void ContinueRegistration()
        {
            SelectedControlViewModel = ControlViewModels[++_iterator];
        }
        
        private void ReturnBack()
        {
            SelectedControlViewModel = ControlViewModels[--_iterator];
        }

        #endregion
    }
}