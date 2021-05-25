using System.Windows;
using System.Windows.Controls;

namespace HarwexBank
{
    public partial class AccountControlsView
    {
        void AccountAdditionalInfo_Loaded(object sender, RoutedEventArgs routedEventArgs)
        {
            ((Border) sender).Visibility = Visibility.Collapsed;
        }
    }
}