namespace HarwexBank
{
    public class SendClientNotificationViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "";
        public SendClientNotificationViewModel(ClientsViewModel clientsViewModel)
        {
            ClientsViewModel = clientsViewModel;
        }
        public ClientsViewModel ClientsViewModel { get; }
    }
}