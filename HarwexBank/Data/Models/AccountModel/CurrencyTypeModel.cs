using System.Collections.Generic;

namespace HarwexBank
{
    public class CurrencyTypeModel : ObservableObject, ModelResourcesManager.IModel
    {
        public CurrencyTypeModel()
        {
            Accounts = new HashSet<AccountModel>();
            CreditTypes = new HashSet<CreditTypeModel>();
            OperationModels = new HashSet<OperationModel>();
        }

        private int _id;
        private string _name;

        public virtual ICollection<AccountModel> Accounts { get; set; }
        public virtual ICollection<CreditTypeModel> CreditTypes { get; set; }
        public virtual ICollection<OperationModel> OperationModels { get; set; }
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
    }
}