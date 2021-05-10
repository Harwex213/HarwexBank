using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HarwexBank
{
    public class MainViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "Harwex Bank";

        static MainViewModel()
        {
            Data = new MainWindowInfo();
        }

        public MainViewModel(ApplicationViewModel applicationViewModel)
        {
            ApplicationViewModel = applicationViewModel;

            var windowFactory = MainWindowFactory.GetFactoryByUserType(Data.LoggedInUser.UserTypeModelNavigation);
            windowFactory.GetNecessaryInfo(Data);
            ControlViewModels.AddRange(windowFactory.GetPages());

            SelectedControlViewModel = ControlViewModels[0];
        }
        public ApplicationViewModel ApplicationViewModel { get; }
        public static MainWindowInfo Data { get; }
    }
    
    public class MainWindowInfo
    {
        public UserModel LoggedInUser { get; set; }
        
        public ObservableCollection<UserModel> ExistedClients { get; set; }
        
        public ObservableCollection<CurrencyTypeModel> ExistedCurrencyTypes { get; set; }
        
        public ObservableCollection<CardTypeModel> ExistedCardTypes { get; set; }
        
        public ObservableCollection<CreditTypeModel> ExistedCreditTypes { get; set; }
        
        public ObservableCollection<OperationModel> AllPerformedOperations { get; set; }
    }
}