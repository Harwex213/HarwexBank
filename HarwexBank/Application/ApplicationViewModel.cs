using System.Collections.Generic;

namespace HarwexBank
{
    public class ApplicationViewModel : ObservableObject
    {
        private IPageViewModel _selectedPageViewModel;
        private List<IPageViewModel> _pageViewModels;

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

        public ApplicationViewModel()
        {
            PageViewModels.Add(new CardsViewModel());
            PageViewModels.Add(new FinanceViewModel());

            SelectedPageViewModel = PageViewModels[1];
        }
    }
}