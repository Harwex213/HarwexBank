
namespace HarwexBank
{
    public class ApplicationViewModel : BaseControlViewModel
    {
        private double _minHeight;
        private double _minWidth;

        public ApplicationViewModel()
        {
            GoToAuthorizationView();
        }
        
        public void GoToMainView()
        {
            SelectedControlViewModel = new MainViewModel(this);
        }
        
        public void GoToAuthorizationView()
        {
            SelectedControlViewModel = new AuthorizationViewModel(this);
        }

        public double MinHeight
        {
            get => _minHeight;
            set => Set(ref _minHeight, value);
        }

        public double MinWidth
        {
            get => _minWidth;
            set => Set(ref _minWidth, value);
        }
    }
}