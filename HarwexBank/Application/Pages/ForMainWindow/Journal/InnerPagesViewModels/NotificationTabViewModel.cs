namespace HarwexBank
{
    public class NotificationTabViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "Уведомления";
        public NotificationTabViewModel(JournalViewModel journalView)
        {
            JournalView = journalView;
        }
        public JournalViewModel JournalView { get; set; }
    }
}