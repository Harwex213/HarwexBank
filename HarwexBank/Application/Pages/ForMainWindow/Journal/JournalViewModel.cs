using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace HarwexBank
{
    public class AllJournalNotesTabViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "Всё";
        public JournalViewModel JournalView { get; set; }
    }
    
    public class TransfersTabViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "Переводы";
        public JournalViewModel JournalView { get; set; }
    }
    
    public class CreditRepaymentTabViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "Погашения кредитов";
        public JournalViewModel JournalView { get; set; }
    }
    
    public class NotificationTabViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "Уведомления";
        public JournalViewModel JournalView { get; set; }
    }

    public class JournalClientViewModel : JournalViewModel {}
    public class JournalWorkerViewModel : JournalViewModel {}

    public class JournalViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "Журнал";

        public JournalViewModel()
        {
            switch (MainViewModel.Data.LoggedInUser.UserType)
            {
                case "worker":
                    Journal = MainViewModel.Data.GlobalJournal;
                    
                    ControlViewModels.Add(new AllJournalNotesTabViewModel{ JournalView = this });
                    ControlViewModels.Add(new TransfersTabViewModel{ JournalView = this });
                    ControlViewModels.Add(new CreditRepaymentTabViewModel{ JournalView = this });
                    break;
                
                case "client":
                    Journal = MainViewModel.Data.UserJournal;

                    ControlViewModels.Add(new AllJournalNotesTabViewModel{ JournalView = this });
                    ControlViewModels.Add(new TransfersTabViewModel{ JournalView = this });
                    ControlViewModels.Add(new CreditRepaymentTabViewModel{ JournalView = this });
                    ControlViewModels.Add(new NotificationTabViewModel{ JournalView = this });
                    break;
            }

            Transfers = new ObservableCollection<TransferToAccountModel>();
            CreditRepayments = new ObservableCollection<CreditRepaymentModel>();
            Notifications = new ObservableCollection<NotificationModel>();

            foreach (var journalNote in Journal)
            {
                DetermineJournalModel(journalNote);
            }

            Journal.CollectionChanged += (sender, e) =>
            {
                if (e.NewItems == null || e.Action != NotifyCollectionChangedAction.Add) 
                    return;

                DetermineJournalModel((JournalModel) e.NewItems[0]);
            };

            SelectedControlViewModel = ControlViewModels[0];
        }

        public ObservableCollection<JournalModel> Journal { get; set; }
        public ObservableCollection<TransferToAccountModel> Transfers { get; set; }
        public ObservableCollection<CreditRepaymentModel> CreditRepayments { get; set; }
        public ObservableCollection<NotificationModel> Notifications { get; set; }

        private void DetermineJournalModel(JournalModel journalNote)
        {
            switch (journalNote)
            {
                case TransferToAccountModel transferToAccountModel: 
                    Transfers.Add(transferToAccountModel);
                    break;
                
                case CreditRepaymentModel creditRepaymentModel:
                    CreditRepayments.Add(creditRepaymentModel);
                    break;
                
                case NotificationModel notificationModel:
                    Notifications.Add(notificationModel);
                    break;
            }
        }
    }
}