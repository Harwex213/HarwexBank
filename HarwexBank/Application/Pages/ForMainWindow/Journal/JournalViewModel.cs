using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace HarwexBank
{
    public class AllJournalTab : Control, IControlViewModel
    {
        public new string Name => "Всё";
    }
    
    public class OperationsTab : Control, IControlViewModel
    {
        public new string Name => "Переводы";
    }

    public class CreditRepaymentsTab : Control, IControlViewModel
    {
        public new string Name => "Погашение кредитов";
    }
    
    public class NotificationsTab : Control, IControlViewModel
    {
        public new string Name => "Уведомления";
    }

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

            RefreshPage();
            
            ControlViewModels.Add(new AllJournalTab());
            ControlViewModels.Add(new OperationsTab());
            ControlViewModels.Add(new CreditRepaymentsTab());
            ControlViewModels.Add(new NotificationsTab());
            
            SelectedControlViewModel = ControlViewModels[0];
        }
        
        public ObservableCollection<JournalModel> Journal { get; set; }
        public List<JournalModel> Transfers { get; set; }
        public List<JournalModel> CreditRepayments { get; set; }
        public List<JournalModel> Notifications { get; set; }
        
        #region Commands

        // Fields.
        private ICommand _refreshPageCommand;
        
        // Props.
        public ICommand RefreshPageCommand
        {
            get
            {
                _refreshPageCommand ??= new RelayCommand(
                    _ => RefreshPage());
        
                return _refreshPageCommand;
            }
        }
        
        // Methods.
        private void RefreshPage()
        {
            Transfers = Journal?.Where(j => j is TransferToAccountModel).ToList();
            CreditRepayments = Journal?.Where(j => j is CreditRepaymentModel).ToList();
            Notifications = Journal?.Where(j => j is NotificationModel).ToList();
        }

        #endregion
    }
}