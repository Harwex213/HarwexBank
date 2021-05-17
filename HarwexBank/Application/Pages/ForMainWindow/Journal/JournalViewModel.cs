using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace HarwexBank
{
    public class JournalClientViewModel : JournalViewModel {}
    public class JournalWorkerViewModel : JournalViewModel {}

    public class JournalViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "Журнал";

        public JournalViewModel()
        {
            switch (MainViewModel.WindowFactory)
            {
                case AdminMainWindow:
                    break;
                case ClientMainWindow:
                    Journal = ModelResourcesManager.GetInstance().UserJournal;
                    
                    ControlViewModels.Add(new AllJournalNotesTabViewModel(this));
                    ControlViewModels.Add(new TransfersTabViewModel(this));
                    ControlViewModels.Add(new CreditRepaymentTabViewModel(this));
                    ControlViewModels.Add(new NotificationTabViewModel(this));
                    break;
                case WorkerMainWindow:
                    Journal = ModelResourcesManager.GetInstance().AllJournal;
                    
                    ControlViewModels.Add(new AllJournalNotesTabViewModel(this));
                    ControlViewModels.Add(new TransfersTabViewModel(this));
                    ControlViewModels.Add(new CreditRepaymentTabViewModel(this));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Transfers = new ObservableCollection<TransferToAccountModel>();
            CreditRepayments = new ObservableCollection<CreditRepaymentModel>();
            Notifications = new ObservableCollection<NotificationModel>();

            foreach (var journalNote in Journal)
            {
                DetermineJournalModel(journalNote);
            }

            Journal.CollectionChanged += (_, e) =>
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