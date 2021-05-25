using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HarwexBank
{
    public class MainViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "Harwex Bank";

        public MainViewModel(ApplicationViewModel applicationViewModel)
        {
            ApplicationViewModel = applicationViewModel;
            applicationViewModel.MinHeight = 500;
            applicationViewModel.MinWidth = 700;

            ControlViewModels.AddRange(WindowFactory.GetPages());
            SelectedControlViewModel = ControlViewModels[0];
        }

        public ApplicationViewModel ApplicationViewModel { get; }
        public static MainWindowFactory WindowFactory { get; } = MainWindowFactory.GetFactory();

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