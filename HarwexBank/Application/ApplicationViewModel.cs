
namespace HarwexBank
{
    public class ApplicationViewModel : BaseControlViewModel
    {
        public void GoToMainView()
        {
            SelectedControlViewModel = new MainViewModel(this);
        }
        
        public void GoToAuthorizationView()
        {
            SelectedControlViewModel = new AuthorizationViewModel(this);
        }
    }
}