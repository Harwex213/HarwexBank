namespace HarwexBank
{
    public class RegistrationViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "Регистрация";

        public RegistrationViewModel(AuthorizationViewModel authorizationViewModel)
        {
            AuthorizationViewModel = authorizationViewModel;
            
            
        }
        public AuthorizationViewModel AuthorizationViewModel { get; }
    }
}