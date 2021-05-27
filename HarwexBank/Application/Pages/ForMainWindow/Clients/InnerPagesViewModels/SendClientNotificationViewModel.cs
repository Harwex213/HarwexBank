namespace HarwexBank
{
    public class SendClientNotificationViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "Уведомление клиенту с Id " + ClientsViewModel.SelectedClient.Id;
        public SendClientNotificationViewModel(ClientsViewModel clientsViewModel)
        {
            ClientsViewModel = clientsViewModel;
        }
        
        public ClientsViewModel ClientsViewModel { get; }
    }
}