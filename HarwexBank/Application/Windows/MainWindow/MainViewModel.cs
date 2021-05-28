using System.Globalization;
using System.Windows.Input;

namespace HarwexBank
{
    public class MainViewModel : BaseControlViewModel, IControlViewModel, ICurrencyRatesObserver
    {
        public string Name => "Harwex Bank";

        public MainViewModel(ApplicationViewModel applicationViewModel)
        {
            ApplicationViewModel = applicationViewModel;
            applicationViewModel.MinHeight = 500;
            applicationViewModel.MinWidth = 700;
            
            WindowFactory = MainWindowFactory.GetFactory();
            ControlViewModels.AddRange(WindowFactory.GetPages());
            SelectedControlViewModel = ControlViewModels[0];
            
            ModelResourcesManager.GetInstance().CurrencyConverter.Attach(this);
        }

        public ApplicationViewModel ApplicationViewModel { get; }
        public static MainWindowFactory WindowFactory { get; private set; }

        private string _usdToBynRate;
        private string _rubToBynRate;
        
        public string UsdToBynRate
        {
            get => _usdToBynRate;
            set => Set(ref _usdToBynRate, value);
        }

        public string RubToBynRate
        {
            get => _rubToBynRate;
            set => Set(ref _rubToBynRate, value);
        }
        
        public void NewRates(ICurrencyRatesSubject subject)
        {
            if (subject is not CurrencyConverter converter) 
                return;
            
            UsdToBynRate = converter.UsdToBynRate.ToString(CultureInfo.CurrentCulture);
            RubToBynRate = converter.RubToBynRate.ToString(CultureInfo.CurrentCulture);
        }

        #region Commands

        // Fields.
        private ICommand _loggOutCommand;

        // Props.
        public ICommand LoggOutCommand
        {
            get
            {
                _loggOutCommand ??= new RelayCommand(
                    _ => LoggOut());

                return _loggOutCommand;
            }
        }

        // Methods.
        private void LoggOut()
        {
            ApplicationViewModel.GoToAuthorizationView();
        }
        

        #endregion
    }
}