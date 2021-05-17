namespace HarwexBank
{
    public class AllJournalNotesTabViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "Всё";
        public AllJournalNotesTabViewModel(JournalViewModel journalView)
        {
            JournalView = journalView;
        }
        public JournalViewModel JournalView { get; }
    }
}