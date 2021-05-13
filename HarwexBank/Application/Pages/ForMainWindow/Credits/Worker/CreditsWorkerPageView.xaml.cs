using System.Windows.Controls;

namespace HarwexBank
{
    public partial class CreditsWorkerPageView : UserControl
    {
        public CreditsWorkerPageView()
        {
            InitializeComponent();

            DataContext = new CreditsWorkerPageViewModel();
        }
    }
}