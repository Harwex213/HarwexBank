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
        
        // Fields.
        private ICommand _registrationStartCommand;
        private ICommand _backToLoginCommand;
        private ICommand _registrationFinishCommand;
        
        // Props.
        public ICommand RegistrationStartCommand
        {
            get
            {
                _registrationStartCommand ??= new RelayCommand(
                    _ => RegistrationStart());

                return _registrationStartCommand;
            }
        }
        
        public ICommand BackToLoginCommand
        {
            get
            {
                _backToLoginCommand ??= new RelayCommand(
                    _ => BackToLogin());

                return _backToLoginCommand;
            }
        }

        public ICommand RegistrationFinishCommand
        {
            get
            {
                _registrationFinishCommand ??= new RelayCommand(
                    c => RegistrationFinish((UserModel)c),
                    c => c is UserModel);

                return _registrationFinishCommand;
            }
        }
        
        // Methods.

        private void RegistrationStart()
        {
            SelectedControlViewModel = _registrationViewModel;
        }
        
        private void BackToLogin()
        {
            SelectedControlViewModel = _loginViewModel;
        }
        
        private static void RegistrationFinish(UserModel userModel)
        {
            ModelResourcesManager.GetInstance().AddModel( userModel);
        }

        #endregion
    }
}