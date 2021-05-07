using System.Collections.ObjectModel;

namespace HarwexBank
{
    public class MainViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "Harwex Bank";

        public MainViewModel()
        {
            ControlViewModels.Add(new CardsViewModel());
            ControlViewModels.Add(new FinanceViewModel());
            
            SelectedControlViewModel = ControlViewModels[1];
        }
        
        #region Data global Properties

        public static UserModel LoggedInUser { get; set; }
        public static ObservableCollection<UserModel> ExistedClients { get; set; }

        #endregion
    }
}