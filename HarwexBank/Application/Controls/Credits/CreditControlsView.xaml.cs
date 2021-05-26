using System.Windows;
using System.Windows.Controls;

namespace HarwexBank
{
    public partial class CreditControlsView_xaml
    {
        void CreditAdditionalInfo_Loaded(object sender, RoutedEventArgs routedEventArgs)
        {
            ControlsBase.CollapseVisibility_OnLoad(sender, routedEventArgs);
        }
    }
}