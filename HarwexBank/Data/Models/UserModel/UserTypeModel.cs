using System.Collections.Generic;

namespace HarwexBank
{
    public class UserTypeModel : ObservableObject
    {
        public UserTypeModel()
        {
            Users = new HashSet<UserModel>();
        }
        
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserModel> Users { get; set; }
    }
}