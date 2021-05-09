using System;
using System.Windows;
using System.Windows.Input;

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
                if (user.IsBlocked)
                {
                    throw new Exception("Пользователь заблокирован.");
                }
                
                // If login & password is typed correctly
                MainViewModel.Data.LoggedInUser = user;
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