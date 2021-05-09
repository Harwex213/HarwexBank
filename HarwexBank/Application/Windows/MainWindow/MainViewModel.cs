using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HarwexBank
{
    public class MainViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "Harwex Bank";

        public MainViewModel(ApplicationViewModel applicationViewModel)
        {
            ApplicationViewModel = applicationViewModel;

            ControlViewModels.AddRange(
                PagesFactory.GetFactoryByUserType(LoggedInUser.UserTypeModelNavigation).GetPages());
            
            SelectedControlViewModel = ControlViewModels[0];
        }
        public ApplicationViewModel ApplicationViewModel { get; }
        
        #region Data global properties for pages

        public static UserModel LoggedInUser { get; set; }
        public static ObservableCollection<UserModel> ExistedClients { get; set; }

        #endregion
    }
}