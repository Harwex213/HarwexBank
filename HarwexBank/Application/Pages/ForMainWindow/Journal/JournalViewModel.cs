using System.Collections.ObjectModel;

namespace HarwexBank
{
    public class JournalViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "Журнал";

        public JournalViewModel()
        {
            Journal = MainViewModel.Data.LoggedInUser.UserType switch
            {
                "worker" => MainViewModel.Data.GlobalJournal,
                "client" => MainViewModel.Data.UserJournal,
                _ => null
            };
        }
        
        public ObservableCollection<JournalModel> Journal { get; set; }
    }
}