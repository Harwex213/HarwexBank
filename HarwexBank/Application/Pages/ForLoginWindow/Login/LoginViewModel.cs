namespace HarwexBank
{
    public class LoginViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "Вход";

        public LoginViewModel(AuthorizationViewModel authorizationViewModel)
        {
            AuthorizationViewModel = authorizationViewModel;
            
            
        }
        public AuthorizationViewModel AuthorizationViewModel { get; }

        public string Login { get; set; }

        public string Password { get; set; }

        private void CheckLoginAndPassword()
        {
            var user = ModelResourcesManager.GetInstance().GetUserByLogin(Login);
            // TODO: Validate received login & password
            
            // If login & password is true
            MainViewModel.LoggedInUser = user;
            
        }
    }
}