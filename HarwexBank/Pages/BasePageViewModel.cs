using System.Collections.Generic;

namespace HarwexBank
{
    public abstract class BasePageViewModel : ObservableObject
    {
        private IControlViewModel _selectedControlViewModel;
        private List<IControlViewModel> _controlViewModels;

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
    }
}