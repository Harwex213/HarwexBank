using System.Windows.Controls;

namespace HarwexBank
{
    public partial class Operations : UserControl
    {
        public Operations()
        {
            InitializeComponent();

            DataContext = new OperationsViewModel();
        }
    }
}