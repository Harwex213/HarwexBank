using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace HarwexBank
{
    public class ApplicationViewModel : BaseControlViewModel
    {
        public ApplicationViewModel()
        {
            LoggedInUser = ModelResourcesManager.GetInstance().GetUserByLogin("oleg");
            
            ControlViewModels.Add(new CardsViewModel());
            ControlViewModels.Add(new FinanceViewModel());

            SelectedControlViewModel = ControlViewModels[1];
        }

        #region Data static Properties

        public static UserModel LoggedInUser { get; set; }
        public static ObservableCollection<UserModel> ExistedClients { get; set; }

        #endregion
    }
}