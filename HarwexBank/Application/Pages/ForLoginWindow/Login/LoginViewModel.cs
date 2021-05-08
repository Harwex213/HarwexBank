using System;
using System.Windows;
using System.Windows.Input;

namespace HarwexBank
{
    public class LoginViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "Вход";
        public double MinHeight { get; set; }

        public LoginViewModel(AuthorizationViewModel authorizationViewModel)
        {
            AuthorizationViewModel = authorizationViewModel;
            MinHeight = 455;
        }
        public AuthorizationViewModel AuthorizationViewModel { get; }
        public string Login { get; set; }
        public string Password { get; set; }

        #region Commands

        // Fields.
        private ICommand _logInCommand;
        
        // Props.
        public ICommand LogInCommand
        {
            get
            {
                _logInCommand ??= new RelayCommand(
                    _ => LogIn());

                return _logInCommand;
            }
        }

        // Methods.
        private void LogIn()
        {
            try
            {
                var user = ModelResourcesManager.GetInstance().GetUserByLogin(Login) ??
                           throw new Exception("Пользователь с данным логином не найден.");
                if (user.Password != Password)
                {
                    throw new Exception("Неверно введён пароль.");
                }
                
                // If login & password is typed correctly
                MainViewModel.LoggedInUser = user;
                AuthorizationViewModel.ApplicationViewModel.EnterToApplicationCommand.Execute(new object());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        #endregion
    }
}