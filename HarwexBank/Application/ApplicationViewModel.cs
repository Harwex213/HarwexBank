using System.Collections.Generic;

namespace HarwexBank
{
    public class ApplicationViewModel : ObservableObject
    {
        private IPageViewModel _selectedPageViewModel;
        private List<IPageViewModel> _pageViewModels;
        private UserModel _user;

        public IPageViewModel SelectedPageViewModel
        {
            get => _selectedPageViewModel;
            set
            {
                if (_selectedPageViewModel == value)
                    return;
                _selectedPageViewModel = value;
                OnPropertyChanged("SelectedPageViewModel");
            }
        }

        public List<IPageViewModel> PageViewModels
        {
            get { return _pageViewModels ??= new List<IPageViewModel>(); }
        }

        public UserModel User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged("User");
            }
        }

        public ApplicationViewModel()
        {
            PageViewModels.Add(new CardsViewModel(user));
            PageViewModels.Add(new FinanceViewModel(user));

            SelectedPageViewModel = PageViewModels[1];
        }
    }
}