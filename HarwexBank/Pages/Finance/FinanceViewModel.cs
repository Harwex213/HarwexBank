using System.Collections.Generic;

namespace HarwexBank
{
    public class FinanceViewModel : ObservableObject, IPageViewModel
    {
        public string Name => "Финансы";
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

        public FinanceViewModel()
        {
            ControlViewModels.Add(new AccountViewModel());
            ControlViewModels.Add(new CreditViewModel());

            SelectedControlViewModel = ControlViewModels[0];
        }
    }
}