using System.Windows.Controls;

namespace HarwexBank
{
    public partial class ClientsView : UserControl
    {
        public ClientsView()
        {
            InitializeComponent();

            DataContext = new ClientsViewModel();
        }
    }
}