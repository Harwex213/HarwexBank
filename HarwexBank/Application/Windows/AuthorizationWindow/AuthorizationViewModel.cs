using System.Windows.Input;

namespace HarwexBank
{
    public class AuthorizationViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "Harwex Bank Auth";

        public AuthorizationViewModel(ApplicationViewModel applicationViewModel)
        {
            ApplicationViewModel = applicationViewModel;
            SelectedControlViewModel = new LoginViewModel(this);
        }

        public ApplicationViewModel ApplicationViewModel { get; }

        public void GoToLoginView()
        {
            ApplicationViewModel.GoToAuthorizationView();
        }
        
        public void GoToRegistrationView()
        {
            SelectedControlViewModel = new RegistrationViewModel(this);
        }

        public void RegistrationFinished()
        {
            GoToLoginView();
        }
    }
}