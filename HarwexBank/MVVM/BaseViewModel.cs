namespace HarwexBank
{
    public abstract class BaseViewModel : ObservableObject
    {
        public IControlViewModel ControlViewModelOwner { get; set; }
    }
}