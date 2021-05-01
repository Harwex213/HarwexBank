using System.Windows.Controls;

namespace HarwexBank
{
    public partial class CreditsPageView : UserControl
    {
        public CreditsPageView()
        {
            InitializeComponent();

            DataContext = new CreditsPageViewModel();
        }
    }
}