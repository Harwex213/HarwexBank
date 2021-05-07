using System.Windows.Input;

namespace HarwexBank
{
    public class AuthorizationViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "Harwex Bank Auth";

        public AuthorizationViewModel(ApplicationViewModel applicationViewModel)
        {
            ApplicationViewModel = applicationViewModel;

            _loginViewModel = new LoginViewModel(this);
            _registrationViewModel = new RegistrationViewModel(this);

            SelectedControlViewModel = _loginViewModel;
        }

        public ApplicationViewModel ApplicationViewModel { get; }
        
        private readonly LoginViewModel _loginViewModel;
        private readonly RegistrationViewModel _registrationViewModel;

        #region Commands
        
        private ICommand _registrationStartCommand;
        private ICommand _backToLoginCommand;
        private ICommand _registrationFinishCommand;
        
        public ICommand RegistrationCommand
        {
            get
            {
                _registrationStartCommand ??= new RelayCommand(
                    c => SelectedControlViewModel = _registrationViewModel);

                return _registrationStartCommand;
            }
        }
        
        public ICommand BackToLoginCommand
        {
            get
            {
                _backToLoginCommand ??= new RelayCommand(
                    c => SelectedControlViewModel = _loginViewModel);

                return _backToLoginCommand;
            }
        }

        public ICommand RegistrationFinishCommand
        {
            get
            {
                _registrationFinishCommand ??= new RelayCommand(
                    c => RegistrationFinished((UserModel) c),
                    c => c is UserModel);

                return _registrationFinishCommand;
            }
        }

        #endregion

        #region Methods

        private void RegistrationFinished(UserModel userModel)
        {
            ModelResourcesManager.GetInstance().AddModel(userModel);
        }

        public void EnterToApplication()
        {
            ApplicationViewModel.EnterToApplication();
        }

        #endregion
    }
}