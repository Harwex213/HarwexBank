using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HarwexBank.Models
{
    public class UserType : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}