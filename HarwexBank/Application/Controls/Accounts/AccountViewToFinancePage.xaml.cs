using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HarwexBank
{
    public partial class AccountViewToFinancePage : UserControl
    {
        public AccountViewToFinancePage()
        {
            InitializeComponent();
        }
        
        // public IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        // {
        //     if (depObj != null)
        //     {
        //         for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
        //         {
        //             DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
        //
        //             if (child != null && child is T)
        //                 yield return (T)child;
        //
        //             foreach (T childOfChild in FindVisualChildren<T>(child))
        //                 yield return childOfChild;
        //         }
        //     }
        // }
        //
        // private void AccountGeneralInfo_OnClick(object sender, RoutedEventArgs e)
        // {
        //     foreach (var checkBox in FindVisualChildren<CheckBox>(AccountsList))
        //     {
        //         if (checkBox.Name == "TrashCheckBox")
        //         {
        //             checkBox.IsChecked = !checkBox.IsChecked;
        //         }
        //     }
        // }
    }
}