using System.Windows.Input;

namespace HarwexBank
{
    public class OpenAccountViewModel : ObservableObject, IControlViewModel
    {
        public string Name => "Счета";

        public OpenAccountViewModel()
        {
            AccountToOpen = new AccountModel();
        }

        public AccountModel AccountToOpen { get; set; }

        #region Commands

        // Fields.
        private ICommand _createAccountCommand;
        
        // Props.
        
        
        // Methods.
        private void CreateAccount()
        {
            // TODO: construct account model
            
            ModelResourcesManager.GetInstance().AddModel(AccountToOpen);
        }

        #endregion
    }
}