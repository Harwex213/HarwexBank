using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HarwexBank
{
    public class ClientsViewModel : BaseControlViewModel, IControlViewModel, IDataErrorInfo
    {
        public string Name => "Клиенты";

        public ClientsViewModel()
        {
            ExistedClients = MainViewModel.Data.ExistedClients;

            ControlViewModels.Add(new ExistedClientsViewModel(this));
            ControlViewModels.Add(new SendClientNotificationViewModel(this));

            SelectedControlViewModel = ControlViewModels[0];
        }

        // Data using in Clients List.
        public ObservableCollection<UserModel> ExistedClients { get; set; }

        // Data using in Client Info.
        public UserModel SelectedClient { get; set; }

        // Data using in Notification Generation.
        public string NotificationMessage { get; set; }

        #region Commands

        // Fields.
        private ICommand _selectClientCommand;
        private ICommand _goToClientInfoCommand;
        private ICommand _goToClientsListCommand;
        private ICommand _goToSendClientNotificationCommand;

        private ICommand _blockClientCommand;

        private ICommand _generateNotificationToClientCommand;

        // Props.

        public ICommand SelectClientCommand
        {
            get
            {
                _selectClientCommand ??= new RelayCommand(
                    u => SelectClient((UserModel) u),
                    u => u is UserModel);

                return _selectClientCommand;
            }
        }
        
        public ICommand GoToClientsListCommand
        {
            get
            {
                _goToClientsListCommand ??= new RelayCommand(
                    _ => GoToClientsPage());

                return _goToClientsListCommand;
            }
        }

        public ICommand GoToClientInfoCommand
        {
            get
            {
                _goToClientInfoCommand ??= new RelayCommand(
                    _ => GoToClientInfo());

                return _goToClientInfoCommand;
            }
        }

        public ICommand GoToSendClientNotificationCommand
        {
            get
            {
                _goToSendClientNotificationCommand ??= new RelayCommand(
                    _ => GoToSendClientNotification());

                return _goToSendClientNotificationCommand;
            }
        }

        public ICommand BlockClientCommand
        {
            get
            {
                _blockClientCommand ??= new RelayCommand(
                    _ => BlockClient());

                return _blockClientCommand;
            }
        }

        public ICommand GenerateNotificationToClientCommand
        {
            get
            {
                _generateNotificationToClientCommand ??= new RelayCommand(
                    _ => GenerateNotificationToClient());

                return _generateNotificationToClientCommand;
            }
        }

        // Methods.
        private void GoToClientsPage()
        {
            SelectedControlViewModel = ControlViewModels[0];
        }

        private void SelectClient(UserModel client)
        {
            SelectedClient = client;
            SelectedControlViewModel = new ClientInfoViewModel(this);
        }

        private void GoToClientInfo()
        {
            SelectedControlViewModel = new ClientInfoViewModel(this);
        }

        private void GoToSendClientNotification()
        {
            SelectedControlViewModel = ControlViewModels[1];
        }

        private void BlockClient()
        {
            SelectedClient.IsBlocked = !SelectedClient.IsBlocked;
            SelectedClient.UserBlockedTextMessage = "";
            SelectedClient.UserToBlockButtonText = "";
            ModelResourcesManager.GetInstance().UpdateModel(SelectedClient);
        }

        private void GenerateNotificationToClient()
        {
            JournalModel journalNote = new NotificationModel
            {
                UserId = SelectedClient.Id,
                Date = DateTime.Now,
                Message = NotificationMessage
            };

            ModelResourcesManager.GetInstance().GenerateJournalNote(journalNote);

            NotificationMessage = "";
            
            MessageBox.Show("Уведомление успешно отослано.");
        }

        #endregion

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(NotificationMessage) && NotificationMessage.Length < 10)
                {
                    return "Сообщение слишком короткое";
                }

                return null;
            }
        }
    }
}