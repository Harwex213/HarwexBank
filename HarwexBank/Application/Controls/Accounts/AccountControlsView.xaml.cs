using System.Windows;
using System.Windows.Controls;

namespace HarwexBank
{
    public partial class AccountControlsView
    {
        void AccountAdditionalInfo_Loaded(object sender, RoutedEventArgs routedEventArgs)
        {
            ControlsBase.CollapseVisibility_OnLoad(sender, routedEventArgs);
        }
        
        private void GeneralInfo_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (((Grid)sender).ActualWidth < 480)
            {
                if (((Grid) sender).ColumnDefinitions.Count == 5)
                    return;
                
                Grid.SetRow(((Grid)sender).Children[2], 1);
                Grid.SetColumn(((Grid)sender).Children[2], 1);
                ((FrameworkElement) ((Grid) sender).Children[2]).Margin = new Thickness(0, 8.5, 0, 0);
                
                Grid.SetRow(((Grid)sender).Children[3], 1);
                Grid.SetColumn(((Grid)sender).Children[3], 3);
                ((FrameworkElement) ((Grid) sender).Children[3]).Margin = new Thickness(0, 8.5, 0, 0);
                
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
                Grid.SetColumn(((Grid)sender).Children[2], 5);
                ((FrameworkElement) ((Grid) sender).Children[2]).Margin = new Thickness(0);
                
                Grid.SetRow(((Grid)sender).Children[3], 0);
                Grid.SetColumn(((Grid)sender).Children[3], 7);
                ((FrameworkElement) ((Grid) sender).Children[3]).Margin = new Thickness(0);
            }
        }

        private void CardsInfoBlock_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (((Grid)sender).ActualWidth < 460)
            {
                if (((Grid) sender).ColumnDefinitions.Count == 1)
                    return;
                
                Grid.SetRow(((Grid)sender).Children[1], 2);
                ((FrameworkElement) ((Grid) sender).Children[1]).Margin = new Thickness(0, 8.5, 0, 0);
                
                ((Grid) sender).ColumnDefinitions.RemoveAt(1);
            }
            else
            {
                if (((Grid) sender).ColumnDefinitions.Count == 2)
                    return;
                
                ((Grid) sender).ColumnDefinitions.Add(new ColumnDefinition
                    // ReSharper disable once PossibleNullReferenceException
                    {Width = (GridLength) new GridLengthConverter().ConvertFrom("Auto")});
                
                Grid.SetRow(((Grid)sender).Children[1], 1);
                ((FrameworkElement) ((Grid) sender).Children[1]).Margin = new Thickness(0);
            }
        }
    }
}