using System.Windows.Input;

namespace HarwexBank
{
    public class AuthorizationViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "Harwex Bank Auth";

        public AuthorizationViewModel(ApplicationViewModel applicationViewModel)
        {
            _applicationViewModel = applicationViewModel;
        }

        private readonly ApplicationViewModel _applicationViewModel;
        private ICommand _enterAppCommand;
        
        public ICommand EnterAppCommand
        {
            get
            {
                _enterAppCommand ??= new RelayCommand(
                    c => _applicationViewModel.EnterToApplication((string)c),
                    c => c is string);

                return _enterAppCommand;
            }
        }
    }
}