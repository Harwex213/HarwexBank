using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace HarwexBank
{
    public class ApplicationViewModel : ObservableObject
    {
        public ApplicationViewModel()
        {
            LoggedInUser = ModelResourcesManager.GetInstance().GetUserByLogin("oleg");
            
            PageViewModels.Add(new CardsViewModel());
            PageViewModels.Add(new FinanceViewModel());

            SelectedPageViewModel = PageViewModels[1];
        }

        #region Data static Properties

        public static UserModel LoggedInUser { get; set; }
        public static ObservableCollection<UserModel> ExistedClients { get; set; }

        #endregion
        
        #region Fields
        
        private ICommand _changePageCommand;
        private IPageViewModel _selectedPageViewModel;
        private List<IPageViewModel> _pageViewModels;

        #endregion

        #region Properties / Commands

        public ICommand ChangePageCommand
        {
            get
            {
                _changePageCommand ??= new RelayCommand(
                    p => ChangeViewModel((IPageViewModel)p),
                    p => p is IPageViewModel);

                return _changePageCommand;
            }
        }

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

        #endregion

        #region Methods

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            SelectedPageViewModel = PageViewModels.FirstOrDefault(vm => vm == viewModel);
        }

        #endregion
    }
}