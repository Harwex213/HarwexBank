namespace HarwexBank
{
    public class ExistedClientsViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "";

        public ExistedClientsViewModel(ClientsViewModel clientsViewModel)
        {
            ClientsViewModel = clientsViewModel;
        }

        public ClientsViewModel ClientsViewModel { get; }
    }
}