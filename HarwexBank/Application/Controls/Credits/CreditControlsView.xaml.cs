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

        private void FrameworkElement_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (((Grid)sender).ActualWidth < 700)
            {
                if (((Grid) sender).ColumnDefinitions.Count == 5)
                    return;
                
                Grid.SetRow(((Grid)sender).Children[2], 1);
                Grid.SetRow(((Grid)sender).Children[3], 1);
                Grid.SetColumn(((Grid)sender).Children[2], 1);
                Grid.SetColumn(((Grid)sender).Children[3], 3);
                
                ((Grid) sender).ColumnDefinitions.RemoveAt(8);
                ((Grid) sender).ColumnDefinitions.RemoveAt(7);
                ((Grid) sender).ColumnDefinitions.RemoveAt(6);
                ((Grid) sender).ColumnDefinitions.RemoveAt(5);
            }
            else
            {
                if (((Grid) sender).ColumnDefinitions.Count == 9)
                    return;
                
                ((Grid) sender).ColumnDefinitions.Add(new ColumnDefinition
                    // ReSharper disable once PossibleNullReferenceException
                    {Width = (GridLength) new GridLengthConverter().ConvertFrom("Auto")});
                ((Grid) sender).ColumnDefinitions.Add(new ColumnDefinition
                    // ReSharper disable once PossibleNullReferenceException
                    {Width = (GridLength) new GridLengthConverter().ConvertFrom("*")});
                ((Grid) sender).ColumnDefinitions.Add(new ColumnDefinition
                    // ReSharper disable once PossibleNullReferenceException
                    {Width = (GridLength) new GridLengthConverter().ConvertFrom("Auto")});
                ((Grid) sender).ColumnDefinitions.Add(new ColumnDefinition
                    // ReSharper disable once PossibleNullReferenceException
                    {Width = (GridLength) new GridLengthConverter().ConvertFrom("*")});
                
                Grid.SetRow(((Grid)sender).Children[2], 0);
                Grid.SetRow(((Grid)sender).Children[3], 0);
                Grid.SetColumn(((Grid)sender).Children[2], 5);
                Grid.SetColumn(((Grid)sender).Children[3], 7);
            }
        }
    }
}