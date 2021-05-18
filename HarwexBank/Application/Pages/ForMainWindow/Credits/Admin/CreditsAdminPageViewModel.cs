using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HarwexBank
{
    public class CreditTypesListViewModel : IControlViewModel
    {
        public string Name => "";
    }

    public class CreateNewCreditTypeViewModel : IControlViewModel
    {
        public string Name => "";
    }

    public class UpdateExistedCreditTypeViewModel : IControlViewModel
    {
        public string Name => "";
    }
    
    public class CreditsAdminPageViewModel : BaseControlViewModel, IControlViewModel
    {
        public string Name => "Кредиты";

        public CreditsAdminPageViewModel()
        {
            ExistedCreditTypes = ModelResourcesManager.GetInstance().ExistedCreditTypes;
            
            ControlViewModels.Add(new CreditTypesListViewModel());
            ControlViewModels.Add(new CreateNewCreditTypeViewModel());
            ControlViewModels.Add(new UpdateExistedCreditTypeViewModel());

            SelectedControlViewModel = ControlViewModels.FirstOrDefault();

            InitializeNewData();
        }

        public ObservableCollection<CreditTypeModel> ExistedCreditTypes { get; set; }
        public CreditTypeModel SelectedCreditType { get; set; }
        public CreditTypeModel CreditTypeToCreate { get; set; }

        private void InitializeNewData()
        {
            CreditTypeToCreate = new CreditTypeModel();
        }

        #region Commands

        // Fields.
        private ICommand _goToCreditTypesListCommand;
        private ICommand _goToCreateNewCreditTypeCommand;
        private ICommand _goToUpdateExistedCreditTypeCommand;
        
        private ICommand _selectCreditTypeCommand;
        private ICommand _saveUpdatesOfCreditTypeCommand;
        private ICommand _deleteCreditTypeCommand;
        
        private ICommand _createNewCreditTypeCommand;
        
        // Props.
        public ICommand GoToCreditTypesListCommand
        {
            get
            {
                _goToCreditTypesListCommand ??= new RelayCommand(
                    _ => GoToCreditTypesList());
        
                return _goToCreditTypesListCommand;
            }
        }

        public ICommand GoToCreateNewCreditTypeCommand
        {
            get
            {
                _goToCreateNewCreditTypeCommand ??= new RelayCommand(
                    _ => GoToCreateNewCreditType());
        
                return _goToCreateNewCreditTypeCommand;
            }
        }

        public ICommand GoToUpdateExistedCreditTypeCommand
        {
            get
            {
                _goToUpdateExistedCreditTypeCommand ??= new RelayCommand(
                    _ => GoToUpdateExistedCreditType());
        
                return _goToUpdateExistedCreditTypeCommand;
            }
        }
        
        public ICommand SelectCreditTypeCommand
        {
            get
            {
                _selectCreditTypeCommand ??= new RelayCommand(
                    c => SelectCreditType((CreditTypeModel) c),
                    c => c is CreditTypeModel);

                return _selectCreditTypeCommand;
            }
        }
        
        public ICommand SaveUpdatesOfCreditTypeCommand
        {
            get
            {
                _saveUpdatesOfCreditTypeCommand ??= new RelayCommand(
                    _ => SaveUpdatesOfCreditType());

                return _saveUpdatesOfCreditTypeCommand;
            }
        }
        
        public ICommand DeleteCreditTypeCommand
        {
            get
            {
                _deleteCreditTypeCommand ??= new RelayCommand(
                    _ => DeleteCreditType());

                return _deleteCreditTypeCommand;
            }
        }

        public ICommand CreateNewCreditTypeCommand
        {
            get
            {
                _createNewCreditTypeCommand ??= new RelayCommand(
                    _ => CreateNewCreditType());

                return _createNewCreditTypeCommand;
            }
        }

        // Methods.
        private void GoToCreditTypesList()
        {
            SelectedControlViewModel = ControlViewModels[0];
        }

        private void GoToCreateNewCreditType()
        {
            SelectedControlViewModel = ControlViewModels[1];
        }

        private void GoToUpdateExistedCreditType()
        {
            SelectedControlViewModel = ControlViewModels[2];
        }

        private void SelectCreditType(CreditTypeModel creditTypeModel)
        {
            SelectedCreditType = creditTypeModel;
            GoToUpdateExistedCreditType();
        }

        private void SaveUpdatesOfCreditType()
        {
            ModelResourcesManager.GetInstance().UpdateModel(SelectedCreditType);

            MessageBox.Show("Изменения успешно применены");
        }
        
        private void DeleteCreditType()
        {
            var boxResult = MessageBox.Show("Вы действительно хотите удалить данный вид кредита?",
                "HarwexBank", MessageBoxButton.YesNo);

            if (boxResult != MessageBoxResult.Yes) 
                return;
            
            ModelResourcesManager.GetInstance().RemoveModel(SelectedCreditType);
            ExistedCreditTypes.Remove(SelectedCreditType);
            
            GoToCreditTypesList();
        }

        private void CreateNewCreditType()
        {
            ModelResourcesManager.GetInstance().AddModel(CreditTypeToCreate);
            ExistedCreditTypes.Add(CreditTypeToCreate);
            InitializeNewData();

            GoToCreditTypesList();
        }

        #endregion
    }
}