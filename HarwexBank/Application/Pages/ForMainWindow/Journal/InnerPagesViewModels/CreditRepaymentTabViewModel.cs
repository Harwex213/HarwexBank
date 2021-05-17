namespace HarwexBank
{
    public class CreditRepaymentTabViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "Погашения кредитов";
        public CreditRepaymentTabViewModel(JournalViewModel journalView)
        {
            JournalView = journalView;
        }
        public JournalViewModel JournalView { get; }
    }
}