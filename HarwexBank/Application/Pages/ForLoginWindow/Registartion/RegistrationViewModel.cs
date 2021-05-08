namespace HarwexBank
{
    public class RegistrationViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "Регистрация";
        public double MinHeight { get; set; }

        public RegistrationViewModel(AuthorizationViewModel authorizationViewModel)
        {
            AuthorizationViewModel = authorizationViewModel;
            MinHeight = 690;
            
        }
        public AuthorizationViewModel AuthorizationViewModel { get; }
    }
}