using System.Windows;
using System.Windows.Controls;

namespace HarwexBank
{
    public static class ControlsBase
    {
        public static void CollapseVisibility_OnLoad(object sender, RoutedEventArgs routedEventArgs)
        {
            ((Border) sender).Visibility = Visibility.Collapsed;
        }
    }
}