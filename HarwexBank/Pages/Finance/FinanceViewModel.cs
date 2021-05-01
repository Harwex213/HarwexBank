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

        public FinanceViewModel(UserModel userModel)
        {
            ControlViewModels.Add(new AccountViewModel(userModel));
            ControlViewModels.Add(new CreditViewModel(userModel));

            SelectedControlViewModel = ControlViewModels[1];
        }
    }
}