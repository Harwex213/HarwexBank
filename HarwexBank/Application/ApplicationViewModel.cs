using System.Collections.Generic;
using System.Collections.ObjectModel;

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
        
        public static UserModel LoggedInUser { get; set; }
        public static ObservableCollection<UserModel> ExistedClients { get; set; }
            
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
    }
}