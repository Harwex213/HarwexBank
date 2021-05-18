using System.Collections.ObjectModel;
using System.Linq;

namespace HarwexBank
{
    public class ClientInfoViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "";

        public ClientInfoViewModel(ClientsViewModel clientsViewModel)
        {
            ClientsViewModel = clientsViewModel;
            
            ControlViewModels.Add(new AccountViewModel
            {
                AccountModels = new ObservableCollection<AccountModel>(ClientsViewModel.SelectedClient.Accounts) 
            });
            ControlViewModels.Add(new CreditViewModel
            { 
                UserApprovedCreditModels = new ObservableCollection<IssuedCreditModel>(
                    ClientsViewModel.SelectedClient.IssuedCredits
                        .Where(c => c.IsApproved).Where(c => !c.IsRepaid))
            });
            
            SelectedControlViewModel = ControlViewModels[0];
        }
        
        public ClientsViewModel ClientsViewModel { get; }
    }
}