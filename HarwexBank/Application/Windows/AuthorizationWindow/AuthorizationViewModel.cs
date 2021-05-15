using System.Windows.Input;

namespace HarwexBank
{
    public class AuthorizationViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "Harwex Bank Auth";

        public AuthorizationViewModel(ApplicationViewModel applicationViewModel)
        {
            ApplicationViewModel = applicationViewModel;
            SelectedControlViewModel = LoginViewModel;
        }

        public ApplicationViewModel ApplicationViewModel { get; }

        private LoginViewModel _loginViewModel;
        private RegistrationViewModel _registrationViewModel;

        private LoginViewModel LoginViewModel => _loginViewModel ?? new LoginViewModel(this);
        private RegistrationViewModel RegistrationViewModel => _registrationViewModel ?? new RegistrationViewModel(this);

        public void GoToLoginView()
        {
            SelectedControlViewModel = LoginViewModel;
        }
        
        public void GoToRegistrationView()
        {
            SelectedControlViewModel = RegistrationViewModel;
        }

        public void RegistrationFinished()
        {
            ApplicationViewModel.GoToAuthorizationView();
        }
    }
}