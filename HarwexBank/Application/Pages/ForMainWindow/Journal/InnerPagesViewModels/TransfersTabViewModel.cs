namespace HarwexBank
{
    public class TransfersTabViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "Переводы";
        public TransfersTabViewModel(JournalViewModel journalView)
        {
            JournalView = journalView;
        }
        public JournalViewModel JournalView { get; }
    }
}