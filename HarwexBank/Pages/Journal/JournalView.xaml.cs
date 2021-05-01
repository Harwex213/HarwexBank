using System.Windows.Controls;

namespace HarwexBank
{
    public partial class JournalView : UserControl
    {
        public JournalView()
        {
            InitializeComponent();

            DataContext = new JournalViewModel();
        }
    }
}