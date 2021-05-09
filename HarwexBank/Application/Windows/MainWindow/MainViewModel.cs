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

            ControlViewModels.AddRange(
                MainWindowFactory.GetFactoryByUserType(Data.LoggedInUser.UserTypeModelNavigation).GetPages());
            
            SelectedControlViewModel = ControlViewModels[0];
        }
        public ApplicationViewModel ApplicationViewModel { get; }
        public static MainWindowInfo Data { get; }
    }
    
    public class MainWindowInfo
    {
        public UserModel LoggedInUser { get; set; }
        public ObservableCollection<UserModel> ExistedClients { get; set; }
        public OperationModel AllPerformedOperations { get; set; }
    }
}