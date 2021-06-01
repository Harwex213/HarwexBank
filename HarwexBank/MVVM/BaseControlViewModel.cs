using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace HarwexBank
{
    public abstract class BaseControlViewModel : ObservableObject
    {
        private ICommand _changeControlCommand;
        private IControlViewModel _selectedControlViewModel;
        private List<IControlViewModel> _controlViewModels;

        public ICommand ChangeControlCommand
        {
            get
            {
                _changeControlCommand ??= new RelayCommand(
                    c => ChangeViewModel((IControlViewModel)c),
                    c => c is IControlViewModel);

                return _changeControlCommand;
            }
        }
        
        public IControlViewModel SelectedControlViewModel
        {
            get => _selectedControlViewModel;
            set
            {
                if (_selectedControlViewModel == value)
                    return;
                _selectedControlViewModel = value;
                OnPropertyChanged("SelectedControlViewModel");
            }
        }

        public List<IControlViewModel> ControlViewModels
        {
            get { return _controlViewModels ??= new List<IControlViewModel>(); }
        }

        private void ChangeViewModel(IControlViewModel viewModel)
        {
            if (!ControlViewModels.Contains(viewModel))
                ControlViewModels.Add(viewModel);

            SelectedControlViewModel = ControlViewModels.FirstOrDefault(vm => vm == viewModel);
        }

        public BaseControlViewModel()
        {
            App.LanguageChanged += () => OnPropertyChanged("Name");
        }
    }
}